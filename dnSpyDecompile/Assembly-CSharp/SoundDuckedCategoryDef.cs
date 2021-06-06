using System;
using Assets;

// Token: 0x0200095A RID: 2394
[Serializable]
public class SoundDuckedCategoryDef
{
	// Token: 0x04006EA3 RID: 28323
	public Global.SoundCategory m_Category;

	// Token: 0x04006EA4 RID: 28324
	public float m_Volume = 0.2f;

	// Token: 0x04006EA5 RID: 28325
	public float m_BeginSec = 0.7f;

	// Token: 0x04006EA6 RID: 28326
	public iTween.EaseType m_BeginEaseType = iTween.EaseType.linear;

	// Token: 0x04006EA7 RID: 28327
	public float m_RestoreSec = 0.7f;

	// Token: 0x04006EA8 RID: 28328
	public iTween.EaseType m_RestoreEaseType = iTween.EaseType.linear;
}
