using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B78 RID: 2936
	[Serializable]
	public class ProductStoreAttribute
	{
		// Token: 0x06009AC4 RID: 39620 RVA: 0x0031C98C File Offset: 0x0031AB8C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ProductStoreAttribute {\n");
			stringBuilder.Append("  value: ").Append(this.value).Append("\n");
			stringBuilder.Append("  key: ").Append(this.key).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008096 RID: 32918
		public string value;

		// Token: 0x04008097 RID: 32919
		public string key;
	}
}
