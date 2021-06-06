using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	public class BattlePayStatusResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 265
		}

		public enum PurchaseState
		{
			PS_READY,
			PS_CHECK_RESULTS,
			PS_ERROR
		}

		public bool HasPmtProductId;

		private long _PmtProductId;

		public bool HasPurchaseError;

		private PurchaseError _PurchaseError;

		public bool HasTransactionId;

		private long _TransactionId;

		public bool HasThirdPartyId;

		private string _ThirdPartyId;

		public bool HasCurrencyDeprecated;

		private int _CurrencyDeprecated;

		public bool HasProvider;

		private BattlePayProvider _Provider;

		public bool HasCurrencyCode;

		private string _CurrencyCode;

		public PurchaseState Status { get; set; }

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

		public PurchaseError PurchaseError
		{
			get
			{
				return _PurchaseError;
			}
			set
			{
				_PurchaseError = value;
				HasPurchaseError = value != null;
			}
		}

		public bool BattlePayAvailable { get; set; }

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

		public BattlePayProvider Provider
		{
			get
			{
				return _Provider;
			}
			set
			{
				_Provider = value;
				HasProvider = true;
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
			hashCode ^= Status.GetHashCode();
			if (HasPmtProductId)
			{
				hashCode ^= PmtProductId.GetHashCode();
			}
			if (HasPurchaseError)
			{
				hashCode ^= PurchaseError.GetHashCode();
			}
			hashCode ^= BattlePayAvailable.GetHashCode();
			if (HasTransactionId)
			{
				hashCode ^= TransactionId.GetHashCode();
			}
			if (HasThirdPartyId)
			{
				hashCode ^= ThirdPartyId.GetHashCode();
			}
			if (HasCurrencyDeprecated)
			{
				hashCode ^= CurrencyDeprecated.GetHashCode();
			}
			if (HasProvider)
			{
				hashCode ^= Provider.GetHashCode();
			}
			if (HasCurrencyCode)
			{
				hashCode ^= CurrencyCode.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			BattlePayStatusResponse battlePayStatusResponse = obj as BattlePayStatusResponse;
			if (battlePayStatusResponse == null)
			{
				return false;
			}
			if (!Status.Equals(battlePayStatusResponse.Status))
			{
				return false;
			}
			if (HasPmtProductId != battlePayStatusResponse.HasPmtProductId || (HasPmtProductId && !PmtProductId.Equals(battlePayStatusResponse.PmtProductId)))
			{
				return false;
			}
			if (HasPurchaseError != battlePayStatusResponse.HasPurchaseError || (HasPurchaseError && !PurchaseError.Equals(battlePayStatusResponse.PurchaseError)))
			{
				return false;
			}
			if (!BattlePayAvailable.Equals(battlePayStatusResponse.BattlePayAvailable))
			{
				return false;
			}
			if (HasTransactionId != battlePayStatusResponse.HasTransactionId || (HasTransactionId && !TransactionId.Equals(battlePayStatusResponse.TransactionId)))
			{
				return false;
			}
			if (HasThirdPartyId != battlePayStatusResponse.HasThirdPartyId || (HasThirdPartyId && !ThirdPartyId.Equals(battlePayStatusResponse.ThirdPartyId)))
			{
				return false;
			}
			if (HasCurrencyDeprecated != battlePayStatusResponse.HasCurrencyDeprecated || (HasCurrencyDeprecated && !CurrencyDeprecated.Equals(battlePayStatusResponse.CurrencyDeprecated)))
			{
				return false;
			}
			if (HasProvider != battlePayStatusResponse.HasProvider || (HasProvider && !Provider.Equals(battlePayStatusResponse.Provider)))
			{
				return false;
			}
			if (HasCurrencyCode != battlePayStatusResponse.HasCurrencyCode || (HasCurrencyCode && !CurrencyCode.Equals(battlePayStatusResponse.CurrencyCode)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BattlePayStatusResponse Deserialize(Stream stream, BattlePayStatusResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BattlePayStatusResponse DeserializeLengthDelimited(Stream stream)
		{
			BattlePayStatusResponse battlePayStatusResponse = new BattlePayStatusResponse();
			DeserializeLengthDelimited(stream, battlePayStatusResponse);
			return battlePayStatusResponse;
		}

		public static BattlePayStatusResponse DeserializeLengthDelimited(Stream stream, BattlePayStatusResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BattlePayStatusResponse Deserialize(Stream stream, BattlePayStatusResponse instance, long limit)
		{
			instance.Provider = BattlePayProvider.BP_PROVIDER_BLIZZARD;
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
					instance.Status = (PurchaseState)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.PmtProductId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					if (instance.PurchaseError == null)
					{
						instance.PurchaseError = PurchaseError.DeserializeLengthDelimited(stream);
					}
					else
					{
						PurchaseError.DeserializeLengthDelimited(stream, instance.PurchaseError);
					}
					continue;
				case 32:
					instance.BattlePayAvailable = ProtocolParser.ReadBool(stream);
					continue;
				case 40:
					instance.TransactionId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 50:
					instance.ThirdPartyId = ProtocolParser.ReadString(stream);
					continue;
				case 56:
					instance.CurrencyDeprecated = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.Provider = (BattlePayProvider)ProtocolParser.ReadUInt64(stream);
					continue;
				case 74:
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

		public static void Serialize(Stream stream, BattlePayStatusResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Status);
			if (instance.HasPmtProductId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PmtProductId);
			}
			if (instance.HasPurchaseError)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.PurchaseError.GetSerializedSize());
				PurchaseError.Serialize(stream, instance.PurchaseError);
			}
			stream.WriteByte(32);
			ProtocolParser.WriteBool(stream, instance.BattlePayAvailable);
			if (instance.HasTransactionId)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TransactionId);
			}
			if (instance.HasThirdPartyId)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ThirdPartyId));
			}
			if (instance.HasCurrencyDeprecated)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrencyDeprecated);
			}
			if (instance.HasProvider)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Provider);
			}
			if (instance.HasCurrencyCode)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CurrencyCode));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Status);
			if (HasPmtProductId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PmtProductId);
			}
			if (HasPurchaseError)
			{
				num++;
				uint serializedSize = PurchaseError.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			num++;
			if (HasTransactionId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TransactionId);
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
			if (HasProvider)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Provider);
			}
			if (HasCurrencyCode)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(CurrencyCode);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num + 2;
		}
	}
}
