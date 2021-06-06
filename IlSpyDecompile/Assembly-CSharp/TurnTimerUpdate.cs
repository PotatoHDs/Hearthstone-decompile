public class TurnTimerUpdate
{
	private float m_secondsRemaining;

	private float m_endTimestamp;

	private bool m_show;

	public float GetSecondsRemaining()
	{
		return m_secondsRemaining;
	}

	public void SetSecondsRemaining(float sec)
	{
		m_secondsRemaining = sec;
	}

	public float GetEndTimestamp()
	{
		return m_endTimestamp;
	}

	public void SetEndTimestamp(float timestamp)
	{
		m_endTimestamp = timestamp;
	}

	public bool ShouldShow()
	{
		return m_show;
	}

	public void SetShow(bool show)
	{
		m_show = show;
	}
}
