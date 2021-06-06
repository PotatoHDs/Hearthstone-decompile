using System;
using UnityEngine;

// Token: 0x02000311 RID: 785
public class GameStateFrameTimeTracker : IGameStateTimeTracker
{
	// Token: 0x06002C3B RID: 11323 RVA: 0x000DE66F File Offset: 0x000DC86F
	private GameStateFrameTimeTracker() : this(15, 0f)
	{
	}

	// Token: 0x06002C3C RID: 11324 RVA: 0x000DE680 File Offset: 0x000DC880
	public GameStateFrameTimeTracker(int bufferSize, float desiredFrameTimeInSeconds = 0f)
	{
		this.m_frameTimeBuffer = new float[bufferSize];
		for (int i = 0; i < bufferSize; i++)
		{
			this.m_frameTimeBuffer[i] = 0.016667f;
		}
		if (desiredFrameTimeInSeconds > 0f)
		{
			this.m_desiredFrameTimeReal = Math.Max(desiredFrameTimeInSeconds, 0.016667f);
		}
	}

	// Token: 0x06002C3D RID: 11325 RVA: 0x000DE6D8 File Offset: 0x000DC8D8
	public void Update()
	{
		this.m_lastBufferPos = (this.m_lastBufferPos + 1) % this.m_frameTimeBuffer.Length;
		this.m_frameTimeBuffer[this.m_lastBufferPos] = Time.unscaledDeltaTime;
		if (this.m_desiredFrameTimeReal > 0f && Time.unscaledDeltaTime > this.m_desiredFrameTimeReal)
		{
			this.m_accruedLostFrameTimeReal += Time.unscaledDeltaTime - this.m_desiredFrameTimeReal;
		}
	}

	// Token: 0x06002C3E RID: 11326 RVA: 0x000DE741 File Offset: 0x000DC941
	public void AdjustAccruedLostTime(float deltaSeconds)
	{
		this.m_accruedLostFrameTimeReal += deltaSeconds;
		this.m_accruedLostFrameTimeReal = Math.Max(this.m_accruedLostFrameTimeReal, 0f);
	}

	// Token: 0x06002C3F RID: 11327 RVA: 0x000DE767 File Offset: 0x000DC967
	public void ResetAccruedLostTime()
	{
		this.m_accruedLostFrameTimeReal = 0f;
	}

	// Token: 0x06002C40 RID: 11328 RVA: 0x000DE774 File Offset: 0x000DC974
	private float GetAverageFrameTimeInSeconds()
	{
		float num = 0f;
		float num2 = 1f / (float)this.m_frameTimeBuffer.Length;
		for (int i = 0; i < this.m_frameTimeBuffer.Length; i++)
		{
			num += this.m_frameTimeBuffer[i] * num2;
		}
		return num;
	}

	// Token: 0x06002C41 RID: 11329 RVA: 0x000DE7B8 File Offset: 0x000DC9B8
	public float GetAverageFPS()
	{
		float averageFrameTimeInSeconds = this.GetAverageFrameTimeInSeconds();
		return 1f / averageFrameTimeInSeconds;
	}

	// Token: 0x06002C42 RID: 11330 RVA: 0x000DE7D3 File Offset: 0x000DC9D3
	public float GetAccruedLostTimeInSeconds()
	{
		return this.m_accruedLostFrameTimeReal;
	}

	// Token: 0x04001858 RID: 6232
	protected float[] m_frameTimeBuffer;

	// Token: 0x04001859 RID: 6233
	protected int m_lastBufferPos = -1;

	// Token: 0x0400185A RID: 6234
	protected float m_desiredFrameTimeReal;

	// Token: 0x0400185B RID: 6235
	protected float m_accruedLostFrameTimeReal;
}
