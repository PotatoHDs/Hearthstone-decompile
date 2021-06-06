using System;
using System.Collections.Generic;

// Token: 0x02000766 RID: 1894
public class BnetFriendChangelist
{
	// Token: 0x06006A2B RID: 27179 RVA: 0x002291A7 File Offset: 0x002273A7
	public List<BnetPlayer> GetAddedFriends()
	{
		return this.m_friendsAdded;
	}

	// Token: 0x06006A2C RID: 27180 RVA: 0x002291AF File Offset: 0x002273AF
	public List<BnetPlayer> GetRemovedFriends()
	{
		return this.m_friendsRemoved;
	}

	// Token: 0x06006A2D RID: 27181 RVA: 0x002291B7 File Offset: 0x002273B7
	public List<BnetInvitation> GetAddedReceivedInvites()
	{
		return this.m_receivedInvitesAdded;
	}

	// Token: 0x06006A2E RID: 27182 RVA: 0x002291BF File Offset: 0x002273BF
	public List<BnetInvitation> GetRemovedReceivedInvites()
	{
		return this.m_receivedInvitesRemoved;
	}

	// Token: 0x06006A2F RID: 27183 RVA: 0x002291C7 File Offset: 0x002273C7
	public List<BnetInvitation> GetAddedSentInvites()
	{
		return this.m_sentInvitesAdded;
	}

	// Token: 0x06006A30 RID: 27184 RVA: 0x002291CF File Offset: 0x002273CF
	public List<BnetInvitation> GetRemovedSentInvites()
	{
		return this.m_sentInvitesRemoved;
	}

	// Token: 0x06006A31 RID: 27185 RVA: 0x002291D8 File Offset: 0x002273D8
	public bool IsEmpty()
	{
		return (this.m_friendsAdded == null || this.m_friendsAdded.Count <= 0) && (this.m_friendsRemoved == null || this.m_friendsRemoved.Count <= 0) && (this.m_receivedInvitesAdded == null || this.m_receivedInvitesAdded.Count <= 0) && (this.m_receivedInvitesRemoved == null || this.m_receivedInvitesRemoved.Count <= 0) && (this.m_sentInvitesAdded == null || this.m_sentInvitesAdded.Count <= 0) && (this.m_sentInvitesRemoved == null || this.m_sentInvitesRemoved.Count <= 0);
	}

	// Token: 0x06006A32 RID: 27186 RVA: 0x00229276 File Offset: 0x00227476
	public void Clear()
	{
		this.ClearAddedFriends();
		this.ClearRemovedFriends();
		this.ClearAddedReceivedInvites();
		this.ClearRemovedReceivedInvites();
		this.ClearAddedSentInvites();
		this.ClearRemovedSentInvites();
	}

	// Token: 0x06006A33 RID: 27187 RVA: 0x0022929C File Offset: 0x0022749C
	public bool AddAddedFriend(BnetPlayer friend)
	{
		if (this.m_friendsAdded == null)
		{
			this.m_friendsAdded = new List<BnetPlayer>();
		}
		else if (this.m_friendsAdded.Contains(friend))
		{
			return false;
		}
		this.m_friendsAdded.Add(friend);
		return true;
	}

	// Token: 0x06006A34 RID: 27188 RVA: 0x002292D0 File Offset: 0x002274D0
	public bool RemoveAddedFriend(BnetPlayer friend)
	{
		return this.m_friendsAdded != null && this.m_friendsAdded.Remove(friend);
	}

	// Token: 0x06006A35 RID: 27189 RVA: 0x002292E8 File Offset: 0x002274E8
	public void ClearAddedFriends()
	{
		this.m_friendsAdded = null;
	}

	// Token: 0x06006A36 RID: 27190 RVA: 0x002292F1 File Offset: 0x002274F1
	public bool AddRemovedFriend(BnetPlayer friend)
	{
		if (this.m_friendsRemoved == null)
		{
			this.m_friendsRemoved = new List<BnetPlayer>();
		}
		else if (this.m_friendsRemoved.Contains(friend))
		{
			return false;
		}
		this.m_friendsRemoved.Add(friend);
		return true;
	}

	// Token: 0x06006A37 RID: 27191 RVA: 0x00229325 File Offset: 0x00227525
	public bool RemoveRemovedFriend(BnetPlayer friend)
	{
		return this.m_friendsRemoved != null && this.m_friendsRemoved.Remove(friend);
	}

	// Token: 0x06006A38 RID: 27192 RVA: 0x0022933D File Offset: 0x0022753D
	public void ClearRemovedFriends()
	{
		this.m_friendsRemoved = null;
	}

