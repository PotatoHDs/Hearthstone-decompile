using System;
using System.IO;

namespace BobNetProto
{
	// Token: 0x020001DB RID: 475
	public class DebugConsoleGetCmdList : IProtoBuf
	{
		// Token: 0x06001E3B RID: 7739 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x0006A186 File Offset: 0x00068386
		public override bool Equals(object obj)
		{
			return obj is DebugConsoleGetCmdList;
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x0006A193 File Offset: 0x00068393
		public void Deserialize(Stream stream)
		{
			DebugConsoleGetCmdList.Deserialize(stream, this);
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x0006A19D File Offset: 0x0006839D
		public static DebugConsoleGetCmdList Deserialize(Stream stream, DebugConsoleGetCmdList instance)
		{
			return DebugConsoleGetCmdList.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x0006A1A8 File Offset: 0x000683A8
		public static DebugConsoleGetCmdList DeserializeLengthDelimited(Stream stream)
		{
			DebugConsoleGetCmdList debugConsoleGetCmdList = new DebugConsoleGetCmdList();
			DebugConsoleGetCmdList.DeserializeLengthDelimited(stream, debugConsoleGetCmdList);
			return debugConsoleGetCmdList;
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x0006A1C4 File Offset: 0x000683C4
		public static DebugConsoleGetCmdList DeserializeLengthDelimited(Stream stream, DebugConsoleGetCmdList instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DebugConsoleGetCmdList.Deserialize(stream, instance, num);
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x0006A1EC File Offset: 0x000683EC
		public static DebugConsoleGetCmdList Deserialize(Stream stream, DebugConsoleGetCmdList instance, long limit)
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

		// Token: 0x06001E42 RID: 7746 RVA: 0x0006A259 File Offset: 0x00068459
		public void Serialize(Stream stream)
		{
			DebugConsoleGetCmdList.Serialize(stream, this);
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, DebugConsoleGetCmdList instance)
		{
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000665 RID: 1637
		public enum PacketID
		{
			// Token: 0x04002164 RID: 8548
			ID = 125
		}
	}
}
