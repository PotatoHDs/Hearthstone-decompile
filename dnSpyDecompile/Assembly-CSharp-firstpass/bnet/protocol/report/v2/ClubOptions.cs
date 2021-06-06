using System;
using System.IO;
using bnet.protocol.report.v2.Types;

namespace bnet.protocol.report.v2
{
	// Token: 0x02000322 RID: 802
	public class ClubOptions : IProtoBuf
	{
		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06003123 RID: 12579 RVA: 0x000A5498 File Offset: 0x000A3698
		// (set) Token: 0x06003124 RID: 12580 RVA: 0x000A54A0 File Offset: 0x000A36A0
		public ulong ClubId
		{
			get
			{
				return this._ClubId;
			}
			set
			{
				this._ClubId = value;
				this.HasClubId = true;
			}
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000A54B0 File Offset: 0x000A36B0
		public void SetClubId(ulong val)
		{
			this.ClubId = val;
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06003126 RID: 12582 RVA: 0x000A54B9 File Offset: 0x000A36B9
		// (set) Token: 0x06003127 RID: 12583 RVA: 0x000A54C1 File Offset: 0x000A36C1
		public ulong StreamId
		{
			get
			{
				return this._StreamId;
			}
			set
			{
				this._StreamId = value;
				this.HasStreamId = true;
			}
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x000A54D1 File Offset: 0x000A36D1
		public void SetStreamId(ulong val)
		{
			this.StreamId = val;
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06003129 RID: 12585 RVA: 0x000A54DA File Offset: 0x000A36DA
		// (set) Token: 0x0600312A RID: 12586 RVA: 0x000A54E2 File Offset: 0x000A36E2
		public IssueType Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.HasType = true;
			}
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x000A54F2 File Offset: 0x000A36F2
		public void SetType(IssueType val)
		{
			this.Type = val;
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x000A54FB File Offset: 0x000A36FB
		// (set) Token: 0x0600312D RID: 12589 RVA: 0x000A5503 File Offset: 0x000A3703
		public ClubSource Source
		{
			get
			{
				return this._Source;
			}
			set
			{
				this._Source = value;
				this.HasSource = true;
			}
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x000A5513 File Offset: 0x000A3713
		public void SetSource(ClubSource val)
		{
			this.Source = val;
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x0600312F RID: 12591 RVA: 0x000A551C File Offset: 0x000A371C
		// (set) Token: 0x06003130 RID: 12592 RVA: 0x000A5524 File Offset: 0x000A3724
		public ReportItem Item
		{
			get
			{
				return this._Item;
			}
			set
			{
				this._Item = value;
				this.HasItem = (value != null);
			}
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x000A5537 File Offset: 0x000A3737
		public void SetItem(ReportItem val)
		{
			this.Item = val;
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x000A5540 File Offset: 0x000A3740
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasClubId)
			{
				num ^= this.ClubId.GetHashCode();
			}
			if (this.HasStreamId)
			{
				num ^= this.StreamId.GetHashCode();
			}
			if (this.HasType)
			{
				num ^= this.Type.GetHashCode();
			}
			if (this.HasSource)
			{
				num ^= this.Source.GetHashCode();
			}
			if (this.HasItem)
			{
				num ^= this.Item.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x000A55E0 File Offset: 0x000A37E0
		public override bool Equals(object obj)
		{
			ClubOptions clubOptions = obj as ClubOptions;
			return clubOptions != null && this.HasClubId == clubOptions.HasClubId && (!this.HasClubId || this.ClubId.Equals(clubOptions.ClubId)) && this.HasStreamId == clubOptions.HasStreamId && (!this.HasStreamId || this.StreamId.Equals(clubOptions.StreamId)) && this.HasType == clubOptions.HasType && (!this.HasType || this.Type.Equals(clubOptions.Type)) && this.HasSource == clubOptions.HasSource && (!this.HasSource || this.Source.Equals(clubOptions.Source)) && this.HasItem == clubOptions.HasItem && (!this.HasItem || this.Item.Equals(clubOptions.Item));
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06003134 RID: 12596 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x000A56F3 File Offset: 0x000A38F3
		public static ClubOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ClubOptions>(bs, 0, -1);
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x000A56FD File Offset: 0x000A38FD
		public void Deserialize(Stream stream)
		{
			ClubOptions.Deserialize(stream, this);
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x000A5707 File Offset: 0x000A3907
		public static ClubOptions Deserialize(Stream stream, ClubOptions instance)
		{
			return ClubOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000A5714 File Offset: 0x000A3914
		public static ClubOptions DeserializeLengthDelimited(Stream stream)
		{
			ClubOptions clubOptions = new ClubOptions();
			ClubOptions.DeserializeLengthDelimited(stream, clubOptions);
			return clubOptions;
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000A5730 File Offset: 0x000A3930
		public static ClubOptions DeserializeLengthDelimited(Stream stream, ClubOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ClubOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x000A5758 File Offset: 0x000A3958
		public static ClubOptions Deserialize(Stream stream, ClubOptions instance, long limit)
		{
			instance.Type = IssueType.ISSUE_TYPE_SPAM;
			instance.Source = ClubSource.CLUB_SOURCE_OTHER;
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
							instance.ClubId = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.StreamId = ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Type = (IssueType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.Source = (ClubSource)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 42)
						{
							if (instance.Item == null)
							{
								instance.Item = ReportItem.DeserializeLengthDelimited(stream);
								continue;
							}
							ReportItem.DeserializeLengthDelimited(stream, instance.Item);
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

		// Token: 0x0600313B RID: 12603 RVA: 0x000A5868 File Offset: 0x000A3A68
		public void Serialize(Stream stream)
		{
			ClubOptions.Serialize(stream, this);
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x000A5874 File Offset: 0x000A3A74
		public static void Serialize(Stream stream, ClubOptions instance)
		{
			if (instance.HasClubId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.ClubId);
			}
			if (instance.HasStreamId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.StreamId);
			}
			if (instance.HasType)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Type));
			}
			if (instance.HasSource)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Source));
			}
			if (instance.HasItem)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Item.GetSerializedSize());
				ReportItem.Serialize(stream, instance.Item);
			}
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x000A5920 File Offset: 0x000A3B20
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasClubId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ClubId);
			}
			if (this.HasStreamId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.StreamId);
			}
			if (this.HasType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Type));
			}
			if (this.HasSource)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Source));
			}
			if (this.HasItem)
			{
				num += 1U;
				uint serializedSize = this.Item.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x0400137A RID: 4986
		public bool HasClubId;

		// Token: 0x0400137B RID: 4987
		private ulong _ClubId;

		// Token: 0x0400137C RID: 4988
		public bool HasStreamId;

		// Token: 0x0400137D RID: 4989
		private ulong _StreamId;

		// Token: 0x0400137E RID: 4990
		public bool HasType;

		// Token: 0x0400137F RID: 4991
		private IssueType _Type;

		// Token: 0x04001380 RID: 4992
		public bool HasSource;

		// Token: 0x04001381 RID: 4993
		private ClubSource _Source;

		// Token: 0x04001382 RID: 4994
		public bool HasItem;

		// Token: 0x04001383 RID: 4995
		private ReportItem _Item;
	}
}
