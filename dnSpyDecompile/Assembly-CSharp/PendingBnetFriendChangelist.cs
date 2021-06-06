using System;
using System.Collections.Generic;
using bgs;

// Token: 0x02000767 RID: 1895
public class PendingBnetFriendChangelist
{
	// Token: 0x06006A46 RID: 27206 RVA: 0x0022949A File Offset: 0x0022769A
	public List<BnetPlayer> GetFriends()
	{
		return this.m_friends;
	}

	// Token: 0x06006A47 RID: 27207 RVA: 0x002294A2 File Offset: 0x002276A2
	public bool Add(BnetPlayer friend)
	{
		if (this.m_friends.Contains(friend))
		{
			return false;
		}
		this.m_friends.Add(friend);
		return true;
	}

	// Token: 0x06006A48 RID: 27208 RVA: 0x002294C1 File Offset: 0x002276C1
	public bool Remove(BnetPlayer friend)
	{
		return this.m_friends.Remove(friend);
	}

	// Token: 0x06006A49 RID: 27209 RVA: 0x002294CF File Offset: 0x002276CF
	public void Clear()
	{
		this.m_friends.Clear();
	}

	// Token: 0x06006A4A RID: 27210 RVA: 0x002294DC File Offset: 0x002276DC
	public int GetCount()
	{
		return this.m_friends.Count;
	}

	// Token: 0x06006A4B RID: 27211 RVA: 0x002294EC File Offset: 0x002276EC
	public BnetPlayer FindFriend(BnetAccountId id)
	{
		foreach (BnetPlayer bnetPlayer in this.m_friends)
		{
			if (bnetPlayer.GetAccountId() == id)
			{
				return bnetPlayer;
			}
		}
		return null;
	}

	// Token: 0x06006A4C RID: 27212 RVA: 0x00229550 File Offset: 0x00227750
	public BnetPlayer FindFriend(BnetGameAccountId id)
	{
		foreach (BnetPlayer bnetPlayer in this.m_friends)
		{
			if (bnetPlayer.HasGameAccount(id))
			{
				return bnetPlayer;
			}
		}
		return null;
	}

	// Token: 0x06006A4D RID: 27213 RVA: 0x002295AC File Offset: 0x002277AC
	public bool IsFriend(BnetPlayer player)
	{
		if (this.m_friends.Contains(player))
		{
			return true;
		}
		if (player == null)
		{
			return false;
		}
		BnetAccountId accountId = player.GetAccountId();
		if (accountId != null)
		{
			return this.IsFriend(accountId);
		}
		foreach (BnetGameAccountId id in player.GetGameAccounts().Keys)
		{
			if (this.IsFriend(id))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06006A4E RID: 27214 RVA: 0x0022963C File Offset: 0x0022783C
	public bool IsFriend(BnetAccountId id)
	{
		return this.FindFriend(id) != null;
	}

	// Token: 0x06006A4F RID: 27215 RVA: 0x00229648 File Offset: 0x00227848
	public bool IsFriend(BnetGameAccountId id)
	{
		return this.FindFriend(id) != null;
	}

	// Token: 0x06006A50 RID: 27216 RVA: 0x00229654 File Offset: 0x00227854
	public BnetFriendChangelist CreateChangelist()
	{
		BnetFriendChangelist bnetFriendChangelist = new BnetFriendChangelist();
		for (int i = this.m_friends.Count - 1; i >= 0; i--)
		{
			BnetPlayer bnetPlayer = this.m_friends[i];
			if (bnetPlayer.IsDisplayable())
			{
				bnetFriendChangelist.AddAddedFriend(bnetPlayer);
				this.m_friends.RemoveAt(i);
			}
		}
		return bnetFriendChangelist;
	}

	// Token: 0x040056DC RID: 22236
	private List<BnetPlayer> m_friends = new List<BnetPlayer>();
}
