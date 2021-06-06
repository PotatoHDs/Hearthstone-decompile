using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class Sale
	{
		public SaleLocalization localization;

		public int salePurchaseEndTimeMs;

		public int saleStartTimeMs;

		public int saleId;

		public string name;

		public int saleDisplayEndTimeMs;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class Sale {\n");
			stringBuilder.Append("  localization: ").Append(localization).Append("\n");
			stringBuilder.Append("  salePurchaseEndTimeMs: ").Append(salePurchaseEndTimeMs).Append("\n");
			stringBuilder.Append("  saleStartTimeMs: ").Append(saleStartTimeMs).Append("\n");
			stringBuilder.Append("  saleId: ").Append(saleId).Append("\n");
			stringBuilder.Append("  name: ").Append(name).Append("\n");
			stringBuilder.Append("  saleDisplayEndTimeMs: ").Append(saleDisplayEndTimeMs).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
