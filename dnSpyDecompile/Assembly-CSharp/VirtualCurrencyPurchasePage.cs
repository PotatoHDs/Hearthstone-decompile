using System;
using Hearthstone.DataModels;

// Token: 0x020006C1 RID: 1729
public class VirtualCurrencyPurchasePage : ProductPage
{
	// Token: 0x060060FD RID: 24829 RVA: 0x001F9F94 File Offset: 0x001F8194
	protected override void Start()
	{
		base.Start();
		StoreManager storeManager = StoreManager.Get();
		if (storeManager != null)
		{
			storeManager.RegisterSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.HandleSuccessfulPurchaseAck));
		}
	}

	// Token: 0x060060FE RID: 24830 RVA: 0x001F9FC4 File Offset: 0x001F81C4
	protected override void OnDestroy()
	{
		base.OnDestroy();
		StoreManager storeManager = StoreManager.Get();
		if (storeManager != null)
		{
			storeManager.RemoveSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.HandleSuccessfulPurchaseAck));
		}
	}

	// Token: 0x060060FF RID: 24831 RVA: 0x001F9FF4 File Offset: 0x001F81F4
	public void OpenToSKU(float desiredAmount, bool closeOnPurchase = false)
	{
		base.Open();
		ProductDataModel virtualCurrencyProductItem = StoreManager.Get().Catalog.VirtualCurrencyProductItem;
		ProductDataModel variant = ShopUtils.FindCurrencyProduct(CurrencyType.RUNESTONES, desiredAmount);
		this.m_closeOnPurchase = closeOnPurchase;
		base.SetProduct(virtualCurrencyProductItem, variant);
	}

	// Token: 0x06006100 RID: 24832 RVA: 0x001FA02E File Offset: 0x001F822E
	private void HandleSuccessfulPurchaseAck(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		if (base.IsOpen && this.m_closeOnPurchase)
		{
			this.Close();
		}
	}

	// Token: 0x040050F7 RID: 20727
	private bool m_closeOnPurchase;
}
