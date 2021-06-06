using bgs;

public class SharedPlayerInfo : Entity
{
	private BnetGameAccountId m_gameAccountId;

	private string m_name;

	private Entity m_playerHero;

	public void InitPlayerInfo(Network.HistCreateGame.SharedPlayerInfo netPlayerInfo)
	{
		SetPlayerId(netPlayerInfo.ID);
		SetGameAccountId(netPlayerInfo.GameAccountId);
	}

	public int GetPlayerId()
	{
		return GetTag(GAME_TAG.PLAYER_ID);
	}

	public void SetPlayerId(int playerId)
	{
		SetTag(GAME_TAG.PLAYER_ID, playerId);
	}

	public Entity GetPlayerHero()
	{
		return m_playerHero;
	}

	public void SetPlayerHero(Entity playerHero)
	{
		m_playerHero = playerHero;
	}

	public void SetGameAccountId(BnetGameAccountId id)
	{
		m_gameAccountId = id;
		if (IsDisplayable())
		{
			UpdateDisplayInfo();
		}
		else if (GameUtils.IsBnetPlayer(m_gameAccountId))
		{
			BnetPresenceMgr.Get().AddPlayersChangedListener(OnBnetPlayersChanged);
			if (!BnetFriendMgr.Get().IsFriend(m_gameAccountId))
			{
				GameUtils.RequestPlayerPresence(m_gameAccountId);
			}
		}
	}

	public override string GetName()
	{
		return m_name;
	}

	private void OnBnetPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		if (changelist.FindChange(m_gameAccountId) != null && IsDisplayable())
		{
			BnetPresenceMgr.Get().RemovePlayersChangedListener(OnBnetPlayersChanged);
			UpdateDisplayInfo();
		}
	}

	public bool IsDisplayable()
	{
		if (m_gameAccountId == null)
		{
			return false;
		}
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(m_gameAccountId);
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
			if (!hearthstoneGameAccount.HasGameField(18u))
			{
				return false;
			}
		}
		return true;
	}

	private void UpdateDisplayInfo()
	{
		UpdateName();
	}

	private void UpdateName()
	{
		if (GameUtils.IsBnetPlayer(m_gameAccountId))
		{
			BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(m_gameAccountId);
			if (player != null)
			{
				m_name = player.GetBestName();
			}
			if (!string.IsNullOrEmpty(m_name))
			{
				GameMgr.Get().SetLastDisplayedPlayerName(GetPlayerId(), m_name);
			}
		}
		else
		{
			m_name = "Player " + GetPlayerId();
		}
	}
}
