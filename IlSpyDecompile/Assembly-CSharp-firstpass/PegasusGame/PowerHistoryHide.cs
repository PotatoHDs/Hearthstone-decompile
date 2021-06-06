using System.IO;

namespace PegasusGame
{
	public class PowerHistoryHide : IProtoBuf
	{
		public int Entity { get; set; }

		public int Zone { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Entity.GetHashCode() ^ Zone.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			PowerHistoryHide powerHistoryHide = obj as PowerHistoryHide;
			if (powerHistoryHide == null)
			{
				return false;
			}
			if (!Entity.Equals(powerHistoryHide.Entity))
			{
				return false;
			}
			if (!Zone.Equals(powerHistoryHide.Zone))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PowerHistoryHide Deserialize(Stream stream, PowerHistoryHide instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PowerHistoryHide DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryHide powerHistoryHide = new PowerHistoryHide();
			DeserializeLengthDelimited(stream, powerHistoryHide);
			return powerHistoryHide;
		}

		public static PowerHistoryHide DeserializeLengthDelimited(Stream stream, PowerHistoryHide instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PowerHistoryHide Deserialize(Stream stream, PowerHistoryHide instance, long limit)
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
					instance.Entity = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Zone = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, PowerHistoryHide instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Entity);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Zone);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Entity) + ProtocolParser.SizeOfUInt64((ulong)Zone) + 2;
		}
	}
}
