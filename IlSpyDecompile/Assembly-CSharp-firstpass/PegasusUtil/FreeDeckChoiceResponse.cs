using System.IO;

namespace PegasusUtil
{
	public class FreeDeckChoiceResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 334,
			System = 0
		}

		public bool Success { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Success.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			FreeDeckChoiceResponse freeDeckChoiceResponse = obj as FreeDeckChoiceResponse;
			if (freeDeckChoiceResponse == null)
			{
				return false;
			}
			if (!Success.Equals(freeDeckChoiceResponse.Success))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FreeDeckChoiceResponse Deserialize(Stream stream, FreeDeckChoiceResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FreeDeckChoiceResponse DeserializeLengthDelimited(Stream stream)
		{
			FreeDeckChoiceResponse freeDeckChoiceResponse = new FreeDeckChoiceResponse();
			DeserializeLengthDelimited(stream, freeDeckChoiceResponse);
			return freeDeckChoiceResponse;
		}

		public static FreeDeckChoiceResponse DeserializeLengthDelimited(Stream stream, FreeDeckChoiceResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FreeDeckChoiceResponse Deserialize(Stream stream, FreeDeckChoiceResponse instance, long limit)
		{
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 8:
					instance.Success = ProtocolParser.ReadBool(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, FreeDeckChoiceResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.Success);
		}

		public uint GetSerializedSize()
		{
			return 2u;
		}
	}
}
