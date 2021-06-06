using System;
using System.IO;

namespace BobNetProto
{
	// Token: 0x020001DE RID: 478
	public class DebugConsoleGetZones : IProtoBuf
	{
		// Token: 0x06001E62 RID: 7778 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x0006A650 File Offset: 0x00068850
		public override bool Equals(object obj)
		{
			return obj is DebugConsoleGetZones;
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x0006A65D File Offset: 0x0006885D
		public void Deserialize(Stream stream)
		{
			DebugConsoleGetZones.Deserialize(stream, this);
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x0006A667 File Offset: 0x00068867
		public static DebugConsoleGetZones Deserialize(Stream stream, DebugConsoleGetZones instance)
		{
			return DebugConsoleGetZones.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x0006A674 File Offset: 0x00068874
		public static DebugConsoleGetZones DeserializeLengthDelimited(Stream stream)
		{
			DebugConsoleGetZones debugConsoleGetZones = new DebugConsoleGetZones();
			DebugConsoleGetZones.DeserializeLengthDelimited(stream, debugConsoleGetZones);
			return debugConsoleGetZones;
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x0006A690 File Offset: 0x00068890
		public static DebugConsoleGetZones DeserializeLengthDelimited(Stream stream, DebugConsoleGetZones instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DebugConsoleGetZones.Deserialize(stream, instance, num);
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x0006A6B8 File Offset: 0x000688B8
		public static DebugConsoleGetZones Deserialize(Stream stream, DebugConsoleGetZones instance, long limit)
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

		// Token: 0x06001E69 RID: 7785 RVA: 0x0006A725 File Offset: 0x00068925
		public void Serialize(Stream stream)
		{
			DebugConsoleGetZones.Serialize(stream, this);
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, DebugConsoleGetZones instance)
		{
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000668 RID: 1640
		public enum PacketID
		{
			// Token: 0x0400216A RID: 8554
			ID = 147
		}
	}
}
