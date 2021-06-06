using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000138 RID: 312
	public class ProfileNoticeTavernBrawlTicket : IProtoBuf
	{
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x00046599 File Offset: 0x00044799
		// (set) Token: 0x0600147B RID: 5243 RVA: 0x000465A1 File Offset: 0x000447A1
		public int TicketType { get; set; }

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x000465AA File Offset: 0x000447AA
		// (set) Token: 0x0600147D RID: 5245 RVA: 0x000465B2 File Offset: 0x000447B2
		public int Quantity { get; set; }

		// Token: 0x0600147E RID: 5246 RVA: 0x000465BC File Offset: 0x000447BC
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.TicketType.GetHashCode() ^ this.Quantity.GetHashCode();
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x000465F4 File Offset: 0x000447F4
		public override bool Equals(object obj)
		{
			ProfileNoticeTavernBrawlTicket profileNoticeTavernBrawlTicket = obj as ProfileNoticeTavernBrawlTicket;
			return profileNoticeTavernBrawlTicket != null && this.TicketType.Equals(profileNoticeTavernBrawlTicket.TicketType) && this.Quantity.Equals(profileNoticeTavernBrawlTicket.Quantity);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0004663E File Offset: 0x0004483E
		public void Deserialize(Stream stream)
		{
			ProfileNoticeTavernBrawlTicket.Deserialize(stream, this);
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00046648 File Offset: 0x00044848
		public static ProfileNoticeTavernBrawlTicket Deserialize(Stream stream, ProfileNoticeTavernBrawlTicket instance)
		{
			return ProfileNoticeTavernBrawlTicket.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00046654 File Offset: 0x00044854
		public static ProfileNoticeTavernBrawlTicket DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeTavernBrawlTicket profileNoticeTavernBrawlTicket = new ProfileNoticeTavernBrawlTicket();
			ProfileNoticeTavernBrawlTicket.DeserializeLengthDelimited(stream, profileNoticeTavernBrawlTicket);
			return profileNoticeTavernBrawlTicket;
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00046670 File Offset: 0x00044870
		public static ProfileNoticeTavernBrawlTicket DeserializeLengthDelimited(Stream stream, ProfileNoticeTavernBrawlTicket instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeTavernBrawlTicket.Deserialize(stream, instance, num);
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00046698 File Offset: 0x00044898
		public static ProfileNoticeTavernBrawlTicket Deserialize(Stream stream, ProfileNoticeTavernBrawlTicket instance, long limit)
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
						instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.TicketType = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00046731 File Offset: 0x00044931
		public void Serialize(Stream stream)
		{
			ProfileNoticeTavernBrawlTicket.Serialize(stream, this);
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0004673A File Offset: 0x0004493A
		public static void Serialize(Stream stream, ProfileNoticeTavernBrawlTicket instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.TicketType));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Quantity));
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00046765 File Offset: 0x00044965
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.TicketType)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Quantity)) + 2U;
		}

		// Token: 0x0200062C RID: 1580
		public enum NoticeID
		{
			// Token: 0x040020B2 RID: 8370
			ID = 18
		}
	}
}
