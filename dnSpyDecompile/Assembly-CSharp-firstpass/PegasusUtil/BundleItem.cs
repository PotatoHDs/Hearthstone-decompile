using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000044 RID: 68
	public class BundleItem : IProtoBuf
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00011280 File Offset: 0x0000F480
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x00011288 File Offset: 0x0000F488
		public ProductType ProductType { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00011291 File Offset: 0x0000F491
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x00011299 File Offset: 0x0000F499
		public int Data { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x000112A2 File Offset: 0x0000F4A2
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x000112AA File Offset: 0x0000F4AA
		public int Quantity { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x000112B3 File Offset: 0x0000F4B3
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x000112BB File Offset: 0x0000F4BB
		public int BaseQuantity
		{
			get
			{
				return this._BaseQuantity;
			}
			set
			{
				this._BaseQuantity = value;
				this.HasBaseQuantity = true;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x000112CB File Offset: 0x0000F4CB
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x000112D3 File Offset: 0x0000F4D3
		public List<ProductAttribute> Attributes
		{
			get
			{
				return this._Attributes;
			}
			set
			{
				this._Attributes = value;
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000112DC File Offset: 0x0000F4DC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ProductType.GetHashCode();
			num ^= this.Data.GetHashCode();
			num ^= this.Quantity.GetHashCode();
			if (this.HasBaseQuantity)
			{
				num ^= this.BaseQuantity.GetHashCode();
			}
			foreach (ProductAttribute productAttribute in this.Attributes)
			{
				num ^= productAttribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00011394 File Offset: 0x0000F594
		public override bool Equals(object obj)
		{
			BundleItem bundleItem = obj as BundleItem;
			if (bundleItem == null)
			{
				return false;
			}
			if (!this.ProductType.Equals(bundleItem.ProductType))
			{
				return false;
			}
			if (!this.Data.Equals(bundleItem.Data))
			{
				return false;
			}
			if (!this.Quantity.Equals(bundleItem.Quantity))
			{
				return false;
			}
			if (this.HasBaseQuantity != bundleItem.HasBaseQuantity || (this.HasBaseQuantity && !this.BaseQuantity.Equals(bundleItem.BaseQuantity)))
			{
				return false;
			}
			if (this.Attributes.Count != bundleItem.Attributes.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attributes.Count; i++)
			{
				if (!this.Attributes[i].Equals(bundleItem.Attributes[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00011480 File Offset: 0x0000F680
		public void Deserialize(Stream stream)
		{
			BundleItem.Deserialize(stream, this);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001148A File Offset: 0x0000F68A
		public static BundleItem Deserialize(Stream stream, BundleItem instance)
		{
			return BundleItem.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00011498 File Offset: 0x0000F698
		public static BundleItem DeserializeLengthDelimited(Stream stream)
		{
			BundleItem bundleItem = new BundleItem();
			BundleItem.DeserializeLengthDelimited(stream, bundleItem);
			return bundleItem;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000114B4 File Offset: 0x0000F6B4
		public static BundleItem DeserializeLengthDelimited(Stream stream, BundleItem instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BundleItem.Deserialize(stream, instance, num);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000114DC File Offset: 0x0000F6DC
		public static BundleItem Deserialize(Stream stream, BundleItem instance, long limit)
		{
			if (instance.Attributes == null)
			{
				instance.Attributes = new List<ProductAttribute>();
			}
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
						if (num == 8)
						{
							instance.ProductType = (ProductType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Data = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.BaseQuantity = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 42)
						{
							instance.Attributes.Add(ProductAttribute.DeserializeLengthDelimited(stream));
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

		// Token: 0x06000429 RID: 1065 RVA: 0x000115DE File Offset: 0x0000F7DE
		public void Serialize(Stream stream)
		{
			BundleItem.Serialize(stream, this);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000115E8 File Offset: 0x0000F7E8
		public static void Serialize(Stream stream, BundleItem instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ProductType));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Data));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Quantity));
			if (instance.HasBaseQuantity)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BaseQuantity));
			}
			if (instance.Attributes.Count > 0)
			{
				foreach (ProductAttribute productAttribute in instance.Attributes)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, productAttribute.GetSerializedSize());
					ProductAttribute.Serialize(stream, productAttribute);
				}
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000116B8 File Offset: 0x0000F8B8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ProductType));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Data));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Quantity));
			if (this.HasBaseQuantity)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BaseQuantity));
			}
			if (this.Attributes.Count > 0)
			{
				foreach (ProductAttribute productAttribute in this.Attributes)
				{
					num += 1U;
					uint serializedSize = productAttribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 3U;
			return num;
		}

		// Token: 0x0400017F RID: 383
		public bool HasBaseQuantity;

		// Token: 0x04000180 RID: 384
		private int _BaseQuantity;

		// Token: 0x04000181 RID: 385
		private List<ProductAttribute> _Attributes = new List<ProductAttribute>();
	}
}
