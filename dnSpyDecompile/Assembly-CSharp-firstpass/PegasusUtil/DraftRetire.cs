using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200005F RID: 95
	public class DraftRetire : IProtoBuf
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00017AFA File Offset: 0x00015CFA
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x00017B02 File Offset: 0x00015D02
		public long DeckId { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00017B0B File Offset: 0x00015D0B
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x00017B13 File Offset: 0x00015D13
		public int Slot { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00017B1C File Offset: 0x00015D1C
		// (set) Token: 0x06000606 RID: 1542 RVA: 0x00017B24 File Offset: 0x00015D24
		public int SeasonId
		{
			get
			{
				return this._SeasonId;
			}
			set
			{
				this._SeasonId = value;
				this.HasSeasonId = true;
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00017B34 File Offset: 0x00015D34
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.DeckId.GetHashCode();
			num ^= this.Slot.GetHashCode();
			if (this.HasSeasonId)
			{
				num ^= this.SeasonId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00017B8C File Offset: 0x00015D8C
		public override bool Equals(object obj)
		{
			DraftRetire draftRetire = obj as DraftRetire;
			return draftRetire != null && this.DeckId.Equals(draftRetire.DeckId) && this.Slot.Equals(draftRetire.Slot) && this.HasSeasonId == draftRetire.HasSeasonId && (!this.HasSeasonId || this.SeasonId.Equals(draftRetire.SeasonId));
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00017C04 File Offset: 0x00015E04
		public void Deserialize(Stream stream)
		{
			DraftRetire.Deserialize(stream, this);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00017C0E File Offset: 0x00015E0E
		public static DraftRetire Deserialize(Stream stream, DraftRetire instance)
		{
			return DraftRetire.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00017C1C File Offset: 0x00015E1C
		public static DraftRetire DeserializeLengthDelimited(Stream stream)
		{
			DraftRetire draftRetire = new DraftRetire();
			DraftRetire.DeserializeLengthDelimited(stream, draftRetire);
			return draftRetire;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00017C38 File Offset: 0x00015E38
		public static DraftRetire DeserializeLengthDelimited(Stream stream, DraftRetire instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DraftRetire.Deserialize(stream, instance, num);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00017C60 File Offset: 0x00015E60
		public static DraftRetire Deserialize(Stream stream, DraftRetire instance, long limit)
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
						if (num != 24)
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
							instance.SeasonId = (int)ProtocolParser.ReadUInt64(stream);
						}
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

		// Token: 0x0600060E RID: 1550 RVA: 0x00017D0F File Offset: 0x00015F0F
		public void Serialize(Stream stream)
		{
			DraftRetire.Serialize(stream, this);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00017D18 File Offset: 0x00015F18
		public static void Serialize(Stream stream, DraftRetire instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Slot));
			if (instance.HasSeasonId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SeasonId));
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00017D6C File Offset: 0x00015F6C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)this.DeckId);
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Slot));
			if (this.HasSeasonId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SeasonId));
			}
			return num + 2U;
		}

		// Token: 0x04000214 RID: 532
		public bool HasSeasonId;

		// Token: 0x04000215 RID: 533
		private int _SeasonId;

		// Token: 0x02000571 RID: 1393
		public enum PacketID
		{
			// Token: 0x04001EA9 RID: 7849
			ID = 242,
			// Token: 0x04001EAA RID: 7850
			System = 0
		}
	}
}
