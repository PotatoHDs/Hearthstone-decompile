using System;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;

namespace Hearthstone.Progression
{
	// Token: 0x0200111D RID: 4381
	public class RewardPresenter
	{
		// Token: 0x0600C001 RID: 49153 RVA: 0x003A8074 File Offset: 0x003A6274
		public bool ShowNextReward(Action onHiddenCallback)
		{
			if (this.m_rewardsToShow.Count == 0)
			{
				return false;
			}
			ValueTuple<RewardScrollDataModel, Action> rewardParams = this.m_rewardsToShow.Dequeue();
			this.CreateRewardPrefab(rewardParams.Item1, delegate
			{
				rewardParams.Item2();
				Action onHiddenCallback2 = onHiddenCallback;
				if (onHiddenCallback2 == null)
				{
					return;
				}
				onHiddenCallback2();
			});
			return true;
		}

		// Token: 0x0600C002 RID: 49154 RVA: 0x003A80CC File Offset: 0x003A62CC
		public void EnqueueReward(RewardScrollDataModel dataModel, Action acknowledge)
		{
			if (dataModel == null)
			{
				return;
			}
			if (dataModel.RewardList.Items.Count > 4)
			{
				List<RewardScrollDataModel> list = this.SplitRewardScrollDataModel(dataModel, 4);
				for (int i = 0; i < list.Count; i++)
				{
					ValueTuple<RewardScrollDataModel, Action> item = new ValueTuple<RewardScrollDataModel, Action>(list[i], acknowledge);
					this.m_rewardsToShow.Enqueue(item);
				}
				return;
			}
			ValueTuple<RewardScrollDataModel, Action> item2 = new ValueTuple<RewardScrollDataModel, Action>(dataModel, acknowledge);
			this.m_rewardsToShow.Enqueue(item2);
		}

		// Token: 0x0600C003 RID: 49155 RVA: 0x003A813B File Offset: 0x003A633B
		public void Clear()
		{
			this.m_rewardsToShow.Clear();
		}

		// Token: 0x0600C004 RID: 49156 RVA: 0x003A8148 File Offset: 0x003A6348
		private List<RewardScrollDataModel> SplitRewardScrollDataModel(RewardScrollDataModel dataModel, int size)
		{
			DataModelList<RewardItemDataModel> items = dataModel.RewardList.Items;
			DataModelList<RewardItemDataModel> dataModelList = new DataModelList<RewardItemDataModel>();
			List<RewardScrollDataModel> list = new List<RewardScrollDataModel>();
			int num = 0;
			foreach (RewardItemDataModel item in items)
			{
				dataModelList.Add(item);
				if (++num == size || items.IndexOf(item) == items.Count - 1)
				{
					RewardScrollDataModel rewardScrollDataModel = dataModel.CloneDataModel<RewardScrollDataModel>();
					rewardScrollDataModel.RewardList = dataModel.RewardList.CloneDataModel<RewardListDataModel>();
					rewardScrollDataModel.RewardList.Items = dataModelList;
					list.Add(rewardScrollDataModel);
					dataModelList = new DataModelList<RewardItemDataModel>();
					num = 0;
				}
			}
			return list;
		}

		// Token: 0x0600C005 RID: 49157 RVA: 0x003A8204 File Offset: 0x003A6404
		private void CreateRewardPrefab(RewardScrollDataModel rewardScrollDataModel, Action onHiddenCallback)
		{
			Widget rewardWidget = WidgetInstance.Create(RewardPresenter.REWARD_PREFAB, false);
			Action <>9__1;
			rewardWidget.RegisterDoneChangingStatesListener(delegate(object _)
			{
				RewardScroll componentInChildren = rewardWidget.GetComponentInChildren<RewardScroll>();
				RewardScrollDataModel rewardScrollDataModel2 = rewardScrollDataModel;
				Action onHiddenCallback2;
				if ((onHiddenCallback2 = <>9__1) == null)
				{
					onHiddenCallback2 = (<>9__1 = delegate()
					{
						Action onHiddenCallback3 = onHiddenCallback;
						if (onHiddenCallback3 == null)
						{
							return;
						}
						onHiddenCallback3();
					});
				}
				componentInChildren.Initialize(rewardScrollDataModel2, onHiddenCallback2, null);
				componentInChildren.Show();
			}, null, true, true);
		}

		// Token: 0x04009BB8 RID: 39864
		public static readonly AssetReference REWARD_PREFAB = new AssetReference("RewardScroll.prefab:ccd55a2608a26544da63232e330ad1d5");

		// Token: 0x04009BB9 RID: 39865
		private const string HIDE = "CODE_HIDE";

		// Token: 0x04009BBA RID: 39866
		private const int MAX_REWARDS_PER_SCROLL = 4;

		// Token: 0x04009BBB RID: 39867
		private readonly Queue<ValueTuple<RewardScrollDataModel, Action>> m_rewardsToShow = new Queue<ValueTuple<RewardScrollDataModel, Action>>();
	}
}
