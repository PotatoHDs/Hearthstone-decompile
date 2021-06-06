using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusUtil;

namespace Hearthstone.Progression
{
	// Token: 0x02001121 RID: 4385
	public static class RewardTrackFactory
	{
		// Token: 0x0600C021 RID: 49185 RVA: 0x003A8B38 File Offset: 0x003A6D38
		public static DataModelList<RewardTrackNodeDataModel> CreateRewardTrackNodeDataModelList(RewardTrackDbfRecord trackRecord, RewardTrackDataModel trackDataModel, Func<int, PlayerRewardTrackLevelState> levelState, int startLevel, int endLevel)
		{
			RewardTrackDbfRecord trackRecord2 = trackRecord;
			if (trackRecord2 == null)
			{
				return null;
			}
			return (from record in (from record in trackRecord2.Levels
			orderby record.Level
			select record).Where(delegate(RewardTrackLevelDbfRecord record)
			{
				bool flag = record.Level >= startLevel && record.Level <= endLevel;
				bool flag2 = trackDataModel.LevelSoftCap <= 0 || record.Level < trackDataModel.LevelSoftCap;
				bool flag3 = endLevel > trackDataModel.LevelSoftCap;
				bool flag4 = record.Level == trackDataModel.Level + 1;
				bool flag5 = record.Level == trackDataModel.LevelSoftCap;
				bool flag6 = (trackDataModel.Level >= trackDataModel.LevelSoftCap) ? flag4 : flag5;
				bool flag7 = trackDataModel.Level == trackRecord.Levels.Count && trackDataModel.Level == record.Level;
				return (flag && flag2) || (flag3 && flag6) || (flag3 && flag7);
			})
			select RewardTrackFactory.CreateRewardTrackNodeDataModel(record, trackDataModel, levelState)).ToDataModelList<RewardTrackNodeDataModel>();
		}

		// Token: 0x0600C022 RID: 49186 RVA: 0x003A8BCC File Offset: 0x003A6DCC
		public static RewardTrackNodeDataModel CreateRewardTrackNodeDataModel(RewardTrackLevelDbfRecord record, RewardTrackDataModel TrackDataModel, Func<int, PlayerRewardTrackLevelState> levelState)
		{
			RewardTrackNodeRewardsDataModel freeRewards = RewardTrackFactory.CreateRewardTrackNodeRewardsDataModel(record.FreeRewardListRecord, TrackDataModel, false, levelState(record.Level));
			RewardTrackNodeRewardsDataModel premiumRewards = RewardTrackFactory.CreateRewardTrackNodeRewardsDataModel(record.PaidRewardListRecord, TrackDataModel, true, levelState(record.Level));
			return new RewardTrackNodeDataModel
			{
				Level = record.Level,
				StyleName = record.StyleName,
				FreeRewards = freeRewards,
				PremiumRewards = premiumRewards
			};
		}

		// Token: 0x0600C023 RID: 49187 RVA: 0x003A8C38 File Offset: 0x003A6E38
		public static RewardTrackNodeRewardsDataModel CreateRewardTrackNodeRewardsDataModel(RewardListDbfRecord record, RewardTrackDataModel TrackDataModel, bool isPremium, PlayerRewardTrackLevelState levelState)
		{
			RewardListDataModel rewardListDataModel = (record != null) ? RewardUtils.CreateRewardListDataModelFromRewardListId(record.ID, 0, null) : new RewardListDataModel();
			if (record != null && record.ChooseOne)
			{
				rewardListDataModel.Items = rewardListDataModel.Items.OrderBy((RewardItemDataModel item) => item, new RewardUtils.RewardOwnedItemComparer()).ToDataModelList<RewardItemDataModel>();
			}
			RewardTrackManager.RewardStatus status = (RewardTrackManager.RewardStatus)(isPremium ? levelState.PaidRewardStatus : levelState.FreeRewardStatus);
			string summary;
			if (levelState.Level >= TrackDataModel.LevelSoftCap)
			{
				RewardItemDataModel rewardItemDataModel = rewardListDataModel.Items.FirstOrDefault<RewardItemDataModel>();
				summary = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_POPUP_BONUS_DESCRIPTION", new object[]
				{
					((rewardItemDataModel != null) ? rewardItemDataModel.Quantity.ToString() : null) ?? string.Empty,
					TrackDataModel.LevelSoftCap,
					TrackDataModel.LevelHardCap
				});
			}
			else if (TrackDataModel.Level >= levelState.Level)
			{
				summary = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_POPUP_COMPLETE_LEVEL", new object[]
				{
					levelState.Level
				});
			}
			else
			{
				summary = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_POPUP_INCOMPLETE_LEVEL", new object[]
				{
					levelState.Level
				});
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

