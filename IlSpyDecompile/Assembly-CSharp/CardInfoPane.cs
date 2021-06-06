using PegasusShared;
using UnityEngine;

public class CardInfoPane : MonoBehaviour
{
	public UberText m_artistName;

	public UberText m_rarityLabel;

	public UberText m_flavorText;

	public UberText m_setName;

	public GameObject m_standardTheming;

	public RarityGem m_rarityGem;

	public GameObject m_wildTheming;

	public RarityGem m_wildRarityGem;

	public GameObject m_classicTheming;

	public RarityGem m_classicRarityGem;

	public void UpdateContent()
	{
		if (!CraftingManager.Get().GetShownCardInfo(out var entityDef, out var premium))
		{
			return;
		}
		TAG_RARITY rarity = entityDef.GetRarity();
		TAG_CARD_SET tAG_CARD_SET = entityDef.GetCardSet();
		if (GameUtils.IsLegacySet(tAG_CARD_SET))
		{
			tAG_CARD_SET = TAG_CARD_SET.LEGACY;
		}
		if (rarity == TAG_RARITY.FREE)
		{
			m_rarityLabel.Text = "";
		}
		else
		{
			m_rarityLabel.Text = GameStrings.GetRarityText(rarity);
		}
		AssignRarityColors(rarity, tAG_CARD_SET);
		FormatType cardSetFormat = GameUtils.GetCardSetFormat(tAG_CARD_SET);
		m_wildTheming.SetActive(cardSetFormat == FormatType.FT_WILD);
		m_standardTheming.SetActive(cardSetFormat == FormatType.FT_STANDARD);
		m_classicTheming.SetActive(cardSetFormat == FormatType.FT_CLASSIC);
		switch (cardSetFormat)
		{
		case FormatType.FT_STANDARD:
			m_rarityGem.SetRarityGem(rarity, tAG_CARD_SET);
			break;
		case FormatType.FT_WILD:
			m_wildRarityGem.SetRarityGem(rarity, tAG_CARD_SET);
			break;
		case FormatType.FT_CLASSIC:
			m_classicRarityGem.SetRarityGem(rarity, tAG_CARD_SET);
			break;
		}
		m_setName.Text = GameStrings.GetCardSetName(tAG_CARD_SET);
		m_artistName.Text = GameStrings.Format("GLUE_COLLECTION_ARTIST", entityDef.GetArtistName());
		m_wildTheming.SetActive(cardSetFormat == FormatType.FT_WILD);
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
		m_flavorText.Text = text;
	}

	private void AssignRarityColors(TAG_RARITY rarity, TAG_CARD_SET cardSet)
	{
		switch (rarity)
		{
		default:
			m_rarityLabel.TextColor = Color.white;
			break;
		case TAG_RARITY.RARE:
			m_rarityLabel.TextColor = new Color(0.11f, 0.33f, 0.8f, 1f);
			break;
		case TAG_RARITY.EPIC:
			m_rarityLabel.TextColor = new Color(0.77f, 0.03f, 1f, 1f);
			break;
		case TAG_RARITY.LEGENDARY:
			m_rarityLabel.TextColor = new Color(1f, 0.56f, 0f, 1f);
			break;
		}
	}
}
