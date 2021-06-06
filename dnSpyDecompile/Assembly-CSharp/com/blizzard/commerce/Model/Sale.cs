using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B83 RID: 2947
	[Serializable]
	public class Sale
	{
		// Token: 0x06009ADA RID: 39642 RVA: 0x0031CF28 File Offset: 0x0031B128
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class Sale {\n");
			stringBuilder.Append("  localization: ").Append(this.localization).Append("\n");
			stringBuilder.Append("  salePurchaseEndTimeMs: ").Append(this.salePurchaseEndTimeMs).Append("\n");
			stringBuilder.Append("  saleStartTimeMs: ").Append(this.saleStartTimeMs).Append("\n");
			stringBuilder.Append("  saleId: ").Append(this.saleId).Append("\n");
			stringBuilder.Append("  name: ").Append(this.name).Append("\n");
			stringBuilder.Append("  saleDisplayEndTimeMs: ").Append(this.saleDisplayEndTimeMs).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x040080B1 RID: 32945
		public SaleLocalization localization;

		// Token: 0x040080B2 RID: 32946
		public int salePurchaseEndTimeMs;

		// Token: 0x040080B3 RID: 32947
		public int saleStartTimeMs;

		// Token: 0x040080B4 RID: 32948
		public int saleId;

		// Token: 0x040080B5 RID: 32949
		public string name;

		// Token: 0x040080B6 RID: 32950
		public int saleDisplayEndTimeMs;
	}
}
