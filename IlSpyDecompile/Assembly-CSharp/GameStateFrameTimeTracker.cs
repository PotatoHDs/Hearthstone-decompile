using System;
using UnityEngine;

public class GameStateFrameTimeTracker : IGameStateTimeTracker
{
	protected float[] m_frameTimeBuffer;

	protected int m_lastBufferPos = -1;

	protected float m_desiredFrameTimeReal;

	protected float m_accruedLostFrameTimeReal;

	private GameStateFrameTimeTracker()
		: this(15)
	{
	}

	public GameStateFrameTimeTracker(int bufferSize, float desiredFrameTimeInSeconds = 0f)
	{
		m_frameTimeBuffer = new float[bufferSize];
		for (int i = 0; i < bufferSize; i++)
		{
			m_frameTimeBuffer[i] = 0.016667f;
		}
		if (desiredFrameTimeInSeconds > 0f)
		{
			m_desiredFrameTimeReal = Math.Max(desiredFrameTimeInSeconds, 0.016667f);
		}
	}

	public void Update()
	{
		m_lastBufferPos = (m_lastBufferPos + 1) % m_frameTimeBuffer.Length;
		m_frameTimeBuffer[m_lastBufferPos] = Time.unscaledDeltaTime;
		if (m_desiredFrameTimeReal > 0f && Time.unscaledDeltaTime > m_desiredFrameTimeReal)
		{
			m_accruedLostFrameTimeReal += Time.unscaledDeltaTime - m_desiredFrameTimeReal;
		}
	}

	public void AdjustAccruedLostTime(float deltaSeconds)
	{
		m_accruedLostFrameTimeReal += deltaSeconds;
		m_accruedLostFrameTimeReal = Math.Max(m_accruedLostFrameTimeReal, 0f);
	}

	public void ResetAccruedLostTime()
	{
		m_accruedLostFrameTimeReal = 0f;
	}

	private float GetAverageFrameTimeInSeconds()
	{
		float num = 0f;
		float num2 = 1f / (float)m_frameTimeBuffer.Length;
		for (int i = 0; i < m_frameTimeBuffer.Length; i++)
		{
			num += m_frameTimeBuffer[i] * num2;
		}
		return num;
	}

	public float GetAverageFPS()
	{
		float averageFrameTimeInSeconds = GetAverageFrameTimeInSeconds();
		return 1f / averageFrameTimeInSeconds;
	}

	public float GetAccruedLostTimeInSeconds()
	{
		return m_accruedLostFrameTimeReal;
	}
}
