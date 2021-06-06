using System;
using System.Collections.Generic;
using System.Text;
using bgs;

// Token: 0x0200076F RID: 1903
public class BnetPlayer
{
	// Token: 0x06006B59 RID: 27481 RVA: 0x0022C9D8 File Offset: 0x0022ABD8
	public BnetPlayer(BnetPlayerSource source)
	{
		this.m_source = source;
	}

	// Token: 0x06006B5A RID: 27482 RVA: 0x0022C9F4 File Offset: 0x0022ABF4
	public BnetPlayer Clone()
	{
		BnetPlayer bnetPlayer = (BnetPlayer)base.MemberwiseClone();
		if (this.m_accountId != null)
		{
			bnetPlayer.m_accountId = this.m_accountId.Clone();
		}
		if (this.m_account != null)
		{
			bnetPlayer.m_account = this.m_account.Clone();
		}
		if (this.m_hsGameAccount != null)
		{
			bnetPlayer.m_hsGameAccount = this.m_hsGameAccount.Clone();
		}
		if (this.m_bestGameAccount != null)
		{
			bnetPlayer.m_bestGameAccount = this.m_bestGameAccount.Clone();
		}
		bnetPlayer.m_gameAccounts = new global::Map<BnetGameAccountId, BnetGameAccount>();
		foreach (KeyValuePair<BnetGameAccountId, BnetGameAccount> keyValuePair in this.m_gameAccounts)
		{
			bnetPlayer.m_gameAccounts.Add(keyValuePair.Key.Clone(), keyValuePair.Value.Clone());
		}
		return bnetPlayer;
	}

	// Token: 0x17000665 RID: 1637
	// (get) Token: 0x06006B5B RID: 27483 RVA: 0x0022CAF8 File Offset: 0x0022ACF8
	public BnetPlayerSource Source
	{
		get
		{
			return this.m_source;
		}
	}

	// Token: 0x06006B5C RID: 27484 RVA: 0x0022CB00 File Offset: 0x0022AD00
	public BnetAccountId GetAccountId()
	{
		if (this.m_accountId != null)
		{
			return this.m_accountId;
		}
		BnetGameAccount firstGameAccount = this.GetFirstGameAccount();
		if (firstGameAccount != null)
		{
			return firstGameAccount.GetOwnerId();
		}
		return null;
	}

	// Token: 0x06006B5D RID: 27485 RVA: 0x0022CB3A File Offset: 0x0022AD3A
	public void SetAccountId(BnetAccountId accountId)
	{
		this.m_accountId = accountId;
	}

	// Token: 0x06006B5E RID: 27486 RVA: 0x0022CB43 File Offset: 0x0022AD43
	public BnetAccount GetAccount()
	{
		return this.m_account;
	}

	// Token: 0x06006B5F RID: 27487 RVA: 0x0022CB4B File Offset: 0x0022AD4B
	public void SetAccount(BnetAccount account)
	{
		this.m_account = account;
		this.m_accountId = account.GetId();
	}

	// Token: 0x06006B60 RID: 27488 RVA: 0x0022CB60 File Offset: 0x0022AD60
	public string GetFullName()
	{
		if (!(this.m_account == null))
		{
			return this.m_account.GetFullName();
		}
		return null;
	}

	// Token: 0x06006B61 RID: 27489 RVA: 0x0022CB80 File Offset: 0x0022AD80
	public BnetBattleTag GetBattleTag()
	{
		if (this.m_account != null && this.m_account.GetBattleTag() != null)
		{
			return this.m_account.GetBattleTag();
		}
		BnetGameAccount firstGameAccount = this.GetFirstGameAccount();
		if (firstGameAccount != null)
		{
			return firstGameAccount.GetBattleTag();
		}
		return null;
	}

	// Token: 0x06006B62 RID: 27490 RVA: 0x0022CBD4 File Offset: 0x0022ADD4
	public BnetGameAccount GetGameAccount(BnetGameAccountId id)
	{
		BnetGameAccount result = null;
		this.m_gameAccounts.TryGetValue(id, out result);
		return result;
	}

	// Token: 0x06006B63 RID: 27491 RVA: 0x0022CBF3 File Offset: 0x0022ADF3
	public global::Map<BnetGameAccountId, BnetGameAccount> GetGameAccounts()
	{
		return this.m_gameAccounts;
	}

	// Token: 0x06006B64 RID: 27492 RVA: 0x0022CBFB File Offset: 0x0022ADFB
	public bool HasGameAccount(BnetGameAccountId id)
	{
		return this.m_gameAccounts.ContainsKey(id);
	}

