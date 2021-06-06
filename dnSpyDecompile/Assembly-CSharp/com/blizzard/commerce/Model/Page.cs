using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B6E RID: 2926
	[Serializable]
	public class Page
	{
		// Token: 0x06009AB0 RID: 39600 RVA: 0x0031C1D4 File Offset: 0x0031A3D4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class Page {\n");
			stringBuilder.Append("  name: ").Append(this.name).Append("\n");
			stringBuilder.Append("  pageId: ").Append(this.pageId).Append("\n");
			stringBuilder.Append("  sections: ").Append(this.sections).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008069 RID: 32873
		public string name;

		// Token: 0x0400806A RID: 32874
		public string pageId;

		// Token: 0x0400806B RID: 32875
		public List<Section> sections;
	}
}
