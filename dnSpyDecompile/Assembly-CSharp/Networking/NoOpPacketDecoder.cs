using System;

namespace Networking
{
	// Token: 0x02000FB7 RID: 4023
	internal class NoOpPacketDecoder : IPacketDecoder
	{
		// Token: 0x0600AFF8 RID: 45048 RVA: 0x00366298 File Offset: 0x00364498
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
