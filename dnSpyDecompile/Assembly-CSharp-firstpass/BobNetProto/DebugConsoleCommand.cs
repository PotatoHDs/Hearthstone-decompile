using System;
using System.IO;
using System.Text;

namespace BobNetProto
{
	// Token: 0x020001DC RID: 476
	public class DebugConsoleCommand : IProtoBuf
	{
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001E46 RID: 7750 RVA: 0x0006A262 File Offset: 0x00068462
		// (set) Token: 0x06001E47 RID: 7751 RVA: 0x0006A26A File Offset: 0x0006846A
		public string Command { get; set; }

		// Token: 0x06001E48 RID: 7752 RVA: 0x0006A273 File Offset: 0x00068473
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Command.GetHashCode();
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x0006A28C File Offset: 0x0006848C
		public override bool Equals(object obj)
		{
			DebugConsoleCommand debugConsoleCommand = obj as DebugConsoleCommand;
			return debugConsoleCommand != null && this.Command.Equals(debugConsoleCommand.Command);
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x0006A2BB File Offset: 0x000684BB
		public void Deserialize(Stream stream)
		{
			DebugConsoleCommand.Deserialize(stream, this);
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x0006A2C5 File Offset: 0x000684C5
		public static DebugConsoleCommand Deserialize(Stream stream, DebugConsoleCommand instance)
		{
			return DebugConsoleCommand.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x0006A2D0 File Offset: 0x000684D0
		public static DebugConsoleCommand DeserializeLengthDelimited(Stream stream)
		{
			DebugConsoleCommand debugConsoleCommand = new DebugConsoleCommand();
			DebugConsoleCommand.DeserializeLengthDelimited(stream, debugConsoleCommand);
			return debugConsoleCommand;
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x0006A2EC File Offset: 0x000684EC
		public static DebugConsoleCommand DeserializeLengthDelimited(Stream stream, DebugConsoleCommand instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DebugConsoleCommand.Deserialize(stream, instance, num);
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x0006A314 File Offset: 0x00068514
		public static DebugConsoleCommand Deserialize(Stream stream, DebugConsoleCommand instance, long limit)
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
				else if (num == 10)
				{
					instance.Command = ProtocolParser.ReadString(stream);
				}
				else
				{
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

		// Token: 0x06001E4F RID: 7759 RVA: 0x0006A394 File Offset: 0x00068594
		public void Serialize(Stream stream)
		{
			DebugConsoleCommand.Serialize(stream, this);
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x0006A39D File Offset: 0x0006859D
		public static void Serialize(Stream stream, DebugConsoleCommand instance)
		{
			if (instance.Command == null)
			{
				throw new ArgumentNullException("Command", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Command));
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x0006A3D8 File Offset: 0x000685D8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Command);
			return num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + 1U;
		}

		// Token: 0x02000666 RID: 1638
		public enum PacketID
		{
			// Token: 0x04002166 RID: 8550
			ID = 123
		}
	}
}
