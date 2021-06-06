using System;

// Token: 0x020000CB RID: 203
[Serializable]
public class BoxLogoStateInfo
{
	// Token: 0x04000897 RID: 2199
	public float m_ShownAlpha = 1f;

	// Token: 0x04000898 RID: 2200
	public float m_ShownDelaySec;

	// Token: 0x04000899 RID: 2201
	public float m_ShownFadeSec = 0.3f;

	// Token: 0x0400089A RID: 2202
	public iTween.EaseType m_ShownFadeEaseType = iTween.EaseType.linear;

	// Token: 0x0400089B RID: 2203
	public float m_HiddenAlpha;

	// Token: 0x0400089C RID: 2204
	public float m_HiddenDelaySec;

	// Token: 0x0400089D RID: 2205
	public float m_HiddenFadeSec = 0.3f;

	// Token: 0x0400089E RID: 2206
	public iTween.EaseType m_HiddenFadeEaseType = iTween.EaseType.linear;
}
