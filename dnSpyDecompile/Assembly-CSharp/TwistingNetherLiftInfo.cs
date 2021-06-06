using System;
using UnityEngine;

// Token: 0x02000830 RID: 2096
[Serializable]
public class TwistingNetherLiftInfo
{
	// Token: 0x04005A33 RID: 23091
	public Vector3 m_OffsetMin = new Vector3(-3f, 3.5f, -3f);

	// Token: 0x04005A34 RID: 23092
	public Vector3 m_OffsetMax = new Vector3(3f, 5.5f, 3f);

	// Token: 0x04005A35 RID: 23093
	public float m_DelayMin;

	// Token: 0x04005A36 RID: 23094
	public float m_DelayMax = 0.3f;

	// Token: 0x04005A37 RID: 23095
	public float m_DurationMin = 0.1f;

	// Token: 0x04005A38 RID: 23096
	public float m_DurationMax = 0.3f;

	// Token: 0x04005A39 RID: 23097
	public float m_RotDelayMin;

	// Token: 0x04005A3A RID: 23098
	public float m_RotDelayMax = 0.3f;

	// Token: 0x04005A3B RID: 23099
	public float m_RotDurationMin = 1f;

	// Token: 0x04005A3C RID: 23100
	public float m_RotDurationMax = 3f;

	// Token: 0x04005A3D RID: 23101
	public float m_RotationMin;

	// Token: 0x04005A3E RID: 23102
	public float m_RotationMax = 90f;

	// Token: 0x04005A3F RID: 23103
	public iTween.EaseType m_EaseType = iTween.EaseType.easeOutExpo;
}
