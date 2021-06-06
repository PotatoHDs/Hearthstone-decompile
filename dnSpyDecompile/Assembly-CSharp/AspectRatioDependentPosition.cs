using System;
using UnityEngine;

// Token: 0x020005E2 RID: 1506
public class AspectRatioDependentPosition : MonoBehaviour
{
	// Token: 0x06005288 RID: 21128 RVA: 0x001B15B8 File Offset: 0x001AF7B8
	private void Awake()
	{
		base.transform.localPosition = TransformUtil.GetAspectRatioDependentPosition(this.m_minLocalPosition, this.m_wideLocalPosition, this.m_extraWideLocalPosition);
	}

	// Token: 0x040049A4 RID: 18852
	public Vector3 m_minLocalPosition;

	// Token: 0x040049A5 RID: 18853
	public Vector3 m_wideLocalPosition;

	// Token: 0x040049A6 RID: 18854
	public Vector3 m_extraWideLocalPosition;
}
