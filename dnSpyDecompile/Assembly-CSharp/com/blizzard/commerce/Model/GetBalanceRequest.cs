using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B5F RID: 2911
	[Serializable]
	public class GetBalanceRequest
	{
		// Token: 0x06009A92 RID: 39570 RVA: 0x0031B804 File Offset: 0x00319A04
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class GetBalanceRequest {\n");
			stringBuilder.Append("  ledgerKey: ").Append(this.ledgerKey).Append("\n");
			stringBuilder.Append("  currencyCode: ").Append(this.currencyCode).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008033 RID: 32819
		public LedgerKey ledgerKey;

		// Token: 0x04008034 RID: 32820
		public string currencyCode;
	}
}
