using System;
using UnityEngine;

// Token: 0x020000BE RID: 190
[Serializable]
public class BoxDiskStateInfo
{
	// Token: 0x0400083B RID: 2107
	public Vector3 m_MainMenuRotation = new Vector3(0f, 0f, 180f);

	// Token: 0x0400083C RID: 2108
	public float m_MainMenuDelaySec = 0.1f;

	// Token: 0x0400083D RID: 2109
	public float m_MainMenuRotateSec = 0.17f;

	// Token: 0x0400083E RID: 2110
	public iTween.EaseType m_MainMenuRotateEaseType;

	// Token: 0x0400083F RID: 2111
	public Vector3 m_LoadingRotation = new Vector3(0f, 0f, 0f);

	// Token: 0x04000840 RID: 2112
	public float m_LoadingDelaySec = 0.1f;

	// Token: 0x04000841 RID: 2113
	public float m_LoadingRotateSec = 0.17f;

	// Token: 0x04000842 RID: 2114
	public iTween.EaseType m_LoadingRotateEaseType;
}
