using Blizzard.Commerce;

namespace Hearthstone.Commerce
{
	public class PurchaseEventListener
	{
		public delegate void PurchasEventDelegate(blz_commerce_purchase_event_t purchaseEvent);

		public event PurchasEventDelegate Successful;

		public event PurchasEventDelegate Cancel;

		public event PurchasEventDelegate Pending;

		public event PurchasEventDelegate Failure;

		public void ReceivedEvent(blz_commerce_purchase_event_t purchaseEvent)
		{
			switch (purchaseEvent.status)
			{
			case blz_commerce_purchase_status_t.BLZ_COMMERCE_PURCHASE_SUCCESS:
				this.Successful?.Invoke(purchaseEvent);
				break;
			case blz_commerce_purchase_status_t.BLZ_COMMERCE_PURCHASE_CANCEL:
				this.Cancel?.Invoke(purchaseEvent);
				break;
			case blz_commerce_purchase_status_t.BLZ_COMMERCE_PURCHASE_PENDING:
				this.Pending?.Invoke(purchaseEvent);
				break;
			case blz_commerce_purchase_status_t.BLZ_COMMERCE_PURCHASE_FAILURE:
				this.Failure?.Invoke(purchaseEvent);
				break;
			default:
				Log.Store.PrintError("Purchase Event is not of a recognized status! ({0})", purchaseEvent.status.ToString());
				break;
			}
		}
	}
}
