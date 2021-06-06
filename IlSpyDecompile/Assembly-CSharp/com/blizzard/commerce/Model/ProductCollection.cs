using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class ProductCollection
	{
		public int orderInSection;

		public string productCollectionId;

		public string name;

		public List<ProductCollectionItem> items;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ProductCollection {\n");
			stringBuilder.Append("  orderInSection: ").Append(orderInSection).Append("\n");
			stringBuilder.Append("  productCollectionId: ").Append(productCollectionId).Append("\n");
			stringBuilder.Append("  name: ").Append(name).Append("\n");
			stringBuilder.Append("  items: ").Append(items).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
