using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using Hearthstone;
using Hearthstone.Core;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000367 RID: 871
public class Hub : PegasusScene
{
	// Token: 0x06003318 RID: 13080 RVA: 0x001066B4 File Offset: 0x001048B4
	private void Start()
	{
		IJobDependency[] dependencies = HearthstoneJobs.BuildDependencies(new object[]
		{
			typeof(SceneMgr),
			typeof(IAssetLoader),
			typeof(NetCache),
			typeof(SpecialEventManager),
			typeof(DemoMgr),
			typeof(AchieveManager),
			typeof(HealthyGamingMgr),
			typeof(FiresideGatheringManager),
			typeof(TavernBrawlManager),
			typeof(GameMgr),
			typeof(ShownUIMgr),
			typeof(MusicManager),
			typeof(SoundManager),
			typeof(SetRotationManager),
			typeof(PopupDisplayManager)
		});
		Processor.QueueJob("Hub.Initialize", this.Job_Initialize(), dependencies);
	}

	// Token: 0x06003319 RID: 13081 RVA: 0x001067A9 File Offset: 0x001049A9
	private IEnumerator<IAsyncJobResult> Job_Initialize()
	{
		this.VerifyPrequisitesInitialized();
		if (Network.ShouldBeConnectedToAurora())
		{
			if (CollectionManager.Get() != null && CollectionManager.Get().IsFullyLoaded())
			{
				this.OnCollectionLoaded();
			}
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.HUB
			});
		}
		else
		{
			Error.AddDevWarning("Alert", "There is no connection to Battle.net, please restart Hearthstone to log in.", Array.Empty<object>());
		}
		this.RegisterEventListeners();
		SceneMgr.Get().NotifySceneLoaded();
		if (SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.LOGIN)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_MainTitle);
		}
		this.ShowHubStartNotifications();
		if (!Network.ShouldBeConnectedToAurora())
		{
			Box.Get().DisableAllButtons();
		}
		yield break;
	}

	// Token: 0x0600331A RID: 13082 RVA: 0x001067B8 File Offset: 0x001049B8
	private void OnDestroy()
	{
		this.UnregisterEventListeners();
	}

	// Token: 0x0600331B RID: 13083 RVA: 0x001067C0 File Offset: 0x001049C0
	private void VerifyPrequisitesInitialized()
	{
		if (CollectionManager.Get() == null)
		{
			Debug.LogError("Hub.Start Error - CollectionManager is null");
		}
		if (PresenceMgr.Get() == null)
		{
			Debug.LogError("Hub.Start Error - PresenceMgr is null");
		}
		if (Box.Get() == null)
		{
			Debug.LogError("Hub.Start Error - Box is null");
		}
		if (Options.Get() == null)
		{
			Debug.LogError("Hub.Start Error - Options is null");
		}
		if (NotificationManager.Get() == null)
		{
			Debug.LogError("Hub.Start Error - NotificationManager is null");
		}
		if (StoreManager.Get() == null)
		{
			Debug.LogError("Hub.Start Error - StoreManager is null");
		}
	}

	// Token: 0x0600331C RID: 13084 RVA: 0x00106840 File Offset: 0x00104A40
	private void RegisterEventListeners()
	{
		Box box = Box.Get();
		if (box != null)
		{
			box.AddButtonPressListener(new Box.ButtonPressCallback(this.OnBoxButtonPressed));
			if (box.m_QuestLogButton == null)
			{
				Debug.LogError("Hub.Start Error - QuestLogButton is null");
			}
			else
			{
				box.m_QuestLogButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnButtonReleasedHideTooltipNotification));
			}
			if (box.m_journalButtonWidget == null)
			{
				Debug.LogError("Hub.Start Error - JournalButton is null");
			}
			else
			{
				box.m_journalButtonWidget.RegisterEventListener(delegate(string eventName)
				{
					if (eventName == "BUTTON_CLICKED" && this.m_PracticeNotification != null)
					{
						NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.m_PracticeNotification);
					}
				});
			}
			if (box.m_StoreButton == null)
			{
				Debug.LogError("Hub.Start Error - StoreButton is null");
			}
			else
			{
				box.m_StoreButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnButtonReleasedHideTooltipNotification));
			}
			if (UniversalInputManager.UsePhoneUI)
			{
				if (box.m_ribbonButtons == null)
				{
					Debug.LogError("Hub.Start Error - RibbonButtons is null");
				}
				else
				{
					if (box.m_ribbonButtons.m_questLogRibbon == null)
					{
						Debug.LogError("Hub.Start Error - QuestLogRibbon is null");
					}
					else
					{
						box.m_ribbonButtons.m_questLogRibbon.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnButtonReleasedHideTooltipNotification));
					}
					if (box.m_ribbonButtons.m_storeRibbon == null)
					{
						Debug.LogError("Hub.Start Error - StoreRibbon is null");
					}
					else
					{
						box.m_ribbonButtons.m_storeRibbon.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnButtonReleasedHideTooltipNotification));
					}
					if (box.m_ribbonButtons.m_journalButtonWidget == null)
					{
						Debug.LogError("Hub.Start Error - JournalRibbon is null");
					}
					else
					{
						box.m_ribbonButtons.m_journalButtonWidget.RegisterEventListener(delegate(string eventName)
						{
							if (eventName == "BUTTON_CLICKED" && this.m_PracticeNotification != null)
							{
								NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.m_PracticeNotification);
							}
						});
					}
				}
			}
		}
		else
		{
			Debug.LogError("Hub.Start Error - box is null");
		}
		if (StoreManager.IsInitialized())
		{
			StoreManager.Get().RegisterSuccessfulPurchaseListener(new Action<Network.Bundle, PaymentMethod>(this.OnAdventureBundlePurchase));
		}
		else
		{
			Debug.LogError("Hub.Start Error - RegisterSuccessfulPurchaseListener not assigned");
		}
		SpecialEventType specialEventType = SpecialEventType.IGNORE;
		SpecialEventManager specialEventManager = SpecialEventManager.Get();
		if (specialEventManager != null)
		{
			specialEventType = specialEventManager.GetActiveEventType();
		}
		else
		{
			Debug.LogError("Hub.Start Error - SpecialEventManager was null and eventType was not received");
		}
		AchieveManager achieveManager = AchieveManager.Get();
		if (specialEventType != SpecialEventType.IGNORE && achieveManager != null && achieveManager.HasUnlockedArena())
		{
			if (specialEventManager != null)
			{
				specialEventManager.Visuals.LoadEvent(specialEventType);
			}
			if (SceneMgr.IsInitialized())
			{
				SceneMgr.Get().RegisterSceneUnloadedEvent(new SceneMgr.SceneUnloadedCallback(this.OnSceneUnloaded));
			}
			else
			{
				Debug.LogError("Hub.Start Error - SceneMgr did not register scene unload event");
			}
		}
		TavernBrawlManager tavernBrawlManager = TavernBrawlManager.Get();
		if (tavernBrawlManager != null)
		{
			tavernBrawlManager.OnTavernBrawlUpdated += this.DoTavernBrawlAnimsCB;
			tavernBrawlManager.OnSessionLimitRaised += this.MaybeDoTavernBrawlLimitRaisedAlert;
		}
		else
		{
			Debug.LogError("Hub.Start Error - TavernBrawlManager did not register certain events");
		}
		FiresideGatheringManager firesideGatheringManager = FiresideGatheringManager.Get();
		if (firesideGatheringManager != null)
		{
			firesideGatheringManager.OnSignClosed += this.DoTavernBrawlAnimsCBForce;
		}
		else
		{
			Debug.LogError("Hub.Start Error - FiresideGatheringManager did not register OnSignClosed");
		}
		NetCache netCache = NetCache.Get();
		if (netCache != null)
		{
			netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheFeatures), new Action(this.DoTavernBrawlAnimsCB));
			netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheHeroLevels), new Action(this.DoTavernBrawlAnimsCB));
		}
		else
		{
			Debug.LogError("Hub.Start Error - NetCache did not register certain events");
		}
		CollectionManager.OnCollectionManagerReady += this.OnCollectionLoaded;
	}

	// Token: 0x0600331D RID: 13085 RVA: 0x00106B54 File Offset: 0x00104D54
	private void UnregisterEventListeners()
	{
		TavernBrawlManager tavernBrawlManager;
		if (HearthstoneServices.TryGet<TavernBrawlManager>(out tavernBrawlManager))
		{
			tavernBrawlManager.OnTavernBrawlUpdated -= this.DoTavernBrawlAnimsCB;
		}
		FiresideGatheringManager firesideGatheringManager;
		if (HearthstoneServices.TryGet<FiresideGatheringManager>(out firesideGatheringManager))
		{
			firesideGatheringManager.OnSignClosed -= this.DoTavernBrawlAnimsCBForce;
		}
		NetCache netCache;
		if (HearthstoneServices.TryGet<NetCache>(out netCache))
		{
			netCache.RemoveUpdatedListener(typeof(NetCache.NetCacheFeatures), new Action(this.DoTavernBrawlAnimsCB));
			netCache.RemoveUpdatedListener(typeof(NetCache.NetCacheHeroLevels), new Action(this.DoTavernBrawlAnimsCB));
		}
		CollectionManager.OnCollectionManagerReady -= this.OnCollectionLoaded;
	}

	// Token: 0x0600331E RID: 13086 RVA: 0x00106BEC File Offset: 0x00104DEC
	private void ShowHubStartNotifications()
	{
		PopupDisplayManager.Get().ReadyToShowPopups();
		if (Network.ShouldBeConnectedToAurora())
		{
			if (!Options.Get().GetBool(Option.HAS_SEEN_HUB, false) && UserAttentionManager.CanShowAttentionGrabber("Hub.Start:" + Option.HAS_SEEN_HUB))
			{
				base.StartCoroutine(this.DoFirstTimeHubWelcome());
			}
			else if (!Options.Get().GetBool(Option.HAS_SEEN_100g_REMINDER, false))
			{
				NetCache.NetCacheGoldBalance netObject = NetCache.Get().GetNetObject<NetCache.NetCacheGoldBalance>();
				if (netObject == null)
				{
					Debug.LogError("Hub.Start Error - NetCache.NetCacheGoldBalance is null");
				}
				if (netObject.GetTotal() >= 100L && UserAttentionManager.CanShowAttentionGrabber("Hub.Start:" + Option.HAS_SEEN_100g_REMINDER))
				{
					NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_FIRST_100_GOLD"), "VO_INNKEEPER_FIRST_100_GOLD.prefab:c6a50337099a454488acd96d2f37320f", 0f, null, false);
					Options.Get().SetBool(Option.HAS_SEEN_100g_REMINDER, true);
				}
			}
			else if (TavernBrawlManager.Get().IsFirstTimeSeeingThisFeature)
			{
				Hub.DoTavernBrawlIntroVO();
			}
			if (TavernBrawlManager.Get().IsFirstTimeSeeingCurrentSeason && UserAttentionManager.CanShowAttentionGrabber("Hub.TavernBrawl.IsFirstTimeSeeingCurrentSeason") && !Hub.s_hasAlreadyShownTBAnimation)
			{
				base.StartCoroutine(this.DoTavernBrawlAnims(false));
			}
		}
	}

	// Token: 0x0600331F RID: 13087 RVA: 0x00106D0C File Offset: 0x00104F0C
	private static void DoTavernBrawlIntroVO()
	{
		if (!NotificationManager.Get().HasSoundPlayedThisSession("VO_INNKEEPER_TAVERNBRAWL_PUSH_32.prefab:4f57cd2af5fe5194fbc46c91171ab135"))
		{
			Action<int> finishCallback = delegate(int groupId)
			{
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_TAVERNBRAWL_DESC1_29"), "VO_INNKEEPER_TAVERNBRAWL_DESC1_29.prefab:44d1a6b322c3dcf4c950e68eb4f4a05f", 0f, null, false);
			};
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_TAVERNBRAWL_PUSH_32"), "VO_INNKEEPER_TAVERNBRAWL_PUSH_32.prefab:4f57cd2af5fe5194fbc46c91171ab135", finishCallback, false);
			NotificationManager.Get().ForceAddSoundToPlayedList("VO_INNKEEPER_TAVERNBRAWL_PUSH_32.prefab:4f57cd2af5fe5194fbc46c91171ab135");
		}
	}

	// Token: 0x06003320 RID: 13088 RVA: 0x00106D76 File Offset: 0x00104F76
	private void DoTavernBrawlAnimsCB()
	{
		base.StartCoroutine(this.DoTavernBrawlAnims(false));
	}

	// Token: 0x06003321 RID: 13089 RVA: 0x00106D86 File Offset: 0x00104F86
	private void DoTavernBrawlAnimsCBForce()
	{
		base.StartCoroutine(this.DoTavernBrawlAnims(true));
	}

	// Token: 0x06003322 RID: 13090 RVA: 0x00106D96 File Offset: 0x00104F96
	private IEnumerator DoTavernBrawlAnims(bool force = false)
	{
		Box theBox = Box.Get();
		if (!theBox.UpdateTavernBrawlButtonState(true))
		{
			yield break;
		}
		TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
		if (true)
		{
			bool isFirstTimeSeeingCurrentSeason = TavernBrawlManager.Get().IsFirstTimeSeeingCurrentSeason;
			if (isFirstTimeSeeingCurrentSeason || theBox.IsTavernBrawlButtonDeactivated || force)
			{
				theBox.UpdateTavernBrawlButtonState(false);
				if (isFirstTimeSeeingCurrentSeason)
				{
					float length = theBox.m_TavernBrawlButtonVisual.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
					yield return new WaitForSeconds(length);
				}
				if (TavernBrawlManager.Get().IsFirstTimeSeeingThisFeature)
				{
					Hub.DoTavernBrawlIntroVO();
				}
				if (theBox.m_tavernBrawlActivateSound != string.Empty)
				{
					SoundManager.Get().LoadAndPlay(theBox.m_tavernBrawlActivateSound);
				}
				theBox.PlayTavernBrawlButtonActivation(true, false);
				yield return new WaitForSeconds(0.65f);
				CameraShakeMgr.Shake(Camera.main, new Vector3(0.5f, 0.5f, 0.5f), 0.3f);
				theBox.UpdateTavernBrawlButtonState(true);
			}
		}
		else if (!theBox.IsTavernBrawlButtonDeactivated)
		{
			if (theBox.m_tavernBrawlDeactivateSound != string.Empty)
			{
				SoundManager.Get().LoadAndPlay(Box.Get().m_tavernBrawlDeactivateSound);
			}
			theBox.PlayTavernBrawlButtonActivation(false, false);
		}
		Hub.s_hasAlreadyShownTBAnimation = true;
		yield break;
	}

	// Token: 0x06003323 RID: 13091 RVA: 0x00106DA8 File Offset: 0x00104FA8
	private void MaybeDoTavernBrawlLimitRaisedAlert(int lastSeenLimit, int newLimit)
	{
		int numSessionsAvailableForPurchase = TavernBrawlManager.Get().NumSessionsAvailableForPurchase;
		if (numSessionsAvailableForPurchase == newLimit - lastSeenLimit)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
			popupInfo.m_headerText = GameStrings.Get("GLUE_HEROIC_BRAWL_SESSION_LIMIT_ALERT_TITLE");
			popupInfo.m_text = GameStrings.Format("GLUE_HEROIC_BRAWL_SESSION_LIMIT_ALERT_LIMIT_RAISED", new object[]
			{
				numSessionsAvailableForPurchase
			});
			DialogManager.Get().ShowPopup(popupInfo);
		}
	}

	// Token: 0x06003324 RID: 13092 RVA: 0x00019DD3 File Offset: 0x00017FD3
	private void Update()
	{
		Network.Get().ProcessNetwork();
	}

	// Token: 0x06003325 RID: 13093 RVA: 0x00106E14 File Offset: 0x00105014
	public override void Unload()
	{
		StoreManager.Get().RemoveSuccessfulPurchaseListener(new Action<Network.Bundle, PaymentMethod>(this.OnAdventureBundlePurchase));
		this.HideTooltipNotification();
		Box box = Box.Get();
		if (box != null)
		{
			box.m_QuestLogButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnButtonReleasedHideTooltipNotification));
			box.m_StoreButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnButtonReleasedHideTooltipNotification));
			box.RemoveButtonPressListener(new Box.ButtonPressCallback(this.OnBoxButtonPressed));
		}
	}

	// Token: 0x06003326 RID: 13094 RVA: 0x00106E94 File Offset: 0x00105094
	private void OnSceneUnloaded(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		SpecialEventType activeEventType = SpecialEventManager.Get().GetActiveEventType();
		if (activeEventType != SpecialEventType.IGNORE)
		{
			SpecialEventManager.Get().Visuals.UnloadEvent(activeEventType);
		}
		SceneMgr.Get().UnregisterSceneUnloadedEvent(new SceneMgr.SceneUnloadedCallback(this.OnSceneUnloaded));
	}

	// Token: 0x06003327 RID: 13095 RVA: 0x00106ED8 File Offset: 0x001050D8
	private void OnBoxButtonPressed(Box.ButtonType buttonType, object userData)
	{
		if (buttonType == Box.ButtonType.TOURNAMENT)
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.TOURNAMENT, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			Tournament.Get().NotifyOfBoxTransitionStart();
			return;
		}
		if (buttonType == Box.ButtonType.FORGE)
		{
			this.HandleArenaButtonPressed();
			return;
		}
		if (buttonType == Box.ButtonType.GAME_MODES)
		{
			this.HandleGameModesButtonPressed();
			return;
		}
		if (buttonType == Box.ButtonType.BACON)
		{
			this.HandleBaconButtonPressed();
			return;
		}
		if (buttonType == Box.ButtonType.ADVENTURE)
		{
			AdventureConfig.Get().ResetSubScene();
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.ADVENTURE, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			return;
		}
		if (buttonType == Box.ButtonType.COLLECTION)
		{
			CollectionManager.Get().NotifyOfBoxTransitionStart();
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.COLLECTIONMANAGER, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			return;
		}
		if (buttonType == Box.ButtonType.OPEN_PACKS)
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.PACKOPENING, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			return;
		}
		if (buttonType == Box.ButtonType.TAVERN_BRAWL)
		{
			this.HandleTavernBrawlButtonPressed();
			return;
		}
		if (buttonType == Box.ButtonType.SET_ROTATION)
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.TOURNAMENT, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			Tournament.Get().NotifyOfBoxTransitionStart();
		}
	}

	// Token: 0x06003328 RID: 13096 RVA: 0x00106F95 File Offset: 0x00105195
	private void HandleGameModesButtonPressed()
	{
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.GAME_MODE, SceneMgr.TransitionHandlerType.SCENEMGR, null);
	}

	// Token: 0x06003329 RID: 13097 RVA: 0x00106FA8 File Offset: 0x001051A8
	private void HandleArenaButtonPressed()
	{
		ulong secondsUntilEndOfSeason = DraftManager.Get().SecondsUntilEndOfSeason;
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		if (!DraftManager.Get().HasActiveRun && secondsUntilEndOfSeason <= (ulong)netObject.ArenaClosedToNewSessionsSeconds)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_ARENA_1ST_TIME_HEADER");
			popupInfo.m_text = GameStrings.Get("GLUE_ARENA_SIGNUPS_CLOSED");
			popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo);
			return;
		}
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.DRAFT, SceneMgr.TransitionHandlerType.SCENEMGR, null);
	}

	// Token: 0x0600332A RID: 13098 RVA: 0x0010702F File Offset: 0x0010522F
	private void HandleBaconButtonPressed()
	{
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.BACON, SceneMgr.TransitionHandlerType.SCENEMGR, null);
	}

	// Token: 0x0600332B RID: 13099 RVA: 0x0010703F File Offset: 0x0010523F
	private void HandleTavernBrawlButtonPressed()
	{
		this.SelectTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
	}

	// Token: 0x0600332C RID: 13100 RVA: 0x00107048 File Offset: 0x00105248
	private void ShowTavernBrawlSelectionDialog()
	{
		DialogManager.Get().ShowTavernBrawlChoiceDialog(new FiresideBrawlChoiceDialog.ResponseCallback(this.SelectTavernBrawl));
	}

	// Token: 0x0600332D RID: 13101 RVA: 0x00107060 File Offset: 0x00105260
	private void SelectTavernBrawl(BrawlType brawlType)
	{
		TavernBrawlManager.Get().CurrentBrawlType = brawlType;
		if (TavernBrawlManager.Get().IsCurrentTavernBrawlSeasonClosedToPlayer)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_HEROIC_BRAWL_SIGNUPS_CLOSED_TITLE");
			popupInfo.m_text = GameStrings.Get("GLUE_HEROIC_BRAWL_SIGNUPS_CLOSED");
			popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo);
			return;
		}
		if (TavernBrawlManager.Get().IsPlayerAtSessionMaxForCurrentTavernBrawl)
		{
			AlertPopup.PopupInfo popupInfo2 = new AlertPopup.PopupInfo();
			popupInfo2.m_headerText = GameStrings.Get("GLOBAL_HEROIC_BRAWL");
			popupInfo2.m_text = GameStrings.Get("GLUE_HEROIC_BRAWL_SESSION_LIMIT_ALERT_LIMIT_HIT");
			popupInfo2.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
			popupInfo2.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo2);
			return;
		}
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.TAVERN_BRAWL, SceneMgr.TransitionHandlerType.SCENEMGR, null);
	}

	// Token: 0x0600332E RID: 13102 RVA: 0x0010711E File Offset: 0x0010531E
	private IEnumerator DoFirstTimeHubWelcome()
	{
		Box box = Box.Get();
		while (box == null || box.IsBusy() || box.m_SoloAdventuresButton == null || !box.m_SoloAdventuresButton.IsEnabled() || box.GetState() != Box.State.HUB_WITH_DRAWER || box.GetBoxCamera() == null || box.GetBoxCamera().GetState() != BoxCamera.State.CLOSED_WITH_DRAWER)
		{
			yield return Hub.HumanInteractionPollSpan;
			box = Box.Get();
		}
		StoreManager storeManager = StoreManager.Get();
		QuestLog questLog = QuestLog.Get();
		while ((storeManager != null && storeManager.IsShown()) || (questLog != null && questLog.IsShown()))
		{
			yield return Hub.HumanInteractionPollSpan;
			storeManager = StoreManager.Get();
			questLog = QuestLog.Get();
		}
		AchieveManager achieveManager = AchieveManager.Get();
		while ((achieveManager != null && achieveManager.HasQuestsToShow(true)) || WelcomeQuests.Get() != null)
		{
			yield return Hub.HumanInteractionPollSpan;
			achieveManager = AchieveManager.Get();
		}
		PopupDisplayManager popupDisplayManager = PopupDisplayManager.Get();
		while (popupDisplayManager != null && popupDisplayManager.IsShowing)
		{
			yield return Hub.HumanInteractionPollSpan;
			popupDisplayManager = PopupDisplayManager.Get();
		}
		NotificationManager notificationManager = NotificationManager.Get();
		if (notificationManager != null)
		{
			notificationManager.CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_1ST_HUB_06"), "VO_INNKEEPER_1ST_HUB_06.prefab:9774392944a21424788286f80d401d8c", 3f, null, false);
			if (UniversalInputManager.UsePhoneUI)
			{
				this.m_PracticeNotification = notificationManager.CreatePopupText(UserAttentionBlocker.NONE, new Vector3(-30.46f, 33.5f, 3f), 25f * Vector3.one, GameStrings.Get("GLUE_PRACTICE_HINT"), true, NotificationManager.PopupTextType.BASIC);
			}
			else
			{
				this.m_PracticeNotification = notificationManager.CreatePopupText(UserAttentionBlocker.NONE, new Vector3(-33.62785f, 33.52365f, 3f), 15f * Vector3.one, GameStrings.Get("GLUE_PRACTICE_HINT"), true, NotificationManager.PopupTextType.BASIC);
			}
			this.m_PracticeNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
		}
		Options.Get().SetBool(Option.HAS_SEEN_HUB, true);
		AdTrackingManager adTrackingManager = AdTrackingManager.Get();
		if (adTrackingManager != null)
		{
			adTrackingManager.TrackFirstLogin();
		}
		else
		{
			Debug.LogWarning("AdTrackingManager was not initialized during Hub.DoFirstTimeHubWelcome()");
		}
		yield break;
	}

	// Token: 0x0600332F RID: 13103 RVA: 0x00107130 File Offset: 0x00105330
	private void OnAdventureBundlePurchase(Network.Bundle bundle, PaymentMethod purchaseMethod)
	{
		if (bundle == null || bundle.Items == null)
		{
			return;
		}
		using (List<Network.BundleItem>.Enumerator enumerator = bundle.Items.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ItemType == ProductType.PRODUCT_TYPE_NAXX)
				{
					Options.Get().SetBool(Option.BUNDLE_JUST_PURCHASE_IN_HUB, true);
					AdventureConfig.Get().SetSelectedAdventureMode(AdventureDbId.NAXXRAMAS, AdventureModeDbId.LINEAR);
					break;
				}
			}
		}
	}

	// Token: 0x06003330 RID: 13104 RVA: 0x001071B0 File Offset: 0x001053B0
	private void OnButtonReleasedHideTooltipNotification(UIEvent e)
	{
		this.HideTooltipNotification();
	}

	// Token: 0x06003331 RID: 13105 RVA: 0x001071B8 File Offset: 0x001053B8
	private void OnCollectionLoaded()
	{
		if (CollectionManager.Get().GetPreconDeck(TAG_CLASS.MAGE) == null)
		{
			Error.AddFatal(FatalErrorReason.ACCOUNT_SETUP_ERROR, "GLOBAL_ERROR_NO_MAGE_PRECON", Array.Empty<object>());
			return;
		}
	}

	// Token: 0x06003332 RID: 13106 RVA: 0x001071D8 File Offset: 0x001053D8
	private void HideTooltipNotification()
	{
		if (this.m_PracticeNotification != null)
		{
			NotificationManager.Get().DestroyNotification(this.m_PracticeNotification, 0f);
		}
	}

	// Token: 0x04001C13 RID: 7187
	public static bool s_hasAlreadyShownTBAnimation = false;

	// Token: 0x04001C14 RID: 7188
	private static readonly WaitForSeconds HumanInteractionPollSpan = new WaitForSeconds(0.1f);

	// Token: 0x04001C15 RID: 7189
	private Notification m_PracticeNotification;
}
