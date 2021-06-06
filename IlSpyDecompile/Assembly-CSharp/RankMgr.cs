using System;
using System.Collections.Generic;
using System.Linq;
using Assets;
using PegasusClient;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

public class RankMgr
{
	private static RankMgr s_instance;

	private Map<int, int> m_maxStarLevelByLeagueId;

	private int m_maxChestVisualIndex;

	private Map<int, Map<int, LeagueRankDbfRecord>> m_rankConfigByLeagueAndStarLevel;

	public const int MIN_STAR_LEVEL = 1;

	public static readonly AssetReference RANK_CHANGE_TWO_SCOOP_PREFAB_LEGACY = new AssetReference("RankChangeTwoScoop_LEGACY.prefab:c10232b70104d6e42b2dd9e6e1233495");

	public static readonly AssetReference RANK_CHANGE_TWO_SCOOP_PREFAB_NEW = new AssetReference("RankChangeTwoScoop_NEW.prefab:606c949d2ac1a8643a5ab70f4d8f67e6");

	public static readonly AssetReference RANKED_REWARD_DISPLAY_PREFAB = new AssetReference("RankedRewardDisplay.prefab:f95c6e7ec80adde4aa6c2f6df24262ea");

	public static readonly AssetReference RANKED_CARDBACK_PROGRESS_DISPLAY_PREFAB = new AssetReference("RankedCardBackProgressDisplay.prefab:b7a7de3cdf473fe4784b100111f02cbb");

	public static readonly AssetReference RANKED_REWARD_LIST_POPUP = new AssetReference("RankedRewardListPopup.prefab:6ee69b3ca628c0047b9016ffda861c5c");

	public static readonly AssetReference BONUS_STAR_POPUP_PREFAB = new AssetReference("RankedBonusStarsPopUp.prefab:d3e043ebff5163846a986cb55a69760c");

	public static readonly AssetReference RANKED_INTRO_POPUP_PREFAB = new AssetReference("RankedIntroPopUp.prefab:b0edfa4af7328bc4d92b637af2f1c32d");

	private const int LEGACY_NORMAL_LEAGUE_VERSION = 2;

	private const int LEGACY_NEW_PLAYER_LEAGUE_VERSION = 2;

	public bool DidPromoteSelfThisSession { get; set; }

	public bool HasLocalPlayerMedalInfo => NetCache.Get().GetNetObject<NetCache.NetCacheMedalInfo>() != null;

	public bool IsLegendRankInAnyFormat
	{
		get
		{
			foreach (FormatType value in Enum.GetValues(typeof(FormatType)))
			{
				if (value != 0 && IsLegendRank(value))
				{
					return true;
				}
			}
			return false;
		}
	}

	public static RankMgr Get()
	{
		if (s_instance == null)
		{
			s_instance = new RankMgr();
		}
		return s_instance;
	}

	public void PostProcessDbfLoad_League()
	{
		m_maxStarLevelByLeagueId = new Map<int, int>();
		m_maxChestVisualIndex = 0;
		m_rankConfigByLeagueAndStarLevel = new Map<int, Map<int, LeagueRankDbfRecord>>();
		foreach (LeagueRankDbfRecord record in GameDbf.LeagueRank.GetRecords())
		{
			if (!m_maxStarLevelByLeagueId.TryGetValue(record.LeagueId, out var value))
			{
				m_maxStarLevelByLeagueId.Add(record.LeagueId, record.StarLevel);
			}
			else if (record.StarLevel > value)
			{
				m_maxStarLevelByLeagueId[record.LeagueId] = record.StarLevel;
			}
			if (record.RewardChestVisualIndex > m_maxChestVisualIndex)
			{
				m_maxChestVisualIndex = record.RewardChestVisualIndex;
			}
			if (!m_rankConfigByLeagueAndStarLevel.TryGetValue(record.LeagueId, out var value2) || value2 == null)
			{
				value2 = (m_rankConfigByLeagueAndStarLevel[record.LeagueId] = new Map<int, LeagueRankDbfRecord>());
			}
			value2[record.StarLevel] = record;
		}
	}

	public bool UseLegacyRankedPlay()
	{
		LeagueDbfRecord localPlayerStandardLeagueConfig = GetLocalPlayerStandardLeagueConfig();
		if (localPlayerStandardLeagueConfig == null)
		{
			return false;
		}
		return UseLegacyRankedPlay(localPlayerStandardLeagueConfig.ID);
	}

	public bool UseLegacyRankedPlay(int leagueId)
	{
		LeagueDbfRecord leagueRecord = GetLeagueRecord(leagueId);
		if (leagueRecord != null)
		{
			switch (leagueRecord.LeagueType)
			{
			case League.LeagueType.NORMAL:
				return leagueRecord.LeagueVersion <= 2;
			case League.LeagueType.NEW_PLAYER:
				return leagueRecord.LeagueVersion <= 2;
			}
		}
		return false;
	}

