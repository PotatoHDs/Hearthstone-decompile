using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011B8 RID: 4536
	public class DeviceInfo : IProtoBuf
	{
		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x0600C914 RID: 51476 RVA: 0x003C4C49 File Offset: 0x003C2E49
		// (set) Token: 0x0600C915 RID: 51477 RVA: 0x003C4C51 File Offset: 0x003C2E51
		public DeviceInfo.OSCategory Os
		{
			get
			{
				return this._Os;
			}
			set
			{
				this._Os = value;
				this.HasOs = true;
			}
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x0600C916 RID: 51478 RVA: 0x003C4C61 File Offset: 0x003C2E61
		// (set) Token: 0x0600C917 RID: 51479 RVA: 0x003C4C69 File Offset: 0x003C2E69
		public string OsVersion
		{
			get
			{
				return this._OsVersion;
			}
			set
			{
				this._OsVersion = value;
				this.HasOsVersion = (value != null);
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x0600C918 RID: 51480 RVA: 0x003C4C7C File Offset: 0x003C2E7C
		// (set) Token: 0x0600C919 RID: 51481 RVA: 0x003C4C84 File Offset: 0x003C2E84
		public string Model
		{
			get
			{
				return this._Model;
			}
			set
			{
				this._Model = value;
				this.HasModel = (value != null);
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x0600C91A RID: 51482 RVA: 0x003C4C97 File Offset: 0x003C2E97
		// (set) Token: 0x0600C91B RID: 51483 RVA: 0x003C4C9F File Offset: 0x003C2E9F
		public DeviceInfo.ScreenCategory Screen
		{
			get
			{
				return this._Screen;
			}
			set
			{
				this._Screen = value;
				this.HasScreen = true;
			}
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x0600C91C RID: 51484 RVA: 0x003C4CAF File Offset: 0x003C2EAF
		// (set) Token: 0x0600C91D RID: 51485 RVA: 0x003C4CB7 File Offset: 0x003C2EB7
		public DeviceInfo.ConnectionType ConnectionType_
		{
			get
			{
				return this._ConnectionType_;
			}
			set
			{
				this._ConnectionType_ = value;
				this.HasConnectionType_ = true;
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x0600C91E RID: 51486 RVA: 0x003C4CC7 File Offset: 0x003C2EC7
		// (set) Token: 0x0600C91F RID: 51487 RVA: 0x003C4CCF File Offset: 0x003C2ECF
		public string DroidTextureCompression
		{
			get
			{
				return this._DroidTextureCompression;
			}
			set
			{
				this._DroidTextureCompression = value;
				this.HasDroidTextureCompression = (value != null);
			}
		}

		// Token: 0x0600C920 RID: 51488 RVA: 0x003C4CE4 File Offset: 0x003C2EE4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasOs)
			{
				num ^= this.Os.GetHashCode();
			}
			if (this.HasOsVersion)
			{
				num ^= this.OsVersion.GetHashCode();
			}
			if (this.HasModel)
			{
				num ^= this.Model.GetHashCode();
			}
			if (this.HasScreen)
			{
				num ^= this.Screen.GetHashCode();
			}
			if (this.HasConnectionType_)
			{
				num ^= this.ConnectionType_.GetHashCode();
			}
			if (this.HasDroidTextureCompression)
			{
				num ^= this.DroidTextureCompression.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C921 RID: 51489 RVA: 0x003C4DA0 File Offset: 0x003C2FA0
		public override bool Equals(object obj)
		{
			DeviceInfo deviceInfo = obj as DeviceInfo;
			return deviceInfo != null && this.HasOs == deviceInfo.HasOs && (!this.HasOs || this.Os.Equals(deviceInfo.Os)) && this.HasOsVersion == deviceInfo.HasOsVersion && (!this.HasOsVersion || this.OsVersion.Equals(deviceInfo.OsVersion)) && this.HasModel == deviceInfo.HasModel && (!this.HasModel || this.Model.Equals(deviceInfo.Model)) && this.HasScreen == deviceInfo.HasScreen && (!this.HasScreen || this.Screen.Equals(deviceInfo.Screen)) && this.HasConnectionType_ == deviceInfo.HasConnectionType_ && (!this.HasConnectionType_ || this.ConnectionType_.Equals(deviceInfo.ConnectionType_)) && this.HasDroidTextureCompression == deviceInfo.HasDroidTextureCompression && (!this.HasDroidTextureCompression || this.DroidTextureCompression.Equals(deviceInfo.DroidTextureCompression));
		}

		// Token: 0x0600C922 RID: 51490 RVA: 0x003C4EE6 File Offset: 0x003C30E6
		public void Deserialize(Stream stream)
		{
			DeviceInfo.Deserialize(stream, this);
		}

		// Token: 0x0600C923 RID: 51491 RVA: 0x003C4EF0 File Offset: 0x003C30F0
		public static DeviceInfo Deserialize(Stream stream, DeviceInfo instance)
		{
			return DeviceInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C924 RID: 51492 RVA: 0x003C4EFC File Offset: 0x003C30FC
		public static DeviceInfo DeserializeLengthDelimited(Stream stream)
		{
			DeviceInfo deviceInfo = new DeviceInfo();
			DeviceInfo.DeserializeLengthDelimited(stream, deviceInfo);
			return deviceInfo;
		}

		// Token: 0x0600C925 RID: 51493 RVA: 0x003C4F18 File Offset: 0x003C3118
		public static DeviceInfo DeserializeLengthDelimited(Stream stream, DeviceInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeviceInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C926 RID: 51494 RVA: 0x003C4F40 File Offset: 0x003C3140
		public static DeviceInfo Deserialize(Stream stream, DeviceInfo instance, long limit)
		{
			instance.Os = DeviceInfo.OSCategory.WINDOWS;
			instance.Screen = DeviceInfo.ScreenCategory.PHONE;
			instance.ConnectionType_ = DeviceInfo.ConnectionType.WIRED;
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
					if (num <= 26)
					{
						if (num == 8)
						{
							instance.Os = (DeviceInfo.OSCategory)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 18)
						{
							instance.OsVersion = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 26)
						{
							instance.Model = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.Screen = (DeviceInfo.ScreenCategory)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.ConnectionType_ = (DeviceInfo.ConnectionType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 50)
						{
							instance.DroidTextureCompression = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600C927 RID: 51495 RVA: 0x003C5057 File Offset: 0x003C3257
		public void Serialize(Stream stream)
		{
			DeviceInfo.Serialize(stream, this);
		}

		// Token: 0x0600C928 RID: 51496 RVA: 0x003C5060 File Offset: 0x003C3260
		public static void Serialize(Stream stream, DeviceInfo instance)
		{
			if (instance.HasOs)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Os));
			}
			if (instance.HasOsVersion)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.OsVersion));
			}
			if (instance.HasModel)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Model));
			}
			if (instance.HasScreen)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Screen));
			}
			if (instance.HasConnectionType_)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ConnectionType_));
			}
			if (instance.HasDroidTextureCompression)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DroidTextureCompression));
			}
		}

		// Token: 0x0600C929 RID: 51497 RVA: 0x003C5138 File Offset: 0x003C3338
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasOs)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Os));
			}
			if (this.HasOsVersion)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.OsVersion);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasModel)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Model);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasScreen)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Screen));
			}
			if (this.HasConnectionType_)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ConnectionType_));
			}
			if (this.HasDroidTextureCompression)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.DroidTextureCompression);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}

		// Token: 0x04009F25 RID: 40741
		public bool HasOs;

		// Token: 0x04009F26 RID: 40742
		private DeviceInfo.OSCategory _Os;

		// Token: 0x04009F27 RID: 40743
		public bool HasOsVersion;

		// Token: 0x04009F28 RID: 40744
		private string _OsVersion;

		// Token: 0x04009F29 RID: 40745
		public bool HasModel;

		// Token: 0x04009F2A RID: 40746
		private string _Model;

		// Token: 0x04009F2B RID: 40747
		public bool HasScreen;

		// Token: 0x04009F2C RID: 40748
		private DeviceInfo.ScreenCategory _Screen;

		// Token: 0x04009F2D RID: 40749
		public bool HasConnectionType_;

		// Token: 0x04009F2E RID: 40750
		private DeviceInfo.ConnectionType _ConnectionType_;

		// Token: 0x04009F2F RID: 40751
		public bool HasDroidTextureCompression;

		// Token: 0x04009F30 RID: 40752
		private string _DroidTextureCompression;

		// Token: 0x0200293F RID: 10559
		public enum OSCategory
		{
			// Token: 0x0400FC4A RID: 64586
			WINDOWS = 1,
			// Token: 0x0400FC4B RID: 64587
			MAC,
			// Token: 0x0400FC4C RID: 64588
			IOS,
			// Token: 0x0400FC4D RID: 64589
			ANDROID
		}

		// Token: 0x02002940 RID: 10560
		public enum ConnectionType
		{
			// Token: 0x0400FC4F RID: 64591
			WIRED = 1,
			// Token: 0x0400FC50 RID: 64592
			WIFI,
			// Token: 0x0400FC51 RID: 64593
			CELLULAR,
			// Token: 0x0400FC52 RID: 64594
			UNKNOWN
		}

		// Token: 0x02002941 RID: 10561
		public enum ScreenCategory
		{
			// Token: 0x0400FC54 RID: 64596
			PHONE = 1,
			// Token: 0x0400FC55 RID: 64597
			MINI_TABLET,
			// Token: 0x0400FC56 RID: 64598
			TABLET,
			// Token: 0x0400FC57 RID: 64599
			PC
		}
	}
}
