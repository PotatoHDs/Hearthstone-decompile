using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011DF RID: 4575
	public class MusicVolumeChanged : IProtoBuf
	{
		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x0600CC0B RID: 52235 RVA: 0x003CF8CB File Offset: 0x003CDACB
		// (set) Token: 0x0600CC0C RID: 52236 RVA: 0x003CF8D3 File Offset: 0x003CDAD3
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

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x0600CC0D RID: 52237 RVA: 0x003CF8E6 File Offset: 0x003CDAE6
		// (set) Token: 0x0600CC0E RID: 52238 RVA: 0x003CF8EE File Offset: 0x003CDAEE
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

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x0600CC0F RID: 52239 RVA: 0x003CF901 File Offset: 0x003CDB01
		// (set) Token: 0x0600CC10 RID: 52240 RVA: 0x003CF909 File Offset: 0x003CDB09
		public float OldVolume
		{
			get
			{
				return this._OldVolume;
			}
			set
			{
				this._OldVolume = value;
				this.HasOldVolume = true;
			}
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x0600CC11 RID: 52241 RVA: 0x003CF919 File Offset: 0x003CDB19
		// (set) Token: 0x0600CC12 RID: 52242 RVA: 0x003CF921 File Offset: 0x003CDB21
		public float NewVolume
		{
			get
			{
				return this._NewVolume;
			}
			set
			{
				this._NewVolume = value;
				this.HasNewVolume = true;
			}
		}

		// Token: 0x0600CC13 RID: 52243 RVA: 0x003CF934 File Offset: 0x003CDB34
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasOldVolume)
			{
				num ^= this.OldVolume.GetHashCode();
			}
			if (this.HasNewVolume)
			{
				num ^= this.NewVolume.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CC14 RID: 52244 RVA: 0x003CF9AC File Offset: 0x003CDBAC
		public override bool Equals(object obj)
		{
			MusicVolumeChanged musicVolumeChanged = obj as MusicVolumeChanged;
			return musicVolumeChanged != null && this.HasPlayer == musicVolumeChanged.HasPlayer && (!this.HasPlayer || this.Player.Equals(musicVolumeChanged.Player)) && this.HasDeviceInfo == musicVolumeChanged.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(musicVolumeChanged.DeviceInfo)) && this.HasOldVolume == musicVolumeChanged.HasOldVolume && (!this.HasOldVolume || this.OldVolume.Equals(musicVolumeChanged.OldVolume)) && this.HasNewVolume == musicVolumeChanged.HasNewVolume && (!this.HasNewVolume || this.NewVolume.Equals(musicVolumeChanged.NewVolume));
		}

		// Token: 0x0600CC15 RID: 52245 RVA: 0x003CFA78 File Offset: 0x003CDC78
		public void Deserialize(Stream stream)
		{
			MusicVolumeChanged.Deserialize(stream, this);
		}

		// Token: 0x0600CC16 RID: 52246 RVA: 0x003CFA82 File Offset: 0x003CDC82
		public static MusicVolumeChanged Deserialize(Stream stream, MusicVolumeChanged instance)
		{
			return MusicVolumeChanged.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CC17 RID: 52247 RVA: 0x003CFA90 File Offset: 0x003CDC90
		public static MusicVolumeChanged DeserializeLengthDelimited(Stream stream)
		{
			MusicVolumeChanged musicVolumeChanged = new MusicVolumeChanged();
			MusicVolumeChanged.DeserializeLengthDelimited(stream, musicVolumeChanged);
			return musicVolumeChanged;
		}

		// Token: 0x0600CC18 RID: 52248 RVA: 0x003CFAAC File Offset: 0x003CDCAC
		public static MusicVolumeChanged DeserializeLengthDelimited(Stream stream, MusicVolumeChanged instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MusicVolumeChanged.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CC19 RID: 52249 RVA: 0x003CFAD4 File Offset: 0x003CDCD4
		public static MusicVolumeChanged Deserialize(Stream stream, MusicVolumeChanged instance, long limit)
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
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
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
						if (num == 37)
						{
							instance.OldVolume = binaryReader.ReadSingle();
							continue;
						}
						if (num == 45)
						{
							instance.NewVolume = binaryReader.ReadSingle();
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

		// Token: 0x0600CC1A RID: 52250 RVA: 0x003CFBE6 File Offset: 0x003CDDE6
		public void Serialize(Stream stream)
		{
			MusicVolumeChanged.Serialize(stream, this);
		}

		// Token: 0x0600CC1B RID: 52251 RVA: 0x003CFBF0 File Offset: 0x003CDDF0
		public static void Serialize(Stream stream, MusicVolumeChanged instance)
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

		// Token: 0x0600CC1C RID: 52252 RVA: 0x003CFC98 File Offset: 0x003CDE98
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize = this.Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize2 = this.DeviceInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasOldVolume)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasNewVolume)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x0400A06F RID: 41071
		public bool HasPlayer;

		// Token: 0x0400A070 RID: 41072
		private Player _Player;

		// Token: 0x0400A071 RID: 41073
		public bool HasDeviceInfo;

		// Token: 0x0400A072 RID: 41074
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A073 RID: 41075
		public bool HasOldVolume;

		// Token: 0x0400A074 RID: 41076
		private float _OldVolume;

		// Token: 0x0400A075 RID: 41077
		public bool HasNewVolume;

		// Token: 0x0400A076 RID: 41078
		private float _NewVolume;
	}
}
