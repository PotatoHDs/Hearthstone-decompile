using System;
using Blizzard.Commerce;

// Token: 0x02000010 RID: 16
public class CatalogListener : ICatalogListener
{
	// Token: 0x06000051 RID: 81 RVA: 0x00002D80 File Offset: 0x00000F80
	public CatalogListener(ICatalogListener catalogEventListener)
	{
		this.m_catalogListener = catalogEventListener;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00002D8F File Offset: 0x00000F8F
	public void OnGetPersonalizedShopEvent(blz_commerce_catalog_personalized_shop_event_t response)
	{
		if (this.m_catalogListener != null)
		{
			this.m_catalogListener.OnGetPersonalizedShopEvent(response);
		}
	}

	// Token: 0x04000014 RID: 20
	private ICatalogListener m_catalogListener;
}
