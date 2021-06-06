using System;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.DungeonCrawl;
using Hearthstone.UI;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x0200062C RID: 1580
public class PvPDungeonRunDisplay : MonoBehaviour
{
	// Token: 0x060058A1 RID: 22689 RVA: 0x001CE9E7 File Offset: 0x001CCBE7
	public static PvPDungeonRunDisplay Get()
	{
		return PvPDungeonRunDisplay.m_instance;
	}

	// Token: 0x1700052B RID: 1323
	// (get) Token: 0x060058A2 RID: 22690 RVA: 0x001CE9EE File Offset: 0x001CCBEE
	public bool IsFinishedLoading
	{
		get
		{
			return this.m_playButtonFinishedLoading && this.m_backButtonFinishedLoading && this.m_dataModelLoaded && this.m_seasonNameTextFinishedLoading;
		}
	}

	// Token: 0x060058A3 RID: 22691 RVA: 0x001CEA10 File Offset: 0x001CCC10
	private void Awake()
	{
		PvPDungeonRunDisplay.m_instance = this;
	}

	// Token: 0x060058A4 RID: 22692 RVA: 0x001CEA18 File Offset: 0x001CCC18
	private void Start()
	{
		Navigation.Push(new Navigation.NavigateBackHandler(PvPDungeonRunDisplay.OnNavigateBack));
		this.m_BackButtonReference.RegisterReadyListener<UIBButton>(new Action<UIBButton>(this.OnBackButtonReady));
		this.m_BackButtonPhoneReference.RegisterReadyListener<UIBButton>(new Action<UIBButton>(this.OnBackButtonReady));
		this.m_PlayButtonReference.RegisterReadyListener<PlayButton>(new Action<PlayButton>(this.OnPlayButtonReady));
		this.m_PlayButtonPhoneReference.RegisterReadyListener<PlayButton>(new Action<PlayButton>(this.OnPlayButtonReady));
		this.m_StickyContainerReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnLobbyStickiesReady));
		this.m_StickyContainerPhoneReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnLobbyStickiesReady));
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_SeasonNameTextPhoneReference.RegisterReadyListener<UberText>(new Action<UberText>(this.OnSeasonNameTextReady));
		}
		else
		{
			this.m_SeasonNameTextReference.RegisterReadyListener<UberText>(new Action<UberText>(this.OnSeasonNameTextReady));
		}
		this.m_seasonEndDate = DateTime.Now;
		this.InitializeLobbyData();
		Network.Get().RegisterNetHandler(PVPDRSessionInfoResponse.PacketID.ID, new Network.NetHandler(this.OnPVPDRSessionInfoResponse), null);
		Network.Get().SendPVPDRSessionInfoRequest();
		this.m_duelsPopupManager = PvPDungeonRunScene.Get().GetPopupManager();
		if (this.m_duelsPopupManager != null)
		{
			this.m_duelsPopupManager.AddOnNormalButtonPressedDelegate(delegate
			{
				this.StartSession(false);
			});
			this.m_duelsPopupManager.AddOnSuccessfulPurchaseDelegate(delegate
			{
				this.StartSession(true);
			});
		}
	}

	// Token: 0x060058A5 RID: 22693 RVA: 0x001CEB88 File Offset: 0x001CCD88
	public void InitializeLobbyData()
	{
		this.m_dataModel = this.GetPVPDRLobbyDataModel();
		if (this.m_dataModel == null)
		{
			Log.Net.PrintError("Could not retrieve PVPDRLobby data model.", Array.Empty<object>());
			return;
		}
		NetCache netCache = NetCache.Get();
		if (netCache == null)
		{
			Log.Net.PrintError("Could not retrieve NetCache.", Array.Empty<object>());
			return;
		}
		NetCache.NetCachePVPDRStatsInfo netObject = netCache.GetNetObject<NetCache.NetCachePVPDRStatsInfo>();
		if (netObject != null)
		{
			this.m_dataModel.Rating = netObject.Rating;
			this.m_dataModel.PaidRating = netObject.PaidRating;
			this.m_dataModel.HighWatermark = netObject.HighWatermark;
		}
		else
		{
			Log.Net.PrintError("No PVPDR rating info in NetCache.", Array.Empty<object>());
		}
		this.m_dataModel.IsEarlyAccess = DuelsConfig.IsEarlyAccess();
		this.m_dataModel.IsFreeUnlocked = DuelsConfig.IsFreeUnlocked();
		this.m_dataModel.IsPaidUnlocked = DuelsConfig.IsPaidUnlocked();
		GameModeDisplay gameModeDisplay = GameModeDisplay.Get();
		if (gameModeDisplay != null)
		{
			gameModeDisplay.RegisterOnHideTrayListener(new Action(PvPDungeonRunDisplay.OnGameModeTrayHidden));
			return;
		}
		Log.Net.PrintError("GameModeDisplay not instantiated.", Array.Empty<object>());
	}

	// Token: 0x060058A6 RID: 22694 RVA: 0x001CEC98 File Offset: 0x001CCE98
	public void OnPVPDRSessionInfoResponse()
	{
		PVPDRSessionInfoResponse pvpdrsessionInfoResponse = Network.Get().GetPVPDRSessionInfoResponse();
		if (pvpdrsessionInfoResponse.HasSession)
		{
			this.m_dataModel.Wins = (int)pvpdrsessionInfoResponse.Session.Wins;
			this.m_dataModel.Losses = (int)pvpdrsessionInfoResponse.Session.Losses;
			this.m_dataModel.HasSession = pvpdrsessionInfoResponse.Session.HasSession;
			this.m_dataModel.IsSessionActive = pvpdrsessionInfoResponse.Session.IsActive;
			this.m_dataModel.IsPaidEntry = pvpdrsessionInfoResponse.Session.IsPaidEntry;
			if (this.m_dataModel.IsSessionActive)
			{
				this.m_dataModel.LastPlayedMode = (this.m_dataModel.IsPaidEntry ? 2 : 1);
			}
		}
		this.m_isSeasonActive = pvpdrsessionInfoResponse.HasCurrentSeason;
		if (this.m_isSeasonActive)
		{
			int adventureIdForSeason = DuelsConfig.GetAdventureIdForSeason(pvpdrsessionInfoResponse.CurrentSeason.Season.GameContentSeason.SeasonId);
			int adventureIdForSeason2 = DuelsConfig.GetAdventureIdForSeason(pvpdrsessionInfoResponse.CurrentSeason.NextSeasonId);
			this.m_isContentRoll = (adventureIdForSeason2 == 0 || adventureIdForSeason != adventureIdForSeason2);
			TimeUtils.ElapsedStringSet stringSet = new TimeUtils.ElapsedStringSet
			{
				m_seconds = (this.m_isContentRoll ? "GLUE_PVPDR_LABEL_SEASON_ENDING_SECONDS" : "GLUE_PVPDR_LABEL_RATING_RESET_SECONDS"),
				m_minutes = (this.m_isContentRoll ? "GLUE_PVPDR_LABEL_SEASON_ENDING_MINUTES" : "GLUE_PVPDR_LABEL_RATING_RESET_MINUTES"),
				m_hours = (this.m_isContentRoll ? "GLUE_PVPDR_LABEL_SEASON_ENDING_HOURS" : "GLUE_PVPDR_LABEL_RATING_RESET_HOURS"),
				m_yesterday = null,
				m_days = (this.m_isContentRoll ? "GLUE_PVPDR_LABEL_SEASON_ENDING_DAYS" : "GLUE_PVPDR_LABEL_RATING_RESET_DAYS"),
				m_weeks = (this.m_isContentRoll ? "GLUE_PVPDR_LABEL_SEASON_ENDING_WEEKS" : "GLUE_PVPDR_LABEL_RATING_RESET_WEEKS"),
				m_monthAgo = (this.m_isContentRoll ? "GLUE_PVPDR_LABEL_SEASON_ENDING_OVER_1_MONTH" : "GLUE_PVPDR_LABEL_RATING_RESET_OVER_1_MONTH")
			};
			long endSecondsFromNow = (long)pvpdrsessionInfoResponse.CurrentSeason.Season.GameContentSeason.EndSecondsFromNow;
			this.m_seasonEndDate = DateTime.Now.AddSeconds((double)endSecondsFromNow);
			this.m_dataModel.TimeRemainingString = TimeUtils.GetElapsedTimeString(endSecondsFromNow, stringSet, true);
			this.m_dataModel.Season = pvpdrsessionInfoResponse.CurrentSeason.Season.GameContentSeason.SeasonId;
			if (this.m_playButton != null && !this.m_playButtonWasEnabled)
			{
				this.m_playButton.Enable();
				this.m_playButtonWasEnabled = true;
			}
			if (this.m_seasonName != null)
			{
				this.m_seasonName.Text = GameDbf.Adventure.GetRecord(adventureIdForSeason).Name;
			}
		}
		this.m_dataModelLoaded = true;
	}

	// Token: 0x060058A7 RID: 22695 RVA: 0x001CEF10 File Offset: 0x001CD110
	public static void OnGameModeTrayHidden()
	{
		if (PvPDungeonRunDisplay.m_instance != null)
		{
			PvPDungeonRunDisplay.m_instance.ShowNewUnlocksPopupIfNecessary();
			GameModeDisplay.Get().UnRegisterOnHideTrayListener(new Action(PvPDungeonRunDisplay.OnGameModeTrayHidden));
		}
	}

	// Token: 0x060058A8 RID: 22696 RVA: 0x001CEF3F File Offset: 0x001CD13F
	public void EnableButtons(bool enabled = true)
	{
		if (!this.m_isStartingSession)
		{
			this.EnablePlayButton(enabled, false);
			this.EnableBackButton(enabled);
		}
	}

	// Token: 0x060058A9 RID: 22697 RVA: 0x001CEF58 File Offset: 0x001CD158
	public void EnablePlayButton(bool enabled, bool textEnabled = false)
	{
		if (this.m_playButton != null)
		{
			if (enabled)
			{
				this.m_playButton.Enable();
				return;
			}
			this.m_playButton.Disable(textEnabled);
		}
	}

	// Token: 0x060058AA RID: 22698 RVA: 0x001CEF83 File Offset: 0x001CD183
	public void EnableBackButton(bool enabled)
	{
		if (this.m_backButton != null)
		{
			this.m_backButton.SetEnabled(enabled, false);
			this.m_backButton.Flip(enabled, false);
		}
	}

	// Token: 0x060058AB RID: 22699 RVA: 0x001CEFB0 File Offset: 0x001CD1B0
	public void OnBackButtonReady(UIBButton button)
	{
		if (button == null)
		{
			Error.AddDevWarning("UI Error!", "BackButton could not be found! You will not be able to click 'Back'!", Array.Empty<object>());
			return;
		}
		this.m_backButton = button;
		this.m_backButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.BackButtonRelease));
		this.m_backButtonFinishedLoading = true;
	}

	// Token: 0x060058AC RID: 22700 RVA: 0x001CF002 File Offset: 0x001CD202
	public void BackButtonRelease(UIEvent e)
	{
		this.EnablePlayButton(false, false);
		Navigation.GoBack();
	}

	// Token: 0x060058AD RID: 22701 RVA: 0x001CF014 File Offset: 0x001CD214
	public void OnPlayButtonReady(PlayButton button)
	{
		if (button == null)
		{
			Error.AddDevWarning("UI Error!", "PlayButton could not be found! You will not be able to click 'Play'!", Array.Empty<object>());
			return;
		}
		this.m_playButton = button;
		this.m_playButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.PlayButtonRelease));
		if (this.m_isSeasonActive)
		{
			this.m_playButton.Enable();
			this.m_playButtonWasEnabled = true;
		}
		else
		{
			this.m_playButton.Disable(true);
		}
		this.m_playButtonFinishedLoading = true;
	}

	// Token: 0x060058AE RID: 22702 RVA: 0x001CF090 File Offset: 0x001CD290
	public void PlayButtonRelease(UIEvent e)
	{
		this.EnableButtons(false);
		if (this.m_dataModel.HasSession)
		{
			this.TransitionToNextScreen();
			return;
		}
		double totalSeconds = (this.m_seasonEndDate - DateTime.Now).TotalSeconds;
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		if (this.m_isContentRoll && totalSeconds <= netObject.PVPDRClosedToNewSessionsSeconds)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_PVPDR");
			popupInfo.m_text = GameStrings.Get("GLUE_PVPDR_SIGNUPS_CLOSED");
			popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				this.EnableButtons(true);
			};
			DialogManager.Get().ShowPopup(popupInfo);
			return;
		}
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.DUELS_PURCHASE
		});
		this.m_duelsPopupManager.Show();
	}

	// Token: 0x060058AF RID: 22703 RVA: 0x001CF16A File Offset: 0x001CD36A
	public void OnLobbyStickiesReady(Widget stickiesContainer)
	{
		if (stickiesContainer == null)
		{
			Error.AddDevWarning("UI Error!", "Stickies widget could not be found!", Array.Empty<object>());
			return;
		}
		this.m_stickyContainer = stickiesContainer;
		this.m_stickyContainerFinishedLoading = true;
	}

	// Token: 0x060058B0 RID: 22704 RVA: 0x001CF198 File Offset: 0x001CD398
	public void OnSeasonNameTextReady(UberText uberText)
	{
		this.m_seasonName = uberText;
		this.m_seasonNameTextFinishedLoading = true;
	}

	// Token: 0x060058B1 RID: 22705 RVA: 0x001CF1A8 File Offset: 0x001CD3A8
	public PVPDRLobbyDataModel GetPVPDRLobbyDataModel()
	{
		if (this.m_dataModel != null)
		{
			return this.m_dataModel;
		}
		Widget component = base.GetComponent<Widget>();
		IDataModel dataModel;
		if (!component.GetDataModel(181, out dataModel))
		{
			dataModel = new PVPDRLobbyDataModel();
			component.BindDataModel(dataModel, false);
		}
		return dataModel as PVPDRLobbyDataModel;
	}

	// Token: 0x060058B2 RID: 22706 RVA: 0x001CF1EE File Offset: 0x001CD3EE
	public void OnHeroPickerShown()
	{
		this.m_isStartingSession = false;
	}

	// Token: 0x060058B3 RID: 22707 RVA: 0x001CF1F7 File Offset: 0x001CD3F7
	private void StartSession(bool paidEntry)
	{
		Network.Get().RegisterNetHandler(PVPDRSessionStartResponse.PacketID.ID, new Network.NetHandler(this.OnSessionStartResponse), null);
		Network.Get().SendPVPDRSessionStartRequest(paidEntry);
		this.m_isStartingSession = true;
	}

	// Token: 0x060058B4 RID: 22708 RVA: 0x001CF230 File Offset: 0x001CD430
	private void OnSessionStartResponse()
	{
		Network.Get().RemoveNetHandler(PVPDRSessionStartResponse.PacketID.ID, new Network.NetHandler(this.OnSessionStartResponse));
		if (Network.Get().GetPVPDRSessionStartResponse().ErrorCode == ErrorCode.ERROR_OK)
		{
			DuelsConfig.Get().SetRecentEnd(false);
			AdventureConfig.Get().ChangeSubScene(AdventureData.Adventuresubscene.CHOOSER, true);
			Network.Get().RegisterNetHandler(PVPDRSessionInfoResponse.PacketID.ID, new Network.NetHandler(this.OnAfterStartSessionInfoResponse), null);
			Network.Get().SendPVPDRSessionInfoRequest();
			return;
		}
		this.OnSessionStartRequestFailed();
	}

	// Token: 0x060058B5 RID: 22709 RVA: 0x001CF2BC File Offset: 0x001CD4BC
	private void OnAfterStartSessionInfoResponse()
	{
		Network.Get().RemoveNetHandler(PVPDRSessionInfoResponse.PacketID.ID, new Network.NetHandler(this.OnAfterStartSessionInfoResponse));
		PVPDRSessionInfoResponse pvpdrsessionInfoResponse = Network.Get().GetPVPDRSessionInfoResponse();
		this.m_dataModel.IsPaidEntry = pvpdrsessionInfoResponse.Session.IsPaidEntry;
		GameSaveDataManager.Get().Request(PvPDungeonRunScene.Get().GetGSDKeyForAdventure(), new GameSaveDataManager.OnRequestDataResponseDelegate(this.CheckForTransition));
	}

	// Token: 0x060058B6 RID: 22710 RVA: 0x001CF32C File Offset: 0x001CD52C
	private void OnSessionStartRequestFailed()
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_PVPDR");
		popupInfo.m_text = GameStrings.Get("GLUE_PVPDR_SESSION_START_FAILED_BODY");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		};
		DialogManager.Get().ShowPopup(popupInfo);
		this.m_isStartingSession = false;
	}

	// Token: 0x060058B7 RID: 22711 RVA: 0x001CF3A4 File Offset: 0x001CD5A4
	private void TransitionToNextScreen()
	{
		bool flag = this.m_dataModel.HasSession && !this.m_dataModel.IsSessionActive;
		if (!DuelsConfig.IsInitialLoadoutComplete() && !flag)
		{
			if (!PvPDungeonRunScene.Get().TransitionToGuestHeroPicker())
			{
				this.EnableButtons(true);
				return;
			}
		}
		else
		{
			PvPDungeonRunScene.Get().TransitionToDungeonCrawlPlayMat();
		}
	}

	// Token: 0x060058B8 RID: 22712 RVA: 0x001CF3F8 File Offset: 0x001CD5F8
	private void CheckForTransition(bool success)
	{
		if (success)
		{
			this.TransitionToNextScreen();
		}
	}

	// Token: 0x060058B9 RID: 22713 RVA: 0x001CF403 File Offset: 0x001CD603
	private static bool OnNavigateBack()
	{
		if (PvPDungeonRunDisplay.m_instance != null)
		{
			PvPDungeonRunDisplay.m_instance.EnableButtons(false);
		}
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.GAME_MODE, SceneMgr.TransitionHandlerType.NEXT_SCENE, null);
		return true;
	}

	// Token: 0x060058BA RID: 22714 RVA: 0x001CF42C File Offset: 0x001CD62C
	public void CheckForStatsChanged()
	{
		if (this.m_stickyContainerFinishedLoading && this.m_stickyContainer != null)
		{
			this.m_stickyContainer.TriggerEvent("STATS_CHANGED", default(Widget.TriggerEventParameters));
			DuelsConfig.Get().SetRecentEnd(false);
		}
	}

	// Token: 0x060058BB RID: 22715 RVA: 0x001CF474 File Offset: 0x001CD674
	private void ShowNewUnlocksPopupIfNecessary()
	{
		AdventureDbId advId = AdventureConfig.Get().GetSelectedAdventure();
		List<long> newHeroPowers;
		List<long> newTreasures;
		HashSet<int> unlocks = DungeonCrawlUtil.GetAchievementsForRecentUnlocks(advId, out newHeroPowers, out newTreasures);
		if (unlocks.Count > 0)
		{
			int num = newHeroPowers.Count + newTreasures.Count;
			PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().IsRatingNotice = false;
			PvPDungeonRunScene.ShowDuelsMessagePopup(GameStrings.Get("GLUE_DUELS_NEW_UNLOCKS_HEADER"), GameStrings.Format("GLUE_DUELS_NEW_UNLOCKS_BODY", new object[]
			{
				num
			}), "", delegate
			{
				DungeonCrawlUtil.MarkUnlocksAsNew(advId, AdventureModeDbId.DUNGEON_CRAWL, newHeroPowers, newTreasures, null);
				DungeonCrawlUtil.AcknowledgeUnlocks(unlocks);
			});
		}
	}

	// Token: 0x04004BE2 RID: 19426
	public AsyncReference m_PlayButtonReference;

	// Token: 0x04004BE3 RID: 19427
	public AsyncReference m_PlayButtonPhoneReference;

	// Token: 0x04004BE4 RID: 19428
	public AsyncReference m_BackButtonReference;

	// Token: 0x04004BE5 RID: 19429
	public AsyncReference m_BackButtonPhoneReference;

	// Token: 0x04004BE6 RID: 19430
	public AsyncReference m_SeasonNameTextReference;

	// Token: 0x04004BE7 RID: 19431
	public AsyncReference m_SeasonNameTextPhoneReference;

	// Token: 0x04004BE8 RID: 19432
	public AsyncReference m_StickyContainerReference;

	// Token: 0x04004BE9 RID: 19433
	public AsyncReference m_StickyContainerPhoneReference;

	// Token: 0x04004BEA RID: 19434
	public PlayButton m_playButton;

	// Token: 0x04004BEB RID: 19435
	public UIBButton m_backButton;

	// Token: 0x04004BEC RID: 19436
	public DuelsPopupManager m_duelsPopupManager;

	// Token: 0x04004BED RID: 19437
	public Widget m_stickyContainer;

	// Token: 0x04004BEE RID: 19438
	public UberText m_seasonName;

	// Token: 0x04004BEF RID: 19439
	private bool m_playButtonFinishedLoading;

	// Token: 0x04004BF0 RID: 19440
	private bool m_playButtonWasEnabled;

	// Token: 0x04004BF1 RID: 19441
	private bool m_backButtonFinishedLoading;

	// Token: 0x04004BF2 RID: 19442
	private bool m_dataModelLoaded;

	// Token: 0x04004BF3 RID: 19443
	private bool m_stickyContainerFinishedLoading;

	// Token: 0x04004BF4 RID: 19444
	private bool m_seasonNameTextFinishedLoading;

	// Token: 0x04004BF5 RID: 19445
	private bool m_isContentRoll = true;

	// Token: 0x04004BF6 RID: 19446
	private bool m_isStartingSession;

	// Token: 0x04004BF7 RID: 19447
	private PVPDRLobbyDataModel m_dataModel;

	// Token: 0x04004BF8 RID: 19448
	private DateTime m_seasonEndDate;

	// Token: 0x04004BF9 RID: 19449
	private bool m_isSeasonActive;

	// Token: 0x04004BFA RID: 19450
	private static PvPDungeonRunDisplay m_instance;
}
