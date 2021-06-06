using UnityEngine;

public class ClassFilterButtonContainer : MonoBehaviour
{
	public TAG_CLASS[] m_classTags;

	public ClassFilterButton[] m_classButtons;

	public Material[] m_classMaterials;

	public Material m_inactiveMaterial;

	public Material m_templateMaterial;

	public PegUIElement m_cardBacksButton;

	public PegUIElement m_heroSkinsButton;

	public GameObject m_cardBacksDisabled;

	public GameObject m_heroSkinsDisabled;

	private void SetCardBacksEnabled(bool enabled)
	{
		m_cardBacksButton.SetEnabled(enabled);
		m_cardBacksDisabled.SetActive(!enabled);
	}

	private void SetHeroSkinsEnabled(bool enabled)
	{
		m_heroSkinsButton.SetEnabled(enabled);
		m_heroSkinsDisabled.SetActive(!enabled);
	}

	public void SetDefaults()
	{
		SetCardBacksEnabled(enabled: true);
		SetHeroSkinsEnabled(enabled: true);
		UpdateClassButtons();
	}

	public void SetClass(TAG_CLASS classTag)
	{
		int count = CardBackManager.Get().GetCardBacksOwned().Count;
		int count2 = CollectionManager.Get().GetBestHeroesIOwn(classTag).Count;
		bool flag = CollectionManager.Get().GetEditedDeck()?.HasUIHeroOverride() ?? false;
		SetCardBacksEnabled(count > 1);
		SetHeroSkinsEnabled(!flag && count2 > 1);
		UpdateClassButtons();
	}

	private void UpdateClassButtons()
	{
		for (int i = 0; i < m_classTags.Length; i++)
		{
			m_classButtons[i].SetClass(null, m_inactiveMaterial);
			m_classButtons[i].SetNewCardCount(0);
		}
		CollectionPageManager pageManager = CollectionManager.Get().GetCollectibleDisplay().m_pageManager;
		int num = 0;
		for (int j = 0; j < m_classTags.Length; j++)
		{
			if (pageManager.HasClassCardsAvailable(m_classTags[j]))
			{
				m_classButtons[num].SetClass(m_classTags[j], m_classMaterials[j]);
				int numNewCardsForClass = pageManager.GetNumNewCardsForClass(m_classTags[j]);
				m_classButtons[num].SetNewCardCount(numNewCardsForClass);
				num++;
			}
		}
	}
}
