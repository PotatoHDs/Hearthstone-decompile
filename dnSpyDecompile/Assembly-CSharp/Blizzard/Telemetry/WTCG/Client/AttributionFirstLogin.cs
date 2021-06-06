using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001194 RID: 4500
	public class AttributionFirstLogin : IProtoBuf
	{
		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x0600C65A RID: 50778 RVA: 0x003BA91D File Offset: 0x003B8B1D
		// (set) Token: 0x0600C65B RID: 50779 RVA: 0x003BA925 File Offset: 0x003B8B25
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

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x0600C65C RID: 50780 RVA: 0x003BA938 File Offset: 0x003B8B38
		// (set) Token: 0x0600C65D RID: 50781 RVA: 0x003BA940 File Offset: 0x003B8B40
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

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x0600C65E RID: 50782 RVA: 0x003BA953 File Offset: 0x003B8B53
		// (set) Token: 0x0600C65F RID: 50783 RVA: 0x003BA95B File Offset: 0x003B8B5B
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

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x0600C660 RID: 50784 RVA: 0x003BA96B File Offset: 0x003B8B6B
		// (set) Token: 0x0600C661 RID: 50785 RVA: 0x003BA973 File Offset: 0x003B8B73
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

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x0600C662 RID: 50786 RVA: 0x003BA986 File Offset: 0x003B8B86
		// (set) Token: 0x0600C663 RID: 50787 RVA: 0x003BA98E File Offset: 0x003B8B8E
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

		// Token: 0x0600C664 RID: 50788 RVA: 0x003BA9A4 File Offset: 0x003B8BA4
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
			if (this.HasIdentifier)
			{
				num ^= this.Identifier.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C665 RID: 50789 RVA: 0x003BAA30 File Offset: 0x003B8C30
		public override bool Equals(object obj)
		{
			AttributionFirstLogin attributionFirstLogin = obj as AttributionFirstLogin;
			return attributionFirstLogin != null && this.HasApplicationId == attributionFirstLogin.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(attributionFirstLogin.ApplicationId)) && this.HasDeviceType == attributionFirstLogin.HasDeviceType && (!this.HasDeviceType || this.DeviceType.Equals(attributionFirstLogin.DeviceType)) && this.HasFirstInstallDate == attributionFirstLogin.HasFirstInstallDate && (!this.HasFirstInstallDate || this.FirstInstallDate.Equals(attributionFirstLogin.FirstInstallDate)) && this.HasBundleId == attributionFirstLogin.HasBundleId && (!this.HasBundleId || this.BundleId.Equals(attributionFirstLogin.BundleId)) && this.HasIdentifier == attributionFirstLogin.HasIdentifier && (!this.HasIdentifier || this.Identifier.Equals(attributionFirstLogin.Identifier));
		}

		// Token: 0x0600C666 RID: 50790 RVA: 0x003BAB24 File Offset: 0x003B8D24
		public void Deserialize(Stream stream)
		{
			AttributionFirstLogin.Deserialize(stream, this);
		}

		// Token: 0x0600C667 RID: 50791 RVA: 0x003BAB2E File Offset: 0x003B8D2E
		public static AttributionFirstLogin Deserialize(Stream stream, AttributionFirstLogin instance)
		{
			return AttributionFirstLogin.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C668 RID: 50792 RVA: 0x003BAB3C File Offset: 0x003B8D3C
		public static AttributionFirstLogin DeserializeLengthDelimited(Stream stream)
		{
			AttributionFirstLogin attributionFirstLogin = new AttributionFirstLogin();
			AttributionFirstLogin.DeserializeLengthDelimited(stream, attributionFirstLogin);
			return attributionFirstLogin;
		}

		// Token: 0x0600C669 RID: 50793 RVA: 0x003BAB58 File Offset: 0x003B8D58
		public static AttributionFirstLogin DeserializeLengthDelimited(Stream stream, AttributionFirstLogin instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributionFirstLogin.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C66A RID: 50794 RVA: 0x003BAB80 File Offset: 0x003B8D80
		public static AttributionFirstLogin Deserialize(Stream stream, AttributionFirstLogin instance, long limit)
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
						}
						break;
					case 101U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.DeviceType = ProtocolParser.ReadString(stream);
						}
						break;
					case 102U:
						if (key.WireType == Wire.Varint)
						{
							instance.FirstInstallDate = ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 103U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.BundleId = ProtocolParser.ReadString(stream);
						}
						break;
					default:
						if (field != 1000U)
						{
							ProtocolParser.SkipKey(stream, key);
						}
						else if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.Identifier == null)
							{
								instance.Identifier = IdentifierInfo.DeserializeLengthDelimited(stream);
							}
							else
							{
								IdentifierInfo.DeserializeLengthDelimited(stream, instance.Identifier);
							}
						}
						break;
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C66B RID: 50795 RVA: 0x003BACC9 File Offset: 0x003B8EC9
		public void Serialize(Stream stream)
		{
			AttributionFirstLogin.Serialize(stream, this);
		}

		// Token: 0x0600C66C RID: 50796 RVA: 0x003BACD4 File Offset: 0x003B8ED4
		public static void Serialize(Stream stream, AttributionFirstLogin instance)
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
			if (instance.HasIdentifier)
			{
				stream.WriteByte(194);
				stream.WriteByte(62);
				ProtocolParser.WriteUInt32(stream, instance.Identifier.GetSerializedSize());
				IdentifierInfo.Serialize(stream, instance.Identifier);
			}
		}

		// Token: 0x0600C66D RID: 50797 RVA: 0x003BADD0 File Offset: 0x003B8FD0
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
			if (this.HasIdentifier)
			{
				num += 2U;
				uint serializedSize = this.Identifier.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04009DF9 RID: 40441
		public bool HasApplicationId;

		// Token: 0x04009DFA RID: 40442
		private string _ApplicationId;

		// Token: 0x04009DFB RID: 40443
		public bool HasDeviceType;

		// Token: 0x04009DFC RID: 40444
		private string _DeviceType;

		// Token: 0x04009DFD RID: 40445
		public bool HasFirstInstallDate;

		// Token: 0x04009DFE RID: 40446
		private ulong _FirstInstallDate;

		// Token: 0x04009DFF RID: 40447
		public bool HasBundleId;

		// Token: 0x04009E00 RID: 40448
		private string _BundleId;

		// Token: 0x04009E01 RID: 40449
		public bool HasIdentifier;

		// Token: 0x04009E02 RID: 40450
		private IdentifierInfo _Identifier;
	}
}
