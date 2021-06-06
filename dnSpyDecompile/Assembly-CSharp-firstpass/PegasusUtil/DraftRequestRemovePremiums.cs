using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000EB RID: 235
	public class DraftRequestRemovePremiums : IProtoBuf
	{
		// Token: 0x06000FCF RID: 4047 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00038A86 File Offset: 0x00036C86
		public override bool Equals(object obj)
		{
			return obj is DraftRequestRemovePremiums;
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00038A93 File Offset: 0x00036C93
		public void Deserialize(Stream stream)
		{
			DraftRequestRemovePremiums.Deserialize(stream, this);
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00038A9D File Offset: 0x00036C9D
		public static DraftRequestRemovePremiums Deserialize(Stream stream, DraftRequestRemovePremiums instance)
		{
			return DraftRequestRemovePremiums.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00038AA8 File Offset: 0x00036CA8
		public static DraftRequestRemovePremiums DeserializeLengthDelimited(Stream stream)
		{
			DraftRequestRemovePremiums draftRequestRemovePremiums = new DraftRequestRemovePremiums();
			DraftRequestRemovePremiums.DeserializeLengthDelimited(stream, draftRequestRemovePremiums);
			return draftRequestRemovePremiums;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00038AC4 File Offset: 0x00036CC4
		public static DraftRequestRemovePremiums DeserializeLengthDelimited(Stream stream, DraftRequestRemovePremiums instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DraftRequestRemovePremiums.Deserialize(stream, instance, num);
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00038AEC File Offset: 0x00036CEC
		public static DraftRequestRemovePremiums Deserialize(Stream stream, DraftRequestRemovePremiums instance, long limit)
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

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00038B59 File Offset: 0x00036D59
		public void Serialize(Stream stream)
		{
			DraftRequestRemovePremiums.Serialize(stream, this);
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, DraftRequestRemovePremiums instance)
		{
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x020005EF RID: 1519
		public enum PacketID
		{
			// Token: 0x04002010 RID: 8208
			ID = 354,
			// Token: 0x04002011 RID: 8209
			System = 0
		}
	}
}
