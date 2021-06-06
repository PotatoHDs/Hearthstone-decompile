using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class TavernBrawlRequestSessionRetireResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 348
		}

		public bool HasPlayerRecord;

		private TavernBrawlPlayerRecord _PlayerRecord;

		public bool HasChest;

		private RewardChest _Chest;

		public ErrorCode ErrorCode { get; set; }

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

		public RewardChest Chest
		{
			get
			{
				return _Chest;
			}
			set
			{
				_Chest = value;
				HasChest = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ErrorCode.GetHashCode();
			if (HasPlayerRecord)
			{
				hashCode ^= PlayerRecord.GetHashCode();
			}
			if (HasChest)
			{
				hashCode ^= Chest.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			TavernBrawlRequestSessionRetireResponse tavernBrawlRequestSessionRetireResponse = obj as TavernBrawlRequestSessionRetireResponse;
			if (tavernBrawlRequestSessionRetireResponse == null)
			{
				return false;
			}
			if (!ErrorCode.Equals(tavernBrawlRequestSessionRetireResponse.ErrorCode))
			{
				return false;
			}
			if (HasPlayerRecord != tavernBrawlRequestSessionRetireResponse.HasPlayerRecord || (HasPlayerRecord && !PlayerRecord.Equals(tavernBrawlRequestSessionRetireResponse.PlayerRecord)))
			{
				return false;
			}
			if (HasChest != tavernBrawlRequestSessionRetireResponse.HasChest || (HasChest && !Chest.Equals(tavernBrawlRequestSessionRetireResponse.Chest)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static TavernBrawlRequestSessionRetireResponse Deserialize(Stream stream, TavernBrawlRequestSessionRetireResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static TavernBrawlRequestSessionRetireResponse DeserializeLengthDelimited(Stream stream)
		{
			TavernBrawlRequestSessionRetireResponse tavernBrawlRequestSessionRetireResponse = new TavernBrawlRequestSessionRetireResponse();
			DeserializeLengthDelimited(stream, tavernBrawlRequestSessionRetireResponse);
			return tavernBrawlRequestSessionRetireResponse;
		}

		public static TavernBrawlRequestSessionRetireResponse DeserializeLengthDelimited(Stream stream, TavernBrawlRequestSessionRetireResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static TavernBrawlRequestSessionRetireResponse Deserialize(Stream stream, TavernBrawlRequestSessionRetireResponse instance, long limit)
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
				case 26:
					if (instance.Chest == null)
					{
						instance.Chest = RewardChest.DeserializeLengthDelimited(stream);
					}
					else
					{
						RewardChest.DeserializeLengthDelimited(stream, instance.Chest);
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

		public static void Serialize(Stream stream, TavernBrawlRequestSessionRetireResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ErrorCode);
			if (instance.HasPlayerRecord)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.PlayerRecord.GetSerializedSize());
				TavernBrawlPlayerRecord.Serialize(stream, instance.PlayerRecord);
			}
			if (instance.HasChest)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Chest.GetSerializedSize());
				RewardChest.Serialize(stream, instance.Chest);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)ErrorCode);
			if (HasPlayerRecord)
			{
				num++;
				uint serializedSize = PlayerRecord.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasChest)
			{
				num++;
				uint serializedSize2 = Chest.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1;
		}
	}
}
