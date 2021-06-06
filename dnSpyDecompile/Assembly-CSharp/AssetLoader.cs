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

// Token: 0x0200084E RID: 2126
public class AssetLoader : IAssetLoader, IService
{
	// Token: 0x0600732C RID: 29484 RVA: 0x00250C68 File Offset: 0x0024EE68
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		if (serviceLocator.Exists(typeof(GameDownloadManager), true))
		{
			yield return new WaitForGameDownloadManagerState();
		}
		IAssetBank assetBank = this.CreateAppropriateAssetBank();
		this.m_assetManager = new AssetManager(global::Log.Asset, assetBank);
		this.m_assetManager.OnAssetHandleOrphaned += AssetLoader.SendAssetHandleOrphanedTelemetry;
		this.m_prefabInstantiator = new PrefabInstantiator(global::Log.Asset);
		this.m_prefabInstantiator.OnSharedPrefabHandleOrphaned += AssetLoader.SendSharedPrefabHandleOrphanedTelemetry;
		serviceLocator.SetJobResultHandler<InstantiatePrefab>(new Action<IAsyncJobResult>(this.OnInstantiatePrefabResultHandler));
		serviceLocator.SetJobResultHandler<LoadPrefab>(new Action<IAsyncJobResult>(this.OnLoadAssetResultHandle<GameObject>));
		serviceLocator.SetJobResultHandler<LoadUIScreen>(new Action<IAsyncJobResult>(this.OnInstantiatePrefabResultHandler));
		serviceLocator.SetJobResultHandler<LoadFontDef>(new Action<IAsyncJobResult>(this.OnLoadAssetResultHandle<FontDefinition>));
		Processor.RegisterUpdateDelegate(new Action(this.Update));
		yield break;
	}

	// Token: 0x0600732D RID: 29485 RVA: 0x00090064 File Offset: 0x0008E264
	public Type[] GetDependencies()
	{
		return null;
	}

	// Token: 0x0600732E RID: 29486 RVA: 0x00250C7E File Offset: 0x0024EE7E
	public void Shutdown()
	{
		Processor.UnregisterUpdateDelegate(new Action(this.Update));
	}

	// Token: 0x0600732F RID: 29487 RVA: 0x00250C94 File Offset: 0x0024EE94
	public void Update()
	{
		this.m_assetManager.CheckPendingRequests();
		this.m_prefabInstantiator.ReleaseUnreferencedAssets();
		this.m_assetManager.ReleaseUnreferencedAssets();
		this.m_assetManager.CloseUnreferencedBundles();
		int num = this.m_framesSinceLastDeadHandlesCheck + 1;
		this.m_framesSinceLastDeadHandlesCheck = num;
		if (num > 30)
		{
			this.m_prefabInstantiator.CheckForDeadHandles();
			this.m_assetManager.CheckForDeadHandles();
			this.m_framesSinceLastDeadHandlesCheck = 0;
		}
	}

	// Token: 0x06007330 RID: 29488 RVA: 0x00250D00 File Offset: 0x0024EF00
	private void OnInstantiatePrefabResultHandler(IAsyncJobResult result)
	{
		InstantiatePrefab instantiatePrefab = result as InstantiatePrefab;
		if (instantiatePrefab != null)
		{
			AssetLoadingOptions options = instantiatePrefab.UsePrefabPosition ? AssetLoadingOptions.None : AssetLoadingOptions.IgnorePrefabPosition;
			this.InstantiatePrefab(instantiatePrefab.AssetRef, new PrefabCallback<GameObject>(instantiatePrefab.OnPrefabInstantiated), null, options);
		}
	}

	// Token: 0x06007331 RID: 29489 RVA: 0x00250D40 File Offset: 0x0024EF40
	private void OnLoadAssetResultHandle<T>(IAsyncJobResult result) where T : UnityEngine.Object
	{
		LoadAsset<T> loadAsset = result as LoadAsset<T>;
		if (loadAsset != null)
		{
			this.LoadAsset<T>(loadAsset.AssetRef, new AssetHandleCallback<T>(loadAsset.OnAssetLoaded), null, AssetLoadingOptions.None);
		}
	}

	// Token: 0x06007332 RID: 29490 RVA: 0x00250D72 File Offset: 0x0024EF72
	public static IAssetLoader Get()
	{
		return HearthstoneServices.Get<IAssetLoader>();
	}

	// Token: 0x06007333 RID: 29491 RVA: 0x00250D79 File Offset: 0x0024EF79
	public bool IsWaitingOnObject(GameObject go)
	{
		return this.m_waitingOnObjects.Contains(go) || this.m_prefabInstantiator.IsWaitingOnObject(go);
	}

	// Token: 0x06007334 RID: 29492 RVA: 0x00250D97 File Offset: 0x0024EF97
	public bool IsSharedPrefabInstance(GameObject go)
	{
		return this.m_prefabInstantiator.IsSharedPrefabInstance(go);
	}

	// Token: 0x06007335 RID: 29493 RVA: 0x00250DA8 File Offset: 0x0024EFA8
	private Asset GetAppropriateAsset(AssetReference assetRef, AssetLoadingOptions options)
	{
		AssetVariantTags.Quality quality = options.HasFlag(AssetLoadingOptions.UseLowQuality) ? AssetVariantTags.Quality.Low : AssetVariantTags.Quality.Normal;
		bool disableLocalization = options.HasFlag(AssetLoadingOptions.DisableLocalization);
		return this.GetAppropriateAsset(assetRef, quality, disableLocalization);
	}

	// Token: 0x06007336 RID: 29494 RVA: 0x00250DE8 File Offset: 0x0024EFE8
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
		return this.GetAppropriateAssetInternal(assetRef, quality, locale);
	}

	// Token: 0x06007337 RID: 29495 RVA: 0x00250E2C File Offset: 0x0024F02C
	private Asset GetAppropriateAssetInternal(AssetReference originalAssetRef, AssetVariantTags.Quality quality = AssetVariantTags.Quality.Normal, Locale locale = Locale.enUS)
	{
		if (originalAssetRef == null || originalAssetRef.guid == null)
		{
			global::Log.Asset.PrintError("Invalid assetRef: {0} is null\n{1}", new object[]
			{
				(originalAssetRef == null) ? "assetRef" : "guid",
				new StackTrace()
			});
			return null;
		}
		AssetVariantTags.Locale localeVariantTagForLocale = AssetVariantTags.GetLocaleVariantTagForLocale(locale);
		AssetVariantTags.Platform platform = UniversalInputManager.UsePhoneUI ? AssetVariantTags.Platform.Phone : AssetVariantTags.Platform.Any;
		string text;
		string text2;
		if (!AssetManifest.Get().TryResolveAsset(originalAssetRef.guid, out text, out text2, localeVariantTagForLocale, quality, platform))
		{
			global::Log.Asset.PrintWarning("[AssetLoader.GetAppropriateAssetInternal] Unable to find {0} in asset manifest.", new object[]
			{
				originalAssetRef
			});
			return null;
		}
		if (this.IsAssetWithGuidAvailable(text))
		{
			return new Asset(text);
		}
		global::Log.Asset.PrintWarning("[AssetLoader.GetAppropriateAssetInternal] Appropriate asset {0} to original asset {1} is not available. quality={2}, locale={3}({4}), platform={5}", new object[]
		{
			text,
			originalAssetRef,
			quality,
			locale,
			localeVariantTagForLocale,
			platform
		});
		return null;
	}

	// Token: 0x06007338 RID: 29496 RVA: 0x00250F13 File Offset: 0x0024F113
	public bool LoadMaterial(AssetReference assetRef, AssetLoader.ObjectCallback callback, object callbackData = null, bool persistent = false, bool disableLocalization = false)
	{
		return this.LoadObject<Material>(assetRef, callback, callbackData, persistent, disableLocalization);
	}

	// Token: 0x06007339 RID: 29497 RVA: 0x00250F24 File Offset: 0x0024F124
	public Material LoadMaterial(AssetReference assetRef, bool persistent = false, bool disableLocalization = false)
	{
		Asset appropriateAsset = this.GetAppropriateAsset(assetRef, AssetVariantTags.Quality.Normal, disableLocalization);
		return this.LoadObjectImmediately<Material>(assetRef, appropriateAsset);
	}

	// Token: 0x0600733A RID: 29498 RVA: 0x00250F43 File Offset: 0x0024F143
	public bool LoadTexture(AssetReference assetRef, AssetLoader.ObjectCallback callback, object callbackData = null, bool persistent = false, bool disableLocalization = false)
	{
		return this.LoadObject<Texture>(assetRef, callback, callbackData, persistent, disableLocalization);
	}

	// Token: 0x0600733B RID: 29499 RVA: 0x00250F54 File Offset: 0x0024F154
	public Texture LoadTexture(AssetReference assetRef, bool persistent = false, bool disableLocalization = false)
	{
		global::Log.AsyncLoading.PrintWarning("warning CS0618: `LoadTexture(Asset, bool, bool)' is obsolete: from now on, always use async loading instead (i.e. LoadTexture with callback).", Array.Empty<object>());
		if (assetRef == null)
		{
			global::Error.AddDevFatal("AssetLoader.LoadTexture() - An asset request was made but no file name was given.", Array.Empty<object>());
			return null;
		}
		Asset appropriateAsset = this.GetAppropriateAsset(assetRef, AssetVariantTags.Quality.Normal, disableLocalization);
		return this.LoadObjectImmediately<Texture>(assetRef, appropriateAsset);
	}

	// Token: 0x0600733C RID: 29500 RVA: 0x00250F9C File Offset: 0x0024F19C
	public bool LoadMesh(AssetReference assetRef, AssetLoader.ObjectCallback callback, object callbackData = null, bool persistent = false, bool disableLocalization = false)
	{
		AssetLoader.ObjectCallback callback2 = delegate(AssetReference meshAssetRef, UnityEngine.Object meshObj, object meshCallbackData)
		{
			GameObject gameObject = meshObj as GameObject;
			MeshFilter meshFilter = (gameObject != null) ? gameObject.GetComponent<MeshFilter>() : null;
			Mesh obj = (meshFilter != null) ? meshFilter.sharedMesh : null;
			callback(meshAssetRef, obj, meshCallbackData);
		};
		return this.LoadObject<GameObject>(assetRef, callback2, callbackData, persistent, disableLocalization);
	}

	// Token: 0x0600733D RID: 29501 RVA: 0x00250FD0 File Offset: 0x0024F1D0
	public Mesh LoadMesh(AssetReference assetRef, bool persistent = false, bool disableLocalization = false)
	{
		Asset appropriateAsset = this.GetAppropriateAsset(assetRef, AssetVariantTags.Quality.Normal, disableLocalization);
		GameObject gameObject = this.LoadObjectImmediately<GameObject>(assetRef, appropriateAsset);
		MeshFilter meshFilter = (gameObject != null) ? gameObject.GetComponent<MeshFilter>() : null;
		if (!(meshFilter != null))
		{
			return null;
		}
		return meshFilter.sharedMesh;
	}

	// Token: 0x0600733E RID: 29502 RVA: 0x00251014 File Offset: 0x0024F214
	public bool LoadGameObject(AssetReference assetRef, AssetLoader.GameObjectCallback callback, object callbackData = null, bool persistent = false, bool autoInstantiateOnLoad = true, bool usePrefabPosition = true)
	{
		return this.LoadPrefab(assetRef, usePrefabPosition, callback, callbackData, persistent, null, autoInstantiateOnLoad);
	}

	// Token: 0x0600733F RID: 29503 RVA: 0x00251028 File Offset: 0x0024F228
	[Obsolete("from now on, always use async loading instead (i.e. LoadUberAnimation with callback).")]
	public UberShaderAnimation LoadUberAnimation(AssetReference assetRef, bool usePrefabPosition = true, bool persistent = false)
	{
		Asset appropriateAsset = this.GetAppropriateAsset(assetRef, AssetVariantTags.Quality.Normal, false);
		return this.LoadObjectImmediately<UberShaderAnimation>(assetRef, appropriateAsset);
	}

	// Token: 0x06007340 RID: 29504 RVA: 0x00251047 File Offset: 0x0024F247
	public bool LoadUberAnimation(AssetReference assetRef, AssetLoader.ObjectCallback callback, object callbackData = null, bool persistent = false)
	{
		return this.LoadObject<UberShaderAnimation>(assetRef, callback, callbackData, persistent, false);
	}

	// Token: 0x06007341 RID: 29505 RVA: 0x00251058 File Offset: 0x0024F258
	private IAssetBank CreateAppropriateAssetBank()
	{
		IAssetBank assetBank = this.CreateAssetBundlesAssetBank();
		if (Vars.Key("Application.CachingAssetBankEnabled").GetBool(false))
		{
			assetBank = new CachingAssetBank(global::Log.Asset, assetBank);
		}
		return assetBank;
	}

	// Token: 0x06007342 RID: 29506 RVA: 0x00251090 File Offset: 0x0024F290
	private IAssetBank CreateAssetBundlesAssetBank()
	{
		string assetBundlePath = AssetBundleInfo.GetAssetBundlePath(ScriptableAssetManifest.MainManifestBundleName);
		if (!File.Exists(assetBundlePath))
		{
			global::Log.Asset.PrintError("Cannot find asset bundle for AssetBundleDependencyGraph '{0}', editor {1}, playing {2}", new object[]
			{
				assetBundlePath,
				Application.isEditor,
				Application.isPlaying
			});
			throw new ApplicationException("Could not initialize AssetLoader: missing AssetBundleDependencyGraph");
		}
		AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
		if (assetBundle == null)
		{
			global::Log.Asset.PrintError("Failed to load bundle for AssetBundleDependencyGraph '{0}', editor {1}, playing {2}", new object[]
			{
				assetBundlePath,
				Application.isEditor,
				Application.isPlaying
			});
			throw new ApplicationException("Could not initialize AssetLoader: failed to load bundle with AssetBundleDependencyGraph");
		}
		AssetBundleDependencyGraph assetBundleDependencyGraph = assetBundle.LoadAsset<AssetBundleDependencyGraph>(ScriptableAssetManifest.BundleDepsAssetPath);
		assetBundle.Unload(false);
		if (assetBundleDependencyGraph == null)
		{
			global::Log.Asset.PrintError("Failed to load '{0}' from bundle '{1}'", new object[]
			{
				ScriptableAssetManifest.BundleDepsAssetPath,
				assetBundlePath
			});
			throw new ApplicationException("Could not initialize AssetLoader: failed to load AssetBundleDependencyGraph");
		}
		IAssetLocator assetLocator = new AssetLocator(AssetManifest.Get());
		return new AssetBundleAssetBank(global::Log.Asset, assetLocator, assetBundleDependencyGraph);
	}

	// Token: 0x06007343 RID: 29507 RVA: 0x0025119C File Offset: 0x0024F39C
	private bool LoadObject<T>(AssetReference assetRef, AssetLoader.ObjectCallback callback, object callbackData, bool persistent = false, bool disableLocalization = false) where T : UnityEngine.Object
	{
		Asset asset = this.GetAppropriateAsset(assetRef, AssetVariantTags.Quality.Normal, disableLocalization);
		IAssetManager assetManager = this.m_assetManager;
		Asset asset3 = asset;
		IAsyncAssetRequest<T> asyncAssetRequest = assetManager.LoadAsync<T>((asset3 != null) ? asset3.GetGuid() : null, false);
		asyncAssetRequest.OnCompleted = (AssetLoadedCB<T>)Delegate.Combine(asyncAssetRequest.OnCompleted, new AssetLoadedCB<T>(delegate(IAsyncAssetRequest<T> completedRequest)
		{
			AssetLoader <>4__this = this;
			AssetReference assetRef2 = assetRef;
			Asset asset2 = asset;
			<>4__this.OnAssetLoaded<T>(assetRef2, (asset2 != null) ? asset2.GetGuid() : null, completedRequest.Result);
			callback(assetRef, completedRequest.Result.Asset, callbackData);
		}));
		return true;
	}

	// Token: 0x06007344 RID: 29508 RVA: 0x00251220 File Offset: 0x0024F420
	private bool LoadPrefab(AssetReference assetRef, bool usePrefabPosition, AssetLoader.GameObjectCallback callback, object callbackData, bool persistent = false, UnityEngine.Object fallback = null, bool autoInstantiateOnLoad = true)
	{
		Asset appropriateAsset = this.GetAppropriateAsset(assetRef, AssetVariantTags.Quality.Normal, false);
		this.LoadPrefabInternal(appropriateAsset, assetRef, usePrefabPosition, callback, callbackData, fallback, autoInstantiateOnLoad);
		return appropriateAsset != null && this.IsAssetWithGuidAvailable(appropriateAsset.GetGuid());
	}

	// Token: 0x06007345 RID: 29509 RVA: 0x00251258 File Offset: 0x0024F458
	private GameObject TryGetAsGameObject(string guid, UnityEngine.Object obj)
	{
		GameObject gameObject = obj as GameObject;
		if (!gameObject)
		{
			string text = GameStrings.Format("GLOBAL_ERROR_ASSET_INCORRECT_DATA", new object[]
			{
				guid
			});
			UnityEngine.Debug.LogError(string.Format("AssetLoader.WaitThenCallGameObjectCallback() - {0} (prefab={1})", text, obj));
			global::Error.AddFatal(FatalErrorReason.ASSET_INCORRECT_DATA, text, Array.Empty<object>());
		}
		return gameObject;
	}

	// Token: 0x06007346 RID: 29510 RVA: 0x002512A8 File Offset: 0x0024F4A8
	private void LoadPrefabInternal(Asset assetToLoad, AssetReference requestedReference, bool usePrefabPosition, AssetLoader.GameObjectCallback callback, object callbackData, UnityEngine.Object fallback, bool autoInstantiateOnLoad)
	{
		string guid = (assetToLoad != null) ? assetToLoad.GetGuid() : null;
		if (guid == null)
		{
			if (GeneralUtils.IsCallbackValid(callback))
			{
				callback(null, null, callbackData);
			}
			return;
		}
		IAsyncAssetRequest<GameObject> asyncAssetRequest = this.m_assetManager.LoadAsync<GameObject>(assetToLoad.GetGuid(), false);
		asyncAssetRequest.OnCompleted = (AssetLoadedCB<GameObject>)Delegate.Combine(asyncAssetRequest.OnCompleted, new AssetLoadedCB<GameObject>(delegate(IAsyncAssetRequest<GameObject> completedRequest)
		{
			this.OnAssetLoaded<GameObject>(requestedReference, guid, completedRequest.Result);
			GameObject asset = completedRequest.Result.Asset;
			GameObject gameObject = this.TryGetAsGameObject(guid, asset);
			if (autoInstantiateOnLoad)
			{
				Processor.RunCoroutine(this.InstantiateAndWaitThenCallGameObjectCallback(requestedReference, gameObject, usePrefabPosition, callback, callbackData), null);
				return;
			}
			if (GeneralUtils.IsCallbackValid(callback))
			{
				callback(requestedReference, gameObject, callbackData);
			}
		}));
	}

	// Token: 0x06007347 RID: 29511 RVA: 0x00251356 File Offset: 0x0024F556
	private void OnAssetLoaded<T>(AssetReference requestedAsset, string resolvedGuid, AssetHandle<T> loadedAsset) where T : UnityEngine.Object
	{
		if (loadedAsset == null)
		{
			AssetLoader.LogMissingAsset(requestedAsset, resolvedGuid, typeof(T).Name);
		}
	}

	// Token: 0x06007348 RID: 29512 RVA: 0x00251371 File Offset: 0x0024F571
	public bool IsAssetAvailable(AssetReference assetRef)
	{
		return assetRef != null && this.IsAssetWithGuidAvailable(assetRef.guid);
	}

	// Token: 0x06007349 RID: 29513 RVA: 0x00251384 File Offset: 0x0024F584
	public bool IsAppropriateVariantAvailable(AssetReference assetRef, AssetLoadingOptions options)
	{
		Asset appropriateAsset = this.GetAppropriateAsset(assetRef, options);
		return this.IsAssetWithGuidAvailable((appropriateAsset != null) ? appropriateAsset.GetGuid() : null);
	}

	// Token: 0x0600734A RID: 29514 RVA: 0x002513AC File Offset: 0x0024F5AC
	private bool IsAssetWithGuidAvailable(string assetGuid)
	{
		IGameDownloadManager gameDownloadManager = GameDownloadManagerProvider.Get();
		return gameDownloadManager != null && gameDownloadManager.IsAssetDownloaded(assetGuid);
	}

	// Token: 0x0600734B RID: 29515 RVA: 0x002513CC File Offset: 0x0024F5CC
	public AssetHandle<T> LoadAsset<T>(AssetReference requestedAsset, AssetLoadingOptions options = AssetLoadingOptions.None) where T : UnityEngine.Object
	{
		Asset appropriateAsset = this.GetAppropriateAsset(requestedAsset, options);
		if (appropriateAsset == null)
		{
			return null;
		}
		AssetHandle<T> assetHandle = this.m_assetManager.Load<T>(appropriateAsset.GetGuid(), true);
		this.OnAssetLoaded<T>(requestedAsset, appropriateAsset.GetGuid(), assetHandle);
		return assetHandle;
	}

	// Token: 0x0600734C RID: 29516 RVA: 0x0025140C File Offset: 0x0024F60C
	public bool LoadAsset<T>(ref AssetHandle<T> assetHandle, AssetReference assetRef, AssetLoadingOptions options = AssetLoadingOptions.None) where T : UnityEngine.Object
	{
		AssetHandle<T> assetHandle2 = this.LoadAsset<T>(assetRef, options);
		AssetHandle.SafeDispose<T>(ref assetHandle);
		assetHandle = assetHandle2;
		return assetHandle;
	}

	// Token: 0x0600734D RID: 29517 RVA: 0x00251434 File Offset: 0x0024F634
	public bool LoadAsset<T>(AssetReference assetRef, AssetHandleCallback<T> callback, object callbackData = null, AssetLoadingOptions options = AssetLoadingOptions.None) where T : UnityEngine.Object
	{
		Asset appropriateAsset = this.GetAppropriateAsset(assetRef, options);
		if (appropriateAsset == null)
		{
			if (GeneralUtils.IsCallbackValid(callback))
			{
				callback(null, null, callbackData);
			}
			return false;
		}
		IAsyncAssetRequest<T> asyncAssetRequest = this.m_assetManager.LoadAsync<T>(appropriateAsset.GetGuid(), true);
		asyncAssetRequest.OnCompleted = (AssetLoadedCB<T>)Delegate.Combine(asyncAssetRequest.OnCompleted, new AssetLoadedCB<T>(delegate(IAsyncAssetRequest<T> completedRequest)
		{
			this.OnAssetLoaded<T>(assetRef, appropriateAsset.GetGuid(), completedRequest.Result);
			if (GeneralUtils.IsCallbackValid(callback))
			{
				callback(assetRef, completedRequest.Result, callbackData);
				return;
			}
			AssetHandle<T> result = completedRequest.Result;
			if (result == null)
			{
				return;
			}
			result.Dispose();
		}));
		return true;
	}

	// Token: 0x0600734E RID: 29518 RVA: 0x002514DC File Offset: 0x0024F6DC
	public GameObject InstantiatePrefab(AssetReference assetRef, AssetLoadingOptions options)
	{
		GameObject result;
		using (AssetHandle<GameObject> assetHandle = this.LoadAsset<GameObject>(assetRef, options))
		{
			result = this.m_prefabInstantiator.InstantiatePrefab(assetHandle, options);
		}
		return result;
	}

	// Token: 0x0600734F RID: 29519 RVA: 0x00251520 File Offset: 0x0024F720
	public bool InstantiatePrefab(AssetReference assetRef, PrefabCallback<GameObject> callback, object callbackData, AssetLoadingOptions options)
	{
		AssetLoader.InstantiatePrefabCallbackData<GameObject> callbackData2 = new AssetLoader.InstantiatePrefabCallbackData<GameObject>
		{
			callerCallback = callback,
			callerData = callbackData,
			callerOptions = options,
			requestedAssetRef = assetRef
		};
		return this.LoadAsset<GameObject>(assetRef, new AssetHandleCallback<GameObject>(this.OnPrefabLoaded), callbackData2, options);
	}

	// Token: 0x06007350 RID: 29520 RVA: 0x00251568 File Offset: 0x0024F768
	public AssetHandle<GameObject> GetOrInstantiateSharedPrefab(AssetReference assetRef, AssetLoadingOptions options = AssetLoadingOptions.None)
	{
		AssetHandle<GameObject> orInstantiateSharedPrefab;
		using (AssetHandle<GameObject> assetHandle = this.LoadAsset<GameObject>(assetRef, options))
		{
			orInstantiateSharedPrefab = this.m_prefabInstantiator.GetOrInstantiateSharedPrefab(assetHandle, options);
		}
		return orInstantiateSharedPrefab;
	}

	// Token: 0x06007351 RID: 29521 RVA: 0x002515AC File Offset: 0x0024F7AC
	public bool GetOrInstantiateSharedPrefab(AssetReference assetRef, PrefabCallback<AssetHandle<GameObject>> callback, object callbackData = null, AssetLoadingOptions options = AssetLoadingOptions.None)
	{
		AssetLoader.InstantiatePrefabCallbackData<AssetHandle<GameObject>> callbackData2 = new AssetLoader.InstantiatePrefabCallbackData<AssetHandle<GameObject>>
		{
			callerCallback = callback,
			callerData = callbackData,
			callerOptions = options,
			requestedAssetRef = assetRef
		};
		return this.LoadAsset<GameObject>(assetRef, new AssetHandleCallback<GameObject>(this.OnSharedPrefabLoaded), callbackData2, options);
	}

	// Token: 0x06007352 RID: 29522 RVA: 0x002515F4 File Offset: 0x0024F7F4
	private void OnPrefabLoaded(AssetReference prefabRef, AssetHandle<GameObject> prefabHandle, object callbackData)
	{
		try
		{
			AssetLoader.InstantiatePrefabCallbackData<GameObject> instantiatePrefabCallbackData = callbackData as AssetLoader.InstantiatePrefabCallbackData<GameObject>;
			if (!prefabHandle)
			{
				if (GeneralUtils.IsCallbackValid(instantiatePrefabCallbackData.callerCallback))
				{
					instantiatePrefabCallbackData.callerCallback(prefabRef, null, instantiatePrefabCallbackData.callerData);
				}
			}
			else
			{
				this.m_prefabInstantiator.InstantiatePrefab(prefabHandle, new PrefabInstantiatorCB<GameObject>(this.OnPrefabInstantiated<GameObject>), instantiatePrefabCallbackData, instantiatePrefabCallbackData.callerOptions);
			}
		}
		finally
		{
			if (prefabHandle != null)
			{
				((IDisposable)prefabHandle).Dispose();
			}
		}
	}

	// Token: 0x06007353 RID: 29523 RVA: 0x00251670 File Offset: 0x0024F870
	private void OnSharedPrefabLoaded(AssetReference prefabRef, AssetHandle<GameObject> prefabHandle, object callbackData)
	{
		AssetLoader.InstantiatePrefabCallbackData<AssetHandle<GameObject>> instantiatePrefabCallbackData = callbackData as AssetLoader.InstantiatePrefabCallbackData<AssetHandle<GameObject>>;
		if (!prefabHandle)
		{
			if (GeneralUtils.IsCallbackValid(instantiatePrefabCallbackData.callerCallback))
			{
				instantiatePrefabCallbackData.callerCallback(prefabRef, null, instantiatePrefabCallbackData.callerData);
			}
			return;
		}
		this.m_prefabInstantiator.GetOrInstantiateSharedPrefab(prefabHandle, new PrefabInstantiatorCB<AssetHandle<GameObject>>(this.OnPrefabInstantiated<AssetHandle<GameObject>>), instantiatePrefabCallbackData, instantiatePrefabCallbackData.callerOptions);
	}

	// Token: 0x06007354 RID: 29524 RVA: 0x002516D0 File Offset: 0x0024F8D0
	private void OnPrefabInstantiated<T>(string prefabAddress, T instance, object callbackData)
	{
		AssetLoader.InstantiatePrefabCallbackData<T> instantiatePrefabCallbackData = callbackData as AssetLoader.InstantiatePrefabCallbackData<T>;
		if (GeneralUtils.IsCallbackValid(instantiatePrefabCallbackData.callerCallback))
		{
			instantiatePrefabCallbackData.callerCallback(instantiatePrefabCallbackData.requestedAssetRef, instance, instantiatePrefabCallbackData.callerData);
		}
	}

	// Token: 0x06007355 RID: 29525 RVA: 0x0025170C File Offset: 0x0024F90C
	[Obsolete("from now on, always use async loading instead (i.e. LoadGameObject with callback).")]
	private GameObject LoadGameObjectImmediately(AssetReference requestedAsset, Asset asset, bool usePrefabPosition, GameObject fallback = null, bool autoInstantiate = true)
	{
		GameObject gameObject = this.LoadObjectImmediately<GameObject>(requestedAsset, asset);
		if (!gameObject)
		{
			if (fallback == null)
			{
				return null;
			}
			gameObject = fallback;
		}
		if (!autoInstantiate)
		{
			return gameObject;
		}
		if (!usePrefabPosition)
		{
			return UnityEngine.Object.Instantiate<GameObject>(gameObject, this.NewGameObjectSpawnPosition(), gameObject.transform.rotation);
		}
		return UnityEngine.Object.Instantiate<GameObject>(gameObject);
	}

	// Token: 0x06007356 RID: 29526 RVA: 0x00251760 File Offset: 0x0024F960
	private T LoadObjectImmediately<T>(AssetReference requestedAsset, Asset resolvedAsset) where T : UnityEngine.Object
	{
		string text = (resolvedAsset != null) ? resolvedAsset.GetGuid() : null;
		if (string.IsNullOrEmpty(text))
		{
			return default(T);
		}
		AssetHandle<T> assetHandle = this.m_assetManager.Load<T>(text, false);
		this.OnAssetLoaded<T>(requestedAsset, text, assetHandle);
		return assetHandle.Asset;
	}

	// Token: 0x06007357 RID: 29527 RVA: 0x002517A9 File Offset: 0x0024F9A9
	private IEnumerator InstantiateAndWaitThenCallGameObjectCallback(AssetReference assetRef, GameObject prefab, bool usePrefabPosition, AssetLoader.GameObjectCallback callback, object callbackData)
	{
		if (prefab == null)
		{
			if (GeneralUtils.IsCallbackValid(callback))
			{
				callback(assetRef, null, callbackData);
			}
			yield break;
		}
		GameObject instance = usePrefabPosition ? UnityEngine.Object.Instantiate<GameObject>(prefab) : UnityEngine.Object.Instantiate<GameObject>(prefab, this.NewGameObjectSpawnPosition(), prefab.transform.rotation);
		this.m_waitingOnObjects.Add(instance);
		yield return new WaitForEndOfFrame();
		this.m_waitingOnObjects.Remove(instance);
		if (GeneralUtils.IsCallbackValid(callback))
		{
			callback(assetRef, instance, callbackData);
		}
		yield break;
	}

	// Token: 0x06007358 RID: 29528 RVA: 0x002517DD File Offset: 0x0024F9DD
	private Vector3 NewGameObjectSpawnPosition()
	{
		if (Camera.main == null)
		{
			return Vector3.zero;
		}
		return Camera.main.transform.position + this.SPAWN_POS_CAMERA_OFFSET;
	}

	// Token: 0x06007359 RID: 29529 RVA: 0x0025180C File Offset: 0x0024FA0C
	private static void LogMissingAsset(AssetReference requestedAsset, string resolvedGuid, string assetType)
	{
		AssetLoader.SendMissingAssetTelemetry(requestedAsset, resolvedGuid, assetType);
		global::Log.MissingAssets.PrintError(string.Format("{0} {1} not found", assetType, (requestedAsset != null) ? requestedAsset.GetLegacyAssetName() : null), Array.Empty<object>());
	}

	// Token: 0x0600735A RID: 29530 RVA: 0x0025183C File Offset: 0x0024FA3C
	private static void SendMissingAssetTelemetry(AssetReference requestedAsset, string resolvedGuid, string assetType)
	{
		if (string.IsNullOrEmpty((requestedAsset != null) ? requestedAsset.guid : null))
		{
			global::Log.Telemetry.Print("Missing asset was found, but there was not way to identify it.  No telemetry will be sent.", Array.Empty<object>());
			return;
		}
		if (Application.isEditor)
		{
			global::Log.Telemetry.Print("Missing asset found in editor - not sending missing asset telemetry for requestedGuid={0}, resolvedGuid={1}, name={2}", new object[]
			{
				requestedAsset.guid,
				resolvedGuid,
				requestedAsset.GetLegacyAssetName()
			});
			return;
		}
		TelemetryManager.Client().SendAssetNotFound(assetType, requestedAsset.guid, resolvedGuid, requestedAsset.GetLegacyAssetName());
	}

	// Token: 0x0600735B RID: 29531 RVA: 0x002518BC File Offset: 0x0024FABC
	private static void SendSharedPrefabHandleOrphanedTelemetry(string asset, string owner)
	{
		TelemetryManager.Client().SendAssetOrphaned(asset ?? string.Empty, owner ?? string.Empty, "prefab_instance");
	}

	// Token: 0x0600735C RID: 29532 RVA: 0x002518E1 File Offset: 0x0024FAE1
	private static void SendAssetHandleOrphanedTelemetry(string asset, string owner)
	{
		TelemetryManager.Client().SendAssetOrphaned(asset ?? string.Empty, owner ?? string.Empty, "asset");
	}

	// Token: 0x0600735D RID: 29533 RVA: 0x00251906 File Offset: 0x0024FB06
	public void PopulateDebugStats(AssetManagerDebugStats stats, AssetManagerDebugStats.DataFields requestedFields)
	{
		this.m_assetManager.PopulateDebugStats(stats, requestedFields);
	}

	// Token: 0x04005BBA RID: 23482
	public const string LOCAL_ASSET_BUNDLE_NAME = "local_asset_bundle.unity3d";

	// Token: 0x04005BBB RID: 23483
	private readonly Vector3 SPAWN_POS_CAMERA_OFFSET = new Vector3(0f, 0f, -5000f);

	// Token: 0x04005BBC RID: 23484
	private const int DEAD_HANDLES_CHECK_INTERVAL_FRAMES = 30;

	// Token: 0x04005BBD RID: 23485
	private List<GameObject> m_waitingOnObjects = new List<GameObject>();

	// Token: 0x04005BBE RID: 23486
	private int m_framesSinceLastDeadHandlesCheck;

	// Token: 0x04005BBF RID: 23487
	private IAssetManager m_assetManager;

	// Token: 0x04005BC0 RID: 23488
	private IPrefabInstantiator m_prefabInstantiator;

	// Token: 0x0200244A RID: 9290
	// (Invoke) Token: 0x06012EDD RID: 77533
	public delegate void ObjectCallback(AssetReference assetRef, UnityEngine.Object obj, object callbackData);

	// Token: 0x0200244B RID: 9291
	// (Invoke) Token: 0x06012EE1 RID: 77537
	public delegate void GameObjectCallback(AssetReference assetRef, GameObject go, object callbackData);

	// Token: 0x0200244C RID: 9292
	private class InstantiatePrefabCallbackData<T>
	{
		// Token: 0x0400E9AF RID: 59823
		public AssetReference requestedAssetRef;

		// Token: 0x0400E9B0 RID: 59824
		public AssetLoadingOptions callerOptions;

		// Token: 0x0400E9B1 RID: 59825
		public PrefabCallback<T> callerCallback;

		// Token: 0x0400E9B2 RID: 59826
		public object callerData;
	}
}
