namespace bgs
{
	public interface IClientConnection<T> where T : PacketFormat
	{
		void Connect(string host, uint port, int tryCount);

		void Disconnect();

		void SendPacket(T packet);

		void Update();

		void AddListener(IClientConnectionListener<T> listener, object state);

		void RemoveListener(IClientConnectionListener<T> listener);

		bool AddConnectHandler(ConnectHandler handler);

		bool RemoveConnectHandler(ConnectHandler handler);

		bool AddDisconnectHandler(DisconnectHandler handler);

		bool RemoveDisconnectHandler(DisconnectHandler handler);
	}
}
