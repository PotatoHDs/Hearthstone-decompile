using System.IO;

namespace PegasusShared
{
	public class BnetId : IProtoBuf
	{
		public ulong Hi { get; set; }

		public ulong Lo { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Hi.GetHashCode() ^ Lo.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			BnetId bnetId = obj as BnetId;
			if (bnetId == null)
			{
				return false;
			}
			if (!Hi.Equals(bnetId.Hi))
			{
				return false;
			}
			if (!Lo.Equals(bnetId.Lo))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BnetId Deserialize(Stream stream, BnetId instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BnetId DeserializeLengthDelimited(Stream stream)
		{
			BnetId bnetId = new BnetId();
			DeserializeLengthDelimited(stream, bnetId);
			return bnetId;
		}

		public static BnetId DeserializeLengthDelimited(Stream stream, BnetId instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BnetId Deserialize(Stream stream, BnetId instance, long limit)
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
					instance.Hi = ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Lo = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, BnetId instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.Hi);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.Lo);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64(Hi) + ProtocolParser.SizeOfUInt64(Lo) + 2;
		}
	}
}
