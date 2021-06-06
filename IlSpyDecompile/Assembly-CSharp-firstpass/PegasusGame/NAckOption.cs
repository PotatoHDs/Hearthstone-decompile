using System.IO;

namespace PegasusGame
{
	public class NAckOption : IProtoBuf
	{
		public enum PacketID
		{
			ID = 10
		}

		public int Id { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Id.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			NAckOption nAckOption = obj as NAckOption;
			if (nAckOption == null)
			{
				return false;
			}
			if (!Id.Equals(nAckOption.Id))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static NAckOption Deserialize(Stream stream, NAckOption instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static NAckOption DeserializeLengthDelimited(Stream stream)
		{
			NAckOption nAckOption = new NAckOption();
			DeserializeLengthDelimited(stream, nAckOption);
			return nAckOption;
		}

		public static NAckOption DeserializeLengthDelimited(Stream stream, NAckOption instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static NAckOption Deserialize(Stream stream, NAckOption instance, long limit)
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
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, NAckOption instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Id) + 1;
		}
	}
}
