using System;
using UnityEngine;

// Token: 0x02000080 RID: 128
[Serializable]
public class ChatMgrBubbleInfo
{
	// Token: 0x040004F5 RID: 1269
	public Transform m_Parent;

	// Token: 0x040004F6 RID: 1270
	public float m_ScaleInSec = 1f;

	// Token: 0x040004F7 RID: 1271
	public iTween.EaseType m_ScaleInEaseType = iTween.EaseType.easeOutElastic;

	// Token: 0x040004F8 RID: 1272
	public float m_HoldSec = 7f;

	// Token: 0x040004F9 RID: 1273
	public float m_FadeOutSec = 2f;

	// Token: 0x040004FA RID: 1274
	public iTween.EaseType m_FadeOutEaseType = iTween.EaseType.linear;

	// Token: 0x040004FB RID: 1275
	public float m_MoveOverSec = 1f;

	// Token: 0x040004FC RID: 1276
	public iTween.EaseType m_MoveOverEaseType = iTween.EaseType.easeOutExpo;
}
