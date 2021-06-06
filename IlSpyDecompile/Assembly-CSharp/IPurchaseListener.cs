using Blizzard.Commerce;

public interface IPurchaseListener
{
	void OnPurchaseCanceledBeforeSubmit(blz_commerce_purchase_event_t response);

	void OnPurchaseFailureBeforeSubmit(blz_commerce_purchase_event_t response);

	void OnPurchaseSubmitted(blz_commerce_purchase_event_t response);

	void OnOrderPending(blz_commerce_purchase_event_t response);

	void OnOrderFailure(blz_commerce_purchase_event_t response);

	void OnOrderComplete(blz_commerce_purchase_event_t response);
}
