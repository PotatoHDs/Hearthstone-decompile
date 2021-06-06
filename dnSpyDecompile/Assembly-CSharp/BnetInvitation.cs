using System;
using bgs;
using bgs.types;

// Token: 0x0200076A RID: 1898
public class BnetInvitation
{
	// Token: 0x06006ACF RID: 27343 RVA: 0x0022AAB8 File Offset: 0x00228CB8
	public static BnetInvitation CreateFromFriendsUpdate(FriendsUpdate src)
	{
		BnetInvitation bnetInvitation = new BnetInvitation();
		bnetInvitation.m_id = new BnetInvitationId(src.long1);
		if (src.entity1 != null)
		{
			bnetInvitation.m_inviterId = src.entity1.Clone();
		}
		if (src.entity2 != null)
		{
			bnetInvitation.m_inviteeId = src.entity2.Clone();
		}
		bnetInvitation.m_inviterName = src.string1;
		bnetInvitation.m_inviteeName = src.string2;
		bnetInvitation.m_message = src.string3;
		bnetInvitation.m_creationTimeMicrosec = src.long2;
		bnetInvitation.m_expirationTimeMicrosec = src.long3;
		return bnetInvitation;
	}

	// Token: 0x06006AD0 RID: 27344 RVA: 0x0022AB57 File Offset: 0x00228D57
	public BnetInvitationId GetId()
	{
		return this.m_id;
	}

	// Token: 0x06006AD1 RID: 27345 RVA: 0x0022AB5F File Offset: 0x00228D5F
	public void SetId(BnetInvitationId id)
	{
		this.m_id = id;
	}

	// Token: 0x06006AD2 RID: 27346 RVA: 0x0022AB68 File Offset: 0x00228D68
	public BnetEntityId GetInviterId()
	{
		return this.m_inviterId;
	}

	// Token: 0x06006AD3 RID: 27347 RVA: 0x0022AB70 File Offset: 0x00228D70
	public void SetInviterId(BnetEntityId id)
	{
		this.m_inviterId = id;
	}

	// Token: 0x06006AD4 RID: 27348 RVA: 0x0022AB79 File Offset: 0x00228D79
	public string GetInviterName()
	{
		return this.m_inviterName;
	}

	// Token: 0x06006AD5 RID: 27349 RVA: 0x0022AB81 File Offset: 0x00228D81
	public void SetInviterName(string name)
	{
		this.m_inviterName = name;
	}

	// Token: 0x06006AD6 RID: 27350 RVA: 0x0022AB8A File Offset: 0x00228D8A
	public BnetEntityId GetInviteeId()
	{
		return this.m_inviteeId;
	}

	// Token: 0x06006AD7 RID: 27351 RVA: 0x0022AB92 File Offset: 0x00228D92
	public void SetInviteeId(BnetEntityId id)
	{
		this.m_inviteeId = id;
	}

	// Token: 0x06006AD8 RID: 27352 RVA: 0x0022AB9B File Offset: 0x00228D9B
	public string GetInviteeName()
	{
		return this.m_inviteeName;
	}

	// Token: 0x06006AD9 RID: 27353 RVA: 0x0022ABA3 File Offset: 0x00228DA3
	public void SetInviteeName(string name)
	{
		this.m_inviteeName = name;
	}

	// Token: 0x06006ADA RID: 27354 RVA: 0x0022ABAC File Offset: 0x00228DAC
	public string GetMessage()
	{
		return this.m_message;
	}

	// Token: 0x06006ADB RID: 27355 RVA: 0x0022ABB4 File Offset: 0x00228DB4
	public void SetMessage(string message)
	{
		this.m_message = message;
	}

	// Token: 0x06006ADC RID: 27356 RVA: 0x0022ABBD File Offset: 0x00228DBD
	public ulong GetCreationTimeMicrosec()
	{
		return this.m_creationTimeMicrosec;
	}

	// Token: 0x06006ADD RID: 27357 RVA: 0x0022ABC5 File Offset: 0x00228DC5
	public void SetCreationTimeMicrosec(ulong microsec)
	{
		this.m_creationTimeMicrosec = microsec;
	}

	// Token: 0x06006ADE RID: 27358 RVA: 0x0022ABCE File Offset: 0x00228DCE
	public ulong GetExpirationTimeMicrosec()
	{
		return this.m_expirationTimeMicrosec;
	}

	// Token: 0x06006ADF RID: 27359 RVA: 0x0022ABD6 File Offset: 0x00228DD6
	public void SetExpirationTimeMicroSec(ulong microsec)
	{
		this.m_expirationTimeMicrosec = microsec;
	}

	// Token: 0x06006AE0 RID: 27360 RVA: 0x0022ABE0 File Offset: 0x00228DE0
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		BnetInvitation bnetInvitation = obj as BnetInvitation;
		return bnetInvitation != null && this.m_id.Equals(bnetInvitation.m_id);
	}

	// Token: 0x06006AE1 RID: 27361 RVA: 0x0022AC0F File Offset: 0x00228E0F
	public bool Equals(BnetInvitationId other)
	{
		return other != null && this.m_id.Equals(other);
	}

	// Token: 0x06006AE2 RID: 27362 RVA: 0x0022AC22 File Offset: 0x00228E22
	public override int GetHashCode()
	{
		return this.m_id.GetHashCode();
	}

	// Token: 0x06006AE3 RID: 27363 RVA: 0x0022AC2F File Offset: 0x00228E2F
	public static bool operator ==(BnetInvitation a, BnetInvitation b)
	{
		return a == b || (a != null && b != null && a.m_id == b.m_id);
	}

	// Token: 0x06006AE4 RID: 27364 RVA: 0x0022AC50 File Offset: 0x00228E50
	public static bool operator !=(BnetInvitation a, BnetInvitation b)
	{
		return !(a == b);
	}

	// Token: 0x06006AE5 RID: 27365 RVA: 0x0022AC5C File Offset: 0x00228E5C
	public override string ToString()
	{
		if (this.m_id == null)
		{
			return "UNKNOWN INVITATION";
		}
		return string.Format("[id={0} inviterId={1} inviterName={2} inviteeId={3} inviteeName={4} message={5}]", new object[]
		{
			this.m_id,
			this.m_inviterId,
			this.m_inviterName,
			this.m_inviteeId,
			this.m_inviteeName,
			this.m_message
		});
	}

	// Token: 0x040056F2 RID: 22258
	private BnetInvitationId m_id;

	// Token: 0x040056F3 RID: 22259
	private BnetEntityId m_inviterId;

	// Token: 0x040056F4 RID: 22260
	private string m_inviterName;

	// Token: 0x040056F5 RID: 22261
	private BnetEntityId m_inviteeId;

	// Token: 0x040056F6 RID: 22262
	private string m_inviteeName;

	// Token: 0x040056F7 RID: 22263
	private string m_message;

	// Token: 0x040056F8 RID: 22264
	private ulong m_creationTimeMicrosec;

	// Token: 0x040056F9 RID: 22265
	private ulong m_expirationTimeMicrosec;
}
