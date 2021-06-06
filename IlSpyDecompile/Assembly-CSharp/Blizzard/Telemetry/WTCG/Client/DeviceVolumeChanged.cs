using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class DeviceVolumeChanged : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasOldVolume;

		private float _OldVolume;

		public bool HasNewVolume;

		private float _NewVolume;

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

		public float OldVolume
		{
			get
			{
				return _OldVolume;
			}
			set
			{
				_OldVolume = value;
				HasOldVolume = true;
			}
		}

		public float NewVolume
		{
			get
			{
				return _NewVolume;
			}
			set
			{
				_NewVolume = value;
				HasNewVolume = true;
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
			if (HasOldVolume)
			{
				num ^= OldVolume.GetHashCode();
			}
			if (HasNewVolume)
			{
				num ^= NewVolume.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DeviceVolumeChanged deviceVolumeChanged = obj as DeviceVolumeChanged;
			if (deviceVolumeChanged == null)
			{
				return false;
			}
			if (HasPlayer != deviceVolumeChanged.HasPlayer || (HasPlayer && !Player.Equals(deviceVolumeChanged.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != deviceVolumeChanged.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(deviceVolumeChanged.DeviceInfo)))
			{
				return false;
			}
			if (HasOldVolume != deviceVolumeChanged.HasOldVolume || (HasOldVolume && !OldVolume.Equals(deviceVolumeChanged.OldVolume)))
			{
				return false;
			}
			if (HasNewVolume != deviceVolumeChanged.HasNewVolume || (HasNewVolume && !NewVolume.Equals(deviceVolumeChanged.NewVolume)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DeviceVolumeChanged Deserialize(Stream stream, DeviceVolumeChanged instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeviceVolumeChanged DeserializeLengthDelimited(Stream stream)
		{
			DeviceVolumeChanged deviceVolumeChanged = new DeviceVolumeChanged();
			DeserializeLengthDelimited(stream, deviceVolumeChanged);
			return deviceVolumeChanged;
		}

		public static DeviceVolumeChanged DeserializeLengthDelimited(Stream stream, DeviceVolumeChanged instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeviceVolumeChanged Deserialize(Stream stream, DeviceVolumeChanged instance, long limit)
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
				case 37:
					instance.OldVolume = binaryReader.ReadSingle();
					continue;
				case 45:
					instance.NewVolume = binaryReader.ReadSingle();
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

		public static void Serialize(Stream stream, DeviceVolumeChanged instance)
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
			if (instance.HasOldVolume)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.OldVolume);
			}
			if (instance.HasNewVolume)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.NewVolume);
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
			if (HasOldVolume)
			{
				num++;
				num += 4;
			}
			if (HasNewVolume)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
