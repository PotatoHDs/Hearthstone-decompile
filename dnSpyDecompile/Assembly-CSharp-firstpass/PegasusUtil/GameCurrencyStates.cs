using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000088 RID: 136
	public class GameCurrencyStates : IProtoBuf
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0002131E File Offset: 0x0001F51E
		// (set) Token: 0x06000903 RID: 2307 RVA: 0x00021326 File Offset: 0x0001F526
		public long ArcaneDustBalance
		{
			get
			{
				return this._ArcaneDustBalance;
			}
			set
			{
				this._ArcaneDustBalance = value;
				this.HasArcaneDustBalance = true;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x00021336 File Offset: 0x0001F536
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x0002133E File Offset: 0x0001F53E
		public long CappedGoldBalance
		{
			get
			{
				return this._CappedGoldBalance;
			}
			set
			{
				this._CappedGoldBalance = value;
				this.HasCappedGoldBalance = true;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0002134E File Offset: 0x0001F54E
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x00021356 File Offset: 0x0001F556
		public long BonusGoldBalance
		{
			get
			{
				return this._BonusGoldBalance;
			}
			set
			{
				this._BonusGoldBalance = value;
				this.HasBonusGoldBalance = true;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00021366 File Offset: 0x0001F566
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x0002136E File Offset: 0x0001F56E
		public long GoldCap
		{
			get
			{
				return this._GoldCap;
			}
			set
			{
				this._GoldCap = value;
				this.HasGoldCap = true;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x0002137E File Offset: 0x0001F57E
		// (set) Token: 0x0600090B RID: 2315 RVA: 0x00021386 File Offset: 0x0001F586
		public long GoldCapWarning
		{
			get
			{
				return this._GoldCapWarning;
			}
			set
			{
				this._GoldCapWarning = value;
				this.HasGoldCapWarning = true;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00021396 File Offset: 0x0001F596
		// (set) Token: 0x0600090D RID: 2317 RVA: 0x0002139E File Offset: 0x0001F59E
		public long CurrencyVersion
		{
			get
			{
				return this._CurrencyVersion;
			}
			set
			{
				this._CurrencyVersion = value;
				this.HasCurrencyVersion = true;
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x000213B0 File Offset: 0x0001F5B0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasArcaneDustBalance)
			{
				num ^= this.ArcaneDustBalance.GetHashCode();
			}
			if (this.HasCappedGoldBalance)
			{
				num ^= this.CappedGoldBalance.GetHashCode();
			}
			if (this.HasBonusGoldBalance)
			{
				num ^= this.BonusGoldBalance.GetHashCode();
			}
			if (this.HasGoldCap)
			{
				num ^= this.GoldCap.GetHashCode();
			}
			if (this.HasGoldCapWarning)
			{
				num ^= this.GoldCapWarning.GetHashCode();
			}
			if (this.HasCurrencyVersion)
			{
				num ^= this.CurrencyVersion.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00021460 File Offset: 0x0001F660
		public override bool Equals(object obj)
		{
			GameCurrencyStates gameCurrencyStates = obj as GameCurrencyStates;
			return gameCurrencyStates != null && this.HasArcaneDustBalance == gameCurrencyStates.HasArcaneDustBalance && (!this.HasArcaneDustBalance || this.ArcaneDustBalance.Equals(gameCurrencyStates.ArcaneDustBalance)) && this.HasCappedGoldBalance == gameCurrencyStates.HasCappedGoldBalance && (!this.HasCappedGoldBalance || this.CappedGoldBalance.Equals(gameCurrencyStates.CappedGoldBalance)) && this.HasBonusGoldBalance == gameCurrencyStates.HasBonusGoldBalance && (!this.HasBonusGoldBalance || this.BonusGoldBalance.Equals(gameCurrencyStates.BonusGoldBalance)) && this.HasGoldCap == gameCurrencyStates.HasGoldCap && (!this.HasGoldCap || this.GoldCap.Equals(gameCurrencyStates.GoldCap)) && this.HasGoldCapWarning == gameCurrencyStates.HasGoldCapWarning && (!this.HasGoldCapWarning || this.GoldCapWarning.Equals(gameCurrencyStates.GoldCapWarning)) && this.HasCurrencyVersion == gameCurrencyStates.HasCurrencyVersion && (!this.HasCurrencyVersion || this.CurrencyVersion.Equals(gameCurrencyStates.CurrencyVersion));
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0002158E File Offset: 0x0001F78E
		public void Deserialize(Stream stream)
		{
			GameCurrencyStates.Deserialize(stream, this);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00021598 File Offset: 0x0001F798
		public static GameCurrencyStates Deserialize(Stream stream, GameCurrencyStates instance)
		{
			return GameCurrencyStates.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x000215A4 File Offset: 0x0001F7A4
		public static GameCurrencyStates DeserializeLengthDelimited(Stream stream)
		{
			GameCurrencyStates gameCurrencyStates = new GameCurrencyStates();
			GameCurrencyStates.DeserializeLengthDelimited(stream, gameCurrencyStates);
			return gameCurrencyStates;
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x000215C0 File Offset: 0x0001F7C0
		public static GameCurrencyStates DeserializeLengthDelimited(Stream stream, GameCurrencyStates instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameCurrencyStates.Deserialize(stream, instance, num);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x000215E8 File Offset: 0x0001F7E8
		public static GameCurrencyStates Deserialize(Stream stream, GameCurrencyStates instance, long limit)
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
							instance.ArcaneDustBalance = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.CappedGoldBalance = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 24)
						{
							instance.BonusGoldBalance = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.GoldCap = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.GoldCapWarning = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 48)
						{
							instance.CurrencyVersion = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06000915 RID: 2325 RVA: 0x000216E4 File Offset: 0x0001F8E4
		public void Serialize(Stream stream)
		{
			GameCurrencyStates.Serialize(stream, this);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000216F0 File Offset: 0x0001F8F0
		public static void Serialize(Stream stream, GameCurrencyStates instance)
		{
			if (instance.HasArcaneDustBalance)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ArcaneDustBalance);
			}
			if (instance.HasCappedGoldBalance)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CappedGoldBalance);
			}
			if (instance.HasBonusGoldBalance)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BonusGoldBalance);
			}
			if (instance.HasGoldCap)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GoldCap);
			}
			if (instance.HasGoldCapWarning)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GoldCapWarning);
			}
			if (instance.HasCurrencyVersion)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrencyVersion);
			}
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000217A4 File Offset: 0x0001F9A4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasArcaneDustBalance)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.ArcaneDustBalance);
			}
			if (this.HasCappedGoldBalance)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.CappedGoldBalance);
			}
			if (this.HasBonusGoldBalance)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.BonusGoldBalance);
			}
			if (this.HasGoldCap)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.GoldCap);
			}
			if (this.HasGoldCapWarning)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.GoldCapWarning);
			}
			if (this.HasCurrencyVersion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.CurrencyVersion);
			}
			return num;
		}

		// Token: 0x0400031E RID: 798
		public bool HasArcaneDustBalance;

		// Token: 0x0400031F RID: 799
		private long _ArcaneDustBalance;

		// Token: 0x04000320 RID: 800
		public bool HasCappedGoldBalance;

		// Token: 0x04000321 RID: 801
		private long _CappedGoldBalance;

		// Token: 0x04000322 RID: 802
		public bool HasBonusGoldBalance;

		// Token: 0x04000323 RID: 803
		private long _BonusGoldBalance;

		// Token: 0x04000324 RID: 804
		public bool HasGoldCap;

		// Token: 0x04000325 RID: 805
		private long _GoldCap;

		// Token: 0x04000326 RID: 806
		public bool HasGoldCapWarning;

		// Token: 0x04000327 RID: 807
		private long _GoldCapWarning;

		// Token: 0x04000328 RID: 808
		public bool HasCurrencyVersion;

		// Token: 0x04000329 RID: 809
		private long _CurrencyVersion;
	}
}
