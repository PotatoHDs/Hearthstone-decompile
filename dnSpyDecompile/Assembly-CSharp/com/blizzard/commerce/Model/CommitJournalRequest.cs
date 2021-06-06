using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B55 RID: 2901
	[Serializable]
	public class CommitJournalRequest
	{
		// Token: 0x06009A7F RID: 39551 RVA: 0x0031B3C4 File Offset: 0x003195C4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class CommitJournalRequest {\n");
			stringBuilder.Append("  journalId: ").Append(this.journalId).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0400801D RID: 32797
		public string journalId;
	}
}
