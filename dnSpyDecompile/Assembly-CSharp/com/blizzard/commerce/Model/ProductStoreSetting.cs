using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B7B RID: 2939
	[Serializable]
	public class ProductStoreSetting
	{
		// Token: 0x06009ACA RID: 39626 RVA: 0x0031CAE8 File Offset: 0x0031ACE8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ProductStoreSetting {\n");
			stringBuilder.Append("  regionalSetting: ").Append(this.regionalSetting).Append("\n");
			stringBuilder.Append("  attributes: ").Append(this.attributes).Append("\n");
			stringBuilder.Append("  storeId: ").Append(this.storeId).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0400809C RID: 32924
		public ProductStoreRegionalSetting regionalSetting;

		// Token: 0x0400809D RID: 32925
		public List<ProductStoreAttribute> attributes;

		// Token: 0x0400809E RID: 32926
		public int storeId;
	}
}
