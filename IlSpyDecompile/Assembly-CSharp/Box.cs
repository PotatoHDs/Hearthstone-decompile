using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.AssetManager;
using Hearthstone.Progression;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class Box : MonoBehaviour
{
	public enum State
	{
		INVALID,
		STARTUP,
		PRESS_START,
		LOADING,
		LOADING_HUB,
		HUB,
		HUB_WITH_DRAWER,
		OPEN,
		CLOSED,
		ERROR,
		SET_ROTATION_LOADING,
		SET_ROTATION,
		SET_ROTATION_OPEN
	}

	public enum ButtonType
	{
		START,
		TOURNAMENT,
		ADVENTURE,
		FORGE,
		OPEN_PACKS,
		COLLECTION,
		TAVERN_BRAWL,
		SET_ROTATION,
		QUEST_LOG,
		STORE,
		GAME_MODES,
		BACON,
		PVP_DUNGEON_RUN
	}

	public delegate void TransitionFinishedCallback(object userData);

	public delegate void ButtonPressFunction(UIEvent e);

	public delegate void ButtonPressCallback(ButtonType buttonType, object userData);

	private class TransitionFinishedListener : EventListener<TransitionFinishedCallback>
	{
		public void Fire()
		{
			m_callback(m_userData);
		}
	}

	private class ButtonPressListener : EventListener<ButtonPressCallback>
	{
		public void Fire(ButtonType buttonType)
		{
			m_callback(buttonType, m_userData);
		}
	}

	private class BoxStateConfig
	{
		public class Part<T>
		{
			public bool m_ignore;

			public T m_state;
		}

		public Part<BoxLogo.State> m_logoState = new Part<BoxLogo.State>();

		public Part<BoxStartButton.State> m_startButtonState = new Part<BoxStartButton.State>();

		public Part<BoxDoor.State> m_doorState = new Part<BoxDoor.State>();

		public Part<BoxDisk.State> m_diskState = new Part<BoxDisk.State>();

		public Part<BoxDrawer.State> m_drawerState = new Part<BoxDrawer.State>();

		public Part<BoxCamera.State> m_camState = new Part<BoxCamera.State>();

		public Part<bool> m_fullScreenBlackState = new Part<bool>();
	}

	private enum DataState
	{
		NONE,
		REQUEST_SENT,
		RECEIVED,
		UNLOADING
	}

	public AsyncReference m_boxWidgetRef;

	public AsyncReference m_mainShopWidgetRef;

	public GameObject m_rootObject;

	public BoxStateInfoList m_StateInfoList;

	public BoxLogo m_Logo;

	public BoxStartButton m_StartButton;

	public BoxDoor m_LeftDoor;

	public BoxDoor m_RightDoor;

	public BoxDisk m_Disk;

	public GameObject m_DiskCenter;

	public BoxSpinner m_TopSpinner;

	public BoxSpinner m_BottomSpinner;

	public BoxDrawer m_Drawer;

	public BoxCamera m_Camera;

	public Camera m_NoFxCamera;

	public AudioListener m_AudioListener;

	public BoxLightMgr m_LightMgr;

	public BoxEventMgr m_EventMgr;

	public BoxMenuButton m_TournamentButton;

	public BoxMenuButton m_SoloAdventuresButton;

	public TavernBrawlMenuButton m_TavernBrawlButton;

	public GameObject m_EmptyFourthButton;

	public GameObject m_TavernBrawlButtonVisual;

	public GameObject m_TavernBrawlButtonActivateFX;

	public GameObject m_TavernBrawlButtonDeactivateFX;

	public string m_tavernBrawlActivateSound;

	public string m_tavernBrawlDeactivateSound;

	public string m_tavernBrawlPopupSound;

	public string m_tavernBrawlPopdownSound;

	public List<string> m_tavernBrawlEnterCrowdSounds;

	public BoxMenuButton m_GameModesButton;

	public PackOpeningButton m_OpenPacksButton;

	public BoxMenuButton m_CollectionButton;

	public StoreButton m_StoreButton;

	public QuestLogButton m_QuestLogButton;

	public Widget m_journalButtonWidget;

	public Color m_EnabledMaterial;

	public Color m_DisabledMaterial;

	public Color m_EnabledDrawerMaterial;

	public Color m_DisabledDrawerMaterial;

	public GameObject m_OuterFrame;

	public Texture2D m_textureCompressionTest;

	public RibbonButtonsUI m_ribbonButtons;

	public GameObject m_letterboxingContainer;

	public GameObject m_tableTop;

	public GameObject m_firesideGatheringTavernBrawlButtonFX;

	public GameObject m_firesideGatheringTavernBrawlButtonFlag;

	public BoxSpecialEventScriptableObj m_boxSpecialEventObj;

	public Spell m_activeSpecialEvent;

	public bool m_isSpecialEventActive;

	private static Box s_instance;

	private BoxStateConfig[] m_stateConfigs;

	private State m_state = State.STARTUP;

	private int m_pendingEffects;

	private Queue<State> m_stateQueue = new Queue<State>();

	private bool m_transitioningToSceneMode;

	private List<TransitionFinishedListener> m_transitionFinishedListeners = new List<TransitionFinishedListener>();

	private AssetHandle<Texture> m_tableTopTexture;

	private AssetHandle<Texture> m_boxTopTexture;

	private AssetHandle<Texture> m_specialEventTexture;

	private List<ButtonPressListener> m_buttonPressListeners = new List<ButtonPressListener>();

	private ButtonType? m_queuedButtonFire;

	private bool m_waitingForNetData;

	private GameLayer m_originalLeftDoorLayer;

	private GameLayer m_originalRightDoorLayer;

	private GameLayer m_originalDrawerLayer;

	private bool m_waitingForSceneLoad;

	private bool m_showRibbonButtons;

	private bool m_showFSGBanner;

	private const string SHOW_LOG_COROUTINE = "ShowQuestLogWhenReady";

	private bool m_questLogLoading;

	private DataState m_questLogNetCacheDataState;

	private QuestLog m_questLog;

	private GameObject m_tempInputBlocker;

	private GameObject m_setRotationDisk;

	private BoxMenuButton m_setRotationButton;

	private VisualController m_boxVisualController;

	private const string SHOW_NEW_ADVENTURE_BADGE_STATE = "NewAdventureOn";

	private const string HIDE_NEW_ADVENTURE_BADGE_STATE = "NewAdventureOff";

	private const string SHOW_NEW_GAME_MODE_BADGE_STATE = "NewGameModeOn";

	private const string HIDE_NEW_GAME_MODE_BADGE_STATE = "NewGameModeOff";

	public bool IsTavernBrawlButtonDeactivated { get; private set; }

	private void Awake()
	{
		Log.LoadingScreen.Print("Box.Awake()");
		s_instance = this;
		InitializeStateConfigs();
		if (LoadingScreen.Get() != null)
		{
			LoadingScreen.Get().NotifyMainSceneObjectAwoke(base.gameObject);
		}
		m_originalLeftDoorLayer = (GameLayer)m_LeftDoor.gameObject.layer;
		m_originalRightDoorLayer = (GameLayer)m_RightDoor.gameObject.layer;
		m_originalDrawerLayer = (GameLayer)m_Drawer.gameObject.layer;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			if (TransformUtil.GetAspectRatioDependentValue(0f, 1f, 1f) < 0.99f)
			{
				GameUtils.InstantiateGameObject("Letterboxing.prefab:303d7852a40ab4f178a3f97a102a0ea0", m_letterboxingContainer);
			}
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab("RibbonButtons_Phone.prefab:1b805ba741fd649cabb72b2764c755f5");
			m_ribbonButtons = gameObject.GetComponent<RibbonButtonsUI>();
			m_ribbonButtons.Toggle(show: false);
			GameUtils.SetParent(gameObject, m_rootObject);
			AssetLoader.Get().LoadAsset<Texture>("TheBox_Top_phone.psd:666e602b70e7d6344be3e690de329636", OnBoxTopPhoneTextureLoaded);
		}
		if (SpecialEventManager.Get().HasReceivedEventTimingsFromServer)
		{
			DoBoxSpecialEvents();
		}
	}

	private void Start()
	{
		InitializeNet(fromLogin: false);
		InitializeComponents();
		InitializeState();
		InitializeUI();
		if (DemoMgr.Get().IsExpoDemo())
		{
			m_StoreButton.gameObject.SetActive(value: false);
			m_Drawer.gameObject.SetActive(value: false);
			m_QuestLogButton.gameObject.SetActive(value: false);
		}
		if (m_state != State.HUB_WITH_DRAWER)
		{
			m_journalButtonWidget.Hide();
		}
		TavernBrawlManager tavernBrawlManager = TavernBrawlManager.Get();
		tavernBrawlManager.OnTavernBrawlUpdated += TavernBrawl_UpdateUI;
		if (tavernBrawlManager.IsCurrentBrawlInfoReady)
		{
			DoTavernBrawlButtonInitialization();
		}
		else
		{
			tavernBrawlManager.OnTavernBrawlUpdated += DoTavernBrawlButtonInitialization;
		}
		FiresideGatheringManager.Get().OnSignShown += OnFSGSignShown;
		FiresideGatheringManager.Get().OnLeaveFSG += OnLeaveFSG;
	}

	private void OnDestroy()
	{
		Log.LoadingScreen.Print("Box.OnDestroy()");
		if (PegUI.Get() != null)
		{
			PegUI.Get().RemoveInputCamera(m_Camera.GetComponent<Camera>());
		}
		if (HearthstoneServices.TryGet<TavernBrawlManager>(out var service))
		{
			service.OnTavernBrawlUpdated -= DoTavernBrawlButtonInitialization;
			service.OnTavernBrawlUpdated -= TavernBrawl_UpdateUI;
		}
		if (HearthstoneServices.TryGet<FiresideGatheringManager>(out var service2))
		{
			service2.OnSignShown -= OnFSGSignShown;
			service2.OnLeaveFSG -= OnLeaveFSG;
		}
		if (HearthstoneServices.TryGet<NetCache>(out var service3))
		{
			service3.RemoveUpdatedListener(typeof(NetCache.NetCacheFeatures), DoTavernBrawlButtonInitialization);
			service3.RemoveUpdatedListener(typeof(NetCache.NetCacheHeroLevels), DoTavernBrawlButtonInitialization);
		}
		StoreManager.Get().RemoveStoreShownListener(OnStoreShown);
		if (LoadingScreen.Get() != null)
		{
			LoadingScreen.Get().UnregisterPreviousSceneDestroyedListener(OnTutorialSceneDestroyed);
		}
		ShutdownState();
		ShutdownNet();
		AssetHandle.SafeDispose(ref m_tableTopTexture);
		AssetHandle.SafeDispose(ref m_boxTopTexture);
		AssetHandle.SafeDispose(ref m_specialEventTexture);
		s_instance = null;
	}

	public static Box Get()
	{
		return s_instance;
	}

	public Camera GetCamera()
	{
		return m_Camera.GetComponent<Camera>();
	}

	public BoxCamera GetBoxCamera()
	{
		return m_Camera;
	}

	public Camera GetNoFxCamera()
	{
		return m_NoFxCamera;
	}

	public AudioListener GetAudioListener()
	{
		return m_AudioListener;
	}

	public State GetState()
	{
		return m_state;
	}

	public Texture2D GetTextureCompressionTestTexture()
	{
		return m_textureCompressionTest;
	}

	public bool ChangeState(State state)
	{
		if (state == State.INVALID)
		{
			return false;
		}
		if (m_state == state)
		{
			return false;
		}
		if (HasPendingEffects())
		{
			QueueStateChange(state);
		}
		else
		{
			ChangeStateNow(state);
		}
		return true;
	}

	public void UpdateState()
	{
		if (m_state == State.STARTUP)
		{
			UpdateState_Startup();
		}
		else if (m_state == State.PRESS_START)
		{
			UpdateState_PressStart();
		}
		else if (m_state == State.LOADING_HUB)
		{
			UpdateState_LoadingHub();
		}
		else if (m_state == State.LOADING)
		{
			UpdateState_Loading();
		}
		else if (m_state == State.HUB)
		{
			UpdateState_Hub();
		}
		else if (m_state == State.HUB_WITH_DRAWER)
		{
			UpdateState_HubWithDrawer();
		}
		else if (m_state == State.OPEN)
		{
			UpdateState_Open();
		}
		else if (m_state == State.CLOSED)
		{
			UpdateState_Closed();
		}
		else if (m_state == State.ERROR)
		{
			UpdateState_Error();
		}
		else if (m_state == State.SET_ROTATION_LOADING)
		{
			UpdateState_SetRotation();
		}
		else if (m_state == State.SET_ROTATION)
		{
			UpdateState_SetRotation();
		}
		else if (m_state == State.SET_ROTATION_OPEN)
		{
			UpdateState_SetRotationOpen();
		}
		else
		{
			Debug.LogError($"Box.UpdateState() - unhandled state {m_state}");
		}
	}

	public BoxLightMgr GetLightMgr()
	{
		return m_LightMgr;
	}

	public BoxLightStateType GetLightState()
	{
		return m_LightMgr.GetActiveState();
	}

	public void ChangeLightState(BoxLightStateType stateType)
	{
		m_LightMgr.ChangeState(stateType);
	}

	public void SetLightState(BoxLightStateType stateType)
	{
		m_LightMgr.SetState(stateType);
	}

	public BoxEventMgr GetEventMgr()
	{
		return m_EventMgr;
	}

	public Spell GetEventSpell(BoxEventType eventType)
	{
		return m_EventMgr.GetEventSpell(eventType);
	}

	public bool HasPendingEffects()
	{
		return m_pendingEffects > 0;
	}

	public bool IsBusy()
	{
		if (!HasPendingEffects())
		{
			return m_stateQueue.Count > 0;
		}
		return true;
	}

	public bool IsTransitioningToSceneMode()
	{
		return m_transitioningToSceneMode;
	}

	public bool IsTutorial()
	{
		return m_LightMgr.GetActiveState() == BoxLightStateType.TUTORIAL;
	}

	public void OnAnimStarted()
	{
		m_pendingEffects++;
	}

	public void OnAnimFinished()
	{
		m_pendingEffects--;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_OuterFrame.SetActive(value: false);
		}
		if (HasPendingEffects())
		{
			return;
		}
		if (m_stateQueue.Count == 0)
		{
			UpdateUIEvents();
			if (!m_transitioningToSceneMode)
			{
				return;
			}
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				bool flag = m_state == State.HUB_WITH_DRAWER;
				if (flag != m_showRibbonButtons)
				{
					ToggleRibbonUI(flag);
				}
			}
			FireTransitionFinishedEvent();
			m_transitioningToSceneMode = false;
		}
		else
		{
			ChangeStateQueued();
		}
	}

	public void OnLoggedIn()
	{
		InitializeNet(fromLogin: true);
		DoBoxSpecialEvents();
	}

	public void AddTransitionFinishedListener(TransitionFinishedCallback callback)
	{
		AddTransitionFinishedListener(callback, null);
	}

	public void AddTransitionFinishedListener(TransitionFinishedCallback callback, object userData)
	{
		TransitionFinishedListener transitionFinishedListener = new TransitionFinishedListener();
		transitionFinishedListener.SetCallback(callback);
		transitionFinishedListener.SetUserData(userData);
		if (!m_transitionFinishedListeners.Contains(transitionFinishedListener))
		{
			m_transitionFinishedListeners.Add(transitionFinishedListener);
		}
	}

	public bool RemoveTransitionFinishedListener(TransitionFinishedCallback callback)
	{
		return RemoveTransitionFinishedListener(callback, null);
	}

	public bool RemoveTransitionFinishedListener(TransitionFinishedCallback callback, object userData)
	{
		TransitionFinishedListener transitionFinishedListener = new TransitionFinishedListener();
		transitionFinishedListener.SetCallback(callback);
		transitionFinishedListener.SetUserData(userData);
		return m_transitionFinishedListeners.Remove(transitionFinishedListener);
	}

	public void AddButtonPressListener(ButtonPressCallback callback)
	{
		AddButtonPressListener(callback, null);
	}

	public void AddButtonPressListener(ButtonPressCallback callback, object userData)
	{
		ButtonPressListener buttonPressListener = new ButtonPressListener();
		buttonPressListener.SetCallback(callback);
		buttonPressListener.SetUserData(userData);
		if (!m_buttonPressListeners.Contains(buttonPressListener))
		{
			m_buttonPressListeners.Add(buttonPressListener);
		}
	}

	public bool RemoveButtonPressListener(ButtonPressCallback callback)
	{
		return RemoveButtonPressListener(callback, null);
	}

	public bool RemoveButtonPressListener(ButtonPressCallback callback, object userData)
	{
		ButtonPressListener buttonPressListener = new ButtonPressListener();
		buttonPressListener.SetCallback(callback);
		buttonPressListener.SetUserData(userData);
		return m_buttonPressListeners.Remove(buttonPressListener);
	}

	public void SetToIgnoreFullScreenEffects(bool ignoreEffects)
	{
		if (ignoreEffects)
		{
			SceneUtils.ReplaceLayer(m_LeftDoor.gameObject, GameLayer.IgnoreFullScreenEffects, m_originalLeftDoorLayer);
			SceneUtils.ReplaceLayer(m_RightDoor.gameObject, GameLayer.IgnoreFullScreenEffects, m_originalRightDoorLayer);
			SceneUtils.ReplaceLayer(m_Drawer.gameObject, GameLayer.IgnoreFullScreenEffects, m_originalDrawerLayer);
		}
		else
		{
			SceneUtils.ReplaceLayer(m_LeftDoor.gameObject, m_originalLeftDoorLayer, GameLayer.IgnoreFullScreenEffects);
			SceneUtils.ReplaceLayer(m_RightDoor.gameObject, m_originalRightDoorLayer, GameLayer.IgnoreFullScreenEffects);
			SceneUtils.ReplaceLayer(m_Drawer.gameObject, m_originalDrawerLayer, GameLayer.IgnoreFullScreenEffects);
		}
	}

	public void PlayTavernBrawlCrowdSFX()
	{
		if (m_tavernBrawlEnterCrowdSounds.Count >= 1)
		{
			int index = UnityEngine.Random.Range(0, m_tavernBrawlEnterCrowdSounds.Count);
			SoundManager.Get().LoadAndPlay(m_tavernBrawlEnterCrowdSounds[index]);
		}
	}

	private void InitializeStateConfigs()
	{
		int length = Enum.GetValues(typeof(State)).Length;
		m_stateConfigs = new BoxStateConfig[length];
		m_stateConfigs[1] = new BoxStateConfig
		{
			m_logoState = 
			{
				m_state = BoxLogo.State.HIDDEN
			},
			m_startButtonState = 
			{
				m_state = BoxStartButton.State.HIDDEN
			},
			m_doorState = 
			{
				m_state = BoxDoor.State.CLOSED
			},
			m_diskState = 
			{
				m_state = BoxDisk.State.LOADING
			},
			m_drawerState = 
			{
				m_state = BoxDrawer.State.CLOSED
			},
			m_camState = 
			{
				m_state = BoxCamera.State.CLOSED
			},
			m_fullScreenBlackState = 
			{
				m_state = true
			}
		};
		m_stateConfigs[2] = new BoxStateConfig
		{
			m_logoState = 
			{
				m_state = BoxLogo.State.SHOWN
			},
			m_startButtonState = 
			{
				m_state = BoxStartButton.State.SHOWN
			},
			m_doorState = 
			{
				m_state = BoxDoor.State.CLOSED
			},
			m_diskState = 
			{
				m_state = BoxDisk.State.LOADING
			},
			m_drawerState = 
			{
				m_state = BoxDrawer.State.CLOSED
			},
			m_camState = 
			{
				m_state = BoxCamera.State.CLOSED
			},
			m_fullScreenBlackState = 
			{
				m_ignore = true
			}
		};
		m_stateConfigs[4] = new BoxStateConfig
		{
			m_logoState = 
			{
				m_state = BoxLogo.State.HIDDEN
			},
			m_startButtonState = 
			{
				m_state = BoxStartButton.State.HIDDEN
			},
			m_doorState = 
			{
				m_state = BoxDoor.State.CLOSED
			},
			m_diskState = 
			{
				m_state = BoxDisk.State.LOADING
			},
			m_drawerState = 
			{
				m_state = BoxDrawer.State.CLOSED
			},
			m_camState = 
			{
				m_state = BoxCamera.State.CLOSED
			},
			m_fullScreenBlackState = 
			{
				m_ignore = true
			}
		};
		m_stateConfigs[3] = new BoxStateConfig
		{
			m_logoState = 
			{
				m_state = BoxLogo.State.HIDDEN
			},
			m_startButtonState = 
			{
				m_state = BoxStartButton.State.HIDDEN
			},
			m_doorState = 
			{
				m_state = BoxDoor.State.CLOSED
			},
			m_diskState = 
			{
				m_state = BoxDisk.State.LOADING
			},
			m_drawerState = 
			{
				m_ignore = true
			},
			m_camState = 
			{
				m_ignore = true
			},
			m_fullScreenBlackState = 
			{
				m_ignore = true
			}
		};
		m_stateConfigs[5] = new BoxStateConfig
		{
			m_logoState = 
			{
				m_state = BoxLogo.State.HIDDEN
			},
			m_startButtonState = 
			{
				m_state = BoxStartButton.State.HIDDEN
			},
			m_doorState = 
			{
				m_state = BoxDoor.State.CLOSED
			},
			m_diskState = 
			{
				m_state = BoxDisk.State.MAINMENU
			},
			m_drawerState = 
			{
				m_state = BoxDrawer.State.CLOSED
			},
			m_camState = 
			{
				m_state = BoxCamera.State.CLOSED
			},
			m_fullScreenBlackState = 
			{
				m_ignore = true
			}
		};
		m_stateConfigs[6] = new BoxStateConfig
		{
			m_logoState = 
			{
				m_state = BoxLogo.State.HIDDEN
			},
			m_startButtonState = 
			{
				m_state = BoxStartButton.State.HIDDEN
			},
			m_doorState = 
			{
				m_state = BoxDoor.State.CLOSED
			},
			m_diskState = 
			{
				m_state = BoxDisk.State.MAINMENU
			},
			m_drawerState = 
			{
				m_state = BoxDrawer.State.OPENED
			},
			m_camState = 
			{
				m_state = BoxCamera.State.CLOSED_WITH_DRAWER
			},
			m_fullScreenBlackState = 
			{
				m_ignore = true
			}
		};
		m_stateConfigs[7] = new BoxStateConfig
		{
			m_logoState = 
			{
				m_state = BoxLogo.State.HIDDEN
			},
			m_startButtonState = 
			{
				m_state = BoxStartButton.State.HIDDEN
			},
			m_doorState = 
			{
				m_state = BoxDoor.State.OPENED
			},
			m_diskState = 
			{
				m_state = BoxDisk.State.LOADING
			},
			m_drawerState = 
			{
				m_state = BoxDrawer.State.CLOSED_BOX_OPENED
			},
			m_camState = 
			{
				m_state = BoxCamera.State.OPENED
			},
			m_fullScreenBlackState = 
			{
				m_ignore = true
			}
		};
		m_stateConfigs[8] = new BoxStateConfig
		{
			m_logoState = 
			{
				m_state = BoxLogo.State.HIDDEN
			},
			m_startButtonState = 
			{
				m_state = BoxStartButton.State.HIDDEN
			},
			m_doorState = 
			{
				m_state = BoxDoor.State.CLOSED
			},
			m_diskState = 
			{
				m_state = BoxDisk.State.LOADING
			},
			m_drawerState = 
			{
				m_state = BoxDrawer.State.CLOSED
			},
			m_camState = 
			{
				m_state = BoxCamera.State.CLOSED
			},
			m_fullScreenBlackState = 
			{
				m_state = false
			}
		};
		m_stateConfigs[9] = new BoxStateConfig
		{
			m_logoState = 
			{
				m_state = BoxLogo.State.HIDDEN
			},
			m_startButtonState = 
			{
				m_state = BoxStartButton.State.HIDDEN
			},
			m_doorState = 
			{
				m_state = BoxDoor.State.CLOSED
			},
			m_diskState = 
			{
				m_state = BoxDisk.State.LOADING
			},
			m_drawerState = 
			{
				m_state = BoxDrawer.State.CLOSED
			},
			m_camState = 
			{
				m_state = BoxCamera.State.CLOSED
			},
			m_fullScreenBlackState = 
			{
				m_state = false
			}
		};
		m_stateConfigs[10] = new BoxStateConfig
		{
			m_logoState = 
			{
				m_state = BoxLogo.State.HIDDEN
			},
			m_startButtonState = 
			{
				m_state = BoxStartButton.State.HIDDEN
			},
			m_doorState = 
			{
				m_state = BoxDoor.State.CLOSED
			},
			m_diskState = 
			{
				m_state = BoxDisk.State.LOADING
			},
			m_drawerState = 
			{
				m_state = BoxDrawer.State.CLOSED
			},
			m_camState = 
			{
				m_state = BoxCamera.State.CLOSED
			},
			m_fullScreenBlackState = 
			{
				m_ignore = true
			}
		};
		m_stateConfigs[11] = new BoxStateConfig
		{
			m_logoState = 
			{
				m_state = BoxLogo.State.HIDDEN
			},
			m_startButtonState = 
			{
				m_state = BoxStartButton.State.HIDDEN
			},
			m_doorState = 
			{
				m_state = BoxDoor.State.CLOSED
			},
			m_diskState = 
			{
				m_state = BoxDisk.State.MAINMENU
			},
			m_drawerState = 
			{
				m_state = BoxDrawer.State.CLOSED
			},
			m_camState = 
			{
				m_state = BoxCamera.State.CLOSED
			},
			m_fullScreenBlackState = 
			{
				m_ignore = true
			}
		};
		m_stateConfigs[12] = new BoxStateConfig
		{
			m_logoState = 
			{
				m_state = BoxLogo.State.HIDDEN
			},
			m_startButtonState = 
			{
				m_state = BoxStartButton.State.HIDDEN
			},
			m_doorState = 
			{
				m_state = BoxDoor.State.OPENED
			},
			m_diskState = 
			{
				m_state = BoxDisk.State.LOADING
			},
			m_drawerState = 
			{
				m_state = BoxDrawer.State.CLOSED
			},
			m_camState = 
			{
				m_state = BoxCamera.State.OPENED
			},
			m_fullScreenBlackState = 
			{
				m_ignore = false
			}
		};
	}

	private void InitializeState()
	{
		m_state = State.STARTUP;
		bool flag = GameMgr.Get().WasTutorial() && !GameMgr.Get().WasSpectator();
		if (HearthstoneServices.TryGet<SceneMgr>(out var service))
		{
			if (flag)
			{
				m_state = State.LOADING;
			}
			else
			{
				service.RegisterScenePreUnloadEvent(OnScenePreUnload);
				service.RegisterSceneLoadedEvent(OnSceneLoaded);
				m_state = TranslateSceneModeToBoxState(service.GetMode());
			}
		}
		UpdateState();
		m_TopSpinner.Spin();
		m_BottomSpinner.Spin();
		if (flag)
		{
			LoadingScreen.Get().RegisterPreviousSceneDestroyedListener(OnTutorialSceneDestroyed);
		}
		if (m_state == State.HUB_WITH_DRAWER)
		{
			ToggleRibbonUI(show: true);
			m_journalButtonWidget.Show();
			m_journalButtonWidget.TriggerEvent("ENABLE_INTERACTION");
		}
	}

	private void OnTutorialSceneDestroyed(object userData)
	{
		LoadingScreen.Get().UnregisterPreviousSceneDestroyedListener(OnTutorialSceneDestroyed);
		Spell eventSpell = GetEventSpell(BoxEventType.TUTORIAL_PLAY);
		eventSpell.AddStateFinishedCallback(OnTutorialPlaySpellStateFinished);
		eventSpell.ActivateState(SpellStateType.DEATH);
	}

	private void OnTutorialPlaySpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			SceneMgr sceneMgr = SceneMgr.Get();
			sceneMgr.RegisterScenePreUnloadEvent(OnScenePreUnload);
			sceneMgr.RegisterSceneLoadedEvent(OnSceneLoaded);
			ChangeStateToReflectSceneMode(SceneMgr.Get().GetMode(), isSceneActuallyLoaded: false);
		}
	}

	private void ShutdownState()
	{
		if (m_StoreButton != null)
		{
			m_StoreButton.Unload();
		}
		UnloadQuestLog();
		if (HearthstoneServices.TryGet<SceneMgr>(out var service))
		{
			service.UnregisterSceneLoadedEvent(OnSceneLoaded);
			service.UnregisterScenePreUnloadEvent(OnScenePreUnload);
		}
	}

	private void QueueStateChange(State state)
	{
		m_stateQueue.Enqueue(state);
	}

	private void ChangeStateQueued()
	{
		State state = m_stateQueue.Dequeue();
		ChangeStateNow(state);
	}

	private void ChangeStateNow(State state)
	{
		bool flag = SetRotationManager.ShouldShowSetRotationIntro();
		if (!flag)
		{
			if (m_DiskCenter != null)
			{
				m_DiskCenter.SetActive(value: true);
			}
			if (m_setRotationDisk != null)
			{
				m_setRotationDisk.SetActive(value: false);
			}
		}
		if (state == State.OPEN && flag)
		{
			state = State.SET_ROTATION_OPEN;
		}
		m_state = state;
		TrackBoxInteractable();
		switch (state)
		{
		case State.STARTUP:
			ChangeState_Startup();
			break;
		case State.PRESS_START:
			ChangeState_PressStart();
			break;
		case State.LOADING_HUB:
			ChangeState_LoadingHub();
			break;
		case State.LOADING:
			ChangeState_Loading();
			break;
		case State.HUB:
			ChangeState_Hub();
			break;
		case State.HUB_WITH_DRAWER:
			ChangeState_HubWithDrawer();
			break;
		case State.OPEN:
			ChangeState_Open();
			break;
		case State.CLOSED:
			ChangeState_Closed();
			break;
		case State.ERROR:
			ChangeState_Error();
			break;
		case State.SET_ROTATION_LOADING:
			ChangeState_SetRotationLoading();
			break;
		case State.SET_ROTATION:
			ChangeState_SetRotation();
			break;
		case State.SET_ROTATION_OPEN:
			ChangeState_SetRotationOpen();
			break;
		default:
			Debug.LogError($"Box.ChangeStateNow() - unhandled state {state}");
			break;
		}
		UpdateUIEvents();
	}

	private void ChangeStateToReflectSceneMode(SceneMgr.Mode mode, bool isSceneActuallyLoaded)
	{
		State state = TranslateSceneModeToBoxState(mode);
		bool flag = SetRotationManager.ShouldShowSetRotationIntro();
		if (mode == SceneMgr.Mode.HUB && flag)
		{
			ChangeState(State.SET_ROTATION_LOADING);
			if (isSceneActuallyLoaded)
			{
				StartCoroutine(SetRotation_StartSetRotationIntro());
			}
		}
		else if (mode == SceneMgr.Mode.TOURNAMENT && flag)
		{
			ChangeState(State.SET_ROTATION_OPEN);
			UserAttentionManager.StartBlocking(UserAttentionBlocker.SET_ROTATION_INTRO);
			m_transitioningToSceneMode = true;
		}
		else if (!SceneMgr.Get().IsDoingSceneDrivenTransition() && ChangeState(state))
		{
			m_transitioningToSceneMode = true;
		}
		BoxLightStateType stateType = TranslateSceneModeToLightState(mode);
		m_LightMgr.ChangeState(stateType);
	}

	public void TryToStartSetRotationFromHub()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		bool flag = SetRotationManager.ShouldShowSetRotationIntro();
		if (mode == SceneMgr.Mode.HUB && flag)
		{
			ChangeState(State.SET_ROTATION_LOADING);
			StartCoroutine(SetRotation_StartSetRotationIntro());
		}
	}

	private State TranslateSceneModeToBoxState(SceneMgr.Mode mode)
	{
		return mode switch
		{
			SceneMgr.Mode.STARTUP => State.STARTUP, 
			SceneMgr.Mode.LOGIN => State.INVALID, 
			SceneMgr.Mode.HUB => State.HUB_WITH_DRAWER, 
			SceneMgr.Mode.TOURNAMENT => State.OPEN, 
			SceneMgr.Mode.ADVENTURE => State.OPEN, 
			SceneMgr.Mode.FRIENDLY => State.OPEN, 
			SceneMgr.Mode.DRAFT => State.OPEN, 
			SceneMgr.Mode.COLLECTIONMANAGER => State.OPEN, 
			SceneMgr.Mode.TAVERN_BRAWL => State.OPEN, 
			SceneMgr.Mode.PACKOPENING => State.OPEN, 
			SceneMgr.Mode.FIRESIDE_GATHERING => State.OPEN, 
			SceneMgr.Mode.BACON => State.OPEN, 
			SceneMgr.Mode.GAME_MODE => State.OPEN, 
			SceneMgr.Mode.GAMEPLAY => State.INVALID, 
			SceneMgr.Mode.FATAL_ERROR => State.ERROR, 
			_ => State.OPEN, 
		};
	}

	private BoxLightStateType TranslateSceneModeToLightState(SceneMgr.Mode mode)
	{
		return mode switch
		{
			SceneMgr.Mode.LOGIN => BoxLightStateType.INVALID, 
			SceneMgr.Mode.TOURNAMENT => BoxLightStateType.TOURNAMENT, 
			SceneMgr.Mode.COLLECTIONMANAGER => BoxLightStateType.COLLECTION, 
			SceneMgr.Mode.TAVERN_BRAWL => BoxLightStateType.COLLECTION, 
			SceneMgr.Mode.FIRESIDE_GATHERING => BoxLightStateType.COLLECTION, 
			SceneMgr.Mode.PACKOPENING => BoxLightStateType.PACK_OPENING, 
			SceneMgr.Mode.GAMEPLAY => BoxLightStateType.INVALID, 
			SceneMgr.Mode.DRAFT => BoxLightStateType.FORGE, 
			SceneMgr.Mode.ADVENTURE => BoxLightStateType.ADVENTURE, 
			SceneMgr.Mode.FRIENDLY => BoxLightStateType.ADVENTURE, 
			SceneMgr.Mode.BACON => BoxLightStateType.ADVENTURE, 
			SceneMgr.Mode.GAME_MODE => BoxLightStateType.ADVENTURE, 
			SceneMgr.Mode.PVP_DUNGEON_RUN => BoxLightStateType.ADVENTURE, 
			_ => BoxLightStateType.DEFAULT, 
		};
	}

	private void OnScenePreUnload(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode != SceneMgr.Mode.GAMEPLAY && mode != SceneMgr.Mode.STARTUP && mode != SceneMgr.Mode.RESET)
		{
			if (prevMode == SceneMgr.Mode.HUB)
			{
				ChangeState(State.LOADING);
				m_StoreButton.Unload();
				UnloadQuestLog();
			}
			else if (mode == SceneMgr.Mode.HUB)
			{
				ChangeStateToReflectSceneMode(mode, isSceneActuallyLoaded: false);
				m_waitingForSceneLoad = true;
			}
			else if (ShouldUseLoadingHubState(mode, prevMode))
			{
				ChangeState(State.LOADING_HUB);
			}
			else if (!SceneMgr.Get().IsDoingSceneDrivenTransition())
			{
				ChangeState(State.LOADING);
			}
			ClearQueuedButtonFireEvent();
			UpdateUIEvents();
		}
	}

	private bool ShouldUseLoadingHubState(SceneMgr.Mode mode, SceneMgr.Mode prevMode)
	{
		if (mode == SceneMgr.Mode.FRIENDLY && prevMode != SceneMgr.Mode.HUB)
		{
			return true;
		}
		if (mode == SceneMgr.Mode.FIRESIDE_GATHERING && prevMode != SceneMgr.Mode.HUB)
		{
			return true;
		}
		if (prevMode == SceneMgr.Mode.COLLECTIONMANAGER && (mode == SceneMgr.Mode.ADVENTURE || mode == SceneMgr.Mode.TOURNAMENT))
		{
			return true;
		}
		if (mode == SceneMgr.Mode.COLLECTIONMANAGER && (prevMode == SceneMgr.Mode.ADVENTURE || prevMode == SceneMgr.Mode.TOURNAMENT || prevMode == SceneMgr.Mode.FIRESIDE_GATHERING))
		{
			return true;
		}
		return false;
	}

	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (!TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL) && SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.STARTUP)
		{
			PlayTavernBrawlButtonActivation(activate: false, isInitialization: true);
		}
		ChangeStateToReflectSceneMode(mode, isSceneActuallyLoaded: true);
		if (m_waitingForSceneLoad)
		{
			m_waitingForSceneLoad = false;
			if (m_queuedButtonFire.HasValue)
			{
				FireButtonPressEvent(m_queuedButtonFire.Value);
				m_queuedButtonFire = null;
			}
		}
	}

	private void ChangeState_Startup()
	{
		m_state = State.STARTUP;
		ChangeStateUsingConfig();
	}

	private void ChangeState_PressStart()
	{
		m_state = State.PRESS_START;
		ChangeStateUsingConfig();
	}

	private void ChangeState_SetRotationLoading()
	{
		m_state = State.SET_ROTATION_LOADING;
		ChangeStateUsingConfig();
	}

	private void ChangeState_SetRotation()
	{
		m_state = State.SET_ROTATION;
		ChangeStateUsingConfig();
	}

	private void ChangeState_SetRotationOpen()
	{
		m_state = State.SET_ROTATION_OPEN;
		StartCoroutine(SetRotationOpen_ChangeState());
	}

	private void ChangeState_LoadingHub()
	{
		m_state = State.LOADING_HUB;
		ChangeStateUsingConfig();
	}

	private void ChangeState_Loading()
	{
		m_state = State.LOADING;
		ChangeStateUsingConfig();
	}

	private void ChangeState_Hub()
	{
		m_state = State.HUB;
		UpdateUI();
		ChangeStateUsingConfig();
	}

	private void ChangeState_HubWithDrawer()
	{
		m_state = State.HUB_WITH_DRAWER;
		m_Camera.EnableAccelerometer();
		ChangeStateUsingConfig();
	}

	private void ChangeState_Open()
	{
		m_state = State.OPEN;
		ChangeStateUsingConfig();
	}

	private void ChangeState_Closed()
	{
		m_state = State.CLOSED;
		ChangeStateUsingConfig();
	}

	private void ChangeState_Error()
	{
		m_state = State.ERROR;
		ChangeStateUsingConfig();
	}

	private void UpdateState_Startup()
	{
		m_state = State.STARTUP;
		UpdateStateUsingConfig();
	}

	private void UpdateState_PressStart()
	{
		m_state = State.PRESS_START;
		UpdateStateUsingConfig();
	}

	private void UpdateState_SetRotationLoading()
	{
		m_state = State.SET_ROTATION_LOADING;
		UpdateStateUsingConfig();
	}

	private void UpdateState_SetRotation()
	{
		m_state = State.SET_ROTATION;
		UpdateStateUsingConfig();
	}

	private void UpdateState_SetRotationOpen()
	{
		m_state = State.SET_ROTATION_OPEN;
		UpdateStateUsingConfig();
	}

	private void UpdateState_LoadingHub()
	{
		m_state = State.LOADING_HUB;
		UpdateStateUsingConfig();
	}

	private void UpdateState_Loading()
	{
		m_state = State.LOADING;
		UpdateStateUsingConfig();
	}

	private void UpdateState_Hub()
	{
		m_state = State.HUB;
		UpdateUI();
		UpdateStateUsingConfig();
	}

	private void UpdateState_HubWithDrawer()
	{
		m_state = State.HUB_WITH_DRAWER;
		m_Camera.EnableAccelerometer();
		UpdateStateUsingConfig();
	}

	private void UpdateState_Open()
	{
		m_state = State.OPEN;
		UpdateStateUsingConfig();
	}

	private void UpdateState_Closed()
	{
		m_state = State.CLOSED;
		UpdateStateUsingConfig();
	}

	private void UpdateState_Error()
	{
		m_state = State.ERROR;
		UpdateStateUsingConfig();
	}

	private void ChangeStateUsingConfig()
	{
		BoxStateConfig boxStateConfig = m_stateConfigs[(int)m_state];
		if (!boxStateConfig.m_logoState.m_ignore)
		{
			m_Logo.ChangeState(boxStateConfig.m_logoState.m_state);
		}
		if (!boxStateConfig.m_startButtonState.m_ignore)
		{
			m_StartButton.ChangeState(boxStateConfig.m_startButtonState.m_state);
		}
		if (!boxStateConfig.m_doorState.m_ignore)
		{
			m_LeftDoor.ChangeState(boxStateConfig.m_doorState.m_state);
			m_RightDoor.ChangeState(boxStateConfig.m_doorState.m_state);
		}
		if (!boxStateConfig.m_diskState.m_ignore)
		{
			m_Disk.ChangeState(boxStateConfig.m_diskState.m_state);
			CleanUpButtonsOnDiskStateChange();
		}
		if (!boxStateConfig.m_drawerState.m_ignore)
		{
			if (!UniversalInputManager.UsePhoneUI)
			{
				m_Drawer.ChangeState(boxStateConfig.m_drawerState.m_state);
			}
			else
			{
				bool flag = m_state == State.HUB_WITH_DRAWER;
				if (!flag && flag != m_showRibbonButtons)
				{
					ToggleRibbonUI(flag);
				}
			}
		}
		if (!boxStateConfig.m_camState.m_ignore)
		{
			m_Camera.ChangeState(boxStateConfig.m_camState.m_state);
		}
		if (!boxStateConfig.m_fullScreenBlackState.m_ignore)
		{
			FullScreenBlack_ChangeState(boxStateConfig.m_fullScreenBlackState.m_state);
		}
		DoBoxSpecialEvents();
	}

	private void ToggleRibbonUI(bool show)
	{
		if (!(m_ribbonButtons == null))
		{
			m_ribbonButtons.Toggle(show);
			m_showRibbonButtons = show;
		}
	}

	private void UpdateStateUsingConfig()
	{
		BoxStateConfig boxStateConfig = m_stateConfigs[(int)m_state];
		if (!boxStateConfig.m_logoState.m_ignore)
		{
			m_Logo.UpdateState(boxStateConfig.m_logoState.m_state);
		}
		if (!boxStateConfig.m_startButtonState.m_ignore)
		{
			m_StartButton.UpdateState(boxStateConfig.m_startButtonState.m_state);
		}
		if (!boxStateConfig.m_doorState.m_ignore)
		{
			m_LeftDoor.ChangeState(boxStateConfig.m_doorState.m_state);
			m_RightDoor.ChangeState(boxStateConfig.m_doorState.m_state);
		}
		if (!boxStateConfig.m_diskState.m_ignore)
		{
			m_Disk.UpdateState(boxStateConfig.m_diskState.m_state);
		}
		m_TopSpinner.Reset();
		m_BottomSpinner.Reset();
		if (!boxStateConfig.m_drawerState.m_ignore)
		{
			m_Drawer.UpdateState(boxStateConfig.m_drawerState.m_state);
		}
		if (!boxStateConfig.m_camState.m_ignore)
		{
			m_Camera.UpdateState(boxStateConfig.m_camState.m_state);
		}
		if (!boxStateConfig.m_fullScreenBlackState.m_ignore)
		{
			FullScreenBlack_UpdateState(boxStateConfig.m_fullScreenBlackState.m_state);
		}
	}

	private void FullScreenBlack_ChangeState(bool enable)
	{
		FullScreenBlack_UpdateState(enable);
	}

	private void FullScreenBlack_UpdateState(bool enable)
	{
		FullScreenEffects activeCameraFullScreenEffects = FullScreenFXMgr.Get().ActiveCameraFullScreenEffects;
		if (!(activeCameraFullScreenEffects == null))
		{
			activeCameraFullScreenEffects.BlendToColorEnable = enable;
			if (enable)
			{
				activeCameraFullScreenEffects.BlendColor = Color.black;
				activeCameraFullScreenEffects.BlendToColorAmount = 1f;
			}
		}
	}

	private void FireTransitionFinishedEvent()
	{
		TransitionFinishedListener[] array = m_transitionFinishedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	private void InitializeUI()
	{
		PegUI.Get().AddInputCamera(m_Camera.GetComponent<Camera>());
		m_boxWidgetRef.RegisterReadyListener<Widget>(BoxWidgetIsReady);
		m_mainShopWidgetRef.RegisterReadyListener<Widget>(MainShopWidgetIsReady);
		m_StartButton.AddEventListener(UIEventType.RELEASE, OnStartButtonPressed);
		switch (InputUtil.GetInputScheme())
		{
		case InputScheme.TOUCH:
			m_StartButton.SetText(GameStrings.Get("GLUE_START_TOUCH"));
			break;
		case InputScheme.GAMEPAD:
			m_StartButton.SetText(GameStrings.Get("GLUE_START_PRESS"));
			break;
		case InputScheme.KEYBOARD_MOUSE:
			m_StartButton.SetText(GameStrings.Get("GLUE_START_CLICK"));
			break;
		}
		m_TournamentButton.SetText(GameStrings.Get("GLUE_TOURNAMENT"));
		m_SoloAdventuresButton.SetText(GameStrings.Get("GLUE_ADVENTURE"));
		m_TavernBrawlButton.SetText(GameStrings.Get("GLOBAL_TAVERN_BRAWL"));
		m_GameModesButton.SetText(GameStrings.Get("GLUE_GAME_MODES"));
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_Drawer.gameObject.SetActive(value: false);
			m_ribbonButtons.m_collectionManagerRibbon.AddEventListener(UIEventType.RELEASE, OnCollectionButtonPressed);
			m_ribbonButtons.m_packOpeningRibbon.AddEventListener(UIEventType.RELEASE, OnOpenPacksButtonPressed);
			m_ribbonButtons.m_questLogRibbon.AddEventListener(UIEventType.RELEASE, OnQuestButtonPressed);
			m_ribbonButtons.m_storeRibbon.AddEventListener(UIEventType.RELEASE, OnStoreButtonReleased);
		}
		else
		{
			m_OpenPacksButton.SetText(GameStrings.Get("GLUE_OPEN_PACKS"));
			m_CollectionButton.SetText(GameStrings.Get("GLUE_MY_COLLECTION"));
			m_QuestLogButton.AddEventListener(UIEventType.RELEASE, OnQuestButtonPressed);
			m_StoreButton.AddEventListener(UIEventType.RELEASE, OnStoreButtonReleased);
		}
		RegisterButtonEvents(m_TournamentButton);
		RegisterButtonEvents(m_SoloAdventuresButton);
		RegisterButtonEvents(m_GameModesButton);
		RegisterButtonEvents(m_TavernBrawlButton);
		RegisterButtonEvents(m_OpenPacksButton);
		RegisterButtonEvents(m_CollectionButton);
		UpdateUI(isInitialization: true);
	}

	public void UpdateUI(bool isInitialization = false)
	{
		UpdateUIState(isInitialization);
		UpdateUIEvents();
	}

	private void TavernBrawl_UpdateUI()
	{
		UpdateUI();
	}

	private void UpdateUIState(bool isInitialization)
	{
		if (m_waitingForNetData)
		{
			SetPackCount(-1);
			HighlightButton(m_OpenPacksButton, highlightOn: false);
			HighlightButton(m_TournamentButton, highlightOn: false);
			HighlightButton(m_SoloAdventuresButton, highlightOn: false);
			HighlightButton(m_CollectionButton, highlightOn: false);
			HighlightButton(m_GameModesButton, highlightOn: false);
			HighlightButton(m_TavernBrawlButton, highlightOn: false);
			return;
		}
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		if (DemoMgr.Get().GetMode() == DemoMode.BLIZZCON_2013)
		{
			netObject.Games.Practice = false;
			netObject.Games.Tournament = false;
		}
		int num = ComputeBoosterCount();
		SetPackCount(num);
		bool highlightOn = num > 0 && !Options.Get().GetBool(Option.HAS_SEEN_PACK_OPENING, defaultVal: false);
		HighlightButton(m_OpenPacksButton, highlightOn);
		bool flag = false;
		bool highlightOn2 = false;
		if (netObject.Games.Practice && Options.Get().GetBool(Option.BUNDLE_JUST_PURCHASE_IN_HUB, defaultVal: false))
		{
			flag = true;
		}
		else if (netObject.Games.Practice && !Options.Get().GetBool(Option.HAS_SEEN_PRACTICE_MODE, defaultVal: false))
		{
			flag = true;
		}
		else if (netObject.Games.Tournament && AchieveManager.Get() != null && AchieveManager.Get().HasActiveQuestId(AchievementDbId.FIRST_BLOOD))
		{
			highlightOn2 = true;
		}
		else if (!flag && netObject.Games.Practice && m_boxVisualController != null)
		{
			string state = ((AdventureConfig.GetAdventurePlayerShouldSee() != 0) ? "NewAdventureOn" : "NewAdventureOff");
			m_boxVisualController.SetState(state);
		}
		if (!SpecialEventManager.Get().IsEventActive("battlegrounds_early_access", activeIfDoesNotExist: false) && m_boxVisualController != null)
		{
			string state2 = ((!Options.Get().GetBool(Option.HAS_SEEN_BATTLEGROUNDS_BOX_BUTTON, defaultVal: false)) ? "NewGameModeOn" : "NewGameModeOff");
			m_boxVisualController.SetState(state2);
		}
		HighlightButton(m_TournamentButton, highlightOn2);
		ToggleButtonTextureState(netObject.Games.Tournament, m_TournamentButton);
		HighlightButton(m_SoloAdventuresButton, flag);
		ToggleButtonTextureState(netObject.Games.Practice, m_SoloAdventuresButton);
		bool highlightOn3 = !flag && netObject.Collection.Manager && !Options.Get().GetBool(Option.HAS_SEEN_COLLECTIONMANAGER_AFTER_PRACTICE, defaultVal: false);
		HighlightButton(m_CollectionButton, highlightOn3);
		ToggleDrawerButtonState(netObject.Collection.Manager, m_CollectionButton);
		UpdateTavernBrawlButtonState(highlightAllowed: true);
	}

	private void BoxWidgetIsReady(Widget widget)
	{
		m_boxVisualController = widget.FindWidgetComponent<VisualController>(Array.Empty<string>());
	}

	private void MainShopWidgetIsReady(Widget widget)
	{
	}

	private bool IsCollectionReady()
	{
		if (CollectionManager.Get() != null)
		{
			return CollectionManager.Get().IsFullyLoaded();
		}
		return false;
	}

	private IEnumerator UpdateUIWhenCollectionReady()
	{
		while (!IsCollectionReady())
		{
			yield return null;
		}
		UpdateUI();
	}

	private void OnFSGSignShown()
	{
		m_showFSGBanner = true;
		UpdateTavernBrawlButtonState(highlightAllowed: true);
	}

	private void OnLeaveFSG(FSGConfig fsg)
	{
		UpdateTavernBrawlButtonState(highlightAllowed: true);
	}

	private void DoTavernBrawlButtonInitialization()
	{
		TavernBrawlManager tavernBrawlManager = TavernBrawlManager.Get();
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		if (netObject == null)
		{
			NetCache.Get().RegisterUpdatedListener(typeof(NetCache.NetCacheFeatures), DoTavernBrawlButtonInitialization);
			return;
		}
		NetCache.Get().RemoveUpdatedListener(typeof(NetCache.NetCacheFeatures), DoTavernBrawlButtonInitialization);
		if (NetCache.Get().GetNetObject<NetCache.NetCacheHeroLevels>() == null)
		{
			NetCache.Get().RegisterUpdatedListener(typeof(NetCache.NetCacheHeroLevels), DoTavernBrawlButtonInitialization);
			return;
		}
		NetCache.Get().RemoveUpdatedListener(typeof(NetCache.NetCacheHeroLevels), DoTavernBrawlButtonInitialization);
		if (netObject != null && netObject.Games.TavernBrawl && tavernBrawlManager.HasUnlockedTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL))
		{
			if (tavernBrawlManager.IsCurrentBrawlInfoReady)
			{
				tavernBrawlManager.OnTavernBrawlUpdated -= DoTavernBrawlButtonInitialization;
			}
			if (!TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL) || tavernBrawlManager.IsFirstTimeSeeingCurrentSeason)
			{
				PlayTavernBrawlButtonActivation(activate: false, isInitialization: true);
			}
		}
	}

	public void PlayTavernBrawlButtonActivation(bool activate, bool isInitialization = false)
	{
		Animator component = m_TavernBrawlButtonVisual.GetComponent<Animator>();
		component.StopPlayback();
		if (activate)
		{
			component.Play("TavernBrawl_ButtonActivate");
			if (!isInitialization)
			{
				m_TavernBrawlButtonActivateFX.GetComponent<ParticleSystem>().Play();
			}
		}
		else
		{
			if (!isInitialization)
			{
				m_TavernBrawlButton.ClearHighlightAndTooltip();
			}
			component.Play("TavernBrawl_ButtonDeactivate");
			if (!isInitialization)
			{
				m_TavernBrawlButtonDeactivateFX.GetComponent<ParticleSystem>().Play();
			}
		}
		IsTavernBrawlButtonDeactivated = !activate;
	}

	private void CleanUpButtonsOnDiskStateChange()
	{
		if (!IsTavernBrawlButtonDeactivated)
		{
			Animator component = m_TavernBrawlButtonVisual.GetComponent<Animator>();
			component.StopPlayback();
			component.Play("TavernBrawl_ButtonIdle");
		}
	}

	public bool UpdateTavernBrawlButtonState(bool highlightAllowed)
	{
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		TavernBrawlManager tavernBrawlManager = TavernBrawlManager.Get();
		bool flag = tavernBrawlManager.IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
		bool flag2 = (netObject?.Games.TavernBrawl ?? false) && flag && tavernBrawlManager.HasUnlockedTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
		bool highlightOn = highlightAllowed && flag2 && tavernBrawlManager.IsFirstTimeSeeingCurrentSeason && !IsTavernBrawlButtonDeactivated && !IsButtonHighlighted(m_TournamentButton) && !IsButtonHighlighted(m_SoloAdventuresButton) && !IsButtonHighlighted(m_GameModesButton);
		HighlightButton(m_TavernBrawlButton, highlightOn);
		ToggleButtonTextureState(flag2, m_TavernBrawlButton);
		if (!flag2)
		{
			m_TavernBrawlButton.ClearHighlightAndTooltip();
		}
		return flag2;
	}

	public void ActivateFiresideBrawlButton(bool isCheckedIn)
	{
		if (!isCheckedIn || m_showFSGBanner)
		{
			if (m_firesideGatheringTavernBrawlButtonFlag != null)
			{
				m_firesideGatheringTavernBrawlButtonFlag.SetActive(isCheckedIn);
			}
			m_showFSGBanner = false;
		}
	}

	private void UpdateUIEvents()
	{
		bool num = SetRotationManager.ShouldShowSetRotationIntro() || IsTutorial() || DemoMgr.Get().IsDemo() || m_state == State.LOADING_HUB;
		if (CanEnableUIEvents() && m_state == State.PRESS_START)
		{
			EnableButton(m_StartButton);
		}
		else
		{
			DisableButton(m_StartButton);
		}
		NetCache.NetCacheFeatures netCacheFeatures = null;
		if (!m_waitingForNetData)
		{
			netCacheFeatures = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		}
		if (!num)
		{
			m_StoreButton.gameObject.SetActive(value: true);
			if (netCacheFeatures != null)
			{
				if (netCacheFeatures.ProgressionEnabled)
				{
					if (!netCacheFeatures.JournalButtonDisabled)
					{
						m_journalButtonWidget.Show();
					}
				}
				else
				{
					m_QuestLogButton.gameObject.SetActive(value: true);
				}
			}
		}
		if (CanEnableUIEvents() && (m_state == State.HUB || m_state == State.HUB_WITH_DRAWER))
		{
			if (m_waitingForNetData || !IsCollectionReady())
			{
				ToggleButtonTextureState(enabled: false, m_TournamentButton);
				DisableButton(m_TournamentButton);
			}
			else
			{
				ToggleButtonTextureState(enabled: true, m_TournamentButton);
				EnableButton(m_TournamentButton);
			}
			if (m_waitingForNetData)
			{
				DisableButton(m_SoloAdventuresButton);
				DisableButton(m_GameModesButton);
				DisableButton(m_TavernBrawlButton);
				DisableButton(m_QuestLogButton);
				DisableButton(m_StoreButton);
				m_journalButtonWidget.TriggerEvent("DISABLE_INTERACTION");
			}
			else
			{
				EnableButton(m_SoloAdventuresButton);
				EnableButton(m_GameModesButton);
				EnableButton(m_TavernBrawlButton);
				EnableButton(m_StoreButton);
				EnableButton(m_QuestLogButton);
				m_journalButtonWidget.TriggerEvent("ENABLE_INTERACTION");
			}
			if (m_state == State.HUB_WITH_DRAWER)
			{
				if (m_waitingForNetData)
				{
					DisableButton(m_OpenPacksButton);
					DisableButton(m_CollectionButton);
				}
				else
				{
					EnableButton(m_OpenPacksButton);
					EnableButton(m_CollectionButton);
				}
			}
			else
			{
				DisableButton(m_OpenPacksButton);
				DisableButton(m_CollectionButton);
			}
		}
		else
		{
			DisableButton(m_TournamentButton);
			DisableButton(m_SoloAdventuresButton);
			DisableButton(m_GameModesButton);
			DisableButton(m_TavernBrawlButton);
			DisableButton(m_OpenPacksButton);
			DisableButton(m_CollectionButton);
			DisableButton(m_QuestLogButton);
			DisableButton(m_StoreButton);
			m_journalButtonWidget.TriggerEvent("DISABLE_INTERACTION");
		}
		if (DemoMgr.Get().GetMode() == DemoMode.BLIZZCON_2019_BATTLEGROUNDS)
		{
			DisableButton(m_TournamentButton);
			DisableButton(m_SoloAdventuresButton);
			DisableButton(m_OpenPacksButton);
			DisableButton(m_CollectionButton);
			DisableButton(m_QuestLogButton);
			DisableButton(m_StoreButton);
			DisableButton(m_TavernBrawlButton);
			m_journalButtonWidget.TriggerEvent("DISABLE_INTERACTION");
		}
		if (num)
		{
			m_StoreButton.gameObject.SetActive(value: false);
			m_QuestLogButton.gameObject.SetActive(value: false);
			m_journalButtonWidget.Hide();
		}
		if (netCacheFeatures != null && netCacheFeatures.JournalButtonDisabled)
		{
			m_journalButtonWidget.Hide();
			m_journalButtonWidget.TriggerEvent("DISABLE_INTERACTION");
		}
	}

	public void DisableAllButtons()
	{
		DisableButton(m_TournamentButton);
		DisableButton(m_SoloAdventuresButton);
		DisableButton(m_GameModesButton);
		DisableButton(m_TavernBrawlButton);
		DisableButton(m_setRotationButton);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			DisableButton(m_ribbonButtons.m_collectionManagerRibbon);
			DisableButton(m_ribbonButtons.m_packOpeningRibbon);
			DisableButton(m_ribbonButtons.m_questLogRibbon);
			DisableButton(m_ribbonButtons.m_storeRibbon);
		}
		else
		{
			DisableButton(m_OpenPacksButton);
			DisableButton(m_CollectionButton);
			DisableButton(m_QuestLogButton);
			DisableButton(m_StoreButton);
			m_journalButtonWidget.TriggerEvent("DISABLE_INTERACTION");
		}
		ToggleButtonTextureState(enabled: false, m_TournamentButton);
		ToggleButtonTextureState(enabled: false, m_SoloAdventuresButton);
		ToggleButtonTextureState(enabled: false, m_GameModesButton);
		ToggleButtonTextureState(enabled: false, m_TavernBrawlButton);
		ToggleDrawerButtonState(enabled: false, m_OpenPacksButton);
		ToggleDrawerButtonState(enabled: false, m_CollectionButton);
		ToggleButtonTextureState(enabled: false, m_setRotationButton);
	}

	private bool CanEnableUIEvents()
	{
		if (HasPendingEffects())
		{
			return false;
		}
		if (m_stateQueue.Count > 0)
		{
			return false;
		}
		if (m_state == State.INVALID)
		{
			return false;
		}
		if (m_state == State.STARTUP)
		{
			return false;
		}
		if (m_state == State.LOADING)
		{
			return false;
		}
		if (m_state == State.LOADING_HUB)
		{
			return false;
		}
		if (m_state == State.OPEN)
		{
			return false;
		}
		return true;
	}

	private void RegisterButtonEvents(PegUIElement button)
	{
		button.AddEventListener(UIEventType.RELEASE, OnButtonPressed);
		button.AddEventListener(UIEventType.ROLLOVER, OnButtonMouseOver);
		button.AddEventListener(UIEventType.ROLLOUT, OnButtonMouseOut);
	}

	private void ToggleButtonTextureState(bool enabled, BoxMenuButton button)
	{
		if (!(button == null))
		{
			if (enabled)
			{
				button.m_TextMesh.TextColor = m_EnabledMaterial;
			}
			else
			{
				button.m_TextMesh.TextColor = m_DisabledMaterial;
			}
		}
	}

	private void ToggleDrawerButtonState(bool enabled, BoxMenuButton button)
	{
		if (!(button == null))
		{
			if (enabled)
			{
				button.m_TextMesh.TextColor = m_EnabledDrawerMaterial;
			}
			else
			{
				button.m_TextMesh.TextColor = m_DisabledDrawerMaterial;
			}
		}
	}

	private void HighlightButton(BoxMenuButton button, bool highlightOn)
	{
		if (button.m_HighlightState == null)
		{
			Debug.LogWarning($"Box.HighlighButton {button} - highlight state is null");
			return;
		}
		ActorStateType stateType = (highlightOn ? ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE : ActorStateType.HIGHLIGHT_OFF);
		button.m_HighlightState.ChangeState(stateType);
	}

	private bool IsButtonHighlighted(BoxMenuButton button)
	{
		return button.m_HighlightState.CurrentState == ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE;
	}

	private void SetPackCount(int n)
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_ribbonButtons.SetPackCount(n);
		}
		else
		{
			m_OpenPacksButton.SetPackCount(n);
		}
	}

	public void EnableButton(PegUIElement button)
	{
		button.SetEnabled(enabled: true);
		PegUIElement ribbonButtonFromButton = GetRibbonButtonFromButton(button);
		if (ribbonButtonFromButton != null && ribbonButtonFromButton != button)
		{
			EnableButton(ribbonButtonFromButton);
		}
	}

	public void DisableButton(PegUIElement button)
	{
		if (!(button == null))
		{
			button.SetEnabled(enabled: false);
			TooltipZone component = button.GetComponent<TooltipZone>();
			if (component != null)
			{
				component.HideTooltip();
			}
			PegUIElement ribbonButtonFromButton = GetRibbonButtonFromButton(button);
			if (ribbonButtonFromButton != null && ribbonButtonFromButton != button)
			{
				DisableButton(ribbonButtonFromButton);
			}
		}
	}

	private PegUIElement GetRibbonButtonFromButton(PegUIElement button)
	{
		if (button == null || m_ribbonButtons == null)
		{
			return null;
		}
		if (button == m_CollectionButton)
		{
			return m_ribbonButtons.m_collectionManagerRibbon;
		}
		if (button == m_QuestLogButton)
		{
			return m_ribbonButtons.m_questLogRibbon;
		}
		if (button == m_OpenPacksButton)
		{
			return m_ribbonButtons.m_packOpeningRibbon;
		}
		if (button == m_StoreButton)
		{
			return m_ribbonButtons.m_storeRibbon;
		}
		return null;
	}

	private void OnButtonPressed(UIEvent e)
	{
		PegUIElement element = e.GetElement();
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		if (netObject != null)
		{
			flag = netObject.Games.Tournament;
			flag2 = netObject.Games.Practice;
			flag3 = netObject.Collection.Manager;
			flag4 = netObject.Games.TavernBrawl && TavernBrawlManager.Get().HasUnlockedAnyTavernBrawl;
		}
		BoxMenuButton boxMenuButton = (BoxMenuButton)element;
		if (boxMenuButton == m_StartButton)
		{
			OnStartButtonPressed(e);
		}
		else if (boxMenuButton == m_TournamentButton && flag)
		{
			OnTournamentButtonPressed(e);
		}
		else if (boxMenuButton == m_SoloAdventuresButton && flag2)
		{
			OnSoloAdventuresButtonPressed(e);
		}
		else if (boxMenuButton == m_GameModesButton)
		{
			OnGameModesButtonPressed(e);
		}
		else if (boxMenuButton == m_TavernBrawlButton && flag4)
		{
			OnTavernBrawlButtonPressed(e);
		}
		else if (boxMenuButton == m_OpenPacksButton)
		{
			OnOpenPacksButtonPressed(e);
		}
		else if (boxMenuButton == m_CollectionButton && flag3)
		{
			OnCollectionButtonPressed(e);
		}
		else if (boxMenuButton == m_setRotationButton)
		{
			OnSetRotationButtonPressed(e);
		}
	}

	private void ShowReconnectPopup(UIEvent e, ButtonPressFunction onButtonPressed)
	{
		DialogManager.Get().ShowReconnectHelperDialog(delegate
		{
			if (onButtonPressed != null)
			{
				onButtonPressed(e);
			}
		});
	}

	private void FireButtonPressEvent(ButtonType buttonType)
	{
		if (m_waitingForSceneLoad)
		{
			m_queuedButtonFire = buttonType;
			return;
		}
		ButtonPressListener[] array = m_buttonPressListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(buttonType);
		}
	}

	private void ClearQueuedButtonFireEvent()
	{
		m_queuedButtonFire = null;
	}

	private void OnStartButtonPressed(UIEvent e)
	{
		if (!HearthstoneServices.IsAvailable<SceneMgr>())
		{
			ChangeState(State.HUB);
		}
		else
		{
			FireButtonPressEvent(ButtonType.START);
		}
	}

	private void OnTournamentButtonPressed(UIEvent e)
	{
		if (!HearthstoneServices.IsAvailable<SceneMgr>())
		{
			ChangeState(State.OPEN);
			return;
		}
		if (!Options.Get().HasOption(Option.HAS_CLICKED_TOURNAMENT))
		{
			Options.Get().SetBool(Option.HAS_CLICKED_TOURNAMENT, val: true);
		}
		AchieveManager.Get().NotifyOfClick(Achievement.ClickTriggerType.BUTTON_PLAY);
		FireButtonPressEvent(ButtonType.TOURNAMENT);
	}

	private void OnSetRotationButtonPressed(UIEvent e)
	{
		Log.Box.Print("Set Rotation Button Pressed!");
		if (!HearthstoneServices.IsAvailable<SceneMgr>())
		{
			ChangeState(State.SET_ROTATION_OPEN);
			return;
		}
		if (!Options.Get().HasOption(Option.HAS_CLICKED_TOURNAMENT))
		{
			Options.Get().SetBool(Option.HAS_CLICKED_TOURNAMENT, val: true);
		}
		AchieveManager.Get().NotifyOfClick(Achievement.ClickTriggerType.BUTTON_PLAY);
		FireButtonPressEvent(ButtonType.SET_ROTATION);
	}

	private void OnSoloAdventuresButtonPressed(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			ShowReconnectPopup(e, OnSoloAdventuresButtonPressed);
		}
		else if (!HearthstoneServices.IsAvailable<SceneMgr>())
		{
			ChangeState(State.OPEN);
		}
		else
		{
			FireButtonPressEvent(ButtonType.ADVENTURE);
		}
	}

	private void OnGameModesButtonPressed(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			ShowReconnectPopup(e, OnGameModesButtonPressed);
		}
		else if (SceneMgr.Get() != null && !DialogManager.Get().ShowingDialog())
		{
			FireButtonPressEvent(ButtonType.GAME_MODES);
		}
	}

	private void OnForgeButtonPressed(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			ShowReconnectPopup(e, OnForgeButtonPressed);
			return;
		}
		if (!HearthstoneServices.IsAvailable<SceneMgr>())
		{
			ChangeState(State.OPEN);
			return;
		}
		AchieveManager.Get().NotifyOfClick(Achievement.ClickTriggerType.BUTTON_ARENA);
		FireButtonPressEvent(ButtonType.FORGE);
	}

	private void OnBaconButtonPressed(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			ShowReconnectPopup(e, OnBaconButtonPressed);
		}
		else if (SceneMgr.Get() == null)
		{
			ChangeState(State.OPEN);
		}
		else
		{
			FireButtonPressEvent(ButtonType.BACON);
		}
	}

	private void OnPvPDungeonRunButtonPressed(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			ShowReconnectPopup(e, OnBaconButtonPressed);
		}
		else if (!HearthstoneServices.IsAvailable<SceneMgr>())
		{
			ChangeState(State.OPEN);
		}
		else
		{
			FireButtonPressEvent(ButtonType.PVP_DUNGEON_RUN);
		}
	}

	private void OnTavernBrawlButtonPressed(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			ShowReconnectPopup(e, OnTavernBrawlButtonPressed);
			return;
		}
		if (!HearthstoneServices.IsAvailable<SceneMgr>())
		{
			ChangeState(State.OPEN);
			return;
		}
		PlayTavernBrawlCrowdSFX();
		FireButtonPressEvent(ButtonType.TAVERN_BRAWL);
	}

	private void OnOpenPacksButtonPressed(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			ShowReconnectPopup(e, OnOpenPacksButtonPressed);
		}
		else if (!HearthstoneServices.IsAvailable<SceneMgr>())
		{
			ChangeState(State.OPEN);
		}
		else
		{
			FireButtonPressEvent(ButtonType.OPEN_PACKS);
		}
	}

	public void OnCollectionButtonPressed(UIEvent e)
	{
		if (!HearthstoneServices.IsAvailable<SceneMgr>())
		{
			ChangeState(State.OPEN);
		}
		else
		{
			FireButtonPressEvent(ButtonType.COLLECTION);
		}
	}

	public void OnQuestButtonPressed(UIEvent e)
	{
		if (QuestManager.Get().IsSystemEnabled)
		{
			JournalButton componentInChildren = m_ribbonButtons.m_journalButtonWidget.GetComponentInChildren<JournalButton>();
			if (!(componentInChildren == null))
			{
				componentInChildren.OnClicked();
			}
		}
		else if (!Network.IsLoggedIn())
		{
			ShowReconnectPopup(e, OnQuestButtonPressed);
		}
		else if (!ShownUIMgr.Get().HasShownUI())
		{
			FireButtonPressEvent(ButtonType.QUEST_LOG);
			ShownUIMgr.Get().SetShownUI(ShownUIMgr.UI_WINDOW.QUEST_LOG);
			SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681", base.gameObject);
			m_tempInputBlocker = CameraUtils.CreateInputBlocker(Get().GetCamera(), "QuestLogInputBlocker", null, 30f);
			SceneUtils.SetLayer(m_tempInputBlocker, GameLayer.IgnoreFullScreenEffects);
			m_tempInputBlocker.AddComponent<PegUIElement>();
			StopCoroutine("ShowQuestLogWhenReady");
			StartCoroutine("ShowQuestLogWhenReady");
		}
	}

	private void OnButtonMouseOver(UIEvent e)
	{
		TooltipZone component = e.GetElement().gameObject.GetComponent<TooltipZone>();
		if (!(component == null))
		{
			string text = GameStrings.Get("GLUE_TOOLTIP_BUTTON_DISABLED_DESC");
			string bodytext = text;
			NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (netObject != null)
			{
				flag = netObject.Games.Tournament;
				flag2 = netObject.Games.Practice;
				flag3 = netObject.Collection.Manager;
			}
			if (component.targetObject == m_TournamentButton.gameObject && flag)
			{
				bodytext = GameStrings.Get("GLUE_TOOLTIP_BUTTON_TOURNAMENT_DESC");
			}
			else if (component.targetObject == m_SoloAdventuresButton.gameObject && flag2)
			{
				bodytext = GameStrings.Get("GLUE_TOOLTIP_BUTTON_ADVENTURE_DESC");
			}
			else if (component.targetObject == m_GameModesButton.gameObject)
			{
				bodytext = GameStrings.Get("GLUE_TOOLTIP_BUTTON_GAME_MODES_DESC");
			}
			else if (component.targetObject == m_TavernBrawlButton.gameObject)
			{
				bodytext = ((netObject == null || !netObject.Games.TavernBrawl) ? text : (TavernBrawlManager.Get().HasUnlockedTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL) ? GameStrings.Get("GLUE_TOOLTIP_BUTTON_TAVERN_BRAWL_DESC") : GameStrings.Get("GLUE_TOOLTIP_BUTTON_TAVERN_BRAWL_NOT_UNLOCKED")));
			}
			else if (component.targetObject == m_OpenPacksButton.gameObject)
			{
				bodytext = GameStrings.Get("GLUE_TOOLTIP_BUTTON_PACKOPEN_DESC");
			}
			else if (component.targetObject == m_CollectionButton.gameObject && flag3)
			{
				bodytext = GameStrings.Get("GLUE_TOOLTIP_BUTTON_COLLECTION_DESC");
			}
			if (component.targetObject == m_TournamentButton.gameObject)
			{
				component.ShowBoxTooltip(GameStrings.Get("GLUE_TOOLTIP_BUTTON_TOURNAMENT_HEADLINE"), bodytext);
			}
			else if (component.targetObject == m_SoloAdventuresButton.gameObject)
			{
				component.ShowBoxTooltip(GameStrings.Get("GLUE_TOOLTIP_BUTTON_ADVENTURE_HEADLINE"), bodytext);
			}
			else if (component.targetObject == m_GameModesButton.gameObject)
			{
				component.ShowBoxTooltip(GameStrings.Get("GLUE_TOOLTIP_BUTTON_GAME_MODES_HEADLINE"), bodytext);
			}
			else if (component.targetObject == m_TavernBrawlButton.gameObject)
			{
				component.ShowBoxTooltip(GameStrings.Get("GLUE_TOOLTIP_BUTTON_TAVERN_BRAWL_HEADLINE"), bodytext);
			}
			else if (component.targetObject == m_OpenPacksButton.gameObject)
			{
				component.ShowBoxTooltip(GameStrings.Get("GLUE_TOOLTIP_BUTTON_PACKOPEN_HEADLINE"), bodytext);
			}
			else if (component.targetObject == m_CollectionButton.gameObject)
			{
				component.ShowBoxTooltip(GameStrings.Get("GLUE_TOOLTIP_BUTTON_COLLECTION_HEADLINE"), bodytext);
			}
		}
	}

	private void OnButtonMouseOut(UIEvent e)
	{
		TooltipZone component = e.GetElement().gameObject.GetComponent<TooltipZone>();
		if (!(component == null))
		{
			component.HideTooltip();
		}
	}

	public void InitializeNet(bool fromLogin)
	{
		if (HearthstoneServices.TryGet<SceneMgr>(out var service))
		{
			m_waitingForNetData = true;
			if (service.GetMode() != SceneMgr.Mode.STARTUP || fromLogin)
			{
				NetCache.Get().RegisterScreenBox(OnNetCacheReady);
				NetCache.Get().RegisterUpdatedListener(typeof(NetCache.NetCacheBoosters), OnNetCacheBoostersUpdated);
			}
		}
	}

	private void ShutdownNet()
	{
		if (HearthstoneServices.TryGet<NetCache>(out var service))
		{
			service.UnregisterNetCacheHandler(OnNetCacheReady);
			service.RemoveUpdatedListener(typeof(NetCache.NetCacheBoosters), OnNetCacheBoostersUpdated);
		}
	}

	private void OnNetCacheReady()
	{
		m_waitingForNetData = false;
		if (Network.ShouldBeConnectedToAurora() && Network.IsLoggedIn())
		{
			RankMgr.Get().SetRankPresenceField(NetCache.Get().GetNetObject<NetCache.NetCacheMedalInfo>());
		}
		StartCoroutine(UpdateUIWhenCollectionReady());
	}

	private void OnNetCacheBoostersUpdated()
	{
		UpdateUI();
	}

	private int ComputeBoosterCount()
	{
		return NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>().GetTotalNumBoosters();
	}

	public void UnloadQuestLog()
	{
		m_questLogNetCacheDataState = DataState.UNLOADING;
		DestroyQuestLog();
	}

	private IEnumerator ShowQuestLogWhenReady()
	{
		if (m_questLog == null && !m_questLogLoading)
		{
			m_questLogLoading = true;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				AssetLoader.Get().InstantiatePrefab("QuestLog_phone.prefab:58714c7909a4eae48846cd14873cd8d5", OnQuestLogLoaded);
			}
			else
			{
				AssetLoader.Get().InstantiatePrefab("QuestLog.prefab:0e03112616f6aea4d844e2044b82d8c5", OnQuestLogLoaded);
			}
		}
		if (ShouldRequestData(m_questLogNetCacheDataState))
		{
			m_questLogNetCacheDataState = DataState.REQUEST_SENT;
			NetCache.Get().RegisterScreenQuestLog(OnQuestLogNetCacheReady);
		}
		while (m_questLog == null)
		{
			yield return null;
		}
		while (m_questLogNetCacheDataState != DataState.RECEIVED)
		{
			yield return null;
		}
		m_questLog.Show();
		yield return new WaitForSeconds(0.5f);
		UnityEngine.Object.Destroy(m_tempInputBlocker);
	}

	private void DestroyQuestLog()
	{
		StopCoroutine("ShowQuestLogWhenReady");
		if (!(m_questLog == null))
		{
			UnityEngine.Object.Destroy(m_questLog.gameObject);
		}
	}

	private void OnQuestLogLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_questLogLoading = false;
		if (go == null)
		{
			Debug.LogError($"QuestLogButton.OnQuestLogLoaded() - FAILED to load \"{assetRef}\"");
			return;
		}
		m_questLog = go.GetComponent<QuestLog>();
		if (m_questLog == null)
		{
			Debug.LogError($"QuestLogButton.OnQuestLogLoaded() - ERROR \"{base.name}\" has no {typeof(QuestLog)} component");
		}
		else
		{
			m_questLog.StartHidden();
		}
	}

	private void OnQuestLogNetCacheReady()
	{
		if (m_questLogNetCacheDataState != DataState.UNLOADING)
		{
			m_questLogNetCacheDataState = DataState.RECEIVED;
		}
	}

	private bool ShouldRequestData(DataState state)
	{
		return state switch
		{
			DataState.NONE => true, 
			DataState.UNLOADING => true, 
			_ => false, 
		};
	}

	private void OnStoreButtonReleased(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			Log.Store.PrintDebug("Cannot open Shop due to being offline.");
			ShowReconnectPopup(e, OnStoreButtonReleased);
			return;
		}
		if (FriendChallengeMgr.Get().HasChallenge())
		{
			Log.Store.PrintDebug("Cannot open Shop due to having friendly challenge.");
			return;
		}
		StoreManager storeManager = StoreManager.Get();
		storeManager?.Catalog.TryRefreshStaleProductAvailability();
		bool flag = false;
		if (storeManager == null)
		{
			Log.Store.PrintDebug("Cannot open Shop due to null StoreManager.");
			flag = true;
		}
		else if (!storeManager.IsOpen())
		{
			flag = true;
		}
		else if (m_StoreButton.IsVisualClosed())
		{
			Log.Store.PrintDebug("Cannot open Shop due to button is visually closed.");
			flag = true;
		}
		else if (SetRotationManager.Get().CheckForSetRotationRollover())
		{
			Log.Store.PrintDebug("Cannot open Shop due to pending set rotation rollover.");
			flag = true;
		}
		else if (PlayerMigrationManager.Get() != null && PlayerMigrationManager.Get().CheckForPlayerMigrationRequired())
		{
			Log.Store.PrintDebug("Cannot open Shop due to pending player migration.");
			flag = true;
		}
		else if (!storeManager.IsVintageStoreEnabled() && storeManager.Catalog.Tiers.Count == 0)
		{
			Log.Store.PrintWarning("Cannot open Shop due to no valid tier data received.");
			flag = true;
		}
		else if (SceneMgr.Get() == null || SceneMgr.Get().GetMode() != SceneMgr.Mode.HUB || SceneMgr.Get().IsTransitionNowOrPending())
		{
			Log.Store.PrintWarning("Cannot open Shop due to invalid scene state.");
			flag = true;
		}
		if (flag)
		{
			SoundManager.Get().LoadAndPlay("Store_closed_button_click.prefab:a6b74848e2c7e5748a20524b40fe6c1e", base.gameObject);
			return;
		}
		FireButtonPressEvent(ButtonType.STORE);
		FriendChallengeMgr.Get().OnStoreOpened();
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681", base.gameObject);
		StoreManager.Get().RegisterStoreShownListener(OnStoreShown);
		StoreManager.Get().StartGeneralTransaction();
	}

	private void OnStoreShown()
	{
		StoreManager.Get().RemoveStoreShownListener(OnStoreShown);
	}

	private void SetRotation_ShowRotationDisk()
	{
		if (m_DiskCenter != null)
		{
			m_DiskCenter.SetActive(value: false);
		}
		if (m_setRotationDisk != null)
		{
			m_setRotationDisk.SetActive(value: true);
			return;
		}
		BasicPopup.PopupInfo popupInfo = new BasicPopup.PopupInfo();
		popupInfo.m_blurWhenShown = true;
		popupInfo.m_prefabAssetRefs.Add("CoreSetIntroPopup.prefab:32fcd0d9c45bc9449af825460fac647b");
		DialogManager.Get().ShowBasicPopup(UserAttentionBlocker.ALL_EXCEPT_FATAL_ERROR_SCENE, popupInfo);
		m_setRotationDisk = AssetLoader.Get().InstantiatePrefab("TheBox_CenterDisk_SetRotation.prefab:6f2fa714f0d129e4197fd2922f544816");
		m_setRotationDisk.SetActive(value: true);
		m_setRotationDisk.transform.parent = m_Disk.transform;
		m_setRotationDisk.transform.localPosition = Vector3.zero;
		m_setRotationDisk.transform.localRotation = Quaternion.identity;
		m_setRotationButton = m_setRotationDisk.GetComponentInChildren<BoxMenuButton>();
		m_StoreButton.gameObject.SetActive(value: false);
		m_QuestLogButton.gameObject.SetActive(value: false);
		m_journalButtonWidget.Hide();
		HighlightState componentInChildren = m_setRotationButton.GetComponentInChildren<HighlightState>();
		if (componentInChildren != null)
		{
			componentInChildren.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
		RegisterButtonEvents(m_setRotationButton);
	}

	private IEnumerator SetRotationOpen_ChangeState()
	{
		BoxStateConfig boxStateConfig = m_stateConfigs[12];
		if (!boxStateConfig.m_logoState.m_ignore)
		{
			m_Logo.ChangeState(boxStateConfig.m_logoState.m_state);
		}
		if (!boxStateConfig.m_startButtonState.m_ignore)
		{
			m_StartButton.ChangeState(boxStateConfig.m_startButtonState.m_state);
		}
		if (!boxStateConfig.m_doorState.m_ignore)
		{
			m_LeftDoor.ChangeState(boxStateConfig.m_doorState.m_state);
			m_RightDoor.ChangeState(boxStateConfig.m_doorState.m_state);
		}
		if (!boxStateConfig.m_diskState.m_ignore)
		{
			m_Disk.ChangeState(boxStateConfig.m_diskState.m_state);
		}
		if (!boxStateConfig.m_camState.m_ignore)
		{
			m_Camera.ChangeState(BoxCamera.State.SET_ROTATION_OPENED);
		}
		if (!boxStateConfig.m_fullScreenBlackState.m_ignore)
		{
			FullScreenBlack_ChangeState(boxStateConfig.m_fullScreenBlackState.m_state);
		}
		SetRotationClock setRotationClock = SetRotationClock.Get();
		if (setRotationClock == null)
		{
			Debug.LogError("SetRotationOpen_ChangeState clock = null");
		}
		else
		{
			setRotationClock.StartTheClock();
		}
		yield break;
	}

	private IEnumerator SetRotation_StartSetRotationIntro()
	{
		ResetSetRotationPopupProgress();
		UserAttentionManager.StartBlocking(UserAttentionBlocker.SET_ROTATION_INTRO);
		NotificationManager.Get().DestroyAllPopUps();
		PopupDisplayManager.Get().ReadyToShowPopups();
		yield return StartCoroutine(PopupDisplayManager.Get().WaitForAllPopups());
		SetRotation_FinishShowingRewards();
	}

	private void SetRotation_ShowNerfedCards_DialogHidden(DialogBase dialog, object userData)
	{
		SetRotation_FinishShowingRewards();
	}

	private void SetRotation_FinishShowingRewards()
	{
		ChangeState(State.SET_ROTATION);
		SetRotation_ShowRotationDisk();
	}

	private void DoBoxSpecialEvents()
	{
		if (!m_activeSpecialEvent)
		{
			LoadBoxSpecialEvent();
		}
		if (!m_isSpecialEventActive)
		{
			if ((bool)m_activeSpecialEvent && (m_state == State.HUB || m_state == State.HUB_WITH_DRAWER))
			{
				Log.Box.Print("Box Special Event Birth: {0}", m_activeSpecialEvent);
				m_activeSpecialEvent.ActivateState(SpellStateType.BIRTH);
				m_isSpecialEventActive = true;
			}
		}
		else if ((bool)m_activeSpecialEvent && m_state != State.HUB && m_state != State.HUB_WITH_DRAWER)
		{
			Log.Box.Print("Box Special Event Death: {0}", m_activeSpecialEvent);
			m_activeSpecialEvent.ActivateState(SpellStateType.DEATH);
			m_isSpecialEventActive = false;
		}
	}

	private void LoadBoxSpecialEvent()
	{
		if (m_boxSpecialEventObj == null)
		{
			Log.Box.Print("BoxSpecialEventScriptableObj is null");
			return;
		}
		foreach (BoxSpecialEvent specialEvent in m_boxSpecialEventObj.m_specialEvents)
		{
			if (!SpecialEventManager.Get().IsEventActive(specialEvent.EventType, activeIfDoesNotExist: false) || (!SpecialEventManager.Get().IsEventForcedActive(specialEvent.EventType) && ((!specialEvent.showToNewPlayers && !AchieveManager.Get().HasUnlockedFeature(Achieve.Unlocks.DAILY)) || (!specialEvent.showToReturningPlayers && ReturningPlayerMgr.Get().IsInReturningPlayerMode))))
			{
				continue;
			}
			Log.Box.Print("Box Special Event: {0}", specialEvent.EventType);
			if (PlatformSettings.Screen == ScreenCategory.Phone)
			{
				if (!string.IsNullOrEmpty(specialEvent.BoxTexturePhone))
				{
					LoadSpecialEventBoxTexture(specialEvent.BoxTexturePhone);
				}
			}
			else if (!string.IsNullOrEmpty(specialEvent.BoxTexture))
			{
				LoadSpecialEventBoxTexture(specialEvent.BoxTexture);
			}
			if (!string.IsNullOrEmpty(specialEvent.TableTexture))
			{
				LoadSpecialEventTableTexture(specialEvent.TableTexture);
			}
			if (!string.IsNullOrEmpty(specialEvent.Prefab))
			{
				LoadSpecialEventSpell(specialEvent);
			}
		}
	}

	private void LoadSpecialEventTableTexture(string texturePath)
	{
		Log.Box.Print("Loading Special Event Table Texture: {0}", texturePath);
		AssetLoader.Get().LoadAsset(ref m_tableTopTexture, texturePath);
		if (m_tableTopTexture == null)
		{
			Debug.LogWarning($"Failed to special event table texture: {texturePath}");
		}
		else if (m_tableTop != null)
		{
			Renderer component = m_tableTop.GetComponent<Renderer>();
			if (component != null)
			{
				component.GetMaterial().mainTexture = m_tableTopTexture;
			}
		}
	}

	private void LoadSpecialEventBoxTexture(string texturePath)
	{
		Log.Box.Print("Loading Special Event Box Texture: {0}", texturePath);
		AssetLoader.Get().LoadAsset(ref m_specialEventTexture, texturePath);
		if (m_specialEventTexture == null)
		{
			Debug.LogWarning($"Failed to special event box texture: {texturePath}");
			return;
		}
		if (m_LeftDoor != null)
		{
			Renderer component = m_LeftDoor.GetComponent<Renderer>();
			if (component != null)
			{
				component.GetMaterial().mainTexture = m_specialEventTexture;
			}
		}
		if (m_RightDoor != null)
		{
			Renderer component2 = m_RightDoor.GetComponent<Renderer>();
			if (component2 != null)
			{
				component2.GetMaterial().mainTexture = m_specialEventTexture;
			}
		}
		if (m_DiskCenter != null)
		{
			Renderer component3 = m_DiskCenter.GetComponent<Renderer>();
			if (component3 != null)
			{
				component3.GetMaterial().mainTexture = m_specialEventTexture;
			}
		}
		if (m_Drawer != null)
		{
			Renderer component4 = m_Drawer.GetComponent<Renderer>();
			if (component4 != null)
			{
				component4.GetMaterial().mainTexture = m_specialEventTexture;
			}
		}
		if (m_CollectionButton != null)
		{
			Renderer component5 = m_CollectionButton.GetComponent<Renderer>();
			if (component5 != null)
			{
				component5.GetMaterial().mainTexture = m_specialEventTexture;
			}
		}
		if (m_OpenPacksButton != null)
		{
			Renderer component6 = m_OpenPacksButton.GetComponent<Renderer>();
			if (component6 != null)
			{
				component6.GetMaterial().mainTexture = m_specialEventTexture;
			}
		}
	}

	private void LoadSpecialEventSpell(BoxSpecialEvent boxSpecialEvent)
	{
		string text = boxSpecialEvent.Prefab;
		if (PlatformSettings.Screen == ScreenCategory.Phone && !string.IsNullOrEmpty(boxSpecialEvent.PrefabPhoneOverride))
		{
			text = boxSpecialEvent.PrefabPhoneOverride;
		}
		Log.Box.Print("Loading Box Special Event Prefab: {0}", text);
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text);
		if (gameObject == null)
		{
			Debug.LogWarning($"Failed to load special box event: {text}");
			return;
		}
		gameObject.transform.parent = base.transform;
		m_activeSpecialEvent = gameObject.GetComponent<Spell>();
		if (m_activeSpecialEvent == null)
		{
			Debug.LogWarning($"Spell component not found for special box event: {text}");
			return;
		}
		m_activeSpecialEvent.ActivateState(SpellStateType.BIRTH);
		m_isSpecialEventActive = true;
	}

	public void ShowGameModesDialog()
	{
		DialogManager.Get().ShowGameModesPopup(OnForgeButtonPressed, OnBaconButtonPressed, OnPvPDungeonRunButtonPressed);
	}

	private void InitializeComponents()
	{
		m_Logo.SetParent(this);
		m_Logo.SetInfo(m_StateInfoList.m_LogoInfo);
		m_StartButton.SetParent(this);
		m_StartButton.SetInfo(m_StateInfoList.m_StartButtonInfo);
		m_LeftDoor.SetParent(this);
		m_LeftDoor.SetInfo(m_StateInfoList.m_LeftDoorInfo);
		m_RightDoor.SetParent(this);
		m_RightDoor.SetInfo(m_StateInfoList.m_RightDoorInfo);
		m_RightDoor.EnableMain(enable: true);
		m_Disk.SetParent(this);
		m_Disk.SetInfo(m_StateInfoList.m_DiskInfo);
		m_TopSpinner.SetParent(this);
		m_TopSpinner.SetInfo(m_StateInfoList.m_SpinnerInfo);
		m_BottomSpinner.SetParent(this);
		m_BottomSpinner.SetInfo(m_StateInfoList.m_SpinnerInfo);
		m_Drawer.SetParent(this);
		m_Drawer.SetInfo(m_StateInfoList.m_DrawerInfo);
		m_Camera.SetParent(this);
		m_Camera.SetInfo(m_StateInfoList.m_CameraInfo);
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.BlendToColorEnable = false;
	}

	private void OnBoxTopPhoneTextureLoaded(AssetReference assetRef, AssetHandle<Texture> newTexture, object callbackData)
	{
		AssetHandle.Take(ref m_boxTopTexture, newTexture);
		MeshRenderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer renderer in componentsInChildren)
		{
			Material sharedMaterial = renderer.GetSharedMaterial();
			if (sharedMaterial != null && sharedMaterial.HasProperty("_MainTex"))
			{
				Texture mainTexture = sharedMaterial.mainTexture;
				if (mainTexture != null && mainTexture.name.Equals("TheBox_Top"))
				{
					renderer.GetMaterial().mainTexture = newTexture;
				}
			}
		}
	}

	private void TrackBoxInteractable()
	{
		if (m_state == State.PRESS_START || m_state == State.HUB || m_state == State.SET_ROTATION || m_state == State.HUB_WITH_DRAWER)
		{
			HearthstonePerformance.Get()?.CaptureBoxInteractableTime();
		}
	}

	private void ResetSetRotationPopupProgress()
	{
		GameSaveDataManager gameSaveDataManager = GameSaveDataManager.Get();
		if (gameSaveDataManager == null)
		{
			return;
		}
		bool num = gameSaveDataManager.IsDataReady(GameSaveKeyId.SET_ROTATION);
		bool flag = false;
		List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
		if (!num)
		{
			flag = true;
			list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.ROTATED_BOOSTER_POPUP_PROGRESS, default(long)));
			list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.INNKEEPER_STANDARD_DECKS_VO_PROGRESS, default(long)));
		}
		else
		{
			long value = -1L;
			long value2 = -1L;
			gameSaveDataManager.GetSubkeyValue(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.ROTATED_BOOSTER_POPUP_PROGRESS, out value);
			gameSaveDataManager.GetSubkeyValue(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.INNKEEPER_STANDARD_DECKS_VO_PROGRESS, out value2);
			if (value != 0L)
			{
				list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.ROTATED_BOOSTER_POPUP_PROGRESS, default(long)));
				flag = true;
			}
			if (value2 != 0L)
			{
				list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.INNKEEPER_STANDARD_DECKS_VO_PROGRESS, default(long)));
				flag = true;
			}
		}
		if (flag)
		{
			gameSaveDataManager.SaveSubkeys(list);
		}
	}
}
