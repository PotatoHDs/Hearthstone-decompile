using System;

namespace bgs
{
	// Token: 0x02000228 RID: 552
	public interface ISocketEventListener
	{
		// Token: 0x06002320 RID: 8992
		void ConnectEvent(string host, uint port);

		// Token: 0x06002321 RID: 8993
		void DisconnectEvent(string host, uint port);

		// Token: 0x06002322 RID: 8994
		void SendPacketEvent(string host, uint port, uint bytes);

		// Token: 0x06002323 RID: 8995
		void ReceivePacketEvent(string host, uint port, uint bytes);
	}
}
