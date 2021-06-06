using System;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	public class CardValue : IProtoBuf
	{
		public bool HasDeprecatedNerfed;

		private bool _DeprecatedNerfed;

		public bool HasBuyValueOverride;

		private int _BuyValueOverride;

		public bool HasSellValueOverride;

		private int _SellValueOverride;

		public bool HasOverrideEventName;

		private string _OverrideEventName;

		public CardDef Card { get; set; }

		public int Buy { get; set; }

		public int Sell { get; set; }

		public bool Nerfed { get; set; }

		public bool DeprecatedNerfed
		{
			get
			{
				return _DeprecatedNerfed;
			}
			set
			{
				_DeprecatedNerfed = value;
				HasDeprecatedNerfed = true;
			}
		}

		public int BuyValueOverride
		{
			get
			{
				return _BuyValueOverride;
			}
			set
			{
				_BuyValueOverride = value;
				HasBuyValueOverride = true;
			}
		}

		public int SellValueOverride
		{
			get
			{
				return _SellValueOverride;
			}
			set
			{
				_SellValueOverride = value;
				HasSellValueOverride = true;
			}
		}

		public string OverrideEventName
		{
			get
			{
				return _OverrideEventName;
			}
			set
			{
				_OverrideEventName = value;
				HasOverrideEventName = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Card.GetHashCode();
			hashCode ^= Buy.GetHashCode();
			hashCode ^= Sell.GetHashCode();
			hashCode ^= Nerfed.GetHashCode();
			if (HasDeprecatedNerfed)
			{
				hashCode ^= DeprecatedNerfed.GetHashCode();
			}
			if (HasBuyValueOverride)
			{
				hashCode ^= BuyValueOverride.GetHashCode();
			}
			if (HasSellValueOverride)
			{
				hashCode ^= SellValueOverride.GetHashCode();
			}
			if (HasOverrideEventName)
			{
				hashCode ^= OverrideEventName.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			CardValue cardValue = obj as CardValue;
			if (cardValue == null)
			{
				return false;
			}
			if (!Card.Equals(cardValue.Card))
			{
				return false;
			}
			if (!Buy.Equals(cardValue.Buy))
			{
				return false;
			}
			if (!Sell.Equals(cardValue.Sell))
			{
				return false;
			}
			if (!Nerfed.Equals(cardValue.Nerfed))
			{
				return false;
			}
			if (HasDeprecatedNerfed != cardValue.HasDeprecatedNerfed || (HasDeprecatedNerfed && !DeprecatedNerfed.Equals(cardValue.DeprecatedNerfed)))
			{
				return false;
			}
			if (HasBuyValueOverride != cardValue.HasBuyValueOverride || (HasBuyValueOverride && !BuyValueOverride.Equals(cardValue.BuyValueOverride)))
			{
				return false;
			}
			if (HasSellValueOverride != cardValue.HasSellValueOverride || (HasSellValueOverride && !SellValueOverride.Equals(cardValue.SellValueOverride)))
			{
				return false;
			}
			if (HasOverrideEventName != cardValue.HasOverrideEventName || (HasOverrideEventName && !OverrideEventName.Equals(cardValue.OverrideEventName)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CardValue Deserialize(Stream stream, CardValue instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CardValue DeserializeLengthDelimited(Stream stream)
		{
			CardValue cardValue = new CardValue();
			DeserializeLengthDelimited(stream, cardValue);
			return cardValue;
		}

		public static CardValue DeserializeLengthDelimited(Stream stream, CardValue instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CardValue Deserialize(Stream stream, CardValue instance, long limit)
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
					if (instance.Card == null)
					{
						instance.Card = CardDef.DeserializeLengthDelimited(stream);
					}
					else
					{
						CardDef.DeserializeLengthDelimited(stream, instance.Card);
					}
					continue;
				case 16:
					instance.Buy = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Sell = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.Nerfed = ProtocolParser.ReadBool(stream);
					continue;
				case 40:
					instance.DeprecatedNerfed = ProtocolParser.ReadBool(stream);
					continue;
				case 48:
					instance.BuyValueOverride = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.SellValueOverride = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 66:
					instance.OverrideEventName = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, CardValue instance)
		{
			if (instance.Card == null)
			{
				throw new ArgumentNullException("Card", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Card.GetSerializedSize());
			CardDef.Serialize(stream, instance.Card);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Buy);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Sell);
			stream.WriteByte(32);
			ProtocolParser.WriteBool(stream, instance.Nerfed);
			if (instance.HasDeprecatedNerfed)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.DeprecatedNerfed);
			}
			if (instance.HasBuyValueOverride)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BuyValueOverride);
			}
			if (instance.HasSellValueOverride)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SellValueOverride);
			}
			if (instance.HasOverrideEventName)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.OverrideEventName));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = Card.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			num += ProtocolParser.SizeOfUInt64((ulong)Buy);
			num += ProtocolParser.SizeOfUInt64((ulong)Sell);
			num++;
			if (HasDeprecatedNerfed)
			{
				num++;
				num++;
			}
			if (HasBuyValueOverride)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BuyValueOverride);
			}
			if (HasSellValueOverride)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SellValueOverride);
			}
			if (HasOverrideEventName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(OverrideEventName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 4;
		}
	}
}
