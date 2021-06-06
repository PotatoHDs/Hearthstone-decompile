using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class ProductOffer
	{
		public int offerDisplayEndTimeMs;

		public int offerPurchaseEndTimeMs;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ProductOffer {\n");
			stringBuilder.Append("  offerDisplayEndTimeMs: ").Append(offerDisplayEndTimeMs).Append("\n");
			stringBuilder.Append("  offerPurchaseEndTimeMs: ").Append(offerPurchaseEndTimeMs).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
