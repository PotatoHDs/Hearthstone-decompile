using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class PlaceOrderWithVCResponse
	{
		public string externalTransactionId;

		public RpcError error;

		public string status;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class PlaceOrderWithVCResponse {\n");
			stringBuilder.Append("  externalTransactionId: ").Append(externalTransactionId).Append("\n");
			stringBuilder.Append("  error: ").Append(error).Append("\n");
			stringBuilder.Append("  status: ").Append(status).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
