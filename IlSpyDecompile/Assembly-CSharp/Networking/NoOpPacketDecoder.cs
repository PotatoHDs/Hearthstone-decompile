namespace Networking
{
	internal class NoOpPacketDecoder : IPacketDecoder
	{
		public PegasusPacket DecodePacket(PegasusPacket packet)
		{
			return new PegasusPacket
			{
				Type = 254,
				Context = packet.Context
			};
		}
	}
}
