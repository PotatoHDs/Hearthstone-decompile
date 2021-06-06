using System;
using System.IO;

namespace PegasusFSG
{
	// Token: 0x02000024 RID: 36
	public class BrawlDeckValidity : IProtoBuf
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00007D7A File Offset: 0x00005F7A
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00007D82 File Offset: 0x00005F82
		public int SeasonId { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00007D8B File Offset: 0x00005F8B
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00007D93 File Offset: 0x00005F93
		public bool ValidDeck { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00007D9C File Offset: 0x00005F9C
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00007DA4 File Offset: 0x00005FA4
		public int BrawlLibraryItemId
		{
			get
			{
				return this._BrawlLibraryItemId;
			}
			set
			{
				this._BrawlLibraryItemId = value;
				this.HasBrawlLibraryItemId = true;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007DB4 File Offset: 0x00005FB4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.SeasonId.GetHashCode();
			num ^= this.ValidDeck.GetHashCode();
			if (this.HasBrawlLibraryItemId)
			{
				num ^= this.BrawlLibraryItemId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007E0C File Offset: 0x0000600C
		public override bool Equals(object obj)
		{
			BrawlDeckValidity brawlDeckValidity = obj as BrawlDeckValidity;
			return brawlDeckValidity != null && this.SeasonId.Equals(brawlDeckValidity.SeasonId) && this.ValidDeck.Equals(brawlDeckValidity.ValidDeck) && this.HasBrawlLibraryItemId == brawlDeckValidity.HasBrawlLibraryItemId && (!this.HasBrawlLibraryItemId || this.BrawlLibraryItemId.Equals(brawlDeckValidity.BrawlLibraryItemId));
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007E84 File Offset: 0x00006084
		public void Deserialize(Stream stream)
		{
			BrawlDeckValidity.Deserialize(stream, this);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007E8E File Offset: 0x0000608E
		public static BrawlDeckValidity Deserialize(Stream stream, BrawlDeckValidity instance)
		{
			return BrawlDeckValidity.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007E9C File Offset: 0x0000609C
		public static BrawlDeckValidity DeserializeLengthDelimited(Stream stream)
		{
			BrawlDeckValidity brawlDeckValidity = new BrawlDeckValidity();
			BrawlDeckValidity.DeserializeLengthDelimited(stream, brawlDeckValidity);
			return brawlDeckValidity;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007EB8 File Offset: 0x000060B8
		public static BrawlDeckValidity DeserializeLengthDelimited(Stream stream, BrawlDeckValidity instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BrawlDeckValidity.Deserialize(stream, instance, num);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007EE0 File Offset: 0x000060E0
		public static BrawlDeckValidity Deserialize(Stream stream, BrawlDeckValidity instance, long limit)
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
							instance.BrawlLibraryItemId = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.ValidDeck = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.SeasonId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00007F8F File Offset: 0x0000618F
		public void Serialize(Stream stream)
		{
			BrawlDeckValidity.Serialize(stream, this);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00007F98 File Offset: 0x00006198
		public static void Serialize(Stream stream, BrawlDeckValidity instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SeasonId));
			stream.WriteByte(16);
			ProtocolParser.WriteBool(stream, instance.ValidDeck);
			if (instance.HasBrawlLibraryItemId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BrawlLibraryItemId));
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007FEC File Offset: 0x000061EC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SeasonId));
			num += 1U;
			if (this.HasBrawlLibraryItemId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BrawlLibraryItemId));
			}
			return num + 2U;
		}

		// Token: 0x04000077 RID: 119
		public bool HasBrawlLibraryItemId;

		// Token: 0x04000078 RID: 120
		private int _BrawlLibraryItemId;
	}
}
