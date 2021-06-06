using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200007C RID: 124
	public class BattlegroundsRatingInfoRequest : IProtoBuf
	{
		// Token: 0x060007C4 RID: 1988 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001C687 File Offset: 0x0001A887
		public override bool Equals(object obj)
		{
			return obj is BattlegroundsRatingInfoRequest;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001C694 File Offset: 0x0001A894
		public void Deserialize(Stream stream)
		{
			BattlegroundsRatingInfoRequest.Deserialize(stream, this);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001C69E File Offset: 0x0001A89E
		public static BattlegroundsRatingInfoRequest Deserialize(Stream stream, BattlegroundsRatingInfoRequest instance)
		{
			return BattlegroundsRatingInfoRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001C6AC File Offset: 0x0001A8AC
		public static BattlegroundsRatingInfoRequest DeserializeLengthDelimited(Stream stream)
		{
			BattlegroundsRatingInfoRequest battlegroundsRatingInfoRequest = new BattlegroundsRatingInfoRequest();
			BattlegroundsRatingInfoRequest.DeserializeLengthDelimited(stream, battlegroundsRatingInfoRequest);
			return battlegroundsRatingInfoRequest;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001C6C8 File Offset: 0x0001A8C8
		public static BattlegroundsRatingInfoRequest DeserializeLengthDelimited(Stream stream, BattlegroundsRatingInfoRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BattlegroundsRatingInfoRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001C6F0 File Offset: 0x0001A8F0
		public static BattlegroundsRatingInfoRequest Deserialize(Stream stream, BattlegroundsRatingInfoRequest instance, long limit)
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

		// Token: 0x060007CB RID: 1995 RVA: 0x0001C75D File Offset: 0x0001A95D
		public void Serialize(Stream stream)
		{
			BattlegroundsRatingInfoRequest.Serialize(stream, this);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, BattlegroundsRatingInfoRequest instance)
		{
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x0200058F RID: 1423
		public enum PacketID
		{
			// Token: 0x04001F09 RID: 7945
			ID = 372,
			// Token: 0x04001F0A RID: 7946
			System = 0
		}
	}
}
