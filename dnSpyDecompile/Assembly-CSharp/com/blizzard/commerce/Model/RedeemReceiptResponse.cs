using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B7D RID: 2941
	[Serializable]
	public class RedeemReceiptResponse
	{
		// Token: 0x06009ACE RID: 39630 RVA: 0x0031CBF0 File Offset: 0x0031ADF0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class RedeemReceiptResponse {\n");
			stringBuilder.Append("  journal: ").Append(this.journal).Append("\n");
			stringBuilder.Append("  error: ").Append(this.error).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x040080A1 RID: 32929
		public Journal journal;

		// Token: 0x040080A2 RID: 32930
		public RpcError error;
	}
}
