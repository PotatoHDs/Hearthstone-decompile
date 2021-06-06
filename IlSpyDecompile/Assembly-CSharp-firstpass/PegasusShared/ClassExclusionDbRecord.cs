using System.IO;

namespace PegasusShared
{
	public class ClassExclusionDbRecord : IProtoBuf
	{
		public int ScenarioId { get; set; }

		public int ClassId { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ ScenarioId.GetHashCode() ^ ClassId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ClassExclusionDbRecord classExclusionDbRecord = obj as ClassExclusionDbRecord;
			if (classExclusionDbRecord == null)
			{
				return false;
			}
			if (!ScenarioId.Equals(classExclusionDbRecord.ScenarioId))
			{
				return false;
			}
			if (!ClassId.Equals(classExclusionDbRecord.ClassId))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ClassExclusionDbRecord Deserialize(Stream stream, ClassExclusionDbRecord instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ClassExclusionDbRecord DeserializeLengthDelimited(Stream stream)
		{
			ClassExclusionDbRecord classExclusionDbRecord = new ClassExclusionDbRecord();
			DeserializeLengthDelimited(stream, classExclusionDbRecord);
			return classExclusionDbRecord;
		}

		public static ClassExclusionDbRecord DeserializeLengthDelimited(Stream stream, ClassExclusionDbRecord instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ClassExclusionDbRecord Deserialize(Stream stream, ClassExclusionDbRecord instance, long limit)
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
					instance.ScenarioId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.ClassId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ClassExclusionDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ScenarioId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ClassId);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)ScenarioId) + ProtocolParser.SizeOfUInt64((ulong)ClassId) + 2;
		}
	}
}
