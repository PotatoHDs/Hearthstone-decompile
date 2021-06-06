using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class PurchaseMethod : IProtoBuf
	{
		public enum PacketID
		{
			ID = 272
		}

		public bool HasPmtProductId;

		private long _PmtProductId;

		public bool HasQuantity;

		private int _Quantity;

		public bool HasCurrencyDeprecated;

		private int _CurrencyDeprecated;

		public bool HasWalletName;

		private string _WalletName;

		public bool HasUseEbalance;

		private bool _UseEbalance;

		public bool HasError;

		private PurchaseError _Error;

		public bool HasTransactionId;

		private long _TransactionId;

		public bool HasIsZeroCostLicense;

		private bool _IsZeroCostLicense;

		public bool HasChallengeId;

		private string _ChallengeId;

		public bool HasChallengeUrl;

		private string _ChallengeUrl;

		public bool HasCurrencyCode;

		private string _CurrencyCode;

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

		public int Quantity
		{
			get
			{
				return _Quantity;
			}
			set
			{
				_Quantity = value;
				HasQuantity = true;
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

		public string WalletName
		{
			get
			{
				return _WalletName;
			}
			set
			{
				_WalletName = value;
				HasWalletName = value != null;
			}
		}

		public bool UseEbalance
		{
			get
			{
				return _UseEbalance;
			}
			set
			{
				_UseEbalance = value;
				HasUseEbalance = true;
			}
		}

		public PurchaseError Error
		{
			get
			{
				return _Error;
			}
			set
			{
				_Error = value;
				HasError = value != null;
			}
		}

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

		public string ChallengeId
		{
			get
			{
				return _ChallengeId;
			}
			set
			{
				_ChallengeId = value;
				HasChallengeId = value != null;
			}
		}

		public string ChallengeUrl
		{
			get
			{
				return _ChallengeUrl;
			}
			set
			{
				_ChallengeUrl = value;
				HasChallengeUrl = value != null;
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
			int num = GetType().GetHashCode();
			if (HasPmtProductId)
			{
				num ^= PmtProductId.GetHashCode();
			}
			if (HasQuantity)
			{
				num ^= Quantity.GetHashCode();
			}
			if (HasCurrencyDeprecated)
			{
				num ^= CurrencyDeprecated.GetHashCode();
			}
			if (HasWalletName)
			{
				num ^= WalletName.GetHashCode();
			}
			if (HasUseEbalance)
			{
				num ^= UseEbalance.GetHashCode();
			}
			if (HasError)
			{
				num ^= Error.GetHashCode();
			}
			if (HasTransactionId)
			{
				num ^= TransactionId.GetHashCode();
			}
			if (HasIsZeroCostLicense)
			{
				num ^= IsZeroCostLicense.GetHashCode();
			}
			if (HasChallengeId)
			{
				num ^= ChallengeId.GetHashCode();
			}
			if (HasChallengeUrl)
			{
				num ^= ChallengeUrl.GetHashCode();
			}
			if (HasCurrencyCode)
			{
				num ^= CurrencyCode.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PurchaseMethod purchaseMethod = obj as PurchaseMethod;
			if (purchaseMethod == null)
			{
				return false;
			}
			if (HasPmtProductId != purchaseMethod.HasPmtProductId || (HasPmtProductId && !PmtProductId.Equals(purchaseMethod.PmtProductId)))
			{
				return false;
			}
			if (HasQuantity != purchaseMethod.HasQuantity || (HasQuantity && !Quantity.Equals(purchaseMethod.Quantity)))
			{
				return false;
			}
			if (HasCurrencyDeprecated != purchaseMethod.HasCurrencyDeprecated || (HasCurrencyDeprecated && !CurrencyDeprecated.Equals(purchaseMethod.CurrencyDeprecated)))
			{
				return false;
			}
			if (HasWalletName != purchaseMethod.HasWalletName || (HasWalletName && !WalletName.Equals(purchaseMethod.WalletName)))
			{
				return false;
			}
			if (HasUseEbalance != purchaseMethod.HasUseEbalance || (HasUseEbalance && !UseEbalance.Equals(purchaseMethod.UseEbalance)))
			{
				return false;
			}
			if (HasError != purchaseMethod.HasError || (HasError && !Error.Equals(purchaseMethod.Error)))
			{
				return false;
			}
			if (HasTransactionId != purchaseMethod.HasTransactionId || (HasTransactionId && !TransactionId.Equals(purchaseMethod.TransactionId)))
			{
				return false;
			}
			if (HasIsZeroCostLicense != purchaseMethod.HasIsZeroCostLicense || (HasIsZeroCostLicense && !IsZeroCostLicense.Equals(purchaseMethod.IsZeroCostLicense)))
			{
				return false;
			}
			if (HasChallengeId != purchaseMethod.HasChallengeId || (HasChallengeId && !ChallengeId.Equals(purchaseMethod.ChallengeId)))
			{
				return false;
			}
			if (HasChallengeUrl != purchaseMethod.HasChallengeUrl || (HasChallengeUrl && !ChallengeUrl.Equals(purchaseMethod.ChallengeUrl)))
			{
				return false;
			}
			if (HasCurrencyCode != purchaseMethod.HasCurrencyCode || (HasCurrencyCode && !CurrencyCode.Equals(purchaseMethod.CurrencyCode)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PurchaseMethod Deserialize(Stream stream, PurchaseMethod instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PurchaseMethod DeserializeLengthDelimited(Stream stream)
		{
			PurchaseMethod purchaseMethod = new PurchaseMethod();
			DeserializeLengthDelimited(stream, purchaseMethod);
			return purchaseMethod;
		}

		public static PurchaseMethod DeserializeLengthDelimited(Stream stream, PurchaseMethod instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PurchaseMethod Deserialize(Stream stream, PurchaseMethod instance, long limit)
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
				case 8:
					instance.PmtProductId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.CurrencyDeprecated = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.WalletName = ProtocolParser.ReadString(stream);
					continue;
				case 40:
					instance.UseEbalance = ProtocolParser.ReadBool(stream);
					continue;
				case 50:
					if (instance.Error == null)
					{
						instance.Error = PurchaseError.DeserializeLengthDelimited(stream);
					}
					else
					{
						PurchaseError.DeserializeLengthDelimited(stream, instance.Error);
					}
					continue;
				case 56:
					instance.TransactionId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.IsZeroCostLicense = ProtocolParser.ReadBool(stream);
					continue;
				case 74:
					instance.ChallengeId = ProtocolParser.ReadString(stream);
					continue;
				case 82:
					instance.ChallengeUrl = ProtocolParser.ReadString(stream);
					continue;
				case 90:
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

		public static void Serialize(Stream stream, PurchaseMethod instance)
		{
			if (instance.HasPmtProductId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PmtProductId);
			}
			if (instance.HasQuantity)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Quantity);
			}
			if (instance.HasCurrencyDeprecated)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrencyDeprecated);
			}
			if (instance.HasWalletName)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.WalletName));
			}
			if (instance.HasUseEbalance)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.UseEbalance);
			}
			if (instance.HasError)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.Error.GetSerializedSize());
				PurchaseError.Serialize(stream, instance.Error);
			}
			if (instance.HasTransactionId)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TransactionId);
			}
			if (instance.HasIsZeroCostLicense)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.IsZeroCostLicense);
			}
			if (instance.HasChallengeId)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChallengeId));
			}
			if (instance.HasChallengeUrl)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChallengeUrl));
			}
			if (instance.HasCurrencyCode)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CurrencyCode));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPmtProductId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PmtProductId);
			}
			if (HasQuantity)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Quantity);
			}
			if (HasCurrencyDeprecated)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CurrencyDeprecated);
			}
			if (HasWalletName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(WalletName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasUseEbalance)
			{
				num++;
				num++;
			}
			if (HasError)
			{
				num++;
				uint serializedSize = Error.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasTransactionId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TransactionId);
			}
			if (HasIsZeroCostLicense)
			{
				num++;
				num++;
			}
			if (HasChallengeId)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(ChallengeId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasChallengeUrl)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(ChallengeUrl);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasCurrencyCode)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(CurrencyCode);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			return num;
		}
	}
}
