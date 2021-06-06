using System;
using System.Collections.Generic;
using System.Linq;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusUtil;

namespace Hearthstone.Progression
{
	// Token: 0x02001101 RID: 4353
	public static class AchievementFactory
	{
		// Token: 0x0600BE9F RID: 48799 RVA: 0x003A1748 File Offset: 0x0039F948
		public static AchievementCategoryListDataModel CreateAchievementListDataModel(AchievementStats stats, Func<int, PlayerAchievementState> playerState)
		{
			AchievementCategoryListDataModel achievementCategoryListDataModel = new AchievementCategoryListDataModel();
			achievementCategoryListDataModel.Categories = AchievementFactory.CreateAchievementCategoryDataModelList(stats, playerState);
			achievementCategoryListDataModel.Stats = new AchievementStatsDataModel
			{
				Points = stats.GetTotalPoints(),
				AvailablePoints = CategoryList.CountAvailablePoints(),
				Unclaimed = stats.GetTotalUnclaimed(),
				CompletedAchievements = stats.GetTotalCompleted(),
				TotalAchievements = CategoryList.CountAchievements()
			};
			achievementCategoryListDataModel.Stats.UpdateCompletionPercentage();
			return achievementCategoryListDataModel;
		}

		// Token: 0x0600BEA0 RID: 48800 RVA: 0x003A17B8 File Offset: 0x0039F9B8
		public static DataModelList<AchievementCategoryDataModel> CreateAchievementCategoryDataModelList(AchievementStats stats, Func<int, PlayerAchievementState> playerState)
		{
			return (from record in GameDbf.AchievementCategory.GetRecords()
			orderby record.SortOrder
			select AchievementFactory.CreateAchievementCategoryDataModel(record, stats, playerState)).ToDataModelList<AchievementCategoryDataModel>();
		}

		// Token: 0x0600BEA1 RID: 48801 RVA: 0x003A1820 File Offset: 0x0039FA20
		public static AchievementCategoryDataModel CreateAchievementCategoryDataModel(AchievementCategoryDbfRecord record, AchievementStats stats, Func<int, PlayerAchievementState> playerState)
		{
			AchievementCategoryDataModel achievementCategoryDataModel = new AchievementCategoryDataModel();
			DbfLocValue name = record.Name;
			achievementCategoryDataModel.Name = (((name != null) ? name.GetString(true) : null) ?? string.Empty);
			achievementCategoryDataModel.Icon = record.Icon;
			achievementCategoryDataModel.Subcategories = new AchievementSubcategoryListDataModel
			{
				Subcategories = AchievementFactory.CreateAchievementSubCategoryDataModelList(record.Subcategories, stats, playerState)
			};
			achievementCategoryDataModel.Stats = new AchievementStatsDataModel
			{
				Points = stats.GetCategoryPoints(record),
				AvailablePoints = record.CountAvailablePoints(),
				Unclaimed = stats.GetCategoryUnclaimed(record),
				CompletedAchievements = stats.GetCategoryCompleted(record),
				TotalAchievements = record.CountAchievements()
			};
			achievementCategoryDataModel.ID = record.ID;
			achievementCategoryDataModel.Stats.UpdateCompletionPercentage();
			return achievementCategoryDataModel;
		}

		// Token: 0x0600BEA2 RID: 48802 RVA: 0x003A18E0 File Offset: 0x0039FAE0
		public static DataModelList<AchievementSubcategoryDataModel> CreateAchievementSubCategoryDataModelList(List<AchievementSubcategoryDbfRecord> records, AchievementStats stats, Func<int, PlayerAchievementState> playerState)
		{
			return (from record in records
			orderby record.SortOrder
			select AchievementFactory.CreateAchievementSubcategoryDataModel(record, stats, playerState)).ToDataModelList<AchievementSubcategoryDataModel>();
		}

		// Token: 0x0600BEA3 RID: 48803 RVA: 0x003A193C File Offset: 0x0039FB3C
		public static AchievementSubcategoryDataModel CreateAchievementSubcategoryDataModel(AchievementSubcategoryDbfRecord record, AchievementStats stats, Func<int, PlayerAchievementState> playerState)
		{
			DataModelList<AchievementSectionDataModel> dataModelList = AchievementFactory.CreateAchievementSectionDataModelList(record.Sections, playerState);
			dataModelList.UpdatePreviousChains();
			AchievementSubcategoryDataModel achievementSubcategoryDataModel = new AchievementSubcategoryDataModel();
			DbfLocValue name = record.Name;
			achievementSubcategoryDataModel.Name = (((name != null) ? name.GetString(true) : null) ?? string.Empty);
			achievementSubcategoryDataModel.FullName = ProgressUtils.FormatAchievementSubcategoryFullName(record.GetAchievementCategory(), record);
			achievementSubcategoryDataModel.Icon = record.Icon;
			achievementSubcategoryDataModel.Sections = new AchievementSectionListDataModel
			{
				Sections = dataModelList
			};
			achievementSubcategoryDataModel.Stats = new AchievementStatsDataModel
			{
				Points = stats.GetSubcategoryPoints(record),
				AvailablePoints = record.CountAvailablePoints(),
				Unclaimed = stats.GetSubcategoryUnclaimed(record),
				CompletedAchievements = stats.GetSubcategoryCompleted(record),
				TotalAchievements = record.CountAchievements()
			};
			achievementSubcategoryDataModel.ID = record.ID;
			achievementSubcategoryDataModel.Stats.UpdateCompletionPercentage();
			return achievementSubcategoryDataModel;
		}

