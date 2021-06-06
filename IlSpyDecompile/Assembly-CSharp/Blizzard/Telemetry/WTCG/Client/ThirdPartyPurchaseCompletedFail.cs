using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class ThirdPartyPurchaseCompletedFail : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasTransactionId;

		private string _TransactionId;

		public bool HasProductId;

		private string _ProductId;

		public bool HasBpayProvider;

		private string _BpayProvider;

		public bool HasErrorInfo;

		private string _ErrorInfo;

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

		public string BpayProvider
		{
			get
			{
				return _BpayProvider;
			}
			set
			{
				_BpayProvider = value;
				HasBpayProvider = value != null;
			}
		}

		public string ErrorInfo
		{
			get
			{
				return _ErrorInfo;
			}
			set
			{
				_ErrorInfo = value;
				HasErrorInfo = value != null;
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
			if (HasBpayProvider)
			{
				num ^= BpayProvider.GetHashCode();
			}
			if (HasErrorInfo)
			{
				num ^= ErrorInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ThirdPartyPurchaseCompletedFail thirdPartyPurchaseCompletedFail = obj as ThirdPartyPurchaseCompletedFail;
			if (thirdPartyPurchaseCompletedFail == null)
			{
				return false;
			}
			if (HasPlayer != thirdPartyPurchaseCompletedFail.HasPlayer || (HasPlayer && !Player.Equals(thirdPartyPurchaseCompletedFail.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != thirdPartyPurchaseCompletedFail.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(thirdPartyPurchaseCompletedFail.DeviceInfo)))
			{
				return false;
			}
			if (HasTransactionId != thirdPartyPurchaseCompletedFail.HasTransactionId || (HasTransactionId && !TransactionId.Equals(thirdPartyPurchaseCompletedFail.TransactionId)))
			{
				return false;
			}
			if (HasProductId != thirdPartyPurchaseCompletedFail.HasProductId || (HasProductId && !ProductId.Equals(thirdPartyPurchaseCompletedFail.ProductId)))
			{
				return false;
			}
			if (HasBpayProvider != thirdPartyPurchaseCompletedFail.HasBpayProvider || (HasBpayProvider && !BpayProvider.Equals(thirdPartyPurchaseCompletedFail.BpayProvider)))
			{
				return false;
			}
			if (HasErrorInfo != thirdPartyPurchaseCompletedFail.HasErrorInfo || (HasErrorInfo && !ErrorInfo.Equals(thirdPartyPurchaseCompletedFail.ErrorInfo)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ThirdPartyPurchaseCompletedFail Deserialize(Stream stream, ThirdPartyPurchaseCompletedFail instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ThirdPartyPurchaseCompletedFail DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyPurchaseCompletedFail thirdPartyPurchaseCompletedFail = new ThirdPartyPurchaseCompletedFail();
			DeserializeLengthDelimited(stream, thirdPartyPurchaseCompletedFail);
			return thirdPartyPurchaseCompletedFail;
		}

		public static ThirdPartyPurchaseCompletedFail DeserializeLengthDelimited(Stream stream, ThirdPartyPurchaseCompletedFail instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ThirdPartyPurchaseCompletedFail Deserialize(Stream stream, ThirdPartyPurchaseCompletedFail instance, long limit)
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
					instance.BpayProvider = ProtocolParser.ReadString(stream);
					continue;
				case 50:
					instance.ErrorInfo = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, ThirdPartyPurchaseCompletedFail instance)
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
			if (instance.HasBpayProvider)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BpayProvider));
			}
			if (instance.HasErrorInfo)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ErrorInfo));
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
			if (HasBpayProvider)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(BpayProvider);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasErrorInfo)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(ErrorInfo);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			return num;
		}
	}
}
