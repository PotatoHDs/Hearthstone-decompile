using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000160 RID: 352
	public class TavernBrawlPlayerRecord : IProtoBuf
	{
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x00054077 File Offset: 0x00052277
		// (set) Token: 0x06001801 RID: 6145 RVA: 0x0005407F File Offset: 0x0005227F
		public int RewardProgress { get; set; }

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x00054088 File Offset: 0x00052288
		// (set) Token: 0x06001803 RID: 6147 RVA: 0x00054090 File Offset: 0x00052290
		public int GamesPlayed
		{
			get
			{
				return this._GamesPlayed;
			}
			set
			{
				this._GamesPlayed = value;
				this.HasGamesPlayed = true;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x000540A0 File Offset: 0x000522A0
		// (set) Token: 0x06001805 RID: 6149 RVA: 0x000540A8 File Offset: 0x000522A8
		public int GamesWon { get; set; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x000540B1 File Offset: 0x000522B1
		// (set) Token: 0x06001807 RID: 6151 RVA: 0x000540B9 File Offset: 0x000522B9
		public int WinStreak
		{
			get
			{
				return this._WinStreak;
			}
			set
			{
				this._WinStreak = value;
				this.HasWinStreak = true;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x000540C9 File Offset: 0x000522C9
		// (set) Token: 0x06001809 RID: 6153 RVA: 0x000540D1 File Offset: 0x000522D1
		public TavernBrawlStatus SessionStatus
		{
			get
			{
				return this._SessionStatus;
			}
			set
			{
				this._SessionStatus = value;
				this.HasSessionStatus = true;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x000540E1 File Offset: 0x000522E1
		// (set) Token: 0x0600180B RID: 6155 RVA: 0x000540E9 File Offset: 0x000522E9
		public int NumTicketsOwned
		{
			get
			{
				return this._NumTicketsOwned;
			}
			set
			{
				this._NumTicketsOwned = value;
				this.HasNumTicketsOwned = true;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x000540F9 File Offset: 0x000522F9
		// (set) Token: 0x0600180D RID: 6157 RVA: 0x00054101 File Offset: 0x00052301
		public TavernBrawlPlayerSession Session
		{
			get
			{
				return this._Session;
			}
			set
			{
				this._Session = value;
				this.HasSession = (value != null);
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x00054114 File Offset: 0x00052314
		// (set) Token: 0x0600180F RID: 6159 RVA: 0x0005411C File Offset: 0x0005231C
		public int NumSessionsPurchasable
		{
			get
			{
				return this._NumSessionsPurchasable;
			}
			set
			{
				this._NumSessionsPurchasable = value;
				this.HasNumSessionsPurchasable = true;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x0005412C File Offset: 0x0005232C
		// (set) Token: 0x06001811 RID: 6161 RVA: 0x00054134 File Offset: 0x00052334
		public BrawlType BrawlType
		{
			get
			{
				return this._BrawlType;
			}
			set
			{
				this._BrawlType = value;
				this.HasBrawlType = true;
			}
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00054144 File Offset: 0x00052344
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.RewardProgress.GetHashCode();
			if (this.HasGamesPlayed)
			{
				num ^= this.GamesPlayed.GetHashCode();
			}
			num ^= this.GamesWon.GetHashCode();
			if (this.HasWinStreak)
			{
				num ^= this.WinStreak.GetHashCode();
			}
			if (this.HasSessionStatus)
			{
				num ^= this.SessionStatus.GetHashCode();
			}
			if (this.HasNumTicketsOwned)
			{
				num ^= this.NumTicketsOwned.GetHashCode();
			}
			if (this.HasSession)
			{
				num ^= this.Session.GetHashCode();
			}
			if (this.HasNumSessionsPurchasable)
			{
				num ^= this.NumSessionsPurchasable.GetHashCode();
			}
			if (this.HasBrawlType)
			{
				num ^= this.BrawlType.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00054238 File Offset: 0x00052438
		public override bool Equals(object obj)
		{
			TavernBrawlPlayerRecord tavernBrawlPlayerRecord = obj as TavernBrawlPlayerRecord;
			return tavernBrawlPlayerRecord != null && this.RewardProgress.Equals(tavernBrawlPlayerRecord.RewardProgress) && this.HasGamesPlayed == tavernBrawlPlayerRecord.HasGamesPlayed && (!this.HasGamesPlayed || this.GamesPlayed.Equals(tavernBrawlPlayerRecord.GamesPlayed)) && this.GamesWon.Equals(tavernBrawlPlayerRecord.GamesWon) && this.HasWinStreak == tavernBrawlPlayerRecord.HasWinStreak && (!this.HasWinStreak || this.WinStreak.Equals(tavernBrawlPlayerRecord.WinStreak)) && this.HasSessionStatus == tavernBrawlPlayerRecord.HasSessionStatus && (!this.HasSessionStatus || this.SessionStatus.Equals(tavernBrawlPlayerRecord.SessionStatus)) && this.HasNumTicketsOwned == tavernBrawlPlayerRecord.HasNumTicketsOwned && (!this.HasNumTicketsOwned || this.NumTicketsOwned.Equals(tavernBrawlPlayerRecord.NumTicketsOwned)) && this.HasSession == tavernBrawlPlayerRecord.HasSession && (!this.HasSession || this.Session.Equals(tavernBrawlPlayerRecord.Session)) && this.HasNumSessionsPurchasable == tavernBrawlPlayerRecord.HasNumSessionsPurchasable && (!this.HasNumSessionsPurchasable || this.NumSessionsPurchasable.Equals(tavernBrawlPlayerRecord.NumSessionsPurchasable)) && this.HasBrawlType == tavernBrawlPlayerRecord.HasBrawlType && (!this.HasBrawlType || this.BrawlType.Equals(tavernBrawlPlayerRecord.BrawlType));
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x000543D7 File Offset: 0x000525D7
		public void Deserialize(Stream stream)
		{
			TavernBrawlPlayerRecord.Deserialize(stream, this);
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x000543E1 File Offset: 0x000525E1
		public static TavernBrawlPlayerRecord Deserialize(Stream stream, TavernBrawlPlayerRecord instance)
		{
			return TavernBrawlPlayerRecord.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x000543EC File Offset: 0x000525EC
		public static TavernBrawlPlayerRecord DeserializeLengthDelimited(Stream stream)
		{
			TavernBrawlPlayerRecord tavernBrawlPlayerRecord = new TavernBrawlPlayerRecord();
			TavernBrawlPlayerRecord.DeserializeLengthDelimited(stream, tavernBrawlPlayerRecord);
			return tavernBrawlPlayerRecord;
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00054408 File Offset: 0x00052608
		public static TavernBrawlPlayerRecord DeserializeLengthDelimited(Stream stream, TavernBrawlPlayerRecord instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TavernBrawlPlayerRecord.Deserialize(stream, instance, num);
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00054430 File Offset: 0x00052630
		public static TavernBrawlPlayerRecord Deserialize(Stream stream, TavernBrawlPlayerRecord instance, long limit)
		{
			instance.SessionStatus = TavernBrawlStatus.TB_STATUS_INVALID;
			instance.BrawlType = BrawlType.BRAWL_TYPE_UNKNOWN;
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
					if (num <= 32)
					{
						if (num <= 16)
						{
							if (num == 8)
							{
								instance.RewardProgress = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 16)
							{
								instance.GamesPlayed = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 24)
							{
								instance.GamesWon = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 32)
							{
								instance.WinStreak = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 48)
					{
						if (num == 40)
						{
							instance.SessionStatus = (TavernBrawlStatus)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 48)
						{
							instance.NumTicketsOwned = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else if (num != 58)
					{
						if (num == 64)
						{
							instance.NumSessionsPurchasable = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 72)
						{
							instance.BrawlType = (BrawlType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (instance.Session == null)
						{
							instance.Session = TavernBrawlPlayerSession.DeserializeLengthDelimited(stream);
							continue;
						}
						TavernBrawlPlayerSession.DeserializeLengthDelimited(stream, instance.Session);
						continue;
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

		// Token: 0x06001819 RID: 6169 RVA: 0x000545C7 File Offset: 0x000527C7
		public void Serialize(Stream stream)
		{
			TavernBrawlPlayerRecord.Serialize(stream, this);
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x000545D0 File Offset: 0x000527D0
		public static void Serialize(Stream stream, TavernBrawlPlayerRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RewardProgress));
			if (instance.HasGamesPlayed)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GamesPlayed));
			}
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GamesWon));
			if (instance.HasWinStreak)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.WinStreak));
			}
			if (instance.HasSessionStatus)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SessionStatus));
			}
			if (instance.HasNumTicketsOwned)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.NumTicketsOwned));
			}
			if (instance.HasSession)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteUInt32(stream, instance.Session.GetSerializedSize());
				TavernBrawlPlayerSession.Serialize(stream, instance.Session);
			}
			if (instance.HasNumSessionsPurchasable)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.NumSessionsPurchasable));
			}
			if (instance.HasBrawlType)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BrawlType));
			}
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x000546E4 File Offset: 0x000528E4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RewardProgress));
			if (this.HasGamesPlayed)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.GamesPlayed));
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.GamesWon));
			if (this.HasWinStreak)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.WinStreak));
			}
			if (this.HasSessionStatus)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SessionStatus));
			}
			if (this.HasNumTicketsOwned)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.NumTicketsOwned));
			}
			if (this.HasSession)
			{
				num += 1U;
				uint serializedSize = this.Session.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasNumSessionsPurchasable)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.NumSessionsPurchasable));
			}
			if (this.HasBrawlType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BrawlType));
			}
			return num + 2U;
		}

		// Token: 0x040007A6 RID: 1958
		public bool HasGamesPlayed;

		// Token: 0x040007A7 RID: 1959
		private int _GamesPlayed;

		// Token: 0x040007A9 RID: 1961
		public bool HasWinStreak;

		// Token: 0x040007AA RID: 1962
		private int _WinStreak;

		// Token: 0x040007AB RID: 1963
		public bool HasSessionStatus;

		// Token: 0x040007AC RID: 1964
		private TavernBrawlStatus _SessionStatus;

		// Token: 0x040007AD RID: 1965
		public bool HasNumTicketsOwned;

		// Token: 0x040007AE RID: 1966
		private int _NumTicketsOwned;

		// Token: 0x040007AF RID: 1967
		public bool HasSession;

		// Token: 0x040007B0 RID: 1968
		private TavernBrawlPlayerSession _Session;

		// Token: 0x040007B1 RID: 1969
		public bool HasNumSessionsPurchasable;

		// Token: 0x040007B2 RID: 1970
		private int _NumSessionsPurchasable;

		// Token: 0x040007B3 RID: 1971
		public bool HasBrawlType;

		// Token: 0x040007B4 RID: 1972
		private BrawlType _BrawlType;
	}
}
