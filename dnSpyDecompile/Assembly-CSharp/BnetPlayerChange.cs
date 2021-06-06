using System;

// Token: 0x02000770 RID: 1904
public class BnetPlayerChange
{
	// Token: 0x06006B82 RID: 27522 RVA: 0x0022D4F0 File Offset: 0x0022B6F0
	public BnetPlayer GetOldPlayer()
	{
		return this.m_oldPlayer;
	}

	// Token: 0x06006B83 RID: 27523 RVA: 0x0022D4F8 File Offset: 0x0022B6F8
	public void SetOldPlayer(BnetPlayer player)
	{
		this.m_oldPlayer = player;
	}

	// Token: 0x06006B84 RID: 27524 RVA: 0x0022D501 File Offset: 0x0022B701
	public BnetPlayer GetNewPlayer()
	{
		return this.m_newPlayer;
	}

	// Token: 0x06006B85 RID: 27525 RVA: 0x0022D509 File Offset: 0x0022B709
	public void SetNewPlayer(BnetPlayer player)
	{
		this.m_newPlayer = player;
	}

	// Token: 0x06006B86 RID: 27526 RVA: 0x0022D501 File Offset: 0x0022B701
	public BnetPlayer GetPlayer()
	{
		return this.m_newPlayer;
	}

	// Token: 0x0400572F RID: 22319
	private BnetPlayer m_oldPlayer;

	// Token: 0x04005730 RID: 22320
	private BnetPlayer m_newPlayer;
}
