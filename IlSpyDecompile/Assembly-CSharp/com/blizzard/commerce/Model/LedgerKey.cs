using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class LedgerKey
	{
		public int gameServiceRegionId;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class LedgerKey {\n");
			stringBuilder.Append("  gameServiceRegionId: ").Append(gameServiceRegionId).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
