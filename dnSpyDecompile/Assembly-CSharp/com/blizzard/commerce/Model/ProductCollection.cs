using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B72 RID: 2930
	[Serializable]
	public class ProductCollection
	{
		// Token: 0x06009AB8 RID: 39608 RVA: 0x0031C590 File Offset: 0x0031A790
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ProductCollection {\n");
			stringBuilder.Append("  orderInSection: ").Append(this.orderInSection).Append("\n");
			stringBuilder.Append("  productCollectionId: ").Append(this.productCollectionId).Append("\n");
			stringBuilder.Append("  name: ").Append(this.name).Append("\n");
			stringBuilder.Append("  items: ").Append(this.items).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008080 RID: 32896
		public int orderInSection;

		// Token: 0x04008081 RID: 32897
		public string productCollectionId;

		// Token: 0x04008082 RID: 32898
		public string name;

		// Token: 0x04008083 RID: 32899
		public List<ProductCollectionItem> items;
	}
}
