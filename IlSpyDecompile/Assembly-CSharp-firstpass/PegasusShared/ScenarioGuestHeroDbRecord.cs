using System.IO;

namespace PegasusShared
{
	public class ScenarioGuestHeroDbRecord : IProtoBuf
	{
		public int ScenarioId { get; set; }

		public int GuestHeroId { get; set; }

		public int SortOrder { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ ScenarioId.GetHashCode() ^ GuestHeroId.GetHashCode() ^ SortOrder.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ScenarioGuestHeroDbRecord scenarioGuestHeroDbRecord = obj as ScenarioGuestHeroDbRecord;
			if (scenarioGuestHeroDbRecord == null)
			{
				return false;
			}
			if (!ScenarioId.Equals(scenarioGuestHeroDbRecord.ScenarioId))
			{
				return false;
			}
			if (!GuestHeroId.Equals(scenarioGuestHeroDbRecord.GuestHeroId))
			{
				return false;
			}
			if (!SortOrder.Equals(scenarioGuestHeroDbRecord.SortOrder))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ScenarioGuestHeroDbRecord Deserialize(Stream stream, ScenarioGuestHeroDbRecord instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ScenarioGuestHeroDbRecord DeserializeLengthDelimited(Stream stream)
		{
			ScenarioGuestHeroDbRecord scenarioGuestHeroDbRecord = new ScenarioGuestHeroDbRecord();
			DeserializeLengthDelimited(stream, scenarioGuestHeroDbRecord);
			return scenarioGuestHeroDbRecord;
		}

		public static ScenarioGuestHeroDbRecord DeserializeLengthDelimited(Stream stream, ScenarioGuestHeroDbRecord instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ScenarioGuestHeroDbRecord Deserialize(Stream stream, ScenarioGuestHeroDbRecord instance, long limit)
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
					instance.GuestHeroId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.SortOrder = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ScenarioGuestHeroDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ScenarioId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.GuestHeroId);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.SortOrder);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)ScenarioId) + ProtocolParser.SizeOfUInt64((ulong)GuestHeroId) + ProtocolParser.SizeOfUInt64((ulong)SortOrder) + 3;
		}
	}
}
