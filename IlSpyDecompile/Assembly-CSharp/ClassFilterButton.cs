using UnityEngine;

public class ClassFilterButton : PegUIElement
{
	public GameObject m_newCardCount;

	public UberText m_newCardCountText;

	public GameObject m_disabled;

	public CollectionUtils.ViewMode m_tabViewMode;

	private TAG_CLASS? m_class;

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
		switch (m_tabViewMode)
		{
		case CollectionUtils.ViewMode.CARDS:
			if (m_class.HasValue)
			{
				CollectionManager.Get().GetCollectibleDisplay().m_pageManager.JumpToCollectionClassPage(m_class.Value);
			}
			break;
		case CollectionUtils.ViewMode.HERO_SKINS:
			CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.HERO_SKINS);
			break;
		case CollectionUtils.ViewMode.CARD_BACKS:
			CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.CARD_BACKS);
			break;
		case CollectionUtils.ViewMode.COINS:
			CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.COINS);
			break;
		}
		GetComponentInParent<SlidingTray>().HideTray();
	}

	public void SetClass(TAG_CLASS? classTag, Material material)
	{
		m_class = classTag;
		GetComponent<Renderer>().SetMaterial(material);
		bool flag = !m_class.HasValue;
		GetComponent<BoxCollider>().enabled = !flag;
		GetComponent<Renderer>().enabled = !flag;
		if (m_newCardCount != null)
		{
			m_newCardCount.SetActive(!flag);
		}
		if (m_disabled != null)
		{
			m_disabled.SetActive(flag);
		}
	}

	public void SetNewCardCount(int count)
	{
		if (m_newCardCount != null)
		{
			m_newCardCount.SetActive(count > 0);
		}
		if (count > 0 && m_newCardCountText != null)
		{
			m_newCardCountText.Text = GameStrings.Format("GLUE_COLLECTION_NEW_CARD_CALLOUT", count);
		}
	}
}
