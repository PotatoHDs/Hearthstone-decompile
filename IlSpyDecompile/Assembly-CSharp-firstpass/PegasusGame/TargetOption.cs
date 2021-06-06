using System.IO;

namespace PegasusGame
{
	public class TargetOption : IProtoBuf
	{
		public bool HasPlayErrorParam;

		private int _PlayErrorParam;

		public int Id { get; set; }

		public int PlayError { get; set; }

		public int PlayErrorParam
		{
			get
			{
				return _PlayErrorParam;
			}
			set
			{
				_PlayErrorParam = value;
				HasPlayErrorParam = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			hashCode ^= PlayError.GetHashCode();
			if (HasPlayErrorParam)
			{
				hashCode ^= PlayErrorParam.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			TargetOption targetOption = obj as TargetOption;
			if (targetOption == null)
			{
				return false;
			}
			if (!Id.Equals(targetOption.Id))
			{
				return false;
			}
			if (!PlayError.Equals(targetOption.PlayError))
			{
				return false;
			}
			if (HasPlayErrorParam != targetOption.HasPlayErrorParam || (HasPlayErrorParam && !PlayErrorParam.Equals(targetOption.PlayErrorParam)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static TargetOption Deserialize(Stream stream, TargetOption instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static TargetOption DeserializeLengthDelimited(Stream stream)
		{
			TargetOption targetOption = new TargetOption();
			DeserializeLengthDelimited(stream, targetOption);
			return targetOption;
		}

		public static TargetOption DeserializeLengthDelimited(Stream stream, TargetOption instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static TargetOption Deserialize(Stream stream, TargetOption instance, long limit)
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
					instance.PlayError = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.PlayErrorParam = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, TargetOption instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayError);
			if (instance.HasPlayErrorParam)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayErrorParam);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Id);
			num += ProtocolParser.SizeOfUInt64((ulong)PlayError);
			if (HasPlayErrorParam)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PlayErrorParam);
			}
			return num + 2;
		}
	}
}
