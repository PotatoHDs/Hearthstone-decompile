using System;
using System.Collections.Generic;
using System.Linq;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusUtil;

namespace Hearthstone.Progression
{
	public static class AchievementFactory
	{
		public static AchievementCategoryListDataModel CreateAchievementListDataModel(AchievementStats stats, Func<int, PlayerAchievementState> playerState)
		{
			AchievementCategoryListDataModel obj = new AchievementCategoryListDataModel
			{
				Categories = CreateAchievementCategoryDataModelList(stats, playerState),
				Stats = new AchievementStatsDataModel
				{
					Points = stats.GetTotalPoints(),
					AvailablePoints = CategoryList.CountAvailablePoints(),
					Unclaimed = stats.GetTotalUnclaimed(),
					CompletedAchievements = stats.GetTotalCompleted(),
					TotalAchievements = CategoryList.CountAchievements()
				}
			};
			obj.Stats.UpdateCompletionPercentage();
			return obj;
		}

		public static DataModelList<AchievementCategoryDataModel> CreateAchievementCategoryDataModelList(AchievementStats stats, Func<int, PlayerAchievementState> playerState)
		{
			return (from record in GameDbf.AchievementCategory.GetRecords()
				orderby record.SortOrder
				select CreateAchievementCategoryDataModel(record, stats, playerState)).ToDataModelList();
		}

		public static AchievementCategoryDataModel CreateAchievementCategoryDataModel(AchievementCategoryDbfRecord record, AchievementStats stats, Func<int, PlayerAchievementState> playerState)
		{
			AchievementCategoryDataModel obj = new AchievementCategoryDataModel
			{
				Name = (record.Name?.GetString() ?? string.Empty),
				Icon = record.Icon,
				Subcategories = new AchievementSubcategoryListDataModel
				{
					Subcategories = CreateAchievementSubCategoryDataModelList(record.Subcategories, stats, playerState)
				},
				Stats = new AchievementStatsDataModel
				{
					Points = stats.GetCategoryPoints(record),
					AvailablePoints = record.CountAvailablePoints(),
					Unclaimed = stats.GetCategoryUnclaimed(record),
					CompletedAchievements = stats.GetCategoryCompleted(record),
					TotalAchievements = record.CountAchievements()
				},
				ID = record.ID
			};
			obj.Stats.UpdateCompletionPercentage();
			return obj;
		}

		public static DataModelList<AchievementSubcategoryDataModel> CreateAchievementSubCategoryDataModelList(List<AchievementSubcategoryDbfRecord> records, AchievementStats stats, Func<int, PlayerAchievementState> playerState)
		{
			return (from record in records
				orderby record.SortOrder
				select CreateAchievementSubcategoryDataModel(record, stats, playerState)).ToDataModelList();
		}

		public static AchievementSubcategoryDataModel CreateAchievementSubcategoryDataModel(AchievementSubcategoryDbfRecord record, AchievementStats stats, Func<int, PlayerAchievementState> playerState)
		{
			DataModelList<AchievementSectionDataModel> dataModelList = CreateAchievementSectionDataModelList(record.Sections, playerState);
			dataModelList.UpdatePreviousChains();
			AchievementSubcategoryDataModel obj = new AchievementSubcategoryDataModel
			{
				Name = (record.Name?.GetString() ?? string.Empty),
				FullName = ProgressUtils.FormatAchievementSubcategoryFullName(record.GetAchievementCategory(), record),
				Icon = record.Icon,
				Sections = new AchievementSectionListDataModel
				{
					Sections = dataModelList
				},
				Stats = new AchievementStatsDataModel
				{
					Points = stats.GetSubcategoryPoints(record),
					AvailablePoints = record.CountAvailablePoints(),
					Unclaimed = stats.GetSubcategoryUnclaimed(record),
					CompletedAchievements = stats.GetSubcategoryCompleted(record),
					TotalAchievements = record.CountAchievements()
				},
				ID = record.ID
			};
			obj.Stats.UpdateCompletionPercentage();
			return obj;
		}

