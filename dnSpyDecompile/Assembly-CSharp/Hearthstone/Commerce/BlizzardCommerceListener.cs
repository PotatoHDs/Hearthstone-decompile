using System;
using Blizzard.Commerce;

namespace Hearthstone.Commerce
{
	// Token: 0x02001074 RID: 4212
	public class BlizzardCommerceListener : blz_commerce_listener
	{
		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x0600B5FB RID: 46587 RVA: 0x0037D7CF File Offset: 0x0037B9CF
		public HttpEventListener HttpEventListener
		{
			get
			{
				return this.m_httpEventListener;
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x0600B5FC RID: 46588 RVA: 0x0037D7D7 File Offset: 0x0037B9D7
		public CatalogEventListener CatalogEventListener
		{
			get
			{
				return this.m_catalogEventListener;
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x0600B5FD RID: 46589 RVA: 0x0037D7DF File Offset: 0x0037B9DF
		public VirtualCurrencyEventListener VirtualCurrencyEventListener
		{
			get
			{
				return this.m_virtualCurrencyEventListener;
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x0600B5FE RID: 46590 RVA: 0x0037D7E7 File Offset: 0x0037B9E7
		public SceneEventListener SceneEventListener
		{
			get
			{
				return this.m_sceneEventListener;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x0600B5FF RID: 46591 RVA: 0x0037D7EF File Offset: 0x0037B9EF
		public PurchaseEventListener PurchaseEventListener
		{
			get
			{
				return this.m_purchaseEventListener;
			}
		}

		// Token: 0x0600B600 RID: 46592 RVA: 0x0037D7F7 File Offset: 0x0037B9F7
		public BlizzardCommerceListener()
		{
			this.m_httpEventListener = new HttpEventListener();
			this.m_catalogEventListener = new CatalogEventListener();
			this.m_virtualCurrencyEventListener = new VirtualCurrencyEventListener();
			this.m_sceneEventListener = new SceneEventListener();
			this.m_purchaseEventListener = new PurchaseEventListener();
		}

		// Token: 0x0600B601 RID: 46593 RVA: 0x0037D838 File Offset: 0x0037BA38
		public override void OnEvent(IntPtr owner, blz_commerce_event_t triggeredEvent)
		{
			switch (triggeredEvent.type)
			{
			case blz_commerce_event_type_t.BLZ_COMMERCE_PURCHASE_EVENT:
				this.m_purchaseEventListener.ReceivedEvent(new blz_commerce_purchase_event_t(triggeredEvent.data, false));
				return;
			case blz_commerce_event_type_t.BLZ_COMMERCE_SCENE_EVENT:
				this.m_sceneEventListener.ReceivedEvent(new blz_commerce_scene_event_t(triggeredEvent.data, false));
				return;
			case blz_commerce_event_type_t.BLZ_COMMERCE_VC_EVENT:
				this.m_virtualCurrencyEventListener.ReceivedEvent(new blz_commerce_vc_event_t(triggeredEvent.data, false));
				return;
			case blz_commerce_event_type_t.BLZ_COMMERCE_CATALOG_EVENT:
				this.m_catalogEventListener.ReceivedEvent(new blz_commerce_catalog_event_t(triggeredEvent.data, false));
				return;
			case blz_commerce_event_type_t.BLZ_COMMERCE_HTTP_EVENT:
				this.m_httpEventListener.EventReceived(new blz_commerce_http_event_t(triggeredEvent.data, false));
				return;
			default:
				return;
			}
		}

		// Token: 0x0400977E RID: 38782
		private readonly HttpEventListener m_httpEventListener;

		// Token: 0x0400977F RID: 38783
		private readonly CatalogEventListener m_catalogEventListener;

		// Token: 0x04009780 RID: 38784
		private readonly VirtualCurrencyEventListener m_virtualCurrencyEventListener;

		// Token: 0x04009781 RID: 38785
		private readonly SceneEventListener m_sceneEventListener;

		// Token: 0x04009782 RID: 38786
		private readonly PurchaseEventListener m_purchaseEventListener;
	}
}
