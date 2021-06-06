using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x020003A1 RID: 929
	public class GameStatsBucket : IProtoBuf
	{
		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06003BEA RID: 15338 RVA: 0x000C1863 File Offset: 0x000BFA63
		// (set) Token: 0x06003BEB RID: 15339 RVA: 0x000C186B File Offset: 0x000BFA6B
		public float BucketMin
		{
			get
			{
				return this._BucketMin;
			}
			set
			{
				this._BucketMin = value;
				this.HasBucketMin = true;
			}
		}

		// Token: 0x06003BEC RID: 15340 RVA: 0x000C187B File Offset: 0x000BFA7B
		public void SetBucketMin(float val)
		{
			this.BucketMin = val;
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06003BED RID: 15341 RVA: 0x000C1884 File Offset: 0x000BFA84
		// (set) Token: 0x06003BEE RID: 15342 RVA: 0x000C188C File Offset: 0x000BFA8C
		public float BucketMax
		{
			get
			{
				return this._BucketMax;
			}
			set
			{
				this._BucketMax = value;
				this.HasBucketMax = true;
			}
		}

		// Token: 0x06003BEF RID: 15343 RVA: 0x000C189C File Offset: 0x000BFA9C
		public void SetBucketMax(float val)
		{
			this.BucketMax = val;
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06003BF0 RID: 15344 RVA: 0x000C18A5 File Offset: 0x000BFAA5
		// (set) Token: 0x06003BF1 RID: 15345 RVA: 0x000C18AD File Offset: 0x000BFAAD
		public uint WaitMilliseconds
		{
			get
			{
				return this._WaitMilliseconds;
			}
			set
			{
				this._WaitMilliseconds = value;
				this.HasWaitMilliseconds = true;
			}
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x000C18BD File Offset: 0x000BFABD
		public void SetWaitMilliseconds(uint val)
		{
			this.WaitMilliseconds = val;
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06003BF3 RID: 15347 RVA: 0x000C18C6 File Offset: 0x000BFAC6
		// (set) Token: 0x06003BF4 RID: 15348 RVA: 0x000C18CE File Offset: 0x000BFACE
		public uint GamesPerHour
		{
			get
			{
				return this._GamesPerHour;
			}
			set
			{
				this._GamesPerHour = value;
				this.HasGamesPerHour = true;
			}
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x000C18DE File Offset: 0x000BFADE
		public void SetGamesPerHour(uint val)
		{
			this.GamesPerHour = val;
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06003BF6 RID: 15350 RVA: 0x000C18E7 File Offset: 0x000BFAE7
		// (set) Token: 0x06003BF7 RID: 15351 RVA: 0x000C18EF File Offset: 0x000BFAEF
		public uint ActiveGames
		{
			get
			{
				return this._ActiveGames;
			}
			set
			{
				this._ActiveGames = value;
				this.HasActiveGames = true;
			}
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x000C18FF File Offset: 0x000BFAFF
		public void SetActiveGames(uint val)
		{
			this.ActiveGames = val;
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06003BF9 RID: 15353 RVA: 0x000C1908 File Offset: 0x000BFB08
		// (set) Token: 0x06003BFA RID: 15354 RVA: 0x000C1910 File Offset: 0x000BFB10
		public uint ActivePlayers
		{
			get
			{
				return this._ActivePlayers;
			}
			set
			{
				this._ActivePlayers = value;
				this.HasActivePlayers = true;
			}
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x000C1920 File Offset: 0x000BFB20
		public void SetActivePlayers(uint val)
		{
			this.ActivePlayers = val;
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06003BFC RID: 15356 RVA: 0x000C1929 File Offset: 0x000BFB29
		// (set) Token: 0x06003BFD RID: 15357 RVA: 0x000C1931 File Offset: 0x000BFB31
		public uint FormingGames
		{
			get
			{
				return this._FormingGames;
			}
			set
			{
				this._FormingGames = value;
				this.HasFormingGames = true;
			}
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x000C1941 File Offset: 0x000BFB41
		public void SetFormingGames(uint val)
		{
			this.FormingGames = val;
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06003BFF RID: 15359 RVA: 0x000C194A File Offset: 0x000BFB4A
		// (set) Token: 0x06003C00 RID: 15360 RVA: 0x000C1952 File Offset: 0x000BFB52
		public uint WaitingPlayers
		{
			get
			{
				return this._WaitingPlayers;
			}
			set
			{
				this._WaitingPlayers = value;
				this.HasWaitingPlayers = true;
			}
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x000C1962 File Offset: 0x000BFB62
		public void SetWaitingPlayers(uint val)
		{
			this.WaitingPlayers = val;
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06003C02 RID: 15362 RVA: 0x000C196B File Offset: 0x000BFB6B
		// (set) Token: 0x06003C03 RID: 15363 RVA: 0x000C1973 File Offset: 0x000BFB73
		public uint OpenJoinableGames
		{
			get
			{
				return this._OpenJoinableGames;
			}
			set
			{
				this._OpenJoinableGames = value;
				this.HasOpenJoinableGames = true;
			}
		}

		// Token: 0x06003C04 RID: 15364 RVA: 0x000C1983 File Offset: 0x000BFB83
		public void SetOpenJoinableGames(uint val)
		{
			this.OpenJoinableGames = val;
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06003C05 RID: 15365 RVA: 0x000C198C File Offset: 0x000BFB8C
		// (set) Token: 0x06003C06 RID: 15366 RVA: 0x000C1994 File Offset: 0x000BFB94
		public uint PlayersInOpenJoinableGames
		{
			get
			{
				return this._PlayersInOpenJoinableGames;
			}
			set
			{
				this._PlayersInOpenJoinableGames = value;
				this.HasPlayersInOpenJoinableGames = true;
			}
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x000C19A4 File Offset: 0x000BFBA4
		public void SetPlayersInOpenJoinableGames(uint val)
		{
			this.PlayersInOpenJoinableGames = val;
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06003C08 RID: 15368 RVA: 0x000C19AD File Offset: 0x000BFBAD
		// (set) Token: 0x06003C09 RID: 15369 RVA: 0x000C19B5 File Offset: 0x000BFBB5
		public uint OpenGamesTotal
		{
			get
			{
				return this._OpenGamesTotal;
			}
			set
			{
				this._OpenGamesTotal = value;
				this.HasOpenGamesTotal = true;
			}
		}

		// Token: 0x06003C0A RID: 15370 RVA: 0x000C19C5 File Offset: 0x000BFBC5
		public void SetOpenGamesTotal(uint val)
		{
			this.OpenGamesTotal = val;
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06003C0B RID: 15371 RVA: 0x000C19CE File Offset: 0x000BFBCE
		// (set) Token: 0x06003C0C RID: 15372 RVA: 0x000C19D6 File Offset: 0x000BFBD6
		public uint PlayersInOpenGamesTotal
		{
			get
			{
				return this._PlayersInOpenGamesTotal;
			}
			set
			{
				this._PlayersInOpenGamesTotal = value;
				this.HasPlayersInOpenGamesTotal = true;
			}
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x000C19E6 File Offset: 0x000BFBE6
		public void SetPlayersInOpenGamesTotal(uint val)
		{
			this.PlayersInOpenGamesTotal = val;
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x000C19F0 File Offset: 0x000BFBF0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasBucketMin)
			{
				num ^= this.BucketMin.GetHashCode();
			}
			if (this.HasBucketMax)
			{
				num ^= this.BucketMax.GetHashCode();
			}
			if (this.HasWaitMilliseconds)
			{
				num ^= this.WaitMilliseconds.GetHashCode();
			}
			if (this.HasGamesPerHour)
			{
				num ^= this.GamesPerHour.GetHashCode();
			}
			if (this.HasActiveGames)
			{
				num ^= this.ActiveGames.GetHashCode();
			}
			if (this.HasActivePlayers)
			{
				num ^= this.ActivePlayers.GetHashCode();
			}
			if (this.HasFormingGames)
			{
				num ^= this.FormingGames.GetHashCode();
			}
			if (this.HasWaitingPlayers)
			{
				num ^= this.WaitingPlayers.GetHashCode();
			}
			if (this.HasOpenJoinableGames)
			{
				num ^= this.OpenJoinableGames.GetHashCode();
			}
			if (this.HasPlayersInOpenJoinableGames)
			{
				num ^= this.PlayersInOpenJoinableGames.GetHashCode();
			}
			if (this.HasOpenGamesTotal)
			{
				num ^= this.OpenGamesTotal.GetHashCode();
			}
			if (this.HasPlayersInOpenGamesTotal)
			{
				num ^= this.PlayersInOpenGamesTotal.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x000C1B38 File Offset: 0x000BFD38
		public override bool Equals(object obj)
		{
			GameStatsBucket gameStatsBucket = obj as GameStatsBucket;
			return gameStatsBucket != null && this.HasBucketMin == gameStatsBucket.HasBucketMin && (!this.HasBucketMin || this.BucketMin.Equals(gameStatsBucket.BucketMin)) && this.HasBucketMax == gameStatsBucket.HasBucketMax && (!this.HasBucketMax || this.BucketMax.Equals(gameStatsBucket.BucketMax)) && this.HasWaitMilliseconds == gameStatsBucket.HasWaitMilliseconds && (!this.HasWaitMilliseconds || this.WaitMilliseconds.Equals(gameStatsBucket.WaitMilliseconds)) && this.HasGamesPerHour == gameStatsBucket.HasGamesPerHour && (!this.HasGamesPerHour || this.GamesPerHour.Equals(gameStatsBucket.GamesPerHour)) && this.HasActiveGames == gameStatsBucket.HasActiveGames && (!this.HasActiveGames || this.ActiveGames.Equals(gameStatsBucket.ActiveGames)) && this.HasActivePlayers == gameStatsBucket.HasActivePlayers && (!this.HasActivePlayers || this.ActivePlayers.Equals(gameStatsBucket.ActivePlayers)) && this.HasFormingGames == gameStatsBucket.HasFormingGames && (!this.HasFormingGames || this.FormingGames.Equals(gameStatsBucket.FormingGames)) && this.HasWaitingPlayers == gameStatsBucket.HasWaitingPlayers && (!this.HasWaitingPlayers || this.WaitingPlayers.Equals(gameStatsBucket.WaitingPlayers)) && this.HasOpenJoinableGames == gameStatsBucket.HasOpenJoinableGames && (!this.HasOpenJoinableGames || this.OpenJoinableGames.Equals(gameStatsBucket.OpenJoinableGames)) && this.HasPlayersInOpenJoinableGames == gameStatsBucket.HasPlayersInOpenJoinableGames && (!this.HasPlayersInOpenJoinableGames || this.PlayersInOpenJoinableGames.Equals(gameStatsBucket.PlayersInOpenJoinableGames)) && this.HasOpenGamesTotal == gameStatsBucket.HasOpenGamesTotal && (!this.HasOpenGamesTotal || this.OpenGamesTotal.Equals(gameStatsBucket.OpenGamesTotal)) && this.HasPlayersInOpenGamesTotal == gameStatsBucket.HasPlayersInOpenGamesTotal && (!this.HasPlayersInOpenGamesTotal || this.PlayersInOpenGamesTotal.Equals(gameStatsBucket.PlayersInOpenGamesTotal));
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06003C10 RID: 15376 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x000C1D7A File Offset: 0x000BFF7A
		public static GameStatsBucket ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameStatsBucket>(bs, 0, -1);
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x000C1D84 File Offset: 0x000BFF84
		public void Deserialize(Stream stream)
		{
			GameStatsBucket.Deserialize(stream, this);
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x000C1D8E File Offset: 0x000BFF8E
		public static GameStatsBucket Deserialize(Stream stream, GameStatsBucket instance)
		{
			return GameStatsBucket.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x000C1D9C File Offset: 0x000BFF9C
		public static GameStatsBucket DeserializeLengthDelimited(Stream stream)
		{
			GameStatsBucket gameStatsBucket = new GameStatsBucket();
			GameStatsBucket.DeserializeLengthDelimited(stream, gameStatsBucket);
			return gameStatsBucket;
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x000C1DB8 File Offset: 0x000BFFB8
		public static GameStatsBucket DeserializeLengthDelimited(Stream stream, GameStatsBucket instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameStatsBucket.Deserialize(stream, instance, num);
		}

		// Token: 0x06003C16 RID: 15382 RVA: 0x000C1DE0 File Offset: 0x000BFFE0
		public static GameStatsBucket Deserialize(Stream stream, GameStatsBucket instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.BucketMin = 0f;
			instance.BucketMax = 4.2949673E+09f;
			instance.WaitMilliseconds = 0U;
			instance.GamesPerHour = 0U;
			instance.ActiveGames = 0U;
			instance.ActivePlayers = 0U;
			instance.FormingGames = 0U;
			instance.WaitingPlayers = 0U;
			instance.OpenJoinableGames = 0U;
			instance.PlayersInOpenJoinableGames = 0U;
			instance.OpenGamesTotal = 0U;
			instance.PlayersInOpenGamesTotal = 0U;
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
					if (num <= 48)
					{
						if (num <= 24)
						{
							if (num == 13)
							{
								instance.BucketMin = binaryReader.ReadSingle();
								continue;
							}
							if (num == 21)
							{
								instance.BucketMax = binaryReader.ReadSingle();
								continue;
							}
							if (num == 24)
							{
								instance.WaitMilliseconds = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
						else
						{
							if (num == 32)
							{
								instance.GamesPerHour = ProtocolParser.ReadUInt32(stream);
								continue;
							}
							if (num == 40)
							{
								instance.ActiveGames = ProtocolParser.ReadUInt32(stream);
								continue;
							}
							if (num == 48)
							{
								instance.ActivePlayers = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
					}
					else if (num <= 72)
					{
						if (num == 56)
						{
							instance.FormingGames = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 64)
						{
							instance.WaitingPlayers = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 72)
						{
							instance.OpenJoinableGames = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
					else
					{
						if (num == 80)
						{
							instance.PlayersInOpenJoinableGames = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 88)
						{
							instance.OpenGamesTotal = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 96)
						{
							instance.PlayersInOpenGamesTotal = ProtocolParser.ReadUInt32(stream);
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

		// Token: 0x06003C17 RID: 15383 RVA: 0x000C2002 File Offset: 0x000C0202
		public void Serialize(Stream stream)
		{
			GameStatsBucket.Serialize(stream, this);
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x000C200C File Offset: 0x000C020C
		public static void Serialize(Stream stream, GameStatsBucket instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasBucketMin)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.BucketMin);
			}
			if (instance.HasBucketMax)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.BucketMax);
			}
			if (instance.HasWaitMilliseconds)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.WaitMilliseconds);
			}
			if (instance.HasGamesPerHour)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.GamesPerHour);
			}
			if (instance.HasActiveGames)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt32(stream, instance.ActiveGames);
			}
			if (instance.HasActivePlayers)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.ActivePlayers);
			}
			if (instance.HasFormingGames)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt32(stream, instance.FormingGames);
			}
			if (instance.HasWaitingPlayers)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt32(stream, instance.WaitingPlayers);
			}
			if (instance.HasOpenJoinableGames)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt32(stream, instance.OpenJoinableGames);
			}
			if (instance.HasPlayersInOpenJoinableGames)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt32(stream, instance.PlayersInOpenJoinableGames);
			}
			if (instance.HasOpenGamesTotal)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt32(stream, instance.OpenGamesTotal);
			}
			if (instance.HasPlayersInOpenGamesTotal)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt32(stream, instance.PlayersInOpenGamesTotal);
			}
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x000C2170 File Offset: 0x000C0370
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasBucketMin)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasBucketMax)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasWaitMilliseconds)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.WaitMilliseconds);
			}
			if (this.HasGamesPerHour)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.GamesPerHour);
			}
			if (this.HasActiveGames)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.ActiveGames);
			}
			if (this.HasActivePlayers)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.ActivePlayers);
			}
			if (this.HasFormingGames)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.FormingGames);
			}
			if (this.HasWaitingPlayers)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.WaitingPlayers);
			}
			if (this.HasOpenJoinableGames)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.OpenJoinableGames);
			}
			if (this.HasPlayersInOpenJoinableGames)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.PlayersInOpenJoinableGames);
			}
			if (this.HasOpenGamesTotal)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.OpenGamesTotal);
			}
			if (this.HasPlayersInOpenGamesTotal)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.PlayersInOpenGamesTotal);
			}
			return num;
		}

		// Token: 0x04001591 RID: 5521
		public bool HasBucketMin;

		// Token: 0x04001592 RID: 5522
		private float _BucketMin;

		// Token: 0x04001593 RID: 5523
		public bool HasBucketMax;

		// Token: 0x04001594 RID: 5524
		private float _BucketMax;

		// Token: 0x04001595 RID: 5525
		public bool HasWaitMilliseconds;

		// Token: 0x04001596 RID: 5526
		private uint _WaitMilliseconds;

		// Token: 0x04001597 RID: 5527
		public bool HasGamesPerHour;

		// Token: 0x04001598 RID: 5528
		private uint _GamesPerHour;

		// Token: 0x04001599 RID: 5529
		public bool HasActiveGames;

		// Token: 0x0400159A RID: 5530
		private uint _ActiveGames;

		// Token: 0x0400159B RID: 5531
		public bool HasActivePlayers;

		// Token: 0x0400159C RID: 5532
		private uint _ActivePlayers;

		// Token: 0x0400159D RID: 5533
		public bool HasFormingGames;

		// Token: 0x0400159E RID: 5534
		private uint _FormingGames;

		// Token: 0x0400159F RID: 5535
		public bool HasWaitingPlayers;

		// Token: 0x040015A0 RID: 5536
		private uint _WaitingPlayers;

		// Token: 0x040015A1 RID: 5537
		public bool HasOpenJoinableGames;

		// Token: 0x040015A2 RID: 5538
		private uint _OpenJoinableGames;

		// Token: 0x040015A3 RID: 5539
		public bool HasPlayersInOpenJoinableGames;

		// Token: 0x040015A4 RID: 5540
		private uint _PlayersInOpenJoinableGames;

		// Token: 0x040015A5 RID: 5541
		public bool HasOpenGamesTotal;

		// Token: 0x040015A6 RID: 5542
		private uint _OpenGamesTotal;

		// Token: 0x040015A7 RID: 5543
		public bool HasPlayersInOpenGamesTotal;

		// Token: 0x040015A8 RID: 5544
		private uint _PlayersInOpenGamesTotal;
	}
}
