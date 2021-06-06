using System.IO;

namespace PegasusShared
{
	public class CardDef : IProtoBuf
	{
		public bool HasPremium;

		private int _Premium;

		public int Asset { get; set; }

		public int Premium
		{
			get
			{
				return _Premium;
			}
			set
			{
				_Premium = value;
				HasPremium = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Asset.GetHashCode();
			if (HasPremium)
			{
				hashCode ^= Premium.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			CardDef cardDef = obj as CardDef;
			if (cardDef == null)
			{
				return false;
			}
			if (!Asset.Equals(cardDef.Asset))
			{
				return false;
			}
			if (HasPremium != cardDef.HasPremium || (HasPremium && !Premium.Equals(cardDef.Premium)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CardDef Deserialize(Stream stream, CardDef instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CardDef DeserializeLengthDelimited(Stream stream)
		{
			CardDef cardDef = new CardDef();
			DeserializeLengthDelimited(stream, cardDef);
			return cardDef;
		}

		public static CardDef DeserializeLengthDelimited(Stream stream, CardDef instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CardDef Deserialize(Stream stream, CardDef instance, long limit)
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
					instance.Asset = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Premium = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, CardDef instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Asset);
			if (instance.HasPremium)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Premium);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Asset);
			if (HasPremium)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Premium);
			}
			return num + 1;
		}
	}
}
