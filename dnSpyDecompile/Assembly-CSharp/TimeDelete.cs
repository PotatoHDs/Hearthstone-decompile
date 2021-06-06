using System;
using UnityEngine;

// Token: 0x020009F7 RID: 2551
public class TimeDelete : MonoBehaviour
{
	// Token: 0x06008A1D RID: 35357 RVA: 0x002C41A1 File Offset: 0x002C23A1
	private void Start()
	{
		this.m_StartTime = Time.time;
	}

	// Token: 0x06008A1E RID: 35358 RVA: 0x002C41AE File Offset: 0x002C23AE
	private void Update()
	{
		if (Time.time > this.m_StartTime + this.m_SecondsToDelete)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0400735D RID: 29533
	public float m_SecondsToDelete = 10f;

	// Token: 0x0400735E RID: 29534
	private float m_StartTime;
}
