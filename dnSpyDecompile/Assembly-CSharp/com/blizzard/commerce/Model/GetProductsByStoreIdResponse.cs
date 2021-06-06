using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B64 RID: 2916
	[Serializable]
	public class GetProductsByStoreIdResponse
	{
		// Token: 0x06009A9C RID: 39580 RVA: 0x0031BB0C File Offset: 0x00319D0C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class GetProductsByStoreIdResponse {\n");
			stringBuilder.Append("  paginationToken: ").Append(this.paginationToken).Append("\n");
			stringBuilder.Append("  metaData: ").Append(this.metaData).Append("\n");
			stringBuilder.Append("  error: ").Append(this.error).Append("\n");
			stringBuilder.Append("  products: ").Append(this.products).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008043 RID: 32835
		public string paginationToken;

		// Token: 0x04008044 RID: 32836
		public MetaData metaData;

		// Token: 0x04008045 RID: 32837
		public RpcError error;

		// Token: 0x04008046 RID: 32838
		public List<Product> products;
	}
}
