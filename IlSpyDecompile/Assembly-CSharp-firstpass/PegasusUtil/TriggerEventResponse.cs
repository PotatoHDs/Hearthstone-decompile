using System.IO;

namespace PegasusUtil
{
	public class TriggerEventResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 299
		}

		public int EventId { get; set; }

		public bool Success { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ EventId.GetHashCode() ^ Success.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			TriggerEventResponse triggerEventResponse = obj as TriggerEventResponse;
			if (triggerEventResponse == null)
			{
				return false;
			}
			if (!EventId.Equals(triggerEventResponse.EventId))
			{
				return false;
			}
			if (!Success.Equals(triggerEventResponse.Success))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static TriggerEventResponse Deserialize(Stream stream, TriggerEventResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static TriggerEventResponse DeserializeLengthDelimited(Stream stream)
		{
			TriggerEventResponse triggerEventResponse = new TriggerEventResponse();
			DeserializeLengthDelimited(stream, triggerEventResponse);
			return triggerEventResponse;
		}

		public static TriggerEventResponse DeserializeLengthDelimited(Stream stream, TriggerEventResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static TriggerEventResponse Deserialize(Stream stream, TriggerEventResponse instance, long limit)
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
					instance.EventId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
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

		public static void Serialize(Stream stream, TriggerEventResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.EventId);
			stream.WriteByte(16);
			ProtocolParser.WriteBool(stream, instance.Success);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)EventId) + 1 + 2;
		}
	}
}
