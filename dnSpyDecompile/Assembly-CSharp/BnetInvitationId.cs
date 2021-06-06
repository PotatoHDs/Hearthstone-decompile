using System;

// Token: 0x0200076B RID: 1899
public class BnetInvitationId
{
	// Token: 0x06006AE7 RID: 27367 RVA: 0x0022ACC3 File Offset: 0x00228EC3
	public BnetInvitationId(ulong val)
	{
		this.m_val = val;
	}

	// Token: 0x06006AE8 RID: 27368 RVA: 0x0022ACD2 File Offset: 0x00228ED2
	public ulong GetVal()
	{
		return this.m_val;
	}

	// Token: 0x06006AE9 RID: 27369 RVA: 0x0022ACDA File Offset: 0x00228EDA
	public void SetVal(ulong val)
	{
		this.m_val = val;
	}

	// Token: 0x06006AEA RID: 27370 RVA: 0x0022ACE4 File Offset: 0x00228EE4
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		BnetInvitationId bnetInvitationId = obj as BnetInvitationId;
		return bnetInvitationId != null && this.m_val == bnetInvitationId.m_val;
	}

	// Token: 0x06006AEB RID: 27371 RVA: 0x0022AD10 File Offset: 0x00228F10
	public bool Equals(BnetInvitationId other)
	{
		return other != null && this.m_val == other.m_val;
	}

	// Token: 0x06006AEC RID: 27372 RVA: 0x0022AD25 File Offset: 0x00228F25
	public override int GetHashCode()
	{
		return this.m_val.GetHashCode();
	}

	// Token: 0x06006AED RID: 27373 RVA: 0x0022AD32 File Offset: 0x00228F32
	public static bool operator ==(BnetInvitationId a, BnetInvitationId b)
	{
		return a == b || (a != null && b != null && a.m_val == b.m_val);
	}

	// Token: 0x06006AEE RID: 27374 RVA: 0x0022AD50 File Offset: 0x00228F50
	public static bool operator !=(BnetInvitationId a, BnetInvitationId b)
	{
		return !(a == b);
	}

	// Token: 0x06006AEF RID: 27375 RVA: 0x0022AD5C File Offset: 0x00228F5C
	public override string ToString()
	{
		return this.m_val.ToString();
	}

	// Token: 0x040056FA RID: 22266
	private ulong m_val;
}
