using System.IO;

namespace PegasusGame
{
	public class PowerHistoryCachedTagForDormantChange : IProtoBuf
	{
		public int Entity { get; set; }

		public int Tag { get; set; }

		public int Value { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Entity.GetHashCode() ^ Tag.GetHashCode() ^ Value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			PowerHistoryCachedTagForDormantChange powerHistoryCachedTagForDormantChange = obj as PowerHistoryCachedTagForDormantChange;
			if (powerHistoryCachedTagForDormantChange == null)
			{
				return false;
			}
			if (!Entity.Equals(powerHistoryCachedTagForDormantChange.Entity))
			{
				return false;
			}
			if (!Tag.Equals(powerHistoryCachedTagForDormantChange.Tag))
			{
				return false;
			}
			if (!Value.Equals(powerHistoryCachedTagForDormantChange.Value))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PowerHistoryCachedTagForDormantChange Deserialize(Stream stream, PowerHistoryCachedTagForDormantChange instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PowerHistoryCachedTagForDormantChange DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryCachedTagForDormantChange powerHistoryCachedTagForDormantChange = new PowerHistoryCachedTagForDormantChange();
			DeserializeLengthDelimited(stream, powerHistoryCachedTagForDormantChange);
			return powerHistoryCachedTagForDormantChange;
		}

		public static PowerHistoryCachedTagForDormantChange DeserializeLengthDelimited(Stream stream, PowerHistoryCachedTagForDormantChange instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PowerHistoryCachedTagForDormantChange Deserialize(Stream stream, PowerHistoryCachedTagForDormantChange instance, long limit)
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
					instance.Tag = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Value = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, PowerHistoryCachedTagForDormantChange instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Entity);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Tag);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Value);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Entity) + ProtocolParser.SizeOfUInt64((ulong)Tag) + ProtocolParser.SizeOfUInt64((ulong)Value) + 3;
		}
	}
}
