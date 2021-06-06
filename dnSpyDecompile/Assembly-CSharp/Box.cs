using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.AssetManager;
using Hearthstone.Progression;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

// Token: 0x020000BA RID: 186
[CustomEditClass]
public class Box : MonoBehaviour
{
	// Token: 0x06000B8C RID: 2956 RVA: 0x0004376C File Offset: 0x0004196C
	private void Awake()
	{
		Log.LoadingScreen.Print("Box.Awake()", Array.Empty<object>());
		Box.s_instance = this;
		this.InitializeStateConfigs();
		if (LoadingScreen.Get() != null)
		{
			LoadingScreen.Get().NotifyMainSceneObjectAwoke(base.gameObject);
		}
		this.m_originalLeftDoorLayer = (GameLayer)this.m_LeftDoor.gameObject.layer;
		this.m_originalRightDoorLayer = (GameLayer)this.m_RightDoor.gameObject.layer;
		this.m_originalDrawerLayer = (GameLayer)this.m_Drawer.gameObject.layer;
		if (UniversalInputManager.UsePhoneUI)
		{
			if (TransformUtil.GetAspectRatioDependentValue(0f, 1f, 1f) < 0.99f)
			{
				GameUtils.InstantiateGameObject("Letterboxing.prefab:303d7852a40ab4f178a3f97a102a0ea0", this.m_letterboxingContainer, false);
			}
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab("RibbonButtons_Phone.prefab:1b805ba741fd649cabb72b2764c755f5", AssetLoadingOptions.None);
			this.m_ribbonButtons = gameObject.GetComponent<RibbonButtonsUI>();
			this.m_ribbonButtons.Toggle(false);
			GameUtils.SetParent(gameObject, this.m_rootObject, false);
			AssetLoader.Get().LoadAsset<Texture>("TheBox_Top_phone.psd:666e602b70e7d6344be3e690de329636", new AssetHandleCallback<Texture>(this.OnBoxTopPhoneTextureLoaded), null, AssetLoadingOptions.None);
		}
		if (SpecialEventManager.Get().HasReceivedEventTimingsFromServer)
		{
			this.DoBoxSpecialEvents();
		}
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x000438A4 File Offset: 0x00041AA4
	private void Start()
	{
		this.InitializeNet(false);
		this.InitializeComponents();
		this.InitializeState();
		this.InitializeUI();
		if (DemoMgr.Get().IsExpoDemo())
		{
			this.m_StoreButton.gameObject.SetActive(false);
			this.m_Drawer.gameObject.SetActive(false);
			this.m_QuestLogButton.gameObject.SetActive(false);
		}
		if (this.m_state != Box.State.HUB_WITH_DRAWER)
		{
			this.m_journalButtonWidget.Hide();
		}
		TavernBrawlManager tavernBrawlManager = TavernBrawlManager.Get();
		tavernBrawlManager.OnTavernBrawlUpdated += this.TavernBrawl_UpdateUI;
		if (tavernBrawlManager.IsCurrentBrawlInfoReady)
		{
			this.DoTavernBrawlButtonInitialization();
		}
		else
		{
			tavernBrawlManager.OnTavernBrawlUpdated += this.DoTavernBrawlButtonInitialization;
		}
		FiresideGatheringManager.Get().OnSignShown += this.OnFSGSignShown;
		FiresideGatheringManager.Get().OnLeaveFSG += this.OnLeaveFSG;
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x00043984 File Offset: 0x00041B84
	private void OnDestroy()
	{
		Log.LoadingScreen.Print("Box.OnDestroy()", Array.Empty<object>());
		if (PegUI.Get() != null)
		{
			PegUI.Get().RemoveInputCamera(this.m_Camera.GetComponent<Camera>());
		}
		TavernBrawlManager tavernBrawlManager;
		if (HearthstoneServices.TryGet<TavernBrawlManager>(out tavernBrawlManager))
		{
			tavernBrawlManager.OnTavernBrawlUpdated -= this.DoTavernBrawlButtonInitialization;
			tavernBrawlManager.OnTavernBrawlUpdated -= this.TavernBrawl_UpdateUI;
		}
		FiresideGatheringManager firesideGatheringManager;
		if (HearthstoneServices.TryGet<FiresideGatheringManager>(out firesideGatheringManager))
		{
			firesideGatheringManager.OnSignShown -= this.OnFSGSignShown;
			firesideGatheringManager.OnLeaveFSG -= this.OnLeaveFSG;
		}
		NetCache netCache;
		if (HearthstoneServices.TryGet<NetCache>(out netCache))
		{
			netCache.RemoveUpdatedListener(typeof(NetCache.NetCacheFeatures), new Action(this.DoTavernBrawlButtonInitialization));
			netCache.RemoveUpdatedListener(typeof(NetCache.NetCacheHeroLevels), new Action(this.DoTavernBrawlButtonInitialization));
		}
		StoreManager.Get().RemoveStoreShownListener(new Action(this.OnStoreShown));
		if (LoadingScreen.Get() != null)
		{
			LoadingScreen.Get().UnregisterPreviousSceneDestroyedListener(new LoadingScreen.PreviousSceneDestroyedCallback(this.OnTutorialSceneDestroyed));
		}
		this.ShutdownState();
		this.ShutdownNet();
		AssetHandle.SafeDispose<Texture>(ref this.m_tableTopTexture);
		AssetHandle.SafeDispose<Texture>(ref this.m_boxTopTexture);
		AssetHandle.SafeDispose<Texture>(ref this.m_specialEventTexture);
		Box.s_instance = null;
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x00043ACF File Offset: 0x00041CCF
	public static Box Get()
	{
		return Box.s_instance;
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x00043AD6 File Offset: 0x00041CD6
	public Camera GetCamera()
	{
		return this.m_Camera.GetComponent<Camera>();
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x00043AE3 File Offset: 0x00041CE3
	public BoxCamera GetBoxCamera()
	{
		return this.m_Camera;
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x00043AEB File Offset: 0x00041CEB
	public Camera GetNoFxCamera()
	{
		return this.m_NoFxCamera;
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x00043AF3 File Offset: 0x00041CF3
	public AudioListener GetAudioListener()
	{
		return this.m_AudioListener;
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x00043AFB File Offset: 0x00041CFB
	public Box.State GetState()
	{
		return this.m_state;
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x00043B03 File Offset: 0x00041D03
	public Texture2D GetTextureCompressionTestTexture()
	{
		return this.m_textureCompressionTest;
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x00043B0B File Offset: 0x00041D0B
	public bool ChangeState(Box.State state)
	{
		if (state == Box.State.INVALID)
		{
			return false;
		}
		if (this.m_state == state)
		{
			return false;
		}
		if (this.HasPendingEffects())
		{
			this.QueueStateChange(state);
		}
		else
		{
			this.ChangeStateNow(state);
		}
		return true;
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x00043B38 File Offset: 0x00041D38
	public void UpdateState()
	{
		if (this.m_state == Box.State.STARTUP)
		{
			this.UpdateState_Startup();
			return;
		}
		if (this.m_state == Box.State.PRESS_START)
		{
			this.UpdateState_PressStart();
			return;
		}
		if (this.m_state == Box.State.LOADING_HUB)
		{
			this.UpdateState_LoadingHub();
			return;
		}
		if (this.m_state == Box.State.LOADING)
		{
			this.UpdateState_Loading();
			return;
		}
		if (this.m_state == Box.State.HUB)
		{
			this.UpdateState_Hub();
			return;
		}
		if (this.m_state == Box.State.HUB_WITH_DRAWER)
		{
			this.UpdateState_HubWithDrawer();
			return;
		}
		if (this.m_state == Box.State.OPEN)
		{
			this.UpdateState_Open();
			return;
		}
		if (this.m_state == Box.State.CLOSED)
		{
			this.UpdateState_Closed();
			return;
		}
		if (this.m_state == Box.State.ERROR)
		{
			this.UpdateState_Error();
			return;
		}
		if (this.m_state == Box.State.SET_ROTATION_LOADING)
		{
			this.UpdateState_SetRotation();
			return;
		}
		if (this.m_state == Box.State.SET_ROTATION)
		{
			this.UpdateState_SetRotation();
			return;
		}
		if (this.m_state == Box.State.SET_ROTATION_OPEN)
		{
			this.UpdateState_SetRotationOpen();
			return;
		}
		Debug.LogError(string.Format("Box.UpdateState() - unhandled state {0}", this.m_state));
	}

	// Token: 0x06000B98 RID: 2968 RVA: 0x00043C23 File Offset: 0x00041E23
	public BoxLightMgr GetLightMgr()
	{
		return this.m_LightMgr;
	}

	// Token: 0x06000B99 RID: 2969 RVA: 0x00043C2B File Offset: 0x00041E2B
	public BoxLightStateType GetLightState()
	{
		return this.m_LightMgr.GetActiveState();
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x00043C38 File Offset: 0x00041E38
	public void ChangeLightState(BoxLightStateType stateType)
	{
		this.m_LightMgr.ChangeState(stateType);
	}

	// Token: 0x06000B9B RID: 2971 RVA: 0x00043C46 File Offset: 0x00041E46
	public void SetLightState(BoxLightStateType stateType)
	{
		this.m_LightMgr.SetState(stateType);
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x00043C54 File Offset: 0x00041E54
	public BoxEventMgr GetEventMgr()
	{
		return this.m_EventMgr;
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x00043C5C File Offset: 0x00041E5C
	public Spell GetEventSpell(BoxEventType eventType)
	{
		return this.m_EventMgr.GetEventSpell(eventType);
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x00043C6A File Offset: 0x00041E6A
	public bool HasPendingEffects()
	{
		return this.m_pendingEffects > 0;
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x00043C75 File Offset: 0x00041E75
	public bool IsBusy()
	{
		return this.HasPendingEffects() || this.m_stateQueue.Count > 0;
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x00043C8F File Offset: 0x00041E8F
	public bool IsTransitioningToSceneMode()
	{
		return this.m_transitioningToSceneMode;
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x00043C97 File Offset: 0x00041E97
	public bool IsTutorial()
	{
		return this.m_LightMgr.GetActiveState() == BoxLightStateType.TUTORIAL;
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x00043CA7 File Offset: 0x00041EA7
	public void OnAnimStarted()
	{
		this.m_pendingEffects++;
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x00043CB8 File Offset: 0x00041EB8
	public void OnAnimFinished()
	{
		this.m_pendingEffects--;
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_OuterFrame.SetActive(false);
		}
		if (this.HasPendingEffects())
		{
			return;
		}
		if (this.m_stateQueue.Count == 0)
		{
			this.UpdateUIEvents();
			if (this.m_transitioningToSceneMode)
			{
				if (UniversalInputManager.UsePhoneUI)
				{
					bool flag = this.m_state == Box.State.HUB_WITH_DRAWER;
					if (flag != this.m_showRibbonButtons)
					{
						this.ToggleRibbonUI(flag);
					}
				}
				this.FireTransitionFinishedEvent();
				this.m_transitioningToSceneMode = false;
				return;
			}
		}
		else
		{
			this.ChangeStateQueued();
		}
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x00043D49 File Offset: 0x00041F49
	public void OnLoggedIn()
	{
		this.InitializeNet(true);
		this.DoBoxSpecialEvents();
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x00043D58 File Offset: 0x00041F58
	public void AddTransitionFinishedListener(Box.TransitionFinishedCallback callback)
	{
		this.AddTransitionFinishedListener(callback, null);
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x00043D64 File Offset: 0x00041F64
	public void AddTransitionFinishedListener(Box.TransitionFinishedCallback callback, object userData)
	{
		Box.TransitionFinishedListener transitionFinishedListener = new Box.TransitionFinishedListener();
		transitionFinishedListener.SetCallback(callback);
		transitionFinishedListener.SetUserData(userData);
		if (this.m_transitionFinishedListeners.Contains(transitionFinishedListener))
		{
			return;
		}
		this.m_transitionFinishedListeners.Add(transitionFinishedListener);
	}

	// Token: 0x06000BA7 RID: 2983 RVA: 0x00043DA0 File Offset: 0x00041FA0
	public bool RemoveTransitionFinishedListener(Box.TransitionFinishedCallback callback)
	{
		return this.RemoveTransitionFinishedListener(callback, null);
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x00043DAC File Offset: 0x00041FAC
	public bool RemoveTransitionFinishedListener(Box.TransitionFinishedCallback callback, object userData)
	{
		Box.TransitionFinishedListener transitionFinishedListener = new Box.TransitionFinishedListener();
		transitionFinishedListener.SetCallback(callback);
		transitionFinishedListener.SetUserData(userData);
		return this.m_transitionFinishedListeners.Remove(transitionFinishedListener);
	}

	// Token: 0x06000BA9 RID: 2985 RVA: 0x00043DD9 File Offset: 0x00041FD9
	public void AddButtonPressListener(Box.ButtonPressCallback callback)
	{
		this.AddButtonPressListener(callback, null);
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x00043DE4 File Offset: 0x00041FE4
	public void AddButtonPressListener(Box.ButtonPressCallback callback, object userData)
	{
		Box.ButtonPressListener buttonPressListener = new Box.ButtonPressListener();
		buttonPressListener.SetCallback(callback);
		buttonPressListener.SetUserData(userData);
		if (this.m_buttonPressListeners.Contains(buttonPressListener))
		{
			return;
		}
		this.m_buttonPressListeners.Add(buttonPressListener);
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x00043E20 File Offset: 0x00042020
	public bool RemoveButtonPressListener(Box.ButtonPressCallback callback)
	{
		return this.RemoveButtonPressListener(callback, null);
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x00043E2C File Offset: 0x0004202C
	public bool RemoveButtonPressListener(Box.ButtonPressCallback callback, object userData)
	{
		Box.ButtonPressListener buttonPressListener = new Box.ButtonPressListener();
		buttonPressListener.SetCallback(callback);
		buttonPressListener.SetUserData(userData);
		return this.m_buttonPressListeners.Remove(buttonPressListener);
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x00043E5C File Offset: 0x0004205C
	public void SetToIgnoreFullScreenEffects(bool ignoreEffects)
	{
		if (ignoreEffects)
		{
			SceneUtils.ReplaceLayer(this.m_LeftDoor.gameObject, GameLayer.IgnoreFullScreenEffects, this.m_originalLeftDoorLayer);
			SceneUtils.ReplaceLayer(this.m_RightDoor.gameObject, GameLayer.IgnoreFullScreenEffects, this.m_originalRightDoorLayer);
			SceneUtils.ReplaceLayer(this.m_Drawer.gameObject, GameLayer.IgnoreFullScreenEffects, this.m_originalDrawerLayer);
			return;
		}
		SceneUtils.ReplaceLayer(this.m_LeftDoor.gameObject, this.m_originalLeftDoorLayer, GameLayer.IgnoreFullScreenEffects);
		SceneUtils.ReplaceLayer(this.m_RightDoor.gameObject, this.m_originalRightDoorLayer, GameLayer.IgnoreFullScreenEffects);
		SceneUtils.ReplaceLayer(this.m_Drawer.gameObject, this.m_originalDrawerLayer, GameLayer.IgnoreFullScreenEffects);
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x00043F00 File Offset: 0x00042100
	public void PlayTavernBrawlCrowdSFX()
	{
		if (this.m_tavernBrawlEnterCrowdSounds.Count < 1)
		{
			return;
		}
		int index = UnityEngine.Random.Range(0, this.m_tavernBrawlEnterCrowdSounds.Count);
		SoundManager.Get().LoadAndPlay(this.m_tavernBrawlEnterCrowdSounds[index]);
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x00043F4C File Offset: 0x0004214C
	private void InitializeStateConfigs()
	{
		int length = Enum.GetValues(typeof(Box.State)).Length;
		this.m_stateConfigs = new Box.BoxStateConfig[length];
		this.m_stateConfigs[1] = new Box.BoxStateConfig
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
		this.m_stateConfigs[2] = new Box.BoxStateConfig
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
		this.m_stateConfigs[4] = new Box.BoxStateConfig
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
		this.m_stateConfigs[3] = new Box.BoxStateConfig
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
		this.m_stateConfigs[5] = new Box.BoxStateConfig
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
		this.m_stateConfigs[6] = new Box.BoxStateConfig
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
		this.m_stateConfigs[7] = new Box.BoxStateConfig
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
		this.m_stateConfigs[8] = new Box.BoxStateConfig
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
		this.m_stateConfigs[9] = new Box.BoxStateConfig
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
		this.m_stateConfigs[10] = new Box.BoxStateConfig
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
		this.m_stateConfigs[11] = new Box.BoxStateConfig
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
		this.m_stateConfigs[12] = new Box.BoxStateConfig
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

	// Token: 0x06000BB0 RID: 2992 RVA: 0x0004440C File Offset: 0x0004260C
	private void InitializeState()
	{
		this.m_state = Box.State.STARTUP;
		bool flag = GameMgr.Get().WasTutorial() && !GameMgr.Get().WasSpectator();
		SceneMgr sceneMgr;
		if (HearthstoneServices.TryGet<SceneMgr>(out sceneMgr))
		{
			if (flag)
			{
				this.m_state = Box.State.LOADING;
			}
			else
			{
				sceneMgr.RegisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
				sceneMgr.RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
				this.m_state = this.TranslateSceneModeToBoxState(sceneMgr.GetMode());
			}
		}
		this.UpdateState();
		this.m_TopSpinner.Spin();
		this.m_BottomSpinner.Spin();
		if (flag)
		{
			LoadingScreen.Get().RegisterPreviousSceneDestroyedListener(new LoadingScreen.PreviousSceneDestroyedCallback(this.OnTutorialSceneDestroyed));
		}
		if (this.m_state == Box.State.HUB_WITH_DRAWER)
		{
			this.ToggleRibbonUI(true);
			this.m_journalButtonWidget.Show();
			this.m_journalButtonWidget.TriggerEvent("ENABLE_INTERACTION", default(Widget.TriggerEventParameters));
		}
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x000444F3 File Offset: 0x000426F3
	private void OnTutorialSceneDestroyed(object userData)
	{
		LoadingScreen.Get().UnregisterPreviousSceneDestroyedListener(new LoadingScreen.PreviousSceneDestroyedCallback(this.OnTutorialSceneDestroyed));
		Spell eventSpell = this.GetEventSpell(BoxEventType.TUTORIAL_PLAY);
		eventSpell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnTutorialPlaySpellStateFinished));
		eventSpell.ActivateState(SpellStateType.DEATH);
	}

	// Token: 0x06000BB2 RID: 2994 RVA: 0x0004452C File Offset: 0x0004272C
	private void OnTutorialPlaySpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		SceneMgr sceneMgr = SceneMgr.Get();
		sceneMgr.RegisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
		sceneMgr.RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
		this.ChangeStateToReflectSceneMode(SceneMgr.Get().GetMode(), false);
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x0004457C File Offset: 0x0004277C
	private void ShutdownState()
	{
		if (this.m_StoreButton != null)
		{
			this.m_StoreButton.Unload();
		}
		this.UnloadQuestLog();
		SceneMgr sceneMgr;
		if (HearthstoneServices.TryGet<SceneMgr>(out sceneMgr))
		{
			sceneMgr.UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
			sceneMgr.UnregisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
		}
	}

	// Token: 0x06000BB4 RID: 2996 RVA: 0x000445D7 File Offset: 0x000427D7
	private void QueueStateChange(Box.State state)
	{
		this.m_stateQueue.Enqueue(state);
	}

	// Token: 0x06000BB5 RID: 2997 RVA: 0x000445E8 File Offset: 0x000427E8
	private void ChangeStateQueued()
	{
		Box.State state = this.m_stateQueue.Dequeue();
		this.ChangeStateNow(state);
	}

	// Token: 0x06000BB6 RID: 2998 RVA: 0x00044608 File Offset: 0x00042808
	private void ChangeStateNow(Box.State state)
	{
		bool flag = SetRotationManager.ShouldShowSetRotationIntro();
		if (!flag)
		{
			if (this.m_DiskCenter != null)
			{
				this.m_DiskCenter.SetActive(true);
			}
			if (this.m_setRotationDisk != null)
			{
				this.m_setRotationDisk.SetActive(false);
			}
		}
		if (state == Box.State.OPEN && flag)
		{
			state = Box.State.SET_ROTATION_OPEN;
		}
		this.m_state = state;
		this.TrackBoxInteractable();
		if (state == Box.State.STARTUP)
		{
			this.ChangeState_Startup();
		}
		else if (state == Box.State.PRESS_START)
		{
			this.ChangeState_PressStart();
		}
		else if (state == Box.State.LOADING_HUB)
		{
			this.ChangeState_LoadingHub();
		}
		else if (state == Box.State.LOADING)
		{
			this.ChangeState_Loading();
		}
		else if (state == Box.State.HUB)
		{
			this.ChangeState_Hub();
		}
		else if (state == Box.State.HUB_WITH_DRAWER)
		{
			this.ChangeState_HubWithDrawer();
		}
		else if (state == Box.State.OPEN)
		{
			this.ChangeState_Open();
		}
		else if (state == Box.State.CLOSED)
		{
			this.ChangeState_Closed();
		}
		else if (state == Box.State.ERROR)
		{
			this.ChangeState_Error();
		}
		else if (state == Box.State.SET_ROTATION_LOADING)
		{
			this.ChangeState_SetRotationLoading();
		}
		else if (state == Box.State.SET_ROTATION)
		{
			this.ChangeState_SetRotation();
		}
		else if (state == Box.State.SET_ROTATION_OPEN)
		{
			this.ChangeState_SetRotationOpen();
		}
		else
		{
			Debug.LogError(string.Format("Box.ChangeStateNow() - unhandled state {0}", state));
		}
		this.UpdateUIEvents();
	}

	// Token: 0x06000BB7 RID: 2999 RVA: 0x00044724 File Offset: 0x00042924
	private void ChangeStateToReflectSceneMode(SceneMgr.Mode mode, bool isSceneActuallyLoaded)
	{
		Box.State state = this.TranslateSceneModeToBoxState(mode);
		bool flag = SetRotationManager.ShouldShowSetRotationIntro();
		if (mode == SceneMgr.Mode.HUB && flag)
		{
			this.ChangeState(Box.State.SET_ROTATION_LOADING);
			if (isSceneActuallyLoaded)
			{
				base.StartCoroutine(this.SetRotation_StartSetRotationIntro());
			}
		}
		else if (mode == SceneMgr.Mode.TOURNAMENT && flag)
		{
			this.ChangeState(Box.State.SET_ROTATION_OPEN);
			UserAttentionManager.StartBlocking(UserAttentionBlocker.SET_ROTATION_INTRO);
			this.m_transitioningToSceneMode = true;
		}
		else if (!SceneMgr.Get().IsDoingSceneDrivenTransition() && this.ChangeState(state))
		{
			this.m_transitioningToSceneMode = true;
		}
		BoxLightStateType stateType = this.TranslateSceneModeToLightState(mode);
		this.m_LightMgr.ChangeState(stateType);
	}

	// Token: 0x06000BB8 RID: 3000 RVA: 0x000447B4 File Offset: 0x000429B4
	public void TryToStartSetRotationFromHub()
	{
		int mode = (int)SceneMgr.Get().GetMode();
		bool flag = SetRotationManager.ShouldShowSetRotationIntro();
		if (mode == 3 && flag)
		{
			this.ChangeState(Box.State.SET_ROTATION_LOADING);
			base.StartCoroutine(this.SetRotation_StartSetRotationIntro());
		}
	}

	// Token: 0x06000BB9 RID: 3001 RVA: 0x000447F0 File Offset: 0x000429F0
	private Box.State TranslateSceneModeToBoxState(SceneMgr.Mode mode)
	{
		if (mode == SceneMgr.Mode.STARTUP)
		{
			return Box.State.STARTUP;
		}
		if (mode == SceneMgr.Mode.LOGIN)
		{
			return Box.State.INVALID;
		}
		if (mode == SceneMgr.Mode.HUB)
		{
			return Box.State.HUB_WITH_DRAWER;
		}
		if (mode == SceneMgr.Mode.TOURNAMENT)
		{
			return Box.State.OPEN;
		}
		if (mode == SceneMgr.Mode.ADVENTURE)
		{
			return Box.State.OPEN;
		}
		if (mode == SceneMgr.Mode.FRIENDLY)
		{
			return Box.State.OPEN;
		}
		if (mode == SceneMgr.Mode.DRAFT)
		{
			return Box.State.OPEN;
		}
		if (mode == SceneMgr.Mode.COLLECTIONMANAGER)
		{
			return Box.State.OPEN;
		}
		if (mode == SceneMgr.Mode.TAVERN_BRAWL)
		{
			return Box.State.OPEN;
		}
		if (mode == SceneMgr.Mode.PACKOPENING)
		{
			return Box.State.OPEN;
		}
		if (mode == SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			return Box.State.OPEN;
		}
		if (mode == SceneMgr.Mode.BACON)
		{
			return Box.State.OPEN;
		}
		if (mode == SceneMgr.Mode.GAME_MODE)
		{
			return Box.State.OPEN;
		}
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			return Box.State.INVALID;
		}
		if (mode == SceneMgr.Mode.FATAL_ERROR)
		{
			return Box.State.ERROR;
		}
		return Box.State.OPEN;
	}

	// Token: 0x06000BBA RID: 3002 RVA: 0x00044860 File Offset: 0x00042A60
	private BoxLightStateType TranslateSceneModeToLightState(SceneMgr.Mode mode)
	{
		if (mode == SceneMgr.Mode.LOGIN)
		{
			return BoxLightStateType.INVALID;
		}
		if (mode == SceneMgr.Mode.TOURNAMENT)
		{
			return BoxLightStateType.TOURNAMENT;
		}
		if (mode == SceneMgr.Mode.COLLECTIONMANAGER)
		{
			return BoxLightStateType.COLLECTION;
		}
		if (mode == SceneMgr.Mode.TAVERN_BRAWL)
		{
			return BoxLightStateType.COLLECTION;
		}
		if (mode == SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			return BoxLightStateType.COLLECTION;
		}
		if (mode == SceneMgr.Mode.PACKOPENING)
		{
			return BoxLightStateType.PACK_OPENING;
		}
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			return BoxLightStateType.INVALID;
		}
		if (mode == SceneMgr.Mode.DRAFT)
		{
			return BoxLightStateType.FORGE;
		}
		if (mode == SceneMgr.Mode.ADVENTURE)
		{
			return BoxLightStateType.ADVENTURE;
		}
		if (mode == SceneMgr.Mode.FRIENDLY)
		{
			return BoxLightStateType.ADVENTURE;
		}
		if (mode == SceneMgr.Mode.BACON)
		{
			return BoxLightStateType.ADVENTURE;
		}
		if (mode == SceneMgr.Mode.GAME_MODE)
		{
			return BoxLightStateType.ADVENTURE;
		}
		if (mode == SceneMgr.Mode.PVP_DUNGEON_RUN)
		{
			return BoxLightStateType.ADVENTURE;
		}
		return BoxLightStateType.DEFAULT;
	}

	// Token: 0x06000BBB RID: 3003 RVA: 0x000448C4 File Offset: 0x00042AC4
	private void OnScenePreUnload(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.GAMEPLAY || mode == SceneMgr.Mode.STARTUP || mode == SceneMgr.Mode.RESET)
		{
			return;
		}
		if (prevMode == SceneMgr.Mode.HUB)
		{
			this.ChangeState(Box.State.LOADING);
			this.m_StoreButton.Unload();
			this.UnloadQuestLog();
		}
		else if (mode == SceneMgr.Mode.HUB)
		{
			this.ChangeStateToReflectSceneMode(mode, false);
			this.m_waitingForSceneLoad = true;
		}
		else if (this.ShouldUseLoadingHubState(mode, prevMode))
		{
			this.ChangeState(Box.State.LOADING_HUB);
		}
		else if (!SceneMgr.Get().IsDoingSceneDrivenTransition())
		{
			this.ChangeState(Box.State.LOADING);
		}
		this.ClearQueuedButtonFireEvent();
		this.UpdateUIEvents();
	}

	// Token: 0x06000BBC RID: 3004 RVA: 0x00044952 File Offset: 0x00042B52
	private bool ShouldUseLoadingHubState(SceneMgr.Mode mode, SceneMgr.Mode prevMode)
	{
		return (mode == SceneMgr.Mode.FRIENDLY && prevMode != SceneMgr.Mode.HUB) || (mode == SceneMgr.Mode.FIRESIDE_GATHERING && prevMode != SceneMgr.Mode.HUB) || (prevMode == SceneMgr.Mode.COLLECTIONMANAGER && (mode == SceneMgr.Mode.ADVENTURE || mode == SceneMgr.Mode.TOURNAMENT)) || (mode == SceneMgr.Mode.COLLECTIONMANAGER && (prevMode == SceneMgr.Mode.ADVENTURE || prevMode == SceneMgr.Mode.TOURNAMENT || prevMode == SceneMgr.Mode.FIRESIDE_GATHERING));
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x00044990 File Offset: 0x00042B90
	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (!TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL) && SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.STARTUP)
		{
			this.PlayTavernBrawlButtonActivation(false, true);
		}
		this.ChangeStateToReflectSceneMode(mode, true);
		if (this.m_waitingForSceneLoad)
		{
			this.m_waitingForSceneLoad = false;
			if (this.m_queuedButtonFire != null)
			{
				this.FireButtonPressEvent(this.m_queuedButtonFire.Value);
				this.m_queuedButtonFire = null;
			}
		}
	}

	// Token: 0x06000BBE RID: 3006 RVA: 0x00044A00 File Offset: 0x00042C00
	private void ChangeState_Startup()
	{
		this.m_state = Box.State.STARTUP;
		this.ChangeStateUsingConfig();
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x00044A0F File Offset: 0x00042C0F
	private void ChangeState_PressStart()
	{
		this.m_state = Box.State.PRESS_START;
		this.ChangeStateUsingConfig();
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x00044A1E File Offset: 0x00042C1E
	private void ChangeState_SetRotationLoading()
	{
		this.m_state = Box.State.SET_ROTATION_LOADING;
		this.ChangeStateUsingConfig();
	}

	// Token: 0x06000BC1 RID: 3009 RVA: 0x00044A2E File Offset: 0x00042C2E
	private void ChangeState_SetRotation()
	{
		this.m_state = Box.State.SET_ROTATION;
		this.ChangeStateUsingConfig();
	}

	// Token: 0x06000BC2 RID: 3010 RVA: 0x00044A3E File Offset: 0x00042C3E
	private void ChangeState_SetRotationOpen()
	{
		this.m_state = Box.State.SET_ROTATION_OPEN;
		base.StartCoroutine(this.SetRotationOpen_ChangeState());
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x00044A55 File Offset: 0x00042C55
	private void ChangeState_LoadingHub()
	{
		this.m_state = Box.State.LOADING_HUB;
		this.ChangeStateUsingConfig();
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x00044A64 File Offset: 0x00042C64
	private void ChangeState_Loading()
	{
		this.m_state = Box.State.LOADING;
		this.ChangeStateUsingConfig();
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x00044A73 File Offset: 0x00042C73
	private void ChangeState_Hub()
	{
		this.m_state = Box.State.HUB;
		this.UpdateUI(false);
		this.ChangeStateUsingConfig();
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x00044A89 File Offset: 0x00042C89
	private void ChangeState_HubWithDrawer()
	{
		this.m_state = Box.State.HUB_WITH_DRAWER;
		this.m_Camera.EnableAccelerometer();
		this.ChangeStateUsingConfig();
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x00044AA3 File Offset: 0x00042CA3
	private void ChangeState_Open()
	{
		this.m_state = Box.State.OPEN;
		this.ChangeStateUsingConfig();
	}

	// Token: 0x06000BC8 RID: 3016 RVA: 0x00044AB2 File Offset: 0x00042CB2
	private void ChangeState_Closed()
	{
		this.m_state = Box.State.CLOSED;
		this.ChangeStateUsingConfig();
	}

	// Token: 0x06000BC9 RID: 3017 RVA: 0x00044AC1 File Offset: 0x00042CC1
	private void ChangeState_Error()
	{
		this.m_state = Box.State.ERROR;
		this.ChangeStateUsingConfig();
	}

	// Token: 0x06000BCA RID: 3018 RVA: 0x00044AD1 File Offset: 0x00042CD1
	private void UpdateState_Startup()
	{
		this.m_state = Box.State.STARTUP;
		this.UpdateStateUsingConfig();
	}

	// Token: 0x06000BCB RID: 3019 RVA: 0x00044AE0 File Offset: 0x00042CE0
	private void UpdateState_PressStart()
	{
		this.m_state = Box.State.PRESS_START;
		this.UpdateStateUsingConfig();
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x00044AEF File Offset: 0x00042CEF
	private void UpdateState_SetRotationLoading()
	{
		this.m_state = Box.State.SET_ROTATION_LOADING;
		this.UpdateStateUsingConfig();
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x00044AFF File Offset: 0x00042CFF
	private void UpdateState_SetRotation()
	{
		this.m_state = Box.State.SET_ROTATION;
		this.UpdateStateUsingConfig();
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x00044B0F File Offset: 0x00042D0F
	private void UpdateState_SetRotationOpen()
	{
		this.m_state = Box.State.SET_ROTATION_OPEN;
		this.UpdateStateUsingConfig();
	}

	// Token: 0x06000BCF RID: 3023 RVA: 0x00044B1F File Offset: 0x00042D1F
	private void UpdateState_LoadingHub()
	{
		this.m_state = Box.State.LOADING_HUB;
		this.UpdateStateUsingConfig();
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x00044B2E File Offset: 0x00042D2E
	private void UpdateState_Loading()
	{
		this.m_state = Box.State.LOADING;
		this.UpdateStateUsingConfig();
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x00044B3D File Offset: 0x00042D3D
	private void UpdateState_Hub()
	{
		this.m_state = Box.State.HUB;
		this.UpdateUI(false);
		this.UpdateStateUsingConfig();
	}

	// Token: 0x06000BD2 RID: 3026 RVA: 0x00044B53 File Offset: 0x00042D53
	private void UpdateState_HubWithDrawer()
	{
		this.m_state = Box.State.HUB_WITH_DRAWER;
		this.m_Camera.EnableAccelerometer();
		this.UpdateStateUsingConfig();
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x00044B6D File Offset: 0x00042D6D
	private void UpdateState_Open()
	{
		this.m_state = Box.State.OPEN;
		this.UpdateStateUsingConfig();
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x00044B7C File Offset: 0x00042D7C
	private void UpdateState_Closed()
	{
		this.m_state = Box.State.CLOSED;
		this.UpdateStateUsingConfig();
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x00044B8B File Offset: 0x00042D8B
	private void UpdateState_Error()
	{
		this.m_state = Box.State.ERROR;
		this.UpdateStateUsingConfig();
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x00044B9C File Offset: 0x00042D9C
	private void ChangeStateUsingConfig()
	{
		Box.BoxStateConfig boxStateConfig = this.m_stateConfigs[(int)this.m_state];
		if (!boxStateConfig.m_logoState.m_ignore)
		{
			this.m_Logo.ChangeState(boxStateConfig.m_logoState.m_state);
		}
		if (!boxStateConfig.m_startButtonState.m_ignore)
		{
			this.m_StartButton.ChangeState(boxStateConfig.m_startButtonState.m_state);
		}
		if (!boxStateConfig.m_doorState.m_ignore)
		{
			this.m_LeftDoor.ChangeState(boxStateConfig.m_doorState.m_state);
			this.m_RightDoor.ChangeState(boxStateConfig.m_doorState.m_state);
		}
		if (!boxStateConfig.m_diskState.m_ignore)
		{
			this.m_Disk.ChangeState(boxStateConfig.m_diskState.m_state);
			this.CleanUpButtonsOnDiskStateChange();
		}
		if (!boxStateConfig.m_drawerState.m_ignore)
		{
			if (!UniversalInputManager.UsePhoneUI)
			{
				this.m_Drawer.ChangeState(boxStateConfig.m_drawerState.m_state);
			}
			else
			{
				bool flag = this.m_state == Box.State.HUB_WITH_DRAWER;
				if (!flag && flag != this.m_showRibbonButtons)
				{
					this.ToggleRibbonUI(flag);
				}
			}
		}
		if (!boxStateConfig.m_camState.m_ignore)
		{
			this.m_Camera.ChangeState(boxStateConfig.m_camState.m_state);
		}
		if (!boxStateConfig.m_fullScreenBlackState.m_ignore)
		{
			this.FullScreenBlack_ChangeState(boxStateConfig.m_fullScreenBlackState.m_state);
		}
		this.DoBoxSpecialEvents();
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x00044CFB File Offset: 0x00042EFB
	private void ToggleRibbonUI(bool show)
	{
		if (this.m_ribbonButtons == null)
		{
			return;
		}
		this.m_ribbonButtons.Toggle(show);
		this.m_showRibbonButtons = show;
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x00044D20 File Offset: 0x00042F20
	private void UpdateStateUsingConfig()
	{
		Box.BoxStateConfig boxStateConfig = this.m_stateConfigs[(int)this.m_state];
		if (!boxStateConfig.m_logoState.m_ignore)
		{
			this.m_Logo.UpdateState(boxStateConfig.m_logoState.m_state);
		}
		if (!boxStateConfig.m_startButtonState.m_ignore)
		{
			this.m_StartButton.UpdateState(boxStateConfig.m_startButtonState.m_state);
		}
		if (!boxStateConfig.m_doorState.m_ignore)
		{
			this.m_LeftDoor.ChangeState(boxStateConfig.m_doorState.m_state);
			this.m_RightDoor.ChangeState(boxStateConfig.m_doorState.m_state);
		}
		if (!boxStateConfig.m_diskState.m_ignore)
		{
			this.m_Disk.UpdateState(boxStateConfig.m_diskState.m_state);
		}
		this.m_TopSpinner.Reset();
		this.m_BottomSpinner.Reset();
		if (!boxStateConfig.m_drawerState.m_ignore)
		{
			this.m_Drawer.UpdateState(boxStateConfig.m_drawerState.m_state);
		}
		if (!boxStateConfig.m_camState.m_ignore)
		{
			this.m_Camera.UpdateState(boxStateConfig.m_camState.m_state);
		}
		if (!boxStateConfig.m_fullScreenBlackState.m_ignore)
		{
			this.FullScreenBlack_UpdateState(boxStateConfig.m_fullScreenBlackState.m_state);
		}
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x00044E59 File Offset: 0x00043059
	private void FullScreenBlack_ChangeState(bool enable)
	{
		this.FullScreenBlack_UpdateState(enable);
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x00044E64 File Offset: 0x00043064
	private void FullScreenBlack_UpdateState(bool enable)
	{
		FullScreenEffects activeCameraFullScreenEffects = FullScreenFXMgr.Get().ActiveCameraFullScreenEffects;
		if (activeCameraFullScreenEffects == null)
		{
			return;
		}
		activeCameraFullScreenEffects.BlendToColorEnable = enable;
		if (!enable)
		{
			return;
		}
		activeCameraFullScreenEffects.BlendColor = Color.black;
		activeCameraFullScreenEffects.BlendToColorAmount = 1f;
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x00044EA8 File Offset: 0x000430A8
	private void FireTransitionFinishedEvent()
	{
		Box.TransitionFinishedListener[] array = this.m_transitionFinishedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x00044ED8 File Offset: 0x000430D8
	private void InitializeUI()
	{
		PegUI.Get().AddInputCamera(this.m_Camera.GetComponent<Camera>());
		this.m_boxWidgetRef.RegisterReadyListener<Widget>(new Action<Widget>(this.BoxWidgetIsReady));
		this.m_mainShopWidgetRef.RegisterReadyListener<Widget>(new Action<Widget>(this.MainShopWidgetIsReady));
		this.m_StartButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnStartButtonPressed));
		InputScheme inputScheme = InputUtil.GetInputScheme();
		if (inputScheme == InputScheme.TOUCH)
		{
			this.m_StartButton.SetText(GameStrings.Get("GLUE_START_TOUCH"));
		}
		else if (inputScheme == InputScheme.GAMEPAD)
		{
			this.m_StartButton.SetText(GameStrings.Get("GLUE_START_PRESS"));
		}
		else if (inputScheme == InputScheme.KEYBOARD_MOUSE)
		{
			this.m_StartButton.SetText(GameStrings.Get("GLUE_START_CLICK"));
		}
		this.m_TournamentButton.SetText(GameStrings.Get("GLUE_TOURNAMENT"));
		this.m_SoloAdventuresButton.SetText(GameStrings.Get("GLUE_ADVENTURE"));
		this.m_TavernBrawlButton.SetText(GameStrings.Get("GLOBAL_TAVERN_BRAWL"));
		this.m_GameModesButton.SetText(GameStrings.Get("GLUE_GAME_MODES"));
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_Drawer.gameObject.SetActive(false);
			this.m_ribbonButtons.m_collectionManagerRibbon.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCollectionButtonPressed));
			this.m_ribbonButtons.m_packOpeningRibbon.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnOpenPacksButtonPressed));
			this.m_ribbonButtons.m_questLogRibbon.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnQuestButtonPressed));
			this.m_ribbonButtons.m_storeRibbon.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnStoreButtonReleased));
		}
		else
		{
			this.m_OpenPacksButton.SetText(GameStrings.Get("GLUE_OPEN_PACKS"));
			this.m_CollectionButton.SetText(GameStrings.Get("GLUE_MY_COLLECTION"));
			this.m_QuestLogButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnQuestButtonPressed));
			this.m_StoreButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnStoreButtonReleased));
		}
		this.RegisterButtonEvents(this.m_TournamentButton);
		this.RegisterButtonEvents(this.m_SoloAdventuresButton);
		this.RegisterButtonEvents(this.m_GameModesButton);
		this.RegisterButtonEvents(this.m_TavernBrawlButton);
		this.RegisterButtonEvents(this.m_OpenPacksButton);
		this.RegisterButtonEvents(this.m_CollectionButton);
		this.UpdateUI(true);
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x0004512E File Offset: 0x0004332E
	public void UpdateUI(bool isInitialization = false)
	{
		this.UpdateUIState(isInitialization);
		this.UpdateUIEvents();
	}

	// Token: 0x06000BDE RID: 3038 RVA: 0x0004513D File Offset: 0x0004333D
	private void TavernBrawl_UpdateUI()
	{
		this.UpdateUI(false);
	}

	// Token: 0x06000BDF RID: 3039 RVA: 0x00045148 File Offset: 0x00043348
	private void UpdateUIState(bool isInitialization)
	{
		if (this.m_waitingForNetData)
		{
			this.SetPackCount(-1);
			this.HighlightButton(this.m_OpenPacksButton, false);
			this.HighlightButton(this.m_TournamentButton, false);
			this.HighlightButton(this.m_SoloAdventuresButton, false);
			this.HighlightButton(this.m_CollectionButton, false);
			this.HighlightButton(this.m_GameModesButton, false);
			this.HighlightButton(this.m_TavernBrawlButton, false);
			return;
		}
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		if (DemoMgr.Get().GetMode() == DemoMode.BLIZZCON_2013)
		{
			netObject.Games.Practice = false;
			netObject.Games.Tournament = false;
		}
		int num = this.ComputeBoosterCount();
		this.SetPackCount(num);
		bool highlightOn = num > 0 && !Options.Get().GetBool(Option.HAS_SEEN_PACK_OPENING, false);
		this.HighlightButton(this.m_OpenPacksButton, highlightOn);
		bool flag = false;
		bool highlightOn2 = false;
		if (netObject.Games.Practice && Options.Get().GetBool(Option.BUNDLE_JUST_PURCHASE_IN_HUB, false))
		{
			flag = true;
		}
		else if (netObject.Games.Practice && !Options.Get().GetBool(Option.HAS_SEEN_PRACTICE_MODE, false))
		{
			flag = true;
		}
		else if (netObject.Games.Tournament && AchieveManager.Get() != null && AchieveManager.Get().HasActiveQuestId(AchievementDbId.FIRST_BLOOD))
		{
			highlightOn2 = true;
		}
		else if (!flag && netObject.Games.Practice && this.m_boxVisualController != null)
		{
			string state = (AdventureConfig.GetAdventurePlayerShouldSee() != AdventureDbId.INVALID) ? "NewAdventureOn" : "NewAdventureOff";
			this.m_boxVisualController.SetState(state);
		}
		if (!SpecialEventManager.Get().IsEventActive("battlegrounds_early_access", false) && this.m_boxVisualController != null)
		{
			string state2 = (!Options.Get().GetBool(Option.HAS_SEEN_BATTLEGROUNDS_BOX_BUTTON, false)) ? "NewGameModeOn" : "NewGameModeOff";
			this.m_boxVisualController.SetState(state2);
		}
		this.HighlightButton(this.m_TournamentButton, highlightOn2);
		this.ToggleButtonTextureState(netObject.Games.Tournament, this.m_TournamentButton);
		this.HighlightButton(this.m_SoloAdventuresButton, flag);
		this.ToggleButtonTextureState(netObject.Games.Practice, this.m_SoloAdventuresButton);
		bool highlightOn3 = !flag && netObject.Collection.Manager && !Options.Get().GetBool(Option.HAS_SEEN_COLLECTIONMANAGER_AFTER_PRACTICE, false);
		this.HighlightButton(this.m_CollectionButton, highlightOn3);
		this.ToggleDrawerButtonState(netObject.Collection.Manager, this.m_CollectionButton);
		this.UpdateTavernBrawlButtonState(true);
	}

	// Token: 0x06000BE0 RID: 3040 RVA: 0x000453BB File Offset: 0x000435BB
	private void BoxWidgetIsReady(Widget widget)
	{
		this.m_boxVisualController = widget.FindWidgetComponent<VisualController>(Array.Empty<string>());
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void MainShopWidgetIsReady(Widget widget)
	{
	}

	// Token: 0x06000BE2 RID: 3042 RVA: 0x000453CE File Offset: 0x000435CE
	private bool IsCollectionReady()
	{
		return CollectionManager.Get() != null && CollectionManager.Get().IsFullyLoaded();
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x000453E3 File Offset: 0x000435E3
	private IEnumerator UpdateUIWhenCollectionReady()
	{
		while (!this.IsCollectionReady())
		{
			yield return null;
		}
		this.UpdateUI(false);
		yield break;
	}

	// Token: 0x06000BE4 RID: 3044 RVA: 0x000453F2 File Offset: 0x000435F2
	private void OnFSGSignShown()
	{
		this.m_showFSGBanner = true;
		this.UpdateTavernBrawlButtonState(true);
	}

	// Token: 0x06000BE5 RID: 3045 RVA: 0x00045403 File Offset: 0x00043603
	private void OnLeaveFSG(FSGConfig fsg)
	{
		this.UpdateTavernBrawlButtonState(true);
	}

	// Token: 0x06000BE6 RID: 3046 RVA: 0x00045410 File Offset: 0x00043610
	private void DoTavernBrawlButtonInitialization()
	{
		TavernBrawlManager tavernBrawlManager = TavernBrawlManager.Get();
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		if (netObject == null)
		{
			NetCache.Get().RegisterUpdatedListener(typeof(NetCache.NetCacheFeatures), new Action(this.DoTavernBrawlButtonInitialization));
			return;
		}
		NetCache.Get().RemoveUpdatedListener(typeof(NetCache.NetCacheFeatures), new Action(this.DoTavernBrawlButtonInitialization));
		if (NetCache.Get().GetNetObject<NetCache.NetCacheHeroLevels>() == null)
		{
			NetCache.Get().RegisterUpdatedListener(typeof(NetCache.NetCacheHeroLevels), new Action(this.DoTavernBrawlButtonInitialization));
			return;
		}
		NetCache.Get().RemoveUpdatedListener(typeof(NetCache.NetCacheHeroLevels), new Action(this.DoTavernBrawlButtonInitialization));
		if (netObject == null || !netObject.Games.TavernBrawl || !tavernBrawlManager.HasUnlockedTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL))
		{
			return;
		}
		if (tavernBrawlManager.IsCurrentBrawlInfoReady)
		{
			tavernBrawlManager.OnTavernBrawlUpdated -= this.DoTavernBrawlButtonInitialization;
		}
		if (!TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL) || tavernBrawlManager.IsFirstTimeSeeingCurrentSeason)
		{
			this.PlayTavernBrawlButtonActivation(false, true);
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x00045513 File Offset: 0x00043713
	// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x0004551B File Offset: 0x0004371B
	public bool IsTavernBrawlButtonDeactivated { get; private set; }

	// Token: 0x06000BE9 RID: 3049 RVA: 0x00045524 File Offset: 0x00043724
	public void PlayTavernBrawlButtonActivation(bool activate, bool isInitialization = false)
	{
		Animator component = this.m_TavernBrawlButtonVisual.GetComponent<Animator>();
		component.StopPlayback();
		if (activate)
		{
			component.Play("TavernBrawl_ButtonActivate");
			if (!isInitialization)
			{
				this.m_TavernBrawlButtonActivateFX.GetComponent<ParticleSystem>().Play();
			}
		}
		else
		{
			if (!isInitialization)
			{
				this.m_TavernBrawlButton.ClearHighlightAndTooltip();
			}
			component.Play("TavernBrawl_ButtonDeactivate");
			if (!isInitialization)
			{
				this.m_TavernBrawlButtonDeactivateFX.GetComponent<ParticleSystem>().Play();
			}
		}
		this.IsTavernBrawlButtonDeactivated = !activate;
	}

	// Token: 0x06000BEA RID: 3050 RVA: 0x0004559C File Offset: 0x0004379C
	private void CleanUpButtonsOnDiskStateChange()
	{
		if (!this.IsTavernBrawlButtonDeactivated)
		{
			Animator component = this.m_TavernBrawlButtonVisual.GetComponent<Animator>();
			component.StopPlayback();
			component.Play("TavernBrawl_ButtonIdle");
		}
	}

	// Token: 0x06000BEB RID: 3051 RVA: 0x000455C4 File Offset: 0x000437C4
	public bool UpdateTavernBrawlButtonState(bool highlightAllowed)
	{
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		TavernBrawlManager tavernBrawlManager = TavernBrawlManager.Get();
		bool flag = tavernBrawlManager.IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
		bool flag2 = netObject != null && netObject.Games.TavernBrawl && flag && tavernBrawlManager.HasUnlockedTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
		bool highlightOn = highlightAllowed && flag2 && tavernBrawlManager.IsFirstTimeSeeingCurrentSeason && !this.IsTavernBrawlButtonDeactivated && !this.IsButtonHighlighted(this.m_TournamentButton) && !this.IsButtonHighlighted(this.m_SoloAdventuresButton) && !this.IsButtonHighlighted(this.m_GameModesButton);
		this.HighlightButton(this.m_TavernBrawlButton, highlightOn);
		this.ToggleButtonTextureState(flag2, this.m_TavernBrawlButton);
		if (!flag2)
		{
			this.m_TavernBrawlButton.ClearHighlightAndTooltip();
		}
		return flag2;
	}

	// Token: 0x06000BEC RID: 3052 RVA: 0x00045679 File Offset: 0x00043879
	public void ActivateFiresideBrawlButton(bool isCheckedIn)
	{
		if (isCheckedIn && !this.m_showFSGBanner)
		{
			return;
		}
		if (this.m_firesideGatheringTavernBrawlButtonFlag != null)
		{
			this.m_firesideGatheringTavernBrawlButtonFlag.SetActive(isCheckedIn);
		}
		this.m_showFSGBanner = false;
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x000456A8 File Offset: 0x000438A8
	private void UpdateUIEvents()
	{
		object obj = SetRotationManager.ShouldShowSetRotationIntro() || this.IsTutorial() || DemoMgr.Get().IsDemo() || this.m_state == Box.State.LOADING_HUB;
		if (this.CanEnableUIEvents() && this.m_state == Box.State.PRESS_START)
		{
			this.EnableButton(this.m_StartButton);
		}
		else
		{
			this.DisableButton(this.m_StartButton);
		}
		NetCache.NetCacheFeatures netCacheFeatures = null;
		if (!this.m_waitingForNetData)
		{
			netCacheFeatures = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		}
		object obj2 = obj;
		if (obj2 == null)
		{
			this.m_StoreButton.gameObject.SetActive(true);
			if (netCacheFeatures != null)
			{
				if (netCacheFeatures.ProgressionEnabled)
				{
					if (!netCacheFeatures.JournalButtonDisabled)
					{
						this.m_journalButtonWidget.Show();
					}
				}
				else
				{
					this.m_QuestLogButton.gameObject.SetActive(true);
				}
			}
		}
		if (this.CanEnableUIEvents() && (this.m_state == Box.State.HUB || this.m_state == Box.State.HUB_WITH_DRAWER))
		{
			if (this.m_waitingForNetData || !this.IsCollectionReady())
			{
				this.ToggleButtonTextureState(false, this.m_TournamentButton);
				this.DisableButton(this.m_TournamentButton);
			}
			else
			{
				this.ToggleButtonTextureState(true, this.m_TournamentButton);
				this.EnableButton(this.m_TournamentButton);
			}
			if (this.m_waitingForNetData)
			{
				this.DisableButton(this.m_SoloAdventuresButton);
				this.DisableButton(this.m_GameModesButton);
				this.DisableButton(this.m_TavernBrawlButton);
				this.DisableButton(this.m_QuestLogButton);
				this.DisableButton(this.m_StoreButton);
				this.m_journalButtonWidget.TriggerEvent("DISABLE_INTERACTION", default(Widget.TriggerEventParameters));
			}
			else
			{
				this.EnableButton(this.m_SoloAdventuresButton);
				this.EnableButton(this.m_GameModesButton);
				this.EnableButton(this.m_TavernBrawlButton);
				this.EnableButton(this.m_StoreButton);
				this.EnableButton(this.m_QuestLogButton);
				this.m_journalButtonWidget.TriggerEvent("ENABLE_INTERACTION", default(Widget.TriggerEventParameters));
			}
			if (this.m_state == Box.State.HUB_WITH_DRAWER)
			{
				if (this.m_waitingForNetData)
				{
					this.DisableButton(this.m_OpenPacksButton);
					this.DisableButton(this.m_CollectionButton);
				}
				else
				{
					this.EnableButton(this.m_OpenPacksButton);
					this.EnableButton(this.m_CollectionButton);
				}
			}
			else
			{
				this.DisableButton(this.m_OpenPacksButton);
				this.DisableButton(this.m_CollectionButton);
			}
		}
		else
		{
			this.DisableButton(this.m_TournamentButton);
			this.DisableButton(this.m_SoloAdventuresButton);
			this.DisableButton(this.m_GameModesButton);
			this.DisableButton(this.m_TavernBrawlButton);
			this.DisableButton(this.m_OpenPacksButton);
			this.DisableButton(this.m_CollectionButton);
			this.DisableButton(this.m_QuestLogButton);
			this.DisableButton(this.m_StoreButton);
			this.m_journalButtonWidget.TriggerEvent("DISABLE_INTERACTION", default(Widget.TriggerEventParameters));
		}
		if (DemoMgr.Get().GetMode() == DemoMode.BLIZZCON_2019_BATTLEGROUNDS)
		{
			this.DisableButton(this.m_TournamentButton);
			this.DisableButton(this.m_SoloAdventuresButton);
			this.DisableButton(this.m_OpenPacksButton);
			this.DisableButton(this.m_CollectionButton);
			this.DisableButton(this.m_QuestLogButton);
			this.DisableButton(this.m_StoreButton);
			this.DisableButton(this.m_TavernBrawlButton);
			this.m_journalButtonWidget.TriggerEvent("DISABLE_INTERACTION", default(Widget.TriggerEventParameters));
		}
		if (obj2 != null)
		{
			this.m_StoreButton.gameObject.SetActive(false);
			this.m_QuestLogButton.gameObject.SetActive(false);
			this.m_journalButtonWidget.Hide();
		}
		if (netCacheFeatures != null && netCacheFeatures.JournalButtonDisabled)
		{
			this.m_journalButtonWidget.Hide();
			this.m_journalButtonWidget.TriggerEvent("DISABLE_INTERACTION", default(Widget.TriggerEventParameters));
		}
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x00045A38 File Offset: 0x00043C38
	public void DisableAllButtons()
	{
		this.DisableButton(this.m_TournamentButton);
		this.DisableButton(this.m_SoloAdventuresButton);
		this.DisableButton(this.m_GameModesButton);
		this.DisableButton(this.m_TavernBrawlButton);
		this.DisableButton(this.m_setRotationButton);
		if (UniversalInputManager.UsePhoneUI)
		{
			this.DisableButton(this.m_ribbonButtons.m_collectionManagerRibbon);
			this.DisableButton(this.m_ribbonButtons.m_packOpeningRibbon);
			this.DisableButton(this.m_ribbonButtons.m_questLogRibbon);
			this.DisableButton(this.m_ribbonButtons.m_storeRibbon);
		}
		else
		{
			this.DisableButton(this.m_OpenPacksButton);
			this.DisableButton(this.m_CollectionButton);
			this.DisableButton(this.m_QuestLogButton);
			this.DisableButton(this.m_StoreButton);
			this.m_journalButtonWidget.TriggerEvent("DISABLE_INTERACTION", default(Widget.TriggerEventParameters));
		}
		this.ToggleButtonTextureState(false, this.m_TournamentButton);
		this.ToggleButtonTextureState(false, this.m_SoloAdventuresButton);
		this.ToggleButtonTextureState(false, this.m_GameModesButton);
		this.ToggleButtonTextureState(false, this.m_TavernBrawlButton);
		this.ToggleDrawerButtonState(false, this.m_OpenPacksButton);
		this.ToggleDrawerButtonState(false, this.m_CollectionButton);
		this.ToggleButtonTextureState(false, this.m_setRotationButton);
	}

	// Token: 0x06000BEF RID: 3055 RVA: 0x00045B78 File Offset: 0x00043D78
	private bool CanEnableUIEvents()
	{
		return !this.HasPendingEffects() && this.m_stateQueue.Count <= 0 && this.m_state != Box.State.INVALID && this.m_state != Box.State.STARTUP && this.m_state != Box.State.LOADING && this.m_state != Box.State.LOADING_HUB && this.m_state != Box.State.OPEN;
	}

	// Token: 0x06000BF0 RID: 3056 RVA: 0x00045BD6 File Offset: 0x00043DD6
	private void RegisterButtonEvents(PegUIElement button)
	{
		button.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnButtonPressed));
		button.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnButtonMouseOver));
		button.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnButtonMouseOut));
	}

	// Token: 0x06000BF1 RID: 3057 RVA: 0x00045C14 File Offset: 0x00043E14
	private void ToggleButtonTextureState(bool enabled, BoxMenuButton button)
	{
		if (button == null)
		{
			return;
		}
		if (enabled)
		{
			button.m_TextMesh.TextColor = this.m_EnabledMaterial;
			return;
		}
		button.m_TextMesh.TextColor = this.m_DisabledMaterial;
	}

	// Token: 0x06000BF2 RID: 3058 RVA: 0x00045C46 File Offset: 0x00043E46
	private void ToggleDrawerButtonState(bool enabled, BoxMenuButton button)
	{
		if (button == null)
		{
			return;
		}
		if (enabled)
		{
			button.m_TextMesh.TextColor = this.m_EnabledDrawerMaterial;
			return;
		}
		button.m_TextMesh.TextColor = this.m_DisabledDrawerMaterial;
	}

	// Token: 0x06000BF3 RID: 3059 RVA: 0x00045C78 File Offset: 0x00043E78
	private void HighlightButton(BoxMenuButton button, bool highlightOn)
	{
		if (button.m_HighlightState == null)
		{
			Debug.LogWarning(string.Format("Box.HighlighButton {0} - highlight state is null", button));
			return;
		}
		ActorStateType stateType = highlightOn ? ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE : ActorStateType.HIGHLIGHT_OFF;
		button.m_HighlightState.ChangeState(stateType);
	}

	// Token: 0x06000BF4 RID: 3060 RVA: 0x00045CBB File Offset: 0x00043EBB
	private bool IsButtonHighlighted(BoxMenuButton button)
	{
		return button.m_HighlightState.CurrentState == ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE;
	}

	// Token: 0x06000BF5 RID: 3061 RVA: 0x00045CCC File Offset: 0x00043ECC
	private void SetPackCount(int n)
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_ribbonButtons.SetPackCount(n);
			return;
		}
		this.m_OpenPacksButton.SetPackCount(n);
	}

	// Token: 0x06000BF6 RID: 3062 RVA: 0x00045CF4 File Offset: 0x00043EF4
	public void EnableButton(PegUIElement button)
	{
		button.SetEnabled(true, false);
		PegUIElement ribbonButtonFromButton = this.GetRibbonButtonFromButton(button);
		if (ribbonButtonFromButton != null && ribbonButtonFromButton != button)
		{
			this.EnableButton(ribbonButtonFromButton);
		}
	}

	// Token: 0x06000BF7 RID: 3063 RVA: 0x00045D2C File Offset: 0x00043F2C
	public void DisableButton(PegUIElement button)
	{
		if (button == null)
		{
			return;
		}
		button.SetEnabled(false, false);
		TooltipZone component = button.GetComponent<TooltipZone>();
		if (component != null)
		{
			component.HideTooltip();
		}
		PegUIElement ribbonButtonFromButton = this.GetRibbonButtonFromButton(button);
		if (ribbonButtonFromButton != null && ribbonButtonFromButton != button)
		{
			this.DisableButton(ribbonButtonFromButton);
		}
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x00045D84 File Offset: 0x00043F84
	private PegUIElement GetRibbonButtonFromButton(PegUIElement button)
	{
		if (button == null || this.m_ribbonButtons == null)
		{
			return null;
		}
		if (button == this.m_CollectionButton)
		{
			return this.m_ribbonButtons.m_collectionManagerRibbon;
		}
		if (button == this.m_QuestLogButton)
		{
			return this.m_ribbonButtons.m_questLogRibbon;
		}
		if (button == this.m_OpenPacksButton)
		{
			return this.m_ribbonButtons.m_packOpeningRibbon;
		}
		if (button == this.m_StoreButton)
		{
			return this.m_ribbonButtons.m_storeRibbon;
		}
		return null;
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x00045E14 File Offset: 0x00044014
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
			flag4 = (netObject.Games.TavernBrawl && TavernBrawlManager.Get().HasUnlockedAnyTavernBrawl);
		}
		BoxMenuButton x = (BoxMenuButton)element;
		if (x == this.m_StartButton)
		{
			this.OnStartButtonPressed(e);
			return;
		}
		if (x == this.m_TournamentButton && flag)
		{
			this.OnTournamentButtonPressed(e);
			return;
		}
		if (x == this.m_SoloAdventuresButton && flag2)
		{
			this.OnSoloAdventuresButtonPressed(e);
			return;
		}
		if (x == this.m_GameModesButton)
		{
			this.OnGameModesButtonPressed(e);
			return;
		}
		if (x == this.m_TavernBrawlButton && flag4)
		{
			this.OnTavernBrawlButtonPressed(e);
			return;
		}
		if (x == this.m_OpenPacksButton)
		{
			this.OnOpenPacksButtonPressed(e);
			return;
		}
		if (x == this.m_CollectionButton && flag3)
		{
			this.OnCollectionButtonPressed(e);
			return;
		}
		if (x == this.m_setRotationButton)
		{
			this.OnSetRotationButtonPressed(e);
		}
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x00045F48 File Offset: 0x00044148
	private void ShowReconnectPopup(UIEvent e, Box.ButtonPressFunction onButtonPressed)
	{
		DialogManager.Get().ShowReconnectHelperDialog(delegate
		{
			if (onButtonPressed != null)
			{
				onButtonPressed(e);
			}
		}, null);
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x00045F80 File Offset: 0x00044180
	private void FireButtonPressEvent(Box.ButtonType buttonType)
	{
		if (this.m_waitingForSceneLoad)
		{
			this.m_queuedButtonFire = new Box.ButtonType?(buttonType);
			return;
		}
		Box.ButtonPressListener[] array = this.m_buttonPressListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(buttonType);
		}
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x00045FC5 File Offset: 0x000441C5
	private void ClearQueuedButtonFireEvent()
	{
		this.m_queuedButtonFire = null;
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x00045FD3 File Offset: 0x000441D3
	private void OnStartButtonPressed(UIEvent e)
	{
		if (!HearthstoneServices.IsAvailable<SceneMgr>())
		{
			this.ChangeState(Box.State.HUB);
			return;
		}
		this.FireButtonPressEvent(Box.ButtonType.START);
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x00045FEC File Offset: 0x000441EC
	private void OnTournamentButtonPressed(UIEvent e)
	{
		if (!HearthstoneServices.IsAvailable<SceneMgr>())
		{
			this.ChangeState(Box.State.OPEN);
			return;
		}
		if (!Options.Get().HasOption(Option.HAS_CLICKED_TOURNAMENT))
		{
			Options.Get().SetBool(Option.HAS_CLICKED_TOURNAMENT, true);
		}
		AchieveManager.Get().NotifyOfClick(global::Achievement.ClickTriggerType.BUTTON_PLAY);
		this.FireButtonPressEvent(Box.ButtonType.TOURNAMENT);
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x0004603C File Offset: 0x0004423C
	private void OnSetRotationButtonPressed(UIEvent e)
	{
		Log.Box.Print("Set Rotation Button Pressed!", Array.Empty<object>());
		if (!HearthstoneServices.IsAvailable<SceneMgr>())
		{
			this.ChangeState(Box.State.SET_ROTATION_OPEN);
			return;
		}
		if (!Options.Get().HasOption(Option.HAS_CLICKED_TOURNAMENT))
		{
			Options.Get().SetBool(Option.HAS_CLICKED_TOURNAMENT, true);
		}
		AchieveManager.Get().NotifyOfClick(global::Achievement.ClickTriggerType.BUTTON_PLAY);
		this.FireButtonPressEvent(Box.ButtonType.SET_ROTATION);
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x000460A1 File Offset: 0x000442A1
	private void OnSoloAdventuresButtonPressed(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			this.ShowReconnectPopup(e, new Box.ButtonPressFunction(this.OnSoloAdventuresButtonPressed));
			return;
		}
		if (!HearthstoneServices.IsAvailable<SceneMgr>())
		{
			this.ChangeState(Box.State.OPEN);
			return;
		}
		this.FireButtonPressEvent(Box.ButtonType.ADVENTURE);
	}

	// Token: 0x06000C01 RID: 3073 RVA: 0x000460D5 File Offset: 0x000442D5
	private void OnGameModesButtonPressed(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			this.ShowReconnectPopup(e, new Box.ButtonPressFunction(this.OnGameModesButtonPressed));
			return;
		}
		if (SceneMgr.Get() != null && !DialogManager.Get().ShowingDialog())
		{
			this.FireButtonPressEvent(Box.ButtonType.GAME_MODES);
		}
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x0004610D File Offset: 0x0004430D
	private void OnForgeButtonPressed(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			this.ShowReconnectPopup(e, new Box.ButtonPressFunction(this.OnForgeButtonPressed));
			return;
		}
		if (!HearthstoneServices.IsAvailable<SceneMgr>())
		{
			this.ChangeState(Box.State.OPEN);
			return;
		}
		AchieveManager.Get().NotifyOfClick(global::Achievement.ClickTriggerType.BUTTON_ARENA);
		this.FireButtonPressEvent(Box.ButtonType.FORGE);
	}

	// Token: 0x06000C03 RID: 3075 RVA: 0x0004614C File Offset: 0x0004434C
	private void OnBaconButtonPressed(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			this.ShowReconnectPopup(e, new Box.ButtonPressFunction(this.OnBaconButtonPressed));
			return;
		}
		if (SceneMgr.Get() == null)
		{
			this.ChangeState(Box.State.OPEN);
			return;
		}
		this.FireButtonPressEvent(Box.ButtonType.BACON);
	}

	// Token: 0x06000C04 RID: 3076 RVA: 0x00046181 File Offset: 0x00044381
	private void OnPvPDungeonRunButtonPressed(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			this.ShowReconnectPopup(e, new Box.ButtonPressFunction(this.OnBaconButtonPressed));
			return;
		}
		if (!HearthstoneServices.IsAvailable<SceneMgr>())
		{
			this.ChangeState(Box.State.OPEN);
			return;
		}
		this.FireButtonPressEvent(Box.ButtonType.PVP_DUNGEON_RUN);
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x000461B6 File Offset: 0x000443B6
	private void OnTavernBrawlButtonPressed(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			this.ShowReconnectPopup(e, new Box.ButtonPressFunction(this.OnTavernBrawlButtonPressed));
			return;
		}
		if (!HearthstoneServices.IsAvailable<SceneMgr>())
		{
			this.ChangeState(Box.State.OPEN);
			return;
		}
		this.PlayTavernBrawlCrowdSFX();
		this.FireButtonPressEvent(Box.ButtonType.TAVERN_BRAWL);
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x000461F0 File Offset: 0x000443F0
	private void OnOpenPacksButtonPressed(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			this.ShowReconnectPopup(e, new Box.ButtonPressFunction(this.OnOpenPacksButtonPressed));
			return;
		}
		if (!HearthstoneServices.IsAvailable<SceneMgr>())
		{
			this.ChangeState(Box.State.OPEN);
			return;
		}
		this.FireButtonPressEvent(Box.ButtonType.OPEN_PACKS);
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x00046224 File Offset: 0x00044424
	public void OnCollectionButtonPressed(UIEvent e)
	{
		if (!HearthstoneServices.IsAvailable<SceneMgr>())
		{
			this.ChangeState(Box.State.OPEN);
			return;
		}
		this.FireButtonPressEvent(Box.ButtonType.COLLECTION);
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x00046240 File Offset: 0x00044440
	public void OnQuestButtonPressed(UIEvent e)
	{
		if (QuestManager.Get().IsSystemEnabled)
		{
			JournalButton componentInChildren = this.m_ribbonButtons.m_journalButtonWidget.GetComponentInChildren<JournalButton>();
			if (componentInChildren == null)
			{
				return;
			}
			componentInChildren.OnClicked();
			return;
		}
		else
		{
			if (!Network.IsLoggedIn())
			{
				this.ShowReconnectPopup(e, new Box.ButtonPressFunction(this.OnQuestButtonPressed));
				return;
			}
			if (ShownUIMgr.Get().HasShownUI())
			{
				return;
			}
			this.FireButtonPressEvent(Box.ButtonType.QUEST_LOG);
			ShownUIMgr.Get().SetShownUI(ShownUIMgr.UI_WINDOW.QUEST_LOG);
			SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681", base.gameObject);
			this.m_tempInputBlocker = CameraUtils.CreateInputBlocker(Box.Get().GetCamera(), "QuestLogInputBlocker", null, 30f);
			SceneUtils.SetLayer(this.m_tempInputBlocker, GameLayer.IgnoreFullScreenEffects);
			this.m_tempInputBlocker.AddComponent<PegUIElement>();
			base.StopCoroutine("ShowQuestLogWhenReady");
			base.StartCoroutine("ShowQuestLogWhenReady");
			return;
		}
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x00046320 File Offset: 0x00044520
	private void OnButtonMouseOver(UIEvent e)
	{
		TooltipZone component = e.GetElement().gameObject.GetComponent<TooltipZone>();
		if (component == null)
		{
			return;
		}
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
		if (component.targetObject == this.m_TournamentButton.gameObject && flag)
		{
			bodytext = GameStrings.Get("GLUE_TOOLTIP_BUTTON_TOURNAMENT_DESC");
		}
		else if (component.targetObject == this.m_SoloAdventuresButton.gameObject && flag2)
		{
			bodytext = GameStrings.Get("GLUE_TOOLTIP_BUTTON_ADVENTURE_DESC");
		}
		else if (component.targetObject == this.m_GameModesButton.gameObject)
		{
			bodytext = GameStrings.Get("GLUE_TOOLTIP_BUTTON_GAME_MODES_DESC");
		}
		else if (component.targetObject == this.m_TavernBrawlButton.gameObject)
		{
			if (netObject != null && netObject.Games.TavernBrawl)
			{
				if (!TavernBrawlManager.Get().HasUnlockedTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL))
				{
					bodytext = GameStrings.Get("GLUE_TOOLTIP_BUTTON_TAVERN_BRAWL_NOT_UNLOCKED");
				}
				else
				{
					bodytext = GameStrings.Get("GLUE_TOOLTIP_BUTTON_TAVERN_BRAWL_DESC");
				}
			}
			else
			{
				bodytext = text;
			}
		}
		else if (component.targetObject == this.m_OpenPacksButton.gameObject)
		{
			bodytext = GameStrings.Get("GLUE_TOOLTIP_BUTTON_PACKOPEN_DESC");
		}
		else if (component.targetObject == this.m_CollectionButton.gameObject && flag3)
		{
			bodytext = GameStrings.Get("GLUE_TOOLTIP_BUTTON_COLLECTION_DESC");
		}
		if (component.targetObject == this.m_TournamentButton.gameObject)
		{
			component.ShowBoxTooltip(GameStrings.Get("GLUE_TOOLTIP_BUTTON_TOURNAMENT_HEADLINE"), bodytext, 0);
			return;
		}
		if (component.targetObject == this.m_SoloAdventuresButton.gameObject)
		{
			component.ShowBoxTooltip(GameStrings.Get("GLUE_TOOLTIP_BUTTON_ADVENTURE_HEADLINE"), bodytext, 0);
			return;
		}
		if (component.targetObject == this.m_GameModesButton.gameObject)
		{
			component.ShowBoxTooltip(GameStrings.Get("GLUE_TOOLTIP_BUTTON_GAME_MODES_HEADLINE"), bodytext, 0);
			return;
		}
		if (component.targetObject == this.m_TavernBrawlButton.gameObject)
		{
			component.ShowBoxTooltip(GameStrings.Get("GLUE_TOOLTIP_BUTTON_TAVERN_BRAWL_HEADLINE"), bodytext, 0);
			return;
		}
		if (component.targetObject == this.m_OpenPacksButton.gameObject)
		{
			component.ShowBoxTooltip(GameStrings.Get("GLUE_TOOLTIP_BUTTON_PACKOPEN_HEADLINE"), bodytext, 0);
			return;
		}
		if (component.targetObject == this.m_CollectionButton.gameObject)
		{
			component.ShowBoxTooltip(GameStrings.Get("GLUE_TOOLTIP_BUTTON_COLLECTION_HEADLINE"), bodytext, 0);
		}
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x000465B0 File Offset: 0x000447B0
	private void OnButtonMouseOut(UIEvent e)
	{
		TooltipZone component = e.GetElement().gameObject.GetComponent<TooltipZone>();
		if (component == null)
		{
			return;
		}
		component.HideTooltip();
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x000465E0 File Offset: 0x000447E0
	public void InitializeNet(bool fromLogin)
	{
		SceneMgr sceneMgr;
		if (!HearthstoneServices.TryGet<SceneMgr>(out sceneMgr))
		{
			return;
		}
		this.m_waitingForNetData = true;
		if (sceneMgr.GetMode() == SceneMgr.Mode.STARTUP && !fromLogin)
		{
			return;
		}
		NetCache.Get().RegisterScreenBox(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		NetCache.Get().RegisterUpdatedListener(typeof(NetCache.NetCacheBoosters), new Action(this.OnNetCacheBoostersUpdated));
	}

	// Token: 0x06000C0C RID: 3084 RVA: 0x00046644 File Offset: 0x00044844
	private void ShutdownNet()
	{
		NetCache netCache;
		if (HearthstoneServices.TryGet<NetCache>(out netCache))
		{
			netCache.UnregisterNetCacheHandler(new NetCache.NetCacheCallback(this.OnNetCacheReady));
			netCache.RemoveUpdatedListener(typeof(NetCache.NetCacheBoosters), new Action(this.OnNetCacheBoostersUpdated));
		}
	}

	// Token: 0x06000C0D RID: 3085 RVA: 0x00046688 File Offset: 0x00044888
	private void OnNetCacheReady()
	{
		this.m_waitingForNetData = false;
		if (Network.ShouldBeConnectedToAurora() && Network.IsLoggedIn())
		{
			RankMgr.Get().SetRankPresenceField(NetCache.Get().GetNetObject<NetCache.NetCacheMedalInfo>());
		}
		base.StartCoroutine(this.UpdateUIWhenCollectionReady());
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x0004513D File Offset: 0x0004333D
	private void OnNetCacheBoostersUpdated()
	{
		this.UpdateUI(false);
	}

	// Token: 0x06000C0F RID: 3087 RVA: 0x000466C1 File Offset: 0x000448C1
	private int ComputeBoosterCount()
	{
		return NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>().GetTotalNumBoosters();
	}

	// Token: 0x06000C10 RID: 3088 RVA: 0x000466D2 File Offset: 0x000448D2
	public void UnloadQuestLog()
	{
		this.m_questLogNetCacheDataState = Box.DataState.UNLOADING;
		this.DestroyQuestLog();
	}

	// Token: 0x06000C11 RID: 3089 RVA: 0x000466E1 File Offset: 0x000448E1
	private IEnumerator ShowQuestLogWhenReady()
	{
		if (this.m_questLog == null && !this.m_questLogLoading)
		{
			this.m_questLogLoading = true;
			if (UniversalInputManager.UsePhoneUI)
			{
				AssetLoader.Get().InstantiatePrefab("QuestLog_phone.prefab:58714c7909a4eae48846cd14873cd8d5", new PrefabCallback<GameObject>(this.OnQuestLogLoaded), null, AssetLoadingOptions.None);
			}
			else
			{
				AssetLoader.Get().InstantiatePrefab("QuestLog.prefab:0e03112616f6aea4d844e2044b82d8c5", new PrefabCallback<GameObject>(this.OnQuestLogLoaded), null, AssetLoadingOptions.None);
			}
		}
		if (this.ShouldRequestData(this.m_questLogNetCacheDataState))
		{
			this.m_questLogNetCacheDataState = Box.DataState.REQUEST_SENT;
			NetCache.Get().RegisterScreenQuestLog(new NetCache.NetCacheCallback(this.OnQuestLogNetCacheReady));
		}
		while (this.m_questLog == null)
		{
			yield return null;
		}
		while (this.m_questLogNetCacheDataState != Box.DataState.RECEIVED)
		{
			yield return null;
		}
		this.m_questLog.Show();
		yield return new WaitForSeconds(0.5f);
		UnityEngine.Object.Destroy(this.m_tempInputBlocker);
		yield break;
	}

	// Token: 0x06000C12 RID: 3090 RVA: 0x000466F0 File Offset: 0x000448F0
	private void DestroyQuestLog()
	{
		base.StopCoroutine("ShowQuestLogWhenReady");
		if (this.m_questLog == null)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.m_questLog.gameObject);
	}

	// Token: 0x06000C13 RID: 3091 RVA: 0x0004671C File Offset: 0x0004491C
	private void OnQuestLogLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_questLogLoading = false;
		if (go == null)
		{
			Debug.LogError(string.Format("QuestLogButton.OnQuestLogLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		this.m_questLog = go.GetComponent<global::QuestLog>();
		if (this.m_questLog == null)
		{
			Debug.LogError(string.Format("QuestLogButton.OnQuestLogLoaded() - ERROR \"{0}\" has no {1} component", base.name, typeof(global::QuestLog)));
			return;
		}
		this.m_questLog.StartHidden();
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x0004678F File Offset: 0x0004498F
	private void OnQuestLogNetCacheReady()
	{
		if (this.m_questLogNetCacheDataState == Box.DataState.UNLOADING)
		{
			return;
		}
		this.m_questLogNetCacheDataState = Box.DataState.RECEIVED;
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x000467A2 File Offset: 0x000449A2
	private bool ShouldRequestData(Box.DataState state)
	{
		return state == Box.DataState.NONE || state == Box.DataState.UNLOADING;
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x000467B0 File Offset: 0x000449B0
	private void OnStoreButtonReleased(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			Log.Store.PrintDebug("Cannot open Shop due to being offline.", Array.Empty<object>());
			this.ShowReconnectPopup(e, new Box.ButtonPressFunction(this.OnStoreButtonReleased));
			return;
		}
		if (FriendChallengeMgr.Get().HasChallenge())
		{
			Log.Store.PrintDebug("Cannot open Shop due to having friendly challenge.", Array.Empty<object>());
			return;
		}
		StoreManager storeManager = StoreManager.Get();
		if (storeManager != null)
		{
			storeManager.Catalog.TryRefreshStaleProductAvailability();
		}
		bool flag = false;
		if (storeManager == null)
		{
			Log.Store.PrintDebug("Cannot open Shop due to null StoreManager.", Array.Empty<object>());
			flag = true;
		}
		else if (!storeManager.IsOpen(true))
		{
			flag = true;
		}
		else if (this.m_StoreButton.IsVisualClosed())
		{
			Log.Store.PrintDebug("Cannot open Shop due to button is visually closed.", Array.Empty<object>());
			flag = true;
		}
		else if (SetRotationManager.Get().CheckForSetRotationRollover())
		{
			Log.Store.PrintDebug("Cannot open Shop due to pending set rotation rollover.", Array.Empty<object>());
			flag = true;
		}
		else if (PlayerMigrationManager.Get() != null && PlayerMigrationManager.Get().CheckForPlayerMigrationRequired())
		{
			Log.Store.PrintDebug("Cannot open Shop due to pending player migration.", Array.Empty<object>());
			flag = true;
		}
		else if (!storeManager.IsVintageStoreEnabled() && storeManager.Catalog.Tiers.Count == 0)
		{
			Log.Store.PrintWarning("Cannot open Shop due to no valid tier data received.", Array.Empty<object>());
			flag = true;
		}
		else if (SceneMgr.Get() == null || SceneMgr.Get().GetMode() != SceneMgr.Mode.HUB || SceneMgr.Get().IsTransitionNowOrPending())
		{
			Log.Store.PrintWarning("Cannot open Shop due to invalid scene state.", Array.Empty<object>());
			flag = true;
		}
		if (flag)
		{
			SoundManager.Get().LoadAndPlay("Store_closed_button_click.prefab:a6b74848e2c7e5748a20524b40fe6c1e", base.gameObject);
			return;
		}
		this.FireButtonPressEvent(Box.ButtonType.STORE);
		FriendChallengeMgr.Get().OnStoreOpened();
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681", base.gameObject);
		StoreManager.Get().RegisterStoreShownListener(new Action(this.OnStoreShown));
		StoreManager.Get().StartGeneralTransaction();
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x0004699E File Offset: 0x00044B9E
	private void OnStoreShown()
	{
		StoreManager.Get().RemoveStoreShownListener(new Action(this.OnStoreShown));
	}

	// Token: 0x06000C18 RID: 3096 RVA: 0x000469B8 File Offset: 0x00044BB8
	private void SetRotation_ShowRotationDisk()
	{
		if (this.m_DiskCenter != null)
		{
			this.m_DiskCenter.SetActive(false);
		}
		if (this.m_setRotationDisk != null)
		{
			this.m_setRotationDisk.SetActive(true);
			return;
		}
		BasicPopup.PopupInfo popupInfo = new BasicPopup.PopupInfo();
		popupInfo.m_blurWhenShown = true;
		popupInfo.m_prefabAssetRefs.Add("CoreSetIntroPopup.prefab:32fcd0d9c45bc9449af825460fac647b");
		DialogManager.Get().ShowBasicPopup(UserAttentionBlocker.ALL_EXCEPT_FATAL_ERROR_SCENE, popupInfo);
		this.m_setRotationDisk = AssetLoader.Get().InstantiatePrefab("TheBox_CenterDisk_SetRotation.prefab:6f2fa714f0d129e4197fd2922f544816", AssetLoadingOptions.None);
		this.m_setRotationDisk.SetActive(true);
		this.m_setRotationDisk.transform.parent = this.m_Disk.transform;
		this.m_setRotationDisk.transform.localPosition = Vector3.zero;
		this.m_setRotationDisk.transform.localRotation = Quaternion.identity;
		this.m_setRotationButton = this.m_setRotationDisk.GetComponentInChildren<BoxMenuButton>();
		this.m_StoreButton.gameObject.SetActive(false);
		this.m_QuestLogButton.gameObject.SetActive(false);
		this.m_journalButtonWidget.Hide();
		HighlightState componentInChildren = this.m_setRotationButton.GetComponentInChildren<HighlightState>();
		if (componentInChildren != null)
		{
			componentInChildren.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
		this.RegisterButtonEvents(this.m_setRotationButton);
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x00046AF9 File Offset: 0x00044CF9
	private IEnumerator SetRotationOpen_ChangeState()
	{
		Box.BoxStateConfig boxStateConfig = this.m_stateConfigs[12];
		if (!boxStateConfig.m_logoState.m_ignore)
		{
			this.m_Logo.ChangeState(boxStateConfig.m_logoState.m_state);
		}
		if (!boxStateConfig.m_startButtonState.m_ignore)
		{
			this.m_StartButton.ChangeState(boxStateConfig.m_startButtonState.m_state);
		}
		if (!boxStateConfig.m_doorState.m_ignore)
		{
			this.m_LeftDoor.ChangeState(boxStateConfig.m_doorState.m_state);
			this.m_RightDoor.ChangeState(boxStateConfig.m_doorState.m_state);
		}
		if (!boxStateConfig.m_diskState.m_ignore)
		{
			this.m_Disk.ChangeState(boxStateConfig.m_diskState.m_state);
		}
		if (!boxStateConfig.m_camState.m_ignore)
		{
			this.m_Camera.ChangeState(BoxCamera.State.SET_ROTATION_OPENED);
		}
		if (!boxStateConfig.m_fullScreenBlackState.m_ignore)
		{
			this.FullScreenBlack_ChangeState(boxStateConfig.m_fullScreenBlackState.m_state);
		}
		SetRotationClock setRotationClock = SetRotationClock.Get();
		if (setRotationClock == null)
		{
			Debug.LogError("SetRotationOpen_ChangeState clock = null");
			yield break;
		}
		setRotationClock.StartTheClock();
		yield break;
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x00046B08 File Offset: 0x00044D08
	private IEnumerator SetRotation_StartSetRotationIntro()
	{
		this.ResetSetRotationPopupProgress();
		UserAttentionManager.StartBlocking(UserAttentionBlocker.SET_ROTATION_INTRO);
		NotificationManager.Get().DestroyAllPopUps();
		PopupDisplayManager.Get().ReadyToShowPopups();
		yield return base.StartCoroutine(PopupDisplayManager.Get().WaitForAllPopups());
		this.SetRotation_FinishShowingRewards();
		yield break;
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x00046B17 File Offset: 0x00044D17
	private void SetRotation_ShowNerfedCards_DialogHidden(DialogBase dialog, object userData)
	{
		this.SetRotation_FinishShowingRewards();
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x00046B1F File Offset: 0x00044D1F
	private void SetRotation_FinishShowingRewards()
	{
		this.ChangeState(Box.State.SET_ROTATION);
		this.SetRotation_ShowRotationDisk();
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x00046B30 File Offset: 0x00044D30
	private void DoBoxSpecialEvents()
	{
		if (!this.m_activeSpecialEvent)
		{
			this.LoadBoxSpecialEvent();
		}
		if (!this.m_isSpecialEventActive)
		{
			if (this.m_activeSpecialEvent && (this.m_state == Box.State.HUB || this.m_state == Box.State.HUB_WITH_DRAWER))
			{
				Log.Box.Print("Box Special Event Birth: {0}", new object[]
				{
					this.m_activeSpecialEvent
				});
				this.m_activeSpecialEvent.ActivateState(SpellStateType.BIRTH);
				this.m_isSpecialEventActive = true;
				return;
			}
		}
		else if (this.m_activeSpecialEvent && this.m_state != Box.State.HUB && this.m_state != Box.State.HUB_WITH_DRAWER)
		{
			Log.Box.Print("Box Special Event Death: {0}", new object[]
			{
				this.m_activeSpecialEvent
			});
			this.m_activeSpecialEvent.ActivateState(SpellStateType.DEATH);
			this.m_isSpecialEventActive = false;
		}
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x00046C00 File Offset: 0x00044E00
	private void LoadBoxSpecialEvent()
	{
		if (this.m_boxSpecialEventObj == null)
		{
			Log.Box.Print("BoxSpecialEventScriptableObj is null", Array.Empty<object>());
			return;
		}
		foreach (BoxSpecialEvent boxSpecialEvent in this.m_boxSpecialEventObj.m_specialEvents)
		{
			if (SpecialEventManager.Get().IsEventActive(boxSpecialEvent.EventType, false) && (SpecialEventManager.Get().IsEventForcedActive(boxSpecialEvent.EventType) || ((boxSpecialEvent.showToNewPlayers || AchieveManager.Get().HasUnlockedFeature(Achieve.Unlocks.DAILY)) && (boxSpecialEvent.showToReturningPlayers || !ReturningPlayerMgr.Get().IsInReturningPlayerMode))))
			{
				Log.Box.Print("Box Special Event: {0}", new object[]
				{
					boxSpecialEvent.EventType
				});
				if (PlatformSettings.Screen == ScreenCategory.Phone)
				{
					if (!string.IsNullOrEmpty(boxSpecialEvent.BoxTexturePhone))
					{
						this.LoadSpecialEventBoxTexture(boxSpecialEvent.BoxTexturePhone);
					}
				}
				else if (!string.IsNullOrEmpty(boxSpecialEvent.BoxTexture))
				{
					this.LoadSpecialEventBoxTexture(boxSpecialEvent.BoxTexture);
				}
				if (!string.IsNullOrEmpty(boxSpecialEvent.TableTexture))
				{
					this.LoadSpecialEventTableTexture(boxSpecialEvent.TableTexture);
				}
				if (!string.IsNullOrEmpty(boxSpecialEvent.Prefab))
				{
					this.LoadSpecialEventSpell(boxSpecialEvent);
				}
			}
		}
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x00046D60 File Offset: 0x00044F60
	private void LoadSpecialEventTableTexture(string texturePath)
	{
		Log.Box.Print("Loading Special Event Table Texture: {0}", new object[]
		{
			texturePath
		});
		AssetLoader.Get().LoadAsset<Texture>(ref this.m_tableTopTexture, texturePath, AssetLoadingOptions.None);
		if (this.m_tableTopTexture == null)
		{
			Debug.LogWarning(string.Format("Failed to special event table texture: {0}", texturePath));
			return;
		}
		if (this.m_tableTop != null)
		{
			Renderer component = this.m_tableTop.GetComponent<Renderer>();
			if (component != null)
			{
				component.GetMaterial().mainTexture = this.m_tableTopTexture;
			}
		}
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x00046DF0 File Offset: 0x00044FF0
	private void LoadSpecialEventBoxTexture(string texturePath)
	{
		Log.Box.Print("Loading Special Event Box Texture: {0}", new object[]
		{
			texturePath
		});
		AssetLoader.Get().LoadAsset<Texture>(ref this.m_specialEventTexture, texturePath, AssetLoadingOptions.None);
		if (this.m_specialEventTexture == null)
		{
			Debug.LogWarning(string.Format("Failed to special event box texture: {0}", texturePath));
			return;
		}
		if (this.m_LeftDoor != null)
		{
			Renderer component = this.m_LeftDoor.GetComponent<Renderer>();
			if (component != null)
			{
				component.GetMaterial().mainTexture = this.m_specialEventTexture;
			}
		}
		if (this.m_RightDoor != null)
		{
			Renderer component2 = this.m_RightDoor.GetComponent<Renderer>();
			if (component2 != null)
			{
				component2.GetMaterial().mainTexture = this.m_specialEventTexture;
			}
		}
		if (this.m_DiskCenter != null)
		{
			Renderer component3 = this.m_DiskCenter.GetComponent<Renderer>();
			if (component3 != null)
			{
				component3.GetMaterial().mainTexture = this.m_specialEventTexture;
			}
		}
		if (this.m_Drawer != null)
		{
			Renderer component4 = this.m_Drawer.GetComponent<Renderer>();
			if (component4 != null)
			{
				component4.GetMaterial().mainTexture = this.m_specialEventTexture;
			}
		}
		if (this.m_CollectionButton != null)
		{
			Renderer component5 = this.m_CollectionButton.GetComponent<Renderer>();
			if (component5 != null)
			{
				component5.GetMaterial().mainTexture = this.m_specialEventTexture;
			}
		}
		if (this.m_OpenPacksButton != null)
		{
			Renderer component6 = this.m_OpenPacksButton.GetComponent<Renderer>();
			if (component6 != null)
			{
				component6.GetMaterial().mainTexture = this.m_specialEventTexture;
			}
		}
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x00046FA4 File Offset: 0x000451A4
	private void LoadSpecialEventSpell(BoxSpecialEvent boxSpecialEvent)
	{
		string text = boxSpecialEvent.Prefab;
		if (PlatformSettings.Screen == ScreenCategory.Phone && !string.IsNullOrEmpty(boxSpecialEvent.PrefabPhoneOverride))
		{
			text = boxSpecialEvent.PrefabPhoneOverride;
		}
		Log.Box.Print("Loading Box Special Event Prefab: {0}", new object[]
		{
			text
		});
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text, AssetLoadingOptions.None);
		if (gameObject == null)
		{
			Debug.LogWarning(string.Format("Failed to load special box event: {0}", text));
			return;
		}
		gameObject.transform.parent = base.transform;
		this.m_activeSpecialEvent = gameObject.GetComponent<Spell>();
		if (this.m_activeSpecialEvent == null)
		{
			Debug.LogWarning(string.Format("Spell component not found for special box event: {0}", text));
			return;
		}
		this.m_activeSpecialEvent.ActivateState(SpellStateType.BIRTH);
		this.m_isSpecialEventActive = true;
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x00047068 File Offset: 0x00045268
	public void ShowGameModesDialog()
	{
		DialogManager.Get().ShowGameModesPopup(new UIEvent.Handler(this.OnForgeButtonPressed), new UIEvent.Handler(this.OnBaconButtonPressed), new UIEvent.Handler(this.OnPvPDungeonRunButtonPressed));
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x00047098 File Offset: 0x00045298
	private void InitializeComponents()
	{
		this.m_Logo.SetParent(this);
		this.m_Logo.SetInfo(this.m_StateInfoList.m_LogoInfo);
		this.m_StartButton.SetParent(this);
		this.m_StartButton.SetInfo(this.m_StateInfoList.m_StartButtonInfo);
		this.m_LeftDoor.SetParent(this);
		this.m_LeftDoor.SetInfo(this.m_StateInfoList.m_LeftDoorInfo);
		this.m_RightDoor.SetParent(this);
		this.m_RightDoor.SetInfo(this.m_StateInfoList.m_RightDoorInfo);
		this.m_RightDoor.EnableMain(true);
		this.m_Disk.SetParent(this);
		this.m_Disk.SetInfo(this.m_StateInfoList.m_DiskInfo);
		this.m_TopSpinner.SetParent(this);
		this.m_TopSpinner.SetInfo(this.m_StateInfoList.m_SpinnerInfo);
		this.m_BottomSpinner.SetParent(this);
		this.m_BottomSpinner.SetInfo(this.m_StateInfoList.m_SpinnerInfo);
		this.m_Drawer.SetParent(this);
		this.m_Drawer.SetInfo(this.m_StateInfoList.m_DrawerInfo);
		this.m_Camera.SetParent(this);
		this.m_Camera.SetInfo(this.m_StateInfoList.m_CameraInfo);
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.BlendToColorEnable = false;
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x000471F4 File Offset: 0x000453F4
	private void OnBoxTopPhoneTextureLoaded(AssetReference assetRef, AssetHandle<Texture> newTexture, object callbackData)
	{
		AssetHandle.Take<Texture>(ref this.m_boxTopTexture, newTexture);
		foreach (MeshRenderer renderer in base.gameObject.GetComponentsInChildren<MeshRenderer>())
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

	// Token: 0x06000C25 RID: 3109 RVA: 0x00047280 File Offset: 0x00045480
	private void TrackBoxInteractable()
	{
		if (this.m_state != Box.State.PRESS_START && this.m_state != Box.State.HUB && this.m_state != Box.State.SET_ROTATION && this.m_state != Box.State.HUB_WITH_DRAWER)
		{
			return;
		}
		HearthstonePerformance hearthstonePerformance = HearthstonePerformance.Get();
		if (hearthstonePerformance != null)
		{
			hearthstonePerformance.CaptureBoxInteractableTime();
		}
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x000472C4 File Offset: 0x000454C4
	private void ResetSetRotationPopupProgress()
	{
		GameSaveDataManager gameSaveDataManager = GameSaveDataManager.Get();
		if (gameSaveDataManager != null)
		{
			bool flag = gameSaveDataManager.IsDataReady(GameSaveKeyId.SET_ROTATION);
			bool flag2 = false;
			List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
			if (!flag)
			{
				flag2 = true;
				list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.ROTATED_BOOSTER_POPUP_PROGRESS, new long[1]));
				list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.INNKEEPER_STANDARD_DECKS_VO_PROGRESS, new long[1]));
			}
			else
			{
				long num = -1L;
				long num2 = -1L;
				gameSaveDataManager.GetSubkeyValue(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.ROTATED_BOOSTER_POPUP_PROGRESS, out num);
				gameSaveDataManager.GetSubkeyValue(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.INNKEEPER_STANDARD_DECKS_VO_PROGRESS, out num2);
				if (num != 0L)
				{
					list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.ROTATED_BOOSTER_POPUP_PROGRESS, new long[1]));
					flag2 = true;
				}
				if (num2 != 0L)
				{
					list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.INNKEEPER_STANDARD_DECKS_VO_PROGRESS, new long[1]));
					flag2 = true;
				}
			}
			if (flag2)
			{
				gameSaveDataManager.SaveSubkeys(list, null);
			}
		}
	}

	// Token: 0x040007C6 RID: 1990
	public AsyncReference m_boxWidgetRef;

	// Token: 0x040007C7 RID: 1991
	public AsyncReference m_mainShopWidgetRef;

	// Token: 0x040007C8 RID: 1992
	public GameObject m_rootObject;

	// Token: 0x040007C9 RID: 1993
	public BoxStateInfoList m_StateInfoList;

	// Token: 0x040007CA RID: 1994
	public BoxLogo m_Logo;

	// Token: 0x040007CB RID: 1995
	public BoxStartButton m_StartButton;

	// Token: 0x040007CC RID: 1996
	public BoxDoor m_LeftDoor;

	// Token: 0x040007CD RID: 1997
	public BoxDoor m_RightDoor;

	// Token: 0x040007CE RID: 1998
	public BoxDisk m_Disk;

	// Token: 0x040007CF RID: 1999
	public GameObject m_DiskCenter;

	// Token: 0x040007D0 RID: 2000
	public BoxSpinner m_TopSpinner;

	// Token: 0x040007D1 RID: 2001
	public BoxSpinner m_BottomSpinner;

	// Token: 0x040007D2 RID: 2002
	public BoxDrawer m_Drawer;

	// Token: 0x040007D3 RID: 2003
	public BoxCamera m_Camera;

	// Token: 0x040007D4 RID: 2004
	public Camera m_NoFxCamera;

	// Token: 0x040007D5 RID: 2005
	public AudioListener m_AudioListener;

	// Token: 0x040007D6 RID: 2006
	public BoxLightMgr m_LightMgr;

	// Token: 0x040007D7 RID: 2007
	public BoxEventMgr m_EventMgr;

	// Token: 0x040007D8 RID: 2008
	public BoxMenuButton m_TournamentButton;

	// Token: 0x040007D9 RID: 2009
	public BoxMenuButton m_SoloAdventuresButton;

	// Token: 0x040007DA RID: 2010
	public TavernBrawlMenuButton m_TavernBrawlButton;

	// Token: 0x040007DB RID: 2011
	public GameObject m_EmptyFourthButton;

	// Token: 0x040007DC RID: 2012
	public GameObject m_TavernBrawlButtonVisual;

	// Token: 0x040007DD RID: 2013
	public GameObject m_TavernBrawlButtonActivateFX;

	// Token: 0x040007DE RID: 2014
	public GameObject m_TavernBrawlButtonDeactivateFX;

	// Token: 0x040007DF RID: 2015
	public string m_tavernBrawlActivateSound;

	// Token: 0x040007E0 RID: 2016
	public string m_tavernBrawlDeactivateSound;

	// Token: 0x040007E1 RID: 2017
	public string m_tavernBrawlPopupSound;

	// Token: 0x040007E2 RID: 2018
	public string m_tavernBrawlPopdownSound;

	// Token: 0x040007E3 RID: 2019
	public List<string> m_tavernBrawlEnterCrowdSounds;

	// Token: 0x040007E4 RID: 2020
	public BoxMenuButton m_GameModesButton;

	// Token: 0x040007E5 RID: 2021
	public PackOpeningButton m_OpenPacksButton;

	// Token: 0x040007E6 RID: 2022
	public BoxMenuButton m_CollectionButton;

	// Token: 0x040007E7 RID: 2023
	public StoreButton m_StoreButton;

	// Token: 0x040007E8 RID: 2024
	public QuestLogButton m_QuestLogButton;

	// Token: 0x040007E9 RID: 2025
	public Widget m_journalButtonWidget;

	// Token: 0x040007EA RID: 2026
	public Color m_EnabledMaterial;

	// Token: 0x040007EB RID: 2027
	public Color m_DisabledMaterial;

	// Token: 0x040007EC RID: 2028
	public Color m_EnabledDrawerMaterial;

	// Token: 0x040007ED RID: 2029
	public Color m_DisabledDrawerMaterial;

	// Token: 0x040007EE RID: 2030
	public GameObject m_OuterFrame;

	// Token: 0x040007EF RID: 2031
	public Texture2D m_textureCompressionTest;

	// Token: 0x040007F0 RID: 2032
	public RibbonButtonsUI m_ribbonButtons;

	// Token: 0x040007F1 RID: 2033
	public GameObject m_letterboxingContainer;

	// Token: 0x040007F2 RID: 2034
	public GameObject m_tableTop;

	// Token: 0x040007F3 RID: 2035
	public GameObject m_firesideGatheringTavernBrawlButtonFX;

	// Token: 0x040007F4 RID: 2036
	public GameObject m_firesideGatheringTavernBrawlButtonFlag;

	// Token: 0x040007F5 RID: 2037
	public BoxSpecialEventScriptableObj m_boxSpecialEventObj;

	// Token: 0x040007F6 RID: 2038
	public Spell m_activeSpecialEvent;

	// Token: 0x040007F7 RID: 2039
	public bool m_isSpecialEventActive;

	// Token: 0x040007F8 RID: 2040
	private static Box s_instance;

	// Token: 0x040007F9 RID: 2041
	private Box.BoxStateConfig[] m_stateConfigs;

	// Token: 0x040007FA RID: 2042
	private Box.State m_state = Box.State.STARTUP;

	// Token: 0x040007FB RID: 2043
	private int m_pendingEffects;

	// Token: 0x040007FC RID: 2044
	private Queue<Box.State> m_stateQueue = new Queue<Box.State>();

	// Token: 0x040007FD RID: 2045
	private bool m_transitioningToSceneMode;

	// Token: 0x040007FE RID: 2046
	private List<Box.TransitionFinishedListener> m_transitionFinishedListeners = new List<Box.TransitionFinishedListener>();

	// Token: 0x040007FF RID: 2047
	private AssetHandle<Texture> m_tableTopTexture;

	// Token: 0x04000800 RID: 2048
	private AssetHandle<Texture> m_boxTopTexture;

	// Token: 0x04000801 RID: 2049
	private AssetHandle<Texture> m_specialEventTexture;

	// Token: 0x04000802 RID: 2050
	private List<Box.ButtonPressListener> m_buttonPressListeners = new List<Box.ButtonPressListener>();

	// Token: 0x04000803 RID: 2051
	private Box.ButtonType? m_queuedButtonFire;

	// Token: 0x04000804 RID: 2052
	private bool m_waitingForNetData;

	// Token: 0x04000805 RID: 2053
	private GameLayer m_originalLeftDoorLayer;

	// Token: 0x04000806 RID: 2054
	private GameLayer m_originalRightDoorLayer;

	// Token: 0x04000807 RID: 2055
	private GameLayer m_originalDrawerLayer;

	// Token: 0x04000808 RID: 2056
	private bool m_waitingForSceneLoad;

	// Token: 0x04000809 RID: 2057
	private bool m_showRibbonButtons;

	// Token: 0x0400080A RID: 2058
	private bool m_showFSGBanner;

	// Token: 0x0400080B RID: 2059
	private const string SHOW_LOG_COROUTINE = "ShowQuestLogWhenReady";

	// Token: 0x0400080C RID: 2060
	private bool m_questLogLoading;

	// Token: 0x0400080D RID: 2061
	private Box.DataState m_questLogNetCacheDataState;

	// Token: 0x0400080E RID: 2062
	private global::QuestLog m_questLog;

	// Token: 0x0400080F RID: 2063
	private GameObject m_tempInputBlocker;

	// Token: 0x04000810 RID: 2064
	private GameObject m_setRotationDisk;

	// Token: 0x04000811 RID: 2065
	private BoxMenuButton m_setRotationButton;

	// Token: 0x04000812 RID: 2066
	private VisualController m_boxVisualController;

	// Token: 0x04000813 RID: 2067
	private const string SHOW_NEW_ADVENTURE_BADGE_STATE = "NewAdventureOn";

	// Token: 0x04000814 RID: 2068
	private const string HIDE_NEW_ADVENTURE_BADGE_STATE = "NewAdventureOff";

	// Token: 0x04000815 RID: 2069
	private const string SHOW_NEW_GAME_MODE_BADGE_STATE = "NewGameModeOn";

	// Token: 0x04000816 RID: 2070
	private const string HIDE_NEW_GAME_MODE_BADGE_STATE = "NewGameModeOff";

	// Token: 0x020013CC RID: 5068
	public enum State
	{
		// Token: 0x0400A7D9 RID: 42969
		INVALID,
		// Token: 0x0400A7DA RID: 42970
		STARTUP,
		// Token: 0x0400A7DB RID: 42971
		PRESS_START,
		// Token: 0x0400A7DC RID: 42972
		LOADING,
		// Token: 0x0400A7DD RID: 42973
		LOADING_HUB,
		// Token: 0x0400A7DE RID: 42974
		HUB,
		// Token: 0x0400A7DF RID: 42975
		HUB_WITH_DRAWER,
		// Token: 0x0400A7E0 RID: 42976
		OPEN,
		// Token: 0x0400A7E1 RID: 42977
		CLOSED,
		// Token: 0x0400A7E2 RID: 42978
		ERROR,
		// Token: 0x0400A7E3 RID: 42979
		SET_ROTATION_LOADING,
		// Token: 0x0400A7E4 RID: 42980
		SET_ROTATION,
		// Token: 0x0400A7E5 RID: 42981
		SET_ROTATION_OPEN
	}

	// Token: 0x020013CD RID: 5069
	public enum ButtonType
	{
		// Token: 0x0400A7E7 RID: 42983
		START,
		// Token: 0x0400A7E8 RID: 42984
		TOURNAMENT,
		// Token: 0x0400A7E9 RID: 42985
		ADVENTURE,
		// Token: 0x0400A7EA RID: 42986
		FORGE,
		// Token: 0x0400A7EB RID: 42987
		OPEN_PACKS,
		// Token: 0x0400A7EC RID: 42988
		COLLECTION,
		// Token: 0x0400A7ED RID: 42989
		TAVERN_BRAWL,
		// Token: 0x0400A7EE RID: 42990
		SET_ROTATION,
		// Token: 0x0400A7EF RID: 42991
		QUEST_LOG,
		// Token: 0x0400A7F0 RID: 42992
		STORE,
		// Token: 0x0400A7F1 RID: 42993
		GAME_MODES,
		// Token: 0x0400A7F2 RID: 42994
		BACON,
		// Token: 0x0400A7F3 RID: 42995
		PVP_DUNGEON_RUN
	}

	// Token: 0x020013CE RID: 5070
	// (Invoke) Token: 0x0600D8AD RID: 55469
	public delegate void TransitionFinishedCallback(object userData);

	// Token: 0x020013CF RID: 5071
	// (Invoke) Token: 0x0600D8B1 RID: 55473
	public delegate void ButtonPressFunction(UIEvent e);

	// Token: 0x020013D0 RID: 5072
	// (Invoke) Token: 0x0600D8B5 RID: 55477
	public delegate void ButtonPressCallback(Box.ButtonType buttonType, object userData);

	// Token: 0x020013D1 RID: 5073
	private class TransitionFinishedListener : EventListener<Box.TransitionFinishedCallback>
	{
		// Token: 0x0600D8B8 RID: 55480 RVA: 0x003EEC1B File Offset: 0x003ECE1B
		public void Fire()
		{
			this.m_callback(this.m_userData);
		}
	}

	// Token: 0x020013D2 RID: 5074
	private class ButtonPressListener : EventListener<Box.ButtonPressCallback>
	{
		// Token: 0x0600D8BA RID: 55482 RVA: 0x003EEC36 File Offset: 0x003ECE36
		public void Fire(Box.ButtonType buttonType)
		{
			this.m_callback(buttonType, this.m_userData);
		}
	}

	// Token: 0x020013D3 RID: 5075
	private class BoxStateConfig
	{
		// Token: 0x0400A7F4 RID: 42996
		public Box.BoxStateConfig.Part<BoxLogo.State> m_logoState = new Box.BoxStateConfig.Part<BoxLogo.State>();

		// Token: 0x0400A7F5 RID: 42997
		public Box.BoxStateConfig.Part<BoxStartButton.State> m_startButtonState = new Box.BoxStateConfig.Part<BoxStartButton.State>();

		// Token: 0x0400A7F6 RID: 42998
		public Box.BoxStateConfig.Part<BoxDoor.State> m_doorState = new Box.BoxStateConfig.Part<BoxDoor.State>();

		// Token: 0x0400A7F7 RID: 42999
		public Box.BoxStateConfig.Part<BoxDisk.State> m_diskState = new Box.BoxStateConfig.Part<BoxDisk.State>();

		// Token: 0x0400A7F8 RID: 43000
		public Box.BoxStateConfig.Part<BoxDrawer.State> m_drawerState = new Box.BoxStateConfig.Part<BoxDrawer.State>();

		// Token: 0x0400A7F9 RID: 43001
		public Box.BoxStateConfig.Part<BoxCamera.State> m_camState = new Box.BoxStateConfig.Part<BoxCamera.State>();

		// Token: 0x0400A7FA RID: 43002
		public Box.BoxStateConfig.Part<bool> m_fullScreenBlackState = new Box.BoxStateConfig.Part<bool>();

		// Token: 0x02002979 RID: 10617
		public class Part<T>
		{
			// Token: 0x0400FCDE RID: 64734
			public bool m_ignore;

			// Token: 0x0400FCDF RID: 64735
			public T m_state;
		}
	}

	// Token: 0x020013D4 RID: 5076
	private enum DataState
	{
		// Token: 0x0400A7FC RID: 43004
		NONE,
		// Token: 0x0400A7FD RID: 43005
		REQUEST_SENT,
		// Token: 0x0400A7FE RID: 43006
		RECEIVED,
		// Token: 0x0400A7FF RID: 43007
		UNLOADING
	}
}
