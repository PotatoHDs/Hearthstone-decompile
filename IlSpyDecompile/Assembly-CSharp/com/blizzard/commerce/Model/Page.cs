using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class Page
	{
		public string name;

		public string pageId;

		public List<Section> sections;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class Page {\n");
			stringBuilder.Append("  name: ").Append(name).Append("\n");
			stringBuilder.Append("  pageId: ").Append(pageId).Append("\n");
			stringBuilder.Append("  sections: ").Append(sections).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
