using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class ProductStoreRegionalSetting
	{
		public int gameServiceRegionId;

		public List<ProductStoreRegionalAttribute> attributes;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ProductStoreRegionalSetting {\n");
			stringBuilder.Append("  gameServiceRegionId: ").Append(gameServiceRegionId).Append("\n");
			stringBuilder.Append("  attributes: ").Append(attributes).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
