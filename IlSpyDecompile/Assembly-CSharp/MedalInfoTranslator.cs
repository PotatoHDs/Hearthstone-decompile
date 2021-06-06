using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using PegasusClient;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

public class MedalInfoTranslator
{
	private Map<FormatType, TranslatedMedalInfo> m_currMedalInfo = new Map<FormatType, TranslatedMedalInfo>();

	private Map<FormatType, TranslatedMedalInfo> m_prevMedalInfo = new Map<FormatType, TranslatedMedalInfo>();

	public int TotalRankedWins => m_currMedalInfo.Sum((KeyValuePair<FormatType, TranslatedMedalInfo> x) => x.Value.seasonWins);

	public int TotalRankedWinsPrevious => m_prevMedalInfo.Sum((KeyValuePair<FormatType, TranslatedMedalInfo> x) => x.Value.seasonWins);

	public bool IsDisplayable()
	{
		return m_currMedalInfo.Any((KeyValuePair<FormatType, TranslatedMedalInfo> x) => x.Value.starLevel >= 1 && GameDbf.League.HasRecord(x.Value.leagueId));
	}

	public MedalInfoTranslator()
	{
		foreach (FormatType value in Enum.GetValues(typeof(FormatType)))
		{
			if (value != 0)
			{
				m_currMedalInfo.Add(value, CreateTranslatedMedalInfo(value, 0, 0, 0));
				m_prevMedalInfo.Add(value, CreateTranslatedMedalInfo(value, 0, 0, 0));
			}
		}
	}

	public static MedalInfoTranslator CreateMedalInfoForLeagueId(int leagueId, int starLevel, int legendIndex)
	{
		MedalInfoTranslator medalInfoTranslator = new MedalInfoTranslator();
		foreach (FormatType value in Enum.GetValues(typeof(FormatType)))
		{
			if (value != 0)
			{
				medalInfoTranslator.m_currMedalInfo[value] = CreateTranslatedMedalInfo(value, leagueId, starLevel, legendIndex);
				medalInfoTranslator.m_prevMedalInfo[value] = medalInfoTranslator.m_currMedalInfo[value].ShallowCopy();
			}
		}
		return medalInfoTranslator;
	}

	public static MedalInfoTranslator CreateMedalInfoForGamePresenceRank(GamePresenceRank gamePresenceRank)
	{
		MedalInfoTranslator medalInfoTranslator = new MedalInfoTranslator();
		foreach (FormatType formatType in Enum.GetValues(typeof(FormatType)))
		{
			if (formatType != 0)
			{
				GamePresenceRankData gamePresenceRankData = gamePresenceRank.Values.Where((GamePresenceRankData x) => x.FormatType == formatType).FirstOrDefault();
				if (gamePresenceRankData != null)
				{
					medalInfoTranslator.m_currMedalInfo[formatType] = CreateTranslatedMedalInfo(formatType, gamePresenceRankData.LeagueId, gamePresenceRankData.StarLevel, gamePresenceRankData.LegendRank);
				}
				else
				{
					medalInfoTranslator.m_currMedalInfo[formatType] = CreateTranslatedMedalInfo(formatType, 0, 0, 0);
				}
				medalInfoTranslator.m_prevMedalInfo[formatType] = medalInfoTranslator.m_currMedalInfo[formatType].ShallowCopy();
			}
		}
		return medalInfoTranslator;
	}

	public static TranslatedMedalInfo CreateTranslatedMedalInfo(FormatType format, int leagueId, int starLevel, int legendIndex)
	{
		return new TranslatedMedalInfo
		{
			format = format,
			leagueId = leagueId,
			starLevel = starLevel,
			legendIndex = legendIndex
		};
	}

