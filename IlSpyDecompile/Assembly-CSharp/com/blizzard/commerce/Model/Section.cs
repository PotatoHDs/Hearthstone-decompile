using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class Section
	{
		public SectionLocalization localization;

		public List<SectionSlot> slots;

		public string name;

		public List<SectionAttribute> attributes;

		public string sectionId;

		public List<ProductCollection> productCollections;

		public int orderInPage;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class Section {\n");
			stringBuilder.Append("  localization: ").Append(localization).Append("\n");
			stringBuilder.Append("  slots: ").Append(slots).Append("\n");
			stringBuilder.Append("  name: ").Append(name).Append("\n");
			stringBuilder.Append("  attributes: ").Append(attributes).Append("\n");
			stringBuilder.Append("  sectionId: ").Append(sectionId).Append("\n");
			stringBuilder.Append("  productCollections: ").Append(productCollections).Append("\n");
			stringBuilder.Append("  orderInPage: ").Append(orderInPage).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
