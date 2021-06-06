using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class BoughtSoldCard : IProtoBuf
	{
		public enum PacketID
		{
			ID = 258
		}

		public enum Result
		{
			GENERIC_FAILURE = 1,
			SOLD,
			BOUGHT,
			SOULBOUND,
			WRONG_SELL_PRICE,
			WRONG_BUY_PRICE,
			NO_PERMISSION,
			EVENT_NOT_ACTIVE,
			COUNT_MISMATCH
		}

		public bool HasCount;

		private int _Count;

		public bool HasNerfed;

		private bool _Nerfed;

		public bool HasUnitSellPrice;

		private int _UnitSellPrice;

		public bool HasUnitBuyPrice;

		private int _UnitBuyPrice;

		public bool HasCurrentCollectionCount;

		private int _CurrentCollectionCount;

		public bool HasCollectionVersion;

		private long _CollectionVersion;

		public CardDef Def { get; set; }

		public int Amount { get; set; }

		public Result Result_ { get; set; }

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

		public bool Nerfed
		{
			get
			{
				return _Nerfed;
			}
			set
			{
				_Nerfed = value;
				HasNerfed = true;
			}
		}

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

		public long CollectionVersion
		{
			get
			{
				return _CollectionVersion;
			}
			set
			{
				_CollectionVersion = value;
				HasCollectionVersion = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Def.GetHashCode();
			hashCode ^= Amount.GetHashCode();
			hashCode ^= Result_.GetHashCode();
			if (HasCount)
			{
				hashCode ^= Count.GetHashCode();
			}
			if (HasNerfed)
			{
				hashCode ^= Nerfed.GetHashCode();
			}
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
			if (HasCollectionVersion)
			{
				hashCode ^= CollectionVersion.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			BoughtSoldCard boughtSoldCard = obj as BoughtSoldCard;
			if (boughtSoldCard == null)
			{
				return false;
			}
			if (!Def.Equals(boughtSoldCard.Def))
			{
				return false;
			}
			if (!Amount.Equals(boughtSoldCard.Amount))
			{
				return false;
			}
			if (!Result_.Equals(boughtSoldCard.Result_))
			{
				return false;
			}
			if (HasCount != boughtSoldCard.HasCount || (HasCount && !Count.Equals(boughtSoldCard.Count)))
			{
				return false;
			}
			if (HasNerfed != boughtSoldCard.HasNerfed || (HasNerfed && !Nerfed.Equals(boughtSoldCard.Nerfed)))
			{
				return false;
			}
			if (HasUnitSellPrice != boughtSoldCard.HasUnitSellPrice || (HasUnitSellPrice && !UnitSellPrice.Equals(boughtSoldCard.UnitSellPrice)))
			{
				return false;
			}
			if (HasUnitBuyPrice != boughtSoldCard.HasUnitBuyPrice || (HasUnitBuyPrice && !UnitBuyPrice.Equals(boughtSoldCard.UnitBuyPrice)))
			{
				return false;
			}
			if (HasCurrentCollectionCount != boughtSoldCard.HasCurrentCollectionCount || (HasCurrentCollectionCount && !CurrentCollectionCount.Equals(boughtSoldCard.CurrentCollectionCount)))
			{
				return false;
			}
			if (HasCollectionVersion != boughtSoldCard.HasCollectionVersion || (HasCollectionVersion && !CollectionVersion.Equals(boughtSoldCard.CollectionVersion)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BoughtSoldCard Deserialize(Stream stream, BoughtSoldCard instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BoughtSoldCard DeserializeLengthDelimited(Stream stream)
		{
			BoughtSoldCard boughtSoldCard = new BoughtSoldCard();
			DeserializeLengthDelimited(stream, boughtSoldCard);
			return boughtSoldCard;
		}

		public static BoughtSoldCard DeserializeLengthDelimited(Stream stream, BoughtSoldCard instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BoughtSoldCard Deserialize(Stream stream, BoughtSoldCard instance, long limit)
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
					instance.Amount = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Result_ = (Result)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.Count = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.Nerfed = ProtocolParser.ReadBool(stream);
					continue;
				case 48:
					instance.UnitSellPrice = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.UnitBuyPrice = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.CurrentCollectionCount = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 72:
					instance.CollectionVersion = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, BoughtSoldCard instance)
		{
			if (instance.Def == null)
			{
				throw new ArgumentNullException("Def", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Def.GetSerializedSize());
			CardDef.Serialize(stream, instance.Def);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Amount);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Result_);
			if (instance.HasCount)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Count);
			}
			if (instance.HasNerfed)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.Nerfed);
			}
			if (instance.HasUnitSellPrice)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.UnitSellPrice);
			}
			if (instance.HasUnitBuyPrice)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.UnitBuyPrice);
			}
			if (instance.HasCurrentCollectionCount)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrentCollectionCount);
			}
			if (instance.HasCollectionVersion)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CollectionVersion);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = Def.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			num += ProtocolParser.SizeOfUInt64((ulong)Amount);
			num += ProtocolParser.SizeOfUInt64((ulong)Result_);
			if (HasCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Count);
			}
			if (HasNerfed)
			{
				num++;
				num++;
			}
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
			if (HasCollectionVersion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CollectionVersion);
			}
			return num + 3;
		}
	}
}
