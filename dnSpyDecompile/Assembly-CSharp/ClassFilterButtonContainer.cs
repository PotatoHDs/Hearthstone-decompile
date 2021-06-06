using System;
using UnityEngine;

// Token: 0x020000F1 RID: 241
public class ClassFilterButtonContainer : MonoBehaviour
{
	// Token: 0x06000DF8 RID: 3576 RVA: 0x0004EEC8 File Offset: 0x0004D0C8
	private void SetCardBacksEnabled(bool enabled)
	{
		this.m_cardBacksButton.SetEnabled(enabled, false);
		this.m_cardBacksDisabled.SetActive(!enabled);
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x0004EEE6 File Offset: 0x0004D0E6
	private void SetHeroSkinsEnabled(bool enabled)
	{
		this.m_heroSkinsButton.SetEnabled(enabled, false);
		this.m_heroSkinsDisabled.SetActive(!enabled);
	}

	// Token: 0x06000DFA RID: 3578 RVA: 0x0004EF04 File Offset: 0x0004D104
	public void SetDefaults()
	{
		this.SetCardBacksEnabled(true);
		this.SetHeroSkinsEnabled(true);
		this.UpdateClassButtons();
	}

	// Token: 0x06000DFB RID: 3579 RVA: 0x0004EF1C File Offset: 0x0004D11C
	public void SetClass(TAG_CLASS classTag)
	{
		int count = CardBackManager.Get().GetCardBacksOwned().Count;
		int count2 = CollectionManager.Get().GetBestHeroesIOwn(classTag).Count;
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		bool flag = editedDeck != null && editedDeck.HasUIHeroOverride();
		this.SetCardBacksEnabled(count > 1);
		this.SetHeroSkinsEnabled(!flag && count2 > 1);
		this.UpdateClassButtons();
	}

	// Token: 0x06000DFC RID: 3580 RVA: 0x0004EF84 File Offset: 0x0004D184
	private void UpdateClassButtons()
	{
		for (int i = 0; i < this.m_classTags.Length; i++)
		{
			this.m_classButtons[i].SetClass(null, this.m_inactiveMaterial);
			this.m_classButtons[i].SetNewCardCount(0);
		}
		CollectionPageManager pageManager = CollectionManager.Get().GetCollectibleDisplay().m_pageManager;
		int num = 0;
		for (int j = 0; j < this.m_classTags.Length; j++)
		{
			if (pageManager.HasClassCardsAvailable(this.m_classTags[j]))
			{
				this.m_classButtons[num].SetClass(new TAG_CLASS?(this.m_classTags[j]), this.m_classMaterials[j]);
				int numNewCardsForClass = pageManager.GetNumNewCardsForClass(this.m_classTags[j]);
				this.m_classButtons[num].SetNewCardCount(numNewCardsForClass);
				num++;
			}
		}
	}

	// Token: 0x040009AD RID: 2477
	public TAG_CLASS[] m_classTags;

	// Token: 0x040009AE RID: 2478
	public ClassFilterButton[] m_classButtons;

	// Token: 0x040009AF RID: 2479
	public Material[] m_classMaterials;

	// Token: 0x040009B0 RID: 2480
	public Material m_inactiveMaterial;

	// Token: 0x040009B1 RID: 2481
	public Material m_templateMaterial;

	// Token: 0x040009B2 RID: 2482
	public PegUIElement m_cardBacksButton;

	// Token: 0x040009B3 RID: 2483
	public PegUIElement m_heroSkinsButton;

	// Token: 0x040009B4 RID: 2484
	public GameObject m_cardBacksDisabled;

	// Token: 0x040009B5 RID: 2485
	public GameObject m_heroSkinsDisabled;
}
