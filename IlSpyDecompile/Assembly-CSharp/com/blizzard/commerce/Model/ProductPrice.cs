using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class ProductPrice
	{
		public int currencyTypeId;

		public string localizedOriginalPrice;

		public string originalPrice;

		public string currentPrice;

		public string localizedCurrentPrice;

		public string currencyCode;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ProductPrice {\n");
			stringBuilder.Append("  currencyTypeId: ").Append(currencyTypeId).Append("\n");
			stringBuilder.Append("  localizedOriginalPrice: ").Append(localizedOriginalPrice).Append("\n");
			stringBuilder.Append("  originalPrice: ").Append(originalPrice).Append("\n");
			stringBuilder.Append("  currentPrice: ").Append(currentPrice).Append("\n");
			stringBuilder.Append("  localizedCurrentPrice: ").Append(localizedCurrentPrice).Append("\n");
			stringBuilder.Append("  currencyCode: ").Append(currencyCode).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
