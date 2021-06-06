using System;
using System.Collections.Generic;

// Token: 0x0200076C RID: 1900
public class BnetNearbyPlayerChangelist
{
	// Token: 0x06006AF0 RID: 27376 RVA: 0x0022AD69 File Offset: 0x00228F69
	public List<BnetPlayer> GetAddedPlayers()
	{
		return this.m_playersAdded;
	}

	// Token: 0x06006AF1 RID: 27377 RVA: 0x0022AD71 File Offset: 0x00228F71
	public List<BnetPlayer> GetUpdatedPlayers()
	{
		return this.m_playersUpdated;
	}

	// Token: 0x06006AF2 RID: 27378 RVA: 0x0022AD79 File Offset: 0x00228F79
	public List<BnetPlayer> GetRemovedPlayers()
	{
		return this.m_playersRemoved;
	}

	// Token: 0x06006AF3 RID: 27379 RVA: 0x0022AD81 File Offset: 0x00228F81
	public List<BnetPlayer> GetAddedFriends()
	{
		return this.m_friendsAdded;
	}

	// Token: 0x06006AF4 RID: 27380 RVA: 0x0022AD89 File Offset: 0x00228F89
	public List<BnetPlayer> GetUpdatedFriends()
	{
		return this.m_friendsUpdated;
	}

	// Token: 0x06006AF5 RID: 27381 RVA: 0x0022AD91 File Offset: 0x00228F91
	public List<BnetPlayer> GetRemovedFriends()
	{
		return this.m_friendsRemoved;
	}

	// Token: 0x06006AF6 RID: 27382 RVA: 0x0022AD99 File Offset: 0x00228F99
	public List<BnetPlayer> GetAddedStrangers()
	{
		return this.m_strangersAdded;
	}

	// Token: 0x06006AF7 RID: 27383 RVA: 0x0022ADA1 File Offset: 0x00228FA1
	public List<BnetPlayer> GetUpdatedStrangers()
	{
		return this.m_strangersUpdated;
	}

	// Token: 0x06006AF8 RID: 27384 RVA: 0x0022ADA9 File Offset: 0x00228FA9
	public List<BnetPlayer> GetRemovedStrangers()
	{
		return this.m_strangersRemoved;
	}

	// Token: 0x06006AF9 RID: 27385 RVA: 0x0022ADB4 File Offset: 0x00228FB4
	public bool IsEmpty()
	{
		return (this.m_playersAdded == null || this.m_playersAdded.Count <= 0) && (this.m_playersUpdated == null || this.m_playersUpdated.Count <= 0) && (this.m_playersRemoved == null || this.m_playersRemoved.Count <= 0) && (this.m_friendsAdded == null || this.m_friendsAdded.Count <= 0) && (this.m_friendsUpdated == null || this.m_friendsUpdated.Count <= 0) && (this.m_friendsRemoved == null || this.m_friendsRemoved.Count <= 0) && (this.m_strangersAdded == null || this.m_strangersAdded.Count <= 0) && (this.m_strangersUpdated == null || this.m_strangersUpdated.Count <= 0) && (this.m_strangersRemoved == null || this.m_strangersRemoved.Count <= 0);
	}

	// Token: 0x06006AFA RID: 27386 RVA: 0x0022AE9A File Offset: 0x0022909A
	public void Clear()
	{
		this.ClearAddedPlayers();
		this.ClearUpdatedPlayers();
		this.ClearRemovedPlayers();
		this.ClearAddedFriends();
		this.ClearUpdatedFriends();
		this.ClearRemovedFriends();
		this.ClearAddedStrangers();
		this.ClearUpdatedStrangers();
		this.ClearRemovedStrangers();
	}

	// Token: 0x06006AFB RID: 27387 RVA: 0x0022AED2 File Offset: 0x002290D2
	public bool AddAddedPlayer(BnetPlayer player)
	{
		if (this.m_playersAdded == null)
		{
			this.m_playersAdded = new List<BnetPlayer>();
		}
		else if (this.m_playersAdded.Contains(player))
		{
			return false;
		}
		this.m_playersAdded.Add(player);
		return true;
	}

