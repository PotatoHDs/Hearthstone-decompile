using Blizzard.Commerce;

public interface ICatalogListener
{
	void OnGetPersonalizedShopEvent(blz_commerce_catalog_personalized_shop_event_t response);
}
