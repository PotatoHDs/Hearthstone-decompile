using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class DeckTemplatePickerButton : PegUIElement
{
	public MeshRenderer m_deckTexture;

	public MeshRenderer m_packRibbon;

	public GameObject m_selectGlow;

	public UberText m_title;

	public List<UberText> m_cardCountTexts = new List<UberText>();

	public GameObject m_incompleteTextRibbon;

	public GameObject m_completeTextRibbon;

	public static readonly int s_MinimumRecommendedSize = 25;

	private bool m_isCoreDeck;

	private int m_ownedCardCount;

	public void SetIsCoreDeck(bool isCore)
	{
		m_isCoreDeck = isCore;
	}

	public bool IsCoreDeck()
	{
		return m_isCoreDeck;
	}

	public void SetSelected(bool selected)
	{
		if (m_selectGlow != null)
		{
			m_selectGlow.SetActive(selected);
		}
	}

	public void SetTitleText(string titleText)
	{
		if (m_title != null)
		{
			m_title.Text = titleText;
		}
	}

	public void SetCardCountText(int count)
	{
		m_ownedCardCount = count;
		int deckSize = CollectionManager.Get().GetDeckSize();
		foreach (UberText cardCountText in m_cardCountTexts)
		{
			cardCountText.Text = $"{count}/{deckSize}";
		}
		bool flag = count < s_MinimumRecommendedSize && !m_isCoreDeck;
		if (m_incompleteTextRibbon != null)
		{
			m_incompleteTextRibbon.SetActive(flag);
		}
		if (m_completeTextRibbon != null)
		{
			m_completeTextRibbon.SetActive(!flag);
		}
	}

	public int GetOwnedCardCount()
	{
		return m_ownedCardCount;
	}

	public void SetDeckRecipeArt(string materialPath)
	{
		if (!(m_deckTexture == null) && !string.IsNullOrEmpty(materialPath))
		{
			AssetLoader.Get().LoadMaterial(materialPath, SetDeckMaterial);
		}
	}

	private void SetDeckMaterial(AssetReference assetRef, Object obj, object callbackData)
	{
		Material material = obj as Material;
		if (material != null)
		{
			m_deckTexture.SetMaterial(material);
		}
	}
}
