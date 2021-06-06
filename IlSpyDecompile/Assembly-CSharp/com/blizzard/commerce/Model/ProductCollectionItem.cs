using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class ProductCollectionItem
	{
		public int orderInProductCollection;

		public int productCollectionItemTypeId;

		public int productCollectionItemValue;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ProductCollectionItem {\n");
			stringBuilder.Append("  orderInProductCollection: ").Append(orderInProductCollection).Append("\n");
			stringBuilder.Append("  productCollectionItemTypeId: ").Append(productCollectionItemTypeId).Append("\n");
			stringBuilder.Append("  productCollectionItemValue: ").Append(productCollectionItemValue).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
