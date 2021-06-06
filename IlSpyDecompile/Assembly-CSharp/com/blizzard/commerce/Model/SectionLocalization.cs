using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class SectionLocalization
	{
		public string name;

		public string description;

		public string locale;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class SectionLocalization {\n");
			stringBuilder.Append("  name: ").Append(name).Append("\n");
			stringBuilder.Append("  description: ").Append(description).Append("\n");
			stringBuilder.Append("  locale: ").Append(locale).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
