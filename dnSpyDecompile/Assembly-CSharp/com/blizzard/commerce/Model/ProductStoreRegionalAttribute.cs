using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B79 RID: 2937
	[Serializable]
	public class ProductStoreRegionalAttribute
	{
		// Token: 0x06009AC6 RID: 39622 RVA: 0x0031CA00 File Offset: 0x0031AC00
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ProductStoreRegionalAttribute {\n");
			stringBuilder.Append("  value: ").Append(this.value).Append("\n");
			stringBuilder.Append("  key: ").Append(this.key).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008098 RID: 32920
		public string value;

		// Token: 0x04008099 RID: 32921
		public string key;
	}
}
