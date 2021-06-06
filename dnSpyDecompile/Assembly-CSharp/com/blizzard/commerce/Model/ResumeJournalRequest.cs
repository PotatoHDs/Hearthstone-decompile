using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B7E RID: 2942
	[Serializable]
	public class ResumeJournalRequest
	{
		// Token: 0x06009AD0 RID: 39632 RVA: 0x0031CC64 File Offset: 0x0031AE64
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ResumeJournalRequest {\n");
			stringBuilder.Append("  createIfMissing: ").Append(this.createIfMissing).Append("\n");
			stringBuilder.Append("  journalId: ").Append(this.journalId).Append("\n");
			stringBuilder.Append("  receipt: ").Append(this.receipt).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x040080A3 RID: 32931
		public CreationParameters createIfMissing;

		// Token: 0x040080A4 RID: 32932
		public string journalId;

		// Token: 0x040080A5 RID: 32933
		public string receipt;
	}
}
