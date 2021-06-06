using System;
using System.IO;

namespace Networking
{
	// Token: 0x02000FB3 RID: 4019
	internal class DefaultProtobufPacketDecoder<T> : IPacketDecoder where T : IProtoBuf, new()
	{
		// Token: 0x0600AFF1 RID: 45041 RVA: 0x0036623C File Offset: 0x0036443C
		public PegasusPacket DecodePacket(PegasusPacket packet)
		{
			byte[] buffer = (byte[])packet.Body;
			T t = Activator.CreateInstance<T>();
			t.Deserialize(new MemoryStream(buffer));
			packet.Body = t;
			return packet;
		}
	}
}
