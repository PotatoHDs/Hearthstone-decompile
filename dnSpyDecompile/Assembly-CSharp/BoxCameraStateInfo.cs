using System;
using UnityEngine;

// Token: 0x020000BB RID: 187
[Serializable]
public class BoxCameraStateInfo
{
	// Token: 0x04000818 RID: 2072
	public GameObject m_ClosedBone;

	// Token: 0x04000819 RID: 2073
	public GameObject m_ClosedMinAspectRatioBone;

	// Token: 0x0400081A RID: 2074
	public GameObject m_ClosedExtraWideAspectRatioBone;

	// Token: 0x0400081B RID: 2075
	public float m_ClosedDelaySec;

	// Token: 0x0400081C RID: 2076
	public float m_ClosedMoveSec = 0.7f;

	// Token: 0x0400081D RID: 2077
	public iTween.EaseType m_ClosedMoveEaseType = iTween.EaseType.easeOutCubic;

	// Token: 0x0400081E RID: 2078
	public GameObject m_ClosedWithDrawerBone;

	// Token: 0x0400081F RID: 2079
	public GameObject m_ClosedWithDrawerMinAspectRatioBone;

	// Token: 0x04000820 RID: 2080
	public GameObject m_ClosedWithDrawerExtraWideAspectRatioBone;

	// Token: 0x04000821 RID: 2081
	public float m_ClosedWithDrawerDelaySec;

	// Token: 0x04000822 RID: 2082
	public float m_ClosedWithDrawerMoveSec = 0.7f;

	// Token: 0x04000823 RID: 2083
	public iTween.EaseType m_ClosedWithDrawerMoveEaseType = iTween.EaseType.easeOutCubic;

	// Token: 0x04000824 RID: 2084
	public GameObject m_OpenedBone;

	// Token: 0x04000825 RID: 2085
	public GameObject m_OpenedMinAspectRatioBone;

	// Token: 0x04000826 RID: 2086
	public GameObject m_OpenedExtraWideAspectRatioBone;

	// Token: 0x04000827 RID: 2087
	public float m_OpenedDelaySec;

	// Token: 0x04000828 RID: 2088
	public float m_OpenedMoveSec = 0.7f;

	// Token: 0x04000829 RID: 2089
	public iTween.EaseType m_OpenedMoveEaseType = iTween.EaseType.easeOutCubic;
}
