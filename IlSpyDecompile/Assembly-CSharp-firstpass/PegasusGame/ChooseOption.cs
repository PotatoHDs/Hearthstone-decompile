using System.IO;

namespace PegasusGame
{
	public class ChooseOption : IProtoBuf
	{
		public enum PacketID
		{
			ID = 2
		}

		public bool HasSubOption;

		private int _SubOption;

		public bool HasPosition;

		private int _Position;

		public int Id { get; set; }

		public int Index { get; set; }

		public int Target { get; set; }

		public int SubOption
		{
			get
			{
				return _SubOption;
			}
			set
			{
				_SubOption = value;
				HasSubOption = true;
			}
		}

		public int Position
		{
			get
			{
				return _Position;
			}
			set
			{
				_Position = value;
				HasPosition = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			hashCode ^= Index.GetHashCode();
			hashCode ^= Target.GetHashCode();
			if (HasSubOption)
			{
				hashCode ^= SubOption.GetHashCode();
			}
			if (HasPosition)
			{
				hashCode ^= Position.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ChooseOption chooseOption = obj as ChooseOption;
			if (chooseOption == null)
			{
				return false;
			}
			if (!Id.Equals(chooseOption.Id))
			{
				return false;
			}
			if (!Index.Equals(chooseOption.Index))
			{
				return false;
			}
			if (!Target.Equals(chooseOption.Target))
			{
				return false;
			}
			if (HasSubOption != chooseOption.HasSubOption || (HasSubOption && !SubOption.Equals(chooseOption.SubOption)))
			{
				return false;
			}
			if (HasPosition != chooseOption.HasPosition || (HasPosition && !Position.Equals(chooseOption.Position)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChooseOption Deserialize(Stream stream, ChooseOption instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChooseOption DeserializeLengthDelimited(Stream stream)
		{
			ChooseOption chooseOption = new ChooseOption();
			DeserializeLengthDelimited(stream, chooseOption);
			return chooseOption;
		}

		public static ChooseOption DeserializeLengthDelimited(Stream stream, ChooseOption instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChooseOption Deserialize(Stream stream, ChooseOption instance, long limit)
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
				case 16:
					instance.Index = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Target = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.SubOption = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.Position = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ChooseOption instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Index);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Target);
			if (instance.HasSubOption)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SubOption);
			}
			if (instance.HasPosition)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Position);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Id);
			num += ProtocolParser.SizeOfUInt64((ulong)Index);
			num += ProtocolParser.SizeOfUInt64((ulong)Target);
			if (HasSubOption)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SubOption);
			}
			if (HasPosition)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Position);
			}
			return num + 3;
		}
	}
}
