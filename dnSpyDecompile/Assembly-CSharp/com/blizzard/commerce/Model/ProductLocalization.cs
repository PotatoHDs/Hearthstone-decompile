using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B75 RID: 2933
	[Serializable]
	public class ProductLocalization
	{
		// Token: 0x06009ABE RID: 39614 RVA: 0x0031C78C File Offset: 0x0031A98C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ProductLocalization {\n");
			stringBuilder.Append("  name: ").Append(this.name).Append("\n");
			stringBuilder.Append("  description: ").Append(this.description).Append("\n");
			stringBuilder.Append("  locale: ").Append(this.locale).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0400808B RID: 32907
		public string name;

		// Token: 0x0400808C RID: 32908
		public string description;

		// Token: 0x0400808D RID: 32909
		public string locale;
	}
}
