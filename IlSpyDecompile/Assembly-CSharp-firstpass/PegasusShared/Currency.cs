using System;
using System.IO;
using System.Text;

namespace PegasusShared
{
	public class Currency : IProtoBuf
	{
		public enum Tax
		{
			TAX_INCLUDED,
			TAX_ADDED,
			NO_TAX
		}

		public bool HasSubRegionId;

		private int _SubRegionId;

		public bool HasSymbol;

		private string _Symbol;

		public bool HasRoundingExponent;

		private int _RoundingExponent;

		public bool HasTaxText;

		private Tax _TaxText;

		public bool HasChangedVersion;

		private int _ChangedVersion;

		public string Code { get; set; }

		public int CurrencyId { get; set; }

		public int SubRegionId
		{
			get
			{
				return _SubRegionId;
			}
			set
			{
				_SubRegionId = value;
				HasSubRegionId = true;
			}
		}

		public string Symbol
		{
			get
			{
				return _Symbol;
			}
			set
			{
				_Symbol = value;
				HasSymbol = value != null;
			}
		}

		public int RoundingExponent
		{
			get
			{
				return _RoundingExponent;
			}
			set
			{
				_RoundingExponent = value;
				HasRoundingExponent = true;
			}
		}

		public Tax TaxText
		{
			get
			{
				return _TaxText;
			}
			set
			{
				_TaxText = value;
				HasTaxText = true;
			}
		}

		public int ChangedVersion
		{
			get
			{
				return _ChangedVersion;
			}
			set
			{
				_ChangedVersion = value;
				HasChangedVersion = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Code.GetHashCode();
			hashCode ^= CurrencyId.GetHashCode();
			if (HasSubRegionId)
			{
				hashCode ^= SubRegionId.GetHashCode();
			}
			if (HasSymbol)
			{
				hashCode ^= Symbol.GetHashCode();
			}
			if (HasRoundingExponent)
			{
				hashCode ^= RoundingExponent.GetHashCode();
			}
			if (HasTaxText)
			{
				hashCode ^= TaxText.GetHashCode();
			}
			if (HasChangedVersion)
			{
				hashCode ^= ChangedVersion.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			Currency currency = obj as Currency;
			if (currency == null)
			{
				return false;
			}
			if (!Code.Equals(currency.Code))
			{
				return false;
			}
			if (!CurrencyId.Equals(currency.CurrencyId))
			{
				return false;
			}
			if (HasSubRegionId != currency.HasSubRegionId || (HasSubRegionId && !SubRegionId.Equals(currency.SubRegionId)))
			{
				return false;
			}
			if (HasSymbol != currency.HasSymbol || (HasSymbol && !Symbol.Equals(currency.Symbol)))
			{
				return false;
			}
			if (HasRoundingExponent != currency.HasRoundingExponent || (HasRoundingExponent && !RoundingExponent.Equals(currency.RoundingExponent)))
			{
				return false;
			}
			if (HasTaxText != currency.HasTaxText || (HasTaxText && !TaxText.Equals(currency.TaxText)))
			{
				return false;
			}
			if (HasChangedVersion != currency.HasChangedVersion || (HasChangedVersion && !ChangedVersion.Equals(currency.ChangedVersion)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Currency Deserialize(Stream stream, Currency instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Currency DeserializeLengthDelimited(Stream stream)
		{
			Currency currency = new Currency();
			DeserializeLengthDelimited(stream, currency);
			return currency;
		}

		public static Currency DeserializeLengthDelimited(Stream stream, Currency instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Currency Deserialize(Stream stream, Currency instance, long limit)
		{
			instance.SubRegionId = 1;
			instance.Symbol = "$";
			instance.RoundingExponent = 2;
			instance.TaxText = Tax.TAX_INCLUDED;
			instance.ChangedVersion = 0;
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
					instance.Code = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.CurrencyId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.SubRegionId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.Symbol = ProtocolParser.ReadString(stream);
					continue;
				case 40:
					instance.RoundingExponent = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.TaxText = (Tax)ProtocolParser.ReadUInt64(stream);
					continue;
				case 72:
					instance.ChangedVersion = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, Currency instance)
		{
			if (instance.Code == null)
			{
				throw new ArgumentNullException("Code", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Code));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrencyId);
			if (instance.HasSubRegionId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SubRegionId);
			}
			if (instance.HasSymbol)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Symbol));
			}
			if (instance.HasRoundingExponent)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RoundingExponent);
			}
			if (instance.HasTaxText)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TaxText);
			}
			if (instance.HasChangedVersion)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ChangedVersion);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Code);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num += ProtocolParser.SizeOfUInt64((ulong)CurrencyId);
			if (HasSubRegionId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SubRegionId);
			}
			if (HasSymbol)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Symbol);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasRoundingExponent)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RoundingExponent);
			}
			if (HasTaxText)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TaxText);
			}
			if (HasChangedVersion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ChangedVersion);
			}
			return num + 2;
		}
	}
}
