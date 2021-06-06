using Blizzard.Commerce;

namespace Hearthstone.Commerce
{
	public class VirtualCurrencyEventListener
	{
		public delegate void CommerceVirtualCurrencyGetBalanceDelegate(blz_commerce_vc_get_balance_event_t getBalanceEvent);

		public delegate void CommerceVirtualCurrencyOrderEventDelegate(blz_commerce_vc_order_event_t virtualCurrencyOrderEvent);

		public event CommerceVirtualCurrencyGetBalanceDelegate GetBalance;

		public event CommerceVirtualCurrencyOrderEventDelegate PurchaseEvent;

		public void ReceivedEvent(blz_commerce_vc_event_t virtualCurrencyEvent)
		{
			switch (virtualCurrencyEvent.vc_type)
			{
			case blz_commerce_vc_type_t.BLZ_COMMERCE_VC_GET_BALANCE:
				this.GetBalance?.Invoke(new blz_commerce_vc_get_balance_event_t(virtualCurrencyEvent.vc_data, cMemoryOwn: false));
				break;
			case blz_commerce_vc_type_t.BLZ_COMMERCE_VC_PURCHASE:
				this.PurchaseEvent?.Invoke(new blz_commerce_vc_order_event_t(virtualCurrencyEvent.vc_data, cMemoryOwn: false));
				break;
			default:
				Log.Store.PrintError("Virtual Currency Event is not of a recognized type! ({0})", virtualCurrencyEvent.vc_type.ToString());
				break;
			}
		}
	}
}
