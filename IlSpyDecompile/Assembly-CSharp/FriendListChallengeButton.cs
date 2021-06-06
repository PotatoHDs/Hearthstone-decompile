using bgs;
using Hearthstone.UI;
using UnityEngine;

[RequireComponent(typeof(WidgetTemplate))]
public class FriendListChallengeButton : MonoBehaviour
{
	public WidgetInstance m_challengeMenu;

	public TooltipZone m_tooltipZone;

	private BnetPlayer m_player;

	private Widget m_widget;

	public bool IsChallengeMenuOpen { get; private set; }

	protected void Awake()
	{
		m_widget = GetComponent<WidgetTemplate>();
		m_widget.RegisterReadyListener(delegate
		{
			m_tooltipZone.gameObject.layer = 26;
		});
		m_challengeMenu.RegisterReadyListener(delegate
		{
			m_challengeMenu.SetLayerOverride(GameLayer.BattleNetDialog);
		});
		ChatMgr.Get().OnChatLogShown += CloseChallengeMenu;
	}

	private void OnDestroy()
	{
		if (IsChallengeMenuOpen)
		{
			FriendlyChallengeHelper.Get().ActiveChallengeMenu = null;
		}
		if (ChatMgr.Get() != null)
		{
			ChatMgr.Get().OnChatLogShown -= CloseChallengeMenu;
		}
	}

	public bool SetPlayer(BnetPlayer player)
	{
		if (m_player == player)
		{
			return false;
		}
		m_player = player;
		return true;
	}

	public BnetPlayer GetPlayer()
	{
		return m_player;
	}

