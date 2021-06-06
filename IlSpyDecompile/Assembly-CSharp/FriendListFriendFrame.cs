using System;
using System.Collections;
using bgs;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

[RequireComponent(typeof(WidgetTemplate))]
public class FriendListFriendFrame : MonoBehaviour
{
	private const float REFRESH_FRIENDS_SECONDS = 30f;

	public PlayerIcon m_playerIcon;

	public FriendListChatIcon m_chatIcon;

	public Widget m_challengeButtonWidget;

	public Widget m_rankedMedalWidget;

	private WidgetTemplate m_widget;

	private Clickable m_clickable;

	private FriendListChallengeButton m_challengeButton;

	private VisualController m_challengeButtonVisualController;

	private Clickable m_challengeButtonClickable;

	private BnetPlayer m_player;

	private MedalInfoTranslator m_medalInfo;

	private RankedMedal m_rankedMedal;

	private RankedPlayDataModel m_rankedDataModel;

	private bool m_isRankedDataModelUpdatePending;

	private FriendDataModel m_friendDataModel;

	private Coroutine m_friendUpdateCoroutine;

	public bool ShouldShowRankedMedal
	{
		get
		{
			if (m_medalInfo != null)
			{
				return m_medalInfo.IsDisplayable();
			}
			return false;
		}
	}

	public bool IsRankedMedalReady
	{
		get
		{
			if (m_rankedMedal != null)
			{
				return m_rankedMedal.IsReady;
			}
			return false;
		}
	}

	private void Awake()
	{
		BnetPresenceMgr.Get().AddPlayersChangedListener(OnPlayersChanged);
		BnetWhisperMgr.Get().AddWhisperListener(OnWhisper);
		ChatMgr.Get().AddPlayerChatInfoChangedListener(OnPlayerChatInfoChanged);
		PartyManager.Get().AddChangedListener(OnPartyChanged);
		m_widget = GetComponent<WidgetTemplate>();
		m_widget.RegisterReadyListener(OnFriendFrameWidgetReady);
		m_friendDataModel = new FriendDataModel();
		m_widget.BindDataModel(m_friendDataModel);
		m_challengeButtonWidget.RegisterReadyListener(OnChallengeButtonWidgetReady);
		m_rankedMedalWidget.RegisterReadyListener(OnRankedMedalWidgetReady);
		m_friendUpdateCoroutine = StartCoroutine(RefreshFriend());
	}

	private void OnEnable()
	{
		StopCoroutine(m_friendUpdateCoroutine);
		m_friendUpdateCoroutine = StartCoroutine(RefreshFriend());
	}

	private void OnDestroy()
	{
		BnetPresenceMgr.Get().RemovePlayersChangedListener(OnPlayersChanged);
		BnetWhisperMgr.Get().RemoveWhisperListener(OnWhisper);
		if (ChatMgr.Get() != null)
		{
			ChatMgr.Get().RemovePlayerChatInfoChangedListener(OnPlayerChatInfoChanged);
		}
		if (PartyManager.Get() != null)
		{
			PartyManager.Get().RemoveChangedListener(OnPartyChanged);
		}
	}

	private IEnumerator RefreshFriend()
	{
		while (true)
		{
			UpdateFriend();
			yield return new WaitForSeconds(30f);
		}
	}

	private void OnFriendFrameWidgetReady(object unused)
	{
		m_clickable = m_widget.FindWidgetComponent<Clickable>(Array.Empty<string>());
		m_clickable.AddEventListener(UIEventType.RELEASE, OnFriendFrameReleased);
		BoxCollider component = m_clickable.GetComponent<BoxCollider>();
		if (component != null)
		{
			component.size = TransformUtil.ComputeSetPointBounds(base.gameObject).size;
		}
	}

