using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B71 RID: 2929
	[Serializable]
	public class Product
	{
		// Token: 0x06009AB6 RID: 39606 RVA: 0x0031C3F4 File Offset: 0x0031A5F4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class Product {\n");
			stringBuilder.Append("  localization: ").Append(this.localization).Append("\n");
			stringBuilder.Append("  serviceItemId: ").Append(this.serviceItemId).Append("\n");
			stringBuilder.Append("  storeSetting: ").Append(this.storeSetting).Append("\n");
			stringBuilder.Append("  licenses: ").Append(this.licenses).Append("\n");
			stringBuilder.Append("  productId: ").Append(this.productId).Append("\n");
			stringBuilder.Append("  activeOffer: ").Append(this.activeOffer).Append("\n");
			stringBuilder.Append("  name: ").Append(this.name).Append("\n");
			stringBuilder.Append("  virtualCurrencyGrants: ").Append(this.virtualCurrencyGrants).Append("\n");
			stringBuilder.Append("  externalPlatformSetting: ").Append(this.externalPlatformSetting).Append("\n");
			stringBuilder.Append("  prices: ").Append(this.prices).Append("\n");
			stringBuilder.Append("  activeSale: ").Append(this.activeSale).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008075 RID: 32885
		public ProductLocalization localization;

		// Token: 0x04008076 RID: 32886
		public int serviceItemId;

		// Token: 0x04008077 RID: 32887
		public ProductStoreSetting storeSetting;

		// Token: 0x04008078 RID: 32888
		public List<License> licenses;

		// Token: 0x04008079 RID: 32889
		public int productId;

		// Token: 0x0400807A RID: 32890
		public ProductOffer activeOffer;

		// Token: 0x0400807B RID: 32891
		public string name;

		// Token: 0x0400807C RID: 32892
		public List<VirtualCurrencyGrant> virtualCurrencyGrants;

		// Token: 0x0400807D RID: 32893
		public ProductExternalPlatformSetting externalPlatformSetting;

		// Token: 0x0400807E RID: 32894
		public List<ProductPrice> prices;

		// Token: 0x0400807F RID: 32895
		public Sale activeSale;
	}
}
