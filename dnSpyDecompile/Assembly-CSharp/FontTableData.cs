using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000840 RID: 2112
public class FontTableData : ScriptableObject
{
	// Token: 0x04005A73 RID: 23155
	public List<FontTableData.FontTableEntry> m_Entries;

	// Token: 0x02002412 RID: 9234
	[Serializable]
	public class FontTableEntry
	{
		// Token: 0x0400E93A RID: 59706
		public string m_FontDefName;

		// Token: 0x0400E93B RID: 59707
		public string m_FontName;

		// Token: 0x0400E93C RID: 59708
		public string m_FontGuid;
	}
}
