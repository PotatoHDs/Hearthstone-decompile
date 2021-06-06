using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.CRM
{
	// Token: 0x02001177 RID: 4471
	public class PushRegistration : IProtoBuf
	{
		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x0600C43F RID: 50239 RVA: 0x003B33D8 File Offset: 0x003B15D8
		// (set) Token: 0x0600C440 RID: 50240 RVA: 0x003B33E0 File Offset: 0x003B15E0
		public string PushId
		{
			get
			{
				return this._PushId;
			}
			set
			{
				this._PushId = value;
				this.HasPushId = (value != null);
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x0600C441 RID: 50241 RVA: 0x003B33F3 File Offset: 0x003B15F3
		// (set) Token: 0x0600C442 RID: 50242 RVA: 0x003B33FB File Offset: 0x003B15FB
		public int UtcOffset
		{
			get
			{
				return this._UtcOffset;
			}
			set
			{
				this._UtcOffset = value;
				this.HasUtcOffset = true;
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x0600C443 RID: 50243 RVA: 0x003B340B File Offset: 0x003B160B
		// (set) Token: 0x0600C444 RID: 50244 RVA: 0x003B3413 File Offset: 0x003B1613
		public string Timezone
		{
			get
			{
				return this._Timezone;
			}
			set
			{
				this._Timezone = value;
				this.HasTimezone = (value != null);
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x0600C445 RID: 50245 RVA: 0x003B3426 File Offset: 0x003B1626
		// (set) Token: 0x0600C446 RID: 50246 RVA: 0x003B342E File Offset: 0x003B162E
		public string ApplicationId
		{
			get
			{
				return this._ApplicationId;
			}
			set
			{
				this._ApplicationId = value;
				this.HasApplicationId = (value != null);
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x0600C447 RID: 50247 RVA: 0x003B3441 File Offset: 0x003B1641
		// (set) Token: 0x0600C448 RID: 50248 RVA: 0x003B3449 File Offset: 0x003B1649
		public string Language
		{
			get
			{
				return this._Language;
			}
			set
			{
				this._Language = value;
				this.HasLanguage = (value != null);
			}
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x0600C449 RID: 50249 RVA: 0x003B345C File Offset: 0x003B165C
		// (set) Token: 0x0600C44A RID: 50250 RVA: 0x003B3464 File Offset: 0x003B1664
		public string Os
		{
			get
			{
				return this._Os;
			}
			set
			{
				this._Os = value;
				this.HasOs = (value != null);
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x0600C44B RID: 50251 RVA: 0x003B3477 File Offset: 0x003B1677
		// (set) Token: 0x0600C44C RID: 50252 RVA: 0x003B347F File Offset: 0x003B167F
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

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x0600C44D RID: 50253 RVA: 0x003B3492 File Offset: 0x003B1692
		// (set) Token: 0x0600C44E RID: 50254 RVA: 0x003B349A File Offset: 0x003B169A
		public string DeviceHeight
		{
			get
			{
				return this._DeviceHeight;
			}
			set
			{
				this._DeviceHeight = value;
				this.HasDeviceHeight = (value != null);
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x0600C44F RID: 50255 RVA: 0x003B34AD File Offset: 0x003B16AD
		// (set) Token: 0x0600C450 RID: 50256 RVA: 0x003B34B5 File Offset: 0x003B16B5
		public string DeviceWidth
		{
			get
			{
				return this._DeviceWidth;
			}
			set
			{
				this._DeviceWidth = value;
				this.HasDeviceWidth = (value != null);
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x0600C451 RID: 50257 RVA: 0x003B34C8 File Offset: 0x003B16C8
		// (set) Token: 0x0600C452 RID: 50258 RVA: 0x003B34D0 File Offset: 0x003B16D0
		public string DeviceDpi
		{
			get
			{
				return this._DeviceDpi;
			}
			set
			{
				this._DeviceDpi = value;
				this.HasDeviceDpi = (value != null);
			}
		}

		// Token: 0x0600C453 RID: 50259 RVA: 0x003B34E4 File Offset: 0x003B16E4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPushId)
			{
				num ^= this.PushId.GetHashCode();
			}
			if (this.HasUtcOffset)
			{
				num ^= this.UtcOffset.GetHashCode();
			}
			if (this.HasTimezone)
			{
				num ^= this.Timezone.GetHashCode();
			}
			if (this.HasApplicationId)
			{
				num ^= this.ApplicationId.GetHashCode();
			}
			if (this.HasLanguage)
			{
				num ^= this.Language.GetHashCode();
			}
			if (this.HasOs)
			{
				num ^= this.Os.GetHashCode();
			}
			if (this.HasOsVersion)
			{
				num ^= this.OsVersion.GetHashCode();
			}
			if (this.HasDeviceHeight)
			{
				num ^= this.DeviceHeight.GetHashCode();
			}
			if (this.HasDeviceWidth)
			{
				num ^= this.DeviceWidth.GetHashCode();
			}
			if (this.HasDeviceDpi)
			{
				num ^= this.DeviceDpi.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C454 RID: 50260 RVA: 0x003B35E0 File Offset: 0x003B17E0
		public override bool Equals(object obj)
		{
			PushRegistration pushRegistration = obj as PushRegistration;
			return pushRegistration != null && this.HasPushId == pushRegistration.HasPushId && (!this.HasPushId || this.PushId.Equals(pushRegistration.PushId)) && this.HasUtcOffset == pushRegistration.HasUtcOffset && (!this.HasUtcOffset || this.UtcOffset.Equals(pushRegistration.UtcOffset)) && this.HasTimezone == pushRegistration.HasTimezone && (!this.HasTimezone || this.Timezone.Equals(pushRegistration.Timezone)) && this.HasApplicationId == pushRegistration.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(pushRegistration.ApplicationId)) && this.HasLanguage == pushRegistration.HasLanguage && (!this.HasLanguage || this.Language.Equals(pushRegistration.Language)) && this.HasOs == pushRegistration.HasOs && (!this.HasOs || this.Os.Equals(pushRegistration.Os)) && this.HasOsVersion == pushRegistration.HasOsVersion && (!this.HasOsVersion || this.OsVersion.Equals(pushRegistration.OsVersion)) && this.HasDeviceHeight == pushRegistration.HasDeviceHeight && (!this.HasDeviceHeight || this.DeviceHeight.Equals(pushRegistration.DeviceHeight)) && this.HasDeviceWidth == pushRegistration.HasDeviceWidth && (!this.HasDeviceWidth || this.DeviceWidth.Equals(pushRegistration.DeviceWidth)) && this.HasDeviceDpi == pushRegistration.HasDeviceDpi && (!this.HasDeviceDpi || this.DeviceDpi.Equals(pushRegistration.DeviceDpi));
		}

		// Token: 0x0600C455 RID: 50261 RVA: 0x003B37AB File Offset: 0x003B19AB
		public void Deserialize(Stream stream)
		{
			PushRegistration.Deserialize(stream, this);
		}

		// Token: 0x0600C456 RID: 50262 RVA: 0x003B37B5 File Offset: 0x003B19B5
		public static PushRegistration Deserialize(Stream stream, PushRegistration instance)
		{
			return PushRegistration.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C457 RID: 50263 RVA: 0x003B37C0 File Offset: 0x003B19C0
		public static PushRegistration DeserializeLengthDelimited(Stream stream)
		{
			PushRegistration pushRegistration = new PushRegistration();
			PushRegistration.DeserializeLengthDelimited(stream, pushRegistration);
			return pushRegistration;
		}

		// Token: 0x0600C458 RID: 50264 RVA: 0x003B37DC File Offset: 0x003B19DC
		public static PushRegistration DeserializeLengthDelimited(Stream stream, PushRegistration instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PushRegistration.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C459 RID: 50265 RVA: 0x003B3804 File Offset: 0x003B1A04
		public static PushRegistration Deserialize(Stream stream, PushRegistration instance, long limit)
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
				else if (num == 82)
				{
					instance.PushId = ProtocolParser.ReadString(stream);
				}
				else
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field <= 50U)
					{
						if (field <= 20U)
						{
							if (field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							if (field == 20U)
							{
								if (key.WireType == Wire.Varint)
								{
									instance.UtcOffset = (int)ProtocolParser.ReadUInt64(stream);
									continue;
								}
								continue;
							}
						}
						else if (field != 30U)
						{
							if (field != 40U)
							{
								if (field == 50U)
								{
									if (key.WireType == Wire.LengthDelimited)
									{
										instance.Language = ProtocolParser.ReadString(stream);
										continue;
									}
									continue;
								}
							}
							else
							{
								if (key.WireType == Wire.LengthDelimited)
								{
									instance.ApplicationId = ProtocolParser.ReadString(stream);
									continue;
								}
								continue;
							}
						}
						else
						{
							if (key.WireType == Wire.LengthDelimited)
							{
								instance.Timezone = ProtocolParser.ReadString(stream);
								continue;
							}
							continue;
						}
					}
					else if (field <= 70U)
					{
						if (field != 60U)
						{
							if (field == 70U)
							{
								if (key.WireType == Wire.LengthDelimited)
								{
									instance.OsVersion = ProtocolParser.ReadString(stream);
									continue;
								}
								continue;
							}
						}
						else
						{
							if (key.WireType == Wire.LengthDelimited)
							{
								instance.Os = ProtocolParser.ReadString(stream);
								continue;
							}
							continue;
						}
					}
					else if (field != 80U)
					{
						if (field != 90U)
						{
							if (field == 100U)
							{
								if (key.WireType == Wire.LengthDelimited)
								{
									instance.DeviceDpi = ProtocolParser.ReadString(stream);
									continue;
								}
								continue;
							}
						}
						else
						{
							if (key.WireType == Wire.LengthDelimited)
							{
								instance.DeviceWidth = ProtocolParser.ReadString(stream);
								continue;
							}
							continue;
						}
					}
					else
					{
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.DeviceHeight = ProtocolParser.ReadString(stream);
							continue;
						}
						continue;
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

		// Token: 0x0600C45A RID: 50266 RVA: 0x003B39F7 File Offset: 0x003B1BF7
		public void Serialize(Stream stream)
		{
			PushRegistration.Serialize(stream, this);
		}

		// Token: 0x0600C45B RID: 50267 RVA: 0x003B3A00 File Offset: 0x003B1C00
		public static void Serialize(Stream stream, PushRegistration instance)
		{
			if (instance.HasPushId)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PushId));
			}
			if (instance.HasUtcOffset)
			{
				stream.WriteByte(160);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.UtcOffset));
			}
			if (instance.HasTimezone)
			{
				stream.WriteByte(242);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Timezone));
			}
			if (instance.HasApplicationId)
			{
				stream.WriteByte(194);
				stream.WriteByte(2);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ApplicationId));
			}
			if (instance.HasLanguage)
			{
				stream.WriteByte(146);
				stream.WriteByte(3);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Language));
			}
			if (instance.HasOs)
			{
				stream.WriteByte(226);
				stream.WriteByte(3);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Os));
			}
			if (instance.HasOsVersion)
			{
				stream.WriteByte(178);
				stream.WriteByte(4);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.OsVersion));
			}
			if (instance.HasDeviceHeight)
			{
				stream.WriteByte(130);
				stream.WriteByte(5);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceHeight));
			}
			if (instance.HasDeviceWidth)
			{
				stream.WriteByte(210);
				stream.WriteByte(5);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceWidth));
			}
			if (instance.HasDeviceDpi)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceDpi));
			}
		}

		// Token: 0x0600C45C RID: 50268 RVA: 0x003B3BDC File Offset: 0x003B1DDC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPushId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.PushId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasUtcOffset)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.UtcOffset));
			}
			if (this.HasTimezone)
			{
				num += 2U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Timezone);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasApplicationId)
			{
				num += 2U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.ApplicationId);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasLanguage)
			{
				num += 2U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.Language);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasOs)
			{
				num += 2U;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(this.Os);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (this.HasOsVersion)
			{
				num += 2U;
				uint byteCount6 = (uint)Encoding.UTF8.GetByteCount(this.OsVersion);
				num += ProtocolParser.SizeOfUInt32(byteCount6) + byteCount6;
			}
			if (this.HasDeviceHeight)
			{
				num += 2U;
				uint byteCount7 = (uint)Encoding.UTF8.GetByteCount(this.DeviceHeight);
				num += ProtocolParser.SizeOfUInt32(byteCount7) + byteCount7;
			}
			if (this.HasDeviceWidth)
			{
				num += 2U;
				uint byteCount8 = (uint)Encoding.UTF8.GetByteCount(this.DeviceWidth);
				num += ProtocolParser.SizeOfUInt32(byteCount8) + byteCount8;
			}
			if (this.HasDeviceDpi)
			{
				num += 2U;
				uint byteCount9 = (uint)Encoding.UTF8.GetByteCount(this.DeviceDpi);
				num += ProtocolParser.SizeOfUInt32(byteCount9) + byteCount9;
			}
			return num;
		}

		// Token: 0x04009D1D RID: 40221
		public bool HasPushId;

		// Token: 0x04009D1E RID: 40222
		private string _PushId;

		// Token: 0x04009D1F RID: 40223
		public bool HasUtcOffset;

		// Token: 0x04009D20 RID: 40224
		private int _UtcOffset;

		// Token: 0x04009D21 RID: 40225
		public bool HasTimezone;

		// Token: 0x04009D22 RID: 40226
		private string _Timezone;

		// Token: 0x04009D23 RID: 40227
		public bool HasApplicationId;

		// Token: 0x04009D24 RID: 40228
		private string _ApplicationId;

		// Token: 0x04009D25 RID: 40229
		public bool HasLanguage;

		// Token: 0x04009D26 RID: 40230
		private string _Language;

		// Token: 0x04009D27 RID: 40231
		public bool HasOs;

		// Token: 0x04009D28 RID: 40232
		private string _Os;

		// Token: 0x04009D29 RID: 40233
		public bool HasOsVersion;

		// Token: 0x04009D2A RID: 40234
		private string _OsVersion;

		// Token: 0x04009D2B RID: 40235
		public bool HasDeviceHeight;

		// Token: 0x04009D2C RID: 40236
		private string _DeviceHeight;

		// Token: 0x04009D2D RID: 40237
		public bool HasDeviceWidth;

		// Token: 0x04009D2E RID: 40238
		private string _DeviceWidth;

		// Token: 0x04009D2F RID: 40239
		public bool HasDeviceDpi;

		// Token: 0x04009D30 RID: 40240
		private string _DeviceDpi;
	}
}
