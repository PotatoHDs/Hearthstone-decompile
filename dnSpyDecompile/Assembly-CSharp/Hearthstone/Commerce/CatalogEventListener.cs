using System;
using Blizzard.Commerce;
using com.blizzard.commerce.Model;
using UnityEngine;

namespace Hearthstone.Commerce
{
	// Token: 0x02001075 RID: 4213
	public class CatalogEventListener
	{
		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x0600B602 RID: 46594 RVA: 0x0037D8E0 File Offset: 0x0037BAE0
		// (remove) Token: 0x0600B603 RID: 46595 RVA: 0x0037D918 File Offset: 0x0037BB18
		public event CatalogEventListener.CommercePersonalizedShopDelegate PersonalizedShopReceived;

		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x0600B604 RID: 46596 RVA: 0x0037D950 File Offset: 0x0037BB50
		// (remove) Token: 0x0600B605 RID: 46597 RVA: 0x0037D988 File Offset: 0x0037BB88
		public event CatalogEventListener.CommerceProductLoadDelegate ProductLoaded;

		// Token: 0x0600B606 RID: 46598 RVA: 0x0037D9C0 File Offset: 0x0037BBC0
		public void ReceivedEvent(blz_commerce_catalog_event_t catalogEvent)
		{
			blz_commerce_catalog_type_t catalog_type = catalogEvent.catalog_type;
			if (catalog_type != blz_commerce_catalog_type_t.BLZ_COMMERCE_CATALOG_PRODUCT_LOAD)
			{
				if (catalog_type != blz_commerce_catalog_type_t.BLZ_COMMERCE_CATALOG_PERSONALIZED_SHOP)
				{
					Log.Store.PrintError("Catalog Event is not of a recognized type! ({0})", new object[]
					{
						catalogEvent.catalog_type.ToString()
					});
					return;
				}
				CatalogEventListener.CommercePersonalizedShopDelegate personalizedShopReceived = this.PersonalizedShopReceived;
				if (personalizedShopReceived == null)
				{
					return;
				}
				personalizedShopReceived(new blz_commerce_catalog_personalized_shop_event_t(catalogEvent.catalog_data, false));
				return;
			}
			else
			{
				blz_commerce_catalog_product_load_event_t blz_commerce_catalog_product_load_event_t = new blz_commerce_catalog_product_load_event_t(catalogEvent.catalog_data, false);
				if (!blz_commerce_catalog_product_load_event_t.http_result.ok)
				{
					Log.Store.PrintError(string.Format("There was an error from the HTTP result of the product loading. ({0}: {1})", blz_commerce_catalog_product_load_event_t.http_result.result_code.ToString(), blz_commerce_catalog_product_load_event_t.http_result.message), Array.Empty<object>());
					return;
				}
				GetProductsByStoreIdResponse productLoadEvent = JsonUtility.FromJson<GetProductsByStoreIdResponse>(blz_commerce_catalog_product_load_event_t.response);
				CatalogEventListener.CommerceProductLoadDelegate productLoaded = this.ProductLoaded;
				if (productLoaded == null)
				{
					return;
				}
				productLoaded(productLoadEvent);
				return;
			}
		}

		// Token: 0x02002874 RID: 10356
		// (Invoke) Token: 0x06013BE9 RID: 80873
		public delegate void CommercePersonalizedShopDelegate(blz_commerce_catalog_personalized_shop_event_t personalizedShopEvent);

		// Token: 0x02002875 RID: 10357
		// (Invoke) Token: 0x06013BED RID: 80877
		public delegate void CommerceProductLoadDelegate(GetProductsByStoreIdResponse productLoadEvent);
	}
}
