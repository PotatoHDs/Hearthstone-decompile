using System;
using UnityEngine;

// Token: 0x02000326 RID: 806
[Serializable]
public class ManaCrystalAssetPaths
{
	// Token: 0x04001924 RID: 6436
	public ManaCrystalType m_Type;

	// Token: 0x04001925 RID: 6437
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_ResourcePath;

	// Token: 0x04001926 RID: 6438
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_SoundOnAddPath;

	// Token: 0x04001927 RID: 6439
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_SoundOnSpendPath;

	// Token: 0x04001928 RID: 6440
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_SoundOnRefreshPath;

	// Token: 0x04001929 RID: 6441
	[CustomEditField(T = EditType.MATERIAL)]
	public Material m_tempManaCrystalMaterial;

	// Token: 0x0400192A RID: 6442
	[CustomEditField(T = EditType.MATERIAL)]
	public Material m_tempManaCrystalProposedQuadMaterial;

	// Token: 0x0400192B RID: 6443
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_phoneLargeResource;
}
