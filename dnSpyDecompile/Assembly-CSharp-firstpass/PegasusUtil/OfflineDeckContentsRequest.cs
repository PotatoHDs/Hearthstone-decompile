using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000F7 RID: 247
	public class OfflineDeckContentsRequest : IProtoBuf
	{
		// Token: 0x06001073 RID: 4211 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0003A3AA File Offset: 0x000385AA
		public override bool Equals(object obj)
		{
			return obj is OfflineDeckContentsRequest;
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0003A3B7 File Offset: 0x000385B7
		public void Deserialize(Stream stream)
		{
			OfflineDeckContentsRequest.Deserialize(stream, this);
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0003A3C1 File Offset: 0x000385C1
		public static OfflineDeckContentsRequest Deserialize(Stream stream, OfflineDeckContentsRequest instance)
		{
			return OfflineDeckContentsRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0003A3CC File Offset: 0x000385CC
		public static OfflineDeckContentsRequest DeserializeLengthDelimited(Stream stream)
		{
			OfflineDeckContentsRequest offlineDeckContentsRequest = new OfflineDeckContentsRequest();
			OfflineDeckContentsRequest.DeserializeLengthDelimited(stream, offlineDeckContentsRequest);
			return offlineDeckContentsRequest;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0003A3E8 File Offset: 0x000385E8
		public static OfflineDeckContentsRequest DeserializeLengthDelimited(Stream stream, OfflineDeckContentsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return OfflineDeckContentsRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0003A410 File Offset: 0x00038610
		public static OfflineDeckContentsRequest Deserialize(Stream stream, OfflineDeckContentsRequest instance, long limit)
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

		// Token: 0x0600107A RID: 4218 RVA: 0x0003A47D File Offset: 0x0003867D
		public void Serialize(Stream stream)
		{
			OfflineDeckContentsRequest.Serialize(stream, this);
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, OfflineDeckContentsRequest instance)
		{
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x020005FB RID: 1531
		public enum PacketID
		{
			// Token: 0x0400202E RID: 8238
			ID = 371,
			// Token: 0x0400202F RID: 8239
			System = 0
		}
	}
}
