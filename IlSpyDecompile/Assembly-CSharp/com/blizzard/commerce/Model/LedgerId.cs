using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class LedgerId
	{
		public int accountId;

		public LedgerKey ledgerKey;

		public string currencyCode;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class LedgerId {\n");
			stringBuilder.Append("  accountId: ").Append(accountId).Append("\n");
			stringBuilder.Append("  ledgerKey: ").Append(ledgerKey).Append("\n");
			stringBuilder.Append("  currencyCode: ").Append(currencyCode).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