	// Token: 0x06006AFC RID: 27388 RVA: 0x0022AF06 File Offset: 0x00229106
	public bool RemoveAddedPlayer(BnetPlayer player)
	{
		return this.m_playersAdded != null && this.m_playersAdded.Remove(player);
	}

	// Token: 0x06006AFD RID: 27389 RVA: 0x0022AF1E File Offset: 0x0022911E
	public void ClearAddedPlayers()
	{
		this.m_playersAdded = null;
	}

	// Token: 0x06006AFE RID: 27390 RVA: 0x0022AF27 File Offset: 0x00229127
	public bool AddUpdatedPlayer(BnetPlayer player)
	{
		if (this.m_playersUpdated == null)
		{
			this.m_playersUpdated = new List<BnetPlayer>();
		}
		else if (this.m_playersUpdated.Contains(player))
		{
			return false;
		}
		this.m_playersUpdated.Add(player);
		return true;
	}

	// Token: 0x06006AFF RID: 27391 RVA: 0x0022AF5B File Offset: 0x0022915B
	public bool RemoveUpdatedPlayer(BnetPlayer player)
	{
		return this.m_playersUpdated != null && this.m_playersUpdated.Remove(player);
	}

	// Token: 0x06006B00 RID: 27392 RVA: 0x0022AF73 File Offset: 0x00229173
	public void ClearUpdatedPlayers()
	{
		this.m_playersUpdated = null;
	}

	// Token: 0x06006B01 RID: 27393 RVA: 0x0022AF7C File Offset: 0x0022917C
	public bool AddRemovedPlayer(BnetPlayer player)
	{
		if (this.m_playersRemoved == null)
		{
			this.m_playersRemoved = new List<BnetPlayer>();
		}
		else if (this.m_playersRemoved.Contains(player))
		{
			return false;
		}
		this.m_playersRemoved.Add(player);
		return true;
	}

	// Token: 0x06006B02 RID: 27394 RVA: 0x0022AFB0 File Offset: 0x002291B0
	public bool RemoveRemovedPlayer(BnetPlayer player)
	{
		return this.m_playersRemoved != null && this.m_playersRemoved.Remove(player);
	}

	// Token: 0x06006B03 RID: 27395 RVA: 0x0022AFC8 File Offset: 0x002291C8
	public void ClearRemovedPlayers()
	{
		this.m_playersRemoved = null;
	}

	// Token: 0x06006B04 RID: 27396 RVA: 0x0022AFD1 File Offset: 0x002291D1
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

	// Token: 0x06006B05 RID: 27397 RVA: 0x0022B005 File Offset: 0x00229205
	public bool RemoveAddedFriend(BnetPlayer friend)
	{
		return this.m_friendsAdded != null && this.m_friendsAdded.Remove(friend);
	}

	// Token: 0x06006B06 RID: 27398 RVA: 0x0022B01D File Offset: 0x0022921D
	public void ClearAddedFriends()
	{
		this.m_friendsAdded = null;
	}

	// Token: 0x06006B07 RID: 27399 RVA: 0x0022B026 File Offset: 0x00229226
	public bool AddUpdatedFriend(BnetPlayer friend)
	{
		if (this.m_friendsUpdated == null)
		{
			this.m_friendsUpdated = new List<BnetPlayer>();
		}
		else if (this.m_friendsUpdated.Contains(friend))
		{
			return false;
		}
		this.m_friendsUpdated.Add(friend);
		return true;
	}

	// Token: 0x06006B08 RID: 27400 RVA: 0x0022B05A File Offset: 0x0022925A
	public bool RemoveUpdatedFriend(BnetPlayer friend)
	{
		return this.m_friendsUpdated != null && this.m_friendsUpdated.Remove(friend);
	}

	// Token: 0x06006B09 RID: 27401 RVA: 0x0022B072 File Offset: 0x00229272
	public void ClearUpdatedFriends()
	{
		this.m_friendsUpdated = null;
	}

	// Token: 0x06006B0A RID: 27402 RVA: 0x0022B07B File Offset: 0x0022927B
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

	// Token: 0x06006B0B RID: 27403 RVA: 0x0022B0AF File Offset: 0x002292AF
	public bool RemoveRemovedFriend(BnetPlayer friend)
	{
		return this.m_friendsRemoved != null && this.m_friendsRemoved.Remove(friend);
	}

