using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B58 RID: 2904
	[Serializable]
	public class CreateJournalResponse
	{
		// Token: 0x06009A85 RID: 39557 RVA: 0x0031B55C File Offset: 0x0031975C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class CreateJournalResponse {\n");
			stringBuilder.Append("  journal: ").Append(this.journal).Append("\n");
			stringBuilder.Append("  error: ").Append(this.error).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008025 RID: 32805
		public Journal journal;

		// Token: 0x04008026 RID: 32806
		public RpcError error;
	}
}
