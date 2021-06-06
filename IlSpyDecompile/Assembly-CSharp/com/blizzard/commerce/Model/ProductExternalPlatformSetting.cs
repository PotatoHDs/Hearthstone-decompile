using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class ProductExternalPlatformSetting
	{
		public int externalPlatformId;

		public string name;

		public int gameServiceRegionId;

		public string externalPlatformProductId;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ProductExternalPlatformSetting {\n");
			stringBuilder.Append("  externalPlatformId: ").Append(externalPlatformId).Append("\n");
			stringBuilder.Append("  name: ").Append(name).Append("\n");
			stringBuilder.Append("  gameServiceRegionId: ").Append(gameServiceRegionId).Append("\n");
			stringBuilder.Append("  externalPlatformProductId: ").Append(externalPlatformProductId).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
