using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001199 RID: 4505
	public class AttributionItemTransaction : IProtoBuf
	{
		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x0600C6CB RID: 50891 RVA: 0x003BC707 File Offset: 0x003BA907
		// (set) Token: 0x0600C6CC RID: 50892 RVA: 0x003BC70F File Offset: 0x003BA90F
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

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x0600C6CD RID: 50893 RVA: 0x003BC722 File Offset: 0x003BA922
		// (set) Token: 0x0600C6CE RID: 50894 RVA: 0x003BC72A File Offset: 0x003BA92A
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

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x0600C6CF RID: 50895 RVA: 0x003BC73D File Offset: 0x003BA93D
		// (set) Token: 0x0600C6D0 RID: 50896 RVA: 0x003BC745 File Offset: 0x003BA945
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

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x0600C6D1 RID: 50897 RVA: 0x003BC755 File Offset: 0x003BA955
		// (set) Token: 0x0600C6D2 RID: 50898 RVA: 0x003BC75D File Offset: 0x003BA95D
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

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x0600C6D3 RID: 50899 RVA: 0x003BC770 File Offset: 0x003BA970
		// (set) Token: 0x0600C6D4 RID: 50900 RVA: 0x003BC778 File Offset: 0x003BA978
		public string ItemId
		{
			get
			{
				return this._ItemId;
			}
			set
			{
				this._ItemId = value;
				this.HasItemId = (value != null);
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x0600C6D5 RID: 50901 RVA: 0x003BC78B File Offset: 0x003BA98B
		// (set) Token: 0x0600C6D6 RID: 50902 RVA: 0x003BC793 File Offset: 0x003BA993
		public int Quantity
		{
			get
			{
				return this._Quantity;
			}
			set
			{
				this._Quantity = value;
				this.HasQuantity = true;
			}
		}

		// Token: 0x0600C6D7 RID: 50903 RVA: 0x003BC7A4 File Offset: 0x003BA9A4
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
			if (this.HasItemId)
			{
				num ^= this.ItemId.GetHashCode();
			}
			if (this.HasQuantity)
			{
				num ^= this.Quantity.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C6D8 RID: 50904 RVA: 0x003BC848 File Offset: 0x003BAA48
		public override bool Equals(object obj)
		{
			AttributionItemTransaction attributionItemTransaction = obj as AttributionItemTransaction;
			return attributionItemTransaction != null && this.HasApplicationId == attributionItemTransaction.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(attributionItemTransaction.ApplicationId)) && this.HasDeviceType == attributionItemTransaction.HasDeviceType && (!this.HasDeviceType || this.DeviceType.Equals(attributionItemTransaction.DeviceType)) && this.HasFirstInstallDate == attributionItemTransaction.HasFirstInstallDate && (!this.HasFirstInstallDate || this.FirstInstallDate.Equals(attributionItemTransaction.FirstInstallDate)) && this.HasBundleId == attributionItemTransaction.HasBundleId && (!this.HasBundleId || this.BundleId.Equals(attributionItemTransaction.BundleId)) && this.HasItemId == attributionItemTransaction.HasItemId && (!this.HasItemId || this.ItemId.Equals(attributionItemTransaction.ItemId)) && this.HasQuantity == attributionItemTransaction.HasQuantity && (!this.HasQuantity || this.Quantity.Equals(attributionItemTransaction.Quantity));
		}

		// Token: 0x0600C6D9 RID: 50905 RVA: 0x003BC96A File Offset: 0x003BAB6A
		public void Deserialize(Stream stream)
		{
			AttributionItemTransaction.Deserialize(stream, this);
		}

		// Token: 0x0600C6DA RID: 50906 RVA: 0x003BC974 File Offset: 0x003BAB74
		public static AttributionItemTransaction Deserialize(Stream stream, AttributionItemTransaction instance)
		{
			return AttributionItemTransaction.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C6DB RID: 50907 RVA: 0x003BC980 File Offset: 0x003BAB80
		public static AttributionItemTransaction DeserializeLengthDelimited(Stream stream)
		{
			AttributionItemTransaction attributionItemTransaction = new AttributionItemTransaction();
			AttributionItemTransaction.DeserializeLengthDelimited(stream, attributionItemTransaction);
			return attributionItemTransaction;
		}

		// Token: 0x0600C6DC RID: 50908 RVA: 0x003BC99C File Offset: 0x003BAB9C
		public static AttributionItemTransaction DeserializeLengthDelimited(Stream stream, AttributionItemTransaction instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributionItemTransaction.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C6DD RID: 50909 RVA: 0x003BC9C4 File Offset: 0x003BABC4
		public static AttributionItemTransaction Deserialize(Stream stream, AttributionItemTransaction instance, long limit)
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
				else if (num != 10)
				{
					if (num != 16)
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
					else
					{
						instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.ItemId = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C6DE RID: 50910 RVA: 0x003BCAF1 File Offset: 0x003BACF1
		public void Serialize(Stream stream)
		{
			AttributionItemTransaction.Serialize(stream, this);
		}

		// Token: 0x0600C6DF RID: 50911 RVA: 0x003BCAFC File Offset: 0x003BACFC
		public static void Serialize(Stream stream, AttributionItemTransaction instance)
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
			if (instance.HasItemId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ItemId));
			}
			if (instance.HasQuantity)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Quantity));
			}
		}

		// Token: 0x0600C6E0 RID: 50912 RVA: 0x003BCC04 File Offset: 0x003BAE04
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
			if (this.HasItemId)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.ItemId);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasQuantity)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Quantity));
			}
			return num;
		}

		// Token: 0x04009E33 RID: 40499
		public bool HasApplicationId;

		// Token: 0x04009E34 RID: 40500
		private string _ApplicationId;

		// Token: 0x04009E35 RID: 40501
		public bool HasDeviceType;

		// Token: 0x04009E36 RID: 40502
		private string _DeviceType;

		// Token: 0x04009E37 RID: 40503
		public bool HasFirstInstallDate;

		// Token: 0x04009E38 RID: 40504
		private ulong _FirstInstallDate;

		// Token: 0x04009E39 RID: 40505
		public bool HasBundleId;

		// Token: 0x04009E3A RID: 40506
		private string _BundleId;

		// Token: 0x04009E3B RID: 40507
		public bool HasItemId;

		// Token: 0x04009E3C RID: 40508
		private string _ItemId;

		// Token: 0x04009E3D RID: 40509
		public bool HasQuantity;

		// Token: 0x04009E3E RID: 40510
		private int _Quantity;
	}
}
