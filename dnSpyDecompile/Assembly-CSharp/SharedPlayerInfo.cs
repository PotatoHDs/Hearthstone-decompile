using System;
using bgs;

// Token: 0x02000346 RID: 838
public class SharedPlayerInfo : Entity
{
	// Token: 0x0600309B RID: 12443 RVA: 0x000FA1B3 File Offset: 0x000F83B3
	public void InitPlayerInfo(Network.HistCreateGame.SharedPlayerInfo netPlayerInfo)
	{
		this.SetPlayerId(netPlayerInfo.ID);
		this.SetGameAccountId(netPlayerInfo.GameAccountId);
	}

	// Token: 0x0600309C RID: 12444 RVA: 0x000ED25A File Offset: 0x000EB45A
	public int GetPlayerId()
	{
		return base.GetTag(GAME_TAG.PLAYER_ID);
	}

	// Token: 0x0600309D RID: 12445 RVA: 0x000ED264 File Offset: 0x000EB464
	public void SetPlayerId(int playerId)
	{
		base.SetTag(GAME_TAG.PLAYER_ID, playerId);
	}

	// Token: 0x0600309E RID: 12446 RVA: 0x000FA1CD File Offset: 0x000F83CD
	public Entity GetPlayerHero()
	{
		return this.m_playerHero;
	}

	// Token: 0x0600309F RID: 12447 RVA: 0x000FA1D5 File Offset: 0x000F83D5
	public void SetPlayerHero(Entity playerHero)
	{
		this.m_playerHero = playerHero;
	}

	// Token: 0x060030A0 RID: 12448 RVA: 0x000FA1E0 File Offset: 0x000F83E0
	public void SetGameAccountId(BnetGameAccountId id)
	{
		this.m_gameAccountId = id;
		if (this.IsDisplayable())
		{
			this.UpdateDisplayInfo();
			return;
		}
		if (GameUtils.IsBnetPlayer(this.m_gameAccountId))
		{
			BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnBnetPlayersChanged));
			if (!BnetFriendMgr.Get().IsFriend(this.m_gameAccountId))
			{
				GameUtils.RequestPlayerPresence(this.m_gameAccountId);
			}
		}
	}

	// Token: 0x060030A1 RID: 12449 RVA: 0x000FA244 File Offset: 0x000F8444
	public override string GetName()
	{
		return this.m_name;
	}

	// Token: 0x060030A2 RID: 12450 RVA: 0x000FA24C File Offset: 0x000F844C
	private void OnBnetPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		if (changelist.FindChange(this.m_gameAccountId) == null)
		{
			return;
		}
		if (!this.IsDisplayable())
		{
			return;
		}
		BnetPresenceMgr.Get().RemovePlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnBnetPlayersChanged));
		this.UpdateDisplayInfo();
	}

	// Token: 0x060030A3 RID: 12451 RVA: 0x000FA284 File Offset: 0x000F8484
	public bool IsDisplayable()
	{
		if (this.m_gameAccountId == null)
		{
			return false;
		}
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(this.m_gameAccountId);
		if (player == null)
		{
			return false;
		}
		if (!player.IsDisplayable())
		{
			return false;
		}
		if (GameUtils.IsGameTypeRanked())
		{
			BnetGameAccount hearthstoneGameAccount = player.GetHearthstoneGameAccount();
			if (hearthstoneGameAccount == null)
			{
				return false;
			}
			if (!hearthstoneGameAccount.HasGameField(18U))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060030A4 RID: 12452 RVA: 0x000FA2E7 File Offset: 0x000F84E7
	private void UpdateDisplayInfo()
	{
		this.UpdateName();
	}

	// Token: 0x060030A5 RID: 12453 RVA: 0x000FA2F0 File Offset: 0x000F84F0
	private void UpdateName()
	{
		if (GameUtils.IsBnetPlayer(this.m_gameAccountId))
		{
			BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(this.m_gameAccountId);
			if (player != null)
			{
				this.m_name = player.GetBestName();
			}
			if (!string.IsNullOrEmpty(this.m_name))
			{
				GameMgr.Get().SetLastDisplayedPlayerName(this.GetPlayerId(), this.m_name);
				return;
			}
		}
		else
		{
			this.m_name = "Player " + this.GetPlayerId();
		}
	}

	// Token: 0x04001AFF RID: 6911
	private BnetGameAccountId m_gameAccountId;

	// Token: 0x04001B00 RID: 6912
	private string m_name;

	// Token: 0x04001B01 RID: 6913
	private Entity m_playerHero;
}