	// Token: 0x06006B65 RID: 27493 RVA: 0x0022CC0C File Offset: 0x0022AE0C
	public void AddGameAccount(BnetGameAccount gameAccount)
	{
		BnetGameAccountId id = gameAccount.GetId();
		if (this.m_gameAccounts.ContainsKey(id))
		{
			return;
		}
		this.m_gameAccounts.Add(id, gameAccount);
		this.CacheSpecialGameAccounts();
	}

	// Token: 0x06006B66 RID: 27494 RVA: 0x0022CC42 File Offset: 0x0022AE42
	public bool RemoveGameAccount(BnetGameAccountId id)
	{
		if (!this.m_gameAccounts.Remove(id))
		{
			return false;
		}
		this.CacheSpecialGameAccounts();
		return true;
	}

	// Token: 0x06006B67 RID: 27495 RVA: 0x0022CC5B File Offset: 0x0022AE5B
	public BnetGameAccount GetHearthstoneGameAccount()
	{
		return this.m_hsGameAccount;
	}

	// Token: 0x06006B68 RID: 27496 RVA: 0x0022CC63 File Offset: 0x0022AE63
	public BnetGameAccountId GetHearthstoneGameAccountId()
	{
		if (this.m_hsGameAccount == null)
		{
			return null;
		}
		return this.m_hsGameAccount.GetId();
	}

	// Token: 0x06006B69 RID: 27497 RVA: 0x0022CC80 File Offset: 0x0022AE80
	public BnetGameAccount GetBestGameAccount()
	{
		return this.m_bestGameAccount;
	}

	// Token: 0x06006B6A RID: 27498 RVA: 0x0022CC88 File Offset: 0x0022AE88
	public BnetGameAccountId GetBestGameAccountId()
	{
		if (this.m_bestGameAccount == null)
		{
			return null;
		}
		return this.m_bestGameAccount.GetId();
	}

	// Token: 0x17000666 RID: 1638
	// (get) Token: 0x06006B6B RID: 27499 RVA: 0x0022CCA5 File Offset: 0x0022AEA5
	// (set) Token: 0x06006B6C RID: 27500 RVA: 0x0022CCAD File Offset: 0x0022AEAD
	public bool IsCheatPlayer { get; set; }

	// Token: 0x06006B6D RID: 27501 RVA: 0x0022CCB6 File Offset: 0x0022AEB6
	public bool IsDisplayable()
	{
		return this.GetBestName() != null;
	}