	public MedalInfoTranslator(NetCache.NetCacheMedalInfo currMedalInfo, NetCache.NetCacheMedalInfo prevMedalInfo = null)
	{
		if (currMedalInfo == null)
		{
			return;
		}
		foreach (MedalInfoData allMedalInfoDatum in currMedalInfo.GetAllMedalInfoData())
		{
			m_currMedalInfo[allMedalInfoDatum.FormatType] = Translate(allMedalInfoDatum.FormatType, allMedalInfoDatum);
		}
		if (prevMedalInfo != null)
		{
			foreach (MedalInfoData allMedalInfoDatum2 in prevMedalInfo.GetAllMedalInfoData())
			{
				m_prevMedalInfo[allMedalInfoDatum2.FormatType] = Translate(allMedalInfoDatum2.FormatType, allMedalInfoDatum2);
			}
			return;
		}
		foreach (MedalInfoData allMedalInfoDatum3 in currMedalInfo.GetAllMedalInfoData())
		{
			m_prevMedalInfo[allMedalInfoDatum3.FormatType] = m_currMedalInfo[allMedalInfoDatum3.FormatType].ShallowCopy();
		}
	}

	private TranslatedMedalInfo Translate(FormatType format, MedalInfoData medalInfoData)
	{
		if (medalInfoData == null)
		{
			return CreateTranslatedMedalInfo(format, 0, 0, 0);
		}
		TranslatedMedalInfo translatedMedalInfo = CreateTranslatedMedalInfo(format, medalInfoData.LeagueId, medalInfoData.StarLevel, medalInfoData.HasLegendRank ? medalInfoData.LegendRank : 0);
		translatedMedalInfo.bestStarLevel = medalInfoData.BestStarLevel;
		translatedMedalInfo.earnedStars = medalInfoData.Stars;
		translatedMedalInfo.winStreak = medalInfoData.Streak;
		translatedMedalInfo.seasonId = medalInfoData.SeasonId;
		translatedMedalInfo.seasonWins = medalInfoData.SeasonWins;
		translatedMedalInfo.seasonGames = medalInfoData.SeasonGames;
		translatedMedalInfo.starsPerWin = medalInfoData.StarsPerWin;
		return translatedMedalInfo;
	}

	public static MedalInfoTranslator DebugCreateMedalInfo(int leagueId, int starLevel, int stars, int starsPerWin, FormatType formatType, bool isWinStreak, bool showWin)
	{
		MedalInfoTranslator medalInfoTranslator = CreateMedalInfoForLeagueId(leagueId, starLevel, 1337);
		TranslatedMedalInfo previousMedal = medalInfoTranslator.GetPreviousMedal(formatType);
		TranslatedMedalInfo currentMedal = medalInfoTranslator.GetCurrentMedal(formatType);
		previousMedal.earnedStars = stars;
		previousMedal.starsPerWin = starsPerWin;
		currentMedal.earnedStars = stars;
		currentMedal.starsPerWin = starsPerWin;
		currentMedal.seasonGames++;
		if (showWin)
		{
			currentMedal.seasonWins++;
			int num = starsPerWin;
			if (isWinStreak)
			{
				previousMedal.winStreak = previousMedal.RankConfig.WinStreakThreshold;
				currentMedal.winStreak = previousMedal.RankConfig.WinStreakThreshold;
				num *= 2;
			}
			while (num > 0 && currentMedal.RankConfig.Stars > 0)
			{
				int num2 = Mathf.Max(currentMedal.RankConfig.Stars - currentMedal.earnedStars, 0);
				if (num <= num2)
				{
					currentMedal.earnedStars += num;
					num = 0;
					continue;
				}
				currentMedal.earnedStars += num2;
				num -= num2;
				currentMedal.starLevel++;
				currentMedal.earnedStars = 0;
			}
			currentMedal.legendIndex++;
		}
		else
		{
			if (currentMedal.RankConfig.CanLoseStars)
			{
				if (currentMedal.earnedStars > 0)
				{
					currentMedal.earnedStars--;
				}
				else if (currentMedal.starLevel > 1 && currentMedal.RankConfig.CanLoseLevel)
				{
					currentMedal.earnedStars = currentMedal.GetMaxStarsAtRank() - 1;
					currentMedal.starLevel--;
				}
			}
			currentMedal.legendIndex--;
		}
		return medalInfoTranslator;
	}

