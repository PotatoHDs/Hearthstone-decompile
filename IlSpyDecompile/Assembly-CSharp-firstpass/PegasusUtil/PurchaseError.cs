using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class PurchaseError : IProtoBuf
	{
		public enum Error
		{
			E_UNKNOWN = -1,
			E_SUCCESS = 0,
			E_STILL_IN_PROGRESS = 1,
			E_INVALID_BNET = 2,
			E_SERVICE_NA = 3,
			E_PURCHASE_IN_PROGRESS = 4,
			E_DATABASE = 5,
			E_INVALID_QUANTITY = 6,
			E_DUPLICATE_LICENSE = 7,
			E_REQUEST_NOT_SENT = 8,
			E_NO_ACTIVE_BPAY = 9,
			E_FAILED_RISK = 10,
			E_CANCELED = 11,
			E_WAIT_MOP = 12,
			E_WAIT_CLIENT_CONFIRM = 13,
			E_WAIT_CLIENT_RISK = 14,
			E_PRODUCT_NA = 0xF,
			E_RISK_TIMEOUT = 0x10,
			E_PRODUCT_ALREADY_OWNED = 17,
			E_WAIT_THIRD_PARTY_RECEIPT = 18,
			E_PRODUCT_EVENT_HAS_ENDED = 19,
			E_BP_GENERIC_FAIL = 100,
			E_BP_INVALID_CC_EXPIRY = 101,
			E_BP_RISK_ERROR = 102,
			E_BP_NO_VALID_PAYMENT = 103,
			E_BP_PAYMENT_AUTH = 104,
			E_BP_PROVIDER_DENIED = 105,
			E_BP_PURCHASE_BAN = 106,
			E_BP_SPENDING_LIMIT = 107,
			E_BP_PARENTAL_CONTROL = 108,
			E_BP_THROTTLED = 109,
			E_BP_THIRD_PARTY_BAD_RECEIPT = 110,
			E_BP_THIRD_PARTY_RECEIPT_USED = 111,
			E_BP_PRODUCT_UNIQUENESS_VIOLATED = 112,
			E_BP_REGION_IS_DOWN = 113,
			E_BP_GENERIC_FAIL_RETRY_CONTACT_CS_IF_PERSISTS = 115,
			E_BP_CHALLENGE_ID_FAILED_VERIFICATION = 116
		}

		public bool HasPurchaseInProgress;

		private string _PurchaseInProgress;

		public bool HasErrorCode;

		private string _ErrorCode;

		public Error Error_ { get; set; }

		public string PurchaseInProgress
		{
			get
			{
				return _PurchaseInProgress;
			}
			set
			{
				_PurchaseInProgress = value;
				HasPurchaseInProgress = value != null;
			}
		}

		public string ErrorCode
		{
			get
			{
				return _ErrorCode;
			}
			set
			{
				_ErrorCode = value;
				HasErrorCode = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Error_.GetHashCode();
			if (HasPurchaseInProgress)
			{
				hashCode ^= PurchaseInProgress.GetHashCode();
			}
			if (HasErrorCode)
			{
				hashCode ^= ErrorCode.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			PurchaseError purchaseError = obj as PurchaseError;
			if (purchaseError == null)
			{
				return false;
			}
			if (!Error_.Equals(purchaseError.Error_))
			{
				return false;
			}
			if (HasPurchaseInProgress != purchaseError.HasPurchaseInProgress || (HasPurchaseInProgress && !PurchaseInProgress.Equals(purchaseError.PurchaseInProgress)))
			{
				return false;
			}
			if (HasErrorCode != purchaseError.HasErrorCode || (HasErrorCode && !ErrorCode.Equals(purchaseError.ErrorCode)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PurchaseError Deserialize(Stream stream, PurchaseError instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PurchaseError DeserializeLengthDelimited(Stream stream)
		{
			PurchaseError purchaseError = new PurchaseError();
			DeserializeLengthDelimited(stream, purchaseError);
			return purchaseError;
		}

		public static PurchaseError DeserializeLengthDelimited(Stream stream, PurchaseError instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PurchaseError Deserialize(Stream stream, PurchaseError instance, long limit)
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
					instance.Error_ = (Error)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.PurchaseInProgress = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.ErrorCode = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, PurchaseError instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Error_);
			if (instance.HasPurchaseInProgress)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PurchaseInProgress));
			}
			if (instance.HasErrorCode)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ErrorCode));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Error_);
			if (HasPurchaseInProgress)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(PurchaseInProgress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasErrorCode)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(ErrorCode);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num + 1;
		}
	}
}
