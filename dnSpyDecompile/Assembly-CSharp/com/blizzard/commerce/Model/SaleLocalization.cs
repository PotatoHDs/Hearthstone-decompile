using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B84 RID: 2948
	[Serializable]
	public class SaleLocalization
	{
		// Token: 0x06009ADC RID: 39644 RVA: 0x0031D020 File Offset: 0x0031B220
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class SaleLocalization {\n");
			stringBuilder.Append("  name: ").Append(this.name).Append("\n");
			stringBuilder.Append("  description: ").Append(this.description).Append("\n");
			stringBuilder.Append("  locale: ").Append(this.locale).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x040080B7 RID: 32951
		public string name;

		// Token: 0x040080B8 RID: 32952
		public string description;

		// Token: 0x040080B9 RID: 32953
		public string locale;
	}
}
