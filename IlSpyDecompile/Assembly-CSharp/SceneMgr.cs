using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.Streaming;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : IService, IHasUpdate
{
	public enum Mode
	{
		INVALID,
		STARTUP,
		[Description("Login")]
		LOGIN,
		[Description("Hub")]
		HUB,
		[Description("Gameplay")]
		GAMEPLAY,
		[Description("CollectionManager")]
		COLLECTIONMANAGER,
		[Description("PackOpening")]
		PACKOPENING,
		[Description("Tournament")]
		TOURNAMENT,
		[Description("Friendly")]
		FRIENDLY,
		[Description("FatalError")]
		FATAL_ERROR,
		[Description("Draft")]
		DRAFT,
		[Description("Credits")]
		CREDITS,
		[Description("Reset")]
		RESET,
		[Description("Adventure")]
		ADVENTURE,
		[Description("TavernBrawl")]
		TAVERN_BRAWL,
		[Description("FiresideGathering")]
		FIRESIDE_GATHERING,
		[Description("Bacon")]
		BACON,
		[Description("GameMode")]
		GAME_MODE,
		[Description("PvPDungeonRun")]
		PVP_DUNGEON_RUN
	}

	public enum TransitionHandlerType
	{
		INVALID,
		SCENEMGR,
		CURRENT_SCENE,
		NEXT_SCENE
	}

	public delegate void ScenePreUnloadCallback(Mode prevMode, PegasusScene prevScene, object userData);

	public delegate void SceneUnloadedCallback(Mode prevMode, PegasusScene prevScene, object userData);

	public delegate void ScenePreLoadCallback(Mode prevMode, Mode mode, object userData);

	public delegate void SceneLoadedCallback(Mode mode, PegasusScene scene, object userData);

	private class ScenePreUnloadListener : EventListener<ScenePreUnloadCallback>
	{
		public void Fire(Mode prevMode, PegasusScene prevScene)
		{
			m_callback(prevMode, prevScene, m_userData);
		}
	}

	private class SceneUnloadedListener : EventListener<SceneUnloadedCallback>
	{
		public void Fire(Mode prevMode, PegasusScene prevScene)
		{
			m_callback(prevMode, prevScene, m_userData);
		}
	}

	private class ScenePreLoadListener : EventListener<ScenePreLoadCallback>
	{
		public void Fire(Mode prevMode, Mode mode)
		{
			m_callback(prevMode, mode, m_userData);
		}
	}

	private class SceneLoadedListener : EventListener<SceneLoadedCallback>
	{
		public void Fire(Mode mode, PegasusScene scene)
		{
			m_callback(mode, scene, m_userData);
		}
	}

	public delegate void OnSceneLoadCompleteForSceneDrivenTransition(Action onTransitionComplete);

	public GameObject m_StartupCamera;

	private const float SCENE_UNLOAD_DELAY = 0.15f;

	private const float SCENE_LOADED_DELAY = 0.15f;

	private static SceneMgr s_instance;

	private int m_startupAssetLoads;

	private Mode m_mode = Mode.STARTUP;

	private Mode m_nextMode;

	private Mode m_prevMode;

	private bool m_reloadMode;

	private PegasusScene m_scene;

	private bool m_sceneLoaded;

	private bool m_transitioning;

	private bool m_performFullCleanup;

	private List<ScenePreUnloadListener> m_scenePreUnloadListeners = new List<ScenePreUnloadListener>();

	private List<SceneUnloadedListener> m_sceneUnloadedListeners = new List<SceneUnloadedListener>();

	private List<ScenePreLoadListener> m_scenePreLoadListeners = new List<ScenePreLoadListener>();

	private List<SceneLoadedListener> m_sceneLoadedListeners = new List<SceneLoadedListener>();

	private OnSceneLoadCompleteForSceneDrivenTransition m_onSceneLoadCompleteForSceneDrivenTransitionCallback;

	private TransitionHandlerType m_transitionHandlerType;

	private long m_boxLoadTimestamp;

	private Coroutine m_switchModeCoroutine;

	private GameObject m_sceneObject;

	public LoadingScreen LoadingScreen { get; private set; }

	public GameObject SceneObject
	{
		get
		{
			if (m_sceneObject == null)
			{
				m_sceneObject = new GameObject("SceneMgr", typeof(HSDontDestroyOnLoad));
			}
			return m_sceneObject;
		}
	}

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
		m_transitioning = true;
		LoadComponentFromResource<LoadingScreen> loadLoadingScreen = new LoadComponentFromResource<LoadingScreen>("Prefabs/LoadingScreen", LoadResourceFlags.AutoInstantiateOnLoad | LoadResourceFlags.FailOnError);
		yield return loadLoadingScreen;
		LoadingScreen = loadLoadingScreen.LoadedComponent;
		LoadingScreen.RegisterSceneListeners(this);
		LoadingScreen.transform.parent = SceneObject.transform;
		HearthstoneApplication.Get().WillReset += WillReset;
		if (!IsModeRequested(Mode.FATAL_ERROR))
		{
			QueueLoadBoxJob();
			RegisterSceneLoadedEvent(UpdatePerformanceTrackingFromModeSwitch);
		}
	}

	public Type[] GetDependencies()
	{
		return new Type[5]
		{
			typeof(GameDownloadManager),
			typeof(Network),
			typeof(GameDbf),
			typeof(IAssetLoader),
			typeof(FontTable)
		};
	}

	public void Shutdown()
	{
		s_instance = null;
		LoadingScreen.UnregisterSceneListeners(this);
		UnregisterSceneLoadedEvent(UpdatePerformanceTrackingFromModeSwitch);
		HearthstoneApplication.Get().WillReset -= WillReset;
	}

	public void LoadShaderPreCompiler()
	{
		if (PlatformSettings.IsMobile() && PlatformSettings.RuntimeOS != OSCategory.Android)
		{
			AssetReference assetReference = new AssetReference("ShaderPreCompiler.prefab:380ca3ee11a2643068cfb3d4766f3fd3");
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(assetReference);
			if (gameObject == null)
			{
				Debug.LogError(string.Format("SceneMgr.LoadShaderPreCompiler() - FAILED to load prefab", assetReference));
			}
			else
			{
				gameObject.transform.parent = SceneObject.transform;
			}
		}
	}

	public void Update()
	{
		if (!m_reloadMode)
		{
			if (m_nextMode == Mode.INVALID)
			{
				return;
			}
			if (m_mode == m_nextMode)
			{
				m_nextMode = Mode.INVALID;
				return;
			}
		}
		m_transitioning = true;
		m_performFullCleanup = !m_reloadMode;
		m_prevMode = m_mode;
		m_mode = m_nextMode;
		m_nextMode = Mode.INVALID;
		m_reloadMode = false;
		if (m_scene != null)
		{
			if (m_switchModeCoroutine != null)
			{
				Processor.CancelCoroutine(m_switchModeCoroutine);
			}
			IEnumerator routine = (IsDoingSceneDrivenTransition() ? SwitchModeWithSceneDrivenTransition() : SwitchMode());
			m_switchModeCoroutine = Processor.RunCoroutine(routine, this);
		}
		else
		{
			LoadMode();
		}
	}

	public static SceneMgr Get()
	{
		if (s_instance == null)
		{
			s_instance = HearthstoneServices.Get<SceneMgr>();
		}
		return s_instance;
	}

	public static bool IsInitialized()
	{
		return s_instance != null;
	}

	private void WillReset()
	{
		Log.Reset.Print("SceneMgr.WillReset()");
		if (HearthstoneApplication.IsPublic())
		{
			TimeScaleMgr.Get().SetGameTimeScale(1f);
			TimeScaleMgr.Get().SetTimeScaleMultiplier(1f);
		}
		Processor.StopAllCoroutinesWithObjectRef(this);
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
		m_mode = Mode.STARTUP;
		m_nextMode = Mode.INVALID;
		m_prevMode = Mode.INVALID;
		m_reloadMode = false;
		PegasusScene scene = m_scene;
		if (scene != null)
		{
			scene.PreUnload();
		}
		FireScenePreUnloadEvent(scene);
		if (m_scene != null)
		{
			m_scene.Unload();
			m_scene = null;
			m_sceneLoaded = false;
		}
		FireSceneUnloadedEvent(scene);
		PostUnloadCleanup();
		QueueLoadBoxJob();
		Log.Reset.Print("\tSceneMgr.WillReset() completed");
	}

	public void SetNextMode(Mode mode, TransitionHandlerType transitionHandler = TransitionHandlerType.SCENEMGR, OnSceneLoadCompleteForSceneDrivenTransition onLoadCompleteCallback = null)
	{
		if (IsModeRequested(Mode.FATAL_ERROR))
		{
			return;
		}
		CacheModeForResume(mode);
		m_nextMode = mode;
		m_reloadMode = false;
		m_transitionHandlerType = transitionHandler;
		if (transitionHandler == TransitionHandlerType.CURRENT_SCENE || transitionHandler == TransitionHandlerType.NEXT_SCENE)
		{
			if (transitionHandler == TransitionHandlerType.CURRENT_SCENE && onLoadCompleteCallback == null)
			{
				Log.All.PrintError("SceneMgr - SetNextMode did not provide the required callback!");
			}
			m_onSceneLoadCompleteForSceneDrivenTransitionCallback = onLoadCompleteCallback;
		}
	}

	public void ReloadMode()
	{
		if (!IsModeRequested(Mode.FATAL_ERROR))
		{
			m_nextMode = m_mode;
			m_reloadMode = true;
		}
	}

	public void ReturnToPreviousMode()
	{
		if (!IsModeRequested(Mode.FATAL_ERROR))
		{
			CacheModeForResume(m_prevMode);
			m_nextMode = m_prevMode;
			m_reloadMode = false;
		}
	}

	public Mode GetPrevMode()
	{
		return m_prevMode;
	}

	public Mode GetMode()
	{
		return m_mode;
	}

	public Mode GetNextMode()
	{
		return m_nextMode;
	}

	public PegasusScene GetScene()
	{
		return m_scene;
	}

	public void SetScene(PegasusScene scene)
	{
		m_scene = scene;
	}

	public bool IsSceneLoaded()
	{
		return m_sceneLoaded;
	}

	public bool WillTransition()
	{
		if (m_reloadMode)
		{
			return true;
		}
		if (m_nextMode == Mode.INVALID)
		{
			return false;
		}
		if (m_nextMode != m_mode)
		{
			return true;
		}
		return false;
	}

	public bool IsTransitioning()
	{
		return m_transitioning;
	}

	public bool IsTransitionNowOrPending()
	{
		if (IsTransitioning())
		{
			return true;
		}
		if (WillTransition())
		{
			return true;
		}
		return false;
	}

	public bool IsDoingSceneDrivenTransition()
	{
		if (m_transitionHandlerType != TransitionHandlerType.CURRENT_SCENE)
		{
			return m_transitionHandlerType == TransitionHandlerType.NEXT_SCENE;
		}
		return true;
	}

	public bool IsModeRequested(Mode mode)
	{
		if (m_mode == mode)
		{
			return true;
		}
		if (m_nextMode == mode)
		{
			return true;
		}
		return false;
	}

	public bool IsInGame()
	{
		return IsModeRequested(Mode.GAMEPLAY);
	}

	public bool IsInTavernBrawlMode()
	{
		if (GetMode() != Mode.TAVERN_BRAWL)
		{
			if (GetMode() == Mode.FIRESIDE_GATHERING)
			{
				return FiresideGatheringManager.Get().InBrawlMode();
			}
			return false;
		}
		return true;
	}

	public bool IsInDuelsMode()
	{
		return GetMode() == Mode.PVP_DUNGEON_RUN;
	}

	public void NotifySceneLoaded()
	{
		m_sceneLoaded = true;
		if (ShouldUseSceneLoadDelays())
		{
			Processor.RunCoroutine(WaitThenFireSceneLoadedEvent(), this);
		}
		else
		{
			FireSceneLoadedEvent();
		}
	}

	public void RegisterScenePreUnloadEvent(ScenePreUnloadCallback callback)
	{
		RegisterScenePreUnloadEvent(callback, null);
	}

	public void RegisterScenePreUnloadEvent(ScenePreUnloadCallback callback, object userData)
	{
		ScenePreUnloadListener scenePreUnloadListener = new ScenePreUnloadListener();
		scenePreUnloadListener.SetCallback(callback);
		scenePreUnloadListener.SetUserData(userData);
		if (!m_scenePreUnloadListeners.Contains(scenePreUnloadListener))
		{
			m_scenePreUnloadListeners.Add(scenePreUnloadListener);
		}
	}

	public bool UnregisterScenePreUnloadEvent(ScenePreUnloadCallback callback)
	{
		return UnregisterScenePreUnloadEvent(callback, null);
	}

	public bool UnregisterScenePreUnloadEvent(ScenePreUnloadCallback callback, object userData)
	{
		ScenePreUnloadListener scenePreUnloadListener = new ScenePreUnloadListener();
		scenePreUnloadListener.SetCallback(callback);
		scenePreUnloadListener.SetUserData(userData);
		return m_scenePreUnloadListeners.Remove(scenePreUnloadListener);
	}

	public static bool UnregisterScenePreUnloadEventFromInstance(ScenePreUnloadCallback callback)
	{
		if (s_instance == null)
		{
			return false;
		}
		return s_instance.UnregisterScenePreUnloadEvent(callback);
	}

	public void RegisterSceneUnloadedEvent(SceneUnloadedCallback callback)
	{
		RegisterSceneUnloadedEvent(callback, null);
	}

	public void RegisterSceneUnloadedEvent(SceneUnloadedCallback callback, object userData)
	{
		SceneUnloadedListener sceneUnloadedListener = new SceneUnloadedListener();
		sceneUnloadedListener.SetCallback(callback);
		sceneUnloadedListener.SetUserData(userData);
		if (!m_sceneUnloadedListeners.Contains(sceneUnloadedListener))
		{
			m_sceneUnloadedListeners.Add(sceneUnloadedListener);
		}
	}

	public bool UnregisterSceneUnloadedEvent(SceneUnloadedCallback callback)
	{
		return UnregisterSceneUnloadedEvent(callback, null);
	}

	public bool UnregisterSceneUnloadedEvent(SceneUnloadedCallback callback, object userData)
	{
		SceneUnloadedListener sceneUnloadedListener = new SceneUnloadedListener();
		sceneUnloadedListener.SetCallback(callback);
		sceneUnloadedListener.SetUserData(userData);
		return m_sceneUnloadedListeners.Remove(sceneUnloadedListener);
	}

	public void RegisterScenePreLoadEvent(ScenePreLoadCallback callback)
	{
		RegisterScenePreLoadEvent(callback, null);
	}

	public void RegisterScenePreLoadEvent(ScenePreLoadCallback callback, object userData)
	{
		ScenePreLoadListener scenePreLoadListener = new ScenePreLoadListener();
		scenePreLoadListener.SetCallback(callback);
		scenePreLoadListener.SetUserData(userData);
		if (!m_scenePreLoadListeners.Contains(scenePreLoadListener))
		{
			m_scenePreLoadListeners.Add(scenePreLoadListener);
		}
	}

	public bool UnregisterScenePreLoadEvent(ScenePreLoadCallback callback)
	{
		return UnregisterScenePreLoadEvent(callback, null);
	}

	public bool UnregisterScenePreLoadEvent(ScenePreLoadCallback callback, object userData)
	{
		ScenePreLoadListener scenePreLoadListener = new ScenePreLoadListener();
		scenePreLoadListener.SetCallback(callback);
		scenePreLoadListener.SetUserData(userData);
		return m_scenePreLoadListeners.Remove(scenePreLoadListener);
	}

	public void RegisterSceneLoadedEvent(SceneLoadedCallback callback)
	{
		RegisterSceneLoadedEvent(callback, null);
	}

	public void RegisterSceneLoadedEvent(SceneLoadedCallback callback, object userData)
	{
		SceneLoadedListener sceneLoadedListener = new SceneLoadedListener();
		sceneLoadedListener.SetCallback(callback);
		sceneLoadedListener.SetUserData(userData);
		if (!m_sceneLoadedListeners.Contains(sceneLoadedListener))
		{
			m_sceneLoadedListeners.Add(sceneLoadedListener);
		}
	}

	public bool UnregisterSceneLoadedEvent(SceneLoadedCallback callback)
	{
		return UnregisterSceneLoadedEvent(callback, null);
	}

	public bool UnregisterSceneLoadedEvent(SceneLoadedCallback callback, object userData)
	{
		SceneLoadedListener sceneLoadedListener = new SceneLoadedListener();
		sceneLoadedListener.SetCallback(callback);
		sceneLoadedListener.SetUserData(userData);
		return m_sceneLoadedListeners.Remove(sceneLoadedListener);
	}

	private IEnumerator WaitThenFireSceneLoadedEvent()
	{
		yield return new WaitForSeconds(0.15f);
		FireSceneLoadedEvent();
	}

	private void FireScenePreUnloadEvent(PegasusScene prevScene)
	{
		ScenePreUnloadListener[] array = m_scenePreUnloadListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(m_prevMode, prevScene);
		}
	}

	private void FireSceneUnloadedEvent(PegasusScene prevScene)
	{
		if (IsDoingSceneDrivenTransition())
		{
			m_transitioning = false;
		}
		SceneUnloadedListener[] array = m_sceneUnloadedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(m_prevMode, prevScene);
		}
	}

	private void FireScenePreLoadEvent()
	{
		ScenePreLoadListener[] array = m_scenePreLoadListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(m_prevMode, m_mode);
		}
	}

	private void FireSceneLoadedEvent()
	{
		if (!IsDoingSceneDrivenTransition())
		{
			m_transitioning = false;
		}
		SceneLoadedListener[] array = m_sceneLoadedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(m_mode, m_scene);
		}
		HearthstonePerformance.Get()?.SendCustomEvent("SceneLoaded" + Enum.GetName(typeof(Mode), m_mode));
	}

	private void LoadMode()
	{
		FireScenePreLoadEvent();
		SceneManager.LoadSceneAsync(EnumUtils.GetString(m_mode), LoadSceneMode.Additive);
	}

	private IEnumerator SwitchMode()
	{
		if (m_scene.IsUnloading())
		{
			yield break;
		}
		PegasusScene prevScene = m_scene;
		prevScene.PreUnload();
		FireScenePreUnloadEvent(prevScene);
		if (LoadingScreen.GetPhase() == LoadingScreen.Phase.WAITING_FOR_SCENE_UNLOAD && LoadingScreen.GetFreezeFrameCamera() != null)
		{
			yield return new WaitForEndOfFrame();
		}
		if (ShouldUseSceneUnloadDelays())
		{
			if (Box.Get() != null)
			{
				while (Box.Get().HasPendingEffects())
				{
					yield return 0;
				}
			}
			else
			{
				yield return new WaitForSeconds(0.15f);
			}
		}
		m_scene.Unload();
		m_scene = null;
		m_sceneLoaded = false;
		FireSceneUnloadedEvent(prevScene);
		PostUnloadCleanup();
		LoadModeFromModeSwitch();
	}

	private IEnumerator SwitchModeWithSceneDrivenTransition()
	{
		if (!m_scene.IsUnloading())
		{
			PegasusScene prevScene = m_scene;
			m_sceneLoaded = false;
			FireScenePreLoadEvent();
			SceneManager.LoadSceneAsync(EnumUtils.GetString(m_mode), LoadSceneMode.Additive);
			while (!m_sceneLoaded)
			{
				yield return null;
			}
			Action action = delegate
			{
				prevScene.PreUnload();
				FireScenePreUnloadEvent(prevScene);
				prevScene.Unload();
				FireSceneUnloadedEvent(prevScene);
			};
			if (m_transitionHandlerType == TransitionHandlerType.CURRENT_SCENE && m_onSceneLoadCompleteForSceneDrivenTransitionCallback != null)
			{
				m_onSceneLoadCompleteForSceneDrivenTransitionCallback(action);
				m_onSceneLoadCompleteForSceneDrivenTransitionCallback = null;
			}
			else if (m_transitionHandlerType == TransitionHandlerType.NEXT_SCENE)
			{
				m_scene.ExecuteSceneDrivenTransition(action);
			}
			else
			{
				Log.All.PrintError("No callback for scene driven scene transition.");
				action();
			}
		}
	}

	private bool ShouldUseSceneUnloadDelays()
	{
		if (m_prevMode == m_mode)
		{
			return false;
		}
		return true;
	}

	private bool ShouldUseSceneLoadDelays()
	{
		if (m_mode == Mode.LOGIN)
		{
			return false;
		}
		if (m_mode == Mode.HUB)
		{
			return false;
		}
		if (m_mode == Mode.FATAL_ERROR)
		{
			return false;
		}
		return true;
	}

	private void PostUnloadCleanup()
	{
		Time.captureFramerate = 0;
		if (Application.isEditor && m_mode == Mode.FATAL_ERROR)
		{
			Log.All.PrintWarning("Not destroying the previous scene's objects for easier debugging! You can (probably) ignore warnings or errors after this point!");
			AudioListener[] array = UnityEngine.Object.FindObjectsOfType<AudioListener>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
			return;
		}
		DestroyAllObjectsOnModeSwitch();
		if (m_performFullCleanup)
		{
			HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
			if (hearthstoneApplication != null)
			{
				hearthstoneApplication.UnloadUnusedAssets();
			}
		}
	}

	private void DestroyAllObjectsOnModeSwitch()
	{
		int sceneCount = SceneManager.sceneCount;
		for (int i = 0; i < sceneCount; i++)
		{
			GameObject[] rootGameObjects = SceneManager.GetSceneAt(i).GetRootGameObjects();
			foreach (GameObject gameObject in rootGameObjects)
			{
				if (ShouldDestroyOnModeSwitch(gameObject))
				{
					UnityEngine.Object.DestroyImmediate(gameObject);
				}
			}
		}
	}

	private bool ShouldDestroyOnModeSwitch(GameObject go)
	{
		if (go == null)
		{
			return false;
		}
		if (go.transform.parent != null)
		{
			return false;
		}
		if (go.GetComponent<HSDontDestroyOnLoad>() != null)
		{
			return false;
		}
		if (go.scene.buildIndex == -1)
		{
			Debug.LogErrorFormat("GameObject ({0}) appears to be marked Don't Destroy On Load, but is being destroyed by our code anyway!", go.name);
		}
		if (PegUI.Get() != null && go == PegUI.Get().gameObject)
		{
			return false;
		}
		if (OverlayUI.Get() != null && go == OverlayUI.Get().gameObject)
		{
			return false;
		}
		if (Box.Get() != null && go == Box.Get().gameObject && DoesModeShowBox(m_mode))
		{
			return false;
		}
		if (AssetLoader.Get().IsSharedPrefabInstance(go))
		{
			return false;
		}
		if (AssetLoader.Get().IsWaitingOnObject(go))
		{
			return false;
		}
		if (go == iTweenManager.Get().gameObject)
		{
			return false;
		}
		return true;
	}

	private void CacheModeForResume(Mode mode)
	{
		if (PlatformSettings.OS == OSCategory.iOS || PlatformSettings.OS == OSCategory.Android)
		{
			switch (mode)
			{
			case Mode.HUB:
			case Mode.FRIENDLY:
				Options.Get().SetInt(Option.LAST_SCENE_MODE, 0);
				break;
			case Mode.COLLECTIONMANAGER:
			case Mode.TOURNAMENT:
			case Mode.DRAFT:
			case Mode.CREDITS:
			case Mode.ADVENTURE:
			case Mode.TAVERN_BRAWL:
			case Mode.FIRESIDE_GATHERING:
			case Mode.BACON:
			case Mode.GAME_MODE:
				Options.Get().SetInt(Option.LAST_SCENE_MODE, (int)mode);
				break;
			case Mode.GAMEPLAY:
			case Mode.PACKOPENING:
			case Mode.FATAL_ERROR:
			case Mode.RESET:
				break;
			}
		}
	}

	private bool DoesModeShowBox(Mode mode)
	{
		switch (mode)
		{
		case Mode.STARTUP:
		case Mode.GAMEPLAY:
		case Mode.FATAL_ERROR:
		case Mode.RESET:
			return false;
		default:
			return true;
		}
	}

	private void LoadModeFromModeSwitch()
	{
		bool flag = DoesModeShowBox(m_prevMode);
		bool flag2 = DoesModeShowBox(m_mode);
		if (!flag && flag2)
		{
			Processor.QueueJob("SceneMgr.Reload", Job_ReloadBox());
			return;
		}
		if (flag && !flag2)
		{
			LoadingScreen.SetAssetLoadStartTimestamp(m_boxLoadTimestamp);
			m_boxLoadTimestamp = 0L;
		}
		LoadMode();
	}

	private void QueueLoadBoxJob()
	{
		IJobDependency[] dependencies = HearthstoneJobs.BuildDependencies(typeof(SceneMgr), typeof(IAssetLoader), typeof(NetCache), new WaitForGameDownloadManagerState(), new WaitForSplashScreen());
		Processor.QueueJob("SceneMgr.LoadBox", Job_LoadBox(), dependencies);
	}

	private IEnumerator<IAsyncJobResult> Job_LoadBox()
	{
		yield return new LoadUIScreen("TheBox.prefab:6b55a928ffdc1b341b5dbe8f8a88e768");
		m_nextMode = Mode.LOGIN;
	}

	private IEnumerator<IAsyncJobResult> Job_ReloadBox()
	{
		yield return new LoadUIScreen("TheBox.prefab:6b55a928ffdc1b341b5dbe8f8a88e768");
		LoadMode();
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.SET_ROTATION_INTRO))
		{
			Log.Offline.Print("SceneMgr.OnFatalError: Error blocked by set rotation.");
			SetNextMode(Mode.FATAL_ERROR);
			return;
		}
		if (!ReconnectMgr.IsReconnectAllowed(message))
		{
			FatalErrorMgr.Get().RemoveErrorListener(OnFatalError);
			if (HearthstoneServices.TryGet<ReconnectMgr>(out var service))
			{
				service.FullResetRequired = true;
			}
			GoToFatalErrorScreen(message);
			return;
		}
		switch (m_mode)
		{
		case Mode.LOGIN:
		case Mode.GAMEPLAY:
			GoToFatalErrorScreen(message);
			break;
		case Mode.COLLECTIONMANAGER:
			CollectionManager.Get().HandleDisconnect();
			break;
		case Mode.HUB:
			StoreManager.Get().HandleDisconnect();
			break;
		case Mode.TAVERN_BRAWL:
		{
			CollectionManager collectionManager = CollectionManager.Get();
			if (collectionManager.IsInEditMode())
			{
				collectionManager.HandleDisconnect();
			}
			break;
		}
		default:
			Log.Offline.PrintDebug("Bypassing Fatal Error To HUB.");
			Navigation.Clear();
			SetNextMode(Mode.HUB);
			DialogManager.Get().ShowReconnectHelperDialog();
			break;
		case Mode.STARTUP:
		case Mode.PACKOPENING:
		case Mode.TOURNAMENT:
		case Mode.CREDITS:
			break;
		}
	}

	private void GoToFatalErrorScreen(FatalErrorMessage message)
	{
		if (HearthstoneApplication.Get().ResetOnErrorIfNecessary())
		{
			Log.Offline.PrintDebug("SceneMgr.GoToFatalErrorScreen() - Auto resetting. Do not display Fatal Error Screen.");
			return;
		}
		Log.BattleNet.PrintDebug("Set FatalError mode={0}, m_allowClick={1}, m_redirectToStore={2}", m_mode, message.m_allowClick, message.m_redirectToStore);
		FatalErrorMgr.Get().SetUnrecoverable(m_mode == Mode.STARTUP && (!message.m_allowClick || !message.m_redirectToStore));
		SetNextMode(Mode.FATAL_ERROR);
	}

	public bool DoesCurrentSceneSupportOfflineActivity()
	{
		switch (m_mode)
		{
		case Mode.STARTUP:
		case Mode.HUB:
		case Mode.COLLECTIONMANAGER:
		case Mode.PACKOPENING:
		case Mode.TOURNAMENT:
		case Mode.CREDITS:
		case Mode.TAVERN_BRAWL:
			return true;
		default:
			return false;
		}
	}

	private void UpdatePerformanceTrackingFromModeSwitch(Mode mode, PegasusScene scene, object userData)
	{
		if (mode == Mode.GAMEPLAY)
		{
			HearthstonePerformance hearthstonePerformance = HearthstonePerformance.Get();
			if (hearthstonePerformance != null)
			{
				hearthstonePerformance.CaptureBoxInteractableTime();
				UnregisterSceneLoadedEvent(UpdatePerformanceTrackingFromModeSwitch);
			}
		}
	}
}
