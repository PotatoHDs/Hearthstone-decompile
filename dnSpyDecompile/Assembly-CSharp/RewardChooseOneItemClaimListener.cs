using System;
using Hearthstone.DataModels;
using Hearthstone.Progression;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000623 RID: 1571
[RequireComponent(typeof(WidgetTemplate))]
public class RewardChooseOneItemClaimListener : MonoBehaviour
{
	// Token: 0x06005815 RID: 22549 RVA: 0x001CCBEB File Offset: 0x001CADEB
	private void Awake()
	{
		this.m_widget = base.GetComponent<WidgetTemplate>();
		this.m_widget.RegisterEventListener(delegate(string eventName)
		{
			if (eventName == "CODE_CLAIM_CHOOSE_ONE_REWARD")
			{
				this.OnClaimChooseOneReward();
			}
		});
	}

	// Token: 0x06005816 RID: 22550 RVA: 0x001CCC10 File Offset: 0x001CAE10
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
			Debug.LogError("RewardTrackItemClaimListener: failed to get reward item data model from event payload.");
			return;
		}
		AchievementDataModel dataModel2 = this.m_widget.GetDataModel<AchievementDataModel>();
		if (dataModel2 == null)
		{
			Debug.LogError("RewardTrackItemClaimListener: failed to get achievement data model from widget.");
			return;
		}
		AchievementManager.Get().ClaimAchievementReward(dataModel2.ID, rewardItemDataModel.AssetId);
		this.m_widget.TriggerEvent("CLEANUP_POPUP_AFTER_CONFIRM", default(Widget.TriggerEventParameters));
	}

	// Token: 0x04004B8E RID: 19342
	public const string CLAIM_CHOOSE_ONE_REWARD = "CODE_CLAIM_CHOOSE_ONE_REWARD";

	// Token: 0x04004B8F RID: 19343
	private WidgetTemplate m_widget;
}
