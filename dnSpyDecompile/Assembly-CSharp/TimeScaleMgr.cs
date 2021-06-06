using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000934 RID: 2356
public class TimeScaleMgr
{
	// Token: 0x0600821F RID: 33311 RVA: 0x002A4FFB File Offset: 0x002A31FB
	public static TimeScaleMgr Get()
	{
		if (TimeScaleMgr.s_instance == null)
		{
			TimeScaleMgr.s_instance = new TimeScaleMgr();
		}
		return TimeScaleMgr.s_instance;
	}

	// Token: 0x06008220 RID: 33312 RVA: 0x002A5013 File Offset: 0x002A3213
	public void PushTemporarySpeedIncrease(float t)
	{
		this.m_temporarySpeedIncrease.Push(this.GetTimeScaleMultiplier());
		if (t > this.GetTimeScaleMultiplier())
		{
			this.SetTimeScaleMultiplier(t);
		}
	}

	// Token: 0x06008221 RID: 33313 RVA: 0x002A5036 File Offset: 0x002A3236
	public float PopTemporarySpeedIncrease()
	{
		if (this.m_temporarySpeedIncrease.Count > 0)
		{
			this.SetTimeScaleMultiplier(this.m_temporarySpeedIncrease.Pop());
		}
		return this.GetTimeScaleMultiplier();
	}

	// Token: 0x06008222 RID: 33314 RVA: 0x002A505D File Offset: 0x002A325D
	private TimeScaleMgr()
	{
	}

	// Token: 0x06008223 RID: 33315 RVA: 0x002A5086 File Offset: 0x002A3286
	public float GetGameTimeScale()
	{
		return this.m_gameTimeScale;
	}

	// Token: 0x06008224 RID: 33316 RVA: 0x002A508E File Offset: 0x002A328E
	public void SetGameTimeScale(float t)
	{
		this.m_gameTimeScale = t;
		this.Update();
	}

	// Token: 0x06008225 RID: 33317 RVA: 0x002A509D File Offset: 0x002A329D
	public float GetTimeScaleMultiplier()
	{
		return this.m_timeScaleMultiplier;
	}

	// Token: 0x06008226 RID: 33318 RVA: 0x002A50A5 File Offset: 0x002A32A5
	public void SetTimeScaleMultiplier(float x)
	{
		this.m_timeScaleMultiplier = x;
		this.Update();
	}

	// Token: 0x06008227 RID: 33319 RVA: 0x002A50B4 File Offset: 0x002A32B4
	private void Update()
	{
		Time.timeScale = this.m_gameTimeScale * this.m_timeScaleMultiplier;
	}

	// Token: 0x04006D11 RID: 27921
	public const float MinTimeScale = 0.01f;

	// Token: 0x04006D12 RID: 27922
	public const float MaxTimeScale = 4f;

	// Token: 0x04006D13 RID: 27923
	private static TimeScaleMgr s_instance;

	// Token: 0x04006D14 RID: 27924
	private Stack<float> m_temporarySpeedIncrease = new Stack<float>();

	// Token: 0x04006D15 RID: 27925
	private float m_gameTimeScale = 1f;

	// Token: 0x04006D16 RID: 27926
	private float m_timeScaleMultiplier = 1f;
}
