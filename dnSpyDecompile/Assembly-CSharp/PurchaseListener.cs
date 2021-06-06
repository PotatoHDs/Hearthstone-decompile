using System;
using Blizzard.Commerce;

// Token: 0x0200000E RID: 14
public class PurchaseListener : IPurchaseListener
{
	// Token: 0x06000048 RID: 72 RVA: 0x00002CDD File Offset: 0x00000EDD
	public PurchaseListener(IPurchaseListener purchaseListener)
	{
		this.m_purchaseListener = purchaseListener;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00002CEC File Offset: 0x00000EEC
	public void OnPurchaseCanceledBeforeSubmit(blz_commerce_purchase_event_t response)
	{
		IPurchaseListener purchaseListener = this.m_purchaseListener;
		if (purchaseListener == null)
		{
			return;
		}
		purchaseListener.OnPurchaseCanceledBeforeSubmit(response);
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00002CFF File Offset: 0x00000EFF
	public void OnPurchaseFailureBeforeSubmit(blz_commerce_purchase_event_t response)
	{
		IPurchaseListener purchaseListener = this.m_purchaseListener;
		if (purchaseListener == null)
		{
			return;
		}
		purchaseListener.OnPurchaseFailureBeforeSubmit(response);
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00002D12 File Offset: 0x00000F12
	public void OnPurchaseSubmitted(blz_commerce_purchase_event_t response)
	{
		IPurchaseListener purchaseListener = this.m_purchaseListener;
		if (purchaseListener == null)
		{
			return;
		}
		purchaseListener.OnPurchaseSubmitted(response);
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00002D25 File Offset: 0x00000F25
	public void OnOrderPending(blz_commerce_purchase_event_t response)
	{
		IPurchaseListener purchaseListener = this.m_purchaseListener;
		if (purchaseListener == null)
		{
			return;
		}
		purchaseListener.OnOrderPending(response);
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00002D38 File Offset: 0x00000F38
	public void OnOrderFailure(blz_commerce_purchase_event_t response)
	{
		IPurchaseListener purchaseListener = this.m_purchaseListener;
		if (purchaseListener == null)
		{
			return;
		}
		purchaseListener.OnOrderFailure(response);
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00002D4B File Offset: 0x00000F4B
	public void OnOrderComplete(blz_commerce_purchase_event_t response)
	{
		IPurchaseListener purchaseListener = this.m_purchaseListener;
		if (purchaseListener == null)
		{
			return;
		}
		purchaseListener.OnOrderComplete(response);
	}

	// Token: 0x04000012 RID: 18
	private IPurchaseListener m_purchaseListener;
}
