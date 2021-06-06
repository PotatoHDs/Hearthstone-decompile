namespace bgs
{
	public interface ISocketEventListener
	{
		void ConnectEvent(string host, uint port);

		void DisconnectEvent(string host, uint port);

		void SendPacketEvent(string host, uint port, uint bytes);

		void ReceivePacketEvent(string host, uint port, uint bytes);
	}
}
