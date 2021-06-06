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

public class Hub : PegasusScene
{
	public static bool s_hasAlreadyShownTBAnimation = false;

	private static readonly WaitForSeconds HumanInteractionPollSpan = new WaitForSeconds(0.1f);

	private Notification m_PracticeNotification;

	private void Start()
	{
		IJobDependency[] dependencies = HearthstoneJobs.BuildDependencies(typeof(SceneMgr), typeof(IAssetLoader), typeof(NetCache), typeof(SpecialEventManager), typeof(DemoMgr), typeof(AchieveManager), typeof(HealthyGamingMgr), typeof(FiresideGatheringManager), typeof(TavernBrawlManager), typeof(GameMgr), typeof(ShownUIMgr), typeof(MusicManager), typeof(SoundManager), typeof(SetRotationManager), typeof(PopupDisplayManager));
		Processor.QueueJob("Hub.Initialize", Job_Initialize(), dependencies);
	}

	private IEnumerator<IAsyncJobResult> Job_Initialize()
	{
		VerifyPrequisitesInitialized();
		if (Network.ShouldBeConnectedToAurora())
		{
			if (CollectionManager.Get() != null && CollectionManager.Get().IsFullyLoaded())
			{
				OnCollectionLoaded();
			}
			PresenceMgr.Get().SetStatus(Global.PresenceStatus.HUB);
		}
		else
		{
			Error.AddDevWarning("Alert", "There is no connection to Battle.net, please restart Hearthstone to log in.");
		}
		RegisterEventListeners();
		SceneMgr.Get().NotifySceneLoaded();
		if (SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.LOGIN)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_MainTitle);
		}
		ShowHubStartNotifications();
		if (!Network.ShouldBeConnectedToAurora())
		{
			Box.Get().DisableAllButtons();
		}
		yield break;
	}

	private void OnDestroy()
	{
		UnregisterEventListeners();
	}

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

	private void RegisterEventListeners()
	{
		Box box = Box.Get();
		if (box != null)
		{
			box.AddButtonPressListener(OnBoxButtonPressed);
			if (box.m_QuestLogButton == null)
			{
				Debug.LogError("Hub.Start Error - QuestLogButton is null");
			}
			else
			{
				box.m_QuestLogButton.AddEventListener(UIEventType.RELEASE, OnButtonReleasedHideTooltipNotification);
			}
			if (box.m_journalButtonWidget == null)
			{
				Debug.LogError("Hub.Start Error - JournalButton is null");
			}
			else
			{
				box.m_journalButtonWidget.RegisterEventListener(delegate(string eventName)
				{
					if (eventName == "BUTTON_CLICKED" && m_PracticeNotification != null)
					{
						NotificationManager.Get().DestroyNotificationNowWithNoAnim(m_PracticeNotification);
					}
				});
			}
			if (box.m_StoreButton == null)
			{
				Debug.LogError("Hub.Start Error - StoreButton is null");
			}
			else
			{
				box.m_StoreButton.AddEventListener(UIEventType.RELEASE, OnButtonReleasedHideTooltipNotification);
			}
			if ((bool)UniversalInputManager.UsePhoneUI)
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
						box.m_ribbonButtons.m_questLogRibbon.AddEventListener(UIEventType.RELEASE, OnButtonReleasedHideTooltipNotification);
					}
					if (box.m_ribbonButtons.m_storeRibbon == null)
					{
						Debug.LogError("Hub.Start Error - StoreRibbon is null");
					}
					else
					{
						box.m_ribbonButtons.m_storeRibbon.AddEventListener(UIEventType.RELEASE, OnButtonReleasedHideTooltipNotification);
					}
					if (box.m_ribbonButtons.m_journalButtonWidget == null)
					{
						Debug.LogError("Hub.Start Error - JournalRibbon is null");
					}
					else
					{
						box.m_ribbonButtons.m_journalButtonWidget.RegisterEventListener(delegate(string eventName)
						{
							if (eventName == "BUTTON_CLICKED" && m_PracticeNotification != null)
							{
								NotificationManager.Get().DestroyNotificationNowWithNoAnim(m_PracticeNotification);
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
			StoreManager.Get().RegisterSuccessfulPurchaseListener(OnAdventureBundlePurchase);
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
		if (specialEventType != 0 && achieveManager != null && achieveManager.HasUnlockedArena())
		{
			specialEventManager?.Visuals.LoadEvent(specialEventType);
			if (SceneMgr.IsInitialized())
			{
				SceneMgr.Get().RegisterSceneUnloadedEvent(OnSceneUnloaded);
			}
			else
			{
				Debug.LogError("Hub.Start Error - SceneMgr did not register scene unload event");
			}
		}
		TavernBrawlManager tavernBrawlManager = TavernBrawlManager.Get();
		if (tavernBrawlManager != null)
		{
			tavernBrawlManager.OnTavernBrawlUpdated += DoTavernBrawlAnimsCB;
			tavernBrawlManager.OnSessionLimitRaised += MaybeDoTavernBrawlLimitRaisedAlert;
		}
		else
		{
			Debug.LogError("Hub.Start Error - TavernBrawlManager did not register certain events");
		}
		FiresideGatheringManager firesideGatheringManager = FiresideGatheringManager.Get();
		if (firesideGatheringManager != null)
		{
			firesideGatheringManager.OnSignClosed += DoTavernBrawlAnimsCBForce;
		}
		else
		{
			Debug.LogError("Hub.Start Error - FiresideGatheringManager did not register OnSignClosed");
		}
		NetCache netCache = NetCache.Get();
		if (netCache != null)
		{
			netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheFeatures), DoTavernBrawlAnimsCB);
			netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheHeroLevels), DoTavernBrawlAnimsCB);
		}
		else
		{
			Debug.LogError("Hub.Start Error - NetCache did not register certain events");
		}
		CollectionManager.OnCollectionManagerReady += OnCollectionLoaded;
	}

	private void UnregisterEventListeners()
	{
		if (HearthstoneServices.TryGet<TavernBrawlManager>(out var service))
		{
			service.OnTavernBrawlUpdated -= DoTavernBrawlAnimsCB;
		}
		if (HearthstoneServices.TryGet<FiresideGatheringManager>(out var service2))
		{
			service2.OnSignClosed -= DoTavernBrawlAnimsCBForce;
		}
		if (HearthstoneServices.TryGet<NetCache>(out var service3))
		{
			service3.RemoveUpdatedListener(typeof(NetCache.NetCacheFeatures), DoTavernBrawlAnimsCB);
			service3.RemoveUpdatedListener(typeof(NetCache.NetCacheHeroLevels), DoTavernBrawlAnimsCB);
		}
		CollectionManager.OnCollectionManagerReady -= OnCollectionLoaded;
	}

	private void ShowHubStartNotifications()
	{
		PopupDisplayManager.Get().ReadyToShowPopups();
		if (!Network.ShouldBeConnectedToAurora())
		{
			return;
		}
		if (!Options.Get().GetBool(Option.HAS_SEEN_HUB, defaultVal: false) && UserAttentionManager.CanShowAttentionGrabber("Hub.Start:" + Option.HAS_SEEN_HUB))
		{
			StartCoroutine(DoFirstTimeHubWelcome());
		}
		else if (!Options.Get().GetBool(Option.HAS_SEEN_100g_REMINDER, defaultVal: false))
		{
			NetCache.NetCacheGoldBalance netObject = NetCache.Get().GetNetObject<NetCache.NetCacheGoldBalance>();
			if (netObject == null)
			{
				Debug.LogError("Hub.Start Error - NetCache.NetCacheGoldBalance is null");
			}
			if (netObject.GetTotal() >= 100 && UserAttentionManager.CanShowAttentionGrabber("Hub.Start:" + Option.HAS_SEEN_100g_REMINDER))
			{
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_FIRST_100_GOLD"), "VO_INNKEEPER_FIRST_100_GOLD.prefab:c6a50337099a454488acd96d2f37320f");
				Options.Get().SetBool(Option.HAS_SEEN_100g_REMINDER, val: true);
			}
		}
		else if (TavernBrawlManager.Get().IsFirstTimeSeeingThisFeature)
		{
			DoTavernBrawlIntroVO();
		}
		if (TavernBrawlManager.Get().IsFirstTimeSeeingCurrentSeason && UserAttentionManager.CanShowAttentionGrabber("Hub.TavernBrawl.IsFirstTimeSeeingCurrentSeason") && !s_hasAlreadyShownTBAnimation)
		{
			StartCoroutine(DoTavernBrawlAnims());
		}
	}

	private static void DoTavernBrawlIntroVO()
	{
		if (!NotificationManager.Get().HasSoundPlayedThisSession("VO_INNKEEPER_TAVERNBRAWL_PUSH_32.prefab:4f57cd2af5fe5194fbc46c91171ab135"))
		{
			Action<int> finishCallback = delegate
			{
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_TAVERNBRAWL_DESC1_29"), "VO_INNKEEPER_TAVERNBRAWL_DESC1_29.prefab:44d1a6b322c3dcf4c950e68eb4f4a05f");
			};
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_TAVERNBRAWL_PUSH_32"), "VO_INNKEEPER_TAVERNBRAWL_PUSH_32.prefab:4f57cd2af5fe5194fbc46c91171ab135", finishCallback);
			NotificationManager.Get().ForceAddSoundToPlayedList("VO_INNKEEPER_TAVERNBRAWL_PUSH_32.prefab:4f57cd2af5fe5194fbc46c91171ab135");
		}
	}

	private void DoTavernBrawlAnimsCB()
	{
		StartCoroutine(DoTavernBrawlAnims());
	}

	private void DoTavernBrawlAnimsCBForce()
	{
		StartCoroutine(DoTavernBrawlAnims(force: true));
	}

	private IEnumerator DoTavernBrawlAnims(bool force = false)
	{
		Box theBox = Box.Get();
		if (!theBox.UpdateTavernBrawlButtonState(highlightAllowed: true))
		{
			yield break;
		}
		TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
		if (true)
		{
			bool isFirstTimeSeeingCurrentSeason = TavernBrawlManager.Get().IsFirstTimeSeeingCurrentSeason;
			if (isFirstTimeSeeingCurrentSeason || theBox.IsTavernBrawlButtonDeactivated || force)
			{
				theBox.UpdateTavernBrawlButtonState(highlightAllowed: false);
				if (isFirstTimeSeeingCurrentSeason)
				{
					float length = theBox.m_TavernBrawlButtonVisual.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
					yield return new WaitForSeconds(length);
				}
				if (TavernBrawlManager.Get().IsFirstTimeSeeingThisFeature)
				{
					DoTavernBrawlIntroVO();
				}
				if (theBox.m_tavernBrawlActivateSound != string.Empty)
				{
					SoundManager.Get().LoadAndPlay(theBox.m_tavernBrawlActivateSound);
				}
				theBox.PlayTavernBrawlButtonActivation(activate: true);
				yield return new WaitForSeconds(0.65f);
				CameraShakeMgr.Shake(Camera.main, new Vector3(0.5f, 0.5f, 0.5f), 0.3f);
				theBox.UpdateTavernBrawlButtonState(highlightAllowed: true);
			}
		}
		else if (!theBox.IsTavernBrawlButtonDeactivated)
		{
			if (theBox.m_tavernBrawlDeactivateSound != string.Empty)
			{
				SoundManager.Get().LoadAndPlay(Box.Get().m_tavernBrawlDeactivateSound);
			}
			theBox.PlayTavernBrawlButtonActivation(activate: false);
		}
		s_hasAlreadyShownTBAnimation = true;
	}

	private void MaybeDoTavernBrawlLimitRaisedAlert(int lastSeenLimit, int newLimit)
	{
		int numSessionsAvailableForPurchase = TavernBrawlManager.Get().NumSessionsAvailableForPurchase;
		if (numSessionsAvailableForPurchase == newLimit - lastSeenLimit)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
			popupInfo.m_headerText = GameStrings.Get("GLUE_HEROIC_BRAWL_SESSION_LIMIT_ALERT_TITLE");
			popupInfo.m_text = GameStrings.Format("GLUE_HEROIC_BRAWL_SESSION_LIMIT_ALERT_LIMIT_RAISED", numSessionsAvailableForPurchase);
			DialogManager.Get().ShowPopup(popupInfo);
		}
	}

	private void Update()
	{
		Network.Get().ProcessNetwork();
	}

	public override void Unload()
	{
		StoreManager.Get().RemoveSuccessfulPurchaseListener(OnAdventureBundlePurchase);
		HideTooltipNotification();
		Box box = Box.Get();
		if (box != null)
		{
			box.m_QuestLogButton.RemoveEventListener(UIEventType.RELEASE, OnButtonReleasedHideTooltipNotification);
			box.m_StoreButton.RemoveEventListener(UIEventType.RELEASE, OnButtonReleasedHideTooltipNotification);
			box.RemoveButtonPressListener(OnBoxButtonPressed);
		}
	}

	private void OnSceneUnloaded(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		SpecialEventType activeEventType = SpecialEventManager.Get().GetActiveEventType();
		if (activeEventType != 0)
		{
			SpecialEventManager.Get().Visuals.UnloadEvent(activeEventType);
		}
		SceneMgr.Get().UnregisterSceneUnloadedEvent(OnSceneUnloaded);
	}

	private void OnBoxButtonPressed(Box.ButtonType buttonType, object userData)
	{
		switch (buttonType)
		{
		case Box.ButtonType.TOURNAMENT:
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.TOURNAMENT);
			Tournament.Get().NotifyOfBoxTransitionStart();
			break;
		case Box.ButtonType.FORGE:
			HandleArenaButtonPressed();
			break;
		case Box.ButtonType.GAME_MODES:
			HandleGameModesButtonPressed();
			break;
		case Box.ButtonType.BACON:
			HandleBaconButtonPressed();
			break;
		case Box.ButtonType.ADVENTURE:
			AdventureConfig.Get().ResetSubScene();
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.ADVENTURE);
			break;
		case Box.ButtonType.COLLECTION:
			CollectionManager.Get().NotifyOfBoxTransitionStart();
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.COLLECTIONMANAGER);
			break;
		case Box.ButtonType.OPEN_PACKS:
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.PACKOPENING);
			break;
		case Box.ButtonType.TAVERN_BRAWL:
			HandleTavernBrawlButtonPressed();
			break;
		case Box.ButtonType.SET_ROTATION:
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.TOURNAMENT);
			Tournament.Get().NotifyOfBoxTransitionStart();
			break;
		}
	}

	private void HandleGameModesButtonPressed()
	{
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.GAME_MODE);
	}

	private void HandleArenaButtonPressed()
	{
		ulong secondsUntilEndOfSeason = DraftManager.Get().SecondsUntilEndOfSeason;
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		if (!DraftManager.Get().HasActiveRun && secondsUntilEndOfSeason <= netObject.ArenaClosedToNewSessionsSeconds)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_ARENA_1ST_TIME_HEADER");
			popupInfo.m_text = GameStrings.Get("GLUE_ARENA_SIGNUPS_CLOSED");
			popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo);
		}
		else
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.DRAFT);
		}
	}

	private void HandleBaconButtonPressed()
	{
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.BACON);
	}

	private void HandleTavernBrawlButtonPressed()
	{
		SelectTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
	}

	private void ShowTavernBrawlSelectionDialog()
	{
		DialogManager.Get().ShowTavernBrawlChoiceDialog(SelectTavernBrawl);
	}

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
		}
		else if (TavernBrawlManager.Get().IsPlayerAtSessionMaxForCurrentTavernBrawl)
		{
			AlertPopup.PopupInfo popupInfo2 = new AlertPopup.PopupInfo();
			popupInfo2.m_headerText = GameStrings.Get("GLOBAL_HEROIC_BRAWL");
			popupInfo2.m_text = GameStrings.Get("GLUE_HEROIC_BRAWL_SESSION_LIMIT_ALERT_LIMIT_HIT");
			popupInfo2.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
			popupInfo2.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo2);
		}
		else
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.TAVERN_BRAWL);
		}
	}

	private IEnumerator DoFirstTimeHubWelcome()
	{
		Box box = Box.Get();
		while (box == null || box.IsBusy() || box.m_SoloAdventuresButton == null || !box.m_SoloAdventuresButton.IsEnabled() || box.GetState() != Box.State.HUB_WITH_DRAWER || box.GetBoxCamera() == null || box.GetBoxCamera().GetState() != BoxCamera.State.CLOSED_WITH_DRAWER)
		{
			yield return HumanInteractionPollSpan;
			box = Box.Get();
		}
		StoreManager storeManager = StoreManager.Get();
		QuestLog questLog = QuestLog.Get();
		while ((storeManager != null && storeManager.IsShown()) || (questLog != null && questLog.IsShown()))
		{
			yield return HumanInteractionPollSpan;
			storeManager = StoreManager.Get();
			questLog = QuestLog.Get();
		}
		AchieveManager achieveManager = AchieveManager.Get();
		while ((achieveManager != null && achieveManager.HasQuestsToShow(onlyNewlyActive: true)) || WelcomeQuests.Get() != null)
		{
			yield return HumanInteractionPollSpan;
			achieveManager = AchieveManager.Get();
		}
		PopupDisplayManager popupDisplayManager = PopupDisplayManager.Get();
		while (popupDisplayManager != null && popupDisplayManager.IsShowing)
		{
			yield return HumanInteractionPollSpan;
			popupDisplayManager = PopupDisplayManager.Get();
		}
		NotificationManager notificationManager = NotificationManager.Get();
		if (notificationManager != null)
		{
			notificationManager.CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_1ST_HUB_06"), "VO_INNKEEPER_1ST_HUB_06.prefab:9774392944a21424788286f80d401d8c", 3f);
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				m_PracticeNotification = notificationManager.CreatePopupText(UserAttentionBlocker.NONE, new Vector3(-30.46f, 33.5f, 3f), 25f * Vector3.one, GameStrings.Get("GLUE_PRACTICE_HINT"));
			}
			else
			{
				m_PracticeNotification = notificationManager.CreatePopupText(UserAttentionBlocker.NONE, new Vector3(-33.62785f, 33.52365f, 3f), 15f * Vector3.one, GameStrings.Get("GLUE_PRACTICE_HINT"));
			}
			m_PracticeNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
		}
		Options.Get().SetBool(Option.HAS_SEEN_HUB, val: true);
		AdTrackingManager adTrackingManager = AdTrackingManager.Get();
		if (adTrackingManager != null)
		{
			adTrackingManager.TrackFirstLogin();
		}
		else
		{
			Debug.LogWarning("AdTrackingManager was not initialized during Hub.DoFirstTimeHubWelcome()");
		}
	}

	private void OnAdventureBundlePurchase(Network.Bundle bundle, PaymentMethod purchaseMethod)
	{
		if (bundle == null || bundle.Items == null)
		{
			return;
		}
		foreach (Network.BundleItem item in bundle.Items)
		{
			if (item.ItemType == ProductType.PRODUCT_TYPE_NAXX)
			{
				Options.Get().SetBool(Option.BUNDLE_JUST_PURCHASE_IN_HUB, val: true);
				AdventureConfig.Get().SetSelectedAdventureMode(AdventureDbId.NAXXRAMAS, AdventureModeDbId.LINEAR);
				break;
			}
		}
	}

	private void OnButtonReleasedHideTooltipNotification(UIEvent e)
	{
		HideTooltipNotification();
	}

	private void OnCollectionLoaded()
	{
		if (CollectionManager.Get().GetPreconDeck(TAG_CLASS.MAGE) == null)
		{
			Error.AddFatal(FatalErrorReason.ACCOUNT_SETUP_ERROR, "GLOBAL_ERROR_NO_MAGE_PRECON");
		}
	}

	private void HideTooltipNotification()
	{
		if (m_PracticeNotification != null)
		{
			NotificationManager.Get().DestroyNotification(m_PracticeNotification, 0f);
		}
	}
}
