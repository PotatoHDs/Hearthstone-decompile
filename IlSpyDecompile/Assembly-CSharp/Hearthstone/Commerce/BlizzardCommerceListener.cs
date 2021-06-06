using System;
using Blizzard.Commerce;

namespace Hearthstone.Commerce
{
	public class BlizzardCommerceListener : blz_commerce_listener
	{
		private readonly HttpEventListener m_httpEventListener;

		private readonly CatalogEventListener m_catalogEventListener;

		private readonly VirtualCurrencyEventListener m_virtualCurrencyEventListener;

		private readonly SceneEventListener m_sceneEventListener;

		private readonly PurchaseEventListener m_purchaseEventListener;

		public HttpEventListener HttpEventListener => m_httpEventListener;

		public CatalogEventListener CatalogEventListener => m_catalogEventListener;

		public VirtualCurrencyEventListener VirtualCurrencyEventListener => m_virtualCurrencyEventListener;

		public SceneEventListener SceneEventListener => m_sceneEventListener;

		public PurchaseEventListener PurchaseEventListener => m_purchaseEventListener;

		public BlizzardCommerceListener()
		{
			m_httpEventListener = new HttpEventListener();
			m_catalogEventListener = new CatalogEventListener();
			m_virtualCurrencyEventListener = new VirtualCurrencyEventListener();
			m_sceneEventListener = new SceneEventListener();
			m_purchaseEventListener = new PurchaseEventListener();
		}

		public override void OnEvent(IntPtr owner, blz_commerce_event_t triggeredEvent)
		{
			switch (triggeredEvent.type)
			{
			case blz_commerce_event_type_t.BLZ_COMMERCE_PURCHASE_EVENT:
				m_purchaseEventListener.ReceivedEvent(new blz_commerce_purchase_event_t(triggeredEvent.data, cMemoryOwn: false));
				break;
			case blz_commerce_event_type_t.BLZ_COMMERCE_SCENE_EVENT:
				m_sceneEventListener.ReceivedEvent(new blz_commerce_scene_event_t(triggeredEvent.data, cMemoryOwn: false));
				break;
			case blz_commerce_event_type_t.BLZ_COMMERCE_VC_EVENT:
				m_virtualCurrencyEventListener.ReceivedEvent(new blz_commerce_vc_event_t(triggeredEvent.data, cMemoryOwn: false));
				break;
			case blz_commerce_event_type_t.BLZ_COMMERCE_CATALOG_EVENT:
				m_catalogEventListener.ReceivedEvent(new blz_commerce_catalog_event_t(triggeredEvent.data, cMemoryOwn: false));
				break;
			case blz_commerce_event_type_t.BLZ_COMMERCE_HTTP_EVENT:
				m_httpEventListener.EventReceived(new blz_commerce_http_event_t(triggeredEvent.data, cMemoryOwn: false));
				break;
			}
		}
	}
}
