using System;
using System.IO;
using System.Text;

namespace PegasusGame
{
	// Token: 0x020001B3 RID: 435
	public class DebugMessage : IProtoBuf
	{
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x000611EF File Offset: 0x0005F3EF
		// (set) Token: 0x06001B87 RID: 7047 RVA: 0x000611F7 File Offset: 0x0005F3F7
		public string Message { get; set; }

		// Token: 0x06001B88 RID: 7048 RVA: 0x00061200 File Offset: 0x0005F400
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Message.GetHashCode();
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x0006121C File Offset: 0x0005F41C
		public override bool Equals(object obj)
		{
			DebugMessage debugMessage = obj as DebugMessage;
			return debugMessage != null && this.Message.Equals(debugMessage.Message);
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x0006124B File Offset: 0x0005F44B
		public void Deserialize(Stream stream)
		{
			DebugMessage.Deserialize(stream, this);
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x00061255 File Offset: 0x0005F455
		public static DebugMessage Deserialize(Stream stream, DebugMessage instance)
		{
			return DebugMessage.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x00061260 File Offset: 0x0005F460
		public static DebugMessage DeserializeLengthDelimited(Stream stream)
		{
			DebugMessage debugMessage = new DebugMessage();
			DebugMessage.DeserializeLengthDelimited(stream, debugMessage);
			return debugMessage;
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x0006127C File Offset: 0x0005F47C
		public static DebugMessage DeserializeLengthDelimited(Stream stream, DebugMessage instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DebugMessage.Deserialize(stream, instance, num);
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x000612A4 File Offset: 0x0005F4A4
		public static DebugMessage Deserialize(Stream stream, DebugMessage instance, long limit)
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
					instance.Message = ProtocolParser.ReadString(stream);
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

		// Token: 0x06001B8F RID: 7055 RVA: 0x00061324 File Offset: 0x0005F524
		public void Serialize(Stream stream)
		{
			DebugMessage.Serialize(stream, this);
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x0006132D File Offset: 0x0005F52D
		public static void Serialize(Stream stream, DebugMessage instance)
		{
			if (instance.Message == null)
			{
				throw new ArgumentNullException("Message", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Message));
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x00061368 File Offset: 0x0005F568
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Message);
			return num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + 1U;
		}

		// Token: 0x0200064A RID: 1610
		public enum PacketID
		{
			// Token: 0x04002108 RID: 8456
			ID = 5
		}
	}
}
