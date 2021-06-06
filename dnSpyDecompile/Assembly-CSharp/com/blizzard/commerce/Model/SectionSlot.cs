using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B88 RID: 2952
	[Serializable]
	public class SectionSlot
	{
		// Token: 0x06009AE4 RID: 39652 RVA: 0x0031D2D4 File Offset: 0x0031B4D4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class SectionSlot {\n");
			stringBuilder.Append("  sectionSlotId: ").Append(this.sectionSlotId).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x040080C6 RID: 32966
		public string sectionSlotId;
	}
}
