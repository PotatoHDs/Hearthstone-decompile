using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

[RequireComponent(typeof(WidgetTemplate))]
public class RewardTrackSkinChoiceConfirmation : MonoBehaviour
{
	public const string CLAIM_CLICKED = "CODE_CLAIM_CLICKED";

	private WidgetTemplate m_widget;

	private void Awake()
	{
		m_widget = GetComponent<WidgetTemplate>();
		m_widget.RegisterEventListener(delegate(string eventName)
		{
			if (eventName == "CODE_CLAIM_CLICKED")
			{
				ClaimClicked();
			}
		});
	}

	public void ClaimClicked()
	{
		RewardItemDataModel rewardItemDataModel = m_widget.GetDataModel<EventDataModel>()?.Payload as RewardItemDataModel;
		if (rewardItemDataModel == null)
		{
			Debug.LogError("RewardTrackSkinChoiceConfirmation: failed to get reward item data model from event payload!");
			return;
		}
		RewardItemDbfRecord record = GameDbf.RewardItem.GetRecord(rewardItemDataModel.AssetId);
		string className = GameStrings.GetClassName(GameUtils.GetTagClassFromCardDbId(record.Card));
		m_widget.TriggerEvent("HIDE_POPUP_FOR_CONFIRM");
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_PROGRESSION_REWARD_TRACK_POPUP_SKIN_CHOICE_CONFIRMATION_HEADER");
		popupInfo.m_text = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_POPUP_SKIN_CHOICE_CONFIRMATION_TEXT", className, record.CardRecord.Name.GetString());
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
		{
			if (response == AlertPopup.Response.CONFIRM)
			{
				m_widget.TriggerEvent("CLAIM_CHOOSE_ONE_REWARD");
			}
			else
			{
				m_widget.TriggerEvent("SHOW_POPUP_AFTER_CONFIRM");
			}
		};
		AlertPopup.PopupInfo info = popupInfo;
		DialogManager.Get().ShowPopup(info);
	}
}
