using System;
using UnityEngine;

// Token: 0x020000F2 RID: 242
public class ClassFilterHeaderButton : PegUIElement
{
	// Token: 0x06000DFE RID: 3582 RVA: 0x0004F050 File Offset: 0x0004D250
	protected override void Awake()
	{
		this.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.HandleRelease();
		});
		base.Awake();
	}

	// Token: 0x06000DFF RID: 3583 RVA: 0x0004F06C File Offset: 0x0004D26C
	public void HandleRelease()
	{
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.HideDeckHelpPopup();
		}
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		bool flag = editedDeck != null;
		if (this.m_buttons == null)
		{
			this.m_buttons = this.m_classFilterTray.GetComponentsInChildren<ClassFilterButton>();
		}
		if (!flag)
		{
			this.m_container.SetDefaults();
		}
		else
		{
			TAG_CLASS @class = editedDeck.GetClass();
			this.m_container.SetClass(@class);
		}
		this.m_classFilterTray.ToggleTraySlider(true, this.m_showTwoRowsBone, true);
		NotificationManager.Get().DestroyAllPopUps();
	}

	// Token: 0x06000E00 RID: 3584 RVA: 0x0004F100 File Offset: 0x0004D300
	public void SetMode(CollectionUtils.ViewMode mode, TAG_CLASS? classTag)
	{
		Log.CollectionManager.Print("transitionPageId={0} mode={1} classTag={2}", new object[]
		{
			CollectionManager.Get().GetCollectibleDisplay().m_pageManager.GetTransitionPageId(),
			mode,
			classTag
		});
		if (mode == CollectionUtils.ViewMode.CARD_BACKS)
		{
			this.m_headerText.Text = GameStrings.Get("GLUE_COLLECTION_MANAGER_CARD_BACKS_TITLE");
			return;
		}
		if (mode == CollectionUtils.ViewMode.HERO_SKINS)
		{
			this.m_headerText.Text = GameStrings.Get("GLUE_COLLECTION_MANAGER_HERO_SKINS_TITLE");
			return;
		}
		if (mode == CollectionUtils.ViewMode.COINS)
		{
			this.m_headerText.Text = GameStrings.Get("GLUE_COLLECTION_MANAGER_COIN_TITLE");
			return;
		}
		if (classTag != null)
		{
			this.m_headerText.Text = GameStrings.GetClassName(classTag.Value);
			return;
		}
		this.m_headerText.Text = "";
	}

	// Token: 0x040009B6 RID: 2486
	public SlidingTray m_classFilterTray;

	// Token: 0x040009B7 RID: 2487
	public UberText m_headerText;

	// Token: 0x040009B8 RID: 2488
	public Transform m_showTwoRowsBone;

	// Token: 0x040009B9 RID: 2489
	public ClassFilterButtonContainer m_container;

	// Token: 0x040009BA RID: 2490
	private ClassFilterButton[] m_buttons;
}
