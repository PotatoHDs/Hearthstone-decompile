using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x02000045 RID: 69
	public class ProductAttribute : IProtoBuf
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0001178B File Offset: 0x0000F98B
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x00011793 File Offset: 0x0000F993
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.HasName = (value != null);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x000117A6 File Offset: 0x0000F9A6
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x000117AE File Offset: 0x0000F9AE
		public string Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				this._Value = value;
				this.HasValue = (value != null);
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000117C4 File Offset: 0x0000F9C4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			if (this.HasValue)
			{
				num ^= this.Value.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001180C File Offset: 0x0000FA0C
		public override bool Equals(object obj)
		{
			ProductAttribute productAttribute = obj as ProductAttribute;
			return productAttribute != null && this.HasName == productAttribute.HasName && (!this.HasName || this.Name.Equals(productAttribute.Name)) && this.HasValue == productAttribute.HasValue && (!this.HasValue || this.Value.Equals(productAttribute.Value));
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001187C File Offset: 0x0000FA7C
		public void Deserialize(Stream stream)
		{
			ProductAttribute.Deserialize(stream, this);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00011886 File Offset: 0x0000FA86
		public static ProductAttribute Deserialize(Stream stream, ProductAttribute instance)
		{
			return ProductAttribute.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00011894 File Offset: 0x0000FA94
		public static ProductAttribute DeserializeLengthDelimited(Stream stream)
		{
			ProductAttribute productAttribute = new ProductAttribute();
			ProductAttribute.DeserializeLengthDelimited(stream, productAttribute);
			return productAttribute;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000118B0 File Offset: 0x0000FAB0
		public static ProductAttribute DeserializeLengthDelimited(Stream stream, ProductAttribute instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProductAttribute.Deserialize(stream, instance, num);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000118D8 File Offset: 0x0000FAD8
		public static ProductAttribute Deserialize(Stream stream, ProductAttribute instance, long limit)
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
					if (num != 18)
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
						instance.Value = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Name = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00011970 File Offset: 0x0000FB70
		public void Serialize(Stream stream)
		{
			ProductAttribute.Serialize(stream, this);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001197C File Offset: 0x0000FB7C
		public static void Serialize(Stream stream, ProductAttribute instance)
		{
			if (instance.HasName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasValue)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Value));
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000119D8 File Offset: 0x0000FBD8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasValue)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Value);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x04000182 RID: 386
		public bool HasName;

		// Token: 0x04000183 RID: 387
		private string _Name;

		// Token: 0x04000184 RID: 388
		public bool HasValue;

		// Token: 0x04000185 RID: 389
		private string _Value;
	}
}
