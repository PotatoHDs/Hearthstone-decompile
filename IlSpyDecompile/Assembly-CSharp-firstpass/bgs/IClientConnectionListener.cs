namespace bgs
{
	public interface IClientConnectionListener<PacketType> where PacketType : PacketFormat
	{
		void PacketReceived(PacketType p, object state);
	}
}
