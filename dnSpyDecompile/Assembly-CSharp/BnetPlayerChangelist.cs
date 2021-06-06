using System;
using System.Collections.Generic;
using bgs;

// Token: 0x02000771 RID: 1905
public class BnetPlayerChangelist
{
	// Token: 0x06006B88 RID: 27528 RVA: 0x0022D512 File Offset: 0x0022B712
	public List<BnetPlayerChange> GetChanges()
	{
		return this.m_changes;
	}

	// Token: 0x06006B89 RID: 27529 RVA: 0x0022D51A File Offset: 0x0022B71A
	public void AddChange(BnetPlayerChange change)
	{
		this.m_changes.Add(change);
	}

	// Token: 0x06006B8A RID: 27530 RVA: 0x0022D528 File Offset: 0x0022B728
	public bool HasChange(BnetGameAccountId id)
	{
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(id);
		return this.HasChange(player);
	}

	// Token: 0x06006B8B RID: 27531 RVA: 0x0022D548 File Offset: 0x0022B748
	public bool HasChange(BnetAccountId id)
	{
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(id);
		return this.HasChange(player);
	}

	// Token: 0x06006B8C RID: 27532 RVA: 0x0022D568 File Offset: 0x0022B768
	public bool HasChange(BnetPlayer player)
	{
		return this.FindChange(player) != null;
	}

	// Token: 0x06006B8D RID: 27533 RVA: 0x0022D574 File Offset: 0x0022B774
	public BnetPlayerChange FindChange(BnetGameAccountId id)
	{
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(id);
		return this.FindChange(player);
	}

	// Token: 0x06006B8E RID: 27534 RVA: 0x0022D594 File Offset: 0x0022B794
	public BnetPlayerChange FindChange(BnetAccountId id)
	{
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(id);
		return this.FindChange(player);
	}

	// Token: 0x06006B8F RID: 27535 RVA: 0x0022D5B4 File Offset: 0x0022B7B4
	public BnetPlayerChange FindChange(BnetPlayer player)
	{
		if (player == null)
		{
			return null;
		}
		return this.m_changes.Find((BnetPlayerChange change) => change.GetPlayer() == player);
	}

	// Token: 0x04005731 RID: 22321
	private List<BnetPlayerChange> m_changes = new List<BnetPlayerChange>();
}
