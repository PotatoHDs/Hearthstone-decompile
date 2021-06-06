using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B74 RID: 2932
	[Serializable]
	public class ProductExternalPlatformSetting
	{
		// Token: 0x06009ABC RID: 39612 RVA: 0x0031C6D8 File Offset: 0x0031A8D8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ProductExternalPlatformSetting {\n");
			stringBuilder.Append("  externalPlatformId: ").Append(this.externalPlatformId).Append("\n");
			stringBuilder.Append("  name: ").Append(this.name).Append("\n");
			stringBuilder.Append("  gameServiceRegionId: ").Append(this.gameServiceRegionId).Append("\n");
			stringBuilder.Append("  externalPlatformProductId: ").Append(this.externalPlatformProductId).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008087 RID: 32903
		public int externalPlatformId;

		// Token: 0x04008088 RID: 32904
		public string name;

		// Token: 0x04008089 RID: 32905
		public int gameServiceRegionId;

		// Token: 0x0400808A RID: 32906
		public string externalPlatformProductId;
	}
}
