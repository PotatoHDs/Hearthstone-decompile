using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B6D RID: 2925
	[Serializable]
	public class MetaData
	{
		// Token: 0x06009AAE RID: 39598 RVA: 0x0031C184 File Offset: 0x0031A384
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class MetaData {\n");
			stringBuilder.Append("  cacheControl: ").Append(this.cacheControl).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008068 RID: 32872
		public CacheControl cacheControl;
	}
}