	private void OnChallengeButtonWidgetReady(object unused)
	{
		m_challengeButton = m_challengeButtonWidget.FindWidgetComponent<FriendListChallengeButton>(Array.Empty<string>());
		m_challengeButton.SetPlayer(m_player);
		m_challengeButtonVisualController = m_challengeButtonWidget.FindWidgetComponent<VisualController>(Array.Empty<string>());
		m_challengeButtonClickable = m_challengeButtonWidget.FindWidgetComponent<Clickable>(Array.Empty<string>());
		m_challengeButtonClickable.AddEventListener(UIEventType.RELEASE, OnChallengeButtonRelease);
		m_challengeButtonClickable.AddEventListener(UIEventType.ROLLOVER, OnChallengeButtonRollover);
		m_challengeButtonClickable.AddEventListener(UIEventType.ROLLOUT, OnChallengeButtonRollout);
		UpdateFriend();
	}

	private void OnRankedMedalWidgetReady(object unused)
	{
		m_rankedMedal = m_rankedMedalWidget.GetComponentInChildren<RankedMedal>();
		UpdatePlayerIcon();
	}

	public void Initialize(BnetPlayer player, bool isFSGPatron = false, bool isFSGInnkeeper = false)
	{
		m_player = player;
		m_playerIcon.SetPlayer(player);
		m_friendDataModel.IsFSGPatron = isFSGPatron;
		m_friendDataModel.IsFSGInnkeeper = isFSGInnkeeper;
		UpdateFriend();
		if (m_widget.IsChangingStates)
		{
			m_widget.Hide();
			m_widget.RegisterDoneChangingStatesListener(OnWidgetDoneChangingStates);
		}
	}

	private void OnWidgetDoneChangingStates(object payload)
	{
		if (m_widget.gameObject.activeInHierarchy && m_widget.enabled && m_widget.IsDesiredHidden)
		{
			m_widget.Show();
		}
	}

	public Widget GetWidget()
	{
		return m_widget;
	}

	public BnetPlayer GetFriend()
	{
		return m_player;
	}

	public void InitializeMobileFriendListItem(MobileFriendListItem item)
	{
		item.OnScrollOutOfViewEvent += OnScrollOutOfView;
	}

	private void OnChallengeButtonRelease(UIEvent e)
	{
		switch (m_challengeButtonVisualController.State)
		{
		case "AVAILABLE":
			OnAvailableButtonPressed();
			break;
		case "DELETE":
			OnDeleteFriendButtonPressed();
			break;
		case "SPECTATE":
			OnSpectateButtonPressed();
			break;
		case "KICK_SPECTATOR":
			OnKickSpectatorButtonPressed();
			break;
		case "INVITE_TO_SPECTATE":
			OnInviteToSpectateButtonPressed();
			break;
		case "LEAVE_SPECTATING":
			OnLeaveSpectatingButtonPressed();
			break;
		case "INVITE_BATTLEGROUNDS":
			OnInviteBattlegroundsButtonPressed();
			break;
		case "KICK_BATTLEGROUNDS":
			OnKickBattlegroundsButtonPressed();
			break;
		}
	}

	private void OnChallengeButtonRollover(UIEvent e)
	{
		m_challengeButton.ShowTooltip();
	}

	private void OnChallengeButtonRollout(UIEvent e)
	{
		m_challengeButton.HideTooltip();
	}

	private void OnFriendFrameReleased(UIEvent e)
	{
		if (!ChatMgr.Get().FriendListFrame.IsInEditMode)
		{
			FriendMgr.Get().SetSelectedFriend(m_player);
			if (BnetFriendMgr.Get().IsFriend(m_player.GetAccountId()))
			{
				ChatMgr.Get().OnFriendListFriendSelected(m_player);
			}
		}
	}

	private void OnAvailableButtonPressed()
	{
		if (m_challengeButton.IsChallengeMenuOpen)
		{
			m_challengeButton.CloseChallengeMenu();
		}
		else
		{
			m_challengeButton.OpenChallengeMenu();
		}
	}

