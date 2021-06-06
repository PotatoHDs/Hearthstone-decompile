using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000128 RID: 296
[CustomEditClass]
public class DeckTemplatePickerButton : PegUIElement
{
	// Token: 0x060013AC RID: 5036 RVA: 0x000712D7 File Offset: 0x0006F4D7
	public void SetIsCoreDeck(bool isCore)
	{
		this.m_isCoreDeck = isCore;
	}

	// Token: 0x060013AD RID: 5037 RVA: 0x000712E0 File Offset: 0x0006F4E0
	public bool IsCoreDeck()
	{
		return this.m_isCoreDeck;
	}

	// Token: 0x060013AE RID: 5038 RVA: 0x000712E8 File Offset: 0x0006F4E8
	public void SetSelected(bool selected)
	{
		if (this.m_selectGlow != null)
		{
			this.m_selectGlow.SetActive(selected);
		}
	}

	// Token: 0x060013AF RID: 5039 RVA: 0x00071304 File Offset: 0x0006F504
	public void SetTitleText(string titleText)
	{
		if (this.m_title != null)
		{
			this.m_title.Text = titleText;
		}
	}

	// Token: 0x060013B0 RID: 5040 RVA: 0x00071320 File Offset: 0x0006F520
	public void SetCardCountText(int count)
	{
		this.m_ownedCardCount = count;
		int deckSize = CollectionManager.Get().GetDeckSize();
		foreach (UberText uberText in this.m_cardCountTexts)
		{
			uberText.Text = string.Format("{0}/{1}", count, deckSize);
		}
		bool flag = count < DeckTemplatePickerButton.s_MinimumRecommendedSize && !this.m_isCoreDeck;
		if (this.m_incompleteTextRibbon != null)
		{
			this.m_incompleteTextRibbon.SetActive(flag);
		}
		if (this.m_completeTextRibbon != null)
		{
			this.m_completeTextRibbon.SetActive(!flag);
		}
	}

	// Token: 0x060013B1 RID: 5041 RVA: 0x000713E4 File Offset: 0x0006F5E4
	public int GetOwnedCardCount()
	{
		return this.m_ownedCardCount;
	}

	// Token: 0x060013B2 RID: 5042 RVA: 0x000713EC File Offset: 0x0006F5EC
	public void SetDeckRecipeArt(string materialPath)
	{
		if (this.m_deckTexture == null)
		{
			return;
		}
		if (string.IsNullOrEmpty(materialPath))
		{
			return;
		}
		AssetLoader.Get().LoadMaterial(materialPath, new AssetLoader.ObjectCallback(this.SetDeckMaterial), null, false, false);
	}

	// Token: 0x060013B3 RID: 5043 RVA: 0x00071428 File Offset: 0x0006F628
	private void SetDeckMaterial(AssetReference assetRef, UnityEngine.Object obj, object callbackData)
	{
		Material material = obj as Material;
		if (material != null)
		{
			this.m_deckTexture.SetMaterial(material);
		}
	}

	// Token: 0x04000CDF RID: 3295
	public MeshRenderer m_deckTexture;

	// Token: 0x04000CE0 RID: 3296
	public MeshRenderer m_packRibbon;

	// Token: 0x04000CE1 RID: 3297
	public GameObject m_selectGlow;

	// Token: 0x04000CE2 RID: 3298
	public UberText m_title;

	// Token: 0x04000CE3 RID: 3299
	public List<UberText> m_cardCountTexts = new List<UberText>();

	// Token: 0x04000CE4 RID: 3300
	public GameObject m_incompleteTextRibbon;

	// Token: 0x04000CE5 RID: 3301
	public GameObject m_completeTextRibbon;

	// Token: 0x04000CE6 RID: 3302
	public static readonly int s_MinimumRecommendedSize = 25;

	// Token: 0x04000CE7 RID: 3303
	private bool m_isCoreDeck;

	// Token: 0x04000CE8 RID: 3304
	private int m_ownedCardCount;
}
