using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class BuySellCard : IProtoBuf
	{
		public enum PacketID
		{
			ID = 257,
			System = 0
		}

		public bool HasCount;

		private int _Count;

		public bool HasUnitSellPrice;

		private int _UnitSellPrice;

		public bool HasUnitBuyPrice;

		private int _UnitBuyPrice;

		public bool HasCurrentCollectionCount;

		private int _CurrentCollectionCount;

		public CardDef Def { get; set; }

		public int Count
		{
			get
			{
				return _Count;
			}
			set
			{
				_Count = value;
				HasCount = true;
			}
		}

		public bool Buying { get; set; }

		public int UnitSellPrice
		{
			get
			{
				return _UnitSellPrice;
			}
			set
			{
				_UnitSellPrice = value;
				HasUnitSellPrice = true;
			}
		}

		public int UnitBuyPrice
		{
			get
			{
				return _UnitBuyPrice;
			}
			set
			{
				_UnitBuyPrice = value;
				HasUnitBuyPrice = true;
			}
		}

		public int CurrentCollectionCount
		{
			get
			{
				return _CurrentCollectionCount;
			}
			set
			{
				_CurrentCollectionCount = value;
				HasCurrentCollectionCount = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Def.GetHashCode();
			if (HasCount)
			{
				hashCode ^= Count.GetHashCode();
			}
			hashCode ^= Buying.GetHashCode();
			if (HasUnitSellPrice)
			{
				hashCode ^= UnitSellPrice.GetHashCode();
			}
			if (HasUnitBuyPrice)
			{
				hashCode ^= UnitBuyPrice.GetHashCode();
			}
			if (HasCurrentCollectionCount)
			{
				hashCode ^= CurrentCollectionCount.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			BuySellCard buySellCard = obj as BuySellCard;
			if (buySellCard == null)
			{
				return false;
			}
			if (!Def.Equals(buySellCard.Def))
			{
				return false;
			}
			if (HasCount != buySellCard.HasCount || (HasCount && !Count.Equals(buySellCard.Count)))
			{
				return false;
			}
			if (!Buying.Equals(buySellCard.Buying))
			{
				return false;
			}
			if (HasUnitSellPrice != buySellCard.HasUnitSellPrice || (HasUnitSellPrice && !UnitSellPrice.Equals(buySellCard.UnitSellPrice)))
			{
				return false;
			}
			if (HasUnitBuyPrice != buySellCard.HasUnitBuyPrice || (HasUnitBuyPrice && !UnitBuyPrice.Equals(buySellCard.UnitBuyPrice)))
			{
				return false;
			}
			if (HasCurrentCollectionCount != buySellCard.HasCurrentCollectionCount || (HasCurrentCollectionCount && !CurrentCollectionCount.Equals(buySellCard.CurrentCollectionCount)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BuySellCard Deserialize(Stream stream, BuySellCard instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BuySellCard DeserializeLengthDelimited(Stream stream)
		{
			BuySellCard buySellCard = new BuySellCard();
			DeserializeLengthDelimited(stream, buySellCard);
			return buySellCard;
		}

		public static BuySellCard DeserializeLengthDelimited(Stream stream, BuySellCard instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BuySellCard Deserialize(Stream stream, BuySellCard instance, long limit)
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
				case 10:
					if (instance.Def == null)
					{
						instance.Def = CardDef.DeserializeLengthDelimited(stream);
					}
					else
					{
						CardDef.DeserializeLengthDelimited(stream, instance.Def);
					}
					continue;
				case 16:
					instance.Count = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Buying = ProtocolParser.ReadBool(stream);
					continue;
				case 32:
					instance.UnitSellPrice = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.UnitBuyPrice = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.CurrentCollectionCount = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, BuySellCard instance)
		{
			if (instance.Def == null)
			{
				throw new ArgumentNullException("Def", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Def.GetSerializedSize());
			CardDef.Serialize(stream, instance.Def);
			if (instance.HasCount)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Count);
			}
			stream.WriteByte(24);
			ProtocolParser.WriteBool(stream, instance.Buying);
			if (instance.HasUnitSellPrice)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.UnitSellPrice);
			}
			if (instance.HasUnitBuyPrice)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.UnitBuyPrice);
			}
			if (instance.HasCurrentCollectionCount)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrentCollectionCount);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = Def.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Count);
			}
			num++;
			if (HasUnitSellPrice)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)UnitSellPrice);
			}
			if (HasUnitBuyPrice)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)UnitBuyPrice);
			}
			if (HasCurrentCollectionCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CurrentCollectionCount);
			}
			return num + 2;
		}
	}
}
