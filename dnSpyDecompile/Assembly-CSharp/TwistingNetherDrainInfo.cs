using System;

// Token: 0x02000832 RID: 2098
[Serializable]
public class TwistingNetherDrainInfo
{
	// Token: 0x04005A46 RID: 23110
	public float m_DelayMin;

	// Token: 0x04005A47 RID: 23111
	public float m_DelayMax;

	// Token: 0x04005A48 RID: 23112
	public float m_DurationMin = 1.5f;

	// Token: 0x04005A49 RID: 23113
	public float m_DurationMax = 2f;

	// Token: 0x04005A4A RID: 23114
	public iTween.EaseType m_EaseType = iTween.EaseType.easeInOutCubic;
}
