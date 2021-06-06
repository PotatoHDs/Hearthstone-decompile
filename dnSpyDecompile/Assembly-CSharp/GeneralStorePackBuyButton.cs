using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006FD RID: 1789
[CustomEditClass]
public class GeneralStorePackBuyButton : PegUIElement
{
	// Token: 0x060063E6 RID: 25574 RVA: 0x002086E2 File Offset: 0x002068E2
	public bool IsSelected()
	{
		return this.m_selected;
	}

	// Token: 0x060063E7 RID: 25575 RVA: 0x002086EA File Offset: 0x002068EA
	public void Select()
	{
		if (this.m_selected)
		{
			return;
		}
		this.m_selected = true;
		this.UpdateButtonState();
	}

	// Token: 0x060063E8 RID: 25576 RVA: 0x00208702 File Offset: 0x00206902
	public void Unselect()
	{
		if (!this.m_selected)
		{
			return;
		}
		this.m_selected = false;
		this.UpdateButtonState();
	}

	// Token: 0x060063E9 RID: 25577 RVA: 0x0020871C File Offset: 0x0020691C
	public void UpdateFromGTAPP(NoGTAPPTransactionData noGTAPPGoldPrice)
	{
		string quantityText = string.Empty;
		long goldCost;
		if (StoreManager.Get().GetGoldCostNoGTAPP(noGTAPPGoldPrice, out goldCost))
		{
			quantityText = StoreManager.Get().GetProductQuantityText(noGTAPPGoldPrice.Product, noGTAPPGoldPrice.ProductData, noGTAPPGoldPrice.Quantity, 0);
		}
		this.SetGoldValue(goldCost, quantityText);
	}

	// Token: 0x060063EA RID: 25578 RVA: 0x00208764 File Offset: 0x00206964
	public void SetGoldValue(long goldCost, string quantityText)
	{
		if (this.m_fullText != null)
		{
			this.m_quantityText.gameObject.SetActive(true);
			this.m_costText.gameObject.SetActive(true);
			this.m_fullText.gameObject.SetActive(false);
		}
		this.m_costText.Text = goldCost.ToString();
		this.m_costText.TextColor = this.m_goldCostTextColor;
		this.m_quantityText.Text = quantityText;
		this.m_quantityText.TextColor = this.m_goldQuantityTextColor;
		this.m_isGold = true;
		this.UpdateButtonState();
	}

	// Token: 0x060063EB RID: 25579 RVA: 0x00208800 File Offset: 0x00206A00
	public void SetMoneyValue(Network.Bundle bundle, Network.BundleItem packsBundleItem, string quantityText)
	{
		if (bundle != null && !StoreManager.Get().IsProductAlreadyOwned(bundle))
		{
			if (this.m_fullText != null)
			{
				this.m_quantityText.gameObject.SetActive(true);
				this.m_costText.gameObject.SetActive(true);
				this.m_fullText.gameObject.SetActive(false);
			}
			this.m_costText.Text = StoreManager.Get().FormatCostBundle(bundle);
			this.m_costText.TextColor = this.m_moneyCostTextColor;
			this.m_costText.Outline = false;
			this.m_quantityText.Text = quantityText;
			this.m_quantityText.TextColor = this.m_moneyQuantityTextColor;
			this.m_quantityText.Outline = false;
			if (packsBundleItem != null && packsBundleItem.BaseQuantity > 0)
			{
				this.m_quantityText.TextColor = this.m_moneyQuantityBonusPacksTextColor;
				this.m_quantityText.Outline = true;
				this.m_quantityText.OutlineSize = (float)this.m_moneyQuantityBonusPacksTextOutlineSize;
			}
		}
		else
		{
			this.m_costText.Text = string.Empty;
			UberText uberText = this.m_quantityText;
			if (this.m_fullText != null)
			{
				this.m_quantityText.gameObject.SetActive(false);
				this.m_costText.gameObject.SetActive(false);
				this.m_fullText.gameObject.SetActive(true);
				uberText = this.m_fullText;
			}
			uberText.Text = GameStrings.Get("GLUE_STORE_PACK_BUTTON_TEXT_PURCHASED");
		}
		this.m_isGold = false;
		this.UpdateButtonState();
	}

	// Token: 0x060063EC RID: 25580 RVA: 0x00208980 File Offset: 0x00206B80
	private void UpdateButtonState()
	{
		if (this.m_goldIcon != null)
		{
			this.m_goldIcon.SetActive(this.m_isGold);
		}
		Vector2 value = Vector2.zero;
		if (this.m_isGold)
		{
			value = (this.m_selected ? this.m_goldBtnDownMatOffset : this.m_goldBtnMatOffset);
		}
		else
		{
			value = (this.m_selected ? this.m_moneyBtnDownMatOffset : this.m_moneyBtnMatOffset);
		}
		foreach (Renderer renderer in this.m_buttonRenderers)
		{
			renderer.GetMaterial(this.m_materialIndex).SetTextureOffset(this.m_materialPropName, value);
		}
		if (this.m_selectGlow != null)
		{
			this.m_selectGlow.SetActive(this.m_selected);
		}
	}

	// Token: 0x060063ED RID: 25581 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected override void OnDoubleClick()
	{
	}

	// Token: 0x040052DF RID: 21215
	public UberText m_quantityText;

	// Token: 0x040052E0 RID: 21216
	public UberText m_costText;

	// Token: 0x040052E1 RID: 21217
	public UberText m_fullText;

	// Token: 0x040052E2 RID: 21218
	public Color m_goldQuantityTextColor;

	// Token: 0x040052E3 RID: 21219
	public Color m_moneyQuantityTextColor;

	// Token: 0x040052E4 RID: 21220
	public Color m_moneyQuantityBonusPacksTextColor;

	// Token: 0x040052E5 RID: 21221
	public int m_moneyQuantityBonusPacksTextOutlineSize;

	// Token: 0x040052E6 RID: 21222
	public Color m_goldCostTextColor;

	// Token: 0x040052E7 RID: 21223
	public Color m_moneyCostTextColor;

	// Token: 0x040052E8 RID: 21224
	public GameObject m_goldIcon;

	// Token: 0x040052E9 RID: 21225
	public GameObject m_selectGlow;

	// Token: 0x040052EA RID: 21226
	public List<Renderer> m_buttonRenderers = new List<Renderer>();

	// Token: 0x040052EB RID: 21227
	public int m_materialIndex;

	// Token: 0x040052EC RID: 21228
	public string m_materialPropName = "_MainTex";

	// Token: 0x040052ED RID: 21229
	public Vector2 m_goldBtnMatOffset;

	// Token: 0x040052EE RID: 21230
	public Vector2 m_goldBtnDownMatOffset;

	// Token: 0x040052EF RID: 21231
	public Vector2 m_moneyBtnMatOffset;

	// Token: 0x040052F0 RID: 21232
	public Vector2 m_moneyBtnDownMatOffset;

	// Token: 0x040052F1 RID: 21233
	private bool m_selected;

	// Token: 0x040052F2 RID: 21234
	private bool m_isGold;
}
