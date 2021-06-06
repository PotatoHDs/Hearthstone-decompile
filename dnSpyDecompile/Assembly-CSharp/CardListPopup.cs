using System;
using System.Collections.Generic;

// Token: 0x02000AFD RID: 2813
[CustomEditClass]
public class CardListPopup : DialogBase
{
	// Token: 0x060095D1 RID: 38353 RVA: 0x0030863B File Offset: 0x0030683B
	protected override void Awake()
	{
		base.Awake();
		this.m_okayButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.Hide();
		});
	}

	// Token: 0x060095D2 RID: 38354 RVA: 0x0030865C File Offset: 0x0030685C
	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().SetSystemDialogActive(false);
		}
	}

	// Token: 0x060095D3 RID: 38355 RVA: 0x00308676 File Offset: 0x00306876
	public void SetInfo(CardListPopup.Info info)
	{
		this.m_info = info;
		if (this.m_info.m_callbackOnHide != null)
		{
			base.AddHideListener(this.m_info.m_callbackOnHide);
		}
	}

	// Token: 0x060095D4 RID: 38356 RVA: 0x003086A0 File Offset: 0x003068A0
	public override void Show()
	{
		base.Show();
		DialogBase.DoBlur();
		if (this.m_info.m_useMultiLineDescription)
		{
			this.m_CardsContainer_MultiLineDescription.Show(this.m_info.m_cards);
			this.m_descriptionMultiLine.Text = this.m_info.m_description;
		}
		else
		{
			this.m_CardsContainer_SingleLineDescription.Show(this.m_info.m_cards);
			this.m_descriptionSingleLine.Text = this.m_info.m_description;
		}
		UniversalInputManager.Get().SetSystemDialogActive(true);
	}

	// Token: 0x060095D5 RID: 38357 RVA: 0x002FCDB3 File Offset: 0x002FAFB3
	public override void Hide()
	{
		base.Hide();
		DialogBase.EndBlur();
	}

	// Token: 0x04007D8E RID: 32142
	[CustomEditField(Sections = "Object Links")]
	public CardListPanel m_CardsContainer_SingleLineDescription;

	// Token: 0x04007D8F RID: 32143
	[CustomEditField(Sections = "Object Links")]
	public CardListPanel m_CardsContainer_MultiLineDescription;

	// Token: 0x04007D90 RID: 32144
	[CustomEditField(Sections = "Object Links")]
	public UIBButton m_okayButton;

	// Token: 0x04007D91 RID: 32145
	[CustomEditField(Sections = "Object Links")]
	public UberText m_descriptionSingleLine;

	// Token: 0x04007D92 RID: 32146
	[CustomEditField(Sections = "Object Links")]
	public UberText m_descriptionMultiLine;

	// Token: 0x04007D93 RID: 32147
	private CardListPopup.Info m_info = new CardListPopup.Info();

	// Token: 0x02002745 RID: 10053
	public class Info
	{
		// Token: 0x0400F3CB RID: 62411
		public string m_description;

		// Token: 0x0400F3CC RID: 62412
		public List<CollectibleCard> m_cards;

		// Token: 0x0400F3CD RID: 62413
		public DialogBase.HideCallback m_callbackOnHide;

		// Token: 0x0400F3CE RID: 62414
		public bool m_useMultiLineDescription;
	}
}
