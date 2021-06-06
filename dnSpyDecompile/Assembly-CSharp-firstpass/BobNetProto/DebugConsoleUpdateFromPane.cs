using System;
using System.IO;
using System.Text;

namespace BobNetProto
{
	// Token: 0x020001DD RID: 477
	public class DebugConsoleUpdateFromPane : IProtoBuf
	{
		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x0006A402 File Offset: 0x00068602
		// (set) Token: 0x06001E54 RID: 7764 RVA: 0x0006A40A File Offset: 0x0006860A
		public string Name { get; set; }

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001E55 RID: 7765 RVA: 0x0006A413 File Offset: 0x00068613
		// (set) Token: 0x06001E56 RID: 7766 RVA: 0x0006A41B File Offset: 0x0006861B
		public string Value { get; set; }

		// Token: 0x06001E57 RID: 7767 RVA: 0x0006A424 File Offset: 0x00068624
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Name.GetHashCode() ^ this.Value.GetHashCode();
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x0006A44C File Offset: 0x0006864C
		public override bool Equals(object obj)
		{
			DebugConsoleUpdateFromPane debugConsoleUpdateFromPane = obj as DebugConsoleUpdateFromPane;
			return debugConsoleUpdateFromPane != null && this.Name.Equals(debugConsoleUpdateFromPane.Name) && this.Value.Equals(debugConsoleUpdateFromPane.Value);
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x0006A490 File Offset: 0x00068690
		public void Deserialize(Stream stream)
		{
			DebugConsoleUpdateFromPane.Deserialize(stream, this);
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x0006A49A File Offset: 0x0006869A
		public static DebugConsoleUpdateFromPane Deserialize(Stream stream, DebugConsoleUpdateFromPane instance)
		{
			return DebugConsoleUpdateFromPane.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x0006A4A8 File Offset: 0x000686A8
		public static DebugConsoleUpdateFromPane DeserializeLengthDelimited(Stream stream)
		{
			DebugConsoleUpdateFromPane debugConsoleUpdateFromPane = new DebugConsoleUpdateFromPane();
			DebugConsoleUpdateFromPane.DeserializeLengthDelimited(stream, debugConsoleUpdateFromPane);
			return debugConsoleUpdateFromPane;
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x0006A4C4 File Offset: 0x000686C4
		public static DebugConsoleUpdateFromPane DeserializeLengthDelimited(Stream stream, DebugConsoleUpdateFromPane instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DebugConsoleUpdateFromPane.Deserialize(stream, instance, num);
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x0006A4EC File Offset: 0x000686EC
		public static DebugConsoleUpdateFromPane Deserialize(Stream stream, DebugConsoleUpdateFromPane instance, long limit)
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

		// Token: 0x06001E5E RID: 7774 RVA: 0x0006A584 File Offset: 0x00068784
		public void Serialize(Stream stream)
		{
			DebugConsoleUpdateFromPane.Serialize(stream, this);
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x0006A590 File Offset: 0x00068790
		public static void Serialize(Stream stream, DebugConsoleUpdateFromPane instance)
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
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Value));
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x0006A60C File Offset: 0x0006880C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
			uint num2 = num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount);
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Value);
			return num2 + (ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2) + 2U;
		}

		// Token: 0x02000667 RID: 1639
		public enum PacketID
		{
			// Token: 0x04002168 RID: 8552
			ID = 145
		}
	}
}
