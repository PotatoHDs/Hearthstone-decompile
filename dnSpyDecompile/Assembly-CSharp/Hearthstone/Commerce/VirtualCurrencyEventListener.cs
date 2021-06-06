using System;
using Blizzard.Commerce;

namespace Hearthstone.Commerce
{
	// Token: 0x02001079 RID: 4217
	public class VirtualCurrencyEventListener
	{
		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x0600B634 RID: 46644 RVA: 0x0037E5C0 File Offset: 0x0037C7C0
		// (remove) Token: 0x0600B635 RID: 46645 RVA: 0x0037E5F8 File Offset: 0x0037C7F8
		public event VirtualCurrencyEventListener.CommerceVirtualCurrencyGetBalanceDelegate GetBalance;

		// Token: 0x140000C6 RID: 198
		// (add) Token: 0x0600B636 RID: 46646 RVA: 0x0037E630 File Offset: 0x0037C830
		// (remove) Token: 0x0600B637 RID: 46647 RVA: 0x0037E668 File Offset: 0x0037C868
		public event VirtualCurrencyEventListener.CommerceVirtualCurrencyOrderEventDelegate PurchaseEvent;

		// Token: 0x0600B638 RID: 46648 RVA: 0x0037E6A0 File Offset: 0x0037C8A0
		public void ReceivedEvent(blz_commerce_vc_event_t virtualCurrencyEvent)
		{
			blz_commerce_vc_type_t vc_type = virtualCurrencyEvent.vc_type;
			if (vc_type != blz_commerce_vc_type_t.BLZ_COMMERCE_VC_GET_BALANCE)
			{
				if (vc_type != blz_commerce_vc_type_t.BLZ_COMMERCE_VC_PURCHASE)
				{
					Log.Store.PrintError("Virtual Currency Event is not of a recognized type! ({0})", new object[]
					{
						virtualCurrencyEvent.vc_type.ToString()
					});
					return;
				}
				VirtualCurrencyEventListener.CommerceVirtualCurrencyOrderEventDelegate purchaseEvent = this.PurchaseEvent;
				if (purchaseEvent == null)
				{
					return;
				}
				purchaseEvent(new blz_commerce_vc_order_event_t(virtualCurrencyEvent.vc_data, false));
				return;
			}
			else
			{
				VirtualCurrencyEventListener.CommerceVirtualCurrencyGetBalanceDelegate getBalance = this.GetBalance;
				if (getBalance == null)
				{
					return;
				}
				getBalance(new blz_commerce_vc_get_balance_event_t(virtualCurrencyEvent.vc_data, false));
				return;
			}
		}

		// Token: 0x02002880 RID: 10368
		// (Invoke) Token: 0x06013C19 RID: 80921
		public delegate void CommerceVirtualCurrencyGetBalanceDelegate(blz_commerce_vc_get_balance_event_t getBalanceEvent);

		// Token: 0x02002881 RID: 10369
		// (Invoke) Token: 0x06013C1D RID: 80925
		public delegate void CommerceVirtualCurrencyOrderEventDelegate(blz_commerce_vc_order_event_t virtualCurrencyOrderEvent);
	}
}
