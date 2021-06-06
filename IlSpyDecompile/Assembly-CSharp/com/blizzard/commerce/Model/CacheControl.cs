using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class CacheControl
	{
		public int maxAgeDurationS;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class CacheControl {\n");
			stringBuilder.Append("  maxAgeDurationS: ").Append(maxAgeDurationS).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
