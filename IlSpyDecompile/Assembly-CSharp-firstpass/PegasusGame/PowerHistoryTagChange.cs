using System.IO;

namespace PegasusGame
{
	public class PowerHistoryTagChange : IProtoBuf
	{
		public bool HasChangeDef;

		private bool _ChangeDef;

		public int Entity { get; set; }

		public int Tag { get; set; }

		public int Value { get; set; }

		public bool ChangeDef
		{
			get
			{
				return _ChangeDef;
			}
			set
			{
				_ChangeDef = value;
				HasChangeDef = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Entity.GetHashCode();
			hashCode ^= Tag.GetHashCode();
			hashCode ^= Value.GetHashCode();
			if (HasChangeDef)
			{
				hashCode ^= ChangeDef.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			PowerHistoryTagChange powerHistoryTagChange = obj as PowerHistoryTagChange;
			if (powerHistoryTagChange == null)
			{
				return false;
			}
			if (!Entity.Equals(powerHistoryTagChange.Entity))
			{
				return false;
			}
			if (!Tag.Equals(powerHistoryTagChange.Tag))
			{
				return false;
			}
			if (!Value.Equals(powerHistoryTagChange.Value))
			{
				return false;
			}
			if (HasChangeDef != powerHistoryTagChange.HasChangeDef || (HasChangeDef && !ChangeDef.Equals(powerHistoryTagChange.ChangeDef)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PowerHistoryTagChange Deserialize(Stream stream, PowerHistoryTagChange instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PowerHistoryTagChange DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryTagChange powerHistoryTagChange = new PowerHistoryTagChange();
			DeserializeLengthDelimited(stream, powerHistoryTagChange);
			return powerHistoryTagChange;
		}

		public static PowerHistoryTagChange DeserializeLengthDelimited(Stream stream, PowerHistoryTagChange instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PowerHistoryTagChange Deserialize(Stream stream, PowerHistoryTagChange instance, long limit)
		{
			instance.ChangeDef = false;
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
				case 32:
					instance.ChangeDef = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, PowerHistoryTagChange instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Entity);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Tag);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Value);
			if (instance.HasChangeDef)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.ChangeDef);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Entity);
			num += ProtocolParser.SizeOfUInt64((ulong)Tag);
			num += ProtocolParser.SizeOfUInt64((ulong)Value);
			if (HasChangeDef)
			{
				num++;
				num++;
			}
			return num + 3;
		}
	}
}
