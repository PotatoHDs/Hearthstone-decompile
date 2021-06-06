using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B81 RID: 2945
	[Serializable]
	public class RpcErrorInfo
	{
		// Token: 0x06009AD6 RID: 39638 RVA: 0x0031CE40 File Offset: 0x0031B040
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class RpcErrorInfo {\n");
			stringBuilder.Append("  code: ").Append(this.code).Append("\n");
			stringBuilder.Append("  value: ").Append(this.value).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x040080AD RID: 32941
		public string code;

		// Token: 0x040080AE RID: 32942
		public string value;
	}
}
