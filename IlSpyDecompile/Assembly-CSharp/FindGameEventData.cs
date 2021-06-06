using bgs.types;

public class FindGameEventData
{
	public FindGameState m_state;

	public GameServerInfo m_gameServer;

	public Network.GameCancelInfo m_cancelInfo;

	public int m_queueMinSeconds;

	public int m_queueMaxSeconds;
}
