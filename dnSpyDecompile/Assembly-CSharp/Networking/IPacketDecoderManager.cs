using System;

namespace Networking
{
	// Token: 0x02000FB6 RID: 4022
	public interface IPacketDecoderManager
	{
		// Token: 0x0600AFF6 RID: 45046
		bool CanDecodePacket(int packetId);

		// Token: 0x0600AFF7 RID: 45047
		PegasusPacket DecodePacket(PegasusPacket packet);
	}
}
