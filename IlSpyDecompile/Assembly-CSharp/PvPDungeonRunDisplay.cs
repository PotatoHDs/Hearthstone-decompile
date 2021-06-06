using System;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.DungeonCrawl;
using Hearthstone.UI;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

public class PvPDungeonRunDisplay : MonoBehaviour
{
	public AsyncReference m_PlayButtonReference;

	public AsyncReference m_PlayButtonPhoneReference;

	public AsyncReference m_BackButtonReference;

	public AsyncReference m_BackButtonPhoneReference;

	public AsyncReference m_SeasonNameTextReference;

	public AsyncReference m_SeasonNameTextPhoneReference;

	public AsyncReference m_StickyContainerReference;

	public AsyncReference m_StickyContainerPhoneReference;

	public PlayButton m_playButton;

	public UIBButton m_backButton;

	public DuelsPopupManager m_duelsPopupManager;

	public Widget m_stickyContainer;

	public UberText m_seasonName;

	private bool m_playButtonFinishedLoading;

	private bool m_playButtonWasEnabled;

	private bool m_backButtonFinishedLoading;

	private bool m_dataModelLoaded;

	private bool m_stickyContainerFinishedLoading;

	private bool m_seasonNameTextFinishedLoading;

	private bool m_isContentRoll = true;

	private bool m_isStartingSession;

	private PVPDRLobbyDataModel m_dataModel;

	private DateTime m_seasonEndDate;

	private bool m_isSeasonActive;

	private static PvPDungeonRunDisplay m_instance;

	public bool IsFinishedLoading
	{
		get
		{
			if (m_playButtonFinishedLoading && m_backButtonFinishedLoading && m_dataModelLoaded)
			{
				return m_seasonNameTextFinishedLoading;
			}
			return false;
		}
	}

	public static PvPDungeonRunDisplay Get()
	{
		return m_instance;
	}

	private void Awake()
	{
		m_instance = this;
	}