	public void ShowTooltip()
	{
		if (IsChallengeMenuOpen)
		{
			return;
		}
		string text = string.Empty;
		string text2 = string.Empty;
		BnetGameAccountId hearthstoneGameAccountId = m_player.GetHearthstoneGameAccountId();
		SpectatorManager spectatorManager = SpectatorManager.Get();
		bool flag = PartyManager.Get().IsInBattlegroundsParty() && !SceneMgr.Get().IsInGame() && !GameMgr.Get().IsFindingGame();
		if (ChatMgr.Get().FriendListFrame.EditMode != FriendListFrame.FriendListEditMode.REMOVE_FRIENDS)
		{
			if (flag && PartyManager.Get().CanInvite(hearthstoneGameAccountId))
			{
				text = "GLOBAL_FRIENDLIST_BATTLEGROUNDS_TOOLTIP_INVITE_HEADER";
				text2 = "GLOBAL_FRIENDLIST_BATTLEGROUNDS_TOOLTIP_INVITE_BODY";
			}
			else if (flag && PartyManager.Get().CanKick(hearthstoneGameAccountId))
			{
				text = "GLOBAL_FRIENDLIST_BATTLEGROUNDS_TOOLTIP_KICK_HEADER";
				text2 = "GLOBAL_FRIENDLIST_BATTLEGROUNDS_TOOLTIP_KICK_BODY";
			}
			else if (flag && !PartyManager.Get().IsPartyLeader() && BnetFriendMgr.Get().IsFriend(m_player))
			{
				text = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_HEADER";
				text2 = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_BATTLEGROUNDS_PARTY_MEMBER";
			}
			else if (spectatorManager.HasInvitedMeToSpectate(hearthstoneGameAccountId))
			{
				text = "GLOBAL_FRIENDLIST_SPECTATE_TOOLTIP_AVAILABLE_HEADER";
				text2 = "GLOBAL_FRIENDLIST_SPECTATE_TOOLTIP_RECEIVED_INVITE_TEXT";
			}
			else if (!spectatorManager.CanSpectate(m_player))
			{
				if (spectatorManager.IsSpectatingMe(hearthstoneGameAccountId))
				{
					text = "GLOBAL_FRIENDLIST_SPECTATE_TOOLTIP_KICK_HEADER";
					text2 = "GLOBAL_FRIENDLIST_SPECTATE_TOOLTIP_KICK_TEXT";
				}
				else if (spectatorManager.CanInviteToSpectateMyGame(hearthstoneGameAccountId))
				{
					if (spectatorManager.IsPlayerSpectatingMyGamesOpposingSide(hearthstoneGameAccountId))
					{
						text = "GLOBAL_FRIENDLIST_SPECTATE_TOOLTIP_INVITE_OTHER_SIDE_HEADER";
						text2 = "GLOBAL_FRIENDLIST_SPECTATE_TOOLTIP_INVITE_OTHER_SIDE_TEXT";
					}
					else
					{
						text = "GLOBAL_FRIENDLIST_SPECTATE_TOOLTIP_INVITE_HEADER";
						text2 = "GLOBAL_FRIENDLIST_SPECTATE_TOOLTIP_INVITE_TEXT";
					}
				}
				else if (spectatorManager.IsInvitedToSpectateMyGame(hearthstoneGameAccountId))
				{
					text = "GLOBAL_FRIENDLIST_SPECTATE_TOOLTIP_INVITED_HEADER";
					text2 = "GLOBAL_FRIENDLIST_SPECTATE_TOOLTIP_INVITED_TEXT";
				}
				else if (spectatorManager.IsSpectatingPlayer(hearthstoneGameAccountId))
				{
					text = "GLOBAL_FRIENDLIST_SPECTATE_TOOLTIP_SPECTATING_HEADER";
					text2 = "GLOBAL_FRIENDLIST_SPECTATE_TOOLTIP_SPECTATING_TEXT";
				}
				else if (spectatorManager.HasPreviouslyKickedMeFromGame(hearthstoneGameAccountId, SpectatorManager.GetSpectatorGameHandleFromPlayer(m_player)))
				{
					text = "GLOBAL_FRIENDLIST_SPECTATE_TOOLTIP_PREVIOUSLY_KICKED_HEADER";
					text2 = "GLOBAL_FRIENDLIST_SPECTATE_TOOLTIP_PREVIOUSLY_KICKED_TEXT";
				}
				else
				{
					text = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_HEADER";
					text2 = ((!FriendChallengeMgr.Get().AmIAvailable()) ? (BnetPresenceMgr.Get().GetMyPlayer().IsAppearingOffline() ? "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_IM_APPEARING_OFFLINE" : ((!flag || BnetFriendMgr.Get().IsFriend(m_player)) ? "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_IM_UNAVAILABLE" : "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_AVAILABLE")) : ((FriendChallengeMgr.Get().CanChallenge(m_player) || !BnetFriendMgr.Get().IsFriend(m_player)) ? "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_AVAILABLE" : "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_THEYRE_UNAVAILABLE"));
				}
			}
		}
		if (UniversalInputManager.Get().IsTouchMode())
		{
			if (GameStrings.HasKey(text + "_TOUCH"))
			{
				text += "_TOUCH";
			}
			if (GameStrings.HasKey(text2 + "_TOUCH"))
			{
				text2 += "_TOUCH";
			}
		}
		if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
		{
			string headline = GameStrings.Get(text);
			string bodytext = GameStrings.Format(text2, m_player.GetBestName());
			m_tooltipZone.ShowSocialTooltip(this, headline, bodytext, 75f, GameLayer.BattleNetDialog);
		}
	}

	public void HideTooltip()
	{
		m_tooltipZone.HideTooltip();
	}

	public void OpenChallengeMenu()
	{
		m_widget.TriggerEvent("OPEN_CHALLENGE_MENU");
		ChatMgr.Get().FriendListFrame.CloseFlyoutMenu();
	}

	public void CloseChallengeMenu()
	{
		m_widget.TriggerEvent("CLOSE_CHALLENGE_MENU");
		HideTooltip();
	}

	public void CloseFriendsListMenu()
	{
		ChatMgr.Get().CloseFriendsList();
	}
}
