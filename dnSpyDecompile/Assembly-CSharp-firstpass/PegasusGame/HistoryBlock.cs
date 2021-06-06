using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001BB RID: 443
	public class HistoryBlock : IProtoBuf
	{
		// Token: 0x06001C28 RID: 7208 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x0006358D File Offset: 0x0006178D
		public override bool Equals(object obj)
		{
			return obj is HistoryBlock;
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x0006359A File Offset: 0x0006179A
		public void Deserialize(Stream stream)
		{
			HistoryBlock.Deserialize(stream, this);
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x000635A4 File Offset: 0x000617A4
		public static HistoryBlock Deserialize(Stream stream, HistoryBlock instance)
		{
			return HistoryBlock.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x000635B0 File Offset: 0x000617B0
		public static HistoryBlock DeserializeLengthDelimited(Stream stream)
		{
			HistoryBlock historyBlock = new HistoryBlock();
			HistoryBlock.DeserializeLengthDelimited(stream, historyBlock);
			return historyBlock;
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x000635CC File Offset: 0x000617CC
		public static HistoryBlock DeserializeLengthDelimited(Stream stream, HistoryBlock instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return HistoryBlock.Deserialize(stream, instance, num);
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x000635F4 File Offset: 0x000617F4
		public static HistoryBlock Deserialize(Stream stream, HistoryBlock instance, long limit)
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

		// Token: 0x06001C2F RID: 7215 RVA: 0x00063661 File Offset: 0x00061861
		public void Serialize(Stream stream)
		{
			HistoryBlock.Serialize(stream, this);
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, HistoryBlock instance)
		{
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000652 RID: 1618
		public enum Type
		{
			// Token: 0x04002118 RID: 8472
			INVALID,
			// Token: 0x04002119 RID: 8473
			ATTACK,
			// Token: 0x0400211A RID: 8474
			JOUST,
			// Token: 0x0400211B RID: 8475
			POWER,
			// Token: 0x0400211C RID: 8476
			TRIGGER = 5,
			// Token: 0x0400211D RID: 8477
			DEATHS,
			// Token: 0x0400211E RID: 8478
			PLAY,
			// Token: 0x0400211F RID: 8479
			FATIGUE,
			// Token: 0x04002120 RID: 8480
			RITUAL,
			// Token: 0x04002121 RID: 8481
			REVEAL_CARD,
			// Token: 0x04002122 RID: 8482
			GAME_RESET,
			// Token: 0x04002123 RID: 8483
			MOVE_MINION
		}
	}
}
