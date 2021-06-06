using System.IO;

namespace Networking
{
	internal class DefaultProtobufPacketDecoder<T> : IPacketDecoder where T : IProtoBuf, new()
	{
		public PegasusPacket DecodePacket(PegasusPacket packet)
		{
			byte[] buffer = (byte[])packet.Body;
			T val = new T();
			val.Deserialize(new MemoryStream(buffer));
			packet.Body = val;
			return packet;
		}
	}
}
