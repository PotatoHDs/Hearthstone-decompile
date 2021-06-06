using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class GetBalanceResponse
	{
		public LedgerBalance balance;

		public RpcError error;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class GetBalanceResponse {\n");
			stringBuilder.Append("  balance: ").Append(balance).Append("\n");
			stringBuilder.Append("  error: ").Append(error).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
