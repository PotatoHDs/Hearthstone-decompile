using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class ReconnectSuccess : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasDisconnectDuration;

		private float _DisconnectDuration;

		public bool HasReconnectDuration;

		private float _ReconnectDuration;

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

		public float DisconnectDuration
		{
			get
			{
				return _DisconnectDuration;
			}
			set
			{
				_DisconnectDuration = value;
				HasDisconnectDuration = true;
			}
		}

		public float ReconnectDuration
		{
			get
			{
				return _ReconnectDuration;
			}
			set
			{
				_ReconnectDuration = value;
				HasReconnectDuration = true;
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
			if (HasDisconnectDuration)
			{
				num ^= DisconnectDuration.GetHashCode();
			}
			if (HasReconnectDuration)
			{
				num ^= ReconnectDuration.GetHashCode();
			}
			if (HasReconnectType)
			{
				num ^= ReconnectType.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ReconnectSuccess reconnectSuccess = obj as ReconnectSuccess;
			if (reconnectSuccess == null)
			{
				return false;
			}
			if (HasPlayer != reconnectSuccess.HasPlayer || (HasPlayer && !Player.Equals(reconnectSuccess.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != reconnectSuccess.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(reconnectSuccess.DeviceInfo)))
			{
				return false;
			}
			if (HasDisconnectDuration != reconnectSuccess.HasDisconnectDuration || (HasDisconnectDuration && !DisconnectDuration.Equals(reconnectSuccess.DisconnectDuration)))
			{
				return false;
			}
			if (HasReconnectDuration != reconnectSuccess.HasReconnectDuration || (HasReconnectDuration && !ReconnectDuration.Equals(reconnectSuccess.ReconnectDuration)))
			{
				return false;
			}
			if (HasReconnectType != reconnectSuccess.HasReconnectType || (HasReconnectType && !ReconnectType.Equals(reconnectSuccess.ReconnectType)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ReconnectSuccess Deserialize(Stream stream, ReconnectSuccess instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ReconnectSuccess DeserializeLengthDelimited(Stream stream)
		{
			ReconnectSuccess reconnectSuccess = new ReconnectSuccess();
			DeserializeLengthDelimited(stream, reconnectSuccess);
			return reconnectSuccess;
		}

		public static ReconnectSuccess DeserializeLengthDelimited(Stream stream, ReconnectSuccess instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ReconnectSuccess Deserialize(Stream stream, ReconnectSuccess instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				case 29:
					instance.DisconnectDuration = binaryReader.ReadSingle();
					continue;
				case 37:
					instance.ReconnectDuration = binaryReader.ReadSingle();
					continue;
				case 42:
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

		public static void Serialize(Stream stream, ReconnectSuccess instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
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
			if (instance.HasDisconnectDuration)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.DisconnectDuration);
			}
			if (instance.HasReconnectDuration)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.ReconnectDuration);
			}
			if (instance.HasReconnectType)
			{
				stream.WriteByte(42);
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
			if (HasDisconnectDuration)
			{
				num++;
				num += 4;
			}
			if (HasReconnectDuration)
			{
				num++;
				num += 4;
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
