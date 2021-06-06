using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class GetPageRequest
	{
		public int gameServiceRegionId;

		public string pageId;

		public string locale;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class GetPageRequest {\n");
			stringBuilder.Append("  gameServiceRegionId: ").Append(gameServiceRegionId).Append("\n");
			stringBuilder.Append("  pageId: ").Append(pageId).Append("\n");
			stringBuilder.Append("  locale: ").Append(locale).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
