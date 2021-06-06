using System;
using Hearthstone.DataModels;
using Hearthstone.Progression;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000625 RID: 1573
[RequireComponent(typeof(WidgetTemplate))]
public class RewardTrackItemClaimListener : MonoBehaviour
{
	// Token: 0x0600581E RID: 22558 RVA: 0x001CCDB6 File Offset: 0x001CAFB6
	private void Awake()
	{
		this.m_widget = base.GetComponent<WidgetTemplate>();
		this.m_widget.RegisterEventListener(delegate(string eventName)
		{
			if (eventName == "CODE_CLAIM_INDIVIDUAL_REWARD")
			{
				this.OnClaimIndividualReward();
				return;
			}
			if (!(eventName == "CODE_CLAIM_CHOOSE_ONE_REWARD"))
			{
				return;
			}
			this.OnClaimChooseOneReward();
		});
	}

	// Token: 0x17000528 RID: 1320
	// (get) Token: 0x0600581F RID: 22559 RVA: 0x001CCDDB File Offset: 0x001CAFDB
	private RewardTrackNodeRewardsDataModel NodeDataModel
	{
		get
		{
			RewardTrackNodeRewardsDataModel dataModel = this.m_widget.GetDataModel<RewardTrackNodeRewardsDataModel>();
			if (dataModel == null)
			{
				Debug.LogError("RewardTrackItemClaimListener: Failed to get reward track node rewards data model!");
			}
			return dataModel;
		}
	}

	// Token: 0x06005820 RID: 22560 RVA: 0x001CCDF8 File Offset: 0x001CAFF8
	private void OnClaimIndividualReward()
	{
		if (!Network.IsLoggedIn())
		{
			ProgressUtils.ShowOfflinePopup();
			return;
		}
		RewardTrackNodeRewardsDataModel nodeDataModel = this.NodeDataModel;
		if (nodeDataModel.IsClaimed)
		{
			return;
		}
		RewardTrackDataModel dataModel = this.m_widget.GetDataModel<RewardTrackDataModel>();
		if (dataModel == null)
		{
			return;
		}
		RewardTrackManager.Get().ClaimRewardTrackReward(dataModel.RewardTrackId, nodeDataModel.Level, nodeDataModel.IsPremium, 0);
	}

	// Token: 0x06005821 RID: 22561 RVA: 0x001CCE50 File Offset: 0x001CB050
	private void OnClaimChooseOneReward()
	{
		if (!Network.IsLoggedIn())
		{
			ProgressUtils.ShowOfflinePopup();
			return;
		}
		EventDataModel dataModel = this.m_widget.GetDataModel<EventDataModel>();
		RewardItemDataModel rewardItemDataModel = ((dataModel != null) ? dataModel.Payload : null) as RewardItemDataModel;
		if (rewardItemDataModel == null)
		{
			Debug.LogError("RewardTrackItemClaimListener: failed to get reward item data model from event payload!");
			return;
		}
		RewardTrackNodeRewardsDataModel nodeDataModel = this.NodeDataModel;
		if (nodeDataModel.IsClaimed)
		{
			return;
		}
		RewardTrackDataModel dataModel2 = this.m_widget.GetDataModel<RewardTrackDataModel>();
		if (dataModel2 == null)
		{
			return;
		}
		RewardTrackManager.Get().ClaimRewardTrackReward(dataModel2.RewardTrackId, nodeDataModel.Level, nodeDataModel.IsPremium, rewardItemDataModel.AssetId);
		this.m_widget.TriggerEvent("CLEANUP_POPUP_AFTER_CONFIRM", default(Widget.TriggerEventParameters));
	}

	// Token: 0x04004B94 RID: 19348
	public const string CLAIM_INDIVIDUAL_REWARD = "CODE_CLAIM_INDIVIDUAL_REWARD";

	// Token: 0x04004B95 RID: 19349
	public const string CLAIM_CHOOSE_ONE_REWARD = "CODE_CLAIM_CHOOSE_ONE_REWARD";

	// Token: 0x04004B96 RID: 19350
	private WidgetTemplate m_widget;
}
