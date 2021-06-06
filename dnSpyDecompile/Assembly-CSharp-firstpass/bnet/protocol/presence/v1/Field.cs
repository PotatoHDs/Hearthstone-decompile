using System;
using System.IO;

namespace bnet.protocol.presence.v1
{
	// Token: 0x02000342 RID: 834
	public class Field : IProtoBuf
	{
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x060033CB RID: 13259 RVA: 0x000AC720 File Offset: 0x000AA920
		// (set) Token: 0x060033CC RID: 13260 RVA: 0x000AC728 File Offset: 0x000AA928
		public FieldKey Key { get; set; }

		// Token: 0x060033CD RID: 13261 RVA: 0x000AC731 File Offset: 0x000AA931
		public void SetKey(FieldKey val)
		{
			this.Key = val;
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x060033CE RID: 13262 RVA: 0x000AC73A File Offset: 0x000AA93A
		// (set) Token: 0x060033CF RID: 13263 RVA: 0x000AC742 File Offset: 0x000AA942
		public Variant Value { get; set; }

		// Token: 0x060033D0 RID: 13264 RVA: 0x000AC74B File Offset: 0x000AA94B
		public void SetValue(Variant val)
		{
			this.Value = val;
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x000AC754 File Offset: 0x000AA954
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Key.GetHashCode() ^ this.Value.GetHashCode();
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x000AC77C File Offset: 0x000AA97C
		public override bool Equals(object obj)
		{
			Field field = obj as Field;
			return field != null && this.Key.Equals(field.Key) && this.Value.Equals(field.Value);
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x060033D3 RID: 13267 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x000AC7C0 File Offset: 0x000AA9C0
		public static Field ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Field>(bs, 0, -1);
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x000AC7CA File Offset: 0x000AA9CA
		public void Deserialize(Stream stream)
		{
			Field.Deserialize(stream, this);
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x000AC7D4 File Offset: 0x000AA9D4
		public static Field Deserialize(Stream stream, Field instance)
		{
			return Field.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x000AC7E0 File Offset: 0x000AA9E0
		public static Field DeserializeLengthDelimited(Stream stream)
		{
			Field field = new Field();
			Field.DeserializeLengthDelimited(stream, field);
			return field;
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x000AC7FC File Offset: 0x000AA9FC
		public static Field DeserializeLengthDelimited(Stream stream, Field instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Field.Deserialize(stream, instance, num);
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x000AC824 File Offset: 0x000AAA24
		public static Field Deserialize(Stream stream, Field instance, long limit)
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
				else if (instance.Key == null)
				{
					instance.Key = FieldKey.DeserializeLengthDelimited(stream);
				}
				else
				{
					FieldKey.DeserializeLengthDelimited(stream, instance.Key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x000AC8F6 File Offset: 0x000AAAF6
		public void Serialize(Stream stream)
		{
			Field.Serialize(stream, this);
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x000AC900 File Offset: 0x000AAB00
		public static void Serialize(Stream stream, Field instance)
		{
			if (instance.Key == null)
			{
				throw new ArgumentNullException("Key", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Key.GetSerializedSize());
			FieldKey.Serialize(stream, instance.Key);
			if (instance.Value == null)
			{
				throw new ArgumentNullException("Value", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Value.GetSerializedSize());
			Variant.Serialize(stream, instance.Value);
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x000AC988 File Offset: 0x000AAB88
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Key.GetSerializedSize();
			uint num2 = num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize));
			uint serializedSize2 = this.Value.GetSerializedSize();
			return num2 + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + 2U;
		}
	}
}
