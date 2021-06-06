using System;
using bgs.types;

// Token: 0x020008C3 RID: 2243
public class FindGameEventData
{
	// Token: 0x040064FA RID: 25850
	public FindGameState m_state;

	// Token: 0x040064FB RID: 25851
	public GameServerInfo m_gameServer;

	// Token: 0x040064FC RID: 25852
	public Network.GameCancelInfo m_cancelInfo;

	// Token: 0x040064FD RID: 25853
	public int m_queueMinSeconds;

	// Token: 0x040064FE RID: 25854
	public int m_queueMaxSeconds;
}
