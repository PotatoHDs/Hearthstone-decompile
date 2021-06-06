using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011F2 RID: 4594
	public class ShopCard : IProtoBuf
	{
		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x0600CD70 RID: 52592 RVA: 0x003D47E7 File Offset: 0x003D29E7
		// (set) Token: 0x0600CD71 RID: 52593 RVA: 0x003D47EF File Offset: 0x003D29EF
		public Product Product
		{
			get
			{
				return this._Product;
			}
			set
			{
				this._Product = value;
				this.HasProduct = (value != null);
			}
		}

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x0600CD72 RID: 52594 RVA: 0x003D4802 File Offset: 0x003D2A02
		// (set) Token: 0x0600CD73 RID: 52595 RVA: 0x003D480A File Offset: 0x003D2A0A
		public string SectionName
		{
			get
			{
				return this._SectionName;
			}
			set
			{
				this._SectionName = value;
				this.HasSectionName = (value != null);
			}
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x0600CD74 RID: 52596 RVA: 0x003D481D File Offset: 0x003D2A1D
		// (set) Token: 0x0600CD75 RID: 52597 RVA: 0x003D4825 File Offset: 0x003D2A25
		public int SectionIndex
		{
			get
			{
				return this._SectionIndex;
			}
			set
			{
				this._SectionIndex = value;
				this.HasSectionIndex = true;
			}
		}

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x0600CD76 RID: 52598 RVA: 0x003D4835 File Offset: 0x003D2A35
		// (set) Token: 0x0600CD77 RID: 52599 RVA: 0x003D483D File Offset: 0x003D2A3D
		public int SlotIndex
		{
			get
			{
				return this._SlotIndex;
			}
			set
			{
				this._SlotIndex = value;
				this.HasSlotIndex = true;
			}
		}

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x0600CD78 RID: 52600 RVA: 0x003D484D File Offset: 0x003D2A4D
		// (set) Token: 0x0600CD79 RID: 52601 RVA: 0x003D4855 File Offset: 0x003D2A55
		public int SecondsRemaining
		{
			get
			{
				return this._SecondsRemaining;
			}
			set
			{
				this._SecondsRemaining = value;
				this.HasSecondsRemaining = true;
			}
		}

		// Token: 0x0600CD7A RID: 52602 RVA: 0x003D4868 File Offset: 0x003D2A68
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasProduct)
			{
				num ^= this.Product.GetHashCode();
			}
			if (this.HasSectionName)
			{
				num ^= this.SectionName.GetHashCode();
			}
			if (this.HasSectionIndex)
			{
				num ^= this.SectionIndex.GetHashCode();
			}
			if (this.HasSlotIndex)
			{
				num ^= this.SlotIndex.GetHashCode();
			}
			if (this.HasSecondsRemaining)
			{
				num ^= this.SecondsRemaining.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CD7B RID: 52603 RVA: 0x003D48FC File Offset: 0x003D2AFC
		public override bool Equals(object obj)
		{
			ShopCard shopCard = obj as ShopCard;
			return shopCard != null && this.HasProduct == shopCard.HasProduct && (!this.HasProduct || this.Product.Equals(shopCard.Product)) && this.HasSectionName == shopCard.HasSectionName && (!this.HasSectionName || this.SectionName.Equals(shopCard.SectionName)) && this.HasSectionIndex == shopCard.HasSectionIndex && (!this.HasSectionIndex || this.SectionIndex.Equals(shopCard.SectionIndex)) && this.HasSlotIndex == shopCard.HasSlotIndex && (!this.HasSlotIndex || this.SlotIndex.Equals(shopCard.SlotIndex)) && this.HasSecondsRemaining == shopCard.HasSecondsRemaining && (!this.HasSecondsRemaining || this.SecondsRemaining.Equals(shopCard.SecondsRemaining));
		}

		// Token: 0x0600CD7C RID: 52604 RVA: 0x003D49F6 File Offset: 0x003D2BF6
		public void Deserialize(Stream stream)
		{
			ShopCard.Deserialize(stream, this);
		}

		// Token: 0x0600CD7D RID: 52605 RVA: 0x003D4A00 File Offset: 0x003D2C00
		public static ShopCard Deserialize(Stream stream, ShopCard instance)
		{
			return ShopCard.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CD7E RID: 52606 RVA: 0x003D4A0C File Offset: 0x003D2C0C
		public static ShopCard DeserializeLengthDelimited(Stream stream)
		{
			ShopCard shopCard = new ShopCard();
			ShopCard.DeserializeLengthDelimited(stream, shopCard);
			return shopCard;
		}

		// Token: 0x0600CD7F RID: 52607 RVA: 0x003D4A28 File Offset: 0x003D2C28
		public static ShopCard DeserializeLengthDelimited(Stream stream, ShopCard instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ShopCard.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CD80 RID: 52608 RVA: 0x003D4A50 File Offset: 0x003D2C50
		public static ShopCard Deserialize(Stream stream, ShopCard instance, long limit)
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
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								instance.SectionName = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.Product == null)
							{
								instance.Product = Product.DeserializeLengthDelimited(stream);
								continue;
							}
							Product.DeserializeLengthDelimited(stream, instance.Product);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.SectionIndex = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.SlotIndex = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.SecondsRemaining = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600CD81 RID: 52609 RVA: 0x003D4B5A File Offset: 0x003D2D5A
		public void Serialize(Stream stream)
		{
			ShopCard.Serialize(stream, this);
		}

		// Token: 0x0600CD82 RID: 52610 RVA: 0x003D4B64 File Offset: 0x003D2D64
		public static void Serialize(Stream stream, ShopCard instance)
		{
			if (instance.HasProduct)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Product.GetSerializedSize());
				Product.Serialize(stream, instance.Product);
			}
			if (instance.HasSectionName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SectionName));
			}
			if (instance.HasSectionIndex)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SectionIndex));
			}
			if (instance.HasSlotIndex)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SlotIndex));
			}
			if (instance.HasSecondsRemaining)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SecondsRemaining));
			}
		}

		// Token: 0x0600CD83 RID: 52611 RVA: 0x003D4C1C File Offset: 0x003D2E1C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasProduct)
			{
				num += 1U;
				uint serializedSize = this.Product.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSectionName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.SectionName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasSectionIndex)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SectionIndex));
			}
			if (this.HasSlotIndex)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SlotIndex));
			}
			if (this.HasSecondsRemaining)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SecondsRemaining));
			}
			return num;
		}

		// Token: 0x0400A102 RID: 41218
		public bool HasProduct;

		// Token: 0x0400A103 RID: 41219
		private Product _Product;

		// Token: 0x0400A104 RID: 41220
		public bool HasSectionName;

		// Token: 0x0400A105 RID: 41221
		private string _SectionName;

		// Token: 0x0400A106 RID: 41222
		public bool HasSectionIndex;

		// Token: 0x0400A107 RID: 41223
		private int _SectionIndex;

		// Token: 0x0400A108 RID: 41224
		public bool HasSlotIndex;

		// Token: 0x0400A109 RID: 41225
		private int _SlotIndex;

		// Token: 0x0400A10A RID: 41226
		public bool HasSecondsRemaining;

		// Token: 0x0400A10B RID: 41227
		private int _SecondsRemaining;
	}
}
