using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000060 RID: 96
	public class DraftAckRewards : IProtoBuf
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00017DB8 File Offset: 0x00015FB8
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x00017DC0 File Offset: 0x00015FC0
		public long DeckId { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00017DC9 File Offset: 0x00015FC9
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x00017DD1 File Offset: 0x00015FD1
		public int Slot { get; set; }

		// Token: 0x06000616 RID: 1558 RVA: 0x00017DDC File Offset: 0x00015FDC
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.DeckId.GetHashCode() ^ this.Slot.GetHashCode();
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00017E14 File Offset: 0x00016014
		public override bool Equals(object obj)
		{
			DraftAckRewards draftAckRewards = obj as DraftAckRewards;
			return draftAckRewards != null && this.DeckId.Equals(draftAckRewards.DeckId) && this.Slot.Equals(draftAckRewards.Slot);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00017E5E File Offset: 0x0001605E
		public void Deserialize(Stream stream)
		{
			DraftAckRewards.Deserialize(stream, this);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00017E68 File Offset: 0x00016068
		public static DraftAckRewards Deserialize(Stream stream, DraftAckRewards instance)
		{
			return DraftAckRewards.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00017E74 File Offset: 0x00016074
		public static DraftAckRewards DeserializeLengthDelimited(Stream stream)
		{
			DraftAckRewards draftAckRewards = new DraftAckRewards();
			DraftAckRewards.DeserializeLengthDelimited(stream, draftAckRewards);
			return draftAckRewards;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00017E90 File Offset: 0x00016090
		public static DraftAckRewards DeserializeLengthDelimited(Stream stream, DraftAckRewards instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DraftAckRewards.Deserialize(stream, instance, num);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00017EB8 File Offset: 0x000160B8
		public static DraftAckRewards Deserialize(Stream stream, DraftAckRewards instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Slot = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00017F50 File Offset: 0x00016150
		public void Serialize(Stream stream)
		{
			DraftAckRewards.Serialize(stream, this);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00017F59 File Offset: 0x00016159
		public static void Serialize(Stream stream, DraftAckRewards instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Slot));
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00017F83 File Offset: 0x00016183
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)this.DeckId) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Slot)) + 2U;
		}

		// Token: 0x02000572 RID: 1394
		public enum PacketID
		{
			// Token: 0x04001EAC RID: 7852
			ID = 287,
			// Token: 0x04001EAD RID: 7853
			System = 0
		}
	}
}
