using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class VirtualCurrencyGrant
	{
		public string amount;

		public string currencyCode;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class VirtualCurrencyGrant {\n");
			stringBuilder.Append("  amount: ").Append(amount).Append("\n");
			stringBuilder.Append("  currencyCode: ").Append(currencyCode).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
