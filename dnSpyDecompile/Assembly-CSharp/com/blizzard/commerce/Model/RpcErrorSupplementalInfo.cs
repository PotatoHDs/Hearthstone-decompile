using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B82 RID: 2946
	[Serializable]
	public class RpcErrorSupplementalInfo
	{
		// Token: 0x06009AD8 RID: 39640 RVA: 0x0031CEB4 File Offset: 0x0031B0B4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class RpcErrorSupplementalInfo {\n");
			stringBuilder.Append("  type: ").Append(this.type).Append("\n");
			stringBuilder.Append("  value: ").Append(this.value).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x040080AF RID: 32943
		public string type;

		// Token: 0x040080B0 RID: 32944
		public string value;
	}
}
