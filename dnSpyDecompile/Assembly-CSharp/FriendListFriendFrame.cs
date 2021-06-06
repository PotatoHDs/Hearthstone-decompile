using System;
using System.Collections;
using bgs;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x0200008F RID: 143
[RequireComponent(typeof(WidgetTemplate))]
public class FriendListFriendFrame : MonoBehaviour
{
	// Token: 0x060008EA RID: 2282 RVA: 0x00035158 File Offset: 0x00033358
	private void Awake()
	{
		BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
		BnetWhisperMgr.Get().AddWhisperListener(new BnetWhisperMgr.WhisperCallback(this.OnWhisper));
		ChatMgr.Get().AddPlayerChatInfoChangedListener(new ChatMgr.PlayerChatInfoChangedCallback(this.OnPlayerChatInfoChanged));
		PartyManager.Get().AddChangedListener(new PartyManager.ChangedCallback(this.OnPartyChanged));
		this.m_widget = base.GetComponent<WidgetTemplate>();
		this.m_widget.RegisterReadyListener(new Action<object>(this.OnFriendFrameWidgetReady), null, true);
		this.m_friendDataModel = new FriendDataModel();
		this.m_widget.BindDataModel(this.m_friendDataModel, false);
		this.m_challengeButtonWidget.RegisterReadyListener(new Action<object>(this.OnChallengeButtonWidgetReady), null, true);
		this.m_rankedMedalWidget.RegisterReadyListener(new Action<object>(this.OnRankedMedalWidgetReady), null, true);
		this.m_friendUpdateCoroutine = base.StartCoroutine(this.RefreshFriend());
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x00035246 File Offset: 0x00033446
	private void OnEnable()
	{
		base.StopCoroutine(this.m_friendUpdateCoroutine);
		this.m_friendUpdateCoroutine = base.StartCoroutine(this.RefreshFriend());
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x00035268 File Offset: 0x00033468
	private void OnDestroy()
	{
		BnetPresenceMgr.Get().RemovePlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
		BnetWhisperMgr.Get().RemoveWhisperListener(new BnetWhisperMgr.WhisperCallback(this.OnWhisper));
		if (ChatMgr.Get() != null)
		{
			ChatMgr.Get().RemovePlayerChatInfoChangedListener(new ChatMgr.PlayerChatInfoChangedCallback(this.OnPlayerChatInfoChanged));
		}
		if (PartyManager.Get() != null)
		{
			PartyManager.Get().RemoveChangedListener(new PartyManager.ChangedCallback(this.OnPartyChanged));
		}
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x000352E5 File Offset: 0x000334E5
	private IEnumerator RefreshFriend()
	{
		for (;;)
		{
			this.UpdateFriend();
			yield return new WaitForSeconds(30f);
		}
		yield break;
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x000352F4 File Offset: 0x000334F4
	private void OnFriendFrameWidgetReady(object unused)
	{
		this.m_clickable = this.m_widget.FindWidgetComponent<Clickable>(Array.Empty<string>());
		this.m_clickable.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnFriendFrameReleased));
		BoxCollider component = this.m_clickable.GetComponent<BoxCollider>();
		if (component != null)
		{
			component.size = TransformUtil.ComputeSetPointBounds(base.gameObject).size;
		}
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x00035360 File Offset: 0x00033560
	private void OnChallengeButtonWidgetReady(object unused)
	{
		this.m_challengeButton = this.m_challengeButtonWidget.FindWidgetComponent<FriendListChallengeButton>(Array.Empty<string>());
		this.m_challengeButton.SetPlayer(this.m_player);
		this.m_challengeButtonVisualController = this.m_challengeButtonWidget.FindWidgetComponent<VisualController>(Array.Empty<string>());
		this.m_challengeButtonClickable = this.m_challengeButtonWidget.FindWidgetComponent<Clickable>(Array.Empty<string>());
		this.m_challengeButtonClickable.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnChallengeButtonRelease));
		this.m_challengeButtonClickable.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnChallengeButtonRollover));
		this.m_challengeButtonClickable.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnChallengeButtonRollout));
		this.UpdateFriend();
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x00035412 File Offset: 0x00033612
	private void OnRankedMedalWidgetReady(object unused)
	{
		this.m_rankedMedal = this.m_rankedMedalWidget.GetComponentInChildren<RankedMedal>();
		this.UpdatePlayerIcon();
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x0003542C File Offset: 0x0003362C
	public void Initialize(BnetPlayer player, bool isFSGPatron = false, bool isFSGInnkeeper = false)
	{
		this.m_player = player;
		this.m_playerIcon.SetPlayer(player);
		this.m_friendDataModel.IsFSGPatron = isFSGPatron;
		this.m_friendDataModel.IsFSGInnkeeper = isFSGInnkeeper;
		this.UpdateFriend();
		if (this.m_widget.IsChangingStates)
		{
			this.m_widget.Hide();
			this.m_widget.RegisterDoneChangingStatesListener(new Action<object>(this.OnWidgetDoneChangingStates), null, true, false);
		}
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x0003549D File Offset: 0x0003369D
	private void OnWidgetDoneChangingStates(object payload)
	{
		if (this.m_widget.gameObject.activeInHierarchy && this.m_widget.enabled && this.m_widget.IsDesiredHidden)
		{
			this.m_widget.Show();
		}
	}

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x060008F3 RID: 2291 RVA: 0x000354D6 File Offset: 0x000336D6
	public bool ShouldShowRankedMedal
	{
		get
		{
			return this.m_medalInfo != null && this.m_medalInfo.IsDisplayable();
		}
	}

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x060008F4 RID: 2292 RVA: 0x000354ED File Offset: 0x000336ED
	public bool IsRankedMedalReady
	{
		get
		{
			return this.m_rankedMedal != null && this.m_rankedMedal.IsReady;
		}
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x0003550A File Offset: 0x0003370A
	public Widget GetWidget()
	{
		return this.m_widget;
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x00035512 File Offset: 0x00033712
	public BnetPlayer GetFriend()
	{
		return this.m_player;
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x0003551A File Offset: 0x0003371A
	public void InitializeMobileFriendListItem(MobileFriendListItem item)
	{
		item.OnScrollOutOfViewEvent += this.OnScrollOutOfView;
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x00035530 File Offset: 0x00033730
	private void OnChallengeButtonRelease(UIEvent e)
	{
		string state = this.m_challengeButtonVisualController.State;
		uint num = <PrivateImplementationDetails>.ComputeStringHash(state);
		if (num <= 2573447773U)
		{
			if (num <= 1819604754U)
			{
				if (num != 1473763033U)
				{
					if (num != 1819604754U)
					{
						return;
					}
					if (!(state == "KICK_BATTLEGROUNDS"))
					{
						return;
					}
					this.OnKickBattlegroundsButtonPressed();
					return;
				}
				else
				{
					if (!(state == "INVITE_BATTLEGROUNDS"))
					{
						return;
					}
					this.OnInviteBattlegroundsButtonPressed();
					return;
				}
			}
			else if (num != 2236267653U)
			{
				if (num != 2573447773U)
				{
					return;
				}
				if (!(state == "KICK_SPECTATOR"))
				{
					return;
				}
				this.OnKickSpectatorButtonPressed();
				return;
			}
			else
			{
				if (!(state == "LEAVE_SPECTATING"))
				{
					return;
				}
				this.OnLeaveSpectatingButtonPressed();
				return;
			}
		}
		else if (num <= 3570226206U)
		{
			if (num != 3143037882U)
			{
				if (num != 3570226206U)
				{
					return;
				}
				if (!(state == "INVITE_TO_SPECTATE"))
				{
					return;
				}
				this.OnInviteToSpectateButtonPressed();
				return;
			}
			else
			{
				if (!(state == "SPECTATE"))
				{
					return;
				}
				this.OnSpectateButtonPressed();
				return;
			}
		}
		else if (num != 4017771448U)
		{
			if (num != 4168191690U)
			{
				return;
			}
			if (!(state == "DELETE"))
			{
				return;
			}
			this.OnDeleteFriendButtonPressed();
			return;
		}
		else
		{
			if (!(state == "AVAILABLE"))
			{
				return;
			}
			this.OnAvailableButtonPressed();
			return;
		}
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x00035659 File Offset: 0x00033859
	private void OnChallengeButtonRollover(UIEvent e)
	{
		this.m_challengeButton.ShowTooltip();
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x00035666 File Offset: 0x00033866
	private void OnChallengeButtonRollout(UIEvent e)
	{
		this.m_challengeButton.HideTooltip();
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x00035674 File Offset: 0x00033874
	private void OnFriendFrameReleased(UIEvent e)
	{
		if (ChatMgr.Get().FriendListFrame.IsInEditMode)
		{
			return;
		}
		FriendMgr.Get().SetSelectedFriend(this.m_player);
		if (BnetFriendMgr.Get().IsFriend(this.m_player.GetAccountId()))
		{
			ChatMgr.Get().OnFriendListFriendSelected(this.m_player);
		}
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x000356CA File Offset: 0x000338CA
	private void OnAvailableButtonPressed()
	{
		if (this.m_challengeButton.IsChallengeMenuOpen)
		{
			this.m_challengeButton.CloseChallengeMenu();
			return;
		}
		this.m_challengeButton.OpenChallengeMenu();
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x000356F0 File Offset: 0x000338F0
	private void OnDeleteFriendButtonPressed()
	{
		ChatMgr.Get().FriendListFrame.ShowRemoveFriendPopup(this.m_player);
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x00035707 File Offset: 0x00033907
	private void OnSpectateButtonPressed()
	{
		SpectatorManager.Get().SpectatePlayer(this.m_player);
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x0003571C File Offset: 0x0003391C
	private void OnKickSpectatorButtonPressed()
	{
		BnetGameAccountId hearthstoneGameAccountId = this.m_player.GetHearthstoneGameAccountId();
		if (SpectatorManager.Get().IsSpectatingMe(hearthstoneGameAccountId))
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLOBAL_SPECTATOR_KICK_PROMPT_HEADER");
			popupInfo.m_text = GameStrings.Format("GLOBAL_SPECTATOR_KICK_PROMPT_TEXT", new object[]
			{
				FriendUtils.GetUniqueName(this.m_player)
			});
			popupInfo.m_showAlertIcon = true;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(FriendListFriendFrame.OnKickSpectatorDialogResponse);
			popupInfo.m_responseUserData = this.m_player;
			DialogManager.DialogProcessCallback callback = delegate(DialogBase dialog, object userData)
			{
				BnetGameAccountId gameAccountId = (BnetGameAccountId)userData;
				return SpectatorManager.Get().IsSpectatingMe(gameAccountId);
			};
			DialogManager.Get().ShowPopup(popupInfo, callback, hearthstoneGameAccountId);
		}
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x000357D8 File Offset: 0x000339D8
	private void OnInviteToSpectateButtonPressed()
	{
		SpectatorManager.Get().InviteToSpectateMe(this.m_player);
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x000357EC File Offset: 0x000339EC
	private void OnLeaveSpectatingButtonPressed()
	{
		BnetGameAccountId gameAccountId = this.m_player.GetHearthstoneGameAccountId();
		SpectatorManager spectator = SpectatorManager.Get();
		if (GameMgr.Get().IsFindingGame() || SceneMgr.Get().IsTransitioning() || GameMgr.Get().IsTransitionPopupShown())
		{
			return;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_SPECTATOR_LEAVE_PROMPT_HEADER");
		popupInfo.m_text = GameStrings.Get("GLOBAL_SPECTATOR_LEAVE_PROMPT_TEXT");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(FriendListFriendFrame.OnLeaveSpectatingDialogResponse);
		DialogManager.DialogProcessCallback callback = (DialogBase dialog, object userData) => spectator.IsSpectatingPlayer(gameAccountId);
		DialogManager.Get().ShowPopup(popupInfo, callback);
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x0003589F File Offset: 0x00033A9F
	private void OnInviteBattlegroundsButtonPressed()
	{
		PartyManager.Get().SendInvite(PartyType.BATTLEGROUNDS_PARTY, this.m_player.GetBestGameAccountId());
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x000358B7 File Offset: 0x00033AB7
	private void OnKickBattlegroundsButtonPressed()
	{
		PartyManager.Get().KickPlayerFromParty(this.m_player.GetBestGameAccountId());
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x000358CE File Offset: 0x00033ACE
	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		if (!changelist.HasChange(this.m_player))
		{
			return;
		}
		this.UpdateFriend();
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x000358E5 File Offset: 0x00033AE5
	private void OnWhisper(BnetWhisper whisper, object userData)
	{
		if (this.m_player == null)
		{
			return;
		}
		if (!WhisperUtil.IsSpeakerOrReceiver(this.m_player, whisper))
		{
			return;
		}
		this.UpdateFriend();
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x00035905 File Offset: 0x00033B05
	private void OnPlayerChatInfoChanged(PlayerChatInfo chatInfo, object userData)
	{
		if (this.m_player != chatInfo.GetPlayer())
		{
			return;
		}
		this.UpdateFriend();
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x0003591C File Offset: 0x00033B1C
	private static void OnLeaveSpectatingDialogResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CONFIRM)
		{
			SpectatorManager.Get().LeaveSpectatorMode();
		}
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x0003592C File Offset: 0x00033B2C
	private static void OnKickSpectatorDialogResponse(AlertPopup.Response response, object userData)
	{
		BnetPlayer player = (BnetPlayer)userData;
		if (response == AlertPopup.Response.CONFIRM)
		{
			SpectatorManager.Get().KickSpectator(player, true);
		}
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x00035950 File Offset: 0x00033B50
	private void OnScrollOutOfView()
	{
		if (this.m_challengeButton != null)
		{
			this.m_challengeButton.CloseChallengeMenu();
		}
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x0003596C File Offset: 0x00033B6C
	public void UpdateFriend()
	{
		if (this.m_player == null)
		{
			return;
		}
		BnetPlayer bnetPlayer = BnetFriendMgr.Get().FindFriend(this.m_player.GetAccountId());
		if (bnetPlayer != null)
		{
			this.m_friendDataModel.PlayerName = FriendUtils.GetFriendListName(bnetPlayer, true);
		}
		else
		{
			this.m_friendDataModel.PlayerName = FriendUtils.GetFriendListName(this.m_player, false);
		}
		BnetGameAccount bestGameAccount = this.m_player.GetBestGameAccount();
		this.m_medalInfo = ((bestGameAccount == null) ? null : RankMgr.Get().GetRankPresenceField(bestGameAccount));
		this.m_chatIcon.UpdateIcon();
		this.UpdatePresence();
		this.UpdatePlayerIcon();
		this.UpdateInteractionState();
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x00035A0C File Offset: 0x00033C0C
	private void UpdateInteractionState()
	{
		if (this.m_challengeButtonWidget == null)
		{
			return;
		}
		if (this.m_challengeButton != null)
		{
			this.m_challengeButton.SetPlayer(this.m_player);
		}
		if (ChatMgr.Get() != null && ChatMgr.Get().FriendListFrame != null && ChatMgr.Get().FriendListFrame.IsInEditMode && ChatMgr.Get().FriendListFrame.EditMode == FriendListFrame.FriendListEditMode.REMOVE_FRIENDS)
		{
			this.m_friendDataModel.IsInEditMode = true;
			this.m_challengeButtonWidget.TriggerEvent("DELETE", default(Widget.TriggerEventParameters));
			return;
		}
		this.m_friendDataModel.IsInEditMode = false;
		BnetGameAccountId hearthstoneGameAccountId = this.m_player.GetHearthstoneGameAccountId();
		if (PartyManager.Get().IsInBattlegroundsParty() && !SceneMgr.Get().IsInGame() && !GameMgr.Get().IsFindingGame())
		{
			if (PartyManager.Get().CanInvite(hearthstoneGameAccountId))
			{
				this.m_challengeButtonWidget.TriggerEvent("INVITE_BATTLEGROUNDS", default(Widget.TriggerEventParameters));
				return;
			}
			if (PartyManager.Get().CanKick(hearthstoneGameAccountId))
			{
				this.m_challengeButtonWidget.TriggerEvent("KICK_BATTLEGROUNDS", default(Widget.TriggerEventParameters));
				return;
			}
			if (BnetFriendMgr.Get().IsFriend(this.m_player))
			{
				this.m_challengeButtonWidget.TriggerEvent("BUSY", default(Widget.TriggerEventParameters));
				return;
			}
			this.m_challengeButtonWidget.TriggerEvent("AVAILABLE", default(Widget.TriggerEventParameters));
			return;
		}
		else
		{
			SpectatorManager spectatorManager = SpectatorManager.Get();
			if (spectatorManager.IsSpectatingMe(hearthstoneGameAccountId))
			{
				this.m_challengeButtonWidget.TriggerEvent("KICK_SPECTATOR", default(Widget.TriggerEventParameters));
				return;
			}
			if (spectatorManager.CanInviteToSpectateMyGame(hearthstoneGameAccountId) || spectatorManager.IsInvitedToSpectateMyGame(hearthstoneGameAccountId))
			{
				this.m_challengeButtonWidget.TriggerEvent("INVITE_TO_SPECTATE", default(Widget.TriggerEventParameters));
				return;
			}
			if (spectatorManager.IsSpectatingPlayer(hearthstoneGameAccountId))
			{
				this.m_challengeButtonWidget.TriggerEvent("LEAVE_SPECTATING", default(Widget.TriggerEventParameters));
				return;
			}
			if (spectatorManager.CanSpectate(this.m_player))
			{
				this.m_challengeButtonWidget.TriggerEvent("SPECTATE", default(Widget.TriggerEventParameters));
				return;
			}
			if (FriendChallengeMgr.Get().CanChallenge(this.m_player) || !BnetFriendMgr.Get().IsFriend(this.m_player))
			{
				this.m_challengeButtonWidget.TriggerEvent("AVAILABLE", default(Widget.TriggerEventParameters));
				return;
			}
			this.m_challengeButtonWidget.TriggerEvent("BUSY", default(Widget.TriggerEventParameters));
			return;
		}
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x00035C8C File Offset: 0x00033E8C
	private void UpdatePlayerIcon()
	{
		if (!this.m_player.IsOnline())
		{
			this.m_widget.TriggerEvent("LAYOUT_WITHOUT_ICON", default(Widget.TriggerEventParameters));
			return;
		}
		BnetProgramId bestProgramId = this.m_player.GetBestProgramId();
		if (bestProgramId == null || bestProgramId.IsPhoenix())
		{
			this.m_widget.TriggerEvent("LAYOUT_WITHOUT_ICON", default(Widget.TriggerEventParameters));
			return;
		}
		this.m_widget.TriggerEvent("LAYOUT_WITH_ICON", default(Widget.TriggerEventParameters));
		this.m_rankedMedalWidget.gameObject.SetActive(true);
		if (bestProgramId == BnetProgramId.HEARTHSTONE && this.ShouldShowRankedMedal && this.m_rankedMedal != null)
		{
			this.m_playerIcon.Hide();
			if (this.NeedsRankedDataModelUpdate())
			{
				this.m_isRankedDataModelUpdatePending = true;
				this.m_medalInfo.CreateOrUpdateDataModel(this.m_medalInfo.GetBestCurrentRankFormatType(), ref this.m_rankedDataModel, RankedMedal.DisplayMode.Default, false, false, delegate(RankedPlayDataModel dm)
				{
					this.m_rankedMedal.BindRankedPlayDataModel(dm);
					this.m_rankedMedalWidget.Show();
					this.m_isRankedDataModelUpdatePending = false;
				});
				return;
			}
		}
		else
		{
			this.m_rankedMedalWidget.Hide();
			this.m_playerIcon.Show();
			this.m_playerIcon.UpdateIcon();
		}
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x00035DB0 File Offset: 0x00033FB0
	private bool NeedsRankedDataModelUpdate()
	{
		if (this.m_isRankedDataModelUpdatePending)
		{
			return false;
		}
		if (this.m_rankedDataModel != null && this.m_medalInfo != null)
		{
			TranslatedMedalInfo currentMedal = this.m_medalInfo.GetCurrentMedal(this.m_medalInfo.GetBestCurrentRankFormatType());
			if (this.m_rankedDataModel.FormatType == currentMedal.GetFormatType() && this.m_rankedDataModel.IsNewPlayer == currentMedal.IsNewPlayer() && this.m_rankedDataModel.StarLevel == currentMedal.starLevel)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x00035E2C File Offset: 0x0003402C
	protected void UpdatePresence()
	{
		if (!this.m_player.IsOnline())
		{
			this.m_friendDataModel.IsOnline = false;
			long bestLastOnlineMicrosec = this.m_player.GetBestLastOnlineMicrosec();
			this.m_friendDataModel.PlayerStatus = FriendUtils.GetLastOnlineElapsedTimeString(bestLastOnlineMicrosec);
			return;
		}
		if (this.m_player.IsAway())
		{
			this.m_friendDataModel.PlayerStatus = FriendUtils.GetAwayTimeString(this.m_player.GetBestAwayTimeMicrosec());
			this.m_friendDataModel.IsAway = true;
			return;
		}
		if (this.m_player.IsBusy())
		{
			this.m_friendDataModel.PlayerStatus = GameStrings.Get("GLOBAL_FRIENDLIST_BUSYSTATUS");
			this.m_friendDataModel.IsBusy = true;
			return;
		}
		this.m_friendDataModel.IsOnline = true;
		this.m_friendDataModel.IsAway = false;
		this.m_friendDataModel.IsBusy = false;
		string statusText = PresenceMgr.Get().GetStatusText(this.m_player);
		if (statusText != null)
		{
			this.m_friendDataModel.PlayerStatus = statusText;
			return;
		}
		BnetProgramId bestProgramId = this.m_player.GetBestProgramId();
		if (bestProgramId != null)
		{
			this.m_friendDataModel.PlayerStatus = BnetUtils.GetNameForProgramId(bestProgramId);
			return;
		}
		this.m_friendDataModel.PlayerStatus = GameStrings.Get("GLOBAL_PROGRAMNAME_PHOENIX");
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x00035F55 File Offset: 0x00034155
	private void OnPartyChanged(PartyManager.PartyInviteEvent inviteEvent, BnetGameAccountId playerGameAccountId, PartyManager.PartyData data, object userData)
	{
		this.UpdateInteractionState();
	}

	// Token: 0x04000606 RID: 1542
	private const float REFRESH_FRIENDS_SECONDS = 30f;

	// Token: 0x04000607 RID: 1543
	public PlayerIcon m_playerIcon;

	// Token: 0x04000608 RID: 1544
	public FriendListChatIcon m_chatIcon;

	// Token: 0x04000609 RID: 1545
	public Widget m_challengeButtonWidget;

	// Token: 0x0400060A RID: 1546
	public Widget m_rankedMedalWidget;

	// Token: 0x0400060B RID: 1547
	private WidgetTemplate m_widget;

	// Token: 0x0400060C RID: 1548
	private Clickable m_clickable;

	// Token: 0x0400060D RID: 1549
	private FriendListChallengeButton m_challengeButton;

	// Token: 0x0400060E RID: 1550
	private VisualController m_challengeButtonVisualController;

	// Token: 0x0400060F RID: 1551
	private Clickable m_challengeButtonClickable;

	// Token: 0x04000610 RID: 1552
	private BnetPlayer m_player;

	// Token: 0x04000611 RID: 1553
	private MedalInfoTranslator m_medalInfo;

	// Token: 0x04000612 RID: 1554
	private RankedMedal m_rankedMedal;

	// Token: 0x04000613 RID: 1555
	private RankedPlayDataModel m_rankedDataModel;

	// Token: 0x04000614 RID: 1556
	private bool m_isRankedDataModelUpdatePending;

	// Token: 0x04000615 RID: 1557
	private FriendDataModel m_friendDataModel;

	// Token: 0x04000616 RID: 1558
	private Coroutine m_friendUpdateCoroutine;
}