	// Token: 0x06006A39 RID: 27193 RVA: 0x00229346 File Offset: 0x00227546
	public bool AddAddedReceivedInvite(BnetInvitation invite)
	{
		if (this.m_receivedInvitesAdded == null)
		{
			this.m_receivedInvitesAdded = new List<BnetInvitation>();
		}
		else if (this.m_receivedInvitesAdded.Contains(invite))
		{
			return false;
		}
		this.m_receivedInvitesAdded.Add(invite);
		return true;
	}

	// Token: 0x06006A3A RID: 27194 RVA: 0x0022937A File Offset: 0x0022757A
	public bool RemoveAddedReceivedInvite(BnetInvitation invite)
	{
		return this.m_receivedInvitesAdded != null && this.m_receivedInvitesAdded.Remove(invite);
	}

	// Token: 0x06006A3B RID: 27195 RVA: 0x00229392 File Offset: 0x00227592
	public void ClearAddedReceivedInvites()
	{
		this.m_receivedInvitesAdded = null;
	}

	// Token: 0x06006A3C RID: 27196 RVA: 0x0022939B File Offset: 0x0022759B
	public bool AddRemovedReceivedInvite(BnetInvitation invite)
	{
		if (this.m_receivedInvitesRemoved == null)
		{
			this.m_receivedInvitesRemoved = new List<BnetInvitation>();
		}
		else if (this.m_receivedInvitesRemoved.Contains(invite))
		{
			return false;
		}
		this.m_receivedInvitesRemoved.Add(invite);
		return true;
	}

	// Token: 0x06006A3D RID: 27197 RVA: 0x002293CF File Offset: 0x002275CF
	public bool RemoveRemovedReceivedInvite(BnetInvitation invite)
	{
		return this.m_receivedInvitesRemoved != null && this.m_receivedInvitesRemoved.Remove(invite);
	}

	// Token: 0x06006A3E RID: 27198 RVA: 0x002293E7 File Offset: 0x002275E7
	public void ClearRemovedReceivedInvites()
	{
		this.m_receivedInvitesRemoved = null;
	}

	// Token: 0x06006A3F RID: 27199 RVA: 0x002293F0 File Offset: 0x002275F0
	public bool AddAddedSentInvite(BnetInvitation invite)
	{
		if (this.m_sentInvitesAdded == null)
		{
			this.m_sentInvitesAdded = new List<BnetInvitation>();
		}
		else if (this.m_sentInvitesAdded.Contains(invite))
		{
			return false;
		}
		this.m_sentInvitesAdded.Add(invite);
		return true;
	}

	// Token: 0x06006A40 RID: 27200 RVA: 0x00229424 File Offset: 0x00227624
	public bool RemoveAddedSentInvite(BnetInvitation invite)
	{
		return this.m_sentInvitesAdded != null && this.m_sentInvitesAdded.Remove(invite);
	}

	// Token: 0x06006A41 RID: 27201 RVA: 0x0022943C File Offset: 0x0022763C
	public void ClearAddedSentInvites()
	{
		this.m_sentInvitesAdded = null;
	}

	// Token: 0x06006A42 RID: 27202 RVA: 0x00229445 File Offset: 0x00227645
	public bool AddRemovedSentInvite(BnetInvitation invite)
	{
		if (this.m_sentInvitesRemoved == null)
		{
			this.m_sentInvitesRemoved = new List<BnetInvitation>();
		}
		else if (this.m_sentInvitesRemoved.Contains(invite))
		{
			return false;
		}
		this.m_sentInvitesRemoved.Add(invite);
		return true;
	}

	// Token: 0x06006A43 RID: 27203 RVA: 0x00229479 File Offset: 0x00227679
	public bool RemoveRemovedSentInvite(BnetInvitation invite)
	{
		return this.m_sentInvitesRemoved != null && this.m_sentInvitesRemoved.Remove(invite);
	}

	// Token: 0x06006A44 RID: 27204 RVA: 0x00229491 File Offset: 0x00227691
	public void ClearRemovedSentInvites()
	{
		this.m_sentInvitesRemoved = null;
	}

	// Token: 0x040056D6 RID: 22230
	private List<BnetPlayer> m_friendsAdded;

	// Token: 0x040056D7 RID: 22231
	private List<BnetPlayer> m_friendsRemoved;

	// Token: 0x040056D8 RID: 22232
	private List<BnetInvitation> m_receivedInvitesAdded;

	// Token: 0x040056D9 RID: 22233
	private List<BnetInvitation> m_receivedInvitesRemoved;

	// Token: 0x040056DA RID: 22234
	private List<BnetInvitation> m_sentInvitesAdded;

	// Token: 0x040056DB RID: 22235
	private List<BnetInvitation> m_sentInvitesRemoved;
}
