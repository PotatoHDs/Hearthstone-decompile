using System;
using UnityEngine;

// Token: 0x0200012F RID: 303
[Serializable]
public class DisenchantBar
{
	// Token: 0x0600142B RID: 5163 RVA: 0x00073A7D File Offset: 0x00071C7D
	public void Reset()
	{
		this.m_numCards = 0;
		this.m_amount = 0;
		this.m_numGoldCards = 0;
		this.SetPercent(0f);
	}

	// Token: 0x0600142C RID: 5164 RVA: 0x00073A9F File Offset: 0x00071C9F
	public void AddCards(int count, int sellAmount, TAG_PREMIUM premiumType)
	{
		this.m_numCards += count;
		if (this.m_premiumType != premiumType)
		{
			this.m_numGoldCards += count;
		}
		this.m_amount += sellAmount;
	}

	// Token: 0x0600142D RID: 5165 RVA: 0x00073AD4 File Offset: 0x00071CD4
	public void Init()
	{
		if (this.m_typeText != null)
		{
			string rarityText = GameStrings.GetRarityText(this.m_rarity);
			this.m_typeText.Text = ((TAG_PREMIUM.GOLDEN == this.m_premiumType) ? GameStrings.Format("GLUE_MASS_DISENCHANT_PREMIUM_TITLE", new object[]
			{
				rarityText
			}) : rarityText);
		}
	}

	// Token: 0x0600142E RID: 5166 RVA: 0x00073B26 File Offset: 0x00071D26
	public int GetNumCards()
	{
		return this.m_numCards;
	}

	// Token: 0x0600142F RID: 5167 RVA: 0x00073B2E File Offset: 0x00071D2E
	public int GetAmountDust()
	{
		return this.m_amount;
	}

	// Token: 0x06001430 RID: 5168 RVA: 0x00073B38 File Offset: 0x00071D38
	public void UpdateVisuals(int totalNumCards)
	{
		this.m_numCardsText.Text = this.m_numCards.ToString();
		this.m_amountText.Text = this.m_amount.ToString();
		if (this.m_numGoldText != null)
		{
			if (this.m_numGoldCards > 0)
			{
				this.m_numGoldText.gameObject.SetActive(true);
				this.m_numGoldText.Text = GameStrings.Format("GLUE_MASS_DISENCHANT_NUM_GOLDEN_CARDS", new object[]
				{
					this.m_numGoldCards.ToString()
				});
				TransformUtil.SetLocalPosX(this.m_numCardsText, 2.902672f);
				this.m_barFrameMesh.GetComponent<MeshFilter>().mesh = MassDisenchant.Get().m_rarityBarGoldMesh;
				this.m_barFrameMesh.SetMaterial(MassDisenchant.Get().m_rarityBarGoldMaterial);
			}
			else
			{
				this.m_numGoldText.gameObject.SetActive(false);
				TransformUtil.SetLocalPosX(this.m_numCardsText, 2.902672f);
				this.m_barFrameMesh.GetComponent<MeshFilter>().mesh = MassDisenchant.Get().m_rarityBarNormalMesh;
				this.m_barFrameMesh.SetMaterial(MassDisenchant.Get().m_rarityBarNormalMaterial);
			}
		}
		float percent = ((float)totalNumCards > 0f) ? ((float)this.m_numCards / (float)totalNumCards) : 0f;
		this.SetPercent(percent);
	}

	// Token: 0x06001431 RID: 5169 RVA: 0x00073C78 File Offset: 0x00071E78
	public void SetPercent(float percent)
	{
		this.m_amountBar.GetComponent<Renderer>().GetMaterial().SetFloat("_Percent", percent);
	}

	// Token: 0x04000D3D RID: 3389
	public TAG_PREMIUM m_premiumType;

	// Token: 0x04000D3E RID: 3390
	public TAG_RARITY m_rarity;

	// Token: 0x04000D3F RID: 3391
	public UberText m_typeText;

	// Token: 0x04000D40 RID: 3392
	public UberText m_amountText;

	// Token: 0x04000D41 RID: 3393
	public UberText m_numCardsText;

	// Token: 0x04000D42 RID: 3394
	public GameObject m_amountBar;

	// Token: 0x04000D43 RID: 3395
	public GameObject m_dustJar;

	// Token: 0x04000D44 RID: 3396
	public GameObject m_rarityGem;

	// Token: 0x04000D45 RID: 3397
	public GameObject m_glow;

	// Token: 0x04000D46 RID: 3398
	public UberText m_numGoldText;

	// Token: 0x04000D47 RID: 3399
	public MeshRenderer m_barFrameMesh;

	// Token: 0x04000D48 RID: 3400
	private int m_numCards;

	// Token: 0x04000D49 RID: 3401
	private int m_amount;

	// Token: 0x04000D4A RID: 3402
	private int m_numGoldCards;

	// Token: 0x04000D4B RID: 3403
	public const float NUM_CARDS_TEXT_CENTER_X = 2.902672f;
}
