using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using bgs;
using Blizzard.T5.AssetManager;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.Streaming;
using UnityEngine;

public class AssetLoader : IAssetLoader, IService
{
	public delegate void ObjectCallback(AssetReference assetRef, UnityEngine.Object obj, object callbackData);

	public delegate void GameObjectCallback(AssetReference assetRef, GameObject go, object callbackData);

	private class InstantiatePrefabCallbackData<T>
	{
		public AssetReference requestedAssetRef;

		public AssetLoadingOptions callerOptions;

		public PrefabCallback<T> callerCallback;

		public object callerData;
	}

	public const string LOCAL_ASSET_BUNDLE_NAME = "local_asset_bundle.unity3d";

	private readonly Vector3 SPAWN_POS_CAMERA_OFFSET = new Vector3(0f, 0f, -5000f);

	private const int DEAD_HANDLES_CHECK_INTERVAL_FRAMES = 30;

	private List<GameObject> m_waitingOnObjects = new List<GameObject>();

	private int m_framesSinceLastDeadHandlesCheck;

	private IAssetManager m_assetManager;

	private IPrefabInstantiator m_prefabInstantiator;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		if (serviceLocator.Exists(typeof(GameDownloadManager), includeUninitialized: true))
		{
			yield return new WaitForGameDownloadManagerState();
		}
		IAssetBank assetBank = CreateAppropriateAssetBank();
		m_assetManager = new AssetManager(Log.Asset, assetBank);
		m_assetManager.OnAssetHandleOrphaned += SendAssetHandleOrphanedTelemetry;
		m_prefabInstantiator = new PrefabInstantiator(Log.Asset);
		m_prefabInstantiator.OnSharedPrefabHandleOrphaned += SendSharedPrefabHandleOrphanedTelemetry;
		serviceLocator.SetJobResultHandler<InstantiatePrefab>(OnInstantiatePrefabResultHandler);
		serviceLocator.SetJobResultHandler<LoadPrefab>(OnLoadAssetResultHandle<GameObject>);
		serviceLocator.SetJobResultHandler<LoadUIScreen>(OnInstantiatePrefabResultHandler);
		serviceLocator.SetJobResultHandler<LoadFontDef>(OnLoadAssetResultHandle<FontDefinition>);
		Processor.RegisterUpdateDelegate(Update);
	}

	public Type[] GetDependencies()
	{
		return null;
	}

	public void Shutdown()
	{
		Processor.UnregisterUpdateDelegate(Update);
	}

	public void Update()
	{
		m_assetManager.CheckPendingRequests();
		m_prefabInstantiator.ReleaseUnreferencedAssets();
		m_assetManager.ReleaseUnreferencedAssets();
		m_assetManager.CloseUnreferencedBundles();
		if (++m_framesSinceLastDeadHandlesCheck > 30)
		{
			m_prefabInstantiator.CheckForDeadHandles();
			m_assetManager.CheckForDeadHandles();
			m_framesSinceLastDeadHandlesCheck = 0;
		}
	}

	private void OnInstantiatePrefabResultHandler(IAsyncJobResult result)
	{
		InstantiatePrefab instantiatePrefab = result as InstantiatePrefab;
		if (instantiatePrefab != null)
		{
			AssetLoadingOptions options = ((!instantiatePrefab.UsePrefabPosition) ? AssetLoadingOptions.IgnorePrefabPosition : AssetLoadingOptions.None);
			InstantiatePrefab(instantiatePrefab.AssetRef, instantiatePrefab.OnPrefabInstantiated, null, options);
		}
	}

	private void OnLoadAssetResultHandle<T>(IAsyncJobResult result) where T : UnityEngine.Object
	{
		LoadAsset<T> loadAsset = result as LoadAsset<T>;
		if (loadAsset != null)
		{
			LoadAsset<T>(loadAsset.AssetRef, loadAsset.OnAssetLoaded);
		}
	}

	public static IAssetLoader Get()
	{
		return HearthstoneServices.Get<IAssetLoader>();
	}

	public bool IsWaitingOnObject(GameObject go)
	{
		if (!m_waitingOnObjects.Contains(go))
		{
			return m_prefabInstantiator.IsWaitingOnObject(go);
		}
		return true;
	}

	public bool IsSharedPrefabInstance(GameObject go)
	{
		return m_prefabInstantiator.IsSharedPrefabInstance(go);
	}

	private Asset GetAppropriateAsset(AssetReference assetRef, AssetLoadingOptions options)
	{
		AssetVariantTags.Quality quality = (options.HasFlag(AssetLoadingOptions.UseLowQuality) ? AssetVariantTags.Quality.Low : AssetVariantTags.Quality.Normal);
		bool disableLocalization = options.HasFlag(AssetLoadingOptions.DisableLocalization);
		return GetAppropriateAsset(assetRef, quality, disableLocalization);
	}

	private Asset GetAppropriateAsset(AssetReference assetRef, AssetVariantTags.Quality quality = AssetVariantTags.Quality.Normal, bool disableLocalization = false)
	{
		if (assetRef == null)
		{
			return null;
		}
		Locale locale = Locale.enUS;
		if (!disableLocalization)
		{
			locale = Localization.GetLocale();
			if (Network.IsRunning() && BattleNet.GetAccountCountry() == "CHN")
			{
				locale = Locale.zhCN;
			}
		}
		return GetAppropriateAssetInternal(assetRef, quality, locale);
	}

	private Asset GetAppropriateAssetInternal(AssetReference originalAssetRef, AssetVariantTags.Quality quality = AssetVariantTags.Quality.Normal, Locale locale = Locale.enUS)
	{
		if (originalAssetRef == null || originalAssetRef.guid == null)
		{
			Log.Asset.PrintError("Invalid assetRef: {0} is null\n{1}", (originalAssetRef == null) ? "assetRef" : "guid", new StackTrace());
			return null;
		}
		AssetVariantTags.Locale localeVariantTagForLocale = AssetVariantTags.GetLocaleVariantTagForLocale(locale);
		AssetVariantTags.Platform platform = (UniversalInputManager.UsePhoneUI ? AssetVariantTags.Platform.Phone : AssetVariantTags.Platform.Any);
		if (!AssetManifest.Get().TryResolveAsset(originalAssetRef.guid, out var resolvedGuid, out var _, localeVariantTagForLocale, quality, platform))
		{
			Log.Asset.PrintWarning("[AssetLoader.GetAppropriateAssetInternal] Unable to find {0} in asset manifest.", originalAssetRef);
			return null;
		}
		if (IsAssetWithGuidAvailable(resolvedGuid))
		{
			return new Asset(resolvedGuid);
		}
		Log.Asset.PrintWarning("[AssetLoader.GetAppropriateAssetInternal] Appropriate asset {0} to original asset {1} is not available. quality={2}, locale={3}({4}), platform={5}", resolvedGuid, originalAssetRef, quality, locale, localeVariantTagForLocale, platform);
		return null;
	}

	public bool LoadMaterial(AssetReference assetRef, ObjectCallback callback, object callbackData = null, bool persistent = false, bool disableLocalization = false)
	{
		return LoadObject<Material>(assetRef, callback, callbackData, persistent, disableLocalization);
	}

	public Material LoadMaterial(AssetReference assetRef, bool persistent = false, bool disableLocalization = false)
	{
		Asset appropriateAsset = GetAppropriateAsset(assetRef, AssetVariantTags.Quality.Normal, disableLocalization);
		return LoadObjectImmediately<Material>(assetRef, appropriateAsset);
	}

	public bool LoadTexture(AssetReference assetRef, ObjectCallback callback, object callbackData = null, bool persistent = false, bool disableLocalization = false)
	{
		return LoadObject<Texture>(assetRef, callback, callbackData, persistent, disableLocalization);
	}

	public Texture LoadTexture(AssetReference assetRef, bool persistent = false, bool disableLocalization = false)
	{
		Log.AsyncLoading.PrintWarning("warning CS0618: `LoadTexture(Asset, bool, bool)' is obsolete: from now on, always use async loading instead (i.e. LoadTexture with callback).");
		if (assetRef == null)
		{
			Error.AddDevFatal("AssetLoader.LoadTexture() - An asset request was made but no file name was given.");
			return null;
		}
		Asset appropriateAsset = GetAppropriateAsset(assetRef, AssetVariantTags.Quality.Normal, disableLocalization);
		return LoadObjectImmediately<Texture>(assetRef, appropriateAsset);
	}

	public bool LoadMesh(AssetReference assetRef, ObjectCallback callback, object callbackData = null, bool persistent = false, bool disableLocalization = false)
	{
		ObjectCallback callback2 = delegate(AssetReference meshAssetRef, UnityEngine.Object meshObj, object meshCallbackData)
		{
			GameObject gameObject = meshObj as GameObject;
			MeshFilter meshFilter = ((gameObject != null) ? gameObject.GetComponent<MeshFilter>() : null);
			Mesh obj = ((meshFilter != null) ? meshFilter.sharedMesh : null);
			callback(meshAssetRef, obj, meshCallbackData);
		};
		return LoadObject<GameObject>(assetRef, callback2, callbackData, persistent, disableLocalization);
	}

	public Mesh LoadMesh(AssetReference assetRef, bool persistent = false, bool disableLocalization = false)
	{
		Asset appropriateAsset = GetAppropriateAsset(assetRef, AssetVariantTags.Quality.Normal, disableLocalization);
		GameObject gameObject = LoadObjectImmediately<GameObject>(assetRef, appropriateAsset);
		MeshFilter meshFilter = ((gameObject != null) ? gameObject.GetComponent<MeshFilter>() : null);
		if (!(meshFilter != null))
		{
			return null;
		}
		return meshFilter.sharedMesh;
	}

	public bool LoadGameObject(AssetReference assetRef, GameObjectCallback callback, object callbackData = null, bool persistent = false, bool autoInstantiateOnLoad = true, bool usePrefabPosition = true)
	{
		return LoadPrefab(assetRef, usePrefabPosition, callback, callbackData, persistent, null, autoInstantiateOnLoad);
	}

	[Obsolete("from now on, always use async loading instead (i.e. LoadUberAnimation with callback).")]
	public UberShaderAnimation LoadUberAnimation(AssetReference assetRef, bool usePrefabPosition = true, bool persistent = false)
	{
		Asset appropriateAsset = GetAppropriateAsset(assetRef);
		return LoadObjectImmediately<UberShaderAnimation>(assetRef, appropriateAsset);
	}

	public bool LoadUberAnimation(AssetReference assetRef, ObjectCallback callback, object callbackData = null, bool persistent = false)
	{
		return LoadObject<UberShaderAnimation>(assetRef, callback, callbackData, persistent);
	}

	private IAssetBank CreateAppropriateAssetBank()
	{
		IAssetBank assetBank = null;
		assetBank = CreateAssetBundlesAssetBank();
		if (Vars.Key("Application.CachingAssetBankEnabled").GetBool(def: false))
		{
			assetBank = new CachingAssetBank(Log.Asset, assetBank);
		}
		return assetBank;
	}

	private IAssetBank CreateAssetBundlesAssetBank()
	{
		string assetBundlePath = AssetBundleInfo.GetAssetBundlePath(ScriptableAssetManifest.MainManifestBundleName);
		if (!File.Exists(assetBundlePath))
		{
			Log.Asset.PrintError("Cannot find asset bundle for AssetBundleDependencyGraph '{0}', editor {1}, playing {2}", assetBundlePath, Application.isEditor, Application.isPlaying);
			throw new ApplicationException("Could not initialize AssetLoader: missing AssetBundleDependencyGraph");
		}
		AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
		if (assetBundle == null)
		{
			Log.Asset.PrintError("Failed to load bundle for AssetBundleDependencyGraph '{0}', editor {1}, playing {2}", assetBundlePath, Application.isEditor, Application.isPlaying);
			throw new ApplicationException("Could not initialize AssetLoader: failed to load bundle with AssetBundleDependencyGraph");
		}
		AssetBundleDependencyGraph assetBundleDependencyGraph = assetBundle.LoadAsset<AssetBundleDependencyGraph>(ScriptableAssetManifest.BundleDepsAssetPath);
		assetBundle.Unload(unloadAllLoadedObjects: false);
		if (assetBundleDependencyGraph == null)
		{
			Log.Asset.PrintError("Failed to load '{0}' from bundle '{1}'", ScriptableAssetManifest.BundleDepsAssetPath, assetBundlePath);
			throw new ApplicationException("Could not initialize AssetLoader: failed to load AssetBundleDependencyGraph");
		}
		IAssetLocator assetLocator = new AssetLocator(AssetManifest.Get());
		return new AssetBundleAssetBank(Log.Asset, assetLocator, assetBundleDependencyGraph);
	}

	private bool LoadObject<T>(AssetReference assetRef, ObjectCallback callback, object callbackData, bool persistent = false, bool disableLocalization = false) where T : UnityEngine.Object
	{
		Asset asset = GetAppropriateAsset(assetRef, AssetVariantTags.Quality.Normal, disableLocalization);
		IAsyncAssetRequest<T> asyncAssetRequest = m_assetManager.LoadAsync<T>(asset?.GetGuid(), trackHandle: false);
		asyncAssetRequest.OnCompleted = (AssetLoadedCB<T>)Delegate.Combine(asyncAssetRequest.OnCompleted, (AssetLoadedCB<T>)delegate(IAsyncAssetRequest<T> completedRequest)
		{
			OnAssetLoaded(assetRef, asset?.GetGuid(), completedRequest.Result);
			callback(assetRef, completedRequest.Result.Asset, callbackData);
		});
		return true;
	}

	private bool LoadPrefab(AssetReference assetRef, bool usePrefabPosition, GameObjectCallback callback, object callbackData, bool persistent = false, UnityEngine.Object fallback = null, bool autoInstantiateOnLoad = true)
	{
		Asset appropriateAsset = GetAppropriateAsset(assetRef);
		LoadPrefabInternal(appropriateAsset, assetRef, usePrefabPosition, callback, callbackData, fallback, autoInstantiateOnLoad);
		if (appropriateAsset != null)
		{
			return IsAssetWithGuidAvailable(appropriateAsset.GetGuid());
		}
		return false;
	}

	private GameObject TryGetAsGameObject(string guid, UnityEngine.Object obj)
	{
		GameObject gameObject = obj as GameObject;
		if (!gameObject)
		{
			string text = GameStrings.Format("GLOBAL_ERROR_ASSET_INCORRECT_DATA", guid);
			UnityEngine.Debug.LogError($"AssetLoader.WaitThenCallGameObjectCallback() - {text} (prefab={obj})");
			Error.AddFatal(FatalErrorReason.ASSET_INCORRECT_DATA, text);
		}
		return gameObject;
	}

	private void LoadPrefabInternal(Asset assetToLoad, AssetReference requestedReference, bool usePrefabPosition, GameObjectCallback callback, object callbackData, UnityEngine.Object fallback, bool autoInstantiateOnLoad)
	{
		string guid = assetToLoad?.GetGuid();
		if (guid == null)
		{
			if (GeneralUtils.IsCallbackValid(callback))
			{
				callback(null, null, callbackData);
			}
			return;
		}
		IAsyncAssetRequest<GameObject> asyncAssetRequest = m_assetManager.LoadAsync<GameObject>(assetToLoad.GetGuid(), trackHandle: false);
		asyncAssetRequest.OnCompleted = (AssetLoadedCB<GameObject>)Delegate.Combine(asyncAssetRequest.OnCompleted, (AssetLoadedCB<GameObject>)delegate(IAsyncAssetRequest<GameObject> completedRequest)
		{
			OnAssetLoaded(requestedReference, guid, completedRequest.Result);
			GameObject asset = completedRequest.Result.Asset;
			GameObject gameObject = TryGetAsGameObject(guid, asset);
			if (autoInstantiateOnLoad)
			{
				Processor.RunCoroutine(InstantiateAndWaitThenCallGameObjectCallback(requestedReference, gameObject, usePrefabPosition, callback, callbackData));
			}
			else if (GeneralUtils.IsCallbackValid(callback))
			{
				callback(requestedReference, gameObject, callbackData);
			}
		});
	}

	private void OnAssetLoaded<T>(AssetReference requestedAsset, string resolvedGuid, AssetHandle<T> loadedAsset) where T : UnityEngine.Object
	{
		if (loadedAsset == null)
		{
			LogMissingAsset(requestedAsset, resolvedGuid, typeof(T).Name);
		}
	}

	public bool IsAssetAvailable(AssetReference assetRef)
	{
		if (assetRef == null)
		{
			return false;
		}
		return IsAssetWithGuidAvailable(assetRef.guid);
	}

	public bool IsAppropriateVariantAvailable(AssetReference assetRef, AssetLoadingOptions options)
	{
		return IsAssetWithGuidAvailable(GetAppropriateAsset(assetRef, options)?.GetGuid());
	}

	private bool IsAssetWithGuidAvailable(string assetGuid)
	{
		return GameDownloadManagerProvider.Get()?.IsAssetDownloaded(assetGuid) ?? false;
	}

	public AssetHandle<T> LoadAsset<T>(AssetReference requestedAsset, AssetLoadingOptions options = AssetLoadingOptions.None) where T : UnityEngine.Object
	{
		Asset appropriateAsset = GetAppropriateAsset(requestedAsset, options);
		if (appropriateAsset == null)
		{
			return null;
		}
		AssetHandle<T> assetHandle = m_assetManager.Load<T>(appropriateAsset.GetGuid(), trackHandle: true);
		OnAssetLoaded(requestedAsset, appropriateAsset.GetGuid(), assetHandle);
		return assetHandle;
	}

	public bool LoadAsset<T>(ref AssetHandle<T> assetHandle, AssetReference assetRef, AssetLoadingOptions options = AssetLoadingOptions.None) where T : UnityEngine.Object
	{
		AssetHandle<T> assetHandle2 = LoadAsset<T>(assetRef, options);
		AssetHandle.SafeDispose(ref assetHandle);
		assetHandle = assetHandle2;
		return assetHandle;
	}

	public bool LoadAsset<T>(AssetReference assetRef, AssetHandleCallback<T> callback, object callbackData = null, AssetLoadingOptions options = AssetLoadingOptions.None) where T : UnityEngine.Object
	{
		Asset appropriateAsset = GetAppropriateAsset(assetRef, options);
		if (appropriateAsset == null)
		{
			if (GeneralUtils.IsCallbackValid(callback))
			{
				callback(null, null, callbackData);
			}
			return false;
		}
		IAsyncAssetRequest<T> asyncAssetRequest = m_assetManager.LoadAsync<T>(appropriateAsset.GetGuid(), trackHandle: true);
		asyncAssetRequest.OnCompleted = (AssetLoadedCB<T>)Delegate.Combine(asyncAssetRequest.OnCompleted, (AssetLoadedCB<T>)delegate(IAsyncAssetRequest<T> completedRequest)
		{
			OnAssetLoaded(assetRef, appropriateAsset.GetGuid(), completedRequest.Result);
			if (GeneralUtils.IsCallbackValid(callback))
			{
				callback(assetRef, completedRequest.Result, callbackData);
			}
			else
			{
				completedRequest.Result?.Dispose();
			}
		});
		return true;
	}

	public GameObject InstantiatePrefab(AssetReference assetRef, AssetLoadingOptions options)
	{
		using AssetHandle<GameObject> prefabAsset = LoadAsset<GameObject>(assetRef, options);
		return m_prefabInstantiator.InstantiatePrefab(prefabAsset, options);
	}

	public bool InstantiatePrefab(AssetReference assetRef, PrefabCallback<GameObject> callback, object callbackData, AssetLoadingOptions options)
	{
		InstantiatePrefabCallbackData<GameObject> callbackData2 = new InstantiatePrefabCallbackData<GameObject>
		{
			callerCallback = callback,
			callerData = callbackData,
			callerOptions = options,
			requestedAssetRef = assetRef
		};
		return LoadAsset<GameObject>(assetRef, OnPrefabLoaded, callbackData2, options);
	}

	public AssetHandle<GameObject> GetOrInstantiateSharedPrefab(AssetReference assetRef, AssetLoadingOptions options = AssetLoadingOptions.None)
	{
		using AssetHandle<GameObject> prefabAsset = LoadAsset<GameObject>(assetRef, options);
		return m_prefabInstantiator.GetOrInstantiateSharedPrefab(prefabAsset, options);
	}

	public bool GetOrInstantiateSharedPrefab(AssetReference assetRef, PrefabCallback<AssetHandle<GameObject>> callback, object callbackData = null, AssetLoadingOptions options = AssetLoadingOptions.None)
	{
		InstantiatePrefabCallbackData<AssetHandle<GameObject>> callbackData2 = new InstantiatePrefabCallbackData<AssetHandle<GameObject>>
		{
			callerCallback = callback,
			callerData = callbackData,
			callerOptions = options,
			requestedAssetRef = assetRef
		};
		return LoadAsset<GameObject>(assetRef, OnSharedPrefabLoaded, callbackData2, options);
	}

	private void OnPrefabLoaded(AssetReference prefabRef, AssetHandle<GameObject> prefabHandle, object callbackData)
	{
		using (prefabHandle)
		{
			InstantiatePrefabCallbackData<GameObject> instantiatePrefabCallbackData = callbackData as InstantiatePrefabCallbackData<GameObject>;
			if (!prefabHandle)
			{
				if (GeneralUtils.IsCallbackValid(instantiatePrefabCallbackData.callerCallback))
				{
					instantiatePrefabCallbackData.callerCallback(prefabRef, null, instantiatePrefabCallbackData.callerData);
				}
			}
			else
			{
				m_prefabInstantiator.InstantiatePrefab(prefabHandle, OnPrefabInstantiated, instantiatePrefabCallbackData, instantiatePrefabCallbackData.callerOptions);
			}
		}
	}

	private void OnSharedPrefabLoaded(AssetReference prefabRef, AssetHandle<GameObject> prefabHandle, object callbackData)
	{
		InstantiatePrefabCallbackData<AssetHandle<GameObject>> instantiatePrefabCallbackData = callbackData as InstantiatePrefabCallbackData<AssetHandle<GameObject>>;
		if (!prefabHandle)
		{
			if (GeneralUtils.IsCallbackValid(instantiatePrefabCallbackData.callerCallback))
			{
				instantiatePrefabCallbackData.callerCallback(prefabRef, null, instantiatePrefabCallbackData.callerData);
			}
		}
		else
		{
			m_prefabInstantiator.GetOrInstantiateSharedPrefab(prefabHandle, OnPrefabInstantiated, instantiatePrefabCallbackData, instantiatePrefabCallbackData.callerOptions);
		}
	}

	private void OnPrefabInstantiated<T>(string prefabAddress, T instance, object callbackData)
	{
		InstantiatePrefabCallbackData<T> instantiatePrefabCallbackData = callbackData as InstantiatePrefabCallbackData<T>;
		if (GeneralUtils.IsCallbackValid(instantiatePrefabCallbackData.callerCallback))
		{
			instantiatePrefabCallbackData.callerCallback(instantiatePrefabCallbackData.requestedAssetRef, instance, instantiatePrefabCallbackData.callerData);
		}
	}

	[Obsolete("from now on, always use async loading instead (i.e. LoadGameObject with callback).")]
	private GameObject LoadGameObjectImmediately(AssetReference requestedAsset, Asset asset, bool usePrefabPosition, GameObject fallback = null, bool autoInstantiate = true)
	{
		GameObject gameObject = LoadObjectImmediately<GameObject>(requestedAsset, asset);
		if (!gameObject)
		{
			if (fallback == null)
			{
				return null;
			}
			gameObject = fallback;
		}
		if (autoInstantiate)
		{
			if (!usePrefabPosition)
			{
				return UnityEngine.Object.Instantiate(gameObject, NewGameObjectSpawnPosition(), gameObject.transform.rotation);
			}
			return UnityEngine.Object.Instantiate(gameObject);
		}
		return gameObject;
	}

	private T LoadObjectImmediately<T>(AssetReference requestedAsset, Asset resolvedAsset) where T : UnityEngine.Object
	{
		string text = resolvedAsset?.GetGuid();
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		AssetHandle<T> assetHandle = m_assetManager.Load<T>(text, trackHandle: false);
		OnAssetLoaded(requestedAsset, text, assetHandle);
		return assetHandle.Asset;
	}

	private IEnumerator InstantiateAndWaitThenCallGameObjectCallback(AssetReference assetRef, GameObject prefab, bool usePrefabPosition, GameObjectCallback callback, object callbackData)
	{
		if (prefab == null)
		{
			if (GeneralUtils.IsCallbackValid(callback))
			{
				callback(assetRef, null, callbackData);
			}
			yield break;
		}
		GameObject instance = (usePrefabPosition ? UnityEngine.Object.Instantiate(prefab) : UnityEngine.Object.Instantiate(prefab, NewGameObjectSpawnPosition(), prefab.transform.rotation));
		m_waitingOnObjects.Add(instance);
		yield return new WaitForEndOfFrame();
		m_waitingOnObjects.Remove(instance);
		if (GeneralUtils.IsCallbackValid(callback))
		{
			callback(assetRef, instance, callbackData);
		}
	}

	private Vector3 NewGameObjectSpawnPosition()
	{
		if (Camera.main == null)
		{
			return Vector3.zero;
		}
		return Camera.main.transform.position + SPAWN_POS_CAMERA_OFFSET;
	}

	private static void LogMissingAsset(AssetReference requestedAsset, string resolvedGuid, string assetType)
	{
		SendMissingAssetTelemetry(requestedAsset, resolvedGuid, assetType);
		Log.MissingAssets.PrintError($"{assetType} {requestedAsset?.GetLegacyAssetName()} not found");
	}

	private static void SendMissingAssetTelemetry(AssetReference requestedAsset, string resolvedGuid, string assetType)
	{
		if (string.IsNullOrEmpty(requestedAsset?.guid))
		{
			Log.Telemetry.Print("Missing asset was found, but there was not way to identify it.  No telemetry will be sent.");
		}
		else if (Application.isEditor)
		{
			Log.Telemetry.Print("Missing asset found in editor - not sending missing asset telemetry for requestedGuid={0}, resolvedGuid={1}, name={2}", requestedAsset.guid, resolvedGuid, requestedAsset.GetLegacyAssetName());
		}
		else
		{
			TelemetryManager.Client().SendAssetNotFound(assetType, requestedAsset.guid, resolvedGuid, requestedAsset.GetLegacyAssetName());
		}
	}

	private static void SendSharedPrefabHandleOrphanedTelemetry(string asset, string owner)
	{
		TelemetryManager.Client().SendAssetOrphaned(asset ?? string.Empty, owner ?? string.Empty, "prefab_instance");
	}

	private static void SendAssetHandleOrphanedTelemetry(string asset, string owner)
	{
		TelemetryManager.Client().SendAssetOrphaned(asset ?? string.Empty, owner ?? string.Empty, "asset");
	}

	public void PopulateDebugStats(AssetManagerDebugStats stats, AssetManagerDebugStats.DataFields requestedFields)
	{
		m_assetManager.PopulateDebugStats(stats, requestedFields);
	}
}
