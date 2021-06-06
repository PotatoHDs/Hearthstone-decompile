using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B73 RID: 2931
	[Serializable]
	public class ProductCollectionItem
	{
		// Token: 0x06009ABA RID: 39610 RVA: 0x0031C644 File Offset: 0x0031A844
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ProductCollectionItem {\n");
			stringBuilder.Append("  orderInProductCollection: ").Append(this.orderInProductCollection).Append("\n");
			stringBuilder.Append("  productCollectionItemTypeId: ").Append(this.productCollectionItemTypeId).Append("\n");
			stringBuilder.Append("  productCollectionItemValue: ").Append(this.productCollectionItemValue).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008084 RID: 32900
		public int orderInProductCollection;

		// Token: 0x04008085 RID: 32901
		public int productCollectionItemTypeId;

		// Token: 0x04008086 RID: 32902
		public int productCollectionItemValue;
	}
}
