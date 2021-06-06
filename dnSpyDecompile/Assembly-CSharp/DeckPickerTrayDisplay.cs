using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using Blizzard.Telemetry.WTCG.Client;
using Hearthstone.Core;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

// Token: 0x020002A9 RID: 681
[CustomEditClass]
public class DeckPickerTrayDisplay : AbsDeckPickerTrayDisplay
{
	// Token: 0x0600228A RID: 8842 RVA: 0x000AAD74 File Offset: 0x000A8F74
	public override void Awake()
	{
		base.Awake();
		SoundManager.Get().Load("hero_panel_slide_on.prefab:236147a924d7cb442872b46dddd56132");
		SoundManager.Get().Load("hero_panel_slide_off.prefab:ed410a050e783564384ca51e701ede4d");
		if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY)
		{
			this.m_delayButtonAnims = true;
			LoadingScreen.Get().RegisterFinishedTransitionListener(new LoadingScreen.FinishedTransitionCallback(this.OnTransitionFromGameplayFinished));
		}
		SceneMgr.Get().RegisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
		DeckPickerTrayDisplay.s_instance = this;
		if (this.m_collectionButton != null)
		{
			if (this.IsDeckSharingActive())
			{
				this.m_collectionButton.gameObject.SetActive(false);
			}
			else
			{
				this.m_collectionButton.gameObject.SetActive(true);
				this.SetCollectionButtonEnabled(this.ShouldShowCollectionButton());
				if (this.m_collectionButton.IsEnabled())
				{
					TelemetryWatcher.WatchFor(TelemetryWatcherWatchType.CollectionManagerFromDeckPicker);
					this.m_collectionButton.SetText(GameStrings.Get("GLUE_MY_COLLECTION"));
					this.m_collectionButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.CollectionButtonPress));
				}
			}
		}
		if (this.m_DeckShareRequestButton != null)
		{
			if (this.IsDeckSharingActive())
			{
				this.m_DeckShareRequestButton.gameObject.SetActive(true);
				this.EnableRequestDeckShareButton(true);
				this.m_DeckShareRequestButton.SetText(GameStrings.Get("GLUE_DECK_SHARE_BUTTON_BORROW_DECKS"));
				this.m_DeckShareRequestButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.RequestDeckShareButtonPress));
				this.m_DeckShareRequestButton.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.RequestDeckShareButtonOver));
				this.m_DeckShareRequestButton.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.RequestDeckShareButtonOut));
			}
			else
			{
				this.m_DeckShareRequestButton.gameObject.SetActive(false);
			}
		}
		if (this.m_DeckShareGlowOutQuad != null)
		{
			this.m_DeckShareGlowOutQuad.SetActive(false);
		}
		this.m_xpBar = UnityEngine.Object.Instantiate<HeroXPBar>(this.m_xpBarPrefab);
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		this.m_xpBar.m_soloLevelLimit = ((netObject == null) ? 60 : netObject.XPSoloLimit);
	}

	// Token: 0x0600228B RID: 8843 RVA: 0x000AAF74 File Offset: 0x000A9174
	private void Start()
	{
		Navigation.PushIfNotOnTop(new Navigation.NavigateBackHandler(DeckPickerTrayDisplay.OnNavigateBack));
		GameObject gameObject = this.m_leftArrowNestedPrefab.PrefabGameObject(false);
		this.m_leftArrow = gameObject.GetComponent<UIBButton>();
		this.m_leftArrow.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnShowPreviousPage));
		gameObject = this.m_rightArrowNestedPrefab.PrefabGameObject(false);
		this.m_rightArrow = gameObject.GetComponent<UIBButton>();
		this.m_rightArrow.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnShowNextPage));
		this.UpdatePageArrows();
		this.m_currentMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo().GetCurrentMedalForCurrentFormatType();
		this.m_formatTypePickerWidget = WidgetInstance.Create(DeckPickerTrayDisplay.FORMAT_TYPE_PICKER_POPUP_PREFAB, false);
		this.m_formatTypePickerWidget.Hide();
		this.m_formatTypePickerWidget.RegisterReadyListener(delegate(object _)
		{
			this.OnFormatTypePickerPopupReady();
		}, null, true);
		this.m_formatTypePickerWidget.RegisterEventListener(new Widget.EventListenerDelegate(this.OnFormatTypePickerEvent));
	}

	// Token: 0x0600228C RID: 8844 RVA: 0x000AB064 File Offset: 0x000A9264
	protected override void OnDestroy()
	{
		base.OnDestroy();
		this.HideDemoQuotes();
		if (TournamentDisplay.Get() != null)
		{
			TournamentDisplay.Get().RemoveMedalChangedListener(new TournamentDisplay.DelMedalChanged(this.OnMedalChanged));
		}
		if (FriendChallengeMgr.Get() != null && DeckPickerTrayDisplay.Get() != null)
		{
			FriendChallengeMgr.Get().RemoveChangedListener(new FriendChallengeMgr.ChangedCallback(DeckPickerTrayDisplay.Get().OnFriendChallengeChanged));
		}
		if (SceneMgr.Get() != null && SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.FRIENDLY && SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY)
		{
			FriendChallengeMgr.Get().CancelChallenge();
		}
		DeckPickerTrayDisplay.s_instance = null;
	}

	// Token: 0x0600228D RID: 8845 RVA: 0x000AB101 File Offset: 0x000A9301
	public static DeckPickerTrayDisplay Get()
	{
		return DeckPickerTrayDisplay.s_instance;
	}

	// Token: 0x0600228E RID: 8846 RVA: 0x000AB108 File Offset: 0x000A9308
	public void SetInHeroPicker()
	{
		this.m_inHeroPicker = true;
	}

	// Token: 0x0600228F RID: 8847 RVA: 0x000AB111 File Offset: 0x000A9311
	public void OverridePlayButtonCallback(UIEvent.Handler callback)
	{
		if (this.m_playButton != null)
		{
			this.m_playButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnPlayGameButtonReleased));
			this.m_playButton.AddEventListener(UIEventType.RELEASE, callback);
		}
	}

	// Token: 0x06002290 RID: 8848 RVA: 0x000AB149 File Offset: 0x000A9349
	public bool IsShowingCustomDecks()
	{
		return this.m_deckPickerMode == DeckPickerMode.CUSTOM;
	}

	// Token: 0x06002291 RID: 8849 RVA: 0x000AB154 File Offset: 0x000A9354
	public void SuckInFinished()
	{
		this.HideRandomDeckPickerTray();
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
	}

	// Token: 0x06002292 RID: 8850 RVA: 0x000AB169 File Offset: 0x000A9369
	private void OnShowNextPage(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("hero_panel_slide_off.prefab:ed410a050e783564384ca51e701ede4d");
		this.ShowNextPage(false);
	}

	// Token: 0x06002293 RID: 8851 RVA: 0x000AB188 File Offset: 0x000A9388
	public override void ResetCurrentMode()
	{
		if (this.m_selectedCustomDeckBox != null)
		{
			this.SetPlayButtonEnabled(true);
			this.SetHeroRaised(true);
		}
		else if (this.m_selectedHeroButton != null)
		{
			this.SetHeroRaised(true);
			this.SetPlayButtonEnabled(!this.m_selectedHeroButton.IsLocked());
		}
		this.SetHeroButtonsEnabled(true);
	}

	// Token: 0x06002294 RID: 8852 RVA: 0x000AB1E3 File Offset: 0x000A93E3
	public int GetSelectedHeroLevel()
	{
		if (this.m_selectedHeroButton == null)
		{
			return 0;
		}
		return GameUtils.GetHeroLevel(this.m_selectedHeroButton.GetEntityDef().GetClass()).CurrentLevel.Level;
	}

	// Token: 0x06002295 RID: 8853 RVA: 0x000AB214 File Offset: 0x000A9414
	public void ToggleRankedDetailsTray(bool shown)
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		this.m_rankedDetailsTray.ToggleTraySlider(shown, null, true);
	}

	// Token: 0x06002296 RID: 8854 RVA: 0x000AB231 File Offset: 0x000A9431
	public override long GetSelectedDeckID()
	{
		if (null != this.m_selectedCustomDeckBox)
		{
			return this.m_selectedCustomDeckBox.GetDeckID();
		}
		return base.GetSelectedDeckID();
	}

	// Token: 0x06002297 RID: 8855 RVA: 0x000AB253 File Offset: 0x000A9453
	public CollectionDeck GetSelectedCollectionDeck()
	{
		if (!(this.m_selectedCustomDeckBox == null))
		{
			return this.m_selectedCustomDeckBox.GetCollectionDeck();
		}
		return null;
	}

	// Token: 0x06002298 RID: 8856 RVA: 0x000AB270 File Offset: 0x000A9470
	public void UpdateCreateDeckText()
	{
		string key;
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			key = ((TavernBrawlManager.Get().CurrentSeasonBrawlMode == TavernBrawlMode.TB_MODE_HEROIC) ? "GLOBAL_HEROIC_BRAWL" : "GLOBAL_TAVERN_BRAWL");
		}
		else
		{
			PegasusShared.FormatType formatType = Options.GetFormatType();
			switch (formatType)
			{
			case PegasusShared.FormatType.FT_WILD:
				key = "GLUE_CREATE_WILD_DECK";
				break;
			case PegasusShared.FormatType.FT_STANDARD:
				key = "GLUE_CREATE_STANDARD_DECK";
				break;
			case PegasusShared.FormatType.FT_CLASSIC:
				key = "GLUE_CREATE_CLASSIC_DECK";
				break;
			default:
				Debug.LogError("DeckPickerTrayDisplay.UpdateCreateDeckText called in unsupported format type: " + formatType.ToString());
				base.SetHeaderText("UNSUPPORTED DECK TEXT " + formatType.ToString());
				return;
			}
		}
		base.SetHeaderText(GameStrings.Get(key));
	}

	// Token: 0x06002299 RID: 8857 RVA: 0x000AB320 File Offset: 0x000A9520
	public bool UpdateRankedClassWinsPlate()
	{
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.TOURNAMENT || !(this.m_heroActor != null) || this.m_heroActor.GetEntityDef() == null || !Options.GetInRankedPlayMode())
		{
			this.m_rankedWinsPlate.SetActive(false);
			return false;
		}
		string heroCardID = this.m_heroActor.GetEntityDef().GetCardId();
		if (this.m_heroActor.GetEntityDef().GetCardSet() == TAG_CARD_SET.HERO_SKINS)
		{
			heroCardID = CollectionManager.GetHeroCardId(this.m_heroActor.GetEntityDef().GetClass(), CardHero.HeroType.VANILLA);
		}
		RankedWinsPlate component = this.m_rankedWinsPlate.GetComponent<RankedWinsPlate>();
		component.TooltipString = GameStrings.Get("GLUE_TOOLTIP_GOLDEN_WINS_DESC");
		global::Achievement unlockGoldenHeroAchievement = AchieveManager.Get().GetUnlockGoldenHeroAchievement(heroCardID, TAG_PREMIUM.GOLDEN);
		global::Achievement unlockPremiumHeroAchievement = AchieveManager.Get().GetUnlockPremiumHeroAchievement(this.m_heroActor.GetEntityDef().GetClass());
		int num = (unlockGoldenHeroAchievement != null) ? unlockGoldenHeroAchievement.Progress : 0;
		int num2 = (unlockGoldenHeroAchievement != null) ? unlockGoldenHeroAchievement.MaxProgress : 0;
		if (unlockGoldenHeroAchievement != null && unlockGoldenHeroAchievement.IsCompleted())
		{
			num = ((unlockPremiumHeroAchievement != null) ? unlockPremiumHeroAchievement.Progress : num);
			num2 = ((unlockPremiumHeroAchievement != null) ? unlockPremiumHeroAchievement.MaxProgress : num2);
			component.TooltipString = GameStrings.Format("GLUE_TOOLTIP_ALTERNATE_WINS_DESC", new object[]
			{
				num2
			});
		}
		if (num == 0)
		{
			this.m_rankedWinsPlate.SetActive(false);
			return false;
		}
		if (num >= num2)
		{
			this.m_rankedWins.Text = GameStrings.Format(UniversalInputManager.UsePhoneUI ? "GLOBAL_HERO_WINS_PAST_MAX_PHONE" : "GLOBAL_HERO_WINS_PAST_MAX", new object[]
			{
				num
			});
			component.TooltipEnabled = false;
		}
		else
		{
			this.m_rankedWins.Text = GameStrings.Format(UniversalInputManager.UsePhoneUI ? "GLOBAL_HERO_WINS_PHONE" : "GLOBAL_HERO_WINS", new object[]
			{
				num,
				num2
			});
			component.TooltipEnabled = true;
		}
		this.m_rankedWinsPlate.SetActive(true);
		return true;
	}

	// Token: 0x0600229A RID: 8858 RVA: 0x000AB50C File Offset: 0x000A970C
	public override void OnServerGameStarted()
	{
		base.OnServerGameStarted();
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.ADVENTURE)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			AdventureData.Adventuresubscene currentSubScene = adventureConfig.CurrentSubScene;
			if (currentSubScene == AdventureData.Adventuresubscene.MISSION_DECK_PICKER && DemoMgr.Get().GetMode() != DemoMode.BLIZZCON_2015)
			{
				adventureConfig.SubSceneGoBack(false);
			}
		}
	}

	// Token: 0x0600229B RID: 8859 RVA: 0x000AB554 File Offset: 0x000A9754
	public override void HandleGameStartupFailure()
	{
		base.HandleGameStartupFailure();
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode != SceneMgr.Mode.TOURNAMENT)
		{
			if (mode == SceneMgr.Mode.ADVENTURE && AdventureConfig.Get().CurrentSubScene == AdventureData.Adventuresubscene.PRACTICE)
			{
				PracticePickerTrayDisplay.Get().OnGameDenied();
				return;
			}
		}
		else if (PresenceMgr.Get().CurrentStatus == Global.PresenceStatus.PLAY_QUEUE)
		{
			PresenceMgr.Get().SetPrevStatus();
		}
	}

	// Token: 0x0600229C RID: 8860 RVA: 0x000AB5AC File Offset: 0x000A97AC
	public void SetHeroDetailsTrayToIgnoreFullScreenEffects(bool ignoreEffects)
	{
		if (this.m_hierarchyDetails == null)
		{
			return;
		}
		if (ignoreEffects)
		{
			SceneUtils.ReplaceLayer(this.m_hierarchyDetails, GameLayer.IgnoreFullScreenEffects, this.m_defaultDetailsLayer);
			return;
		}
		SceneUtils.ReplaceLayer(this.m_hierarchyDetails, this.m_defaultDetailsLayer, GameLayer.IgnoreFullScreenEffects);
	}

	// Token: 0x0600229D RID: 8861 RVA: 0x000AB5E8 File Offset: 0x000A97E8
	public void ShowClickedStandardDeckInClassicPopup()
	{
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.TOURNAMENT && SceneMgr.Get().GetMode() != SceneMgr.Mode.FRIENDLY && SceneMgr.Get().GetMode() != SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			return;
		}
		if (this.m_switchFormatPopup == null && this.m_innkeeperQuote == null)
		{
			if (!this.m_switchFormatButton.IsCovered())
			{
				Action<int> b = delegate(int groupId)
				{
					this.m_switchFormatPopup = null;
				};
				this.m_switchFormatPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.SET_ROTATION_INTRO, this.m_Switch_Format_Notification_Bone.position, this.m_Switch_Format_Notification_Bone.localScale, GameStrings.Get("GLUE_TOURNAMENT_SWITCH_TO_STANDARD"), true, NotificationManager.PopupTextType.BASIC);
				if (this.m_switchFormatPopup != null)
				{
					Notification.PopUpArrowDirection direction = UniversalInputManager.UsePhoneUI ? Notification.PopUpArrowDirection.RightUp : Notification.PopUpArrowDirection.Up;
					this.m_switchFormatPopup.ShowPopUpArrow(direction);
					Notification switchFormatPopup = this.m_switchFormatPopup;
					switchFormatPopup.OnFinishDeathState = (Action<int>)Delegate.Combine(switchFormatPopup.OnFinishDeathState, b);
				}
			}
			Action<int> finishCallback = delegate(int groupId)
			{
				if (this.m_switchFormatButton != null)
				{
					NotificationManager.Get().DestroyNotification(this.m_switchFormatPopup, 0f);
				}
				this.m_innkeeperQuote = null;
			};
			this.m_innkeeperQuote = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.SET_ROTATION_INTRO, DeckPickerTrayDisplay.INNKEEPER_QUOTE_POS, GameStrings.Get("VO_INNKEEPER_STANDARD_DECK_WARNING"), "", 0f, finishCallback, false);
		}
	}

	// Token: 0x0600229E RID: 8862 RVA: 0x000AB710 File Offset: 0x000A9910
	public void ShowClickedWildDeckInClassicPopup()
	{
		this.ShowClickedWildDeckInStandardPopup();
	}

	// Token: 0x0600229F RID: 8863 RVA: 0x000AB718 File Offset: 0x000A9918
	public void ShowClickedWildDeckInStandardPopup()
	{
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.TOURNAMENT && SceneMgr.Get().GetMode() != SceneMgr.Mode.FRIENDLY && SceneMgr.Get().GetMode() != SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			return;
		}
		if (this.m_switchFormatPopup == null && this.m_innkeeperQuote == null)
		{
			if (!this.m_switchFormatButton.IsCovered())
			{
				base.StopCoroutine("ShowSwitchToWildTutorialAfterTransitionsComplete");
				Action<int> b = delegate(int groupId)
				{
					this.m_switchFormatPopup = null;
				};
				this.m_switchFormatPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.SET_ROTATION_INTRO, this.m_Switch_Format_Notification_Bone.position, this.m_Switch_Format_Notification_Bone.localScale, GameStrings.Get("GLUE_TOURNAMENT_SWITCH_TO_WILD"), true, NotificationManager.PopupTextType.BASIC);
				if (this.m_switchFormatPopup != null)
				{
					Notification.PopUpArrowDirection direction = UniversalInputManager.UsePhoneUI ? Notification.PopUpArrowDirection.RightUp : Notification.PopUpArrowDirection.Up;
					this.m_switchFormatPopup.ShowPopUpArrow(direction);
					Notification switchFormatPopup = this.m_switchFormatPopup;
					switchFormatPopup.OnFinishDeathState = (Action<int>)Delegate.Combine(switchFormatPopup.OnFinishDeathState, b);
				}
			}
			Action<int> finishCallback = delegate(int groupId)
			{
				if (this.m_switchFormatButton != null)
				{
					NotificationManager.Get().DestroyNotification(this.m_switchFormatPopup, 0f);
				}
				this.m_innkeeperQuote = null;
			};
			this.m_innkeeperQuote = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.SET_ROTATION_INTRO, DeckPickerTrayDisplay.INNKEEPER_QUOTE_POS, GameStrings.Get("VO_INNKEEPER_WILD_DECK_WARNING"), "VO_INNKEEPER_Male_Dwarf_SetRotation_32.prefab:3377790e79f276a4484ed43edde342c4", 0f, finishCallback, false);
		}
	}

	// Token: 0x060022A0 RID: 8864 RVA: 0x000AB84C File Offset: 0x000A9A4C
	public void ShowClickedClassicDeckInNonClassicPopup()
	{
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.TOURNAMENT && SceneMgr.Get().GetMode() != SceneMgr.Mode.FRIENDLY && SceneMgr.Get().GetMode() != SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			return;
		}
		if (this.m_switchFormatPopup == null && this.m_innkeeperQuote == null)
		{
			Action<int> finishCallback = delegate(int groupId)
			{
				if (this.m_switchFormatButton != null)
				{
					NotificationManager.Get().DestroyNotification(this.m_switchFormatPopup, 0f);
				}
				this.m_innkeeperQuote = null;
			};
			this.m_innkeeperQuote = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.SET_ROTATION_INTRO, DeckPickerTrayDisplay.INNKEEPER_QUOTE_POS, GameStrings.Get("VO_INNKEEPER_CLASSIC_DECK_WARNING"), "VO_Innkeeper_Male_Dwarf_ClassicMode_02.prefab:8cf46784be9929d4d84c40dc428df680", 0f, finishCallback, false);
		}
	}

	// Token: 0x060022A1 RID: 8865 RVA: 0x000AB8D8 File Offset: 0x000A9AD8
	public void ShowSwitchToWildTutorialIfNecessary()
	{
		if (this.m_switchFormatPopup != null)
		{
			return;
		}
		if (!UserAttentionManager.CanShowAttentionGrabber(UserAttentionBlocker.SET_ROTATION_INTRO, "DeckPickerTrayDisplay.ShowSwitchToWildTutorialIfNecessary"))
		{
			return;
		}
		if (Options.GetFormatType() == PegasusShared.FormatType.FT_WILD)
		{
			Options.Get().SetBool(Option.SHOW_SWITCH_TO_WILD_ON_CREATE_DECK, false);
			Options.Get().SetBool(Option.SHOW_SWITCH_TO_WILD_ON_PLAY_SCREEN, false);
		}
		bool flag = false;
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (Options.Get().GetBool(Option.SHOW_SWITCH_TO_WILD_ON_CREATE_DECK) && mode == SceneMgr.Mode.COLLECTIONMANAGER)
		{
			flag = true;
			Options.Get().SetBool(Option.SHOW_SWITCH_TO_WILD_ON_CREATE_DECK, false);
		}
		if (Options.Get().GetBool(Option.SHOW_SWITCH_TO_WILD_ON_PLAY_SCREEN) && mode == SceneMgr.Mode.TOURNAMENT)
		{
			flag = true;
			Options.Get().SetBool(Option.SHOW_SWITCH_TO_WILD_ON_PLAY_SCREEN, false);
		}
		if (flag)
		{
			base.StartCoroutine("ShowSwitchToWildTutorialAfterTransitionsComplete");
		}
	}

	// Token: 0x060022A2 RID: 8866 RVA: 0x000AB994 File Offset: 0x000A9B94
	private IEnumerator ShowSwitchToWildTutorialAfterTransitionsComplete()
	{
		yield return new WaitForSeconds(1f);
		this.m_switchFormatPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.SET_ROTATION_INTRO, this.m_Switch_Format_Notification_Bone, GameStrings.Get("GLUE_TOURNAMENT_SWITCH_TO_WILD"), true, NotificationManager.PopupTextType.BASIC);
		Notification.PopUpArrowDirection direction = UniversalInputManager.UsePhoneUI ? Notification.PopUpArrowDirection.RightUp : Notification.PopUpArrowDirection.Up;
		this.m_switchFormatPopup.ShowPopUpArrow(direction);
		this.m_switchFormatPopup.PulseReminderEveryXSeconds(3f);
		NotificationManager.Get().DestroyNotification(this.m_switchFormatPopup, 6f);
		yield break;
	}

	// Token: 0x060022A3 RID: 8867 RVA: 0x000AB9A4 File Offset: 0x000A9BA4
	public void SkipHeroSelectionAndCloseTray()
	{
		if (this.m_playButton != null)
		{
			this.m_backButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBackButtonReleased));
			this.m_playButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnPlayGameButtonReleased));
		}
		this.SetPlayButtonEnabled(false);
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(DeckPickerTrayDisplay.OnNavigateBack));
		if (this.m_slidingTray != null)
		{
			this.m_slidingTray.ToggleTraySlider(false, null, true);
		}
		if (HeroPickerDisplay.Get() != null)
		{
			HeroPickerDisplay.Get().HideTray(UniversalInputManager.UsePhoneUI ? 0.25f : 0f);
		}
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null && !collectionManagerDisplay.GetHeroPickerDisplay().IsShown())
		{
			CollectionManager.Get().GetCollectibleDisplay().EnableInput(false);
		}
		CollectionDeckTray.Get().RegisterModeSwitchedListener(new DeckTray.ModeSwitched(this.OnModeSwitchedAfterSkippingHeroSelection));
	}

	// Token: 0x060022A4 RID: 8868 RVA: 0x000ABAA3 File Offset: 0x000A9CA3
	public void ShowBonusStarsPopup()
	{
		base.OnPopupShown();
		DialogManager.Get().ShowBonusStarsPopup(this.GetBonusStarsPopupDataModel(), new Action(this.PlayEnterModeDialogues));
	}

	// Token: 0x060022A5 RID: 8869 RVA: 0x000ABAC8 File Offset: 0x000A9CC8
	private bool ShouldShowBonusStarsPopUp()
	{
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.LOGIN && (SceneMgr.Get().GetMode() != SceneMgr.Mode.TOURNAMENT || SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY))
		{
			return false;
		}
		if (this.m_currentMedalInfo.starsPerWin < 2)
		{
			return false;
		}
		int seasonId = this.m_currentMedalInfo.seasonId;
		int rankedIntroSeenRequirement = this.m_currentMedalInfo.LeagueConfig.RankedIntroSeenRequirement;
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_LAST_SEASON_BONUS_STARS_POPUP_SEEN, out this.m_lastSeasonBonusStarPopUpSeen);
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_BONUS_STARS_POPUP_SEEN_COUNT, out this.m_bonusStarsPopUpSeenCount);
		return this.m_lastSeasonBonusStarPopUpSeen < (long)seasonId && this.m_bonusStarsPopUpSeenCount < (long)rankedIntroSeenRequirement;
	}

	// Token: 0x060022A6 RID: 8870 RVA: 0x000ABB83 File Offset: 0x000A9D83
	private void OnModeSwitchedAfterSkippingHeroSelection()
	{
		CollectionDeckTray.Get().UnregisterModeSwitchedListener(new DeckTray.ModeSwitched(this.OnModeSwitchedAfterSkippingHeroSelection));
		CollectionManager.Get().GetCollectibleDisplay().EnableInput(true);
	}

	// Token: 0x060022A7 RID: 8871 RVA: 0x000ABBAB File Offset: 0x000A9DAB
	protected override IEnumerator InitDeckDependentElements()
	{
		Log.PlayModeInvestigation.PrintInfo("DeckPickerTrayDisplay.InitDeckDependentElements() called", Array.Empty<object>());
		bool flag = base.IsChoosingHero();
		DeckPickerMode defaultDeckPickerMode = DeckPickerMode.CUSTOM;
		this.m_deckPickerMode = defaultDeckPickerMode;
		this.m_numPagesToShow = 1;
		this.m_basicDeckPageContainer.gameObject.SetActive(flag);
		if (!flag)
		{
			while (!NetCache.Get().IsNetObjectAvailable<NetCache.NetCacheDecks>())
			{
				yield return null;
			}
			CollectionManager.Get().RequestDeckContentsForDecksWithoutContentsLoaded(null);
			while (!CollectionManager.Get().AreAllDeckContentsReady())
			{
				yield return null;
			}
			this.m_usingSharedDecks = FriendChallengeMgr.Get().ShouldUseSharedDecks();
			this.m_deckPickerMode = (this.m_usingSharedDecks ? DeckPickerMode.CUSTOM : defaultDeckPickerMode);
			this.UpdateDeckShareRequestButton();
			List<CollectionDeck> list = CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK);
			if (FriendChallengeMgr.Get().IsChallengeFriendlyDuel)
			{
				if (this.m_usingSharedDecks)
				{
					list = FriendChallengeMgr.Get().GetSharedDecks();
				}
				else
				{
					list = list.FindAll((CollectionDeck deck) => deck.IsValidForFormat(FriendChallengeMgr.Get().GetFormatType()));
				}
			}
			this.SetupDeckPages(list);
		}
		if (this.m_rankedPlayDisplay != null)
		{
			VisualsFormatType currentVisualsFormatType = VisualsFormatTypeExtensions.GetCurrentVisualsFormatType();
			this.UpdateRankedPlayDisplay(currentVisualsFormatType);
		}
		this.InitSwitchFormatButton();
		yield return base.StartCoroutine(base.InitDeckDependentElements());
		yield break;
	}

	// Token: 0x060022A8 RID: 8872 RVA: 0x000ABBBC File Offset: 0x000A9DBC
	private void SetupDeckPages(List<CollectionDeck> decks)
	{
		this.m_numPagesToShow = Mathf.CeilToInt((float)decks.Count / 9f);
		this.m_numPagesToShow = Mathf.Max(this.m_numPagesToShow, 1);
		Log.PlayModeInvestigation.PrintInfo(string.Format("DeckPickerTrayDisplay.SetupDeckPages() called. m_numPagesToShow={0}, decks.Count={1}", this.m_numPagesToShow, decks.Count), Array.Empty<object>());
		this.InitDeckPages();
		this.SetPageDecks(decks);
		this.UpdateDeckVisuals();
	}

	// Token: 0x060022A9 RID: 8873 RVA: 0x000ABC38 File Offset: 0x000A9E38
	private void UpdateDeckVisuals()
	{
		for (int i = 0; i < this.m_customPages.Count; i++)
		{
			this.m_customPages[i].UpdateDeckVisuals();
		}
	}

	// Token: 0x060022AA RID: 8874 RVA: 0x000ABC6C File Offset: 0x000A9E6C
	protected override void InitForMode(SceneMgr.Mode mode)
	{
		this.m_missingClassicDeck.SetActive(false);
		if (mode != SceneMgr.Mode.TOURNAMENT)
		{
			if (mode == SceneMgr.Mode.TAVERN_BRAWL)
			{
				this.SetHeaderForTavernBrawl();
			}
		}
		else
		{
			this.m_rankedPlayDisplayWidget = WidgetInstance.Create(UniversalInputManager.UsePhoneUI ? "RankedPlayDisplay_phone.prefab:22b0793a4bc044e47a1948619c2aa896" : "RankedPlayDisplay.prefab:1f884a817dbbdd84b9f8713dc21759f1", false);
			this.m_rankedPlayDisplayWidget.RegisterReadyListener(delegate(object _)
			{
				this.OnRankedPlayDisplayWidgetReady();
			}, null, true);
			base.SetPlayButtonText(GameStrings.Get("GLOBAL_PLAY"));
			this.ChangePlayButtonTextAlpha();
			this.UpdateRankedClassWinsPlate();
			this.UpdatePageArrows();
			if (Options.GetFormatType() == PegasusShared.FormatType.FT_CLASSIC && CollectionManager.Get().GetNumberOfClassicDecks() == 0)
			{
				this.m_missingClassicDeck.SetActive(true);
			}
		}
		UnityEngine.Vector2 keyholeTextureOffsets = new UnityEngine.Vector2(0f, 0f);
		this.m_currentModeTextures = this.m_collectionTextures;
		switch (mode)
		{
		case SceneMgr.Mode.COLLECTIONMANAGER:
			this.m_currentModeTextures = this.m_collectionTextures;
			break;
		case SceneMgr.Mode.TOURNAMENT:
			this.m_currentModeTextures = this.m_tournamentTextures;
			break;
		case SceneMgr.Mode.FRIENDLY:
		case SceneMgr.Mode.FIRESIDE_GATHERING:
			if ((mode == SceneMgr.Mode.FIRESIDE_GATHERING && FiresideGatheringManager.Get().InBrawlMode()) || FriendChallengeMgr.Get().IsChallengeTavernBrawl())
			{
				this.m_currentModeTextures = this.m_tavernBrawlTextures;
				keyholeTextureOffsets.x = 0.5f;
				keyholeTextureOffsets.y = 0.61f;
			}
			else
			{
				this.m_currentModeTextures = this.m_friendlyTextures;
				keyholeTextureOffsets.y = 0.61f;
			}
			break;
		case SceneMgr.Mode.ADVENTURE:
			this.m_currentModeTextures = this.m_adventureTextures;
			keyholeTextureOffsets.x = 0.5f;
			break;
		case SceneMgr.Mode.TAVERN_BRAWL:
			this.m_currentModeTextures = this.m_tavernBrawlTextures;
			keyholeTextureOffsets.x = 0.5f;
			keyholeTextureOffsets.y = 0.61f;
			break;
		}
		VisualsFormatType currentVisualsFormatType = VisualsFormatTypeExtensions.GetCurrentVisualsFormatType();
		Texture textureForFormat = this.m_currentModeTextures.GetTextureForFormat(currentVisualsFormatType);
		Texture customTextureForFormat = this.m_currentModeTextures.GetCustomTextureForFormat(currentVisualsFormatType);
		if (UniversalInputManager.UsePhoneUI)
		{
			if (SceneMgr.Mode.TOURNAMENT != mode)
			{
				this.m_detailsTrayFrame.GetComponent<MeshFilter>().mesh = this.m_alternateDetailsTrayMesh;
			}
			this.SetPhoneDetailsTrayTextures(textureForFormat, textureForFormat);
		}
		else
		{
			this.SetTrayFrameAndBasicDeckPageTextures(textureForFormat, textureForFormat);
		}
		this.SetCustomDeckPageTextures(customTextureForFormat, customTextureForFormat);
		this.SetKeyholeTextureOffsets(keyholeTextureOffsets);
		this.UpdateDeckVisuals();
		base.InitForMode(mode);
	}

	// Token: 0x060022AB RID: 8875 RVA: 0x000ABEA5 File Offset: 0x000AA0A5
	private PegasusShared.GameType GetGameTypeForNewPlayModeGame()
	{
		if (!Options.GetInRankedPlayMode())
		{
			return PegasusShared.GameType.GT_CASUAL;
		}
		return PegasusShared.GameType.GT_RANKED;
	}

	// Token: 0x060022AC RID: 8876 RVA: 0x000ABEB4 File Offset: 0x000AA0B4
	private PegasusShared.FormatType GetFormatTypeForNewPlayModeGame()
	{
		if (this.GetGameTypeForNewPlayModeGame() != PegasusShared.GameType.GT_CASUAL)
		{
			return Options.GetFormatType();
		}
		CollectionDeck selectedCollectionDeck = this.GetSelectedCollectionDeck();
		if (selectedCollectionDeck == null)
		{
			return PegasusShared.FormatType.FT_STANDARD;
		}
		return selectedCollectionDeck.FormatType;
	}

	// Token: 0x060022AD RID: 8877 RVA: 0x000ABEE4 File Offset: 0x000AA0E4
	private void UpdateFormat_Tournament(VisualsFormatType newVisualsFormatType)
	{
		Options.GetFormatType();
		bool flag = CollectionManager.Get().ShouldAccountSeeStandardWild();
		base.SetPlayButtonText(GameStrings.Get("GLOBAL_PLAY"));
		if (flag)
		{
			this.m_switchFormatButton.SetVisualsFormatType(newVisualsFormatType);
			if (SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT && SetRotationManager.HasSeenStandardModeTutorial())
			{
				if (newVisualsFormatType == VisualsFormatType.VFT_WILD && !Options.Get().GetBool(Option.HAS_SEEN_WILD_MODE_VO) && UserAttentionManager.CanShowAttentionGrabber("DeckPickerTrayDisplay.UpdateFormat_Tournament:" + Option.HAS_SEEN_WILD_MODE_VO))
				{
					this.HideSetRotationNotifications();
					NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, DeckPickerTrayDisplay.INNKEEPER_QUOTE_POS, GameStrings.Get("VO_INNKEEPER_WILD_GAME"), "VO_INNKEEPER_Male_Dwarf_SetRotation_35.prefab:db2f6e3818fa49b4d8423121eba762f6", 0f, null, false);
					Options.Get().SetBool(Option.HAS_SEEN_WILD_MODE_VO, true);
				}
				if (newVisualsFormatType == VisualsFormatType.VFT_CLASSIC && !Options.Get().GetBool(Option.HAS_SEEN_CLASSIC_MODE_VO) && UserAttentionManager.CanShowAttentionGrabber("DeckPickerTrayDisplay.UpdateFormat_Tournament:" + Option.HAS_SEEN_CLASSIC_MODE_VO))
				{
					this.HideSetRotationNotifications();
					NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, DeckPickerTrayDisplay.INNKEEPER_QUOTE_POS, GameStrings.Get("VO_INNKEEPER_CLASSIC_TAKES_YOU_BACK_ORIGINAL_HEARTHSTONE"), "VO_Innkeeper_Male_Dwarf_ClassicMode_06.prefab:f91da6f7e66fd754fb4e568d15d49116", 0f, null, false);
					Options.Get().SetBool(Option.HAS_SEEN_CLASSIC_MODE_VO, true);
				}
			}
			if (this.m_selectedCustomDeckBox != null && !this.m_selectedCustomDeckBox.IsValidForCurrentMode())
			{
				this.Deselect();
			}
			this.UpdateCustomTournamentBackgroundAndDecks();
		}
		this.ChangePlayButtonTextAlpha();
		this.UpdateRankedClassWinsPlate();
		this.UpdateRankedPlayDisplay(newVisualsFormatType);
	}

	// Token: 0x060022AE RID: 8878 RVA: 0x000AC054 File Offset: 0x000AA254
	private void ChangePlayButtonTextAlpha()
	{
		if (this.m_playButton != null)
		{
			if (this.m_playButton.IsEnabled())
			{
				this.m_playButton.m_newPlayButtonText.TextAlpha = 1f;
				return;
			}
			this.m_playButton.m_newPlayButtonText.TextAlpha = 0f;
		}
	}

	// Token: 0x060022AF RID: 8879 RVA: 0x000AC0A8 File Offset: 0x000AA2A8
	private void UpdateRankedPlayDisplay(VisualsFormatType newVisualsFormatType)
	{
		if (!newVisualsFormatType.IsRanked())
		{
			this.m_casualPlayDisplayWidget.Show();
			this.m_rankedPlayDisplay.Hide(0f);
			return;
		}
		this.m_casualPlayDisplayWidget.Hide();
		this.m_rankedPlayDisplay.Show(0f);
		this.m_rankedPlayDisplay.UpdateMode(newVisualsFormatType);
		RankedRewardInfoButton componentInChildren = this.m_rankedPlayDisplay.GetComponentInChildren<RankedRewardInfoButton>();
		if (componentInChildren != null)
		{
			TournamentDisplay tournamentDisplay = TournamentDisplay.Get();
			if (tournamentDisplay == null)
			{
				return;
			}
			NetCache.NetCacheMedalInfo currentMedalInfo = tournamentDisplay.GetCurrentMedalInfo();
			if (currentMedalInfo == null)
			{
				return;
			}
			MedalInfoTranslator mit = new MedalInfoTranslator(currentMedalInfo, null);
			componentInChildren.Initialize(mit);
		}
	}

	// Token: 0x060022B0 RID: 8880 RVA: 0x000AC140 File Offset: 0x000AA340
	private void UpdateFormat_CollectionManager()
	{
		PegasusShared.FormatType formatType = Options.GetFormatType();
		bool inRankedPlayMode = Options.GetInRankedPlayMode();
		if (formatType == PegasusShared.FormatType.FT_WILD && !this.m_HasSeenPlayStandardToWildVO)
		{
			this.m_HasSeenPlayStandardToWildVO = true;
			this.m_HasSeenPlayStandardToClassicVO = false;
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, NotificationManager.DEFAULT_CHARACTER_POS, GameStrings.Get("VO_INNKEEPER_PLAY_STANDARD_TO_WILD"), "VO_INNKEEPER_Male_Dwarf_SetRotation_43.prefab:4b4ce858139927946905ec0d40d5b3c1", 0f, null, false);
		}
		else if (formatType == PegasusShared.FormatType.FT_CLASSIC && !this.m_HasSeenPlayStandardToClassicVO)
		{
			this.m_HasSeenPlayStandardToClassicVO = true;
			this.m_HasSeenPlayStandardToWildVO = false;
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, NotificationManager.DEFAULT_CHARACTER_POS, GameStrings.Get("VO_INNKEEPER_CLASSIC_PLAY_CLASSIC_MODE_ONLY"), "VO_Innkeeper_Male_Dwarf_ClassicMode_01.prefab:5ac6a7a19130d8c4795330b7a8693513", 0f, null, false);
		}
		else if (formatType == PegasusShared.FormatType.FT_STANDARD)
		{
			this.m_HasSeenPlayStandardToClassicVO = false;
			this.m_HasSeenPlayStandardToWildVO = false;
		}
		base.StartCoroutine(this.InitModeWhenReady());
		this.m_switchFormatButton.SetVisualsFormatType(VisualsFormatTypeExtensions.ToVisualsFormatType(formatType, inRankedPlayMode));
		this.TransitionToFormatType(formatType, inRankedPlayMode, 2f);
	}

	// Token: 0x060022B1 RID: 8881 RVA: 0x000AC220 File Offset: 0x000AA420
	private void UpdateCustomTournamentBackgroundAndDecks()
	{
		this.TransitionToFormatType(Options.GetFormatType(), Options.GetInRankedPlayMode(), 2f);
		foreach (CustomDeckPage customDeckPage in this.m_customPages)
		{
			customDeckPage.UpdateDeckVisuals();
		}
	}

	// Token: 0x060022B2 RID: 8882 RVA: 0x000AC288 File Offset: 0x000AA488
	private IEnumerator InitButtonAchievements()
	{
		List<global::Achievement> unlockHeroAchieves = AchieveManager.Get().GetAchievesInGroup(Achieve.Type.HERO);
		this.UpdateCollectionButtonGlow();
		using (List<global::Achievement>.Enumerator enumerator = unlockHeroAchieves.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				global::Achievement achievement = enumerator.Current;
				HeroPickerButton heroPickerButton = this.m_heroButtons.Find((HeroPickerButton obj) => obj.GetEntityDef().GetClass() == achievement.ClassReward.Value);
				if (heroPickerButton == null)
				{
					if (this.m_validClasses.Contains(achievement.ClassReward.Value))
					{
						Debug.LogWarning(string.Format("DeckPickerTrayDisplay.InitButtonAchievements() - could not find hero picker button matching UnlockHeroAchievement with class {0}", achievement.ClassReward.Value));
					}
				}
				else
				{
					if (achievement.ClassReward.Value == TAG_CLASS.MAGE)
					{
						achievement.AckCurrentProgressAndRewardNotices();
					}
					heroPickerButton.SetProgress(achievement.AcknowledgedProgress, achievement.Progress, achievement.MaxProgress, false);
					if (base.IsChoosingHero())
					{
						CollectionManager.PreconDeck preconDeck = CollectionManager.Get().GetPreconDeck(achievement.ClassReward.Value);
						long num = 0L;
						if (preconDeck != null)
						{
							num = preconDeck.ID;
						}
						heroPickerButton.SetPreconDeckID(num);
						if (achievement.IsCompleted() && num == 0L)
						{
							Debug.LogError(string.Format("DeckPickerTrayDisplay.InitButtonAchievements() - preconDeckID = 0 for achievement {0}", achievement));
						}
						SceneMgr.Mode mode = SceneMgr.Get().GetMode();
						if (mode == SceneMgr.Mode.TAVERN_BRAWL || (mode == SceneMgr.Mode.FRIENDLY && FriendChallengeMgr.Get().IsChallengeTavernBrawl()) || (mode == SceneMgr.Mode.FIRESIDE_GATHERING && FiresideGatheringManager.Get().InBrawlMode()))
						{
							heroPickerButton.Unlock();
						}
					}
				}
			}
		}
		if (!DemoMgr.Get().IsDemo())
		{
			goto IL_284;
		}
		using (List<HeroPickerButton>.Enumerator enumerator2 = this.m_heroButtons.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				HeroPickerButton heroPickerButton2 = enumerator2.Current;
				if (!DemoMgr.Get().IsHeroClassPlayable(heroPickerButton2.m_heroClass))
				{
					Collider[] componentsInChildren = heroPickerButton2.GetComponentsInChildren<Collider>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].enabled = false;
					}
					heroPickerButton2.Lock();
					heroPickerButton2.Activate(false);
				}
			}
			goto IL_284;
		}
		IL_26D:
		yield return null;
		IL_284:
		if (!this.m_delayButtonAnims)
		{
			using (List<global::Achievement>.Enumerator enumerator = unlockHeroAchieves.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					global::Achievement achievement = enumerator.Current;
					HeroPickerButton heroPickerButton3 = this.m_heroButtons.Find((HeroPickerButton obj) => obj.GetEntityDef().GetClass() == achievement.ClassReward.Value);
					if (heroPickerButton3 == null)
					{
						if (this.m_validClasses.Contains(achievement.ClassReward.Value))
						{
							Debug.LogWarning(string.Format("DeckPickerTrayDisplay.InitButtonAchievements() - could not find hero picker button matching UnlockHeroAchievement with class {0}", achievement.ClassReward.Value));
						}
					}
					else
					{
						if (!base.IsChoosingHero() && heroPickerButton3.GetPreconDeckID() != 0L)
						{
							heroPickerButton3.SetProgress(achievement.AcknowledgedProgress, achievement.Progress, achievement.MaxProgress);
						}
						achievement.AckCurrentProgressAndRewardNotices();
					}
				}
				yield break;
			}
			yield break;
		}
		goto IL_26D;
	}

	// Token: 0x060022B3 RID: 8883 RVA: 0x000AC297 File Offset: 0x000AA497
	protected override void SetHeaderForTavernBrawl()
	{
		if (this.m_labelDecoration != null)
		{
			this.m_labelDecoration.SetActive(false);
		}
		base.SetHeaderForTavernBrawl();
	}

	// Token: 0x060022B4 RID: 8884 RVA: 0x000AC2BC File Offset: 0x000AA4BC
	protected override void InitHeroPickerButtons()
	{
		base.InitHeroPickerButtons();
		CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK);
		this.m_heroDefsLoading = this.m_validClasses.Count;
		for (int i = 0; i < this.m_validClasses.Count; i++)
		{
			if (i >= this.m_heroButtons.Count || this.m_heroButtons[i] == null)
			{
				Debug.LogWarning("InitHeroPickerButtons: not enough buttons for total guest heroes.");
				break;
			}
			HeroPickerButton heroPickerButton = this.m_heroButtons[i];
			heroPickerButton.Lock();
			heroPickerButton.SetProgress(0, 0, 1);
			TAG_CLASS tag_CLASS = this.m_validClasses[i];
			NetCache.CardDefinition favoriteHero = CollectionManager.Get().GetFavoriteHero(tag_CLASS);
			if (favoriteHero == null)
			{
				if (tag_CLASS != TAG_CLASS.WHIZBANG)
				{
					Debug.LogWarning("Couldn't find Favorite Hero for hero class: " + tag_CLASS + " defaulting to Normal Vanilla Hero!");
				}
				string heroCardId = CollectionManager.GetHeroCardId(tag_CLASS, CardHero.HeroType.VANILLA);
				AbsDeckPickerTrayDisplay.HeroFullDefLoadedCallbackData userData = new AbsDeckPickerTrayDisplay.HeroFullDefLoadedCallbackData(heroPickerButton, TAG_PREMIUM.NORMAL);
				DefLoader.Get().LoadFullDef(heroCardId, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnHeroFullDefLoaded), userData, null);
			}
			else
			{
				AbsDeckPickerTrayDisplay.HeroFullDefLoadedCallbackData userData2 = new AbsDeckPickerTrayDisplay.HeroFullDefLoadedCallbackData(heroPickerButton, favoriteHero.Premium);
				DefLoader.Get().LoadFullDef(favoriteHero.Name, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnHeroFullDefLoaded), userData2, null);
			}
			if (base.IsChoosingHero())
			{
				heroPickerButton.SetDivotTexture(this.m_currentModeTextures.classDivotTex);
			}
			else
			{
				heroPickerButton.SetDivotTexture(this.m_currentModeTextures.guestHeroDivotTex);
			}
		}
		if (base.IsChoosingHeroForDungeonCrawlAdventure())
		{
			base.SetUpHeroCrowns();
		}
	}

	// Token: 0x060022B5 RID: 8885 RVA: 0x000AC428 File Offset: 0x000AA628
	private void InitDeckPages()
	{
		Log.PlayModeInvestigation.PrintInfo(string.Format("DeckPickerTrayDisplay.InitDeckPages() called. m_numPagesToShow={0}, m_customPages.Count={1}", this.m_numPagesToShow, this.m_customPages.Count), Array.Empty<object>());
		if (this.m_numPagesToShow <= 0)
		{
			Debug.LogWarning("DeckPickerTrayDisplay.InitDeckPages() called with invalid amount of pages");
			return;
		}
		while (this.m_numPagesToShow > this.m_customPages.Count)
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(DeckPickerTrayDisplay.CUSTOM_DECK_PAGE, AssetLoadingOptions.None);
			gameObject.transform.SetParent(this.m_customDeckPagesRoot.transform, false);
			gameObject.transform.localPosition = ((this.m_customPages.Count == 0) ? this.m_customDeckPageUpperBone.transform.localPosition : this.m_customDeckPageLowerBone.transform.localPosition);
			CustomDeckPage component = gameObject.GetComponent<CustomDeckPage>();
			component.SetDeckButtonCallback(new CustomDeckPage.DeckButtonCallback(this.OnCustomDeckPressed));
			component.SetDecks(new List<CollectionDeck>());
			this.m_customPages.Add(component);
			Log.PlayModeInvestigation.PrintInfo(string.Format("DeckPickerTrayDisplay.InitDeckPages() -- Deck page added. New total: {0}", this.m_customPages.Count), Array.Empty<object>());
		}
		while (this.m_numPagesToShow < this.m_customPages.Count - 1)
		{
			UnityEngine.Object.Destroy(this.m_customPages[this.m_customPages.Count - 1]);
			this.m_customPages.Remove(this.m_customPages[this.m_customPages.Count - 1]);
			Log.PlayModeInvestigation.PrintInfo(string.Format("DeckPickerTrayDisplay.InitDeckPages() -- Deck page removed. New total: {0}", this.m_customPages.Count), Array.Empty<object>());
		}
	}

	// Token: 0x060022B6 RID: 8886 RVA: 0x000AC5D0 File Offset: 0x000AA7D0
	private void OpponentDecksSlidingTrayToggledListener(bool shown)
	{
		if (shown)
		{
			this.m_collectionButtonGlow.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			return;
		}
		this.m_collectionButtonGlow.ChangeState(ActorStateType.HIGHLIGHT_OFF);
	}

	// Token: 0x060022B7 RID: 8887 RVA: 0x000AC5F4 File Offset: 0x000AA7F4
	private void SetPageDecks(List<CollectionDeck> decks)
	{
		if (this.m_customPages == null)
		{
			Debug.LogError("{0}.UpdateCustomPages(): m_customPages is null. Make sure you call InitCustomPages() first!", this);
		}
		foreach (CustomDeckPage customDeckPage in this.m_customPages)
		{
			int count = Mathf.Min(decks.Count, customDeckPage.m_maxCustomDecksToDisplay);
			List<CollectionDeck> range = decks.GetRange(0, count);
			customDeckPage.SetDecks(range);
			customDeckPage.InitCustomDecks();
			foreach (CollectionDeck collectionDeck in range)
			{
				string heroPowerCardIdFromHero = GameUtils.GetHeroPowerCardIdFromHero(collectionDeck.HeroCardID);
				if (string.IsNullOrEmpty(heroPowerCardIdFromHero))
				{
					Debug.LogErrorFormat("No hero power set up for hero {0}", new object[]
					{
						collectionDeck.HeroCardID
					});
				}
				else
				{
					base.LoadHeroPowerDef(heroPowerCardIdFromHero);
				}
			}
			decks.RemoveRange(0, count);
			if (decks.Count <= 0)
			{
				break;
			}
		}
		if (decks.Count > 0)
		{
			Debug.LogWarningFormat("DeckPickerTrayDisplay - {0} more decks than we can display!", new object[]
			{
				decks.Count
			});
		}
	}

	// Token: 0x060022B8 RID: 8888 RVA: 0x000AC730 File Offset: 0x000AA930
	private void InitMode()
	{
		if (base.IsChoosingHero())
		{
			this.ShowFirstPage(true);
		}
		else
		{
			this.SetSelectionAndPageFromOptions();
		}
		this.InitExpoDemoMode();
		this.ShowSwitchToWildTutorialIfNecessary();
	}

	// Token: 0x060022B9 RID: 8889 RVA: 0x000AC755 File Offset: 0x000AA955
	private void InitExpoDemoMode()
	{
		if (!DemoMgr.Get().IsExpoDemo())
		{
			return;
		}
		this.UpdatePageArrows();
		base.SetBackButtonEnabled(false);
		base.StartCoroutine("ShowDemoQuotes");
	}

	// Token: 0x060022BA RID: 8890 RVA: 0x000AC77D File Offset: 0x000AA97D
	private IEnumerator ShowDemoQuotes()
	{
		string text = Vars.Key("Demo.ThankQuote").GetStr("");
		int @int = Vars.Key("Demo.ThankQuoteMsTime").GetInt(0);
		text = text.Replace("\\n", "\n");
		if (!string.IsNullOrEmpty(text) && @int > 0)
		{
			if (DemoMgr.Get().GetMode() == DemoMode.BLIZZCON_2015)
			{
				this.m_expoThankQuote = NotificationManager.Get().CreateCharacterQuote("Reno_Quote.prefab:0a2e34fa6782a0747b4f5d5574d1331a", new Vector3(0f, NotificationManager.DEPTH, 0f), text, "", true, (float)@int / 1000f, null, CanvasAnchor.CENTER, false);
			}
			else
			{
				this.m_expoThankQuote = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(158.1f, NotificationManager.DEPTH, 80.2f), text, "", (float)@int / 1000f, null, false);
			}
			this.EnableClickBlocker(true);
			yield return new WaitForSeconds((float)@int / 1000f);
			this.EnableClickBlocker(false);
		}
		this.ShowIntroQuote();
		yield break;
	}

	// Token: 0x060022BB RID: 8891 RVA: 0x000AC78C File Offset: 0x000AA98C
	private void ShowIntroQuote()
	{
		this.HideIntroQuote();
		string text = Vars.Key("Demo.IntroQuote").GetStr("");
		text = text.Replace("\\n", "\n");
		if (!string.IsNullOrEmpty(text))
		{
			if (DemoMgr.Get().GetMode() == DemoMode.BLIZZCON_2015)
			{
				this.m_expoIntroQuote = NotificationManager.Get().CreateCharacterQuote("Reno_Quote.prefab:0a2e34fa6782a0747b4f5d5574d1331a", new Vector3(0f, NotificationManager.DEPTH, -54.22f), text, "", true, 0f, null, CanvasAnchor.CENTER, false);
				return;
			}
			this.m_expoIntroQuote = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(147.6f, NotificationManager.DEPTH, 23.1f), text, "", 0f, null, false);
		}
	}

	// Token: 0x060022BC RID: 8892 RVA: 0x000AC848 File Offset: 0x000AAA48
	private void EnableClickBlocker(bool enable)
	{
		if (this.m_clickBlocker == null)
		{
			return;
		}
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (enable)
		{
			fullScreenFXMgr.SetBlurBrightness(1f);
			fullScreenFXMgr.SetBlurDesaturation(0f);
			fullScreenFXMgr.Vignette();
			fullScreenFXMgr.Blur(1f, 0.4f, iTween.EaseType.easeOutCirc, null);
		}
		else
		{
			fullScreenFXMgr.StopVignette();
			fullScreenFXMgr.StopBlur();
		}
		this.m_clickBlocker.gameObject.SetActive(enable);
	}

	// Token: 0x060022BD RID: 8893 RVA: 0x000AC8BC File Offset: 0x000AAABC
	private void HideDemoQuotes()
	{
		DemoMgr demoMgr = DemoMgr.Get();
		if (demoMgr != null && !demoMgr.IsExpoDemo())
		{
			return;
		}
		base.StopCoroutine("ShowDemoQuotes");
		if (this.m_expoThankQuote != null)
		{
			NotificationManager notificationManager = NotificationManager.Get();
			if (notificationManager != null)
			{
				notificationManager.DestroyNotification(this.m_expoThankQuote, 0f);
			}
			this.m_expoThankQuote = null;
			FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
			if (fullScreenFXMgr != null)
			{
				fullScreenFXMgr.StopVignette();
				fullScreenFXMgr.StopBlur();
			}
		}
		this.HideIntroQuote();
	}

	// Token: 0x060022BE RID: 8894 RVA: 0x000AC936 File Offset: 0x000AAB36
	private void HideIntroQuote()
	{
		if (this.m_expoIntroQuote != null)
		{
			NotificationManager.Get().DestroyNotification(this.m_expoIntroQuote, 0f);
			this.m_expoIntroQuote = null;
		}
	}

	// Token: 0x060022BF RID: 8895 RVA: 0x000AC964 File Offset: 0x000AAB64
	private void HideSetRotationNotifications()
	{
		if (this.m_innkeeperQuote != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.m_innkeeperQuote);
			this.m_innkeeperQuote = null;
		}
		if (this.m_switchFormatPopup != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.m_switchFormatPopup);
			this.m_switchFormatPopup = null;
		}
	}

	// Token: 0x060022C0 RID: 8896 RVA: 0x000AC9BB File Offset: 0x000AABBB
	private void OnTransitionFromGameplayFinished(bool cutoff, object userData)
	{
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.FRIENDLY && !FriendChallengeMgr.Get().HasChallenge())
		{
			this.GoBackUntilOnNavigateBackCalled();
		}
		LoadingScreen.Get().UnregisterFinishedTransitionListener(new LoadingScreen.FinishedTransitionCallback(this.OnTransitionFromGameplayFinished));
		this.m_delayButtonAnims = false;
	}

	// Token: 0x060022C1 RID: 8897 RVA: 0x000AC9FC File Offset: 0x000AABFC
	private void CollectionButtonPress(UIEvent e)
	{
		if (this.ShouldGlowCollectionButton())
		{
			if (!Options.Get().GetBool(Option.HAS_CLICKED_COLLECTION_BUTTON_FOR_NEW_DECK) && this.HaveDecksThatNeedNames())
			{
				Options.Get().SetBool(Option.HAS_CLICKED_COLLECTION_BUTTON_FOR_NEW_DECK, true);
			}
			else if (!Options.Get().GetBool(Option.HAS_CLICKED_COLLECTION_BUTTON_FOR_NEW_CARD) && this.HaveUnseenCards())
			{
				Options.Get().SetBool(Option.HAS_CLICKED_COLLECTION_BUTTON_FOR_NEW_CARD, true);
			}
			if (Options.Get().GetBool(Option.GLOW_COLLECTION_BUTTON_AFTER_SET_ROTATION) && SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT)
			{
				Options.Get().SetBool(Option.GLOW_COLLECTION_BUTTON_AFTER_SET_ROTATION, false);
			}
		}
		if (PracticePickerTrayDisplay.Get() != null && PracticePickerTrayDisplay.Get().IsShown())
		{
			Navigation.GoBack();
		}
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			Navigation.Clear();
		}
		TelemetryWatcher.StopWatchingFor(TelemetryWatcherWatchType.CollectionManagerFromDeckPicker);
		TelemetryManager.Client().SendDeckPickerToCollection(DeckPickerToCollection.Path.DECK_PICKER_BUTTON);
		CollectionManager.Get().NotifyOfBoxTransitionStart();
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.COLLECTIONMANAGER, SceneMgr.TransitionHandlerType.SCENEMGR, null);
	}

	// Token: 0x060022C2 RID: 8898 RVA: 0x000ACAF0 File Offset: 0x000AACF0
	private void RequestDeckShareButtonPress(UIEvent e)
	{
		if (this.m_doingDeckShareTransition)
		{
			return;
		}
		if (this.m_usingSharedDecks)
		{
			FriendChallengeMgr.Get().EndDeckShare();
		}
		else
		{
			if (!FriendChallengeMgr.Get().HasOpponentSharedDecks())
			{
				this.EnableRequestDeckShareButton(false);
			}
			FriendChallengeMgr.Get().RequestDeckShare();
		}
		this.UpdateDeckShareTooltip();
	}

	// Token: 0x060022C3 RID: 8899 RVA: 0x000ACB3D File Offset: 0x000AAD3D
	private void RequestDeckShareButtonOver(UIEvent e)
	{
		this.m_isDeckShareRequestButtonHovered = true;
		this.UpdateDeckShareTooltip();
	}

	// Token: 0x060022C4 RID: 8900 RVA: 0x000ACB4C File Offset: 0x000AAD4C
	private void RequestDeckShareButtonOut(UIEvent e)
	{
		this.m_isDeckShareRequestButtonHovered = false;
		this.UpdateDeckShareTooltip();
	}

	// Token: 0x060022C5 RID: 8901 RVA: 0x000ACB5B File Offset: 0x000AAD5B
	private void EnableRequestDeckShareButton(bool enable)
	{
		if (this.m_DeckShareRequestButton.IsEnabled() != enable)
		{
			if (!enable)
			{
				this.m_DeckShareRequestButton.TriggerOut();
			}
			this.m_DeckShareRequestButton.SetEnabled(enable, false);
			this.m_DeckShareRequestButton.Flip(enable, false);
		}
		this.UpdateDeckShareRequestButton();
	}

	// Token: 0x060022C6 RID: 8902 RVA: 0x000ACB9C File Offset: 0x000AAD9C
	private void UpdateDeckShareRequestButton()
	{
		if (this.m_DeckShareRequestButton == null)
		{
			return;
		}
		if (!this.IsDeckSharingActive())
		{
			return;
		}
		if (!FriendChallengeMgr.Get().HasOpponentSharedDecks())
		{
			this.m_DeckShareRequestButton.SetText(GameStrings.Get("GLUE_DECK_SHARE_BUTTON_BORROW_DECKS"));
		}
		else if (this.m_usingSharedDecks)
		{
			this.m_DeckShareRequestButton.SetText(GameStrings.Get("GLUE_DECK_SHARE_BUTTON_SHOW_MY_DECKS"));
		}
		else
		{
			this.m_DeckShareRequestButton.SetText(GameStrings.Format("GLUE_DECK_SHARE_BUTTON_SHOW_OPPONENT_DECKS", Array.Empty<object>()));
		}
		this.UpdateDeckShareTooltip();
	}

	// Token: 0x060022C7 RID: 8903 RVA: 0x000ACC24 File Offset: 0x000AAE24
	private void UpdateDeckShareTooltip()
	{
		if (this.m_DeckShareRequestButton == null)
		{
			return;
		}
		TooltipZone componentInChildren = this.m_DeckShareRequestButton.GetComponentInChildren<TooltipZone>();
		if (componentInChildren == null)
		{
			return;
		}
		if (!FriendChallengeMgr.Get().HasOpponentSharedDecks())
		{
			if (this.m_isDeckShareRequestButtonHovered && !componentInChildren.IsShowingTooltip(0))
			{
				string text = string.Empty;
				BnetPlayer myOpponent = FriendChallengeMgr.Get().GetMyOpponent();
				if (myOpponent != null)
				{
					text = myOpponent.GetBestName();
				}
				componentInChildren.ShowTooltip(GameStrings.Get("GLUE_DECK_SHARE_TOOLTIP_HEADER"), GameStrings.Format("GLUE_DECK_SHARE_TOOLTIP_BODY_REQUEST", new object[]
				{
					text
				}), 5f, 0);
				return;
			}
			if (!this.m_isDeckShareRequestButtonHovered && componentInChildren.IsShowingTooltip(0))
			{
				componentInChildren.HideTooltip();
				return;
			}
		}
		else if (componentInChildren.IsShowingTooltip(0))
		{
			componentInChildren.HideTooltip();
		}
	}

	// Token: 0x060022C8 RID: 8904 RVA: 0x000ACCE1 File Offset: 0x000AAEE1
	private void OnDeckShareRequestCancelDeclineOrError()
	{
		base.StopCoroutine("WaitThanEnableRequestDeckShareButton");
		base.StartCoroutine("WaitThanEnableRequestDeckShareButton");
	}

	// Token: 0x060022C9 RID: 8905 RVA: 0x000ACCFA File Offset: 0x000AAEFA
	private IEnumerator WaitThanEnableRequestDeckShareButton()
	{
		yield return new WaitForSeconds(1f);
		this.EnableRequestDeckShareButton(true);
		yield break;
	}

	// Token: 0x060022CA RID: 8906 RVA: 0x000ACD09 File Offset: 0x000AAF09
	public void UseSharedDecks(List<CollectionDeck> decks)
	{
		base.StartCoroutine(this.UseSharedDecksImpl(decks));
	}

	// Token: 0x060022CB RID: 8907 RVA: 0x000ACD19 File Offset: 0x000AAF19
	private IEnumerator UseSharedDecksImpl(List<CollectionDeck> decks)
	{
		if (this.m_usingSharedDecks)
		{
			yield break;
		}
		if (decks == null)
		{
			yield break;
		}
		this.m_doingDeckShareTransition = true;
		this.m_clickBlocker.gameObject.SetActive(true);
		this.m_usingSharedDecks = true;
		this.UpdateDeckShareRequestButton();
		this.Deselect();
		this.m_deckPickerMode = DeckPickerMode.CUSTOM;
		if (!string.IsNullOrEmpty(this.m_wildDeckTransitionSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_wildDeckTransitionSound);
		}
		if (this.m_DeckShareGlowOutQuad != null)
		{
			this.m_DeckShareGlowOutQuad.SetActive(true);
			yield return base.StartCoroutine(this.FadeDeckShareGlowOutQuad(0f, this.m_DeckShareGlowOutIntensity, this.m_DeckShareTransitionTime * 0.5f));
		}
		if (this.m_DeckShareParticles != null)
		{
			this.m_DeckShareParticles.Stop();
			this.m_DeckShareParticles.Play();
		}
		this.SetupDeckPages(decks);
		this.m_basicDeckPageContainer.gameObject.SetActive(false);
		foreach (CollectionDeck collectionDeck in decks)
		{
			collectionDeck.Locked = false;
		}
		this.ShowFirstPage(false);
		if (this.m_DeckShareGlowOutQuad != null)
		{
			yield return base.StartCoroutine(this.FadeDeckShareGlowOutQuad(this.m_DeckShareGlowOutIntensity, 0f, this.m_DeckShareTransitionTime * 0.5f));
			this.m_DeckShareGlowOutQuad.SetActive(false);
		}
		this.EnableRequestDeckShareButton(true);
		this.m_clickBlocker.gameObject.SetActive(false);
		this.m_doingDeckShareTransition = false;
		yield break;
	}

	// Token: 0x060022CC RID: 8908 RVA: 0x000ACD2F File Offset: 0x000AAF2F
	public void StopUsingSharedDecks()
	{
		base.StartCoroutine(this.StopUsingSharedDecksImpl());
	}

	// Token: 0x060022CD RID: 8909 RVA: 0x000ACD3E File Offset: 0x000AAF3E
	private IEnumerator StopUsingSharedDecksImpl()
	{
		if (!this.m_usingSharedDecks)
		{
			yield break;
		}
		this.m_clickBlocker.gameObject.SetActive(true);
		this.m_doingDeckShareTransition = true;
		this.m_usingSharedDecks = false;
		this.UpdateDeckShareRequestButton();
		this.Deselect();
		if (!string.IsNullOrEmpty(this.m_wildDeckTransitionSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_wildDeckTransitionSound);
		}
		if (this.m_DeckShareGlowOutQuad != null)
		{
			this.m_DeckShareGlowOutQuad.SetActive(true);
			yield return base.StartCoroutine(this.FadeDeckShareGlowOutQuad(0f, this.m_DeckShareGlowOutIntensity, this.m_DeckShareTransitionTime * 0.5f));
		}
		if (this.m_DeckShareParticles != null)
		{
			this.m_DeckShareParticles.Stop();
			this.m_DeckShareParticles.Play();
		}
		List<CollectionDeck> decks = CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK).FindAll((CollectionDeck deck) => deck.IsValidForFormat(FriendChallengeMgr.Get().GetFormatType()));
		this.SetupDeckPages(decks);
		this.ShowFirstPage(false);
		if (this.m_DeckShareGlowOutQuad != null)
		{
			yield return base.StartCoroutine(this.FadeDeckShareGlowOutQuad(this.m_DeckShareGlowOutIntensity, 0f, this.m_DeckShareTransitionTime * 0.5f));
			this.m_DeckShareGlowOutQuad.SetActive(false);
		}
		this.EnableRequestDeckShareButton(true);
		this.m_doingDeckShareTransition = false;
		this.m_clickBlocker.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x060022CE RID: 8910 RVA: 0x000ACD4D File Offset: 0x000AAF4D
	private IEnumerator FadeDeckShareGlowOutQuad(float startingIntensity, float finalIntensity, float fadeTime)
	{
		if (this.m_DeckShareGlowOutQuad == null)
		{
			yield break;
		}
		int propertyID = Shader.PropertyToID("_Intensity");
		float currentIntensity = startingIntensity;
		Material mat = this.m_DeckShareGlowOutQuad.GetComponentInChildren<MeshRenderer>(true).GetMaterial();
		mat.SetFloat(propertyID, currentIntensity);
		float transitionSpeed = Mathf.Abs(finalIntensity - startingIntensity) / fadeTime;
		while (currentIntensity != finalIntensity)
		{
			currentIntensity = Mathf.MoveTowards(currentIntensity, finalIntensity, transitionSpeed * Time.deltaTime);
			mat.SetFloat(propertyID, currentIntensity);
			yield return null;
		}
		yield break;
	}

	// Token: 0x060022CF RID: 8911 RVA: 0x000ACD71 File Offset: 0x000AAF71
	private void SwitchFormatButtonPress(UIEvent e)
	{
		this.m_switchFormatButton.Disable();
		this.m_switchFormatButton.gameObject.SetActive(false);
		this.ShowFormatTypePickerPopup();
		this.TransitionToFormatType(PegasusShared.FormatType.FT_STANDARD, true, 2f);
	}

	// Token: 0x060022D0 RID: 8912 RVA: 0x000ACDA4 File Offset: 0x000AAFA4
	public void ShowFormatTypePickerPopup()
	{
		if (this.m_showStandardComingSoonNotice)
		{
			BasicPopup.PopupInfo popupInfo = new BasicPopup.PopupInfo();
			popupInfo.m_responseCallback = new BasicPopup.ResponseCallback(this.OnStandardComingSoonResponse);
			popupInfo.m_prefabAssetRefs.Add(DeckPickerTrayDisplay.STANDARD_COMING_SOON_POPUP_NAME);
			DialogManager.Get().ShowStandardComingSoonPopup(UserAttentionBlocker.ALL_EXCEPT_FATAL_ERROR_SCENE, popupInfo);
			this.m_dimQuad.GetComponent<Renderer>().enabled = true;
			this.m_dimQuad.enabled = true;
			this.m_dimQuad.StopPlayback();
			this.m_dimQuad.Play("DimQuad_FadeIn", -1, 0.5f);
			return;
		}
		this.m_formatTypePickerWidget.transform.position = Vector3.zero;
		this.m_formatTypePickerWidget.Show();
		this.m_formatTypePickerWidget.TriggerEvent(this.m_inHeroPicker ? "OPEN_WITHOUT_CASUAL" : "OPEN_WITH_CASUAL", new Widget.TriggerEventParameters
		{
			Payload = (int)VisualsFormatTypeExtensions.GetCurrentVisualsFormatType()
		});
	}

	// Token: 0x060022D1 RID: 8913 RVA: 0x000ACE88 File Offset: 0x000AB088
	public void ShowPopupDuringSetRotation(VisualsFormatType visualsFormatType)
	{
		this.m_formatTypePickerWidget.transform.position = Vector3.zero;
		this.m_formatTypePickerWidget.Show();
		this.m_formatTypePickerWidget.TriggerEvent("SETROTATION_OPEN_WITH_CASUAL", new Widget.TriggerEventParameters
		{
			Payload = (int)visualsFormatType
		});
	}

	// Token: 0x060022D2 RID: 8914 RVA: 0x000ACEDC File Offset: 0x000AB0DC
	private void OnFormatTypePickerEvent(string eventName)
	{
		if (eventName == "HIDE" || eventName == "HIDE_WITH_CASUAL" || eventName == "HIDE_WITHOUT_CASUAL")
		{
			base.FireFormatTypePickerClosedEvent();
		}
	}

	// Token: 0x060022D3 RID: 8915 RVA: 0x000ACF0C File Offset: 0x000AB10C
	private void SwitchFormatTypeAndRankedPlayMode(VisualsFormatType newVisualsFormatType)
	{
		if (VisualsFormatTypeExtensions.ToVisualsFormatType(Options.GetFormatType(), Options.GetInRankedPlayMode()) != newVisualsFormatType)
		{
			Options.SetFormatType(newVisualsFormatType.ToFormatType());
			Options.SetInRankedPlayMode(newVisualsFormatType.IsRanked());
		}
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT)
		{
			this.UpdateFormat_Tournament(newVisualsFormatType);
			TournamentDisplay.Get().UpdateHeaderText();
			this.m_rankedPlayDisplay.OnSwitchFormat(newVisualsFormatType);
		}
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER)
		{
			this.UpdateCreateDeckText();
			this.UpdateFormat_CollectionManager();
		}
		this.m_missingClassicDeck.SetActive(false);
		if (newVisualsFormatType == VisualsFormatType.VFT_CLASSIC && SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT && CollectionManager.Get().GetNumberOfClassicDecks() == 0)
		{
			this.m_missingClassicDeck.SetActive(true);
		}
		this.UpdatePageArrows();
		this.m_formatTypePickerWidget.TriggerEvent(this.m_inHeroPicker ? "HIDE_WITHOUT_CASUAL" : "HIDE_WITH_CASUAL", new Widget.TriggerEventParameters
		{
			Payload = (int)newVisualsFormatType
		});
		base.StartCoroutine(this.m_switchFormatButton.EnableWithDelay(0.8f));
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT)
		{
			if (this.ShouldShowRotatedBoosterPopup(newVisualsFormatType))
			{
				base.StartCoroutine(this.ShowRotatedBoostersPopup(null));
				return;
			}
			if (this.ShouldShowStandardDeckVO(newVisualsFormatType))
			{
				base.StartCoroutine(this.ShowStandardDeckVO());
			}
		}
	}

	// Token: 0x060022D4 RID: 8916 RVA: 0x000AD047 File Offset: 0x000AB247
	private void OnFormatTypeSwitchCancelled()
	{
		this.TransitionToFormatType(this.m_PreviousFormatType, this.m_PreviousInRankedPlayMode, 2f);
	}

	// Token: 0x060022D5 RID: 8917 RVA: 0x000AD060 File Offset: 0x000AB260
	private void OnStandardComingSoonResponse(BasicPopup.Response response, object userData)
	{
		if (response == BasicPopup.Response.CUSTOM_RESPONSE)
		{
			string setRotationInfoLink = ExternalUrlService.Get().GetSetRotationInfoLink();
			Log.DeckTray.Print("Set Rotation web page URL: {0}", new object[]
			{
				setRotationInfoLink
			});
			if (!string.IsNullOrEmpty(setRotationInfoLink))
			{
				Application.OpenURL(setRotationInfoLink);
			}
		}
		this.m_dimQuad.StopPlayback();
		this.m_dimQuad.Play("DimQuad_FadeOut");
	}

	// Token: 0x060022D6 RID: 8918 RVA: 0x000AD0BE File Offset: 0x000AB2BE
	public static bool OnNavigateBack()
	{
		if (DeckPickerTrayDisplay.Get() != null)
		{
			return DeckPickerTrayDisplay.Get().OnNavigateBackImplementation();
		}
		Debug.LogError("HeroPickerTrayDisplay: tried to navigate back but had null instance!");
		return false;
	}

	// Token: 0x060022D7 RID: 8919 RVA: 0x000AD0E4 File Offset: 0x000AB2E4
	protected override bool OnNavigateBackImplementation()
	{
		if (!this.m_backButton.IsEnabled())
		{
			return false;
		}
		SceneMgr.Mode mode = (SceneMgr.Get() != null) ? SceneMgr.Get().GetMode() : SceneMgr.Mode.INVALID;
		if (mode <= SceneMgr.Mode.TOURNAMENT)
		{
			if (mode != SceneMgr.Mode.COLLECTIONMANAGER)
			{
				if (mode != SceneMgr.Mode.TOURNAMENT)
				{
					goto IL_16A;
				}
				this.BackOutToHub();
				GameMgr.Get().CancelFindGame();
				goto IL_16A;
			}
		}
		else
		{
			if (mode == SceneMgr.Mode.ADVENTURE)
			{
				AdventureConfig.Get().SubSceneGoBack(true);
				if (AdventureConfig.Get().CurrentSubScene == AdventureData.Adventuresubscene.PRACTICE)
				{
					PracticePickerTrayDisplay.Get().gameObject.SetActive(false);
				}
				GameMgr.Get().CancelFindGame();
				goto IL_16A;
			}
			if (mode - SceneMgr.Mode.TAVERN_BRAWL > 1)
			{
				goto IL_16A;
			}
		}
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (CollectionDeckTray.Get() != null)
		{
			CollectionDeckTray.Get().GetDecksContent().CreateNewDeckCancelled();
		}
		if (DeckPickerTrayDisplay.Get() != null && !DeckPickerTrayDisplay.Get().m_heroChosen && collectionManagerDisplay != null)
		{
			collectionManagerDisplay.CancelSelectNewDeckHeroMode();
		}
		if (HeroPickerDisplay.Get() != null)
		{
			HeroPickerDisplay.Get().HideTray(0f);
		}
		PresenceMgr.Get().SetPrevStatus();
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			TavernBrawlDisplay.Get().EnablePlayButton();
		}
		if (collectionManagerDisplay != null)
		{
			DeckTemplatePicker deckTemplatePicker = UniversalInputManager.UsePhoneUI ? collectionManagerDisplay.GetPhoneDeckTemplateTray() : collectionManagerDisplay.m_pageManager.GetDeckTemplatePicker();
			if (deckTemplatePicker != null)
			{
				Navigation.RemoveHandler(new Navigation.NavigateBackHandler(deckTemplatePicker.OnNavigateBack));
			}
		}
		IL_16A:
		return base.OnNavigateBackImplementation();
	}

	// Token: 0x060022D8 RID: 8920 RVA: 0x000AD261 File Offset: 0x000AB461
	protected override void GoBackUntilOnNavigateBackCalled()
	{
		Navigation.GoBackUntilOnNavigateBackCalled(new Navigation.NavigateBackHandler(DeckPickerTrayDisplay.OnNavigateBack));
	}

	// Token: 0x060022D9 RID: 8921 RVA: 0x000AD274 File Offset: 0x000AB474
	protected override void BackOutToHub()
	{
		base.BackOutToHub();
		if (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
		{
			return;
		}
		if (DeckPickerTrayDisplay.Get() != null && !this.IsShowingFirstPage())
		{
			DeckPickerTrayDisplay.Get().SuckInPreconDecks();
		}
	}

	// Token: 0x060022DA RID: 8922 RVA: 0x000AD2A9 File Offset: 0x000AB4A9
	public override void PreUnload()
	{
		if (!this.IsShowingFirstPage() && this.m_randomDeckPickerTray.activeSelf)
		{
			this.HideRandomDeckPickerTray();
		}
	}

	// Token: 0x060022DB RID: 8923 RVA: 0x000AD2C6 File Offset: 0x000AB4C6
	private void ShowNextPage(bool skipTraySlidingAnimation = false)
	{
		this.ShowPage(this.m_currentPageIndex + 1, skipTraySlidingAnimation);
	}

	// Token: 0x060022DC RID: 8924 RVA: 0x000AD2D7 File Offset: 0x000AB4D7
	private void ShowPreviousPage(bool skipTraySlidingAnimation = false)
	{
		this.ShowPage(this.m_currentPageIndex - 1, skipTraySlidingAnimation);
	}

	// Token: 0x060022DD RID: 8925 RVA: 0x000AD2E8 File Offset: 0x000AB4E8
	private void ShowPage(int pageNum, bool skipTraySlidingAnimation = false)
	{
		if (iTween.Count(this.m_randomDeckPickerTray) > 0)
		{
			return;
		}
		if (pageNum < 0 || pageNum >= this.m_customPages.Count)
		{
			return;
		}
		for (int i = 0; i < this.m_customPages.Count; i++)
		{
			this.m_customPages[i].gameObject.SetActive((i == this.m_currentPageIndex && !skipTraySlidingAnimation) || i == pageNum);
			if (skipTraySlidingAnimation)
			{
				Vector3 localPosition = this.m_customDeckPageUpperBone.transform.localPosition;
				if (i < pageNum)
				{
					localPosition = this.m_customDeckPageHideBone.transform.localPosition;
				}
				else if (i > pageNum)
				{
					localPosition = this.m_customDeckPageLowerBone.transform.localPosition;
				}
				this.m_customPages[i].gameObject.transform.localPosition = localPosition;
			}
		}
		if (this.m_currentPageIndex != pageNum && !skipTraySlidingAnimation)
		{
			GameObject currentPage = this.m_customPages[this.m_currentPageIndex].gameObject;
			GameObject gameObject = this.m_customPages[pageNum].gameObject;
			if (pageNum > this.m_currentPageIndex)
			{
				iTween.MoveTo(currentPage, iTween.Hash(new object[]
				{
					"time",
					0.25f,
					"position",
					this.m_customDeckPageHideBone.transform.localPosition,
					"isLocal",
					true,
					"easetype",
					iTween.EaseType.easeOutCubic,
					"oncomplete",
					new Action<object>(delegate(object e)
					{
						currentPage.SetActive(false);
					}),
					"oncompletetarget",
					base.gameObject
				}));
				iTween.MoveTo(gameObject, iTween.Hash(new object[]
				{
					"time",
					0.25f,
					"delay",
					0.25f,
					"easetype",
					iTween.EaseType.easeOutCubic,
					"position",
					this.m_customDeckPageUpperBone.transform.localPosition,
					"isLocal",
					true
				}));
			}
			else
			{
				iTween.MoveTo(currentPage, iTween.Hash(new object[]
				{
					"time",
					0.25f,
					"easetype",
					iTween.EaseType.easeOutCubic,
					"position",
					this.m_customDeckPageLowerBone.transform.localPosition,
					"isLocal",
					true
				}));
				iTween.MoveTo(gameObject, iTween.Hash(new object[]
				{
					"time",
					0.25f,
					"delay",
					0.25f,
					"easetype",
					iTween.EaseType.easeOutCubic,
					"position",
					this.m_customDeckPageUpperBone.transform.localPosition,
					"isLocal",
					true
				}));
			}
		}
		this.m_currentPageIndex = pageNum;
		this.HideAllPreconHighlights();
		this.LowerHeroButtons();
		if (this.ShouldHandleBoxTransition() || skipTraySlidingAnimation)
		{
			Box.Get().AddTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxTransitionFinished));
			this.HideRandomDeckPickerTray();
		}
		else
		{
			iTween.MoveTo(this.m_randomDeckPickerTray, iTween.Hash(new object[]
			{
				"time",
				0.25f,
				"position",
				this.m_randomDecksHiddenBone.transform.localPosition,
				"oncomplete",
				new Action<object>(delegate(object e)
				{
					this.HideRandomDeckPickerTray();
				}),
				"oncompletetarget",
				base.gameObject,
				"isLocal",
				true,
				"delay",
				0f
			}));
		}
		this.UpdatePageArrows();
		Options.Get().SetBool(Option.HAS_SEEN_CUSTOM_DECK_PICKER, true);
	}

	// Token: 0x060022DE RID: 8926 RVA: 0x000AD706 File Offset: 0x000AB906
	private IEnumerator ArrowDelayedActivate(UIBButton arrow, float delay)
	{
		yield return new WaitForSeconds(delay);
		arrow.gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x060022DF RID: 8927 RVA: 0x000AD71C File Offset: 0x000AB91C
	private bool ShouldHandleBoxTransition()
	{
		return SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.GAMEPLAY && (Box.Get().IsBusy() || Box.Get().GetState() == Box.State.LOADING || Box.Get().GetState() == Box.State.LOADING_HUB);
	}

	// Token: 0x060022E0 RID: 8928 RVA: 0x000AD756 File Offset: 0x000AB956
	private void OnBoxTransitionFinished(object userData)
	{
		if (this.m_randomDeckPickerTray != null && this.IsShowingFirstPage())
		{
			this.ShowBasicDeckPickerTray();
		}
		Box.Get().RemoveTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxTransitionFinished));
	}

	// Token: 0x060022E1 RID: 8929 RVA: 0x000AD78B File Offset: 0x000AB98B
	private void OnScenePreUnload(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		this.HideSetRotationNotifications();
		SceneMgr.Get().UnregisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
	}

	// Token: 0x060022E2 RID: 8930 RVA: 0x000AD7AC File Offset: 0x000AB9AC
	private void LowerHeroButtons()
	{
		foreach (HeroPickerButton heroPickerButton in this.m_heroButtons)
		{
			if (heroPickerButton.gameObject.activeSelf)
			{
				heroPickerButton.Lower();
			}
		}
	}

	// Token: 0x060022E3 RID: 8931 RVA: 0x000AD80C File Offset: 0x000ABA0C
	private void RaiseHeroButtons()
	{
		foreach (HeroPickerButton heroPickerButton in this.m_heroButtons)
		{
			if (heroPickerButton.gameObject.activeSelf)
			{
				heroPickerButton.Raise();
			}
		}
	}

	// Token: 0x060022E4 RID: 8932 RVA: 0x000AD86C File Offset: 0x000ABA6C
	protected void SetKeyholeTextureOffsets(UnityEngine.Vector2 offset)
	{
		if (base.IsChoosingHero())
		{
			return;
		}
		int materialIndex = UniversalInputManager.UsePhoneUI ? 1 : 0;
		foreach (HeroPickerButton heroPickerButton in this.m_heroButtons)
		{
			Renderer component = heroPickerButton.m_buttonFrame.GetComponent<Renderer>();
			if (component == null)
			{
				Debug.LogWarning("Couldn't set keyhole texture offset on invalid renderer");
			}
			else
			{
				component.GetMaterial(materialIndex).mainTextureOffset = offset;
			}
		}
	}

	// Token: 0x060022E5 RID: 8933 RVA: 0x000AD900 File Offset: 0x000ABB00
	private void HideRandomDeckPickerTray()
	{
		this.m_randomDeckPickerTray.transform.localPosition = new Vector3(-5000f, -5000f, -5000f);
	}

	// Token: 0x060022E6 RID: 8934 RVA: 0x000AD926 File Offset: 0x000ABB26
	private void ShowBasicDeckPickerTray()
	{
		this.m_randomDeckPickerTray.transform.localPosition = this.m_randomDecksHiddenBone.transform.localPosition;
	}

	// Token: 0x060022E7 RID: 8935 RVA: 0x000AD948 File Offset: 0x000ABB48
	private void OnShowPreviousPage(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("hero_panel_slide_on.prefab:236147a924d7cb442872b46dddd56132");
		this.ShowPreviousPage(false);
	}

	// Token: 0x060022E8 RID: 8936 RVA: 0x000AD968 File Offset: 0x000ABB68
	private void ShowFirstPage(bool skipTraySlidingAnimation = false)
	{
		if (iTween.Count(this.m_randomDeckPickerTray) > 0)
		{
			return;
		}
		this.ShowBasicDeckPickerTray();
		this.m_currentPageIndex = 0;
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (base.IsChoosingHero())
		{
			if (this.m_modeLabelBg != null)
			{
				this.m_modeLabelBg.transform.localEulerAngles = new Vector3(180f, 0f, 0f);
			}
			if (skipTraySlidingAnimation)
			{
				this.m_randomDeckPickerTray.transform.localPosition = this.m_randomDecksShownBone.transform.localPosition;
				this.RaiseHeroButtons();
			}
			else
			{
				iTween.MoveTo(this.m_randomDeckPickerTray, iTween.Hash(new object[]
				{
					"time",
					0.25f,
					"position",
					this.m_randomDecksShownBone.transform.localPosition,
					"isLocal",
					true,
					"oncomplete",
					"RaiseHeroButtons",
					"oncompletetarget",
					base.gameObject
				}));
			}
		}
		else if (mode == SceneMgr.Mode.ADVENTURE || mode == SceneMgr.Mode.TOURNAMENT || mode == SceneMgr.Mode.FRIENDLY || mode == SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			for (int i = 0; i < this.m_customPages.Count; i++)
			{
				this.m_customPages[i].gameObject.SetActive(i == 0);
			}
			if (skipTraySlidingAnimation)
			{
				this.m_randomDeckPickerTray.transform.localPosition = this.m_randomDecksShownBone.transform.localPosition;
				this.RaiseHeroButtons();
			}
			else
			{
				iTween.MoveTo(this.m_randomDeckPickerTray, iTween.Hash(new object[]
				{
					"time",
					0.25f,
					"position",
					this.m_randomDecksShownBone.transform.localPosition,
					"isLocal",
					true,
					"oncomplete",
					"RaiseHeroButtons",
					"oncompletetarget",
					base.gameObject
				}));
			}
		}
		this.UpdatePageArrows();
	}

	// Token: 0x060022E9 RID: 8937 RVA: 0x000ADB80 File Offset: 0x000ABD80
	private void SuckInPreconDecks()
	{
		this.ShowBasicDeckPickerTray();
		iTween.MoveTo(this.m_randomDeckPickerTray, iTween.Hash(new object[]
		{
			"time",
			0.25f,
			"position",
			this.m_suckedInRandomDecksBone.transform.localPosition,
			"isLocal",
			true,
			"oncomplete",
			"SuckInFinished",
			"oncompletetarget",
			base.gameObject
		}));
	}

	// Token: 0x060022EA RID: 8938 RVA: 0x000ADC14 File Offset: 0x000ABE14
	private void OnCustomDeckPressed(CollectionDeckBoxVisual deckbox)
	{
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT && Options.GetInRankedPlayMode())
		{
			CollectionDeck collectionDeck = deckbox.GetCollectionDeck();
			if (collectionDeck == null)
			{
				return;
			}
			if (collectionDeck.FormatType == PegasusShared.FormatType.FT_CLASSIC && Options.GetFormatType() != PegasusShared.FormatType.FT_CLASSIC)
			{
				this.ShowClickedClassicDeckInNonClassicPopup();
				return;
			}
			if (collectionDeck.FormatType == PegasusShared.FormatType.FT_STANDARD && Options.GetFormatType() == PegasusShared.FormatType.FT_CLASSIC)
			{
				this.ShowClickedStandardDeckInClassicPopup();
				return;
			}
			if (collectionDeck.FormatType == PegasusShared.FormatType.FT_WILD && Options.GetFormatType() == PegasusShared.FormatType.FT_CLASSIC)
			{
				this.ShowClickedWildDeckInClassicPopup();
				return;
			}
			if (!deckbox.IsDeckSelectableForCurrentMode())
			{
				return;
			}
			if (this.HandleClickToFixDeck(deckbox.GetCollectionDeck(), null, deckbox.CanClickToConvertToStandard(), deckbox.IsMissingCards))
			{
				return;
			}
		}
		else
		{
			if (!deckbox.IsDeckSelectableForCurrentMode())
			{
				return;
			}
			if (this.HandleClickToFixDeck(deckbox.GetCollectionDeck(), null, false, deckbox.IsMissingCards))
			{
				return;
			}
		}
		this.SelectCustomDeck(deckbox);
	}

	// Token: 0x060022EB RID: 8939 RVA: 0x000ADCD4 File Offset: 0x000ABED4
	private void SelectCustomDeck(CollectionDeckBoxVisual deckbox)
	{
		this.HideDemoQuotes();
		if (!this.m_validClasses.Contains(deckbox.GetClass()))
		{
			deckbox.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
			this.ShowInvalidClassPopup();
			return;
		}
		this.SetPlayButtonEnabled(true);
		base.RemoveHeroLockedTooltip();
		Options.Get().SetLong(Option.LAST_CUSTOM_DECK_CHOSEN, deckbox.GetDeckID());
		deckbox.SetIsSelected(true);
		if (AbsDeckPickerTrayDisplay.HIGHLIGHT_SELECTED_DECK)
		{
			deckbox.SetHighlightState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
		else
		{
			deckbox.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
		}
		if (this.m_selectedCustomDeckBox != null && this.m_selectedCustomDeckBox != deckbox)
		{
			this.m_selectedCustomDeckBox.SetIsSelected(false);
			this.m_selectedCustomDeckBox.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
		}
		this.m_selectedCustomDeckBox = deckbox;
		this.UpdateHeroInfo(deckbox);
		base.ShowPreconHero(true);
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_slidingTray.ToggleTraySlider(true, null, true);
		}
	}

	// Token: 0x060022EC RID: 8940 RVA: 0x000ADDB4 File Offset: 0x000ABFB4
	private bool HandleClickToFixDeck(CollectionDeck deck, HeroPickerButton button, bool isClickToConvertCase, bool IsMissingCards)
	{
		if (deck == null)
		{
			return false;
		}
		global::DeckRuleset ruleset = deck.GetRuleset();
		if (ruleset != null && ruleset.EntityInDeckIgnoresRuleset(deck))
		{
			return false;
		}
		bool flag = false;
		foreach (CollectionDeckSlot collectionDeckSlot in deck.GetSlots())
		{
			if (GameDbf.GetIndex().HasCardPlayerDeckOverride(collectionDeckSlot.CardID))
			{
				flag = true;
				break;
			}
		}
		if (isClickToConvertCase && flag)
		{
			this.ShowClickedWildDeckInStandardPopup();
			return true;
		}
		if (IsMissingCards | isClickToConvertCase)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_showAlertIcon = false;
			popupInfo.m_confirmText = GameStrings.Get("GLOBAL_BUTTON_YES");
			popupInfo.m_cancelText = GameStrings.Get("GLOBAL_BUTTON_NO");
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			if (isClickToConvertCase)
			{
				popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_DECK_WILD_POPUP_HEADER");
				popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_DECK_WILD_POPUP_TEXT");
				popupInfo.m_confirmText = GameStrings.Get("GLUE_COLLECTION_DECK_WILD_POPUP_CONFIRM");
				popupInfo.m_cancelText = GameStrings.Get("GLUE_COLLECTION_DECK_WILD_POPUP_CANCEL");
			}
			else
			{
				int num = deck.GetMaxCardCount() - deck.GetTotalValidCardCount();
				popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_DECK_INCOMPLETE_POPUP_HEADER");
				if (CollectionManager.Get().ShouldAccountSeeStandardWild())
				{
					popupInfo.m_text = GameStrings.Format("GLUE_COLLECTION_DECK_INCOMPLETE_POPUP_TEXT", new object[]
					{
						num
					});
				}
				else
				{
					popupInfo.m_text = GameStrings.Format("GLUE_COLLECTION_DECK_INCOMPLETE_POPUP_TEXT_NPR_NEW", new object[]
					{
						num
					});
				}
			}
			popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response == AlertPopup.Response.CONFIRM)
				{
					if (isClickToConvertCase)
					{
						deck.FormatType = PegasusShared.FormatType.FT_STANDARD;
					}
					deck.RemoveInvalidCards();
					if (isClickToConvertCase)
					{
						CollectionManager.Get().AutoFillDeck(deck, true, new CollectionManager.DeckAutoFillCallback(this.OnAutoFillDeckConvertedToStandard));
					}
					else
					{
						CollectionManager.Get().AutoFillDeck(deck, true, new CollectionManager.DeckAutoFillCallback(this.OnAutoFillDeckFilledMissingCards));
					}
					if (button != null)
					{
						button.SetIsDeckValid(true);
						return;
					}
				}
				else if (isClickToConvertCase)
				{
					this.ShowClickedWildDeckInStandardPopup();
				}
			};
			DialogManager.Get().ShowPopup(popupInfo);
			return true;
		}
		return false;
	}

	// Token: 0x060022ED RID: 8941 RVA: 0x000ADFB0 File Offset: 0x000AC1B0
	private void OnAutoFillDeckConvertedToStandard(CollectionDeck deck, IEnumerable<DeckMaker.DeckFill> fillCards)
	{
		this.OnAutoFillDeckComplete(deck, fillCards, true);
	}

	// Token: 0x060022EE RID: 8942 RVA: 0x000ADFBB File Offset: 0x000AC1BB
	private void OnAutoFillDeckFilledMissingCards(CollectionDeck deck, IEnumerable<DeckMaker.DeckFill> fillCards)
	{
		this.OnAutoFillDeckComplete(deck, fillCards, false);
	}

	// Token: 0x060022EF RID: 8943 RVA: 0x000ADFC8 File Offset: 0x000AC1C8
	private void OnAutoFillDeckComplete(CollectionDeck deck, IEnumerable<DeckMaker.DeckFill> fillCards, bool setFormatToStandard)
	{
		if (deck == null)
		{
			return;
		}
		deck.FillFromCardList(fillCards);
		CustomDeckPage currentCustomPage = this.GetCurrentCustomPage();
		if (currentCustomPage != null)
		{
			currentCustomPage.UpdateDeckVisuals();
			CollectionDeckBoxVisual collectionDeckBoxVisual = currentCustomPage.FindDeckVisual(deck);
			if (collectionDeckBoxVisual != null)
			{
				this.SelectCustomDeck(collectionDeckBoxVisual);
				if (!UniversalInputManager.UsePhoneUI)
				{
					collectionDeckBoxVisual.PlayGlowAnim(setFormatToStandard);
				}
			}
		}
	}

	// Token: 0x060022F0 RID: 8944 RVA: 0x000AE021 File Offset: 0x000AC221
	protected override void OnHeroButtonReleased(UIEvent e)
	{
		base.OnHeroButtonReleased(e);
		this.HideDemoQuotes();
	}

	// Token: 0x060022F1 RID: 8945 RVA: 0x000AE030 File Offset: 0x000AC230
	protected override void SelectHero(HeroPickerButton button, bool showTrayForPhone = true)
	{
		if (button == this.m_selectedHeroButton && !UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		if (!base.IsChoosingHero() && !button.IsDeckValid())
		{
			return;
		}
		base.SelectHero(button, showTrayForPhone);
		Options.Get().SetInt(Option.LAST_PRECON_HERO_CHOSEN, (int)button.m_heroClass);
		if (button.IsLocked())
		{
			string shortName = button.GetEntityDef().GetShortName();
			string className = GameStrings.GetClassName(button.m_heroClass);
			base.AddHeroLockedTooltip(GameStrings.Get("GLUE_HERO_LOCKED_NAME"), GameStrings.Format("GLUE_HERO_LOCKED_DESC", new object[]
			{
				shortName,
				className
			}));
		}
	}

	// Token: 0x060022F2 RID: 8946 RVA: 0x000AE0CC File Offset: 0x000AC2CC
	private void Deselect()
	{
		if (this.m_selectedHeroButton == null && this.m_selectedCustomDeckBox == null)
		{
			return;
		}
		this.SetPlayButtonEnabled(false);
		if (this.m_heroLockedTooltip != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_heroLockedTooltip.gameObject);
		}
		if (this.m_selectedCustomDeckBox != null)
		{
			this.m_selectedCustomDeckBox.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
			this.m_selectedCustomDeckBox.SetEnabled(true, false);
			this.m_selectedCustomDeckBox.SetIsSelected(false);
			this.m_selectedCustomDeckBox = null;
		}
		this.m_heroActor.SetEntityDef(null);
		this.m_heroActor.SetCardDef(null);
		this.m_heroActor.Hide();
		if (this.m_selectedHeroButton != null)
		{
			this.m_selectedHeroButton.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
			this.m_selectedHeroButton.SetSelected(false);
			this.m_selectedHeroButton = null;
		}
		if (this.ShouldShowHeroPower())
		{
			this.m_heroPowerActor.SetCardDef(null);
			this.m_heroPowerActor.SetEntityDef(null);
			this.m_heroPowerActor.Hide();
			this.m_goldenHeroPowerActor.SetCardDef(null);
			this.m_goldenHeroPowerActor.SetEntityDef(null);
			this.m_goldenHeroPowerActor.Hide();
			this.m_heroPower.GetComponent<Collider>().enabled = false;
			this.m_goldenHeroPower.GetComponent<Collider>().enabled = false;
			if (this.m_heroPowerShadowQuad != null)
			{
				this.m_heroPowerShadowQuad.SetActive(false);
			}
		}
		this.m_selectedHeroPowerFullDef = null;
		if (this.m_heroPowerBigCard != null)
		{
			iTween.Stop(this.m_heroPowerBigCard.gameObject);
			this.m_heroPowerBigCard.Hide();
		}
		if (this.m_goldenHeroPowerBigCard != null)
		{
			iTween.Stop(this.m_goldenHeroPowerBigCard.gameObject);
			this.m_goldenHeroPowerBigCard.Hide();
		}
		this.m_selectedHeroName = null;
		this.m_heroName.Text = "";
	}

	// Token: 0x060022F3 RID: 8947 RVA: 0x000AE2A4 File Offset: 0x000AC4A4
	private void UpdateHeroInfo(CollectionDeckBoxVisual deckBox)
	{
		using (DefLoader.DisposableFullDef disposableFullDef = deckBox.SharedDisposableFullDef())
		{
			this.UpdateHeroInfo(disposableFullDef, deckBox.GetDeckNameText().Text, deckBox.GetHeroCardPremium(), false);
		}
	}

	// Token: 0x060022F4 RID: 8948 RVA: 0x000AE2F0 File Offset: 0x000AC4F0
	protected override void UpdateHeroInfo(HeroPickerButton button)
	{
		using (DefLoader.DisposableFullDef disposableFullDef = button.ShareFullDef())
		{
			string name = disposableFullDef.EntityDef.GetName();
			string cardID = disposableFullDef.EntityDef.GetCardId();
			TAG_CARD_SET cardSet = disposableFullDef.EntityDef.GetCardSet();
			if (TAG_CARD_SET.HERO_SKINS == cardSet)
			{
				cardID = CollectionManager.GetHeroCardId(disposableFullDef.EntityDef.GetClass(), CardHero.HeroType.VANILLA);
			}
			this.UpdateHeroInfo(disposableFullDef, name, CollectionManager.Get().GetBestCardPremium(cardID), button.IsLocked());
		}
	}

	// Token: 0x060022F5 RID: 8949 RVA: 0x000AE374 File Offset: 0x000AC574
	private void UpdateHeroInfo(DefLoader.DisposableFullDef fullDef, string heroName, TAG_PREMIUM premium, bool locked = false)
	{
		this.m_heroName.Text = heroName;
		this.m_selectedHeroName = fullDef.EntityDef.GetName();
		this.m_heroActor.SetPremium(premium);
		this.m_heroActor.SetFullDef(fullDef);
		this.m_heroActor.UpdateAllComponents();
		this.m_heroActor.SetUnlit();
		NetCache.HeroLevel heroLevel = (!locked) ? GameUtils.GetHeroLevel(fullDef.EntityDef.GetClass()) : null;
		int totalLevel = GameUtils.GetTotalHeroLevel() ?? 0;
		this.m_xpBar.UpdateDisplay(heroLevel, totalLevel);
		string heroPowerCardIdFromHero = GameUtils.GetHeroPowerCardIdFromHero(fullDef.EntityDef.GetCardId());
		if (!locked && this.ShouldShowHeroPower() && !string.IsNullOrEmpty(heroPowerCardIdFromHero))
		{
			this.m_heroPowerContainer.SetActive(true);
			base.UpdateHeroPowerInfo(this.m_heroPowerDefs[heroPowerCardIdFromHero], premium);
		}
		else
		{
			base.SetHeroPowerActorColliderEnabled(false);
			base.HideHeroPowerActor();
			this.m_heroPowerContainer.SetActive(false);
		}
		this.UpdateRankedClassWinsPlate();
	}

	// Token: 0x060022F6 RID: 8950 RVA: 0x000AE474 File Offset: 0x000AC674
	protected override void TransitionToFormatType(PegasusShared.FormatType formatType, bool inRankedPlayMode, float transitionSpeed = 2f)
	{
		VisualsFormatType visualsFormatType = VisualsFormatTypeExtensions.ToVisualsFormatType(this.m_PreviousFormatType, this.m_PreviousInRankedPlayMode);
		VisualsFormatType visualsFormatType2 = VisualsFormatTypeExtensions.ToVisualsFormatType(formatType, inRankedPlayMode);
		this.m_PreviousFormatType = formatType;
		this.m_PreviousInRankedPlayMode = inRankedPlayMode;
		base.TransitionToFormatType(formatType, inRankedPlayMode, transitionSpeed);
		this.UpdateTrayBackgroundTransitionValues(visualsFormatType, visualsFormatType2, transitionSpeed);
		if (!UniversalInputManager.UsePhoneUI)
		{
			if (!inRankedPlayMode)
			{
				this.m_casualPlayDisplayWidget.Show();
				this.m_rankedPlayDisplay.Hide(this.m_rankedPlayDisplayHideDelay);
			}
			else
			{
				this.m_casualPlayDisplayWidget.Hide();
				if (this.m_rankedPlayDisplay != null)
				{
					this.m_rankedPlayDisplay.Show(this.m_rankedPlayDisplayShowDelay);
					this.m_rankedPlayDisplay.OnSwitchFormat(visualsFormatType2);
				}
			}
		}
		base.UpdateValidHeroClasses();
		if (this.m_inHeroPicker && ((visualsFormatType == VisualsFormatType.VFT_CLASSIC && visualsFormatType2 != VisualsFormatType.VFT_CLASSIC) || (visualsFormatType != VisualsFormatType.VFT_CLASSIC && visualsFormatType2 == VisualsFormatType.VFT_CLASSIC)))
		{
			this.Deselect();
			base.StartCoroutine(base.LoadHeroButtons(null));
		}
		this.PlayTrayTransitionSound(visualsFormatType2);
		this.PlayTrayTransitionGlowBursts(visualsFormatType, visualsFormatType2);
	}

	// Token: 0x060022F7 RID: 8951 RVA: 0x000AE568 File Offset: 0x000AC768
	private void UpdateTrayBackgroundTransitionValues(VisualsFormatType oldVisualsFormatType, VisualsFormatType visualsFormatType, float transitionSpeed = 2f)
	{
		float targetValue = 1f;
		Texture textureForFormat = this.m_currentModeTextures.GetTextureForFormat(oldVisualsFormatType);
		Texture customTextureForFormat = this.m_currentModeTextures.GetCustomTextureForFormat(oldVisualsFormatType);
		Texture textureForFormat2 = this.m_currentModeTextures.GetTextureForFormat(visualsFormatType);
		Texture customTextureForFormat2 = this.m_currentModeTextures.GetCustomTextureForFormat(visualsFormatType);
		if (UniversalInputManager.UsePhoneUI && this.m_slidingTray.IsShown())
		{
			this.m_detailsTrayFrame.GetComponentInChildren<Renderer>().GetMaterial().mainTexture = textureForFormat2;
		}
		this.SetCustomDeckPageTextures(customTextureForFormat, customTextureForFormat2);
		if (UniversalInputManager.UsePhoneUI)
		{
			this.SetPhoneDetailsTrayTextures(textureForFormat, textureForFormat2);
		}
		else
		{
			this.SetTrayFrameAndBasicDeckPageTextures(textureForFormat, textureForFormat2);
		}
		base.StopCoroutine("TransitionTrayMaterial");
		base.StartCoroutine(this.TransitionTrayMaterial(targetValue, transitionSpeed));
	}

	// Token: 0x060022F8 RID: 8952 RVA: 0x000AE620 File Offset: 0x000AC820
	private void PlayTrayTransitionGlowBursts(VisualsFormatType oldVisualsFormatType, VisualsFormatType visualsFormatType)
	{
		if (oldVisualsFormatType == visualsFormatType)
		{
			return;
		}
		if (this.m_customPages != null && (oldVisualsFormatType == VisualsFormatType.VFT_WILD || visualsFormatType == VisualsFormatType.VFT_WILD))
		{
			bool useFX = oldVisualsFormatType == VisualsFormatType.VFT_WILD;
			bool flag = this.GetNumValidStandardDecks() > 0U;
			if (this.m_customPages.Count > 1 && !this.IsShowingFirstPage())
			{
				this.m_customPages[1].PlayVineGlowBurst(useFX, flag);
			}
			else if (this.m_customPages.Count > 0)
			{
				if (flag)
				{
					GameObject[] customVineGlowToggle = this.m_customPages[0].m_customVineGlowToggle;
					for (int i = 0; i < customVineGlowToggle.Length; i++)
					{
						customVineGlowToggle[i].SetActive(true);
					}
				}
				this.m_customPages[0].PlayVineGlowBurst(useFX, flag);
			}
		}
		if (this.m_inHeroPicker)
		{
			this.PlayTransitionGlowBurstsForNewDeckFSMs(oldVisualsFormatType, visualsFormatType);
			return;
		}
		this.PlayTransitionGlowBurstsForNonNewDeckFSMs(oldVisualsFormatType, visualsFormatType);
	}

	// Token: 0x060022F9 RID: 8953 RVA: 0x000AE6E8 File Offset: 0x000AC8E8
	private void PlayTransitionGlowBurstsForNonNewDeckFSMs(VisualsFormatType oldVisualsFormatType, VisualsFormatType visualsFormatType)
	{
		string text;
		switch (oldVisualsFormatType)
		{
		case VisualsFormatType.VFT_WILD:
			text = this.m_leavingWildGlowEvent;
			goto IL_37;
		case VisualsFormatType.VFT_CLASSIC:
			text = this.m_leavingClassicGlowEvent;
			goto IL_37;
		case VisualsFormatType.VFT_CASUAL:
			text = this.m_leavingCasualGlowEvent;
			goto IL_37;
		}
		text = null;
		IL_37:
		if (!string.IsNullOrEmpty(text))
		{
			foreach (PlayMakerFSM playMakerFSM in this.formatChangeGlowFSMs)
			{
				if (playMakerFSM != null)
				{
					playMakerFSM.SendEvent(text);
				}
			}
			if (this.m_rankedPlayDisplay != null)
			{
				this.m_rankedPlayDisplay.PlayTransitionGlowBurstsForNonNewDeckFSMs(text);
			}
		}
		switch (visualsFormatType)
		{
		case VisualsFormatType.VFT_WILD:
			text = this.m_enteringWildGlowEvent;
			goto IL_CF;
		case VisualsFormatType.VFT_CLASSIC:
			text = this.m_enteringClassicGlowEvent;
			goto IL_CF;
		case VisualsFormatType.VFT_CASUAL:
			text = this.m_enteringCasualGlowEvent;
			goto IL_CF;
		}
		text = null;
		IL_CF:
		if (!string.IsNullOrEmpty(text))
		{
			foreach (PlayMakerFSM playMakerFSM2 in this.formatChangeGlowFSMs)
			{
				if (playMakerFSM2 != null)
				{
					playMakerFSM2.SendEvent(text);
				}
			}
			if (this.m_rankedPlayDisplay != null)
			{
				this.m_rankedPlayDisplay.PlayTransitionGlowBurstsForNonNewDeckFSMs(text);
			}
		}
	}

	// Token: 0x060022FA RID: 8954 RVA: 0x000AE844 File Offset: 0x000ACA44
	private void PlayTransitionGlowBurstsForNewDeckFSMs(VisualsFormatType oldVisualsFormatType, VisualsFormatType visualsFormatType)
	{
		string text = null;
		if (oldVisualsFormatType == VisualsFormatType.VFT_CLASSIC && visualsFormatType != VisualsFormatType.VFT_CLASSIC)
		{
			text = this.m_newDeckLeavingClassicGlowEvent;
		}
		else if (oldVisualsFormatType != VisualsFormatType.VFT_CLASSIC && visualsFormatType == VisualsFormatType.VFT_CLASSIC)
		{
			text = this.m_newDeckEnteringClassicGlowEvent;
		}
		else if (oldVisualsFormatType == VisualsFormatType.VFT_WILD && visualsFormatType != VisualsFormatType.VFT_WILD)
		{
			text = this.m_newDeckLeavingWildGlowEvent;
		}
		else if (oldVisualsFormatType != VisualsFormatType.VFT_WILD && visualsFormatType == VisualsFormatType.VFT_WILD)
		{
			text = this.m_newDeckEnteringWildGlowEvent;
		}
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		foreach (PlayMakerFSM playMakerFSM in this.newDeckFormatChangeGlowFSMs)
		{
			if (playMakerFSM != null)
			{
				playMakerFSM.SendEvent(text);
			}
		}
		if (this.m_rankedPlayDisplay != null)
		{
			this.m_rankedPlayDisplay.PlayTransitionGlowBurstsForNewDeckFSMs(text);
		}
	}

	// Token: 0x060022FB RID: 8955 RVA: 0x000AE908 File Offset: 0x000ACB08
	private void PlayTrayTransitionSound(VisualsFormatType visualsFormatType)
	{
		Box.State state = Box.Get().GetState();
		if (state == Box.State.LOADING || (state == Box.State.SET_ROTATION_OPEN && this.m_setRotationTutorialState == DeckPickerTrayDisplay.SetRotationTutorialState.PREPARING))
		{
			return;
		}
		string text;
		switch (visualsFormatType)
		{
		case VisualsFormatType.VFT_WILD:
		case VisualsFormatType.VFT_CASUAL:
			text = this.m_wildTransitionSound;
			break;
		case VisualsFormatType.VFT_STANDARD:
			text = this.m_standardTransitionSound;
			break;
		case VisualsFormatType.VFT_CLASSIC:
			text = this.m_classicTransitionSound;
			break;
		default:
			Debug.LogError("No transition sound for format " + visualsFormatType.ToString());
			text = "";
			break;
		}
		if (!string.IsNullOrEmpty(text))
		{
			SoundManager.Get().LoadAndPlay(text);
		}
	}

	// Token: 0x060022FC RID: 8956 RVA: 0x000AE9A2 File Offset: 0x000ACBA2
	private IEnumerator TransitionTrayMaterial(float targetValue, float speed)
	{
		Material trayMat = null;
		Material randomTrayMat = null;
		float currentValue;
		if (UniversalInputManager.UsePhoneUI)
		{
			trayMat = null;
			randomTrayMat = this.m_basicDeckPage.GetComponent<Renderer>().GetMaterial();
			currentValue = randomTrayMat.GetFloat("_Transistion");
		}
		else
		{
			trayMat = this.m_trayFrame.GetComponentInChildren<Renderer>().GetMaterial();
			currentValue = trayMat.GetFloat("_Transistion");
			Renderer componentInChildren = this.m_basicDeckPage.GetComponentInChildren<Renderer>();
			if (componentInChildren != null)
			{
				randomTrayMat = componentInChildren.GetMaterial();
			}
		}
		do
		{
			currentValue = Mathf.MoveTowards(currentValue, targetValue, speed * Time.deltaTime);
			if (trayMat != null)
			{
				trayMat.SetFloat("_Transistion", currentValue);
			}
			if (randomTrayMat != null)
			{
				randomTrayMat.SetFloat("_Transistion", currentValue);
			}
			if (this.m_customPages != null)
			{
				foreach (CustomDeckPage customDeckPage in this.m_customPages)
				{
					customDeckPage.UpdateTrayTransitionValue(currentValue);
				}
			}
			yield return null;
		}
		while (currentValue != targetValue);
		yield break;
	}

	// Token: 0x060022FD RID: 8957 RVA: 0x000AE9C0 File Offset: 0x000ACBC0
	private void SetTrayFrameAndBasicDeckPageTextures(Texture mainTexture, Texture transitionToTexture)
	{
		Material material = this.m_trayFrame.GetComponentInChildren<Renderer>().GetMaterial();
		material.mainTexture = mainTexture;
		material.SetTexture("_MainTex2", transitionToTexture);
		material.SetFloat("_Transistion", 0f);
		Renderer componentInChildren = this.m_basicDeckPage.GetComponentInChildren<Renderer>();
		if (componentInChildren != null)
		{
			Material material2 = componentInChildren.GetMaterial();
			material2.mainTexture = mainTexture;
			material2.SetTexture("_MainTex2", transitionToTexture);
			material2.SetFloat("_Transistion", 0f);
		}
	}

	// Token: 0x060022FE RID: 8958 RVA: 0x000AEA3C File Offset: 0x000ACC3C
	private void SetCustomDeckPageTextures(Texture transitionFromTexture, Texture targetTexture)
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			Material material = this.m_basicDeckPage.GetComponent<Renderer>().GetMaterial();
			material.mainTexture = transitionFromTexture;
			material.SetTexture("_MainTex2", targetTexture);
			material.SetFloat("_Transistion", 0f);
		}
		if (this.m_customPages != null)
		{
			foreach (CustomDeckPage customDeckPage in this.m_customPages)
			{
				customDeckPage.SetTrayTextures(transitionFromTexture, targetTexture);
			}
		}
	}

	// Token: 0x060022FF RID: 8959 RVA: 0x000AEAD4 File Offset: 0x000ACCD4
	private void SetPhoneDetailsTrayTextures(Texture transitionFromTexture, Texture targetTexture)
	{
		Material sharedMaterial = this.m_detailsTrayFrame.GetComponent<Renderer>().GetSharedMaterial();
		sharedMaterial.mainTexture = transitionFromTexture;
		sharedMaterial.SetTexture("_MainTex2", targetTexture);
		sharedMaterial.SetFloat("_Transistion", 0f);
	}

	// Token: 0x06002300 RID: 8960 RVA: 0x000AEB08 File Offset: 0x000ACD08
	private void OnRankedPlayDisplayWidgetReady()
	{
		this.m_rankedPlayDisplayWidget.transform.SetParent(this.m_rankedPlayDisplayWidgetBone, false);
		this.m_rankedPlayDisplay = this.m_rankedPlayDisplayWidget.GetComponentInChildren<RankedPlayDisplay>();
		VisualsFormatType currentVisualsFormatType = VisualsFormatTypeExtensions.GetCurrentVisualsFormatType();
		this.UpdateRankedPlayDisplay(currentVisualsFormatType);
		base.StartCoroutine(this.SetRankedMedalWhenReady());
	}

	// Token: 0x06002301 RID: 8961 RVA: 0x000AEB58 File Offset: 0x000ACD58
	private void OnFormatTypePickerPopupReady()
	{
		this.m_formatTypePickerWidget.transform.SetParent(base.gameObject.transform);
		this.m_formatTypePickerWidget.RegisterEventListener(new Widget.EventListenerDelegate(this.OnFormatTypePickerPopupEvent));
		bool flag = CollectionManager.Get().ShouldAccountSeeStandardWild();
		bool inHeroPicker = this.m_inHeroPicker;
		if (flag)
		{
			this.m_formatTypePickerWidget.TriggerEvent(inHeroPicker ? "3BUTTONS" : "4BUTTONS", default(Widget.TriggerEventParameters));
			return;
		}
		this.m_formatTypePickerWidget.TriggerEvent("2BUTTONS", default(Widget.TriggerEventParameters));
	}

	// Token: 0x06002302 RID: 8962 RVA: 0x000AEBEC File Offset: 0x000ACDEC
	private void OnFormatTypePickerPopupEvent(string eventName)
	{
		if (eventName == "WILD_BUTTON_CLICKED")
		{
			this.SwitchFormatTypeAndRankedPlayMode(VisualsFormatType.VFT_WILD);
			return;
		}
		if (eventName == "STANDARD_BUTTON_CLICKED")
		{
			this.SwitchFormatTypeAndRankedPlayMode(VisualsFormatType.VFT_STANDARD);
			return;
		}
		if (eventName == "CLASSIC_BUTTON_CLICKED")
		{
			this.SwitchFormatTypeAndRankedPlayMode(VisualsFormatType.VFT_CLASSIC);
			return;
		}
		if (eventName == "CASUAL_BUTTON_CLICKED")
		{
			this.SwitchFormatTypeAndRankedPlayMode(VisualsFormatType.VFT_CASUAL);
			return;
		}
		if (eventName == "HIDE")
		{
			this.OnFormatTypeSwitchCancelled();
		}
	}

	// Token: 0x06002303 RID: 8963 RVA: 0x000AEC60 File Offset: 0x000ACE60
	private IEnumerator SetRankedMedalWhenReady()
	{
		while (TournamentDisplay.Get().GetCurrentMedalInfo() == null)
		{
			yield return null;
		}
		this.OnMedalChanged(TournamentDisplay.Get().GetCurrentMedalInfo());
		TournamentDisplay.Get().RegisterMedalChangedListener(new TournamentDisplay.DelMedalChanged(this.OnMedalChanged));
		yield break;
	}

	// Token: 0x06002304 RID: 8964 RVA: 0x000AEC6F File Offset: 0x000ACE6F
	private void OnMedalChanged(NetCache.NetCacheMedalInfo medalInfo)
	{
		this.m_rankedPlayDisplay.OnMedalChanged(medalInfo);
	}

	// Token: 0x06002305 RID: 8965 RVA: 0x000AEC7D File Offset: 0x000ACE7D
	protected override void OnPlayGameButtonReleased(UIEvent e)
	{
		if (SetRotationManager.Get().CheckForSetRotationRollover())
		{
			return;
		}
		if (PlayerMigrationManager.Get() != null && PlayerMigrationManager.Get().CheckForPlayerMigrationRequired())
		{
			return;
		}
		this.HideDemoQuotes();
		this.HideSetRotationNotifications();
		this.m_heroChosen = true;
		base.OnPlayGameButtonReleased(e);
	}

	// Token: 0x06002306 RID: 8966 RVA: 0x000AECBA File Offset: 0x000ACEBA
	protected override void SetCollectionButtonEnabled(bool enable)
	{
		base.SetCollectionButtonEnabled(enable);
		this.UpdateCollectionButtonGlow();
	}

	// Token: 0x06002307 RID: 8967 RVA: 0x000AECC9 File Offset: 0x000ACEC9
	private void UpdateCollectionButtonGlow()
	{
		if (this.ShouldGlowCollectionButton())
		{
			this.m_collectionButtonGlow.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			return;
		}
		this.m_collectionButtonGlow.ChangeState(ActorStateType.HIGHLIGHT_OFF);
	}

	// Token: 0x06002308 RID: 8968 RVA: 0x000AECF0 File Offset: 0x000ACEF0
	private void InitSwitchFormatButton()
	{
		if (this.m_switchFormatButtonContainer != null && this.m_switchFormatButtonContainer.PrefabGameObject(false) != null)
		{
			this.m_switchFormatButton = this.m_switchFormatButtonContainer.PrefabGameObject(false).GetComponent<SwitchFormatButton>();
			bool flag = false;
			if ((SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT || SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER) && CollectionManager.Get().ShouldAccountSeeStandardWild())
			{
				flag = true;
			}
			else if (SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT && CollectionManager.Get().ShouldAccountSeeStandardComingSoon())
			{
				flag = true;
				this.m_showStandardComingSoonNotice = true;
			}
			if (flag)
			{
				PegasusShared.FormatType formatType;
				bool inRankedPlayMode;
				if (this.m_showStandardComingSoonNotice)
				{
					formatType = PegasusShared.FormatType.FT_STANDARD;
					inRankedPlayMode = true;
				}
				else if (this.m_inHeroPicker)
				{
					formatType = Options.GetFormatType();
					inRankedPlayMode = true;
				}
				else
				{
					formatType = Options.GetFormatType();
					inRankedPlayMode = Options.GetInRankedPlayMode();
				}
				this.m_switchFormatButton.Uncover();
				this.m_switchFormatButton.SetVisualsFormatType(VisualsFormatTypeExtensions.ToVisualsFormatType(formatType, inRankedPlayMode));
				this.m_switchFormatButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.SwitchFormatButtonPress));
				return;
			}
			this.m_switchFormatButton.Cover();
			this.m_switchFormatButton.Disable();
		}
	}

	// Token: 0x06002309 RID: 8969 RVA: 0x000AEE08 File Offset: 0x000AD008
	protected override void ShowHero()
	{
		if (this.m_selectedHeroButton != null)
		{
			this.UpdateHeroInfo(this.m_selectedHeroButton);
		}
		else
		{
			if (!(this.m_selectedCustomDeckBox != null))
			{
				Log.All.PrintError("DeckPickerTrayDisplay.ShowHero with no button or deck box selected!", Array.Empty<object>());
				return;
			}
			this.UpdateHeroInfo(this.m_selectedCustomDeckBox);
		}
		base.ShowHero();
		this.SetLockedPortraitMaterial(this.m_selectedHeroButton);
	}

	// Token: 0x0600230A RID: 8970 RVA: 0x000AEE74 File Offset: 0x000AD074
	protected override void SetHeroRaised(bool raised)
	{
		this.m_xpBar.SetEnabled(raised, false);
		base.SetHeroRaised(raised);
	}

	// Token: 0x0600230B RID: 8971 RVA: 0x000AEE8C File Offset: 0x000AD08C
	private void HideAllPreconHighlights()
	{
		foreach (HeroPickerButton heroPickerButton in this.m_heroButtons)
		{
			heroPickerButton.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
		}
	}

	// Token: 0x0600230C RID: 8972 RVA: 0x000AEEE0 File Offset: 0x000AD0E0
	private void ShowPreconHighlights()
	{
		if (!AbsDeckPickerTrayDisplay.HIGHLIGHT_SELECTED_DECK)
		{
			return;
		}
		foreach (HeroPickerButton heroPickerButton in this.m_heroButtons)
		{
			if (heroPickerButton == this.m_selectedHeroButton)
			{
				heroPickerButton.SetHighlightState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			}
		}
	}

	// Token: 0x0600230D RID: 8973 RVA: 0x000AEF50 File Offset: 0x000AD150
	protected override void PlayGame()
	{
		base.PlayGame();
		switch (SceneMgr.Get().GetMode())
		{
		case SceneMgr.Mode.COLLECTIONMANAGER:
			this.SelectHeroForCollectionManager();
			goto IL_314;
		case SceneMgr.Mode.PACKOPENING:
		case SceneMgr.Mode.FATAL_ERROR:
		case SceneMgr.Mode.DRAFT:
		case SceneMgr.Mode.CREDITS:
		case SceneMgr.Mode.RESET:
			goto IL_314;
		case SceneMgr.Mode.TOURNAMENT:
		{
			if (this.BlockOnInvalidDeckHero())
			{
				return;
			}
			long selectedDeckID = this.GetSelectedDeckID();
			if (selectedDeckID == 0L)
			{
				Debug.LogError("Trying to play game with deck ID 0!");
				return;
			}
			base.SetBackButtonEnabled(false);
			PegasusShared.GameType gameTypeForNewPlayModeGame = this.GetGameTypeForNewPlayModeGame();
			PegasusShared.FormatType formatTypeForNewPlayModeGame = this.GetFormatTypeForNewPlayModeGame();
			Options.Get().SetEnum<PegasusShared.FormatType>(Option.FORMAT_TYPE_LAST_PLAYED, formatTypeForNewPlayModeGame);
			GameMgr.Get().FindGame(gameTypeForNewPlayModeGame, formatTypeForNewPlayModeGame, 2, 0, selectedDeckID, null, null, false, null, PegasusShared.GameType.GT_UNKNOWN);
			bool flag = true;
			if (gameTypeForNewPlayModeGame == PegasusShared.GameType.GT_RANKED && RankMgr.Get().IsLegendRank(formatTypeForNewPlayModeGame))
			{
				flag = false;
			}
			if (flag)
			{
				PresenceMgr.Get().SetStatus(new Enum[]
				{
					Global.PresenceStatus.PLAY_QUEUE
				});
				goto IL_314;
			}
			goto IL_314;
		}
		case SceneMgr.Mode.FRIENDLY:
			if (!FriendChallengeMgr.Get().IsChallengeTavernBrawl())
			{
				if (this.BlockOnInvalidDeckHero())
				{
					return;
				}
				long selectedDeckID2 = this.GetSelectedDeckID();
				if (selectedDeckID2 == 0L)
				{
					Debug.LogError("Trying to play friendly game with deck ID 0!");
					return;
				}
				FriendChallengeMgr.Get().SelectDeck(selectedDeckID2);
				FriendlyChallengeHelper.Get().StartChallengeOrWaitForOpponent("GLOBAL_FRIEND_CHALLENGE_OPPONENT_WAITING_DECK", new AlertPopup.ResponseCallback(this.OnFriendChallengeWaitingForOpponentDialogResponse));
				goto IL_314;
			}
			break;
		case SceneMgr.Mode.ADVENTURE:
		{
			long selectedDeckID3 = this.GetSelectedDeckID();
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig.GetSelectedAdventure() == AdventureDbId.NAXXRAMAS && !Options.Get().GetBool(Option.HAS_PLAYED_NAXX))
			{
				AdTrackingManager.Get().TrackAdventureProgress(Option.HAS_PLAYED_NAXX.ToString());
				Options.Get().SetBool(Option.HAS_PLAYED_NAXX, true);
			}
			AdventureData.Adventuresubscene currentSubScene = adventureConfig.CurrentSubScene;
			if (currentSubScene != AdventureData.Adventuresubscene.PRACTICE)
			{
				if (currentSubScene != AdventureData.Adventuresubscene.MISSION_DECK_PICKER)
				{
					goto IL_314;
				}
				if (base.OnPlayButtonPressed_SaveHeroAndAdvanceToDungeonRunIfNecessary())
				{
					goto IL_314;
				}
				int heroCardDbId = 0;
				if (this.m_selectedHeroButton != null && this.m_selectedHeroButton.m_heroClass != TAG_CLASS.INVALID)
				{
					heroCardDbId = GameUtils.GetFavoriteHeroCardDBIdFromClass(this.m_selectedHeroButton.m_heroClass);
				}
				ScenarioDbId missionToPlay = adventureConfig.GetMissionToPlay();
				if (GameDbf.Scenario.GetRecord((int)missionToPlay).RuleType == Scenario.RuleType.CHOOSE_HERO)
				{
					GameMgr.Get().FindGameWithHero(PegasusShared.GameType.GT_VS_AI, PegasusShared.FormatType.FT_WILD, (int)missionToPlay, 0, heroCardDbId, 0L);
					goto IL_314;
				}
				GameMgr.Get().FindGame(PegasusShared.GameType.GT_VS_AI, PegasusShared.FormatType.FT_WILD, (int)missionToPlay, 0, selectedDeckID3, null, null, false, null, PegasusShared.GameType.GT_UNKNOWN);
				goto IL_314;
			}
			else
			{
				if (this.BlockOnInvalidDeckHero())
				{
					return;
				}
				PracticePickerTrayDisplay.Get().Show();
				this.SetHeroRaised(false);
				goto IL_314;
			}
			break;
		}
		case SceneMgr.Mode.TAVERN_BRAWL:
			break;
		case SceneMgr.Mode.FIRESIDE_GATHERING:
			if (FiresideGatheringManager.Get().InBrawlMode() && !TavernBrawlManager.Get().SelectHeroBeforeMission())
			{
				this.SelectHeroForCollectionManager();
				goto IL_314;
			}
			if (FiresideGatheringManager.Get().InBrawlMode() && GameUtils.IsAIMission(TavernBrawlManager.Get().CurrentMission().missionId))
			{
				if (!TavernBrawlManager.Get().SelectHeroBeforeMission())
				{
					TavernBrawlManager.Get().StartGame(0L);
					goto IL_314;
				}
				goto IL_314;
			}
			else
			{
				if (!FiresideGatheringManager.Get().InBrawlMode() || !TavernBrawlManager.Get().SelectHeroBeforeMission())
				{
					long selectedDeckID4 = this.GetSelectedDeckID();
					FriendChallengeMgr.Get().SelectDeckBeforeSendingChallenge(selectedDeckID4);
					goto IL_314;
				}
				goto IL_314;
			}
			break;
		default:
			goto IL_314;
		}
		if (!TavernBrawlManager.Get().SelectHeroBeforeMission())
		{
			this.SelectHeroForCollectionManager();
		}
		IL_314:
		if (UniversalInputManager.UsePhoneUI && (SceneMgr.Get().GetMode() != SceneMgr.Mode.ADVENTURE || AdventureConfig.Get().CurrentSubScene != AdventureData.Adventuresubscene.PRACTICE))
		{
			this.m_slidingTray.ToggleTraySlider(false, null, true);
		}
	}

	// Token: 0x0600230E RID: 8974 RVA: 0x000AF2A6 File Offset: 0x000AD4A6
	private bool BlockOnInvalidDeckHero()
	{
		if (!GameUtils.IsCardGameplayEventActive(this.m_selectedCustomDeckBox.GetHeroCardID()))
		{
			DialogManager.Get().ShowClassUpcomingPopup();
			return true;
		}
		return false;
	}

	// Token: 0x0600230F RID: 8975 RVA: 0x000AF2C8 File Offset: 0x000AD4C8
	private void SelectHeroForCollectionManager()
	{
		if (this.m_selectedHeroButton == null)
		{
			Debug.LogError("DeckPickerTrayDisplay.SelectHeroForCollectionManager called when m_selectedHeroButton was null");
			return;
		}
		this.m_backButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBackButtonReleased));
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(DeckPickerTrayDisplay.OnNavigateBack));
		if (DeckPickerTrayDisplay.s_selectHeroCoroutine != null)
		{
			Processor.CancelCoroutine(DeckPickerTrayDisplay.s_selectHeroCoroutine);
		}
		DeckPickerTrayDisplay.s_selectHeroCoroutine = Processor.RunCoroutine(DeckPickerTrayDisplay.SelectHeroForCollectionManagerImpl(this.m_selectedHeroButton.GetEntityDef()), null);
	}

	// Token: 0x06002310 RID: 8976 RVA: 0x000AF346 File Offset: 0x000AD546
	private static IEnumerator SelectHeroForCollectionManagerImpl(EntityDef heroDef)
	{
		CollectionDeckTrayDeckListContent.s_HeroPickerFormat = Options.GetFormatType();
		if (HeroPickerDisplay.Get() != null)
		{
			HeroPickerDisplay.Get().HideTray(UniversalInputManager.UsePhoneUI ? 0.25f : 0f);
		}
		CollectionDeckTray deckTray = CollectionDeckTray.Get();
		CollectionDeckTrayDeckListContent decksContent = deckTray.GetDecksContent();
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			decksContent.CreateNewDeckFromUserSelection(heroDef.GetClass(), heroDef.GetCardId(), null, DeckSourceType.DECK_SOURCE_TYPE_NORMAL, null);
			CollectionManager.Get().GetCollectibleDisplay().EnableInput(true);
			yield break;
		}
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		DeckTemplatePicker deckTemplatePicker = null;
		if (CollectionManager.Get().GetNonStarterTemplateDecks(Options.GetFormatType(), heroDef.GetClass()).Count > 0)
		{
			deckTemplatePicker = (UniversalInputManager.UsePhoneUI ? collectionManagerDisplay.GetPhoneDeckTemplateTray() : collectionManagerDisplay.m_pageManager.GetDeckTemplatePicker());
		}
		if (deckTemplatePicker != null && UniversalInputManager.UsePhoneUI)
		{
			deckTemplatePicker.m_phoneBackButton.SetEnabled(false, false);
		}
		deckTray.m_doneButton.SetEnabled(false, false);
		while (deckTray.IsUpdatingTrayMode() || decksContent.NumDecksToDelete() > 0 || deckTray.IsWaitingToDeleteDeck())
		{
			yield return null;
		}
		decksContent.CreateNewDeckFromUserSelection(heroDef.GetClass(), heroDef.GetCardId(), null, DeckSourceType.DECK_SOURCE_TYPE_NORMAL, null);
		while (deckTemplatePicker != null && !deckTemplatePicker.IsShowingPacks())
		{
			yield return null;
		}
		if (CollectionManager.Get().GetCollectibleDisplay() != null)
		{
			CollectionManager.Get().GetCollectibleDisplay().EnableInput(true);
		}
		while (deckTray != null && deckTray.IsUpdatingTrayMode())
		{
			yield return null;
		}
		if (deckTemplatePicker != null && UniversalInputManager.UsePhoneUI)
		{
			deckTemplatePicker.m_phoneBackButton.SetEnabled(true, false);
		}
		if (deckTray != null)
		{
			deckTray.m_doneButton.SetEnabled(true, false);
		}
		yield break;
	}

	// Token: 0x06002311 RID: 8977 RVA: 0x000AF355 File Offset: 0x000AD555
	protected override void OnSlidingTrayToggled(bool isShowing)
	{
		base.OnSlidingTrayToggled(isShowing);
		if (isShowing)
		{
			this.TransitionToFormatType(Options.GetFormatType(), Options.GetInRankedPlayMode(), 2f);
		}
	}

	// Token: 0x06002312 RID: 8978 RVA: 0x000AF376 File Offset: 0x000AD576
	protected override IEnumerator InitModeWhenReady()
	{
		while ((this.ShouldLoadCustomDecks() && !this.CustomPagesReady()) || (SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT && this.m_rankedPlayDisplay == null))
		{
			if (!SceneMgr.Get().DoesCurrentSceneSupportOfflineActivity() && !Network.IsLoggedIn())
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
				yield break;
			}
			yield return null;
		}
		if (!base.IsChoosingHero())
		{
			while (!NetCache.Get().IsNetObjectAvailable<NetCache.NetCacheDecks>())
			{
				yield return null;
			}
		}
		yield return base.StartCoroutine(base.InitModeWhenReady());
		this.InitMode();
		while (LoadingScreen.Get().IsTransitioning())
		{
			yield return null;
		}
		if (this.ShouldShowBonusStarsPopUp())
		{
			this.ShowBonusStarsPopup();
		}
		else
		{
			this.PlayEnterModeDialogues();
		}
		yield break;
	}

	// Token: 0x06002313 RID: 8979 RVA: 0x000AF388 File Offset: 0x000AD588
	private bool CustomPagesReady()
	{
		if (this.m_customPages == null)
		{
			return false;
		}
		foreach (CustomDeckPage customDeckPage in this.m_customPages)
		{
			if (customDeckPage == null || !customDeckPage.PageReady())
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06002314 RID: 8980 RVA: 0x000AF3F8 File Offset: 0x000AD5F8
	private CustomDeckPage GetCurrentCustomPage()
	{
		if (this.m_currentPageIndex < this.m_customPages.Count)
		{
			return this.m_customPages[this.m_currentPageIndex];
		}
		return null;
	}

	// Token: 0x06002315 RID: 8981 RVA: 0x000AF420 File Offset: 0x000AD620
	protected override void InitRichPresence(Global.PresenceStatus? newStatus = null)
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.TOURNAMENT)
		{
			newStatus = new Global.PresenceStatus?(Global.PresenceStatus.PLAY_DECKPICKER);
		}
		base.InitRichPresence(newStatus);
	}

	// Token: 0x06002316 RID: 8982 RVA: 0x000AF44C File Offset: 0x000AD64C
	private void SetSelectionAndPageFromOptions()
	{
		bool flag = UniversalInputManager.UsePhoneUI && SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.GAMEPLAY;
		int pageNum = 0;
		long deckID = 0L;
		if (this.HasNewRewardedDeck(out deckID))
		{
			RewardUtils.MarkNewestRewardedDeckAsSeen();
		}
		else
		{
			deckID = this.GetLastChosenDeckId();
		}
		CollectionDeckBoxVisual deckboxWithDeckID = this.GetDeckboxWithDeckID(deckID, out pageNum);
		this.ShowPage(pageNum, true);
		if (!flag && deckboxWithDeckID != null && deckboxWithDeckID.IsValidForCurrentMode())
		{
			this.SelectCustomDeck(deckboxWithDeckID);
		}
	}

	// Token: 0x06002317 RID: 8983 RVA: 0x000AF4C4 File Offset: 0x000AD6C4
	private bool HasNewRewardedDeck(out long deckId)
	{
		bool flag = RewardUtils.HasNewRewardedDeck(out deckId);
		if (flag && !this.HasValidDeckboxWithId(deckId))
		{
			Log.DeckTray.PrintWarning("HasNewRewardedDeckId - Newest rewarded deck ID option was set to an invalid deck ID: {0}", new object[]
			{
				deckId
			});
			return false;
		}
		return flag;
	}

	// Token: 0x06002318 RID: 8984 RVA: 0x000AF507 File Offset: 0x000AD707
	private bool HasValidDeckboxWithId(long deckId)
	{
		return this.GetDeckboxWithDeckID(deckId) != null;
	}

	// Token: 0x06002319 RID: 8985 RVA: 0x000AF516 File Offset: 0x000AD716
	private long GetLastChosenDeckId()
	{
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.FRIENDLY)
		{
			return Options.Get().GetLong(Option.LAST_CUSTOM_DECK_CHOSEN);
		}
		return 0L;
	}

	// Token: 0x0600231A RID: 8986 RVA: 0x000AF534 File Offset: 0x000AD734
	private CollectionDeckBoxVisual GetDeckboxWithDeckID(long deckID)
	{
		int num;
		return this.GetDeckboxWithDeckID(deckID, out num);
	}

	// Token: 0x0600231B RID: 8987 RVA: 0x000AF54C File Offset: 0x000AD74C
	private CollectionDeckBoxVisual GetDeckboxWithDeckID(long deckID, out int pageNum)
	{
		for (pageNum = 0; pageNum < this.m_customPages.Count; pageNum++)
		{
			CollectionDeckBoxVisual deckboxWithDeckID = this.m_customPages[pageNum].GetDeckboxWithDeckID(deckID);
			if (deckboxWithDeckID != null)
			{
				return deckboxWithDeckID;
			}
		}
		pageNum = 0;
		return null;
	}

	// Token: 0x0600231C RID: 8988 RVA: 0x000AF598 File Offset: 0x000AD798
	protected override void OnFriendChallengeWaitingForOpponentDialogResponse(AlertPopup.Response response, object userData)
	{
		if (response != AlertPopup.Response.CANCEL)
		{
			return;
		}
		if (FriendChallengeMgr.Get().AmIInGameState())
		{
			return;
		}
		this.Deselect();
		base.OnFriendChallengeWaitingForOpponentDialogResponse(response, userData);
	}

	// Token: 0x0600231D RID: 8989 RVA: 0x000AF5BC File Offset: 0x000AD7BC
	protected override void OnFriendChallengeChanged(FriendChallengeEvent challengeEvent, BnetPlayer player, FriendlyChallengeData challengeData, object userData)
	{
		base.OnFriendChallengeChanged(challengeEvent, player, challengeData, userData);
		if (challengeEvent == FriendChallengeEvent.I_RECEIVED_SHARED_DECKS)
		{
			this.UseSharedDecks(FriendChallengeMgr.Get().GetSharedDecks());
			return;
		}
		if (challengeEvent == FriendChallengeEvent.I_ENDED_DECK_SHARE)
		{
			this.StopUsingSharedDecks();
			return;
		}
		if (challengeEvent == FriendChallengeEvent.I_CANCELED_DECK_SHARE_REQUEST)
		{
			this.OnDeckShareRequestCancelDeclineOrError();
			return;
		}
		if (challengeEvent == FriendChallengeEvent.OPPONENT_DECLINED_DECK_SHARE_REQUEST)
		{
			this.OnDeckShareRequestCancelDeclineOrError();
			return;
		}
		if (challengeEvent == FriendChallengeEvent.DECK_SHARE_ERROR_OCCURED)
		{
			this.OnDeckShareRequestCancelDeclineOrError();
			return;
		}
		if ((challengeEvent == FriendChallengeEvent.I_ACCEPTED_DECK_SHARE_REQUEST || challengeEvent == FriendChallengeEvent.I_DECLINED_DECK_SHARE_REQUEST) && FriendChallengeMgr.Get().DidISelectDeckOrHero())
		{
			FriendlyChallengeHelper.Get().StartChallengeOrWaitForOpponent("GLOBAL_FRIEND_CHALLENGE_OPPONENT_WAITING_DECK", new AlertPopup.ResponseCallback(this.OnFriendChallengeWaitingForOpponentDialogResponse));
		}
	}

	// Token: 0x0600231E RID: 8990 RVA: 0x000AF64C File Offset: 0x000AD84C
	protected override void OnHeroFullDefLoaded(string cardId, DefLoader.DisposableFullDef fullDef, object userData)
	{
		base.OnHeroFullDefLoaded(cardId, fullDef, userData);
		if (base.IsChoosingHero() && this.m_heroDefsLoading <= 0)
		{
			base.StartCoroutine(this.InitButtonAchievements());
		}
	}

	// Token: 0x0600231F RID: 8991 RVA: 0x000AF678 File Offset: 0x000AD878
	protected override void OnHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		base.OnHeroActorLoaded(assetRef, go, callbackData);
		if (this.m_heroActor == null)
		{
			return;
		}
		this.m_xpBar.transform.parent = this.m_heroActor.GetRootObject().transform;
		this.m_xpBar.transform.localScale = new Vector3(0.89f, 0.89f, 0.89f);
		this.m_xpBar.transform.localPosition = new Vector3(-0.1776525f, 0.2245596f, -0.7309282f);
		this.m_xpBar.m_isOnDeck = false;
	}

	// Token: 0x06002320 RID: 8992 RVA: 0x000AF711 File Offset: 0x000AD911
	protected override bool ShouldShowHeroPower()
	{
		return !UniversalInputManager.UsePhoneUI || base.IsChoosingHero();
	}

	// Token: 0x06002321 RID: 8993 RVA: 0x000AF727 File Offset: 0x000AD927
	private bool IsDeckSharingActive()
	{
		return !(this.m_DeckShareRequestButton == null) && SceneMgr.Get().GetMode() == SceneMgr.Mode.FRIENDLY && FriendChallengeMgr.Get().IsDeckShareEnabled() && !base.IsChoosingHero();
	}

	// Token: 0x06002322 RID: 8994 RVA: 0x000AF761 File Offset: 0x000AD961
	private bool ShouldShowCollectionButton()
	{
		return !this.IsDeckSharingActive() && !base.IsChoosingHero() && SceneMgr.Get().GetMode() != SceneMgr.Mode.FRIENDLY;
	}

	// Token: 0x06002323 RID: 8995 RVA: 0x000AF788 File Offset: 0x000AD988
	private bool ShouldGlowCollectionButton()
	{
		return this.ShouldShowCollectionButton() && this.m_collectionButton.IsEnabled() && ((!Options.Get().GetBool(Option.HAS_CLICKED_COLLECTION_BUTTON_FOR_NEW_DECK) && this.HaveDecksThatNeedNames()) || (!Options.Get().GetBool(Option.HAS_CLICKED_COLLECTION_BUTTON_FOR_NEW_CARD) && this.HaveUnseenCards()) || (Options.Get().GetBool(Option.GLOW_COLLECTION_BUTTON_AFTER_SET_ROTATION) && SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT));
	}

	// Token: 0x06002324 RID: 8996 RVA: 0x000AF808 File Offset: 0x000ADA08
	private bool HaveDecksThatNeedNames()
	{
		using (List<CollectionDeck>.Enumerator enumerator = CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.NeedsName)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002325 RID: 8997 RVA: 0x000AF868 File Offset: 0x000ADA68
	private uint GetNumValidStandardDecks()
	{
		uint num = 0U;
		foreach (CollectionDeck collectionDeck in CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK))
		{
			if (collectionDeck.IsValidForFormat(PegasusShared.FormatType.FT_STANDARD) && collectionDeck.IsValidForRuleset)
			{
				num += 1U;
			}
		}
		return num;
	}

	// Token: 0x06002326 RID: 8998 RVA: 0x000AF8D4 File Offset: 0x000ADAD4
	private bool HaveUnseenCards()
	{
		CollectionManager collectionManager = CollectionManager.Get();
		string searchString = null;
		List<CollectibleCardFilter.FilterMask> filterMasks = null;
		int? manaCost = null;
		TAG_CARD_SET[] theseCardSets = null;
		TAG_CLASS[] theseClassTypes = null;
		TAG_CARDTYPE[] theseCardTypes = null;
		TAG_RARITY? rarity = null;
		TAG_RACE? race = null;
		int? minOwned = new int?(1);
		bool? notSeen = new bool?(true);
		return collectionManager.FindCards(searchString, filterMasks, manaCost, theseCardSets, theseClassTypes, theseCardTypes, rarity, race, new bool?(false), minOwned, notSeen, null, null, null, null, true, null, null, null).m_cards.Count > 0;
	}

	// Token: 0x06002327 RID: 8999 RVA: 0x000AF954 File Offset: 0x000ADB54
	private void PlayEnterModeDialogues()
	{
		if (!this.ShowInnkeeperQuoteIfNeeded())
		{
			this.ShowWhizbangPopupIfNeeded();
		}
	}

	// Token: 0x06002328 RID: 9000 RVA: 0x000AF968 File Offset: 0x000ADB68
	private bool ShowInnkeeperQuoteIfNeeded()
	{
		bool result = false;
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.COLLECTIONMANAGER && Options.Get().GetBool(Option.SHOW_WILD_DISCLAIMER_POPUP_ON_CREATE_DECK) && Options.GetFormatType() == PegasusShared.FormatType.FT_WILD && UserAttentionManager.CanShowAttentionGrabber("DeckPickTrayDisplay.ShowInnkeeperQuoteIfNeeded:" + Option.SHOW_WILD_DISCLAIMER_POPUP_ON_CREATE_DECK))
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, NotificationManager.DEFAULT_CHARACTER_POS, GameStrings.Get("VO_INNKEEPER_PLAY_STANDARD_TO_WILD"), "VO_INNKEEPER_Male_Dwarf_SetRotation_43.prefab:4b4ce858139927946905ec0d40d5b3c1", 0f, null, false);
			Options.Get().SetBool(Option.SHOW_WILD_DISCLAIMER_POPUP_ON_CREATE_DECK, false);
			result = true;
		}
		return result;
	}

	// Token: 0x06002329 RID: 9001 RVA: 0x000AF9F4 File Offset: 0x000ADBF4
	private bool ShowWhizbangPopupIfNeeded()
	{
		if (SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.GAMEPLAY)
		{
			return false;
		}
		LastGameData lastGameData = GameMgr.Get().LastGameData;
		if (lastGameData.GameResult != TAG_PLAYSTATE.WON || !lastGameData.HasWhizbangDeckID())
		{
			return false;
		}
		int num = Options.Get().GetInt(Option.WHIZBANG_POPUP_COUNTER);
		if (num >= 7)
		{
			return false;
		}
		CollectionManager.TemplateDeck templateDeck = CollectionManager.Get().GetTemplateDeck(lastGameData.WhizbangDeckID);
		if (templateDeck == null || (!string.IsNullOrEmpty(templateDeck.m_event) && !SpecialEventManager.Get().IsEventActive(templateDeck.m_event, false)))
		{
			return false;
		}
		bool result = false;
		if (num == 0 || num == 2 || num == 6)
		{
			if (UserAttentionManager.CanShowAttentionGrabber("DeckPickerTrayDisplay.ShowWhizbangPopupIfNeeded()"))
			{
				base.StartCoroutine(this.ShowWhizbangPopup(templateDeck));
				num++;
				result = true;
			}
		}
		else
		{
			num++;
		}
		Options.Get().SetInt(Option.WHIZBANG_POPUP_COUNTER, num);
		return result;
	}

	// Token: 0x0600232A RID: 9002 RVA: 0x000AFABE File Offset: 0x000ADCBE
	private IEnumerator ShowWhizbangPopup(CollectionManager.TemplateDeck whizbangDeck)
	{
		if (whizbangDeck == null)
		{
			yield break;
		}
		yield return new WaitForSeconds(1f);
		BasicPopup.PopupInfo popupInfo = new BasicPopup.PopupInfo();
		popupInfo.m_prefabAssetRefs.Add("WhizbangDialog_notification.prefab:89912cf72b2d5cf47820d2328de40f3f");
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_MANAGER_WHIZBANG_POPUP_HEADER");
		popupInfo.m_bodyText = GameStrings.Format("GLUE_COLLECTION_MANAGER_WHIZBANG_POPUP_BODY", new object[]
		{
			GameStrings.GetClassName(whizbangDeck.m_class),
			whizbangDeck.m_title
		});
		popupInfo.m_disableBnetBar = true;
		DialogManager.Get().ShowBasicPopup(UserAttentionBlocker.NONE, popupInfo);
		yield break;
	}

	// Token: 0x0600232B RID: 9003 RVA: 0x000AFAD0 File Offset: 0x000ADCD0
	private void SetLockedPortraitMaterial(HeroPickerButton button)
	{
		if (button != null && button.IsLocked())
		{
			SceneMgr.Mode mode = SceneMgr.Get().GetMode();
			if (mode != SceneMgr.Mode.TAVERN_BRAWL && (mode != SceneMgr.Mode.FRIENDLY || !FriendChallengeMgr.Get().IsChallengeTavernBrawl()) && (mode != SceneMgr.Mode.FIRESIDE_GATHERING || !FiresideGatheringManager.Get().InBrawlMode()))
			{
				using (DefLoader.DisposableFullDef disposableFullDef = button.ShareFullDef())
				{
					if (!(disposableFullDef.CardDef.m_LockedClassPortrait == null))
					{
						this.m_heroActor.SetPortraitMaterial(disposableFullDef.CardDef.m_LockedClassPortrait);
					}
				}
			}
		}
	}

	// Token: 0x0600232C RID: 9004 RVA: 0x000AFB74 File Offset: 0x000ADD74
	private bool ShouldLoadCustomDecks()
	{
		if (this.m_deckPickerMode == DeckPickerMode.INVALID)
		{
			Debug.LogWarning("DeckPickerTrayDisplay.ShouldLoadCustomDecks() - querying m_deckPickerMode when it hasn't been set yet!");
		}
		return this.IsDeckSharingActive() || this.m_deckPickerMode == DeckPickerMode.CUSTOM;
	}

	// Token: 0x0600232D RID: 9005 RVA: 0x000AFB9C File Offset: 0x000ADD9C
	private RankedPlayDataModel GetBonusStarsPopupDataModel()
	{
		TournamentDisplay tournamentDisplay = TournamentDisplay.Get();
		if (tournamentDisplay == null)
		{
			return null;
		}
		NetCache.NetCacheMedalInfo currentMedalInfo = tournamentDisplay.GetCurrentMedalInfo();
		if (currentMedalInfo == null)
		{
			return null;
		}
		return new MedalInfoTranslator(currentMedalInfo, null).CreateDataModel(Options.GetFormatType(), RankedMedal.DisplayMode.Default, false, false, null);
	}

	// Token: 0x0600232E RID: 9006 RVA: 0x000AFBDC File Offset: 0x000ADDDC
	private void ShowInvalidClassPopup()
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_CLASS_INVALID_DECK_TITLE");
		popupInfo.m_text = GameStrings.Get("GLUE_CLASS_INVALID_DECK_DESC");
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.DialogProcessCallback callback = delegate(DialogBase dialog, object userData)
		{
			this.SetPlayButtonEnabled(true);
			return true;
		};
		DialogManager.Get().ShowPopup(popupInfo, callback);
	}

	// Token: 0x0600232F RID: 9007 RVA: 0x000AFC38 File Offset: 0x000ADE38
	private void ShowClassLockedPopup(TAG_CLASS tagClass)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_headerText = GameStrings.Get("GLUE_CLASS_INVALID_DECK_TITLE");
		popupInfo.m_text = GameStrings.Get("GLUE_CLASS_INVALID_DECK_DESC");
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x06002330 RID: 9008 RVA: 0x000AFC84 File Offset: 0x000ADE84
	private void UpdatePageArrows()
	{
		bool flag = true;
		bool flag2 = true;
		if (this.m_numPagesToShow <= 1 || (Options.GetFormatType() == PegasusShared.FormatType.FT_CLASSIC && CollectionManager.Get().GetNumberOfClassicDecks() == 0) || DemoMgr.Get().IsExpoDemo() || base.IsChoosingHero())
		{
			flag = false;
			flag2 = false;
		}
		else
		{
			if (this.IsShowingFirstPage())
			{
				flag = false;
			}
			if (this.IsShowingLastPage())
			{
				flag2 = false;
			}
		}
		if (flag)
		{
			if (!this.m_leftArrow.gameObject.activeInHierarchy)
			{
				this.m_showLeftArrowCoroutine = base.StartCoroutine(this.ArrowDelayedActivate(this.m_leftArrow, 0.25f));
			}
		}
		else
		{
			if (this.m_showLeftArrowCoroutine != null)
			{
				base.StopCoroutine(this.m_showLeftArrowCoroutine);
			}
			this.m_leftArrow.gameObject.SetActive(false);
		}
		if (flag2)
		{
			if (!this.m_rightArrow.gameObject.activeInHierarchy)
			{
				this.m_showRightArrowCoroutine = base.StartCoroutine(this.ArrowDelayedActivate(this.m_rightArrow, 0.25f));
				return;
			}
		}
		else
		{
			if (this.m_showRightArrowCoroutine != null)
			{
				base.StopCoroutine(this.m_showRightArrowCoroutine);
			}
			this.m_rightArrow.gameObject.SetActive(false);
		}
	}

	// Token: 0x06002331 RID: 9009 RVA: 0x000AFD91 File Offset: 0x000ADF91
	private bool IsShowingFirstPage()
	{
		return this.m_currentPageIndex == 0;
	}

	// Token: 0x06002332 RID: 9010 RVA: 0x000AFD9C File Offset: 0x000ADF9C
	private bool IsShowingLastPage()
	{
		return this.m_currentPageIndex == this.m_customPages.Count - 1;
	}

	// Token: 0x06002333 RID: 9011 RVA: 0x000AFDB4 File Offset: 0x000ADFB4
	public void InitSetRotationTutorial(bool veteranFlow)
	{
		if (this.m_setRotationTutorialState != DeckPickerTrayDisplay.SetRotationTutorialState.INACTIVE)
		{
			Debug.LogError("Tried to call DeckPickerTrayDisplay.InitTutorial() when m_setRotationTutorialState was " + this.m_setRotationTutorialState.ToString());
			return;
		}
		this.m_setRotationTutorialState = DeckPickerTrayDisplay.SetRotationTutorialState.PREPARING;
		this.m_switchFormatButton.Disable();
		this.m_switchFormatButton.gameObject.SetActive(false);
		this.TransitionToFormatType(PegasusShared.FormatType.FT_STANDARD, true, 2f);
		Options.SetFormatType(PegasusShared.FormatType.FT_STANDARD);
		Options.SetInRankedPlayMode(true);
		this.Deselect();
		this.ShowFirstPage(false);
		this.m_rankedPlayDisplay.StartSetRotationTutorial();
		this.SetPlayButtonEnabled(false);
		base.SetBackButtonEnabled(false);
		this.SetCollectionButtonEnabled(false);
		this.m_rightArrow.gameObject.SetActive(false);
		this.m_leftArrow.gameObject.SetActive(false);
		this.m_rightArrow.SetEnabled(false, false);
		this.m_leftArrow.SetEnabled(false, false);
		base.SetHeaderText(GameStrings.Get("GLUE_TOURNAMENT"));
		if (this.m_heroPower != null)
		{
			this.m_heroPower.GetComponent<Collider>().enabled = false;
		}
		if (this.m_goldenHeroPower != null)
		{
			this.m_goldenHeroPower.GetComponent<Collider>().enabled = false;
		}
		foreach (CustomDeckPage customDeckPage in this.m_customPages)
		{
			customDeckPage.EnableDeckButtons(false);
		}
		this.m_setRotationTutorialState = DeckPickerTrayDisplay.SetRotationTutorialState.READY;
	}

	// Token: 0x06002334 RID: 9012 RVA: 0x000AFF28 File Offset: 0x000AE128
	public void StartSetRotationTutorial(SetRotationClock.DisableTheClockCallback callback)
	{
		if (this.m_setRotationTutorialState == DeckPickerTrayDisplay.SetRotationTutorialState.READY)
		{
			base.StartCoroutine(this.ShowSetRotationTutorialPopups(callback));
			return;
		}
		Debug.LogError("Tried to start Play Screen Set Rotation Tutorial without calling DeckPickerTrayDisplay.InitTutorial()");
		callback();
	}

	// Token: 0x06002335 RID: 9013 RVA: 0x000AFF52 File Offset: 0x000AE152
	private IEnumerator ShowSetRotationTutorialPopups(SetRotationClock.DisableTheClockCallback disableClockCallback)
	{
		bool veteranFlow = SetRotationManager.HasSeenStandardModeTutorial();
		this.m_setRotationTutorialState = DeckPickerTrayDisplay.SetRotationTutorialState.SHOW_TUTORIAL_POPUPS;
		this.m_dimQuad.GetComponent<Renderer>().enabled = true;
		this.m_dimQuad.enabled = true;
		this.m_dimQuad.StopPlayback();
		this.m_dimQuad.Play("DimQuad_FadeIn");
		GameSaveDataManager gameSaveDataManager = GameSaveDataManager.Get();
		if (gameSaveDataManager != null)
		{
			long num = -1L;
			long num2 = -1L;
			gameSaveDataManager.GetSubkeyValue(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.ROTATED_BOOSTER_POPUP_PROGRESS, out num);
			gameSaveDataManager.GetSubkeyValue(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.INNKEEPER_STANDARD_DECKS_VO_PROGRESS, out num2);
			bool flag = false;
			List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
			if (num == 0L)
			{
				list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.ROTATED_BOOSTER_POPUP_PROGRESS, new long[]
				{
					1L
				}));
				flag = true;
			}
			if (num2 == 0L)
			{
				list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.INNKEEPER_STANDARD_DECKS_VO_PROGRESS, new long[]
				{
					1L
				}));
				flag = true;
			}
			if (flag)
			{
				gameSaveDataManager.SaveSubkeys(list, null);
			}
		}
		this.m_shouldContinue = false;
		DeckPickerTrayDisplay.Get().AddFormatTypePickerClosedListener(new AbsDeckPickerTrayDisplay.FormatTypePickerClosed(this.ContinueTutorial));
		DeckPickerTrayDisplay.Get().ShowPopupDuringSetRotation(VisualsFormatType.VFT_STANDARD);
		disableClockCallback();
		while (!this.m_shouldContinue)
		{
			yield return null;
		}
		DeckPickerTrayDisplay.Get().RemoveFormatTypePickerClosedListener(new AbsDeckPickerTrayDisplay.FormatTypePickerClosed(this.ContinueTutorial));
		if (veteranFlow)
		{
			base.StartCoroutine(this.ShowWelcomeQuests());
		}
		else
		{
			this.StartSwitchModeWalkthrough();
		}
		yield break;
	}

	// Token: 0x06002336 RID: 9014 RVA: 0x000AFF68 File Offset: 0x000AE168
	private void ContinueTutorial(DialogBase dialog, object userData)
	{
		this.m_shouldContinue = true;
	}

	// Token: 0x06002337 RID: 9015 RVA: 0x000AFF68 File Offset: 0x000AE168
	private void ContinueTutorial()
	{
		this.m_shouldContinue = true;
	}

	// Token: 0x06002338 RID: 9016 RVA: 0x000AFF74 File Offset: 0x000AE174
	private bool ShouldShowRotatedBoosterPopup(VisualsFormatType newVisualsFormatType)
	{
		if (newVisualsFormatType == VisualsFormatType.VFT_STANDARD || (newVisualsFormatType == VisualsFormatType.VFT_WILD && newVisualsFormatType.IsRanked()))
		{
			GameSaveDataManager gameSaveDataManager = GameSaveDataManager.Get();
			if (gameSaveDataManager != null)
			{
				long num = -1L;
				gameSaveDataManager.GetSubkeyValue(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.ROTATED_BOOSTER_POPUP_PROGRESS, out num);
				if (num == 1L)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002339 RID: 9017 RVA: 0x000AFFB8 File Offset: 0x000AE1B8
	private IEnumerator ShowRotatedBoostersPopup(Action callbackOnHide = null)
	{
		yield return new WaitForSeconds(1f);
		if (!UserAttentionManager.CanShowAttentionGrabber(UserAttentionBlocker.SET_ROTATION_INTRO, "ShowSetRotationTutorialDialog"))
		{
			yield break;
		}
		SetRotationRotatedBoostersPopup.SetRotationRotatedBoostersPopupInfo setRotationRotatedBoostersPopupInfo = new SetRotationRotatedBoostersPopup.SetRotationRotatedBoostersPopupInfo();
		setRotationRotatedBoostersPopupInfo.m_onHiddenCallback = callbackOnHide;
		DialogManager.Get().ShowSetRotationTutorialPopup(UserAttentionBlocker.SET_ROTATION_INTRO, setRotationRotatedBoostersPopupInfo);
		GameSaveDataManager gameSaveDataManager = GameSaveDataManager.Get();
		if (gameSaveDataManager != null)
		{
			gameSaveDataManager.SaveSubkeys(new List<GameSaveDataManager.SubkeySaveRequest>
			{
				new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.ROTATED_BOOSTER_POPUP_PROGRESS, new long[]
				{
					2L
				})
			}, null);
		}
		yield break;
	}

	// Token: 0x0600233A RID: 9018 RVA: 0x000AFFC7 File Offset: 0x000AE1C7
	private void StartSwitchModeWalkthrough()
	{
		this.m_setRotationTutorialState = DeckPickerTrayDisplay.SetRotationTutorialState.SWITCH_MODE_WALKTHROUGH;
		base.StartCoroutine(this.TutorialSwitchToStandard());
	}

	// Token: 0x0600233B RID: 9019 RVA: 0x000AFFDD File Offset: 0x000AE1DD
	private IEnumerator TutorialSwitchToStandard()
	{
		this.m_switchFormatPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.SET_ROTATION_INTRO, this.m_Switch_Format_Notification_Bone.position, this.m_Switch_Format_Notification_Bone.localScale, GameStrings.Get("GLUE_TOURNAMENT_SWITCH_MODE"), true, NotificationManager.PopupTextType.BASIC);
		if (this.m_switchFormatPopup != null)
		{
			Notification.PopUpArrowDirection direction = UniversalInputManager.UsePhoneUI ? Notification.PopUpArrowDirection.RightUp : Notification.PopUpArrowDirection.Up;
			this.m_switchFormatPopup.ShowPopUpArrow(direction);
		}
		this.m_switchFormatButton.EnableHighlight(true);
		this.m_switchFormatButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnSwitchFormatReleased));
		this.m_switchFormatButton.Enable();
		yield break;
	}

	// Token: 0x0600233C RID: 9020 RVA: 0x000AFFEC File Offset: 0x000AE1EC
	private void OnSwitchFormatReleased(UIEvent e)
	{
		if (this.m_setRotationTutorialState == DeckPickerTrayDisplay.SetRotationTutorialState.SWITCH_MODE_WALKTHROUGH)
		{
			this.m_switchFormatButton.Disable();
			this.m_switchFormatButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnSwitchFormatReleased));
			Processor.QueueJob("LoginManager.CompleteLoginFlow", LoginManager.Get().CompleteLoginFlow(), Array.Empty<IJobDependency>());
			base.StartCoroutine(this.ShowWelcomeQuests());
			return;
		}
		Debug.Log("OnSwitchFormatReleased called when not in SWITCH_MODE_WALKTHROUGH Set Rotation Tutorial state");
	}

	// Token: 0x0600233D RID: 9021 RVA: 0x000B0058 File Offset: 0x000AE258
	private void PlayTransitionSounds()
	{
		if (this.m_customPages[this.m_currentPageIndex].HasWildDeck() && !string.IsNullOrEmpty(this.m_wildDeckTransitionSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_wildDeckTransitionSound);
		}
	}

	// Token: 0x0600233E RID: 9022 RVA: 0x000B0094 File Offset: 0x000AE294
	private void MarkSetRotationComplete()
	{
		this.m_setRotationTutorialState = DeckPickerTrayDisplay.SetRotationTutorialState.INACTIVE;
		Options.Get().SetBool(Option.HAS_SEEN_STANDARD_MODE_TUTORIAL, true);
		Options.Get().SetInt(Option.SET_ROTATION_INTRO_PROGRESS, 6);
		foreach (long id in this.m_noticeIdsToAck)
		{
			Network.Get().AckNotice(id);
		}
	}

	// Token: 0x0600233F RID: 9023 RVA: 0x000B0114 File Offset: 0x000AE314
	private IEnumerator ShowWelcomeQuests()
	{
		this.MarkSetRotationComplete();
		this.m_switchFormatButton.EnableHighlight(false);
		NotificationManager.Get().DestroyNotification(this.m_switchFormatPopup, 0f);
		this.m_switchFormatPopup = null;
		this.m_dimQuad.StopPlayback();
		this.m_dimQuad.Play("DimQuad_FadeOut");
		yield return new WaitForEndOfFrame();
		float length = this.m_dimQuad.GetCurrentAnimatorStateInfo(0).length;
		yield return new WaitForSeconds(length);
		this.m_dimQuad.GetComponent<Renderer>().enabled = false;
		this.m_dimQuad.enabled = false;
		yield return new WaitForSeconds(this.m_showQuestPause);
		this.OnWelcomeQuestDismiss();
		yield break;
	}

	// Token: 0x06002340 RID: 9024 RVA: 0x000B0123 File Offset: 0x000AE323
	private void OnWelcomeQuestDismiss()
	{
		base.StartCoroutine(this.EndTutorial());
	}

	// Token: 0x06002341 RID: 9025 RVA: 0x000B0132 File Offset: 0x000AE332
	private IEnumerator EndTutorial()
	{
		yield return new WaitForSeconds(this.m_playVOPause);
		if (this.m_heroPower != null)
		{
			this.m_heroPower.GetComponent<Collider>().enabled = true;
		}
		if (this.m_goldenHeroPower != null)
		{
			this.m_goldenHeroPower.GetComponent<Collider>().enabled = true;
		}
		base.SetBackButtonEnabled(true);
		this.SetCollectionButtonEnabled(true);
		this.m_rightArrow.SetEnabled(true, false);
		this.m_leftArrow.SetEnabled(true, false);
		this.m_leftArrow.gameObject.SetActive(!this.IsShowingFirstPage());
		this.m_rightArrow.gameObject.SetActive(!this.IsShowingLastPage());
		foreach (CustomDeckPage customDeckPage in this.m_customPages)
		{
			customDeckPage.EnableDeckButtons(true);
		}
		Options.Get().SetBool(Option.GLOW_COLLECTION_BUTTON_AFTER_SET_ROTATION, true);
		this.UpdateCollectionButtonGlow();
		if (this.m_switchFormatButton != null)
		{
			this.m_switchFormatButton.Enable();
		}
		UserAttentionManager.StopBlocking(UserAttentionBlocker.SET_ROTATION_INTRO);
		yield break;
	}

	// Token: 0x06002342 RID: 9026 RVA: 0x000B0144 File Offset: 0x000AE344
	private bool ShouldShowStandardDeckVO(VisualsFormatType newVisualsFormatType)
	{
		if (newVisualsFormatType == VisualsFormatType.VFT_STANDARD)
		{
			GameSaveDataManager gameSaveDataManager = GameSaveDataManager.Get();
			if (gameSaveDataManager != null)
			{
				long num = -1L;
				gameSaveDataManager.GetSubkeyValue(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.INNKEEPER_STANDARD_DECKS_VO_PROGRESS, out num);
				if (num == 1L)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002343 RID: 9027 RVA: 0x000B017C File Offset: 0x000AE37C
	private IEnumerator ShowStandardDeckVO()
	{
		yield return new WaitForSeconds(1f);
		uint numValidStandardDecks = this.GetNumValidStandardDecks();
		if (numValidStandardDecks == 1U)
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.SET_ROTATION_INTRO, DeckPickerTrayDisplay.INNKEEPER_QUOTE_POS, GameStrings.Get("VO_INNKEEPER_HAVE_ONE_STANDARD_DECK"), "VO_INNKEEPER_Male_Dwarf_HAVE_STANDARD_DECK_07.prefab:282cd0db8b3737d4bb55d71f915074e4", 0f, null, false);
		}
		else if (numValidStandardDecks > 1U)
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.SET_ROTATION_INTRO, DeckPickerTrayDisplay.INNKEEPER_QUOTE_POS, GameStrings.Get("VO_INNKEEPER_HAVE_STANDARD_DECKS"), "VO_INNKEEPER_Male_Dwarf_HAVE_STANDARD_DECKS_08.prefab:0c1c2ab2c4ead094abc69ec278aa4878", 0f, null, false);
		}
		GameSaveDataManager gameSaveDataManager = GameSaveDataManager.Get();
		if (gameSaveDataManager != null)
		{
			gameSaveDataManager.SaveSubkeys(new List<GameSaveDataManager.SubkeySaveRequest>
			{
				new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.INNKEEPER_STANDARD_DECKS_VO_PROGRESS, new long[]
				{
					2L
				})
			}, null);
		}
		yield break;
	}

	// Token: 0x04001322 RID: 4898
	public Transform m_rankedPlayDisplayWidgetBone;

	// Token: 0x04001323 RID: 4899
	public Texture m_emptyHeroTexture;

	// Token: 0x04001324 RID: 4900
	public NestedPrefab m_leftArrowNestedPrefab;

	// Token: 0x04001325 RID: 4901
	public NestedPrefab m_rightArrowNestedPrefab;

	// Token: 0x04001326 RID: 4902
	public GameObject m_modeLabelBg;

	// Token: 0x04001327 RID: 4903
	public GameObject m_randomDecksHiddenBone;

	// Token: 0x04001328 RID: 4904
	public GameObject m_suckedInRandomDecksBone;

	// Token: 0x04001329 RID: 4905
	public HeroXPBar m_xpBarPrefab;

	// Token: 0x0400132A RID: 4906
	public GameObject m_rankedWinsPlate;

	// Token: 0x0400132B RID: 4907
	public UberText m_rankedWins;

	// Token: 0x0400132C RID: 4908
	public BoxCollider m_clickBlocker;

	// Token: 0x0400132D RID: 4909
	public Animator m_premadeDeckGlowAnimator;

	// Token: 0x0400132E RID: 4910
	public GameObject m_hierarchyDeckTray;

	// Token: 0x0400132F RID: 4911
	[CustomEditField(Sections = "Deck Pages")]
	public GameObject m_customDeckPagesRoot;

	// Token: 0x04001330 RID: 4912
	[CustomEditField(Sections = "Deck Pages")]
	public GameObject m_customDeckPageUpperBone;

	// Token: 0x04001331 RID: 4913
	[CustomEditField(Sections = "Deck Pages")]
	public GameObject m_customDeckPageLowerBone;

	// Token: 0x04001332 RID: 4914
	[CustomEditField(Sections = "Deck Pages")]
	public GameObject m_customDeckPageHideBone;

	// Token: 0x04001333 RID: 4915
	public Widget m_casualPlayDisplayWidget;

	// Token: 0x04001334 RID: 4916
	public GameObject m_missingClassicDeck;

	// Token: 0x04001335 RID: 4917
	public HighlightState m_collectionButtonGlow;

	// Token: 0x04001336 RID: 4918
	public GameObject m_labelDecoration;

	// Token: 0x04001337 RID: 4919
	public List<PlayMakerFSM> formatChangeGlowFSMs;

	// Token: 0x04001338 RID: 4920
	public List<PlayMakerFSM> newDeckFormatChangeGlowFSMs;

	// Token: 0x04001339 RID: 4921
	public List<GameObject> m_premadeDeckGlowBurstObjects;

	// Token: 0x0400133A RID: 4922
	public NestedPrefab m_switchFormatButtonContainer;

	// Token: 0x0400133B RID: 4923
	private SwitchFormatButton m_switchFormatButton;

	// Token: 0x0400133C RID: 4924
	public GameObject m_TheClockButtonBone;

	// Token: 0x0400133D RID: 4925
	public string m_leavingWildGlowEvent;

	// Token: 0x0400133E RID: 4926
	public string m_leavingClassicGlowEvent;

	// Token: 0x0400133F RID: 4927
	public string m_leavingCasualGlowEvent;

	// Token: 0x04001340 RID: 4928
	public string m_enteringWildGlowEvent;

	// Token: 0x04001341 RID: 4929
	public string m_enteringClassicGlowEvent;

	// Token: 0x04001342 RID: 4930
	public string m_enteringCasualGlowEvent;

	// Token: 0x04001343 RID: 4931
	public string m_newDeckLeavingClassicGlowEvent;

	// Token: 0x04001344 RID: 4932
	public string m_newDeckEnteringClassicGlowEvent;

	// Token: 0x04001345 RID: 4933
	public string m_newDeckLeavingWildGlowEvent;

	// Token: 0x04001346 RID: 4934
	public string m_newDeckEnteringWildGlowEvent;

	// Token: 0x04001347 RID: 4935
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_standardTransitionSound;

	// Token: 0x04001348 RID: 4936
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_wildTransitionSound;

	// Token: 0x04001349 RID: 4937
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_classicTransitionSound;

	// Token: 0x0400134A RID: 4938
	[CustomEditField(Sections = "Deck Sharing")]
	public UIBButton m_DeckShareRequestButton;

	// Token: 0x0400134B RID: 4939
	[CustomEditField(Sections = "Deck Sharing")]
	public GameObject m_DeckShareGlowOutQuad;

	// Token: 0x0400134C RID: 4940
	[CustomEditField(Sections = "Deck Sharing")]
	public float m_DeckShareGlowOutIntensity;

	// Token: 0x0400134D RID: 4941
	[CustomEditField(Sections = "Deck Sharing")]
	public ParticleSystem m_DeckShareParticles;

	// Token: 0x0400134E RID: 4942
	[CustomEditField(Sections = "Deck Sharing")]
	public float m_DeckShareTransitionTime = 1f;

	// Token: 0x0400134F RID: 4943
	[CustomEditField(Sections = "Phone Only")]
	public SlidingTray m_rankedDetailsTray;

	// Token: 0x04001350 RID: 4944
	[CustomEditField(Sections = "Phone Only")]
	public GameObject m_detailsTrayFrame;

	// Token: 0x04001351 RID: 4945
	[CustomEditField(Sections = "Phone Only")]
	public Transform m_medalBone_phone;

	// Token: 0x04001352 RID: 4946
	[CustomEditField(Sections = "Phone Only")]
	public Mesh m_alternateDetailsTrayMesh;

	// Token: 0x04001353 RID: 4947
	[CustomEditField(Sections = "Phone Only")]
	public Material m_arrowButtonShadowMaterial;

	// Token: 0x04001354 RID: 4948
	[CustomEditField(Sections = "Mode Background Textures")]
	public DeckPickerTrayDisplay.ModeTextures m_adventureTextures;

	// Token: 0x04001355 RID: 4949
	[CustomEditField(Sections = "Mode Background Textures")]
	public DeckPickerTrayDisplay.ModeTextures m_collectionTextures;

	// Token: 0x04001356 RID: 4950
	[CustomEditField(Sections = "Mode Background Textures")]
	public DeckPickerTrayDisplay.ModeTextures m_tavernBrawlTextures;

	// Token: 0x04001357 RID: 4951
	[CustomEditField(Sections = "Mode Background Textures")]
	public DeckPickerTrayDisplay.ModeTextures m_tournamentTextures;

	// Token: 0x04001358 RID: 4952
	[CustomEditField(Sections = "Mode Background Textures")]
	public DeckPickerTrayDisplay.ModeTextures m_friendlyTextures;

	// Token: 0x04001359 RID: 4953
	public float m_rankedPlayDisplayShowDelay;

	// Token: 0x0400135A RID: 4954
	public float m_rankedPlayDisplayHideDelay;

	// Token: 0x0400135B RID: 4955
	private const float TRAY_SLIDE_TIME = 0.25f;

	// Token: 0x0400135C RID: 4956
	private const float TRAY_SINK_TIME = 0f;

	// Token: 0x0400135D RID: 4957
	private static readonly Vector3 INNKEEPER_QUOTE_POS = new Vector3(103f, NotificationManager.DEPTH, 42f);

	// Token: 0x0400135E RID: 4958
	private static readonly string STANDARD_COMING_SOON_POPUP_NAME = "RotationPopUp_ComingSoon.prefab:afff670e4001e11429c04d2e0c27dd76";

	// Token: 0x0400135F RID: 4959
	private static readonly AssetReference CUSTOM_DECK_PAGE = new AssetReference("CustomDeckPage_Top.prefab:650072e121717c04f89ac014eb3dc290");

	// Token: 0x04001360 RID: 4960
	private static readonly AssetReference FORMAT_TYPE_PICKER_POPUP_PREFAB = new AssetReference("FormatTypePickerPopup.prefab:aa88133d144782b40b3fd8818084006c");

	// Token: 0x04001361 RID: 4961
	private const string CREATE_WILD_DECK_STRING_FORMAT = "GLUE_CREATE_WILD_DECK";

	// Token: 0x04001362 RID: 4962
	private const string CREATE_STANDARD_DECK_STRING_FORMAT = "GLUE_CREATE_STANDARD_DECK";

	// Token: 0x04001363 RID: 4963
	private const string CREATE_CLASSIC_DECK_STRING_FORMAT = "GLUE_CREATE_CLASSIC_DECK";

	// Token: 0x04001364 RID: 4964
	private const string WILD_CLICKED_EVENT_NAME = "WILD_BUTTON_CLICKED";

	// Token: 0x04001365 RID: 4965
	private const string STANDARD_CLICKED_EVENT_NAME = "STANDARD_BUTTON_CLICKED";

	// Token: 0x04001366 RID: 4966
	private const string CLASSIC_CLICKED_EVENT_NAME = "CLASSIC_BUTTON_CLICKED";

	// Token: 0x04001367 RID: 4967
	private const string CASUAL_CLICKED_EVENT_NAME = "CASUAL_BUTTON_CLICKED";

	// Token: 0x04001368 RID: 4968
	private const string OPEN_WITH_CASUAL_EVENT = "OPEN_WITH_CASUAL";

	// Token: 0x04001369 RID: 4969
	private const string OPEN_WITHOUT_CASUAL_EVENT = "OPEN_WITHOUT_CASUAL";

	// Token: 0x0400136A RID: 4970
	private const string SETROTATION_OPEN_WITH_CASUAL_EVENT = "SETROTATION_OPEN_WITH_CASUAL";

	// Token: 0x0400136B RID: 4971
	private const string HIDE = "HIDE";

	// Token: 0x0400136C RID: 4972
	private const string HIDE_WITH_CASUAL_EVENT = "HIDE_WITH_CASUAL";

	// Token: 0x0400136D RID: 4973
	private const string HIDE_WITHOUT_CASUAL_EVENT = "HIDE_WITHOUT_CASUAL";

	// Token: 0x0400136E RID: 4974
	private const string FORMAT_PICKER_4_BUTTONS = "4BUTTONS";

	// Token: 0x0400136F RID: 4975
	private const string FORMAT_PICKER_3_BUTTONS = "3BUTTONS";

	// Token: 0x04001370 RID: 4976
	private const string FORMAT_PICKER_2_BUTTONS = "2BUTTONS";

	// Token: 0x04001371 RID: 4977
	private UIBButton m_leftArrow;

	// Token: 0x04001372 RID: 4978
	private UIBButton m_rightArrow;

	// Token: 0x04001373 RID: 4979
	private HeroXPBar m_xpBar;

	// Token: 0x04001374 RID: 4980
	private CollectionDeckBoxVisual m_selectedCustomDeckBox;

	// Token: 0x04001375 RID: 4981
	private DeckPickerTrayDisplay.ModeTextures m_currentModeTextures;

	// Token: 0x04001376 RID: 4982
	private bool m_heroChosen;

	// Token: 0x04001377 RID: 4983
	private static Coroutine s_selectHeroCoroutine = null;

	// Token: 0x04001378 RID: 4984
	private DeckPickerMode m_deckPickerMode;

	// Token: 0x04001379 RID: 4985
	private int m_currentPageIndex;

	// Token: 0x0400137A RID: 4986
	private static DeckPickerTrayDisplay s_instance;

	// Token: 0x0400137B RID: 4987
	private RankedPlayDisplay m_rankedPlayDisplay;

	// Token: 0x0400137C RID: 4988
	private int m_numDecks;

	// Token: 0x0400137D RID: 4989
	private int m_numPagesToShow = 1;

	// Token: 0x0400137E RID: 4990
	private List<CustomDeckPage> m_customPages = new List<CustomDeckPage>();

	// Token: 0x0400137F RID: 4991
	private bool m_delayButtonAnims;

	// Token: 0x04001380 RID: 4992
	private Notification m_expoThankQuote;

	// Token: 0x04001381 RID: 4993
	private Notification m_expoIntroQuote;

	// Token: 0x04001382 RID: 4994
	private Notification m_switchFormatPopup;

	// Token: 0x04001383 RID: 4995
	private Notification m_innkeeperQuote;

	// Token: 0x04001384 RID: 4996
	private bool m_showStandardComingSoonNotice;

	// Token: 0x04001385 RID: 4997
	private GameLayer m_defaultDetailsLayer;

	// Token: 0x04001386 RID: 4998
	private bool m_usingSharedDecks;

	// Token: 0x04001387 RID: 4999
	private bool m_doingDeckShareTransition;

	// Token: 0x04001388 RID: 5000
	private bool m_isDeckShareRequestButtonHovered;

	// Token: 0x04001389 RID: 5001
	private long m_lastSeasonBonusStarPopUpSeen;

	// Token: 0x0400138A RID: 5002
	private long m_bonusStarsPopUpSeenCount;

	// Token: 0x0400138B RID: 5003
	private TranslatedMedalInfo m_currentMedalInfo;

	// Token: 0x0400138C RID: 5004
	private bool m_inHeroPicker;

	// Token: 0x0400138D RID: 5005
	private Widget m_formatTypePickerWidget;

	// Token: 0x0400138E RID: 5006
	private Widget m_rankedPlayDisplayWidget;

	// Token: 0x0400138F RID: 5007
	private bool m_HasSeenPlayStandardToWildVO;

	// Token: 0x04001390 RID: 5008
	private bool m_HasSeenPlayStandardToClassicVO;

	// Token: 0x04001391 RID: 5009
	private Coroutine m_showLeftArrowCoroutine;

	// Token: 0x04001392 RID: 5010
	private Coroutine m_showRightArrowCoroutine;

	// Token: 0x04001393 RID: 5011
	[CustomEditField(Sections = "Set Rotation Tutorial")]
	public GameObject m_formatTutorialPopUpPrefab;

	// Token: 0x04001394 RID: 5012
	[CustomEditField(Sections = "Set Rotation Tutorial")]
	public Transform m_formatTutorialPopUpBone;

	// Token: 0x04001395 RID: 5013
	[CustomEditField(Sections = "Set Rotation Tutorial")]
	public Transform m_Switch_Format_Notification_Bone;

	// Token: 0x04001396 RID: 5014
	[CustomEditField(Sections = "Set Rotation Tutorial")]
	public Animator m_dimQuad;

	// Token: 0x04001397 RID: 5015
	[CustomEditField(Sections = "Set Rotation Tutorial")]
	public PegUIElement m_clickCatcher;

	// Token: 0x04001398 RID: 5016
	[CustomEditField(Sections = "Set Rotation Tutorial", T = EditType.SOUND_PREFAB)]
	public string m_wildDeckTransitionSound;

	// Token: 0x04001399 RID: 5017
	private DeckPickerTrayDisplay.SetRotationTutorialState m_setRotationTutorialState;

	// Token: 0x0400139A RID: 5018
	private float m_showQuestPause = 1f;

	// Token: 0x0400139B RID: 5019
	private float m_playVOPause = 1f;

	// Token: 0x0400139C RID: 5020
	private bool m_shouldContinue;

	// Token: 0x0400139D RID: 5021
	private List<long> m_noticeIdsToAck = new List<long>();

	// Token: 0x0200157F RID: 5503
	[Serializable]
	public class ModeTextures
	{
		// Token: 0x0600E079 RID: 57465 RVA: 0x003FD468 File Offset: 0x003FB668
		public Texture GetTextureForFormat(VisualsFormatType visualsFormatType)
		{
			switch (visualsFormatType)
			{
			case VisualsFormatType.VFT_WILD:
				return this.wildTex;
			case VisualsFormatType.VFT_STANDARD:
				return this.standardTex;
			case VisualsFormatType.VFT_CLASSIC:
				return this.classicTex;
			case VisualsFormatType.VFT_CASUAL:
				return this.casualTex;
			default:
				Debug.LogError("ModeTextures.GetTextureForFormat does not support " + visualsFormatType.ToString());
				return null;
			}
		}

		// Token: 0x0600E07A RID: 57466 RVA: 0x003FD4C8 File Offset: 0x003FB6C8
		public Texture GetCustomTextureForFormat(VisualsFormatType visualsFormatType)
		{
			switch (visualsFormatType)
			{
			case VisualsFormatType.VFT_WILD:
				return this.customWildTex;
			case VisualsFormatType.VFT_STANDARD:
				return this.customStandardTex;
			case VisualsFormatType.VFT_CLASSIC:
				return this.customClassicTex;
			case VisualsFormatType.VFT_CASUAL:
				return this.customCasualTex;
			default:
				Debug.LogError("ModeTextures.GetTextureForFormat does not support " + visualsFormatType.ToString());
				return null;
			}
		}

		// Token: 0x0400AE03 RID: 44547
		[SerializeField]
		public Texture customStandardTex;

		// Token: 0x0400AE04 RID: 44548
		[SerializeField]
		public Texture customWildTex;

		// Token: 0x0400AE05 RID: 44549
		[SerializeField]
		public Texture customClassicTex;

		// Token: 0x0400AE06 RID: 44550
		[SerializeField]
		public Texture customCasualTex;

		// Token: 0x0400AE07 RID: 44551
		[SerializeField]
		public Texture standardTex;

		// Token: 0x0400AE08 RID: 44552
		[SerializeField]
		public Texture wildTex;

		// Token: 0x0400AE09 RID: 44553
		[SerializeField]
		public Texture classicTex;

		// Token: 0x0400AE0A RID: 44554
		[SerializeField]
		public Texture casualTex;

		// Token: 0x0400AE0B RID: 44555
		[SerializeField]
		public Texture classDivotTex;

		// Token: 0x0400AE0C RID: 44556
		[SerializeField]
		public Texture guestHeroDivotTex;
	}

	// Token: 0x02001580 RID: 5504
	private enum SetRotationTutorialState
	{
		// Token: 0x0400AE0E RID: 44558
		INACTIVE,
		// Token: 0x0400AE0F RID: 44559
		PREPARING,
		// Token: 0x0400AE10 RID: 44560
		READY,
		// Token: 0x0400AE11 RID: 44561
		SHOW_TUTORIAL_POPUPS,
		// Token: 0x0400AE12 RID: 44562
		SWITCH_MODE_WALKTHROUGH,
		// Token: 0x0400AE13 RID: 44563
		SHOW_QUEST_LOG
	}
}
