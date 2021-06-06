using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class MetaData
	{
		public CacheControl cacheControl;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class MetaData {\n");
			stringBuilder.Append("  cacheControl: ").Append(cacheControl).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
