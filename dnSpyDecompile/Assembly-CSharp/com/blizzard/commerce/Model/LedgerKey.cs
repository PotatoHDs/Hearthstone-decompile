using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B6A RID: 2922
	[Serializable]
	public class LedgerKey
	{
		// Token: 0x06009AA8 RID: 39592 RVA: 0x0031C00C File Offset: 0x0031A20C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class LedgerKey {\n");
			stringBuilder.Append("  gameServiceRegionId: ").Append(this.gameServiceRegionId).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008061 RID: 32865
		public int gameServiceRegionId;
	}
}