	public TranslatedMedalInfo GetCurrentMedal(FormatType formatType)
	{
		if (!m_currMedalInfo.TryGetValue(formatType, out var value))
		{
			Debug.LogError("MedalInfoTranslator.GetCurrentMedal called for unsupported format type " + formatType.ToString() + ". Returning default TranslatedMedalInfo");
			return new TranslatedMedalInfo();
		}
		return value;
	}

	public TranslatedMedalInfo GetCurrentMedalForCurrentFormatType()
	{
		return GetCurrentMedal(Options.Get().GetEnum<FormatType>(Option.FORMAT_TYPE));
	}

	public TranslatedMedalInfo GetPreviousMedal(FormatType formatType)
	{
		if (!m_prevMedalInfo.TryGetValue(formatType, out var value))
		{
			Debug.LogError("MedalInfoTranslator.GetPreviousMedal called for unsupported format type " + formatType.ToString() + ". Returning default TranslatedMedalInfo");
			return new TranslatedMedalInfo();
		}
		return value;
	}

	public FormatType GetBestCurrentRankFormatType()
	{
		if (m_currMedalInfo == null || m_currMedalInfo.Count == 0)
		{
			Debug.LogError("MedalInfoTranslator.GetBestCurrentRankFormatType had a null or empty m_currMedalInfo. Returning FT_STANDARD. Was this called before the ctor?");
			return FormatType.FT_STANDARD;
		}
		List<KeyValuePair<FormatType, TranslatedMedalInfo>> list = m_currMedalInfo.ToList();
		list.Sort(delegate(KeyValuePair<FormatType, TranslatedMedalInfo> f1, KeyValuePair<FormatType, TranslatedMedalInfo> f2)
		{
			int num = f1.Value.LeagueConfig.LeagueLevel.CompareTo(f2.Value.LeagueConfig.LeagueLevel);
			if (num != 0)
			{
				return num;
			}
			if (f1.Value.IsLegendRank() && f2.Value.IsLegendRank())
			{
				num = f1.Value.legendIndex.CompareTo(f2.Value.legendIndex);
				if (num != 0)
				{
					return -num;
				}
			}
			num = f1.Value.starLevel.CompareTo(f2.Value.starLevel);
			if (num != 0)
			{
				return num;
			}
			num = f1.Value.earnedStars.CompareTo(f2.Value.earnedStars);
			return (num != 0) ? num : CompareFormatTypes(f1.Value.format, f2.Value.format);
		});
		return list.Last().Key;
	}

	private int CompareFormatTypes(FormatType f1, FormatType f2)
	{
		List<FormatType> obj = new List<FormatType>
		{
			FormatType.FT_CLASSIC,
			FormatType.FT_WILD,
			FormatType.FT_STANDARD
		};
		int num = obj.IndexOf(f1);
		int value = obj.IndexOf(f2);
		return num.CompareTo(value);
	}

	public int GetCurrentSeasonId()
	{
		return GetCurrentMedal(FormatType.FT_STANDARD).seasonId;
	}

	public int GetSeasonCardBackMinWins()
	{
		int num = GetPreviousMedal(FormatType.FT_WILD).LeagueConfig.SeasonCardBackMinWins;
		foreach (FormatType value in Enum.GetValues(typeof(FormatType)))
		{
			if (value != 0 && value != FormatType.FT_WILD)
			{
				num = Mathf.Min(num, GetPreviousMedal(value).LeagueConfig.SeasonCardBackMinWins);
			}
		}
		return num;
	}

	public int GetSeasonCardBackWinsRemaining()
	{
		return Mathf.Max(0, GetSeasonCardBackMinWins() - TotalRankedWins);
	}

