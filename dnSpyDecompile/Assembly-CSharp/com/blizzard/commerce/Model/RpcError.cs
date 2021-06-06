using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B80 RID: 2944
	[Serializable]
	public class RpcError
	{
		// Token: 0x06009AD4 RID: 39636 RVA: 0x0031CD6C File Offset: 0x0031AF6C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class RpcError {\n");
			stringBuilder.Append("  code: ").Append(this.code).Append("\n");
			stringBuilder.Append("  errorHierarchy: ").Append(this.errorHierarchy).Append("\n");
			stringBuilder.Append("  supplementalInfo: ").Append(this.supplementalInfo).Append("\n");
			stringBuilder.Append("  message: ").Append(this.message).Append("\n");
			stringBuilder.Append("  category: ").Append(this.category).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x040080A8 RID: 32936
		public string code;

		// Token: 0x040080A9 RID: 32937
		public List<RpcErrorInfo> errorHierarchy;

		// Token: 0x040080AA RID: 32938
		public List<RpcErrorSupplementalInfo> supplementalInfo;

		// Token: 0x040080AB RID: 32939
		public string message;

		// Token: 0x040080AC RID: 32940
		public string category;
	}
}
