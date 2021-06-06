using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B62 RID: 2914
	[Serializable]
	public class GetPageResponse
	{
		// Token: 0x06009A98 RID: 39576 RVA: 0x0031B980 File Offset: 0x00319B80
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class GetPageResponse {\n");
			stringBuilder.Append("  page: ").Append(this.page).Append("\n");
			stringBuilder.Append("  error: ").Append(this.error).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0400803A RID: 32826
		public Page page;

		// Token: 0x0400803B RID: 32827
		public RpcError error;
	}
}
