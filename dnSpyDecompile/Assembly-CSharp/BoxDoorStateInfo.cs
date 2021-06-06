using System;
using UnityEngine;

// Token: 0x020000C0 RID: 192
[Serializable]
public class BoxDoorStateInfo
{
	// Token: 0x04000846 RID: 2118
	public Vector3 m_OpenedRotation = new Vector3(0f, 0f, 180f);

	// Token: 0x04000847 RID: 2119
	public float m_OpenedDelaySec;

	// Token: 0x04000848 RID: 2120
	public float m_OpenedRotateSec = 0.35f;

	// Token: 0x04000849 RID: 2121
	public iTween.EaseType m_OpenedRotateEaseType;

	// Token: 0x0400084A RID: 2122
	public Vector3 m_ClosedRotation = Vector3.zero;

	// Token: 0x0400084B RID: 2123
	public float m_ClosedDelaySec;

	// Token: 0x0400084C RID: 2124
	public float m_ClosedRotateSec = 0.35f;

	// Token: 0x0400084D RID: 2125
	public iTween.EaseType m_ClosedRotateEaseType;
}
