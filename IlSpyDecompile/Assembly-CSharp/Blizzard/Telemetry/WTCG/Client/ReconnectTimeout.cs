using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class ReconnectTimeout : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasReconnectType;

		private string _ReconnectType;

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

		public string ReconnectType
		{
			get
			{
				return _ReconnectType;
			}
			set
			{
				_ReconnectType = value;
				HasReconnectType = value != null;
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
			if (HasReconnectType)
			{
				num ^= ReconnectType.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ReconnectTimeout reconnectTimeout = obj as ReconnectTimeout;
			if (reconnectTimeout == null)
			{
				return false;
			}
			if (HasPlayer != reconnectTimeout.HasPlayer || (HasPlayer && !Player.Equals(reconnectTimeout.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != reconnectTimeout.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(reconnectTimeout.DeviceInfo)))
			{
				return false;
			}
			if (HasReconnectType != reconnectTimeout.HasReconnectType || (HasReconnectType && !ReconnectType.Equals(reconnectTimeout.ReconnectType)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ReconnectTimeout Deserialize(Stream stream, ReconnectTimeout instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ReconnectTimeout DeserializeLengthDelimited(Stream stream)
		{
			ReconnectTimeout reconnectTimeout = new ReconnectTimeout();
			DeserializeLengthDelimited(stream, reconnectTimeout);
			return reconnectTimeout;
		}

		public static ReconnectTimeout DeserializeLengthDelimited(Stream stream, ReconnectTimeout instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ReconnectTimeout Deserialize(Stream stream, ReconnectTimeout instance, long limit)
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
					instance.ReconnectType = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, ReconnectTimeout instance)
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
			if (instance.HasReconnectType)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ReconnectType));
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
			if (HasReconnectType)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ReconnectType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
