using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B60 RID: 2912
	[Serializable]
	public class GetBalanceResponse
	{
		// Token: 0x06009A94 RID: 39572 RVA: 0x0031B878 File Offset: 0x00319A78
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class GetBalanceResponse {\n");
			stringBuilder.Append("  balance: ").Append(this.balance).Append("\n");
			stringBuilder.Append("  error: ").Append(this.error).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008035 RID: 32821
		public LedgerBalance balance;

		// Token: 0x04008036 RID: 32822
		public RpcError error;
	}
}
