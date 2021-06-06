using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011C5 RID: 4549
	public class GameRoundStartAudioSettings : IProtoBuf
	{
		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x0600CA21 RID: 51745 RVA: 0x003C8BBB File Offset: 0x003C6DBB
		// (set) Token: 0x0600CA22 RID: 51746 RVA: 0x003C8BC3 File Offset: 0x003C6DC3
		public DeviceInfo DeviceInfo
		{
			get
			{
				return this._DeviceInfo;
			}
			set
			{
				this._DeviceInfo = value;
				this.HasDeviceInfo = (value != null);
			}
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x0600CA23 RID: 51747 RVA: 0x003C8BD6 File Offset: 0x003C6DD6
		// (set) Token: 0x0600CA24 RID: 51748 RVA: 0x003C8BDE File Offset: 0x003C6DDE
		public Player Player
		{
			get
			{
				return this._Player;
			}
			set
			{
				this._Player = value;
				this.HasPlayer = (value != null);
			}
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x0600CA25 RID: 51749 RVA: 0x003C8BF1 File Offset: 0x003C6DF1
		// (set) Token: 0x0600CA26 RID: 51750 RVA: 0x003C8BF9 File Offset: 0x003C6DF9
		public bool DeviceMuted
		{
			get
			{
				return this._DeviceMuted;
			}
			set
			{
				this._DeviceMuted = value;
				this.HasDeviceMuted = true;
			}
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x0600CA27 RID: 51751 RVA: 0x003C8C09 File Offset: 0x003C6E09
		// (set) Token: 0x0600CA28 RID: 51752 RVA: 0x003C8C11 File Offset: 0x003C6E11
		public float DeviceVolume
		{
			get
			{
				return this._DeviceVolume;
			}
			set
			{
				this._DeviceVolume = value;
				this.HasDeviceVolume = true;
			}
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x0600CA29 RID: 51753 RVA: 0x003C8C21 File Offset: 0x003C6E21
		// (set) Token: 0x0600CA2A RID: 51754 RVA: 0x003C8C29 File Offset: 0x003C6E29
		public float MasterVolume
		{
			get
			{
				return this._MasterVolume;
			}
			set
			{
				this._MasterVolume = value;
				this.HasMasterVolume = true;
			}
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x0600CA2B RID: 51755 RVA: 0x003C8C39 File Offset: 0x003C6E39
		// (set) Token: 0x0600CA2C RID: 51756 RVA: 0x003C8C41 File Offset: 0x003C6E41
		public float MusicVolume
		{
			get
			{
				return this._MusicVolume;
			}
			set
			{
				this._MusicVolume = value;
				this.HasMusicVolume = true;
			}
		}

		// Token: 0x0600CA2D RID: 51757 RVA: 0x003C8C54 File Offset: 0x003C6E54
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			if (this.HasDeviceMuted)
			{
				num ^= this.DeviceMuted.GetHashCode();
			}
			if (this.HasDeviceVolume)
			{
				num ^= this.DeviceVolume.GetHashCode();
			}
			if (this.HasMasterVolume)
			{
				num ^= this.MasterVolume.GetHashCode();
			}
			if (this.HasMusicVolume)
			{
				num ^= this.MusicVolume.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CA2E RID: 51758 RVA: 0x003C8D00 File Offset: 0x003C6F00
		public override bool Equals(object obj)
		{
			GameRoundStartAudioSettings gameRoundStartAudioSettings = obj as GameRoundStartAudioSettings;
			return gameRoundStartAudioSettings != null && this.HasDeviceInfo == gameRoundStartAudioSettings.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(gameRoundStartAudioSettings.DeviceInfo)) && this.HasPlayer == gameRoundStartAudioSettings.HasPlayer && (!this.HasPlayer || this.Player.Equals(gameRoundStartAudioSettings.Player)) && this.HasDeviceMuted == gameRoundStartAudioSettings.HasDeviceMuted && (!this.HasDeviceMuted || this.DeviceMuted.Equals(gameRoundStartAudioSettings.DeviceMuted)) && this.HasDeviceVolume == gameRoundStartAudioSettings.HasDeviceVolume && (!this.HasDeviceVolume || this.DeviceVolume.Equals(gameRoundStartAudioSettings.DeviceVolume)) && this.HasMasterVolume == gameRoundStartAudioSettings.HasMasterVolume && (!this.HasMasterVolume || this.MasterVolume.Equals(gameRoundStartAudioSettings.MasterVolume)) && this.HasMusicVolume == gameRoundStartAudioSettings.HasMusicVolume && (!this.HasMusicVolume || this.MusicVolume.Equals(gameRoundStartAudioSettings.MusicVolume));
		}

		// Token: 0x0600CA2F RID: 51759 RVA: 0x003C8E28 File Offset: 0x003C7028
		public void Deserialize(Stream stream)
		{
			GameRoundStartAudioSettings.Deserialize(stream, this);
		}

		// Token: 0x0600CA30 RID: 51760 RVA: 0x003C8E32 File Offset: 0x003C7032
		public static GameRoundStartAudioSettings Deserialize(Stream stream, GameRoundStartAudioSettings instance)
		{
			return GameRoundStartAudioSettings.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CA31 RID: 51761 RVA: 0x003C8E40 File Offset: 0x003C7040
		public static GameRoundStartAudioSettings DeserializeLengthDelimited(Stream stream)
		{
			GameRoundStartAudioSettings gameRoundStartAudioSettings = new GameRoundStartAudioSettings();
			GameRoundStartAudioSettings.DeserializeLengthDelimited(stream, gameRoundStartAudioSettings);
			return gameRoundStartAudioSettings;
		}

		// Token: 0x0600CA32 RID: 51762 RVA: 0x003C8E5C File Offset: 0x003C705C
		public static GameRoundStartAudioSettings DeserializeLengthDelimited(Stream stream, GameRoundStartAudioSettings instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameRoundStartAudioSettings.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CA33 RID: 51763 RVA: 0x003C8E84 File Offset: 0x003C7084
		public static GameRoundStartAudioSettings Deserialize(Stream stream, GameRoundStartAudioSettings instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else
				{
					if (num <= 24)
					{
						if (num != 10)
						{
							if (num != 18)
							{
								if (num == 24)
								{
									instance.DeviceMuted = ProtocolParser.ReadBool(stream);
									continue;
								}
							}
							else
							{
								if (instance.Player == null)
								{
									instance.Player = Player.DeserializeLengthDelimited(stream);
									continue;
								}
								Player.DeserializeLengthDelimited(stream, instance.Player);
								continue;
							}
						}
						else
						{
							if (instance.DeviceInfo == null)
							{
								instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
								continue;
							}
							DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
							continue;
						}
					}
					else
					{
						if (num == 37)
						{
							instance.DeviceVolume = binaryReader.ReadSingle();
							continue;
						}
						if (num == 45)
						{
							instance.MasterVolume = binaryReader.ReadSingle();
							continue;
						}
						if (num == 53)
						{
							instance.MusicVolume = binaryReader.ReadSingle();
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600CA34 RID: 51764 RVA: 0x003C8FCE File Offset: 0x003C71CE
		public void Serialize(Stream stream)
		{
			GameRoundStartAudioSettings.Serialize(stream, this);
		}

		// Token: 0x0600CA35 RID: 51765 RVA: 0x003C8FD8 File Offset: 0x003C71D8
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

		// Token: 0x0600CA36 RID: 51766 RVA: 0x003C90B8 File Offset: 0x003C72B8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize2 = this.Player.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasDeviceMuted)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasDeviceVolume)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasMasterVolume)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasMusicVolume)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x04009FA3 RID: 40867
		public bool HasDeviceInfo;

		// Token: 0x04009FA4 RID: 40868
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009FA5 RID: 40869
		public bool HasPlayer;

		// Token: 0x04009FA6 RID: 40870
		private Player _Player;

		// Token: 0x04009FA7 RID: 40871
		public bool HasDeviceMuted;

		// Token: 0x04009FA8 RID: 40872
		private bool _DeviceMuted;

		// Token: 0x04009FA9 RID: 40873
		public bool HasDeviceVolume;

		// Token: 0x04009FAA RID: 40874
		private float _DeviceVolume;

		// Token: 0x04009FAB RID: 40875
		public bool HasMasterVolume;

		// Token: 0x04009FAC RID: 40876
		private float _MasterVolume;

		// Token: 0x04009FAD RID: 40877
		public bool HasMusicVolume;

		// Token: 0x04009FAE RID: 40878
		private float _MusicVolume;
	}
}
