using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class GetPageResponse
	{
		public Page page;

		public RpcError error;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class GetPageResponse {\n");
			stringBuilder.Append("  page: ").Append(page).Append("\n");
			stringBuilder.Append("  error: ").Append(error).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
