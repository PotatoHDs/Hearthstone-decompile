using System;
using Blizzard.Commerce;

// Token: 0x0200000B RID: 11
public interface IVirtualCurrencyEventListener
{
	// Token: 0x0600003C RID: 60
	void OnGetBalanceResponse(blz_commerce_vc_get_balance_event_t response);
}
