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

// Token: 0x02000898 RID: 2200
[CustomEditClass]
public class DialogManager : MonoBehaviour
{
	// Token: 0x1400007B RID: 123
	// (add) Token: 0x060078C7 RID: 30919 RVA: 0x00276430 File Offset: 0x00274630
	// (remove) Token: 0x060078C8 RID: 30920 RVA: 0x00276464 File Offset: 0x00274664
	public static event Action OnStarted;

	// Token: 0x1400007C RID: 124
	// (add) Token: 0x060078C9 RID: 30921 RVA: 0x00276498 File Offset: 0x00274698
	// (remove) Token: 0x060078CA RID: 30922 RVA: 0x002764D0 File Offset: 0x002746D0
	public event Action OnDialogShown;

	// Token: 0x1400007D RID: 125
	// (add) Token: 0x060078CB RID: 30923 RVA: 0x00276508 File Offset: 0x00274708
	// (remove) Token: 0x060078CC RID: 30924 RVA: 0x00276540 File Offset: 0x00274740
	public event Action OnDialogHidden;

	// Token: 0x060078CD RID: 30925 RVA: 0x00276575 File Offset: 0x00274775
	private void Awake()
	{
		DialogManager.s_instance = this;
	}

	// Token: 0x060078CE RID: 30926 RVA: 0x0027657D File Offset: 0x0027477D
	private void Start()
	{
		LoginManager.Get().OnInitialClientStateReceived += this.HandleSeasonEnd;
		if (DialogManager.OnStarted != null)
		{
			DialogManager.OnStarted();
		}
	}

	// Token: 0x060078CF RID: 30927 RVA: 0x002765A8 File Offset: 0x002747A8
	private void OnDestroy()
	{
		NetCache netCache;
		if (HearthstoneServices.TryGet<NetCache>(out netCache))
		{
			netCache.RemoveNewNoticesListener(new NetCache.DelNewNoticesListener(this.OnNewNotices));
		}
		if (LoginManager.Get() != null)
		{
			LoginManager.Get().OnInitialClientStateReceived -= this.HandleSeasonEnd;
		}
		DialogManager.s_instance = null;
	}

	// Token: 0x060078D0 RID: 30928 RVA: 0x002765F4 File Offset: 0x002747F4
	public void HandleSeasonEnd()
	{
		NetCache.NetCacheProfileNotices netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>();
		if (netObject != null)
		{
			this.MaybeShowSeasonEndDialog(netObject.Notices, false);
		}
		NetCache.Get().RegisterNewNoticesListener(new NetCache.DelNewNoticesListener(this.OnNewNotices));
	}

	// Token: 0x060078D1 RID: 30929 RVA: 0x00276632 File Offset: 0x00274832
	public static DialogManager Get()
	{
		return DialogManager.s_instance;
	}

	// Token: 0x060078D2 RID: 30930 RVA: 0x00276639 File Offset: 0x00274839
	public void GoBack()
	{
		if (this.m_currentDialog)
		{
			this.m_currentDialog.GoBack();
		}
	}

	// Token: 0x060078D3 RID: 30931 RVA: 0x00276653 File Offset: 0x00274853
	public void ReadyForSeasonEndPopup(bool ready)
	{
		this.m_isReadyForSeasonEndPopup = ready;
	}

	// Token: 0x060078D4 RID: 30932 RVA: 0x0027665C File Offset: 0x0027485C
	public void ClearHandledMedalNotices()
	{
		this.m_handledMedalNoticeIDs.Clear();
	}

	// Token: 0x060078D5 RID: 30933 RVA: 0x00276669 File Offset: 0x00274869
	public bool HandleKeyboardInput()
	{
		return InputCollection.GetKeyUp(KeyCode.Escape) && this.m_currentDialog && this.m_currentDialog.HandleKeyboardInput();
	}

