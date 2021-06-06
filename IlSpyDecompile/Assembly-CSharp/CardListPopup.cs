using System.Collections.Generic;

[CustomEditClass]
public class CardListPopup : DialogBase
{
	public class Info
	{
		public string m_description;

		public List<CollectibleCard> m_cards;

		public HideCallback m_callbackOnHide;

		public bool m_useMultiLineDescription;
	}

	[CustomEditField(Sections = "Object Links")]
	public CardListPanel m_CardsContainer_SingleLineDescription;

	[CustomEditField(Sections = "Object Links")]
	public CardListPanel m_CardsContainer_MultiLineDescription;

	[CustomEditField(Sections = "Object Links")]
	public UIBButton m_okayButton;

	[CustomEditField(Sections = "Object Links")]
	public UberText m_descriptionSingleLine;

	[CustomEditField(Sections = "Object Links")]
	public UberText m_descriptionMultiLine;

	private Info m_info = new Info();

	protected override void Awake()
	{
		base.Awake();
		m_okayButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			Hide();
		});
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().SetSystemDialogActive(active: false);
		}
	}

	public void SetInfo(Info info)
	{
		m_info = info;
		if (m_info.m_callbackOnHide != null)
		{
			AddHideListener(m_info.m_callbackOnHide);
		}
	}

	public override void Show()
	{
		base.Show();
		DialogBase.DoBlur();
		if (m_info.m_useMultiLineDescription)
		{
			m_CardsContainer_MultiLineDescription.Show(m_info.m_cards);
			m_descriptionMultiLine.Text = m_info.m_description;
		}
		else
		{
			m_CardsContainer_SingleLineDescription.Show(m_info.m_cards);
			m_descriptionSingleLine.Text = m_info.m_description;
		}
		UniversalInputManager.Get().SetSystemDialogActive(active: true);
	}

	public override void Hide()
	{
		base.Hide();
		DialogBase.EndBlur();
	}
}
