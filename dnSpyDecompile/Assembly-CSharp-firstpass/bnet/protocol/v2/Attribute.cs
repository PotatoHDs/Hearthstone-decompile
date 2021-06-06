using System;
using System.IO;
using System.Text;

namespace bnet.protocol.v2
{
	// Token: 0x020002CE RID: 718
	public class Attribute : IProtoBuf
	{
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002A18 RID: 10776 RVA: 0x00093035 File Offset: 0x00091235
		// (set) Token: 0x06002A19 RID: 10777 RVA: 0x0009303D File Offset: 0x0009123D
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

		// Token: 0x06002A1A RID: 10778 RVA: 0x00093050 File Offset: 0x00091250
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06002A1B RID: 10779 RVA: 0x00093059 File Offset: 0x00091259
		// (set) Token: 0x06002A1C RID: 10780 RVA: 0x00093061 File Offset: 0x00091261
		public Variant Value
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

		// Token: 0x06002A1D RID: 10781 RVA: 0x00093074 File Offset: 0x00091274
		public void SetValue(Variant val)
		{
			this.Value = val;
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x00093080 File Offset: 0x00091280
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

		// Token: 0x06002A1F RID: 10783 RVA: 0x000930C8 File Offset: 0x000912C8
		public override bool Equals(object obj)
		{
			Attribute attribute = obj as Attribute;
			return attribute != null && this.HasName == attribute.HasName && (!this.HasName || this.Name.Equals(attribute.Name)) && this.HasValue == attribute.HasValue && (!this.HasValue || this.Value.Equals(attribute.Value));
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06002A20 RID: 10784 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x00093138 File Offset: 0x00091338
		public static Attribute ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Attribute>(bs, 0, -1);
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x00093142 File Offset: 0x00091342
		public void Deserialize(Stream stream)
		{
			Attribute.Deserialize(stream, this);
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x0009314C File Offset: 0x0009134C
		public static Attribute Deserialize(Stream stream, Attribute instance)
		{
			return Attribute.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x00093158 File Offset: 0x00091358
		public static Attribute DeserializeLengthDelimited(Stream stream)
		{
			Attribute attribute = new Attribute();
			Attribute.DeserializeLengthDelimited(stream, attribute);
			return attribute;
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x00093174 File Offset: 0x00091374
		public static Attribute DeserializeLengthDelimited(Stream stream, Attribute instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Attribute.Deserialize(stream, instance, num);
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x0009319C File Offset: 0x0009139C
		public static Attribute Deserialize(Stream stream, Attribute instance, long limit)
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
					else if (instance.Value == null)
					{
						instance.Value = Variant.DeserializeLengthDelimited(stream);
					}
					else
					{
						Variant.DeserializeLengthDelimited(stream, instance.Value);
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

		// Token: 0x06002A27 RID: 10791 RVA: 0x0009324E File Offset: 0x0009144E
		public void Serialize(Stream stream)
		{
			Attribute.Serialize(stream, this);
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x00093258 File Offset: 0x00091458
		public static void Serialize(Stream stream, Attribute instance)
		{
			if (instance.HasName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasValue)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Value.GetSerializedSize());
				Variant.Serialize(stream, instance.Value);
			}
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x000932B8 File Offset: 0x000914B8
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
				uint serializedSize = this.Value.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x040011EB RID: 4587
		public bool HasName;

		// Token: 0x040011EC RID: 4588
		private string _Name;

		// Token: 0x040011ED RID: 4589
		public bool HasValue;

		// Token: 0x040011EE RID: 4590
		private Variant _Value;
	}
}
