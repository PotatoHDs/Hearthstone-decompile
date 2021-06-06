using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B70 RID: 2928
	[Serializable]
	public class PlaceOrderWithVCResponse
	{
		// Token: 0x06009AB4 RID: 39604 RVA: 0x0031C360 File Offset: 0x0031A560
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class PlaceOrderWithVCResponse {\n");
			stringBuilder.Append("  externalTransactionId: ").Append(this.externalTransactionId).Append("\n");
			stringBuilder.Append("  error: ").Append(this.error).Append("\n");
			stringBuilder.Append("  status: ").Append(this.status).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008072 RID: 32882
		public string externalTransactionId;

		// Token: 0x04008073 RID: 32883
		public RpcError error;

		// Token: 0x04008074 RID: 32884
		public string status;
	}
}
