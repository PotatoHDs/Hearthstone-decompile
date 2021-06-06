using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200009E RID: 158
	public class PlayerDraftTickets : IProtoBuf
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x00026F0C File Offset: 0x0002510C
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x00026F14 File Offset: 0x00025114
		public int UnusedTicketBalance
		{
			get
			{
				return this._UnusedTicketBalance;
			}
			set
			{
				this._UnusedTicketBalance = value;
				this.HasUnusedTicketBalance = true;
			}
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00026F24 File Offset: 0x00025124
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasUnusedTicketBalance)
			{
				num ^= this.UnusedTicketBalance.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00026F58 File Offset: 0x00025158
		public override bool Equals(object obj)
		{
			PlayerDraftTickets playerDraftTickets = obj as PlayerDraftTickets;
			return playerDraftTickets != null && this.HasUnusedTicketBalance == playerDraftTickets.HasUnusedTicketBalance && (!this.HasUnusedTicketBalance || this.UnusedTicketBalance.Equals(playerDraftTickets.UnusedTicketBalance));
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00026FA0 File Offset: 0x000251A0
		public void Deserialize(Stream stream)
		{
			PlayerDraftTickets.Deserialize(stream, this);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00026FAA File Offset: 0x000251AA
		public static PlayerDraftTickets Deserialize(Stream stream, PlayerDraftTickets instance)
		{
			return PlayerDraftTickets.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00026FB8 File Offset: 0x000251B8
		public static PlayerDraftTickets DeserializeLengthDelimited(Stream stream)
		{
			PlayerDraftTickets playerDraftTickets = new PlayerDraftTickets();
			PlayerDraftTickets.DeserializeLengthDelimited(stream, playerDraftTickets);
			return playerDraftTickets;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00026FD4 File Offset: 0x000251D4
		public static PlayerDraftTickets DeserializeLengthDelimited(Stream stream, PlayerDraftTickets instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PlayerDraftTickets.Deserialize(stream, instance, num);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00026FFC File Offset: 0x000251FC
		public static PlayerDraftTickets Deserialize(Stream stream, PlayerDraftTickets instance, long limit)
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
					instance.UnusedTicketBalance = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06000A9B RID: 2715 RVA: 0x0002707C File Offset: 0x0002527C
		public void Serialize(Stream stream)
		{
			PlayerDraftTickets.Serialize(stream, this);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00027085 File Offset: 0x00025285
		public static void Serialize(Stream stream, PlayerDraftTickets instance)
		{
			if (instance.HasUnusedTicketBalance)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.UnusedTicketBalance));
			}
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x000270A4 File Offset: 0x000252A4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasUnusedTicketBalance)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.UnusedTicketBalance));
			}
			return num;
		}

		// Token: 0x04000396 RID: 918
		public bool HasUnusedTicketBalance;

		// Token: 0x04000397 RID: 919
		private int _UnusedTicketBalance;
	}
}
