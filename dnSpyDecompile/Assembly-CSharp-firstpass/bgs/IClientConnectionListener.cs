using System;

namespace bgs
{
	// Token: 0x02000221 RID: 545
	public interface IClientConnectionListener<PacketType> where PacketType : PacketFormat
	{
		// Token: 0x06002300 RID: 8960
		void PacketReceived(PacketType p, object state);
	}
}
