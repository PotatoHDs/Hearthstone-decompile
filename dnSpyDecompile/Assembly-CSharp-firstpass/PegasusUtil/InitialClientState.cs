using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x0200009F RID: 159
	public class InitialClientState : IProtoBuf
	{
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x000270CF File Offset: 0x000252CF
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x000270D7 File Offset: 0x000252D7
		public Collection Collection
		{
			get
			{
				return this._Collection;
			}
			set
			{
				this._Collection = value;
				this.HasCollection = (value != null);
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x000270EA File Offset: 0x000252EA
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x000270F2 File Offset: 0x000252F2
		public ProfileNotices Notices
		{
			get
			{
				return this._Notices;
			}
			set
			{
				this._Notices = value;
				this.HasNotices = (value != null);
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x00027105 File Offset: 0x00025305
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x0002710D File Offset: 0x0002530D
		public Achieves Achievements
		{
			get
			{
				return this._Achievements;
			}
			set
			{
				this._Achievements = value;
				this.HasAchievements = (value != null);
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x00027120 File Offset: 0x00025320
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x00027128 File Offset: 0x00025328
		public GameCurrencyStates GameCurrencyStates
		{
			get
			{
				return this._GameCurrencyStates;
			}
			set
			{
				this._GameCurrencyStates = value;
				this.HasGameCurrencyStates = (value != null);
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0002713B File Offset: 0x0002533B
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x00027143 File Offset: 0x00025343
		public ClientOptions ClientOptions
		{
			get
			{
				return this._ClientOptions;
			}
			set
			{
				this._ClientOptions = value;
				this.HasClientOptions = (value != null);
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00027156 File Offset: 0x00025356
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x0002715E File Offset: 0x0002535E
		public GuardianVars GuardianVars
		{
			get
			{
				return this._GuardianVars;
			}
			set
			{
				this._GuardianVars = value;
				this.HasGuardianVars = (value != null);
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x00027171 File Offset: 0x00025371
		// (set) Token: 0x06000AAC RID: 2732 RVA: 0x00027179 File Offset: 0x00025379
		public List<SpecialEventTiming> SpecialEventTiming
		{
			get
			{
				return this._SpecialEventTiming;
			}
			set
			{
				this._SpecialEventTiming = value;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x00027182 File Offset: 0x00025382
		// (set) Token: 0x06000AAE RID: 2734 RVA: 0x0002718A File Offset: 0x0002538A
		public List<TavernBrawlInfo> TavernBrawlsList
		{
			get
			{
				return this._TavernBrawlsList;
			}
			set
			{
				this._TavernBrawlsList = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x00027193 File Offset: 0x00025393
		// (set) Token: 0x06000AB0 RID: 2736 RVA: 0x0002719B File Offset: 0x0002539B
		public Boosters Boosters
		{
			get
			{
				return this._Boosters;
			}
			set
			{
				this._Boosters = value;
				this.HasBoosters = (value != null);
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x000271AE File Offset: 0x000253AE
		// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x000271B6 File Offset: 0x000253B6
		public GameConnectionInfo DisconnectedGame
		{
			get
			{
				return this._DisconnectedGame;
			}
			set
			{
				this._DisconnectedGame = value;
				this.HasDisconnectedGame = (value != null);
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x000271C9 File Offset: 0x000253C9
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x000271D1 File Offset: 0x000253D1
		public ArenaSessionResponse ArenaSession
		{
			get
			{
				return this._ArenaSession;
			}
			set
			{
				this._ArenaSession = value;
				this.HasArenaSession = (value != null);
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x000271E4 File Offset: 0x000253E4
		// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x000271EC File Offset: 0x000253EC
		public int DisplayBanner
		{
			get
			{
				return this._DisplayBanner;
			}
			set
			{
				this._DisplayBanner = value;
				this.HasDisplayBanner = true;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x000271FC File Offset: 0x000253FC
		// (set) Token: 0x06000AB8 RID: 2744 RVA: 0x00027204 File Offset: 0x00025404
		public List<DeckInfo> Decks
		{
			get
			{
				return this._Decks;
			}
			set
			{
				this._Decks = value;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0002720D File Offset: 0x0002540D
		// (set) Token: 0x06000ABA RID: 2746 RVA: 0x00027215 File Offset: 0x00025415
		public MedalInfo MedalInfo
		{
			get
			{
				return this._MedalInfo;
			}
			set
			{
				this._MedalInfo = value;
				this.HasMedalInfo = (value != null);
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x00027228 File Offset: 0x00025428
		// (set) Token: 0x06000ABC RID: 2748 RVA: 0x00027230 File Offset: 0x00025430
		public long DevTimeOffsetSeconds
		{
			get
			{
				return this._DevTimeOffsetSeconds;
			}
			set
			{
				this._DevTimeOffsetSeconds = value;
				this.HasDevTimeOffsetSeconds = true;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00027240 File Offset: 0x00025440
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x00027248 File Offset: 0x00025448
		public List<GameSaveDataUpdate> GameSaveData
		{
			get
			{
				return this._GameSaveData;
			}
			set
			{
				this._GameSaveData = value;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00027251 File Offset: 0x00025451
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x00027259 File Offset: 0x00025459
		public List<DeckContents> DeckContents
		{
			get
			{
				return this._DeckContents;
			}
			set
			{
				this._DeckContents = value;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00027262 File Offset: 0x00025462
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x0002726A File Offset: 0x0002546A
		public List<long> ValidCachedDeckIds
		{
			get
			{
				return this._ValidCachedDeckIds;
			}
			set
			{
				this._ValidCachedDeckIds = value;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x00027273 File Offset: 0x00025473
		// (set) Token: 0x06000AC4 RID: 2756 RVA: 0x0002727B File Offset: 0x0002547B
		public long PlayerId
		{
			get
			{
				return this._PlayerId;
			}
			set
			{
				this._PlayerId = value;
				this.HasPlayerId = true;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0002728B File Offset: 0x0002548B
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x00027293 File Offset: 0x00025493
		public PlayerDraftTickets PlayerDraftTickets
		{
			get
			{
				return this._PlayerDraftTickets;
			}
			set
			{
				this._PlayerDraftTickets = value;
				this.HasPlayerDraftTickets = (value != null);
			}
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x000272A8 File Offset: 0x000254A8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasCollection)
			{
				num ^= this.Collection.GetHashCode();
			}
			if (this.HasNotices)
			{
				num ^= this.Notices.GetHashCode();
			}
			if (this.HasAchievements)
			{
				num ^= this.Achievements.GetHashCode();
			}
			if (this.HasGameCurrencyStates)
			{
				num ^= this.GameCurrencyStates.GetHashCode();
			}
			if (this.HasClientOptions)
			{
				num ^= this.ClientOptions.GetHashCode();
			}
			if (this.HasGuardianVars)
			{
				num ^= this.GuardianVars.GetHashCode();
			}
			foreach (SpecialEventTiming specialEventTiming in this.SpecialEventTiming)
			{
				num ^= specialEventTiming.GetHashCode();
			}
			foreach (TavernBrawlInfo tavernBrawlInfo in this.TavernBrawlsList)
			{
				num ^= tavernBrawlInfo.GetHashCode();
			}
			if (this.HasBoosters)
			{
				num ^= this.Boosters.GetHashCode();
			}
			if (this.HasDisconnectedGame)
			{
				num ^= this.DisconnectedGame.GetHashCode();
			}
			if (this.HasArenaSession)
			{
				num ^= this.ArenaSession.GetHashCode();
			}
			if (this.HasDisplayBanner)
			{
				num ^= this.DisplayBanner.GetHashCode();
			}
			foreach (DeckInfo deckInfo in this.Decks)
			{
				num ^= deckInfo.GetHashCode();
			}
			if (this.HasMedalInfo)
			{
				num ^= this.MedalInfo.GetHashCode();
			}
			if (this.HasDevTimeOffsetSeconds)
			{
				num ^= this.DevTimeOffsetSeconds.GetHashCode();
			}
			foreach (GameSaveDataUpdate gameSaveDataUpdate in this.GameSaveData)
			{
				num ^= gameSaveDataUpdate.GetHashCode();
			}
			foreach (DeckContents deckContents in this.DeckContents)
			{
				num ^= deckContents.GetHashCode();
			}
			foreach (long num2 in this.ValidCachedDeckIds)
			{
				num ^= num2.GetHashCode();
			}
			if (this.HasPlayerId)
			{
				num ^= this.PlayerId.GetHashCode();
			}
			if (this.HasPlayerDraftTickets)
			{
				num ^= this.PlayerDraftTickets.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x000275AC File Offset: 0x000257AC
		public override bool Equals(object obj)
		{
			InitialClientState initialClientState = obj as InitialClientState;
			if (initialClientState == null)
			{
				return false;
			}
			if (this.HasCollection != initialClientState.HasCollection || (this.HasCollection && !this.Collection.Equals(initialClientState.Collection)))
			{
				return false;
			}
			if (this.HasNotices != initialClientState.HasNotices || (this.HasNotices && !this.Notices.Equals(initialClientState.Notices)))
			{
				return false;
			}
			if (this.HasAchievements != initialClientState.HasAchievements || (this.HasAchievements && !this.Achievements.Equals(initialClientState.Achievements)))
			{
				return false;
			}
			if (this.HasGameCurrencyStates != initialClientState.HasGameCurrencyStates || (this.HasGameCurrencyStates && !this.GameCurrencyStates.Equals(initialClientState.GameCurrencyStates)))
			{
				return false;
			}
			if (this.HasClientOptions != initialClientState.HasClientOptions || (this.HasClientOptions && !this.ClientOptions.Equals(initialClientState.ClientOptions)))
			{
				return false;
			}
			if (this.HasGuardianVars != initialClientState.HasGuardianVars || (this.HasGuardianVars && !this.GuardianVars.Equals(initialClientState.GuardianVars)))
			{
				return false;
			}
			if (this.SpecialEventTiming.Count != initialClientState.SpecialEventTiming.Count)
			{
				return false;
			}
			for (int i = 0; i < this.SpecialEventTiming.Count; i++)
			{
				if (!this.SpecialEventTiming[i].Equals(initialClientState.SpecialEventTiming[i]))
				{
					return false;
				}
			}
			if (this.TavernBrawlsList.Count != initialClientState.TavernBrawlsList.Count)
			{
				return false;
			}
			for (int j = 0; j < this.TavernBrawlsList.Count; j++)
			{
				if (!this.TavernBrawlsList[j].Equals(initialClientState.TavernBrawlsList[j]))
				{
					return false;
				}
			}
			if (this.HasBoosters != initialClientState.HasBoosters || (this.HasBoosters && !this.Boosters.Equals(initialClientState.Boosters)))
			{
				return false;
			}
			if (this.HasDisconnectedGame != initialClientState.HasDisconnectedGame || (this.HasDisconnectedGame && !this.DisconnectedGame.Equals(initialClientState.DisconnectedGame)))
			{
				return false;
			}
			if (this.HasArenaSession != initialClientState.HasArenaSession || (this.HasArenaSession && !this.ArenaSession.Equals(initialClientState.ArenaSession)))
			{
				return false;
			}
			if (this.HasDisplayBanner != initialClientState.HasDisplayBanner || (this.HasDisplayBanner && !this.DisplayBanner.Equals(initialClientState.DisplayBanner)))
			{
				return false;
			}
			if (this.Decks.Count != initialClientState.Decks.Count)
			{
				return false;
			}
			for (int k = 0; k < this.Decks.Count; k++)
			{
				if (!this.Decks[k].Equals(initialClientState.Decks[k]))
				{
					return false;
				}
			}
			if (this.HasMedalInfo != initialClientState.HasMedalInfo || (this.HasMedalInfo && !this.MedalInfo.Equals(initialClientState.MedalInfo)))
			{
				return false;
			}
			if (this.HasDevTimeOffsetSeconds != initialClientState.HasDevTimeOffsetSeconds || (this.HasDevTimeOffsetSeconds && !this.DevTimeOffsetSeconds.Equals(initialClientState.DevTimeOffsetSeconds)))
			{
				return false;
			}
			if (this.GameSaveData.Count != initialClientState.GameSaveData.Count)
			{
				return false;
			}
			for (int l = 0; l < this.GameSaveData.Count; l++)
			{
				if (!this.GameSaveData[l].Equals(initialClientState.GameSaveData[l]))
				{
					return false;
				}
			}
			if (this.DeckContents.Count != initialClientState.DeckContents.Count)
			{
				return false;
			}
			for (int m = 0; m < this.DeckContents.Count; m++)
			{
				if (!this.DeckContents[m].Equals(initialClientState.DeckContents[m]))
				{
					return false;
				}
			}
			if (this.ValidCachedDeckIds.Count != initialClientState.ValidCachedDeckIds.Count)
			{
				return false;
			}
			for (int n = 0; n < this.ValidCachedDeckIds.Count; n++)
			{
				if (!this.ValidCachedDeckIds[n].Equals(initialClientState.ValidCachedDeckIds[n]))
				{
					return false;
				}
			}
			return this.HasPlayerId == initialClientState.HasPlayerId && (!this.HasPlayerId || this.PlayerId.Equals(initialClientState.PlayerId)) && this.HasPlayerDraftTickets == initialClientState.HasPlayerDraftTickets && (!this.HasPlayerDraftTickets || this.PlayerDraftTickets.Equals(initialClientState.PlayerDraftTickets));
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00027A2D File Offset: 0x00025C2D
		public void Deserialize(Stream stream)
		{
			InitialClientState.Deserialize(stream, this);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00027A37 File Offset: 0x00025C37
		public static InitialClientState Deserialize(Stream stream, InitialClientState instance)
		{
			return InitialClientState.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00027A44 File Offset: 0x00025C44
		public static InitialClientState DeserializeLengthDelimited(Stream stream)
		{
			InitialClientState initialClientState = new InitialClientState();
			InitialClientState.DeserializeLengthDelimited(stream, initialClientState);
			return initialClientState;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00027A60 File Offset: 0x00025C60
		public static InitialClientState DeserializeLengthDelimited(Stream stream, InitialClientState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InitialClientState.Deserialize(stream, instance, num);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00027A88 File Offset: 0x00025C88
		public static InitialClientState Deserialize(Stream stream, InitialClientState instance, long limit)
		{
			if (instance.SpecialEventTiming == null)
			{
				instance.SpecialEventTiming = new List<SpecialEventTiming>();
			}
			if (instance.TavernBrawlsList == null)
			{
				instance.TavernBrawlsList = new List<TavernBrawlInfo>();
			}
			instance.DisplayBanner = 0;
			if (instance.Decks == null)
			{
				instance.Decks = new List<DeckInfo>();
			}
			if (instance.GameSaveData == null)
			{
				instance.GameSaveData = new List<GameSaveDataUpdate>();
			}
			if (instance.DeckContents == null)
			{
				instance.DeckContents = new List<DeckContents>();
			}
			if (instance.ValidCachedDeckIds == null)
			{
				instance.ValidCachedDeckIds = new List<long>();
			}
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
					if (num <= 58)
					{
						if (num <= 26)
						{
							if (num != 10)
							{
								if (num != 18)
								{
									if (num == 26)
									{
										if (instance.Achievements == null)
										{
											instance.Achievements = Achieves.DeserializeLengthDelimited(stream);
											continue;
										}
										Achieves.DeserializeLengthDelimited(stream, instance.Achievements);
										continue;
									}
								}
								else
								{
									if (instance.Notices == null)
									{
										instance.Notices = ProfileNotices.DeserializeLengthDelimited(stream);
										continue;
									}
									ProfileNotices.DeserializeLengthDelimited(stream, instance.Notices);
									continue;
								}
							}
							else
							{
								if (instance.Collection == null)
								{
									instance.Collection = Collection.DeserializeLengthDelimited(stream);
									continue;
								}
								Collection.DeserializeLengthDelimited(stream, instance.Collection);
								continue;
							}
						}
						else if (num <= 42)
						{
							if (num != 34)
							{
								if (num == 42)
								{
									if (instance.ClientOptions == null)
									{
										instance.ClientOptions = ClientOptions.DeserializeLengthDelimited(stream);
										continue;
									}
									ClientOptions.DeserializeLengthDelimited(stream, instance.ClientOptions);
									continue;
								}
							}
							else
							{
								if (instance.GameCurrencyStates == null)
								{
									instance.GameCurrencyStates = GameCurrencyStates.DeserializeLengthDelimited(stream);
									continue;
								}
								GameCurrencyStates.DeserializeLengthDelimited(stream, instance.GameCurrencyStates);
								continue;
							}
						}
						else if (num != 50)
						{
							if (num == 58)
							{
								instance.SpecialEventTiming.Add(PegasusUtil.SpecialEventTiming.DeserializeLengthDelimited(stream));
								continue;
							}
						}
						else
						{
							if (instance.GuardianVars == null)
							{
								instance.GuardianVars = GuardianVars.DeserializeLengthDelimited(stream);
								continue;
							}
							GuardianVars.DeserializeLengthDelimited(stream, instance.GuardianVars);
							continue;
						}
					}
					else if (num <= 90)
					{
						if (num <= 74)
						{
							if (num == 66)
							{
								instance.TavernBrawlsList.Add(TavernBrawlInfo.DeserializeLengthDelimited(stream));
								continue;
							}
							if (num == 74)
							{
								if (instance.Boosters == null)
								{
									instance.Boosters = Boosters.DeserializeLengthDelimited(stream);
									continue;
								}
								Boosters.DeserializeLengthDelimited(stream, instance.Boosters);
								continue;
							}
						}
						else if (num != 82)
						{
							if (num == 90)
							{
								if (instance.ArenaSession == null)
								{
									instance.ArenaSession = ArenaSessionResponse.DeserializeLengthDelimited(stream);
									continue;
								}
								ArenaSessionResponse.DeserializeLengthDelimited(stream, instance.ArenaSession);
								continue;
							}
						}
						else
						{
							if (instance.DisconnectedGame == null)
							{
								instance.DisconnectedGame = GameConnectionInfo.DeserializeLengthDelimited(stream);
								continue;
							}
							GameConnectionInfo.DeserializeLengthDelimited(stream, instance.DisconnectedGame);
							continue;
						}
					}
					else if (num <= 106)
					{
						if (num == 96)
						{
							instance.DisplayBanner = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 106)
						{
							instance.Decks.Add(DeckInfo.DeserializeLengthDelimited(stream));
							continue;
						}
					}
					else if (num != 114)
					{
						if (num == 120)
						{
							instance.DevTimeOffsetSeconds = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (instance.MedalInfo == null)
						{
							instance.MedalInfo = MedalInfo.DeserializeLengthDelimited(stream);
							continue;
						}
						MedalInfo.DeserializeLengthDelimited(stream, instance.MedalInfo);
						continue;
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					switch (field)
					{
					case 16U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.GameSaveData.Add(GameSaveDataUpdate.DeserializeLengthDelimited(stream));
						}
						break;
					case 17U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.DeckContents.Add(PegasusUtil.DeckContents.DeserializeLengthDelimited(stream));
						}
						break;
					case 18U:
						if (key.WireType == Wire.Varint)
						{
							instance.ValidCachedDeckIds.Add((long)ProtocolParser.ReadUInt64(stream));
						}
						break;
					case 19U:
						if (key.WireType == Wire.Varint)
						{
							instance.PlayerId = (long)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 20U:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.PlayerDraftTickets == null)
							{
								instance.PlayerDraftTickets = PlayerDraftTickets.DeserializeLengthDelimited(stream);
							}
							else
							{
								PlayerDraftTickets.DeserializeLengthDelimited(stream, instance.PlayerDraftTickets);
							}
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00027F1D File Offset: 0x0002611D
		public void Serialize(Stream stream)
		{
			InitialClientState.Serialize(stream, this);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00027F28 File Offset: 0x00026128
		public static void Serialize(Stream stream, InitialClientState instance)
		{
			if (instance.HasCollection)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Collection.GetSerializedSize());
				Collection.Serialize(stream, instance.Collection);
			}
			if (instance.HasNotices)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Notices.GetSerializedSize());
				ProfileNotices.Serialize(stream, instance.Notices);
			}
			if (instance.HasAchievements)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Achievements.GetSerializedSize());
				Achieves.Serialize(stream, instance.Achievements);
			}
			if (instance.HasGameCurrencyStates)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.GameCurrencyStates.GetSerializedSize());
				GameCurrencyStates.Serialize(stream, instance.GameCurrencyStates);
			}
			if (instance.HasClientOptions)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.ClientOptions.GetSerializedSize());
				ClientOptions.Serialize(stream, instance.ClientOptions);
			}
			if (instance.HasGuardianVars)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.GuardianVars.GetSerializedSize());
				GuardianVars.Serialize(stream, instance.GuardianVars);
			}
			if (instance.SpecialEventTiming.Count > 0)
			{
				foreach (SpecialEventTiming specialEventTiming in instance.SpecialEventTiming)
				{
					stream.WriteByte(58);
					ProtocolParser.WriteUInt32(stream, specialEventTiming.GetSerializedSize());
					PegasusUtil.SpecialEventTiming.Serialize(stream, specialEventTiming);
				}
			}
			if (instance.TavernBrawlsList.Count > 0)
			{
				foreach (TavernBrawlInfo tavernBrawlInfo in instance.TavernBrawlsList)
				{
					stream.WriteByte(66);
					ProtocolParser.WriteUInt32(stream, tavernBrawlInfo.GetSerializedSize());
					TavernBrawlInfo.Serialize(stream, tavernBrawlInfo);
				}
			}
			if (instance.HasBoosters)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteUInt32(stream, instance.Boosters.GetSerializedSize());
				Boosters.Serialize(stream, instance.Boosters);
			}
			if (instance.HasDisconnectedGame)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.DisconnectedGame.GetSerializedSize());
				GameConnectionInfo.Serialize(stream, instance.DisconnectedGame);
			}
			if (instance.HasArenaSession)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteUInt32(stream, instance.ArenaSession.GetSerializedSize());
				ArenaSessionResponse.Serialize(stream, instance.ArenaSession);
			}
			if (instance.HasDisplayBanner)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DisplayBanner));
			}
			if (instance.Decks.Count > 0)
			{
				foreach (DeckInfo deckInfo in instance.Decks)
				{
					stream.WriteByte(106);
					ProtocolParser.WriteUInt32(stream, deckInfo.GetSerializedSize());
					DeckInfo.Serialize(stream, deckInfo);
				}
			}
			if (instance.HasMedalInfo)
			{
				stream.WriteByte(114);
				ProtocolParser.WriteUInt32(stream, instance.MedalInfo.GetSerializedSize());
				MedalInfo.Serialize(stream, instance.MedalInfo);
			}
			if (instance.HasDevTimeOffsetSeconds)
			{
				stream.WriteByte(120);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DevTimeOffsetSeconds);
			}
			if (instance.GameSaveData.Count > 0)
			{
				foreach (GameSaveDataUpdate gameSaveDataUpdate in instance.GameSaveData)
				{
					stream.WriteByte(130);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, gameSaveDataUpdate.GetSerializedSize());
					GameSaveDataUpdate.Serialize(stream, gameSaveDataUpdate);
				}
			}
			if (instance.DeckContents.Count > 0)
			{
				foreach (DeckContents deckContents in instance.DeckContents)
				{
					stream.WriteByte(138);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, deckContents.GetSerializedSize());
					PegasusUtil.DeckContents.Serialize(stream, deckContents);
				}
			}
			if (instance.ValidCachedDeckIds.Count > 0)
			{
				foreach (long val in instance.ValidCachedDeckIds)
				{
					stream.WriteByte(144);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt64(stream, (ulong)val);
				}
			}
			if (instance.HasPlayerId)
			{
				stream.WriteByte(152);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerId);
			}
			if (instance.HasPlayerDraftTickets)
			{
				stream.WriteByte(162);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.PlayerDraftTickets.GetSerializedSize());
				PlayerDraftTickets.Serialize(stream, instance.PlayerDraftTickets);
			}
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0002840C File Offset: 0x0002660C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasCollection)
			{
				num += 1U;
				uint serializedSize = this.Collection.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasNotices)
			{
				num += 1U;
				uint serializedSize2 = this.Notices.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasAchievements)
			{
				num += 1U;
				uint serializedSize3 = this.Achievements.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasGameCurrencyStates)
			{
				num += 1U;
				uint serializedSize4 = this.GameCurrencyStates.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasClientOptions)
			{
				num += 1U;
				uint serializedSize5 = this.ClientOptions.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (this.HasGuardianVars)
			{
				num += 1U;
				uint serializedSize6 = this.GuardianVars.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			if (this.SpecialEventTiming.Count > 0)
			{
				foreach (SpecialEventTiming specialEventTiming in this.SpecialEventTiming)
				{
					num += 1U;
					uint serializedSize7 = specialEventTiming.GetSerializedSize();
					num += serializedSize7 + ProtocolParser.SizeOfUInt32(serializedSize7);
				}
			}
			if (this.TavernBrawlsList.Count > 0)
			{
				foreach (TavernBrawlInfo tavernBrawlInfo in this.TavernBrawlsList)
				{
					num += 1U;
					uint serializedSize8 = tavernBrawlInfo.GetSerializedSize();
					num += serializedSize8 + ProtocolParser.SizeOfUInt32(serializedSize8);
				}
			}
			if (this.HasBoosters)
			{
				num += 1U;
				uint serializedSize9 = this.Boosters.GetSerializedSize();
				num += serializedSize9 + ProtocolParser.SizeOfUInt32(serializedSize9);
			}
			if (this.HasDisconnectedGame)
			{
				num += 1U;
				uint serializedSize10 = this.DisconnectedGame.GetSerializedSize();
				num += serializedSize10 + ProtocolParser.SizeOfUInt32(serializedSize10);
			}
			if (this.HasArenaSession)
			{
				num += 1U;
				uint serializedSize11 = this.ArenaSession.GetSerializedSize();
				num += serializedSize11 + ProtocolParser.SizeOfUInt32(serializedSize11);
			}
			if (this.HasDisplayBanner)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DisplayBanner));
			}
			if (this.Decks.Count > 0)
			{
				foreach (DeckInfo deckInfo in this.Decks)
				{
					num += 1U;
					uint serializedSize12 = deckInfo.GetSerializedSize();
					num += serializedSize12 + ProtocolParser.SizeOfUInt32(serializedSize12);
				}
			}
			if (this.HasMedalInfo)
			{
				num += 1U;
				uint serializedSize13 = this.MedalInfo.GetSerializedSize();
				num += serializedSize13 + ProtocolParser.SizeOfUInt32(serializedSize13);
			}
			if (this.HasDevTimeOffsetSeconds)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.DevTimeOffsetSeconds);
			}
			if (this.GameSaveData.Count > 0)
			{
				foreach (GameSaveDataUpdate gameSaveDataUpdate in this.GameSaveData)
				{
					num += 2U;
					uint serializedSize14 = gameSaveDataUpdate.GetSerializedSize();
					num += serializedSize14 + ProtocolParser.SizeOfUInt32(serializedSize14);
				}
			}
			if (this.DeckContents.Count > 0)
			{
				foreach (DeckContents deckContents in this.DeckContents)
				{
					num += 2U;
					uint serializedSize15 = deckContents.GetSerializedSize();
					num += serializedSize15 + ProtocolParser.SizeOfUInt32(serializedSize15);
				}
			}
			if (this.ValidCachedDeckIds.Count > 0)
			{
				foreach (long val in this.ValidCachedDeckIds)
				{
					num += 2U;
					num += ProtocolParser.SizeOfUInt64((ulong)val);
				}
			}
			if (this.HasPlayerId)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.PlayerId);
			}
			if (this.HasPlayerDraftTickets)
			{
				num += 2U;
				uint serializedSize16 = this.PlayerDraftTickets.GetSerializedSize();
				num += serializedSize16 + ProtocolParser.SizeOfUInt32(serializedSize16);
			}
			return num;
		}

		// Token: 0x04000398 RID: 920
		public bool HasCollection;

		// Token: 0x04000399 RID: 921
		private Collection _Collection;

		// Token: 0x0400039A RID: 922
		public bool HasNotices;

		// Token: 0x0400039B RID: 923
		private ProfileNotices _Notices;

		// Token: 0x0400039C RID: 924
		public bool HasAchievements;

		// Token: 0x0400039D RID: 925
		private Achieves _Achievements;

		// Token: 0x0400039E RID: 926
		public bool HasGameCurrencyStates;

		// Token: 0x0400039F RID: 927
		private GameCurrencyStates _GameCurrencyStates;

		// Token: 0x040003A0 RID: 928
		public bool HasClientOptions;

		// Token: 0x040003A1 RID: 929
		private ClientOptions _ClientOptions;

		// Token: 0x040003A2 RID: 930
		public bool HasGuardianVars;

		// Token: 0x040003A3 RID: 931
		private GuardianVars _GuardianVars;

		// Token: 0x040003A4 RID: 932
		private List<SpecialEventTiming> _SpecialEventTiming = new List<SpecialEventTiming>();

		// Token: 0x040003A5 RID: 933
		private List<TavernBrawlInfo> _TavernBrawlsList = new List<TavernBrawlInfo>();

		// Token: 0x040003A6 RID: 934
		public bool HasBoosters;

		// Token: 0x040003A7 RID: 935
		private Boosters _Boosters;

		// Token: 0x040003A8 RID: 936
		public bool HasDisconnectedGame;

		// Token: 0x040003A9 RID: 937
		private GameConnectionInfo _DisconnectedGame;

		// Token: 0x040003AA RID: 938
		public bool HasArenaSession;

		// Token: 0x040003AB RID: 939
		private ArenaSessionResponse _ArenaSession;

		// Token: 0x040003AC RID: 940
		public bool HasDisplayBanner;

		// Token: 0x040003AD RID: 941
		private int _DisplayBanner;

		// Token: 0x040003AE RID: 942
		private List<DeckInfo> _Decks = new List<DeckInfo>();

		// Token: 0x040003AF RID: 943
		public bool HasMedalInfo;

		// Token: 0x040003B0 RID: 944
		private MedalInfo _MedalInfo;

		// Token: 0x040003B1 RID: 945
		public bool HasDevTimeOffsetSeconds;

		// Token: 0x040003B2 RID: 946
		private long _DevTimeOffsetSeconds;

		// Token: 0x040003B3 RID: 947
		private List<GameSaveDataUpdate> _GameSaveData = new List<GameSaveDataUpdate>();

		// Token: 0x040003B4 RID: 948
		private List<DeckContents> _DeckContents = new List<DeckContents>();

		// Token: 0x040003B5 RID: 949
		private List<long> _ValidCachedDeckIds = new List<long>();

		// Token: 0x040003B6 RID: 950
		public bool HasPlayerId;

		// Token: 0x040003B7 RID: 951
		private long _PlayerId;

		// Token: 0x040003B8 RID: 952
		public bool HasPlayerDraftTickets;

		// Token: 0x040003B9 RID: 953
		private PlayerDraftTickets _PlayerDraftTickets;

		// Token: 0x020005AA RID: 1450
		public enum PacketID
		{
			// Token: 0x04001F59 RID: 8025
			ID = 207,
			// Token: 0x04001F5A RID: 8026
			System = 0
		}
	}
}
