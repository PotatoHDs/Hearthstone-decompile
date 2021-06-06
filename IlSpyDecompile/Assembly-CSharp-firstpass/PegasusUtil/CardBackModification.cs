using System.IO;

namespace PegasusUtil
{
	public class CardBackModification : IProtoBuf
	{
		public bool HasAutoSetAsFavorite;

		private bool _AutoSetAsFavorite;

		public int AssetCardBackId { get; set; }

		public bool AutoSetAsFavorite
		{
			get
			{
				return _AutoSetAsFavorite;
			}
			set
			{
				_AutoSetAsFavorite = value;
				HasAutoSetAsFavorite = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= AssetCardBackId.GetHashCode();
			if (HasAutoSetAsFavorite)
			{
				hashCode ^= AutoSetAsFavorite.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			CardBackModification cardBackModification = obj as CardBackModification;
			if (cardBackModification == null)
			{
				return false;
			}
			if (!AssetCardBackId.Equals(cardBackModification.AssetCardBackId))
			{
				return false;
			}
			if (HasAutoSetAsFavorite != cardBackModification.HasAutoSetAsFavorite || (HasAutoSetAsFavorite && !AutoSetAsFavorite.Equals(cardBackModification.AutoSetAsFavorite)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CardBackModification Deserialize(Stream stream, CardBackModification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CardBackModification DeserializeLengthDelimited(Stream stream)
		{
			CardBackModification cardBackModification = new CardBackModification();
			DeserializeLengthDelimited(stream, cardBackModification);
			return cardBackModification;
		}

		public static CardBackModification DeserializeLengthDelimited(Stream stream, CardBackModification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CardBackModification Deserialize(Stream stream, CardBackModification instance, long limit)
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
					instance.AssetCardBackId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.AutoSetAsFavorite = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, CardBackModification instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.AssetCardBackId);
			if (instance.HasAutoSetAsFavorite)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.AutoSetAsFavorite);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)AssetCardBackId);
			if (HasAutoSetAsFavorite)
			{
				num++;
				num++;
			}
			return num + 1;
		}
	}
}
