using Hearthstone.DataModels;
using Hearthstone.Progression;
using Hearthstone.UI;
using UnityEngine;

[RequireComponent(typeof(WidgetTemplate))]
public class RewardTrackItemClaimListener : MonoBehaviour
{
	public const string CLAIM_INDIVIDUAL_REWARD = "CODE_CLAIM_INDIVIDUAL_REWARD";

	public const string CLAIM_CHOOSE_ONE_REWARD = "CODE_CLAIM_CHOOSE_ONE_REWARD";

	private WidgetTemplate m_widget;

	private RewardTrackNodeRewardsDataModel NodeDataModel
	{
		get
		{
			RewardTrackNodeRewardsDataModel dataModel = m_widget.GetDataModel<RewardTrackNodeRewardsDataModel>();
			if (dataModel == null)
			{
				Debug.LogError("RewardTrackItemClaimListener: Failed to get reward track node rewards data model!");
			}
			return dataModel;
		}
	}

	private void Awake()
	{
		m_widget = GetComponent<WidgetTemplate>();
		m_widget.RegisterEventListener(delegate(string eventName)
		{
			if (!(eventName == "CODE_CLAIM_INDIVIDUAL_REWARD"))
			{
				if (eventName == "CODE_CLAIM_CHOOSE_ONE_REWARD")
				{
					OnClaimChooseOneReward();
				}
			}
			else
			{
				OnClaimIndividualReward();
			}
		});
	}

	private void OnClaimIndividualReward()
	{
		if (!Network.IsLoggedIn())
		{
			ProgressUtils.ShowOfflinePopup();
			return;
		}
		RewardTrackNodeRewardsDataModel nodeDataModel = NodeDataModel;
		if (!nodeDataModel.IsClaimed)
		{
			RewardTrackDataModel dataModel = m_widget.GetDataModel<RewardTrackDataModel>();
			if (dataModel != null)
			{
				RewardTrackManager.Get().ClaimRewardTrackReward(dataModel.RewardTrackId, nodeDataModel.Level, nodeDataModel.IsPremium);
			}
		}
	}

	private void OnClaimChooseOneReward()
	{
		if (!Network.IsLoggedIn())
		{
			ProgressUtils.ShowOfflinePopup();
			return;
		}
		RewardItemDataModel rewardItemDataModel = m_widget.GetDataModel<EventDataModel>()?.Payload as RewardItemDataModel;
		if (rewardItemDataModel == null)
		{
			Debug.LogError("RewardTrackItemClaimListener: failed to get reward item data model from event payload!");
			return;
		}
		RewardTrackNodeRewardsDataModel nodeDataModel = NodeDataModel;
		if (!nodeDataModel.IsClaimed)
		{
			RewardTrackDataModel dataModel = m_widget.GetDataModel<RewardTrackDataModel>();
			if (dataModel != null)
			{
				RewardTrackManager.Get().ClaimRewardTrackReward(dataModel.RewardTrackId, nodeDataModel.Level, nodeDataModel.IsPremium, rewardItemDataModel.AssetId);
				m_widget.TriggerEvent("CLEANUP_POPUP_AFTER_CONFIRM");
			}
		}
	}
}
