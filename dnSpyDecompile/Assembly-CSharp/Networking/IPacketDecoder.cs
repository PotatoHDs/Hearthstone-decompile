using System;

namespace Networking
{
	// Token: 0x02000FB5 RID: 4021
	public interface IPacketDecoder
	{
		// Token: 0x0600AFF5 RID: 45045
		PegasusPacket DecodePacket(PegasusPacket p);
	}
}
