using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class TavernBrawlPlayerRecordResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 317
		}

		public bool HasRecord;

		private TavernBrawlPlayerRecord _Record;

		public TavernBrawlPlayerRecord Record
		{
			get
			{
				return _Record;
			}
			set
			{
				_Record = value;
				HasRecord = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRecord)
			{
				num ^= Record.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			TavernBrawlPlayerRecordResponse tavernBrawlPlayerRecordResponse = obj as TavernBrawlPlayerRecordResponse;
			if (tavernBrawlPlayerRecordResponse == null)
			{
				return false;
			}
			if (HasRecord != tavernBrawlPlayerRecordResponse.HasRecord || (HasRecord && !Record.Equals(tavernBrawlPlayerRecordResponse.Record)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static TavernBrawlPlayerRecordResponse Deserialize(Stream stream, TavernBrawlPlayerRecordResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static TavernBrawlPlayerRecordResponse DeserializeLengthDelimited(Stream stream)
		{
			TavernBrawlPlayerRecordResponse tavernBrawlPlayerRecordResponse = new TavernBrawlPlayerRecordResponse();
			DeserializeLengthDelimited(stream, tavernBrawlPlayerRecordResponse);
			return tavernBrawlPlayerRecordResponse;
		}

		public static TavernBrawlPlayerRecordResponse DeserializeLengthDelimited(Stream stream, TavernBrawlPlayerRecordResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static TavernBrawlPlayerRecordResponse Deserialize(Stream stream, TavernBrawlPlayerRecordResponse instance, long limit)
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
				case 50:
					if (instance.Record == null)
					{
						instance.Record = TavernBrawlPlayerRecord.DeserializeLengthDelimited(stream);
					}
					else
					{
						TavernBrawlPlayerRecord.DeserializeLengthDelimited(stream, instance.Record);
					}
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

		public static void Serialize(Stream stream, TavernBrawlPlayerRecordResponse instance)
		{
			if (instance.HasRecord)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.Record.GetSerializedSize());
				TavernBrawlPlayerRecord.Serialize(stream, instance.Record);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRecord)
			{
				num++;
				uint serializedSize = Record.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
