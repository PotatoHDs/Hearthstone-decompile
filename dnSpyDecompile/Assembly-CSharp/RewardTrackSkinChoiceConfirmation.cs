using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000628 RID: 1576
[RequireComponent(typeof(WidgetTemplate))]
public class RewardTrackSkinChoiceConfirmation : MonoBehaviour
{
	// Token: 0x06005833 RID: 22579 RVA: 0x001CD3EC File Offset: 0x001CB5EC
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

	// Token: 0x06005834 RID: 22580 RVA: 0x001CD414 File Offset: 0x001CB614
	public void ClaimClicked()
	{
		EventDataModel dataModel = this.m_widget.GetDataModel<EventDataModel>();
		RewardItemDataModel rewardItemDataModel = ((dataModel != null) ? dataModel.Payload : null) as RewardItemDataModel;
		if (rewardItemDataModel == null)
		{
			Debug.LogError("RewardTrackSkinChoiceConfirmation: failed to get reward item data model from event payload!");
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

	// Token: 0x04004BA5 RID: 19365
	public const string CLAIM_CLICKED = "CODE_CLAIM_CLICKED";

	// Token: 0x04004BA6 RID: 19366
	private WidgetTemplate m_widget;
}
