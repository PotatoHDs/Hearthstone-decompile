using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class BlizzardCheckoutPurchaseCompletedSuccess : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasTransactionId;

		private string _TransactionId;

		public bool HasProductId;

		private string _ProductId;

		public bool HasCurrency;

		private string _Currency;

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

		public string Currency
		{
			get
			{
				return _Currency;
			}
			set
			{
				_Currency = value;
				HasCurrency = value != null;
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
			if (HasCurrency)
			{
				num ^= Currency.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BlizzardCheckoutPurchaseCompletedSuccess blizzardCheckoutPurchaseCompletedSuccess = obj as BlizzardCheckoutPurchaseCompletedSuccess;
			if (blizzardCheckoutPurchaseCompletedSuccess == null)
			{
				return false;
			}
			if (HasPlayer != blizzardCheckoutPurchaseCompletedSuccess.HasPlayer || (HasPlayer && !Player.Equals(blizzardCheckoutPurchaseCompletedSuccess.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != blizzardCheckoutPurchaseCompletedSuccess.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(blizzardCheckoutPurchaseCompletedSuccess.DeviceInfo)))
			{
				return false;
			}
			if (HasTransactionId != blizzardCheckoutPurchaseCompletedSuccess.HasTransactionId || (HasTransactionId && !TransactionId.Equals(blizzardCheckoutPurchaseCompletedSuccess.TransactionId)))
			{
				return false;
			}
			if (HasProductId != blizzardCheckoutPurchaseCompletedSuccess.HasProductId || (HasProductId && !ProductId.Equals(blizzardCheckoutPurchaseCompletedSuccess.ProductId)))
			{
				return false;
			}
			if (HasCurrency != blizzardCheckoutPurchaseCompletedSuccess.HasCurrency || (HasCurrency && !Currency.Equals(blizzardCheckoutPurchaseCompletedSuccess.Currency)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BlizzardCheckoutPurchaseCompletedSuccess Deserialize(Stream stream, BlizzardCheckoutPurchaseCompletedSuccess instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BlizzardCheckoutPurchaseCompletedSuccess DeserializeLengthDelimited(Stream stream)
		{
			BlizzardCheckoutPurchaseCompletedSuccess blizzardCheckoutPurchaseCompletedSuccess = new BlizzardCheckoutPurchaseCompletedSuccess();
			DeserializeLengthDelimited(stream, blizzardCheckoutPurchaseCompletedSuccess);
			return blizzardCheckoutPurchaseCompletedSuccess;
		}

		public static BlizzardCheckoutPurchaseCompletedSuccess DeserializeLengthDelimited(Stream stream, BlizzardCheckoutPurchaseCompletedSuccess instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BlizzardCheckoutPurchaseCompletedSuccess Deserialize(Stream stream, BlizzardCheckoutPurchaseCompletedSuccess instance, long limit)
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
					instance.Currency = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, BlizzardCheckoutPurchaseCompletedSuccess instance)
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
			if (instance.HasCurrency)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Currency));
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
			if (HasCurrency)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(Currency);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}
	}
}
