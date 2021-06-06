using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class GameRoundStartAudioSettings : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasPlayer;

		private Player _Player;

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
			if (HasPlayer)
			{
				num ^= Player.GetHashCode();
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
			GameRoundStartAudioSettings gameRoundStartAudioSettings = obj as GameRoundStartAudioSettings;
			if (gameRoundStartAudioSettings == null)
			{
				return false;
			}
			if (HasDeviceInfo != gameRoundStartAudioSettings.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(gameRoundStartAudioSettings.DeviceInfo)))
			{
				return false;
			}
			if (HasPlayer != gameRoundStartAudioSettings.HasPlayer || (HasPlayer && !Player.Equals(gameRoundStartAudioSettings.Player)))
			{
				return false;
			}
			if (HasDeviceMuted != gameRoundStartAudioSettings.HasDeviceMuted || (HasDeviceMuted && !DeviceMuted.Equals(gameRoundStartAudioSettings.DeviceMuted)))
			{
				return false;
			}
			if (HasDeviceVolume != gameRoundStartAudioSettings.HasDeviceVolume || (HasDeviceVolume && !DeviceVolume.Equals(gameRoundStartAudioSettings.DeviceVolume)))
			{
				return false;
			}
			if (HasMasterVolume != gameRoundStartAudioSettings.HasMasterVolume || (HasMasterVolume && !MasterVolume.Equals(gameRoundStartAudioSettings.MasterVolume)))
			{
				return false;
			}
			if (HasMusicVolume != gameRoundStartAudioSettings.HasMusicVolume || (HasMusicVolume && !MusicVolume.Equals(gameRoundStartAudioSettings.MusicVolume)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameRoundStartAudioSettings Deserialize(Stream stream, GameRoundStartAudioSettings instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameRoundStartAudioSettings DeserializeLengthDelimited(Stream stream)
		{
			GameRoundStartAudioSettings gameRoundStartAudioSettings = new GameRoundStartAudioSettings();
			DeserializeLengthDelimited(stream, gameRoundStartAudioSettings);
			return gameRoundStartAudioSettings;
		}

		public static GameRoundStartAudioSettings DeserializeLengthDelimited(Stream stream, GameRoundStartAudioSettings instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameRoundStartAudioSettings Deserialize(Stream stream, GameRoundStartAudioSettings instance, long limit)
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
				case 18:
					if (instance.Player == null)
					{
						instance.Player = Player.DeserializeLengthDelimited(stream);
					}
					else
					{
						Player.DeserializeLengthDelimited(stream, instance.Player);
					}
					continue;
				case 24:
					instance.DeviceMuted = ProtocolParser.ReadBool(stream);
					continue;
				case 37:
					instance.DeviceVolume = binaryReader.ReadSingle();
					continue;
				case 45:
					instance.MasterVolume = binaryReader.ReadSingle();
					continue;
				case 53:
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

		public static void Serialize(Stream stream, GameRoundStartAudioSettings instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasPlayer)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasDeviceMuted)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.DeviceMuted);
			}
			if (instance.HasDeviceVolume)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.DeviceVolume);
			}
			if (instance.HasMasterVolume)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.MasterVolume);
			}
			if (instance.HasMusicVolume)
			{
				stream.WriteByte(53);
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
			if (HasPlayer)
			{
				num++;
				uint serializedSize2 = Player.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
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
