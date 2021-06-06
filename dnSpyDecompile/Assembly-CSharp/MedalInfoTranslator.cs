using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using PegasusClient;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000643 RID: 1603
public class MedalInfoTranslator
{
	// Token: 0x1700054A RID: 1354
	// (get) Token: 0x06005A4A RID: 23114 RVA: 0x001D7671 File Offset: 0x001D5871
	public int TotalRankedWins
	{
		get
		{
			return this.m_currMedalInfo.Sum((KeyValuePair<FormatType, TranslatedMedalInfo> x) => x.Value.seasonWins);
		}
	}

	// Token: 0x1700054B RID: 1355
	// (get) Token: 0x06005A4B RID: 23115 RVA: 0x001D769D File Offset: 0x001D589D
	public int TotalRankedWinsPrevious
	{
		get
		{
			return this.m_prevMedalInfo.Sum((KeyValuePair<FormatType, TranslatedMedalInfo> x) => x.Value.seasonWins);
		}
	}

	// Token: 0x06005A4C RID: 23116 RVA: 0x001D76C9 File Offset: 0x001D58C9
	public bool IsDisplayable()
	{
		return this.m_currMedalInfo.Any((KeyValuePair<FormatType, TranslatedMedalInfo> x) => x.Value.starLevel >= 1 && GameDbf.League.HasRecord(x.Value.leagueId));
	}

	// Token: 0x06005A4D RID: 23117 RVA: 0x001D76F8 File Offset: 0x001D58F8
	public MedalInfoTranslator()
	{
		foreach (object obj in Enum.GetValues(typeof(FormatType)))
		{
			FormatType formatType = (FormatType)obj;
			if (formatType != FormatType.FT_UNKNOWN)
			{
				this.m_currMedalInfo.Add(formatType, MedalInfoTranslator.CreateTranslatedMedalInfo(formatType, 0, 0, 0));
				this.m_prevMedalInfo.Add(formatType, MedalInfoTranslator.CreateTranslatedMedalInfo(formatType, 0, 0, 0));
			}
		}
	}

	// Token: 0x06005A4E RID: 23118 RVA: 0x001D779C File Offset: 0x001D599C
	public static MedalInfoTranslator CreateMedalInfoForLeagueId(int leagueId, int starLevel, int legendIndex)
	{
		MedalInfoTranslator medalInfoTranslator = new MedalInfoTranslator();
		foreach (object obj in Enum.GetValues(typeof(FormatType)))
		{
			FormatType formatType = (FormatType)obj;
			if (formatType != FormatType.FT_UNKNOWN)
			{
				medalInfoTranslator.m_currMedalInfo[formatType] = MedalInfoTranslator.CreateTranslatedMedalInfo(formatType, leagueId, starLevel, legendIndex);
				medalInfoTranslator.m_prevMedalInfo[formatType] = medalInfoTranslator.m_currMedalInfo[formatType].ShallowCopy();
			}
		}
		return medalInfoTranslator;
	}