	// Token: 0x06006B0C RID: 27404 RVA: 0x0022B0C7 File Offset: 0x002292C7
	public void ClearRemovedFriends()
	{
		this.m_friendsRemoved = null;
	}

	// Token: 0x06006B0D RID: 27405 RVA: 0x0022B0D0 File Offset: 0x002292D0
	public bool AddAddedStranger(BnetPlayer stranger)
	{
		if (this.m_strangersAdded == null)
		{
			this.m_strangersAdded = new List<BnetPlayer>();
		}
		else if (this.m_strangersAdded.Contains(stranger))
		{
			return false;
		}
		this.m_strangersAdded.Add(stranger);
		return true;
	}

	// Token: 0x06006B0E RID: 27406 RVA: 0x0022B104 File Offset: 0x00229304
	public bool RemoveAddedStranger(BnetPlayer stranger)
	{
		return this.m_strangersAdded != null && this.m_strangersAdded.Remove(stranger);
	}

	// Token: 0x06006B0F RID: 27407 RVA: 0x0022B11C File Offset: 0x0022931C
	public void ClearAddedStrangers()
	{
		this.m_strangersAdded = null;
	}

	// Token: 0x06006B10 RID: 27408 RVA: 0x0022B125 File Offset: 0x00229325
	public bool AddUpdatedStranger(BnetPlayer stranger)
	{
		if (this.m_strangersUpdated == null)
		{
			this.m_strangersUpdated = new List<BnetPlayer>();
		}
		else if (this.m_strangersUpdated.Contains(stranger))
		{
			return false;
		}
		this.m_strangersUpdated.Add(stranger);
		return true;
	}

	// Token: 0x06006B11 RID: 27409 RVA: 0x0022B159 File Offset: 0x00229359
	public bool RemoveUpdatedStranger(BnetPlayer stranger)
	{
		return this.m_strangersUpdated != null && this.m_strangersUpdated.Remove(stranger);
	}

	// Token: 0x06006B12 RID: 27410 RVA: 0x0022B171 File Offset: 0x00229371
	public void ClearUpdatedStrangers()
	{
		this.m_strangersUpdated = null;
	}

	// Token: 0x06006B13 RID: 27411 RVA: 0x0022B17A File Offset: 0x0022937A
	public bool AddRemovedStranger(BnetPlayer stranger)
	{
		if (this.m_strangersRemoved == null)
		{
			this.m_strangersRemoved = new List<BnetPlayer>();
		}
		else if (this.m_strangersRemoved.Contains(stranger))
		{
			return false;
		}
		this.m_strangersRemoved.Add(stranger);
		return true;
	}

	// Token: 0x06006B14 RID: 27412 RVA: 0x0022B1AE File Offset: 0x002293AE
	public bool RemoveRemovedStranger(BnetPlayer stranger)
	{
		return this.m_strangersRemoved != null && this.m_strangersRemoved.Remove(stranger);
	}

	// Token: 0x06006B15 RID: 27413 RVA: 0x0022B1C6 File Offset: 0x002293C6
	public void ClearRemovedStrangers()
	{
		this.m_strangersRemoved = null;
	}

	// Token: 0x040056FB RID: 22267
	private List<BnetPlayer> m_playersAdded;

	// Token: 0x040056FC RID: 22268
	private List<BnetPlayer> m_playersUpdated;

	// Token: 0x040056FD RID: 22269
	private List<BnetPlayer> m_playersRemoved;

	// Token: 0x040056FE RID: 22270
	private List<BnetPlayer> m_friendsAdded;

	// Token: 0x040056FF RID: 22271
	private List<BnetPlayer> m_friendsUpdated;

	// Token: 0x04005700 RID: 22272
	private List<BnetPlayer> m_friendsRemoved;

	// Token: 0x04005701 RID: 22273
	private List<BnetPlayer> m_strangersAdded;

	// Token: 0x04005702 RID: 22274
	private List<BnetPlayer> m_strangersUpdated;

	// Token: 0x04005703 RID: 22275
	private List<BnetPlayer> m_strangersRemoved;
}
