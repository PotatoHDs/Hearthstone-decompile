using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B6B RID: 2923
	[Serializable]
	public class License
	{
		// Token: 0x06009AAA RID: 39594 RVA: 0x0031C05C File Offset: 0x0031A25C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class License {\n");
			stringBuilder.Append("  licenseType: ").Append(this.licenseType).Append("\n");
			stringBuilder.Append("  licenseAttributes: ").Append(this.licenseAttributes).Append("\n");
			stringBuilder.Append("  name: ").Append(this.name).Append("\n");
			stringBuilder.Append("  licenseId: ").Append(this.licenseId).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008062 RID: 32866
		public int licenseType;

		// Token: 0x04008063 RID: 32867
		public List<LicenseAttribute> licenseAttributes;

		// Token: 0x04008064 RID: 32868
		public string name;

		// Token: 0x04008065 RID: 32869
		public int licenseId;
	}
}
