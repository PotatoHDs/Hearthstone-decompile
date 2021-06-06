using System.Collections.Generic;

public class BnetFriendChangelist
{
	private List<BnetPlayer> m_friendsAdded;

	private List<BnetPlayer> m_friendsRemoved;

	private List<BnetInvitation> m_receivedInvitesAdded;

	private List<BnetInvitation> m_receivedInvitesRemoved;

	private List<BnetInvitation> m_sentInvitesAdded;

	private List<BnetInvitation> m_sentInvitesRemoved;

	public List<BnetPlayer> GetAddedFriends()
	{
		return m_friendsAdded;
	}

	public List<BnetPlayer> GetRemovedFriends()
	{
		return m_friendsRemoved;
	}

	public List<BnetInvitation> GetAddedReceivedInvites()
	{
		return m_receivedInvitesAdded;
	}

	public List<BnetInvitation> GetRemovedReceivedInvites()
	{
		return m_receivedInvitesRemoved;
	}

	public List<BnetInvitation> GetAddedSentInvites()
	{
		return m_sentInvitesAdded;
	}

	public List<BnetInvitation> GetRemovedSentInvites()
	{
		return m_sentInvitesRemoved;
	}

	public bool IsEmpty()
	{
		if (m_friendsAdded != null && m_friendsAdded.Count > 0)
		{
			return false;
		}
		if (m_friendsRemoved != null && m_friendsRemoved.Count > 0)
		{
			return false;
		}
		if (m_receivedInvitesAdded != null && m_receivedInvitesAdded.Count > 0)
		{
			return false;
		}
		if (m_receivedInvitesRemoved != null && m_receivedInvitesRemoved.Count > 0)
		{
			return false;
		}
		if (m_sentInvitesAdded != null && m_sentInvitesAdded.Count > 0)
		{
			return false;
		}
		if (m_sentInvitesRemoved != null && m_sentInvitesRemoved.Count > 0)
		{
			return false;
		}
		return true;
	}

	public void Clear()
	{
		ClearAddedFriends();
		ClearRemovedFriends();
		ClearAddedReceivedInvites();
		ClearRemovedReceivedInvites();
		ClearAddedSentInvites();
		ClearRemovedSentInvites();
	}

	public bool AddAddedFriend(BnetPlayer friend)
	{
		if (m_friendsAdded == null)
		{
			m_friendsAdded = new List<BnetPlayer>();
		}
		else if (m_friendsAdded.Contains(friend))
		{
			return false;
		}
		m_friendsAdded.Add(friend);
		return true;
	}

	public bool RemoveAddedFriend(BnetPlayer friend)
	{
		if (m_friendsAdded == null)
		{
			return false;
		}
		return m_friendsAdded.Remove(friend);
	}

	public void ClearAddedFriends()
	{
		m_friendsAdded = null;
	}

	public bool AddRemovedFriend(BnetPlayer friend)
	{
		if (m_friendsRemoved == null)
		{
			m_friendsRemoved = new List<BnetPlayer>();
		}
		else if (m_friendsRemoved.Contains(friend))
		{
			return false;
		}
		m_friendsRemoved.Add(friend);
		return true;
	}

	public bool RemoveRemovedFriend(BnetPlayer friend)
	{
		if (m_friendsRemoved == null)
		{
			return false;
		}
		return m_friendsRemoved.Remove(friend);
	}

	public void ClearRemovedFriends()
	{
		m_friendsRemoved = null;
	}

	public bool AddAddedReceivedInvite(BnetInvitation invite)
	{
		if (m_receivedInvitesAdded == null)
		{
			m_receivedInvitesAdded = new List<BnetInvitation>();
		}
		else if (m_receivedInvitesAdded.Contains(invite))
		{
			return false;
		}
		m_receivedInvitesAdded.Add(invite);
		return true;
	}

	public bool RemoveAddedReceivedInvite(BnetInvitation invite)
	{
		if (m_receivedInvitesAdded == null)
		{
			return false;
		}
		return m_receivedInvitesAdded.Remove(invite);
	}

	public void ClearAddedReceivedInvites()
	{
		m_receivedInvitesAdded = null;
	}

	public bool AddRemovedReceivedInvite(BnetInvitation invite)
	{
		if (m_receivedInvitesRemoved == null)
		{
			m_receivedInvitesRemoved = new List<BnetInvitation>();
		}
		else if (m_receivedInvitesRemoved.Contains(invite))
		{
			return false;
		}
		m_receivedInvitesRemoved.Add(invite);
		return true;
	}

	public bool RemoveRemovedReceivedInvite(BnetInvitation invite)
	{
		if (m_receivedInvitesRemoved == null)
		{
			return false;
		}
		return m_receivedInvitesRemoved.Remove(invite);
	}

	public void ClearRemovedReceivedInvites()
	{
		m_receivedInvitesRemoved = null;
	}

	public bool AddAddedSentInvite(BnetInvitation invite)
	{
		if (m_sentInvitesAdded == null)
		{
			m_sentInvitesAdded = new List<BnetInvitation>();
		}
		else if (m_sentInvitesAdded.Contains(invite))
		{
			return false;
		}
		m_sentInvitesAdded.Add(invite);
		return true;
	}

	public bool RemoveAddedSentInvite(BnetInvitation invite)
	{
		if (m_sentInvitesAdded == null)
		{
			return false;
		}
		return m_sentInvitesAdded.Remove(invite);
	}

	public void ClearAddedSentInvites()
	{
		m_sentInvitesAdded = null;
	}

	public bool AddRemovedSentInvite(BnetInvitation invite)
	{
		if (m_sentInvitesRemoved == null)
		{
			m_sentInvitesRemoved = new List<BnetInvitation>();
		}
		else if (m_sentInvitesRemoved.Contains(invite))
		{
			return false;
		}
		m_sentInvitesRemoved.Add(invite);
		return true;
	}

	public bool RemoveRemovedSentInvite(BnetInvitation invite)
	{
		if (m_sentInvitesRemoved == null)
		{
			return false;
		}
		return m_sentInvitesRemoved.Remove(invite);
	}

	public void ClearRemovedSentInvites()
	{
		m_sentInvitesRemoved = null;
	}
}