	// Token: 0x060078D6 RID: 30934 RVA: 0x00276690 File Offset: 0x00274890
	public bool AddToQueue(DialogManager.DialogRequest request)
	{
		UserAttentionBlocker attentionCategory = (request == null) ? UserAttentionBlocker.NONE : request.m_attentionCategory;
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) || !UserAttentionManager.CanShowAttentionGrabber(attentionCategory, "DialogManager.AddToQueue:" + ((request == null) ? "null" : request.m_type.ToString())))
		{
			return false;
		}
		this.m_dialogRequests.Enqueue(request);
		this.UpdateQueue();
		return true;
	}

	// Token: 0x060078D7 RID: 30935 RVA: 0x002766F4 File Offset: 0x002748F4
	private void UpdateQueue()
	{
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE))
		{
			return;
		}
		if (this.m_currentDialog != null)
		{
			return;
		}
		if (this.m_loadingDialog)
		{
			return;
		}
		if (this.m_dialogRequests.Count == 0)
		{
			return;
		}
		DialogManager.DialogRequest dialogRequest = this.m_dialogRequests.Peek();
		if (!UserAttentionManager.CanShowAttentionGrabber(dialogRequest.m_attentionCategory, "DialogManager.UpdateQueue:" + dialogRequest.m_attentionCategory))
		{
			Processor.ScheduleCallback(0.5f, false, delegate(object userData)
			{
				this.UpdateQueue();
			}, null);
			return;
		}
		this.LoadPopup(dialogRequest);
	}

	// Token: 0x060078D8 RID: 30936 RVA: 0x00276780 File Offset: 0x00274980
	public void ShowPopup(AlertPopup.PopupInfo info, DialogManager.DialogProcessCallback callback, object userData)
	{
		UserAttentionBlocker attentionCategory = (info == null) ? UserAttentionBlocker.NONE : info.m_attentionCategory;
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) || !UserAttentionManager.CanShowAttentionGrabber(attentionCategory, "DialogManager.ShowPopup:" + ((info == null) ? "null" : (info.m_id + ":" + info.m_attentionCategory.ToString()))))
		{
			return;
		}
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.ALERT,
			m_attentionCategory = attentionCategory,
			m_info = info,
			m_callback = callback,
			m_userData = userData
		});
	}

	// Token: 0x060078D9 RID: 30937 RVA: 0x00276811 File Offset: 0x00274A11
	public void ShowPopup(AlertPopup.PopupInfo info, DialogManager.DialogProcessCallback callback)
	{
		this.ShowPopup(info, callback, null);
	}

	// Token: 0x060078DA RID: 30938 RVA: 0x0027681C File Offset: 0x00274A1C
	public void ShowPopup(AlertPopup.PopupInfo info)
	{
		this.ShowPopup(info, null, null);
	}

	// Token: 0x060078DB RID: 30939 RVA: 0x00276828 File Offset: 0x00274A28
	public bool ShowUniquePopup(AlertPopup.PopupInfo info, DialogManager.DialogProcessCallback callback, object userData)
	{
		UserAttentionBlocker attentionCategory = (info == null) ? UserAttentionBlocker.NONE : info.m_attentionCategory;
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) || !UserAttentionManager.CanShowAttentionGrabber(attentionCategory, "DialogManager.ShowUniquePopup:" + ((info == null) ? "null" : (info.m_id + ":" + info.m_attentionCategory.ToString()))))
		{
			return false;
		}
		if (info != null && !string.IsNullOrEmpty(info.m_id))
		{
			foreach (DialogManager.DialogRequest dialogRequest in this.m_dialogRequests)
			{
				if (dialogRequest.m_type == DialogManager.DialogType.ALERT && ((AlertPopup.PopupInfo)dialogRequest.m_info).m_id == info.m_id)
				{
					return false;
				}
			}
		}
		this.ShowPopup(info, callback, userData);
		return true;
	}

	// Token: 0x060078DC RID: 30940 RVA: 0x00276910 File Offset: 0x00274B10
	public bool ShowUniquePopup(AlertPopup.PopupInfo info, DialogManager.DialogProcessCallback callback)
	{
		return this.ShowUniquePopup(info, callback, null);
	}

	// Token: 0x060078DD RID: 30941 RVA: 0x0027691B File Offset: 0x00274B1B
	public bool ShowUniquePopup(AlertPopup.PopupInfo info)
	{
		return this.ShowUniquePopup(info, null, null);
	}

	// Token: 0x060078DE RID: 30942 RVA: 0x00276928 File Offset: 0x00274B28
	public void ShowMessageOfTheDay(string message)
	{
		this.ShowPopup(new AlertPopup.PopupInfo
		{
			m_text = message
		});
	}

	// Token: 0x060078DF RID: 30943 RVA: 0x0027694C File Offset: 0x00274B4C
	public void RemoveUniquePopupRequestFromQueue(string id)
	{
		if (string.IsNullOrEmpty(id))
		{
			return;
		}
		Func<DialogManager.DialogRequest, bool> <>9__0;
		foreach (DialogManager.DialogRequest dialogRequest in this.m_dialogRequests)
		{
			if (dialogRequest.m_type == DialogManager.DialogType.ALERT && ((AlertPopup.PopupInfo)dialogRequest.m_info).m_id == id)
			{
				IEnumerable<DialogManager.DialogRequest> dialogRequests = this.m_dialogRequests;
				Func<DialogManager.DialogRequest, bool> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((DialogManager.DialogRequest r) => r.m_info != null && r.m_info.GetType() == typeof(AlertPopup.PopupInfo) && ((AlertPopup.PopupInfo)r.m_info).m_id != id));
				}
				this.m_dialogRequests = new Queue<DialogManager.DialogRequest>(dialogRequests.Where(predicate));
				break;
			}
		}
	}

	// Token: 0x060078E0 RID: 30944 RVA: 0x00276A10 File Offset: 0x00274C10
	public bool WaitingToShowSeasonEndDialog()
	{
		if (this.m_waitingToShowSeasonEndDialog || (this.m_currentDialog != null && this.m_currentDialog is SeasonEndDialog))
		{
			return true;
		}
		return this.m_dialogRequests.FirstOrDefault((DialogManager.DialogRequest obj) => obj.m_type == DialogManager.DialogType.SEASON_END) != null;
	}

	// Token: 0x060078E1 RID: 30945 RVA: 0x00276A6F File Offset: 0x00274C6F
	public IEnumerator<IAsyncJobResult> Job_WaitForSeasonEndPopup()
	{
		this.ReadyForSeasonEndPopup(true);
		while (this.WaitingToShowSeasonEndDialog())
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x060078E2 RID: 30946 RVA: 0x00276A80 File Offset: 0x00274C80
	public void ShowFriendlyChallenge(FormatType formatType, BnetPlayer challenger, bool challengeIsTavernBrawl, PartyType partyType, FriendlyChallengeDialog.ResponseCallback responseCallback, DialogManager.DialogProcessCallback callback)
	{
		DialogManager.DialogRequest dialogRequest = new DialogManager.DialogRequest();
		if (challengeIsTavernBrawl)
		{
			dialogRequest.m_type = DialogManager.DialogType.TAVERN_BRAWL_CHALLENGE;
		}
		else if (partyType == PartyType.BATTLEGROUNDS_PARTY)
		{
			dialogRequest.m_type = DialogManager.DialogType.BACON_CHALLENGE;
		}
		else
		{
			dialogRequest.m_type = DialogManager.DialogType.FRIENDLY_CHALLENGE;
		}
		dialogRequest.m_info = new FriendlyChallengeDialog.Info
		{
			m_formatType = formatType,
			m_challenger = challenger,
			m_partyType = partyType,
			m_callback = responseCallback
		};
		dialogRequest.m_callback = callback;
		this.AddToQueue(dialogRequest);
	}

	// Token: 0x060078E3 RID: 30947 RVA: 0x00276AF0 File Offset: 0x00274CF0
	public void ShowPrivacyPolicyPopup(PrivacyPolicyPopup.ResponseCallback responseCallback, DialogManager.DialogProcessCallback callback)
	{
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.PRIVACY_POLICY,
			m_info = new PrivacyPolicyPopup.Info
			{
				m_callback = responseCallback
			},
			m_callback = callback
		});
	}

	// Token: 0x060078E4 RID: 30948 RVA: 0x00276B30 File Offset: 0x00274D30
	public void ShowExistingAccountPopup(ExistingAccountPopup.ResponseCallback responseCallback, DialogManager.DialogProcessCallback callback)
	{
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.EXISTING_ACCOUNT,
			m_info = new ExistingAccountPopup.Info
			{
				m_callback = responseCallback
			},
			m_callback = callback
		});
	}

	// Token: 0x060078E5 RID: 30949 RVA: 0x00276B70 File Offset: 0x00274D70
	public void ShowTavernBrawlChoiceDialog(FiresideBrawlChoiceDialog.ResponseCallback callback)
	{
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.TAVERN_BRAWL_CHOICE,
			m_info = new FiresideBrawlChoiceDialog.Info
			{
				m_callback = callback
			},
			m_callback = null
		});
	}

	// Token: 0x060078E6 RID: 30950 RVA: 0x00276BB0 File Offset: 0x00274DB0
	public void ShowFiresideOKDialog()
	{
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.FIRESIDE_BRAWL_OK
		});
	}

	// Token: 0x060078E7 RID: 30951 RVA: 0x00276BD4 File Offset: 0x00274DD4
	public void ShowFiresideGatheringNearbyDialog(FiresideGatheringJoinDialog.ResponseCallback callback)
	{
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.FIRESIDE_GATHERING_JOIN,
			m_info = new FiresideGatheringJoinDialog.Info
			{
				m_callback = callback
			},
			m_callback = null
		});
	}

	// Token: 0x060078E8 RID: 30952 RVA: 0x00276C14 File Offset: 0x00274E14
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

	// Token: 0x060078E9 RID: 30953 RVA: 0x00276C74 File Offset: 0x00274E74
	public void ShowFiresideGatheringLocationHelperDialog(Action callback)
	{
		FiresideGatheringLocationHelperDialog.Info info = this.CreateFiresideGatheringLocationHelperDialogInfo(callback);
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.FIRESIDE_LOCATION_HELPER,
			m_info = info,
			m_callback = null
		});
	}

	// Token: 0x060078EA RID: 30954 RVA: 0x00276CB0 File Offset: 0x00274EB0
	public void ShowFiresideGatheringCheckInFailedDialog()
	{
		FiresideGatheringLocationHelperDialog.Info info = this.CreateFiresideGatheringLocationHelperDialogInfo(null);
		info.m_isCheckInFailure = true;
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.FIRESIDE_LOCATION_HELPER,
			m_info = info,
			m_callback = null
		});
	}

	// Token: 0x060078EB RID: 30955 RVA: 0x00276CF0 File Offset: 0x00274EF0
	public void ShowFiresideGatheringInnkeeperSetupHelperDialog(Action callback)
	{
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.FIRESIDE_LOCATION_HELPER,
			m_info = new FiresideGatheringLocationHelperDialog.Info
			{
				m_callback = callback,
				m_isInnkeeperSetup = true,
				m_gpsOffIntroText = GameStrings.Get("GLUE_FIRESIDE_GATHERING_INNKEEPER_TURN_ON_GPS_BODY"),
				m_wifiOffIntroText = GameStrings.Get("GLUE_FIRESIDE_GATHERING_INNKEEPER_TURN_ON_WIFI_BODY"),
				m_waitingForWifiText = GameStrings.Get("GLUE_FIRESIDE_GATHERING_INKEEPER_CONNECT_TO_WIFI_BODY"),
				m_wifiConfirmText = GameStrings.Get("GLUE_FIRESIDE_GATHERING_INNKEEPER_CONNECT_TO_WIFI_SSID_CONFIRM_TITLE")
			},
			m_callback = null
		});
	}

	// Token: 0x060078EC RID: 30956 RVA: 0x00276D78 File Offset: 0x00274F78
	public void ShowFiresideGatheringFindEventDialog(FiresideGatheringFindEventDialog.ResponseCallback callback)
	{
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.FIRESIDE_FIND_EVENT,
			m_info = new FiresideGatheringFindEventDialog.Info
			{
				m_callback = callback
			},
			m_callback = null
		});
	}

	// Token: 0x060078ED RID: 30957 RVA: 0x00276DB8 File Offset: 0x00274FB8
	public void ShowFiresideGatheringInnkeeperSetupDialog(FiresideGatheringInnkeeperSetupDialog.ResponseCallback callback, string tavernName)
	{
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.FIRESIDE_INNKEEPER_SETUP,
			m_info = new FiresideGatheringInnkeeperSetupDialog.Info
			{
				m_callback = callback,
				m_tavernName = tavernName
			},
			m_callback = null
		});
	}

	// Token: 0x060078EE RID: 30958 RVA: 0x00276E00 File Offset: 0x00275000
	public void ShowLeaguePromoteSelfManuallyDialog(LeaguePromoteSelfManuallyDialog.ResponseCallback callback)
	{
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.LEAGUE_PROMOTE_SELF_MANUALLY,
			m_info = new LeaguePromoteSelfManuallyDialog.Info
			{
				m_callback = callback
			},
			m_callback = null
		});
	}

	// Token: 0x060078EF RID: 30959 RVA: 0x00276E40 File Offset: 0x00275040
	public void ShowCardListPopup(UserAttentionBlocker attentionCategory, CardListPopup.Info info)
	{
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.CARD_LIST,
			m_attentionCategory = attentionCategory,
			m_info = info
		});
	}

	// Token: 0x060078F0 RID: 30960 RVA: 0x00276E70 File Offset: 0x00275070
	public void ShowStandardComingSoonPopup(UserAttentionBlocker attentionCategory, BasicPopup.PopupInfo info)
	{
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.STANDARD_COMING_SOON,
			m_attentionCategory = attentionCategory,
			m_info = info
		});
	}

	// Token: 0x060078F1 RID: 30961 RVA: 0x00276EA0 File Offset: 0x002750A0
	public void ShowSetRotationTutorialPopup(UserAttentionBlocker attentionCategory, SetRotationRotatedBoostersPopup.SetRotationRotatedBoostersPopupInfo info)
	{
		info.m_prefabAssetRefs.Add(new AssetReference("SetRotationRotatedBoostersPopup.prefab:2a1c1ce78c98c1e418039a479c8ddce4"));
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.GENERIC_BASIC_POPUP,
			m_attentionCategory = attentionCategory,
			m_info = info,
			m_isWidget = true
		});
	}

	// Token: 0x060078F2 RID: 30962 RVA: 0x00276EF4 File Offset: 0x002750F4
	public void ShowOutstandingDraftTicketPopup(UserAttentionBlocker attentionCategory, OutstandingDraftTicketDialog.Info info)
	{
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.OUTSTANDING_DRAFT_TICKETS,
			m_attentionCategory = attentionCategory,
			m_info = info
		});
	}

	// Token: 0x060078F3 RID: 30963 RVA: 0x00276F28 File Offset: 0x00275128
	public void ShowFreeArenaWinPopup(UserAttentionBlocker attentionCategory, FreeArenaWinDialog.Info info)
	{
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.FREE_ARENA_WIN,
			m_attentionCategory = attentionCategory,
			m_info = info
		});
	}

	// Token: 0x060078F4 RID: 30964 RVA: 0x00276F5C File Offset: 0x0027515C
	public bool ShowArenaSeasonPopup(UserAttentionBlocker attentionCategory, BasicPopup.PopupInfo info)
	{
		return this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.ARENA_SEASON,
			m_attentionCategory = attentionCategory,
			m_info = info
		});
	}

	// Token: 0x060078F5 RID: 30965 RVA: 0x00276F8C File Offset: 0x0027518C
	public void ShowLoginPopupSequenceBasicPopup(UserAttentionBlocker attentionCategory, LoginPopupSequencePopup.Info info)
	{
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.LOGIN_POPUP_SEQUENCE_BASIC,
			m_attentionCategory = attentionCategory,
			m_info = info,
			m_prefabAssetReferenceOverride = info.m_prefabAssetReference
		});
	}

	// Token: 0x060078F6 RID: 30966 RVA: 0x00276FCC File Offset: 0x002751CC
	public void ShowMultiPagePopup(UserAttentionBlocker attentionCategory, MultiPagePopup.Info info)
	{
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.MULTI_PAGE_POPUP,
			m_attentionCategory = attentionCategory,
			m_info = info,
			m_prefabAssetReferenceOverride = "MultiPagePopup.prefab:a9b6df0282662ed449031d34aa2ecfa7"
		});
	}

	// Token: 0x060078F7 RID: 30967 RVA: 0x00277008 File Offset: 0x00275208
	public bool ShowBasicPopup(UserAttentionBlocker attentionCategory, BasicPopup.PopupInfo info)
	{
		return this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.GENERIC_BASIC_POPUP,
			m_attentionCategory = attentionCategory,
			m_info = info
		});
	}

	// Token: 0x060078F8 RID: 30968 RVA: 0x00277038 File Offset: 0x00275238
	public bool ShowAssetDownloadPopup(AssetDownloadDialog.Info info)
	{
		return this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.ASSET_DOWNLOAD,
			m_attentionCategory = UserAttentionBlocker.NONE,
			m_info = info
		});
	}

	// Token: 0x060078F9 RID: 30969 RVA: 0x00277068 File Offset: 0x00275268
	public void ShowReconnectHelperDialog(Action reconnectSuccessCallback = null, Action goBackCallback = null)
	{
		this.AddToQueue(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.RECONNECT_HELPER,
			m_info = new ReconnectHelperDialog.Info
			{
				m_reconnectSuccessCallback = reconnectSuccessCallback,
				m_goBackCallback = goBackCallback
			},
			m_callback = null
		});
	}

	// Token: 0x060078FA RID: 30970 RVA: 0x002770B0 File Offset: 0x002752B0
	public void ShowGameModesPopup(UIEvent.Handler onArenaButtonReleased, UIEvent.Handler onBaconButtonReleased, UIEvent.Handler onPvPDungeonRunButtonReleased)
	{
		DialogManager.DialogRequest dialogRequest = new DialogManager.DialogRequest();
		dialogRequest.m_type = DialogManager.DialogType.GAME_MODES;
		GameModesPopup.Info info = new GameModesPopup.Info
		{
			m_onArenaButtonReleased = onArenaButtonReleased,
			m_onBaconButtonReleased = onBaconButtonReleased
		};
		dialogRequest.m_info = info;
		dialogRequest.m_callback = null;
		this.AddToQueue(dialogRequest);
	}

	// Token: 0x060078FB RID: 30971 RVA: 0x002770F8 File Offset: 0x002752F8
	public void ShowClassUpcomingPopup()
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_headerText = GameStrings.Get("GLUE_CLASS_UPCOMING_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_CLASS_UPCOMING_DESC");
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x060078FC RID: 30972 RVA: 0x0027714C File Offset: 0x0027534C
	public void ShowBonusStarsPopup(RankedPlayDataModel dataModel, Action onHiddenCallback)
	{
		RankedBonusStarsPopup.BonusStarsPopupInfo bonusStarsPopupInfo = new RankedBonusStarsPopup.BonusStarsPopupInfo
		{
			m_onHiddenCallback = onHiddenCallback
		};
		DialogManager.DialogRequest request = new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.GENERIC_BASIC_POPUP,
			m_dataModel = dataModel,
			m_info = bonusStarsPopupInfo,
			m_isWidget = true
		};
		bonusStarsPopupInfo.m_prefabAssetRefs.Add(RankMgr.BONUS_STAR_POPUP_PREFAB);
		this.AddToQueue(request);
	}

	// Token: 0x060078FD RID: 30973 RVA: 0x002771A8 File Offset: 0x002753A8
	public void ShowRankedIntroPopUp(Action onHiddenCallback)
	{
		RankedIntroPopup.RankedIntroPopupInfo rankedIntroPopupInfo = new RankedIntroPopup.RankedIntroPopupInfo
		{
			m_onHiddenCallback = onHiddenCallback
		};
		DialogManager.DialogRequest request = new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.GENERIC_BASIC_POPUP,
			m_info = rankedIntroPopupInfo,
			m_isWidget = true
		};
		rankedIntroPopupInfo.m_prefabAssetRefs.Add(RankMgr.RANKED_INTRO_POPUP_PREFAB);
		this.AddToQueue(request);
	}

	// Token: 0x060078FE RID: 30974 RVA: 0x002771FB File Offset: 0x002753FB
	public void ClearAllImmediately()
	{
		if (this.m_currentDialog != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_currentDialog.gameObject);
			this.m_currentDialog = null;
		}
		this.m_dialogRequests.Clear();
	}

	// Token: 0x060078FF RID: 30975 RVA: 0x0027722D File Offset: 0x0027542D
	public bool ShowingDialog()
	{
		return this.m_currentDialog != null || this.m_dialogRequests.Count > 0;
	}

	// Token: 0x06007900 RID: 30976 RVA: 0x0027724D File Offset: 0x0027544D
	public bool ShowingHighPriorityDialog()
	{
		return this.m_currentDialog != null && this.m_currentDialog.gameObject.layer == 27;
	}

	// Token: 0x06007901 RID: 30977 RVA: 0x00277273 File Offset: 0x00275473
	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		this.MaybeShowSeasonEndDialog(newNotices, !isInitialNoticeList);
	}

	// Token: 0x06007902 RID: 30978 RVA: 0x00277280 File Offset: 0x00275480
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
			if (a.OriginData != b.OriginData)
			{
				return (int)(a.OriginData - b.OriginData);
			}
			return (int)(a.NoticeID - b.NoticeID);
		});
		NetCache.ProfileNotice profileNotice = this.MaybeShowSeasonEndDialog_GetLatestMedalNotice(newNotices);
		if (profileNotice == null)
		{
			return;
		}
		NetCache.ProfileNoticeMedal profileNoticeMedal = profileNotice as NetCache.ProfileNoticeMedal;
		if (profileNoticeMedal == null)
		{
			return;
		}
		if (this.m_handledMedalNoticeIDs.Contains(profileNoticeMedal.NoticeID))
		{
			return;
		}
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) || !UserAttentionManager.CanShowAttentionGrabber("DialogManager.MaybeShowSeasonEndDialog"))
		{
			return;
		}
		this.m_handledMedalNoticeIDs.Add(profileNoticeMedal.NoticeID);
		if (ReturningPlayerMgr.Get().SuppressOldPopups)
		{
			global::Log.ReturningPlayer.Print("Suppressing popup for Season End Dialogue {0} due to being a Returning Player!", Array.Empty<object>());
			Network.Get().AckNotice(profileNoticeMedal.NoticeID);
			return;
		}
		if (fromOutOfBandNotice)
		{
			NetCache.Get().RefreshNetObject<NetCache.NetCacheMedalInfo>();
			NetCache.Get().ReloadNetObject<NetCache.NetCacheRewardProgress>();
		}
		DialogManager.SeasonEndDialogRequestInfo seasonEndDialogRequestInfo = new DialogManager.SeasonEndDialogRequestInfo();
		seasonEndDialogRequestInfo.m_noticeMedal = profileNoticeMedal;
		base.StartCoroutine(this.ShowSeasonEndDialogWhenReady(new DialogManager.DialogRequest
		{
			m_type = DialogManager.DialogType.SEASON_END,
			m_info = seasonEndDialogRequestInfo
		}));
	}

	// Token: 0x06007903 RID: 30979 RVA: 0x0027737C File Offset: 0x0027557C
	private NetCache.ProfileNotice MaybeShowSeasonEndDialog_GetLatestMedalNotice(List<NetCache.ProfileNotice> newNotices)
	{
		List<NetCache.ProfileNotice> source = new List<NetCache.ProfileNotice>(newNotices);
		IEnumerable<NetCache.ProfileNotice> source2 = from notice in source
		where notice.Type == NetCache.ProfileNotice.NoticeType.GAINED_MEDAL
		select notice;
		IEnumerable<NetCache.ProfileNotice> enumerable = from notice in source
		where notice.Type == NetCache.ProfileNotice.NoticeType.BONUS_STARS
		select notice;
		if (source2.Any<NetCache.ProfileNotice>())
		{
			long val = 52L;
			long maxSeason = Math.Max(val, source2.Max((NetCache.ProfileNotice n) => n.OriginData));
			(from notice in source2
			where notice.OriginData != maxSeason
			select notice).ForEach(delegate(NetCache.ProfileNotice notice)
			{
				Network.Get().AckNotice(notice.NoticeID);
			});
			source2 = from notice in source2
			where notice.OriginData == maxSeason
			select notice;
			source2.Skip(1).ForEach(delegate(NetCache.ProfileNotice notice)
			{
				Network.Get().AckNotice(notice.NoticeID);
			});
		}
		enumerable.ForEach(delegate(NetCache.ProfileNotice notice)
		{
			Network.Get().AckNotice(notice.NoticeID);
		});
		return source2.FirstOrDefault<NetCache.ProfileNotice>();
	}

	// Token: 0x06007904 RID: 30980 RVA: 0x002774C0 File Offset: 0x002756C0
	private void LoadPopup(DialogManager.DialogRequest request)
	{
		List<string> list;
		if (request.m_info is BasicPopup.PopupInfo)
		{
			list = ((BasicPopup.PopupInfo)request.m_info).m_prefabAssetRefs;
		}
		else
		{
			list = new List<string>();
			string prefabNameFromDialogRequest = this.GetPrefabNameFromDialogRequest(request);
			list.Add(prefabNameFromDialogRequest);
		}
		if (list == null || list.Count == 0 || string.IsNullOrEmpty(list[0]))
		{
			global::Error.AddDevFatal("DialogManager.LoadPopup() - no prefab to load for type={0} info={1} attnCategory={2} prefabName={3}", new object[]
			{
				request.m_type,
				request.m_info,
				request.m_attentionCategory,
				(list == null) ? "<null>" : ((list.Count == 0) ? "<empty>" : (list[0] ?? "null"))
			});
			return;
		}
		list.RemoveAll((string assetRef) => string.IsNullOrEmpty(assetRef));
		this.m_loadingDialog = true;
		DialogManager.PopupCallbackSharedData popupCallbackSharedData = new DialogManager.PopupCallbackSharedData(list.Count);
		for (int i = 0; i < list.Count; i++)
		{
			popupCallbackSharedData.m_loadedPrefabs.Add(null);
		}
		for (int j = 0; j < list.Count; j++)
		{
			DialogManager.PopupCallbackData popupCallbackData = new DialogManager.PopupCallbackData(popupCallbackSharedData, j);
			if (request.m_isWidget)
			{
				WidgetInstance widgetInstance = WidgetInstance.Create(list[j], false);
				if (request.m_dataModel != null)
				{
					widgetInstance.BindDataModel(request.m_dataModel, false);
				}
				base.StartCoroutine(this.WaitForWidgetPopupReady(list[j], widgetInstance, popupCallbackData));
			}
			else
			{
				AssetLoader.Get().InstantiatePrefab(list[j], new PrefabCallback<GameObject>(this.OnPopupLoaded), popupCallbackData, AssetLoadingOptions.None);
			}
		}
	}

	// Token: 0x06007905 RID: 30981 RVA: 0x0027767C File Offset: 0x0027587C
	private string GetPrefabNameFromDialogRequest(DialogManager.DialogRequest request)
	{
		if (!string.IsNullOrEmpty(request.m_prefabAssetReferenceOverride))
		{
			return request.m_prefabAssetReferenceOverride;
		}
		DialogManager.DialogTypeMapping dialogTypeMapping = this.m_typeMapping.Find((DialogManager.DialogTypeMapping x) => x.m_type == request.m_type);
		if (dialogTypeMapping == null || dialogTypeMapping.m_prefabName == null)
		{
			global::Error.AddDevFatal("DialogManager.GetPrefabNameFromDialogRequest() - unhandled dialog type {0}", new object[]
			{
				request.m_type
			});
			return null;
		}
		return dialogTypeMapping.m_prefabName;
	}

	// Token: 0x06007906 RID: 30982 RVA: 0x00277702 File Offset: 0x00275902
	private IEnumerator WaitForWidgetPopupReady(AssetReference assetRef, WidgetInstance widgetInstance, object callbackData)
	{
		if (widgetInstance == null)
		{
			yield break;
		}
		widgetInstance.Hide();
		while (!widgetInstance.IsReady || widgetInstance.IsChangingStates)
		{
			yield return null;
		}
		this.OnPopupLoaded(assetRef, widgetInstance.gameObject, callbackData);
		widgetInstance.Show();
		yield break;
	}

	// Token: 0x06007907 RID: 30983 RVA: 0x00277728 File Offset: 0x00275928
	private void OnPopupLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		DialogManager.DialogRequest dialogRequest = (this.m_dialogRequests.Count == 0) ? null : this.m_dialogRequests.Peek();
		UserAttentionBlocker attentionCategory = (dialogRequest == null) ? UserAttentionBlocker.NONE : dialogRequest.m_attentionCategory;
		if (this.m_dialogRequests.Count == 0 || UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) || !UserAttentionManager.CanShowAttentionGrabber(attentionCategory, "DialogManager.OnPopupLoaded:" + ((dialogRequest == null) ? "null" : dialogRequest.m_type.ToString())))
		{
			this.m_loadingDialog = false;
			UnityEngine.Object.DestroyImmediate(go);
			return;
		}
		DialogManager.PopupCallbackData popupCallbackData = (DialogManager.PopupCallbackData)callbackData;
		popupCallbackData.m_sharedData.m_loadedPrefabs[popupCallbackData.m_index] = go;
		DialogManager.PopupCallbackSharedData sharedData = popupCallbackData.m_sharedData;
		int num = sharedData.m_remainingToLoad - 1;
		sharedData.m_remainingToLoad = num;
		if (num > 0)
		{
			return;
		}
		this.m_loadingDialog = false;
		dialogRequest = this.m_dialogRequests.Dequeue();
		GameObject gameObject = (popupCallbackData.m_sharedData.m_loadedPrefabs.Count == 0) ? null : popupCallbackData.m_sharedData.m_loadedPrefabs[0];
		DialogBase dialogBase = (gameObject == null) ? null : gameObject.GetComponentInChildren<DialogBase>();
		if (dialogBase == null)
		{
			Debug.LogError(string.Format("DialogManager.OnPopupLoaded() - game object {0} has no DialogBase component (request_type={1} count prefabs loaded={2})", go, dialogRequest.m_type, popupCallbackData.m_sharedData.m_loadedPrefabs.Count));
			UnityEngine.Object.DestroyImmediate(go);
			this.UpdateQueue();
			return;
		}
		for (int i = 1; i < popupCallbackData.m_sharedData.m_loadedPrefabs.Count; i++)
		{
			GameObject gameObject2 = popupCallbackData.m_sharedData.m_loadedPrefabs[i];
			if (!(gameObject2 == null))
			{
				gameObject2.transform.SetParent(gameObject.transform, false);
			}
		}
		this.ProcessRequest(dialogRequest, dialogBase);
	}

	// Token: 0x06007908 RID: 30984 RVA: 0x002778D8 File Offset: 0x00275AD8
	private void ProcessRequest(DialogManager.DialogRequest request, DialogBase dialog)
	{
		if (request.m_callback != null && !request.m_callback(dialog, request.m_userData))
		{
			this.UpdateQueue();
			UnityEngine.Object.Destroy(dialog.gameObject);
			return;
		}
		this.m_currentDialog = dialog;
		this.m_currentDialog.SetReadyToDestroyCallback(new DialogBase.ReadyToDestroyCallback(this.OnCurrentDialogHidden));
		if (request.m_type == DialogManager.DialogType.ALERT)
		{
			this.ProcessAlertRequest(request, (AlertPopup)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.SEASON_END)
		{
			this.ProcessMedalRequest(request, (SeasonEndDialog)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.FRIENDLY_CHALLENGE || request.m_type == DialogManager.DialogType.TAVERN_BRAWL_CHALLENGE || request.m_type == DialogManager.DialogType.BACON_CHALLENGE)
		{
			this.ProcessFriendlyChallengeRequest(request, (FriendlyChallengeDialog)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.EXISTING_ACCOUNT)
		{
			this.ProcessExistingAccountRequest(request, (ExistingAccountPopup)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.CARD_LIST)
		{
			this.ProcessCardListRequest(request, (CardListPopup)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.TAVERN_BRAWL_CHOICE)
		{
			this.ProcessFiresideBrawlChoiceRequest(request, (FiresideBrawlChoiceDialog)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.FIRESIDE_BRAWL_OK)
		{
			this.ProcessFiresideBrawlOkRequest(request, (FiresideBrawlOkDialog)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.FIRESIDE_GATHERING_JOIN)
		{
			this.ProcessFiresideGatheringNearbyRequest(request, (FiresideGatheringJoinDialog)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.FIRESIDE_FIND_EVENT)
		{
			this.ProcessFiresideGatheringFindEventRequest(request, (FiresideGatheringFindEventDialog)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.FIRESIDE_LOCATION_HELPER)
		{
			this.ProcessFiresideGatheringLocationHelperRequest(request, (FiresideGatheringLocationHelperDialog)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.FIRESIDE_INNKEEPER_SETUP)
		{
			this.ProcessFiresideGatheringInnkeeperSetupRequest(request, (FiresideGatheringInnkeeperSetupDialog)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.LEAGUE_PROMOTE_SELF_MANUALLY)
		{
			this.ProcessLeaguePromoteSelfManuallyRequest(request, (LeaguePromoteSelfManuallyDialog)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.OUTSTANDING_DRAFT_TICKETS)
		{
			this.ProcessOutstandingDraftTicketDialog(request, (OutstandingDraftTicketDialog)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.FREE_ARENA_WIN)
		{
			this.ProcessFreeArenaWinDialog(request, (FreeArenaWinDialog)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.GENERIC_BASIC_POPUP || request.m_type == DialogManager.DialogType.STANDARD_COMING_SOON || request.m_type == DialogManager.DialogType.ARENA_SEASON)
		{
			this.ProcessBasicPopupRequest(request, (BasicPopup)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.ASSET_DOWNLOAD)
		{
			this.ProcessAssetDownloadRequest(request, (AssetDownloadDialog)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.RECONNECT_HELPER)
		{
			this.ProcessReconnectRequest(request, (ReconnectHelperDialog)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.LOGIN_POPUP_SEQUENCE_BASIC)
		{
			this.ProcessLoginPopupSequenceBasicPopupRequest(request, (LoginPopupSequencePopup)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.MULTI_PAGE_POPUP)
		{
			this.ProcessMultiPagePopupRequest(request, (MultiPagePopup)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.GAME_MODES)
		{
			this.ProcessGameModesPopupRequest(request, (GameModesPopup)dialog);
		}
		else if (request.m_type == DialogManager.DialogType.PRIVACY_POLICY)
		{
			this.ProcessPrivacyPolicyRequest(request, (PrivacyPolicyPopup)dialog);
		}
		if (this.OnDialogShown != null)
		{
			this.OnDialogShown();
		}
	}

	// Token: 0x06007909 RID: 30985 RVA: 0x00277B9C File Offset: 0x00275D9C
	private void ProcessExistingAccountRequest(DialogManager.DialogRequest request, ExistingAccountPopup exAcctPopup)
	{
		exAcctPopup.SetInfo((ExistingAccountPopup.Info)request.m_info);
		exAcctPopup.Show();
	}

	// Token: 0x0600790A RID: 30986 RVA: 0x00277BB8 File Offset: 0x00275DB8
	private void ProcessAlertRequest(DialogManager.DialogRequest request, AlertPopup alertPopup)
	{
		AlertPopup.PopupInfo info = (AlertPopup.PopupInfo)request.m_info;
		alertPopup.SetInfo(info);
		alertPopup.Show();
	}

	// Token: 0x0600790B RID: 30987 RVA: 0x00277BE0 File Offset: 0x00275DE0
	private void ProcessFiresideBrawlChoiceRequest(DialogManager.DialogRequest request, FiresideBrawlChoiceDialog choicePopup)
	{
		FiresideBrawlChoiceDialog.Info info = (FiresideBrawlChoiceDialog.Info)request.m_info;
		choicePopup.SetInfo(info);
		choicePopup.Show();
	}

	// Token: 0x0600790C RID: 30988 RVA: 0x00277C06 File Offset: 0x00275E06
	private void ProcessFiresideBrawlOkRequest(DialogManager.DialogRequest request, FiresideBrawlOkDialog okPopup)
	{
		okPopup.Show();
	}

	// Token: 0x0600790D RID: 30989 RVA: 0x00277C10 File Offset: 0x00275E10
	private void ProcessFiresideGatheringNearbyRequest(DialogManager.DialogRequest request, FiresideGatheringJoinDialog choicePopup)
	{
		FiresideGatheringJoinDialog.Info info = (FiresideGatheringJoinDialog.Info)request.m_info;
		choicePopup.SetInfo(info);
		choicePopup.Show();
	}

	// Token: 0x0600790E RID: 30990 RVA: 0x00277C38 File Offset: 0x00275E38
	private void ProcessFiresideGatheringFindEventRequest(DialogManager.DialogRequest request, FiresideGatheringFindEventDialog choicePopup)
	{
		FiresideGatheringFindEventDialog.Info info = (FiresideGatheringFindEventDialog.Info)request.m_info;
		choicePopup.SetInfo(info);
		choicePopup.Show();
	}

	// Token: 0x0600790F RID: 30991 RVA: 0x00277C60 File Offset: 0x00275E60
	private void ProcessFiresideGatheringInnkeeperSetupRequest(DialogManager.DialogRequest request, FiresideGatheringInnkeeperSetupDialog choicePopup)
	{
		FiresideGatheringInnkeeperSetupDialog.Info info = (FiresideGatheringInnkeeperSetupDialog.Info)request.m_info;
		choicePopup.SetInfo(info);
		choicePopup.Show();
	}

	// Token: 0x06007910 RID: 30992 RVA: 0x00277C88 File Offset: 0x00275E88
	private void ProcessFiresideGatheringLocationHelperRequest(DialogManager.DialogRequest request, FiresideGatheringLocationHelperDialog fsgLocationHelperPopup)
	{
		FiresideGatheringLocationHelperDialog.Info info = (FiresideGatheringLocationHelperDialog.Info)request.m_info;
		fsgLocationHelperPopup.SetInfo(info);
		fsgLocationHelperPopup.Show();
	}

	// Token: 0x06007911 RID: 30993 RVA: 0x00277CB0 File Offset: 0x00275EB0
	private void ProcessBasicPopupRequest(DialogManager.DialogRequest request, BasicPopup basicPopup)
	{
		BasicPopup.PopupInfo info = (BasicPopup.PopupInfo)request.m_info;
		basicPopup.SetInfo(info);
		basicPopup.Show();
	}

	// Token: 0x06007912 RID: 30994 RVA: 0x00277C06 File Offset: 0x00275E06
	private void ProcessAssetDownloadRequest(DialogManager.DialogRequest request, AssetDownloadDialog dialog)
	{
		dialog.Show();
	}

	// Token: 0x06007913 RID: 30995 RVA: 0x00277CD6 File Offset: 0x00275ED6
	private void ProcessReconnectRequest(DialogManager.DialogRequest request, ReconnectHelperDialog dialog)
	{
		dialog.SetInfo((ReconnectHelperDialog.Info)request.m_info);
		dialog.Show();
	}

	// Token: 0x06007914 RID: 30996 RVA: 0x00277CF0 File Offset: 0x00275EF0
	private void ProcessMedalRequest(DialogManager.DialogRequest request, SeasonEndDialog seasonEndDialog)
	{
		SeasonEndDialog.SeasonEndInfo seasonEndInfo;
		if (request.m_isFake)
		{
			seasonEndInfo = (request.m_info as SeasonEndDialog.SeasonEndInfo);
			if (seasonEndInfo == null)
			{
				return;
			}
		}
		else
		{
			DialogManager.SeasonEndDialogRequestInfo seasonEndDialogRequestInfo = request.m_info as DialogManager.SeasonEndDialogRequestInfo;
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

	// Token: 0x06007915 RID: 30997 RVA: 0x00277E0E File Offset: 0x0027600E
	private void ProcessFriendlyChallengeRequest(DialogManager.DialogRequest request, FriendlyChallengeDialog friendlyChallengeDialog)
	{
		friendlyChallengeDialog.SetInfo((FriendlyChallengeDialog.Info)request.m_info);
		friendlyChallengeDialog.Show();
	}

	// Token: 0x06007916 RID: 30998 RVA: 0x00277E28 File Offset: 0x00276028
	private void ProcessCardListRequest(DialogManager.DialogRequest request, CardListPopup cardListPopup)
	{
		CardListPopup.Info info = (CardListPopup.Info)request.m_info;
		cardListPopup.SetInfo(info);
		cardListPopup.Show();
	}

	// Token: 0x06007917 RID: 30999 RVA: 0x00277E50 File Offset: 0x00276050
	private void ProcessSetRotationRotatedBoostersPopupRequest(DialogManager.DialogRequest request, SetRotationRotatedBoostersPopup setRotationTutorialDialog)
	{
		SetRotationRotatedBoostersPopup.SetRotationRotatedBoostersPopupInfo info = (SetRotationRotatedBoostersPopup.SetRotationRotatedBoostersPopupInfo)request.m_info;
		setRotationTutorialDialog.SetInfo(info);
		setRotationTutorialDialog.Show();
	}

	// Token: 0x06007918 RID: 31000 RVA: 0x00277E78 File Offset: 0x00276078
	private void ProcessLeaguePromoteSelfManuallyRequest(DialogManager.DialogRequest request, LeaguePromoteSelfManuallyDialog leaguePromoteSelfManuallyDialog)
	{
		LeaguePromoteSelfManuallyDialog.Info info = (LeaguePromoteSelfManuallyDialog.Info)request.m_info;
		leaguePromoteSelfManuallyDialog.SetInfo(info);
		leaguePromoteSelfManuallyDialog.Show();
	}

	// Token: 0x06007919 RID: 31001 RVA: 0x00277EA0 File Offset: 0x002760A0
	private void ProcessOutstandingDraftTicketDialog(DialogManager.DialogRequest request, OutstandingDraftTicketDialog outstandingDraftTicketDialog)
	{
		OutstandingDraftTicketDialog.Info info = (OutstandingDraftTicketDialog.Info)request.m_info;
		outstandingDraftTicketDialog.SetInfo(info);
		outstandingDraftTicketDialog.Show();
	}

	// Token: 0x0600791A RID: 31002 RVA: 0x00277EC8 File Offset: 0x002760C8
	private void ProcessFreeArenaWinDialog(DialogManager.DialogRequest request, FreeArenaWinDialog freeArenaWinDialog)
	{
		FreeArenaWinDialog.Info info = (FreeArenaWinDialog.Info)request.m_info;
		freeArenaWinDialog.SetInfo(info);
		freeArenaWinDialog.Show();
	}

	// Token: 0x0600791B RID: 31003 RVA: 0x00277EF0 File Offset: 0x002760F0
	private void ProcessLoginPopupSequenceBasicPopupRequest(DialogManager.DialogRequest request, LoginPopupSequencePopup loginPopupSequencePopup)
	{
		LoginPopupSequencePopup.Info info = (LoginPopupSequencePopup.Info)request.m_info;
		loginPopupSequencePopup.SetInfo(info);
		loginPopupSequencePopup.LoadAssetsAndShowWhenReady();
	}

	// Token: 0x0600791C RID: 31004 RVA: 0x00277F18 File Offset: 0x00276118
	private void ProcessGameModesPopupRequest(DialogManager.DialogRequest request, GameModesPopup gameModesPopup)
	{
		GameModesPopup.Info info = (GameModesPopup.Info)request.m_info;
		gameModesPopup.SetInfo(info);
		gameModesPopup.Show();
	}

	// Token: 0x0600791D RID: 31005 RVA: 0x00277F3E File Offset: 0x0027613E
	private void ProcessPrivacyPolicyRequest(DialogManager.DialogRequest request, PrivacyPolicyPopup privacyPolicyPopup)
	{
		privacyPolicyPopup.SetInfo((PrivacyPolicyPopup.Info)request.m_info);
		privacyPolicyPopup.Show();
	}

	// Token: 0x0600791E RID: 31006 RVA: 0x00277F58 File Offset: 0x00276158
	private void ProcessMultiPagePopupRequest(DialogManager.DialogRequest request, MultiPagePopup multiPagePopup)
	{
		MultiPagePopup.Info info = (MultiPagePopup.Info)request.m_info;
		multiPagePopup.SetInfo(info);
		multiPagePopup.Show();
	}

	// Token: 0x0600791F RID: 31007 RVA: 0x00277F80 File Offset: 0x00276180
	private void OnCurrentDialogHidden(DialogBase dialog)
	{
		if (dialog != this.m_currentDialog)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.m_currentDialog.gameObject);
		this.m_currentDialog = null;
		this.UpdateQueue();
		if (this.OnDialogHidden != null)
		{
			this.OnDialogHidden();
		}
	}

	// Token: 0x06007920 RID: 31008 RVA: 0x00277FCC File Offset: 0x002761CC
	private IEnumerator ShowSeasonEndDialogWhenReady(DialogManager.DialogRequest request)
	{
		this.m_waitingToShowSeasonEndDialog = true;
		while (!NetCache.Get().IsNetObjectAvailable<NetCache.NetCacheRewardProgress>() || !this.m_isReadyForSeasonEndPopup)
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
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
				IL_FE:
				while (SceneMgr.Get().IsTransitioning())
				{
					yield return null;
				}
				this.AddToQueue(request);
				this.m_waitingToShowSeasonEndDialog = false;
				yield break;
			}
			yield return null;
		}
		goto IL_FE;
	}

	// Token: 0x04005E2E RID: 24110
	private static DialogManager s_instance;

	// Token: 0x04005E2F RID: 24111
	private Queue<DialogManager.DialogRequest> m_dialogRequests = new Queue<DialogManager.DialogRequest>();

	// Token: 0x04005E30 RID: 24112
	private DialogBase m_currentDialog;

	// Token: 0x04005E31 RID: 24113
	private bool m_loadingDialog;

	// Token: 0x04005E32 RID: 24114
	private bool m_isReadyForSeasonEndPopup;

	// Token: 0x04005E33 RID: 24115
	private bool m_waitingToShowSeasonEndDialog;

	// Token: 0x04005E34 RID: 24116
	private List<long> m_handledMedalNoticeIDs = new List<long>();

	// Token: 0x04005E38 RID: 24120
	public List<DialogManager.DialogTypeMapping> m_typeMapping = new List<DialogManager.DialogTypeMapping>();

	// Token: 0x02002501 RID: 9473
	// (Invoke) Token: 0x060131AB RID: 78251
	public delegate bool DialogProcessCallback(DialogBase dialog, object userData);

	// Token: 0x02002502 RID: 9474
	public enum DialogType
	{
		// Token: 0x0400EC47 RID: 60487
		ALERT,
		// Token: 0x0400EC48 RID: 60488
		SEASON_END,
		// Token: 0x0400EC49 RID: 60489
		FRIENDLY_CHALLENGE,
		// Token: 0x0400EC4A RID: 60490
		TAVERN_BRAWL_CHALLENGE,
		// Token: 0x0400EC4B RID: 60491
		EXISTING_ACCOUNT,
		// Token: 0x0400EC4C RID: 60492
		CARD_LIST,
		// Token: 0x0400EC4D RID: 60493
		STANDARD_COMING_SOON,
		// Token: 0x0400EC4E RID: 60494
		ROTATION_TUTORIAL,
		// Token: 0x0400EC4F RID: 60495
		HALL_OF_FAME,
		// Token: 0x0400EC50 RID: 60496
		TAVERN_BRAWL_CHOICE,
		// Token: 0x0400EC51 RID: 60497
		FIRESIDE_BRAWL_OK,
		// Token: 0x0400EC52 RID: 60498
		FIRESIDE_GATHERING_JOIN,
		// Token: 0x0400EC53 RID: 60499
		FIRESIDE_FIND_EVENT,
		// Token: 0x0400EC54 RID: 60500
		FIRESIDE_LOCATION_HELPER,
		// Token: 0x0400EC55 RID: 60501
		FIRESIDE_INNKEEPER_SETUP,
		// Token: 0x0400EC56 RID: 60502
		RETURNING_PLAYER_OPT_OUT,
		// Token: 0x0400EC57 RID: 60503
		OUTSTANDING_DRAFT_TICKETS,
		// Token: 0x0400EC58 RID: 60504
		FREE_ARENA_WIN,
		// Token: 0x0400EC59 RID: 60505
		ARENA_SEASON,
		// Token: 0x0400EC5A RID: 60506
		ASSET_DOWNLOAD,
		// Token: 0x0400EC5B RID: 60507
		LEAGUE_PROMOTE_SELF_MANUALLY,
		// Token: 0x0400EC5C RID: 60508
		RECONNECT_HELPER,
		// Token: 0x0400EC5D RID: 60509
		LOGIN_POPUP_SEQUENCE_BASIC,
		// Token: 0x0400EC5E RID: 60510
		MULTI_PAGE_POPUP,
		// Token: 0x0400EC5F RID: 60511
		GAME_MODES,
		// Token: 0x0400EC60 RID: 60512
		BACON_CHALLENGE,
		// Token: 0x0400EC61 RID: 60513
		PRIVACY_POLICY,
		// Token: 0x0400EC62 RID: 60514
		GENERIC_BASIC_POPUP
	}

	// Token: 0x02002503 RID: 9475
	public class DialogRequest
	{
		// Token: 0x0400EC63 RID: 60515
		public DialogManager.DialogType m_type;

		// Token: 0x0400EC64 RID: 60516
		public UserAttentionBlocker m_attentionCategory;

		// Token: 0x0400EC65 RID: 60517
		public object m_info;

		// Token: 0x0400EC66 RID: 60518
		public DialogManager.DialogProcessCallback m_callback;

		// Token: 0x0400EC67 RID: 60519
		public object m_userData;

		// Token: 0x0400EC68 RID: 60520
		public string m_prefabAssetReferenceOverride;

		// Token: 0x0400EC69 RID: 60521
		public bool m_isWidget;

		// Token: 0x0400EC6A RID: 60522
		public IDataModel m_dataModel;

		// Token: 0x0400EC6B RID: 60523
		public bool m_isFake;
	}

	// Token: 0x02002504 RID: 9476
	[Serializable]
	public class DialogTypeMapping
	{
		// Token: 0x0400EC6C RID: 60524
		public DialogManager.DialogType m_type;

		// Token: 0x0400EC6D RID: 60525
		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string m_prefabName;
	}

	// Token: 0x02002505 RID: 9477
	private class SeasonEndDialogRequestInfo
	{
		// Token: 0x0400EC6E RID: 60526
		public NetCache.ProfileNoticeMedal m_noticeMedal;
	}

	// Token: 0x02002506 RID: 9478
	private class PopupCallbackSharedData
	{
		// Token: 0x060131B1 RID: 78257 RVA: 0x005298B9 File Offset: 0x00527AB9
		public PopupCallbackSharedData(int count)
		{
			this.m_remainingToLoad = count;
		}

		// Token: 0x0400EC6F RID: 60527
		public readonly List<GameObject> m_loadedPrefabs = new List<GameObject>();

		// Token: 0x0400EC70 RID: 60528
		public int m_remainingToLoad;
	}

	// Token: 0x02002507 RID: 9479
	private struct PopupCallbackData
	{
		// Token: 0x060131B2 RID: 78258 RVA: 0x005298D3 File Offset: 0x00527AD3
		public PopupCallbackData(DialogManager.PopupCallbackSharedData sharedData, int index)
		{
			this.m_sharedData = sharedData;
			this.m_index = index;
		}

		// Token: 0x0400EC71 RID: 60529
		public DialogManager.PopupCallbackSharedData m_sharedData;

		// Token: 0x0400EC72 RID: 60530
		public int m_index;
	}
}
