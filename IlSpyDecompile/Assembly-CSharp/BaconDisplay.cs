using System.Collections.Generic;
using System.Linq;
using Assets;
using bgs;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

[RequireComponent(typeof(WidgetTemplate))]
public class BaconDisplay : MonoBehaviour
{
	public AsyncReference m_PlayButtonReference;

	public AsyncReference m_PlayButtonPhoneReference;

	public AsyncReference m_BackButtonReference;

	public AsyncReference m_BackButtonPhoneReference;

	public AsyncReference m_StatsButtonReference;

	public AsyncReference m_StatsButtonPhoneReference;

	public AsyncReference m_StatsPageReference;

	public AsyncReference m_StatsPagePhoneReference;

	public AsyncReference m_LobbyReference;

	public AsyncReference m_LobbyPhoneReference;

	private bool m_playButtonFinishedLoading;

	private bool m_backButtonFinishedLoading;

	private bool m_statsButtonFinishedLoading;

	private WidgetTemplate m_OwningWidget;

	private PlayButton m_playButton;

	private UIBButton m_statsButton;

	private Clickable m_statsButtonClickable;

	private readonly PlatformDependentValue<bool> ShowLowMemoryWarning = new PlatformDependentValue<bool>(PlatformCategory.Memory)
	{
		LowMemory = true,
		MediumMemory = true,
		HighMemory = false
	};

	private const int PAST_GAMES_TO_SHOW = 5;

	private const int MINIONS_PER_BOARD = 7;

	private const string OPEN_SHOP_EVENT = "OpenShop";

	private static bool m_hasSeenLowMemoryWarningThisSession;

	public bool IsFinishedLoading
	{
		get
		{
			if (m_playButtonFinishedLoading && m_backButtonFinishedLoading && m_statsButtonFinishedLoading)
			{
				return !m_OwningWidget.IsChangingStates;
			}
			return false;
		}
	}

	private void Awake()
	{
		RegisterListeners();
		m_OwningWidget = GetComponent<WidgetTemplate>();
	}

