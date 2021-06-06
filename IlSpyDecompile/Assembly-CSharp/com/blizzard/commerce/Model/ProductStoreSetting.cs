using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class ProductStoreSetting
	{
		public ProductStoreRegionalSetting regionalSetting;

		public List<ProductStoreAttribute> attributes;

		public int storeId;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ProductStoreSetting {\n");
			stringBuilder.Append("  regionalSetting: ").Append(regionalSetting).Append("\n");
			stringBuilder.Append("  attributes: ").Append(attributes).Append("\n");
			stringBuilder.Append("  storeId: ").Append(storeId).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
