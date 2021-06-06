using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001D3 RID: 467
	public class BattlegroundsRatingChange : IProtoBuf
	{
		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x00068D13 File Offset: 0x00066F13
		// (set) Token: 0x06001DCD RID: 7629 RVA: 0x00068D1B File Offset: 0x00066F1B
		public int NewRating
		{
			get
			{
				return this._NewRating;
			}
			set
			{
				this._NewRating = value;
				this.HasNewRating = true;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001DCE RID: 7630 RVA: 0x00068D2B File Offset: 0x00066F2B
		// (set) Token: 0x06001DCF RID: 7631 RVA: 0x00068D33 File Offset: 0x00066F33
		public int RatingChange
		{
			get
			{
				return this._RatingChange;
			}
			set
			{
				this._RatingChange = value;
				this.HasRatingChange = true;
			}
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x00068D44 File Offset: 0x00066F44
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasNewRating)
			{
				num ^= this.NewRating.GetHashCode();
			}
			if (this.HasRatingChange)
			{
				num ^= this.RatingChange.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x00068D90 File Offset: 0x00066F90
		public override bool Equals(object obj)
		{
			BattlegroundsRatingChange battlegroundsRatingChange = obj as BattlegroundsRatingChange;
			return battlegroundsRatingChange != null && this.HasNewRating == battlegroundsRatingChange.HasNewRating && (!this.HasNewRating || this.NewRating.Equals(battlegroundsRatingChange.NewRating)) && this.HasRatingChange == battlegroundsRatingChange.HasRatingChange && (!this.HasRatingChange || this.RatingChange.Equals(battlegroundsRatingChange.RatingChange));
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x00068E06 File Offset: 0x00067006
		public void Deserialize(Stream stream)
		{
			BattlegroundsRatingChange.Deserialize(stream, this);
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x00068E10 File Offset: 0x00067010
		public static BattlegroundsRatingChange Deserialize(Stream stream, BattlegroundsRatingChange instance)
		{
			return BattlegroundsRatingChange.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x00068E1C File Offset: 0x0006701C
		public static BattlegroundsRatingChange DeserializeLengthDelimited(Stream stream)
		{
			BattlegroundsRatingChange battlegroundsRatingChange = new BattlegroundsRatingChange();
			BattlegroundsRatingChange.DeserializeLengthDelimited(stream, battlegroundsRatingChange);
			return battlegroundsRatingChange;
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00068E38 File Offset: 0x00067038
		public static BattlegroundsRatingChange DeserializeLengthDelimited(Stream stream, BattlegroundsRatingChange instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BattlegroundsRatingChange.Deserialize(stream, instance, num);
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x00068E60 File Offset: 0x00067060
		public static BattlegroundsRatingChange Deserialize(Stream stream, BattlegroundsRatingChange instance, long limit)
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
						instance.RatingChange = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.NewRating = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x00068EF9 File Offset: 0x000670F9
		public void Serialize(Stream stream)
		{
			BattlegroundsRatingChange.Serialize(stream, this);
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x00068F02 File Offset: 0x00067102
		public static void Serialize(Stream stream, BattlegroundsRatingChange instance)
		{
			if (instance.HasNewRating)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.NewRating));
			}
			if (instance.HasRatingChange)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RatingChange));
			}
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x00068F40 File Offset: 0x00067140
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasNewRating)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.NewRating));
			}
			if (this.HasRatingChange)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RatingChange));
			}
			return num;
		}

		// Token: 0x04000AC4 RID: 2756
		public bool HasNewRating;

		// Token: 0x04000AC5 RID: 2757
		private int _NewRating;

		// Token: 0x04000AC6 RID: 2758
		public bool HasRatingChange;

		// Token: 0x04000AC7 RID: 2759
		private int _RatingChange;

		// Token: 0x0200065E RID: 1630
		public enum PacketID
		{
			// Token: 0x04002156 RID: 8534
			ID = 34
		}
	}
}
