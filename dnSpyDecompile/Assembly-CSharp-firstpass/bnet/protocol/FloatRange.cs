using System;
using System.IO;

namespace bnet.protocol
{
	// Token: 0x020002BB RID: 699
	public class FloatRange : IProtoBuf
	{
		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002933 RID: 10547 RVA: 0x00090EFC File Offset: 0x0008F0FC
		// (set) Token: 0x06002934 RID: 10548 RVA: 0x00090F04 File Offset: 0x0008F104
		public float Min
		{
			get
			{
				return this._Min;
			}
			set
			{
				this._Min = value;
				this.HasMin = true;
			}
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x00090F14 File Offset: 0x0008F114
		public void SetMin(float val)
		{
			this.Min = val;
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06002936 RID: 10550 RVA: 0x00090F1D File Offset: 0x0008F11D
		// (set) Token: 0x06002937 RID: 10551 RVA: 0x00090F25 File Offset: 0x0008F125
		public float Max
		{
			get
			{
				return this._Max;
			}
			set
			{
				this._Max = value;
				this.HasMax = true;
			}
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x00090F35 File Offset: 0x0008F135
		public void SetMax(float val)
		{
			this.Max = val;
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x00090F40 File Offset: 0x0008F140
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMin)
			{
				num ^= this.Min.GetHashCode();
			}
			if (this.HasMax)
			{
				num ^= this.Max.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x00090F8C File Offset: 0x0008F18C
		public override bool Equals(object obj)
		{
			FloatRange floatRange = obj as FloatRange;
			return floatRange != null && this.HasMin == floatRange.HasMin && (!this.HasMin || this.Min.Equals(floatRange.Min)) && this.HasMax == floatRange.HasMax && (!this.HasMax || this.Max.Equals(floatRange.Max));
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600293B RID: 10555 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x00091002 File Offset: 0x0008F202
		public static FloatRange ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FloatRange>(bs, 0, -1);
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x0009100C File Offset: 0x0008F20C
		public void Deserialize(Stream stream)
		{
			FloatRange.Deserialize(stream, this);
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x00091016 File Offset: 0x0008F216
		public static FloatRange Deserialize(Stream stream, FloatRange instance)
		{
			return FloatRange.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x00091024 File Offset: 0x0008F224
		public static FloatRange DeserializeLengthDelimited(Stream stream)
		{
			FloatRange floatRange = new FloatRange();
			FloatRange.DeserializeLengthDelimited(stream, floatRange);
			return floatRange;
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x00091040 File Offset: 0x0008F240
		public static FloatRange DeserializeLengthDelimited(Stream stream, FloatRange instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FloatRange.Deserialize(stream, instance, num);
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x00091068 File Offset: 0x0008F268
		public static FloatRange Deserialize(Stream stream, FloatRange instance, long limit)
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
				else if (num != 13)
				{
					if (num != 21)
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
						instance.Max = binaryReader.ReadSingle();
					}
				}
				else
				{
					instance.Min = binaryReader.ReadSingle();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x00091107 File Offset: 0x0008F307
		public void Serialize(Stream stream)
		{
			FloatRange.Serialize(stream, this);
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x00091110 File Offset: 0x0008F310
		public static void Serialize(Stream stream, FloatRange instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMin)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.Min);
			}
			if (instance.HasMax)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Max);
			}
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x0009115C File Offset: 0x0008F35C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMin)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasMax)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x040011A0 RID: 4512
		public bool HasMin;

		// Token: 0x040011A1 RID: 4513
		private float _Min;

		// Token: 0x040011A2 RID: 4514
		public bool HasMax;

		// Token: 0x040011A3 RID: 4515
		private float _Max;
	}
}
