using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class StartupAudioSettings : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasDeviceMuted;

		private bool _DeviceMuted;

		public bool HasDeviceVolume;

		private float _DeviceVolume;

		public bool HasMasterVolume;

		private float _MasterVolume;

		public bool HasMusicVolume;

		private float _MusicVolume;

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

		public bool DeviceMuted
		{
			get
			{
				return _DeviceMuted;
			}
			set
			{
				_DeviceMuted = value;
				HasDeviceMuted = true;
			}
		}

		public float DeviceVolume
		{
			get
			{
				return _DeviceVolume;
			}
			set
			{
				_DeviceVolume = value;
				HasDeviceVolume = true;
			}
		}

		public float MasterVolume
		{
			get
			{
				return _MasterVolume;
			}
			set
			{
				_MasterVolume = value;
				HasMasterVolume = true;
			}
		}

		public float MusicVolume
		{
			get
			{
				return _MusicVolume;
			}
			set
			{
				_MusicVolume = value;
				HasMusicVolume = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasDeviceMuted)
			{
				num ^= DeviceMuted.GetHashCode();
			}
			if (HasDeviceVolume)
			{
				num ^= DeviceVolume.GetHashCode();
			}
			if (HasMasterVolume)
			{
				num ^= MasterVolume.GetHashCode();
			}
			if (HasMusicVolume)
			{
				num ^= MusicVolume.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			StartupAudioSettings startupAudioSettings = obj as StartupAudioSettings;
			if (startupAudioSettings == null)
			{
				return false;
			}
			if (HasDeviceInfo != startupAudioSettings.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(startupAudioSettings.DeviceInfo)))
			{
				return false;
			}
			if (HasDeviceMuted != startupAudioSettings.HasDeviceMuted || (HasDeviceMuted && !DeviceMuted.Equals(startupAudioSettings.DeviceMuted)))
			{
				return false;
			}
			if (HasDeviceVolume != startupAudioSettings.HasDeviceVolume || (HasDeviceVolume && !DeviceVolume.Equals(startupAudioSettings.DeviceVolume)))
			{
				return false;
			}
			if (HasMasterVolume != startupAudioSettings.HasMasterVolume || (HasMasterVolume && !MasterVolume.Equals(startupAudioSettings.MasterVolume)))
			{
				return false;
			}
			if (HasMusicVolume != startupAudioSettings.HasMusicVolume || (HasMusicVolume && !MusicVolume.Equals(startupAudioSettings.MusicVolume)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static StartupAudioSettings Deserialize(Stream stream, StartupAudioSettings instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static StartupAudioSettings DeserializeLengthDelimited(Stream stream)
		{
			StartupAudioSettings startupAudioSettings = new StartupAudioSettings();
			DeserializeLengthDelimited(stream, startupAudioSettings);
			return startupAudioSettings;
		}

		public static StartupAudioSettings DeserializeLengthDelimited(Stream stream, StartupAudioSettings instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static StartupAudioSettings Deserialize(Stream stream, StartupAudioSettings instance, long limit)
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
					if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
					continue;
				case 16:
					instance.DeviceMuted = ProtocolParser.ReadBool(stream);
					continue;
				case 29:
					instance.DeviceVolume = binaryReader.ReadSingle();
					continue;
				case 37:
					instance.MasterVolume = binaryReader.ReadSingle();
					continue;
				case 45:
					instance.MusicVolume = binaryReader.ReadSingle();
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

		public static void Serialize(Stream stream, StartupAudioSettings instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasDeviceMuted)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.DeviceMuted);
			}
			if (instance.HasDeviceVolume)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.DeviceVolume);
			}
			if (instance.HasMasterVolume)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.MasterVolume);
			}
			if (instance.HasMusicVolume)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.MusicVolume);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasDeviceInfo)
			{
				num++;
				uint serializedSize = DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasDeviceMuted)
			{
				num++;
				num++;
			}
			if (HasDeviceVolume)
			{
				num++;
				num += 4;
			}
			if (HasMasterVolume)
			{
				num++;
				num += 4;
			}
			if (HasMusicVolume)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
