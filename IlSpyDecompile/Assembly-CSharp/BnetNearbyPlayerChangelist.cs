using System.Collections.Generic;

public class BnetNearbyPlayerChangelist
{
	private List<BnetPlayer> m_playersAdded;

	private List<BnetPlayer> m_playersUpdated;

	private List<BnetPlayer> m_playersRemoved;

	private List<BnetPlayer> m_friendsAdded;

	private List<BnetPlayer> m_friendsUpdated;

	private List<BnetPlayer> m_friendsRemoved;

	private List<BnetPlayer> m_strangersAdded;

	private List<BnetPlayer> m_strangersUpdated;

	private List<BnetPlayer> m_strangersRemoved;

	public List<BnetPlayer> GetAddedPlayers()
	{
		return m_playersAdded;
	}

	public List<BnetPlayer> GetUpdatedPlayers()
	{
		return m_playersUpdated;
	}

	public List<BnetPlayer> GetRemovedPlayers()
	{
		return m_playersRemoved;
	}

	public List<BnetPlayer> GetAddedFriends()
	{
		return m_friendsAdded;
	}

	public List<BnetPlayer> GetUpdatedFriends()
	{
		return m_friendsUpdated;
	}

	public List<BnetPlayer> GetRemovedFriends()
	{
		return m_friendsRemoved;
	}

	public List<BnetPlayer> GetAddedStrangers()
	{
		return m_strangersAdded;
	}

	public List<BnetPlayer> GetUpdatedStrangers()
	{
		return m_strangersUpdated;
	}

	public List<BnetPlayer> GetRemovedStrangers()
	{
		return m_strangersRemoved;
	}

	public bool IsEmpty()
	{
		if (m_playersAdded != null && m_playersAdded.Count > 0)
		{
			return false;
		}
		if (m_playersUpdated != null && m_playersUpdated.Count > 0)
		{
			return false;
		}
		if (m_playersRemoved != null && m_playersRemoved.Count > 0)
		{
			return false;
		}
		if (m_friendsAdded != null && m_friendsAdded.Count > 0)
		{
			return false;
		}
		if (m_friendsUpdated != null && m_friendsUpdated.Count > 0)
		{
			return false;
		}
		if (m_friendsRemoved != null && m_friendsRemoved.Count > 0)
		{
			return false;
		}
		if (m_strangersAdded != null && m_strangersAdded.Count > 0)
		{
			return false;
		}
		if (m_strangersUpdated != null && m_strangersUpdated.Count > 0)
		{
			return false;
		}
		if (m_strangersRemoved != null && m_strangersRemoved.Count > 0)
		{
			return false;
		}
		return true;
	}

	public void Clear()
	{
		ClearAddedPlayers();
		ClearUpdatedPlayers();
		ClearRemovedPlayers();
		ClearAddedFriends();
		ClearUpdatedFriends();
		ClearRemovedFriends();
		ClearAddedStrangers();
		ClearUpdatedStrangers();
		ClearRemovedStrangers();
	}

	public bool AddAddedPlayer(BnetPlayer player)
	{
		if (m_playersAdded == null)
		{
			m_playersAdded = new List<BnetPlayer>();
		}
		else if (m_playersAdded.Contains(player))
		{
			return false;
		}
		m_playersAdded.Add(player);
		return true;
	}

	public bool RemoveAddedPlayer(BnetPlayer player)
	{
		if (m_playersAdded == null)
		{
			return false;
		}
		return m_playersAdded.Remove(player);
	}

	public void ClearAddedPlayers()
	{
		m_playersAdded = null;
	}

	public bool AddUpdatedPlayer(BnetPlayer player)
	{
		if (m_playersUpdated == null)
		{
			m_playersUpdated = new List<BnetPlayer>();
		}
		else if (m_playersUpdated.Contains(player))
		{
			return false;
		}
		m_playersUpdated.Add(player);
		return true;
	}

	public bool RemoveUpdatedPlayer(BnetPlayer player)
	{
		if (m_playersUpdated == null)
		{
			return false;
		}
		return m_playersUpdated.Remove(player);
	}

	public void ClearUpdatedPlayers()
	{
		m_playersUpdated = null;
	}

	public bool AddRemovedPlayer(BnetPlayer player)
	{
		if (m_playersRemoved == null)
		{
			m_playersRemoved = new List<BnetPlayer>();
		}
		else if (m_playersRemoved.Contains(player))
		{
			return false;
		}
		m_playersRemoved.Add(player);
		return true;
	}

	public bool RemoveRemovedPlayer(BnetPlayer player)
	{
		if (m_playersRemoved == null)
		{
			return false;
		}
		return m_playersRemoved.Remove(player);
	}

	public void ClearRemovedPlayers()
	{
		m_playersRemoved = null;
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

	public bool AddUpdatedFriend(BnetPlayer friend)
	{
		if (m_friendsUpdated == null)
		{
			m_friendsUpdated = new List<BnetPlayer>();
		}
		else if (m_friendsUpdated.Contains(friend))
		{
			return false;
		}
		m_friendsUpdated.Add(friend);
		return true;
	}

	public bool RemoveUpdatedFriend(BnetPlayer friend)
	{
		if (m_friendsUpdated == null)
		{
			return false;
		}
		return m_friendsUpdated.Remove(friend);
	}

	public void ClearUpdatedFriends()
	{
		m_friendsUpdated = null;
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

	public bool AddAddedStranger(BnetPlayer stranger)
	{
		if (m_strangersAdded == null)
		{
			m_strangersAdded = new List<BnetPlayer>();
		}
		else if (m_strangersAdded.Contains(stranger))
		{
			return false;
		}
		m_strangersAdded.Add(stranger);
		return true;
	}

	public bool RemoveAddedStranger(BnetPlayer stranger)
	{
		if (m_strangersAdded == null)
		{
			return false;
		}
		return m_strangersAdded.Remove(stranger);
	}

	public void ClearAddedStrangers()
	{
		m_strangersAdded = null;
	}

	public bool AddUpdatedStranger(BnetPlayer stranger)
	{
		if (m_strangersUpdated == null)
		{
			m_strangersUpdated = new List<BnetPlayer>();
		}
		else if (m_strangersUpdated.Contains(stranger))
		{
			return false;
		}
		m_strangersUpdated.Add(stranger);
		return true;
	}

	public bool RemoveUpdatedStranger(BnetPlayer stranger)
	{
		if (m_strangersUpdated == null)
		{
			return false;
		}
		return m_strangersUpdated.Remove(stranger);
	}

	public void ClearUpdatedStrangers()
	{
		m_strangersUpdated = null;
	}

	public bool AddRemovedStranger(BnetPlayer stranger)
	{
		if (m_strangersRemoved == null)
		{
			m_strangersRemoved = new List<BnetPlayer>();
		}
		else if (m_strangersRemoved.Contains(stranger))
		{
			return false;
		}
		m_strangersRemoved.Add(stranger);
		return true;
	}

	public bool RemoveRemovedStranger(BnetPlayer stranger)
	{
		if (m_strangersRemoved == null)
		{
			return false;
		}
		return m_strangersRemoved.Remove(stranger);
	}

	public void ClearRemovedStrangers()
	{
		m_strangersRemoved = null;
	}
}
