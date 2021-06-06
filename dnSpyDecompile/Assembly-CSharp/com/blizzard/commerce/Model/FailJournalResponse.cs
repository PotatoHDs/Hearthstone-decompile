using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B5C RID: 2908
	[Serializable]
	public class FailJournalResponse
	{
		// Token: 0x06009A8C RID: 39564 RVA: 0x0031B6F8 File Offset: 0x003198F8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class FailJournalResponse {\n");
			stringBuilder.Append("  journal: ").Append(this.journal).Append("\n");
			stringBuilder.Append("  error: ").Append(this.error).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0400802F RID: 32815
		public Journal journal;

		// Token: 0x04008030 RID: 32816
		public RpcError error;
	}
}
