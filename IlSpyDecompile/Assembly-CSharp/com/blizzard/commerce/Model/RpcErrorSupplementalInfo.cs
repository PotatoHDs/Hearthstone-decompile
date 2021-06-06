using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class RpcErrorSupplementalInfo
	{
		public string type;

		public string value;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class RpcErrorSupplementalInfo {\n");
			stringBuilder.Append("  type: ").Append(type).Append("\n");
			stringBuilder.Append("  value: ").Append(value).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
