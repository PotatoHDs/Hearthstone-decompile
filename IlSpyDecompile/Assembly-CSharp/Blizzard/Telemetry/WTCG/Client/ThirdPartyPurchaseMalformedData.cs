using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class ThirdPartyPurchaseMalformedData : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasTransactionId;

		private string _TransactionId;

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
			return num;
		}

		public override bool Equals(object obj)
		{
			ThirdPartyPurchaseMalformedData thirdPartyPurchaseMalformedData = obj as ThirdPartyPurchaseMalformedData;
			if (thirdPartyPurchaseMalformedData == null)
			{
				return false;
			}
			if (HasPlayer != thirdPartyPurchaseMalformedData.HasPlayer || (HasPlayer && !Player.Equals(thirdPartyPurchaseMalformedData.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != thirdPartyPurchaseMalformedData.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(thirdPartyPurchaseMalformedData.DeviceInfo)))
			{
				return false;
			}
			if (HasTransactionId != thirdPartyPurchaseMalformedData.HasTransactionId || (HasTransactionId && !TransactionId.Equals(thirdPartyPurchaseMalformedData.TransactionId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ThirdPartyPurchaseMalformedData Deserialize(Stream stream, ThirdPartyPurchaseMalformedData instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ThirdPartyPurchaseMalformedData DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyPurchaseMalformedData thirdPartyPurchaseMalformedData = new ThirdPartyPurchaseMalformedData();
			DeserializeLengthDelimited(stream, thirdPartyPurchaseMalformedData);
			return thirdPartyPurchaseMalformedData;
		}

		public static ThirdPartyPurchaseMalformedData DeserializeLengthDelimited(Stream stream, ThirdPartyPurchaseMalformedData instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ThirdPartyPurchaseMalformedData Deserialize(Stream stream, ThirdPartyPurchaseMalformedData instance, long limit)
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

		public static void Serialize(Stream stream, ThirdPartyPurchaseMalformedData instance)
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
			return num;
		}
	}
}
