using System;
using UnityEngine;

// Token: 0x02000831 RID: 2097
[Serializable]
public class TwistingNetherFloatInfo
{
	// Token: 0x04005A40 RID: 23104
	public Vector3 m_OffsetMin = new Vector3(-1.5f, -1.5f, -1.5f);

	// Token: 0x04005A41 RID: 23105
	public Vector3 m_OffsetMax = new Vector3(1.5f, 1.5f, 1.5f);

	// Token: 0x04005A42 RID: 23106
	public Vector2 m_RotationXZMin = new Vector2(-10f, -10f);

	// Token: 0x04005A43 RID: 23107
	public Vector2 m_RotationXZMax = new Vector2(10f, 10f);

	// Token: 0x04005A44 RID: 23108
	public float m_DurationMin = 1.5f;

	// Token: 0x04005A45 RID: 23109
	public float m_DurationMax = 2f;
}
