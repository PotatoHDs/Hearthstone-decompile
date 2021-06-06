using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B53 RID: 2899
	[Serializable]
	public class CancelJournalRequest
	{
		// Token: 0x06009A7B RID: 39547 RVA: 0x0031B300 File Offset: 0x00319500
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class CancelJournalRequest {\n");
			stringBuilder.Append("  journalId: ").Append(this.journalId).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0400801A RID: 32794
		public string journalId;
	}
}
