using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class RpcError
	{
		public string code;

		public List<RpcErrorInfo> errorHierarchy;

		public List<RpcErrorSupplementalInfo> supplementalInfo;

		public string message;

		public string category;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class RpcError {\n");
			stringBuilder.Append("  code: ").Append(code).Append("\n");
			stringBuilder.Append("  errorHierarchy: ").Append(errorHierarchy).Append("\n");
			stringBuilder.Append("  supplementalInfo: ").Append(supplementalInfo).Append("\n");
			stringBuilder.Append("  message: ").Append(message).Append("\n");
			stringBuilder.Append("  category: ").Append(category).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
