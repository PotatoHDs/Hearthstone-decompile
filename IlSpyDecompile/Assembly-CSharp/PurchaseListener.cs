using Blizzard.Commerce;

public class PurchaseListener : IPurchaseListener
{
	private IPurchaseListener m_purchaseListener;

	public PurchaseListener(IPurchaseListener purchaseListener)
	{
		m_purchaseListener = purchaseListener;
	}

	public void OnPurchaseCanceledBeforeSubmit(blz_commerce_purchase_event_t response)
	{
		m_purchaseListener?.OnPurchaseCanceledBeforeSubmit(response);
	}

	public void OnPurchaseFailureBeforeSubmit(blz_commerce_purchase_event_t response)
	{
		m_purchaseListener?.OnPurchaseFailureBeforeSubmit(response);
	}

	public void OnPurchaseSubmitted(blz_commerce_purchase_event_t response)
	{
		m_purchaseListener?.OnPurchaseSubmitted(response);
	}

	public void OnOrderPending(blz_commerce_purchase_event_t response)
	{
		m_purchaseListener?.OnOrderPending(response);
	}

	public void OnOrderFailure(blz_commerce_purchase_event_t response)
	{
		m_purchaseListener?.OnOrderFailure(response);
	}

	public void OnOrderComplete(blz_commerce_purchase_event_t response)
	{
		m_purchaseListener?.OnOrderComplete(response);
	}
}
