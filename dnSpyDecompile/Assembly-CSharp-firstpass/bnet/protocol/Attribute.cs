using System;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	// Token: 0x0200029E RID: 670
	public class Attribute : IProtoBuf
	{
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x00088A7D File Offset: 0x00086C7D
		// (set) Token: 0x0600263C RID: 9788 RVA: 0x00088A85 File Offset: 0x00086C85
		public string Name { get; set; }

		// Token: 0x0600263D RID: 9789 RVA: 0x00088A8E File Offset: 0x00086C8E
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x00088A97 File Offset: 0x00086C97
		// (set) Token: 0x0600263F RID: 9791 RVA: 0x00088A9F File Offset: 0x00086C9F
		public Variant Value { get; set; }

		// Token: 0x06002640 RID: 9792 RVA: 0x00088AA8 File Offset: 0x00086CA8
		public void SetValue(Variant val)
		{
			this.Value = val;
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x00088AB1 File Offset: 0x00086CB1
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Name.GetHashCode() ^ this.Value.GetHashCode();
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x00088AD8 File Offset: 0x00086CD8
		public override bool Equals(object obj)
		{
			Attribute attribute = obj as Attribute;
			return attribute != null && this.Name.Equals(attribute.Name) && this.Value.Equals(attribute.Value);
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06002643 RID: 9795 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x00088B1C File Offset: 0x00086D1C
		public static Attribute ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Attribute>(bs, 0, -1);
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x00088B26 File Offset: 0x00086D26
		public void Deserialize(Stream stream)
		{
			Attribute.Deserialize(stream, this);
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x00088B30 File Offset: 0x00086D30
		public static Attribute Deserialize(Stream stream, Attribute instance)
		{
			return Attribute.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x00088B3C File Offset: 0x00086D3C
		public static Attribute DeserializeLengthDelimited(Stream stream)
		{
			Attribute attribute = new Attribute();
			Attribute.DeserializeLengthDelimited(stream, attribute);
			return attribute;
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x00088B58 File Offset: 0x00086D58
		public static Attribute DeserializeLengthDelimited(Stream stream, Attribute instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Attribute.Deserialize(stream, instance, num);
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x00088B80 File Offset: 0x00086D80
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

		// Token: 0x0600264A RID: 9802 RVA: 0x00088C32 File Offset: 0x00086E32
		public void Serialize(Stream stream)
		{
			Attribute.Serialize(stream, this);
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x00088C3C File Offset: 0x00086E3C
		public static void Serialize(Stream stream, Attribute instance)
		{
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			if (instance.Value == null)
			{
				throw new ArgumentNullException("Value", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Value.GetSerializedSize());
			Variant.Serialize(stream, instance.Value);
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x00088CBC File Offset: 0x00086EBC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
			uint num2 = num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount);
			uint serializedSize = this.Value.GetSerializedSize();
			return num2 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 2U;
		}
	}
}
