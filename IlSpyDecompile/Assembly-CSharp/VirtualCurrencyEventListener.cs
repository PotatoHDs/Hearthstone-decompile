using Blizzard.Commerce;

public class VirtualCurrencyEventListener : IVirtualCurrencyEventListener
{
	private IVirtualCurrencyEventListener m_virtualCurrencyEventListener;

	public VirtualCurrencyEventListener(IVirtualCurrencyEventListener virtualCurrencyEventListener)
	{
		m_virtualCurrencyEventListener = virtualCurrencyEventListener;
	}

	public void OnGetBalanceResponse(blz_commerce_vc_get_balance_event_t response)
	{
		m_virtualCurrencyEventListener?.OnGetBalanceResponse(response);
	}
}