	public MedalInfoTranslator GetLocalPlayerMedalInfo()
	{
		NetCache.NetCacheMedalInfo netObject = NetCache.Get().GetNetObject<NetCache.NetCacheMedalInfo>();
		if (netObject != null)
		{
			return new MedalInfoTranslator(netObject, netObject.PreviousMedalInfo);
		}
		Log.All.PrintError("NetCacheMedalInfo not yet available!");
		return new MedalInfoTranslator();
	}

	public bool WildCardsAllowedInCurrentLeague()
	{
		return !GetLocalPlayerStandardLeagueConfig().LockWildCards;
	}

	public bool ShouldAccountSeeCasualMode(FormatType format)
	{
		return GetBnetGameTypeForLeague(inRankedMode: false, format) != BnetGameType.BGT_UNKNOWN;
	}

	public bool CanPromoteSelfManually()
	{
		return GetLocalPlayerStandardLeagueConfig().CanPromoteSelfManually;
	}

	public BnetGameType GetBnetGameTypeForLeague(bool inRankedMode, FormatType format)
	{
		LeagueDbfRecord localPlayerLeagueConfig = GetLocalPlayerLeagueConfig(format);
		List<LeagueGameType.BnetGameType> gameTypesToExclude = new List<LeagueGameType.BnetGameType>();
		if (inRankedMode)
		{
			gameTypesToExclude.AddRange(new LeagueGameType.BnetGameType[3]
			{
				LeagueGameType.BnetGameType.BGT_CASUAL_STANDARD,
				LeagueGameType.BnetGameType.BGT_CASUAL_WILD,
				LeagueGameType.BnetGameType.BGT_CASUAL_CLASSIC
			});
		}
		else
		{
			gameTypesToExclude.AddRange(new LeagueGameType.BnetGameType[3]
			{
				LeagueGameType.BnetGameType.BGT_RANKED_STANDARD,
				LeagueGameType.BnetGameType.BGT_RANKED_WILD,
				LeagueGameType.BnetGameType.BGT_RANKED_CLASSIC
			});
		}
		return (BnetGameType)(localPlayerLeagueConfig.LeagueGameType.Where((LeagueGameTypeDbfRecord x) => x.FormatType == (LeagueGameType.FormatType)format && !gameTypesToExclude.Contains(x.BnetGameType)).FirstOrDefault()?.BnetGameType ?? LeagueGameType.BnetGameType.BGT_UNKNOWN);
	}

	public bool IsFormatAllowedInLeague(FormatType format)
	{
		if (GetBnetGameTypeForLeague(inRankedMode: false, format) == BnetGameType.BGT_UNKNOWN)
		{
			return GetBnetGameTypeForLeague(inRankedMode: true, format) != BnetGameType.BGT_UNKNOWN;
		}
		return true;
	}

	public bool IsLegendRank(FormatType formatType)
	{
		return GetLocalPlayerMedalInfo().GetCurrentMedal(formatType).IsLegendRank();
	}

