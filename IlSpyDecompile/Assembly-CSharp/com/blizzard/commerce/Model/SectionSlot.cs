using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class SectionSlot
	{
		public string sectionSlotId;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class SectionSlot {\n");
			stringBuilder.Append("  sectionSlotId: ").Append(sectionSlotId).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
