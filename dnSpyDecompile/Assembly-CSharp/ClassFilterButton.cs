using System;
using UnityEngine;

// Token: 0x020000F0 RID: 240
public class ClassFilterButton : PegUIElement
{
	// Token: 0x06000DF2 RID: 3570 RVA: 0x0004ED1C File Offset: 0x0004CF1C
	protected override void Awake()
	{
		this.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.HandleRelease();
		});
		base.Awake();
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x0004ED38 File Offset: 0x0004CF38
	public void HandleRelease()
	{
		switch (this.m_tabViewMode)
		{
		case CollectionUtils.ViewMode.CARDS:
			if (this.m_class != null)
			{
				CollectionManager.Get().GetCollectibleDisplay().m_pageManager.JumpToCollectionClassPage(this.m_class.Value);
			}
			break;
		case CollectionUtils.ViewMode.HERO_SKINS:
			CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.HERO_SKINS, null);
			break;
		case CollectionUtils.ViewMode.CARD_BACKS:
			CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.CARD_BACKS, null);
			break;
		case CollectionUtils.ViewMode.COINS:
			CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.COINS, null);
			break;
		}
		base.GetComponentInParent<SlidingTray>().HideTray();
	}

	// Token: 0x06000DF4 RID: 3572 RVA: 0x0004EDDC File Offset: 0x0004CFDC
	public void SetClass(TAG_CLASS? classTag, Material material)
	{
		this.m_class = classTag;
		base.GetComponent<Renderer>().SetMaterial(material);
		bool flag = this.m_class == null;
		base.GetComponent<BoxCollider>().enabled = !flag;
		base.GetComponent<Renderer>().enabled = !flag;
		if (this.m_newCardCount != null)
		{
			this.m_newCardCount.SetActive(!flag);
		}
		if (this.m_disabled != null)
		{
			this.m_disabled.SetActive(flag);
		}
	}

	// Token: 0x06000DF5 RID: 3573 RVA: 0x0004EE60 File Offset: 0x0004D060
	public void SetNewCardCount(int count)
	{
		if (this.m_newCardCount != null)
		{
			this.m_newCardCount.SetActive(count > 0);
		}
		if (count > 0 && this.m_newCardCountText != null)
		{
			this.m_newCardCountText.Text = GameStrings.Format("GLUE_COLLECTION_NEW_CARD_CALLOUT", new object[]
			{
				count
			});
		}
	}

	// Token: 0x040009A8 RID: 2472
	public GameObject m_newCardCount;

	// Token: 0x040009A9 RID: 2473
	public UberText m_newCardCountText;

	// Token: 0x040009AA RID: 2474
	public GameObject m_disabled;

	// Token: 0x040009AB RID: 2475
	public CollectionUtils.ViewMode m_tabViewMode;

	// Token: 0x040009AC RID: 2476
	private TAG_CLASS? m_class;
}