		// Token: 0x0600BEA4 RID: 48804 RVA: 0x003A1A14 File Offset: 0x0039FC14
		public static DataModelList<AchievementSectionDataModel> CreateAchievementSectionDataModelList(List<AchievementSectionItemDbfRecord> records, Func<int, PlayerAchievementState> playerState)
		{
			return (from record in (from record in records
			orderby record.SortOrder
			select record).ThenBy(delegate(AchievementSectionItemDbfRecord record)
			{
				DbfLocValue name = record.AchievementSectionRecord.Name;
				return ((name != null) ? name.GetString(true) : null) ?? string.Empty;
			})
			select record.AchievementSectionRecord).Select((AchievementSectionDbfRecord record, int index) => AchievementFactory.CreateAchievementSectionDataModel(record, playerState, index)).ToDataModelList<AchievementSectionDataModel>();
		}

		// Token: 0x0600BEA5 RID: 48805 RVA: 0x003A1AB4 File Offset: 0x0039FCB4
		public static AchievementSectionDataModel CreateAchievementSectionDataModel(AchievementSectionDbfRecord record, Func<int, PlayerAchievementState> playerState, int index)
		{
			AchievementSectionDataModel achievementSectionDataModel = new AchievementSectionDataModel();
			DbfLocValue name = record.Name;
			achievementSectionDataModel.Name = (((name != null) ? name.GetString(true) : null) ?? string.Empty);
			achievementSectionDataModel.Achievements = new AchievementListDataModel
			{
				Achievements = new DataModelList<AchievementDataModel>()
			};
			achievementSectionDataModel.ID = record.ID;
			achievementSectionDataModel.Index = index;
			achievementSectionDataModel.TileCount = record.CountChains();
			return achievementSectionDataModel;
		}

		// Token: 0x0600BEA6 RID: 48806 RVA: 0x003A1B20 File Offset: 0x0039FD20
		public static DataModelList<AchievementDataModel> CreateAchievementDataModelList(int sectionId, Func<int, PlayerAchievementState> playerState)
		{
			DataModelList<AchievementDataModel> dataModelList = (from record in GameDbf.Achievement.GetRecords((AchievementDbfRecord record) => record.AchievementSection == sectionId && record.Enabled && (record.AchievementVisibility == Assets.Achievement.AchievementVisibility.VISIBLE || (record.AchievementVisibility == Assets.Achievement.AchievementVisibility.HIDDEN_UNTIL_COMPLETED && AchievementManager.Get().IsAchievementComplete(record.ID))), -1)
			select AchievementFactory.CreateAchievementDataModel(record, playerState(record.ID))).ToDataModelList<AchievementDataModel>();
			dataModelList.UpdateTiers();
			return dataModelList;
		}

		// Token: 0x0600BEA7 RID: 48807 RVA: 0x003A1B74 File Offset: 0x0039FD74
		public static AchievementDataModel CreateAchievementDataModel(AchievementDbfRecord record, PlayerAchievementState playerState)
		{
			AchievementManager.AchievementStatus status = (AchievementManager.AchievementStatus)playerState.Status;
			int progress = playerState.Progress;
			RewardListDataModel rewardListDataModel = RewardUtils.CreateRewardListDataModelFromRewardListId(record.RewardList, 0, null) ?? new RewardListDataModel();
			int rewardTrackXp = record.RewardTrackXp;
			int num = RewardTrackManager.Get().ApplyXpBonusPercent(rewardTrackXp);
			int xpBonusPercent = RewardTrackManager.Get().TrackDataModel.XpBonusPercent;
			AchievementDataModel achievementDataModel = new AchievementDataModel();
			DbfLocValue name = record.Name;
			achievementDataModel.Name = (((name != null) ? name.GetString(true) : null) ?? string.Empty);
			achievementDataModel.Description = ProgressUtils.FormatDescription(record.Description, record.Quota);
			achievementDataModel.Progress = progress;
			achievementDataModel.Quota = record.Quota;
			achievementDataModel.Points = record.Points;
			achievementDataModel.Status = status;
			achievementDataModel.CompletionDate = ((playerState.CompletedDate != 0L) ? ProgressUtils.FormatAchievementCompletionDate(playerState.CompletedDate) : string.Empty);
			achievementDataModel.RewardList = rewardListDataModel;
			achievementDataModel.ID = record.ID;
			achievementDataModel.NextTierID = record.NextTier;
			achievementDataModel.SortOrder = record.SortOrder;
			achievementDataModel.RewardSummary = ProgressUtils.FormatRewardsSummary(rewardListDataModel, num, record.Points, xpBonusPercent > 0);
			achievementDataModel.RewardTrackXp = rewardTrackXp;
			achievementDataModel.RewardTrackXpBonusAdjusted = num;
			achievementDataModel.RewardTrackXpBonusPercent = xpBonusPercent;
			achievementDataModel.AllowExceedQuota = record.AllowExceedQuota;
			achievementDataModel.UpdateProgress();
			return achievementDataModel;
		}

		// Token: 0x0600BEA8 RID: 48808 RVA: 0x003A1CC4 File Offset: 0x0039FEC4
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
