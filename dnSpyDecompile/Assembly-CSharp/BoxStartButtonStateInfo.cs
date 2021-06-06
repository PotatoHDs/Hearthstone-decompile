using System;

// Token: 0x020000D2 RID: 210
[Serializable]
public class BoxStartButtonStateInfo
{
	// Token: 0x040008B4 RID: 2228
	public float m_ShownAlpha = 1f;

	// Token: 0x040008B5 RID: 2229
	public float m_ShownDelaySec;

	// Token: 0x040008B6 RID: 2230
	public float m_ShownFadeSec = 0.3f;

	// Token: 0x040008B7 RID: 2231
	public iTween.EaseType m_ShownFadeEaseType = iTween.EaseType.linear;

	// Token: 0x040008B8 RID: 2232
	public float m_HiddenAlpha;

	// Token: 0x040008B9 RID: 2233
	public float m_HiddenDelaySec;

	// Token: 0x040008BA RID: 2234
	public float m_HiddenFadeSec = 0.3f;

	// Token: 0x040008BB RID: 2235
	public iTween.EaseType m_HiddenFadeEaseType = iTween.EaseType.linear;
}
