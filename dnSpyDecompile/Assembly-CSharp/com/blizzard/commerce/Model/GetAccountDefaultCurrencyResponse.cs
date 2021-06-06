using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B5E RID: 2910
	[Serializable]
	public class GetAccountDefaultCurrencyResponse
	{
		// Token: 0x06009A90 RID: 39568 RVA: 0x0031B790 File Offset: 0x00319990
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class GetAccountDefaultCurrencyResponse {\n");
			stringBuilder.Append("  currencyAlphaCode: ").Append(this.currencyAlphaCode).Append("\n");
			stringBuilder.Append("  error: ").Append(this.error).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008031 RID: 32817
		public string currencyAlphaCode;

		// Token: 0x04008032 RID: 32818
		public RpcError error;
	}
}
