using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	// Token: 0x0200117F RID: 4479
	public class DeviceInfo : IProtoBuf
	{
		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x0600C4DF RID: 50399 RVA: 0x003B5A6E File Offset: 0x003B3C6E
		// (set) Token: 0x0600C4E0 RID: 50400 RVA: 0x003B5A76 File Offset: 0x003B3C76
		public string AndroidId
		{
			get
			{
				return this._AndroidId;
			}
			set
			{
				this._AndroidId = value;
				this.HasAndroidId = (value != null);
			}
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x0600C4E1 RID: 50401 RVA: 0x003B5A89 File Offset: 0x003B3C89
		// (set) Token: 0x0600C4E2 RID: 50402 RVA: 0x003B5A91 File Offset: 0x003B3C91
		public string AndroidModel
		{
			get
			{
				return this._AndroidModel;
			}
			set
			{
				this._AndroidModel = value;
				this.HasAndroidModel = (value != null);
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x0600C4E3 RID: 50403 RVA: 0x003B5AA4 File Offset: 0x003B3CA4
		// (set) Token: 0x0600C4E4 RID: 50404 RVA: 0x003B5AAC File Offset: 0x003B3CAC
		public uint AndroidSdkVersion
		{
			get
			{
				return this._AndroidSdkVersion;
			}
			set
			{
				this._AndroidSdkVersion = value;
				this.HasAndroidSdkVersion = true;
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x0600C4E5 RID: 50405 RVA: 0x003B5ABC File Offset: 0x003B3CBC
		// (set) Token: 0x0600C4E6 RID: 50406 RVA: 0x003B5AC4 File Offset: 0x003B3CC4
		public bool IsConnectedToWifi
		{
			get
			{
				return this._IsConnectedToWifi;
			}
			set
			{
				this._IsConnectedToWifi = value;
				this.HasIsConnectedToWifi = true;
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x0600C4E7 RID: 50407 RVA: 0x003B5AD4 File Offset: 0x003B3CD4
		// (set) Token: 0x0600C4E8 RID: 50408 RVA: 0x003B5ADC File Offset: 0x003B3CDC
		public string GpuTextureFormat
		{
			get
			{
				return this._GpuTextureFormat;
			}
			set
			{
				this._GpuTextureFormat = value;
				this.HasGpuTextureFormat = (value != null);
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x0600C4E9 RID: 50409 RVA: 0x003B5AEF File Offset: 0x003B3CEF
		// (set) Token: 0x0600C4EA RID: 50410 RVA: 0x003B5AF7 File Offset: 0x003B3CF7
		public string Locale
		{
			get
			{
				return this._Locale;
			}
			set
			{
				this._Locale = value;
				this.HasLocale = (value != null);
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x0600C4EB RID: 50411 RVA: 0x003B5B0A File Offset: 0x003B3D0A
		// (set) Token: 0x0600C4EC RID: 50412 RVA: 0x003B5B12 File Offset: 0x003B3D12
		public string BnetRegion
		{
			get
			{
				return this._BnetRegion;
			}
			set
			{
				this._BnetRegion = value;
				this.HasBnetRegion = (value != null);
			}
		}

		// Token: 0x0600C4ED RID: 50413 RVA: 0x003B5B28 File Offset: 0x003B3D28
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAndroidId)
			{
				num ^= this.AndroidId.GetHashCode();
			}
			if (this.HasAndroidModel)
			{
				num ^= this.AndroidModel.GetHashCode();
			}
			if (this.HasAndroidSdkVersion)
			{
				num ^= this.AndroidSdkVersion.GetHashCode();
			}
			if (this.HasIsConnectedToWifi)
			{
				num ^= this.IsConnectedToWifi.GetHashCode();
			}
			if (this.HasGpuTextureFormat)
			{
				num ^= this.GpuTextureFormat.GetHashCode();
			}
			if (this.HasLocale)
			{
				num ^= this.Locale.GetHashCode();
			}
			if (this.HasBnetRegion)
			{
				num ^= this.BnetRegion.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C4EE RID: 50414 RVA: 0x003B5BE4 File Offset: 0x003B3DE4
		public override bool Equals(object obj)
		{
			DeviceInfo deviceInfo = obj as DeviceInfo;
			return deviceInfo != null && this.HasAndroidId == deviceInfo.HasAndroidId && (!this.HasAndroidId || this.AndroidId.Equals(deviceInfo.AndroidId)) && this.HasAndroidModel == deviceInfo.HasAndroidModel && (!this.HasAndroidModel || this.AndroidModel.Equals(deviceInfo.AndroidModel)) && this.HasAndroidSdkVersion == deviceInfo.HasAndroidSdkVersion && (!this.HasAndroidSdkVersion || this.AndroidSdkVersion.Equals(deviceInfo.AndroidSdkVersion)) && this.HasIsConnectedToWifi == deviceInfo.HasIsConnectedToWifi && (!this.HasIsConnectedToWifi || this.IsConnectedToWifi.Equals(deviceInfo.IsConnectedToWifi)) && this.HasGpuTextureFormat == deviceInfo.HasGpuTextureFormat && (!this.HasGpuTextureFormat || this.GpuTextureFormat.Equals(deviceInfo.GpuTextureFormat)) && this.HasLocale == deviceInfo.HasLocale && (!this.HasLocale || this.Locale.Equals(deviceInfo.Locale)) && this.HasBnetRegion == deviceInfo.HasBnetRegion && (!this.HasBnetRegion || this.BnetRegion.Equals(deviceInfo.BnetRegion));
		}

		// Token: 0x0600C4EF RID: 50415 RVA: 0x003B5D31 File Offset: 0x003B3F31
		public void Deserialize(Stream stream)
		{
			DeviceInfo.Deserialize(stream, this);
		}

		// Token: 0x0600C4F0 RID: 50416 RVA: 0x003B5D3B File Offset: 0x003B3F3B
		public static DeviceInfo Deserialize(Stream stream, DeviceInfo instance)
		{
			return DeviceInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C4F1 RID: 50417 RVA: 0x003B5D48 File Offset: 0x003B3F48
		public static DeviceInfo DeserializeLengthDelimited(Stream stream)
		{
			DeviceInfo deviceInfo = new DeviceInfo();
			DeviceInfo.DeserializeLengthDelimited(stream, deviceInfo);
			return deviceInfo;
		}

		// Token: 0x0600C4F2 RID: 50418 RVA: 0x003B5D64 File Offset: 0x003B3F64
		public static DeviceInfo DeserializeLengthDelimited(Stream stream, DeviceInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeviceInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C4F3 RID: 50419 RVA: 0x003B5D8C File Offset: 0x003B3F8C
		public static DeviceInfo Deserialize(Stream stream, DeviceInfo instance, long limit)
		{
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
						if (num == 10)
						{
							instance.AndroidId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 18)
						{
							instance.AndroidModel = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 24)
						{
							instance.AndroidSdkVersion = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
					else if (num <= 42)
					{
						if (num == 32)
						{
							instance.IsConnectedToWifi = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 42)
						{
							instance.GpuTextureFormat = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 50)
						{
							instance.Locale = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 58)
						{
							instance.BnetRegion = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600C4F4 RID: 50420 RVA: 0x003B5EB2 File Offset: 0x003B40B2
		public void Serialize(Stream stream)
		{
			DeviceInfo.Serialize(stream, this);
		}

		// Token: 0x0600C4F5 RID: 50421 RVA: 0x003B5EBC File Offset: 0x003B40BC
		public static void Serialize(Stream stream, DeviceInfo instance)
		{
			if (instance.HasAndroidId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AndroidId));
			}
			if (instance.HasAndroidModel)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AndroidModel));
			}
			if (instance.HasAndroidSdkVersion)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.AndroidSdkVersion);
			}
			if (instance.HasIsConnectedToWifi)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.IsConnectedToWifi);
			}
			if (instance.HasGpuTextureFormat)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.GpuTextureFormat));
			}
			if (instance.HasLocale)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Locale));
			}
			if (instance.HasBnetRegion)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BnetRegion));
			}
		}

		// Token: 0x0600C4F6 RID: 50422 RVA: 0x003B5FC0 File Offset: 0x003B41C0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAndroidId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.AndroidId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasAndroidModel)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.AndroidModel);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasAndroidSdkVersion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.AndroidSdkVersion);
			}
			if (this.HasIsConnectedToWifi)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasGpuTextureFormat)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.GpuTextureFormat);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasLocale)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.Locale);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasBnetRegion)
			{
				num += 1U;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(this.BnetRegion);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			return num;
		}

		// Token: 0x04009D65 RID: 40293
		public bool HasAndroidId;

		// Token: 0x04009D66 RID: 40294
		private string _AndroidId;

		// Token: 0x04009D67 RID: 40295
		public bool HasAndroidModel;

		// Token: 0x04009D68 RID: 40296
		private string _AndroidModel;

		// Token: 0x04009D69 RID: 40297
		public bool HasAndroidSdkVersion;

		// Token: 0x04009D6A RID: 40298
		private uint _AndroidSdkVersion;

		// Token: 0x04009D6B RID: 40299
		public bool HasIsConnectedToWifi;

		// Token: 0x04009D6C RID: 40300
		private bool _IsConnectedToWifi;

		// Token: 0x04009D6D RID: 40301
		public bool HasGpuTextureFormat;

		// Token: 0x04009D6E RID: 40302
		private string _GpuTextureFormat;

		// Token: 0x04009D6F RID: 40303
		public bool HasLocale;

		// Token: 0x04009D70 RID: 40304
		private string _Locale;

		// Token: 0x04009D71 RID: 40305
		public bool HasBnetRegion;

		// Token: 0x04009D72 RID: 40306
		private string _BnetRegion;
	}
}
