using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011F8 RID: 4600
	public class StartupAudioSettings : IProtoBuf
	{
		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x0600CDE0 RID: 52704 RVA: 0x003D6192 File Offset: 0x003D4392
		// (set) Token: 0x0600CDE1 RID: 52705 RVA: 0x003D619A File Offset: 0x003D439A
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

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x0600CDE2 RID: 52706 RVA: 0x003D61AD File Offset: 0x003D43AD
		// (set) Token: 0x0600CDE3 RID: 52707 RVA: 0x003D61B5 File Offset: 0x003D43B5
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

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x0600CDE4 RID: 52708 RVA: 0x003D61C5 File Offset: 0x003D43C5
		// (set) Token: 0x0600CDE5 RID: 52709 RVA: 0x003D61CD File Offset: 0x003D43CD
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

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x0600CDE6 RID: 52710 RVA: 0x003D61DD File Offset: 0x003D43DD
		// (set) Token: 0x0600CDE7 RID: 52711 RVA: 0x003D61E5 File Offset: 0x003D43E5
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

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x0600CDE8 RID: 52712 RVA: 0x003D61F5 File Offset: 0x003D43F5
		// (set) Token: 0x0600CDE9 RID: 52713 RVA: 0x003D61FD File Offset: 0x003D43FD
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

		// Token: 0x0600CDEA RID: 52714 RVA: 0x003D6210 File Offset: 0x003D4410
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
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

		// Token: 0x0600CDEB RID: 52715 RVA: 0x003D62A4 File Offset: 0x003D44A4
		public override bool Equals(object obj)
		{
			StartupAudioSettings startupAudioSettings = obj as StartupAudioSettings;
			return startupAudioSettings != null && this.HasDeviceInfo == startupAudioSettings.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(startupAudioSettings.DeviceInfo)) && this.HasDeviceMuted == startupAudioSettings.HasDeviceMuted && (!this.HasDeviceMuted || this.DeviceMuted.Equals(startupAudioSettings.DeviceMuted)) && this.HasDeviceVolume == startupAudioSettings.HasDeviceVolume && (!this.HasDeviceVolume || this.DeviceVolume.Equals(startupAudioSettings.DeviceVolume)) && this.HasMasterVolume == startupAudioSettings.HasMasterVolume && (!this.HasMasterVolume || this.MasterVolume.Equals(startupAudioSettings.MasterVolume)) && this.HasMusicVolume == startupAudioSettings.HasMusicVolume && (!this.HasMusicVolume || this.MusicVolume.Equals(startupAudioSettings.MusicVolume));
		}

		// Token: 0x0600CDEC RID: 52716 RVA: 0x003D63A1 File Offset: 0x003D45A1
		public void Deserialize(Stream stream)
		{
			StartupAudioSettings.Deserialize(stream, this);
		}

		// Token: 0x0600CDED RID: 52717 RVA: 0x003D63AB File Offset: 0x003D45AB
		public static StartupAudioSettings Deserialize(Stream stream, StartupAudioSettings instance)
		{
			return StartupAudioSettings.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CDEE RID: 52718 RVA: 0x003D63B8 File Offset: 0x003D45B8
		public static StartupAudioSettings DeserializeLengthDelimited(Stream stream)
		{
			StartupAudioSettings startupAudioSettings = new StartupAudioSettings();
			StartupAudioSettings.DeserializeLengthDelimited(stream, startupAudioSettings);
			return startupAudioSettings;
		}

		// Token: 0x0600CDEF RID: 52719 RVA: 0x003D63D4 File Offset: 0x003D45D4
		public static StartupAudioSettings DeserializeLengthDelimited(Stream stream, StartupAudioSettings instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return StartupAudioSettings.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CDF0 RID: 52720 RVA: 0x003D63FC File Offset: 0x003D45FC
		public static StartupAudioSettings Deserialize(Stream stream, StartupAudioSettings instance, long limit)
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
					if (num <= 16)
					{
						if (num != 10)
						{
							if (num == 16)
							{
								instance.DeviceMuted = ProtocolParser.ReadBool(stream);
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
						if (num == 29)
						{
							instance.DeviceVolume = binaryReader.ReadSingle();
							continue;
						}
						if (num == 37)
						{
							instance.MasterVolume = binaryReader.ReadSingle();
							continue;
						}
						if (num == 45)
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

		// Token: 0x0600CDF1 RID: 52721 RVA: 0x003D6507 File Offset: 0x003D4707
		public void Serialize(Stream stream)
		{
			StartupAudioSettings.Serialize(stream, this);
		}

		// Token: 0x0600CDF2 RID: 52722 RVA: 0x003D6510 File Offset: 0x003D4710
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

		// Token: 0x0600CDF3 RID: 52723 RVA: 0x003D65C4 File Offset: 0x003D47C4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
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

		// Token: 0x0400A12F RID: 41263
		public bool HasDeviceInfo;

		// Token: 0x0400A130 RID: 41264
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A131 RID: 41265
		public bool HasDeviceMuted;

		// Token: 0x0400A132 RID: 41266
		private bool _DeviceMuted;

		// Token: 0x0400A133 RID: 41267
		public bool HasDeviceVolume;

		// Token: 0x0400A134 RID: 41268
		private float _DeviceVolume;

		// Token: 0x0400A135 RID: 41269
		public bool HasMasterVolume;

		// Token: 0x0400A136 RID: 41270
		private float _MasterVolume;

		// Token: 0x0400A137 RID: 41271
		public bool HasMusicVolume;

		// Token: 0x0400A138 RID: 41272
		private float _MusicVolume;
	}
}
