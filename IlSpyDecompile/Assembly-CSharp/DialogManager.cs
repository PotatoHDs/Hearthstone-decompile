using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using bgs;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class DialogManager : MonoBehaviour
{
	public delegate bool DialogProcessCallback(DialogBase dialog, object userData);

	public enum DialogType
	{
		ALERT,
		SEASON_END,
		FRIENDLY_CHALLENGE,
		TAVERN_BRAWL_CHALLENGE,
		EXISTING_ACCOUNT,
		CARD_LIST,
		STANDARD_COMING_SOON,
		ROTATION_TUTORIAL,
		HALL_OF_FAME,
		TAVERN_BRAWL_CHOICE,
		FIRESIDE_BRAWL_OK,
		FIRESIDE_GATHERING_JOIN,
		FIRESIDE_FIND_EVENT,
		FIRESIDE_LOCATION_HELPER,
		FIRESIDE_INNKEEPER_SETUP,
		RETURNING_PLAYER_OPT_OUT,
		OUTSTANDING_DRAFT_TICKETS,
		FREE_ARENA_WIN,
		ARENA_SEASON,
		ASSET_DOWNLOAD,
		LEAGUE_PROMOTE_SELF_MANUALLY,
		RECONNECT_HELPER,
		LOGIN_POPUP_SEQUENCE_BASIC,
		MULTI_PAGE_POPUP,
		GAME_MODES,
		BACON_CHALLENGE,
		PRIVACY_POLICY,
		GENERIC_BASIC_POPUP
	}

	public class DialogRequest
	{
		public DialogType m_type;

		public UserAttentionBlocker m_attentionCategory;

		public object m_info;

		public DialogProcessCallback m_callback;

		public object m_userData;

		public string m_prefabAssetReferenceOverride;

		public bool m_isWidget;

		public IDataModel m_dataModel;

		public bool m_isFake;
	}

	[Serializable]
	public class DialogTypeMapping
	{
		public DialogType m_type;

		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string m_prefabName;
	}

	private class SeasonEndDialogRequestInfo
	{
		public NetCache.ProfileNoticeMedal m_noticeMedal;
	}

	private class PopupCallbackSharedData
	{
		public readonly List<GameObject> m_loadedPrefabs = new List<GameObject>();

		public int m_remainingToLoad;

		public PopupCallbackSharedData(int count)
		{
			m_remainingToLoad = count;
		}
	}

	private struct PopupCallbackData
	{
		public PopupCallbackSharedData m_sharedData;

		public int m_index;

		public PopupCallbackData(PopupCallbackSharedData sharedData, int index)
		{
			m_sharedData = sharedData;
			m_index = index;
		}
	}

	private static DialogManager s_instance;

	private Queue<DialogRequest> m_dialogRequests = new Queue<DialogRequest>();

	private DialogBase m_currentDialog;

	private bool m_loadingDialog;

	private bool m_isReadyForSeasonEndPopup;

	private bool m_waitingToShowSeasonEndDialog;

	private List<long> m_handledMedalNoticeIDs = new List<long>();

	public List<DialogTypeMapping> m_typeMapping = new List<DialogTypeMapping>();

	public static event Action OnStarted;

	public event Action OnDialogShown;

	public event Action OnDialogHidden;

	private void Awake()
	{
		s_instance = this;
	}

	private void Start()
	{
		LoginManager.Get().OnInitialClientStateReceived += HandleSeasonEnd;
		if (DialogManager.OnStarted != null)
		{
			DialogManager.OnStarted();
		}
	}

	private void OnDestroy()
	{
		if (HearthstoneServices.TryGet<NetCache>(out var service))
		{
			service.RemoveNewNoticesListener(OnNewNotices);
		}
		if (LoginManager.Get() != null)
		{
			LoginManager.Get().OnInitialClientStateReceived -= HandleSeasonEnd;
		}
		s_instance = null;
	}

	public void HandleSeasonEnd()
	{
		NetCache.NetCacheProfileNotices netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>();
		if (netObject != null)
		{
			MaybeShowSeasonEndDialog(netObject.Notices, fromOutOfBandNotice: false);
		}
		NetCache.Get().RegisterNewNoticesListener(OnNewNotices);
	}

	public static DialogManager Get()
	{
		return s_instance;
	}

	public void GoBack()
	{
		if ((bool)m_currentDialog)
		{
			m_currentDialog.GoBack();
		}
	}

	public void ReadyForSeasonEndPopup(bool ready)
	{
		m_isReadyForSeasonEndPopup = ready;
	}

	public void ClearHandledMedalNotices()
	{
		m_handledMedalNoticeIDs.Clear();
	}

	public bool HandleKeyboardInput()
	{
		if (InputCollection.GetKeyUp(KeyCode.Escape))
		{
			if (!m_currentDialog)
			{
				return false;
			}
			return m_currentDialog.HandleKeyboardInput();
		}
		return false;
	}

	public bool AddToQueue(DialogRequest request)
	{
		UserAttentionBlocker attentionCategory = request?.m_attentionCategory ?? UserAttentionBlocker.NONE;
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) || !UserAttentionManager.CanShowAttentionGrabber(attentionCategory, "DialogManager.AddToQueue:" + ((request == null) ? "null" : request.m_type.ToString())))
		{
			return false;
		}
		m_dialogRequests.Enqueue(request);
		UpdateQueue();
		return true;
	}

	private void UpdateQueue()
	{
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) || m_currentDialog != null || m_loadingDialog || m_dialogRequests.Count == 0)
		{
			return;
		}
		DialogRequest dialogRequest = m_dialogRequests.Peek();
		if (!UserAttentionManager.CanShowAttentionGrabber(dialogRequest.m_attentionCategory, "DialogManager.UpdateQueue:" + dialogRequest.m_attentionCategory))
		{
			Processor.ScheduleCallback(0.5f, realTime: false, delegate
			{
				UpdateQueue();
			});
		}
		else
		{
			LoadPopup(dialogRequest);
		}
	}

	public void ShowPopup(AlertPopup.PopupInfo info, DialogProcessCallback callback, object userData)
	{
		UserAttentionBlocker attentionCategory = info?.m_attentionCategory ?? UserAttentionBlocker.NONE;
		if (!UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) && UserAttentionManager.CanShowAttentionGrabber(attentionCategory, "DialogManager.ShowPopup:" + ((info == null) ? "null" : (info.m_id + ":" + info.m_attentionCategory))))
		{
			DialogRequest dialogRequest = new DialogRequest();
			dialogRequest.m_type = DialogType.ALERT;
			dialogRequest.m_attentionCategory = attentionCategory;
			dialogRequest.m_info = info;
			dialogRequest.m_callback = callback;
			dialogRequest.m_userData = userData;
			AddToQueue(dialogRequest);
		}
	}

	public void ShowPopup(AlertPopup.PopupInfo info, DialogProcessCallback callback)
	{
		ShowPopup(info, callback, null);
	}

	public void ShowPopup(AlertPopup.PopupInfo info)
	{
		ShowPopup(info, null, null);
	}

	public bool ShowUniquePopup(AlertPopup.PopupInfo info, DialogProcessCallback callback, object userData)
	{
		UserAttentionBlocker attentionCategory = info?.m_attentionCategory ?? UserAttentionBlocker.NONE;
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) || !UserAttentionManager.CanShowAttentionGrabber(attentionCategory, "DialogManager.ShowUniquePopup:" + ((info == null) ? "null" : (info.m_id + ":" + info.m_attentionCategory))))
		{
			return false;
		}
		if (info != null && !string.IsNullOrEmpty(info.m_id))
		{
			foreach (DialogRequest dialogRequest in m_dialogRequests)
			{
				if (dialogRequest.m_type == DialogType.ALERT && ((AlertPopup.PopupInfo)dialogRequest.m_info).m_id == info.m_id)
				{
					return false;
				}
			}
		}
		ShowPopup(info, callback, userData);
		return true;
	}

	public bool ShowUniquePopup(AlertPopup.PopupInfo info, DialogProcessCallback callback)
	{
		return ShowUniquePopup(info, callback, null);
	}

	public bool ShowUniquePopup(AlertPopup.PopupInfo info)
	{
		return ShowUniquePopup(info, null, null);
	}

	public void ShowMessageOfTheDay(string message)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_text = message;
		ShowPopup(popupInfo);
	}

	public void RemoveUniquePopupRequestFromQueue(string id)
	{
		if (string.IsNullOrEmpty(id))
		{
			return;
		}
		foreach (DialogRequest dialogRequest in m_dialogRequests)
		{
			if (dialogRequest.m_type == DialogType.ALERT && ((AlertPopup.PopupInfo)dialogRequest.m_info).m_id == id)
			{
				m_dialogRequests = new Queue<DialogRequest>(m_dialogRequests.Where((DialogRequest r) => r.m_info != null && r.m_info.GetType() == typeof(AlertPopup.PopupInfo) && ((AlertPopup.PopupInfo)r.m_info).m_id != id));
				break;
			}
		}
	}

	public bool WaitingToShowSeasonEndDialog()
	{
		if (m_waitingToShowSeasonEndDialog || (m_currentDialog != null && m_currentDialog is SeasonEndDialog))
		{
			return true;
		}
		return m_dialogRequests.FirstOrDefault((DialogRequest obj) => obj.m_type == DialogType.SEASON_END) != null;
	}

	public IEnumerator<IAsyncJobResult> Job_WaitForSeasonEndPopup()
	{
		ReadyForSeasonEndPopup(ready: true);
		while (WaitingToShowSeasonEndDialog())
		{
			yield return null;
		}
	}

	public void ShowFriendlyChallenge(FormatType formatType, BnetPlayer challenger, bool challengeIsTavernBrawl, PartyType partyType, FriendlyChallengeDialog.ResponseCallback responseCallback, DialogProcessCallback callback)
	{
		DialogRequest dialogRequest = new DialogRequest();
		if (challengeIsTavernBrawl)
		{
			dialogRequest.m_type = DialogType.TAVERN_BRAWL_CHALLENGE;
		}
		else if (partyType == PartyType.BATTLEGROUNDS_PARTY)
		{
			dialogRequest.m_type = DialogType.BACON_CHALLENGE;
		}
		else
		{
			dialogRequest.m_type = DialogType.FRIENDLY_CHALLENGE;
		}
		FriendlyChallengeDialog.Info info = new FriendlyChallengeDialog.Info();
		info.m_formatType = formatType;
		info.m_challenger = challenger;
		info.m_partyType = partyType;
		info.m_callback = responseCallback;
		dialogRequest.m_info = info;
		dialogRequest.m_callback = callback;
		AddToQueue(dialogRequest);
	}

	public void ShowPrivacyPolicyPopup(PrivacyPolicyPopup.ResponseCallback responseCallback, DialogProcessCallback callback)
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.PRIVACY_POLICY;
		PrivacyPolicyPopup.Info info = new PrivacyPolicyPopup.Info();
		info.m_callback = responseCallback;
		dialogRequest.m_info = info;
		dialogRequest.m_callback = callback;
		AddToQueue(dialogRequest);
	}

	public void ShowExistingAccountPopup(ExistingAccountPopup.ResponseCallback responseCallback, DialogProcessCallback callback)
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.EXISTING_ACCOUNT;
		ExistingAccountPopup.Info info = new ExistingAccountPopup.Info();
		info.m_callback = responseCallback;
		dialogRequest.m_info = info;
		dialogRequest.m_callback = callback;
		AddToQueue(dialogRequest);
	}

	public void ShowTavernBrawlChoiceDialog(FiresideBrawlChoiceDialog.ResponseCallback callback)
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.TAVERN_BRAWL_CHOICE;
		FiresideBrawlChoiceDialog.Info info = new FiresideBrawlChoiceDialog.Info();
		info.m_callback = callback;
		dialogRequest.m_info = info;
		dialogRequest.m_callback = null;
		AddToQueue(dialogRequest);
	}

	public void ShowFiresideOKDialog()
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.FIRESIDE_BRAWL_OK;
		AddToQueue(dialogRequest);
	}

	public void ShowFiresideGatheringNearbyDialog(FiresideGatheringJoinDialog.ResponseCallback callback)
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.FIRESIDE_GATHERING_JOIN;
		FiresideGatheringJoinDialog.Info info = new FiresideGatheringJoinDialog.Info();
		info.m_callback = callback;
		dialogRequest.m_info = info;
		dialogRequest.m_callback = null;
		AddToQueue(dialogRequest);
	}

	private FiresideGatheringLocationHelperDialog.Info CreateFiresideGatheringLocationHelperDialogInfo(Action callback)
	{
		return new FiresideGatheringLocationHelperDialog.Info
		{
			m_callback = callback,
			m_isInnkeeperSetup = false,
			m_gpsOffIntroText = GameStrings.Get("GLUE_FIRESIDE_GATHERING_TURN_ON_GPS_BODY"),
			m_wifiOffIntroText = GameStrings.Get("GLUE_FIRESIDE_GATHERING_TURN_ON_WIFI_BODY"),
			m_waitingForWifiText = GameStrings.Get("GLUE_FIRESIDE_GATHERING_CONNECT_TO_WIFI_BODY"),
			m_wifiConfirmText = GameStrings.Get("GLUE_FIRESIDE_GATHERING_CONNECT_TO_WIFI_SSID_CONFIRM_TITLE")
		};
	}

	public void ShowFiresideGatheringLocationHelperDialog(Action callback)
	{
		FiresideGatheringLocationHelperDialog.Info info = CreateFiresideGatheringLocationHelperDialogInfo(callback);
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.FIRESIDE_LOCATION_HELPER;
		dialogRequest.m_info = info;
		dialogRequest.m_callback = null;
		AddToQueue(dialogRequest);
	}

	public void ShowFiresideGatheringCheckInFailedDialog()
	{
		FiresideGatheringLocationHelperDialog.Info info = CreateFiresideGatheringLocationHelperDialogInfo(null);
		info.m_isCheckInFailure = true;
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.FIRESIDE_LOCATION_HELPER;
		dialogRequest.m_info = info;
		dialogRequest.m_callback = null;
		AddToQueue(dialogRequest);
	}

	public void ShowFiresideGatheringInnkeeperSetupHelperDialog(Action callback)
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.FIRESIDE_LOCATION_HELPER;
		FiresideGatheringLocationHelperDialog.Info info = new FiresideGatheringLocationHelperDialog.Info();
		info.m_callback = callback;
		info.m_isInnkeeperSetup = true;
		info.m_gpsOffIntroText = GameStrings.Get("GLUE_FIRESIDE_GATHERING_INNKEEPER_TURN_ON_GPS_BODY");
		info.m_wifiOffIntroText = GameStrings.Get("GLUE_FIRESIDE_GATHERING_INNKEEPER_TURN_ON_WIFI_BODY");
		info.m_waitingForWifiText = GameStrings.Get("GLUE_FIRESIDE_GATHERING_INKEEPER_CONNECT_TO_WIFI_BODY");
		info.m_wifiConfirmText = GameStrings.Get("GLUE_FIRESIDE_GATHERING_INNKEEPER_CONNECT_TO_WIFI_SSID_CONFIRM_TITLE");
		dialogRequest.m_info = info;
		dialogRequest.m_callback = null;
		AddToQueue(dialogRequest);
	}

	public void ShowFiresideGatheringFindEventDialog(FiresideGatheringFindEventDialog.ResponseCallback callback)
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.FIRESIDE_FIND_EVENT;
		FiresideGatheringFindEventDialog.Info info = new FiresideGatheringFindEventDialog.Info();
		info.m_callback = callback;
		dialogRequest.m_info = info;
		dialogRequest.m_callback = null;
		AddToQueue(dialogRequest);
	}

	public void ShowFiresideGatheringInnkeeperSetupDialog(FiresideGatheringInnkeeperSetupDialog.ResponseCallback callback, string tavernName)
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.FIRESIDE_INNKEEPER_SETUP;
		FiresideGatheringInnkeeperSetupDialog.Info info = new FiresideGatheringInnkeeperSetupDialog.Info();
		info.m_callback = callback;
		info.m_tavernName = tavernName;
		dialogRequest.m_info = info;
		dialogRequest.m_callback = null;
		AddToQueue(dialogRequest);
	}

	public void ShowLeaguePromoteSelfManuallyDialog(LeaguePromoteSelfManuallyDialog.ResponseCallback callback)
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.LEAGUE_PROMOTE_SELF_MANUALLY;
		LeaguePromoteSelfManuallyDialog.Info info = new LeaguePromoteSelfManuallyDialog.Info();
		info.m_callback = callback;
		dialogRequest.m_info = info;
		dialogRequest.m_callback = null;
		AddToQueue(dialogRequest);
	}

	public void ShowCardListPopup(UserAttentionBlocker attentionCategory, CardListPopup.Info info)
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.CARD_LIST;
		dialogRequest.m_attentionCategory = attentionCategory;
		dialogRequest.m_info = info;
		AddToQueue(dialogRequest);
	}

	public void ShowStandardComingSoonPopup(UserAttentionBlocker attentionCategory, BasicPopup.PopupInfo info)
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.STANDARD_COMING_SOON;
		dialogRequest.m_attentionCategory = attentionCategory;
		dialogRequest.m_info = info;
		AddToQueue(dialogRequest);
	}

	public void ShowSetRotationTutorialPopup(UserAttentionBlocker attentionCategory, SetRotationRotatedBoostersPopup.SetRotationRotatedBoostersPopupInfo info)
	{
		info.m_prefabAssetRefs.Add(new AssetReference("SetRotationRotatedBoostersPopup.prefab:2a1c1ce78c98c1e418039a479c8ddce4"));
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.GENERIC_BASIC_POPUP;
		dialogRequest.m_attentionCategory = attentionCategory;
		dialogRequest.m_info = info;
		dialogRequest.m_isWidget = true;
		AddToQueue(dialogRequest);
	}

	public void ShowOutstandingDraftTicketPopup(UserAttentionBlocker attentionCategory, OutstandingDraftTicketDialog.Info info)
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.OUTSTANDING_DRAFT_TICKETS;
		dialogRequest.m_attentionCategory = attentionCategory;
		dialogRequest.m_info = info;
		AddToQueue(dialogRequest);
	}

	public void ShowFreeArenaWinPopup(UserAttentionBlocker attentionCategory, FreeArenaWinDialog.Info info)
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.FREE_ARENA_WIN;
		dialogRequest.m_attentionCategory = attentionCategory;
		dialogRequest.m_info = info;
		AddToQueue(dialogRequest);
	}

	public bool ShowArenaSeasonPopup(UserAttentionBlocker attentionCategory, BasicPopup.PopupInfo info)
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.ARENA_SEASON;
		dialogRequest.m_attentionCategory = attentionCategory;
		dialogRequest.m_info = info;
		return AddToQueue(dialogRequest);
	}

	public void ShowLoginPopupSequenceBasicPopup(UserAttentionBlocker attentionCategory, LoginPopupSequencePopup.Info info)
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.LOGIN_POPUP_SEQUENCE_BASIC;
		dialogRequest.m_attentionCategory = attentionCategory;
		dialogRequest.m_info = info;
		dialogRequest.m_prefabAssetReferenceOverride = info.m_prefabAssetReference;
		AddToQueue(dialogRequest);
	}

	public void ShowMultiPagePopup(UserAttentionBlocker attentionCategory, MultiPagePopup.Info info)
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.MULTI_PAGE_POPUP;
		dialogRequest.m_attentionCategory = attentionCategory;
		dialogRequest.m_info = info;
		dialogRequest.m_prefabAssetReferenceOverride = "MultiPagePopup.prefab:a9b6df0282662ed449031d34aa2ecfa7";
		AddToQueue(dialogRequest);
	}

	public bool ShowBasicPopup(UserAttentionBlocker attentionCategory, BasicPopup.PopupInfo info)
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.GENERIC_BASIC_POPUP;
		dialogRequest.m_attentionCategory = attentionCategory;
		dialogRequest.m_info = info;
		return AddToQueue(dialogRequest);
	}

	public bool ShowAssetDownloadPopup(AssetDownloadDialog.Info info)
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.ASSET_DOWNLOAD;
		dialogRequest.m_attentionCategory = UserAttentionBlocker.NONE;
		dialogRequest.m_info = info;
		return AddToQueue(dialogRequest);
	}

	public void ShowReconnectHelperDialog(Action reconnectSuccessCallback = null, Action goBackCallback = null)
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.RECONNECT_HELPER;
		ReconnectHelperDialog.Info info = new ReconnectHelperDialog.Info();
		info.m_reconnectSuccessCallback = reconnectSuccessCallback;
		info.m_goBackCallback = goBackCallback;
		dialogRequest.m_info = info;
		dialogRequest.m_callback = null;
		AddToQueue(dialogRequest);
	}

	public void ShowGameModesPopup(UIEvent.Handler onArenaButtonReleased, UIEvent.Handler onBaconButtonReleased, UIEvent.Handler onPvPDungeonRunButtonReleased)
	{
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.GAME_MODES;
		GameModesPopup.Info info = (GameModesPopup.Info)(dialogRequest.m_info = new GameModesPopup.Info
		{
			m_onArenaButtonReleased = onArenaButtonReleased,
			m_onBaconButtonReleased = onBaconButtonReleased
		});
		dialogRequest.m_callback = null;
		AddToQueue(dialogRequest);
	}

	public void ShowClassUpcomingPopup()
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_headerText = GameStrings.Get("GLUE_CLASS_UPCOMING_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_CLASS_UPCOMING_DESC");
		Get().ShowPopup(popupInfo);
	}

	public void ShowBonusStarsPopup(RankedPlayDataModel dataModel, Action onHiddenCallback)
	{
		RankedBonusStarsPopup.BonusStarsPopupInfo bonusStarsPopupInfo = new RankedBonusStarsPopup.BonusStarsPopupInfo
		{
			m_onHiddenCallback = onHiddenCallback
		};
		DialogRequest request = new DialogRequest
		{
			m_type = DialogType.GENERIC_BASIC_POPUP,
			m_dataModel = dataModel,
			m_info = bonusStarsPopupInfo,
			m_isWidget = true
		};
		bonusStarsPopupInfo.m_prefabAssetRefs.Add(RankMgr.BONUS_STAR_POPUP_PREFAB);
		AddToQueue(request);
	}

	public void ShowRankedIntroPopUp(Action onHiddenCallback)
	{
		RankedIntroPopup.RankedIntroPopupInfo rankedIntroPopupInfo = new RankedIntroPopup.RankedIntroPopupInfo
		{
			m_onHiddenCallback = onHiddenCallback
		};
		DialogRequest request = new DialogRequest
		{
			m_type = DialogType.GENERIC_BASIC_POPUP,
			m_info = rankedIntroPopupInfo,
			m_isWidget = true
		};
		rankedIntroPopupInfo.m_prefabAssetRefs.Add(RankMgr.RANKED_INTRO_POPUP_PREFAB);
		AddToQueue(request);
	}

	public void ClearAllImmediately()
	{
		if (m_currentDialog != null)
		{
			UnityEngine.Object.DestroyImmediate(m_currentDialog.gameObject);
			m_currentDialog = null;
		}
		m_dialogRequests.Clear();
	}

	public bool ShowingDialog()
	{
		if (!(m_currentDialog != null))
		{
			return m_dialogRequests.Count > 0;
		}
		return true;
	}

	public bool ShowingHighPriorityDialog()
	{
		if (m_currentDialog != null)
		{
			return m_currentDialog.gameObject.layer == 27;
		}
		return false;
	}

	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		MaybeShowSeasonEndDialog(newNotices, !isInitialNoticeList);
	}

	private void MaybeShowSeasonEndDialog(List<NetCache.ProfileNotice> newNotices, bool fromOutOfBandNotice)
	{
		newNotices.Sort(delegate(NetCache.ProfileNotice a, NetCache.ProfileNotice b)
		{
			if (a.Type != b.Type)
			{
				return a.Type - b.Type;
			}
			if (a.Origin != b.Origin)
			{
				return a.Origin - b.Origin;
			}
			return (int)((a.OriginData != b.OriginData) ? (a.OriginData - b.OriginData) : (a.NoticeID - b.NoticeID));
		});
		NetCache.ProfileNotice profileNotice = MaybeShowSeasonEndDialog_GetLatestMedalNotice(newNotices);
		if (profileNotice == null)
		{
			return;
		}
		NetCache.ProfileNoticeMedal profileNoticeMedal = profileNotice as NetCache.ProfileNoticeMedal;
		if (profileNoticeMedal == null || m_handledMedalNoticeIDs.Contains(profileNoticeMedal.NoticeID) || UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) || !UserAttentionManager.CanShowAttentionGrabber("DialogManager.MaybeShowSeasonEndDialog"))
		{
			return;
		}
		m_handledMedalNoticeIDs.Add(profileNoticeMedal.NoticeID);
		if (ReturningPlayerMgr.Get().SuppressOldPopups)
		{
			Log.ReturningPlayer.Print("Suppressing popup for Season End Dialogue {0} due to being a Returning Player!");
			Network.Get().AckNotice(profileNoticeMedal.NoticeID);
			return;
		}
		if (fromOutOfBandNotice)
		{
			NetCache.Get().RefreshNetObject<NetCache.NetCacheMedalInfo>();
			NetCache.Get().ReloadNetObject<NetCache.NetCacheRewardProgress>();
		}
		SeasonEndDialogRequestInfo seasonEndDialogRequestInfo = new SeasonEndDialogRequestInfo();
		seasonEndDialogRequestInfo.m_noticeMedal = profileNoticeMedal;
		DialogRequest dialogRequest = new DialogRequest();
		dialogRequest.m_type = DialogType.SEASON_END;
		dialogRequest.m_info = seasonEndDialogRequestInfo;
		StartCoroutine(ShowSeasonEndDialogWhenReady(dialogRequest));
	}

	private NetCache.ProfileNotice MaybeShowSeasonEndDialog_GetLatestMedalNotice(List<NetCache.ProfileNotice> newNotices)
	{
		List<NetCache.ProfileNotice> source = new List<NetCache.ProfileNotice>(newNotices);
		IEnumerable<NetCache.ProfileNotice> source2 = source.Where((NetCache.ProfileNotice notice) => notice.Type == NetCache.ProfileNotice.NoticeType.GAINED_MEDAL);
		IEnumerable<NetCache.ProfileNotice> enumerable = source.Where((NetCache.ProfileNotice notice) => notice.Type == NetCache.ProfileNotice.NoticeType.BONUS_STARS);
		if (source2.Any())
		{
			long val = 52L;
			long maxSeason = Math.Max(val, source2.Max((NetCache.ProfileNotice n) => n.OriginData));
			source2.Where((NetCache.ProfileNotice notice) => notice.OriginData != maxSeason).ForEach(delegate(NetCache.ProfileNotice notice)
			{
				Network.Get().AckNotice(notice.NoticeID);
			});
			source2 = source2.Where((NetCache.ProfileNotice notice) => notice.OriginData == maxSeason);
			source2.Skip(1).ForEach(delegate(NetCache.ProfileNotice notice)
			{
				Network.Get().AckNotice(notice.NoticeID);
			});
		}
		enumerable.ForEach(delegate(NetCache.ProfileNotice notice)
		{
			Network.Get().AckNotice(notice.NoticeID);
		});
		return source2.FirstOrDefault();
	}

	private void LoadPopup(DialogRequest request)
	{
		List<string> list = null;
		if (request.m_info is BasicPopup.PopupInfo)
		{
			list = ((BasicPopup.PopupInfo)request.m_info).m_prefabAssetRefs;
		}
		else
		{
			list = new List<string>();
			string prefabNameFromDialogRequest = GetPrefabNameFromDialogRequest(request);
			list.Add(prefabNameFromDialogRequest);
		}
		if (list == null || list.Count == 0 || string.IsNullOrEmpty(list[0]))
		{
			Error.AddDevFatal("DialogManager.LoadPopup() - no prefab to load for type={0} info={1} attnCategory={2} prefabName={3}", request.m_type, request.m_info, request.m_attentionCategory, (list == null) ? "<null>" : ((list.Count == 0) ? "<empty>" : (list[0] ?? "null")));
			return;
		}
		list.RemoveAll((string assetRef) => string.IsNullOrEmpty(assetRef));
		m_loadingDialog = true;
		PopupCallbackSharedData popupCallbackSharedData = new PopupCallbackSharedData(list.Count);
		for (int i = 0; i < list.Count; i++)
		{
			popupCallbackSharedData.m_loadedPrefabs.Add(null);
		}
		for (int j = 0; j < list.Count; j++)
		{
			PopupCallbackData popupCallbackData = new PopupCallbackData(popupCallbackSharedData, j);
			if (request.m_isWidget)
			{
				WidgetInstance widgetInstance = WidgetInstance.Create(list[j]);
				if (request.m_dataModel != null)
				{
					widgetInstance.BindDataModel(request.m_dataModel);
				}
				StartCoroutine(WaitForWidgetPopupReady(list[j], widgetInstance, popupCallbackData));
			}
			else
			{
				AssetLoader.Get().InstantiatePrefab(list[j], OnPopupLoaded, popupCallbackData);
			}
		}
	}

	private string GetPrefabNameFromDialogRequest(DialogRequest request)
	{
		if (!string.IsNullOrEmpty(request.m_prefabAssetReferenceOverride))
		{
			return request.m_prefabAssetReferenceOverride;
		}
		DialogTypeMapping dialogTypeMapping = m_typeMapping.Find((DialogTypeMapping x) => x.m_type == request.m_type);
		if (dialogTypeMapping == null || dialogTypeMapping.m_prefabName == null)
		{
			Error.AddDevFatal("DialogManager.GetPrefabNameFromDialogRequest() - unhandled dialog type {0}", request.m_type);
			return null;
		}
		return dialogTypeMapping.m_prefabName;
	}

	private IEnumerator WaitForWidgetPopupReady(AssetReference assetRef, WidgetInstance widgetInstance, object callbackData)
	{
		if (!(widgetInstance == null))
		{
			widgetInstance.Hide();
			while (!widgetInstance.IsReady || widgetInstance.IsChangingStates)
			{
				yield return null;
			}
			OnPopupLoaded(assetRef, widgetInstance.gameObject, callbackData);
			widgetInstance.Show();
		}
	}

	private void OnPopupLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		DialogRequest dialogRequest = ((m_dialogRequests.Count == 0) ? null : m_dialogRequests.Peek());
		UserAttentionBlocker attentionCategory = dialogRequest?.m_attentionCategory ?? UserAttentionBlocker.NONE;
		if (m_dialogRequests.Count == 0 || UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) || !UserAttentionManager.CanShowAttentionGrabber(attentionCategory, "DialogManager.OnPopupLoaded:" + ((dialogRequest == null) ? "null" : dialogRequest.m_type.ToString())))
		{
			m_loadingDialog = false;
			UnityEngine.Object.DestroyImmediate(go);
			return;
		}
		PopupCallbackData popupCallbackData = (PopupCallbackData)callbackData;
		popupCallbackData.m_sharedData.m_loadedPrefabs[popupCallbackData.m_index] = go;
		if (--popupCallbackData.m_sharedData.m_remainingToLoad > 0)
		{
			return;
		}
		m_loadingDialog = false;
		dialogRequest = m_dialogRequests.Dequeue();
		GameObject gameObject = ((popupCallbackData.m_sharedData.m_loadedPrefabs.Count == 0) ? null : popupCallbackData.m_sharedData.m_loadedPrefabs[0]);
		DialogBase dialogBase = ((gameObject == null) ? null : gameObject.GetComponentInChildren<DialogBase>());
		if (dialogBase == null)
		{
			Debug.LogError($"DialogManager.OnPopupLoaded() - game object {go} has no DialogBase component (request_type={dialogRequest.m_type} count prefabs loaded={popupCallbackData.m_sharedData.m_loadedPrefabs.Count})");
			UnityEngine.Object.DestroyImmediate(go);
			UpdateQueue();
			return;
		}
		for (int i = 1; i < popupCallbackData.m_sharedData.m_loadedPrefabs.Count; i++)
		{
			GameObject gameObject2 = popupCallbackData.m_sharedData.m_loadedPrefabs[i];
			if (!(gameObject2 == null))
			{
				gameObject2.transform.SetParent(gameObject.transform, worldPositionStays: false);
			}
		}
		ProcessRequest(dialogRequest, dialogBase);
	}

	private void ProcessRequest(DialogRequest request, DialogBase dialog)
	{
		if (request.m_callback != null && !request.m_callback(dialog, request.m_userData))
		{
			UpdateQueue();
			UnityEngine.Object.Destroy(dialog.gameObject);
			return;
		}
		m_currentDialog = dialog;
		m_currentDialog.SetReadyToDestroyCallback(OnCurrentDialogHidden);
		if (request.m_type == DialogType.ALERT)
		{
			ProcessAlertRequest(request, (AlertPopup)dialog);
		}
		else if (request.m_type == DialogType.SEASON_END)
		{
			ProcessMedalRequest(request, (SeasonEndDialog)dialog);
		}
		else if (request.m_type == DialogType.FRIENDLY_CHALLENGE || request.m_type == DialogType.TAVERN_BRAWL_CHALLENGE || request.m_type == DialogType.BACON_CHALLENGE)
		{
			ProcessFriendlyChallengeRequest(request, (FriendlyChallengeDialog)dialog);
		}
		else if (request.m_type == DialogType.EXISTING_ACCOUNT)
		{
			ProcessExistingAccountRequest(request, (ExistingAccountPopup)dialog);
		}
		else if (request.m_type == DialogType.CARD_LIST)
		{
			ProcessCardListRequest(request, (CardListPopup)dialog);
		}
		else if (request.m_type == DialogType.TAVERN_BRAWL_CHOICE)
		{
			ProcessFiresideBrawlChoiceRequest(request, (FiresideBrawlChoiceDialog)dialog);
		}
		else if (request.m_type == DialogType.FIRESIDE_BRAWL_OK)
		{
			ProcessFiresideBrawlOkRequest(request, (FiresideBrawlOkDialog)dialog);
		}
		else if (request.m_type == DialogType.FIRESIDE_GATHERING_JOIN)
		{
			ProcessFiresideGatheringNearbyRequest(request, (FiresideGatheringJoinDialog)dialog);
		}
		else if (request.m_type == DialogType.FIRESIDE_FIND_EVENT)
		{
			ProcessFiresideGatheringFindEventRequest(request, (FiresideGatheringFindEventDialog)dialog);
		}
		else if (request.m_type == DialogType.FIRESIDE_LOCATION_HELPER)
		{
			ProcessFiresideGatheringLocationHelperRequest(request, (FiresideGatheringLocationHelperDialog)dialog);
		}
		else if (request.m_type == DialogType.FIRESIDE_INNKEEPER_SETUP)
		{
			ProcessFiresideGatheringInnkeeperSetupRequest(request, (FiresideGatheringInnkeeperSetupDialog)dialog);
		}
		else if (request.m_type == DialogType.LEAGUE_PROMOTE_SELF_MANUALLY)
		{
			ProcessLeaguePromoteSelfManuallyRequest(request, (LeaguePromoteSelfManuallyDialog)dialog);
		}
		else if (request.m_type == DialogType.OUTSTANDING_DRAFT_TICKETS)
		{
			ProcessOutstandingDraftTicketDialog(request, (OutstandingDraftTicketDialog)dialog);
		}
		else if (request.m_type == DialogType.FREE_ARENA_WIN)
		{
			ProcessFreeArenaWinDialog(request, (FreeArenaWinDialog)dialog);
		}
		else if (request.m_type == DialogType.GENERIC_BASIC_POPUP || request.m_type == DialogType.STANDARD_COMING_SOON || request.m_type == DialogType.ARENA_SEASON)
		{
			ProcessBasicPopupRequest(request, (BasicPopup)dialog);
		}
		else if (request.m_type == DialogType.ASSET_DOWNLOAD)
		{
			ProcessAssetDownloadRequest(request, (AssetDownloadDialog)dialog);
		}
		else if (request.m_type == DialogType.RECONNECT_HELPER)
		{
			ProcessReconnectRequest(request, (ReconnectHelperDialog)dialog);
		}
		else if (request.m_type == DialogType.LOGIN_POPUP_SEQUENCE_BASIC)
		{
			ProcessLoginPopupSequenceBasicPopupRequest(request, (LoginPopupSequencePopup)dialog);
		}
		else if (request.m_type == DialogType.MULTI_PAGE_POPUP)
		{
			ProcessMultiPagePopupRequest(request, (MultiPagePopup)dialog);
		}
		else if (request.m_type == DialogType.GAME_MODES)
		{
			ProcessGameModesPopupRequest(request, (GameModesPopup)dialog);
		}
		else if (request.m_type == DialogType.PRIVACY_POLICY)
		{
			ProcessPrivacyPolicyRequest(request, (PrivacyPolicyPopup)dialog);
		}
		if (this.OnDialogShown != null)
		{
			this.OnDialogShown();
		}
	}

	private void ProcessExistingAccountRequest(DialogRequest request, ExistingAccountPopup exAcctPopup)
	{
		exAcctPopup.SetInfo((ExistingAccountPopup.Info)request.m_info);
		exAcctPopup.Show();
	}

	private void ProcessAlertRequest(DialogRequest request, AlertPopup alertPopup)
	{
		AlertPopup.PopupInfo info = (AlertPopup.PopupInfo)request.m_info;
		alertPopup.SetInfo(info);
		alertPopup.Show();
	}

	private void ProcessFiresideBrawlChoiceRequest(DialogRequest request, FiresideBrawlChoiceDialog choicePopup)
	{
		FiresideBrawlChoiceDialog.Info info = (FiresideBrawlChoiceDialog.Info)request.m_info;
		choicePopup.SetInfo(info);
		choicePopup.Show();
	}

	private void ProcessFiresideBrawlOkRequest(DialogRequest request, FiresideBrawlOkDialog okPopup)
	{
		okPopup.Show();
	}

	private void ProcessFiresideGatheringNearbyRequest(DialogRequest request, FiresideGatheringJoinDialog choicePopup)
	{
		FiresideGatheringJoinDialog.Info info = (FiresideGatheringJoinDialog.Info)request.m_info;
		choicePopup.SetInfo(info);
		choicePopup.Show();
	}

	private void ProcessFiresideGatheringFindEventRequest(DialogRequest request, FiresideGatheringFindEventDialog choicePopup)
	{
		FiresideGatheringFindEventDialog.Info info = (FiresideGatheringFindEventDialog.Info)request.m_info;
		choicePopup.SetInfo(info);
		choicePopup.Show();
	}

	private void ProcessFiresideGatheringInnkeeperSetupRequest(DialogRequest request, FiresideGatheringInnkeeperSetupDialog choicePopup)
	{
		FiresideGatheringInnkeeperSetupDialog.Info info = (FiresideGatheringInnkeeperSetupDialog.Info)request.m_info;
		choicePopup.SetInfo(info);
		choicePopup.Show();
	}

	private void ProcessFiresideGatheringLocationHelperRequest(DialogRequest request, FiresideGatheringLocationHelperDialog fsgLocationHelperPopup)
	{
		FiresideGatheringLocationHelperDialog.Info info = (FiresideGatheringLocationHelperDialog.Info)request.m_info;
		fsgLocationHelperPopup.SetInfo(info);
		fsgLocationHelperPopup.Show();
	}

	private void ProcessBasicPopupRequest(DialogRequest request, BasicPopup basicPopup)
	{
		BasicPopup.PopupInfo info = (BasicPopup.PopupInfo)request.m_info;
		basicPopup.SetInfo(info);
		basicPopup.Show();
	}

	private void ProcessAssetDownloadRequest(DialogRequest request, AssetDownloadDialog dialog)
	{
		dialog.Show();
	}

	private void ProcessReconnectRequest(DialogRequest request, ReconnectHelperDialog dialog)
	{
		dialog.SetInfo((ReconnectHelperDialog.Info)request.m_info);
		dialog.Show();
	}

	private void ProcessMedalRequest(DialogRequest request, SeasonEndDialog seasonEndDialog)
	{
		SeasonEndDialog.SeasonEndInfo seasonEndInfo = null;
		if (request.m_isFake)
		{
			seasonEndInfo = request.m_info as SeasonEndDialog.SeasonEndInfo;
			if (seasonEndInfo == null)
			{
				return;
			}
		}
		else
		{
			SeasonEndDialogRequestInfo seasonEndDialogRequestInfo = request.m_info as SeasonEndDialogRequestInfo;
			if (PopupDisplayManager.ShouldDisableNotificationOnLogin())
			{
				Network.Get().AckNotice(seasonEndDialogRequestInfo.m_noticeMedal.NoticeID);
				UIStatus.Get().AddInfo("Season Roll skipped due to disableLoginPopups", 5f);
				return;
			}
			seasonEndInfo = new SeasonEndDialog.SeasonEndInfo();
			seasonEndInfo.m_noticesToAck.Add(seasonEndDialogRequestInfo.m_noticeMedal.NoticeID);
			seasonEndInfo.m_seasonID = (int)seasonEndDialogRequestInfo.m_noticeMedal.OriginData;
			seasonEndInfo.m_leagueId = seasonEndDialogRequestInfo.m_noticeMedal.LeagueId;
			seasonEndInfo.m_starLevelAtEndOfSeason = seasonEndDialogRequestInfo.m_noticeMedal.StarLevel;
			seasonEndInfo.m_bestStarLevelAtEndOfSeason = seasonEndDialogRequestInfo.m_noticeMedal.BestStarLevel;
			seasonEndInfo.m_legendIndex = seasonEndDialogRequestInfo.m_noticeMedal.LegendRank;
			seasonEndInfo.m_rankedRewards = seasonEndDialogRequestInfo.m_noticeMedal.Chest.Rewards;
			seasonEndInfo.m_formatType = seasonEndDialogRequestInfo.m_noticeMedal.FormatType;
			seasonEndInfo.m_wasLimitedByBestEverStarLevel = seasonEndDialogRequestInfo.m_noticeMedal.WasLimitedByBestEverStarLevel;
		}
		seasonEndDialog.Init(seasonEndInfo);
		seasonEndDialog.Show();
	}

	private void ProcessFriendlyChallengeRequest(DialogRequest request, FriendlyChallengeDialog friendlyChallengeDialog)
	{
		friendlyChallengeDialog.SetInfo((FriendlyChallengeDialog.Info)request.m_info);
		friendlyChallengeDialog.Show();
	}

	private void ProcessCardListRequest(DialogRequest request, CardListPopup cardListPopup)
	{
		CardListPopup.Info info = (CardListPopup.Info)request.m_info;
		cardListPopup.SetInfo(info);
		cardListPopup.Show();
	}

	private void ProcessSetRotationRotatedBoostersPopupRequest(DialogRequest request, SetRotationRotatedBoostersPopup setRotationTutorialDialog)
	{
		SetRotationRotatedBoostersPopup.SetRotationRotatedBoostersPopupInfo info = (SetRotationRotatedBoostersPopup.SetRotationRotatedBoostersPopupInfo)request.m_info;
		setRotationTutorialDialog.SetInfo(info);
		setRotationTutorialDialog.Show();
	}

	private void ProcessLeaguePromoteSelfManuallyRequest(DialogRequest request, LeaguePromoteSelfManuallyDialog leaguePromoteSelfManuallyDialog)
	{
		LeaguePromoteSelfManuallyDialog.Info info = (LeaguePromoteSelfManuallyDialog.Info)request.m_info;
		leaguePromoteSelfManuallyDialog.SetInfo(info);
		leaguePromoteSelfManuallyDialog.Show();
	}

	private void ProcessOutstandingDraftTicketDialog(DialogRequest request, OutstandingDraftTicketDialog outstandingDraftTicketDialog)
	{
		OutstandingDraftTicketDialog.Info info = (OutstandingDraftTicketDialog.Info)request.m_info;
		outstandingDraftTicketDialog.SetInfo(info);
		outstandingDraftTicketDialog.Show();
	}

	private void ProcessFreeArenaWinDialog(DialogRequest request, FreeArenaWinDialog freeArenaWinDialog)
	{
		FreeArenaWinDialog.Info info = (FreeArenaWinDialog.Info)request.m_info;
		freeArenaWinDialog.SetInfo(info);
		freeArenaWinDialog.Show();
	}

	private void ProcessLoginPopupSequenceBasicPopupRequest(DialogRequest request, LoginPopupSequencePopup loginPopupSequencePopup)
	{
		LoginPopupSequencePopup.Info info = (LoginPopupSequencePopup.Info)request.m_info;
		loginPopupSequencePopup.SetInfo(info);
		loginPopupSequencePopup.LoadAssetsAndShowWhenReady();
	}

	private void ProcessGameModesPopupRequest(DialogRequest request, GameModesPopup gameModesPopup)
	{
		GameModesPopup.Info info = (GameModesPopup.Info)request.m_info;
		gameModesPopup.SetInfo(info);
		gameModesPopup.Show();
	}

	private void ProcessPrivacyPolicyRequest(DialogRequest request, PrivacyPolicyPopup privacyPolicyPopup)
	{
		privacyPolicyPopup.SetInfo((PrivacyPolicyPopup.Info)request.m_info);
		privacyPolicyPopup.Show();
	}

	private void ProcessMultiPagePopupRequest(DialogRequest request, MultiPagePopup multiPagePopup)
	{
		MultiPagePopup.Info info = (MultiPagePopup.Info)request.m_info;
		multiPagePopup.SetInfo(info);
		multiPagePopup.Show();
	}

	private void OnCurrentDialogHidden(DialogBase dialog)
	{
		if (!(dialog != m_currentDialog))
		{
			UnityEngine.Object.Destroy(m_currentDialog.gameObject);
			m_currentDialog = null;
			UpdateQueue();
			if (this.OnDialogHidden != null)
			{
				this.OnDialogHidden();
			}
		}
	}

	private IEnumerator ShowSeasonEndDialogWhenReady(DialogRequest request)
	{
		m_waitingToShowSeasonEndDialog = true;
		while (!NetCache.Get().IsNetObjectAvailable<NetCache.NetCacheRewardProgress>() || !m_isReadyForSeasonEndPopup)
		{
			yield return null;
		}
		while (SceneMgr.Get().IsTransitioning())
		{
			yield return null;
		}
		while (SceneMgr.Get().GetMode() != SceneMgr.Mode.HUB)
		{
			if ((SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT || SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN) && !SceneMgr.Get().IsTransitioning())
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
				break;
			}
			yield return null;
		}
		while (SceneMgr.Get().IsTransitioning())
		{
			yield return null;
		}
		AddToQueue(request);
		m_waitingToShowSeasonEndDialog = false;
	}
}