	public bool HasEarnedSeasonCardBack()
	{
		return GetSeasonCardBackWinsRemaining() == 0;
	}

	public bool ShouldShowCardBackProgress()
	{
		if (TotalRankedWins > TotalRankedWinsPrevious)
		{
			return TotalRankedWinsPrevious < GetSeasonCardBackMinWins();
		}
		return false;
	}

	public bool GetRankedRewardsEarned(FormatType formatType, ref List<List<RewardData>> rewardsEarned)
	{
		TranslatedMedalInfo previousMedal = GetPreviousMedal(formatType);
		TranslatedMedalInfo currentMedal = GetCurrentMedal(formatType);
		if (previousMedal == null || currentMedal == null)
		{
			return false;
		}
		int num = 0;
		foreach (FormatType value in Enum.GetValues(typeof(FormatType)))
		{
			if (value != 0 && value != formatType)
			{
				num = Math.Max(num, GetCurrentMedal(formatType).bestStarLevel);
			}
		}
		bool num2 = previousMedal.bestStarLevel >= currentMedal.bestStarLevel;
		bool flag = num > currentMedal.bestStarLevel;
		if (num2 || flag)
		{
			return false;
		}
		rewardsEarned.Clear();
		int num3 = previousMedal.starLevel;
		while (num3 < currentMedal.starLevel)
		{
			num3++;
			LeagueRankDbfRecord leagueRankRecord = RankMgr.Get().GetLeagueRankRecord(previousMedal.leagueId, num3);
			List<RewardData> rewardData = new List<RewardData>();
			RewardUtils.AddRewardDataStubForBag(leagueRankRecord.RewardBagId, currentMedal.seasonId, ref rewardData);
			if (rewardData.Count > 0)
			{
				rewardsEarned.Add(rewardData);
			}
		}
		return true;
	}

	public RankChangeType GetChangeType(FormatType formatType)
	{
		TranslatedMedalInfo previousMedal = GetPreviousMedal(formatType);
		TranslatedMedalInfo currentMedal = GetCurrentMedal(formatType);
		if (previousMedal == null || currentMedal == null)
		{
			return RankChangeType.UNKNOWN;
		}
		if (currentMedal.seasonId == previousMedal.seasonId && currentMedal.seasonGames == previousMedal.seasonGames)
		{
			return RankChangeType.NO_GAME_PLAYED;
		}
		if (currentMedal.LeagueConfig.LeagueLevel < previousMedal.LeagueConfig.LeagueLevel)
		{
			return RankChangeType.RANK_DOWN;
		}
		if (currentMedal.LeagueConfig.LeagueLevel > previousMedal.LeagueConfig.LeagueLevel)
		{
			return RankChangeType.RANK_UP;
		}
		if (currentMedal.starLevel < previousMedal.starLevel)
		{
			return RankChangeType.RANK_DOWN;
		}
		if (currentMedal.starLevel > previousMedal.starLevel)
		{
			return RankChangeType.RANK_UP;
		}
		return RankChangeType.RANK_SAME;
	}

	public RankedPlayDataModel CreateDataModel(FormatType formatType, RankedMedal.DisplayMode mode, bool isTooltipEnabled = false, bool hasEarnedCardBack = false, Action<RankedPlayDataModel> dataModelReadyCallback = null)
	{
		return GetCurrentMedal(formatType).CreateDataModel(mode, isTooltipEnabled, hasEarnedCardBack, dataModelReadyCallback);
	}

	public void CreateOrUpdateDataModel(FormatType formatType, ref RankedPlayDataModel dataModel, RankedMedal.DisplayMode mode, bool isTooltipEnabled = false, bool hasEarnedCardBack = false, Action<RankedPlayDataModel> dataModelReadyCallback = null)
	{
		GetCurrentMedal(formatType).CreateOrUpdateDataModel(ref dataModel, mode, isTooltipEnabled, hasEarnedCardBack, dataModelReadyCallback);
	}
}
