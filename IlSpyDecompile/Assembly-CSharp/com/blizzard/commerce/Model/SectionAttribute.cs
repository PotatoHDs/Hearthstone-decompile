using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class SectionAttribute
	{
		public string sectionAttributeKey;

		public string sectionAttributeValue;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class SectionAttribute {\n");
			stringBuilder.Append("  sectionAttributeKey: ").Append(sectionAttributeKey).Append("\n");
			stringBuilder.Append("  sectionAttributeValue: ").Append(sectionAttributeValue).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