	private void Start()
	{
		Navigation.Push(OnNavigateBack);
		m_BackButtonReference.RegisterReadyListener<UIBButton>(OnBackButtonReady);
		m_BackButtonPhoneReference.RegisterReadyListener<UIBButton>(OnBackButtonReady);
		m_PlayButtonReference.RegisterReadyListener<PlayButton>(OnPlayButtonReady);
		m_PlayButtonPhoneReference.RegisterReadyListener<PlayButton>(OnPlayButtonReady);
		m_StickyContainerReference.RegisterReadyListener<Widget>(OnLobbyStickiesReady);
		m_StickyContainerPhoneReference.RegisterReadyListener<Widget>(OnLobbyStickiesReady);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_SeasonNameTextPhoneReference.RegisterReadyListener<UberText>(OnSeasonNameTextReady);
		}
		else
		{
			m_SeasonNameTextReference.RegisterReadyListener<UberText>(OnSeasonNameTextReady);
		}
		m_seasonEndDate = DateTime.Now;
		InitializeLobbyData();
		Network.Get().RegisterNetHandler(PVPDRSessionInfoResponse.PacketID.ID, OnPVPDRSessionInfoResponse);
		Network.Get().SendPVPDRSessionInfoRequest();
		m_duelsPopupManager = PvPDungeonRunScene.Get().GetPopupManager();
		if (m_duelsPopupManager != null)
		{
			m_duelsPopupManager.AddOnNormalButtonPressedDelegate(delegate
			{
				StartSession(paidEntry: false);
			});
			m_duelsPopupManager.AddOnSuccessfulPurchaseDelegate(delegate
			{
				StartSession(paidEntry: true);
			});
		}
	}

	public void InitializeLobbyData()
	{
		m_dataModel = GetPVPDRLobbyDataModel();
		if (m_dataModel == null)
		{
			Log.Net.PrintError("Could not retrieve PVPDRLobby data model.");
			return;
		}
		NetCache netCache = NetCache.Get();
		if (netCache == null)
		{
			Log.Net.PrintError("Could not retrieve NetCache.");
			return;
		}
		NetCache.NetCachePVPDRStatsInfo netObject = netCache.GetNetObject<NetCache.NetCachePVPDRStatsInfo>();
		if (netObject != null)
		{
			m_dataModel.Rating = netObject.Rating;
			m_dataModel.PaidRating = netObject.PaidRating;
			m_dataModel.HighWatermark = netObject.HighWatermark;
		}
		else
		{
			Log.Net.PrintError("No PVPDR rating info in NetCache.");
		}
		m_dataModel.IsEarlyAccess = DuelsConfig.IsEarlyAccess();
		m_dataModel.IsFreeUnlocked = DuelsConfig.IsFreeUnlocked();
		m_dataModel.IsPaidUnlocked = DuelsConfig.IsPaidUnlocked();
		GameModeDisplay gameModeDisplay = GameModeDisplay.Get();
		if (gameModeDisplay != null)
		{
			gameModeDisplay.RegisterOnHideTrayListener(OnGameModeTrayHidden);
		}
		else
		{
			Log.Net.PrintError("GameModeDisplay not instantiated.");
		}
	}

	public void OnPVPDRSessionInfoResponse()
	{
		PVPDRSessionInfoResponse pVPDRSessionInfoResponse = Network.Get().GetPVPDRSessionInfoResponse();
		if (pVPDRSessionInfoResponse.HasSession)
		{
			m_dataModel.Wins = (int)pVPDRSessionInfoResponse.Session.Wins;
			m_dataModel.Losses = (int)pVPDRSessionInfoResponse.Session.Losses;
			m_dataModel.HasSession = pVPDRSessionInfoResponse.Session.HasSession;
			m_dataModel.IsSessionActive = pVPDRSessionInfoResponse.Session.IsActive;
			m_dataModel.IsPaidEntry = pVPDRSessionInfoResponse.Session.IsPaidEntry;
			if (m_dataModel.IsSessionActive)
			{
				m_dataModel.LastPlayedMode = ((!m_dataModel.IsPaidEntry) ? 1 : 2);
			}
		}
		m_isSeasonActive = pVPDRSessionInfoResponse.HasCurrentSeason;
		if (m_isSeasonActive)
		{
			int adventureIdForSeason = DuelsConfig.GetAdventureIdForSeason(pVPDRSessionInfoResponse.CurrentSeason.Season.GameContentSeason.SeasonId);
			int adventureIdForSeason2 = DuelsConfig.GetAdventureIdForSeason(pVPDRSessionInfoResponse.CurrentSeason.NextSeasonId);
			m_isContentRoll = adventureIdForSeason2 == 0 || adventureIdForSeason != adventureIdForSeason2;
			TimeUtils.ElapsedStringSet stringSet = new TimeUtils.ElapsedStringSet
			{
				m_seconds = (m_isContentRoll ? "GLUE_PVPDR_LABEL_SEASON_ENDING_SECONDS" : "GLUE_PVPDR_LABEL_RATING_RESET_SECONDS"),
				m_minutes = (m_isContentRoll ? "GLUE_PVPDR_LABEL_SEASON_ENDING_MINUTES" : "GLUE_PVPDR_LABEL_RATING_RESET_MINUTES"),
				m_hours = (m_isContentRoll ? "GLUE_PVPDR_LABEL_SEASON_ENDING_HOURS" : "GLUE_PVPDR_LABEL_RATING_RESET_HOURS"),
				m_yesterday = null,
				m_days = (m_isContentRoll ? "GLUE_PVPDR_LABEL_SEASON_ENDING_DAYS" : "GLUE_PVPDR_LABEL_RATING_RESET_DAYS"),
				m_weeks = (m_isContentRoll ? "GLUE_PVPDR_LABEL_SEASON_ENDING_WEEKS" : "GLUE_PVPDR_LABEL_RATING_RESET_WEEKS"),
				m_monthAgo = (m_isContentRoll ? "GLUE_PVPDR_LABEL_SEASON_ENDING_OVER_1_MONTH" : "GLUE_PVPDR_LABEL_RATING_RESET_OVER_1_MONTH")
			};
			long endSecondsFromNow = (long)pVPDRSessionInfoResponse.CurrentSeason.Season.GameContentSeason.EndSecondsFromNow;
			m_seasonEndDate = DateTime.Now.AddSeconds(endSecondsFromNow);
			m_dataModel.TimeRemainingString = TimeUtils.GetElapsedTimeString(endSecondsFromNow, stringSet, roundUp: true);
			m_dataModel.Season = pVPDRSessionInfoResponse.CurrentSeason.Season.GameContentSeason.SeasonId;
			if (m_playButton != null && !m_playButtonWasEnabled)
			{
				m_playButton.Enable();
				m_playButtonWasEnabled = true;
			}
			if (m_seasonName != null)
			{
				m_seasonName.Text = GameDbf.Adventure.GetRecord(adventureIdForSeason).Name;
			}
		}
		m_dataModelLoaded = true;
	}

	public static void OnGameModeTrayHidden()
	{
		if (m_instance != null)
		{
			m_instance.ShowNewUnlocksPopupIfNecessary();
			GameModeDisplay.Get().UnRegisterOnHideTrayListener(OnGameModeTrayHidden);
		}
	}

	public void EnableButtons(bool enabled = true)
	{
		if (!m_isStartingSession)
		{
			EnablePlayButton(enabled);
			EnableBackButton(enabled);
		}
	}

	public void EnablePlayButton(bool enabled, bool textEnabled = false)
	{
		if (m_playButton != null)
		{
			if (enabled)
			{
				m_playButton.Enable();
			}
			else
			{
				m_playButton.Disable(textEnabled);
			}
		}
	}

	public void EnableBackButton(bool enabled)
	{
		if (m_backButton != null)
		{
			m_backButton.SetEnabled(enabled);
			m_backButton.Flip(enabled);
		}
	}

	public void OnBackButtonReady(UIBButton button)
	{
		if (button == null)
		{
			Error.AddDevWarning("UI Error!", "BackButton could not be found! You will not be able to click 'Back'!");
			return;
		}
		m_backButton = button;
		m_backButton.AddEventListener(UIEventType.RELEASE, BackButtonRelease);
		m_backButtonFinishedLoading = true;
	}

	public void BackButtonRelease(UIEvent e)
	{
		EnablePlayButton(enabled: false);
		Navigation.GoBack();
	}

	public void OnPlayButtonReady(PlayButton button)
	{
		if (button == null)
		{
			Error.AddDevWarning("UI Error!", "PlayButton could not be found! You will not be able to click 'Play'!");
			return;
		}
		m_playButton = button;
		m_playButton.AddEventListener(UIEventType.RELEASE, PlayButtonRelease);
		if (m_isSeasonActive)
		{
			m_playButton.Enable();
			m_playButtonWasEnabled = true;
		}
		else
		{
			m_playButton.Disable(keepLabelTextVisible: true);
		}
		m_playButtonFinishedLoading = true;
	}

	public void PlayButtonRelease(UIEvent e)
	{
		EnableButtons(enabled: false);
		if (!m_dataModel.HasSession)
		{
			double totalSeconds = (m_seasonEndDate - DateTime.Now).TotalSeconds;
			NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
			if (m_isContentRoll && totalSeconds <= (double)netObject.PVPDRClosedToNewSessionsSeconds)
			{
				AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
				popupInfo.m_headerText = GameStrings.Get("GLUE_PVPDR");
				popupInfo.m_text = GameStrings.Get("GLUE_PVPDR_SIGNUPS_CLOSED");
				popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
				popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
				popupInfo.m_responseCallback = delegate
				{
					EnableButtons();
				};
				DialogManager.Get().ShowPopup(popupInfo);
			}
			else
			{
				PresenceMgr.Get().SetStatus(Global.PresenceStatus.DUELS_PURCHASE);
				m_duelsPopupManager.Show();
			}
		}
		else
		{
			TransitionToNextScreen();
		}
	}

	public void OnLobbyStickiesReady(Widget stickiesContainer)
	{
		if (stickiesContainer == null)
		{
			Error.AddDevWarning("UI Error!", "Stickies widget could not be found!");
			return;
		}
		m_stickyContainer = stickiesContainer;
		m_stickyContainerFinishedLoading = true;
	}

	public void OnSeasonNameTextReady(UberText uberText)
	{
		m_seasonName = uberText;
		m_seasonNameTextFinishedLoading = true;
	}

	public PVPDRLobbyDataModel GetPVPDRLobbyDataModel()
	{
		if (m_dataModel != null)
		{
			return m_dataModel;
		}
		Widget component = GetComponent<Widget>();
		if (!component.GetDataModel(181, out var model))
		{
			model = new PVPDRLobbyDataModel();
			component.BindDataModel(model);
		}
		return model as PVPDRLobbyDataModel;
	}

	public void OnHeroPickerShown()
	{
		m_isStartingSession = false;
	}

	private void StartSession(bool paidEntry)
	{
		Network.Get().RegisterNetHandler(PVPDRSessionStartResponse.PacketID.ID, OnSessionStartResponse);
		Network.Get().SendPVPDRSessionStartRequest(paidEntry);
		m_isStartingSession = true;
	}

	private void OnSessionStartResponse()
	{
		Network.Get().RemoveNetHandler(PVPDRSessionStartResponse.PacketID.ID, OnSessionStartResponse);
		if (Network.Get().GetPVPDRSessionStartResponse().ErrorCode == ErrorCode.ERROR_OK)
		{
			DuelsConfig.Get().SetRecentEnd(value: false);
			AdventureConfig.Get().ChangeSubScene(AdventureData.Adventuresubscene.CHOOSER);
			Network.Get().RegisterNetHandler(PVPDRSessionInfoResponse.PacketID.ID, OnAfterStartSessionInfoResponse);
			Network.Get().SendPVPDRSessionInfoRequest();
		}
		else
		{
			OnSessionStartRequestFailed();
		}
	}

	private void OnAfterStartSessionInfoResponse()
	{
		Network.Get().RemoveNetHandler(PVPDRSessionInfoResponse.PacketID.ID, OnAfterStartSessionInfoResponse);
		PVPDRSessionInfoResponse pVPDRSessionInfoResponse = Network.Get().GetPVPDRSessionInfoResponse();
		m_dataModel.IsPaidEntry = pVPDRSessionInfoResponse.Session.IsPaidEntry;
		GameSaveDataManager.Get().Request(PvPDungeonRunScene.Get().GetGSDKeyForAdventure(), CheckForTransition);
	}

	private void OnSessionStartRequestFailed()
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_PVPDR");
		popupInfo.m_text = GameStrings.Get("GLUE_PVPDR_SESSION_START_FAILED_BODY");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_responseCallback = delegate
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
		};
		DialogManager.Get().ShowPopup(popupInfo);
		m_isStartingSession = false;
	}

	private void TransitionToNextScreen()
	{
		bool flag = m_dataModel.HasSession && !m_dataModel.IsSessionActive;
		if (!DuelsConfig.IsInitialLoadoutComplete() && !flag)
		{
			if (!PvPDungeonRunScene.Get().TransitionToGuestHeroPicker())
			{
				EnableButtons();
			}
		}
		else
		{
			PvPDungeonRunScene.Get().TransitionToDungeonCrawlPlayMat();
		}
	}

	private void CheckForTransition(bool success)
	{
		if (success)
		{
			TransitionToNextScreen();
		}
	}

	private static bool OnNavigateBack()
	{
		if (m_instance != null)
		{
			m_instance.EnableButtons(enabled: false);
		}
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.GAME_MODE, SceneMgr.TransitionHandlerType.NEXT_SCENE);
		return true;
	}

	public void CheckForStatsChanged()
	{
		if (m_stickyContainerFinishedLoading && m_stickyContainer != null)
		{
			m_stickyContainer.TriggerEvent("STATS_CHANGED");
			DuelsConfig.Get().SetRecentEnd(value: false);
		}
	}

	private void ShowNewUnlocksPopupIfNecessary()
	{
		AdventureDbId advId = AdventureConfig.Get().GetSelectedAdventure();
		List<long> newHeroPowers;
		List<long> newTreasures;
		HashSet<int> unlocks = DungeonCrawlUtil.GetAchievementsForRecentUnlocks(advId, out newHeroPowers, out newTreasures);
		if (unlocks.Count > 0)
		{
			int num = newHeroPowers.Count + newTreasures.Count;
			Get().GetPVPDRLobbyDataModel().IsRatingNotice = false;
			PvPDungeonRunScene.ShowDuelsMessagePopup(GameStrings.Get("GLUE_DUELS_NEW_UNLOCKS_HEADER"), GameStrings.Format("GLUE_DUELS_NEW_UNLOCKS_BODY", num), "", delegate
			{
				DungeonCrawlUtil.MarkUnlocksAsNew(advId, AdventureModeDbId.DUNGEON_CRAWL, newHeroPowers, newTreasures);
				DungeonCrawlUtil.AcknowledgeUnlocks(unlocks);
			});
		}
	}
}
