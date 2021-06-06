using System;
using System.Collections.Generic;
using System.Linq;
using Assets;
using PegasusClient;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x0200064F RID: 1615
public class RankMgr
{
	// Token: 0x06005B0E RID: 23310 RVA: 0x001DB073 File Offset: 0x001D9273
	public static RankMgr Get()
	{
		if (RankMgr.s_instance == null)
		{
			RankMgr.s_instance = new RankMgr();
		}
		return RankMgr.s_instance;
	}

	// Token: 0x06005B0F RID: 23311 RVA: 0x001DB08C File Offset: 0x001D928C
	public void PostProcessDbfLoad_League()
	{
		this.m_maxStarLevelByLeagueId = new Map<int, int>();
		this.m_maxChestVisualIndex = 0;
		this.m_rankConfigByLeagueAndStarLevel = new Map<int, Map<int, LeagueRankDbfRecord>>();
		foreach (LeagueRankDbfRecord leagueRankDbfRecord in GameDbf.LeagueRank.GetRecords())
		{
			int num;
			if (!this.m_maxStarLevelByLeagueId.TryGetValue(leagueRankDbfRecord.LeagueId, out num))
			{
				this.m_maxStarLevelByLeagueId.Add(leagueRankDbfRecord.LeagueId, leagueRankDbfRecord.StarLevel);
			}
			else if (leagueRankDbfRecord.StarLevel > num)
			{
				this.m_maxStarLevelByLeagueId[leagueRankDbfRecord.LeagueId] = leagueRankDbfRecord.StarLevel;
			}
			if (leagueRankDbfRecord.RewardChestVisualIndex > this.m_maxChestVisualIndex)
			{
				this.m_maxChestVisualIndex = leagueRankDbfRecord.RewardChestVisualIndex;
			}
			Map<int, LeagueRankDbfRecord> map;
			if (!this.m_rankConfigByLeagueAndStarLevel.TryGetValue(leagueRankDbfRecord.LeagueId, out map) || map == null)
			{
				map = (this.m_rankConfigByLeagueAndStarLevel[leagueRankDbfRecord.LeagueId] = new Map<int, LeagueRankDbfRecord>());
			}
			map[leagueRankDbfRecord.StarLevel] = leagueRankDbfRecord;
		}
	}

	// Token: 0x06005B10 RID: 23312 RVA: 0x001DB1A4 File Offset: 0x001D93A4
	public bool UseLegacyRankedPlay()
	{
		LeagueDbfRecord localPlayerStandardLeagueConfig = this.GetLocalPlayerStandardLeagueConfig();
		return localPlayerStandardLeagueConfig != null && this.UseLegacyRankedPlay(localPlayerStandardLeagueConfig.ID);
	}

	// Token: 0x06005B11 RID: 23313 RVA: 0x001DB1CC File Offset: 0x001D93CC
	public bool UseLegacyRankedPlay(int leagueId)
	{
		LeagueDbfRecord leagueRecord = this.GetLeagueRecord(leagueId);
		if (leagueRecord != null)
		{
			League.LeagueType leagueType = leagueRecord.LeagueType;
			if (leagueType == League.LeagueType.NORMAL)
			{
				return leagueRecord.LeagueVersion <= 2;
			}
			if (leagueType == League.LeagueType.NEW_PLAYER)
			{
				return leagueRecord.LeagueVersion <= 2;
			}
		}
		return false;
	}

	// Token: 0x17000554 RID: 1364
	// (get) Token: 0x06005B12 RID: 23314 RVA: 0x001DB210 File Offset: 0x001D9410
	// (set) Token: 0x06005B13 RID: 23315 RVA: 0x001DB218 File Offset: 0x001D9418
	public bool DidPromoteSelfThisSession { get; set; }

	// Token: 0x17000555 RID: 1365
	// (get) Token: 0x06005B14 RID: 23316 RVA: 0x001DB221 File Offset: 0x001D9421
	public bool HasLocalPlayerMedalInfo
	{
		get
		{
			return NetCache.Get().GetNetObject<NetCache.NetCacheMedalInfo>() != null;
		}
	}

