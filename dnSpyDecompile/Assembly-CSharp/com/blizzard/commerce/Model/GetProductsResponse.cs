using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B66 RID: 2918
	[Serializable]
	public class GetProductsResponse
	{
		// Token: 0x06009AA0 RID: 39584 RVA: 0x0031BCF8 File Offset: 0x00319EF8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class GetProductsResponse {\n");
			stringBuilder.Append("  paginationToken: ").Append(this.paginationToken).Append("\n");
			stringBuilder.Append("  metaData: ").Append(this.metaData).Append("\n");
			stringBuilder.Append("  error: ").Append(this.error).Append("\n");
			stringBuilder.Append("  products: ").Append(this.products).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0400804F RID: 32847
		public string paginationToken;

		// Token: 0x04008050 RID: 32848
		public MetaData metaData;

		// Token: 0x04008051 RID: 32849
		public RpcError error;

		// Token: 0x04008052 RID: 32850
		public List<Product> products;
	}
}
