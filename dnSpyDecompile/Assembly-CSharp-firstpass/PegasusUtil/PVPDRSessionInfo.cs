using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200010F RID: 271
	public class PVPDRSessionInfo : IProtoBuf
	{
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x0003EEDF File Offset: 0x0003D0DF
		// (set) Token: 0x060011E2 RID: 4578 RVA: 0x0003EEE7 File Offset: 0x0003D0E7
		public bool HasSession
		{
			get
			{
				return this._HasSession;
			}
			set
			{
				this._HasSession = value;
				this.HasHasSession = true;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x0003EEF7 File Offset: 0x0003D0F7
		// (set) Token: 0x060011E4 RID: 4580 RVA: 0x0003EEFF File Offset: 0x0003D0FF
		public bool IsActive
		{
			get
			{
				return this._IsActive;
			}
			set
			{
				this._IsActive = value;
				this.HasIsActive = true;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x0003EF0F File Offset: 0x0003D10F
		// (set) Token: 0x060011E6 RID: 4582 RVA: 0x0003EF17 File Offset: 0x0003D117
		public uint Season
		{
			get
			{
				return this._Season;
			}
			set
			{
				this._Season = value;
				this.HasSeason = true;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x0003EF27 File Offset: 0x0003D127
		// (set) Token: 0x060011E8 RID: 4584 RVA: 0x0003EF2F File Offset: 0x0003D12F
		public uint Wins
		{
			get
			{
				return this._Wins;
			}
			set
			{
				this._Wins = value;
				this.HasWins = true;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x0003EF3F File Offset: 0x0003D13F
		// (set) Token: 0x060011EA RID: 4586 RVA: 0x0003EF47 File Offset: 0x0003D147
		public uint Losses
		{
			get
			{
				return this._Losses;
			}
			set
			{
				this._Losses = value;
				this.HasLosses = true;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x0003EF57 File Offset: 0x0003D157
		// (set) Token: 0x060011EC RID: 4588 RVA: 0x0003EF5F File Offset: 0x0003D15F
		public long DeckId
		{
			get
			{
				return this._DeckId;
			}
			set
			{
				this._DeckId = value;
				this.HasDeckId = true;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x0003EF6F File Offset: 0x0003D16F
		// (set) Token: 0x060011EE RID: 4590 RVA: 0x0003EF77 File Offset: 0x0003D177
		public bool IsPaidEntry
		{
			get
			{
				return this._IsPaidEntry;
			}
			set
			{
				this._IsPaidEntry = value;
				this.HasIsPaidEntry = true;
			}
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0003EF88 File Offset: 0x0003D188
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasHasSession)
			{
				num ^= this.HasSession.GetHashCode();
			}
			if (this.HasIsActive)
			{
				num ^= this.IsActive.GetHashCode();
			}
			if (this.HasSeason)
			{
				num ^= this.Season.GetHashCode();
			}
			if (this.HasWins)
			{
				num ^= this.Wins.GetHashCode();
			}
			if (this.HasLosses)
			{
				num ^= this.Losses.GetHashCode();
			}
			if (this.HasDeckId)
			{
				num ^= this.DeckId.GetHashCode();
			}
			if (this.HasIsPaidEntry)
			{
				num ^= this.IsPaidEntry.GetHashCode();
			}
			return num;
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0003F054 File Offset: 0x0003D254
		public override bool Equals(object obj)
		{
			PVPDRSessionInfo pvpdrsessionInfo = obj as PVPDRSessionInfo;
			return pvpdrsessionInfo != null && this.HasHasSession == pvpdrsessionInfo.HasHasSession && (!this.HasHasSession || this.HasSession.Equals(pvpdrsessionInfo.HasSession)) && this.HasIsActive == pvpdrsessionInfo.HasIsActive && (!this.HasIsActive || this.IsActive.Equals(pvpdrsessionInfo.IsActive)) && this.HasSeason == pvpdrsessionInfo.HasSeason && (!this.HasSeason || this.Season.Equals(pvpdrsessionInfo.Season)) && this.HasWins == pvpdrsessionInfo.HasWins && (!this.HasWins || this.Wins.Equals(pvpdrsessionInfo.Wins)) && this.HasLosses == pvpdrsessionInfo.HasLosses && (!this.HasLosses || this.Losses.Equals(pvpdrsessionInfo.Losses)) && this.HasDeckId == pvpdrsessionInfo.HasDeckId && (!this.HasDeckId || this.DeckId.Equals(pvpdrsessionInfo.DeckId)) && this.HasIsPaidEntry == pvpdrsessionInfo.HasIsPaidEntry && (!this.HasIsPaidEntry || this.IsPaidEntry.Equals(pvpdrsessionInfo.IsPaidEntry));
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0003F1B0 File Offset: 0x0003D3B0
		public void Deserialize(Stream stream)
		{
			PVPDRSessionInfo.Deserialize(stream, this);
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0003F1BA File Offset: 0x0003D3BA
		public static PVPDRSessionInfo Deserialize(Stream stream, PVPDRSessionInfo instance)
		{
			return PVPDRSessionInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0003F1C8 File Offset: 0x0003D3C8
		public static PVPDRSessionInfo DeserializeLengthDelimited(Stream stream)
		{
			PVPDRSessionInfo pvpdrsessionInfo = new PVPDRSessionInfo();
			PVPDRSessionInfo.DeserializeLengthDelimited(stream, pvpdrsessionInfo);
			return pvpdrsessionInfo;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0003F1E4 File Offset: 0x0003D3E4
		public static PVPDRSessionInfo DeserializeLengthDelimited(Stream stream, PVPDRSessionInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PVPDRSessionInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0003F20C File Offset: 0x0003D40C
		public static PVPDRSessionInfo Deserialize(Stream stream, PVPDRSessionInfo instance, long limit)
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
					if (num <= 24)
					{
						if (num == 8)
						{
							instance.HasSession = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 16)
						{
							instance.IsActive = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 24)
						{
							instance.Season = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
					else if (num <= 40)
					{
						if (num == 32)
						{
							instance.Wins = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 40)
						{
							instance.Losses = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
					else
					{
						if (num == 48)
						{
							instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 56)
						{
							instance.IsPaidEntry = ProtocolParser.ReadBool(stream);
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

		// Token: 0x060011F6 RID: 4598 RVA: 0x0003F331 File Offset: 0x0003D531
		public void Serialize(Stream stream)
		{
			PVPDRSessionInfo.Serialize(stream, this);
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0003F33C File Offset: 0x0003D53C
		public static void Serialize(Stream stream, PVPDRSessionInfo instance)
		{
			if (instance.HasHasSession)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.HasSession);
			}
			if (instance.HasIsActive)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.IsActive);
			}
			if (instance.HasSeason)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Season);
			}
			if (instance.HasWins)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.Wins);
			}
			if (instance.HasLosses)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt32(stream, instance.Losses);
			}
			if (instance.HasDeckId)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			}
			if (instance.HasIsPaidEntry)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.IsPaidEntry);
			}
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0003F40C File Offset: 0x0003D60C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasHasSession)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIsActive)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasSeason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Season);
			}
			if (this.HasWins)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Wins);
			}
			if (this.HasLosses)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Losses);
			}
			if (this.HasDeckId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.DeckId);
			}
			if (this.HasIsPaidEntry)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x0400057B RID: 1403
		public bool HasHasSession;

		// Token: 0x0400057C RID: 1404
		private bool _HasSession;

		// Token: 0x0400057D RID: 1405
		public bool HasIsActive;

		// Token: 0x0400057E RID: 1406
		private bool _IsActive;

		// Token: 0x0400057F RID: 1407
		public bool HasSeason;

		// Token: 0x04000580 RID: 1408
		private uint _Season;

		// Token: 0x04000581 RID: 1409
		public bool HasWins;

		// Token: 0x04000582 RID: 1410
		private uint _Wins;

		// Token: 0x04000583 RID: 1411
		public bool HasLosses;

		// Token: 0x04000584 RID: 1412
		private uint _Losses;

		// Token: 0x04000585 RID: 1413
		public bool HasDeckId;

		// Token: 0x04000586 RID: 1414
		private long _DeckId;

		// Token: 0x04000587 RID: 1415
		public bool HasIsPaidEntry;

		// Token: 0x04000588 RID: 1416
		private bool _IsPaidEntry;
	}
}
