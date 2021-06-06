using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200008F RID: 143
	public class DraftRewardsAcked : IProtoBuf
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00022FF5 File Offset: 0x000211F5
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x00022FFD File Offset: 0x000211FD
		public long DeckId { get; set; }

		// Token: 0x06000989 RID: 2441 RVA: 0x00023008 File Offset: 0x00021208
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.DeckId.GetHashCode();
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00023030 File Offset: 0x00021230
		public override bool Equals(object obj)
		{
			DraftRewardsAcked draftRewardsAcked = obj as DraftRewardsAcked;
			return draftRewardsAcked != null && this.DeckId.Equals(draftRewardsAcked.DeckId);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00023062 File Offset: 0x00021262
		public void Deserialize(Stream stream)
		{
			DraftRewardsAcked.Deserialize(stream, this);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0002306C File Offset: 0x0002126C
		public static DraftRewardsAcked Deserialize(Stream stream, DraftRewardsAcked instance)
		{
			return DraftRewardsAcked.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00023078 File Offset: 0x00021278
		public static DraftRewardsAcked DeserializeLengthDelimited(Stream stream)
		{
			DraftRewardsAcked draftRewardsAcked = new DraftRewardsAcked();
			DraftRewardsAcked.DeserializeLengthDelimited(stream, draftRewardsAcked);
			return draftRewardsAcked;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00023094 File Offset: 0x00021294
		public static DraftRewardsAcked DeserializeLengthDelimited(Stream stream, DraftRewardsAcked instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DraftRewardsAcked.Deserialize(stream, instance, num);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x000230BC File Offset: 0x000212BC
		public static DraftRewardsAcked Deserialize(Stream stream, DraftRewardsAcked instance, long limit)
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
					instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06000990 RID: 2448 RVA: 0x0002313B File Offset: 0x0002133B
		public void Serialize(Stream stream)
		{
			DraftRewardsAcked.Serialize(stream, this);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00023144 File Offset: 0x00021344
		public static void Serialize(Stream stream, DraftRewardsAcked instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00023159 File Offset: 0x00021359
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)this.DeckId) + 1U;
		}

		// Token: 0x020005A3 RID: 1443
		public enum PacketID
		{
			// Token: 0x04001F4B RID: 8011
			ID = 288
		}
	}
}
