using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class ThirdPartyPurchaseReceiptSubmitFail : IProtoBuf
	{
		public enum FailureReason
		{
			INVALID_STATE = 1,
			INVALID_PROVIDER,
			NO_ACTIVE_TRANSACTION,
			NO_THIRD_PARTY_USER_ID
		}

		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasTransactionId;

		private string _TransactionId;

		public bool HasProductId;

		private string _ProductId;

		public bool HasProvider;

		private string _Provider;

		public bool HasReason;

		private FailureReason _Reason;

		public bool HasInvalidData;

		private string _InvalidData;

		public Player Player
		{
			get
			{
				return _Player;
			}
			set
			{
				_Player = value;
				HasPlayer = value != null;
			}
		}

		public DeviceInfo DeviceInfo
		{
			get
			{
				return _DeviceInfo;
			}
			set
			{
				_DeviceInfo = value;
				HasDeviceInfo = value != null;
			}
		}

		public string TransactionId
		{
			get
			{
				return _TransactionId;
			}
			set
			{
				_TransactionId = value;
				HasTransactionId = value != null;
			}
		}

		public string ProductId
		{
			get
			{
				return _ProductId;
			}
			set
			{
				_ProductId = value;
				HasProductId = value != null;
			}
		}

		public string Provider
		{
			get
			{
				return _Provider;
			}
			set
			{
				_Provider = value;
				HasProvider = value != null;
			}
		}

		public FailureReason Reason
		{
			get
			{
				return _Reason;
			}
			set
			{
				_Reason = value;
				HasReason = true;
			}
		}

		public string InvalidData
		{
			get
			{
				return _InvalidData;
			}
			set
			{
				_InvalidData = value;
				HasInvalidData = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPlayer)
			{
				num ^= Player.GetHashCode();
			}
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasTransactionId)
			{
				num ^= TransactionId.GetHashCode();
			}
			if (HasProductId)
			{
				num ^= ProductId.GetHashCode();
			}
			if (HasProvider)
			{
				num ^= Provider.GetHashCode();
			}
			if (HasReason)
			{
				num ^= Reason.GetHashCode();
			}
			if (HasInvalidData)
			{
				num ^= InvalidData.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ThirdPartyPurchaseReceiptSubmitFail thirdPartyPurchaseReceiptSubmitFail = obj as ThirdPartyPurchaseReceiptSubmitFail;
			if (thirdPartyPurchaseReceiptSubmitFail == null)
			{
				return false;
			}
			if (HasPlayer != thirdPartyPurchaseReceiptSubmitFail.HasPlayer || (HasPlayer && !Player.Equals(thirdPartyPurchaseReceiptSubmitFail.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != thirdPartyPurchaseReceiptSubmitFail.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(thirdPartyPurchaseReceiptSubmitFail.DeviceInfo)))
			{
				return false;
			}
			if (HasTransactionId != thirdPartyPurchaseReceiptSubmitFail.HasTransactionId || (HasTransactionId && !TransactionId.Equals(thirdPartyPurchaseReceiptSubmitFail.TransactionId)))
			{
				return false;
			}
			if (HasProductId != thirdPartyPurchaseReceiptSubmitFail.HasProductId || (HasProductId && !ProductId.Equals(thirdPartyPurchaseReceiptSubmitFail.ProductId)))
			{
				return false;
			}
			if (HasProvider != thirdPartyPurchaseReceiptSubmitFail.HasProvider || (HasProvider && !Provider.Equals(thirdPartyPurchaseReceiptSubmitFail.Provider)))
			{
				return false;
			}
			if (HasReason != thirdPartyPurchaseReceiptSubmitFail.HasReason || (HasReason && !Reason.Equals(thirdPartyPurchaseReceiptSubmitFail.Reason)))
			{
				return false;
			}
			if (HasInvalidData != thirdPartyPurchaseReceiptSubmitFail.HasInvalidData || (HasInvalidData && !InvalidData.Equals(thirdPartyPurchaseReceiptSubmitFail.InvalidData)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ThirdPartyPurchaseReceiptSubmitFail Deserialize(Stream stream, ThirdPartyPurchaseReceiptSubmitFail instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ThirdPartyPurchaseReceiptSubmitFail DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyPurchaseReceiptSubmitFail thirdPartyPurchaseReceiptSubmitFail = new ThirdPartyPurchaseReceiptSubmitFail();
			DeserializeLengthDelimited(stream, thirdPartyPurchaseReceiptSubmitFail);
			return thirdPartyPurchaseReceiptSubmitFail;
		}

		public static ThirdPartyPurchaseReceiptSubmitFail DeserializeLengthDelimited(Stream stream, ThirdPartyPurchaseReceiptSubmitFail instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ThirdPartyPurchaseReceiptSubmitFail Deserialize(Stream stream, ThirdPartyPurchaseReceiptSubmitFail instance, long limit)
		{
			instance.Reason = FailureReason.INVALID_STATE;
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
					if (instance.Player == null)
					{
						instance.Player = Player.DeserializeLengthDelimited(stream);
					}
					else
					{
						Player.DeserializeLengthDelimited(stream, instance.Player);
					}
					continue;
				case 18:
					if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
					continue;
				case 26:
					instance.TransactionId = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.ProductId = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					instance.Provider = ProtocolParser.ReadString(stream);
					continue;
				case 48:
					instance.Reason = (FailureReason)ProtocolParser.ReadUInt64(stream);
					continue;
				case 58:
					instance.InvalidData = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, ThirdPartyPurchaseReceiptSubmitFail instance)
		{
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasTransactionId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TransactionId));
			}
			if (instance.HasProductId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ProductId));
			}
			if (instance.HasProvider)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Provider));
			}
			if (instance.HasReason)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Reason);
			}
			if (instance.HasInvalidData)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.InvalidData));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPlayer)
			{
				num++;
				uint serializedSize = Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasDeviceInfo)
			{
				num++;
				uint serializedSize2 = DeviceInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasTransactionId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(TransactionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasProductId)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(ProductId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasProvider)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(Provider);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasReason)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Reason);
			}
			if (HasInvalidData)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(InvalidData);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			return num;
		}
	}
}