		// Token: 0x0600C024 RID: 49188 RVA: 0x003A8DA0 File Offset: 0x003A6FA0
		public static RewardListDataModel CreatePaidRewardListDataModel(RewardTrackDbfRecord record)
		{
			RewardListDataModel rewardListDataModel = new RewardListDataModel();
			rewardListDataModel.Items = (from element in record.Levels
			where element.PaidRewardListRecord != null
			select element).SelectMany(delegate(RewardTrackLevelDbfRecord element)
			{
				RewardListDbfRecord paidRewardListRecord = element.PaidRewardListRecord;
				if (paidRewardListRecord == null)
				{
					return null;
				}
				return paidRewardListRecord.RewardItems;
			}).SelectMany((RewardItemDbfRecord element) => RewardFactory.CreateRewardItemDataModel(element, null)).Consolidate().OrderBy((RewardItemDataModel item) => item, new RewardUtils.RewardItemComparer()).ToDataModelList<RewardItemDataModel>();
			return rewardListDataModel;
		}

		// Token: 0x0600C025 RID: 49189 RVA: 0x003A8E60 File Offset: 0x003A7060
		public static RewardScrollDataModel CreateRewardScrollDataModel(int rewardListId, int level, int chooseOneRewardItemId = 0, List<RewardItemOutput> rewardItemOutputs = null)
		{
			RewardListDbfRecord record = GameDbf.RewardList.GetRecord(rewardListId);
			RewardListDataModel rewardListDataModel = RewardUtils.CreateRewardListDataModelFromRewardListRecord(record, chooseOneRewardItemId, rewardItemOutputs);
			string description;
			if (record != null && record.ChooseOne && record.RewardItems != null)
			{
				RewardItemDbfRecord rewardItemDbfRecord = null;
				foreach (RewardItemDbfRecord rewardItemDbfRecord2 in record.RewardItems)
				{
					if (rewardItemDbfRecord2 != null && rewardItemDbfRecord2.ID == chooseOneRewardItemId)
					{
						rewardItemDbfRecord = rewardItemDbfRecord2;
						break;
					}
				}
				if (rewardItemDbfRecord == null || rewardItemDbfRecord.Card == 0)
				{
					DbfLocValue dbfLocValue = record.Description;
					description = ((dbfLocValue != null) ? dbfLocValue : string.Empty);
				}
				else
				{
					DataModelList<RewardItemDataModel> dataModelList = (rewardListDataModel != null) ? rewardListDataModel.Items : null;
					string text;
					if (dataModelList == null)
					{
						text = null;
					}
					else
					{
						RewardItemDataModel rewardItemDataModel = ((IList<RewardItemDataModel>)dataModelList)[0];
						if (rewardItemDataModel == null)
						{
							text = null;
						}
						else
						{
							CardDataModel card = rewardItemDataModel.Card;
							text = ((card != null) ? card.Name : null);
						}
					}
					string text2;
					if ((text2 = text) == null)
					{
						DbfLocValue dbfLocValue = record.Description;
						text2 = ((dbfLocValue != null) ? dbfLocValue : string.Empty);
					}
					description = text2;
				}
			}
			else
			{
				DbfLocValue dbfLocValue = (record != null) ? record.Description : null;
				description = ((dbfLocValue != null) ? dbfLocValue : string.Empty);
			}
			return new RewardScrollDataModel
			{
				DisplayName = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_REWARD_SCROLL_TITLE", new object[]
				{
					level
				}),
				Description = description,
				RewardList = rewardListDataModel
			};
		}
	}
}
