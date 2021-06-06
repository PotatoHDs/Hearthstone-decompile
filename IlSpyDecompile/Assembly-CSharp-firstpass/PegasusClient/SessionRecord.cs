using System.IO;

namespace PegasusClient
{
	public class SessionRecord : IProtoBuf
	{
		public uint Wins { get; set; }

		public uint Losses { get; set; }

		public bool RunFinished { get; set; }

		public SessionRecordType SessionRecordType { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Wins.GetHashCode() ^ Losses.GetHashCode() ^ RunFinished.GetHashCode() ^ SessionRecordType.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SessionRecord sessionRecord = obj as SessionRecord;
			if (sessionRecord == null)
			{
				return false;
			}
			if (!Wins.Equals(sessionRecord.Wins))
			{
				return false;
			}
			if (!Losses.Equals(sessionRecord.Losses))
			{
				return false;
			}
			if (!RunFinished.Equals(sessionRecord.RunFinished))
			{
				return false;
			}
			if (!SessionRecordType.Equals(sessionRecord.SessionRecordType))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SessionRecord Deserialize(Stream stream, SessionRecord instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SessionRecord DeserializeLengthDelimited(Stream stream)
		{
			SessionRecord sessionRecord = new SessionRecord();
			DeserializeLengthDelimited(stream, sessionRecord);
			return sessionRecord;
		}

		public static SessionRecord DeserializeLengthDelimited(Stream stream, SessionRecord instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SessionRecord Deserialize(Stream stream, SessionRecord instance, long limit)
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
					instance.Wins = ProtocolParser.ReadUInt32(stream);
					continue;
				case 16:
					instance.Losses = ProtocolParser.ReadUInt32(stream);
					continue;
				case 24:
					instance.RunFinished = ProtocolParser.ReadBool(stream);
					continue;
				case 32:
					instance.SessionRecordType = (SessionRecordType)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, SessionRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Wins);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.Losses);
			stream.WriteByte(24);
			ProtocolParser.WriteBool(stream, instance.RunFinished);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.SessionRecordType);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt32(Wins) + ProtocolParser.SizeOfUInt32(Losses) + 1 + ProtocolParser.SizeOfUInt64((ulong)SessionRecordType) + 4;
		}
	}
}
