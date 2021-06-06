using System;
using Blizzard.Commerce;

namespace Hearthstone.Commerce
{
	// Token: 0x02001077 RID: 4215
	public class PurchaseEventListener
	{
		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x0600B60C RID: 46604 RVA: 0x0037DB64 File Offset: 0x0037BD64
		// (remove) Token: 0x0600B60D RID: 46605 RVA: 0x0037DB9C File Offset: 0x0037BD9C
		public event PurchaseEventListener.PurchasEventDelegate Successful;

		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x0600B60E RID: 46606 RVA: 0x0037DBD4 File Offset: 0x0037BDD4
		// (remove) Token: 0x0600B60F RID: 46607 RVA: 0x0037DC0C File Offset: 0x0037BE0C
		public event PurchaseEventListener.PurchasEventDelegate Cancel;

		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x0600B610 RID: 46608 RVA: 0x0037DC44 File Offset: 0x0037BE44
		// (remove) Token: 0x0600B611 RID: 46609 RVA: 0x0037DC7C File Offset: 0x0037BE7C
		public event PurchaseEventListener.PurchasEventDelegate Pending;

		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x0600B612 RID: 46610 RVA: 0x0037DCB4 File Offset: 0x0037BEB4
		// (remove) Token: 0x0600B613 RID: 46611 RVA: 0x0037DCEC File Offset: 0x0037BEEC
		public event PurchaseEventListener.PurchasEventDelegate Failure;

		// Token: 0x0600B614 RID: 46612 RVA: 0x0037DD24 File Offset: 0x0037BF24
		public void ReceivedEvent(blz_commerce_purchase_event_t purchaseEvent)
		{
			switch (purchaseEvent.status)
			{
			case blz_commerce_purchase_status_t.BLZ_COMMERCE_PURCHASE_SUCCESS:
			{
				PurchaseEventListener.PurchasEventDelegate successful = this.Successful;
				if (successful == null)
				{
					return;
				}
				successful(purchaseEvent);
				return;
			}
			case blz_commerce_purchase_status_t.BLZ_COMMERCE_PURCHASE_CANCEL:
			{
				PurchaseEventListener.PurchasEventDelegate cancel = this.Cancel;
				if (cancel == null)
				{
					return;
				}
				cancel(purchaseEvent);
				return;
			}
			case blz_commerce_purchase_status_t.BLZ_COMMERCE_PURCHASE_PENDING:
			{
				PurchaseEventListener.PurchasEventDelegate pending = this.Pending;
				if (pending == null)
				{
					return;
				}
				pending(purchaseEvent);
				return;
			}
			case blz_commerce_purchase_status_t.BLZ_COMMERCE_PURCHASE_FAILURE:
			{
				PurchaseEventListener.PurchasEventDelegate failure = this.Failure;
				if (failure == null)
				{
					return;
				}
				failure(purchaseEvent);
				return;
			}
			default:
				Log.Store.PrintError("Purchase Event is not of a recognized status! ({0})", new object[]
				{
					purchaseEvent.status.ToString()
				});
				return;
			}
		}

		// Token: 0x02002877 RID: 10359
		// (Invoke) Token: 0x06013BF5 RID: 80885
		public delegate void PurchasEventDelegate(blz_commerce_purchase_event_t purchaseEvent);
	}
}
