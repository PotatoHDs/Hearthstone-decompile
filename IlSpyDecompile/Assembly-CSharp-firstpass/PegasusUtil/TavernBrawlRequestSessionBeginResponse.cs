using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class TavernBrawlRequestSessionBeginResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 347
		}

		public bool HasErrorCode;

		private ErrorCode _ErrorCode;

		public bool HasPlayerRecord;

		private TavernBrawlPlayerRecord _PlayerRecord;

		public ErrorCode ErrorCode
		{
			get
			{
				return _ErrorCode;
			}
			set
			{
				_ErrorCode = value;
				HasErrorCode = true;
			}
		}

		public TavernBrawlPlayerRecord PlayerRecord
		{
			get
			{
				return _PlayerRecord;
			}
			set
			{
				_PlayerRecord = value;
				HasPlayerRecord = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasErrorCode)
			{
				num ^= ErrorCode.GetHashCode();
			}
			if (HasPlayerRecord)
			{
				num ^= PlayerRecord.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			TavernBrawlRequestSessionBeginResponse tavernBrawlRequestSessionBeginResponse = obj as TavernBrawlRequestSessionBeginResponse;
			if (tavernBrawlRequestSessionBeginResponse == null)
			{
				return false;
			}
			if (HasErrorCode != tavernBrawlRequestSessionBeginResponse.HasErrorCode || (HasErrorCode && !ErrorCode.Equals(tavernBrawlRequestSessionBeginResponse.ErrorCode)))
			{
				return false;
			}
			if (HasPlayerRecord != tavernBrawlRequestSessionBeginResponse.HasPlayerRecord || (HasPlayerRecord && !PlayerRecord.Equals(tavernBrawlRequestSessionBeginResponse.PlayerRecord)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static TavernBrawlRequestSessionBeginResponse Deserialize(Stream stream, TavernBrawlRequestSessionBeginResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static TavernBrawlRequestSessionBeginResponse DeserializeLengthDelimited(Stream stream)
		{
			TavernBrawlRequestSessionBeginResponse tavernBrawlRequestSessionBeginResponse = new TavernBrawlRequestSessionBeginResponse();
			DeserializeLengthDelimited(stream, tavernBrawlRequestSessionBeginResponse);
			return tavernBrawlRequestSessionBeginResponse;
		}

		public static TavernBrawlRequestSessionBeginResponse DeserializeLengthDelimited(Stream stream, TavernBrawlRequestSessionBeginResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static TavernBrawlRequestSessionBeginResponse Deserialize(Stream stream, TavernBrawlRequestSessionBeginResponse instance, long limit)
		{
			instance.ErrorCode = ErrorCode.ERROR_OK;
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
					instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					if (instance.PlayerRecord == null)
					{
						instance.PlayerRecord = TavernBrawlPlayerRecord.DeserializeLengthDelimited(stream);
					}
					else
					{
						TavernBrawlPlayerRecord.DeserializeLengthDelimited(stream, instance.PlayerRecord);
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

		public static void Serialize(Stream stream, TavernBrawlRequestSessionBeginResponse instance)
		{
			if (instance.HasErrorCode)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ErrorCode);
			}
			if (instance.HasPlayerRecord)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.PlayerRecord.GetSerializedSize());
				TavernBrawlPlayerRecord.Serialize(stream, instance.PlayerRecord);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasErrorCode)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ErrorCode);
			}
			if (HasPlayerRecord)
			{
				num++;
				uint serializedSize = PlayerRecord.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