		public static DataModelList<AchievementSectionDataModel> CreateAchievementSectionDataModelList(List<AchievementSectionItemDbfRecord> records, Func<int, PlayerAchievementState> playerState)
		{
			return (from record in records
				orderby record.SortOrder, record.AchievementSectionRecord.Name?.GetString() ?? string.Empty
				select record.AchievementSectionRecord).Select((AchievementSectionDbfRecord record, int index) => CreateAchievementSectionDataModel(record, playerState, index)).ToDataModelList();
		}

		public static AchievementSectionDataModel CreateAchievementSectionDataModel(AchievementSectionDbfRecord record, Func<int, PlayerAchievementState> playerState, int index)
		{
			return new AchievementSectionDataModel
			{
				Name = (record.Name?.GetString() ?? string.Empty),
				Achievements = new AchievementListDataModel
				{
					Achievements = new DataModelList<AchievementDataModel>()
				},
				ID = record.ID,
				Index = index,
				TileCount = record.CountChains()
			};
		}

		public static DataModelList<AchievementDataModel> CreateAchievementDataModelList(int sectionId, Func<int, PlayerAchievementState> playerState)
		{
			DataModelList<AchievementDataModel> dataModelList = (from record in GameDbf.Achievement.GetRecords((AchievementDbfRecord record) => record.AchievementSection == sectionId && record.Enabled && (record.AchievementVisibility == Assets.Achievement.AchievementVisibility.VISIBLE || (record.AchievementVisibility == Assets.Achievement.AchievementVisibility.HIDDEN_UNTIL_COMPLETED && AchievementManager.Get().IsAchievementComplete(record.ID))))
				select CreateAchievementDataModel(record, playerState(record.ID))).ToDataModelList();
			dataModelList.UpdateTiers();
			return dataModelList;
		}

		public static AchievementDataModel CreateAchievementDataModel(AchievementDbfRecord record, PlayerAchievementState playerState)
		{
			AchievementManager.AchievementStatus status = (AchievementManager.AchievementStatus)playerState.Status;
			int progress = playerState.Progress;
			RewardListDataModel rewardListDataModel = RewardUtils.CreateRewardListDataModelFromRewardListId(record.RewardList) ?? new RewardListDataModel();
			int rewardTrackXp = record.RewardTrackXp;
			int num = RewardTrackManager.Get().ApplyXpBonusPercent(rewardTrackXp);
			int xpBonusPercent = RewardTrackManager.Get().TrackDataModel.XpBonusPercent;
			AchievementDataModel obj = new AchievementDataModel
			{
				Name = (record.Name?.GetString() ?? string.Empty),
				Description = ProgressUtils.FormatDescription(record.Description, record.Quota),
				Progress = progress,
				Quota = record.Quota,
				Points = record.Points,
				Status = status,
				CompletionDate = ((playerState.CompletedDate != 0L) ? ProgressUtils.FormatAchievementCompletionDate(playerState.CompletedDate) : string.Empty),
				RewardList = rewardListDataModel,
				ID = record.ID,
				NextTierID = record.NextTier,
				SortOrder = record.SortOrder,
				RewardSummary = ProgressUtils.FormatRewardsSummary(rewardListDataModel, num, record.Points, xpBonusPercent > 0),
				RewardTrackXp = rewardTrackXp,
				RewardTrackXpBonusAdjusted = num,
				RewardTrackXpBonusPercent = xpBonusPercent,
				AllowExceedQuota = record.AllowExceedQuota
			};
			obj.UpdateProgress();
			return obj;
		}

		public static RewardScrollDataModel CreateRewardScrollDataModel(int achievementId, int chooseOneRewardItemId = 0, List<RewardItemOutput> rewardItemOutput = null)
		{
			AchievementDbfRecord record = GameDbf.Achievement.GetRecord(achievementId);
			if (record == null)
			{
				return null;
			}
			return new RewardScrollDataModel
			{
				DisplayName = record.Name,
				Description = ProgressUtils.FormatDescription(record.Description, record.Quota),
				RewardList = RewardUtils.CreateRewardListDataModelFromRewardListId(record.RewardList, chooseOneRewardItemId, rewardItemOutput)
			};
		}
	}
}
