using System;
using System.Collections.Generic;
using System.Linq;
using Assets;
using bgs;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x0200006A RID: 106
[RequireComponent(typeof(WidgetTemplate))]
public class BaconDisplay : MonoBehaviour
{
	// Token: 0x17000053 RID: 83
	// (get) Token: 0x060005DC RID: 1500 RVA: 0x00022365 File Offset: 0x00020565
	public bool IsFinishedLoading
	{
		get
		{
			return this.m_playButtonFinishedLoading && this.m_backButtonFinishedLoading && this.m_statsButtonFinishedLoading && !this.m_OwningWidget.IsChangingStates;
		}
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x0002238F File Offset: 0x0002058F
	private void Awake()
	{
		this.RegisterListeners();
		this.m_OwningWidget = base.GetComponent<WidgetTemplate>();
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x000223A4 File Offset: 0x000205A4
	private void Start()
	{
		this.m_PlayButtonReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnPlayButtonReady));
		this.m_BackButtonReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnBackButtonReady));
		this.m_PlayButtonPhoneReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnPlayButtonReady));
		this.m_BackButtonPhoneReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnBackButtonReady));
		this.m_StatsButtonReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnStatsButtonReady));
		this.m_StatsButtonPhoneReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnStatsButtonReady));
		this.m_StatsPageReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnStatsPagePCReady));
		this.m_StatsPagePhoneReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnStatsPagePhoneReady));
		this.m_LobbyReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnLobbyPCReady));
		this.m_LobbyPhoneReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnLobbyPhoneReady));
		NetCache.Get().RegisterScreenBattlegrounds(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		PartyManager.Get().AddChangedListener(new PartyManager.ChangedCallback(this.OnPartyChanged));
		this.InitializeBaconLobbyData();
		NarrativeManager.Get().OnBattlegroundsEntered();
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_Battlegrounds);
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.BATTLEGROUNDS_SCREEN
		});
		this.ShowLowMemoryAlertMessage();
		this.ShowPerkUpsellMessage();
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x00022509 File Offset: 0x00020709
	private void OnDestroy()
	{
		this.UnregisterListeners();
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x00022511 File Offset: 0x00020711
	private void BaconDisplayEventListener(string eventName)
	{
		if (eventName == "OpenShop")
		{
			this.OpenBattlegroundsShop();
		}
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x00022528 File Offset: 0x00020728
	public void OnPlayButtonReady(VisualController buttonVisualController)
	{
		if (buttonVisualController == null)
		{
			global::Error.AddDevWarning("UI Error!", "PlayButton could not be found! You will not be able to click 'Play'!", Array.Empty<object>());
			return;
		}
		this.m_playButton = buttonVisualController.gameObject.GetComponent<PlayButton>();
		this.m_playButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.PlayButtonRelease));
		this.UpdatePlayButtonBasedOnPartyInfo();
		this.m_playButtonFinishedLoading = true;
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x0002258C File Offset: 0x0002078C
	public void OnBackButtonReady(VisualController buttonVisualController)
	{
		if (buttonVisualController == null)
		{
			global::Error.AddDevWarning("UI Error!", "BackButton could not be found! You will not be able to click 'Back'!", Array.Empty<object>());
			return;
		}
		buttonVisualController.gameObject.GetComponent<UIBButton>().AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.BackButtonRelease));
		this.m_backButtonFinishedLoading = true;
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x000225DC File Offset: 0x000207DC
	public void OnStatsButtonReady(VisualController buttonVisualController)
	{
		if (buttonVisualController == null)
		{
			global::Error.AddDevWarning("UI Error!", "StatsButton could not be found! You will not be able to show 'Stats'!", Array.Empty<object>());
			return;
		}
		this.m_statsButton = buttonVisualController.gameObject.GetComponent<UIBButton>();
		this.m_statsButtonClickable = buttonVisualController.gameObject.GetComponent<Clickable>();
		this.UpdateStatsButtonState();
		this.m_statsButtonFinishedLoading = true;
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x00022638 File Offset: 0x00020838
	private void UpdateStatsButtonState()
	{
		if (this.m_statsButton == null || this.m_statsButtonClickable == null)
		{
			return;
		}
		bool flag = this.HasAccessToStatsPage();
		this.m_statsButton.Flip(flag, true);
		this.m_statsButton.SetEnabled(flag, false);
		this.m_statsButtonClickable.Active = flag;
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x00022690 File Offset: 0x00020890
	public void PlayButtonRelease(UIEvent e)
	{
		if (!BattleNet.IsConnected())
		{
			return;
		}
		if (GameMgr.Get().IsFindingGame())
		{
			return;
		}
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.BATTLEGROUNDS_QUEUE
		});
		bool flag = !NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.BattlegroundsTutorial;
		long num;
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.BACON, GameSaveKeySubkeyId.BACON_HAS_SEEN_TUTORIAL, out num);
		PartyManager partyManager = PartyManager.Get();
		if (partyManager.IsInParty() && partyManager.IsInBattlegroundsParty() && partyManager.IsPartyLeader())
		{
			partyManager.FindGame();
			return;
		}
		if (num == 0L && !flag)
		{
			this.PlayBaconTutorial();
			return;
		}
		GameMgr.Get().FindGame(GameType.GT_BATTLEGROUNDS, FormatType.FT_WILD, 3459, 0, 0L, null, null, false, null, GameType.GT_UNKNOWN);
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x00022750 File Offset: 0x00020950
	public void PlayBaconTutorial()
	{
		GameMgr.Get().FindGame(GameType.GT_VS_AI, FormatType.FT_WILD, 3539, 0, 0L, null, null, false, null, GameType.GT_UNKNOWN);
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x0002277E File Offset: 0x0002097E
	public void BackButtonRelease(UIEvent e)
	{
		if (PartyManager.Get().IsInBattlegroundsParty())
		{
			this.ShowLeavePartyDialog();
			return;
		}
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.GAME_MODE, SceneMgr.TransitionHandlerType.NEXT_SCENE, null);
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x000227A4 File Offset: 0x000209A4
	private void ShowLeavePartyDialog()
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_BACON_LEAVE_PARTY_CONFIRMATION_HEADER");
		popupInfo.m_text = (PartyManager.Get().IsPartyLeader() ? GameStrings.Get("GLUE_BACON_DISBAND_PARTY_CONFIRMATION_BODY") : GameStrings.Get("GLUE_BACON_LEAVE_PARTY_CONFIRMATION_BODY"));
		popupInfo.m_iconSet = AlertPopup.PopupInfo.IconSet.Default;
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_confirmText = GameStrings.Get("GLUE_BACON_LEAVE_PARTY_CONFIRMATION_CONFIRM");
		popupInfo.m_cancelText = GameStrings.Get("GLUE_BACON_LEAVE_PARTY_CONFIRMATION_CANCEL");
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
		{
			if (response == AlertPopup.Response.CONFIRM)
			{
				BaconParty.Get().LeaveParty();
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			}
		};
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x0002285C File Offset: 0x00020A5C
	private void OnNetCacheReady()
	{
		NetCache.Get().UnregisterNetCacheHandler(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		if (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.Battlegrounds)
		{
			return;
		}
		if (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
		{
			return;
		}
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		global::Error.AddWarningLoc("GLOBAL_FEATURE_DISABLED_TITLE", "GLOBAL_FEATURE_DISABLED_MESSAGE_BATTLEGROUNDS", Array.Empty<object>());
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x000228C5 File Offset: 0x00020AC5
	private void OnStatsPagePCReady(VisualController visualController)
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		if (visualController == null)
		{
			global::Error.AddDevWarning("UI Error!", "StatsPage could not be found! You will not be able to view stats!", Array.Empty<object>());
			return;
		}
		this.InitializeBaconStatsPageData(visualController);
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x000228F9 File Offset: 0x00020AF9
	private void OnStatsPagePhoneReady(VisualController visualController)
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		if (visualController == null)
		{
			global::Error.AddDevWarning("UI Error!", "StatsPage could not be found! You will not be able to view stats!", Array.Empty<object>());
			return;
		}
		this.InitializeBaconStatsPageData(visualController);
	}

	// Token: 0x060005EC RID: 1516 RVA: 0x0002292D File Offset: 0x00020B2D
	private void OnLobbyPCReady(Widget widget)
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		if (widget == null)
		{
			global::Error.AddDevWarning("UI Error!", "LobbyReference could not be found!", Array.Empty<object>());
			return;
		}
		widget.RegisterEventListener(new Widget.EventListenerDelegate(this.BaconDisplayEventListener));
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x0002296C File Offset: 0x00020B6C
	private void OnLobbyPhoneReady(Widget widget)
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		if (widget == null)
		{
			global::Error.AddDevWarning("UI Error!", "LobbyReference could not be found!", Array.Empty<object>());
			return;
		}
		widget.RegisterEventListener(new Widget.EventListenerDelegate(this.BaconDisplayEventListener));
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x000229AC File Offset: 0x00020BAC
	public BaconLobbyDataModel GetBaconLobbyDataModel()
	{
		VisualController component = base.GetComponent<VisualController>();
		if (component == null)
		{
			return null;
		}
		Widget owner = component.Owner;
		IDataModel dataModel;
		if (!owner.GetDataModel(43, out dataModel))
		{
			dataModel = new BaconLobbyDataModel();
			owner.BindDataModel(dataModel, false);
		}
		return dataModel as BaconLobbyDataModel;
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x000229F4 File Offset: 0x00020BF4
	private void InitializeBaconLobbyData()
	{
		BaconLobbyDataModel baconLobbyDataModel = this.GetBaconLobbyDataModel();
		if (baconLobbyDataModel == null)
		{
			return;
		}
		NetCache.NetCacheBaconRatingInfo netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBaconRatingInfo>();
		if (netObject != null)
		{
			baconLobbyDataModel.Rating = netObject.Rating;
		}
		else
		{
			global::Log.Net.PrintError("No bacon rating info in NetCache.", Array.Empty<object>());
		}
		baconLobbyDataModel.Top4Finishes = (int)this.GetBaconGameSaveValue(GameSaveKeySubkeyId.BACON_TOP_4_FINISHES);
		baconLobbyDataModel.FirstPlaceFinishes = (int)this.GetBaconGameSaveValue(GameSaveKeySubkeyId.BACON_FIRST_PLACE_FINISHES);
		NetCache.NetCacheBaconPremiumStatus netObject2 = NetCache.Get().GetNetObject<NetCache.NetCacheBaconPremiumStatus>();
		if (netObject2 == null)
		{
			return;
		}
		int num = 0;
		BoosterDbId premiumPackType = BoosterDbId.INVALID;
		foreach (BattlegroundSeasonPremiumStatus battlegroundSeasonPremiumStatus in netObject2.SeasonPremiumStatus)
		{
			if ((ulong)battlegroundSeasonPremiumStatus.NumPacksOpened > (ulong)((long)num))
			{
				num = (int)battlegroundSeasonPremiumStatus.NumPacksOpened;
				premiumPackType = (BoosterDbId)battlegroundSeasonPremiumStatus.PackType;
			}
		}
		baconLobbyDataModel.PremiumPackOwnedCount = num;
		baconLobbyDataModel.PremiumPackType = premiumPackType;
		baconLobbyDataModel.BonusesLicenseOwned = this.OwnsBattlegroundsBonusesLicenseThisSeason();
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x00022AF0 File Offset: 0x00020CF0
	private void RegisterListeners()
	{
		GameMgr.Get().RegisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		Network.Get().RegisterNetHandler(BattlegroundsPremiumStatusResponse.PacketID.ID, new Network.NetHandler(this.OnBaconPremiumStatus), null);
		AccountLicenseMgr.Get().RegisterAccountLicensesChangedListener(new AccountLicenseMgr.AccountLicensesChangedCallback(this.OnAccountLicensesChanged));
		GameMgr.Get().OnTransitionPopupShown += this.OnTransitionPopupShown;
		BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPresenceUpdated));
		BnetNearbyPlayerMgr.Get().AddChangeListener(new BnetNearbyPlayerMgr.ChangeCallback(this.OnNearbyPlayersUpdated));
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x00022B90 File Offset: 0x00020D90
	private void UnregisterListeners()
	{
		if (GameMgr.Get() != null)
		{
			GameMgr.Get().UnregisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
			GameMgr.Get().OnTransitionPopupShown -= this.OnTransitionPopupShown;
		}
		if (Network.Get() != null)
		{
			Network.Get().RemoveNetHandler(BattlegroundsPremiumStatusResponse.PacketID.ID, new Network.NetHandler(this.OnBaconPremiumStatus));
		}
		if (AccountLicenseMgr.Get() != null)
		{
			AccountLicenseMgr.Get().RemoveAccountLicensesChangedListener(new AccountLicenseMgr.AccountLicensesChangedCallback(this.OnAccountLicensesChanged));
		}
		if (PartyManager.Get() != null)
		{
			PartyManager.Get().RemoveChangedListener(new PartyManager.ChangedCallback(this.OnPartyChanged));
		}
		if (BnetPresenceMgr.Get() != null)
		{
			BnetPresenceMgr.Get().RemovePlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPresenceUpdated));
		}
		if (BnetNearbyPlayerMgr.Get() != null)
		{
			BnetNearbyPlayerMgr.Get().RemoveChangeListener(new BnetNearbyPlayerMgr.ChangeCallback(this.OnNearbyPlayersUpdated));
		}
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x00022C74 File Offset: 0x00020E74
	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		FindGameState state = eventData.m_state;
		if (state - FindGameState.CLIENT_CANCELED <= 1 || state - FindGameState.BNET_QUEUE_CANCELED <= 1 || state == FindGameState.SERVER_GAME_CANCELED)
		{
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.BATTLEGROUNDS_SCREEN
			});
		}
		return false;
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x00022CB5 File Offset: 0x00020EB5
	private void OnTransitionPopupShown()
	{
		Shop.Get().Close();
		DialogManager.Get().ClearAllImmediately();
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x00022CCC File Offset: 0x00020ECC
	private void ShowLowMemoryAlertMessage()
	{
		if (!this.ShowLowMemoryWarning || BaconDisplay.m_hasSeenLowMemoryWarningThisSession)
		{
			return;
		}
		BaconDisplay.m_hasSeenLowMemoryWarningThisSession = true;
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_BACON_LOW_MEMORY_HEADER"),
			m_text = GameStrings.Get("GLUE_BACON_LOW_MEMORY_BODY"),
			m_showAlertIcon = true,
			m_responseDisplay = AlertPopup.ResponseDisplay.OK
		};
		DialogManager.Get().ShowPopup(info);
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x00022D34 File Offset: 0x00020F34
	private void ShowPerkUpsellMessage()
	{
		if (SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.GAMEPLAY || GameMgr.Get().LastGameData.BattlegroundsLeaderboardPlace > 4 || this.OwnsBattlegroundsBonusesLicenseThisSeason())
		{
			return;
		}
		NotificationManager.Get().CreateCharacterQuote("Bob_Banner_Quote.prefab:723ff2809fb544344a9dd587aafbc1aa", GameStrings.Get("GLUE_BACON_VICTORY_BONUS_MESSAGE"), "VO_DALA_BOSS_99h_Male_Human_CombatWin_03.prefab:8ff8566f08747ad4bb76409e6db1504b", false, 0f, CanvasAnchor.BOTTOM_LEFT, false);
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x00022D90 File Offset: 0x00020F90
	public BaconStatsPageDataModel GetBaconStatsPageDataModel(VisualController visualController)
	{
		if (visualController == null)
		{
			return null;
		}
		Widget owner = visualController.Owner;
		IDataModel dataModel;
		if (!owner.GetDataModel(122, out dataModel))
		{
			dataModel = new BaconStatsPageDataModel();
			owner.BindDataModel(dataModel, false);
		}
		return dataModel as BaconStatsPageDataModel;
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x00022DD0 File Offset: 0x00020FD0
	private void InitializeBaconStatsPageData(VisualController visualController)
	{
		BaconStatsPageDataModel dataModel = this.GetBaconStatsPageDataModel(visualController);
		if (dataModel == null)
		{
			return;
		}
		dataModel.Top4Finishes = (int)this.GetBaconGameSaveValue(GameSaveKeySubkeyId.BACON_TOP_4_FINISHES);
		dataModel.FirstPlaceFinishes = (int)this.GetBaconGameSaveValue(GameSaveKeySubkeyId.BACON_FIRST_PLACE_FINISHES);
		dataModel.TriplesCreated = (int)this.GetBaconGameSaveValue(GameSaveKeySubkeyId.BACON_TRIPLES_CREATED);
		dataModel.TavernUpgrades = (int)this.GetBaconGameSaveValue(GameSaveKeySubkeyId.BACON_TAVERN_UPGRADES);
		dataModel.DamageInOneTurn = (int)this.GetBaconGameSaveValue(GameSaveKeySubkeyId.BACON_MOST_DAMAGE_ONE_TURN);
		dataModel.LongestWinStreak = (int)this.GetBaconGameSaveValue(GameSaveKeySubkeyId.BACON_LONGEST_COMBAT_WIN_STREAK);
		dataModel.SecondsPlayed = (int)this.GetBaconGameSaveValue(GameSaveKeySubkeyId.BACON_TIME_PLAYED);
		List<long> baconGameSaveValueList = this.GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_MINIONS_KILLED_COUNT);
		dataModel.MinionsDestroyed = ((baconGameSaveValueList == null) ? 0 : ((int)baconGameSaveValueList.Sum()));
		List<long> baconGameSaveValueList2 = this.GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_HEROES_KILLED_COUNT);
		dataModel.PlayersEliminated = ((baconGameSaveValueList2 == null) ? 0 : ((int)baconGameSaveValueList2.Sum()));
		List<long> baconGameSaveValueList3 = this.GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_LARGEST_MINION_ATTACK_HEALTH);
		BaconStatsPageDataModel dataModel2 = dataModel;
		CardDataModel biggestMinionId;
		if (baconGameSaveValueList3 != null && baconGameSaveValueList3.Count<long>() >= 3)
		{
			CardDataModel cardDataModel = new CardDataModel();
			cardDataModel.CardId = GameUtils.TranslateDbIdToCardId((int)baconGameSaveValueList3[0], false);
			biggestMinionId = cardDataModel;
			cardDataModel.Premium = TAG_PREMIUM.NORMAL;
		}
		else
		{
			biggestMinionId = null;
		}
		dataModel2.BiggestMinionId = biggestMinionId;
		dataModel.BiggestMinionAttack = ((baconGameSaveValueList3 == null || baconGameSaveValueList3.Count<long>() < 3) ? 0 : ((int)baconGameSaveValueList3[1]));
		dataModel.BiggestMinionHealth = ((baconGameSaveValueList3 == null || baconGameSaveValueList3.Count<long>() < 3) ? 0 : ((int)baconGameSaveValueList3[2]));
		dataModel.BiggestMinionString = GameStrings.Format("GLUE_BACON_STATS_VALUE_BIGGEST_MINION", new object[]
		{
			dataModel.BiggestMinionAttack,
			dataModel.BiggestMinionHealth
		});
		if (dataModel.SecondsPlayed > 3600)
		{
			dataModel.TimePlayedString = GameStrings.Format("GLUE_BACON_STATS_VALUE_HOURS_PLAYED", new object[]
			{
				Mathf.FloorToInt((float)(dataModel.SecondsPlayed / 3600))
			});
		}
		else
		{
			dataModel.TimePlayedString = GameStrings.Format("GLUE_BACON_STATS_VALUE_MINUTES_PLAYED", new object[]
			{
				Mathf.FloorToInt((float)(dataModel.SecondsPlayed / 60))
			});
		}
		List<KeyValuePair<long, long>> sortedListFromGameSaveDataLists = this.GetSortedListFromGameSaveDataLists(this.GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_BOUGHT_MINIONS), this.GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_BOUGHT_MINIONS_COUNT));
		dataModel.MostBoughtMinionsCardIds = new DataModelList<CardDataModel>();
		dataModel.MostBoughtMinionsCardIds.AddRange(from kvp in sortedListFromGameSaveDataLists
		select new CardDataModel
		{
			CardId = GameUtils.TranslateDbIdToCardId((int)kvp.Key, false),
			Premium = TAG_PREMIUM.NORMAL
		});
		dataModel.MostBoughtMinionsCount = new DataModelList<int>();
		dataModel.MostBoughtMinionsCount.AddRange(from kvp in sortedListFromGameSaveDataLists
		select (int)kvp.Value);
		List<KeyValuePair<long, long>> sortedListFromGameSaveDataLists2 = this.GetSortedListFromGameSaveDataLists(this.GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_HEROES_WON_WITH), this.GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_HEROES_WON_WITH_COUNT));
		dataModel.TopHeroesByWinCardIds = new DataModelList<CardDataModel>();
		dataModel.TopHeroesByWinCardIds.AddRange(from kvp in sortedListFromGameSaveDataLists2
		select new CardDataModel
		{
			CardId = GameUtils.TranslateDbIdToCardId((int)kvp.Key, false),
			Premium = TAG_PREMIUM.NORMAL
		});
		dataModel.TopHeroesByWinCount = new DataModelList<int>();
		dataModel.TopHeroesByWinCount.AddRange(from kvp in sortedListFromGameSaveDataLists2
		select (int)kvp.Value);
		List<KeyValuePair<long, long>> sortedListFromGameSaveDataLists3 = this.GetSortedListFromGameSaveDataLists(this.GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_HEROES_PICKED), this.GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_HEROES_PICKED_COUNT));
		dataModel.TopHeroesByGamesPlayedCardIds = new DataModelList<CardDataModel>();
		dataModel.TopHeroesByGamesPlayedCardIds.AddRange(from kvp in sortedListFromGameSaveDataLists3
		select new CardDataModel
		{
			CardId = GameUtils.TranslateDbIdToCardId((int)kvp.Key, false),
			Premium = TAG_PREMIUM.NORMAL
		});
		dataModel.TopHeroesByGamesPlayedCount = new DataModelList<int>();
		dataModel.TopHeroesByGamesPlayedCount.AddRange(from kvp in sortedListFromGameSaveDataLists3
		select (int)kvp.Value);
		dataModel.PastGames = new DataModelList<BaconPastGameStatsDataModel>();
		List<long> baconGameSaveValueList4 = this.GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_HEROES);
		List<long> baconGameSaveValueList5 = this.GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_PLACES);
		List<long> baconGameSaveValueList6 = this.GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_MINIONS_ID);
		List<long> baconGameSaveValueList7 = this.GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_MINIONS_ATTACK);
		List<long> baconGameSaveValueList8 = this.GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_MINIONS_HEALTH);
		List<long> baconGameSaveValueList9 = this.GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_MINIONS_GOLDEN);
		List<long> list = new List<long>();
		List<long> list2 = new List<long>();
		List<long> list3 = new List<long>();
		List<long> list4 = new List<long>();
		this.PopulateSpellLists((baconGameSaveValueList4 == null) ? 0 : baconGameSaveValueList4.Count, ref list, ref list2, ref list3, ref list4);
		List<BaconPastGameStatsDataModel> list5 = new List<BaconPastGameStatsDataModel>();
		int num = 0;
		while (num < 5 && baconGameSaveValueList4 != null && num < baconGameSaveValueList4.Count && num < baconGameSaveValueList5.Count)
		{
			string cardId = GameUtils.TranslateDbIdToCardId((int)baconGameSaveValueList4[num], false);
			CardDataModel hero = new CardDataModel
			{
				CardId = cardId,
				Premium = TAG_PREMIUM.NORMAL
			};
			CardDbfRecord cardRecord = GameUtils.GetCardRecord(cardId);
			string heroName = (cardRecord == null) ? "" : cardRecord.Name;
			CardDataModel heroPower = new CardDataModel
			{
				CardId = GameUtils.GetHeroPowerCardIdFromHero((int)baconGameSaveValueList4[num]),
				Premium = TAG_PREMIUM.NORMAL,
				SpellTypes = new DataModelList<SpellType>
				{
					SpellType.COIN_MANA_GEM
				}
			};
			DataModelList<CardDataModel> dataModelList = new DataModelList<CardDataModel>();
			for (int i = 0; i < 7; i++)
			{
				int num2 = num * 7 + i;
				if (baconGameSaveValueList6.Count <= num2 || baconGameSaveValueList7.Count <= num2 || baconGameSaveValueList8.Count <= num2 || baconGameSaveValueList9.Count <= num2 || list.Count <= num2 || list2.Count <= num2 || list3.Count <= num2 || list4.Count <= num2)
				{
					Debug.LogErrorFormat("Missing Minion Data for GameIndex={0}, MinionIndex={1}", new object[]
					{
						num,
						num2
					});
					break;
				}
				if (baconGameSaveValueList6[num2] == 0L)
				{
					break;
				}
				DataModelList<SpellType> dataModelList2 = new DataModelList<SpellType>();
				bool flag = baconGameSaveValueList9[num2] > 0L;
				if (list[num2] > 0L)
				{
					dataModelList2.Add(flag ? SpellType.TAUNT_INSTANT_PREMIUM : SpellType.TAUNT_INSTANT);
				}
				if (list2[num2] > 0L)
				{
					dataModelList2.Add(SpellType.DIVINE_SHIELD);
				}
				if (list3[num2] > 0L)
				{
					dataModelList2.Add(SpellType.POISONOUS);
				}
				if (list4[num2] > 0L)
				{
					dataModelList2.Add(SpellType.WINDFURY_IDLE);
				}
				dataModelList.Add(new CardDataModel
				{
					CardId = GameUtils.TranslateDbIdToCardId((int)baconGameSaveValueList6[num2], false),
					Premium = (flag ? TAG_PREMIUM.GOLDEN : TAG_PREMIUM.NORMAL),
					Attack = (int)baconGameSaveValueList7[num2],
					Health = (int)baconGameSaveValueList8[num2],
					SpellTypes = dataModelList2
				});
			}
			list5.Add(new BaconPastGameStatsDataModel
			{
				Hero = hero,
				HeroPower = heroPower,
				HeroName = heroName,
				Place = (int)baconGameSaveValueList5[num],
				Minions = dataModelList
			});
			num++;
		}
		list5.Reverse();
		list5.ForEach(delegate(BaconPastGameStatsDataModel g)
		{
			dataModel.PastGames.Add(g);
		});
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x00023568 File Offset: 0x00021768
	private void PopulateSpellLists(int pastGames, ref List<long> tauntList, ref List<long> divineShieldList, ref List<long> poisonousList, ref List<long> windfuryList)
	{
		tauntList = this.GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_MINIONS_TAUNT);
		divineShieldList = this.GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_MINIONS_DIVINE_SHIELD);
		poisonousList = this.GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_MINIONS_POISONOUS);
		windfuryList = this.GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_MINIONS_WINDFURY);
		if (tauntList == null)
		{
			tauntList = new List<long>();
		}
		if (divineShieldList == null)
		{
			divineShieldList = new List<long>();
		}
		if (poisonousList == null)
		{
			poisonousList = new List<long>();
		}
		if (windfuryList == null)
		{
			windfuryList = new List<long>();
		}
		while (tauntList.Count < pastGames * 7)
		{
			tauntList.Insert(0, 0L);
		}
		while (divineShieldList.Count < pastGames * 7)
		{
			divineShieldList.Insert(0, 0L);
		}
		while (poisonousList.Count < pastGames * 7)
		{
			poisonousList.Insert(0, 0L);
		}
		while (windfuryList.Count < pastGames * 7)
		{
			windfuryList.Insert(0, 0L);
		}
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x00023640 File Offset: 0x00021840
	private long GetBaconGameSaveValue(GameSaveKeySubkeyId subkey)
	{
		long result;
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.BACON, subkey, out result);
		return result;
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x00023664 File Offset: 0x00021864
	private List<long> GetBaconGameSaveValueList(GameSaveKeySubkeyId subkey)
	{
		List<long> result;
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.BACON, subkey, out result);
		return result;
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x00023688 File Offset: 0x00021888
	private List<KeyValuePair<long, long>> GetSortedListFromGameSaveDataLists(List<long> keys, List<long> values)
	{
		List<KeyValuePair<long, long>> list = new List<KeyValuePair<long, long>>();
		if (keys == null || values == null)
		{
			return list;
		}
		if (keys.Count != values.Count)
		{
			Debug.LogError("GetSortedListFromGameSaveDataLists: Stats Page Game Save Data Lists Length Not Equal!");
			return list;
		}
		for (int i = 0; i < keys.Count; i++)
		{
			list.Add(new KeyValuePair<long, long>(keys[i], values[i]));
		}
		return (from kvp in list
		orderby kvp.Value descending
		select kvp).ToList<KeyValuePair<long, long>>();
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x00023714 File Offset: 0x00021914
	private bool OwnsBattlegroundsBonusesLicenseThisSeason()
	{
		foreach (BattlegroundsSeasonDbfRecord battlegroundsSeasonDbfRecord in GameDbf.BattlegroundsSeason.GetRecords((BattlegroundsSeasonDbfRecord r) => SpecialEventManager.Get().IsEventActive(r.Event, true), -1))
		{
			if (battlegroundsSeasonDbfRecord.AccountLicenseRecord != null && AccountLicenseMgr.Get().OwnsAccountLicense(battlegroundsSeasonDbfRecord.AccountLicenseRecord.LicenseId))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x000237AC File Offset: 0x000219AC
	private bool HasAccessToStatsPage()
	{
		NetCache.NetCacheBaconPremiumStatus netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBaconPremiumStatus>();
		if (netObject == null)
		{
			return false;
		}
		using (List<BattlegroundSeasonPremiumStatus>.Enumerator enumerator = netObject.SeasonPremiumStatus.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.PremiumRewardUnlocked.Contains(BattlegroundSeasonRewardType.BG_REWARD_GAME_STATS))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x0002381C File Offset: 0x00021A1C
	private void OpenBattlegroundsShop()
	{
		long pmtProductId;
		if (StoreManager.Get().TryGetBonusProductBundleId(ProductType.PRODUCT_TYPE_BATTLEGROUNDS_BONUS, out pmtProductId))
		{
			Shop.OpenToProductPageWhenReady(pmtProductId, true);
			return;
		}
		this.ShowBattlegroundsBonusErrorPopup();
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x00023848 File Offset: 0x00021A48
	private void OnAccountLicensesChanged(List<AccountLicenseInfo> changedLicensesInfo, object userData)
	{
		BaconLobbyDataModel baconLobbyDataModel = this.GetBaconLobbyDataModel();
		if (baconLobbyDataModel != null)
		{
			baconLobbyDataModel.BonusesLicenseOwned = this.OwnsBattlegroundsBonusesLicenseThisSeason();
		}
		Network.Get().RequestBattlegroundsPremiumStatus();
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x00023875 File Offset: 0x00021A75
	private void OnBaconPremiumStatus()
	{
		this.UpdateStatsButtonState();
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x00023880 File Offset: 0x00021A80
	private void ShowBattlegroundsBonusErrorPopup()
	{
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_BACON_PERKS_ERROR_HEADER"),
			m_text = GameStrings.Get("GLUE_BACON_PERKS_ERROR_BODY"),
			m_showAlertIcon = false,
			m_responseDisplay = AlertPopup.ResponseDisplay.OK
		};
		DialogManager.Get().ShowPopup(info);
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x000238CC File Offset: 0x00021ACC
	private void OnPartyChanged(PartyManager.PartyInviteEvent inviteEvent, BnetGameAccountId playerGameAccountId, PartyManager.PartyData data, object userData)
	{
		this.UpdatePlayButtonBasedOnPartyInfo();
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x000238CC File Offset: 0x00021ACC
	private void OnPresenceUpdated(BnetPlayerChangelist changelist, object userData)
	{
		this.UpdatePlayButtonBasedOnPartyInfo();
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x000238CC File Offset: 0x00021ACC
	private void OnNearbyPlayersUpdated(BnetNearbyPlayerChangelist changelist, object userData)
	{
		this.UpdatePlayButtonBasedOnPartyInfo();
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x000238D4 File Offset: 0x00021AD4
	private void UpdatePlayButtonBasedOnPartyInfo()
	{
		if (this.m_playButton == null)
		{
			return;
		}
		string text = (!PartyManager.Get().IsInBattlegroundsParty() || PartyManager.Get().IsPartyLeader()) ? GameStrings.Get("GLOBAL_PLAY") : GameStrings.Get("GLOBAL_PLAY_WAITING");
		this.m_playButton.SetText(text);
		int readyPartyMemberCount = PartyManager.Get().GetReadyPartyMemberCount();
		int currentPartySize = PartyManager.Get().GetCurrentPartySize();
		string secondaryText = "";
		if (PartyManager.Get().IsInBattlegroundsParty() && PartyManager.Get().IsPartyLeader() && !GameMgr.Get().IsFindingGame() && readyPartyMemberCount < currentPartySize)
		{
			secondaryText = string.Format("{0}/{1}", readyPartyMemberCount, currentPartySize);
		}
		this.m_playButton.SetSecondaryText(secondaryText);
		if (PartyManager.Get().IsInBattlegroundsParty() && (!PartyManager.Get().IsPartyLeader() || readyPartyMemberCount < currentPartySize))
		{
			this.m_playButton.Disable(true);
			return;
		}
		this.m_playButton.Enable();
	}

	// Token: 0x04000424 RID: 1060
	public AsyncReference m_PlayButtonReference;

	// Token: 0x04000425 RID: 1061
	public AsyncReference m_PlayButtonPhoneReference;

	// Token: 0x04000426 RID: 1062
	public AsyncReference m_BackButtonReference;

	// Token: 0x04000427 RID: 1063
	public AsyncReference m_BackButtonPhoneReference;

	// Token: 0x04000428 RID: 1064
	public AsyncReference m_StatsButtonReference;

	// Token: 0x04000429 RID: 1065
	public AsyncReference m_StatsButtonPhoneReference;

	// Token: 0x0400042A RID: 1066
	public AsyncReference m_StatsPageReference;

	// Token: 0x0400042B RID: 1067
	public AsyncReference m_StatsPagePhoneReference;

	// Token: 0x0400042C RID: 1068
	public AsyncReference m_LobbyReference;

	// Token: 0x0400042D RID: 1069
	public AsyncReference m_LobbyPhoneReference;

	// Token: 0x0400042E RID: 1070
	private bool m_playButtonFinishedLoading;

	// Token: 0x0400042F RID: 1071
	private bool m_backButtonFinishedLoading;

	// Token: 0x04000430 RID: 1072
	private bool m_statsButtonFinishedLoading;

	// Token: 0x04000431 RID: 1073
	private WidgetTemplate m_OwningWidget;

	// Token: 0x04000432 RID: 1074
	private PlayButton m_playButton;

	// Token: 0x04000433 RID: 1075
	private UIBButton m_statsButton;

	// Token: 0x04000434 RID: 1076
	private Clickable m_statsButtonClickable;

	// Token: 0x04000435 RID: 1077
	private readonly PlatformDependentValue<bool> ShowLowMemoryWarning = new PlatformDependentValue<bool>(PlatformCategory.Memory)
	{
		LowMemory = true,
		MediumMemory = true,
		HighMemory = false
	};

	// Token: 0x04000436 RID: 1078
	private const int PAST_GAMES_TO_SHOW = 5;

	// Token: 0x04000437 RID: 1079
	private const int MINIONS_PER_BOARD = 7;

	// Token: 0x04000438 RID: 1080
	private const string OPEN_SHOP_EVENT = "OpenShop";

	// Token: 0x04000439 RID: 1081
	private static bool m_hasSeenLowMemoryWarningThisSession;
}
