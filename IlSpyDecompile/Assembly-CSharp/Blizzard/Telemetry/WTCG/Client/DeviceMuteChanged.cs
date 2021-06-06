using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class DeviceMuteChanged : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasMuted;

		private bool _Muted;

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

		public bool Muted
		{
			get
			{
				return _Muted;
			}
			set
			{
				_Muted = value;
				HasMuted = true;
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
			if (HasMuted)
			{
				num ^= Muted.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DeviceMuteChanged deviceMuteChanged = obj as DeviceMuteChanged;
			if (deviceMuteChanged == null)
			{
				return false;
			}
			if (HasPlayer != deviceMuteChanged.HasPlayer || (HasPlayer && !Player.Equals(deviceMuteChanged.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != deviceMuteChanged.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(deviceMuteChanged.DeviceInfo)))
			{
				return false;
			}
			if (HasMuted != deviceMuteChanged.HasMuted || (HasMuted && !Muted.Equals(deviceMuteChanged.Muted)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DeviceMuteChanged Deserialize(Stream stream, DeviceMuteChanged instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeviceMuteChanged DeserializeLengthDelimited(Stream stream)
		{
			DeviceMuteChanged deviceMuteChanged = new DeviceMuteChanged();
			DeserializeLengthDelimited(stream, deviceMuteChanged);
			return deviceMuteChanged;
		}

		public static DeviceMuteChanged DeserializeLengthDelimited(Stream stream, DeviceMuteChanged instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeviceMuteChanged Deserialize(Stream stream, DeviceMuteChanged instance, long limit)
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
				case 32:
					instance.Muted = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, DeviceMuteChanged instance)
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
			if (instance.HasMuted)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.Muted);
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
			if (HasMuted)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
