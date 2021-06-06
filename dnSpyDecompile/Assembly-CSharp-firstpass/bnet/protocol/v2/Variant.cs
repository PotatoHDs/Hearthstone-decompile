using System;
using System.IO;
using System.Text;

namespace bnet.protocol.v2
{
	// Token: 0x020002CD RID: 717
	public class Variant : IProtoBuf
	{
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060029F9 RID: 10745 RVA: 0x00092AAB File Offset: 0x00090CAB
		// (set) Token: 0x060029FA RID: 10746 RVA: 0x00092AB3 File Offset: 0x00090CB3
		public bool BoolValue
		{
			get
			{
				return this._BoolValue;
			}
			set
			{
				this._BoolValue = value;
				this.HasBoolValue = true;
			}
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x00092AC3 File Offset: 0x00090CC3
		public void SetBoolValue(bool val)
		{
			this.BoolValue = val;
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060029FC RID: 10748 RVA: 0x00092ACC File Offset: 0x00090CCC
		// (set) Token: 0x060029FD RID: 10749 RVA: 0x00092AD4 File Offset: 0x00090CD4
		public long IntValue
		{
			get
			{
				return this._IntValue;
			}
			set
			{
				this._IntValue = value;
				this.HasIntValue = true;
			}
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x00092AE4 File Offset: 0x00090CE4
		public void SetIntValue(long val)
		{
			this.IntValue = val;
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x060029FF RID: 10751 RVA: 0x00092AED File Offset: 0x00090CED
		// (set) Token: 0x06002A00 RID: 10752 RVA: 0x00092AF5 File Offset: 0x00090CF5
		public double FloatValue
		{
			get
			{
				return this._FloatValue;
			}
			set
			{
				this._FloatValue = value;
				this.HasFloatValue = true;
			}
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x00092B05 File Offset: 0x00090D05
		public void SetFloatValue(double val)
		{
			this.FloatValue = val;
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06002A02 RID: 10754 RVA: 0x00092B0E File Offset: 0x00090D0E
		// (set) Token: 0x06002A03 RID: 10755 RVA: 0x00092B16 File Offset: 0x00090D16
		public string StringValue
		{
			get
			{
				return this._StringValue;
			}
			set
			{
				this._StringValue = value;
				this.HasStringValue = (value != null);
			}
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x00092B29 File Offset: 0x00090D29
		public void SetStringValue(string val)
		{
			this.StringValue = val;
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06002A05 RID: 10757 RVA: 0x00092B32 File Offset: 0x00090D32
		// (set) Token: 0x06002A06 RID: 10758 RVA: 0x00092B3A File Offset: 0x00090D3A
		public byte[] BlobValue
		{
			get
			{
				return this._BlobValue;
			}
			set
			{
				this._BlobValue = value;
				this.HasBlobValue = (value != null);
			}
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x00092B4D File Offset: 0x00090D4D
		public void SetBlobValue(byte[] val)
		{
			this.BlobValue = val;
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06002A08 RID: 10760 RVA: 0x00092B56 File Offset: 0x00090D56
		// (set) Token: 0x06002A09 RID: 10761 RVA: 0x00092B5E File Offset: 0x00090D5E
		public ulong UintValue
		{
			get
			{
				return this._UintValue;
			}
			set
			{
				this._UintValue = value;
				this.HasUintValue = true;
			}
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x00092B6E File Offset: 0x00090D6E
		public void SetUintValue(ulong val)
		{
			this.UintValue = val;
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x00092B78 File Offset: 0x00090D78
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasBoolValue)
			{
				num ^= this.BoolValue.GetHashCode();
			}
			if (this.HasIntValue)
			{
				num ^= this.IntValue.GetHashCode();
			}
			if (this.HasFloatValue)
			{
				num ^= this.FloatValue.GetHashCode();
			}
			if (this.HasStringValue)
			{
				num ^= this.StringValue.GetHashCode();
			}
			if (this.HasBlobValue)
			{
				num ^= this.BlobValue.GetHashCode();
			}
			if (this.HasUintValue)
			{
				num ^= this.UintValue.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x00092C24 File Offset: 0x00090E24
		public override bool Equals(object obj)
		{
			Variant variant = obj as Variant;
			return variant != null && this.HasBoolValue == variant.HasBoolValue && (!this.HasBoolValue || this.BoolValue.Equals(variant.BoolValue)) && this.HasIntValue == variant.HasIntValue && (!this.HasIntValue || this.IntValue.Equals(variant.IntValue)) && this.HasFloatValue == variant.HasFloatValue && (!this.HasFloatValue || this.FloatValue.Equals(variant.FloatValue)) && this.HasStringValue == variant.HasStringValue && (!this.HasStringValue || this.StringValue.Equals(variant.StringValue)) && this.HasBlobValue == variant.HasBlobValue && (!this.HasBlobValue || this.BlobValue.Equals(variant.BlobValue)) && this.HasUintValue == variant.HasUintValue && (!this.HasUintValue || this.UintValue.Equals(variant.UintValue));
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002A0D RID: 10765 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x00092D4D File Offset: 0x00090F4D
		public static Variant ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Variant>(bs, 0, -1);
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x00092D57 File Offset: 0x00090F57
		public void Deserialize(Stream stream)
		{
			Variant.Deserialize(stream, this);
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x00092D61 File Offset: 0x00090F61
		public static Variant Deserialize(Stream stream, Variant instance)
		{
			return Variant.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x00092D6C File Offset: 0x00090F6C
		public static Variant DeserializeLengthDelimited(Stream stream)
		{
			Variant variant = new Variant();
			Variant.DeserializeLengthDelimited(stream, variant);
			return variant;
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x00092D88 File Offset: 0x00090F88
		public static Variant DeserializeLengthDelimited(Stream stream, Variant instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Variant.Deserialize(stream, instance, num);
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x00092DB0 File Offset: 0x00090FB0
		public static Variant Deserialize(Stream stream, Variant instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (num <= 25)
					{
						if (num == 8)
						{
							instance.BoolValue = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 16)
						{
							instance.IntValue = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 25)
						{
							instance.FloatValue = binaryReader.ReadDouble();
							continue;
						}
					}
					else
					{
						if (num == 34)
						{
							instance.StringValue = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 42)
						{
							instance.BlobValue = ProtocolParser.ReadBytes(stream);
							continue;
						}
						if (num == 48)
						{
							instance.UintValue = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06002A14 RID: 10772 RVA: 0x00092EB3 File Offset: 0x000910B3
		public void Serialize(Stream stream)
		{
			Variant.Serialize(stream, this);
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x00092EBC File Offset: 0x000910BC
		public static void Serialize(Stream stream, Variant instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasBoolValue)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.BoolValue);
			}
			if (instance.HasIntValue)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.IntValue);
			}
			if (instance.HasFloatValue)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.FloatValue);
			}
			if (instance.HasStringValue)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.StringValue));
			}
			if (instance.HasBlobValue)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, instance.BlobValue);
			}
			if (instance.HasUintValue)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.UintValue);
			}
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x00092F84 File Offset: 0x00091184
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasBoolValue)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIntValue)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.IntValue);
			}
			if (this.HasFloatValue)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasStringValue)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.StringValue);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasBlobValue)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.BlobValue.Length) + (uint)this.BlobValue.Length;
			}
			if (this.HasUintValue)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.UintValue);
			}
			return num;
		}

		// Token: 0x040011DF RID: 4575
		public bool HasBoolValue;

		// Token: 0x040011E0 RID: 4576
		private bool _BoolValue;

		// Token: 0x040011E1 RID: 4577
		public bool HasIntValue;

		// Token: 0x040011E2 RID: 4578
		private long _IntValue;

		// Token: 0x040011E3 RID: 4579
		public bool HasFloatValue;

		// Token: 0x040011E4 RID: 4580
		private double _FloatValue;

		// Token: 0x040011E5 RID: 4581
		public bool HasStringValue;

		// Token: 0x040011E6 RID: 4582
		private string _StringValue;

		// Token: 0x040011E7 RID: 4583
		public bool HasBlobValue;

		// Token: 0x040011E8 RID: 4584
		private byte[] _BlobValue;

		// Token: 0x040011E9 RID: 4585
		public bool HasUintValue;

		// Token: 0x040011EA RID: 4586
		private ulong _UintValue;
	}
}
