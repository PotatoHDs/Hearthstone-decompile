using System;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;

// Token: 0x02000887 RID: 2183
public class ChooseDeckReward : CustomVisualReward
{
	// Token: 0x0600775A RID: 30554 RVA: 0x0026F8E4 File Offset: 0x0026DAE4
	public override void Start()
	{
		this.m_classButtonWidgets = new Widget[this.m_classButtonReferences.Length];
		this.m_buttonDataModels = new DeckChoiceDataModel[this.m_classButtonReferences.Length];
		for (int i = 0; i < this.m_classButtonReferences.Length; i++)
		{
			int classIndex = i;
			this.m_classButtonReferences[classIndex].RegisterReadyListener<Widget>(delegate(Widget w)
			{
				this.SetupDataModelForButton(w, classIndex);
			});
		}
		this.m_deckChoiceDataModel = new DeckChoiceDataModel();
		this.m_chooseDeckReference.RegisterReadyListener<Widget>(delegate(Widget w)
		{
			this.m_chooseDeckWidget = w;
			w.BindDataModel(this.m_deckChoiceDataModel, false);
		});
		this.m_deckTemplates = GameDbf.DeckTemplate.GetRecords((DeckTemplateDbfRecord deckTemplateRecord) => deckTemplateRecord.IsFreeReward, -1);
		base.Start();
	}

	// Token: 0x0600775B RID: 30555 RVA: 0x0026F9B4 File Offset: 0x0026DBB4
	public void SetSelectedButtonIndex(int index)
	{
		this.m_deckChoiceDataModel.ChoiceClassID = (int)this.m_classByButtonIndex[index];
		this.m_deckChoiceDataModel.ChoiceClassName = GameStrings.GetClassName(this.m_classByButtonIndex[index]);
		DeckTemplateDbfRecord deckTemplateDbfRecord = this.m_deckTemplates.Find((DeckTemplateDbfRecord record) => record.ClassId == this.m_deckChoiceDataModel.ChoiceClassID);
		if (deckTemplateDbfRecord == null)
		{
			Log.MissingAssets.PrintError("Could not find a free deck template for class id = {0}", new object[]
			{
				this.m_deckChoiceDataModel.ChoiceClassID
			});
			return;
		}
		this.m_deckChoiceDataModel.DeckDescription = (string.IsNullOrEmpty(deckTemplateDbfRecord.DeckRecord.AltDescription) ? deckTemplateDbfRecord.DeckRecord.Description : deckTemplateDbfRecord.DeckRecord.AltDescription);
		this.m_chooseDeckWidget.TriggerEvent("UpdateVisuals", default(Widget.TriggerEventParameters));
	}

	// Token: 0x0600775C RID: 30556 RVA: 0x0026FA8C File Offset: 0x0026DC8C
	public void ChoiceConfirmed()
	{
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_FREE_DECK_CONFIRMATION_HEADER"),
			m_text = GameStrings.Format("GLUE_FREE_DECK_CONFIRMATION_TEXT", new object[]
			{
				this.m_deckChoiceDataModel.ChoiceClassName
			}),
			m_showAlertIcon = false,
			m_alertTextAlignment = UberText.AlignmentOptions.Center,
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response == AlertPopup.Response.CONFIRM)
				{
					Network.Get().SendFreeDeckChoice(this.m_deckChoiceDataModel.ChoiceClassID, this.m_noticeId);
					this.m_chooseDeckWidget.TriggerEvent("COMPLETE", default(Widget.TriggerEventParameters));
					return;
				}
				this.m_chooseDeckWidget.TriggerEvent("SHOW", default(Widget.TriggerEventParameters));
			}
		};
		DialogManager.Get().ShowPopup(info);
	}

	// Token: 0x0600775D RID: 30557 RVA: 0x0026FB07 File Offset: 0x0026DD07
	public void SetNoticeId(long noticeId)
	{
		this.m_noticeId = noticeId;
	}

	// Token: 0x0600775E RID: 30558 RVA: 0x0026FB10 File Offset: 0x0026DD10
	private void SetupDataModelForButton(Widget w, int index)
	{
		string buttonClass = this.m_classByButtonIndex[index].ToString();
		DeckChoiceDataModel deckChoiceDataModel = new DeckChoiceDataModel();
		deckChoiceDataModel.ButtonClass = buttonClass;
		this.m_classButtonWidgets[index] = w;
		this.m_buttonDataModels[index] = deckChoiceDataModel;
		w.BindDataModel(deckChoiceDataModel, false);
	}

	// Token: 0x04005D82 RID: 23938
	public AsyncReference m_chooseDeckReference;

	// Token: 0x04005D83 RID: 23939
	public AsyncReference[] m_classButtonReferences;

	// Token: 0x04005D84 RID: 23940
	private DeckChoiceDataModel m_deckChoiceDataModel;

	// Token: 0x04005D85 RID: 23941
	private DeckChoiceDataModel[] m_buttonDataModels;

	// Token: 0x04005D86 RID: 23942
	private Widget[] m_classButtonWidgets;

	// Token: 0x04005D87 RID: 23943
	private Widget m_chooseDeckWidget;

	// Token: 0x04005D88 RID: 23944
	private List<DeckDbfRecord> m_decks;

	// Token: 0x04005D89 RID: 23945
	private List<DeckTemplateDbfRecord> m_deckTemplates;

	// Token: 0x04005D8A RID: 23946
	private Achievement.ClickTriggerType m_currentClickTrigger;

	// Token: 0x04005D8B RID: 23947
	private string m_deckName;

	// Token: 0x04005D8C RID: 23948
	private Action m_completeCallback;

	// Token: 0x04005D8D RID: 23949
	private long m_noticeId;

	// Token: 0x04005D8E RID: 23950
	private TAG_CLASS[] m_classByButtonIndex = new TAG_CLASS[]
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
}
