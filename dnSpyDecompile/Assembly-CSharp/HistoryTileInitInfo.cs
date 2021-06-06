using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000314 RID: 788
public class HistoryTileInitInfo : HistoryItemInitInfo
{
	// Token: 0x0400185E RID: 6238
	public HistoryInfoType m_type;

	// Token: 0x0400185F RID: 6239
	public List<HistoryInfo> m_childInfos;

	// Token: 0x04001860 RID: 6240
	public Texture m_fatigueTexture;

	// Token: 0x04001861 RID: 6241
	public Texture m_burnedCardsTexture;

	// Token: 0x04001862 RID: 6242
	public Material m_fullTileMaterial;

	// Token: 0x04001863 RID: 6243
	public Material m_halfTileMaterial;

	// Token: 0x04001864 RID: 6244
	public bool m_dead;

	// Token: 0x04001865 RID: 6245
	public bool m_burned;

	// Token: 0x04001866 RID: 6246
	public bool m_isPoisonous;

	// Token: 0x04001867 RID: 6247
	public int m_splatAmount;
}
