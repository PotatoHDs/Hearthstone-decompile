using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000041 RID: 65
	public class ProfileNotice : IProtoBuf
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000F31B File Offset: 0x0000D51B
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000F323 File Offset: 0x0000D523
		public long Entry { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000F32C File Offset: 0x0000D52C
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x0000F334 File Offset: 0x0000D534
		public ProfileNoticeMedal Medal
		{
			get
			{
				return this._Medal;
			}
			set
			{
				this._Medal = value;
				this.HasMedal = (value != null);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000F347 File Offset: 0x0000D547
		// (set) Token: 0x060003BA RID: 954 RVA: 0x0000F34F File Offset: 0x0000D54F
		public ProfileNoticeRewardBooster RewardBooster
		{
			get
			{
				return this._RewardBooster;
			}
			set
			{
				this._RewardBooster = value;
				this.HasRewardBooster = (value != null);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000F362 File Offset: 0x0000D562
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0000F36A File Offset: 0x0000D56A
		public ProfileNoticeRewardCard RewardCard
		{
			get
			{
				return this._RewardCard;
			}
			set
			{
				this._RewardCard = value;
				this.HasRewardCard = (value != null);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000F37D File Offset: 0x0000D57D
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000F385 File Offset: 0x0000D585
		public ProfileNoticePreconDeck PreconDeck
		{
			get
			{
				return this._PreconDeck;
			}
			set
			{
				this._PreconDeck = value;
				this.HasPreconDeck = (value != null);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000F398 File Offset: 0x0000D598
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0000F3A0 File Offset: 0x0000D5A0
		public ProfileNoticeRewardDust RewardDust
		{
			get
			{
				return this._RewardDust;
			}
			set
			{
				this._RewardDust = value;
				this.HasRewardDust = (value != null);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000F3B3 File Offset: 0x0000D5B3
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x0000F3BB File Offset: 0x0000D5BB
		public ProfileNoticeRewardCurrency RewardCurrency
		{
			get
			{
				return this._RewardCurrency;
			}
			set
			{
				this._RewardCurrency = value;
				this.HasRewardCurrency = (value != null);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000F3CE File Offset: 0x0000D5CE
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000F3D6 File Offset: 0x0000D5D6
		public ProfileNoticeRewardMount RewardMount
		{
			get
			{
				return this._RewardMount;
			}
			set
			{
				this._RewardMount = value;
				this.HasRewardMount = (value != null);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000F3E9 File Offset: 0x0000D5E9
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x0000F3F1 File Offset: 0x0000D5F1
		public ProfileNoticeRewardForge RewardForge
		{
			get
			{
				return this._RewardForge;
			}
			set
			{
				this._RewardForge = value;
				this.HasRewardForge = (value != null);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000F404 File Offset: 0x0000D604
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x0000F40C File Offset: 0x0000D60C
		public int Origin { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000F415 File Offset: 0x0000D615
		// (set) Token: 0x060003CA RID: 970 RVA: 0x0000F41D File Offset: 0x0000D61D
		public long OriginData
		{
			get
			{
				return this._OriginData;
			}
			set
			{
				this._OriginData = value;
				this.HasOriginData = true;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000F42D File Offset: 0x0000D62D
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0000F435 File Offset: 0x0000D635
		public Date When { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000F43E File Offset: 0x0000D63E
		// (set) Token: 0x060003CE RID: 974 RVA: 0x0000F446 File Offset: 0x0000D646
		public ProfileNoticePurchase Purchase
		{
			get
			{
				return this._Purchase;
			}
			set
			{
				this._Purchase = value;
				this.HasPurchase = (value != null);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000F459 File Offset: 0x0000D659
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x0000F461 File Offset: 0x0000D661
		public ProfileNoticeCardBack RewardCardBack
		{
			get
			{
				return this._RewardCardBack;
			}
			set
			{
				this._RewardCardBack = value;
				this.HasRewardCardBack = (value != null);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000F474 File Offset: 0x0000D674
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x0000F47C File Offset: 0x0000D67C
		public ProfileNoticeDisconnectedGameResult DcGameResult
		{
			get
			{
				return this._DcGameResult;
			}
			set
			{
				this._DcGameResult = value;
				this.HasDcGameResult = (value != null);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000F48F File Offset: 0x0000D68F
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x0000F497 File Offset: 0x0000D697
		public ProfileNoticeBonusStars BonusStars
		{
			get
			{
				return this._BonusStars;
			}
			set
			{
				this._BonusStars = value;
				this.HasBonusStars = (value != null);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000F4AA File Offset: 0x0000D6AA
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x0000F4B2 File Offset: 0x0000D6B2
		public ProfileNoticeAdventureProgress AdventureProgress
		{
			get
			{
				return this._AdventureProgress;
			}
			set
			{
				this._AdventureProgress = value;
				this.HasAdventureProgress = (value != null);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000F4C5 File Offset: 0x0000D6C5
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x0000F4CD File Offset: 0x0000D6CD
		public ProfileNoticeLevelUp LevelUp
		{
			get
			{
				return this._LevelUp;
			}
			set
			{
				this._LevelUp = value;
				this.HasLevelUp = (value != null);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
		// (set) Token: 0x060003DA RID: 986 RVA: 0x0000F4E8 File Offset: 0x0000D6E8
		public ProfileNoticeAccountLicense AccountLicense
		{
			get
			{
				return this._AccountLicense;
			}
			set
			{
				this._AccountLicense = value;
				this.HasAccountLicense = (value != null);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000F4FB File Offset: 0x0000D6FB
		// (set) Token: 0x060003DC RID: 988 RVA: 0x0000F503 File Offset: 0x0000D703
		public ProfileNoticeTavernBrawlRewards TavernBrawlRewards
		{
			get
			{
				return this._TavernBrawlRewards;
			}
			set
			{
				this._TavernBrawlRewards = value;
				this.HasTavernBrawlRewards = (value != null);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000F516 File Offset: 0x0000D716
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0000F51E File Offset: 0x0000D71E
		public ProfileNoticeTavernBrawlTicket TavernBrawlTicket
		{
			get
			{
				return this._TavernBrawlTicket;
			}
			set
			{
				this._TavernBrawlTicket = value;
				this.HasTavernBrawlTicket = (value != null);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000F531 File Offset: 0x0000D731
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0000F539 File Offset: 0x0000D739
		public ProfileNoticeGenericRewardChest GenericRewardChest
		{
			get
			{
				return this._GenericRewardChest;
			}
			set
			{
				this._GenericRewardChest = value;
				this.HasGenericRewardChest = (value != null);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000F54C File Offset: 0x0000D74C
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000F554 File Offset: 0x0000D754
		public ProfileNoticeLeaguePromotionRewards LeaguePromotionRewards
		{
			get
			{
				return this._LeaguePromotionRewards;
			}
			set
			{
				this._LeaguePromotionRewards = value;
				this.HasLeaguePromotionRewards = (value != null);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000F567 File Offset: 0x0000D767
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000F56F File Offset: 0x0000D76F
		public ProfileNoticeDisconnectedGameResultNew DcGameResultNew
		{
			get
			{
				return this._DcGameResultNew;
			}
			set
			{
				this._DcGameResultNew = value;
				this.HasDcGameResultNew = (value != null);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000F582 File Offset: 0x0000D782
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000F58A File Offset: 0x0000D78A
		public ProfileNoticeFreeDeckChoice FreeDeckChoice
		{
			get
			{
				return this._FreeDeckChoice;
			}
			set
			{
				this._FreeDeckChoice = value;
				this.HasFreeDeckChoice = (value != null);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000F59D File Offset: 0x0000D79D
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000F5A5 File Offset: 0x0000D7A5
		public ProfileNoticeDeckRemoved DeckRemoved
		{
			get
			{
				return this._DeckRemoved;
			}
			set
			{
				this._DeckRemoved = value;
				this.HasDeckRemoved = (value != null);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000F5B8 File Offset: 0x0000D7B8
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000F5C0 File Offset: 0x0000D7C0
		public ProfileNoticeDeckGranted DeckGranted
		{
			get
			{
				return this._DeckGranted;
			}
			set
			{
				this._DeckGranted = value;
				this.HasDeckGranted = (value != null);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000F5D3 File Offset: 0x0000D7D3
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000F5DB File Offset: 0x0000D7DB
		public ProfileNoticeMiniSetGranted MiniSetGranted
		{
			get
			{
				return this._MiniSetGranted;
			}
			set
			{
				this._MiniSetGranted = value;
				this.HasMiniSetGranted = (value != null);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000F5EE File Offset: 0x0000D7EE
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000F5F6 File Offset: 0x0000D7F6
		public ProfileNoticeSellableDeckGranted SellableDeckGranted
		{
			get
			{
				return this._SellableDeckGranted;
			}
			set
			{
				this._SellableDeckGranted = value;
				this.HasSellableDeckGranted = (value != null);
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000F60C File Offset: 0x0000D80C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Entry.GetHashCode();
			if (this.HasMedal)
			{
				num ^= this.Medal.GetHashCode();
			}
			if (this.HasRewardBooster)
			{
				num ^= this.RewardBooster.GetHashCode();
			}
			if (this.HasRewardCard)
			{
				num ^= this.RewardCard.GetHashCode();
			}
			if (this.HasPreconDeck)
			{
				num ^= this.PreconDeck.GetHashCode();
			}
			if (this.HasRewardDust)
			{
				num ^= this.RewardDust.GetHashCode();
			}
			if (this.HasRewardCurrency)
			{
				num ^= this.RewardCurrency.GetHashCode();
			}
			if (this.HasRewardMount)
			{
				num ^= this.RewardMount.GetHashCode();
			}
			if (this.HasRewardForge)
			{
				num ^= this.RewardForge.GetHashCode();
			}
			num ^= this.Origin.GetHashCode();
			if (this.HasOriginData)
			{
				num ^= this.OriginData.GetHashCode();
			}
			num ^= this.When.GetHashCode();
			if (this.HasPurchase)
			{
				num ^= this.Purchase.GetHashCode();
			}
			if (this.HasRewardCardBack)
			{
				num ^= this.RewardCardBack.GetHashCode();
			}
			if (this.HasDcGameResult)
			{
				num ^= this.DcGameResult.GetHashCode();
			}
			if (this.HasBonusStars)
			{
				num ^= this.BonusStars.GetHashCode();
			}
			if (this.HasAdventureProgress)
			{
				num ^= this.AdventureProgress.GetHashCode();
			}
			if (this.HasLevelUp)
			{
				num ^= this.LevelUp.GetHashCode();
			}
			if (this.HasAccountLicense)
			{
				num ^= this.AccountLicense.GetHashCode();
			}
			if (this.HasTavernBrawlRewards)
			{
				num ^= this.TavernBrawlRewards.GetHashCode();
			}
			if (this.HasTavernBrawlTicket)
			{
				num ^= this.TavernBrawlTicket.GetHashCode();
			}
			if (this.HasGenericRewardChest)
			{
				num ^= this.GenericRewardChest.GetHashCode();
			}
			if (this.HasLeaguePromotionRewards)
			{
				num ^= this.LeaguePromotionRewards.GetHashCode();
			}
			if (this.HasDcGameResultNew)
			{
				num ^= this.DcGameResultNew.GetHashCode();
			}
			if (this.HasFreeDeckChoice)
			{
				num ^= this.FreeDeckChoice.GetHashCode();
			}
			if (this.HasDeckRemoved)
			{
				num ^= this.DeckRemoved.GetHashCode();
			}
			if (this.HasDeckGranted)
			{
				num ^= this.DeckGranted.GetHashCode();
			}
			if (this.HasMiniSetGranted)
			{
				num ^= this.MiniSetGranted.GetHashCode();
			}
			if (this.HasSellableDeckGranted)
			{
				num ^= this.SellableDeckGranted.GetHashCode();
			}
			return num;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000F898 File Offset: 0x0000DA98
		public override bool Equals(object obj)
		{
			ProfileNotice profileNotice = obj as ProfileNotice;
			return profileNotice != null && this.Entry.Equals(profileNotice.Entry) && this.HasMedal == profileNotice.HasMedal && (!this.HasMedal || this.Medal.Equals(profileNotice.Medal)) && this.HasRewardBooster == profileNotice.HasRewardBooster && (!this.HasRewardBooster || this.RewardBooster.Equals(profileNotice.RewardBooster)) && this.HasRewardCard == profileNotice.HasRewardCard && (!this.HasRewardCard || this.RewardCard.Equals(profileNotice.RewardCard)) && this.HasPreconDeck == profileNotice.HasPreconDeck && (!this.HasPreconDeck || this.PreconDeck.Equals(profileNotice.PreconDeck)) && this.HasRewardDust == profileNotice.HasRewardDust && (!this.HasRewardDust || this.RewardDust.Equals(profileNotice.RewardDust)) && this.HasRewardCurrency == profileNotice.HasRewardCurrency && (!this.HasRewardCurrency || this.RewardCurrency.Equals(profileNotice.RewardCurrency)) && this.HasRewardMount == profileNotice.HasRewardMount && (!this.HasRewardMount || this.RewardMount.Equals(profileNotice.RewardMount)) && this.HasRewardForge == profileNotice.HasRewardForge && (!this.HasRewardForge || this.RewardForge.Equals(profileNotice.RewardForge)) && this.Origin.Equals(profileNotice.Origin) && this.HasOriginData == profileNotice.HasOriginData && (!this.HasOriginData || this.OriginData.Equals(profileNotice.OriginData)) && this.When.Equals(profileNotice.When) && this.HasPurchase == profileNotice.HasPurchase && (!this.HasPurchase || this.Purchase.Equals(profileNotice.Purchase)) && this.HasRewardCardBack == profileNotice.HasRewardCardBack && (!this.HasRewardCardBack || this.RewardCardBack.Equals(profileNotice.RewardCardBack)) && this.HasDcGameResult == profileNotice.HasDcGameResult && (!this.HasDcGameResult || this.DcGameResult.Equals(profileNotice.DcGameResult)) && this.HasBonusStars == profileNotice.HasBonusStars && (!this.HasBonusStars || this.BonusStars.Equals(profileNotice.BonusStars)) && this.HasAdventureProgress == profileNotice.HasAdventureProgress && (!this.HasAdventureProgress || this.AdventureProgress.Equals(profileNotice.AdventureProgress)) && this.HasLevelUp == profileNotice.HasLevelUp && (!this.HasLevelUp || this.LevelUp.Equals(profileNotice.LevelUp)) && this.HasAccountLicense == profileNotice.HasAccountLicense && (!this.HasAccountLicense || this.AccountLicense.Equals(profileNotice.AccountLicense)) && this.HasTavernBrawlRewards == profileNotice.HasTavernBrawlRewards && (!this.HasTavernBrawlRewards || this.TavernBrawlRewards.Equals(profileNotice.TavernBrawlRewards)) && this.HasTavernBrawlTicket == profileNotice.HasTavernBrawlTicket && (!this.HasTavernBrawlTicket || this.TavernBrawlTicket.Equals(profileNotice.TavernBrawlTicket)) && this.HasGenericRewardChest == profileNotice.HasGenericRewardChest && (!this.HasGenericRewardChest || this.GenericRewardChest.Equals(profileNotice.GenericRewardChest)) && this.HasLeaguePromotionRewards == profileNotice.HasLeaguePromotionRewards && (!this.HasLeaguePromotionRewards || this.LeaguePromotionRewards.Equals(profileNotice.LeaguePromotionRewards)) && this.HasDcGameResultNew == profileNotice.HasDcGameResultNew && (!this.HasDcGameResultNew || this.DcGameResultNew.Equals(profileNotice.DcGameResultNew)) && this.HasFreeDeckChoice == profileNotice.HasFreeDeckChoice && (!this.HasFreeDeckChoice || this.FreeDeckChoice.Equals(profileNotice.FreeDeckChoice)) && this.HasDeckRemoved == profileNotice.HasDeckRemoved && (!this.HasDeckRemoved || this.DeckRemoved.Equals(profileNotice.DeckRemoved)) && this.HasDeckGranted == profileNotice.HasDeckGranted && (!this.HasDeckGranted || this.DeckGranted.Equals(profileNotice.DeckGranted)) && this.HasMiniSetGranted == profileNotice.HasMiniSetGranted && (!this.HasMiniSetGranted || this.MiniSetGranted.Equals(profileNotice.MiniSetGranted)) && this.HasSellableDeckGranted == profileNotice.HasSellableDeckGranted && (!this.HasSellableDeckGranted || this.SellableDeckGranted.Equals(profileNotice.SellableDeckGranted));
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000FD58 File Offset: 0x0000DF58
		public void Deserialize(Stream stream)
		{
			ProfileNotice.Deserialize(stream, this);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000FD62 File Offset: 0x0000DF62
		public static ProfileNotice Deserialize(Stream stream, ProfileNotice instance)
		{
			return ProfileNotice.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000FD70 File Offset: 0x0000DF70
		public static ProfileNotice DeserializeLengthDelimited(Stream stream)
		{
			ProfileNotice profileNotice = new ProfileNotice();
			ProfileNotice.DeserializeLengthDelimited(stream, profileNotice);
			return profileNotice;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000FD8C File Offset: 0x0000DF8C
		public static ProfileNotice DeserializeLengthDelimited(Stream stream, ProfileNotice instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNotice.Deserialize(stream, instance, num);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000FDB4 File Offset: 0x0000DFB4
		public static ProfileNotice Deserialize(Stream stream, ProfileNotice instance, long limit)
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
					if (num <= 66)
					{
						if (num <= 26)
						{
							if (num == 8)
							{
								instance.Entry = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num != 18)
							{
								if (num == 26)
								{
									if (instance.RewardBooster == null)
									{
										instance.RewardBooster = ProfileNoticeRewardBooster.DeserializeLengthDelimited(stream);
										continue;
									}
									ProfileNoticeRewardBooster.DeserializeLengthDelimited(stream, instance.RewardBooster);
									continue;
								}
							}
							else
							{
								if (instance.Medal == null)
								{
									instance.Medal = ProfileNoticeMedal.DeserializeLengthDelimited(stream);
									continue;
								}
								ProfileNoticeMedal.DeserializeLengthDelimited(stream, instance.Medal);
								continue;
							}
						}
						else if (num <= 50)
						{
							if (num != 34)
							{
								if (num == 50)
								{
									if (instance.PreconDeck == null)
									{
										instance.PreconDeck = ProfileNoticePreconDeck.DeserializeLengthDelimited(stream);
										continue;
									}
									ProfileNoticePreconDeck.DeserializeLengthDelimited(stream, instance.PreconDeck);
									continue;
								}
							}
							else
							{
								if (instance.RewardCard == null)
								{
									instance.RewardCard = ProfileNoticeRewardCard.DeserializeLengthDelimited(stream);
									continue;
								}
								ProfileNoticeRewardCard.DeserializeLengthDelimited(stream, instance.RewardCard);
								continue;
							}
						}
						else if (num != 58)
						{
							if (num == 66)
							{
								if (instance.RewardCurrency == null)
								{
									instance.RewardCurrency = ProfileNoticeRewardCurrency.DeserializeLengthDelimited(stream);
									continue;
								}
								ProfileNoticeRewardCurrency.DeserializeLengthDelimited(stream, instance.RewardCurrency);
								continue;
							}
						}
						else
						{
							if (instance.RewardDust == null)
							{
								instance.RewardDust = ProfileNoticeRewardDust.DeserializeLengthDelimited(stream);
								continue;
							}
							ProfileNoticeRewardDust.DeserializeLengthDelimited(stream, instance.RewardDust);
							continue;
						}
					}
					else if (num <= 88)
					{
						if (num != 74)
						{
							if (num != 82)
							{
								if (num == 88)
								{
									instance.Origin = (int)ProtocolParser.ReadUInt64(stream);
									continue;
								}
							}
							else
							{
								if (instance.RewardForge == null)
								{
									instance.RewardForge = ProfileNoticeRewardForge.DeserializeLengthDelimited(stream);
									continue;
								}
								ProfileNoticeRewardForge.DeserializeLengthDelimited(stream, instance.RewardForge);
								continue;
							}
						}
						else
						{
							if (instance.RewardMount == null)
							{
								instance.RewardMount = ProfileNoticeRewardMount.DeserializeLengthDelimited(stream);
								continue;
							}
							ProfileNoticeRewardMount.DeserializeLengthDelimited(stream, instance.RewardMount);
							continue;
						}
					}
					else if (num <= 106)
					{
						if (num == 96)
						{
							instance.OriginData = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 106)
						{
							if (instance.When == null)
							{
								instance.When = Date.DeserializeLengthDelimited(stream);
								continue;
							}
							Date.DeserializeLengthDelimited(stream, instance.When);
							continue;
						}
					}
					else if (num != 114)
					{
						if (num == 122)
						{
							if (instance.RewardCardBack == null)
							{
								instance.RewardCardBack = ProfileNoticeCardBack.DeserializeLengthDelimited(stream);
								continue;
							}
							ProfileNoticeCardBack.DeserializeLengthDelimited(stream, instance.RewardCardBack);
							continue;
						}
					}
					else
					{
						if (instance.Purchase == null)
						{
							instance.Purchase = ProfileNoticePurchase.DeserializeLengthDelimited(stream);
							continue;
						}
						ProfileNoticePurchase.DeserializeLengthDelimited(stream, instance.Purchase);
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
						if (key.WireType != Wire.LengthDelimited)
						{
							continue;
						}
						if (instance.DcGameResult == null)
						{
							instance.DcGameResult = ProfileNoticeDisconnectedGameResult.DeserializeLengthDelimited(stream);
							continue;
						}
						ProfileNoticeDisconnectedGameResult.DeserializeLengthDelimited(stream, instance.DcGameResult);
						continue;
					case 17U:
						if (key.WireType != Wire.LengthDelimited)
						{
							continue;
						}
						if (instance.BonusStars == null)
						{
							instance.BonusStars = ProfileNoticeBonusStars.DeserializeLengthDelimited(stream);
							continue;
						}
						ProfileNoticeBonusStars.DeserializeLengthDelimited(stream, instance.BonusStars);
						continue;
					case 18U:
						if (key.WireType != Wire.LengthDelimited)
						{
							continue;
						}
						if (instance.AdventureProgress == null)
						{
							instance.AdventureProgress = ProfileNoticeAdventureProgress.DeserializeLengthDelimited(stream);
							continue;
						}
						ProfileNoticeAdventureProgress.DeserializeLengthDelimited(stream, instance.AdventureProgress);
						continue;
					case 19U:
						if (key.WireType != Wire.LengthDelimited)
						{
							continue;
						}
						if (instance.LevelUp == null)
						{
							instance.LevelUp = ProfileNoticeLevelUp.DeserializeLengthDelimited(stream);
							continue;
						}
						ProfileNoticeLevelUp.DeserializeLengthDelimited(stream, instance.LevelUp);
						continue;
					case 20U:
						if (key.WireType != Wire.LengthDelimited)
						{
							continue;
						}
						if (instance.AccountLicense == null)
						{
							instance.AccountLicense = ProfileNoticeAccountLicense.DeserializeLengthDelimited(stream);
							continue;
						}
						ProfileNoticeAccountLicense.DeserializeLengthDelimited(stream, instance.AccountLicense);
						continue;
					case 21U:
						if (key.WireType != Wire.LengthDelimited)
						{
							continue;
						}
						if (instance.TavernBrawlRewards == null)
						{
							instance.TavernBrawlRewards = ProfileNoticeTavernBrawlRewards.DeserializeLengthDelimited(stream);
							continue;
						}
						ProfileNoticeTavernBrawlRewards.DeserializeLengthDelimited(stream, instance.TavernBrawlRewards);
						continue;
					case 22U:
						if (key.WireType != Wire.LengthDelimited)
						{
							continue;
						}
						if (instance.TavernBrawlTicket == null)
						{
							instance.TavernBrawlTicket = ProfileNoticeTavernBrawlTicket.DeserializeLengthDelimited(stream);
							continue;
						}
						ProfileNoticeTavernBrawlTicket.DeserializeLengthDelimited(stream, instance.TavernBrawlTicket);
						continue;
					case 23U:
						if (key.WireType != Wire.LengthDelimited)
						{
							continue;
						}
						if (instance.GenericRewardChest == null)
						{
							instance.GenericRewardChest = ProfileNoticeGenericRewardChest.DeserializeLengthDelimited(stream);
							continue;
						}
						ProfileNoticeGenericRewardChest.DeserializeLengthDelimited(stream, instance.GenericRewardChest);
						continue;
					case 24U:
						if (key.WireType != Wire.LengthDelimited)
						{
							continue;
						}
						if (instance.LeaguePromotionRewards == null)
						{
							instance.LeaguePromotionRewards = ProfileNoticeLeaguePromotionRewards.DeserializeLengthDelimited(stream);
							continue;
						}
						ProfileNoticeLeaguePromotionRewards.DeserializeLengthDelimited(stream, instance.LeaguePromotionRewards);
						continue;
					case 26U:
						if (key.WireType != Wire.LengthDelimited)
						{
							continue;
						}
						if (instance.DcGameResultNew == null)
						{
							instance.DcGameResultNew = ProfileNoticeDisconnectedGameResultNew.DeserializeLengthDelimited(stream);
							continue;
						}
						ProfileNoticeDisconnectedGameResultNew.DeserializeLengthDelimited(stream, instance.DcGameResultNew);
						continue;
					case 27U:
						if (key.WireType != Wire.LengthDelimited)
						{
							continue;
						}
						if (instance.FreeDeckChoice == null)
						{
							instance.FreeDeckChoice = ProfileNoticeFreeDeckChoice.DeserializeLengthDelimited(stream);
							continue;
						}
						ProfileNoticeFreeDeckChoice.DeserializeLengthDelimited(stream, instance.FreeDeckChoice);
						continue;
					case 28U:
						if (key.WireType != Wire.LengthDelimited)
						{
							continue;
						}
						if (instance.DeckRemoved == null)
						{
							instance.DeckRemoved = ProfileNoticeDeckRemoved.DeserializeLengthDelimited(stream);
							continue;
						}
						ProfileNoticeDeckRemoved.DeserializeLengthDelimited(stream, instance.DeckRemoved);
						continue;
					case 29U:
						if (key.WireType != Wire.LengthDelimited)
						{
							continue;
						}
						if (instance.DeckGranted == null)
						{
							instance.DeckGranted = ProfileNoticeDeckGranted.DeserializeLengthDelimited(stream);
							continue;
						}
						ProfileNoticeDeckGranted.DeserializeLengthDelimited(stream, instance.DeckGranted);
						continue;
					case 30U:
						if (key.WireType != Wire.LengthDelimited)
						{
							continue;
						}
						if (instance.MiniSetGranted == null)
						{
							instance.MiniSetGranted = ProfileNoticeMiniSetGranted.DeserializeLengthDelimited(stream);
							continue;
						}
						ProfileNoticeMiniSetGranted.DeserializeLengthDelimited(stream, instance.MiniSetGranted);
						continue;
					case 31U:
						if (key.WireType != Wire.LengthDelimited)
						{
							continue;
						}
						if (instance.SellableDeckGranted == null)
						{
							instance.SellableDeckGranted = ProfileNoticeSellableDeckGranted.DeserializeLengthDelimited(stream);
							continue;
						}
						ProfileNoticeSellableDeckGranted.DeserializeLengthDelimited(stream, instance.SellableDeckGranted);
						continue;
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

		// Token: 0x060003F6 RID: 1014 RVA: 0x00010464 File Offset: 0x0000E664
		public void Serialize(Stream stream)
		{
			ProfileNotice.Serialize(stream, this);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00010470 File Offset: 0x0000E670
		public static void Serialize(Stream stream, ProfileNotice instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Entry);
			if (instance.HasMedal)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Medal.GetSerializedSize());
				ProfileNoticeMedal.Serialize(stream, instance.Medal);
			}
			if (instance.HasRewardBooster)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.RewardBooster.GetSerializedSize());
				ProfileNoticeRewardBooster.Serialize(stream, instance.RewardBooster);
			}
			if (instance.HasRewardCard)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.RewardCard.GetSerializedSize());
				ProfileNoticeRewardCard.Serialize(stream, instance.RewardCard);
			}
			if (instance.HasPreconDeck)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.PreconDeck.GetSerializedSize());
				ProfileNoticePreconDeck.Serialize(stream, instance.PreconDeck);
			}
			if (instance.HasRewardDust)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteUInt32(stream, instance.RewardDust.GetSerializedSize());
				ProfileNoticeRewardDust.Serialize(stream, instance.RewardDust);
			}
			if (instance.HasRewardCurrency)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.RewardCurrency.GetSerializedSize());
				ProfileNoticeRewardCurrency.Serialize(stream, instance.RewardCurrency);
			}
			if (instance.HasRewardMount)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteUInt32(stream, instance.RewardMount.GetSerializedSize());
				ProfileNoticeRewardMount.Serialize(stream, instance.RewardMount);
			}
			if (instance.HasRewardForge)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.RewardForge.GetSerializedSize());
				ProfileNoticeRewardForge.Serialize(stream, instance.RewardForge);
			}
			stream.WriteByte(88);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Origin));
			if (instance.HasOriginData)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.OriginData);
			}
			if (instance.When == null)
			{
				throw new ArgumentNullException("When", "Required by proto specification.");
			}
			stream.WriteByte(106);
			ProtocolParser.WriteUInt32(stream, instance.When.GetSerializedSize());
			Date.Serialize(stream, instance.When);
			if (instance.HasPurchase)
			{
				stream.WriteByte(114);
				ProtocolParser.WriteUInt32(stream, instance.Purchase.GetSerializedSize());
				ProfileNoticePurchase.Serialize(stream, instance.Purchase);
			}
			if (instance.HasRewardCardBack)
			{
				stream.WriteByte(122);
				ProtocolParser.WriteUInt32(stream, instance.RewardCardBack.GetSerializedSize());
				ProfileNoticeCardBack.Serialize(stream, instance.RewardCardBack);
			}
			if (instance.HasDcGameResult)
			{
				stream.WriteByte(130);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.DcGameResult.GetSerializedSize());
				ProfileNoticeDisconnectedGameResult.Serialize(stream, instance.DcGameResult);
			}
			if (instance.HasBonusStars)
			{
				stream.WriteByte(138);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.BonusStars.GetSerializedSize());
				ProfileNoticeBonusStars.Serialize(stream, instance.BonusStars);
			}
			if (instance.HasAdventureProgress)
			{
				stream.WriteByte(146);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.AdventureProgress.GetSerializedSize());
				ProfileNoticeAdventureProgress.Serialize(stream, instance.AdventureProgress);
			}
			if (instance.HasLevelUp)
			{
				stream.WriteByte(154);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.LevelUp.GetSerializedSize());
				ProfileNoticeLevelUp.Serialize(stream, instance.LevelUp);
			}
			if (instance.HasAccountLicense)
			{
				stream.WriteByte(162);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.AccountLicense.GetSerializedSize());
				ProfileNoticeAccountLicense.Serialize(stream, instance.AccountLicense);
			}
			if (instance.HasTavernBrawlRewards)
			{
				stream.WriteByte(170);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.TavernBrawlRewards.GetSerializedSize());
				ProfileNoticeTavernBrawlRewards.Serialize(stream, instance.TavernBrawlRewards);
			}
			if (instance.HasTavernBrawlTicket)
			{
				stream.WriteByte(178);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.TavernBrawlTicket.GetSerializedSize());
				ProfileNoticeTavernBrawlTicket.Serialize(stream, instance.TavernBrawlTicket);
			}
			if (instance.HasGenericRewardChest)
			{
				stream.WriteByte(186);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.GenericRewardChest.GetSerializedSize());
				ProfileNoticeGenericRewardChest.Serialize(stream, instance.GenericRewardChest);
			}
			if (instance.HasLeaguePromotionRewards)
			{
				stream.WriteByte(194);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.LeaguePromotionRewards.GetSerializedSize());
				ProfileNoticeLeaguePromotionRewards.Serialize(stream, instance.LeaguePromotionRewards);
			}
			if (instance.HasDcGameResultNew)
			{
				stream.WriteByte(210);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.DcGameResultNew.GetSerializedSize());
				ProfileNoticeDisconnectedGameResultNew.Serialize(stream, instance.DcGameResultNew);
			}
			if (instance.HasFreeDeckChoice)
			{
				stream.WriteByte(218);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.FreeDeckChoice.GetSerializedSize());
				ProfileNoticeFreeDeckChoice.Serialize(stream, instance.FreeDeckChoice);
			}
			if (instance.HasDeckRemoved)
			{
				stream.WriteByte(226);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.DeckRemoved.GetSerializedSize());
				ProfileNoticeDeckRemoved.Serialize(stream, instance.DeckRemoved);
			}
			if (instance.HasDeckGranted)
			{
				stream.WriteByte(234);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.DeckGranted.GetSerializedSize());
				ProfileNoticeDeckGranted.Serialize(stream, instance.DeckGranted);
			}
			if (instance.HasMiniSetGranted)
			{
				stream.WriteByte(242);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.MiniSetGranted.GetSerializedSize());
				ProfileNoticeMiniSetGranted.Serialize(stream, instance.MiniSetGranted);
			}
			if (instance.HasSellableDeckGranted)
			{
				stream.WriteByte(250);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.SellableDeckGranted.GetSerializedSize());
				ProfileNoticeSellableDeckGranted.Serialize(stream, instance.SellableDeckGranted);
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000109FC File Offset: 0x0000EBFC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)this.Entry);
			if (this.HasMedal)
			{
				num += 1U;
				uint serializedSize = this.Medal.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasRewardBooster)
			{
				num += 1U;
				uint serializedSize2 = this.RewardBooster.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasRewardCard)
			{
				num += 1U;
				uint serializedSize3 = this.RewardCard.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasPreconDeck)
			{
				num += 1U;
				uint serializedSize4 = this.PreconDeck.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasRewardDust)
			{
				num += 1U;
				uint serializedSize5 = this.RewardDust.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (this.HasRewardCurrency)
			{
				num += 1U;
				uint serializedSize6 = this.RewardCurrency.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			if (this.HasRewardMount)
			{
				num += 1U;
				uint serializedSize7 = this.RewardMount.GetSerializedSize();
				num += serializedSize7 + ProtocolParser.SizeOfUInt32(serializedSize7);
			}
			if (this.HasRewardForge)
			{
				num += 1U;
				uint serializedSize8 = this.RewardForge.GetSerializedSize();
				num += serializedSize8 + ProtocolParser.SizeOfUInt32(serializedSize8);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Origin));
			if (this.HasOriginData)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.OriginData);
			}
			uint serializedSize9 = this.When.GetSerializedSize();
			num += serializedSize9 + ProtocolParser.SizeOfUInt32(serializedSize9);
			if (this.HasPurchase)
			{
				num += 1U;
				uint serializedSize10 = this.Purchase.GetSerializedSize();
				num += serializedSize10 + ProtocolParser.SizeOfUInt32(serializedSize10);
			}
			if (this.HasRewardCardBack)
			{
				num += 1U;
				uint serializedSize11 = this.RewardCardBack.GetSerializedSize();
				num += serializedSize11 + ProtocolParser.SizeOfUInt32(serializedSize11);
			}
			if (this.HasDcGameResult)
			{
				num += 2U;
				uint serializedSize12 = this.DcGameResult.GetSerializedSize();
				num += serializedSize12 + ProtocolParser.SizeOfUInt32(serializedSize12);
			}
			if (this.HasBonusStars)
			{
				num += 2U;
				uint serializedSize13 = this.BonusStars.GetSerializedSize();
				num += serializedSize13 + ProtocolParser.SizeOfUInt32(serializedSize13);
			}
			if (this.HasAdventureProgress)
			{
				num += 2U;
				uint serializedSize14 = this.AdventureProgress.GetSerializedSize();
				num += serializedSize14 + ProtocolParser.SizeOfUInt32(serializedSize14);
			}
			if (this.HasLevelUp)
			{
				num += 2U;
				uint serializedSize15 = this.LevelUp.GetSerializedSize();
				num += serializedSize15 + ProtocolParser.SizeOfUInt32(serializedSize15);
			}
			if (this.HasAccountLicense)
			{
				num += 2U;
				uint serializedSize16 = this.AccountLicense.GetSerializedSize();
				num += serializedSize16 + ProtocolParser.SizeOfUInt32(serializedSize16);
			}
			if (this.HasTavernBrawlRewards)
			{
				num += 2U;
				uint serializedSize17 = this.TavernBrawlRewards.GetSerializedSize();
				num += serializedSize17 + ProtocolParser.SizeOfUInt32(serializedSize17);
			}
			if (this.HasTavernBrawlTicket)
			{
				num += 2U;
				uint serializedSize18 = this.TavernBrawlTicket.GetSerializedSize();
				num += serializedSize18 + ProtocolParser.SizeOfUInt32(serializedSize18);
			}
			if (this.HasGenericRewardChest)
			{
				num += 2U;
				uint serializedSize19 = this.GenericRewardChest.GetSerializedSize();
				num += serializedSize19 + ProtocolParser.SizeOfUInt32(serializedSize19);
			}
			if (this.HasLeaguePromotionRewards)
			{
				num += 2U;
				uint serializedSize20 = this.LeaguePromotionRewards.GetSerializedSize();
				num += serializedSize20 + ProtocolParser.SizeOfUInt32(serializedSize20);
			}
			if (this.HasDcGameResultNew)
			{
				num += 2U;
				uint serializedSize21 = this.DcGameResultNew.GetSerializedSize();
				num += serializedSize21 + ProtocolParser.SizeOfUInt32(serializedSize21);
			}
			if (this.HasFreeDeckChoice)
			{
				num += 2U;
				uint serializedSize22 = this.FreeDeckChoice.GetSerializedSize();
				num += serializedSize22 + ProtocolParser.SizeOfUInt32(serializedSize22);
			}
			if (this.HasDeckRemoved)
			{
				num += 2U;
				uint serializedSize23 = this.DeckRemoved.GetSerializedSize();
				num += serializedSize23 + ProtocolParser.SizeOfUInt32(serializedSize23);
			}
			if (this.HasDeckGranted)
			{
				num += 2U;
				uint serializedSize24 = this.DeckGranted.GetSerializedSize();
				num += serializedSize24 + ProtocolParser.SizeOfUInt32(serializedSize24);
			}
			if (this.HasMiniSetGranted)
			{
				num += 2U;
				uint serializedSize25 = this.MiniSetGranted.GetSerializedSize();
				num += serializedSize25 + ProtocolParser.SizeOfUInt32(serializedSize25);
			}
			if (this.HasSellableDeckGranted)
			{
				num += 2U;
				uint serializedSize26 = this.SellableDeckGranted.GetSerializedSize();
				num += serializedSize26 + ProtocolParser.SizeOfUInt32(serializedSize26);
			}
			return num + 3U;
		}

		// Token: 0x04000142 RID: 322
		public bool HasMedal;

		// Token: 0x04000143 RID: 323
		private ProfileNoticeMedal _Medal;

		// Token: 0x04000144 RID: 324
		public bool HasRewardBooster;

		// Token: 0x04000145 RID: 325
		private ProfileNoticeRewardBooster _RewardBooster;

		// Token: 0x04000146 RID: 326
		public bool HasRewardCard;

		// Token: 0x04000147 RID: 327
		private ProfileNoticeRewardCard _RewardCard;

		// Token: 0x04000148 RID: 328
		public bool HasPreconDeck;

		// Token: 0x04000149 RID: 329
		private ProfileNoticePreconDeck _PreconDeck;

		// Token: 0x0400014A RID: 330
		public bool HasRewardDust;

		// Token: 0x0400014B RID: 331
		private ProfileNoticeRewardDust _RewardDust;

		// Token: 0x0400014C RID: 332
		public bool HasRewardCurrency;

		// Token: 0x0400014D RID: 333
		private ProfileNoticeRewardCurrency _RewardCurrency;

		// Token: 0x0400014E RID: 334
		public bool HasRewardMount;

		// Token: 0x0400014F RID: 335
		private ProfileNoticeRewardMount _RewardMount;

		// Token: 0x04000150 RID: 336
		public bool HasRewardForge;

		// Token: 0x04000151 RID: 337
		private ProfileNoticeRewardForge _RewardForge;

		// Token: 0x04000153 RID: 339
		public bool HasOriginData;

		// Token: 0x04000154 RID: 340
		private long _OriginData;

		// Token: 0x04000156 RID: 342
		public bool HasPurchase;

		// Token: 0x04000157 RID: 343
		private ProfileNoticePurchase _Purchase;

		// Token: 0x04000158 RID: 344
		public bool HasRewardCardBack;

		// Token: 0x04000159 RID: 345
		private ProfileNoticeCardBack _RewardCardBack;

		// Token: 0x0400015A RID: 346
		public bool HasDcGameResult;

		// Token: 0x0400015B RID: 347
		private ProfileNoticeDisconnectedGameResult _DcGameResult;

		// Token: 0x0400015C RID: 348
		public bool HasBonusStars;

		// Token: 0x0400015D RID: 349
		private ProfileNoticeBonusStars _BonusStars;

		// Token: 0x0400015E RID: 350
		public bool HasAdventureProgress;

		// Token: 0x0400015F RID: 351
		private ProfileNoticeAdventureProgress _AdventureProgress;

		// Token: 0x04000160 RID: 352
		public bool HasLevelUp;

		// Token: 0x04000161 RID: 353
		private ProfileNoticeLevelUp _LevelUp;

		// Token: 0x04000162 RID: 354
		public bool HasAccountLicense;

		// Token: 0x04000163 RID: 355
		private ProfileNoticeAccountLicense _AccountLicense;

		// Token: 0x04000164 RID: 356
		public bool HasTavernBrawlRewards;

		// Token: 0x04000165 RID: 357
		private ProfileNoticeTavernBrawlRewards _TavernBrawlRewards;

		// Token: 0x04000166 RID: 358
		public bool HasTavernBrawlTicket;

		// Token: 0x04000167 RID: 359
		private ProfileNoticeTavernBrawlTicket _TavernBrawlTicket;

		// Token: 0x04000168 RID: 360
		public bool HasGenericRewardChest;

		// Token: 0x04000169 RID: 361
		private ProfileNoticeGenericRewardChest _GenericRewardChest;

		// Token: 0x0400016A RID: 362
		public bool HasLeaguePromotionRewards;

		// Token: 0x0400016B RID: 363
		private ProfileNoticeLeaguePromotionRewards _LeaguePromotionRewards;

		// Token: 0x0400016C RID: 364
		public bool HasDcGameResultNew;

		// Token: 0x0400016D RID: 365
		private ProfileNoticeDisconnectedGameResultNew _DcGameResultNew;

		// Token: 0x0400016E RID: 366
		public bool HasFreeDeckChoice;

		// Token: 0x0400016F RID: 367
		private ProfileNoticeFreeDeckChoice _FreeDeckChoice;

		// Token: 0x04000170 RID: 368
		public bool HasDeckRemoved;

		// Token: 0x04000171 RID: 369
		private ProfileNoticeDeckRemoved _DeckRemoved;

		// Token: 0x04000172 RID: 370
		public bool HasDeckGranted;

		// Token: 0x04000173 RID: 371
		private ProfileNoticeDeckGranted _DeckGranted;

		// Token: 0x04000174 RID: 372
		public bool HasMiniSetGranted;

		// Token: 0x04000175 RID: 373
		private ProfileNoticeMiniSetGranted _MiniSetGranted;

		// Token: 0x04000176 RID: 374
		public bool HasSellableDeckGranted;

		// Token: 0x04000177 RID: 375
		private ProfileNoticeSellableDeckGranted _SellableDeckGranted;
	}
}
