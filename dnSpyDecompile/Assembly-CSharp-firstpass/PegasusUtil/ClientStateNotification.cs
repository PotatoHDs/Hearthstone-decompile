using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000D9 RID: 217
	public class ClientStateNotification : IProtoBuf
	{
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00035A4B File Offset: 0x00033C4B
		// (set) Token: 0x06000EBE RID: 3774 RVA: 0x00035A53 File Offset: 0x00033C53
		public AchievementNotifications AchievementNotifications
		{
			get
			{
				return this._AchievementNotifications;
			}
			set
			{
				this._AchievementNotifications = value;
				this.HasAchievementNotifications = (value != null);
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00035A66 File Offset: 0x00033C66
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x00035A6E File Offset: 0x00033C6E
		public NoticeNotifications NoticeNotifications
		{
			get
			{
				return this._NoticeNotifications;
			}
			set
			{
				this._NoticeNotifications = value;
				this.HasNoticeNotifications = (value != null);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00035A81 File Offset: 0x00033C81
		// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x00035A89 File Offset: 0x00033C89
		public CollectionModifications CollectionModifications
		{
			get
			{
				return this._CollectionModifications;
			}
			set
			{
				this._CollectionModifications = value;
				this.HasCollectionModifications = (value != null);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00035A9C File Offset: 0x00033C9C
		// (set) Token: 0x06000EC4 RID: 3780 RVA: 0x00035AA4 File Offset: 0x00033CA4
		public GameCurrencyStates CurrencyState
		{
			get
			{
				return this._CurrencyState;
			}
			set
			{
				this._CurrencyState = value;
				this.HasCurrencyState = (value != null);
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00035AB7 File Offset: 0x00033CB7
		// (set) Token: 0x06000EC6 RID: 3782 RVA: 0x00035ABF File Offset: 0x00033CBF
		public BoosterModifications BoosterModifications
		{
			get
			{
				return this._BoosterModifications;
			}
			set
			{
				this._BoosterModifications = value;
				this.HasBoosterModifications = (value != null);
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00035AD2 File Offset: 0x00033CD2
		// (set) Token: 0x06000EC8 RID: 3784 RVA: 0x00035ADA File Offset: 0x00033CDA
		public HeroXP HeroXp
		{
			get
			{
				return this._HeroXp;
			}
			set
			{
				this._HeroXp = value;
				this.HasHeroXp = (value != null);
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00035AED File Offset: 0x00033CED
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x00035AF5 File Offset: 0x00033CF5
		public PlayerRecords PlayerRecords
		{
			get
			{
				return this._PlayerRecords;
			}
			set
			{
				this._PlayerRecords = value;
				this.HasPlayerRecords = (value != null);
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00035B08 File Offset: 0x00033D08
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x00035B10 File Offset: 0x00033D10
		public ArenaSessionResponse ArenaSessionResponse
		{
			get
			{
				return this._ArenaSessionResponse;
			}
			set
			{
				this._ArenaSessionResponse = value;
				this.HasArenaSessionResponse = (value != null);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x00035B23 File Offset: 0x00033D23
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x00035B2B File Offset: 0x00033D2B
		public CardBackModifications CardBackModifications
		{
			get
			{
				return this._CardBackModifications;
			}
			set
			{
				this._CardBackModifications = value;
				this.HasCardBackModifications = (value != null);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x00035B3E File Offset: 0x00033D3E
		// (set) Token: 0x06000ED0 RID: 3792 RVA: 0x00035B46 File Offset: 0x00033D46
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

		// Token: 0x06000ED1 RID: 3793 RVA: 0x00035B5C File Offset: 0x00033D5C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAchievementNotifications)
			{
				num ^= this.AchievementNotifications.GetHashCode();
			}
			if (this.HasNoticeNotifications)
			{
				num ^= this.NoticeNotifications.GetHashCode();
			}
			if (this.HasCollectionModifications)
			{
				num ^= this.CollectionModifications.GetHashCode();
			}
			if (this.HasCurrencyState)
			{
				num ^= this.CurrencyState.GetHashCode();
			}
			if (this.HasBoosterModifications)
			{
				num ^= this.BoosterModifications.GetHashCode();
			}
			if (this.HasHeroXp)
			{
				num ^= this.HeroXp.GetHashCode();
			}
			if (this.HasPlayerRecords)
			{
				num ^= this.PlayerRecords.GetHashCode();
			}
			if (this.HasArenaSessionResponse)
			{
				num ^= this.ArenaSessionResponse.GetHashCode();
			}
			if (this.HasCardBackModifications)
			{
				num ^= this.CardBackModifications.GetHashCode();
			}
			if (this.HasPlayerDraftTickets)
			{
				num ^= this.PlayerDraftTickets.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00035C54 File Offset: 0x00033E54
		public override bool Equals(object obj)
		{
			ClientStateNotification clientStateNotification = obj as ClientStateNotification;
			return clientStateNotification != null && this.HasAchievementNotifications == clientStateNotification.HasAchievementNotifications && (!this.HasAchievementNotifications || this.AchievementNotifications.Equals(clientStateNotification.AchievementNotifications)) && this.HasNoticeNotifications == clientStateNotification.HasNoticeNotifications && (!this.HasNoticeNotifications || this.NoticeNotifications.Equals(clientStateNotification.NoticeNotifications)) && this.HasCollectionModifications == clientStateNotification.HasCollectionModifications && (!this.HasCollectionModifications || this.CollectionModifications.Equals(clientStateNotification.CollectionModifications)) && this.HasCurrencyState == clientStateNotification.HasCurrencyState && (!this.HasCurrencyState || this.CurrencyState.Equals(clientStateNotification.CurrencyState)) && this.HasBoosterModifications == clientStateNotification.HasBoosterModifications && (!this.HasBoosterModifications || this.BoosterModifications.Equals(clientStateNotification.BoosterModifications)) && this.HasHeroXp == clientStateNotification.HasHeroXp && (!this.HasHeroXp || this.HeroXp.Equals(clientStateNotification.HeroXp)) && this.HasPlayerRecords == clientStateNotification.HasPlayerRecords && (!this.HasPlayerRecords || this.PlayerRecords.Equals(clientStateNotification.PlayerRecords)) && this.HasArenaSessionResponse == clientStateNotification.HasArenaSessionResponse && (!this.HasArenaSessionResponse || this.ArenaSessionResponse.Equals(clientStateNotification.ArenaSessionResponse)) && this.HasCardBackModifications == clientStateNotification.HasCardBackModifications && (!this.HasCardBackModifications || this.CardBackModifications.Equals(clientStateNotification.CardBackModifications)) && this.HasPlayerDraftTickets == clientStateNotification.HasPlayerDraftTickets && (!this.HasPlayerDraftTickets || this.PlayerDraftTickets.Equals(clientStateNotification.PlayerDraftTickets));
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00035E1C File Offset: 0x0003401C
		public void Deserialize(Stream stream)
		{
			ClientStateNotification.Deserialize(stream, this);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00035E26 File Offset: 0x00034026
		public static ClientStateNotification Deserialize(Stream stream, ClientStateNotification instance)
		{
			return ClientStateNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00035E34 File Offset: 0x00034034
		public static ClientStateNotification DeserializeLengthDelimited(Stream stream)
		{
			ClientStateNotification clientStateNotification = new ClientStateNotification();
			ClientStateNotification.DeserializeLengthDelimited(stream, clientStateNotification);
			return clientStateNotification;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00035E50 File Offset: 0x00034050
		public static ClientStateNotification DeserializeLengthDelimited(Stream stream, ClientStateNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ClientStateNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00035E78 File Offset: 0x00034078
		public static ClientStateNotification Deserialize(Stream stream, ClientStateNotification instance, long limit)
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
					if (num <= 42)
					{
						if (num <= 18)
						{
							if (num != 10)
							{
								if (num == 18)
								{
									if (instance.NoticeNotifications == null)
									{
										instance.NoticeNotifications = NoticeNotifications.DeserializeLengthDelimited(stream);
										continue;
									}
									NoticeNotifications.DeserializeLengthDelimited(stream, instance.NoticeNotifications);
									continue;
								}
							}
							else
							{
								if (instance.AchievementNotifications == null)
								{
									instance.AchievementNotifications = AchievementNotifications.DeserializeLengthDelimited(stream);
									continue;
								}
								AchievementNotifications.DeserializeLengthDelimited(stream, instance.AchievementNotifications);
								continue;
							}
						}
						else if (num != 26)
						{
							if (num != 34)
							{
								if (num == 42)
								{
									if (instance.BoosterModifications == null)
									{
										instance.BoosterModifications = BoosterModifications.DeserializeLengthDelimited(stream);
										continue;
									}
									BoosterModifications.DeserializeLengthDelimited(stream, instance.BoosterModifications);
									continue;
								}
							}
							else
							{
								if (instance.CurrencyState == null)
								{
									instance.CurrencyState = GameCurrencyStates.DeserializeLengthDelimited(stream);
									continue;
								}
								GameCurrencyStates.DeserializeLengthDelimited(stream, instance.CurrencyState);
								continue;
							}
						}
						else
						{
							if (instance.CollectionModifications == null)
							{
								instance.CollectionModifications = CollectionModifications.DeserializeLengthDelimited(stream);
								continue;
							}
							CollectionModifications.DeserializeLengthDelimited(stream, instance.CollectionModifications);
							continue;
						}
					}
					else if (num <= 58)
					{
						if (num != 50)
						{
							if (num == 58)
							{
								if (instance.PlayerRecords == null)
								{
									instance.PlayerRecords = PlayerRecords.DeserializeLengthDelimited(stream);
									continue;
								}
								PlayerRecords.DeserializeLengthDelimited(stream, instance.PlayerRecords);
								continue;
							}
						}
						else
						{
							if (instance.HeroXp == null)
							{
								instance.HeroXp = HeroXP.DeserializeLengthDelimited(stream);
								continue;
							}
							HeroXP.DeserializeLengthDelimited(stream, instance.HeroXp);
							continue;
						}
					}
					else if (num != 66)
					{
						if (num != 74)
						{
							if (num == 82)
							{
								if (instance.PlayerDraftTickets == null)
								{
									instance.PlayerDraftTickets = PlayerDraftTickets.DeserializeLengthDelimited(stream);
									continue;
								}
								PlayerDraftTickets.DeserializeLengthDelimited(stream, instance.PlayerDraftTickets);
								continue;
							}
						}
						else
						{
							if (instance.CardBackModifications == null)
							{
								instance.CardBackModifications = CardBackModifications.DeserializeLengthDelimited(stream);
								continue;
							}
							CardBackModifications.DeserializeLengthDelimited(stream, instance.CardBackModifications);
							continue;
						}
					}
					else
					{
						if (instance.ArenaSessionResponse == null)
						{
							instance.ArenaSessionResponse = ArenaSessionResponse.DeserializeLengthDelimited(stream);
							continue;
						}
						ArenaSessionResponse.DeserializeLengthDelimited(stream, instance.ArenaSessionResponse);
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

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0003610C File Offset: 0x0003430C
		public void Serialize(Stream stream)
		{
			ClientStateNotification.Serialize(stream, this);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00036118 File Offset: 0x00034318
		public static void Serialize(Stream stream, ClientStateNotification instance)
		{
			if (instance.HasAchievementNotifications)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AchievementNotifications.GetSerializedSize());
				AchievementNotifications.Serialize(stream, instance.AchievementNotifications);
			}
			if (instance.HasNoticeNotifications)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.NoticeNotifications.GetSerializedSize());
				NoticeNotifications.Serialize(stream, instance.NoticeNotifications);
			}
			if (instance.HasCollectionModifications)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.CollectionModifications.GetSerializedSize());
				CollectionModifications.Serialize(stream, instance.CollectionModifications);
			}
			if (instance.HasCurrencyState)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.CurrencyState.GetSerializedSize());
				GameCurrencyStates.Serialize(stream, instance.CurrencyState);
			}
			if (instance.HasBoosterModifications)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.BoosterModifications.GetSerializedSize());
				BoosterModifications.Serialize(stream, instance.BoosterModifications);
			}
			if (instance.HasHeroXp)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.HeroXp.GetSerializedSize());
				HeroXP.Serialize(stream, instance.HeroXp);
			}
			if (instance.HasPlayerRecords)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteUInt32(stream, instance.PlayerRecords.GetSerializedSize());
				PlayerRecords.Serialize(stream, instance.PlayerRecords);
			}
			if (instance.HasArenaSessionResponse)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.ArenaSessionResponse.GetSerializedSize());
				ArenaSessionResponse.Serialize(stream, instance.ArenaSessionResponse);
			}
			if (instance.HasCardBackModifications)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteUInt32(stream, instance.CardBackModifications.GetSerializedSize());
				CardBackModifications.Serialize(stream, instance.CardBackModifications);
			}
			if (instance.HasPlayerDraftTickets)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.PlayerDraftTickets.GetSerializedSize());
				PlayerDraftTickets.Serialize(stream, instance.PlayerDraftTickets);
			}
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x000362E8 File Offset: 0x000344E8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAchievementNotifications)
			{
				num += 1U;
				uint serializedSize = this.AchievementNotifications.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasNoticeNotifications)
			{
				num += 1U;
				uint serializedSize2 = this.NoticeNotifications.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasCollectionModifications)
			{
				num += 1U;
				uint serializedSize3 = this.CollectionModifications.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasCurrencyState)
			{
				num += 1U;
				uint serializedSize4 = this.CurrencyState.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasBoosterModifications)
			{
				num += 1U;
				uint serializedSize5 = this.BoosterModifications.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (this.HasHeroXp)
			{
				num += 1U;
				uint serializedSize6 = this.HeroXp.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			if (this.HasPlayerRecords)
			{
				num += 1U;
				uint serializedSize7 = this.PlayerRecords.GetSerializedSize();
				num += serializedSize7 + ProtocolParser.SizeOfUInt32(serializedSize7);
			}
			if (this.HasArenaSessionResponse)
			{
				num += 1U;
				uint serializedSize8 = this.ArenaSessionResponse.GetSerializedSize();
				num += serializedSize8 + ProtocolParser.SizeOfUInt32(serializedSize8);
			}
			if (this.HasCardBackModifications)
			{
				num += 1U;
				uint serializedSize9 = this.CardBackModifications.GetSerializedSize();
				num += serializedSize9 + ProtocolParser.SizeOfUInt32(serializedSize9);
			}
			if (this.HasPlayerDraftTickets)
			{
				num += 1U;
				uint serializedSize10 = this.PlayerDraftTickets.GetSerializedSize();
				num += serializedSize10 + ProtocolParser.SizeOfUInt32(serializedSize10);
			}
			return num;
		}

		// Token: 0x040004D6 RID: 1238
		public bool HasAchievementNotifications;

		// Token: 0x040004D7 RID: 1239
		private AchievementNotifications _AchievementNotifications;

		// Token: 0x040004D8 RID: 1240
		public bool HasNoticeNotifications;

		// Token: 0x040004D9 RID: 1241
		private NoticeNotifications _NoticeNotifications;

		// Token: 0x040004DA RID: 1242
		public bool HasCollectionModifications;

		// Token: 0x040004DB RID: 1243
		private CollectionModifications _CollectionModifications;

		// Token: 0x040004DC RID: 1244
		public bool HasCurrencyState;

		// Token: 0x040004DD RID: 1245
		private GameCurrencyStates _CurrencyState;

		// Token: 0x040004DE RID: 1246
		public bool HasBoosterModifications;

		// Token: 0x040004DF RID: 1247
		private BoosterModifications _BoosterModifications;

		// Token: 0x040004E0 RID: 1248
		public bool HasHeroXp;

		// Token: 0x040004E1 RID: 1249
		private HeroXP _HeroXp;

		// Token: 0x040004E2 RID: 1250
		public bool HasPlayerRecords;

		// Token: 0x040004E3 RID: 1251
		private PlayerRecords _PlayerRecords;

		// Token: 0x040004E4 RID: 1252
		public bool HasArenaSessionResponse;

		// Token: 0x040004E5 RID: 1253
		private ArenaSessionResponse _ArenaSessionResponse;

		// Token: 0x040004E6 RID: 1254
		public bool HasCardBackModifications;

		// Token: 0x040004E7 RID: 1255
		private CardBackModifications _CardBackModifications;

		// Token: 0x040004E8 RID: 1256
		public bool HasPlayerDraftTickets;

		// Token: 0x040004E9 RID: 1257
		private PlayerDraftTickets _PlayerDraftTickets;

		// Token: 0x020005DF RID: 1503
		public enum PacketID
		{
			// Token: 0x04001FE7 RID: 8167
			ID = 333,
			// Token: 0x04001FE8 RID: 8168
			System = 0
		}
	}
}
