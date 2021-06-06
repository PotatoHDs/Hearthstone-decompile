using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001193 RID: 4499
	public class AttributionContentUnlocked : IProtoBuf
	{
		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x0600C645 RID: 50757 RVA: 0x003BA3E3 File Offset: 0x003B85E3
		// (set) Token: 0x0600C646 RID: 50758 RVA: 0x003BA3EB File Offset: 0x003B85EB
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

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x0600C647 RID: 50759 RVA: 0x003BA3FE File Offset: 0x003B85FE
		// (set) Token: 0x0600C648 RID: 50760 RVA: 0x003BA406 File Offset: 0x003B8606
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

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x0600C649 RID: 50761 RVA: 0x003BA419 File Offset: 0x003B8619
		// (set) Token: 0x0600C64A RID: 50762 RVA: 0x003BA421 File Offset: 0x003B8621
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

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x0600C64B RID: 50763 RVA: 0x003BA431 File Offset: 0x003B8631
		// (set) Token: 0x0600C64C RID: 50764 RVA: 0x003BA439 File Offset: 0x003B8639
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

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x0600C64D RID: 50765 RVA: 0x003BA44C File Offset: 0x003B864C
		// (set) Token: 0x0600C64E RID: 50766 RVA: 0x003BA454 File Offset: 0x003B8654
		public string ContentId
		{
			get
			{
				return this._ContentId;
			}
			set
			{
				this._ContentId = value;
				this.HasContentId = (value != null);
			}
		}

		// Token: 0x0600C64F RID: 50767 RVA: 0x003BA468 File Offset: 0x003B8668
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
			if (this.HasContentId)
			{
				num ^= this.ContentId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C650 RID: 50768 RVA: 0x003BA4F4 File Offset: 0x003B86F4
		public override bool Equals(object obj)
		{
			AttributionContentUnlocked attributionContentUnlocked = obj as AttributionContentUnlocked;
			return attributionContentUnlocked != null && this.HasApplicationId == attributionContentUnlocked.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(attributionContentUnlocked.ApplicationId)) && this.HasDeviceType == attributionContentUnlocked.HasDeviceType && (!this.HasDeviceType || this.DeviceType.Equals(attributionContentUnlocked.DeviceType)) && this.HasFirstInstallDate == attributionContentUnlocked.HasFirstInstallDate && (!this.HasFirstInstallDate || this.FirstInstallDate.Equals(attributionContentUnlocked.FirstInstallDate)) && this.HasBundleId == attributionContentUnlocked.HasBundleId && (!this.HasBundleId || this.BundleId.Equals(attributionContentUnlocked.BundleId)) && this.HasContentId == attributionContentUnlocked.HasContentId && (!this.HasContentId || this.ContentId.Equals(attributionContentUnlocked.ContentId));
		}

		// Token: 0x0600C651 RID: 50769 RVA: 0x003BA5E8 File Offset: 0x003B87E8
		public void Deserialize(Stream stream)
		{
			AttributionContentUnlocked.Deserialize(stream, this);
		}

		// Token: 0x0600C652 RID: 50770 RVA: 0x003BA5F2 File Offset: 0x003B87F2
		public static AttributionContentUnlocked Deserialize(Stream stream, AttributionContentUnlocked instance)
		{
			return AttributionContentUnlocked.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C653 RID: 50771 RVA: 0x003BA600 File Offset: 0x003B8800
		public static AttributionContentUnlocked DeserializeLengthDelimited(Stream stream)
		{
			AttributionContentUnlocked attributionContentUnlocked = new AttributionContentUnlocked();
			AttributionContentUnlocked.DeserializeLengthDelimited(stream, attributionContentUnlocked);
			return attributionContentUnlocked;
		}

		// Token: 0x0600C654 RID: 50772 RVA: 0x003BA61C File Offset: 0x003B881C
		public static AttributionContentUnlocked DeserializeLengthDelimited(Stream stream, AttributionContentUnlocked instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributionContentUnlocked.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C655 RID: 50773 RVA: 0x003BA644 File Offset: 0x003B8844
		public static AttributionContentUnlocked Deserialize(Stream stream, AttributionContentUnlocked instance, long limit)
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
				else if (num == 42)
				{
					instance.ContentId = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600C656 RID: 50774 RVA: 0x003BA75B File Offset: 0x003B895B
		public void Serialize(Stream stream)
		{
			AttributionContentUnlocked.Serialize(stream, this);
		}

		// Token: 0x0600C657 RID: 50775 RVA: 0x003BA764 File Offset: 0x003B8964
		public static void Serialize(Stream stream, AttributionContentUnlocked instance)
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
			if (instance.HasContentId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ContentId));
			}
		}

		// Token: 0x0600C658 RID: 50776 RVA: 0x003BA850 File Offset: 0x003B8A50
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
			if (this.HasContentId)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.ContentId);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			return num;
		}

		// Token: 0x04009DEF RID: 40431
		public bool HasApplicationId;

		// Token: 0x04009DF0 RID: 40432
		private string _ApplicationId;

		// Token: 0x04009DF1 RID: 40433
		public bool HasDeviceType;

		// Token: 0x04009DF2 RID: 40434
		private string _DeviceType;

		// Token: 0x04009DF3 RID: 40435
		public bool HasFirstInstallDate;

		// Token: 0x04009DF4 RID: 40436
		private ulong _FirstInstallDate;

		// Token: 0x04009DF5 RID: 40437
		public bool HasBundleId;

		// Token: 0x04009DF6 RID: 40438
		private string _BundleId;

		// Token: 0x04009DF7 RID: 40439
		public bool HasContentId;

		// Token: 0x04009DF8 RID: 40440
		private string _ContentId;
	}
}
