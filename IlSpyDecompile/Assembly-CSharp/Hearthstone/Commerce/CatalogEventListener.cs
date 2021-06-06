using Blizzard.Commerce;
using com.blizzard.commerce.Model;
using UnityEngine;

namespace Hearthstone.Commerce
{
	public class CatalogEventListener
	{
		public delegate void CommercePersonalizedShopDelegate(blz_commerce_catalog_personalized_shop_event_t personalizedShopEvent);

		public delegate void CommerceProductLoadDelegate(GetProductsByStoreIdResponse productLoadEvent);

		public event CommercePersonalizedShopDelegate PersonalizedShopReceived;

		public event CommerceProductLoadDelegate ProductLoaded;

		public void ReceivedEvent(blz_commerce_catalog_event_t catalogEvent)
		{
			switch (catalogEvent.catalog_type)
			{
			case blz_commerce_catalog_type_t.BLZ_COMMERCE_CATALOG_PRODUCT_LOAD:
			{
				blz_commerce_catalog_product_load_event_t blz_commerce_catalog_product_load_event_t = new blz_commerce_catalog_product_load_event_t(catalogEvent.catalog_data, cMemoryOwn: false);
				if (!blz_commerce_catalog_product_load_event_t.http_result.ok)
				{
					Log.Store.PrintError($"There was an error from the HTTP result of the product loading. ({blz_commerce_catalog_product_load_event_t.http_result.result_code.ToString()}: {blz_commerce_catalog_product_load_event_t.http_result.message})");
					break;
				}
				GetProductsByStoreIdResponse productLoadEvent = JsonUtility.FromJson<GetProductsByStoreIdResponse>(blz_commerce_catalog_product_load_event_t.response);
				this.ProductLoaded?.Invoke(productLoadEvent);
				break;
			}
			case blz_commerce_catalog_type_t.BLZ_COMMERCE_CATALOG_PERSONALIZED_SHOP:
				this.PersonalizedShopReceived?.Invoke(new blz_commerce_catalog_personalized_shop_event_t(catalogEvent.catalog_data, cMemoryOwn: false));
				break;
			default:
				Log.Store.PrintError("Catalog Event is not of a recognized type! ({0})", catalogEvent.catalog_type.ToString());
				break;
			}
		}
	}
}
