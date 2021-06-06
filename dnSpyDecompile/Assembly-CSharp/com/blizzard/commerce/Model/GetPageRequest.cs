using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B61 RID: 2913
	[Serializable]
	public class GetPageRequest
	{
		// Token: 0x06009A96 RID: 39574 RVA: 0x0031B8EC File Offset: 0x00319AEC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class GetPageRequest {\n");
			stringBuilder.Append("  gameServiceRegionId: ").Append(this.gameServiceRegionId).Append("\n");
			stringBuilder.Append("  pageId: ").Append(this.pageId).Append("\n");
			stringBuilder.Append("  locale: ").Append(this.locale).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008037 RID: 32823
		public int gameServiceRegionId;

		// Token: 0x04008038 RID: 32824
		public string pageId;

		// Token: 0x04008039 RID: 32825
		public string locale;
	}
}
