using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x0200119C RID: 4508
	public class AttributionRegistration : IProtoBuf
	{
		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x0600C716 RID: 50966 RVA: 0x003BDC47 File Offset: 0x003BBE47
		// (set) Token: 0x0600C717 RID: 50967 RVA: 0x003BDC4F File Offset: 0x003BBE4F
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

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x0600C718 RID: 50968 RVA: 0x003BDC62 File Offset: 0x003BBE62
		// (set) Token: 0x0600C719 RID: 50969 RVA: 0x003BDC6A File Offset: 0x003BBE6A
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

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x0600C71A RID: 50970 RVA: 0x003BDC7D File Offset: 0x003BBE7D
		// (set) Token: 0x0600C71B RID: 50971 RVA: 0x003BDC85 File Offset: 0x003BBE85
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

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x0600C71C RID: 50972 RVA: 0x003BDC95 File Offset: 0x003BBE95
		// (set) Token: 0x0600C71D RID: 50973 RVA: 0x003BDC9D File Offset: 0x003BBE9D
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

		// Token: 0x0600C71E RID: 50974 RVA: 0x003BDCB0 File Offset: 0x003BBEB0
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
			return num;
		}

		// Token: 0x0600C71F RID: 50975 RVA: 0x003BDD28 File Offset: 0x003BBF28
		public override bool Equals(object obj)
		{
			AttributionRegistration attributionRegistration = obj as AttributionRegistration;
			return attributionRegistration != null && this.HasApplicationId == attributionRegistration.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(attributionRegistration.ApplicationId)) && this.HasDeviceType == attributionRegistration.HasDeviceType && (!this.HasDeviceType || this.DeviceType.Equals(attributionRegistration.DeviceType)) && this.HasFirstInstallDate == attributionRegistration.HasFirstInstallDate && (!this.HasFirstInstallDate || this.FirstInstallDate.Equals(attributionRegistration.FirstInstallDate)) && this.HasBundleId == attributionRegistration.HasBundleId && (!this.HasBundleId || this.BundleId.Equals(attributionRegistration.BundleId));
		}

		// Token: 0x0600C720 RID: 50976 RVA: 0x003BDDF1 File Offset: 0x003BBFF1
		public void Deserialize(Stream stream)
		{
			AttributionRegistration.Deserialize(stream, this);
		}

		// Token: 0x0600C721 RID: 50977 RVA: 0x003BDDFB File Offset: 0x003BBFFB
		public static AttributionRegistration Deserialize(Stream stream, AttributionRegistration instance)
		{
			return AttributionRegistration.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C722 RID: 50978 RVA: 0x003BDE08 File Offset: 0x003BC008
		public static AttributionRegistration DeserializeLengthDelimited(Stream stream)
		{
			AttributionRegistration attributionRegistration = new AttributionRegistration();
			AttributionRegistration.DeserializeLengthDelimited(stream, attributionRegistration);
			return attributionRegistration;
		}

		// Token: 0x0600C723 RID: 50979 RVA: 0x003BDE24 File Offset: 0x003BC024
		public static AttributionRegistration DeserializeLengthDelimited(Stream stream, AttributionRegistration instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributionRegistration.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C724 RID: 50980 RVA: 0x003BDE4C File Offset: 0x003BC04C
		public static AttributionRegistration Deserialize(Stream stream, AttributionRegistration instance, long limit)
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
						ProtocolParser.SkipKey(stream, key);
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

		// Token: 0x0600C725 RID: 50981 RVA: 0x003BDF4D File Offset: 0x003BC14D
		public void Serialize(Stream stream)
		{
			AttributionRegistration.Serialize(stream, this);
		}

		// Token: 0x0600C726 RID: 50982 RVA: 0x003BDF58 File Offset: 0x003BC158
		public static void Serialize(Stream stream, AttributionRegistration instance)
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
		}

		// Token: 0x0600C727 RID: 50983 RVA: 0x003BE01C File Offset: 0x003BC21C
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
			return num;
		}

		// Token: 0x04009E5C RID: 40540
		public bool HasApplicationId;

		// Token: 0x04009E5D RID: 40541
		private string _ApplicationId;

		// Token: 0x04009E5E RID: 40542
		public bool HasDeviceType;

		// Token: 0x04009E5F RID: 40543
		private string _DeviceType;

		// Token: 0x04009E60 RID: 40544
		public bool HasFirstInstallDate;

		// Token: 0x04009E61 RID: 40545
		private ulong _FirstInstallDate;

		// Token: 0x04009E62 RID: 40546
		public bool HasBundleId;

		// Token: 0x04009E63 RID: 40547
		private string _BundleId;
	}
}