	private void OnDeleteFriendButtonPressed()
	{
		ChatMgr.Get().FriendListFrame.ShowRemoveFriendPopup(m_player);
	}

	private void OnSpectateButtonPressed()
	{
		SpectatorManager.Get().SpectatePlayer(m_player);
	}

	private void OnKickSpectatorButtonPressed()
	{
		BnetGameAccountId hearthstoneGameAccountId = m_player.GetHearthstoneGameAccountId();
		if (SpectatorManager.Get().IsSpectatingMe(hearthstoneGameAccountId))
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLOBAL_SPECTATOR_KICK_PROMPT_HEADER");
			popupInfo.m_text = GameStrings.Format("GLOBAL_SPECTATOR_KICK_PROMPT_TEXT", FriendUtils.GetUniqueName(m_player));
			popupInfo.m_showAlertIcon = true;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			popupInfo.m_responseCallback = OnKickSpectatorDialogResponse;
			popupInfo.m_responseUserData = m_player;
			DialogManager.DialogProcessCallback callback = delegate(DialogBase dialog, object userData)
			{
				BnetGameAccountId gameAccountId = (BnetGameAccountId)userData;
				return SpectatorManager.Get().IsSpectatingMe(gameAccountId) ? true : false;
			};
			DialogManager.Get().ShowPopup(popupInfo, callback, hearthstoneGameAccountId);
		}
	}

	private void OnInviteToSpectateButtonPressed()
	{
		SpectatorManager.Get().InviteToSpectateMe(m_player);
	}

	private void OnLeaveSpectatingButtonPressed()
	{
		BnetGameAccountId gameAccountId = m_player.GetHearthstoneGameAccountId();
		SpectatorManager spectator = SpectatorManager.Get();
		if (!GameMgr.Get().IsFindingGame() && !SceneMgr.Get().IsTransitioning() && !GameMgr.Get().IsTransitionPopupShown())
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLOBAL_SPECTATOR_LEAVE_PROMPT_HEADER");
			popupInfo.m_text = GameStrings.Get("GLOBAL_SPECTATOR_LEAVE_PROMPT_TEXT");
			popupInfo.m_showAlertIcon = true;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			popupInfo.m_responseCallback = OnLeaveSpectatingDialogResponse;
			DialogManager.DialogProcessCallback callback = (DialogBase dialog, object userData) => spectator.IsSpectatingPlayer(gameAccountId) ? true : false;
			DialogManager.Get().ShowPopup(popupInfo, callback);
		}
	}

	private void OnInviteBattlegroundsButtonPressed()
	{
		PartyManager.Get().SendInvite(PartyType.BATTLEGROUNDS_PARTY, m_player.GetBestGameAccountId());
	}

	private void OnKickBattlegroundsButtonPressed()
	{
		PartyManager.Get().KickPlayerFromParty(m_player.GetBestGameAccountId());
	}

	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		if (changelist.HasChange(m_player))
		{
			UpdateFriend();
		}
	}

	private void OnWhisper(BnetWhisper whisper, object userData)
	{
		if (m_player != null && WhisperUtil.IsSpeakerOrReceiver(m_player, whisper))
		{
			UpdateFriend();
		}
	}

	private void OnPlayerChatInfoChanged(PlayerChatInfo chatInfo, object userData)
	{
		if (m_player == chatInfo.GetPlayer())
		{
			UpdateFriend();
		}
	}

	private static void OnLeaveSpectatingDialogResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CONFIRM)
		{
			SpectatorManager.Get().LeaveSpectatorMode();
		}
	}

	private static void OnKickSpectatorDialogResponse(AlertPopup.Response response, object userData)
	{
		BnetPlayer player = (BnetPlayer)userData;
		if (response == AlertPopup.Response.CONFIRM)
		{
			SpectatorManager.Get().KickSpectator(player, regenerateSpectatorPassword: true);
		}
	}

	private void OnScrollOutOfView()
	{
		if (m_challengeButton != null)
		{
			m_challengeButton.CloseChallengeMenu();
		}
	}

	public void UpdateFriend()
	{
		if (m_player != null)
		{
			BnetPlayer bnetPlayer = BnetFriendMgr.Get().FindFriend(m_player.GetAccountId());
			if (bnetPlayer != null)
			{
				m_friendDataModel.PlayerName = FriendUtils.GetFriendListName(bnetPlayer, addColorTags: true);
			}
			else
			{
				m_friendDataModel.PlayerName = FriendUtils.GetFriendListName(m_player, addColorTags: false);
			}
			BnetGameAccount bestGameAccount = m_player.GetBestGameAccount();
			m_medalInfo = ((bestGameAccount == null) ? null : RankMgr.Get().GetRankPresenceField(bestGameAccount));
			m_chatIcon.UpdateIcon();
			UpdatePresence();
			UpdatePlayerIcon();
			UpdateInteractionState();
		}
	}

	private void UpdateInteractionState()
	{
		if (m_challengeButtonWidget == null)
		{
			return;
		}
		if (m_challengeButton != null)
		{
			m_challengeButton.SetPlayer(m_player);
		}
		if (ChatMgr.Get() != null && ChatMgr.Get().FriendListFrame != null && ChatMgr.Get().FriendListFrame.IsInEditMode && ChatMgr.Get().FriendListFrame.EditMode == FriendListFrame.FriendListEditMode.REMOVE_FRIENDS)
		{
			m_friendDataModel.IsInEditMode = true;
			m_challengeButtonWidget.TriggerEvent("DELETE");
			return;
		}
		m_friendDataModel.IsInEditMode = false;
		BnetGameAccountId hearthstoneGameAccountId = m_player.GetHearthstoneGameAccountId();
		if (PartyManager.Get().IsInBattlegroundsParty() && !SceneMgr.Get().IsInGame() && !GameMgr.Get().IsFindingGame())
		{
			if (PartyManager.Get().CanInvite(hearthstoneGameAccountId))
			{
				m_challengeButtonWidget.TriggerEvent("INVITE_BATTLEGROUNDS");
			}
			else if (PartyManager.Get().CanKick(hearthstoneGameAccountId))
			{
				m_challengeButtonWidget.TriggerEvent("KICK_BATTLEGROUNDS");
			}
			else if (BnetFriendMgr.Get().IsFriend(m_player))
			{
				m_challengeButtonWidget.TriggerEvent("BUSY");
			}
			else
			{
				m_challengeButtonWidget.TriggerEvent("AVAILABLE");
			}
			return;
		}
		SpectatorManager spectatorManager = SpectatorManager.Get();
		if (spectatorManager.IsSpectatingMe(hearthstoneGameAccountId))
		{
			m_challengeButtonWidget.TriggerEvent("KICK_SPECTATOR");
		}
		else if (spectatorManager.CanInviteToSpectateMyGame(hearthstoneGameAccountId) || spectatorManager.IsInvitedToSpectateMyGame(hearthstoneGameAccountId))
		{
			m_challengeButtonWidget.TriggerEvent("INVITE_TO_SPECTATE");
		}
		else if (spectatorManager.IsSpectatingPlayer(hearthstoneGameAccountId))
		{
			m_challengeButtonWidget.TriggerEvent("LEAVE_SPECTATING");
		}
		else if (spectatorManager.CanSpectate(m_player))
		{
			m_challengeButtonWidget.TriggerEvent("SPECTATE");
		}
		else if (FriendChallengeMgr.Get().CanChallenge(m_player) || !BnetFriendMgr.Get().IsFriend(m_player))
		{
			m_challengeButtonWidget.TriggerEvent("AVAILABLE");
		}
		else
		{
			m_challengeButtonWidget.TriggerEvent("BUSY");
		}
	}

	private void UpdatePlayerIcon()
	{
		if (!m_player.IsOnline())
		{
			m_widget.TriggerEvent("LAYOUT_WITHOUT_ICON");
			return;
		}
		BnetProgramId bestProgramId = m_player.GetBestProgramId();
		if (bestProgramId == null || bestProgramId.IsPhoenix())
		{
			m_widget.TriggerEvent("LAYOUT_WITHOUT_ICON");
			return;
		}
		m_widget.TriggerEvent("LAYOUT_WITH_ICON");
		m_rankedMedalWidget.gameObject.SetActive(value: true);
		if (bestProgramId == BnetProgramId.HEARTHSTONE && ShouldShowRankedMedal && m_rankedMedal != null)
		{
			m_playerIcon.Hide();
			if (NeedsRankedDataModelUpdate())
			{
				m_isRankedDataModelUpdatePending = true;
				m_medalInfo.CreateOrUpdateDataModel(m_medalInfo.GetBestCurrentRankFormatType(), ref m_rankedDataModel, RankedMedal.DisplayMode.Default, isTooltipEnabled: false, hasEarnedCardBack: false, delegate(RankedPlayDataModel dm)
				{
					m_rankedMedal.BindRankedPlayDataModel(dm);
					m_rankedMedalWidget.Show();
					m_isRankedDataModelUpdatePending = false;
				});
			}
		}
		else
		{
			m_rankedMedalWidget.Hide();
			m_playerIcon.Show();
			m_playerIcon.UpdateIcon();
		}
	}

	private bool NeedsRankedDataModelUpdate()
	{
		if (m_isRankedDataModelUpdatePending)
		{
			return false;
		}
		if (m_rankedDataModel != null && m_medalInfo != null)
		{
			TranslatedMedalInfo currentMedal = m_medalInfo.GetCurrentMedal(m_medalInfo.GetBestCurrentRankFormatType());
			if (m_rankedDataModel.FormatType == currentMedal.GetFormatType() && m_rankedDataModel.IsNewPlayer == currentMedal.IsNewPlayer() && m_rankedDataModel.StarLevel == currentMedal.starLevel)
			{
				return false;
			}
		}
		return true;
	}

	protected void UpdatePresence()
	{
		if (!m_player.IsOnline())
		{
			m_friendDataModel.IsOnline = false;
			long bestLastOnlineMicrosec = m_player.GetBestLastOnlineMicrosec();
			m_friendDataModel.PlayerStatus = FriendUtils.GetLastOnlineElapsedTimeString(bestLastOnlineMicrosec);
			return;
		}
		if (m_player.IsAway())
		{
			m_friendDataModel.PlayerStatus = FriendUtils.GetAwayTimeString(m_player.GetBestAwayTimeMicrosec());
			m_friendDataModel.IsAway = true;
			return;
		}
		if (m_player.IsBusy())
		{
			m_friendDataModel.PlayerStatus = GameStrings.Get("GLOBAL_FRIENDLIST_BUSYSTATUS");
			m_friendDataModel.IsBusy = true;
			return;
		}
		m_friendDataModel.IsOnline = true;
		m_friendDataModel.IsAway = false;
		m_friendDataModel.IsBusy = false;
		string statusText = PresenceMgr.Get().GetStatusText(m_player);
		if (statusText != null)
		{
			m_friendDataModel.PlayerStatus = statusText;
			return;
		}
		BnetProgramId bestProgramId = m_player.GetBestProgramId();
		if (bestProgramId != null)
		{
			m_friendDataModel.PlayerStatus = BnetUtils.GetNameForProgramId(bestProgramId);
		}
		else
		{
			m_friendDataModel.PlayerStatus = GameStrings.Get("GLOBAL_PROGRAMNAME_PHOENIX");
		}
	}

	private void OnPartyChanged(PartyManager.PartyInviteEvent inviteEvent, BnetGameAccountId playerGameAccountId, PartyManager.PartyData data, object userData)
	{
		UpdateInteractionState();
	}
}
