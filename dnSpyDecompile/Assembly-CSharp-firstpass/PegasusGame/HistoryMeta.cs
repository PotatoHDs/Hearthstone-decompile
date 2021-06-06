using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001BC RID: 444
	public class HistoryMeta : IProtoBuf
	{
		// Token: 0x06001C33 RID: 7219 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x0006366A File Offset: 0x0006186A
		public override bool Equals(object obj)
		{
			return obj is HistoryMeta;
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x00063677 File Offset: 0x00061877
		public void Deserialize(Stream stream)
		{
			HistoryMeta.Deserialize(stream, this);
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x00063681 File Offset: 0x00061881
		public static HistoryMeta Deserialize(Stream stream, HistoryMeta instance)
		{
			return HistoryMeta.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x0006368C File Offset: 0x0006188C
		public static HistoryMeta DeserializeLengthDelimited(Stream stream)
		{
			HistoryMeta historyMeta = new HistoryMeta();
			HistoryMeta.DeserializeLengthDelimited(stream, historyMeta);
			return historyMeta;
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x000636A8 File Offset: 0x000618A8
		public static HistoryMeta DeserializeLengthDelimited(Stream stream, HistoryMeta instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return HistoryMeta.Deserialize(stream, instance, num);
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x000636D0 File Offset: 0x000618D0
		public static HistoryMeta Deserialize(Stream stream, HistoryMeta instance, long limit)
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

		// Token: 0x06001C3A RID: 7226 RVA: 0x0006373D File Offset: 0x0006193D
		public void Serialize(Stream stream)
		{
			HistoryMeta.Serialize(stream, this);
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, HistoryMeta instance)
		{
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000653 RID: 1619
		public enum Type
		{
			// Token: 0x04002125 RID: 8485
			TARGET,
			// Token: 0x04002126 RID: 8486
			DAMAGE,
			// Token: 0x04002127 RID: 8487
			HEALING,
			// Token: 0x04002128 RID: 8488
			JOUST,
			// Token: 0x04002129 RID: 8489
			SHOW_BIG_CARD = 5,
			// Token: 0x0400212A RID: 8490
			EFFECT_TIMING,
			// Token: 0x0400212B RID: 8491
			HISTORY_TARGET,
			// Token: 0x0400212C RID: 8492
			OVERRIDE_HISTORY,
			// Token: 0x0400212D RID: 8493
			HISTORY_TARGET_DONT_DUPLICATE_UNTIL_END,
			// Token: 0x0400212E RID: 8494
			BEGIN_ARTIFICIAL_HISTORY_TILE,
			// Token: 0x0400212F RID: 8495
			BEGIN_ARTIFICIAL_HISTORY_TRIGGER_TILE,
			// Token: 0x04002130 RID: 8496
			END_ARTIFICIAL_HISTORY_TILE,
			// Token: 0x04002131 RID: 8497
			START_DRAW,
			// Token: 0x04002132 RID: 8498
			BURNED_CARD,
			// Token: 0x04002133 RID: 8499
			EFFECT_SELECTION,
			// Token: 0x04002134 RID: 8500
			BEGIN_LISTENING_FOR_TURN_EVENTS,
			// Token: 0x04002135 RID: 8501
			HOLD_DRAWN_CARD,
			// Token: 0x04002136 RID: 8502
			CONTROLLER_AND_ZONE_CHANGE,
			// Token: 0x04002137 RID: 8503
			ARTIFICIAL_PAUSE,
			// Token: 0x04002138 RID: 8504
			SLUSH_TIME,
			// Token: 0x04002139 RID: 8505
			ARTIFICIAL_HISTORY_INTERRUPT,
			// Token: 0x0400213A RID: 8506
			POISONOUS,
			// Token: 0x0400213B RID: 8507
			STUB_20_6_LETTUCE
		}
	}
}
