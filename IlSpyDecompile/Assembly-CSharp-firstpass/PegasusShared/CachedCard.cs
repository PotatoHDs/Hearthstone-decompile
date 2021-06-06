using System.IO;

namespace PegasusShared
{
	public class CachedCard : IProtoBuf
	{
		public long CardId { get; set; }

		public int AssetCardId { get; set; }

		public int UnixTimestamp { get; set; }

		public bool IsSeen { get; set; }

		public int Premium { get; set; }

		public int InsertSource { get; set; }

		public long InsertData { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ CardId.GetHashCode() ^ AssetCardId.GetHashCode() ^ UnixTimestamp.GetHashCode() ^ IsSeen.GetHashCode() ^ Premium.GetHashCode() ^ InsertSource.GetHashCode() ^ InsertData.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			CachedCard cachedCard = obj as CachedCard;
			if (cachedCard == null)
			{
				return false;
			}
			if (!CardId.Equals(cachedCard.CardId))
			{
				return false;
			}
			if (!AssetCardId.Equals(cachedCard.AssetCardId))
			{
				return false;
			}
			if (!UnixTimestamp.Equals(cachedCard.UnixTimestamp))
			{
				return false;
			}
			if (!IsSeen.Equals(cachedCard.IsSeen))
			{
				return false;
			}
			if (!Premium.Equals(cachedCard.Premium))
			{
				return false;
			}
			if (!InsertSource.Equals(cachedCard.InsertSource))
			{
				return false;
			}
			if (!InsertData.Equals(cachedCard.InsertData))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CachedCard Deserialize(Stream stream, CachedCard instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CachedCard DeserializeLengthDelimited(Stream stream)
		{
			CachedCard cachedCard = new CachedCard();
			DeserializeLengthDelimited(stream, cachedCard);
			return cachedCard;
		}

		public static CachedCard DeserializeLengthDelimited(Stream stream, CachedCard instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CachedCard Deserialize(Stream stream, CachedCard instance, long limit)
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
					instance.CardId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.AssetCardId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.UnixTimestamp = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.IsSeen = ProtocolParser.ReadBool(stream);
					continue;
				case 40:
					instance.Premium = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.InsertSource = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.InsertData = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, CachedCard instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CardId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.AssetCardId);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.UnixTimestamp);
			stream.WriteByte(32);
			ProtocolParser.WriteBool(stream, instance.IsSeen);
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Premium);
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.InsertSource);
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.InsertData);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)CardId) + ProtocolParser.SizeOfUInt64((ulong)AssetCardId) + ProtocolParser.SizeOfUInt64((ulong)UnixTimestamp) + 1 + ProtocolParser.SizeOfUInt64((ulong)Premium) + ProtocolParser.SizeOfUInt64((ulong)InsertSource) + ProtocolParser.SizeOfUInt64((ulong)InsertData) + 7;
		}
	}
}
