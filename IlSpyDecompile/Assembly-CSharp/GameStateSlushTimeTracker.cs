using System;

public class GameStateSlushTimeTracker : IGameStateTimeTracker
{
	protected float m_accruedLostFrameTimeReal;

	public void Update()
	{
	}

	public void AdjustAccruedLostTime(float deltaSeconds)
	{
		m_accruedLostFrameTimeReal = deltaSeconds;
		m_accruedLostFrameTimeReal = Math.Max(m_accruedLostFrameTimeReal, 0f);
	}

	public void ResetAccruedLostTime()
	{
		m_accruedLostFrameTimeReal = 0f;
	}

	public float GetAccruedLostTimeInSeconds()
	{
		return m_accruedLostFrameTimeReal;
	}
}
