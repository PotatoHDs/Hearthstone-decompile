using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B86 RID: 2950
	[Serializable]
	public class SectionAttribute
	{
		// Token: 0x06009AE0 RID: 39648 RVA: 0x0031D1CC File Offset: 0x0031B3CC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class SectionAttribute {\n");
			stringBuilder.Append("  sectionAttributeKey: ").Append(this.sectionAttributeKey).Append("\n");
			stringBuilder.Append("  sectionAttributeValue: ").Append(this.sectionAttributeValue).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x040080C1 RID: 32961
		public string sectionAttributeKey;

		// Token: 0x040080C2 RID: 32962
		public string sectionAttributeValue;
	}
}
