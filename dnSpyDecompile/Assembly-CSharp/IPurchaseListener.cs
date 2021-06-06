using System;
using Blizzard.Commerce;

// Token: 0x0200000A RID: 10
public interface IPurchaseListener
{
	// Token: 0x06000036 RID: 54
	void OnPurchaseCanceledBeforeSubmit(blz_commerce_purchase_event_t response);

	// Token: 0x06000037 RID: 55
	void OnPurchaseFailureBeforeSubmit(blz_commerce_purchase_event_t response);

	// Token: 0x06000038 RID: 56
	void OnPurchaseSubmitted(blz_commerce_purchase_event_t response);

	// Token: 0x06000039 RID: 57
	void OnOrderPending(blz_commerce_purchase_event_t response);

	// Token: 0x0600003A RID: 58
	void OnOrderFailure(blz_commerce_purchase_event_t response);

	// Token: 0x0600003B RID: 59
	void OnOrderComplete(blz_commerce_purchase_event_t response);
}
