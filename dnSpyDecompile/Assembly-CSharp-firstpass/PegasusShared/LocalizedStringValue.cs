using System;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x0200014A RID: 330
	public class LocalizedStringValue : IProtoBuf
	{
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x0004AD81 File Offset: 0x00048F81
		// (set) Token: 0x060015D5 RID: 5589 RVA: 0x0004AD89 File Offset: 0x00048F89
		public int Locale { get; set; }

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x0004AD92 File Offset: 0x00048F92
		// (set) Token: 0x060015D7 RID: 5591 RVA: 0x0004AD9A File Offset: 0x00048F9A
		public string Value { get; set; }

		// Token: 0x060015D8 RID: 5592 RVA: 0x0004ADA4 File Offset: 0x00048FA4
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Locale.GetHashCode() ^ this.Value.GetHashCode();
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0004ADD8 File Offset: 0x00048FD8
		public override bool Equals(object obj)
		{
			LocalizedStringValue localizedStringValue = obj as LocalizedStringValue;
			return localizedStringValue != null && this.Locale.Equals(localizedStringValue.Locale) && this.Value.Equals(localizedStringValue.Value);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0004AE1F File Offset: 0x0004901F
		public void Deserialize(Stream stream)
		{
			LocalizedStringValue.Deserialize(stream, this);
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0004AE29 File Offset: 0x00049029
		public static LocalizedStringValue Deserialize(Stream stream, LocalizedStringValue instance)
		{
			return LocalizedStringValue.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x0004AE34 File Offset: 0x00049034
		public static LocalizedStringValue DeserializeLengthDelimited(Stream stream)
		{
			LocalizedStringValue localizedStringValue = new LocalizedStringValue();
			LocalizedStringValue.DeserializeLengthDelimited(stream, localizedStringValue);
			return localizedStringValue;
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x0004AE50 File Offset: 0x00049050
		public static LocalizedStringValue DeserializeLengthDelimited(Stream stream, LocalizedStringValue instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LocalizedStringValue.Deserialize(stream, instance, num);
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x0004AE78 File Offset: 0x00049078
		public static LocalizedStringValue Deserialize(Stream stream, LocalizedStringValue instance, long limit)
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
					instance.Locale = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0004AF10 File Offset: 0x00049110
		public void Serialize(Stream stream)
		{
			LocalizedStringValue.Serialize(stream, this);
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0004AF1C File Offset: 0x0004911C
		public static void Serialize(Stream stream, LocalizedStringValue instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Locale));
			if (instance.Value == null)
			{
				throw new ArgumentNullException("Value", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Value));
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x0004AF74 File Offset: 0x00049174
		public uint GetSerializedSize()
		{
			uint num = 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Locale));
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Value);
			return num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + 2U;
		}
	}
}
