using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B6C RID: 2924
	[Serializable]
	public class LicenseAttribute
	{
		// Token: 0x06009AAC RID: 39596 RVA: 0x0031C110 File Offset: 0x0031A310
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class LicenseAttribute {\n");
			stringBuilder.Append("  value: ").Append(this.value).Append("\n");
			stringBuilder.Append("  key: ").Append(this.key).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008066 RID: 32870
		public string value;

		// Token: 0x04008067 RID: 32871
		public string key;
	}
}
