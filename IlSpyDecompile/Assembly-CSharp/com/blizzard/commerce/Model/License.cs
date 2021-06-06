using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class License
	{
		public int licenseType;

		public List<LicenseAttribute> licenseAttributes;

		public string name;

		public int licenseId;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class License {\n");
			stringBuilder.Append("  licenseType: ").Append(licenseType).Append("\n");
			stringBuilder.Append("  licenseAttributes: ").Append(licenseAttributes).Append("\n");
			stringBuilder.Append("  name: ").Append(name).Append("\n");
			stringBuilder.Append("  licenseId: ").Append(licenseId).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
