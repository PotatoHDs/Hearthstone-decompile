using System.Collections.Generic;
using UnityEngine;

public class TimeScaleMgr
{
	public const float MinTimeScale = 0.01f;

	public const float MaxTimeScale = 4f;

	private static TimeScaleMgr s_instance;

	private Stack<float> m_temporarySpeedIncrease = new Stack<float>();

	private float m_gameTimeScale = 1f;

	private float m_timeScaleMultiplier = 1f;

	public static TimeScaleMgr Get()
	{
		if (s_instance == null)
		{
			s_instance = new TimeScaleMgr();
		}
		return s_instance;
	}

	public void PushTemporarySpeedIncrease(float t)
	{
		m_temporarySpeedIncrease.Push(GetTimeScaleMultiplier());
		if (t > GetTimeScaleMultiplier())
		{
			SetTimeScaleMultiplier(t);
		}
	}

	public float PopTemporarySpeedIncrease()
	{
		if (m_temporarySpeedIncrease.Count > 0)
		{
			SetTimeScaleMultiplier(m_temporarySpeedIncrease.Pop());
		}
		return GetTimeScaleMultiplier();
	}

	private TimeScaleMgr()
	{
	}

	public float GetGameTimeScale()
	{
		return m_gameTimeScale;
	}

	public void SetGameTimeScale(float t)
	{
		m_gameTimeScale = t;
		Update();
	}

	public float GetTimeScaleMultiplier()
	{
		return m_timeScaleMultiplier;
	}

	public void SetTimeScaleMultiplier(float x)
	{
		m_timeScaleMultiplier = x;
		Update();
	}

	private void Update()
	{
		Time.timeScale = m_gameTimeScale * m_timeScaleMultiplier;
	}
}