	public bool IsNewPlayer()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return false;
		}
		if (NetCache.Get().GetNetObject<NetCache.NetCacheMedalInfo>() == null)
		{
			return false;
		}
		return Get().GetLocalPlayerStandardLeagueConfig().LeagueType == League.LeagueType.NEW_PLAYER;
	}

	public bool SetRankPresenceField(NetCache.NetCacheMedalInfo medalInfo)
	{
		GamePresenceRank gamePresenceRank = new GamePresenceRank();
		foreach (FormatType value in Enum.GetValues(typeof(FormatType)))
		{
			if (value != 0)
			{
				MedalInfoData medalInfoData = medalInfo.GetMedalInfoData(value);
				if (medalInfoData != null)
				{
					GamePresenceRankData item = new GamePresenceRankData
					{
						FormatType = value,
						LeagueId = medalInfoData.LeagueId,
						StarLevel = medalInfoData.StarLevel,
						LegendRank = medalInfoData.LegendRank
					};
					gamePresenceRank.Values.Add(item);
				}
				else
				{
					Debug.LogError("RankMgr.SetRankPresenceField failed to get medal info data for " + value.ToString() + ". The format will not be added to gamePresenceRank");
				}
			}
		}
		return BnetPresenceMgr.Get().SetGameFieldBlob(18u, gamePresenceRank);
	}

	public MedalInfoTranslator GetRankPresenceField(BnetPlayer player)
	{
		if (player == null)
		{
			return null;
		}
		return GetRankPresenceField(player.GetHearthstoneGameAccount());
	}

	public MedalInfoTranslator GetRankPresenceField(BnetGameAccount gameAccount)
	{
		if (gameAccount != null && gameAccount.TryGetGameFieldBytes(18u, out var val))
		{
			try
			{
				return MedalInfoTranslator.CreateMedalInfoForGamePresenceRank(ProtobufUtil.ParseFrom<GamePresenceRank>(val));
			}
			catch (Exception ex)
			{
				Log.Presence.PrintInfo(ex.ToString());
			}
		}
		return new MedalInfoTranslator();
	}

	public LeagueDbfRecord GetLeagueRecord(int leagueId)
	{
		LeagueDbfRecord leagueDbfRecord = GameDbf.League.GetRecords((LeagueDbfRecord r) => r.ID == leagueId).FirstOrDefault();
		if (leagueDbfRecord == null)
		{
			Log.All.PrintError("No record for leagueId={0}", leagueId);
			return new LeagueDbfRecord();
		}
		return leagueDbfRecord;
	}

	public LeagueRankDbfRecord GetLeagueRankRecord(int leagueId, int starLevel)
	{
		if (m_rankConfigByLeagueAndStarLevel.TryGetValue(leagueId, out var value) && value != null && value.TryGetValue(starLevel, out var value2) && value2 != null)
		{
			return value2;
		}
		Log.All.PrintError("No record for leagueId={0} starLevel={1}", leagueId, starLevel);
		return new LeagueRankDbfRecord();
	}

	public LeagueDbfRecord GetLeagueRecordForType(League.LeagueType leagueType, int seasonId)
	{
		LeagueDbfRecord leagueDbfRecord = null;
		int num = 0;
		foreach (LeagueDbfRecord record in GameDbf.League.GetRecords())
		{
			if (record.LeagueType == leagueType && record.InitialSeasonId <= seasonId && record.LeagueVersion > num)
			{
				num = record.LeagueVersion;
				leagueDbfRecord = record;
			}
		}
		if (leagueDbfRecord == null)
		{
			Log.All.PrintError("No record for leagueType={0}", leagueType);
		}
		return leagueDbfRecord;
	}

	public LeagueRankDbfRecord GetLeagueRankRecordByCheatName(string cheatName)
	{
		LeagueRankDbfRecord leagueRankDbfRecord = null;
		int num = 0;
		foreach (LeagueRankDbfRecord record in GameDbf.LeagueRank.GetRecords())
		{
			if (record.CheatName == cheatName)
			{
				LeagueDbfRecord leagueRecord = GetLeagueRecord(record.LeagueId);
				if (leagueRecord.LeagueVersion > num)
				{
					num = leagueRecord.LeagueVersion;
					leagueRankDbfRecord = record;
				}
			}
		}
		if (leagueRankDbfRecord == null)
		{
			Log.All.PrintError("No record for cheatName={0}", cheatName);
		}
		return leagueRankDbfRecord;
	}

	public LeagueDbfRecord GetLocalPlayerStandardLeagueConfig()
	{
		return GetLocalPlayerLeagueConfig(FormatType.FT_STANDARD);
	}

	public LeagueDbfRecord GetLocalPlayerLeagueConfig(FormatType format)
	{
		return GetLocalPlayerMedalInfo().GetCurrentMedal(format).LeagueConfig;
	}

	public int GetMaxStarLevel(int leagueId)
	{
		if (!m_maxStarLevelByLeagueId.TryGetValue(leagueId, out var value))
		{
			return 0;
		}
		return value;
	}

	public int GetMaxRewardChestVisualIndex()
	{
		return m_maxChestVisualIndex;
	}

	public bool IsCardLockedInCurrentLeague(EntityDef entityDef)
	{
		LeagueDbfRecord localPlayerStandardLeagueConfig = GetLocalPlayerStandardLeagueConfig();
		if (localPlayerStandardLeagueConfig == null)
		{
			return false;
		}
		if (localPlayerStandardLeagueConfig.LockWildCards && GameUtils.IsWildCard(entityDef))
		{
			return true;
		}
		if (IsCardBannedInCurrentLeague(entityDef))
		{
			return true;
		}
		return false;
	}

	public HashSet<string> GetBannedCardsInCurrentLeague()
	{
		int lockCardsFromSubsetId = GetLocalPlayerStandardLeagueConfig().LockCardsFromSubsetId;
		return GameDbf.GetIndex().GetSubsetById(lockCardsFromSubsetId);
	}

	public bool IsCardBannedInCurrentLeague(EntityDef entityDef)
	{
		if (GetBannedCardsInCurrentLeague().Contains(entityDef.GetCardId()))
		{
			return true;
		}
		return false;
	}

	public int GetRankedRewardBoosterIdForSeasonId(int seasonId)
	{
		List<BoosterDbfRecord> records = GameDbf.Booster.GetRecords((BoosterDbfRecord r) => r.RankedRewardInitialSeason > 0);
		records.Sort((BoosterDbfRecord a, BoosterDbfRecord b) => b.RankedRewardInitialSeason - a.RankedRewardInitialSeason);
		foreach (BoosterDbfRecord item in records)
		{
			if (seasonId >= item.RankedRewardInitialSeason)
			{
				return item.ID;
			}
		}
		return 1;
	}

	public int GetRankedCardBackIdForSeasonId(int seasonId)
	{
		int result = 0;
		foreach (CardBackDbfRecord record in GameDbf.CardBack.GetRecords())
		{
			if (EnumUtils.GetEnum<CardBackData.CardBackSource>(record.Source) == CardBackData.CardBackSource.SEASON && record.Data1 == seasonId)
			{
				return record.ID;
			}
		}
		return result;
	}
}
