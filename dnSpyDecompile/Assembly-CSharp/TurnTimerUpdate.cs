using System;

// Token: 0x0200034F RID: 847
public class TurnTimerUpdate
{
	// Token: 0x0600313B RID: 12603 RVA: 0x000FD325 File Offset: 0x000FB525
	public float GetSecondsRemaining()
	{
		return this.m_secondsRemaining;
	}

	// Token: 0x0600313C RID: 12604 RVA: 0x000FD32D File Offset: 0x000FB52D
	public void SetSecondsRemaining(float sec)
	{
		this.m_secondsRemaining = sec;
	}

	// Token: 0x0600313D RID: 12605 RVA: 0x000FD336 File Offset: 0x000FB536
	public float GetEndTimestamp()
	{
		return this.m_endTimestamp;
	}

	// Token: 0x0600313E RID: 12606 RVA: 0x000FD33E File Offset: 0x000FB53E
	public void SetEndTimestamp(float timestamp)
	{
		this.m_endTimestamp = timestamp;
	}

	// Token: 0x0600313F RID: 12607 RVA: 0x000FD347 File Offset: 0x000FB547
	public bool ShouldShow()
	{
		return this.m_show;
	}

	// Token: 0x06003140 RID: 12608 RVA: 0x000FD34F File Offset: 0x000FB54F
	public void SetShow(bool show)
	{
		this.m_show = show;
	}

	// Token: 0x04001B61 RID: 7009
	private float m_secondsRemaining;

	// Token: 0x04001B62 RID: 7010
	private float m_endTimestamp;

	// Token: 0x04001B63 RID: 7011
	private bool m_show;
}
