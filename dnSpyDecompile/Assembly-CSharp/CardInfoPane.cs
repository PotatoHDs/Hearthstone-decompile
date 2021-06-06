using System;
using PegasusShared;
using UnityEngine;

// Token: 0x020000EF RID: 239
public class CardInfoPane : MonoBehaviour
{
	// Token: 0x06000DEF RID: 3567 RVA: 0x0004EADC File Offset: 0x0004CCDC
	public void UpdateContent()
	{
		EntityDef entityDef;
		TAG_PREMIUM premium;
		if (!CraftingManager.Get().GetShownCardInfo(out entityDef, out premium))
		{
			return;
		}
		TAG_RARITY rarity = entityDef.GetRarity();
		TAG_CARD_SET tag_CARD_SET = entityDef.GetCardSet();
		if (GameUtils.IsLegacySet(tag_CARD_SET))
		{
			tag_CARD_SET = TAG_CARD_SET.LEGACY;
		}
		if (rarity == TAG_RARITY.FREE)
		{
			this.m_rarityLabel.Text = "";
		}
		else
		{
			this.m_rarityLabel.Text = GameStrings.GetRarityText(rarity);
		}
		this.AssignRarityColors(rarity, tag_CARD_SET);
		FormatType cardSetFormat = GameUtils.GetCardSetFormat(tag_CARD_SET);
		this.m_wildTheming.SetActive(cardSetFormat == FormatType.FT_WILD);
		this.m_standardTheming.SetActive(cardSetFormat == FormatType.FT_STANDARD);
		this.m_classicTheming.SetActive(cardSetFormat == FormatType.FT_CLASSIC);
		switch (cardSetFormat)
		{
		case FormatType.FT_WILD:
			this.m_wildRarityGem.SetRarityGem(rarity, tag_CARD_SET);
			break;
		case FormatType.FT_STANDARD:
			this.m_rarityGem.SetRarityGem(rarity, tag_CARD_SET);
			break;
		case FormatType.FT_CLASSIC:
			this.m_classicRarityGem.SetRarityGem(rarity, tag_CARD_SET);
			break;
		}
		this.m_setName.Text = GameStrings.GetCardSetName(tag_CARD_SET);
		this.m_artistName.Text = GameStrings.Format("GLUE_COLLECTION_ARTIST", new object[]
		{
			entityDef.GetArtistName()
		});
		this.m_wildTheming.SetActive(cardSetFormat == FormatType.FT_WILD);
		string text = "<color=#000000ff>" + entityDef.GetFlavorText() + "</color>";
		NetCache.CardValue cardValue = CraftingManager.Get().GetCardValue(entityDef.GetCardId(), premium);
		if (cardValue != null && cardValue.IsOverrideActive())
		{
			if (!string.IsNullOrEmpty(text))
			{
				text += "\n\n";
			}
			text += GameStrings.Get("GLUE_COLLECTION_RECENTLY_NERFED");
		}
		this.m_flavorText.Text = text;
	}

	// Token: 0x06000DF0 RID: 3568 RVA: 0x0004EC74 File Offset: 0x0004CE74
	private void AssignRarityColors(TAG_RARITY rarity, TAG_CARD_SET cardSet)
	{
		switch (rarity)
		{
		default:
			this.m_rarityLabel.TextColor = Color.white;
			return;
		case TAG_RARITY.RARE:
			this.m_rarityLabel.TextColor = new Color(0.11f, 0.33f, 0.8f, 1f);
			return;
		case TAG_RARITY.EPIC:
			this.m_rarityLabel.TextColor = new Color(0.77f, 0.03f, 1f, 1f);
			return;
		case TAG_RARITY.LEGENDARY:
			this.m_rarityLabel.TextColor = new Color(1f, 0.56f, 0f, 1f);
			return;
		}
	}

	// Token: 0x0400099E RID: 2462
	public UberText m_artistName;

	// Token: 0x0400099F RID: 2463
	public UberText m_rarityLabel;

	// Token: 0x040009A0 RID: 2464
	public UberText m_flavorText;

	// Token: 0x040009A1 RID: 2465
	public UberText m_setName;

	// Token: 0x040009A2 RID: 2466
	public GameObject m_standardTheming;

	// Token: 0x040009A3 RID: 2467
	public RarityGem m_rarityGem;

	// Token: 0x040009A4 RID: 2468
	public GameObject m_wildTheming;

	// Token: 0x040009A5 RID: 2469
	public RarityGem m_wildRarityGem;

	// Token: 0x040009A6 RID: 2470
	public GameObject m_classicTheming;

	// Token: 0x040009A7 RID: 2471
	public RarityGem m_classicRarityGem;
}
