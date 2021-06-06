using System;
using Blizzard.Commerce;

// Token: 0x0200000C RID: 12
public interface ICatalogListener
{
	// Token: 0x0600003D RID: 61
	void OnGetPersonalizedShopEvent(blz_commerce_catalog_personalized_shop_event_t response);
}
