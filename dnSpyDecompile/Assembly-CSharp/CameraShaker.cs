using System;
using UnityEngine;

// Token: 0x02000A16 RID: 2582
public class CameraShaker : MonoBehaviour
{
	// Token: 0x06008B5F RID: 35679 RVA: 0x002C9224 File Offset: 0x002C7424
	public void StartShake()
	{
		float? holdAtTime = null;
		if (this.m_Hold)
		{
			holdAtTime = new float?(this.m_HoldAtSec);
		}
		CameraShakeMgr.Shake(Camera.main, this.m_Amount, this.m_IntensityCurve, holdAtTime);
	}

	// Token: 0x040073DD RID: 29661
	public Vector3 m_Amount;

	// Token: 0x040073DE RID: 29662
	public AnimationCurve m_IntensityCurve;

	// Token: 0x040073DF RID: 29663
	public bool m_Hold;

	// Token: 0x040073E0 RID: 29664
	public float m_HoldAtSec;
}
