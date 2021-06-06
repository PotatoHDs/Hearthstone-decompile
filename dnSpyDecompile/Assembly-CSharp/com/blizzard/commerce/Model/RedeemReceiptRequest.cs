using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B7C RID: 2940
	[Serializable]
	public class RedeemReceiptRequest
	{
		// Token: 0x06009ACC RID: 39628 RVA: 0x0031CB7C File Offset: 0x0031AD7C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class RedeemReceiptRequest {\n");
			stringBuilder.Append("  journalId: ").Append(this.journalId).Append("\n");
			stringBuilder.Append("  receipt: ").Append(this.receipt).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0400809F RID: 32927
		public string journalId;

		// Token: 0x040080A0 RID: 32928
		public string receipt;
	}
}
