using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B7A RID: 2938
	[Serializable]
	public class ProductStoreRegionalSetting
	{
		// Token: 0x06009AC8 RID: 39624 RVA: 0x0031CA74 File Offset: 0x0031AC74
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ProductStoreRegionalSetting {\n");
			stringBuilder.Append("  gameServiceRegionId: ").Append(this.gameServiceRegionId).Append("\n");
			stringBuilder.Append("  attributes: ").Append(this.attributes).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0400809A RID: 32922
		public int gameServiceRegionId;

		// Token: 0x0400809B RID: 32923
		public List<ProductStoreRegionalAttribute> attributes;
	}
}
