using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class CancelPurchaseResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 275
		}

		public enum CancelResult
		{
			CR_SUCCESS = 1,
			CR_NOT_ALLOWED,
			CR_NOTHING_TO_CANCEL
		}

		public bool HasTransactionId;

		private long _TransactionId;

		public bool HasPmtProductId;

		private long _PmtProductId;

		public bool HasCurrencyDeprecated;

		private int _CurrencyDeprecated;

		public bool HasCurrencyCode;

		private string _CurrencyCode;

		public CancelResult Result { get; set; }

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
			hashCode ^= Result.GetHashCode();
			if (HasTransactionId)
			{
				hashCode ^= TransactionId.GetHashCode();
			}
			if (HasPmtProductId)
			{
				hashCode ^= PmtProductId.GetHashCode();
			}
			if (HasCurrencyDeprecated)
			{
				hashCode ^= CurrencyDeprecated.GetHashCode();
			}
			if (HasCurrencyCode)
			{
				hashCode ^= CurrencyCode.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			CancelPurchaseResponse cancelPurchaseResponse = obj as CancelPurchaseResponse;
			if (cancelPurchaseResponse == null)
			{
				return false;
			}
			if (!Result.Equals(cancelPurchaseResponse.Result))
			{
				return false;
			}
			if (HasTransactionId != cancelPurchaseResponse.HasTransactionId || (HasTransactionId && !TransactionId.Equals(cancelPurchaseResponse.TransactionId)))
			{
				return false;
			}
			if (HasPmtProductId != cancelPurchaseResponse.HasPmtProductId || (HasPmtProductId && !PmtProductId.Equals(cancelPurchaseResponse.PmtProductId)))
			{
				return false;
			}
			if (HasCurrencyDeprecated != cancelPurchaseResponse.HasCurrencyDeprecated || (HasCurrencyDeprecated && !CurrencyDeprecated.Equals(cancelPurchaseResponse.CurrencyDeprecated)))
			{
				return false;
			}
			if (HasCurrencyCode != cancelPurchaseResponse.HasCurrencyCode || (HasCurrencyCode && !CurrencyCode.Equals(cancelPurchaseResponse.CurrencyCode)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CancelPurchaseResponse Deserialize(Stream stream, CancelPurchaseResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CancelPurchaseResponse DeserializeLengthDelimited(Stream stream)
		{
			CancelPurchaseResponse cancelPurchaseResponse = new CancelPurchaseResponse();
			DeserializeLengthDelimited(stream, cancelPurchaseResponse);
			return cancelPurchaseResponse;
		}

		public static CancelPurchaseResponse DeserializeLengthDelimited(Stream stream, CancelPurchaseResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CancelPurchaseResponse Deserialize(Stream stream, CancelPurchaseResponse instance, long limit)
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
					instance.Result = (CancelResult)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.TransactionId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.PmtProductId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.CurrencyDeprecated = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 42:
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

		public static void Serialize(Stream stream, CancelPurchaseResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Result);
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
			if (instance.HasCurrencyDeprecated)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrencyDeprecated);
			}
			if (instance.HasCurrencyCode)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CurrencyCode));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Result);
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
			if (HasCurrencyDeprecated)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CurrencyDeprecated);
			}
			if (HasCurrencyCode)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(CurrencyCode);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 1;
		}
	}
}
