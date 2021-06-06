public class BnetPlayerChange
{
	private BnetPlayer m_oldPlayer;

	private BnetPlayer m_newPlayer;

	public BnetPlayer GetOldPlayer()
	{
		return m_oldPlayer;
	}

	public void SetOldPlayer(BnetPlayer player)
	{
		m_oldPlayer = player;
	}

	public BnetPlayer GetNewPlayer()
	{
		return m_newPlayer;
	}

	public void SetNewPlayer(BnetPlayer player)
	{
		m_newPlayer = player;
	}

	public BnetPlayer GetPlayer()
	{
		return m_newPlayer;
	}
}
