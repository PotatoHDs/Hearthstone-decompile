using Blizzard.Commerce;

public interface IVirtualCurrencyEventListener
{
	void OnGetBalanceResponse(blz_commerce_vc_get_balance_event_t response);
}
