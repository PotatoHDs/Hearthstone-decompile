using Hearthstone.DataModels;

public class VirtualCurrencyPurchasePage : ProductPage
{
	private bool m_closeOnPurchase;

	protected override void Start()
	{
		base.Start();
		StoreManager.Get()?.RegisterSuccessfulPurchaseAckListener(HandleSuccessfulPurchaseAck);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		StoreManager.Get()?.RemoveSuccessfulPurchaseAckListener(HandleSuccessfulPurchaseAck);
	}

	public void OpenToSKU(float desiredAmount, bool closeOnPurchase = false)
	{
		base.Open();
		ProductDataModel virtualCurrencyProductItem = StoreManager.Get().Catalog.VirtualCurrencyProductItem;
		ProductDataModel variant = ShopUtils.FindCurrencyProduct(CurrencyType.RUNESTONES, desiredAmount);
		m_closeOnPurchase = closeOnPurchase;
		SetProduct(virtualCurrencyProductItem, variant);
	}

	private void HandleSuccessfulPurchaseAck(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		if (base.IsOpen && m_closeOnPurchase)
		{
			Close();
		}
	}
}