	private void Start()
	{
		m_PlayButtonReference.RegisterReadyListener<VisualController>(OnPlayButtonReady);
		m_BackButtonReference.RegisterReadyListener<VisualController>(OnBackButtonReady);
		m_PlayButtonPhoneReference.RegisterReadyListener<VisualController>(OnPlayButtonReady);
		m_BackButtonPhoneReference.RegisterReadyListener<VisualController>(OnBackButtonReady);
		m_StatsButtonReference.RegisterReadyListener<VisualController>(OnStatsButtonReady);
		m_StatsButtonPhoneReference.RegisterReadyListener<VisualController>(OnStatsButtonReady);
		m_StatsPageReference.RegisterReadyListener<VisualController>(OnStatsPagePCReady);
		m_StatsPagePhoneReference.RegisterReadyListener<VisualController>(OnStatsPagePhoneReady);
		m_LobbyReference.RegisterReadyListener<Widget>(OnLobbyPCReady);
		m_LobbyPhoneReference.RegisterReadyListener<Widget>(OnLobbyPhoneReady);
		NetCache.Get().RegisterScreenBattlegrounds(OnNetCacheReady);
		PartyManager.Get().AddChangedListener(OnPartyChanged);
		InitializeBaconLobbyData();
		NarrativeManager.Get().OnBattlegroundsEntered();
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_Battlegrounds);
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.BATTLEGROUNDS_SCREEN);
		ShowLowMemoryAlertMessage();
		ShowPerkUpsellMessage();
	}

	private void OnDestroy()
	{
		UnregisterListeners();
	}

	private void BaconDisplayEventListener(string eventName)
	{
		if (eventName == "OpenShop")
		{
			OpenBattlegroundsShop();
		}
	}

	public void OnPlayButtonReady(VisualController buttonVisualController)
	{
		if (buttonVisualController == null)
		{
			Error.AddDevWarning("UI Error!", "PlayButton could not be found! You will not be able to click 'Play'!");
			return;
		}
		m_playButton = buttonVisualController.gameObject.GetComponent<PlayButton>();
		m_playButton.AddEventListener(UIEventType.RELEASE, PlayButtonRelease);
		UpdatePlayButtonBasedOnPartyInfo();
		m_playButtonFinishedLoading = true;
	}

	public void OnBackButtonReady(VisualController buttonVisualController)
	{
		if (buttonVisualController == null)
		{
			Error.AddDevWarning("UI Error!", "BackButton could not be found! You will not be able to click 'Back'!");
			return;
		}
		buttonVisualController.gameObject.GetComponent<UIBButton>().AddEventListener(UIEventType.RELEASE, BackButtonRelease);
		m_backButtonFinishedLoading = true;
	}

	public void OnStatsButtonReady(VisualController buttonVisualController)
	{
		if (buttonVisualController == null)
		{
			Error.AddDevWarning("UI Error!", "StatsButton could not be found! You will not be able to show 'Stats'!");
			return;
		}
		m_statsButton = buttonVisualController.gameObject.GetComponent<UIBButton>();
		m_statsButtonClickable = buttonVisualController.gameObject.GetComponent<Clickable>();
		UpdateStatsButtonState();
		m_statsButtonFinishedLoading = true;
	}

	private void UpdateStatsButtonState()
	{
		if (!(m_statsButton == null) && !(m_statsButtonClickable == null))
		{
			bool flag = HasAccessToStatsPage();
			m_statsButton.Flip(flag, forceImmediate: true);
			m_statsButton.SetEnabled(flag);
			m_statsButtonClickable.Active = flag;
		}
	}

	public void PlayButtonRelease(UIEvent e)
	{
		if (BattleNet.IsConnected() && !GameMgr.Get().IsFindingGame())
		{
			PresenceMgr.Get().SetStatus(Global.PresenceStatus.BATTLEGROUNDS_QUEUE);
			bool flag = !NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.BattlegroundsTutorial;
			GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.BACON, GameSaveKeySubkeyId.BACON_HAS_SEEN_TUTORIAL, out long value);
			PartyManager partyManager = PartyManager.Get();
			if (partyManager.IsInParty() && partyManager.IsInBattlegroundsParty() && partyManager.IsPartyLeader())
			{
				partyManager.FindGame();
			}
			else if (value == 0L && !flag)
			{
				PlayBaconTutorial();
			}
			else
			{
				GameMgr.Get().FindGame(GameType.GT_BATTLEGROUNDS, FormatType.FT_WILD, 3459, 0, 0L);
			}
		}
	}

	public void PlayBaconTutorial()
	{
		GameMgr.Get().FindGame(GameType.GT_VS_AI, FormatType.FT_WILD, 3539, 0, 0L);
	}

	public void BackButtonRelease(UIEvent e)
	{
		if (PartyManager.Get().IsInBattlegroundsParty())
		{
			ShowLeavePartyDialog();
		}
		else
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.GAME_MODE, SceneMgr.TransitionHandlerType.NEXT_SCENE);
		}
	}

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
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			}
		};
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private void OnNetCacheReady()
	{
		NetCache.Get().UnregisterNetCacheHandler(OnNetCacheReady);
		if (!NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.Battlegrounds && !SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			Error.AddWarningLoc("GLOBAL_FEATURE_DISABLED_TITLE", "GLOBAL_FEATURE_DISABLED_MESSAGE_BATTLEGROUNDS");
		}
	}

	private void OnStatsPagePCReady(VisualController visualController)
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			if (visualController == null)
			{
				Error.AddDevWarning("UI Error!", "StatsPage could not be found! You will not be able to view stats!");
			}
			else
			{
				InitializeBaconStatsPageData(visualController);
			}
		}
	}

	private void OnStatsPagePhoneReady(VisualController visualController)
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			if (visualController == null)
			{
				Error.AddDevWarning("UI Error!", "StatsPage could not be found! You will not be able to view stats!");
			}
			else
			{
				InitializeBaconStatsPageData(visualController);
			}
		}
	}

	private void OnLobbyPCReady(Widget widget)
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			if (widget == null)
			{
				Error.AddDevWarning("UI Error!", "LobbyReference could not be found!");
			}
			else
			{
				widget.RegisterEventListener(BaconDisplayEventListener);
			}
		}
	}

	private void OnLobbyPhoneReady(Widget widget)
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			if (widget == null)
			{
				Error.AddDevWarning("UI Error!", "LobbyReference could not be found!");
			}
			else
			{
				widget.RegisterEventListener(BaconDisplayEventListener);
			}
		}
	}

	public BaconLobbyDataModel GetBaconLobbyDataModel()
	{
		VisualController component = GetComponent<VisualController>();
		if (component == null)
		{
			return null;
		}
		Widget owner = component.Owner;
		if (!owner.GetDataModel(43, out var model))
		{
			model = new BaconLobbyDataModel();
			owner.BindDataModel(model);
		}
		return model as BaconLobbyDataModel;
	}

	private void InitializeBaconLobbyData()
	{
		BaconLobbyDataModel baconLobbyDataModel = GetBaconLobbyDataModel();
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
			Log.Net.PrintError("No bacon rating info in NetCache.");
		}
		baconLobbyDataModel.Top4Finishes = (int)GetBaconGameSaveValue(GameSaveKeySubkeyId.BACON_TOP_4_FINISHES);
		baconLobbyDataModel.FirstPlaceFinishes = (int)GetBaconGameSaveValue(GameSaveKeySubkeyId.BACON_FIRST_PLACE_FINISHES);
		NetCache.NetCacheBaconPremiumStatus netObject2 = NetCache.Get().GetNetObject<NetCache.NetCacheBaconPremiumStatus>();
		if (netObject2 == null)
		{
			return;
		}
		int num = 0;
		BoosterDbId premiumPackType = BoosterDbId.INVALID;
		foreach (BattlegroundSeasonPremiumStatus item in netObject2.SeasonPremiumStatus)
		{
			if (item.NumPacksOpened > num)
			{
				num = (int)item.NumPacksOpened;
				premiumPackType = (BoosterDbId)item.PackType;
			}
		}
		baconLobbyDataModel.PremiumPackOwnedCount = num;
		baconLobbyDataModel.PremiumPackType = premiumPackType;
		baconLobbyDataModel.BonusesLicenseOwned = OwnsBattlegroundsBonusesLicenseThisSeason();
	}

	private void RegisterListeners()
	{
		GameMgr.Get().RegisterFindGameEvent(OnFindGameEvent);
		Network.Get().RegisterNetHandler(BattlegroundsPremiumStatusResponse.PacketID.ID, OnBaconPremiumStatus);
		AccountLicenseMgr.Get().RegisterAccountLicensesChangedListener(OnAccountLicensesChanged);
		GameMgr.Get().OnTransitionPopupShown += OnTransitionPopupShown;
		BnetPresenceMgr.Get().AddPlayersChangedListener(OnPresenceUpdated);
		BnetNearbyPlayerMgr.Get().AddChangeListener(OnNearbyPlayersUpdated);
	}

	private void UnregisterListeners()
	{
		if (GameMgr.Get() != null)
		{
			GameMgr.Get().UnregisterFindGameEvent(OnFindGameEvent);
			GameMgr.Get().OnTransitionPopupShown -= OnTransitionPopupShown;
		}
		if (Network.Get() != null)
		{
			Network.Get().RemoveNetHandler(BattlegroundsPremiumStatusResponse.PacketID.ID, OnBaconPremiumStatus);
		}
		if (AccountLicenseMgr.Get() != null)
		{
			AccountLicenseMgr.Get().RemoveAccountLicensesChangedListener(OnAccountLicensesChanged);
		}
		if (PartyManager.Get() != null)
		{
			PartyManager.Get().RemoveChangedListener(OnPartyChanged);
		}
		if (BnetPresenceMgr.Get() != null)
		{
			BnetPresenceMgr.Get().RemovePlayersChangedListener(OnPresenceUpdated);
		}
		if (BnetNearbyPlayerMgr.Get() != null)
		{
			BnetNearbyPlayerMgr.Get().RemoveChangeListener(OnNearbyPlayersUpdated);
		}
	}

	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		FindGameState state = eventData.m_state;
		if ((uint)(state - 2) <= 1u || (uint)(state - 7) <= 1u || state == FindGameState.SERVER_GAME_CANCELED)
		{
			PresenceMgr.Get().SetStatus(Global.PresenceStatus.BATTLEGROUNDS_SCREEN);
		}
		return false;
	}

	private void OnTransitionPopupShown()
	{
		Shop.Get().Close();
		DialogManager.Get().ClearAllImmediately();
	}

	private void ShowLowMemoryAlertMessage()
	{
		if ((bool)ShowLowMemoryWarning && !m_hasSeenLowMemoryWarningThisSession)
		{
			m_hasSeenLowMemoryWarningThisSession = true;
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_BACON_LOW_MEMORY_HEADER"),
				m_text = GameStrings.Get("GLUE_BACON_LOW_MEMORY_BODY"),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info);
		}
	}

	private void ShowPerkUpsellMessage()
	{
		if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY && GameMgr.Get().LastGameData.BattlegroundsLeaderboardPlace <= 4 && !OwnsBattlegroundsBonusesLicenseThisSeason())
		{
			NotificationManager.Get().CreateCharacterQuote("Bob_Banner_Quote.prefab:723ff2809fb544344a9dd587aafbc1aa", GameStrings.Get("GLUE_BACON_VICTORY_BONUS_MESSAGE"), "VO_DALA_BOSS_99h_Male_Human_CombatWin_03.prefab:8ff8566f08747ad4bb76409e6db1504b", allowRepeatDuringSession: false);
		}
	}

	public BaconStatsPageDataModel GetBaconStatsPageDataModel(VisualController visualController)
	{
		if (visualController == null)
		{
			return null;
		}
		Widget owner = visualController.Owner;
		if (!owner.GetDataModel(122, out var model))
		{
			model = new BaconStatsPageDataModel();
			owner.BindDataModel(model);
		}
		return model as BaconStatsPageDataModel;
	}

	private void InitializeBaconStatsPageData(VisualController visualController)
	{
		BaconStatsPageDataModel dataModel = GetBaconStatsPageDataModel(visualController);
		if (dataModel == null)
		{
			return;
		}
		dataModel.Top4Finishes = (int)GetBaconGameSaveValue(GameSaveKeySubkeyId.BACON_TOP_4_FINISHES);
		dataModel.FirstPlaceFinishes = (int)GetBaconGameSaveValue(GameSaveKeySubkeyId.BACON_FIRST_PLACE_FINISHES);
		dataModel.TriplesCreated = (int)GetBaconGameSaveValue(GameSaveKeySubkeyId.BACON_TRIPLES_CREATED);
		dataModel.TavernUpgrades = (int)GetBaconGameSaveValue(GameSaveKeySubkeyId.BACON_TAVERN_UPGRADES);
		dataModel.DamageInOneTurn = (int)GetBaconGameSaveValue(GameSaveKeySubkeyId.BACON_MOST_DAMAGE_ONE_TURN);
		dataModel.LongestWinStreak = (int)GetBaconGameSaveValue(GameSaveKeySubkeyId.BACON_LONGEST_COMBAT_WIN_STREAK);
		dataModel.SecondsPlayed = (int)GetBaconGameSaveValue(GameSaveKeySubkeyId.BACON_TIME_PLAYED);
		List<long> baconGameSaveValueList = GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_MINIONS_KILLED_COUNT);
		dataModel.MinionsDestroyed = (int)((baconGameSaveValueList != null) ? baconGameSaveValueList.Sum() : 0);
		List<long> baconGameSaveValueList2 = GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_HEROES_KILLED_COUNT);
		dataModel.PlayersEliminated = (int)((baconGameSaveValueList2 != null) ? baconGameSaveValueList2.Sum() : 0);
		List<long> baconGameSaveValueList3 = GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_LARGEST_MINION_ATTACK_HEALTH);
		dataModel.BiggestMinionId = ((baconGameSaveValueList3 == null || baconGameSaveValueList3.Count() < 3) ? null : new CardDataModel
		{
			CardId = GameUtils.TranslateDbIdToCardId((int)baconGameSaveValueList3[0]),
			Premium = TAG_PREMIUM.NORMAL
		});
		dataModel.BiggestMinionAttack = (int)((baconGameSaveValueList3 != null && baconGameSaveValueList3.Count() >= 3) ? baconGameSaveValueList3[1] : 0);
		dataModel.BiggestMinionHealth = (int)((baconGameSaveValueList3 != null && baconGameSaveValueList3.Count() >= 3) ? baconGameSaveValueList3[2] : 0);
		dataModel.BiggestMinionString = GameStrings.Format("GLUE_BACON_STATS_VALUE_BIGGEST_MINION", dataModel.BiggestMinionAttack, dataModel.BiggestMinionHealth);
		if (dataModel.SecondsPlayed > 3600)
		{
			dataModel.TimePlayedString = GameStrings.Format("GLUE_BACON_STATS_VALUE_HOURS_PLAYED", Mathf.FloorToInt(dataModel.SecondsPlayed / 3600));
		}
		else
		{
			dataModel.TimePlayedString = GameStrings.Format("GLUE_BACON_STATS_VALUE_MINUTES_PLAYED", Mathf.FloorToInt(dataModel.SecondsPlayed / 60));
		}
		List<KeyValuePair<long, long>> sortedListFromGameSaveDataLists = GetSortedListFromGameSaveDataLists(GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_BOUGHT_MINIONS), GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_BOUGHT_MINIONS_COUNT));
		dataModel.MostBoughtMinionsCardIds = new DataModelList<CardDataModel>();
		dataModel.MostBoughtMinionsCardIds.AddRange(sortedListFromGameSaveDataLists.Select((KeyValuePair<long, long> kvp) => new CardDataModel
		{
			CardId = GameUtils.TranslateDbIdToCardId((int)kvp.Key),
			Premium = TAG_PREMIUM.NORMAL
		}));
		dataModel.MostBoughtMinionsCount = new DataModelList<int>();
		dataModel.MostBoughtMinionsCount.AddRange(sortedListFromGameSaveDataLists.Select((KeyValuePair<long, long> kvp) => (int)kvp.Value));
		List<KeyValuePair<long, long>> sortedListFromGameSaveDataLists2 = GetSortedListFromGameSaveDataLists(GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_HEROES_WON_WITH), GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_HEROES_WON_WITH_COUNT));
		dataModel.TopHeroesByWinCardIds = new DataModelList<CardDataModel>();
		dataModel.TopHeroesByWinCardIds.AddRange(sortedListFromGameSaveDataLists2.Select((KeyValuePair<long, long> kvp) => new CardDataModel
		{
			CardId = GameUtils.TranslateDbIdToCardId((int)kvp.Key),
			Premium = TAG_PREMIUM.NORMAL
		}));
		dataModel.TopHeroesByWinCount = new DataModelList<int>();
		dataModel.TopHeroesByWinCount.AddRange(sortedListFromGameSaveDataLists2.Select((KeyValuePair<long, long> kvp) => (int)kvp.Value));
		List<KeyValuePair<long, long>> sortedListFromGameSaveDataLists3 = GetSortedListFromGameSaveDataLists(GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_HEROES_PICKED), GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_HEROES_PICKED_COUNT));
		dataModel.TopHeroesByGamesPlayedCardIds = new DataModelList<CardDataModel>();
		dataModel.TopHeroesByGamesPlayedCardIds.AddRange(sortedListFromGameSaveDataLists3.Select((KeyValuePair<long, long> kvp) => new CardDataModel
		{
			CardId = GameUtils.TranslateDbIdToCardId((int)kvp.Key),
			Premium = TAG_PREMIUM.NORMAL
		}));
		dataModel.TopHeroesByGamesPlayedCount = new DataModelList<int>();
		dataModel.TopHeroesByGamesPlayedCount.AddRange(sortedListFromGameSaveDataLists3.Select((KeyValuePair<long, long> kvp) => (int)kvp.Value));
		dataModel.PastGames = new DataModelList<BaconPastGameStatsDataModel>();
		List<long> baconGameSaveValueList4 = GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_HEROES);
		List<long> baconGameSaveValueList5 = GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_PLACES);
		List<long> baconGameSaveValueList6 = GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_MINIONS_ID);
		List<long> baconGameSaveValueList7 = GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_MINIONS_ATTACK);
		List<long> baconGameSaveValueList8 = GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_MINIONS_HEALTH);
		List<long> baconGameSaveValueList9 = GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_MINIONS_GOLDEN);
		List<long> tauntList = new List<long>();
		List<long> divineShieldList = new List<long>();
		List<long> poisonousList = new List<long>();
		List<long> windfuryList = new List<long>();
		PopulateSpellLists(baconGameSaveValueList4?.Count ?? 0, ref tauntList, ref divineShieldList, ref poisonousList, ref windfuryList);
		List<BaconPastGameStatsDataModel> list = new List<BaconPastGameStatsDataModel>();
		for (int i = 0; i < 5; i++)
		{
			if (baconGameSaveValueList4 == null)
			{
				break;
			}
			if (i >= baconGameSaveValueList4.Count)
			{
				break;
			}
			if (i >= baconGameSaveValueList5.Count)
			{
				break;
			}
			string cardId = GameUtils.TranslateDbIdToCardId((int)baconGameSaveValueList4[i]);
			CardDataModel hero = new CardDataModel
			{
				CardId = cardId,
				Premium = TAG_PREMIUM.NORMAL
			};
			CardDbfRecord cardRecord = GameUtils.GetCardRecord(cardId);
			string heroName = ((cardRecord == null) ? "" : ((string)cardRecord.Name));
			CardDataModel heroPower = new CardDataModel
			{
				CardId = GameUtils.GetHeroPowerCardIdFromHero((int)baconGameSaveValueList4[i]),
				Premium = TAG_PREMIUM.NORMAL,
				SpellTypes = new DataModelList<SpellType> { SpellType.COIN_MANA_GEM }
			};
			DataModelList<CardDataModel> dataModelList = new DataModelList<CardDataModel>();
			for (int j = 0; j < 7; j++)
			{
				int num = i * 7 + j;
				if (baconGameSaveValueList6.Count <= num || baconGameSaveValueList7.Count <= num || baconGameSaveValueList8.Count <= num || baconGameSaveValueList9.Count <= num || tauntList.Count <= num || divineShieldList.Count <= num || poisonousList.Count <= num || windfuryList.Count <= num)
				{
					Debug.LogErrorFormat("Missing Minion Data for GameIndex={0}, MinionIndex={1}", i, num);
					break;
				}
				if (baconGameSaveValueList6[num] == 0L)
				{
					break;
				}
				DataModelList<SpellType> dataModelList2 = new DataModelList<SpellType>();
				bool flag = baconGameSaveValueList9[num] > 0;
				if (tauntList[num] > 0)
				{
					dataModelList2.Add(flag ? SpellType.TAUNT_INSTANT_PREMIUM : SpellType.TAUNT_INSTANT);
				}
				if (divineShieldList[num] > 0)
				{
					dataModelList2.Add(SpellType.DIVINE_SHIELD);
				}
				if (poisonousList[num] > 0)
				{
					dataModelList2.Add(SpellType.POISONOUS);
				}
				if (windfuryList[num] > 0)
				{
					dataModelList2.Add(SpellType.WINDFURY_IDLE);
				}
				dataModelList.Add(new CardDataModel
				{
					CardId = GameUtils.TranslateDbIdToCardId((int)baconGameSaveValueList6[num]),
					Premium = (flag ? TAG_PREMIUM.GOLDEN : TAG_PREMIUM.NORMAL),
					Attack = (int)baconGameSaveValueList7[num],
					Health = (int)baconGameSaveValueList8[num],
					SpellTypes = dataModelList2
				});
			}
			list.Add(new BaconPastGameStatsDataModel
			{
				Hero = hero,
				HeroPower = heroPower,
				HeroName = heroName,
				Place = (int)baconGameSaveValueList5[i],
				Minions = dataModelList
			});
		}
		list.Reverse();
		list.ForEach(delegate(BaconPastGameStatsDataModel g)
		{
			dataModel.PastGames.Add(g);
		});
	}

	private void PopulateSpellLists(int pastGames, ref List<long> tauntList, ref List<long> divineShieldList, ref List<long> poisonousList, ref List<long> windfuryList)
	{
		tauntList = GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_MINIONS_TAUNT);
		divineShieldList = GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_MINIONS_DIVINE_SHIELD);
		poisonousList = GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_MINIONS_POISONOUS);
		windfuryList = GetBaconGameSaveValueList(GameSaveKeySubkeyId.BACON_PAST_GAME_MINIONS_WINDFURY);
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

	private long GetBaconGameSaveValue(GameSaveKeySubkeyId subkey)
	{
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.BACON, subkey, out long value);
		return value;
	}

	private List<long> GetBaconGameSaveValueList(GameSaveKeySubkeyId subkey)
	{
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.BACON, subkey, out List<long> values);
		return values;
	}

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
		return list.OrderByDescending((KeyValuePair<long, long> kvp) => kvp.Value).ToList();
	}

	private bool OwnsBattlegroundsBonusesLicenseThisSeason()
	{
		foreach (BattlegroundsSeasonDbfRecord record in GameDbf.BattlegroundsSeason.GetRecords((BattlegroundsSeasonDbfRecord r) => SpecialEventManager.Get().IsEventActive(r.Event, activeIfDoesNotExist: true)))
		{
			if (record.AccountLicenseRecord != null && AccountLicenseMgr.Get().OwnsAccountLicense(record.AccountLicenseRecord.LicenseId))
			{
				return true;
			}
		}
		return false;
	}

	private bool HasAccessToStatsPage()
	{
		NetCache.NetCacheBaconPremiumStatus netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBaconPremiumStatus>();
		if (netObject == null)
		{
			return false;
		}
		foreach (BattlegroundSeasonPremiumStatus item in netObject.SeasonPremiumStatus)
		{
			if (item.PremiumRewardUnlocked.Contains(BattlegroundSeasonRewardType.BG_REWARD_GAME_STATS))
			{
				return true;
			}
		}
		return false;
	}

	private void OpenBattlegroundsShop()
	{
		if (StoreManager.Get().TryGetBonusProductBundleId(ProductType.PRODUCT_TYPE_BATTLEGROUNDS_BONUS, out var pmtId))
		{
			Shop.OpenToProductPageWhenReady(pmtId, suppressBox: true);
		}
		else
		{
			ShowBattlegroundsBonusErrorPopup();
		}
	}

	private void OnAccountLicensesChanged(List<AccountLicenseInfo> changedLicensesInfo, object userData)
	{
		BaconLobbyDataModel baconLobbyDataModel = GetBaconLobbyDataModel();
		if (baconLobbyDataModel != null)
		{
			baconLobbyDataModel.BonusesLicenseOwned = OwnsBattlegroundsBonusesLicenseThisSeason();
		}
		Network.Get().RequestBattlegroundsPremiumStatus();
	}

	private void OnBaconPremiumStatus()
	{
		UpdateStatsButtonState();
	}

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

	private void OnPartyChanged(PartyManager.PartyInviteEvent inviteEvent, BnetGameAccountId playerGameAccountId, PartyManager.PartyData data, object userData)
	{
		UpdatePlayButtonBasedOnPartyInfo();
	}

	private void OnPresenceUpdated(BnetPlayerChangelist changelist, object userData)
	{
		UpdatePlayButtonBasedOnPartyInfo();
	}

	private void OnNearbyPlayersUpdated(BnetNearbyPlayerChangelist changelist, object userData)
	{
		UpdatePlayButtonBasedOnPartyInfo();
	}

	private void UpdatePlayButtonBasedOnPartyInfo()
	{
		if (!(m_playButton == null))
		{
			string text = ((!PartyManager.Get().IsInBattlegroundsParty() || PartyManager.Get().IsPartyLeader()) ? GameStrings.Get("GLOBAL_PLAY") : GameStrings.Get("GLOBAL_PLAY_WAITING"));
			m_playButton.SetText(text);
			int readyPartyMemberCount = PartyManager.Get().GetReadyPartyMemberCount();
			int currentPartySize = PartyManager.Get().GetCurrentPartySize();
			string secondaryText = "";
			if (PartyManager.Get().IsInBattlegroundsParty() && PartyManager.Get().IsPartyLeader() && !GameMgr.Get().IsFindingGame() && readyPartyMemberCount < currentPartySize)
			{
				secondaryText = $"{readyPartyMemberCount}/{currentPartySize}";
			}
			m_playButton.SetSecondaryText(secondaryText);
			if (PartyManager.Get().IsInBattlegroundsParty() && (!PartyManager.Get().IsPartyLeader() || readyPartyMemberCount < currentPartySize))
			{
				m_playButton.Disable(keepLabelTextVisible: true);
			}
			else
			{
				m_playButton.Enable();
			}
		}
	}
}
