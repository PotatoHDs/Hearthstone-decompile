using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B8A RID: 2954
	[Serializable]
	public class VirtualCurrencyGrant
	{
		// Token: 0x06009AE7 RID: 39655 RVA: 0x0031D324 File Offset: 0x0031B524
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class VirtualCurrencyGrant {\n");
			stringBuilder.Append("  amount: ").Append(this.amount).Append("\n");
			stringBuilder.Append("  currencyCode: ").Append(this.currencyCode).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x040080CA RID: 32970
		public string amount;

		// Token: 0x040080CB RID: 32971
		public string currencyCode;
	}
}
