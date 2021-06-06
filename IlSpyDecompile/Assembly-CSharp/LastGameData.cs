using PegasusShared;

public class LastGameData
{
	public TAG_PLAYSTATE GameResult { get; set; }

	public int WhizbangDeckID { get; set; }

	public GameConnectionInfo GameConnectionInfo { get; set; }

	public int BattlegroundsLeaderboardPlace { get; set; }

	public LastGameData()
	{
		Clear();
	}

	public void Clear()
	{
		GameResult = TAG_PLAYSTATE.INVALID;
		WhizbangDeckID = 0;
		GameConnectionInfo = null;
	}

	public bool HasWhizbangDeckID()
	{
		return WhizbangDeckID > 0;
	}
}
