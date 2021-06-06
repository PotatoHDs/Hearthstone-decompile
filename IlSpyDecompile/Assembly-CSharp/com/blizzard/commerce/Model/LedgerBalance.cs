using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class LedgerBalance
	{
		public LedgerId ledgerId;

		public string balance;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class LedgerBalance {\n");
			stringBuilder.Append("  ledgerId: ").Append(ledgerId).Append("\n");
			stringBuilder.Append("  balance: ").Append(balance).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
