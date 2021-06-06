using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x0200120A RID: 4618
	public class AttributionInstall : IProtoBuf
	{
		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x0600CF3C RID: 53052 RVA: 0x003DB2FA File Offset: 0x003D94FA
		// (set) Token: 0x0600CF3D RID: 53053 RVA: 0x003DB302 File Offset: 0x003D9502
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

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x0600CF3E RID: 53054 RVA: 0x003DB315 File Offset: 0x003D9515
		// (set) Token: 0x0600CF3F RID: 53055 RVA: 0x003DB31D File Offset: 0x003D951D
		public string DeviceType
		{
			get
			{
				return this._DeviceType;
			}
			set
			{
				this._DeviceType = value;
				this.HasDeviceType = (value != null);
			}
		}

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x0600CF40 RID: 53056 RVA: 0x003DB330 File Offset: 0x003D9530
		// (set) Token: 0x0600CF41 RID: 53057 RVA: 0x003DB338 File Offset: 0x003D9538
		public ulong FirstInstallDate
		{
			get
			{
				return this._FirstInstallDate;
			}
			set
			{
				this._FirstInstallDate = value;
				this.HasFirstInstallDate = true;
			}
		}

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x0600CF42 RID: 53058 RVA: 0x003DB348 File Offset: 0x003D9548
		// (set) Token: 0x0600CF43 RID: 53059 RVA: 0x003DB350 File Offset: 0x003D9550
		public string BundleId
		{
			get
			{
				return this._BundleId;
			}
			set
			{
				this._BundleId = value;
				this.HasBundleId = (value != null);
			}
		}

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x0600CF44 RID: 53060 RVA: 0x003DB363 File Offset: 0x003D9563
		// (set) Token: 0x0600CF45 RID: 53061 RVA: 0x003DB36B File Offset: 0x003D956B
		public string Referrer
		{
			get
			{
				return this._Referrer;
			}
			set
			{
				this._Referrer = value;
				this.HasReferrer = (value != null);
			}
		}

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x0600CF46 RID: 53062 RVA: 0x003DB37E File Offset: 0x003D957E
		// (set) Token: 0x0600CF47 RID: 53063 RVA: 0x003DB386 File Offset: 0x003D9586
		public string AppleSearchAdsJson
		{
			get
			{
				return this._AppleSearchAdsJson;
			}
			set
			{
				this._AppleSearchAdsJson = value;
				this.HasAppleSearchAdsJson = (value != null);
			}
		}

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x0600CF48 RID: 53064 RVA: 0x003DB399 File Offset: 0x003D9599
		// (set) Token: 0x0600CF49 RID: 53065 RVA: 0x003DB3A1 File Offset: 0x003D95A1
		public int AppleSearchAdsErrorCode
		{
			get
			{
				return this._AppleSearchAdsErrorCode;
			}
			set
			{
				this._AppleSearchAdsErrorCode = value;
				this.HasAppleSearchAdsErrorCode = true;
			}
		}

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x0600CF4A RID: 53066 RVA: 0x003DB3B1 File Offset: 0x003D95B1
		// (set) Token: 0x0600CF4B RID: 53067 RVA: 0x003DB3B9 File Offset: 0x003D95B9
		public IdentifierInfo Identifier
		{
			get
			{
				return this._Identifier;
			}
			set
			{
				this._Identifier = value;
				this.HasIdentifier = (value != null);
			}
		}

		// Token: 0x0600CF4C RID: 53068 RVA: 0x003DB3CC File Offset: 0x003D95CC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasApplicationId)
			{
				num ^= this.ApplicationId.GetHashCode();
			}
			if (this.HasDeviceType)
			{
				num ^= this.DeviceType.GetHashCode();
			}
			if (this.HasFirstInstallDate)
			{
				num ^= this.FirstInstallDate.GetHashCode();
			}
			if (this.HasBundleId)
			{
				num ^= this.BundleId.GetHashCode();
			}
			if (this.HasReferrer)
			{
				num ^= this.Referrer.GetHashCode();
			}
			if (this.HasAppleSearchAdsJson)
			{
				num ^= this.AppleSearchAdsJson.GetHashCode();
			}
			if (this.HasAppleSearchAdsErrorCode)
			{
				num ^= this.AppleSearchAdsErrorCode.GetHashCode();
			}
			if (this.HasIdentifier)
			{
				num ^= this.Identifier.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CF4D RID: 53069 RVA: 0x003DB49C File Offset: 0x003D969C
		public override bool Equals(object obj)
		{
			AttributionInstall attributionInstall = obj as AttributionInstall;
			return attributionInstall != null && this.HasApplicationId == attributionInstall.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(attributionInstall.ApplicationId)) && this.HasDeviceType == attributionInstall.HasDeviceType && (!this.HasDeviceType || this.DeviceType.Equals(attributionInstall.DeviceType)) && this.HasFirstInstallDate == attributionInstall.HasFirstInstallDate && (!this.HasFirstInstallDate || this.FirstInstallDate.Equals(attributionInstall.FirstInstallDate)) && this.HasBundleId == attributionInstall.HasBundleId && (!this.HasBundleId || this.BundleId.Equals(attributionInstall.BundleId)) && this.HasReferrer == attributionInstall.HasReferrer && (!this.HasReferrer || this.Referrer.Equals(attributionInstall.Referrer)) && this.HasAppleSearchAdsJson == attributionInstall.HasAppleSearchAdsJson && (!this.HasAppleSearchAdsJson || this.AppleSearchAdsJson.Equals(attributionInstall.AppleSearchAdsJson)) && this.HasAppleSearchAdsErrorCode == attributionInstall.HasAppleSearchAdsErrorCode && (!this.HasAppleSearchAdsErrorCode || this.AppleSearchAdsErrorCode.Equals(attributionInstall.AppleSearchAdsErrorCode)) && this.HasIdentifier == attributionInstall.HasIdentifier && (!this.HasIdentifier || this.Identifier.Equals(attributionInstall.Identifier));
		}

		// Token: 0x0600CF4E RID: 53070 RVA: 0x003DB614 File Offset: 0x003D9814
		public void Deserialize(Stream stream)
		{
			AttributionInstall.Deserialize(stream, this);
		}

		// Token: 0x0600CF4F RID: 53071 RVA: 0x003DB61E File Offset: 0x003D981E
		public static AttributionInstall Deserialize(Stream stream, AttributionInstall instance)
		{
			return AttributionInstall.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CF50 RID: 53072 RVA: 0x003DB62C File Offset: 0x003D982C
		public static AttributionInstall DeserializeLengthDelimited(Stream stream)
		{
			AttributionInstall attributionInstall = new AttributionInstall();
			AttributionInstall.DeserializeLengthDelimited(stream, attributionInstall);
			return attributionInstall;
		}

		// Token: 0x0600CF51 RID: 53073 RVA: 0x003DB648 File Offset: 0x003D9848
		public static AttributionInstall DeserializeLengthDelimited(Stream stream, AttributionInstall instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributionInstall.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CF52 RID: 53074 RVA: 0x003DB670 File Offset: 0x003D9870
		public static AttributionInstall Deserialize(Stream stream, AttributionInstall instance, long limit)
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
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field <= 200U)
					{
						if (field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						switch (field)
						{
						case 100U:
							if (key.WireType == Wire.LengthDelimited)
							{
								instance.ApplicationId = ProtocolParser.ReadString(stream);
								continue;
							}
							continue;
						case 101U:
							if (key.WireType == Wire.LengthDelimited)
							{
								instance.DeviceType = ProtocolParser.ReadString(stream);
								continue;
							}
							continue;
						case 102U:
							if (key.WireType == Wire.Varint)
							{
								instance.FirstInstallDate = ProtocolParser.ReadUInt64(stream);
								continue;
							}
							continue;
						case 103U:
							if (key.WireType == Wire.LengthDelimited)
							{
								instance.BundleId = ProtocolParser.ReadString(stream);
								continue;
							}
							continue;
						default:
							if (field == 200U)
							{
								if (key.WireType == Wire.LengthDelimited)
								{
									instance.Referrer = ProtocolParser.ReadString(stream);
									continue;
								}
								continue;
							}
							break;
						}
					}
					else if (field != 300U)
					{
						if (field != 301U)
						{
							if (field == 1000U)
							{
								if (key.WireType != Wire.LengthDelimited)
								{
									continue;
								}
								if (instance.Identifier == null)
								{
									instance.Identifier = IdentifierInfo.DeserializeLengthDelimited(stream);
									continue;
								}
								IdentifierInfo.DeserializeLengthDelimited(stream, instance.Identifier);
								continue;
							}
						}
						else
						{
							if (key.WireType == Wire.Varint)
							{
								instance.AppleSearchAdsErrorCode = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							continue;
						}
					}
					else
					{
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.AppleSearchAdsJson = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600CF53 RID: 53075 RVA: 0x003DB83E File Offset: 0x003D9A3E
		public void Serialize(Stream stream)
		{
			AttributionInstall.Serialize(stream, this);
		}

		// Token: 0x0600CF54 RID: 53076 RVA: 0x003DB848 File Offset: 0x003D9A48
		public static void Serialize(Stream stream, AttributionInstall instance)
		{
			if (instance.HasApplicationId)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ApplicationId));
			}
			if (instance.HasDeviceType)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceType));
			}
			if (instance.HasFirstInstallDate)
			{
				stream.WriteByte(176);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt64(stream, instance.FirstInstallDate);
			}
			if (instance.HasBundleId)
			{
				stream.WriteByte(186);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BundleId));
			}
			if (instance.HasReferrer)
			{
				stream.WriteByte(194);
				stream.WriteByte(12);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Referrer));
			}
			if (instance.HasAppleSearchAdsJson)
			{
				stream.WriteByte(226);
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AppleSearchAdsJson));
			}
			if (instance.HasAppleSearchAdsErrorCode)
			{
				stream.WriteByte(232);
				stream.WriteByte(18);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AppleSearchAdsErrorCode));
			}
			if (instance.HasIdentifier)
			{
				stream.WriteByte(194);
				stream.WriteByte(62);
				ProtocolParser.WriteUInt32(stream, instance.Identifier.GetSerializedSize());
				IdentifierInfo.Serialize(stream, instance.Identifier);
			}
		}

		// Token: 0x0600CF55 RID: 53077 RVA: 0x003DB9D0 File Offset: 0x003D9BD0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasApplicationId)
			{
				num += 2U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ApplicationId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasDeviceType)
			{
				num += 2U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.DeviceType);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasFirstInstallDate)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64(this.FirstInstallDate);
			}
			if (this.HasBundleId)
			{
				num += 2U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.BundleId);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasReferrer)
			{
				num += 2U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.Referrer);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasAppleSearchAdsJson)
			{
				num += 2U;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(this.AppleSearchAdsJson);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (this.HasAppleSearchAdsErrorCode)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.AppleSearchAdsErrorCode));
			}
			if (this.HasIdentifier)
			{
				num += 2U;
				uint serializedSize = this.Identifier.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x0400A1C5 RID: 41413
		public bool HasApplicationId;

		// Token: 0x0400A1C6 RID: 41414
		private string _ApplicationId;

		// Token: 0x0400A1C7 RID: 41415
		public bool HasDeviceType;

		// Token: 0x0400A1C8 RID: 41416
		private string _DeviceType;

		// Token: 0x0400A1C9 RID: 41417
		public bool HasFirstInstallDate;

		// Token: 0x0400A1CA RID: 41418
		private ulong _FirstInstallDate;

		// Token: 0x0400A1CB RID: 41419
		public bool HasBundleId;

		// Token: 0x0400A1CC RID: 41420
		private string _BundleId;

		// Token: 0x0400A1CD RID: 41421
		public bool HasReferrer;

		// Token: 0x0400A1CE RID: 41422
		private string _Referrer;

		// Token: 0x0400A1CF RID: 41423
		public bool HasAppleSearchAdsJson;

		// Token: 0x0400A1D0 RID: 41424
		private string _AppleSearchAdsJson;

		// Token: 0x0400A1D1 RID: 41425
		public bool HasAppleSearchAdsErrorCode;

		// Token: 0x0400A1D2 RID: 41426
		private int _AppleSearchAdsErrorCode;

		// Token: 0x0400A1D3 RID: 41427
		public bool HasIdentifier;

		// Token: 0x0400A1D4 RID: 41428
		private IdentifierInfo _Identifier;
	}
}
