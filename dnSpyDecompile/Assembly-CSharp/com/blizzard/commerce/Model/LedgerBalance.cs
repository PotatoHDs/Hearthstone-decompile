using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B68 RID: 2920
	[Serializable]
	public class LedgerBalance
	{
		// Token: 0x06009AA4 RID: 39588 RVA: 0x0031BF04 File Offset: 0x0031A104
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class LedgerBalance {\n");
			stringBuilder.Append("  ledgerId: ").Append(this.ledgerId).Append("\n");
			stringBuilder.Append("  balance: ").Append(this.balance).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0400805C RID: 32860
		public LedgerId ledgerId;

		// Token: 0x0400805D RID: 32861
		public string balance;
	}
}
