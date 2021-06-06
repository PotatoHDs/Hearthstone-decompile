using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000621 RID: 1569
[RequireComponent(typeof(WidgetTemplate))]
public class ChooseOneRewardChoiceConfirmation : MonoBehaviour
{
	// Token: 0x0600580A RID: 22538 RVA: 0x001CC953 File Offset: 0x001CAB53
	private void Awake()
	{
		this.m_widget = base.GetComponent<WidgetTemplate>();
		this.m_widget.RegisterEventListener(delegate(string eventName)
		{
			if (eventName == "CODE_CLAIM_CLICKED")
			{
				this.ClaimClicked();
			}
		});
	}

	// Token: 0x0600580B RID: 22539 RVA: 0x001CC978 File Offset: 0x001CAB78
	public void ClaimClicked()
	{
		EventDataModel dataModel = this.m_widget.GetDataModel<EventDataModel>();
		RewardItemDataModel rewardItemDataModel = ((dataModel != null) ? dataModel.Payload : null) as RewardItemDataModel;
		if (rewardItemDataModel == null)
		{
			Debug.LogError("ChooseOneRewardChoiceConfirmation: failed to get reward item data model from event payload!");
			return;
		}
		RewardItemDbfRecord record = GameDbf.RewardItem.GetRecord(rewardItemDataModel.AssetId);
		string className = GameStrings.GetClassName(GameUtils.GetTagClassFromCardDbId(record.Card));
		this.m_widget.TriggerEvent("HIDE_POPUP_FOR_CONFIRM", default(Widget.TriggerEventParameters));
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_PROGRESSION_REWARD_TRACK_POPUP_SKIN_CHOICE_CONFIRMATION_HEADER"),
			m_text = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_POPUP_SKIN_CHOICE_CONFIRMATION_TEXT", new object[]
			{
				className,
				record.CardRecord.Name.GetString(true)
			}),
			m_showAlertIcon = false,
			m_alertTextAlignment = UberText.AlignmentOptions.Center,
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response == AlertPopup.Response.CONFIRM)
				{
					this.m_widget.TriggerEvent("CLAIM_CHOOSE_ONE_REWARD", default(Widget.TriggerEventParameters));
					return;
				}
				this.m_widget.TriggerEvent("SHOW_POPUP_AFTER_CONFIRM", default(Widget.TriggerEventParameters));
			}
		};
		DialogManager.Get().ShowPopup(info);
	}

	// Token: 0x04004B85 RID: 19333
	public const string CLAIM_CLICKED = "CODE_CLAIM_CLICKED";

	// Token: 0x04004B86 RID: 19334
	private WidgetTemplate m_widget;
}
