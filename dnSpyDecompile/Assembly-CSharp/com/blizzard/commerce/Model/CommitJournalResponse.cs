using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B56 RID: 2902
	[Serializable]
	public class CommitJournalResponse
	{
		// Token: 0x06009A81 RID: 39553 RVA: 0x0031B414 File Offset: 0x00319614
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class CommitJournalResponse {\n");
			stringBuilder.Append("  journal: ").Append(this.journal).Append("\n");
			stringBuilder.Append("  error: ").Append(this.error).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0400801E RID: 32798
		public Journal journal;

		// Token: 0x0400801F RID: 32799
		public RpcError error;
	}
}