	// Token: 0x06005A4F RID: 23119 RVA: 0x001D7834 File Offset: 0x001D5A34
	public static MedalInfoTranslator CreateMedalInfoForGamePresenceRank(GamePresenceRank gamePresenceRank)
	{
		MedalInfoTranslator medalInfoTranslator = new MedalInfoTranslator();
		using (IEnumerator enumerator = Enum.GetValues(typeof(FormatType)).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				FormatType formatType = (FormatType)enumerator.Current;
				if (formatType != FormatType.FT_UNKNOWN)
				{
					GamePresenceRankData gamePresenceRankData = (from x in gamePresenceRank.Values
					where x.FormatType == formatType
					select x).FirstOrDefault<GamePresenceRankData>();
					if (gamePresenceRankData != null)
					{
						medalInfoTranslator.m_currMedalInfo[formatType] = MedalInfoTranslator.CreateTranslatedMedalInfo(formatType, gamePresenceRankData.LeagueId, gamePresenceRankData.StarLevel, gamePresenceRankData.LegendRank);
					}
					else
					{
						medalInfoTranslator.m_currMedalInfo[formatType] = MedalInfoTranslator.CreateTranslatedMedalInfo(formatType, 0, 0, 0);
					}
					medalInfoTranslator.m_prevMedalInfo[formatType] = medalInfoTranslator.m_currMedalInfo[formatType].ShallowCopy();
				}
			}
		}
		return medalInfoTranslator;
	}

	// Token: 0x06005A50 RID: 23120 RVA: 0x001D794C File Offset: 0x001D5B4C
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

	// Token: 0x06005A51 RID: 23121 RVA: 0x001D7970 File Offset: 0x001D5B70
	public MedalInfoTranslator(NetCache.NetCacheMedalInfo currMedalInfo, NetCache.NetCacheMedalInfo prevMedalInfo = null)
	{
		if (currMedalInfo == null)
		{
			return;
		}
		foreach (MedalInfoData medalInfoData in currMedalInfo.GetAllMedalInfoData())
		{
			this.m_currMedalInfo[medalInfoData.FormatType] = this.Translate(medalInfoData.FormatType, medalInfoData);
		}
		if (prevMedalInfo != null)
		{
			using (Map<FormatType, MedalInfoData>.ValueCollection.Enumerator enumerator = prevMedalInfo.GetAllMedalInfoData().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MedalInfoData medalInfoData2 = enumerator.Current;
					this.m_prevMedalInfo[medalInfoData2.FormatType] = this.Translate(medalInfoData2.FormatType, medalInfoData2);
				}
				return;
			}
		}
		foreach (MedalInfoData medalInfoData3 in currMedalInfo.GetAllMedalInfoData())
		{
			this.m_prevMedalInfo[medalInfoData3.FormatType] = this.m_currMedalInfo[medalInfoData3.FormatType].ShallowCopy();
		}
	}

	// Token: 0x06005A52 RID: 23122 RVA: 0x001D7AB8 File Offset: 0x001D5CB8
	private TranslatedMedalInfo Translate(FormatType format, MedalInfoData medalInfoData)
	{
		if (medalInfoData == null)
		{
			return MedalInfoTranslator.CreateTranslatedMedalInfo(format, 0, 0, 0);
		}
		TranslatedMedalInfo translatedMedalInfo = MedalInfoTranslator.CreateTranslatedMedalInfo(format, medalInfoData.LeagueId, medalInfoData.StarLevel, medalInfoData.HasLegendRank ? medalInfoData.LegendRank : 0);
		translatedMedalInfo.bestStarLevel = medalInfoData.BestStarLevel;
		translatedMedalInfo.earnedStars = medalInfoData.Stars;
		translatedMedalInfo.winStreak = medalInfoData.Streak;
		translatedMedalInfo.seasonId = medalInfoData.SeasonId;
		translatedMedalInfo.seasonWins = medalInfoData.SeasonWins;
		translatedMedalInfo.seasonGames = medalInfoData.SeasonGames;
		translatedMedalInfo.starsPerWin = medalInfoData.StarsPerWin;
		return translatedMedalInfo;
	}

	// Token: 0x06005A53 RID: 23123 RVA: 0x001D7B4C File Offset: 0x001D5D4C
	public static MedalInfoTranslator DebugCreateMedalInfo(int leagueId, int starLevel, int stars, int starsPerWin, FormatType formatType, bool isWinStreak, bool showWin)
	{
		MedalInfoTranslator medalInfoTranslator = MedalInfoTranslator.CreateMedalInfoForLeagueId(leagueId, starLevel, 1337);
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
				}
				else
				{
					currentMedal.earnedStars += num2;
					num -= num2;
					currentMedal.starLevel++;
					currentMedal.earnedStars = 0;
				}
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

	// Token: 0x06005A54 RID: 23124 RVA: 0x001D7CCC File Offset: 0x001D5ECC
	public TranslatedMedalInfo GetCurrentMedal(FormatType formatType)
	{
		TranslatedMedalInfo result;
		if (!this.m_currMedalInfo.TryGetValue(formatType, out result))
		{
			Debug.LogError("MedalInfoTranslator.GetCurrentMedal called for unsupported format type " + formatType.ToString() + ". Returning default TranslatedMedalInfo");
			result = new TranslatedMedalInfo();
		}
		return result;
	}

	// Token: 0x06005A55 RID: 23125 RVA: 0x001D7D11 File Offset: 0x001D5F11
	public TranslatedMedalInfo GetCurrentMedalForCurrentFormatType()
	{
		return this.GetCurrentMedal(Options.Get().GetEnum<FormatType>(Option.FORMAT_TYPE));
	}

	// Token: 0x06005A56 RID: 23126 RVA: 0x001D7D28 File Offset: 0x001D5F28
	public TranslatedMedalInfo GetPreviousMedal(FormatType formatType)
	{
		TranslatedMedalInfo result;
		if (!this.m_prevMedalInfo.TryGetValue(formatType, out result))
		{
			Debug.LogError("MedalInfoTranslator.GetPreviousMedal called for unsupported format type " + formatType.ToString() + ". Returning default TranslatedMedalInfo");
			result = new TranslatedMedalInfo();
		}
		return result;
	}

	// Token: 0x06005A57 RID: 23127 RVA: 0x001D7D70 File Offset: 0x001D5F70
	public FormatType GetBestCurrentRankFormatType()
	{
		if (this.m_currMedalInfo == null || this.m_currMedalInfo.Count == 0)
		{
			Debug.LogError("MedalInfoTranslator.GetBestCurrentRankFormatType had a null or empty m_currMedalInfo. Returning FT_STANDARD. Was this called before the ctor?");
			return FormatType.FT_STANDARD;
		}
		List<KeyValuePair<FormatType, TranslatedMedalInfo>> list = this.m_currMedalInfo.ToList<KeyValuePair<FormatType, TranslatedMedalInfo>>();
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
			if (num != 0)
			{
				return num;
			}
			return this.CompareFormatTypes(f1.Value.format, f2.Value.format);
		});
		return list.Last<KeyValuePair<FormatType, TranslatedMedalInfo>>().Key;
	}

	// Token: 0x06005A58 RID: 23128 RVA: 0x001D7DC8 File Offset: 0x001D5FC8
	private int CompareFormatTypes(FormatType f1, FormatType f2)
	{
		List<FormatType> list = new List<FormatType>();
		list.Add(FormatType.FT_CLASSIC);
		list.Add(FormatType.FT_WILD);
		list.Add(FormatType.FT_STANDARD);
		int num = list.IndexOf(f1);
		int value = list.IndexOf(f2);
		return num.CompareTo(value);
	}

	// Token: 0x06005A59 RID: 23129 RVA: 0x001D7E06 File Offset: 0x001D6006
	public int GetCurrentSeasonId()
	{
		return this.GetCurrentMedal(FormatType.FT_STANDARD).seasonId;
	}

	// Token: 0x06005A5A RID: 23130 RVA: 0x001D7E14 File Offset: 0x001D6014
	public int GetSeasonCardBackMinWins()
	{
		int num = this.GetPreviousMedal(FormatType.FT_WILD).LeagueConfig.SeasonCardBackMinWins;
		foreach (object obj in Enum.GetValues(typeof(FormatType)))
		{
			FormatType formatType = (FormatType)obj;
			if (formatType != FormatType.FT_UNKNOWN && formatType != FormatType.FT_WILD)
			{
				num = Mathf.Min(num, this.GetPreviousMedal(formatType).LeagueConfig.SeasonCardBackMinWins);
			}
		}
		return num;
	}

	// Token: 0x06005A5B RID: 23131 RVA: 0x001D7EA4 File Offset: 0x001D60A4
	public int GetSeasonCardBackWinsRemaining()
	{
		return Mathf.Max(0, this.GetSeasonCardBackMinWins() - this.TotalRankedWins);
	}

	// Token: 0x06005A5C RID: 23132 RVA: 0x001D7EB9 File Offset: 0x001D60B9
	public bool HasEarnedSeasonCardBack()
	{
		return this.GetSeasonCardBackWinsRemaining() == 0;
	}

	// Token: 0x06005A5D RID: 23133 RVA: 0x001D7EC4 File Offset: 0x001D60C4
	public bool ShouldShowCardBackProgress()
	{
		return this.TotalRankedWins > this.TotalRankedWinsPrevious && this.TotalRankedWinsPrevious < this.GetSeasonCardBackMinWins();
	}

	// Token: 0x06005A5E RID: 23134 RVA: 0x001D7EE4 File Offset: 0x001D60E4
	public bool GetRankedRewardsEarned(FormatType formatType, ref List<List<RewardData>> rewardsEarned)
	{
		TranslatedMedalInfo previousMedal = this.GetPreviousMedal(formatType);
		TranslatedMedalInfo currentMedal = this.GetCurrentMedal(formatType);
		if (previousMedal == null || currentMedal == null)
		{
			return false;
		}
		int num = 0;
		foreach (object obj in Enum.GetValues(typeof(FormatType)))
		{
			FormatType formatType2 = (FormatType)obj;
			if (formatType2 != FormatType.FT_UNKNOWN && formatType2 != formatType)
			{
				num = Math.Max(num, this.GetCurrentMedal(formatType).bestStarLevel);
			}
		}
		bool flag = previousMedal.bestStarLevel >= currentMedal.bestStarLevel;
		bool flag2 = num > currentMedal.bestStarLevel;
		if (flag || flag2)
		{
			return false;
		}
		rewardsEarned.Clear();
		int i = previousMedal.starLevel;
		while (i < currentMedal.starLevel)
		{
			i++;
			LeagueRankDbfRecord leagueRankRecord = RankMgr.Get().GetLeagueRankRecord(previousMedal.leagueId, i);
			List<RewardData> list = new List<RewardData>();
			RewardUtils.AddRewardDataStubForBag(leagueRankRecord.RewardBagId, currentMedal.seasonId, ref list);
			if (list.Count > 0)
			{
				rewardsEarned.Add(list);
			}
		}
		return true;
	}

	// Token: 0x06005A5F RID: 23135 RVA: 0x001D8000 File Offset: 0x001D6200
	public RankChangeType GetChangeType(FormatType formatType)
	{
		TranslatedMedalInfo previousMedal = this.GetPreviousMedal(formatType);
		TranslatedMedalInfo currentMedal = this.GetCurrentMedal(formatType);
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

	// Token: 0x06005A60 RID: 23136 RVA: 0x001D8098 File Offset: 0x001D6298
	public RankedPlayDataModel CreateDataModel(FormatType formatType, RankedMedal.DisplayMode mode, bool isTooltipEnabled = false, bool hasEarnedCardBack = false, Action<RankedPlayDataModel> dataModelReadyCallback = null)
	{
		return this.GetCurrentMedal(formatType).CreateDataModel(mode, isTooltipEnabled, hasEarnedCardBack, dataModelReadyCallback);
	}

	// Token: 0x06005A61 RID: 23137 RVA: 0x001D80AC File Offset: 0x001D62AC
	public void CreateOrUpdateDataModel(FormatType formatType, ref RankedPlayDataModel dataModel, RankedMedal.DisplayMode mode, bool isTooltipEnabled = false, bool hasEarnedCardBack = false, Action<RankedPlayDataModel> dataModelReadyCallback = null)
	{
		this.GetCurrentMedal(formatType).CreateOrUpdateDataModel(ref dataModel, mode, isTooltipEnabled, hasEarnedCardBack, dataModelReadyCallback);
	}

	// Token: 0x04004D42 RID: 19778
	private Map<FormatType, TranslatedMedalInfo> m_currMedalInfo = new Map<FormatType, TranslatedMedalInfo>();

	// Token: 0x04004D43 RID: 19779
	private Map<FormatType, TranslatedMedalInfo> m_prevMedalInfo = new Map<FormatType, TranslatedMedalInfo>();
}
