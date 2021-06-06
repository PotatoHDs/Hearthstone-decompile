using System;
using UnityEngine;

// Token: 0x02000A12 RID: 2578
public class BounceScale : MonoBehaviour
{
	// Token: 0x06008B44 RID: 35652 RVA: 0x002C8200 File Offset: 0x002C6400
	public void BounceyScale()
	{
		Vector3 localScale = base.transform.localScale;
		base.transform.localScale = Vector3.zero;
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			localScale,
			"time",
			this.m_Time,
			"easetype",
			iTween.EaseType.easeOutElastic
		}));
	}

	// Token: 0x040073C1 RID: 29633
	public float m_Time;
}
