using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B7F RID: 2943
	[Serializable]
	public class ResumeJournalResponse
	{
		// Token: 0x06009AD2 RID: 39634 RVA: 0x0031CCF8 File Offset: 0x0031AEF8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ResumeJournalResponse {\n");
			stringBuilder.Append("  journal: ").Append(this.journal).Append("\n");
			stringBuilder.Append("  error: ").Append(this.error).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x040080A6 RID: 32934
		public Journal journal;

		// Token: 0x040080A7 RID: 32935
		public RpcError error;
	}
}
