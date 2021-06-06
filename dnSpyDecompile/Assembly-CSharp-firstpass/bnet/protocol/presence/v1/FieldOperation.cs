using System;
using System.IO;

namespace bnet.protocol.presence.v1
{
	// Token: 0x02000343 RID: 835
	public class FieldOperation : IProtoBuf
	{
		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x060033DE RID: 13278 RVA: 0x000AC9C2 File Offset: 0x000AABC2
		// (set) Token: 0x060033DF RID: 13279 RVA: 0x000AC9CA File Offset: 0x000AABCA
		public Field Field { get; set; }

		// Token: 0x060033E0 RID: 13280 RVA: 0x000AC9D3 File Offset: 0x000AABD3
		public void SetField(Field val)
		{
			this.Field = val;
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x060033E1 RID: 13281 RVA: 0x000AC9DC File Offset: 0x000AABDC
		// (set) Token: 0x060033E2 RID: 13282 RVA: 0x000AC9E4 File Offset: 0x000AABE4
		public FieldOperation.Types.OperationType Operation
		{
			get
			{
				return this._Operation;
			}
			set
			{
				this._Operation = value;
				this.HasOperation = true;
			}
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x000AC9F4 File Offset: 0x000AABF4
		public void SetOperation(FieldOperation.Types.OperationType val)
		{
			this.Operation = val;
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x000ACA00 File Offset: 0x000AAC00
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Field.GetHashCode();
			if (this.HasOperation)
			{
				num ^= this.Operation.GetHashCode();
			}
			return num;
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x000ACA48 File Offset: 0x000AAC48
		public override bool Equals(object obj)
		{
			FieldOperation fieldOperation = obj as FieldOperation;
			return fieldOperation != null && this.Field.Equals(fieldOperation.Field) && this.HasOperation == fieldOperation.HasOperation && (!this.HasOperation || this.Operation.Equals(fieldOperation.Operation));
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x060033E6 RID: 13286 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x000ACAB0 File Offset: 0x000AACB0
		public static FieldOperation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FieldOperation>(bs, 0, -1);
		}

		// Token: 0x060033E8 RID: 13288 RVA: 0x000ACABA File Offset: 0x000AACBA
		public void Deserialize(Stream stream)
		{
			FieldOperation.Deserialize(stream, this);
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x000ACAC4 File Offset: 0x000AACC4
		public static FieldOperation Deserialize(Stream stream, FieldOperation instance)
		{
			return FieldOperation.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x000ACAD0 File Offset: 0x000AACD0
		public static FieldOperation DeserializeLengthDelimited(Stream stream)
		{
			FieldOperation fieldOperation = new FieldOperation();
			FieldOperation.DeserializeLengthDelimited(stream, fieldOperation);
			return fieldOperation;
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x000ACAEC File Offset: 0x000AACEC
		public static FieldOperation DeserializeLengthDelimited(Stream stream, FieldOperation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FieldOperation.Deserialize(stream, instance, num);
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x000ACB14 File Offset: 0x000AAD14
		public static FieldOperation Deserialize(Stream stream, FieldOperation instance, long limit)
		{
			instance.Operation = FieldOperation.Types.OperationType.SET;
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
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Operation = (FieldOperation.Types.OperationType)ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.Field == null)
				{
					instance.Field = Field.DeserializeLengthDelimited(stream);
				}
				else
				{
					Field.DeserializeLengthDelimited(stream, instance.Field);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x000ACBCE File Offset: 0x000AADCE
		public void Serialize(Stream stream)
		{
			FieldOperation.Serialize(stream, this);
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x000ACBD8 File Offset: 0x000AADD8
		public static void Serialize(Stream stream, FieldOperation instance)
		{
			if (instance.Field == null)
			{
				throw new ArgumentNullException("Field", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Field.GetSerializedSize());
			Field.Serialize(stream, instance.Field);
			if (instance.HasOperation)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Operation));
			}
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x000ACC40 File Offset: 0x000AAE40
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Field.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasOperation)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Operation));
			}
			return num + 1U;
		}

		// Token: 0x04001412 RID: 5138
		public bool HasOperation;

		// Token: 0x04001413 RID: 5139
		private FieldOperation.Types.OperationType _Operation;

		// Token: 0x020006FC RID: 1788
		public static class Types
		{
			// Token: 0x02000713 RID: 1811
			public enum OperationType
			{
				// Token: 0x04002300 RID: 8960
				SET,
				// Token: 0x04002301 RID: 8961
				CLEAR
			}
		}
	}
}
