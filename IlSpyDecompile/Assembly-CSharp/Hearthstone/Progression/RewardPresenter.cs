using System;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;

namespace Hearthstone.Progression
{
	public class RewardPresenter
	{
		public static readonly AssetReference REWARD_PREFAB = new AssetReference("RewardScroll.prefab:ccd55a2608a26544da63232e330ad1d5");

		private const string HIDE = "CODE_HIDE";

		private const int MAX_REWARDS_PER_SCROLL = 4;

		private readonly Queue<(RewardScrollDataModel, Action)> m_rewardsToShow = new Queue<(RewardScrollDataModel, Action)>();

		public bool ShowNextReward(Action onHiddenCallback)
		{
			if (m_rewardsToShow.Count == 0)
			{
				return false;
			}
			(RewardScrollDataModel, Action) rewardParams = m_rewardsToShow.Dequeue();
			CreateRewardPrefab(rewardParams.Item1, delegate
			{
				rewardParams.Item2();
				onHiddenCallback?.Invoke();
			});
			return true;
		}

		public void EnqueueReward(RewardScrollDataModel dataModel, Action acknowledge)
		{
			if (dataModel == null)
			{
				return;
			}
			if (dataModel.RewardList.Items.Count > 4)
			{
				List<RewardScrollDataModel> list = SplitRewardScrollDataModel(dataModel, 4);
				for (int i = 0; i < list.Count; i++)
				{
					(RewardScrollDataModel, Action) item = (list[i], acknowledge);
					m_rewardsToShow.Enqueue(item);
				}
			}
			else
			{
				(RewardScrollDataModel, Action) item2 = (dataModel, acknowledge);
				m_rewardsToShow.Enqueue(item2);
			}
		}

		public void Clear()
		{
			m_rewardsToShow.Clear();
		}

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
					RewardScrollDataModel rewardScrollDataModel = dataModel.CloneDataModel();
					rewardScrollDataModel.RewardList = dataModel.RewardList.CloneDataModel();
					rewardScrollDataModel.RewardList.Items = dataModelList;
					list.Add(rewardScrollDataModel);
					dataModelList = new DataModelList<RewardItemDataModel>();
					num = 0;
				}
			}
			return list;
		}

		private void CreateRewardPrefab(RewardScrollDataModel rewardScrollDataModel, Action onHiddenCallback)
		{
			Widget rewardWidget = WidgetInstance.Create(REWARD_PREFAB);
			rewardWidget.RegisterDoneChangingStatesListener(delegate
			{
				RewardScroll componentInChildren = rewardWidget.GetComponentInChildren<RewardScroll>();
				componentInChildren.Initialize(rewardScrollDataModel, delegate
				{
					onHiddenCallback?.Invoke();
				});
				componentInChildren.Show();
			}, null, callImmediatelyIfSet: true, doOnce: true);
		}
	}
}
