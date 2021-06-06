using System;
using UnityEngine;

// Token: 0x020000C2 RID: 194
[Serializable]
public class BoxDrawerStateInfo
{
	// Token: 0x04000854 RID: 2132
	public GameObject m_ClosedBone;

	// Token: 0x04000855 RID: 2133
	public float m_ClosedDelaySec;

	// Token: 0x04000856 RID: 2134
	public float m_ClosedMoveSec = 1f;

	// Token: 0x04000857 RID: 2135
	public iTween.EaseType m_ClosedMoveEaseType = iTween.EaseType.linear;

	// Token: 0x04000858 RID: 2136
	public GameObject m_ClosedBoxOpenedBone;

	// Token: 0x04000859 RID: 2137
	public float m_ClosedBoxOpenedDelaySec;

	// Token: 0x0400085A RID: 2138
	public float m_ClosedBoxOpenedMoveSec = 1f;

	// Token: 0x0400085B RID: 2139
	public iTween.EaseType m_ClosedBoxOpenedMoveEaseType = iTween.EaseType.linear;

	// Token: 0x0400085C RID: 2140
	public GameObject m_OpenedBone;

	// Token: 0x0400085D RID: 2141
	public float m_OpenedDelaySec;

	// Token: 0x0400085E RID: 2142
	public float m_OpenedMoveSec = 1f;

	// Token: 0x0400085F RID: 2143
	public iTween.EaseType m_OpenedMoveEaseType = iTween.EaseType.easeOutBounce;
}
