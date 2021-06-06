using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusUtil;

namespace Hearthstone.Progression
{
	public static class RewardTrackFactory
	{
		public static DataModelList<RewardTrackNodeDataModel> CreateRewardTrackNodeDataModelList(RewardTrackDbfRecord trackRecord, RewardTrackDataModel trackDataModel, Func<int, PlayerRewardTrackLevelState> levelState, int startLevel, int endLevel)
		{
			return (from record in trackRecord?.Levels.OrderBy((RewardTrackLevelDbfRecord record) => record.Level).Where(delegate(RewardTrackLevelDbfRecord record)
				{
					bool num = record.Level >= startLevel && record.Level <= endLevel;
					bool flag = trackDataModel.LevelSoftCap <= 0 || record.Level < trackDataModel.LevelSoftCap;
					bool flag2 = endLevel > trackDataModel.LevelSoftCap;
					bool flag3 = record.Level == trackDataModel.Level + 1;
					bool flag4 = record.Level == trackDataModel.LevelSoftCap;
					bool flag5 = ((trackDataModel.Level >= trackDataModel.LevelSoftCap) ? flag3 : flag4);
					bool flag6 = trackDataModel.Level == trackRecord.Levels.Count && trackDataModel.Level == record.Level;
					return (num && flag) || (flag2 && flag5) || (flag2 && flag6);
				})
				select CreateRewardTrackNodeDataModel(record, trackDataModel, levelState)).ToDataModelList();
		}

		public static RewardTrackNodeDataModel CreateRewardTrackNodeDataModel(RewardTrackLevelDbfRecord record, RewardTrackDataModel TrackDataModel, Func<int, PlayerRewardTrackLevelState> levelState)
		{
			RewardTrackNodeRewardsDataModel freeRewards = CreateRewardTrackNodeRewardsDataModel(record.FreeRewardListRecord, TrackDataModel, isPremium: false, levelState(record.Level));
			RewardTrackNodeRewardsDataModel premiumRewards = CreateRewardTrackNodeRewardsDataModel(record.PaidRewardListRecord, TrackDataModel, isPremium: true, levelState(record.Level));
			return new RewardTrackNodeDataModel
			{
				Level = record.Level,
				StyleName = record.StyleName,
				FreeRewards = freeRewards,
				PremiumRewards = premiumRewards
			};
		}

		public static RewardTrackNodeRewardsDataModel CreateRewardTrackNodeRewardsDataModel(RewardListDbfRecord record, RewardTrackDataModel TrackDataModel, bool isPremium, PlayerRewardTrackLevelState levelState)
		{
			RewardListDataModel rewardListDataModel = ((record != null) ? RewardUtils.CreateRewardListDataModelFromRewardListId(record.ID) : new RewardListDataModel());
			if (record != null && record.ChooseOne)
			{
				rewardListDataModel.Items = rewardListDataModel.Items.OrderBy((RewardItemDataModel item) => item, new RewardUtils.RewardOwnedItemComparer()).ToDataModelList();
			}
			RewardTrackManager.RewardStatus status = (RewardTrackManager.RewardStatus)(isPremium ? levelState.PaidRewardStatus : levelState.FreeRewardStatus);
			string summary;
			if (levelState.Level < TrackDataModel.LevelSoftCap)
			{
				summary = ((TrackDataModel.Level < levelState.Level) ? GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_POPUP_INCOMPLETE_LEVEL", levelState.Level) : GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_POPUP_COMPLETE_LEVEL", levelState.Level));
			}
			else
			{
				RewardItemDataModel rewardItemDataModel = rewardListDataModel.Items.FirstOrDefault();
				summary = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_POPUP_BONUS_DESCRIPTION", rewardItemDataModel?.Quantity.ToString() ?? string.Empty, TrackDataModel.LevelSoftCap, TrackDataModel.LevelHardCap);
			}
			return new RewardTrackNodeRewardsDataModel
			{
				Level = levelState.Level,
				Summary = summary,
				IsPremium = isPremium,
				Items = rewardListDataModel,
				IsClaimed = ProgressUtils.HasClaimedRewardTrackReward(status)
			};
		}

		public static RewardListDataModel CreatePaidRewardListDataModel(RewardTrackDbfRecord record)
		{
			return new RewardListDataModel
			{
				Items = record.Levels.Where((RewardTrackLevelDbfRecord element) => element.PaidRewardListRecord != null).SelectMany((RewardTrackLevelDbfRecord element) => element.PaidRewardListRecord?.RewardItems).SelectMany((RewardItemDbfRecord element) => RewardFactory.CreateRewardItemDataModel(element))
					.Consolidate()
					.OrderBy((RewardItemDataModel item) => item, new RewardUtils.RewardItemComparer())
					.ToDataModelList()
			};
		}

		public static RewardScrollDataModel CreateRewardScrollDataModel(int rewardListId, int level, int chooseOneRewardItemId = 0, List<RewardItemOutput> rewardItemOutputs = null)
		{
			RewardListDbfRecord record = GameDbf.RewardList.GetRecord(rewardListId);
			RewardListDataModel rewardListDataModel = RewardUtils.CreateRewardListDataModelFromRewardListRecord(record, chooseOneRewardItemId, rewardItemOutputs);
			string description2;
			if (record != null && record.ChooseOne && record.RewardItems != null)
			{
				RewardItemDbfRecord rewardItemDbfRecord = null;
				foreach (RewardItemDbfRecord rewardItem in record.RewardItems)
				{
					if (rewardItem != null && rewardItem.ID == chooseOneRewardItemId)
					{
						rewardItemDbfRecord = rewardItem;
						break;
					}
				}
				if ((rewardItemDbfRecord?.Card ?? 0) == 0)
				{
					DbfLocValue description = record.Description;
					description2 = ((description != null) ? ((string)description) : string.Empty);
				}
				else
				{
					object obj = ((IList<RewardItemDataModel>)(rewardListDataModel?.Items))?[0]?.Card?.Name;
					if (obj == null)
					{
						DbfLocValue description = record.Description;
						obj = ((description != null) ? ((string)description) : string.Empty);
					}
					description2 = (string)obj;
				}
			}
			else
			{
				DbfLocValue description = record?.Description;
				description2 = ((description != null) ? ((string)description) : string.Empty);
			}
			RewardScrollDataModel rewardScrollDataModel = new RewardScrollDataModel();
			rewardScrollDataModel.DisplayName = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_REWARD_SCROLL_TITLE", level);
			rewardScrollDataModel.Description = description2;
			rewardScrollDataModel.RewardList = rewardListDataModel;
			return rewardScrollDataModel;
		}
	}
}
