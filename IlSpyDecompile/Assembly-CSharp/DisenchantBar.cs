using System;
using UnityEngine;

[Serializable]
public class DisenchantBar
{
	public TAG_PREMIUM m_premiumType;

	public TAG_RARITY m_rarity;

	public UberText m_typeText;

	public UberText m_amountText;

	public UberText m_numCardsText;

	public GameObject m_amountBar;

	public GameObject m_dustJar;

	public GameObject m_rarityGem;

	public GameObject m_glow;

	public UberText m_numGoldText;

	public MeshRenderer m_barFrameMesh;

	private int m_numCards;

	private int m_amount;

	private int m_numGoldCards;

	public const float NUM_CARDS_TEXT_CENTER_X = 2.902672f;

	public void Reset()
	{
		m_numCards = 0;
		m_amount = 0;
		m_numGoldCards = 0;
		SetPercent(0f);
	}

	public void AddCards(int count, int sellAmount, TAG_PREMIUM premiumType)
	{
		m_numCards += count;
		if (m_premiumType != premiumType)
		{
			m_numGoldCards += count;
		}
		m_amount += sellAmount;
	}

	public void Init()
	{
		if (m_typeText != null)
		{
			string rarityText = GameStrings.GetRarityText(m_rarity);
			m_typeText.Text = ((TAG_PREMIUM.GOLDEN == m_premiumType) ? GameStrings.Format("GLUE_MASS_DISENCHANT_PREMIUM_TITLE", rarityText) : rarityText);
		}
	}

	public int GetNumCards()
	{
		return m_numCards;
	}

	public int GetAmountDust()
	{
		return m_amount;
	}

	public void UpdateVisuals(int totalNumCards)
	{
		m_numCardsText.Text = m_numCards.ToString();
		m_amountText.Text = m_amount.ToString();
		if (m_numGoldText != null)
		{
			if (m_numGoldCards > 0)
			{
				m_numGoldText.gameObject.SetActive(value: true);
				m_numGoldText.Text = GameStrings.Format("GLUE_MASS_DISENCHANT_NUM_GOLDEN_CARDS", m_numGoldCards.ToString());
				TransformUtil.SetLocalPosX(m_numCardsText, 2.902672f);
				m_barFrameMesh.GetComponent<MeshFilter>().mesh = MassDisenchant.Get().m_rarityBarGoldMesh;
				m_barFrameMesh.SetMaterial(MassDisenchant.Get().m_rarityBarGoldMaterial);
			}
			else
			{
				m_numGoldText.gameObject.SetActive(value: false);
				TransformUtil.SetLocalPosX(m_numCardsText, 2.902672f);
				m_barFrameMesh.GetComponent<MeshFilter>().mesh = MassDisenchant.Get().m_rarityBarNormalMesh;
				m_barFrameMesh.SetMaterial(MassDisenchant.Get().m_rarityBarNormalMaterial);
			}
		}
		float percent = (((float)totalNumCards > 0f) ? ((float)m_numCards / (float)totalNumCards) : 0f);
		SetPercent(percent);
	}

	public void SetPercent(float percent)
	{
		m_amountBar.GetComponent<Renderer>().GetMaterial().SetFloat("_Percent", percent);
	}
}
