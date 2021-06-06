using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class CreationParameters
	{
		public int pointOfSaleId;

		public int gameServiceRegionId;

		public int productType;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class CreationParameters {\n");
			stringBuilder.Append("  pointOfSaleId: ").Append(pointOfSaleId).Append("\n");
			stringBuilder.Append("  gameServiceRegionId: ").Append(gameServiceRegionId).Append("\n");
			stringBuilder.Append("  productType: ").Append(productType).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
