using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B85 RID: 2949
	[Serializable]
	public class Section
	{
		// Token: 0x06009ADE RID: 39646 RVA: 0x0031D0B4 File Offset: 0x0031B2B4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class Section {\n");
			stringBuilder.Append("  localization: ").Append(this.localization).Append("\n");
			stringBuilder.Append("  slots: ").Append(this.slots).Append("\n");
			stringBuilder.Append("  name: ").Append(this.name).Append("\n");
			stringBuilder.Append("  attributes: ").Append(this.attributes).Append("\n");
			stringBuilder.Append("  sectionId: ").Append(this.sectionId).Append("\n");
			stringBuilder.Append("  productCollections: ").Append(this.productCollections).Append("\n");
			stringBuilder.Append("  orderInPage: ").Append(this.orderInPage).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x040080BA RID: 32954
		public SectionLocalization localization;

		// Token: 0x040080BB RID: 32955
		public List<SectionSlot> slots;

		// Token: 0x040080BC RID: 32956
		public string name;

		// Token: 0x040080BD RID: 32957
		public List<SectionAttribute> attributes;

		// Token: 0x040080BE RID: 32958
		public string sectionId;

		// Token: 0x040080BF RID: 32959
		public List<ProductCollection> productCollections;

		// Token: 0x040080C0 RID: 32960
		public int orderInPage;
	}
}
