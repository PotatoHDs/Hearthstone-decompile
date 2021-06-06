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

// Token: 0x0200068D RID: 1677
public class SceneMgr : IService, IHasUpdate
{
	// Token: 0x17000591 RID: 1425
	// (get) Token: 0x06005DCD RID: 24013 RVA: 0x001E8EC2 File Offset: 0x001E70C2
	// (set) Token: 0x06005DCE RID: 24014 RVA: 0x001E8ECA File Offset: 0x001E70CA
	public LoadingScreen LoadingScreen { get; private set; }

	// Token: 0x17000592 RID: 1426
	// (get) Token: 0x06005DCF RID: 24015 RVA: 0x001E8ED3 File Offset: 0x001E70D3
	public GameObject SceneObject
	{
		get
		{
			if (this.m_sceneObject == null)
			{
				this.m_sceneObject = new GameObject("SceneMgr", new Type[]
				{
					typeof(HSDontDestroyOnLoad)
				});
			}
			return this.m_sceneObject;
		}
	}

	// Token: 0x06005DD0 RID: 24016 RVA: 0x001E8F0C File Offset: 0x001E710C
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		this.m_transitioning = true;
		LoadComponentFromResource<LoadingScreen> loadLoadingScreen = new LoadComponentFromResource<LoadingScreen>("Prefabs/LoadingScreen", LoadResourceFlags.AutoInstantiateOnLoad | LoadResourceFlags.FailOnError);
		yield return loadLoadingScreen;
		this.LoadingScreen = loadLoadingScreen.LoadedComponent;
		this.LoadingScreen.RegisterSceneListeners(this);
		this.LoadingScreen.transform.parent = this.SceneObject.transform;
		HearthstoneApplication.Get().WillReset += this.WillReset;
		if (this.IsModeRequested(SceneMgr.Mode.FATAL_ERROR))
		{
			yield break;
		}
		this.QueueLoadBoxJob();
		this.RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.UpdatePerformanceTrackingFromModeSwitch));
		yield break;
	}

	// Token: 0x06005DD1 RID: 24017 RVA: 0x001E8F1C File Offset: 0x001E711C
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(GameDownloadManager),
			typeof(Network),
			typeof(GameDbf),
			typeof(IAssetLoader),
			typeof(FontTable)
		};
	}

	// Token: 0x06005DD2 RID: 24018 RVA: 0x001E8F70 File Offset: 0x001E7170
	public void Shutdown()
	{
		SceneMgr.s_instance = null;
		this.LoadingScreen.UnregisterSceneListeners(this);
		this.UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.UpdatePerformanceTrackingFromModeSwitch));
		HearthstoneApplication.Get().WillReset -= this.WillReset;
	}

	// Token: 0x06005DD3 RID: 24019 RVA: 0x001E8FB0 File Offset: 0x001E71B0
	public void LoadShaderPreCompiler()
	{
		if (PlatformSettings.IsMobile() && PlatformSettings.RuntimeOS != OSCategory.Android)
		{
			AssetReference assetReference = new AssetReference("ShaderPreCompiler.prefab:380ca3ee11a2643068cfb3d4766f3fd3");
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(assetReference, AssetLoadingOptions.None);
			if (gameObject == null)
			{
				Debug.LogError(string.Format("SceneMgr.LoadShaderPreCompiler() - FAILED to load prefab", assetReference));
				return;
			}
			gameObject.transform.parent = this.SceneObject.transform;
		}
	}

	// Token: 0x06005DD4 RID: 24020 RVA: 0x001E901C File Offset: 0x001E721C
	public void Update()
	{
		if (!this.m_reloadMode)
		{
			if (this.m_nextMode == SceneMgr.Mode.INVALID)
			{
				return;
			}
			if (this.m_mode == this.m_nextMode)
			{
				this.m_nextMode = SceneMgr.Mode.INVALID;
				return;
			}
		}
		this.m_transitioning = true;
		this.m_performFullCleanup = !this.m_reloadMode;
		this.m_prevMode = this.m_mode;
		this.m_mode = this.m_nextMode;
		this.m_nextMode = SceneMgr.Mode.INVALID;
		this.m_reloadMode = false;
		if (this.m_scene != null)
		{
			if (this.m_switchModeCoroutine != null)
			{
				Processor.CancelCoroutine(this.m_switchModeCoroutine);
			}
			IEnumerator routine = this.IsDoingSceneDrivenTransition() ? this.SwitchModeWithSceneDrivenTransition() : this.SwitchMode();
			this.m_switchModeCoroutine = Processor.RunCoroutine(routine, this);
			return;
		}
		this.LoadMode();
	}

	// Token: 0x06005DD5 RID: 24021 RVA: 0x001E90D8 File Offset: 0x001E72D8
	public static SceneMgr Get()
	{
		if (SceneMgr.s_instance == null)
		{
			SceneMgr.s_instance = HearthstoneServices.Get<SceneMgr>();
		}
		return SceneMgr.s_instance;
	}

	// Token: 0x06005DD6 RID: 24022 RVA: 0x001E90F0 File Offset: 0x001E72F0
	public static bool IsInitialized()
	{
		return SceneMgr.s_instance != null;
	}

	// Token: 0x06005DD7 RID: 24023 RVA: 0x001E90FC File Offset: 0x001E72FC
	private void WillReset()
	{
		Log.Reset.Print("SceneMgr.WillReset()", Array.Empty<object>());
		if (HearthstoneApplication.IsPublic())
		{
			TimeScaleMgr.Get().SetGameTimeScale(1f);
			TimeScaleMgr.Get().SetTimeScaleMultiplier(1f);
		}
		Processor.StopAllCoroutinesWithObjectRef(this);
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		this.m_mode = SceneMgr.Mode.STARTUP;
		this.m_nextMode = SceneMgr.Mode.INVALID;
		this.m_prevMode = SceneMgr.Mode.INVALID;
		this.m_reloadMode = false;
		PegasusScene scene = this.m_scene;
		if (scene != null)
		{
			scene.PreUnload();
		}
		this.FireScenePreUnloadEvent(scene);
		if (this.m_scene != null)
		{
			this.m_scene.Unload();
			this.m_scene = null;
			this.m_sceneLoaded = false;
		}
		this.FireSceneUnloadedEvent(scene);
		this.PostUnloadCleanup();
		this.QueueLoadBoxJob();
		Log.Reset.Print("\tSceneMgr.WillReset() completed", Array.Empty<object>());
	}

	// Token: 0x06005DD8 RID: 24024 RVA: 0x001E91E8 File Offset: 0x001E73E8
	public void SetNextMode(SceneMgr.Mode mode, SceneMgr.TransitionHandlerType transitionHandler = SceneMgr.TransitionHandlerType.SCENEMGR, SceneMgr.OnSceneLoadCompleteForSceneDrivenTransition onLoadCompleteCallback = null)
	{
		if (this.IsModeRequested(SceneMgr.Mode.FATAL_ERROR))
		{
			return;
		}
		this.CacheModeForResume(mode);
		this.m_nextMode = mode;
		this.m_reloadMode = false;
		this.m_transitionHandlerType = transitionHandler;
		if (transitionHandler == SceneMgr.TransitionHandlerType.CURRENT_SCENE || transitionHandler == SceneMgr.TransitionHandlerType.NEXT_SCENE)
		{
			if (transitionHandler == SceneMgr.TransitionHandlerType.CURRENT_SCENE && onLoadCompleteCallback == null)
			{
				Log.All.PrintError("SceneMgr - SetNextMode did not provide the required callback!", Array.Empty<object>());
			}
			this.m_onSceneLoadCompleteForSceneDrivenTransitionCallback = onLoadCompleteCallback;
		}
	}

	// Token: 0x06005DD9 RID: 24025 RVA: 0x001E9246 File Offset: 0x001E7446
	public void ReloadMode()
	{
		if (this.IsModeRequested(SceneMgr.Mode.FATAL_ERROR))
		{
			return;
		}
		this.m_nextMode = this.m_mode;
		this.m_reloadMode = true;
	}

	// Token: 0x06005DDA RID: 24026 RVA: 0x001E9266 File Offset: 0x001E7466
	public void ReturnToPreviousMode()
	{
		if (this.IsModeRequested(SceneMgr.Mode.FATAL_ERROR))
		{
			return;
		}
		this.CacheModeForResume(this.m_prevMode);
		this.m_nextMode = this.m_prevMode;
		this.m_reloadMode = false;
	}

	// Token: 0x06005DDB RID: 24027 RVA: 0x001E9292 File Offset: 0x001E7492
	public SceneMgr.Mode GetPrevMode()
	{
		return this.m_prevMode;
	}

	// Token: 0x06005DDC RID: 24028 RVA: 0x001E929A File Offset: 0x001E749A
	public SceneMgr.Mode GetMode()
	{
		return this.m_mode;
	}

	// Token: 0x06005DDD RID: 24029 RVA: 0x001E92A2 File Offset: 0x001E74A2
	public SceneMgr.Mode GetNextMode()
	{
		return this.m_nextMode;
	}

	// Token: 0x06005DDE RID: 24030 RVA: 0x001E92AA File Offset: 0x001E74AA
	public PegasusScene GetScene()
	{
		return this.m_scene;
	}

	// Token: 0x06005DDF RID: 24031 RVA: 0x001E92B2 File Offset: 0x001E74B2
	public void SetScene(PegasusScene scene)
	{
		this.m_scene = scene;
	}

	// Token: 0x06005DE0 RID: 24032 RVA: 0x001E92BB File Offset: 0x001E74BB
	public bool IsSceneLoaded()
	{
		return this.m_sceneLoaded;
	}

	// Token: 0x06005DE1 RID: 24033 RVA: 0x001E92C3 File Offset: 0x001E74C3
	public bool WillTransition()
	{
		return this.m_reloadMode || (this.m_nextMode != SceneMgr.Mode.INVALID && this.m_nextMode != this.m_mode);
	}

	// Token: 0x06005DE2 RID: 24034 RVA: 0x001E92EA File Offset: 0x001E74EA
	public bool IsTransitioning()
	{
		return this.m_transitioning;
	}

	// Token: 0x06005DE3 RID: 24035 RVA: 0x001E92F2 File Offset: 0x001E74F2
	public bool IsTransitionNowOrPending()
	{
		return this.IsTransitioning() || this.WillTransition();
	}

	// Token: 0x06005DE4 RID: 24036 RVA: 0x001E9309 File Offset: 0x001E7509
	public bool IsDoingSceneDrivenTransition()
	{
		return this.m_transitionHandlerType == SceneMgr.TransitionHandlerType.CURRENT_SCENE || this.m_transitionHandlerType == SceneMgr.TransitionHandlerType.NEXT_SCENE;
	}

	// Token: 0x06005DE5 RID: 24037 RVA: 0x001E931F File Offset: 0x001E751F
	public bool IsModeRequested(SceneMgr.Mode mode)
	{
		return this.m_mode == mode || this.m_nextMode == mode;
	}

	// Token: 0x06005DE6 RID: 24038 RVA: 0x001E9338 File Offset: 0x001E7538
	public bool IsInGame()
	{
		return this.IsModeRequested(SceneMgr.Mode.GAMEPLAY);
	}

	// Token: 0x06005DE7 RID: 24039 RVA: 0x001E9341 File Offset: 0x001E7541
	public bool IsInTavernBrawlMode()
	{
		return this.GetMode() == SceneMgr.Mode.TAVERN_BRAWL || (this.GetMode() == SceneMgr.Mode.FIRESIDE_GATHERING && FiresideGatheringManager.Get().InBrawlMode());
	}

	// Token: 0x06005DE8 RID: 24040 RVA: 0x001E9365 File Offset: 0x001E7565
	public bool IsInDuelsMode()
	{
		return this.GetMode() == SceneMgr.Mode.PVP_DUNGEON_RUN;
	}

	// Token: 0x06005DE9 RID: 24041 RVA: 0x001E9371 File Offset: 0x001E7571
	public void NotifySceneLoaded()
	{
		this.m_sceneLoaded = true;
		if (this.ShouldUseSceneLoadDelays())
		{
			Processor.RunCoroutine(this.WaitThenFireSceneLoadedEvent(), this);
			return;
		}
		this.FireSceneLoadedEvent();
	}

	// Token: 0x06005DEA RID: 24042 RVA: 0x001E9396 File Offset: 0x001E7596
	public void RegisterScenePreUnloadEvent(SceneMgr.ScenePreUnloadCallback callback)
	{
		this.RegisterScenePreUnloadEvent(callback, null);
	}

	// Token: 0x06005DEB RID: 24043 RVA: 0x001E93A0 File Offset: 0x001E75A0
	public void RegisterScenePreUnloadEvent(SceneMgr.ScenePreUnloadCallback callback, object userData)
	{
		SceneMgr.ScenePreUnloadListener scenePreUnloadListener = new SceneMgr.ScenePreUnloadListener();
		scenePreUnloadListener.SetCallback(callback);
		scenePreUnloadListener.SetUserData(userData);
		if (this.m_scenePreUnloadListeners.Contains(scenePreUnloadListener))
		{
			return;
		}
		this.m_scenePreUnloadListeners.Add(scenePreUnloadListener);
	}

	// Token: 0x06005DEC RID: 24044 RVA: 0x001E93DC File Offset: 0x001E75DC
	public bool UnregisterScenePreUnloadEvent(SceneMgr.ScenePreUnloadCallback callback)
	{
		return this.UnregisterScenePreUnloadEvent(callback, null);
	}

	// Token: 0x06005DED RID: 24045 RVA: 0x001E93E8 File Offset: 0x001E75E8
	public bool UnregisterScenePreUnloadEvent(SceneMgr.ScenePreUnloadCallback callback, object userData)
	{
		SceneMgr.ScenePreUnloadListener scenePreUnloadListener = new SceneMgr.ScenePreUnloadListener();
		scenePreUnloadListener.SetCallback(callback);
		scenePreUnloadListener.SetUserData(userData);
		return this.m_scenePreUnloadListeners.Remove(scenePreUnloadListener);
	}

	// Token: 0x06005DEE RID: 24046 RVA: 0x001E9415 File Offset: 0x001E7615
	public static bool UnregisterScenePreUnloadEventFromInstance(SceneMgr.ScenePreUnloadCallback callback)
	{
		return SceneMgr.s_instance != null && SceneMgr.s_instance.UnregisterScenePreUnloadEvent(callback);
	}

	// Token: 0x06005DEF RID: 24047 RVA: 0x001E942B File Offset: 0x001E762B
	public void RegisterSceneUnloadedEvent(SceneMgr.SceneUnloadedCallback callback)
	{
		this.RegisterSceneUnloadedEvent(callback, null);
	}

	// Token: 0x06005DF0 RID: 24048 RVA: 0x001E9438 File Offset: 0x001E7638
	public void RegisterSceneUnloadedEvent(SceneMgr.SceneUnloadedCallback callback, object userData)
	{
		SceneMgr.SceneUnloadedListener sceneUnloadedListener = new SceneMgr.SceneUnloadedListener();
		sceneUnloadedListener.SetCallback(callback);
		sceneUnloadedListener.SetUserData(userData);
		if (this.m_sceneUnloadedListeners.Contains(sceneUnloadedListener))
		{
			return;
		}
		this.m_sceneUnloadedListeners.Add(sceneUnloadedListener);
	}

	// Token: 0x06005DF1 RID: 24049 RVA: 0x001E9474 File Offset: 0x001E7674
	public bool UnregisterSceneUnloadedEvent(SceneMgr.SceneUnloadedCallback callback)
	{
		return this.UnregisterSceneUnloadedEvent(callback, null);
	}

	// Token: 0x06005DF2 RID: 24050 RVA: 0x001E9480 File Offset: 0x001E7680
	public bool UnregisterSceneUnloadedEvent(SceneMgr.SceneUnloadedCallback callback, object userData)
	{
		SceneMgr.SceneUnloadedListener sceneUnloadedListener = new SceneMgr.SceneUnloadedListener();
		sceneUnloadedListener.SetCallback(callback);
		sceneUnloadedListener.SetUserData(userData);
		return this.m_sceneUnloadedListeners.Remove(sceneUnloadedListener);
	}

	// Token: 0x06005DF3 RID: 24051 RVA: 0x001E94AD File Offset: 0x001E76AD
	public void RegisterScenePreLoadEvent(SceneMgr.ScenePreLoadCallback callback)
	{
		this.RegisterScenePreLoadEvent(callback, null);
	}

	// Token: 0x06005DF4 RID: 24052 RVA: 0x001E94B8 File Offset: 0x001E76B8
	public void RegisterScenePreLoadEvent(SceneMgr.ScenePreLoadCallback callback, object userData)
	{
		SceneMgr.ScenePreLoadListener scenePreLoadListener = new SceneMgr.ScenePreLoadListener();
		scenePreLoadListener.SetCallback(callback);
		scenePreLoadListener.SetUserData(userData);
		if (this.m_scenePreLoadListeners.Contains(scenePreLoadListener))
		{
			return;
		}
		this.m_scenePreLoadListeners.Add(scenePreLoadListener);
	}

	// Token: 0x06005DF5 RID: 24053 RVA: 0x001E94F4 File Offset: 0x001E76F4
	public bool UnregisterScenePreLoadEvent(SceneMgr.ScenePreLoadCallback callback)
	{
		return this.UnregisterScenePreLoadEvent(callback, null);
	}

	// Token: 0x06005DF6 RID: 24054 RVA: 0x001E9500 File Offset: 0x001E7700
	public bool UnregisterScenePreLoadEvent(SceneMgr.ScenePreLoadCallback callback, object userData)
	{
		SceneMgr.ScenePreLoadListener scenePreLoadListener = new SceneMgr.ScenePreLoadListener();
		scenePreLoadListener.SetCallback(callback);
		scenePreLoadListener.SetUserData(userData);
		return this.m_scenePreLoadListeners.Remove(scenePreLoadListener);
	}

	// Token: 0x06005DF7 RID: 24055 RVA: 0x001E952D File Offset: 0x001E772D
	public void RegisterSceneLoadedEvent(SceneMgr.SceneLoadedCallback callback)
	{
		this.RegisterSceneLoadedEvent(callback, null);
	}

	// Token: 0x06005DF8 RID: 24056 RVA: 0x001E9538 File Offset: 0x001E7738
	public void RegisterSceneLoadedEvent(SceneMgr.SceneLoadedCallback callback, object userData)
	{
		SceneMgr.SceneLoadedListener sceneLoadedListener = new SceneMgr.SceneLoadedListener();
		sceneLoadedListener.SetCallback(callback);
		sceneLoadedListener.SetUserData(userData);
		if (this.m_sceneLoadedListeners.Contains(sceneLoadedListener))
		{
			return;
		}
		this.m_sceneLoadedListeners.Add(sceneLoadedListener);
	}

	// Token: 0x06005DF9 RID: 24057 RVA: 0x001E9574 File Offset: 0x001E7774
	public bool UnregisterSceneLoadedEvent(SceneMgr.SceneLoadedCallback callback)
	{
		return this.UnregisterSceneLoadedEvent(callback, null);
	}

	// Token: 0x06005DFA RID: 24058 RVA: 0x001E9580 File Offset: 0x001E7780
	public bool UnregisterSceneLoadedEvent(SceneMgr.SceneLoadedCallback callback, object userData)
	{
		SceneMgr.SceneLoadedListener sceneLoadedListener = new SceneMgr.SceneLoadedListener();
		sceneLoadedListener.SetCallback(callback);
		sceneLoadedListener.SetUserData(userData);
		return this.m_sceneLoadedListeners.Remove(sceneLoadedListener);
	}

	// Token: 0x06005DFB RID: 24059 RVA: 0x001E95AD File Offset: 0x001E77AD
	private IEnumerator WaitThenFireSceneLoadedEvent()
	{
		yield return new WaitForSeconds(0.15f);
		this.FireSceneLoadedEvent();
		yield break;
	}

	// Token: 0x06005DFC RID: 24060 RVA: 0x001E95BC File Offset: 0x001E77BC
	private void FireScenePreUnloadEvent(PegasusScene prevScene)
	{
		SceneMgr.ScenePreUnloadListener[] array = this.m_scenePreUnloadListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(this.m_prevMode, prevScene);
		}
	}

	// Token: 0x06005DFD RID: 24061 RVA: 0x001E95F4 File Offset: 0x001E77F4
	private void FireSceneUnloadedEvent(PegasusScene prevScene)
	{
		if (this.IsDoingSceneDrivenTransition())
		{
			this.m_transitioning = false;
		}
		SceneMgr.SceneUnloadedListener[] array = this.m_sceneUnloadedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(this.m_prevMode, prevScene);
		}
	}

	// Token: 0x06005DFE RID: 24062 RVA: 0x001E963C File Offset: 0x001E783C
	private void FireScenePreLoadEvent()
	{
		SceneMgr.ScenePreLoadListener[] array = this.m_scenePreLoadListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(this.m_prevMode, this.m_mode);
		}
	}

	// Token: 0x06005DFF RID: 24063 RVA: 0x001E9678 File Offset: 0x001E7878
	private void FireSceneLoadedEvent()
	{
		if (!this.IsDoingSceneDrivenTransition())
		{
			this.m_transitioning = false;
		}
		SceneMgr.SceneLoadedListener[] array = this.m_sceneLoadedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(this.m_mode, this.m_scene);
		}
		HearthstonePerformance hearthstonePerformance = HearthstonePerformance.Get();
		if (hearthstonePerformance == null)
		{
			return;
		}
		hearthstonePerformance.SendCustomEvent("SceneLoaded" + Enum.GetName(typeof(SceneMgr.Mode), this.m_mode));
	}

	// Token: 0x06005E00 RID: 24064 RVA: 0x001E96F5 File Offset: 0x001E78F5
	private void LoadMode()
	{
		this.FireScenePreLoadEvent();
		SceneManager.LoadSceneAsync(EnumUtils.GetString<SceneMgr.Mode>(this.m_mode), LoadSceneMode.Additive);
	}

	// Token: 0x06005E01 RID: 24065 RVA: 0x001E970F File Offset: 0x001E790F
	private IEnumerator SwitchMode()
	{
		if (this.m_scene.IsUnloading())
		{
			yield break;
		}
		PegasusScene prevScene = this.m_scene;
		prevScene.PreUnload();
		this.FireScenePreUnloadEvent(prevScene);
		if (this.LoadingScreen.GetPhase() == LoadingScreen.Phase.WAITING_FOR_SCENE_UNLOAD && this.LoadingScreen.GetFreezeFrameCamera() != null)
		{
			yield return new WaitForEndOfFrame();
		}
		if (this.ShouldUseSceneUnloadDelays())
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
		this.m_scene.Unload();
		this.m_scene = null;
		this.m_sceneLoaded = false;
		this.FireSceneUnloadedEvent(prevScene);
		this.PostUnloadCleanup();
		this.LoadModeFromModeSwitch();
		yield break;
	}

	// Token: 0x06005E02 RID: 24066 RVA: 0x001E971E File Offset: 0x001E791E
	private IEnumerator SwitchModeWithSceneDrivenTransition()
	{
		if (this.m_scene.IsUnloading())
		{
			yield break;
		}
		PegasusScene prevScene = this.m_scene;
		this.m_sceneLoaded = false;
		this.FireScenePreLoadEvent();
		SceneManager.LoadSceneAsync(EnumUtils.GetString<SceneMgr.Mode>(this.m_mode), LoadSceneMode.Additive);
		while (!this.m_sceneLoaded)
		{
			yield return null;
		}
		Action action = delegate()
		{
			prevScene.PreUnload();
			this.FireScenePreUnloadEvent(prevScene);
			prevScene.Unload();
			this.FireSceneUnloadedEvent(prevScene);
		};
		if (this.m_transitionHandlerType == SceneMgr.TransitionHandlerType.CURRENT_SCENE && this.m_onSceneLoadCompleteForSceneDrivenTransitionCallback != null)
		{
			this.m_onSceneLoadCompleteForSceneDrivenTransitionCallback(action);
			this.m_onSceneLoadCompleteForSceneDrivenTransitionCallback = null;
		}
		else if (this.m_transitionHandlerType == SceneMgr.TransitionHandlerType.NEXT_SCENE)
		{
			this.m_scene.ExecuteSceneDrivenTransition(action);
		}
		else
		{
			Log.All.PrintError("No callback for scene driven scene transition.", Array.Empty<object>());
			action();
		}
		yield break;
	}

	// Token: 0x06005E03 RID: 24067 RVA: 0x001E972D File Offset: 0x001E792D
	private bool ShouldUseSceneUnloadDelays()
	{
		return this.m_prevMode != this.m_mode;
	}

	// Token: 0x06005E04 RID: 24068 RVA: 0x001E9740 File Offset: 0x001E7940
	private bool ShouldUseSceneLoadDelays()
	{
		return this.m_mode != SceneMgr.Mode.LOGIN && this.m_mode != SceneMgr.Mode.HUB && this.m_mode != SceneMgr.Mode.FATAL_ERROR;
	}

	// Token: 0x06005E05 RID: 24069 RVA: 0x001E9768 File Offset: 0x001E7968
	private void PostUnloadCleanup()
	{
		Time.captureFramerate = 0;
		if (Application.isEditor && this.m_mode == SceneMgr.Mode.FATAL_ERROR)
		{
			Log.All.PrintWarning("Not destroying the previous scene's objects for easier debugging! You can (probably) ignore warnings or errors after this point!", Array.Empty<object>());
			AudioListener[] array = UnityEngine.Object.FindObjectsOfType<AudioListener>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
			return;
		}
		this.DestroyAllObjectsOnModeSwitch();
		if (this.m_performFullCleanup)
		{
			HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
			if (hearthstoneApplication != null)
			{
				hearthstoneApplication.UnloadUnusedAssets();
			}
		}
	}

	// Token: 0x06005E06 RID: 24070 RVA: 0x001E97E4 File Offset: 0x001E79E4
	private void DestroyAllObjectsOnModeSwitch()
	{
		int sceneCount = SceneManager.sceneCount;
		for (int i = 0; i < sceneCount; i++)
		{
			foreach (GameObject gameObject in SceneManager.GetSceneAt(i).GetRootGameObjects())
			{
				if (this.ShouldDestroyOnModeSwitch(gameObject))
				{
					UnityEngine.Object.DestroyImmediate(gameObject);
				}
			}
		}
	}

	// Token: 0x06005E07 RID: 24071 RVA: 0x001E983C File Offset: 0x001E7A3C
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
			Debug.LogErrorFormat("GameObject ({0}) appears to be marked Don't Destroy On Load, but is being destroyed by our code anyway!", new object[]
			{
				go.name
			});
		}
		return (!(PegUI.Get() != null) || !(go == PegUI.Get().gameObject)) && (!(OverlayUI.Get() != null) || !(go == OverlayUI.Get().gameObject)) && (!(Box.Get() != null) || !(go == Box.Get().gameObject) || !this.DoesModeShowBox(this.m_mode)) && !AssetLoader.Get().IsSharedPrefabInstance(go) && !AssetLoader.Get().IsWaitingOnObject(go) && !(go == iTweenManager.Get().gameObject);
	}

	// Token: 0x06005E08 RID: 24072 RVA: 0x001E9948 File Offset: 0x001E7B48
	private void CacheModeForResume(SceneMgr.Mode mode)
	{
		if (PlatformSettings.OS != OSCategory.iOS && PlatformSettings.OS != OSCategory.Android)
		{
			return;
		}
		switch (mode)
		{
		case SceneMgr.Mode.HUB:
		case SceneMgr.Mode.FRIENDLY:
			Options.Get().SetInt(Option.LAST_SCENE_MODE, 0);
			return;
		case SceneMgr.Mode.GAMEPLAY:
		case SceneMgr.Mode.PACKOPENING:
		case SceneMgr.Mode.FATAL_ERROR:
		case SceneMgr.Mode.RESET:
			break;
		case SceneMgr.Mode.COLLECTIONMANAGER:
		case SceneMgr.Mode.TOURNAMENT:
		case SceneMgr.Mode.DRAFT:
		case SceneMgr.Mode.CREDITS:
		case SceneMgr.Mode.ADVENTURE:
		case SceneMgr.Mode.TAVERN_BRAWL:
		case SceneMgr.Mode.FIRESIDE_GATHERING:
		case SceneMgr.Mode.BACON:
		case SceneMgr.Mode.GAME_MODE:
			Options.Get().SetInt(Option.LAST_SCENE_MODE, (int)mode);
			break;
		default:
			return;
		}
	}

	// Token: 0x06005E09 RID: 24073 RVA: 0x001E99C6 File Offset: 0x001E7BC6
	private bool DoesModeShowBox(SceneMgr.Mode mode)
	{
		if (mode <= SceneMgr.Mode.GAMEPLAY)
		{
			if (mode != SceneMgr.Mode.STARTUP && mode != SceneMgr.Mode.GAMEPLAY)
			{
				return true;
			}
		}
		else if (mode != SceneMgr.Mode.FATAL_ERROR && mode != SceneMgr.Mode.RESET)
		{
			return true;
		}
		return false;
	}

	// Token: 0x06005E0A RID: 24074 RVA: 0x001E99E4 File Offset: 0x001E7BE4
	private void LoadModeFromModeSwitch()
	{
		bool flag = this.DoesModeShowBox(this.m_prevMode);
		bool flag2 = this.DoesModeShowBox(this.m_mode);
		if (!flag && flag2)
		{
			Processor.QueueJob("SceneMgr.Reload", this.Job_ReloadBox(), Array.Empty<IJobDependency>());
			return;
		}
		if (flag && !flag2)
		{
			this.LoadingScreen.SetAssetLoadStartTimestamp(this.m_boxLoadTimestamp);
			this.m_boxLoadTimestamp = 0L;
		}
		this.LoadMode();
	}

	// Token: 0x06005E0B RID: 24075 RVA: 0x001E9A58 File Offset: 0x001E7C58
	private void QueueLoadBoxJob()
	{
		IJobDependency[] dependencies = HearthstoneJobs.BuildDependencies(new object[]
		{
			typeof(SceneMgr),
			typeof(IAssetLoader),
			typeof(NetCache),
			new WaitForGameDownloadManagerState(),
			new WaitForSplashScreen()
		});
		Processor.QueueJob("SceneMgr.LoadBox", this.Job_LoadBox(), dependencies);
	}

	// Token: 0x06005E0C RID: 24076 RVA: 0x001E9ABA File Offset: 0x001E7CBA
	private IEnumerator<IAsyncJobResult> Job_LoadBox()
	{
		LoadUIScreen loadUIScreen = new LoadUIScreen("TheBox.prefab:6b55a928ffdc1b341b5dbe8f8a88e768");
		yield return loadUIScreen;
		this.m_nextMode = SceneMgr.Mode.LOGIN;
		yield break;
	}

	// Token: 0x06005E0D RID: 24077 RVA: 0x001E9AC9 File Offset: 0x001E7CC9
	private IEnumerator<IAsyncJobResult> Job_ReloadBox()
	{
		LoadUIScreen loadUIScreen = new LoadUIScreen("TheBox.prefab:6b55a928ffdc1b341b5dbe8f8a88e768");
		yield return loadUIScreen;
		this.LoadMode();
		yield break;
	}

	// Token: 0x06005E0E RID: 24078 RVA: 0x001E9AD8 File Offset: 0x001E7CD8
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.SET_ROTATION_INTRO))
		{
			Log.Offline.Print("SceneMgr.OnFatalError: Error blocked by set rotation.", Array.Empty<object>());
			this.SetNextMode(SceneMgr.Mode.FATAL_ERROR, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			return;
		}
		if (!ReconnectMgr.IsReconnectAllowed(message))
		{
			FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
			ReconnectMgr reconnectMgr;
			if (HearthstoneServices.TryGet<ReconnectMgr>(out reconnectMgr))
			{
				reconnectMgr.FullResetRequired = true;
			}
			this.GoToFatalErrorScreen(message);
			return;
		}
		switch (this.m_mode)
		{
		case SceneMgr.Mode.STARTUP:
		case SceneMgr.Mode.PACKOPENING:
		case SceneMgr.Mode.TOURNAMENT:
		case SceneMgr.Mode.CREDITS:
			return;
		case SceneMgr.Mode.LOGIN:
		case SceneMgr.Mode.GAMEPLAY:
			this.GoToFatalErrorScreen(message);
			return;
		case SceneMgr.Mode.HUB:
			StoreManager.Get().HandleDisconnect();
			return;
		case SceneMgr.Mode.COLLECTIONMANAGER:
			CollectionManager.Get().HandleDisconnect();
			return;
		case SceneMgr.Mode.TAVERN_BRAWL:
		{
			CollectionManager collectionManager = CollectionManager.Get();
			if (collectionManager.IsInEditMode())
			{
				collectionManager.HandleDisconnect();
				return;
			}
			return;
		}
		}
		Log.Offline.PrintDebug("Bypassing Fatal Error To HUB.", Array.Empty<object>());
		Navigation.Clear();
		this.SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		DialogManager.Get().ShowReconnectHelperDialog(null, null);
	}

	// Token: 0x06005E0F RID: 24079 RVA: 0x001E9BFC File Offset: 0x001E7DFC
	private void GoToFatalErrorScreen(FatalErrorMessage message)
	{
		if (HearthstoneApplication.Get().ResetOnErrorIfNecessary())
		{
			Log.Offline.PrintDebug("SceneMgr.GoToFatalErrorScreen() - Auto resetting. Do not display Fatal Error Screen.", Array.Empty<object>());
			return;
		}
		Log.BattleNet.PrintDebug("Set FatalError mode={0}, m_allowClick={1}, m_redirectToStore={2}", new object[]
		{
			this.m_mode,
			message.m_allowClick,
			message.m_redirectToStore
		});
		FatalErrorMgr.Get().SetUnrecoverable(this.m_mode == SceneMgr.Mode.STARTUP && (!message.m_allowClick || !message.m_redirectToStore));
		this.SetNextMode(SceneMgr.Mode.FATAL_ERROR, SceneMgr.TransitionHandlerType.SCENEMGR, null);
	}

	// Token: 0x06005E10 RID: 24080 RVA: 0x001E9CA0 File Offset: 0x001E7EA0
	public bool DoesCurrentSceneSupportOfflineActivity()
	{
		switch (this.m_mode)
		{
		case SceneMgr.Mode.STARTUP:
		case SceneMgr.Mode.HUB:
		case SceneMgr.Mode.COLLECTIONMANAGER:
		case SceneMgr.Mode.PACKOPENING:
		case SceneMgr.Mode.TOURNAMENT:
		case SceneMgr.Mode.CREDITS:
		case SceneMgr.Mode.TAVERN_BRAWL:
			return true;
		}
		return false;
	}

	// Token: 0x06005E11 RID: 24081 RVA: 0x001E9D08 File Offset: 0x001E7F08
	private void UpdatePerformanceTrackingFromModeSwitch(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			HearthstonePerformance hearthstonePerformance = HearthstonePerformance.Get();
			if (hearthstonePerformance != null)
			{
				hearthstonePerformance.CaptureBoxInteractableTime();
				this.UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.UpdatePerformanceTrackingFromModeSwitch));
			}
		}
	}

	// Token: 0x04004F38 RID: 20280
	public GameObject m_StartupCamera;

	// Token: 0x04004F39 RID: 20281
	private const float SCENE_UNLOAD_DELAY = 0.15f;

	// Token: 0x04004F3A RID: 20282
	private const float SCENE_LOADED_DELAY = 0.15f;

	// Token: 0x04004F3B RID: 20283
	private static SceneMgr s_instance;

	// Token: 0x04004F3C RID: 20284
	private int m_startupAssetLoads;

	// Token: 0x04004F3D RID: 20285
	private SceneMgr.Mode m_mode = SceneMgr.Mode.STARTUP;

	// Token: 0x04004F3E RID: 20286
	private SceneMgr.Mode m_nextMode;

	// Token: 0x04004F3F RID: 20287
	private SceneMgr.Mode m_prevMode;

	// Token: 0x04004F40 RID: 20288
	private bool m_reloadMode;

	// Token: 0x04004F41 RID: 20289
	private PegasusScene m_scene;

	// Token: 0x04004F42 RID: 20290
	private bool m_sceneLoaded;

	// Token: 0x04004F43 RID: 20291
	private bool m_transitioning;

	// Token: 0x04004F44 RID: 20292
	private bool m_performFullCleanup;

	// Token: 0x04004F45 RID: 20293
	private List<SceneMgr.ScenePreUnloadListener> m_scenePreUnloadListeners = new List<SceneMgr.ScenePreUnloadListener>();

	// Token: 0x04004F46 RID: 20294
	private List<SceneMgr.SceneUnloadedListener> m_sceneUnloadedListeners = new List<SceneMgr.SceneUnloadedListener>();

	// Token: 0x04004F47 RID: 20295
	private List<SceneMgr.ScenePreLoadListener> m_scenePreLoadListeners = new List<SceneMgr.ScenePreLoadListener>();

	// Token: 0x04004F48 RID: 20296
	private List<SceneMgr.SceneLoadedListener> m_sceneLoadedListeners = new List<SceneMgr.SceneLoadedListener>();

	// Token: 0x04004F49 RID: 20297
	private SceneMgr.OnSceneLoadCompleteForSceneDrivenTransition m_onSceneLoadCompleteForSceneDrivenTransitionCallback;

	// Token: 0x04004F4A RID: 20298
	private SceneMgr.TransitionHandlerType m_transitionHandlerType;

	// Token: 0x04004F4B RID: 20299
	private long m_boxLoadTimestamp;

	// Token: 0x04004F4C RID: 20300
	private Coroutine m_switchModeCoroutine;

	// Token: 0x04004F4E RID: 20302
	private GameObject m_sceneObject;

	// Token: 0x020021AC RID: 8620
	public enum Mode
	{
		// Token: 0x0400E105 RID: 57605
		INVALID,
		// Token: 0x0400E106 RID: 57606
		STARTUP,
		// Token: 0x0400E107 RID: 57607
		[Description("Login")]
		LOGIN,
		// Token: 0x0400E108 RID: 57608
		[Description("Hub")]
		HUB,
		// Token: 0x0400E109 RID: 57609
		[Description("Gameplay")]
		GAMEPLAY,
		// Token: 0x0400E10A RID: 57610
		[Description("CollectionManager")]
		COLLECTIONMANAGER,
		// Token: 0x0400E10B RID: 57611
		[Description("PackOpening")]
		PACKOPENING,
		// Token: 0x0400E10C RID: 57612
		[Description("Tournament")]
		TOURNAMENT,
		// Token: 0x0400E10D RID: 57613
		[Description("Friendly")]
		FRIENDLY,
		// Token: 0x0400E10E RID: 57614
		[Description("FatalError")]
		FATAL_ERROR,
		// Token: 0x0400E10F RID: 57615
		[Description("Draft")]
		DRAFT,
		// Token: 0x0400E110 RID: 57616
		[Description("Credits")]
		CREDITS,
		// Token: 0x0400E111 RID: 57617
		[Description("Reset")]
		RESET,
		// Token: 0x0400E112 RID: 57618
		[Description("Adventure")]
		ADVENTURE,
		// Token: 0x0400E113 RID: 57619
		[Description("TavernBrawl")]
		TAVERN_BRAWL,
		// Token: 0x0400E114 RID: 57620
		[Description("FiresideGathering")]
		FIRESIDE_GATHERING,
		// Token: 0x0400E115 RID: 57621
		[Description("Bacon")]
		BACON,
		// Token: 0x0400E116 RID: 57622
		[Description("GameMode")]
		GAME_MODE,
		// Token: 0x0400E117 RID: 57623
		[Description("PvPDungeonRun")]
		PVP_DUNGEON_RUN
	}

	// Token: 0x020021AD RID: 8621
	public enum TransitionHandlerType
	{
		// Token: 0x0400E119 RID: 57625
		INVALID,
		// Token: 0x0400E11A RID: 57626
		SCENEMGR,
		// Token: 0x0400E11B RID: 57627
		CURRENT_SCENE,
		// Token: 0x0400E11C RID: 57628
		NEXT_SCENE
	}

	// Token: 0x020021AE RID: 8622
	// (Invoke) Token: 0x06012464 RID: 74852
	public delegate void ScenePreUnloadCallback(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData);

	// Token: 0x020021AF RID: 8623
	// (Invoke) Token: 0x06012468 RID: 74856
	public delegate void SceneUnloadedCallback(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData);

	// Token: 0x020021B0 RID: 8624
	// (Invoke) Token: 0x0601246C RID: 74860
	public delegate void ScenePreLoadCallback(SceneMgr.Mode prevMode, SceneMgr.Mode mode, object userData);

	// Token: 0x020021B1 RID: 8625
	// (Invoke) Token: 0x06012470 RID: 74864
	public delegate void SceneLoadedCallback(SceneMgr.Mode mode, PegasusScene scene, object userData);

	// Token: 0x020021B2 RID: 8626
	private class ScenePreUnloadListener : EventListener<SceneMgr.ScenePreUnloadCallback>
	{
		// Token: 0x06012473 RID: 74867 RVA: 0x005037AE File Offset: 0x005019AE
		public void Fire(SceneMgr.Mode prevMode, PegasusScene prevScene)
		{
			this.m_callback(prevMode, prevScene, this.m_userData);
		}
	}

	// Token: 0x020021B3 RID: 8627
	private class SceneUnloadedListener : EventListener<SceneMgr.SceneUnloadedCallback>
	{
		// Token: 0x06012475 RID: 74869 RVA: 0x005037CB File Offset: 0x005019CB
		public void Fire(SceneMgr.Mode prevMode, PegasusScene prevScene)
		{
			this.m_callback(prevMode, prevScene, this.m_userData);
		}
	}

	// Token: 0x020021B4 RID: 8628
	private class ScenePreLoadListener : EventListener<SceneMgr.ScenePreLoadCallback>
	{
		// Token: 0x06012477 RID: 74871 RVA: 0x005037E8 File Offset: 0x005019E8
		public void Fire(SceneMgr.Mode prevMode, SceneMgr.Mode mode)
		{
			this.m_callback(prevMode, mode, this.m_userData);
		}
	}

	// Token: 0x020021B5 RID: 8629
	private class SceneLoadedListener : EventListener<SceneMgr.SceneLoadedCallback>
	{
		// Token: 0x06012479 RID: 74873 RVA: 0x00503805 File Offset: 0x00501A05
		public void Fire(SceneMgr.Mode mode, PegasusScene scene)
		{
			this.m_callback(mode, scene, this.m_userData);
		}
	}

	// Token: 0x020021B6 RID: 8630
	// (Invoke) Token: 0x0601247C RID: 74876
	public delegate void OnSceneLoadCompleteForSceneDrivenTransition(Action onTransitionComplete);
}
