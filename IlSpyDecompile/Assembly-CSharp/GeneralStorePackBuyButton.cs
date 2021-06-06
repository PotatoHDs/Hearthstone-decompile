using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class GeneralStorePackBuyButton : PegUIElement
{
	public UberText m_quantityText;

	public UberText m_costText;

	public UberText m_fullText;

	public Color m_goldQuantityTextColor;

	public Color m_moneyQuantityTextColor;

	public Color m_moneyQuantityBonusPacksTextColor;

	public int m_moneyQuantityBonusPacksTextOutlineSize;

	public Color m_goldCostTextColor;

	public Color m_moneyCostTextColor;

	public GameObject m_goldIcon;

	public GameObject m_selectGlow;

	public List<Renderer> m_buttonRenderers = new List<Renderer>();

	public int m_materialIndex;

	public string m_materialPropName = "_MainTex";

	public Vector2 m_goldBtnMatOffset;

	public Vector2 m_goldBtnDownMatOffset;

	public Vector2 m_moneyBtnMatOffset;

	public Vector2 m_moneyBtnDownMatOffset;

	private bool m_selected;

	private bool m_isGold;

	public bool IsSelected()
	{
		return m_selected;
	}

	public void Select()
	{
		if (!m_selected)
		{
			m_selected = true;
			UpdateButtonState();
		}
	}

	public void Unselect()
	{
		if (m_selected)
		{
			m_selected = false;
			UpdateButtonState();
		}
	}

	public void UpdateFromGTAPP(NoGTAPPTransactionData noGTAPPGoldPrice)
	{
		string quantityText = string.Empty;
		if (StoreManager.Get().GetGoldCostNoGTAPP(noGTAPPGoldPrice, out var cost))
		{
			quantityText = StoreManager.Get().GetProductQuantityText(noGTAPPGoldPrice.Product, noGTAPPGoldPrice.ProductData, noGTAPPGoldPrice.Quantity, 0);
		}
		SetGoldValue(cost, quantityText);
	}

	public void SetGoldValue(long goldCost, string quantityText)
	{
		if (m_fullText != null)
		{
			m_quantityText.gameObject.SetActive(value: true);
			m_costText.gameObject.SetActive(value: true);
			m_fullText.gameObject.SetActive(value: false);
		}
		m_costText.Text = goldCost.ToString();
		m_costText.TextColor = m_goldCostTextColor;
		m_quantityText.Text = quantityText;
		m_quantityText.TextColor = m_goldQuantityTextColor;
		m_isGold = true;
		UpdateButtonState();
	}

	public void SetMoneyValue(Network.Bundle bundle, Network.BundleItem packsBundleItem, string quantityText)
	{
		if (bundle != null && !StoreManager.Get().IsProductAlreadyOwned(bundle))
		{
			if (m_fullText != null)
			{
				m_quantityText.gameObject.SetActive(value: true);
				m_costText.gameObject.SetActive(value: true);
				m_fullText.gameObject.SetActive(value: false);
			}
			m_costText.Text = StoreManager.Get().FormatCostBundle(bundle);
			m_costText.TextColor = m_moneyCostTextColor;
			m_costText.Outline = false;
			m_quantityText.Text = quantityText;
			m_quantityText.TextColor = m_moneyQuantityTextColor;
			m_quantityText.Outline = false;
			if (packsBundleItem != null && packsBundleItem.BaseQuantity > 0)
			{
				m_quantityText.TextColor = m_moneyQuantityBonusPacksTextColor;
				m_quantityText.Outline = true;
				m_quantityText.OutlineSize = m_moneyQuantityBonusPacksTextOutlineSize;
			}
		}
		else
		{
			m_costText.Text = string.Empty;
			UberText uberText = m_quantityText;
			if (m_fullText != null)
			{
				m_quantityText.gameObject.SetActive(value: false);
				m_costText.gameObject.SetActive(value: false);
				m_fullText.gameObject.SetActive(value: true);
				uberText = m_fullText;
			}
			uberText.Text = GameStrings.Get("GLUE_STORE_PACK_BUTTON_TEXT_PURCHASED");
		}
		m_isGold = false;
		UpdateButtonState();
	}

	private void UpdateButtonState()
	{
		if (m_goldIcon != null)
		{
			m_goldIcon.SetActive(m_isGold);
		}
		Vector2 zero = Vector2.zero;
		zero = ((!m_isGold) ? (m_selected ? m_moneyBtnDownMatOffset : m_moneyBtnMatOffset) : (m_selected ? m_goldBtnDownMatOffset : m_goldBtnMatOffset));
		foreach (Renderer buttonRenderer in m_buttonRenderers)
		{
			buttonRenderer.GetMaterial(m_materialIndex).SetTextureOffset(m_materialPropName, zero);
		}
		if (m_selectGlow != null)
		{
			m_selectGlow.SetActive(m_selected);
		}
	}

	protected override void OnDoubleClick()
	{
	}
}
