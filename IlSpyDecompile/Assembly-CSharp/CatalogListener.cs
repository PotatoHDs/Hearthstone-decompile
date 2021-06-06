using Blizzard.Commerce;

public class CatalogListener : ICatalogListener
{
	private ICatalogListener m_catalogListener;

	public CatalogListener(ICatalogListener catalogEventListener)
	{
		m_catalogListener = catalogEventListener;
	}

	public void OnGetPersonalizedShopEvent(blz_commerce_catalog_personalized_shop_event_t response)
	{
		if (m_catalogListener != null)
		{
			m_catalogListener.OnGetPersonalizedShopEvent(response);
		}
	}
}
