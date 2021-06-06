using System;
using bgs;

// Token: 0x02000763 RID: 1891
public class BnetAccount
{
	// Token: 0x060069F9 RID: 27129 RVA: 0x00228C58 File Offset: 0x00226E58
	public BnetAccount Clone()
	{
		BnetAccount bnetAccount = (BnetAccount)base.MemberwiseClone();
		if (this.m_id != null)
		{
			bnetAccount.m_id = this.m_id.Clone();
		}
		if (this.m_battleTag != null)
		{
			bnetAccount.m_battleTag = this.m_battleTag.Clone();
		}
		return bnetAccount;
	}

	// Token: 0x060069FA RID: 27130 RVA: 0x00228CB0 File Offset: 0x00226EB0
	public BnetAccountId GetId()
	{
		return this.m_id;
	}

	// Token: 0x060069FB RID: 27131 RVA: 0x00228CB8 File Offset: 0x00226EB8
	public void SetId(BnetAccountId id)
	{
		this.m_id = id;
	}

	// Token: 0x060069FC RID: 27132 RVA: 0x00228CC1 File Offset: 0x00226EC1
	public string GetFullName()
	{
		return this.m_fullName;
	}

	// Token: 0x060069FD RID: 27133 RVA: 0x00228CC9 File Offset: 0x00226EC9
	public void SetFullName(string fullName)
	{
		this.m_fullName = fullName;
	}

	// Token: 0x060069FE RID: 27134 RVA: 0x00228CD2 File Offset: 0x00226ED2
	public BnetBattleTag GetBattleTag()
	{
		return this.m_battleTag;
	}

	// Token: 0x060069FF RID: 27135 RVA: 0x00228CDA File Offset: 0x00226EDA
	public void SetBattleTag(BnetBattleTag battleTag)
	{
		this.m_battleTag = battleTag;
	}

	// Token: 0x06006A00 RID: 27136 RVA: 0x00228CE3 File Offset: 0x00226EE3
	public long GetLastOnlineMicrosec()
	{
		return this.m_lastOnlineMicrosec;
	}

	// Token: 0x06006A01 RID: 27137 RVA: 0x00228CEB File Offset: 0x00226EEB
	public void SetLastOnlineMicrosec(long microsec)
	{
		this.m_lastOnlineMicrosec = microsec;
	}

	// Token: 0x06006A02 RID: 27138 RVA: 0x00228CF4 File Offset: 0x00226EF4
	public bool IsAway()
	{
		return this.m_away;
	}

	// Token: 0x06006A03 RID: 27139 RVA: 0x00228CFC File Offset: 0x00226EFC
	public void SetAway(bool away)
	{
		this.m_away = away;
	}

	// Token: 0x06006A04 RID: 27140 RVA: 0x00228D05 File Offset: 0x00226F05
	public long GetAwayTimeMicrosec()
	{
		return this.m_awayTimeMicrosec;
	}

	// Token: 0x06006A05 RID: 27141 RVA: 0x00228D0D File Offset: 0x00226F0D
	public void SetAwayTimeMicrosec(long awayTimeMicrosec)
	{
		this.m_awayTimeMicrosec = awayTimeMicrosec;
	}

	// Token: 0x06006A06 RID: 27142 RVA: 0x00228D16 File Offset: 0x00226F16
	public bool IsBusy()
	{
		return this.m_busy;
	}

	// Token: 0x06006A07 RID: 27143 RVA: 0x00228D1E File Offset: 0x00226F1E
	public void SetBusy(bool busy)
	{
		this.m_busy = busy;
	}

	// Token: 0x06006A08 RID: 27144 RVA: 0x00228D27 File Offset: 0x00226F27
	public bool IsAppearingOffline()
	{
		return this.m_appearingOffline;
	}

	// Token: 0x06006A09 RID: 27145 RVA: 0x00228D2F File Offset: 0x00226F2F
	public void SetAppearingOffline(bool appearingOffline)
	{
		this.m_appearingOffline = appearingOffline;
	}

	// Token: 0x06006A0A RID: 27146 RVA: 0x00228D38 File Offset: 0x00226F38
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		BnetAccount bnetAccount = obj as BnetAccount;
		return bnetAccount != null && this.m_id.Equals(bnetAccount.m_id);
	}

	// Token: 0x06006A0B RID: 27147 RVA: 0x00228D67 File Offset: 0x00226F67
	public bool Equals(BnetAccountId other)
	{
		return other != null && this.m_id.Equals(other);
	}

	// Token: 0x06006A0C RID: 27148 RVA: 0x00228D7A File Offset: 0x00226F7A
	public override int GetHashCode()
	{
		return this.m_id.GetHashCode();
	}

	// Token: 0x06006A0D RID: 27149 RVA: 0x00228D87 File Offset: 0x00226F87
	public static bool operator ==(BnetAccount a, BnetAccount b)
	{
		return a == b || (a != null && b != null && a.m_id == b.m_id);
	}

	// Token: 0x06006A0E RID: 27150 RVA: 0x00228DA8 File Offset: 0x00226FA8
	public static bool operator !=(BnetAccount a, BnetAccount b)
	{
		return !(a == b);
	}

	// Token: 0x06006A0F RID: 27151 RVA: 0x00228DB4 File Offset: 0x00226FB4
	public override string ToString()
	{
		if (this.m_id == null)
		{
			return "UNKNOWN ACCOUNT";
		}
		return string.Format("[id={0} m_fullName={1} battleTag={2} lastOnline={3}]", new object[]
		{
			this.m_id,
			this.m_fullName,
			this.m_battleTag,
			global::TimeUtils.ConvertEpochMicrosecToDateTime(this.m_lastOnlineMicrosec)
		});
	}

	// Token: 0x17000663 RID: 1635
	// (get) Token: 0x06006A10 RID: 27152 RVA: 0x00228E14 File Offset: 0x00227014
	public string FullPresenceSummary
	{
		get
		{
			return string.Format("BnetAccount [id={0} fullName={1} battleTag={2} away={3} busy={4} lastOnline={5} awayTime={6}]", new object[]
			{
				this.m_id,
				this.m_fullName,
				this.m_battleTag,
				this.m_away,
				this.m_busy,
				(this.m_lastOnlineMicrosec == 0L) ? "null" : global::TimeUtils.ConvertEpochMicrosecToDateTime(this.m_lastOnlineMicrosec).ToString("R"),
				(this.m_awayTimeMicrosec == 0L) ? "null" : global::TimeUtils.ConvertEpochMicrosecToDateTime(this.m_awayTimeMicrosec).ToString("R")
			});
		}
	}

	// Token: 0x040056CA RID: 22218
	private BnetAccountId m_id;

	// Token: 0x040056CB RID: 22219
	private string m_fullName;

	// Token: 0x040056CC RID: 22220
	private BnetBattleTag m_battleTag;

	// Token: 0x040056CD RID: 22221
	private long m_lastOnlineMicrosec;

	// Token: 0x040056CE RID: 22222
	private bool m_away;

	// Token: 0x040056CF RID: 22223
	private long m_awayTimeMicrosec;

	// Token: 0x040056D0 RID: 22224
	private bool m_busy;

	// Token: 0x040056D1 RID: 22225
	private bool m_appearingOffline;
}
