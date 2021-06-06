using bgs;

public class PlayerChatInfo
{
	private BnetPlayer m_player;

	private float m_lastFocusTime;

	private BnetWhisper m_lastSeenWhisper;

	public BnetPlayer GetPlayer()
	{
		return m_player;
	}

	public void SetPlayer(BnetPlayer player)
	{
		m_player = player;
	}

	public float GetLastFocusTime()
	{
		return m_lastFocusTime;
	}

	public void SetLastFocusTime(float time)
	{
		m_lastFocusTime = time;
	}

	public BnetWhisper GetLastSeenWhisper()
	{
		return m_lastSeenWhisper;
	}

	public void SetLastSeenWhisper(BnetWhisper whisper)
	{
		m_lastSeenWhisper = whisper;
	}
}
