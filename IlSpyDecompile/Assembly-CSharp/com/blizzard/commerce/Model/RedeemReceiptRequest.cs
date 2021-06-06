using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class RedeemReceiptRequest
	{
		public string journalId;

		public string receipt;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class RedeemReceiptRequest {\n");
			stringBuilder.Append("  journalId: ").Append(journalId).Append("\n");
			stringBuilder.Append("  receipt: ").Append(receipt).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
