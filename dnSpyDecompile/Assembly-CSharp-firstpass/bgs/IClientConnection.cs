using System;

namespace bgs
{
	// Token: 0x02000220 RID: 544
	public interface IClientConnection<T> where T : PacketFormat
	{
		// Token: 0x060022F6 RID: 8950
		void Connect(string host, uint port, int tryCount);

		// Token: 0x060022F7 RID: 8951
		void Disconnect();

		// Token: 0x060022F8 RID: 8952
		void SendPacket(T packet);

		// Token: 0x060022F9 RID: 8953
		void Update();

		// Token: 0x060022FA RID: 8954
		void AddListener(IClientConnectionListener<T> listener, object state);

		// Token: 0x060022FB RID: 8955
		void RemoveListener(IClientConnectionListener<T> listener);

		// Token: 0x060022FC RID: 8956
		bool AddConnectHandler(ConnectHandler handler);

		// Token: 0x060022FD RID: 8957
		bool RemoveConnectHandler(ConnectHandler handler);

		// Token: 0x060022FE RID: 8958
		bool AddDisconnectHandler(DisconnectHandler handler);

		// Token: 0x060022FF RID: 8959
		bool RemoveDisconnectHandler(DisconnectHandler handler);
	}
}
