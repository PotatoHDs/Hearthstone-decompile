using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000061 RID: 97
	public class DraftGetChoicesAndContents : IProtoBuf
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00017FA1 File Offset: 0x000161A1
		public override bool Equals(object obj)
		{
			return obj is DraftGetChoicesAndContents;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00017FAE File Offset: 0x000161AE
		public void Deserialize(Stream stream)
		{
			DraftGetChoicesAndContents.Deserialize(stream, this);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00017FB8 File Offset: 0x000161B8
		public static DraftGetChoicesAndContents Deserialize(Stream stream, DraftGetChoicesAndContents instance)
		{
			return DraftGetChoicesAndContents.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00017FC4 File Offset: 0x000161C4
		public static DraftGetChoicesAndContents DeserializeLengthDelimited(Stream stream)
		{
			DraftGetChoicesAndContents draftGetChoicesAndContents = new DraftGetChoicesAndContents();
			DraftGetChoicesAndContents.DeserializeLengthDelimited(stream, draftGetChoicesAndContents);
			return draftGetChoicesAndContents;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00017FE0 File Offset: 0x000161E0
		public static DraftGetChoicesAndContents DeserializeLengthDelimited(Stream stream, DraftGetChoicesAndContents instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DraftGetChoicesAndContents.Deserialize(stream, instance, num);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00018008 File Offset: 0x00016208
		public static DraftGetChoicesAndContents Deserialize(Stream stream, DraftGetChoicesAndContents instance, long limit)
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

		// Token: 0x06000628 RID: 1576 RVA: 0x00018075 File Offset: 0x00016275
		public void Serialize(Stream stream)
		{
			DraftGetChoicesAndContents.Serialize(stream, this);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, DraftGetChoicesAndContents instance)
		{
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000573 RID: 1395
		public enum PacketID
		{
			// Token: 0x04001EAF RID: 7855
			ID = 244,
			// Token: 0x04001EB0 RID: 7856
			System = 0
		}
	}
}
