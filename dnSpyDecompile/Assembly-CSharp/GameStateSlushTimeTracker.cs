using System;

// Token: 0x02000312 RID: 786
public class GameStateSlushTimeTracker : IGameStateTimeTracker
{
	// Token: 0x06002C43 RID: 11331 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Update()
	{
	}

	// Token: 0x06002C44 RID: 11332 RVA: 0x000DE7DB File Offset: 0x000DC9DB
	public void AdjustAccruedLostTime(float deltaSeconds)
	{
		this.m_accruedLostFrameTimeReal = deltaSeconds;
		this.m_accruedLostFrameTimeReal = Math.Max(this.m_accruedLostFrameTimeReal, 0f);
	}

	// Token: 0x06002C45 RID: 11333 RVA: 0x000DE7FA File Offset: 0x000DC9FA
	public void ResetAccruedLostTime()
	{
		this.m_accruedLostFrameTimeReal = 0f;
	}

	// Token: 0x06002C46 RID: 11334 RVA: 0x000DE807 File Offset: 0x000DCA07
	public float GetAccruedLostTimeInSeconds()
	{
		return this.m_accruedLostFrameTimeReal;
	}

	// Token: 0x0400185C RID: 6236
	protected float m_accruedLostFrameTimeReal;
}
