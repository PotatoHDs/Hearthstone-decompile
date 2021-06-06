using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000087 RID: 135
	public class GuardianVars : IProtoBuf
	{
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x0001D3DF File Offset: 0x0001B5DF
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x0001D3E7 File Offset: 0x0001B5E7
		public bool Tourney
		{
			get
			{
				return this._Tourney;
			}
			set
			{
				this._Tourney = value;
				this.HasTourney = true;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x0001D3F7 File Offset: 0x0001B5F7
		// (set) Token: 0x0600084C RID: 2124 RVA: 0x0001D3FF File Offset: 0x0001B5FF
		public bool Practice
		{
			get
			{
				return this._Practice;
			}
			set
			{
				this._Practice = value;
				this.HasPractice = true;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x0001D40F File Offset: 0x0001B60F
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x0001D417 File Offset: 0x0001B617
		public bool Casual
		{
			get
			{
				return this._Casual;
			}
			set
			{
				this._Casual = value;
				this.HasCasual = true;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x0001D427 File Offset: 0x0001B627
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x0001D42F File Offset: 0x0001B62F
		public bool Forge
		{
			get
			{
				return this._Forge;
			}
			set
			{
				this._Forge = value;
				this.HasForge = true;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0001D43F File Offset: 0x0001B63F
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x0001D447 File Offset: 0x0001B647
		public bool Friendly
		{
			get
			{
				return this._Friendly;
			}
			set
			{
				this._Friendly = value;
				this.HasFriendly = true;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x0001D457 File Offset: 0x0001B657
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x0001D45F File Offset: 0x0001B65F
		public bool Manager
		{
			get
			{
				return this._Manager;
			}
			set
			{
				this._Manager = value;
				this.HasManager = true;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x0001D46F File Offset: 0x0001B66F
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x0001D477 File Offset: 0x0001B677
		public bool Crafting
		{
			get
			{
				return this._Crafting;
			}
			set
			{
				this._Crafting = value;
				this.HasCrafting = true;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x0001D487 File Offset: 0x0001B687
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x0001D48F File Offset: 0x0001B68F
		public bool Hunter
		{
			get
			{
				return this._Hunter;
			}
			set
			{
				this._Hunter = value;
				this.HasHunter = true;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x0001D49F File Offset: 0x0001B69F
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x0001D4A7 File Offset: 0x0001B6A7
		public bool Mage
		{
			get
			{
				return this._Mage;
			}
			set
			{
				this._Mage = value;
				this.HasMage = true;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0001D4B7 File Offset: 0x0001B6B7
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x0001D4BF File Offset: 0x0001B6BF
		public bool Paladin
		{
			get
			{
				return this._Paladin;
			}
			set
			{
				this._Paladin = value;
				this.HasPaladin = true;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0001D4CF File Offset: 0x0001B6CF
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x0001D4D7 File Offset: 0x0001B6D7
		public bool Priest
		{
			get
			{
				return this._Priest;
			}
			set
			{
				this._Priest = value;
				this.HasPriest = true;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x0001D4E7 File Offset: 0x0001B6E7
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x0001D4EF File Offset: 0x0001B6EF
		public bool Rogue
		{
			get
			{
				return this._Rogue;
			}
			set
			{
				this._Rogue = value;
				this.HasRogue = true;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x0001D4FF File Offset: 0x0001B6FF
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x0001D507 File Offset: 0x0001B707
		public bool Shaman
		{
			get
			{
				return this._Shaman;
			}
			set
			{
				this._Shaman = value;
				this.HasShaman = true;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0001D517 File Offset: 0x0001B717
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x0001D51F File Offset: 0x0001B71F
		public bool Warlock
		{
			get
			{
				return this._Warlock;
			}
			set
			{
				this._Warlock = value;
				this.HasWarlock = true;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x0001D52F File Offset: 0x0001B72F
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x0001D537 File Offset: 0x0001B737
		public bool Warrior
		{
			get
			{
				return this._Warrior;
			}
			set
			{
				this._Warrior = value;
				this.HasWarrior = true;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x0001D547 File Offset: 0x0001B747
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x0001D54F File Offset: 0x0001B74F
		public int ShowUserUI
		{
			get
			{
				return this._ShowUserUI;
			}
			set
			{
				this._ShowUserUI = value;
				this.HasShowUserUI = true;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0001D55F File Offset: 0x0001B75F
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x0001D567 File Offset: 0x0001B767
		public bool Store
		{
			get
			{
				return this._Store;
			}
			set
			{
				this._Store = value;
				this.HasStore = true;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0001D577 File Offset: 0x0001B777
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x0001D57F File Offset: 0x0001B77F
		public bool BattlePay
		{
			get
			{
				return this._BattlePay;
			}
			set
			{
				this._BattlePay = value;
				this.HasBattlePay = true;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x0001D58F File Offset: 0x0001B78F
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x0001D597 File Offset: 0x0001B797
		public bool BuyWithGold
		{
			get
			{
				return this._BuyWithGold;
			}
			set
			{
				this._BuyWithGold = value;
				this.HasBuyWithGold = true;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x0001D5A7 File Offset: 0x0001B7A7
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x0001D5AF File Offset: 0x0001B7AF
		public bool TavernBrawl
		{
			get
			{
				return this._TavernBrawl;
			}
			set
			{
				this._TavernBrawl = value;
				this.HasTavernBrawl = true;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x0001D5BF File Offset: 0x0001B7BF
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x0001D5C7 File Offset: 0x0001B7C7
		public int ClientOptionsUpdateIntervalSeconds
		{
			get
			{
				return this._ClientOptionsUpdateIntervalSeconds;
			}
			set
			{
				this._ClientOptionsUpdateIntervalSeconds = value;
				this.HasClientOptionsUpdateIntervalSeconds = true;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0001D5D7 File Offset: 0x0001B7D7
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x0001D5DF File Offset: 0x0001B7DF
		public bool CaisEnabledNonMobile
		{
			get
			{
				return this._CaisEnabledNonMobile;
			}
			set
			{
				this._CaisEnabledNonMobile = value;
				this.HasCaisEnabledNonMobile = true;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0001D5EF File Offset: 0x0001B7EF
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x0001D5F7 File Offset: 0x0001B7F7
		public bool CaisEnabledMobileChina
		{
			get
			{
				return this._CaisEnabledMobileChina;
			}
			set
			{
				this._CaisEnabledMobileChina = value;
				this.HasCaisEnabledMobileChina = true;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x0001D607 File Offset: 0x0001B807
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x0001D60F File Offset: 0x0001B80F
		public bool CaisEnabledMobileSouthKorea
		{
			get
			{
				return this._CaisEnabledMobileSouthKorea;
			}
			set
			{
				this._CaisEnabledMobileSouthKorea = value;
				this.HasCaisEnabledMobileSouthKorea = true;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0001D61F File Offset: 0x0001B81F
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x0001D627 File Offset: 0x0001B827
		public bool SendTelemetryPresence
		{
			get
			{
				return this._SendTelemetryPresence;
			}
			set
			{
				this._SendTelemetryPresence = value;
				this.HasSendTelemetryPresence = true;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x0001D637 File Offset: 0x0001B837
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x0001D63F File Offset: 0x0001B83F
		public int FriendWeekConcederMaxDefense
		{
			get
			{
				return this._FriendWeekConcederMaxDefense;
			}
			set
			{
				this._FriendWeekConcederMaxDefense = value;
				this.HasFriendWeekConcederMaxDefense = true;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x0001D64F File Offset: 0x0001B84F
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x0001D657 File Offset: 0x0001B857
		public int WinsPerGold
		{
			get
			{
				return this._WinsPerGold;
			}
			set
			{
				this._WinsPerGold = value;
				this.HasWinsPerGold = true;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x0001D667 File Offset: 0x0001B867
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x0001D66F File Offset: 0x0001B86F
		public int GoldPerReward
		{
			get
			{
				return this._GoldPerReward;
			}
			set
			{
				this._GoldPerReward = value;
				this.HasGoldPerReward = true;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0001D67F File Offset: 0x0001B87F
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x0001D687 File Offset: 0x0001B887
		public int MaxGoldPerDay
		{
			get
			{
				return this._MaxGoldPerDay;
			}
			set
			{
				this._MaxGoldPerDay = value;
				this.HasMaxGoldPerDay = true;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0001D697 File Offset: 0x0001B897
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x0001D69F File Offset: 0x0001B89F
		public int XpSoloLimit
		{
			get
			{
				return this._XpSoloLimit;
			}
			set
			{
				this._XpSoloLimit = value;
				this.HasXpSoloLimit = true;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0001D6AF File Offset: 0x0001B8AF
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x0001D6B7 File Offset: 0x0001B8B7
		public int MaxHeroLevel
		{
			get
			{
				return this._MaxHeroLevel;
			}
			set
			{
				this._MaxHeroLevel = value;
				this.HasMaxHeroLevel = true;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x0001D6C7 File Offset: 0x0001B8C7
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x0001D6CF File Offset: 0x0001B8CF
		public float EventTimingMod
		{
			get
			{
				return this._EventTimingMod;
			}
			set
			{
				this._EventTimingMod = value;
				this.HasEventTimingMod = true;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0001D6DF File Offset: 0x0001B8DF
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x0001D6E7 File Offset: 0x0001B8E7
		public bool FsgEnabled
		{
			get
			{
				return this._FsgEnabled;
			}
			set
			{
				this._FsgEnabled = value;
				this.HasFsgEnabled = true;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0001D6F7 File Offset: 0x0001B8F7
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x0001D6FF File Offset: 0x0001B8FF
		public bool FsgAutoCheckinEnabled
		{
			get
			{
				return this._FsgAutoCheckinEnabled;
			}
			set
			{
				this._FsgAutoCheckinEnabled = value;
				this.HasFsgAutoCheckinEnabled = true;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x0001D70F File Offset: 0x0001B90F
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x0001D717 File Offset: 0x0001B917
		public int FriendWeekConcededGameMinTotalTurns
		{
			get
			{
				return this._FriendWeekConcededGameMinTotalTurns;
			}
			set
			{
				this._FriendWeekConcededGameMinTotalTurns = value;
				this.HasFriendWeekConcededGameMinTotalTurns = true;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0001D727 File Offset: 0x0001B927
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x0001D72F File Offset: 0x0001B92F
		public bool FriendWeekAllowsTavernBrawlRecordUpdate
		{
			get
			{
				return this._FriendWeekAllowsTavernBrawlRecordUpdate;
			}
			set
			{
				this._FriendWeekAllowsTavernBrawlRecordUpdate = value;
				this.HasFriendWeekAllowsTavernBrawlRecordUpdate = true;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x0001D73F File Offset: 0x0001B93F
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x0001D747 File Offset: 0x0001B947
		public bool FsgShowBetaLabel
		{
			get
			{
				return this._FsgShowBetaLabel;
			}
			set
			{
				this._FsgShowBetaLabel = value;
				this.HasFsgShowBetaLabel = true;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x0001D757 File Offset: 0x0001B957
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x0001D75F File Offset: 0x0001B95F
		public int FsgFriendListPatronCountLimit
		{
			get
			{
				return this._FsgFriendListPatronCountLimit;
			}
			set
			{
				this._FsgFriendListPatronCountLimit = value;
				this.HasFsgFriendListPatronCountLimit = true;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x0001D76F File Offset: 0x0001B96F
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x0001D777 File Offset: 0x0001B977
		public uint ArenaClosedToNewSessionsSeconds
		{
			get
			{
				return this._ArenaClosedToNewSessionsSeconds;
			}
			set
			{
				this._ArenaClosedToNewSessionsSeconds = value;
				this.HasArenaClosedToNewSessionsSeconds = true;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x0001D787 File Offset: 0x0001B987
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x0001D78F File Offset: 0x0001B98F
		public bool FsgLoginScanEnabled
		{
			get
			{
				return this._FsgLoginScanEnabled;
			}
			set
			{
				this._FsgLoginScanEnabled = value;
				this.HasFsgLoginScanEnabled = true;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0001D79F File Offset: 0x0001B99F
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x0001D7A7 File Offset: 0x0001B9A7
		public int FsgMaxPresencePubscribedPatronCount
		{
			get
			{
				return this._FsgMaxPresencePubscribedPatronCount;
			}
			set
			{
				this._FsgMaxPresencePubscribedPatronCount = value;
				this.HasFsgMaxPresencePubscribedPatronCount = true;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0001D7B7 File Offset: 0x0001B9B7
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x0001D7BF File Offset: 0x0001B9BF
		public bool QuickPackOpeningAllowed
		{
			get
			{
				return this._QuickPackOpeningAllowed;
			}
			set
			{
				this._QuickPackOpeningAllowed = value;
				this.HasQuickPackOpeningAllowed = true;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0001D7CF File Offset: 0x0001B9CF
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x0001D7D7 File Offset: 0x0001B9D7
		public bool AllowIosHighres
		{
			get
			{
				return this._AllowIosHighres;
			}
			set
			{
				this._AllowIosHighres = value;
				this.HasAllowIosHighres = true;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0001D7E7 File Offset: 0x0001B9E7
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x0001D7EF File Offset: 0x0001B9EF
		public bool SimpleCheckout
		{
			get
			{
				return this._SimpleCheckout;
			}
			set
			{
				this._SimpleCheckout = value;
				this.HasSimpleCheckout = true;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0001D7FF File Offset: 0x0001B9FF
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x0001D807 File Offset: 0x0001BA07
		public bool DeckCompletionGetPlayerCollectionFromClient
		{
			get
			{
				return this._DeckCompletionGetPlayerCollectionFromClient;
			}
			set
			{
				this._DeckCompletionGetPlayerCollectionFromClient = value;
				this.HasDeckCompletionGetPlayerCollectionFromClient = true;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x0001D817 File Offset: 0x0001BA17
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x0001D81F File Offset: 0x0001BA1F
		public bool SoftAccountPurchasing
		{
			get
			{
				return this._SoftAccountPurchasing;
			}
			set
			{
				this._SoftAccountPurchasing = value;
				this.HasSoftAccountPurchasing = true;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0001D82F File Offset: 0x0001BA2F
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x0001D837 File Offset: 0x0001BA37
		public bool EnableSmartDeckCompletion
		{
			get
			{
				return this._EnableSmartDeckCompletion;
			}
			set
			{
				this._EnableSmartDeckCompletion = value;
				this.HasEnableSmartDeckCompletion = true;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0001D847 File Offset: 0x0001BA47
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x0001D84F File Offset: 0x0001BA4F
		public int NumClassicPacksUntilDeprioritize
		{
			get
			{
				return this._NumClassicPacksUntilDeprioritize;
			}
			set
			{
				this._NumClassicPacksUntilDeprioritize = value;
				this.HasNumClassicPacksUntilDeprioritize = true;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x0001D85F File Offset: 0x0001BA5F
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x0001D867 File Offset: 0x0001BA67
		public bool AllowOfflineClientActivityIos
		{
			get
			{
				return this._AllowOfflineClientActivityIos;
			}
			set
			{
				this._AllowOfflineClientActivityIos = value;
				this.HasAllowOfflineClientActivityIos = true;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0001D877 File Offset: 0x0001BA77
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x0001D87F File Offset: 0x0001BA7F
		public bool AllowOfflineClientActivityAndroid
		{
			get
			{
				return this._AllowOfflineClientActivityAndroid;
			}
			set
			{
				this._AllowOfflineClientActivityAndroid = value;
				this.HasAllowOfflineClientActivityAndroid = true;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0001D88F File Offset: 0x0001BA8F
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x0001D897 File Offset: 0x0001BA97
		public bool AllowOfflineClientActivityDesktop
		{
			get
			{
				return this._AllowOfflineClientActivityDesktop;
			}
			set
			{
				this._AllowOfflineClientActivityDesktop = value;
				this.HasAllowOfflineClientActivityDesktop = true;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0001D8A7 File Offset: 0x0001BAA7
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x0001D8AF File Offset: 0x0001BAAF
		public bool AllowOfflineClientDeckDeletion
		{
			get
			{
				return this._AllowOfflineClientDeckDeletion;
			}
			set
			{
				this._AllowOfflineClientDeckDeletion = value;
				this.HasAllowOfflineClientDeckDeletion = true;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x0001D8BF File Offset: 0x0001BABF
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x0001D8C7 File Offset: 0x0001BAC7
		public bool Battlegrounds
		{
			get
			{
				return this._Battlegrounds;
			}
			set
			{
				this._Battlegrounds = value;
				this.HasBattlegrounds = true;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x0001D8D7 File Offset: 0x0001BAD7
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x0001D8DF File Offset: 0x0001BADF
		public bool BattlegroundsFriendlyChallenge
		{
			get
			{
				return this._BattlegroundsFriendlyChallenge;
			}
			set
			{
				this._BattlegroundsFriendlyChallenge = value;
				this.HasBattlegroundsFriendlyChallenge = true;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0001D8EF File Offset: 0x0001BAEF
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x0001D8F7 File Offset: 0x0001BAF7
		public bool SimpleCheckoutIos
		{
			get
			{
				return this._SimpleCheckoutIos;
			}
			set
			{
				this._SimpleCheckoutIos = value;
				this.HasSimpleCheckoutIos = true;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0001D907 File Offset: 0x0001BB07
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x0001D90F File Offset: 0x0001BB0F
		public bool SimpleCheckoutAndroidAmazon
		{
			get
			{
				return this._SimpleCheckoutAndroidAmazon;
			}
			set
			{
				this._SimpleCheckoutAndroidAmazon = value;
				this.HasSimpleCheckoutAndroidAmazon = true;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0001D91F File Offset: 0x0001BB1F
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x0001D927 File Offset: 0x0001BB27
		public bool SimpleCheckoutAndroidGoogle
		{
			get
			{
				return this._SimpleCheckoutAndroidGoogle;
			}
			set
			{
				this._SimpleCheckoutAndroidGoogle = value;
				this.HasSimpleCheckoutAndroidGoogle = true;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x0001D937 File Offset: 0x0001BB37
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x0001D93F File Offset: 0x0001BB3F
		public bool SimpleCheckoutAndroidGlobal
		{
			get
			{
				return this._SimpleCheckoutAndroidGlobal;
			}
			set
			{
				this._SimpleCheckoutAndroidGlobal = value;
				this.HasSimpleCheckoutAndroidGlobal = true;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0001D94F File Offset: 0x0001BB4F
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x0001D957 File Offset: 0x0001BB57
		public bool SimpleCheckoutWin
		{
			get
			{
				return this._SimpleCheckoutWin;
			}
			set
			{
				this._SimpleCheckoutWin = value;
				this.HasSimpleCheckoutWin = true;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x0001D967 File Offset: 0x0001BB67
		// (set) Token: 0x060008C0 RID: 2240 RVA: 0x0001D96F File Offset: 0x0001BB6F
		public bool SimpleCheckoutMac
		{
			get
			{
				return this._SimpleCheckoutMac;
			}
			set
			{
				this._SimpleCheckoutMac = value;
				this.HasSimpleCheckoutMac = true;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0001D97F File Offset: 0x0001BB7F
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x0001D987 File Offset: 0x0001BB87
		public int BattlegroundsEarlyAccessLicense
		{
			get
			{
				return this._BattlegroundsEarlyAccessLicense;
			}
			set
			{
				this._BattlegroundsEarlyAccessLicense = value;
				this.HasBattlegroundsEarlyAccessLicense = true;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0001D997 File Offset: 0x0001BB97
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x0001D99F File Offset: 0x0001BB9F
		public bool VirtualCurrencyEnabled
		{
			get
			{
				return this._VirtualCurrencyEnabled;
			}
			set
			{
				this._VirtualCurrencyEnabled = value;
				this.HasVirtualCurrencyEnabled = true;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0001D9AF File Offset: 0x0001BBAF
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x0001D9B7 File Offset: 0x0001BBB7
		public bool BattlegroundsTutorial
		{
			get
			{
				return this._BattlegroundsTutorial;
			}
			set
			{
				this._BattlegroundsTutorial = value;
				this.HasBattlegroundsTutorial = true;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x0001D9C7 File Offset: 0x0001BBC7
		// (set) Token: 0x060008C8 RID: 2248 RVA: 0x0001D9CF File Offset: 0x0001BBCF
		public bool VintageStoreEnabled
		{
			get
			{
				return this._VintageStoreEnabled;
			}
			set
			{
				this._VintageStoreEnabled = value;
				this.HasVintageStoreEnabled = true;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x0001D9DF File Offset: 0x0001BBDF
		// (set) Token: 0x060008CA RID: 2250 RVA: 0x0001D9E7 File Offset: 0x0001BBE7
		public int BoosterRotatingSoonWarnDaysWithoutSale
		{
			get
			{
				return this._BoosterRotatingSoonWarnDaysWithoutSale;
			}
			set
			{
				this._BoosterRotatingSoonWarnDaysWithoutSale = value;
				this.HasBoosterRotatingSoonWarnDaysWithoutSale = true;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x0001D9F7 File Offset: 0x0001BBF7
		// (set) Token: 0x060008CC RID: 2252 RVA: 0x0001D9FF File Offset: 0x0001BBFF
		public int BoosterRotatingSoonWarnDaysWithSale
		{
			get
			{
				return this._BoosterRotatingSoonWarnDaysWithSale;
			}
			set
			{
				this._BoosterRotatingSoonWarnDaysWithSale = value;
				this.HasBoosterRotatingSoonWarnDaysWithSale = true;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0001DA0F File Offset: 0x0001BC0F
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x0001DA17 File Offset: 0x0001BC17
		public int BattlegroundsMaxRankedPartySize
		{
			get
			{
				return this._BattlegroundsMaxRankedPartySize;
			}
			set
			{
				this._BattlegroundsMaxRankedPartySize = value;
				this.HasBattlegroundsMaxRankedPartySize = true;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0001DA27 File Offset: 0x0001BC27
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x0001DA2F File Offset: 0x0001BC2F
		public bool DeckReordering
		{
			get
			{
				return this._DeckReordering;
			}
			set
			{
				this._DeckReordering = value;
				this.HasDeckReordering = true;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x0001DA3F File Offset: 0x0001BC3F
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x0001DA47 File Offset: 0x0001BC47
		public bool ProgressionEnabled
		{
			get
			{
				return this._ProgressionEnabled;
			}
			set
			{
				this._ProgressionEnabled = value;
				this.HasProgressionEnabled = true;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x0001DA57 File Offset: 0x0001BC57
		// (set) Token: 0x060008D4 RID: 2260 RVA: 0x0001DA5F File Offset: 0x0001BC5F
		public uint PvpdrClosedToNewSessionsSeconds
		{
			get
			{
				return this._PvpdrClosedToNewSessionsSeconds;
			}
			set
			{
				this._PvpdrClosedToNewSessionsSeconds = value;
				this.HasPvpdrClosedToNewSessionsSeconds = true;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0001DA6F File Offset: 0x0001BC6F
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x0001DA77 File Offset: 0x0001BC77
		public bool Duels
		{
			get
			{
				return this._Duels;
			}
			set
			{
				this._Duels = value;
				this.HasDuels = true;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0001DA87 File Offset: 0x0001BC87
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x0001DA8F File Offset: 0x0001BC8F
		public uint DuelsEarlyAccessLicense
		{
			get
			{
				return this._DuelsEarlyAccessLicense;
			}
			set
			{
				this._DuelsEarlyAccessLicense = value;
				this.HasDuelsEarlyAccessLicense = true;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0001DA9F File Offset: 0x0001BC9F
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x0001DAA7 File Offset: 0x0001BCA7
		public bool PaidDuels
		{
			get
			{
				return this._PaidDuels;
			}
			set
			{
				this._PaidDuels = value;
				this.HasPaidDuels = true;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x0001DAB7 File Offset: 0x0001BCB7
		// (set) Token: 0x060008DC RID: 2268 RVA: 0x0001DABF File Offset: 0x0001BCBF
		public bool AllowLiveFpsGathering
		{
			get
			{
				return this._AllowLiveFpsGathering;
			}
			set
			{
				this._AllowLiveFpsGathering = value;
				this.HasAllowLiveFpsGathering = true;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x0001DACF File Offset: 0x0001BCCF
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x0001DAD7 File Offset: 0x0001BCD7
		public bool JournalButtonDisabled
		{
			get
			{
				return this._JournalButtonDisabled;
			}
			set
			{
				this._JournalButtonDisabled = value;
				this.HasJournalButtonDisabled = true;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x0001DAE7 File Offset: 0x0001BCE7
		// (set) Token: 0x060008E0 RID: 2272 RVA: 0x0001DAEF File Offset: 0x0001BCEF
		public bool AchievementToastDisabled
		{
			get
			{
				return this._AchievementToastDisabled;
			}
			set
			{
				this._AchievementToastDisabled = value;
				this.HasAchievementToastDisabled = true;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0001DAFF File Offset: 0x0001BCFF
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x0001DB07 File Offset: 0x0001BD07
		public float CheckForNewQuestsIntervalJitterSecs
		{
			get
			{
				return this._CheckForNewQuestsIntervalJitterSecs;
			}
			set
			{
				this._CheckForNewQuestsIntervalJitterSecs = value;
				this.HasCheckForNewQuestsIntervalJitterSecs = true;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0001DB17 File Offset: 0x0001BD17
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x0001DB1F File Offset: 0x0001BD1F
		public bool RankedStandard
		{
			get
			{
				return this._RankedStandard;
			}
			set
			{
				this._RankedStandard = value;
				this.HasRankedStandard = true;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0001DB2F File Offset: 0x0001BD2F
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x0001DB37 File Offset: 0x0001BD37
		public bool RankedWild
		{
			get
			{
				return this._RankedWild;
			}
			set
			{
				this._RankedWild = value;
				this.HasRankedWild = true;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0001DB47 File Offset: 0x0001BD47
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x0001DB4F File Offset: 0x0001BD4F
		public bool RankedClassic
		{
			get
			{
				return this._RankedClassic;
			}
			set
			{
				this._RankedClassic = value;
				this.HasRankedClassic = true;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0001DB5F File Offset: 0x0001BD5F
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x0001DB67 File Offset: 0x0001BD67
		public bool RankedNewPlayer
		{
			get
			{
				return this._RankedNewPlayer;
			}
			set
			{
				this._RankedNewPlayer = value;
				this.HasRankedNewPlayer = true;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0001DB77 File Offset: 0x0001BD77
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x0001DB7F File Offset: 0x0001BD7F
		public bool ContentstackEnabled
		{
			get
			{
				return this._ContentstackEnabled;
			}
			set
			{
				this._ContentstackEnabled = value;
				this.HasContentstackEnabled = true;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0001DB8F File Offset: 0x0001BD8F
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x0001DB97 File Offset: 0x0001BD97
		public float EndOfTurnToastPauseBufferSecs
		{
			get
			{
				return this._EndOfTurnToastPauseBufferSecs;
			}
			set
			{
				this._EndOfTurnToastPauseBufferSecs = value;
				this.HasEndOfTurnToastPauseBufferSecs = true;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0001DBA7 File Offset: 0x0001BDA7
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x0001DBAF File Offset: 0x0001BDAF
		public bool AppRatingEnabled
		{
			get
			{
				return this._AppRatingEnabled;
			}
			set
			{
				this._AppRatingEnabled = value;
				this.HasAppRatingEnabled = true;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0001DBBF File Offset: 0x0001BDBF
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x0001DBC7 File Offset: 0x0001BDC7
		public float AppRatingSamplingPercentage
		{
			get
			{
				return this._AppRatingSamplingPercentage;
			}
			set
			{
				this._AppRatingSamplingPercentage = value;
				this.HasAppRatingSamplingPercentage = true;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0001DBD7 File Offset: 0x0001BDD7
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x0001DBDF File Offset: 0x0001BDDF
		public bool BuyCardBacksFromCollectionManagerEnabled
		{
			get
			{
				return this._BuyCardBacksFromCollectionManagerEnabled;
			}
			set
			{
				this._BuyCardBacksFromCollectionManagerEnabled = value;
				this.HasBuyCardBacksFromCollectionManagerEnabled = true;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0001DBEF File Offset: 0x0001BDEF
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x0001DBF7 File Offset: 0x0001BDF7
		public bool BuyHeroSkinsFromCollectionManagerEnabled
		{
			get
			{
				return this._BuyHeroSkinsFromCollectionManagerEnabled;
			}
			set
			{
				this._BuyHeroSkinsFromCollectionManagerEnabled = value;
				this.HasBuyHeroSkinsFromCollectionManagerEnabled = true;
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001DC08 File Offset: 0x0001BE08
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTourney)
			{
				num ^= this.Tourney.GetHashCode();
			}
			if (this.HasPractice)
			{
				num ^= this.Practice.GetHashCode();
			}
			if (this.HasCasual)
			{
				num ^= this.Casual.GetHashCode();
			}
			if (this.HasForge)
			{
				num ^= this.Forge.GetHashCode();
			}
			if (this.HasFriendly)
			{
				num ^= this.Friendly.GetHashCode();
			}
			if (this.HasManager)
			{
				num ^= this.Manager.GetHashCode();
			}
			if (this.HasCrafting)
			{
				num ^= this.Crafting.GetHashCode();
			}
			if (this.HasHunter)
			{
				num ^= this.Hunter.GetHashCode();
			}
			if (this.HasMage)
			{
				num ^= this.Mage.GetHashCode();
			}
			if (this.HasPaladin)
			{
				num ^= this.Paladin.GetHashCode();
			}
			if (this.HasPriest)
			{
				num ^= this.Priest.GetHashCode();
			}
			if (this.HasRogue)
			{
				num ^= this.Rogue.GetHashCode();
			}
			if (this.HasShaman)
			{
				num ^= this.Shaman.GetHashCode();
			}
			if (this.HasWarlock)
			{
				num ^= this.Warlock.GetHashCode();
			}
			if (this.HasWarrior)
			{
				num ^= this.Warrior.GetHashCode();
			}
			if (this.HasShowUserUI)
			{
				num ^= this.ShowUserUI.GetHashCode();
			}
			if (this.HasStore)
			{
				num ^= this.Store.GetHashCode();
			}
			if (this.HasBattlePay)
			{
				num ^= this.BattlePay.GetHashCode();
			}
			if (this.HasBuyWithGold)
			{
				num ^= this.BuyWithGold.GetHashCode();
			}
			if (this.HasTavernBrawl)
			{
				num ^= this.TavernBrawl.GetHashCode();
			}
			if (this.HasClientOptionsUpdateIntervalSeconds)
			{
				num ^= this.ClientOptionsUpdateIntervalSeconds.GetHashCode();
			}
			if (this.HasCaisEnabledNonMobile)
			{
				num ^= this.CaisEnabledNonMobile.GetHashCode();
			}
			if (this.HasCaisEnabledMobileChina)
			{
				num ^= this.CaisEnabledMobileChina.GetHashCode();
			}
			if (this.HasCaisEnabledMobileSouthKorea)
			{
				num ^= this.CaisEnabledMobileSouthKorea.GetHashCode();
			}
			if (this.HasSendTelemetryPresence)
			{
				num ^= this.SendTelemetryPresence.GetHashCode();
			}
			if (this.HasFriendWeekConcederMaxDefense)
			{
				num ^= this.FriendWeekConcederMaxDefense.GetHashCode();
			}
			if (this.HasWinsPerGold)
			{
				num ^= this.WinsPerGold.GetHashCode();
			}
			if (this.HasGoldPerReward)
			{
				num ^= this.GoldPerReward.GetHashCode();
			}
			if (this.HasMaxGoldPerDay)
			{
				num ^= this.MaxGoldPerDay.GetHashCode();
			}
			if (this.HasXpSoloLimit)
			{
				num ^= this.XpSoloLimit.GetHashCode();
			}
			if (this.HasMaxHeroLevel)
			{
				num ^= this.MaxHeroLevel.GetHashCode();
			}
			if (this.HasEventTimingMod)
			{
				num ^= this.EventTimingMod.GetHashCode();
			}
			if (this.HasFsgEnabled)
			{
				num ^= this.FsgEnabled.GetHashCode();
			}
			if (this.HasFsgAutoCheckinEnabled)
			{
				num ^= this.FsgAutoCheckinEnabled.GetHashCode();
			}
			if (this.HasFriendWeekConcededGameMinTotalTurns)
			{
				num ^= this.FriendWeekConcededGameMinTotalTurns.GetHashCode();
			}
			if (this.HasFriendWeekAllowsTavernBrawlRecordUpdate)
			{
				num ^= this.FriendWeekAllowsTavernBrawlRecordUpdate.GetHashCode();
			}
			if (this.HasFsgShowBetaLabel)
			{
				num ^= this.FsgShowBetaLabel.GetHashCode();
			}
			if (this.HasFsgFriendListPatronCountLimit)
			{
				num ^= this.FsgFriendListPatronCountLimit.GetHashCode();
			}
			if (this.HasArenaClosedToNewSessionsSeconds)
			{
				num ^= this.ArenaClosedToNewSessionsSeconds.GetHashCode();
			}
			if (this.HasFsgLoginScanEnabled)
			{
				num ^= this.FsgLoginScanEnabled.GetHashCode();
			}
			if (this.HasFsgMaxPresencePubscribedPatronCount)
			{
				num ^= this.FsgMaxPresencePubscribedPatronCount.GetHashCode();
			}
			if (this.HasQuickPackOpeningAllowed)
			{
				num ^= this.QuickPackOpeningAllowed.GetHashCode();
			}
			if (this.HasAllowIosHighres)
			{
				num ^= this.AllowIosHighres.GetHashCode();
			}
			if (this.HasSimpleCheckout)
			{
				num ^= this.SimpleCheckout.GetHashCode();
			}
			if (this.HasDeckCompletionGetPlayerCollectionFromClient)
			{
				num ^= this.DeckCompletionGetPlayerCollectionFromClient.GetHashCode();
			}
			if (this.HasSoftAccountPurchasing)
			{
				num ^= this.SoftAccountPurchasing.GetHashCode();
			}
			if (this.HasEnableSmartDeckCompletion)
			{
				num ^= this.EnableSmartDeckCompletion.GetHashCode();
			}
			if (this.HasNumClassicPacksUntilDeprioritize)
			{
				num ^= this.NumClassicPacksUntilDeprioritize.GetHashCode();
			}
			if (this.HasAllowOfflineClientActivityIos)
			{
				num ^= this.AllowOfflineClientActivityIos.GetHashCode();
			}
			if (this.HasAllowOfflineClientActivityAndroid)
			{
				num ^= this.AllowOfflineClientActivityAndroid.GetHashCode();
			}
			if (this.HasAllowOfflineClientActivityDesktop)
			{
				num ^= this.AllowOfflineClientActivityDesktop.GetHashCode();
			}
			if (this.HasAllowOfflineClientDeckDeletion)
			{
				num ^= this.AllowOfflineClientDeckDeletion.GetHashCode();
			}
			if (this.HasBattlegrounds)
			{
				num ^= this.Battlegrounds.GetHashCode();
			}
			if (this.HasBattlegroundsFriendlyChallenge)
			{
				num ^= this.BattlegroundsFriendlyChallenge.GetHashCode();
			}
			if (this.HasSimpleCheckoutIos)
			{
				num ^= this.SimpleCheckoutIos.GetHashCode();
			}
			if (this.HasSimpleCheckoutAndroidAmazon)
			{
				num ^= this.SimpleCheckoutAndroidAmazon.GetHashCode();
			}
			if (this.HasSimpleCheckoutAndroidGoogle)
			{
				num ^= this.SimpleCheckoutAndroidGoogle.GetHashCode();
			}
			if (this.HasSimpleCheckoutAndroidGlobal)
			{
				num ^= this.SimpleCheckoutAndroidGlobal.GetHashCode();
			}
			if (this.HasSimpleCheckoutWin)
			{
				num ^= this.SimpleCheckoutWin.GetHashCode();
			}
			if (this.HasSimpleCheckoutMac)
			{
				num ^= this.SimpleCheckoutMac.GetHashCode();
			}
			if (this.HasBattlegroundsEarlyAccessLicense)
			{
				num ^= this.BattlegroundsEarlyAccessLicense.GetHashCode();
			}
			if (this.HasVirtualCurrencyEnabled)
			{
				num ^= this.VirtualCurrencyEnabled.GetHashCode();
			}
			if (this.HasBattlegroundsTutorial)
			{
				num ^= this.BattlegroundsTutorial.GetHashCode();
			}
			if (this.HasVintageStoreEnabled)
			{
				num ^= this.VintageStoreEnabled.GetHashCode();
			}
			if (this.HasBoosterRotatingSoonWarnDaysWithoutSale)
			{
				num ^= this.BoosterRotatingSoonWarnDaysWithoutSale.GetHashCode();
			}
			if (this.HasBoosterRotatingSoonWarnDaysWithSale)
			{
				num ^= this.BoosterRotatingSoonWarnDaysWithSale.GetHashCode();
			}
			if (this.HasBattlegroundsMaxRankedPartySize)
			{
				num ^= this.BattlegroundsMaxRankedPartySize.GetHashCode();
			}
			if (this.HasDeckReordering)
			{
				num ^= this.DeckReordering.GetHashCode();
			}
			if (this.HasProgressionEnabled)
			{
				num ^= this.ProgressionEnabled.GetHashCode();
			}
			if (this.HasPvpdrClosedToNewSessionsSeconds)
			{
				num ^= this.PvpdrClosedToNewSessionsSeconds.GetHashCode();
			}
			if (this.HasDuels)
			{
				num ^= this.Duels.GetHashCode();
			}
			if (this.HasDuelsEarlyAccessLicense)
			{
				num ^= this.DuelsEarlyAccessLicense.GetHashCode();
			}
			if (this.HasPaidDuels)
			{
				num ^= this.PaidDuels.GetHashCode();
			}
			if (this.HasAllowLiveFpsGathering)
			{
				num ^= this.AllowLiveFpsGathering.GetHashCode();
			}
			if (this.HasJournalButtonDisabled)
			{
				num ^= this.JournalButtonDisabled.GetHashCode();
			}
			if (this.HasAchievementToastDisabled)
			{
				num ^= this.AchievementToastDisabled.GetHashCode();
			}
			if (this.HasCheckForNewQuestsIntervalJitterSecs)
			{
				num ^= this.CheckForNewQuestsIntervalJitterSecs.GetHashCode();
			}
			if (this.HasRankedStandard)
			{
				num ^= this.RankedStandard.GetHashCode();
			}
			if (this.HasRankedWild)
			{
				num ^= this.RankedWild.GetHashCode();
			}
			if (this.HasRankedClassic)
			{
				num ^= this.RankedClassic.GetHashCode();
			}
			if (this.HasRankedNewPlayer)
			{
				num ^= this.RankedNewPlayer.GetHashCode();
			}
			if (this.HasContentstackEnabled)
			{
				num ^= this.ContentstackEnabled.GetHashCode();
			}
			if (this.HasEndOfTurnToastPauseBufferSecs)
			{
				num ^= this.EndOfTurnToastPauseBufferSecs.GetHashCode();
			}
			if (this.HasAppRatingEnabled)
			{
				num ^= this.AppRatingEnabled.GetHashCode();
			}
			if (this.HasAppRatingSamplingPercentage)
			{
				num ^= this.AppRatingSamplingPercentage.GetHashCode();
			}
			if (this.HasBuyCardBacksFromCollectionManagerEnabled)
			{
				num ^= this.BuyCardBacksFromCollectionManagerEnabled.GetHashCode();
			}
			if (this.HasBuyHeroSkinsFromCollectionManagerEnabled)
			{
				num ^= this.BuyHeroSkinsFromCollectionManagerEnabled.GetHashCode();
			}
			return num;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001E4A4 File Offset: 0x0001C6A4
		public override bool Equals(object obj)
		{
			GuardianVars guardianVars = obj as GuardianVars;
			return guardianVars != null && this.HasTourney == guardianVars.HasTourney && (!this.HasTourney || this.Tourney.Equals(guardianVars.Tourney)) && this.HasPractice == guardianVars.HasPractice && (!this.HasPractice || this.Practice.Equals(guardianVars.Practice)) && this.HasCasual == guardianVars.HasCasual && (!this.HasCasual || this.Casual.Equals(guardianVars.Casual)) && this.HasForge == guardianVars.HasForge && (!this.HasForge || this.Forge.Equals(guardianVars.Forge)) && this.HasFriendly == guardianVars.HasFriendly && (!this.HasFriendly || this.Friendly.Equals(guardianVars.Friendly)) && this.HasManager == guardianVars.HasManager && (!this.HasManager || this.Manager.Equals(guardianVars.Manager)) && this.HasCrafting == guardianVars.HasCrafting && (!this.HasCrafting || this.Crafting.Equals(guardianVars.Crafting)) && this.HasHunter == guardianVars.HasHunter && (!this.HasHunter || this.Hunter.Equals(guardianVars.Hunter)) && this.HasMage == guardianVars.HasMage && (!this.HasMage || this.Mage.Equals(guardianVars.Mage)) && this.HasPaladin == guardianVars.HasPaladin && (!this.HasPaladin || this.Paladin.Equals(guardianVars.Paladin)) && this.HasPriest == guardianVars.HasPriest && (!this.HasPriest || this.Priest.Equals(guardianVars.Priest)) && this.HasRogue == guardianVars.HasRogue && (!this.HasRogue || this.Rogue.Equals(guardianVars.Rogue)) && this.HasShaman == guardianVars.HasShaman && (!this.HasShaman || this.Shaman.Equals(guardianVars.Shaman)) && this.HasWarlock == guardianVars.HasWarlock && (!this.HasWarlock || this.Warlock.Equals(guardianVars.Warlock)) && this.HasWarrior == guardianVars.HasWarrior && (!this.HasWarrior || this.Warrior.Equals(guardianVars.Warrior)) && this.HasShowUserUI == guardianVars.HasShowUserUI && (!this.HasShowUserUI || this.ShowUserUI.Equals(guardianVars.ShowUserUI)) && this.HasStore == guardianVars.HasStore && (!this.HasStore || this.Store.Equals(guardianVars.Store)) && this.HasBattlePay == guardianVars.HasBattlePay && (!this.HasBattlePay || this.BattlePay.Equals(guardianVars.BattlePay)) && this.HasBuyWithGold == guardianVars.HasBuyWithGold && (!this.HasBuyWithGold || this.BuyWithGold.Equals(guardianVars.BuyWithGold)) && this.HasTavernBrawl == guardianVars.HasTavernBrawl && (!this.HasTavernBrawl || this.TavernBrawl.Equals(guardianVars.TavernBrawl)) && this.HasClientOptionsUpdateIntervalSeconds == guardianVars.HasClientOptionsUpdateIntervalSeconds && (!this.HasClientOptionsUpdateIntervalSeconds || this.ClientOptionsUpdateIntervalSeconds.Equals(guardianVars.ClientOptionsUpdateIntervalSeconds)) && this.HasCaisEnabledNonMobile == guardianVars.HasCaisEnabledNonMobile && (!this.HasCaisEnabledNonMobile || this.CaisEnabledNonMobile.Equals(guardianVars.CaisEnabledNonMobile)) && this.HasCaisEnabledMobileChina == guardianVars.HasCaisEnabledMobileChina && (!this.HasCaisEnabledMobileChina || this.CaisEnabledMobileChina.Equals(guardianVars.CaisEnabledMobileChina)) && this.HasCaisEnabledMobileSouthKorea == guardianVars.HasCaisEnabledMobileSouthKorea && (!this.HasCaisEnabledMobileSouthKorea || this.CaisEnabledMobileSouthKorea.Equals(guardianVars.CaisEnabledMobileSouthKorea)) && this.HasSendTelemetryPresence == guardianVars.HasSendTelemetryPresence && (!this.HasSendTelemetryPresence || this.SendTelemetryPresence.Equals(guardianVars.SendTelemetryPresence)) && this.HasFriendWeekConcederMaxDefense == guardianVars.HasFriendWeekConcederMaxDefense && (!this.HasFriendWeekConcederMaxDefense || this.FriendWeekConcederMaxDefense.Equals(guardianVars.FriendWeekConcederMaxDefense)) && this.HasWinsPerGold == guardianVars.HasWinsPerGold && (!this.HasWinsPerGold || this.WinsPerGold.Equals(guardianVars.WinsPerGold)) && this.HasGoldPerReward == guardianVars.HasGoldPerReward && (!this.HasGoldPerReward || this.GoldPerReward.Equals(guardianVars.GoldPerReward)) && this.HasMaxGoldPerDay == guardianVars.HasMaxGoldPerDay && (!this.HasMaxGoldPerDay || this.MaxGoldPerDay.Equals(guardianVars.MaxGoldPerDay)) && this.HasXpSoloLimit == guardianVars.HasXpSoloLimit && (!this.HasXpSoloLimit || this.XpSoloLimit.Equals(guardianVars.XpSoloLimit)) && this.HasMaxHeroLevel == guardianVars.HasMaxHeroLevel && (!this.HasMaxHeroLevel || this.MaxHeroLevel.Equals(guardianVars.MaxHeroLevel)) && this.HasEventTimingMod == guardianVars.HasEventTimingMod && (!this.HasEventTimingMod || this.EventTimingMod.Equals(guardianVars.EventTimingMod)) && this.HasFsgEnabled == guardianVars.HasFsgEnabled && (!this.HasFsgEnabled || this.FsgEnabled.Equals(guardianVars.FsgEnabled)) && this.HasFsgAutoCheckinEnabled == guardianVars.HasFsgAutoCheckinEnabled && (!this.HasFsgAutoCheckinEnabled || this.FsgAutoCheckinEnabled.Equals(guardianVars.FsgAutoCheckinEnabled)) && this.HasFriendWeekConcededGameMinTotalTurns == guardianVars.HasFriendWeekConcededGameMinTotalTurns && (!this.HasFriendWeekConcededGameMinTotalTurns || this.FriendWeekConcededGameMinTotalTurns.Equals(guardianVars.FriendWeekConcededGameMinTotalTurns)) && this.HasFriendWeekAllowsTavernBrawlRecordUpdate == guardianVars.HasFriendWeekAllowsTavernBrawlRecordUpdate && (!this.HasFriendWeekAllowsTavernBrawlRecordUpdate || this.FriendWeekAllowsTavernBrawlRecordUpdate.Equals(guardianVars.FriendWeekAllowsTavernBrawlRecordUpdate)) && this.HasFsgShowBetaLabel == guardianVars.HasFsgShowBetaLabel && (!this.HasFsgShowBetaLabel || this.FsgShowBetaLabel.Equals(guardianVars.FsgShowBetaLabel)) && this.HasFsgFriendListPatronCountLimit == guardianVars.HasFsgFriendListPatronCountLimit && (!this.HasFsgFriendListPatronCountLimit || this.FsgFriendListPatronCountLimit.Equals(guardianVars.FsgFriendListPatronCountLimit)) && this.HasArenaClosedToNewSessionsSeconds == guardianVars.HasArenaClosedToNewSessionsSeconds && (!this.HasArenaClosedToNewSessionsSeconds || this.ArenaClosedToNewSessionsSeconds.Equals(guardianVars.ArenaClosedToNewSessionsSeconds)) && this.HasFsgLoginScanEnabled == guardianVars.HasFsgLoginScanEnabled && (!this.HasFsgLoginScanEnabled || this.FsgLoginScanEnabled.Equals(guardianVars.FsgLoginScanEnabled)) && this.HasFsgMaxPresencePubscribedPatronCount == guardianVars.HasFsgMaxPresencePubscribedPatronCount && (!this.HasFsgMaxPresencePubscribedPatronCount || this.FsgMaxPresencePubscribedPatronCount.Equals(guardianVars.FsgMaxPresencePubscribedPatronCount)) && this.HasQuickPackOpeningAllowed == guardianVars.HasQuickPackOpeningAllowed && (!this.HasQuickPackOpeningAllowed || this.QuickPackOpeningAllowed.Equals(guardianVars.QuickPackOpeningAllowed)) && this.HasAllowIosHighres == guardianVars.HasAllowIosHighres && (!this.HasAllowIosHighres || this.AllowIosHighres.Equals(guardianVars.AllowIosHighres)) && this.HasSimpleCheckout == guardianVars.HasSimpleCheckout && (!this.HasSimpleCheckout || this.SimpleCheckout.Equals(guardianVars.SimpleCheckout)) && this.HasDeckCompletionGetPlayerCollectionFromClient == guardianVars.HasDeckCompletionGetPlayerCollectionFromClient && (!this.HasDeckCompletionGetPlayerCollectionFromClient || this.DeckCompletionGetPlayerCollectionFromClient.Equals(guardianVars.DeckCompletionGetPlayerCollectionFromClient)) && this.HasSoftAccountPurchasing == guardianVars.HasSoftAccountPurchasing && (!this.HasSoftAccountPurchasing || this.SoftAccountPurchasing.Equals(guardianVars.SoftAccountPurchasing)) && this.HasEnableSmartDeckCompletion == guardianVars.HasEnableSmartDeckCompletion && (!this.HasEnableSmartDeckCompletion || this.EnableSmartDeckCompletion.Equals(guardianVars.EnableSmartDeckCompletion)) && this.HasNumClassicPacksUntilDeprioritize == guardianVars.HasNumClassicPacksUntilDeprioritize && (!this.HasNumClassicPacksUntilDeprioritize || this.NumClassicPacksUntilDeprioritize.Equals(guardianVars.NumClassicPacksUntilDeprioritize)) && this.HasAllowOfflineClientActivityIos == guardianVars.HasAllowOfflineClientActivityIos && (!this.HasAllowOfflineClientActivityIos || this.AllowOfflineClientActivityIos.Equals(guardianVars.AllowOfflineClientActivityIos)) && this.HasAllowOfflineClientActivityAndroid == guardianVars.HasAllowOfflineClientActivityAndroid && (!this.HasAllowOfflineClientActivityAndroid || this.AllowOfflineClientActivityAndroid.Equals(guardianVars.AllowOfflineClientActivityAndroid)) && this.HasAllowOfflineClientActivityDesktop == guardianVars.HasAllowOfflineClientActivityDesktop && (!this.HasAllowOfflineClientActivityDesktop || this.AllowOfflineClientActivityDesktop.Equals(guardianVars.AllowOfflineClientActivityDesktop)) && this.HasAllowOfflineClientDeckDeletion == guardianVars.HasAllowOfflineClientDeckDeletion && (!this.HasAllowOfflineClientDeckDeletion || this.AllowOfflineClientDeckDeletion.Equals(guardianVars.AllowOfflineClientDeckDeletion)) && this.HasBattlegrounds == guardianVars.HasBattlegrounds && (!this.HasBattlegrounds || this.Battlegrounds.Equals(guardianVars.Battlegrounds)) && this.HasBattlegroundsFriendlyChallenge == guardianVars.HasBattlegroundsFriendlyChallenge && (!this.HasBattlegroundsFriendlyChallenge || this.BattlegroundsFriendlyChallenge.Equals(guardianVars.BattlegroundsFriendlyChallenge)) && this.HasSimpleCheckoutIos == guardianVars.HasSimpleCheckoutIos && (!this.HasSimpleCheckoutIos || this.SimpleCheckoutIos.Equals(guardianVars.SimpleCheckoutIos)) && this.HasSimpleCheckoutAndroidAmazon == guardianVars.HasSimpleCheckoutAndroidAmazon && (!this.HasSimpleCheckoutAndroidAmazon || this.SimpleCheckoutAndroidAmazon.Equals(guardianVars.SimpleCheckoutAndroidAmazon)) && this.HasSimpleCheckoutAndroidGoogle == guardianVars.HasSimpleCheckoutAndroidGoogle && (!this.HasSimpleCheckoutAndroidGoogle || this.SimpleCheckoutAndroidGoogle.Equals(guardianVars.SimpleCheckoutAndroidGoogle)) && this.HasSimpleCheckoutAndroidGlobal == guardianVars.HasSimpleCheckoutAndroidGlobal && (!this.HasSimpleCheckoutAndroidGlobal || this.SimpleCheckoutAndroidGlobal.Equals(guardianVars.SimpleCheckoutAndroidGlobal)) && this.HasSimpleCheckoutWin == guardianVars.HasSimpleCheckoutWin && (!this.HasSimpleCheckoutWin || this.SimpleCheckoutWin.Equals(guardianVars.SimpleCheckoutWin)) && this.HasSimpleCheckoutMac == guardianVars.HasSimpleCheckoutMac && (!this.HasSimpleCheckoutMac || this.SimpleCheckoutMac.Equals(guardianVars.SimpleCheckoutMac)) && this.HasBattlegroundsEarlyAccessLicense == guardianVars.HasBattlegroundsEarlyAccessLicense && (!this.HasBattlegroundsEarlyAccessLicense || this.BattlegroundsEarlyAccessLicense.Equals(guardianVars.BattlegroundsEarlyAccessLicense)) && this.HasVirtualCurrencyEnabled == guardianVars.HasVirtualCurrencyEnabled && (!this.HasVirtualCurrencyEnabled || this.VirtualCurrencyEnabled.Equals(guardianVars.VirtualCurrencyEnabled)) && this.HasBattlegroundsTutorial == guardianVars.HasBattlegroundsTutorial && (!this.HasBattlegroundsTutorial || this.BattlegroundsTutorial.Equals(guardianVars.BattlegroundsTutorial)) && this.HasVintageStoreEnabled == guardianVars.HasVintageStoreEnabled && (!this.HasVintageStoreEnabled || this.VintageStoreEnabled.Equals(guardianVars.VintageStoreEnabled)) && this.HasBoosterRotatingSoonWarnDaysWithoutSale == guardianVars.HasBoosterRotatingSoonWarnDaysWithoutSale && (!this.HasBoosterRotatingSoonWarnDaysWithoutSale || this.BoosterRotatingSoonWarnDaysWithoutSale.Equals(guardianVars.BoosterRotatingSoonWarnDaysWithoutSale)) && this.HasBoosterRotatingSoonWarnDaysWithSale == guardianVars.HasBoosterRotatingSoonWarnDaysWithSale && (!this.HasBoosterRotatingSoonWarnDaysWithSale || this.BoosterRotatingSoonWarnDaysWithSale.Equals(guardianVars.BoosterRotatingSoonWarnDaysWithSale)) && this.HasBattlegroundsMaxRankedPartySize == guardianVars.HasBattlegroundsMaxRankedPartySize && (!this.HasBattlegroundsMaxRankedPartySize || this.BattlegroundsMaxRankedPartySize.Equals(guardianVars.BattlegroundsMaxRankedPartySize)) && this.HasDeckReordering == guardianVars.HasDeckReordering && (!this.HasDeckReordering || this.DeckReordering.Equals(guardianVars.DeckReordering)) && this.HasProgressionEnabled == guardianVars.HasProgressionEnabled && (!this.HasProgressionEnabled || this.ProgressionEnabled.Equals(guardianVars.ProgressionEnabled)) && this.HasPvpdrClosedToNewSessionsSeconds == guardianVars.HasPvpdrClosedToNewSessionsSeconds && (!this.HasPvpdrClosedToNewSessionsSeconds || this.PvpdrClosedToNewSessionsSeconds.Equals(guardianVars.PvpdrClosedToNewSessionsSeconds)) && this.HasDuels == guardianVars.HasDuels && (!this.HasDuels || this.Duels.Equals(guardianVars.Duels)) && this.HasDuelsEarlyAccessLicense == guardianVars.HasDuelsEarlyAccessLicense && (!this.HasDuelsEarlyAccessLicense || this.DuelsEarlyAccessLicense.Equals(guardianVars.DuelsEarlyAccessLicense)) && this.HasPaidDuels == guardianVars.HasPaidDuels && (!this.HasPaidDuels || this.PaidDuels.Equals(guardianVars.PaidDuels)) && this.HasAllowLiveFpsGathering == guardianVars.HasAllowLiveFpsGathering && (!this.HasAllowLiveFpsGathering || this.AllowLiveFpsGathering.Equals(guardianVars.AllowLiveFpsGathering)) && this.HasJournalButtonDisabled == guardianVars.HasJournalButtonDisabled && (!this.HasJournalButtonDisabled || this.JournalButtonDisabled.Equals(guardianVars.JournalButtonDisabled)) && this.HasAchievementToastDisabled == guardianVars.HasAchievementToastDisabled && (!this.HasAchievementToastDisabled || this.AchievementToastDisabled.Equals(guardianVars.AchievementToastDisabled)) && this.HasCheckForNewQuestsIntervalJitterSecs == guardianVars.HasCheckForNewQuestsIntervalJitterSecs && (!this.HasCheckForNewQuestsIntervalJitterSecs || this.CheckForNewQuestsIntervalJitterSecs.Equals(guardianVars.CheckForNewQuestsIntervalJitterSecs)) && this.HasRankedStandard == guardianVars.HasRankedStandard && (!this.HasRankedStandard || this.RankedStandard.Equals(guardianVars.RankedStandard)) && this.HasRankedWild == guardianVars.HasRankedWild && (!this.HasRankedWild || this.RankedWild.Equals(guardianVars.RankedWild)) && this.HasRankedClassic == guardianVars.HasRankedClassic && (!this.HasRankedClassic || this.RankedClassic.Equals(guardianVars.RankedClassic)) && this.HasRankedNewPlayer == guardianVars.HasRankedNewPlayer && (!this.HasRankedNewPlayer || this.RankedNewPlayer.Equals(guardianVars.RankedNewPlayer)) && this.HasContentstackEnabled == guardianVars.HasContentstackEnabled && (!this.HasContentstackEnabled || this.ContentstackEnabled.Equals(guardianVars.ContentstackEnabled)) && this.HasEndOfTurnToastPauseBufferSecs == guardianVars.HasEndOfTurnToastPauseBufferSecs && (!this.HasEndOfTurnToastPauseBufferSecs || this.EndOfTurnToastPauseBufferSecs.Equals(guardianVars.EndOfTurnToastPauseBufferSecs)) && this.HasAppRatingEnabled == guardianVars.HasAppRatingEnabled && (!this.HasAppRatingEnabled || this.AppRatingEnabled.Equals(guardianVars.AppRatingEnabled)) && this.HasAppRatingSamplingPercentage == guardianVars.HasAppRatingSamplingPercentage && (!this.HasAppRatingSamplingPercentage || this.AppRatingSamplingPercentage.Equals(guardianVars.AppRatingSamplingPercentage)) && this.HasBuyCardBacksFromCollectionManagerEnabled == guardianVars.HasBuyCardBacksFromCollectionManagerEnabled && (!this.HasBuyCardBacksFromCollectionManagerEnabled || this.BuyCardBacksFromCollectionManagerEnabled.Equals(guardianVars.BuyCardBacksFromCollectionManagerEnabled)) && this.HasBuyHeroSkinsFromCollectionManagerEnabled == guardianVars.HasBuyHeroSkinsFromCollectionManagerEnabled && (!this.HasBuyHeroSkinsFromCollectionManagerEnabled || this.BuyHeroSkinsFromCollectionManagerEnabled.Equals(guardianVars.BuyHeroSkinsFromCollectionManagerEnabled));
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001F463 File Offset: 0x0001D663
		public void Deserialize(Stream stream)
		{
			GuardianVars.Deserialize(stream, this);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0001F46D File Offset: 0x0001D66D
		public static GuardianVars Deserialize(Stream stream, GuardianVars instance)
		{
			return GuardianVars.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001F478 File Offset: 0x0001D678
		public static GuardianVars DeserializeLengthDelimited(Stream stream)
		{
			GuardianVars guardianVars = new GuardianVars();
			GuardianVars.DeserializeLengthDelimited(stream, guardianVars);
			return guardianVars;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0001F494 File Offset: 0x0001D694
		public static GuardianVars DeserializeLengthDelimited(Stream stream, GuardianVars instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GuardianVars.Deserialize(stream, instance, num);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0001F4BC File Offset: 0x0001D6BC
		public static GuardianVars Deserialize(Stream stream, GuardianVars instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (num <= 56)
					{
						if (num <= 24)
						{
							if (num == 8)
							{
								instance.Tourney = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 16)
							{
								instance.Practice = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 24)
							{
								instance.Casual = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else if (num <= 40)
						{
							if (num == 32)
							{
								instance.Forge = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 40)
							{
								instance.Friendly = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else
						{
							if (num == 48)
							{
								instance.Manager = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 56)
							{
								instance.Crafting = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
					}
					else if (num <= 88)
					{
						if (num <= 72)
						{
							if (num == 64)
							{
								instance.Hunter = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 72)
							{
								instance.Mage = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else
						{
							if (num == 80)
							{
								instance.Paladin = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 88)
							{
								instance.Priest = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
					}
					else if (num <= 104)
					{
						if (num == 96)
						{
							instance.Rogue = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 104)
						{
							instance.Shaman = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 112)
						{
							instance.Warlock = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 120)
						{
							instance.Warrior = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0U:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 16U:
						if (key.WireType == Wire.Varint)
						{
							instance.ShowUserUI = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 17U:
						if (key.WireType == Wire.Varint)
						{
							instance.Store = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 18U:
						if (key.WireType == Wire.Varint)
						{
							instance.BattlePay = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 19U:
						if (key.WireType == Wire.Varint)
						{
							instance.BuyWithGold = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 20U:
						if (key.WireType == Wire.Varint)
						{
							instance.TavernBrawl = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 21U:
						if (key.WireType == Wire.Varint)
						{
							instance.ClientOptionsUpdateIntervalSeconds = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 22U:
						if (key.WireType == Wire.Varint)
						{
							instance.CaisEnabledNonMobile = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 23U:
						if (key.WireType == Wire.Varint)
						{
							instance.CaisEnabledMobileChina = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 24U:
						if (key.WireType == Wire.Varint)
						{
							instance.CaisEnabledMobileSouthKorea = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 25U:
						if (key.WireType == Wire.Varint)
						{
							instance.SendTelemetryPresence = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 26U:
						if (key.WireType == Wire.Varint)
						{
							instance.FriendWeekConcederMaxDefense = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 27U:
						if (key.WireType == Wire.Varint)
						{
							instance.WinsPerGold = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 28U:
						if (key.WireType == Wire.Varint)
						{
							instance.GoldPerReward = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 29U:
						if (key.WireType == Wire.Varint)
						{
							instance.MaxGoldPerDay = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 30U:
						if (key.WireType == Wire.Varint)
						{
							instance.XpSoloLimit = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 31U:
						if (key.WireType == Wire.Varint)
						{
							instance.MaxHeroLevel = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 32U:
						if (key.WireType == Wire.Fixed32)
						{
							instance.EventTimingMod = binaryReader.ReadSingle();
							continue;
						}
						continue;
					case 33U:
						if (key.WireType == Wire.Varint)
						{
							instance.FsgEnabled = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 34U:
						if (key.WireType == Wire.Varint)
						{
							instance.FsgAutoCheckinEnabled = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 35U:
						if (key.WireType == Wire.Varint)
						{
							instance.FriendWeekConcededGameMinTotalTurns = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 36U:
						if (key.WireType == Wire.Varint)
						{
							instance.FriendWeekAllowsTavernBrawlRecordUpdate = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 37U:
						if (key.WireType == Wire.Varint)
						{
							instance.FsgShowBetaLabel = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 38U:
						if (key.WireType == Wire.Varint)
						{
							instance.FsgFriendListPatronCountLimit = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 39U:
						if (key.WireType == Wire.Varint)
						{
							instance.ArenaClosedToNewSessionsSeconds = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						continue;
					case 40U:
						if (key.WireType == Wire.Varint)
						{
							instance.FsgLoginScanEnabled = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 41U:
						if (key.WireType == Wire.Varint)
						{
							instance.FsgMaxPresencePubscribedPatronCount = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 42U:
						if (key.WireType == Wire.Varint)
						{
							instance.QuickPackOpeningAllowed = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 43U:
						if (key.WireType == Wire.Varint)
						{
							instance.AllowIosHighres = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 44U:
						if (key.WireType == Wire.Varint)
						{
							instance.SimpleCheckout = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 45U:
						if (key.WireType == Wire.Varint)
						{
							instance.DeckCompletionGetPlayerCollectionFromClient = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 46U:
						if (key.WireType == Wire.Varint)
						{
							instance.SoftAccountPurchasing = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 47U:
						if (key.WireType == Wire.Varint)
						{
							instance.EnableSmartDeckCompletion = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 48U:
						if (key.WireType == Wire.Varint)
						{
							instance.NumClassicPacksUntilDeprioritize = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 49U:
						if (key.WireType == Wire.Varint)
						{
							instance.AllowOfflineClientActivityIos = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 50U:
						if (key.WireType == Wire.Varint)
						{
							instance.AllowOfflineClientActivityAndroid = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 51U:
						if (key.WireType == Wire.Varint)
						{
							instance.AllowOfflineClientActivityDesktop = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 52U:
						if (key.WireType == Wire.Varint)
						{
							instance.AllowOfflineClientDeckDeletion = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 53U:
						if (key.WireType == Wire.Varint)
						{
							instance.Battlegrounds = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 54U:
						if (key.WireType == Wire.Varint)
						{
							instance.BattlegroundsFriendlyChallenge = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 55U:
						if (key.WireType == Wire.Varint)
						{
							instance.SimpleCheckoutIos = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 56U:
						if (key.WireType == Wire.Varint)
						{
							instance.SimpleCheckoutAndroidAmazon = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 57U:
						if (key.WireType == Wire.Varint)
						{
							instance.SimpleCheckoutAndroidGoogle = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 58U:
						if (key.WireType == Wire.Varint)
						{
							instance.SimpleCheckoutAndroidGlobal = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 59U:
						if (key.WireType == Wire.Varint)
						{
							instance.SimpleCheckoutWin = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 60U:
						if (key.WireType == Wire.Varint)
						{
							instance.SimpleCheckoutMac = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 61U:
						if (key.WireType == Wire.Varint)
						{
							instance.BattlegroundsEarlyAccessLicense = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 62U:
						if (key.WireType == Wire.Varint)
						{
							instance.VirtualCurrencyEnabled = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 63U:
						if (key.WireType == Wire.Varint)
						{
							instance.BattlegroundsTutorial = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 64U:
						if (key.WireType == Wire.Varint)
						{
							instance.VintageStoreEnabled = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 65U:
						if (key.WireType == Wire.Varint)
						{
							instance.BoosterRotatingSoonWarnDaysWithoutSale = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 66U:
						if (key.WireType == Wire.Varint)
						{
							instance.BoosterRotatingSoonWarnDaysWithSale = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 67U:
						if (key.WireType == Wire.Varint)
						{
							instance.BattlegroundsMaxRankedPartySize = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 68U:
						if (key.WireType == Wire.Varint)
						{
							instance.DeckReordering = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 69U:
						if (key.WireType == Wire.Varint)
						{
							instance.ProgressionEnabled = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 70U:
						if (key.WireType == Wire.Varint)
						{
							instance.PvpdrClosedToNewSessionsSeconds = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						continue;
					case 71U:
						if (key.WireType == Wire.Varint)
						{
							instance.Duels = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 72U:
						if (key.WireType == Wire.Varint)
						{
							instance.DuelsEarlyAccessLicense = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						continue;
					case 73U:
						if (key.WireType == Wire.Varint)
						{
							instance.PaidDuels = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 74U:
						if (key.WireType == Wire.Varint)
						{
							instance.AllowLiveFpsGathering = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 75U:
						if (key.WireType == Wire.Varint)
						{
							instance.JournalButtonDisabled = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 76U:
						if (key.WireType == Wire.Varint)
						{
							instance.AchievementToastDisabled = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 77U:
						if (key.WireType == Wire.Fixed32)
						{
							instance.CheckForNewQuestsIntervalJitterSecs = binaryReader.ReadSingle();
							continue;
						}
						continue;
					case 78U:
						if (key.WireType == Wire.Varint)
						{
							instance.RankedStandard = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 79U:
						if (key.WireType == Wire.Varint)
						{
							instance.RankedWild = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 80U:
						if (key.WireType == Wire.Varint)
						{
							instance.RankedClassic = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 81U:
						if (key.WireType == Wire.Varint)
						{
							instance.RankedNewPlayer = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 82U:
						if (key.WireType == Wire.Varint)
						{
							instance.ContentstackEnabled = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 83U:
						if (key.WireType == Wire.Fixed32)
						{
							instance.EndOfTurnToastPauseBufferSecs = binaryReader.ReadSingle();
							continue;
						}
						continue;
					case 84U:
						if (key.WireType == Wire.Varint)
						{
							instance.AppRatingEnabled = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 85U:
						if (key.WireType == Wire.Fixed32)
						{
							instance.AppRatingSamplingPercentage = binaryReader.ReadSingle();
							continue;
						}
						continue;
					case 86U:
						if (key.WireType == Wire.Varint)
						{
							instance.BuyCardBacksFromCollectionManagerEnabled = ProtocolParser.ReadBool(stream);
							continue;
						}
						continue;
					case 87U:
						if (key.WireType == Wire.Varint)
						{
							instance.BuyHeroSkinsFromCollectionManagerEnabled = ProtocolParser.ReadBool(stream);
							continue;
						}
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

		// Token: 0x060008FE RID: 2302 RVA: 0x0002004C File Offset: 0x0001E24C
		public void Serialize(Stream stream)
		{
			GuardianVars.Serialize(stream, this);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00020058 File Offset: 0x0001E258
		public static void Serialize(Stream stream, GuardianVars instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasTourney)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.Tourney);
			}
			if (instance.HasPractice)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Practice);
			}
			if (instance.HasCasual)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Casual);
			}
			if (instance.HasForge)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.Forge);
			}
			if (instance.HasFriendly)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.Friendly);
			}
			if (instance.HasManager)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.Manager);
			}
			if (instance.HasCrafting)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.Crafting);
			}
			if (instance.HasHunter)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.Hunter);
			}
			if (instance.HasMage)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.Mage);
			}
			if (instance.HasPaladin)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteBool(stream, instance.Paladin);
			}
			if (instance.HasPriest)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteBool(stream, instance.Priest);
			}
			if (instance.HasRogue)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteBool(stream, instance.Rogue);
			}
			if (instance.HasShaman)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteBool(stream, instance.Shaman);
			}
			if (instance.HasWarlock)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteBool(stream, instance.Warlock);
			}
			if (instance.HasWarrior)
			{
				stream.WriteByte(120);
				ProtocolParser.WriteBool(stream, instance.Warrior);
			}
			if (instance.HasShowUserUI)
			{
				stream.WriteByte(128);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ShowUserUI));
			}
			if (instance.HasStore)
			{
				stream.WriteByte(136);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.Store);
			}
			if (instance.HasBattlePay)
			{
				stream.WriteByte(144);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.BattlePay);
			}
			if (instance.HasBuyWithGold)
			{
				stream.WriteByte(152);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.BuyWithGold);
			}
			if (instance.HasTavernBrawl)
			{
				stream.WriteByte(160);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.TavernBrawl);
			}
			if (instance.HasClientOptionsUpdateIntervalSeconds)
			{
				stream.WriteByte(168);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ClientOptionsUpdateIntervalSeconds));
			}
			if (instance.HasCaisEnabledNonMobile)
			{
				stream.WriteByte(176);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.CaisEnabledNonMobile);
			}
			if (instance.HasCaisEnabledMobileChina)
			{
				stream.WriteByte(184);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.CaisEnabledMobileChina);
			}
			if (instance.HasCaisEnabledMobileSouthKorea)
			{
				stream.WriteByte(192);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.CaisEnabledMobileSouthKorea);
			}
			if (instance.HasSendTelemetryPresence)
			{
				stream.WriteByte(200);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.SendTelemetryPresence);
			}
			if (instance.HasFriendWeekConcederMaxDefense)
			{
				stream.WriteByte(208);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FriendWeekConcederMaxDefense));
			}
			if (instance.HasWinsPerGold)
			{
				stream.WriteByte(216);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.WinsPerGold));
			}
			if (instance.HasGoldPerReward)
			{
				stream.WriteByte(224);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GoldPerReward));
			}
			if (instance.HasMaxGoldPerDay)
			{
				stream.WriteByte(232);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MaxGoldPerDay));
			}
			if (instance.HasXpSoloLimit)
			{
				stream.WriteByte(240);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.XpSoloLimit));
			}
			if (instance.HasMaxHeroLevel)
			{
				stream.WriteByte(248);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MaxHeroLevel));
			}
			if (instance.HasEventTimingMod)
			{
				stream.WriteByte(133);
				stream.WriteByte(2);
				binaryWriter.Write(instance.EventTimingMod);
			}
			if (instance.HasFsgEnabled)
			{
				stream.WriteByte(136);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.FsgEnabled);
			}
			if (instance.HasFsgAutoCheckinEnabled)
			{
				stream.WriteByte(144);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.FsgAutoCheckinEnabled);
			}
			if (instance.HasFriendWeekConcededGameMinTotalTurns)
			{
				stream.WriteByte(152);
				stream.WriteByte(2);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FriendWeekConcededGameMinTotalTurns));
			}
			if (instance.HasFriendWeekAllowsTavernBrawlRecordUpdate)
			{
				stream.WriteByte(160);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.FriendWeekAllowsTavernBrawlRecordUpdate);
			}
			if (instance.HasFsgShowBetaLabel)
			{
				stream.WriteByte(168);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.FsgShowBetaLabel);
			}
			if (instance.HasFsgFriendListPatronCountLimit)
			{
				stream.WriteByte(176);
				stream.WriteByte(2);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FsgFriendListPatronCountLimit));
			}
			if (instance.HasArenaClosedToNewSessionsSeconds)
			{
				stream.WriteByte(184);
				stream.WriteByte(2);
				ProtocolParser.WriteUInt32(stream, instance.ArenaClosedToNewSessionsSeconds);
			}
			if (instance.HasFsgLoginScanEnabled)
			{
				stream.WriteByte(192);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.FsgLoginScanEnabled);
			}
			if (instance.HasFsgMaxPresencePubscribedPatronCount)
			{
				stream.WriteByte(200);
				stream.WriteByte(2);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FsgMaxPresencePubscribedPatronCount));
			}
			if (instance.HasQuickPackOpeningAllowed)
			{
				stream.WriteByte(208);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.QuickPackOpeningAllowed);
			}
			if (instance.HasAllowIosHighres)
			{
				stream.WriteByte(216);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.AllowIosHighres);
			}
			if (instance.HasSimpleCheckout)
			{
				stream.WriteByte(224);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.SimpleCheckout);
			}
			if (instance.HasDeckCompletionGetPlayerCollectionFromClient)
			{
				stream.WriteByte(232);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.DeckCompletionGetPlayerCollectionFromClient);
			}
			if (instance.HasSoftAccountPurchasing)
			{
				stream.WriteByte(240);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.SoftAccountPurchasing);
			}
			if (instance.HasEnableSmartDeckCompletion)
			{
				stream.WriteByte(248);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.EnableSmartDeckCompletion);
			}
			if (instance.HasNumClassicPacksUntilDeprioritize)
			{
				stream.WriteByte(128);
				stream.WriteByte(3);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.NumClassicPacksUntilDeprioritize));
			}
			if (instance.HasAllowOfflineClientActivityIos)
			{
				stream.WriteByte(136);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.AllowOfflineClientActivityIos);
			}
			if (instance.HasAllowOfflineClientActivityAndroid)
			{
				stream.WriteByte(144);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.AllowOfflineClientActivityAndroid);
			}
			if (instance.HasAllowOfflineClientActivityDesktop)
			{
				stream.WriteByte(152);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.AllowOfflineClientActivityDesktop);
			}
			if (instance.HasAllowOfflineClientDeckDeletion)
			{
				stream.WriteByte(160);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.AllowOfflineClientDeckDeletion);
			}
			if (instance.HasBattlegrounds)
			{
				stream.WriteByte(168);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.Battlegrounds);
			}
			if (instance.HasBattlegroundsFriendlyChallenge)
			{
				stream.WriteByte(176);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.BattlegroundsFriendlyChallenge);
			}
			if (instance.HasSimpleCheckoutIos)
			{
				stream.WriteByte(184);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.SimpleCheckoutIos);
			}
			if (instance.HasSimpleCheckoutAndroidAmazon)
			{
				stream.WriteByte(192);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.SimpleCheckoutAndroidAmazon);
			}
			if (instance.HasSimpleCheckoutAndroidGoogle)
			{
				stream.WriteByte(200);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.SimpleCheckoutAndroidGoogle);
			}
			if (instance.HasSimpleCheckoutAndroidGlobal)
			{
				stream.WriteByte(208);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.SimpleCheckoutAndroidGlobal);
			}
			if (instance.HasSimpleCheckoutWin)
			{
				stream.WriteByte(216);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.SimpleCheckoutWin);
			}
			if (instance.HasSimpleCheckoutMac)
			{
				stream.WriteByte(224);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.SimpleCheckoutMac);
			}
			if (instance.HasBattlegroundsEarlyAccessLicense)
			{
				stream.WriteByte(232);
				stream.WriteByte(3);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BattlegroundsEarlyAccessLicense));
			}
			if (instance.HasVirtualCurrencyEnabled)
			{
				stream.WriteByte(240);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.VirtualCurrencyEnabled);
			}
			if (instance.HasBattlegroundsTutorial)
			{
				stream.WriteByte(248);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.BattlegroundsTutorial);
			}
			if (instance.HasVintageStoreEnabled)
			{
				stream.WriteByte(128);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.VintageStoreEnabled);
			}
			if (instance.HasBoosterRotatingSoonWarnDaysWithoutSale)
			{
				stream.WriteByte(136);
				stream.WriteByte(4);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BoosterRotatingSoonWarnDaysWithoutSale));
			}
			if (instance.HasBoosterRotatingSoonWarnDaysWithSale)
			{
				stream.WriteByte(144);
				stream.WriteByte(4);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BoosterRotatingSoonWarnDaysWithSale));
			}
			if (instance.HasBattlegroundsMaxRankedPartySize)
			{
				stream.WriteByte(152);
				stream.WriteByte(4);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BattlegroundsMaxRankedPartySize));
			}
			if (instance.HasDeckReordering)
			{
				stream.WriteByte(160);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.DeckReordering);
			}
			if (instance.HasProgressionEnabled)
			{
				stream.WriteByte(168);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.ProgressionEnabled);
			}
			if (instance.HasPvpdrClosedToNewSessionsSeconds)
			{
				stream.WriteByte(176);
				stream.WriteByte(4);
				ProtocolParser.WriteUInt32(stream, instance.PvpdrClosedToNewSessionsSeconds);
			}
			if (instance.HasDuels)
			{
				stream.WriteByte(184);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.Duels);
			}
			if (instance.HasDuelsEarlyAccessLicense)
			{
				stream.WriteByte(192);
				stream.WriteByte(4);
				ProtocolParser.WriteUInt32(stream, instance.DuelsEarlyAccessLicense);
			}
			if (instance.HasPaidDuels)
			{
				stream.WriteByte(200);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.PaidDuels);
			}
			if (instance.HasAllowLiveFpsGathering)
			{
				stream.WriteByte(208);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.AllowLiveFpsGathering);
			}
			if (instance.HasJournalButtonDisabled)
			{
				stream.WriteByte(216);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.JournalButtonDisabled);
			}
			if (instance.HasAchievementToastDisabled)
			{
				stream.WriteByte(224);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.AchievementToastDisabled);
			}
			if (instance.HasCheckForNewQuestsIntervalJitterSecs)
			{
				stream.WriteByte(237);
				stream.WriteByte(4);
				binaryWriter.Write(instance.CheckForNewQuestsIntervalJitterSecs);
			}
			if (instance.HasRankedStandard)
			{
				stream.WriteByte(240);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.RankedStandard);
			}
			if (instance.HasRankedWild)
			{
				stream.WriteByte(248);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.RankedWild);
			}
			if (instance.HasRankedClassic)
			{
				stream.WriteByte(128);
				stream.WriteByte(5);
				ProtocolParser.WriteBool(stream, instance.RankedClassic);
			}
			if (instance.HasRankedNewPlayer)
			{
				stream.WriteByte(136);
				stream.WriteByte(5);
				ProtocolParser.WriteBool(stream, instance.RankedNewPlayer);
			}
			if (instance.HasContentstackEnabled)
			{
				stream.WriteByte(144);
				stream.WriteByte(5);
				ProtocolParser.WriteBool(stream, instance.ContentstackEnabled);
			}
			if (instance.HasEndOfTurnToastPauseBufferSecs)
			{
				stream.WriteByte(157);
				stream.WriteByte(5);
				binaryWriter.Write(instance.EndOfTurnToastPauseBufferSecs);
			}
			if (instance.HasAppRatingEnabled)
			{
				stream.WriteByte(160);
				stream.WriteByte(5);
				ProtocolParser.WriteBool(stream, instance.AppRatingEnabled);
			}
			if (instance.HasAppRatingSamplingPercentage)
			{
				stream.WriteByte(173);
				stream.WriteByte(5);
				binaryWriter.Write(instance.AppRatingSamplingPercentage);
			}
			if (instance.HasBuyCardBacksFromCollectionManagerEnabled)
			{
				stream.WriteByte(176);
				stream.WriteByte(5);
				ProtocolParser.WriteBool(stream, instance.BuyCardBacksFromCollectionManagerEnabled);
			}
			if (instance.HasBuyHeroSkinsFromCollectionManagerEnabled)
			{
				stream.WriteByte(184);
				stream.WriteByte(5);
				ProtocolParser.WriteBool(stream, instance.BuyHeroSkinsFromCollectionManagerEnabled);
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00020CD0 File Offset: 0x0001EED0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTourney)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasPractice)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCasual)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasForge)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFriendly)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasManager)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCrafting)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasHunter)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasMage)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasPaladin)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasPriest)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasRogue)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasShaman)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasWarlock)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasWarrior)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasShowUserUI)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ShowUserUI));
			}
			if (this.HasStore)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasBattlePay)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasBuyWithGold)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasTavernBrawl)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasClientOptionsUpdateIntervalSeconds)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ClientOptionsUpdateIntervalSeconds));
			}
			if (this.HasCaisEnabledNonMobile)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasCaisEnabledMobileChina)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasCaisEnabledMobileSouthKorea)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasSendTelemetryPresence)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasFriendWeekConcederMaxDefense)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FriendWeekConcederMaxDefense));
			}
			if (this.HasWinsPerGold)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.WinsPerGold));
			}
			if (this.HasGoldPerReward)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.GoldPerReward));
			}
			if (this.HasMaxGoldPerDay)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MaxGoldPerDay));
			}
			if (this.HasXpSoloLimit)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.XpSoloLimit));
			}
			if (this.HasMaxHeroLevel)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MaxHeroLevel));
			}
			if (this.HasEventTimingMod)
			{
				num += 2U;
				num += 4U;
			}
			if (this.HasFsgEnabled)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasFsgAutoCheckinEnabled)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasFriendWeekConcededGameMinTotalTurns)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FriendWeekConcededGameMinTotalTurns));
			}
			if (this.HasFriendWeekAllowsTavernBrawlRecordUpdate)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasFsgShowBetaLabel)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasFsgFriendListPatronCountLimit)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FsgFriendListPatronCountLimit));
			}
			if (this.HasArenaClosedToNewSessionsSeconds)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt32(this.ArenaClosedToNewSessionsSeconds);
			}
			if (this.HasFsgLoginScanEnabled)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasFsgMaxPresencePubscribedPatronCount)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FsgMaxPresencePubscribedPatronCount));
			}
			if (this.HasQuickPackOpeningAllowed)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasAllowIosHighres)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasSimpleCheckout)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasDeckCompletionGetPlayerCollectionFromClient)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasSoftAccountPurchasing)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasEnableSmartDeckCompletion)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasNumClassicPacksUntilDeprioritize)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.NumClassicPacksUntilDeprioritize));
			}
			if (this.HasAllowOfflineClientActivityIos)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasAllowOfflineClientActivityAndroid)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasAllowOfflineClientActivityDesktop)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasAllowOfflineClientDeckDeletion)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasBattlegrounds)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasBattlegroundsFriendlyChallenge)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasSimpleCheckoutIos)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasSimpleCheckoutAndroidAmazon)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasSimpleCheckoutAndroidGoogle)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasSimpleCheckoutAndroidGlobal)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasSimpleCheckoutWin)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasSimpleCheckoutMac)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasBattlegroundsEarlyAccessLicense)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BattlegroundsEarlyAccessLicense));
			}
			if (this.HasVirtualCurrencyEnabled)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasBattlegroundsTutorial)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasVintageStoreEnabled)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasBoosterRotatingSoonWarnDaysWithoutSale)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BoosterRotatingSoonWarnDaysWithoutSale));
			}
			if (this.HasBoosterRotatingSoonWarnDaysWithSale)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BoosterRotatingSoonWarnDaysWithSale));
			}
			if (this.HasBattlegroundsMaxRankedPartySize)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BattlegroundsMaxRankedPartySize));
			}
			if (this.HasDeckReordering)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasProgressionEnabled)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasPvpdrClosedToNewSessionsSeconds)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt32(this.PvpdrClosedToNewSessionsSeconds);
			}
			if (this.HasDuels)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasDuelsEarlyAccessLicense)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt32(this.DuelsEarlyAccessLicense);
			}
			if (this.HasPaidDuels)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasAllowLiveFpsGathering)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasJournalButtonDisabled)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasAchievementToastDisabled)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasCheckForNewQuestsIntervalJitterSecs)
			{
				num += 2U;
				num += 4U;
			}
			if (this.HasRankedStandard)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasRankedWild)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasRankedClassic)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasRankedNewPlayer)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasContentstackEnabled)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasEndOfTurnToastPauseBufferSecs)
			{
				num += 2U;
				num += 4U;
			}
			if (this.HasAppRatingEnabled)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasAppRatingSamplingPercentage)
			{
				num += 2U;
				num += 4U;
			}
			if (this.HasBuyCardBacksFromCollectionManagerEnabled)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasBuyHeroSkinsFromCollectionManagerEnabled)
			{
				num += 2U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04000270 RID: 624
		public bool HasTourney;

		// Token: 0x04000271 RID: 625
		private bool _Tourney;

		// Token: 0x04000272 RID: 626
		public bool HasPractice;

		// Token: 0x04000273 RID: 627
		private bool _Practice;

		// Token: 0x04000274 RID: 628
		public bool HasCasual;

		// Token: 0x04000275 RID: 629
		private bool _Casual;

		// Token: 0x04000276 RID: 630
		public bool HasForge;

		// Token: 0x04000277 RID: 631
		private bool _Forge;

		// Token: 0x04000278 RID: 632
		public bool HasFriendly;

		// Token: 0x04000279 RID: 633
		private bool _Friendly;

		// Token: 0x0400027A RID: 634
		public bool HasManager;

		// Token: 0x0400027B RID: 635
		private bool _Manager;

		// Token: 0x0400027C RID: 636
		public bool HasCrafting;

		// Token: 0x0400027D RID: 637
		private bool _Crafting;

		// Token: 0x0400027E RID: 638
		public bool HasHunter;

		// Token: 0x0400027F RID: 639
		private bool _Hunter;

		// Token: 0x04000280 RID: 640
		public bool HasMage;

		// Token: 0x04000281 RID: 641
		private bool _Mage;

		// Token: 0x04000282 RID: 642
		public bool HasPaladin;

		// Token: 0x04000283 RID: 643
		private bool _Paladin;

		// Token: 0x04000284 RID: 644
		public bool HasPriest;

		// Token: 0x04000285 RID: 645
		private bool _Priest;

		// Token: 0x04000286 RID: 646
		public bool HasRogue;

		// Token: 0x04000287 RID: 647
		private bool _Rogue;

		// Token: 0x04000288 RID: 648
		public bool HasShaman;

		// Token: 0x04000289 RID: 649
		private bool _Shaman;

		// Token: 0x0400028A RID: 650
		public bool HasWarlock;

		// Token: 0x0400028B RID: 651
		private bool _Warlock;

		// Token: 0x0400028C RID: 652
		public bool HasWarrior;

		// Token: 0x0400028D RID: 653
		private bool _Warrior;

		// Token: 0x0400028E RID: 654
		public bool HasShowUserUI;

		// Token: 0x0400028F RID: 655
		private int _ShowUserUI;

		// Token: 0x04000290 RID: 656
		public bool HasStore;

		// Token: 0x04000291 RID: 657
		private bool _Store;

		// Token: 0x04000292 RID: 658
		public bool HasBattlePay;

		// Token: 0x04000293 RID: 659
		private bool _BattlePay;

		// Token: 0x04000294 RID: 660
		public bool HasBuyWithGold;

		// Token: 0x04000295 RID: 661
		private bool _BuyWithGold;

		// Token: 0x04000296 RID: 662
		public bool HasTavernBrawl;

		// Token: 0x04000297 RID: 663
		private bool _TavernBrawl;

		// Token: 0x04000298 RID: 664
		public bool HasClientOptionsUpdateIntervalSeconds;

		// Token: 0x04000299 RID: 665
		private int _ClientOptionsUpdateIntervalSeconds;

		// Token: 0x0400029A RID: 666
		public bool HasCaisEnabledNonMobile;

		// Token: 0x0400029B RID: 667
		private bool _CaisEnabledNonMobile;

		// Token: 0x0400029C RID: 668
		public bool HasCaisEnabledMobileChina;

		// Token: 0x0400029D RID: 669
		private bool _CaisEnabledMobileChina;

		// Token: 0x0400029E RID: 670
		public bool HasCaisEnabledMobileSouthKorea;

		// Token: 0x0400029F RID: 671
		private bool _CaisEnabledMobileSouthKorea;

		// Token: 0x040002A0 RID: 672
		public bool HasSendTelemetryPresence;

		// Token: 0x040002A1 RID: 673
		private bool _SendTelemetryPresence;

		// Token: 0x040002A2 RID: 674
		public bool HasFriendWeekConcederMaxDefense;

		// Token: 0x040002A3 RID: 675
		private int _FriendWeekConcederMaxDefense;

		// Token: 0x040002A4 RID: 676
		public bool HasWinsPerGold;

		// Token: 0x040002A5 RID: 677
		private int _WinsPerGold;

		// Token: 0x040002A6 RID: 678
		public bool HasGoldPerReward;

		// Token: 0x040002A7 RID: 679
		private int _GoldPerReward;

		// Token: 0x040002A8 RID: 680
		public bool HasMaxGoldPerDay;

		// Token: 0x040002A9 RID: 681
		private int _MaxGoldPerDay;

		// Token: 0x040002AA RID: 682
		public bool HasXpSoloLimit;

		// Token: 0x040002AB RID: 683
		private int _XpSoloLimit;

		// Token: 0x040002AC RID: 684
		public bool HasMaxHeroLevel;

		// Token: 0x040002AD RID: 685
		private int _MaxHeroLevel;

		// Token: 0x040002AE RID: 686
		public bool HasEventTimingMod;

		// Token: 0x040002AF RID: 687
		private float _EventTimingMod;

		// Token: 0x040002B0 RID: 688
		public bool HasFsgEnabled;

		// Token: 0x040002B1 RID: 689
		private bool _FsgEnabled;

		// Token: 0x040002B2 RID: 690
		public bool HasFsgAutoCheckinEnabled;

		// Token: 0x040002B3 RID: 691
		private bool _FsgAutoCheckinEnabled;

		// Token: 0x040002B4 RID: 692
		public bool HasFriendWeekConcededGameMinTotalTurns;

		// Token: 0x040002B5 RID: 693
		private int _FriendWeekConcededGameMinTotalTurns;

		// Token: 0x040002B6 RID: 694
		public bool HasFriendWeekAllowsTavernBrawlRecordUpdate;

		// Token: 0x040002B7 RID: 695
		private bool _FriendWeekAllowsTavernBrawlRecordUpdate;

		// Token: 0x040002B8 RID: 696
		public bool HasFsgShowBetaLabel;

		// Token: 0x040002B9 RID: 697
		private bool _FsgShowBetaLabel;

		// Token: 0x040002BA RID: 698
		public bool HasFsgFriendListPatronCountLimit;

		// Token: 0x040002BB RID: 699
		private int _FsgFriendListPatronCountLimit;

		// Token: 0x040002BC RID: 700
		public bool HasArenaClosedToNewSessionsSeconds;

		// Token: 0x040002BD RID: 701
		private uint _ArenaClosedToNewSessionsSeconds;

		// Token: 0x040002BE RID: 702
		public bool HasFsgLoginScanEnabled;

		// Token: 0x040002BF RID: 703
		private bool _FsgLoginScanEnabled;

		// Token: 0x040002C0 RID: 704
		public bool HasFsgMaxPresencePubscribedPatronCount;

		// Token: 0x040002C1 RID: 705
		private int _FsgMaxPresencePubscribedPatronCount;

		// Token: 0x040002C2 RID: 706
		public bool HasQuickPackOpeningAllowed;

		// Token: 0x040002C3 RID: 707
		private bool _QuickPackOpeningAllowed;

		// Token: 0x040002C4 RID: 708
		public bool HasAllowIosHighres;

		// Token: 0x040002C5 RID: 709
		private bool _AllowIosHighres;

		// Token: 0x040002C6 RID: 710
		public bool HasSimpleCheckout;

		// Token: 0x040002C7 RID: 711
		private bool _SimpleCheckout;

		// Token: 0x040002C8 RID: 712
		public bool HasDeckCompletionGetPlayerCollectionFromClient;

		// Token: 0x040002C9 RID: 713
		private bool _DeckCompletionGetPlayerCollectionFromClient;

		// Token: 0x040002CA RID: 714
		public bool HasSoftAccountPurchasing;

		// Token: 0x040002CB RID: 715
		private bool _SoftAccountPurchasing;

		// Token: 0x040002CC RID: 716
		public bool HasEnableSmartDeckCompletion;

		// Token: 0x040002CD RID: 717
		private bool _EnableSmartDeckCompletion;

		// Token: 0x040002CE RID: 718
		public bool HasNumClassicPacksUntilDeprioritize;

		// Token: 0x040002CF RID: 719
		private int _NumClassicPacksUntilDeprioritize;

		// Token: 0x040002D0 RID: 720
		public bool HasAllowOfflineClientActivityIos;

		// Token: 0x040002D1 RID: 721
		private bool _AllowOfflineClientActivityIos;

		// Token: 0x040002D2 RID: 722
		public bool HasAllowOfflineClientActivityAndroid;

		// Token: 0x040002D3 RID: 723
		private bool _AllowOfflineClientActivityAndroid;

		// Token: 0x040002D4 RID: 724
		public bool HasAllowOfflineClientActivityDesktop;

		// Token: 0x040002D5 RID: 725
		private bool _AllowOfflineClientActivityDesktop;

		// Token: 0x040002D6 RID: 726
		public bool HasAllowOfflineClientDeckDeletion;

		// Token: 0x040002D7 RID: 727
		private bool _AllowOfflineClientDeckDeletion;

		// Token: 0x040002D8 RID: 728
		public bool HasBattlegrounds;

		// Token: 0x040002D9 RID: 729
		private bool _Battlegrounds;

		// Token: 0x040002DA RID: 730
		public bool HasBattlegroundsFriendlyChallenge;

		// Token: 0x040002DB RID: 731
		private bool _BattlegroundsFriendlyChallenge;

		// Token: 0x040002DC RID: 732
		public bool HasSimpleCheckoutIos;

		// Token: 0x040002DD RID: 733
		private bool _SimpleCheckoutIos;

		// Token: 0x040002DE RID: 734
		public bool HasSimpleCheckoutAndroidAmazon;

		// Token: 0x040002DF RID: 735
		private bool _SimpleCheckoutAndroidAmazon;

		// Token: 0x040002E0 RID: 736
		public bool HasSimpleCheckoutAndroidGoogle;

		// Token: 0x040002E1 RID: 737
		private bool _SimpleCheckoutAndroidGoogle;

		// Token: 0x040002E2 RID: 738
		public bool HasSimpleCheckoutAndroidGlobal;

		// Token: 0x040002E3 RID: 739
		private bool _SimpleCheckoutAndroidGlobal;

		// Token: 0x040002E4 RID: 740
		public bool HasSimpleCheckoutWin;

		// Token: 0x040002E5 RID: 741
		private bool _SimpleCheckoutWin;

		// Token: 0x040002E6 RID: 742
		public bool HasSimpleCheckoutMac;

		// Token: 0x040002E7 RID: 743
		private bool _SimpleCheckoutMac;

		// Token: 0x040002E8 RID: 744
		public bool HasBattlegroundsEarlyAccessLicense;

		// Token: 0x040002E9 RID: 745
		private int _BattlegroundsEarlyAccessLicense;

		// Token: 0x040002EA RID: 746
		public bool HasVirtualCurrencyEnabled;

		// Token: 0x040002EB RID: 747
		private bool _VirtualCurrencyEnabled;

		// Token: 0x040002EC RID: 748
		public bool HasBattlegroundsTutorial;

		// Token: 0x040002ED RID: 749
		private bool _BattlegroundsTutorial;

		// Token: 0x040002EE RID: 750
		public bool HasVintageStoreEnabled;

		// Token: 0x040002EF RID: 751
		private bool _VintageStoreEnabled;

		// Token: 0x040002F0 RID: 752
		public bool HasBoosterRotatingSoonWarnDaysWithoutSale;

		// Token: 0x040002F1 RID: 753
		private int _BoosterRotatingSoonWarnDaysWithoutSale;

		// Token: 0x040002F2 RID: 754
		public bool HasBoosterRotatingSoonWarnDaysWithSale;

		// Token: 0x040002F3 RID: 755
		private int _BoosterRotatingSoonWarnDaysWithSale;

		// Token: 0x040002F4 RID: 756
		public bool HasBattlegroundsMaxRankedPartySize;

		// Token: 0x040002F5 RID: 757
		private int _BattlegroundsMaxRankedPartySize;

		// Token: 0x040002F6 RID: 758
		public bool HasDeckReordering;

		// Token: 0x040002F7 RID: 759
		private bool _DeckReordering;

		// Token: 0x040002F8 RID: 760
		public bool HasProgressionEnabled;

		// Token: 0x040002F9 RID: 761
		private bool _ProgressionEnabled;

		// Token: 0x040002FA RID: 762
		public bool HasPvpdrClosedToNewSessionsSeconds;

		// Token: 0x040002FB RID: 763
		private uint _PvpdrClosedToNewSessionsSeconds;

		// Token: 0x040002FC RID: 764
		public bool HasDuels;

		// Token: 0x040002FD RID: 765
		private bool _Duels;

		// Token: 0x040002FE RID: 766
		public bool HasDuelsEarlyAccessLicense;

		// Token: 0x040002FF RID: 767
		private uint _DuelsEarlyAccessLicense;

		// Token: 0x04000300 RID: 768
		public bool HasPaidDuels;

		// Token: 0x04000301 RID: 769
		private bool _PaidDuels;

		// Token: 0x04000302 RID: 770
		public bool HasAllowLiveFpsGathering;

		// Token: 0x04000303 RID: 771
		private bool _AllowLiveFpsGathering;

		// Token: 0x04000304 RID: 772
		public bool HasJournalButtonDisabled;

		// Token: 0x04000305 RID: 773
		private bool _JournalButtonDisabled;

		// Token: 0x04000306 RID: 774
		public bool HasAchievementToastDisabled;

		// Token: 0x04000307 RID: 775
		private bool _AchievementToastDisabled;

		// Token: 0x04000308 RID: 776
		public bool HasCheckForNewQuestsIntervalJitterSecs;

		// Token: 0x04000309 RID: 777
		private float _CheckForNewQuestsIntervalJitterSecs;

		// Token: 0x0400030A RID: 778
		public bool HasRankedStandard;

		// Token: 0x0400030B RID: 779
		private bool _RankedStandard;

		// Token: 0x0400030C RID: 780
		public bool HasRankedWild;

		// Token: 0x0400030D RID: 781
		private bool _RankedWild;

		// Token: 0x0400030E RID: 782
		public bool HasRankedClassic;

		// Token: 0x0400030F RID: 783
		private bool _RankedClassic;

		// Token: 0x04000310 RID: 784
		public bool HasRankedNewPlayer;

		// Token: 0x04000311 RID: 785
		private bool _RankedNewPlayer;

		// Token: 0x04000312 RID: 786
		public bool HasContentstackEnabled;

		// Token: 0x04000313 RID: 787
		private bool _ContentstackEnabled;

		// Token: 0x04000314 RID: 788
		public bool HasEndOfTurnToastPauseBufferSecs;

		// Token: 0x04000315 RID: 789
		private float _EndOfTurnToastPauseBufferSecs;

		// Token: 0x04000316 RID: 790
		public bool HasAppRatingEnabled;

		// Token: 0x04000317 RID: 791
		private bool _AppRatingEnabled;

		// Token: 0x04000318 RID: 792
		public bool HasAppRatingSamplingPercentage;

		// Token: 0x04000319 RID: 793
		private float _AppRatingSamplingPercentage;

		// Token: 0x0400031A RID: 794
		public bool HasBuyCardBacksFromCollectionManagerEnabled;

		// Token: 0x0400031B RID: 795
		private bool _BuyCardBacksFromCollectionManagerEnabled;

		// Token: 0x0400031C RID: 796
		public bool HasBuyHeroSkinsFromCollectionManagerEnabled;

		// Token: 0x0400031D RID: 797
		private bool _BuyHeroSkinsFromCollectionManagerEnabled;

		// Token: 0x0200059A RID: 1434
		public enum PacketID
		{
			// Token: 0x04001F28 RID: 7976
			ID = 264
		}
	}
}
