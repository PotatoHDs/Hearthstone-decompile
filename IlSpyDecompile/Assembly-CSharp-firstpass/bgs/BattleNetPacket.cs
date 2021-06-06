using System;
using System.IO;
using bnet.protocol;

namespace bgs
{
	public class BattleNetPacket : PacketFormat
	{
		private Header header;

		private object body;

		private int headerSize = -1;

		private int bodySize = -1;

		public Header GetHeader()
		{
			return header;
		}

		public object GetBody()
		{
			return body;
		}

		public BattleNetPacket()
		{
			header = null;
			body = null;
		}

		public BattleNetPacket(Header h, IProtoBuf b)
		{
			header = h;
			body = b;
		}

		public override bool IsLoaded()
		{
			if (header != null)
			{
				return body != null;
			}
			return false;
		}

		public override int Decode(byte[] bytes, int offset, int available)
		{
			int num = 0;
			if (headerSize < 0)
			{
				if (available < 2)
				{
					return num;
				}
				headerSize = (bytes[offset] << 8) + bytes[offset + 1];
				available -= 2;
				num += 2;
				offset += 2;
			}
			if (header == null)
			{
				if (available < headerSize)
				{
					return num;
				}
				header = new Header();
				header.Deserialize(new MemoryStream(bytes, offset, headerSize));
				bodySize = (int)(header.HasSize ? header.Size : 0);
				if (header == null)
				{
					throw new Exception("failed to parse BattleNet packet header");
				}
				available -= headerSize;
				num += headerSize;
				offset += headerSize;
			}
			if (body == null)
			{
				if (available < bodySize)
				{
					return num;
				}
				byte[] destinationArray = new byte[bodySize];
				Array.Copy(bytes, offset, destinationArray, 0, bodySize);
				body = destinationArray;
				num += bodySize;
			}
			return num;
		}

		public override byte[] Encode()
		{
			if (!(body is IProtoBuf))
			{
				return null;
			}
			IProtoBuf obj = (IProtoBuf)body;
			int serializedSize = (int)header.GetSerializedSize();
			int serializedSize2 = (int)obj.GetSerializedSize();
			byte[] array = new byte[2 + serializedSize + serializedSize2];
			array[0] = (byte)((uint)(serializedSize >> 8) & 0xFFu);
			array[1] = (byte)((uint)serializedSize & 0xFFu);
			header.Serialize(new MemoryStream(array, 2, serializedSize));
			obj.Serialize(new MemoryStream(array, 2 + serializedSize, serializedSize2));
			return array;
		}

		public override string ToString()
		{
			if (header == null)
			{
				return "BattleNetPacket (missing header)";
			}
			return $"BattleNetPacket ServiceID: {header.ServiceId}  MethodId: {header.MethodId}";
		}

		public override bool IsFatalOnError()
		{
			return false;
		}
	}
}
