using UnityEngine;

public class StoreMiniSummary : MonoBehaviour
{
	public UberText m_headlineText;

	public UberText m_itemsHeadlineText;

	public UberText m_itemsText;

	private void Awake()
	{
		m_headlineText.Text = GameStrings.Get("GLUE_STORE_SUMMARY_HEADLINE");
		m_itemsHeadlineText.Text = GameStrings.Get("GLUE_STORE_SUMMARY_ITEMS_ORDERED_HEADLINE");
	}

	public void SetDetails(long pmtProductID, int quantity)
	{
		m_itemsText.Text = GetItemsText(pmtProductID, quantity);
	}

	private string GetItemsText(long pmtProductID, int quantity)
	{
		Network.Bundle bundleFromPmtProductId = StoreManager.Get().GetBundleFromPmtProductId(pmtProductID);
		string text = ((bundleFromPmtProductId != null) ? StoreManager.Get().GetProductName(bundleFromPmtProductId) : GameStrings.Get("GLUE_STORE_PRODUCT_NAME_MOBILE_UNKNOWN"));
		return GameStrings.Format("GLUE_STORE_SUMMARY_ITEM_ORDERED", quantity, text);
	}
}
