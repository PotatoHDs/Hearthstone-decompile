using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B52 RID: 2898
	[Serializable]
	public class CacheControl
	{
		// Token: 0x06009A79 RID: 39545 RVA: 0x0031B2B0 File Offset: 0x003194B0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class CacheControl {\n");
			stringBuilder.Append("  maxAgeDurationS: ").Append(this.maxAgeDurationS).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008019 RID: 32793
		public int maxAgeDurationS;
	}
}
