using System;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;

public class ChooseDeckReward : CustomVisualReward
{
	public AsyncReference m_chooseDeckReference;

	public AsyncReference[] m_classButtonReferences;

	private DeckChoiceDataModel m_deckChoiceDataModel;

	private DeckChoiceDataModel[] m_buttonDataModels;

	private Widget[] m_classButtonWidgets;

	private Widget m_chooseDeckWidget;

	private List<DeckDbfRecord> m_decks;

	private List<DeckTemplateDbfRecord> m_deckTemplates;

	private Achievement.ClickTriggerType m_currentClickTrigger;

	private string m_deckName;

	private Action m_completeCallback;

	private long m_noticeId;

	private TAG_CLASS[] m_classByButtonIndex = new TAG_CLASS[10]
	{
		TAG_CLASS.DRUID,
		TAG_CLASS.HUNTER,
		TAG_CLASS.MAGE,
		TAG_CLASS.PALADIN,
		TAG_CLASS.PRIEST,
		TAG_CLASS.ROGUE,
		TAG_CLASS.SHAMAN,
		TAG_CLASS.WARLOCK,
		TAG_CLASS.WARRIOR,
		TAG_CLASS.DEMONHUNTER
	};

	public override void Start()
	{
		m_classButtonWidgets = new Widget[m_classButtonReferences.Length];
		m_buttonDataModels = new DeckChoiceDataModel[m_classButtonReferences.Length];
		for (int i = 0; i < m_classButtonReferences.Length; i++)
		{
			int classIndex = i;
			m_classButtonReferences[classIndex].RegisterReadyListener(delegate(Widget w)
			{
				SetupDataModelForButton(w, classIndex);
			});
		}
		m_deckChoiceDataModel = new DeckChoiceDataModel();
		m_chooseDeckReference.RegisterReadyListener(delegate(Widget w)
		{
			m_chooseDeckWidget = w;
			w.BindDataModel(m_deckChoiceDataModel);
		});
		m_deckTemplates = GameDbf.DeckTemplate.GetRecords((DeckTemplateDbfRecord deckTemplateRecord) => deckTemplateRecord.IsFreeReward);
		base.Start();
	}

	public void SetSelectedButtonIndex(int index)
	{
		m_deckChoiceDataModel.ChoiceClassID = (int)m_classByButtonIndex[index];
		m_deckChoiceDataModel.ChoiceClassName = GameStrings.GetClassName(m_classByButtonIndex[index]);
		DeckTemplateDbfRecord deckTemplateDbfRecord = m_deckTemplates.Find((DeckTemplateDbfRecord record) => record.ClassId == m_deckChoiceDataModel.ChoiceClassID);
		if (deckTemplateDbfRecord == null)
		{
			Log.MissingAssets.PrintError("Could not find a free deck template for class id = {0}", m_deckChoiceDataModel.ChoiceClassID);
		}
		else
		{
			m_deckChoiceDataModel.DeckDescription = (string.IsNullOrEmpty(deckTemplateDbfRecord.DeckRecord.AltDescription) ? deckTemplateDbfRecord.DeckRecord.Description : deckTemplateDbfRecord.DeckRecord.AltDescription);
			m_chooseDeckWidget.TriggerEvent("UpdateVisuals");
		}
	}

	public void ChoiceConfirmed()
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_FREE_DECK_CONFIRMATION_HEADER");
		popupInfo.m_text = GameStrings.Format("GLUE_FREE_DECK_CONFIRMATION_TEXT", m_deckChoiceDataModel.ChoiceClassName);
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
		{
			if (response == AlertPopup.Response.CONFIRM)
			{
				Network.Get().SendFreeDeckChoice(m_deckChoiceDataModel.ChoiceClassID, m_noticeId);
				m_chooseDeckWidget.TriggerEvent("COMPLETE");
			}
			else
			{
				m_chooseDeckWidget.TriggerEvent("SHOW");
			}
		};
		AlertPopup.PopupInfo info = popupInfo;
		DialogManager.Get().ShowPopup(info);
	}

	public void SetNoticeId(long noticeId)
	{
		m_noticeId = noticeId;
	}

	private void SetupDataModelForButton(Widget w, int index)
	{
		string buttonClass = m_classByButtonIndex[index].ToString();
		DeckChoiceDataModel deckChoiceDataModel = new DeckChoiceDataModel();
		deckChoiceDataModel.ButtonClass = buttonClass;
		m_classButtonWidgets[index] = w;
		m_buttonDataModels[index] = deckChoiceDataModel;
		w.BindDataModel(deckChoiceDataModel);
	}
}
