using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B5B RID: 2907
	[Serializable]
	public class FailJournalRequest
	{
		// Token: 0x06009A8A RID: 39562 RVA: 0x0031B664 File Offset: 0x00319864
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class FailJournalRequest {\n");
			stringBuilder.Append("  journalId: ").Append(this.journalId).Append("\n");
			stringBuilder.Append("  errorMessage: ").Append(this.errorMessage).Append("\n");
			stringBuilder.Append("  errorCode: ").Append(this.errorCode).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0400802C RID: 32812
		public string journalId;

		// Token: 0x0400802D RID: 32813
		public string errorMessage;

		// Token: 0x0400802E RID: 32814
		public int errorCode;
	}
}
