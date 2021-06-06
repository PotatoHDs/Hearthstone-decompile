using System;
using System.Collections.Generic;
using System.Text;
using bgs;

public class BnetPlayer
{
	private BnetPlayerSource m_source;

	private BnetAccountId m_accountId;

	private BnetAccount m_account;

	private Map<BnetGameAccountId, BnetGameAccount> m_gameAccounts = new Map<BnetGameAccountId, BnetGameAccount>();

	private BnetGameAccount m_hsGameAccount;

	private BnetGameAccount m_bestGameAccount;

	public BnetPlayerSource Source => m_source;

	public bool IsCheatPlayer { get; set; }

	public string ShortSummary
	{
		get
		{
			string fullName = GetFullName();
			BnetBattleTag battleTag = GetBattleTag();
			string text = ((battleTag == null) ? "null" : battleTag.ToString());
			if (!string.IsNullOrEmpty(fullName) && battleTag != null)
			{
				text = " " + text;
			}
			string arg = (IsOnline() ? "online" : "offline");
			return $"{fullName}{text} {arg}";
		}
	}

	public string FullPresenceSummary
	{
		get
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (m_account != null)
			{
				stringBuilder.Append(m_account.FullPresenceSummary);
			}
			else
			{
				stringBuilder.Append("null bnet account");
			}
			foreach (KeyValuePair<BnetGameAccountId, BnetGameAccount> gameAccount in m_gameAccounts)
			{
				BnetGameAccount value = gameAccount.Value;
				if (!(value == null))
				{
					stringBuilder.Append("\n").Append(value.FullPresenceSummary);
				}
			}
			return stringBuilder.ToString();
		}
	}

	public BnetPlayer(BnetPlayerSource source)
	{
		m_source = source;
	}

	public BnetPlayer Clone()
	{
		BnetPlayer bnetPlayer = (BnetPlayer)MemberwiseClone();
		if (m_accountId != null)
		{
			bnetPlayer.m_accountId = m_accountId.Clone();
		}
		if (m_account != null)
		{
			bnetPlayer.m_account = m_account.Clone();
		}
		if (m_hsGameAccount != null)
		{
			bnetPlayer.m_hsGameAccount = m_hsGameAccount.Clone();
		}
		if (m_bestGameAccount != null)
		{
			bnetPlayer.m_bestGameAccount = m_bestGameAccount.Clone();
		}
		bnetPlayer.m_gameAccounts = new Map<BnetGameAccountId, BnetGameAccount>();
		foreach (KeyValuePair<BnetGameAccountId, BnetGameAccount> gameAccount in m_gameAccounts)
		{
			bnetPlayer.m_gameAccounts.Add(gameAccount.Key.Clone(), gameAccount.Value.Clone());
		}
		return bnetPlayer;
	}

	public BnetAccountId GetAccountId()
	{
		if (m_accountId != null)
		{
			return m_accountId;
		}
		BnetGameAccount firstGameAccount = GetFirstGameAccount();
		if (firstGameAccount != null)
		{
			return firstGameAccount.GetOwnerId();
		}
		return null;
	}

	public void SetAccountId(BnetAccountId accountId)
	{
		m_accountId = accountId;
	}

	public BnetAccount GetAccount()
	{
		return m_account;
	}

	public void SetAccount(BnetAccount account)
	{
		m_account = account;
		m_accountId = account.GetId();
	}

	public string GetFullName()
	{
		if (!(m_account == null))
		{
			return m_account.GetFullName();
		}
		return null;
	}

	public BnetBattleTag GetBattleTag()
	{
		if (m_account != null && m_account.GetBattleTag() != null)
		{
			return m_account.GetBattleTag();
		}
		BnetGameAccount firstGameAccount = GetFirstGameAccount();
		if (firstGameAccount != null)
		{
			return firstGameAccount.GetBattleTag();
		}
		return null;
	}

	public BnetGameAccount GetGameAccount(BnetGameAccountId id)
	{
		BnetGameAccount value = null;
		m_gameAccounts.TryGetValue(id, out value);
		return value;
	}

	public Map<BnetGameAccountId, BnetGameAccount> GetGameAccounts()
	{
		return m_gameAccounts;
	}

	public bool HasGameAccount(BnetGameAccountId id)
	{
		return m_gameAccounts.ContainsKey(id);
	}

	public void AddGameAccount(BnetGameAccount gameAccount)
	{
		BnetGameAccountId id = gameAccount.GetId();
		if (!m_gameAccounts.ContainsKey(id))
		{
			m_gameAccounts.Add(id, gameAccount);
			CacheSpecialGameAccounts();
		}
	}

	public bool RemoveGameAccount(BnetGameAccountId id)
	{
		if (!m_gameAccounts.Remove(id))
		{
			return false;
		}
		CacheSpecialGameAccounts();
		return true;
	}

	public BnetGameAccount GetHearthstoneGameAccount()
	{
		return m_hsGameAccount;
	}

	public BnetGameAccountId GetHearthstoneGameAccountId()
	{
		if (m_hsGameAccount == null)
		{
			return null;
		}
		return m_hsGameAccount.GetId();
	}

	public BnetGameAccount GetBestGameAccount()
	{
		return m_bestGameAccount;
	}

	public BnetGameAccountId GetBestGameAccountId()
	{
		if (m_bestGameAccount == null)
		{
			return null;
		}
		return m_bestGameAccount.GetId();
	}

	public bool IsDisplayable()
	{
		return GetBestName() != null;
	}

	public BnetGameAccount GetFirstGameAccount()
	{
		using (Map<BnetGameAccountId, BnetGameAccount>.ValueCollection.Enumerator enumerator = m_gameAccounts.Values.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				return enumerator.Current;
			}
		}
		return null;
	}

	public long GetPersistentGameId()
	{
		return 0L;
	}

	public string GetBestName()
	{
		if (this == BnetPresenceMgr.Get().GetMyPlayer())
		{
			if (m_hsGameAccount == null)
			{
				return null;
			}
			if (!(m_hsGameAccount.GetBattleTag() == null))
			{
				return m_hsGameAccount.GetBattleTag().GetName();
			}
			return null;
		}
		if (m_account != null)
		{
			string fullName = m_account.GetFullName();
			if (fullName != null)
			{
				return fullName;
			}
			if (m_account.GetBattleTag() != null)
			{
				return m_account.GetBattleTag().GetName();
			}
		}
		foreach (KeyValuePair<BnetGameAccountId, BnetGameAccount> gameAccount in m_gameAccounts)
		{
			if (gameAccount.Value.GetBattleTag() != null)
			{
				return gameAccount.Value.GetBattleTag().GetName();
			}
		}
		return null;
	}

	public BnetProgramId GetBestProgramId()
	{
		if (m_bestGameAccount == null)
		{
			return null;
		}
		return m_bestGameAccount.GetProgramId();
	}

	public string GetBestRichPresence()
	{
		if (m_bestGameAccount == null)
		{
			return null;
		}
		return m_bestGameAccount.GetRichPresence();
	}

	public bool IsOnline()
	{
		foreach (BnetGameAccount value in m_gameAccounts.Values)
		{
			if (value.IsOnline())
			{
				return true;
			}
		}
		return false;
	}

	public bool IsAway()
	{
		if (m_account != null && m_account.IsAway())
		{
			return true;
		}
		if (m_bestGameAccount != null && m_bestGameAccount.IsAway())
		{
			return true;
		}
		return false;
	}

	public bool IsBusy()
	{
		if (m_account != null && m_account.IsBusy())
		{
			return true;
		}
		if (m_bestGameAccount != null && m_bestGameAccount.IsBusy())
		{
			return true;
		}
		return false;
	}

	public bool IsAppearingOffline()
	{
		return m_account.IsAppearingOffline();
	}

	public long GetBestAwayTimeMicrosec()
	{
		long num = 0L;
		if (m_account != null && m_account.IsAway())
		{
			num = Math.Max(m_account.GetAwayTimeMicrosec(), m_account.GetLastOnlineMicrosec());
			if (num != 0L)
			{
				return num;
			}
		}
		if (m_bestGameAccount != null && m_bestGameAccount.IsAway())
		{
			return Math.Max(m_bestGameAccount.GetAwayTimeMicrosec(), m_bestGameAccount.GetLastOnlineMicrosec());
		}
		return num;
	}

	public long GetBestLastOnlineMicrosec()
	{
		long num = 0L;
		if (m_account != null)
		{
			num = m_account.GetLastOnlineMicrosec();
			if (num != 0L)
			{
				return num;
			}
		}
		if (m_bestGameAccount != null)
		{
			return m_bestGameAccount.GetLastOnlineMicrosec();
		}
		return num;
	}

	public bool HasMultipleOnlineGameAccounts()
	{
		bool flag = false;
		foreach (BnetGameAccount value in m_gameAccounts.Values)
		{
			if (value.IsOnline())
			{
				if (flag)
				{
					return true;
				}
				flag = true;
			}
		}
		return false;
	}

	public int GetNumOnlineGameAccounts()
	{
		int num = 0;
		foreach (BnetGameAccount value in m_gameAccounts.Values)
		{
			if (value.IsOnline())
			{
				num++;
			}
		}
		return num;
	}

	public List<BnetGameAccount> GetOnlineGameAccounts()
	{
		List<BnetGameAccount> list = new List<BnetGameAccount>();
		foreach (BnetGameAccount value in m_gameAccounts.Values)
		{
			if (value.IsOnline())
			{
				list.Add(value);
			}
		}
		return list;
	}

	public bool HasAccount(BnetEntityId id)
	{
		if (id == null)
		{
			return false;
		}
		if (m_accountId == id)
		{
			return true;
		}
		foreach (BnetGameAccountId key in m_gameAccounts.Keys)
		{
			if (key == id)
			{
				return true;
			}
		}
		return false;
	}

	public void OnGameAccountChanged(uint fieldId)
	{
		if (fieldId == 3 || fieldId == 1 || fieldId == 4)
		{
			CacheSpecialGameAccounts();
		}
	}

	public override string ToString()
	{
		BnetAccountId accountId = GetAccountId();
		BnetBattleTag battleTag = GetBattleTag();
		if (accountId == null && battleTag == null)
		{
			return "UNKNOWN PLAYER";
		}
		return $"[account={accountId} battleTag={battleTag} numGameAccounts={m_gameAccounts.Count}]";
	}

	private void CacheSpecialGameAccounts()
	{
		m_hsGameAccount = null;
		m_bestGameAccount = null;
		long num = 0L;
		foreach (BnetGameAccount value in m_gameAccounts.Values)
		{
			BnetProgramId programId = value.GetProgramId();
			if (programId == null)
			{
				continue;
			}
			if (programId == BnetProgramId.HEARTHSTONE)
			{
				m_hsGameAccount = value;
				if (value.IsOnline() || !BnetFriendMgr.Get().IsFriend(value.GetId()))
				{
					m_bestGameAccount = value;
				}
				break;
			}
			if (m_bestGameAccount == null)
			{
				m_bestGameAccount = value;
				num = m_bestGameAccount.GetLastOnlineMicrosec();
				continue;
			}
			if (!m_bestGameAccount.IsOnline() && value.IsOnline())
			{
				m_bestGameAccount = value;
				num = m_bestGameAccount.GetLastOnlineMicrosec();
				continue;
			}
			BnetProgramId programId2 = m_bestGameAccount.GetProgramId();
			if (value.IsOnline())
			{
				if (programId.IsGame() && !programId2.IsGame())
				{
					m_bestGameAccount = value;
					num = m_bestGameAccount.GetLastOnlineMicrosec();
				}
				else if (programId.IsGame() && programId2.IsGame())
				{
					long lastOnlineMicrosec = value.GetLastOnlineMicrosec();
					if (lastOnlineMicrosec > num)
					{
						m_bestGameAccount = value;
						num = lastOnlineMicrosec;
					}
				}
			}
			else if (!m_bestGameAccount.IsOnline())
			{
				long lastOnlineMicrosec2 = value.GetLastOnlineMicrosec();
				if (lastOnlineMicrosec2 > num)
				{
					m_bestGameAccount = value;
					num = lastOnlineMicrosec2;
				}
			}
		}
	}
}
