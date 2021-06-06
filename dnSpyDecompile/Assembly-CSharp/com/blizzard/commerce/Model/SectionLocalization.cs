using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B87 RID: 2951
	[Serializable]
	public class SectionLocalization
	{
		// Token: 0x06009AE2 RID: 39650 RVA: 0x0031D240 File Offset: 0x0031B440
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class SectionLocalization {\n");
			stringBuilder.Append("  name: ").Append(this.name).Append("\n");
			stringBuilder.Append("  description: ").Append(this.description).Append("\n");
			stringBuilder.Append("  locale: ").Append(this.locale).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x040080C3 RID: 32963
		public string name;

		// Token: 0x040080C4 RID: 32964
		public string description;

		// Token: 0x040080C5 RID: 32965
		public string locale;
	}
}
