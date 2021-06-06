namespace Networking
{
	public interface IPacketDecoder
	{
		PegasusPacket DecodePacket(PegasusPacket p);
	}
}
