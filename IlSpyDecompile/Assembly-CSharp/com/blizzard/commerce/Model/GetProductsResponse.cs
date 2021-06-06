using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class GetProductsResponse
	{
		public string paginationToken;

		public MetaData metaData;

		public RpcError error;

		public List<Product> products;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class GetProductsResponse {\n");
			stringBuilder.Append("  paginationToken: ").Append(paginationToken).Append("\n");
			stringBuilder.Append("  metaData: ").Append(metaData).Append("\n");
			stringBuilder.Append("  error: ").Append(error).Append("\n");
			stringBuilder.Append("  products: ").Append(products).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
