using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001C9 RID: 457
	public class PowerHistoryShuffleDeck : IProtoBuf
	{
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x000665F9 File Offset: 0x000647F9
		// (set) Token: 0x06001D17 RID: 7447 RVA: 0x00066601 File Offset: 0x00064801
		public int PlayerId { get; set; }

		// Token: 0x06001D18 RID: 7448 RVA: 0x0006660C File Offset: 0x0006480C
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.PlayerId.GetHashCode();
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x00066634 File Offset: 0x00064834
		public override bool Equals(object obj)
		{
			PowerHistoryShuffleDeck powerHistoryShuffleDeck = obj as PowerHistoryShuffleDeck;
			return powerHistoryShuffleDeck != null && this.PlayerId.Equals(powerHistoryShuffleDeck.PlayerId);
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x00066666 File Offset: 0x00064866
		public void Deserialize(Stream stream)
		{
			PowerHistoryShuffleDeck.Deserialize(stream, this);
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x00066670 File Offset: 0x00064870
		public static PowerHistoryShuffleDeck Deserialize(Stream stream, PowerHistoryShuffleDeck instance)
		{
			return PowerHistoryShuffleDeck.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x0006667C File Offset: 0x0006487C
		public static PowerHistoryShuffleDeck DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryShuffleDeck powerHistoryShuffleDeck = new PowerHistoryShuffleDeck();
			PowerHistoryShuffleDeck.DeserializeLengthDelimited(stream, powerHistoryShuffleDeck);
			return powerHistoryShuffleDeck;
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x00066698 File Offset: 0x00064898
		public static PowerHistoryShuffleDeck DeserializeLengthDelimited(Stream stream, PowerHistoryShuffleDeck instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PowerHistoryShuffleDeck.Deserialize(stream, instance, num);
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x000666C0 File Offset: 0x000648C0
		public static PowerHistoryShuffleDeck Deserialize(Stream stream, PowerHistoryShuffleDeck instance, long limit)
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
				else if (num == 8)
				{
					instance.PlayerId = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001D1F RID: 7455 RVA: 0x00066740 File Offset: 0x00064940
		public void Serialize(Stream stream)
		{
			PowerHistoryShuffleDeck.Serialize(stream, this);
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x00066749 File Offset: 0x00064949
		public static void Serialize(Stream stream, PowerHistoryShuffleDeck instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PlayerId));
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x0006675F File Offset: 0x0006495F
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.PlayerId)) + 1U;
		}
	}
}
