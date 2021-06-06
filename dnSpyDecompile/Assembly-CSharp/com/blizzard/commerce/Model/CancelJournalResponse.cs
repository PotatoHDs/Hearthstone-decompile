using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B54 RID: 2900
	[Serializable]
	public class CancelJournalResponse
	{
		// Token: 0x06009A7D RID: 39549 RVA: 0x0031B350 File Offset: 0x00319550
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class CancelJournalResponse {\n");
			stringBuilder.Append("  journal: ").Append(this.journal).Append("\n");
			stringBuilder.Append("  error: ").Append(this.error).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0400801B RID: 32795
		public Journal journal;

		// Token: 0x0400801C RID: 32796
		public RpcError error;
	}
}
