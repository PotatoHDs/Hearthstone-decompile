using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class Product
	{
		public ProductLocalization localization;

		public int serviceItemId;

		public ProductStoreSetting storeSetting;

		public List<License> licenses;

		public int productId;

		public ProductOffer activeOffer;

		public string name;

		public List<VirtualCurrencyGrant> virtualCurrencyGrants;

		public ProductExternalPlatformSetting externalPlatformSetting;

		public List<ProductPrice> prices;

		public Sale activeSale;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class Product {\n");
			stringBuilder.Append("  localization: ").Append(localization).Append("\n");
			stringBuilder.Append("  serviceItemId: ").Append(serviceItemId).Append("\n");
			stringBuilder.Append("  storeSetting: ").Append(storeSetting).Append("\n");
			stringBuilder.Append("  licenses: ").Append(licenses).Append("\n");
			stringBuilder.Append("  productId: ").Append(productId).Append("\n");
			stringBuilder.Append("  activeOffer: ").Append(activeOffer).Append("\n");
			stringBuilder.Append("  name: ").Append(name).Append("\n");
			stringBuilder.Append("  virtualCurrencyGrants: ").Append(virtualCurrencyGrants).Append("\n");
			stringBuilder.Append("  externalPlatformSetting: ").Append(externalPlatformSetting).Append("\n");
			stringBuilder.Append("  prices: ").Append(prices).Append("\n");
			stringBuilder.Append("  activeSale: ").Append(activeSale).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
