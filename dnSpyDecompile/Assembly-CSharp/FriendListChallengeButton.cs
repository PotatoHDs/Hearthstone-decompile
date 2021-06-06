using System;
using bgs;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x0200008B RID: 139
[RequireComponent(typeof(WidgetTemplate))]
public class FriendListChallengeButton : MonoBehaviour
{
	// Token: 0x06000837 RID: 2103 RVA: 0x00030138 File Offset: 0x0002E338
	protected void Awake()
	{
		this.m_widget = base.GetComponent<WidgetTemplate>();
		this.m_widget.RegisterReadyListener(delegate(object _)
		{
			this.m_tooltipZone.gameObject.layer = 26;
		}, null, true);
		this.m_challengeMenu.RegisterReadyListener(delegate(object _)
		{
			this.m_challengeMenu.SetLayerOverride(GameLayer.BattleNetDialog);
		}, null, true);
		ChatMgr.Get().OnChatLogShown += this.CloseChallengeMenu;
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x00030199 File Offset: 0x0002E399
	private void OnDestroy()
	{
		if (this.IsChallengeMenuOpen)
		{
			FriendlyChallengeHelper.Get().ActiveChallengeMenu = null;
		}
		if (ChatMgr.Get() != null)
		{
			ChatMgr.Get().OnChatLogShown -= this.CloseChallengeMenu;
		}
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x06000839 RID: 2105 RVA: 0x000301D1 File Offset: 0x0002E3D1
	// (set) Token: 0x0600083A RID: 2106 RVA: 0x000301D9 File Offset: 0x0002E3D9
	public bool IsChallengeMenuOpen { get; private set; }

	// Token: 0x0600083B RID: 2107 RVA: 0x000301E2 File Offset: 0x0002E3E2
	public bool SetPlayer(BnetPlayer player)
	{
		if (this.m_player == player)
		{
			return false;
		}
		this.m_player = player;
		return true;
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x000301F7 File Offset: 0x0002E3F7
	public BnetPlayer GetPlayer()
	{
		return this.m_player;
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x00030200 File Offset: 0x0002E400
	public void ShowTooltip()
	{
		if (this.IsChallengeMenuOpen)
		{
			return;
		}
		string text = string.Empty;
		string text2 = string.Empty;
		BnetGameAccountId hearthstoneGameAccountId = this.m_player.GetHearthstoneGameAccountId();
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
			else if (flag && !PartyManager.Get().IsPartyLeader() && BnetFriendMgr.Get().IsFriend(this.m_player))
			{
				text = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_HEADER";
				text2 = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_BATTLEGROUNDS_PARTY_MEMBER";
			}
			else if (spectatorManager.HasInvitedMeToSpectate(hearthstoneGameAccountId))
			{
				text = "GLOBAL_FRIENDLIST_SPECTATE_TOOLTIP_AVAILABLE_HEADER";
				text2 = "GLOBAL_FRIENDLIST_SPECTATE_TOOLTIP_RECEIVED_INVITE_TEXT";
			}
			else if (!spectatorManager.CanSpectate(this.m_player))
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
				else if (spectatorManager.HasPreviouslyKickedMeFromGame(hearthstoneGameAccountId, SpectatorManager.GetSpectatorGameHandleFromPlayer(this.m_player)))
				{
					text = "GLOBAL_FRIENDLIST_SPECTATE_TOOLTIP_PREVIOUSLY_KICKED_HEADER";
					text2 = "GLOBAL_FRIENDLIST_SPECTATE_TOOLTIP_PREVIOUSLY_KICKED_TEXT";
				}
				else
				{
					text = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_HEADER";
					if (!FriendChallengeMgr.Get().AmIAvailable())
					{
						if (BnetPresenceMgr.Get().GetMyPlayer().IsAppearingOffline())
						{
							text2 = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_IM_APPEARING_OFFLINE";
						}
						else if (flag && !BnetFriendMgr.Get().IsFriend(this.m_player))
						{
							text2 = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_AVAILABLE";
						}
						else
						{
							text2 = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_IM_UNAVAILABLE";
						}
					}
					else if (!FriendChallengeMgr.Get().CanChallenge(this.m_player) && BnetFriendMgr.Get().IsFriend(this.m_player))
					{
						text2 = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_THEYRE_UNAVAILABLE";
					}
					else
					{
						text2 = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_AVAILABLE";
					}
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
			string bodytext = GameStrings.Format(text2, new object[]
			{
				this.m_player.GetBestName()
			});
			this.m_tooltipZone.ShowSocialTooltip(this, headline, bodytext, 75f, GameLayer.BattleNetDialog, 0);
		}
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x000304D3 File Offset: 0x0002E6D3
	public void HideTooltip()
	{
		this.m_tooltipZone.HideTooltip();
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x000304E0 File Offset: 0x0002E6E0
	public void OpenChallengeMenu()
	{
		this.m_widget.TriggerEvent("OPEN_CHALLENGE_MENU", default(Widget.TriggerEventParameters));
		ChatMgr.Get().FriendListFrame.CloseFlyoutMenu();
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x00030518 File Offset: 0x0002E718
	public void CloseChallengeMenu()
	{
		this.m_widget.TriggerEvent("CLOSE_CHALLENGE_MENU", default(Widget.TriggerEventParameters));
		this.HideTooltip();
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x00030545 File Offset: 0x0002E745
	public void CloseFriendsListMenu()
	{
		ChatMgr.Get().CloseFriendsList();
	}

	// Token: 0x04000592 RID: 1426
	public WidgetInstance m_challengeMenu;

	// Token: 0x04000593 RID: 1427
	public TooltipZone m_tooltipZone;

	// Token: 0x04000594 RID: 1428
	private BnetPlayer m_player;

	// Token: 0x04000595 RID: 1429
	private Widget m_widget;
}
