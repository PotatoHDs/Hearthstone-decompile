using bgs;

public class BnetAccount
{
	private BnetAccountId m_id;

	private string m_fullName;

	private BnetBattleTag m_battleTag;

	private long m_lastOnlineMicrosec;

	private bool m_away;

	private long m_awayTimeMicrosec;

	private bool m_busy;

	private bool m_appearingOffline;

	public string FullPresenceSummary => string.Format("BnetAccount [id={0} fullName={1} battleTag={2} away={3} busy={4} lastOnline={5} awayTime={6}]", m_id, m_fullName, m_battleTag, m_away, m_busy, (m_lastOnlineMicrosec == 0L) ? "null" : TimeUtils.ConvertEpochMicrosecToDateTime(m_lastOnlineMicrosec).ToString("R"), (m_awayTimeMicrosec == 0L) ? "null" : TimeUtils.ConvertEpochMicrosecToDateTime(m_awayTimeMicrosec).ToString("R"));

	public BnetAccount Clone()
	{
		BnetAccount bnetAccount = (BnetAccount)MemberwiseClone();
		if (m_id != null)
		{
			bnetAccount.m_id = m_id.Clone();
		}
		if (m_battleTag != null)
		{
			bnetAccount.m_battleTag = m_battleTag.Clone();
		}
		return bnetAccount;
	}

	public BnetAccountId GetId()
	{
		return m_id;
	}

	public void SetId(BnetAccountId id)
	{
		m_id = id;
	}

	public string GetFullName()
	{
		return m_fullName;
	}

	public void SetFullName(string fullName)
	{
		m_fullName = fullName;
	}

	public BnetBattleTag GetBattleTag()
	{
		return m_battleTag;
	}

	public void SetBattleTag(BnetBattleTag battleTag)
	{
		m_battleTag = battleTag;
	}

	public long GetLastOnlineMicrosec()
	{
		return m_lastOnlineMicrosec;
	}

	public void SetLastOnlineMicrosec(long microsec)
	{
		m_lastOnlineMicrosec = microsec;
	}

	public bool IsAway()
	{
		return m_away;
	}

	public void SetAway(bool away)
	{
		m_away = away;
	}

	public long GetAwayTimeMicrosec()
	{
		return m_awayTimeMicrosec;
	}

	public void SetAwayTimeMicrosec(long awayTimeMicrosec)
	{
		m_awayTimeMicrosec = awayTimeMicrosec;
	}

	public bool IsBusy()
	{
		return m_busy;
	}

	public void SetBusy(bool busy)
	{
		m_busy = busy;
	}

	public bool IsAppearingOffline()
	{
		return m_appearingOffline;
	}

	public void SetAppearingOffline(bool appearingOffline)
	{
		m_appearingOffline = appearingOffline;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		BnetAccount bnetAccount = obj as BnetAccount;
		if ((object)bnetAccount == null)
		{
			return false;
		}
		return m_id.Equals(bnetAccount.m_id);
	}

	public bool Equals(BnetAccountId other)
	{
		if ((object)other == null)
		{
			return false;
		}
		return m_id.Equals(other);
	}

	public override int GetHashCode()
	{
		return m_id.GetHashCode();
	}

	public static bool operator ==(BnetAccount a, BnetAccount b)
	{
		if ((object)a == b)
		{
			return true;
		}
		if ((object)a == null || (object)b == null)
		{
			return false;
		}
		return a.m_id == b.m_id;
	}

	public static bool operator !=(BnetAccount a, BnetAccount b)
	{
		return !(a == b);
	}

	public override string ToString()
	{
		if (m_id == null)
		{
			return "UNKNOWN ACCOUNT";
		}
		return $"[id={m_id} m_fullName={m_fullName} battleTag={m_battleTag} lastOnline={TimeUtils.ConvertEpochMicrosecToDateTime(m_lastOnlineMicrosec)}]";
	}
}
