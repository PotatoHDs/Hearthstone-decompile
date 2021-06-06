using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000062 RID: 98
	public class DraftMakePick : IProtoBuf
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001807E File Offset: 0x0001627E
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x00018086 File Offset: 0x00016286
		public long DeckId { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001808F File Offset: 0x0001628F
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x00018097 File Offset: 0x00016297
		public int Slot { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x000180A0 File Offset: 0x000162A0
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x000180A8 File Offset: 0x000162A8
		public int Index { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x000180B1 File Offset: 0x000162B1
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x000180B9 File Offset: 0x000162B9
		public int Premium { get; set; }

		// Token: 0x06000634 RID: 1588 RVA: 0x000180C4 File Offset: 0x000162C4
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.DeckId.GetHashCode() ^ this.Slot.GetHashCode() ^ this.Index.GetHashCode() ^ this.Premium.GetHashCode();
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00018118 File Offset: 0x00016318
		public override bool Equals(object obj)
		{
			DraftMakePick draftMakePick = obj as DraftMakePick;
			return draftMakePick != null && this.DeckId.Equals(draftMakePick.DeckId) && this.Slot.Equals(draftMakePick.Slot) && this.Index.Equals(draftMakePick.Index) && this.Premium.Equals(draftMakePick.Premium);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00018192 File Offset: 0x00016392
		public void Deserialize(Stream stream)
		{
			DraftMakePick.Deserialize(stream, this);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001819C File Offset: 0x0001639C
		public static DraftMakePick Deserialize(Stream stream, DraftMakePick instance)
		{
			return DraftMakePick.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x000181A8 File Offset: 0x000163A8
		public static DraftMakePick DeserializeLengthDelimited(Stream stream)
		{
			DraftMakePick draftMakePick = new DraftMakePick();
			DraftMakePick.DeserializeLengthDelimited(stream, draftMakePick);
			return draftMakePick;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000181C4 File Offset: 0x000163C4
		public static DraftMakePick DeserializeLengthDelimited(Stream stream, DraftMakePick instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DraftMakePick.Deserialize(stream, instance, num);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x000181EC File Offset: 0x000163EC
		public static DraftMakePick Deserialize(Stream stream, DraftMakePick instance, long limit)
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
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Slot = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Index = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.Premium = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
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

		// Token: 0x0600063B RID: 1595 RVA: 0x000182BF File Offset: 0x000164BF
		public void Serialize(Stream stream)
		{
			DraftMakePick.Serialize(stream, this);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x000182C8 File Offset: 0x000164C8
		public static void Serialize(Stream stream, DraftMakePick instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Slot));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Index));
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Premium));
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00018327 File Offset: 0x00016527
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)this.DeckId) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Slot)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Index)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Premium)) + 4U;
		}

		// Token: 0x02000574 RID: 1396
		public enum PacketID
		{
			// Token: 0x04001EB2 RID: 7858
			ID = 245,
			// Token: 0x04001EB3 RID: 7859
			System = 0
		}
	}
}