	// Token: 0x06005B15 RID: 23317 RVA: 0x001DB230 File Offset: 0x001D9430
	public MedalInfoTranslator GetLocalPlayerMedalInfo()
	{
		NetCache.NetCacheMedalInfo netObject = NetCache.Get().GetNetObject<NetCache.NetCacheMedalInfo>();
		if (netObject != null)
		{
			return new MedalInfoTranslator(netObject, netObject.PreviousMedalInfo);
		}
		Log.All.PrintError("NetCacheMedalInfo not yet available!", Array.Empty<object>());
		return new MedalInfoTranslator();
	}

	// Token: 0x06005B16 RID: 23318 RVA: 0x001DB271 File Offset: 0x001D9471
	public bool WildCardsAllowedInCurrentLeague()
	{
		return !this.GetLocalPlayerStandardLeagueConfig().LockWildCards;
	}

	// Token: 0x06005B17 RID: 23319 RVA: 0x001DB281 File Offset: 0x001D9481
	public bool ShouldAccountSeeCasualMode(FormatType format)
	{
		return this.GetBnetGameTypeForLeague(false, format) > BnetGameType.BGT_UNKNOWN;
	}

	// Token: 0x06005B18 RID: 23320 RVA: 0x001DB28E File Offset: 0x001D948E
	public bool CanPromoteSelfManually()
	{
		return this.GetLocalPlayerStandardLeagueConfig().CanPromoteSelfManually;
	}

	// Token: 0x06005B19 RID: 23321 RVA: 0x001DB29C File Offset: 0x001D949C
	public BnetGameType GetBnetGameTypeForLeague(bool inRankedMode, FormatType format)
	{
		LeagueDbfRecord localPlayerLeagueConfig = this.GetLocalPlayerLeagueConfig(format);
		List<LeagueGameType.BnetGameType> gameTypesToExclude = new List<LeagueGameType.BnetGameType>();
		if (inRankedMode)
		{
			gameTypesToExclude.AddRange(new LeagueGameType.BnetGameType[]
			{
				LeagueGameType.BnetGameType.BGT_CASUAL_STANDARD,
				LeagueGameType.BnetGameType.BGT_CASUAL_WILD,
				LeagueGameType.BnetGameType.BGT_CASUAL_CLASSIC
			});
		}
		else
		{
			gameTypesToExclude.AddRange(new LeagueGameType.BnetGameType[]
			{
				LeagueGameType.BnetGameType.BGT_RANKED_STANDARD,
				LeagueGameType.BnetGameType.BGT_RANKED_WILD,
				LeagueGameType.BnetGameType.BGT_RANKED_CLASSIC
			});
		}
		LeagueGameTypeDbfRecord leagueGameTypeDbfRecord = (from x in localPlayerLeagueConfig.LeagueGameType
		where x.FormatType == (LeagueGameType.FormatType)format && !gameTypesToExclude.Contains(x.BnetGameType)
		select x).FirstOrDefault<LeagueGameTypeDbfRecord>();
		if (leagueGameTypeDbfRecord == null)
		{
			return BnetGameType.BGT_UNKNOWN;
		}
		return (BnetGameType)leagueGameTypeDbfRecord.BnetGameType;
	}

	// Token: 0x06005B1A RID: 23322 RVA: 0x001DB333 File Offset: 0x001D9533
	public bool IsFormatAllowedInLeague(FormatType format)
	{
		return this.GetBnetGameTypeForLeague(false, format) != BnetGameType.BGT_UNKNOWN || this.GetBnetGameTypeForLeague(true, format) > BnetGameType.BGT_UNKNOWN;
	}

