using System;
using Blizzard.Commerce;

// Token: 0x0200000F RID: 15
public class VirtualCurrencyEventListener : IVirtualCurrencyEventListener
{
	// Token: 0x0600004F RID: 79 RVA: 0x00002D5E File Offset: 0x00000F5E
	public VirtualCurrencyEventListener(IVirtualCurrencyEventListener virtualCurrencyEventListener)
	{
		this.m_virtualCurrencyEventListener = virtualCurrencyEventListener;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00002D6D File Offset: 0x00000F6D
	public void OnGetBalanceResponse(blz_commerce_vc_get_balance_event_t response)
	{
		IVirtualCurrencyEventListener virtualCurrencyEventListener = this.m_virtualCurrencyEventListener;
		if (virtualCurrencyEventListener == null)
		{
			return;
		}
		virtualCurrencyEventListener.OnGetBalanceResponse(response);
	}

	// Token: 0x04000013 RID: 19
	private IVirtualCurrencyEventListener m_virtualCurrencyEventListener;
}