	// Token: 0x06006B6E RID: 27502 RVA: 0x0022CCC4 File Offset: 0x0022AEC4
	public BnetGameAccount GetFirstGameAccount()
	{
		using (global::Map<BnetGameAccountId, BnetGameAccount>.ValueCollection.Enumerator enumerator = this.m_gameAccounts.Values.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				return enumerator.Current;
			}
		}
		return null;
	}

	// Token: 0x06006B6F RID: 27503 RVA: 0x0022CD1C File Offset: 0x0022AF1C
	public long GetPersistentGameId()
	{
		return 0L;
	}

	// Token: 0x06006B70 RID: 27504 RVA: 0x0022CD20 File Offset: 0x0022AF20
	public string GetBestName()
	{
		if (this != BnetPresenceMgr.Get().GetMyPlayer())
		{
			if (this.m_account != null)
			{
				string fullName = this.m_account.GetFullName();
				if (fullName != null)
				{
					return fullName;
				}
				if (this.m_account.GetBattleTag() != null)
				{
					return this.m_account.GetBattleTag().GetName();
				}
			}
			foreach (KeyValuePair<BnetGameAccountId, BnetGameAccount> keyValuePair in this.m_gameAccounts)
			{
				if (keyValuePair.Value.GetBattleTag() != null)
				{
					return keyValuePair.Value.GetBattleTag().GetName();
				}
			}
			return null;
		}
		if (this.m_hsGameAccount == null)
		{
			return null;
		}
		if (!(this.m_hsGameAccount.GetBattleTag() == null))
		{
			return this.m_hsGameAccount.GetBattleTag().GetName();
		}
		return null;
	}

	// Token: 0x06006B71 RID: 27505 RVA: 0x0022CE20 File Offset: 0x0022B020
	public BnetProgramId GetBestProgramId()
	{
		if (this.m_bestGameAccount == null)
		{
			return null;
		}
		return this.m_bestGameAccount.GetProgramId();
	}

	// Token: 0x06006B72 RID: 27506 RVA: 0x0022CE3D File Offset: 0x0022B03D
	public string GetBestRichPresence()
	{
		if (this.m_bestGameAccount == null)
		{
			return null;
		}
		return this.m_bestGameAccount.GetRichPresence();
	}

	// Token: 0x06006B73 RID: 27507 RVA: 0x0022CE5C File Offset: 0x0022B05C
	public bool IsOnline()
	{
		using (global::Map<BnetGameAccountId, BnetGameAccount>.ValueCollection.Enumerator enumerator = this.m_gameAccounts.Values.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsOnline())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06006B74 RID: 27508 RVA: 0x0022CEBC File Offset: 0x0022B0BC
	public bool IsAway()
	{
		return (this.m_account != null && this.m_account.IsAway()) || (this.m_bestGameAccount != null && this.m_bestGameAccount.IsAway());
	}

	// Token: 0x06006B75 RID: 27509 RVA: 0x0022CEF9 File Offset: 0x0022B0F9
	public bool IsBusy()
	{
		return (this.m_account != null && this.m_account.IsBusy()) || (this.m_bestGameAccount != null && this.m_bestGameAccount.IsBusy());
	}

	// Token: 0x06006B76 RID: 27510 RVA: 0x0022CF36 File Offset: 0x0022B136
	public bool IsAppearingOffline()
	{
		return this.m_account.IsAppearingOffline();
	}

	// Token: 0x06006B77 RID: 27511 RVA: 0x0022CF44 File Offset: 0x0022B144
	public long GetBestAwayTimeMicrosec()
	{
		long num = 0L;
		if (this.m_account != null && this.m_account.IsAway())
		{
			num = Math.Max(this.m_account.GetAwayTimeMicrosec(), this.m_account.GetLastOnlineMicrosec());
			if (num != 0L)
			{
				return num;
			}
		}
		if (this.m_bestGameAccount != null && this.m_bestGameAccount.IsAway())
		{
			return Math.Max(this.m_bestGameAccount.GetAwayTimeMicrosec(), this.m_bestGameAccount.GetLastOnlineMicrosec());
		}
		return num;
	}

	// Token: 0x06006B78 RID: 27512 RVA: 0x0022CFCC File Offset: 0x0022B1CC
	public long GetBestLastOnlineMicrosec()
	{
		long num = 0L;
		if (this.m_account != null)
		{
			num = this.m_account.GetLastOnlineMicrosec();
			if (num != 0L)
			{
				return num;
			}
		}
		if (this.m_bestGameAccount != null)
		{
			return this.m_bestGameAccount.GetLastOnlineMicrosec();
		}
		return num;
	}

	// Token: 0x06006B79 RID: 27513 RVA: 0x0022D01C File Offset: 0x0022B21C
	public bool HasMultipleOnlineGameAccounts()
	{
		bool flag = false;
		using (global::Map<BnetGameAccountId, BnetGameAccount>.ValueCollection.Enumerator enumerator = this.m_gameAccounts.Values.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsOnline())
				{
					if (flag)
					{
						return true;
					}
					flag = true;
				}
			}
		}
		return false;
	}

	// Token: 0x06006B7A RID: 27514 RVA: 0x0022D084 File Offset: 0x0022B284
	public int GetNumOnlineGameAccounts()
	{
		int num = 0;
		using (global::Map<BnetGameAccountId, BnetGameAccount>.ValueCollection.Enumerator enumerator = this.m_gameAccounts.Values.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsOnline())
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06006B7B RID: 27515 RVA: 0x0022D0E4 File Offset: 0x0022B2E4
	public List<BnetGameAccount> GetOnlineGameAccounts()
	{
		List<BnetGameAccount> list = new List<BnetGameAccount>();
		foreach (BnetGameAccount bnetGameAccount in this.m_gameAccounts.Values)
		{
			if (bnetGameAccount.IsOnline())
			{
				list.Add(bnetGameAccount);
			}
		}
		return list;
	}

	// Token: 0x06006B7C RID: 27516 RVA: 0x0022D14C File Offset: 0x0022B34C
	public bool HasAccount(BnetEntityId id)
	{
		if (id == null)
		{
			return false;
		}
		if (this.m_accountId == id)
		{
			return true;
		}
		using (global::Map<BnetGameAccountId, BnetGameAccount>.KeyCollection.Enumerator enumerator = this.m_gameAccounts.Keys.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == id)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06006B7D RID: 27517 RVA: 0x0022D1C8 File Offset: 0x0022B3C8
	public void OnGameAccountChanged(uint fieldId)
	{
		if (fieldId == 3U || fieldId == 1U || fieldId == 4U)
		{
			this.CacheSpecialGameAccounts();
		}
	}

	// Token: 0x06006B7E RID: 27518 RVA: 0x0022D1DC File Offset: 0x0022B3DC
	public override string ToString()
	{
		BnetAccountId accountId = this.GetAccountId();
		BnetBattleTag battleTag = this.GetBattleTag();
		if (accountId == null && battleTag == null)
		{
			return "UNKNOWN PLAYER";
		}
		return string.Format("[account={0} battleTag={1} numGameAccounts={2}]", accountId, battleTag, this.m_gameAccounts.Count);
	}

	// Token: 0x17000667 RID: 1639
	// (get) Token: 0x06006B7F RID: 27519 RVA: 0x0022D22C File Offset: 0x0022B42C
	public string ShortSummary
	{
		get
		{
			string fullName = this.GetFullName();
			BnetBattleTag battleTag = this.GetBattleTag();
			string text = (battleTag == null) ? "null" : battleTag.ToString();
			if (!string.IsNullOrEmpty(fullName) && battleTag != null)
			{
				text = " " + text;
			}
			string arg = this.IsOnline() ? "online" : "offline";
			return string.Format("{0}{1} {2}", fullName, text, arg);
		}
	}

	// Token: 0x17000668 RID: 1640
	// (get) Token: 0x06006B80 RID: 27520 RVA: 0x0022D2A0 File Offset: 0x0022B4A0
	public string FullPresenceSummary
	{
		get
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.m_account != null)
			{
				stringBuilder.Append(this.m_account.FullPresenceSummary);
			}
			else
			{
				stringBuilder.Append("null bnet account");
			}
			foreach (KeyValuePair<BnetGameAccountId, BnetGameAccount> keyValuePair in this.m_gameAccounts)
			{
				BnetGameAccount value = keyValuePair.Value;
				if (!(value == null))
				{
					stringBuilder.Append("\n").Append(value.FullPresenceSummary);
				}
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x06006B81 RID: 27521 RVA: 0x0022D350 File Offset: 0x0022B550
	private void CacheSpecialGameAccounts()
	{
		this.m_hsGameAccount = null;
		this.m_bestGameAccount = null;
		long num = 0L;
		foreach (BnetGameAccount bnetGameAccount in this.m_gameAccounts.Values)
		{
			BnetProgramId programId = bnetGameAccount.GetProgramId();
			if (!(programId == null))
			{
				if (programId == BnetProgramId.HEARTHSTONE)
				{
					this.m_hsGameAccount = bnetGameAccount;
					if (bnetGameAccount.IsOnline() || !BnetFriendMgr.Get().IsFriend(bnetGameAccount.GetId()))
					{
						this.m_bestGameAccount = bnetGameAccount;
						break;
					}
					break;
				}
				else if (this.m_bestGameAccount == null)
				{
					this.m_bestGameAccount = bnetGameAccount;
					num = this.m_bestGameAccount.GetLastOnlineMicrosec();
				}
				else if (!this.m_bestGameAccount.IsOnline() && bnetGameAccount.IsOnline())
				{
					this.m_bestGameAccount = bnetGameAccount;
					num = this.m_bestGameAccount.GetLastOnlineMicrosec();
				}
				else
				{
					BnetProgramId programId2 = this.m_bestGameAccount.GetProgramId();
					if (bnetGameAccount.IsOnline())
					{
						if (programId.IsGame() && !programId2.IsGame())
						{
							this.m_bestGameAccount = bnetGameAccount;
							num = this.m_bestGameAccount.GetLastOnlineMicrosec();
						}
						else if (programId.IsGame() && programId2.IsGame())
						{
							long lastOnlineMicrosec = bnetGameAccount.GetLastOnlineMicrosec();
							if (lastOnlineMicrosec > num)
							{
								this.m_bestGameAccount = bnetGameAccount;
								num = lastOnlineMicrosec;
							}
						}
					}
					else if (!this.m_bestGameAccount.IsOnline())
					{
						long lastOnlineMicrosec2 = bnetGameAccount.GetLastOnlineMicrosec();
						if (lastOnlineMicrosec2 > num)
						{
							this.m_bestGameAccount = bnetGameAccount;
							num = lastOnlineMicrosec2;
						}
					}
				}
			}
		}
	}

	// Token: 0x04005728 RID: 22312
	private BnetPlayerSource m_source;

	// Token: 0x04005729 RID: 22313
	private BnetAccountId m_accountId;

	// Token: 0x0400572A RID: 22314
	private BnetAccount m_account;

	// Token: 0x0400572B RID: 22315
	private global::Map<BnetGameAccountId, BnetGameAccount> m_gameAccounts = new global::Map<BnetGameAccountId, BnetGameAccount>();

	// Token: 0x0400572C RID: 22316
	private BnetGameAccount m_hsGameAccount;

	// Token: 0x0400572D RID: 22317
	private BnetGameAccount m_bestGameAccount;
}