	// Token: 0x17000556 RID: 1366
	// (get) Token: 0x06005B1B RID: 23323 RVA: 0x001DB34C File Offset: 0x001D954C
	public bool IsLegendRankInAnyFormat
	{
		get
		{
			foreach (object obj in Enum.GetValues(typeof(FormatType)))
			{
				FormatType formatType = (FormatType)obj;
				if (formatType != FormatType.FT_UNKNOWN && this.IsLegendRank(formatType))
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x06005B1C RID: 23324 RVA: 0x001DB3BC File Offset: 0x001D95BC
	public bool IsLegendRank(FormatType formatType)
	{
		return this.GetLocalPlayerMedalInfo().GetCurrentMedal(formatType).IsLegendRank();
	}

	// Token: 0x06005B1D RID: 23325 RVA: 0x001DB3CF File Offset: 0x001D95CF
	public bool IsNewPlayer()
	{
		return Network.ShouldBeConnectedToAurora() && NetCache.Get().GetNetObject<NetCache.NetCacheMedalInfo>() != null && RankMgr.Get().GetLocalPlayerStandardLeagueConfig().LeagueType == League.LeagueType.NEW_PLAYER;
	}

	// Token: 0x06005B1E RID: 23326 RVA: 0x001DB3FC File Offset: 0x001D95FC
	public bool SetRankPresenceField(NetCache.NetCacheMedalInfo medalInfo)
	{
		GamePresenceRank gamePresenceRank = new GamePresenceRank();
		foreach (object obj in Enum.GetValues(typeof(FormatType)))
		{
			FormatType formatType = (FormatType)obj;
			if (formatType != FormatType.FT_UNKNOWN)
			{
				MedalInfoData medalInfoData = medalInfo.GetMedalInfoData(formatType);
				if (medalInfoData != null)
				{
					GamePresenceRankData item = new GamePresenceRankData
					{
						FormatType = formatType,
						LeagueId = medalInfoData.LeagueId,
						StarLevel = medalInfoData.StarLevel,
						LegendRank = medalInfoData.LegendRank
					};
					gamePresenceRank.Values.Add(item);
				}
				else
				{
					Debug.LogError("RankMgr.SetRankPresenceField failed to get medal info data for " + formatType.ToString() + ". The format will not be added to gamePresenceRank");
				}
			}
		}
		return BnetPresenceMgr.Get().SetGameFieldBlob(18U, gamePresenceRank);
	}

	// Token: 0x06005B1F RID: 23327 RVA: 0x001DB4E0 File Offset: 0x001D96E0
	public MedalInfoTranslator GetRankPresenceField(BnetPlayer player)
	{
		if (player == null)
		{
			return null;
		}
		return this.GetRankPresenceField(player.GetHearthstoneGameAccount());
	}

	// Token: 0x06005B20 RID: 23328 RVA: 0x001DB4F4 File Offset: 0x001D96F4
	public MedalInfoTranslator GetRankPresenceField(BnetGameAccount gameAccount)
	{
		byte[] bytes;
		if (gameAccount != null && gameAccount.TryGetGameFieldBytes(18U, out bytes))
		{
			try
			{
				return MedalInfoTranslator.CreateMedalInfoForGamePresenceRank(ProtobufUtil.ParseFrom<GamePresenceRank>(bytes, 0, -1));
			}
			catch (Exception ex)
			{
				Log.Presence.PrintInfo(ex.ToString(), Array.Empty<object>());
			}
		}
		return new MedalInfoTranslator();
	}

	// Token: 0x06005B21 RID: 23329 RVA: 0x001DB558 File Offset: 0x001D9758
	public LeagueDbfRecord GetLeagueRecord(int leagueId)
	{
		LeagueDbfRecord leagueDbfRecord = GameDbf.League.GetRecords((LeagueDbfRecord r) => r.ID == leagueId, -1).FirstOrDefault<LeagueDbfRecord>();
		if (leagueDbfRecord == null)
		{
			Log.All.PrintError("No record for leagueId={0}", new object[]
			{
				leagueId
			});
			return new LeagueDbfRecord();
		}
		return leagueDbfRecord;
	}

	// Token: 0x06005B22 RID: 23330 RVA: 0x001DB5BC File Offset: 0x001D97BC
	public LeagueRankDbfRecord GetLeagueRankRecord(int leagueId, int starLevel)
	{
		Map<int, LeagueRankDbfRecord> map;
		LeagueRankDbfRecord leagueRankDbfRecord;
		if (this.m_rankConfigByLeagueAndStarLevel.TryGetValue(leagueId, out map) && map != null && map.TryGetValue(starLevel, out leagueRankDbfRecord) && leagueRankDbfRecord != null)
		{
			return leagueRankDbfRecord;
		}
		Log.All.PrintError("No record for leagueId={0} starLevel={1}", new object[]
		{
			leagueId,
			starLevel
		});
		return new LeagueRankDbfRecord();
	}

	// Token: 0x06005B23 RID: 23331 RVA: 0x001DB618 File Offset: 0x001D9818
	public LeagueDbfRecord GetLeagueRecordForType(League.LeagueType leagueType, int seasonId)
	{
		LeagueDbfRecord leagueDbfRecord = null;
		int num = 0;
		foreach (LeagueDbfRecord leagueDbfRecord2 in GameDbf.League.GetRecords())
		{
			if (leagueDbfRecord2.LeagueType == leagueType && leagueDbfRecord2.InitialSeasonId <= seasonId && leagueDbfRecord2.LeagueVersion > num)
			{
				num = leagueDbfRecord2.LeagueVersion;
				leagueDbfRecord = leagueDbfRecord2;
			}
		}
		if (leagueDbfRecord == null)
		{
			Log.All.PrintError("No record for leagueType={0}", new object[]
			{
				leagueType
			});
		}
		return leagueDbfRecord;
	}

	// Token: 0x06005B24 RID: 23332 RVA: 0x001DB6B4 File Offset: 0x001D98B4
	public LeagueRankDbfRecord GetLeagueRankRecordByCheatName(string cheatName)
	{
		LeagueRankDbfRecord leagueRankDbfRecord = null;
		int num = 0;
		foreach (LeagueRankDbfRecord leagueRankDbfRecord2 in GameDbf.LeagueRank.GetRecords())
		{
			if (leagueRankDbfRecord2.CheatName == cheatName)
			{
				LeagueDbfRecord leagueRecord = this.GetLeagueRecord(leagueRankDbfRecord2.LeagueId);
				if (leagueRecord.LeagueVersion > num)
				{
					num = leagueRecord.LeagueVersion;
					leagueRankDbfRecord = leagueRankDbfRecord2;
				}
			}
		}
		if (leagueRankDbfRecord == null)
		{
			Log.All.PrintError("No record for cheatName={0}", new object[]
			{
				cheatName
			});
		}
		return leagueRankDbfRecord;
	}

	// Token: 0x06005B25 RID: 23333 RVA: 0x001DB758 File Offset: 0x001D9958
	public LeagueDbfRecord GetLocalPlayerStandardLeagueConfig()
	{
		return this.GetLocalPlayerLeagueConfig(FormatType.FT_STANDARD);
	}

	// Token: 0x06005B26 RID: 23334 RVA: 0x001DB761 File Offset: 0x001D9961
	public LeagueDbfRecord GetLocalPlayerLeagueConfig(FormatType format)
	{
		return this.GetLocalPlayerMedalInfo().GetCurrentMedal(format).LeagueConfig;
	}

	// Token: 0x06005B27 RID: 23335 RVA: 0x001DB774 File Offset: 0x001D9974
	public int GetMaxStarLevel(int leagueId)
	{
		int result;
		if (!this.m_maxStarLevelByLeagueId.TryGetValue(leagueId, out result))
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x06005B28 RID: 23336 RVA: 0x001DB794 File Offset: 0x001D9994
	public int GetMaxRewardChestVisualIndex()
	{
		return this.m_maxChestVisualIndex;
	}

	// Token: 0x06005B29 RID: 23337 RVA: 0x001DB79C File Offset: 0x001D999C
	public bool IsCardLockedInCurrentLeague(EntityDef entityDef)
	{
		LeagueDbfRecord localPlayerStandardLeagueConfig = this.GetLocalPlayerStandardLeagueConfig();
		return localPlayerStandardLeagueConfig != null && ((localPlayerStandardLeagueConfig.LockWildCards && GameUtils.IsWildCard(entityDef)) || this.IsCardBannedInCurrentLeague(entityDef));
	}

	// Token: 0x06005B2A RID: 23338 RVA: 0x001DB7D4 File Offset: 0x001D99D4
	public HashSet<string> GetBannedCardsInCurrentLeague()
	{
		int lockCardsFromSubsetId = this.GetLocalPlayerStandardLeagueConfig().LockCardsFromSubsetId;
		return GameDbf.GetIndex().GetSubsetById(lockCardsFromSubsetId);
	}

	// Token: 0x06005B2B RID: 23339 RVA: 0x001DB7F8 File Offset: 0x001D99F8
	public bool IsCardBannedInCurrentLeague(EntityDef entityDef)
	{
		return this.GetBannedCardsInCurrentLeague().Contains(entityDef.GetCardId());
	}

	// Token: 0x06005B2C RID: 23340 RVA: 0x001DB810 File Offset: 0x001D9A10
	public int GetRankedRewardBoosterIdForSeasonId(int seasonId)
	{
		List<BoosterDbfRecord> records = GameDbf.Booster.GetRecords((BoosterDbfRecord r) => r.RankedRewardInitialSeason > 0, -1);
		records.Sort((BoosterDbfRecord a, BoosterDbfRecord b) => b.RankedRewardInitialSeason - a.RankedRewardInitialSeason);
		foreach (BoosterDbfRecord boosterDbfRecord in records)
		{
			if (seasonId >= boosterDbfRecord.RankedRewardInitialSeason)
			{
				return boosterDbfRecord.ID;
			}
		}
		return 1;
	}

	// Token: 0x06005B2D RID: 23341 RVA: 0x001DB8BC File Offset: 0x001D9ABC
	public int GetRankedCardBackIdForSeasonId(int seasonId)
	{
		int result = 0;
		foreach (CardBackDbfRecord cardBackDbfRecord in GameDbf.CardBack.GetRecords())
		{
			if (EnumUtils.GetEnum<CardBackData.CardBackSource>(cardBackDbfRecord.Source) == CardBackData.CardBackSource.SEASON && cardBackDbfRecord.Data1 == (long)seasonId)
			{
				result = cardBackDbfRecord.ID;
				break;
			}
		}
		return result;
	}

	// Token: 0x04004DCB RID: 19915
	private static RankMgr s_instance;

	// Token: 0x04004DCC RID: 19916
	private Map<int, int> m_maxStarLevelByLeagueId;

	// Token: 0x04004DCD RID: 19917
	private int m_maxChestVisualIndex;

	// Token: 0x04004DCE RID: 19918
	private Map<int, Map<int, LeagueRankDbfRecord>> m_rankConfigByLeagueAndStarLevel;

	// Token: 0x04004DCF RID: 19919
	public const int MIN_STAR_LEVEL = 1;

	// Token: 0x04004DD0 RID: 19920
	public static readonly AssetReference RANK_CHANGE_TWO_SCOOP_PREFAB_LEGACY = new AssetReference("RankChangeTwoScoop_LEGACY.prefab:c10232b70104d6e42b2dd9e6e1233495");

	// Token: 0x04004DD1 RID: 19921
	public static readonly AssetReference RANK_CHANGE_TWO_SCOOP_PREFAB_NEW = new AssetReference("RankChangeTwoScoop_NEW.prefab:606c949d2ac1a8643a5ab70f4d8f67e6");

	// Token: 0x04004DD2 RID: 19922
	public static readonly AssetReference RANKED_REWARD_DISPLAY_PREFAB = new AssetReference("RankedRewardDisplay.prefab:f95c6e7ec80adde4aa6c2f6df24262ea");

	// Token: 0x04004DD3 RID: 19923
	public static readonly AssetReference RANKED_CARDBACK_PROGRESS_DISPLAY_PREFAB = new AssetReference("RankedCardBackProgressDisplay.prefab:b7a7de3cdf473fe4784b100111f02cbb");

	// Token: 0x04004DD4 RID: 19924
	public static readonly AssetReference RANKED_REWARD_LIST_POPUP = new AssetReference("RankedRewardListPopup.prefab:6ee69b3ca628c0047b9016ffda861c5c");

	// Token: 0x04004DD5 RID: 19925
	public static readonly AssetReference BONUS_STAR_POPUP_PREFAB = new AssetReference("RankedBonusStarsPopUp.prefab:d3e043ebff5163846a986cb55a69760c");

	// Token: 0x04004DD6 RID: 19926
	public static readonly AssetReference RANKED_INTRO_POPUP_PREFAB = new AssetReference("RankedIntroPopUp.prefab:b0edfa4af7328bc4d92b637af2f1c32d");

	// Token: 0x04004DD7 RID: 19927
	private const int LEGACY_NORMAL_LEAGUE_VERSION = 2;

	// Token: 0x04004DD8 RID: 19928
	private const int LEGACY_NEW_PLAYER_LEAGUE_VERSION = 2;
}
