using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011E7 RID: 4583
	public class Product : IProtoBuf
	{
		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x0600CCA1 RID: 52385 RVA: 0x003D19A6 File Offset: 0x003CFBA6
		// (set) Token: 0x0600CCA2 RID: 52386 RVA: 0x003D19AE File Offset: 0x003CFBAE
		public long ProductId
		{
			get
			{
				return this._ProductId;
			}
			set
			{
				this._ProductId = value;
				this.HasProductId = true;
			}
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x0600CCA3 RID: 52387 RVA: 0x003D19BE File Offset: 0x003CFBBE
		// (set) Token: 0x0600CCA4 RID: 52388 RVA: 0x003D19C6 File Offset: 0x003CFBC6
		public string HsProductType
		{
			get
			{
				return this._HsProductType;
			}
			set
			{
				this._HsProductType = value;
				this.HasHsProductType = (value != null);
			}
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x0600CCA5 RID: 52389 RVA: 0x003D19D9 File Offset: 0x003CFBD9
		// (set) Token: 0x0600CCA6 RID: 52390 RVA: 0x003D19E1 File Offset: 0x003CFBE1
		public int HsProductId
		{
			get
			{
				return this._HsProductId;
			}
			set
			{
				this._HsProductId = value;
				this.HasHsProductId = true;
			}
		}

		// Token: 0x0600CCA7 RID: 52391 RVA: 0x003D19F4 File Offset: 0x003CFBF4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasProductId)
			{
				num ^= this.ProductId.GetHashCode();
			}
			if (this.HasHsProductType)
			{
				num ^= this.HsProductType.GetHashCode();
			}
			if (this.HasHsProductId)
			{
				num ^= this.HsProductId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CCA8 RID: 52392 RVA: 0x003D1A58 File Offset: 0x003CFC58
		public override bool Equals(object obj)
		{
			Product product = obj as Product;
			return product != null && this.HasProductId == product.HasProductId && (!this.HasProductId || this.ProductId.Equals(product.ProductId)) && this.HasHsProductType == product.HasHsProductType && (!this.HasHsProductType || this.HsProductType.Equals(product.HsProductType)) && this.HasHsProductId == product.HasHsProductId && (!this.HasHsProductId || this.HsProductId.Equals(product.HsProductId));
		}

		// Token: 0x0600CCA9 RID: 52393 RVA: 0x003D1AF9 File Offset: 0x003CFCF9
		public void Deserialize(Stream stream)
		{
			Product.Deserialize(stream, this);
		}

		// Token: 0x0600CCAA RID: 52394 RVA: 0x003D1B03 File Offset: 0x003CFD03
		public static Product Deserialize(Stream stream, Product instance)
		{
			return Product.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CCAB RID: 52395 RVA: 0x003D1B10 File Offset: 0x003CFD10
		public static Product DeserializeLengthDelimited(Stream stream)
		{
			Product product = new Product();
			Product.DeserializeLengthDelimited(stream, product);
			return product;
		}

		// Token: 0x0600CCAC RID: 52396 RVA: 0x003D1B2C File Offset: 0x003CFD2C
		public static Product DeserializeLengthDelimited(Stream stream, Product instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Product.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CCAD RID: 52397 RVA: 0x003D1B54 File Offset: 0x003CFD54
		public static Product Deserialize(Stream stream, Product instance, long limit)
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
				else if (num != 8)
				{
					if (num != 18)
					{
						if (num != 24)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.HsProductId = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.HsProductType = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.ProductId = (long)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600CCAE RID: 52398 RVA: 0x003D1C02 File Offset: 0x003CFE02
		public void Serialize(Stream stream)
		{
			Product.Serialize(stream, this);
		}

		// Token: 0x0600CCAF RID: 52399 RVA: 0x003D1C0C File Offset: 0x003CFE0C
		public static void Serialize(Stream stream, Product instance)
		{
			if (instance.HasProductId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ProductId);
			}
			if (instance.HasHsProductType)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.HsProductType));
			}
			if (instance.HasHsProductId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.HsProductId));
			}
		}

		// Token: 0x0600CCB0 RID: 52400 RVA: 0x003D1C78 File Offset: 0x003CFE78
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasProductId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.ProductId);
			}
			if (this.HasHsProductType)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.HsProductType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasHsProductId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.HsProductId));
			}
			return num;
		}

		// Token: 0x0400A0AD RID: 41133
		public bool HasProductId;

		// Token: 0x0400A0AE RID: 41134
		private long _ProductId;

		// Token: 0x0400A0AF RID: 41135
		public bool HasHsProductType;

		// Token: 0x0400A0B0 RID: 41136
		private string _HsProductType;

		// Token: 0x0400A0B1 RID: 41137
		public bool HasHsProductId;

		// Token: 0x0400A0B2 RID: 41138
		private int _HsProductId;
	}
}
