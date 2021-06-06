using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class RpcErrorInfo
	{
		public string code;

		public string value;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class RpcErrorInfo {\n");
			stringBuilder.Append("  code: ").Append(code).Append("\n");
			stringBuilder.Append("  value: ").Append(value).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
