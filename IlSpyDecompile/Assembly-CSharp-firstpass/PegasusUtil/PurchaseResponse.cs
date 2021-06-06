using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class PurchaseResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 0x100
		}

		public bool HasTransactionId;

		private long _TransactionId;

		public bool HasPmtProductId;

		private long _PmtProductId;

		public bool HasThirdPartyId;

		private string _ThirdPartyId;

		public bool HasCurrencyDeprecated;

		private int _CurrencyDeprecated;

		public bool HasIsZeroCostLicense;

		private bool _IsZeroCostLicense;

		public bool HasCurrencyCode;

		private string _CurrencyCode;

		public PurchaseError Error { get; set; }

		public long TransactionId
		{
			get
			{
				return _TransactionId;
			}
			set
			{
				_TransactionId = value;
				HasTransactionId = true;
			}
		}

		public long PmtProductId
		{
			get
			{
				return _PmtProductId;
			}
			set
			{
				_PmtProductId = value;
				HasPmtProductId = true;
			}
		}

		public string ThirdPartyId
		{
			get
			{
				return _ThirdPartyId;
			}
			set
			{
				_ThirdPartyId = value;
				HasThirdPartyId = value != null;
			}
		}

		public int CurrencyDeprecated
		{
			get
			{
				return _CurrencyDeprecated;
			}
			set
			{
				_CurrencyDeprecated = value;
				HasCurrencyDeprecated = true;
			}
		}

		public bool IsZeroCostLicense
		{
			get
			{
				return _IsZeroCostLicense;
			}
			set
			{
				_IsZeroCostLicense = value;
				HasIsZeroCostLicense = true;
			}
		}

		public string CurrencyCode
		{
			get
			{
				return _CurrencyCode;
			}
			set
			{
				_CurrencyCode = value;
				HasCurrencyCode = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Error.GetHashCode();
			if (HasTransactionId)
			{
				hashCode ^= TransactionId.GetHashCode();
			}
			if (HasPmtProductId)
			{
				hashCode ^= PmtProductId.GetHashCode();
			}
			if (HasThirdPartyId)
			{
				hashCode ^= ThirdPartyId.GetHashCode();
			}
			if (HasCurrencyDeprecated)
			{
				hashCode ^= CurrencyDeprecated.GetHashCode();
			}
			if (HasIsZeroCostLicense)
			{
				hashCode ^= IsZeroCostLicense.GetHashCode();
			}
			if (HasCurrencyCode)
			{
				hashCode ^= CurrencyCode.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			PurchaseResponse purchaseResponse = obj as PurchaseResponse;
			if (purchaseResponse == null)
			{
				return false;
			}
			if (!Error.Equals(purchaseResponse.Error))
			{
				return false;
			}
			if (HasTransactionId != purchaseResponse.HasTransactionId || (HasTransactionId && !TransactionId.Equals(purchaseResponse.TransactionId)))
			{
				return false;
			}
			if (HasPmtProductId != purchaseResponse.HasPmtProductId || (HasPmtProductId && !PmtProductId.Equals(purchaseResponse.PmtProductId)))
			{
				return false;
			}
			if (HasThirdPartyId != purchaseResponse.HasThirdPartyId || (HasThirdPartyId && !ThirdPartyId.Equals(purchaseResponse.ThirdPartyId)))
			{
				return false;
			}
			if (HasCurrencyDeprecated != purchaseResponse.HasCurrencyDeprecated || (HasCurrencyDeprecated && !CurrencyDeprecated.Equals(purchaseResponse.CurrencyDeprecated)))
			{
				return false;
			}
			if (HasIsZeroCostLicense != purchaseResponse.HasIsZeroCostLicense || (HasIsZeroCostLicense && !IsZeroCostLicense.Equals(purchaseResponse.IsZeroCostLicense)))
			{
				return false;
			}
			if (HasCurrencyCode != purchaseResponse.HasCurrencyCode || (HasCurrencyCode && !CurrencyCode.Equals(purchaseResponse.CurrencyCode)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PurchaseResponse Deserialize(Stream stream, PurchaseResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PurchaseResponse DeserializeLengthDelimited(Stream stream)
		{
			PurchaseResponse purchaseResponse = new PurchaseResponse();
			DeserializeLengthDelimited(stream, purchaseResponse);
			return purchaseResponse;
		}

		public static PurchaseResponse DeserializeLengthDelimited(Stream stream, PurchaseResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PurchaseResponse Deserialize(Stream stream, PurchaseResponse instance, long limit)
		{
			instance.CurrencyCode = "";
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
					if (instance.Error == null)
					{
						instance.Error = PurchaseError.DeserializeLengthDelimited(stream);
					}
					else
					{
						PurchaseError.DeserializeLengthDelimited(stream, instance.Error);
					}
					continue;
				case 16:
					instance.TransactionId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.PmtProductId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.ThirdPartyId = ProtocolParser.ReadString(stream);
					continue;
				case 40:
					instance.CurrencyDeprecated = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.IsZeroCostLicense = ProtocolParser.ReadBool(stream);
					continue;
				case 58:
					instance.CurrencyCode = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, PurchaseResponse instance)
		{
			if (instance.Error == null)
			{
				throw new ArgumentNullException("Error", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Error.GetSerializedSize());
			PurchaseError.Serialize(stream, instance.Error);
			if (instance.HasTransactionId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TransactionId);
			}
			if (instance.HasPmtProductId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PmtProductId);
			}
			if (instance.HasThirdPartyId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ThirdPartyId));
			}
			if (instance.HasCurrencyDeprecated)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrencyDeprecated);
			}
			if (instance.HasIsZeroCostLicense)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.IsZeroCostLicense);
			}
			if (instance.HasCurrencyCode)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CurrencyCode));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = Error.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasTransactionId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TransactionId);
			}
			if (HasPmtProductId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PmtProductId);
			}
			if (HasThirdPartyId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ThirdPartyId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasCurrencyDeprecated)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CurrencyDeprecated);
			}
			if (HasIsZeroCostLicense)
			{
				num++;
				num++;
			}
			if (HasCurrencyCode)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(CurrencyCode);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num + 1;
		}
	}
}
