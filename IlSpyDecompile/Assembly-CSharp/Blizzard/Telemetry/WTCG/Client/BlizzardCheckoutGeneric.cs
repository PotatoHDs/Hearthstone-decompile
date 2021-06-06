using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class BlizzardCheckoutGeneric : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasMessageKey;

		private string _MessageKey;

		public bool HasMessageValue;

		private string _MessageValue;

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

		public string MessageKey
		{
			get
			{
				return _MessageKey;
			}
			set
			{
				_MessageKey = value;
				HasMessageKey = value != null;
			}
		}

		public string MessageValue
		{
			get
			{
				return _MessageValue;
			}
			set
			{
				_MessageValue = value;
				HasMessageValue = value != null;
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
			if (HasMessageKey)
			{
				num ^= MessageKey.GetHashCode();
			}
			if (HasMessageValue)
			{
				num ^= MessageValue.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BlizzardCheckoutGeneric blizzardCheckoutGeneric = obj as BlizzardCheckoutGeneric;
			if (blizzardCheckoutGeneric == null)
			{
				return false;
			}
			if (HasPlayer != blizzardCheckoutGeneric.HasPlayer || (HasPlayer && !Player.Equals(blizzardCheckoutGeneric.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != blizzardCheckoutGeneric.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(blizzardCheckoutGeneric.DeviceInfo)))
			{
				return false;
			}
			if (HasMessageKey != blizzardCheckoutGeneric.HasMessageKey || (HasMessageKey && !MessageKey.Equals(blizzardCheckoutGeneric.MessageKey)))
			{
				return false;
			}
			if (HasMessageValue != blizzardCheckoutGeneric.HasMessageValue || (HasMessageValue && !MessageValue.Equals(blizzardCheckoutGeneric.MessageValue)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BlizzardCheckoutGeneric Deserialize(Stream stream, BlizzardCheckoutGeneric instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BlizzardCheckoutGeneric DeserializeLengthDelimited(Stream stream)
		{
			BlizzardCheckoutGeneric blizzardCheckoutGeneric = new BlizzardCheckoutGeneric();
			DeserializeLengthDelimited(stream, blizzardCheckoutGeneric);
			return blizzardCheckoutGeneric;
		}

		public static BlizzardCheckoutGeneric DeserializeLengthDelimited(Stream stream, BlizzardCheckoutGeneric instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BlizzardCheckoutGeneric Deserialize(Stream stream, BlizzardCheckoutGeneric instance, long limit)
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
					instance.MessageKey = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.MessageValue = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, BlizzardCheckoutGeneric instance)
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
			if (instance.HasMessageKey)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.MessageKey));
			}
			if (instance.HasMessageValue)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.MessageValue));
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
			if (HasMessageKey)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(MessageKey);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasMessageValue)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(MessageValue);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
