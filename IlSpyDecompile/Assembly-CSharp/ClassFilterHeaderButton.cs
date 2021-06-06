using UnityEngine;

public class ClassFilterHeaderButton : PegUIElement
{
	public SlidingTray m_classFilterTray;

	public UberText m_headerText;

	public Transform m_showTwoRowsBone;

	public ClassFilterButtonContainer m_container;

	private ClassFilterButton[] m_buttons;

	protected override void Awake()
	{
		AddEventListener(UIEventType.RELEASE, delegate
		{
			HandleRelease();
		});
		base.Awake();
	}

	public void HandleRelease()
	{
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.HideDeckHelpPopup();
		}
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		bool num = editedDeck != null;
		if (m_buttons == null)
		{
			m_buttons = m_classFilterTray.GetComponentsInChildren<ClassFilterButton>();
		}
		if (!num)
		{
			m_container.SetDefaults();
		}
		else
		{
			TAG_CLASS @class = editedDeck.GetClass();
			m_container.SetClass(@class);
		}
		m_classFilterTray.ToggleTraySlider(show: true, m_showTwoRowsBone);
		NotificationManager.Get().DestroyAllPopUps();
	}

	public void SetMode(CollectionUtils.ViewMode mode, TAG_CLASS? classTag)
	{
		Log.CollectionManager.Print("transitionPageId={0} mode={1} classTag={2}", CollectionManager.Get().GetCollectibleDisplay().m_pageManager.GetTransitionPageId(), mode, classTag);
		switch (mode)
		{
		case CollectionUtils.ViewMode.CARD_BACKS:
			m_headerText.Text = GameStrings.Get("GLUE_COLLECTION_MANAGER_CARD_BACKS_TITLE");
			return;
		case CollectionUtils.ViewMode.HERO_SKINS:
			m_headerText.Text = GameStrings.Get("GLUE_COLLECTION_MANAGER_HERO_SKINS_TITLE");
			return;
		case CollectionUtils.ViewMode.COINS:
			m_headerText.Text = GameStrings.Get("GLUE_COLLECTION_MANAGER_COIN_TITLE");
			return;
		}
		if (classTag.HasValue)
		{
			m_headerText.Text = GameStrings.GetClassName(classTag.Value);
		}
		else
		{
			m_headerText.Text = "";
		}
	}
}
