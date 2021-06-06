using Hearthstone.DataModels;
using Hearthstone.Progression;
using Hearthstone.UI;
using UnityEngine;

[RequireComponent(typeof(WidgetTemplate))]
public class RewardChooseOneItemClaimListener : MonoBehaviour
{
	public const string CLAIM_CHOOSE_ONE_REWARD = "CODE_CLAIM_CHOOSE_ONE_REWARD";

	private WidgetTemplate m_widget;

	private void Awake()
	{
		m_widget = GetComponent<WidgetTemplate>();
		m_widget.RegisterEventListener(delegate(string eventName)
		{
			if (eventName == "CODE_CLAIM_CHOOSE_ONE_REWARD")
			{
				OnClaimChooseOneReward();
			}
		});
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
			Debug.LogError("RewardTrackItemClaimListener: failed to get reward item data model from event payload.");
			return;
		}
		AchievementDataModel dataModel = m_widget.GetDataModel<AchievementDataModel>();
		if (dataModel == null)
		{
			Debug.LogError("RewardTrackItemClaimListener: failed to get achievement data model from widget.");
			return;
		}
		AchievementManager.Get().ClaimAchievementReward(dataModel.ID, rewardItemDataModel.AssetId);
		m_widget.TriggerEvent("CLEANUP_POPUP_AFTER_CONFIRM");
	}
}
