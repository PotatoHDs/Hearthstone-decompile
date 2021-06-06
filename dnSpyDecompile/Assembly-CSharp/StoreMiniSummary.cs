using System;
using UnityEngine;

// Token: 0x02000726 RID: 1830
public class StoreMiniSummary : MonoBehaviour
{
	// Token: 0x060066E1 RID: 26337 RVA: 0x00218DFB File Offset: 0x00216FFB
	private void Awake()
	{
		this.m_headlineText.Text = GameStrings.Get("GLUE_STORE_SUMMARY_HEADLINE");
		this.m_itemsHeadlineText.Text = GameStrings.Get("GLUE_STORE_SUMMARY_ITEMS_ORDERED_HEADLINE");
	}

	// Token: 0x060066E2 RID: 26338 RVA: 0x00218E27 File Offset: 0x00217027
	public void SetDetails(long pmtProductID, int quantity)
	{
		this.m_itemsText.Text = this.GetItemsText(pmtProductID, quantity);
	}

	// Token: 0x060066E3 RID: 26339 RVA: 0x00218E3C File Offset: 0x0021703C
	private string GetItemsText(long pmtProductID, int quantity)
	{
		Network.Bundle bundleFromPmtProductId = StoreManager.Get().GetBundleFromPmtProductId(pmtProductID);
		string text;
		if (bundleFromPmtProductId == null)
		{
			text = GameStrings.Get("GLUE_STORE_PRODUCT_NAME_MOBILE_UNKNOWN");
		}
		else
		{
			text = StoreManager.Get().GetProductName(bundleFromPmtProductId);
		}
		return GameStrings.Format("GLUE_STORE_SUMMARY_ITEM_ORDERED", new object[]
		{
			quantity,
			text
		});
	}

	// Token: 0x040054B2 RID: 21682
	public UberText m_headlineText;

	// Token: 0x040054B3 RID: 21683
	public UberText m_itemsHeadlineText;

	// Token: 0x040054B4 RID: 21684
	public UberText m_itemsText;
}
