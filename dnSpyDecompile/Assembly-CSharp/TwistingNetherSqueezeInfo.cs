using System;

// Token: 0x02000833 RID: 2099
[Serializable]
public class TwistingNetherSqueezeInfo
{
	// Token: 0x04005A4B RID: 23115
	public float m_DelayMin;

	// Token: 0x04005A4C RID: 23116
	public float m_DelayMax;

	// Token: 0x04005A4D RID: 23117
	public float m_DurationMin = 1f;

	// Token: 0x04005A4E RID: 23118
	public float m_DurationMax = 1.5f;

	// Token: 0x04005A4F RID: 23119
	public iTween.EaseType m_EaseType = iTween.EaseType.easeInCubic;
}
