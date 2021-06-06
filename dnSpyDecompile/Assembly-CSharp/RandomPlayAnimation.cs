using System;
using UnityEngine;

// Token: 0x02000A69 RID: 2665
public class RandomPlayAnimation : MonoBehaviour
{
	// Token: 0x06008F0B RID: 36619 RVA: 0x002E3B45 File Offset: 0x002E1D45
	private void Start()
	{
		this.m_animation = base.gameObject.GetComponent<Animation>();
	}

	// Token: 0x06008F0C RID: 36620 RVA: 0x002E3B58 File Offset: 0x002E1D58
	private void Update()
	{
		if (this.m_animation == null)
		{
			base.enabled = false;
		}
		if (this.m_waitTime < 0f)
		{
			if (this.m_MinWaitTime < 0f)
			{
				this.m_MinWaitTime = 0f;
			}
			if (this.m_MaxWaitTime < 0f)
			{
				this.m_MaxWaitTime = 0f;
			}
			if (this.m_MaxWaitTime < this.m_MinWaitTime)
			{
				this.m_MaxWaitTime = this.m_MinWaitTime;
			}
			this.m_waitTime = UnityEngine.Random.Range(this.m_MinWaitTime, this.m_MaxWaitTime);
			this.m_startTime = Time.time;
		}
		if (Time.time - this.m_startTime > this.m_waitTime)
		{
			this.m_waitTime = -1f;
			this.m_animation.Play();
		}
	}

	// Token: 0x04007789 RID: 30601
	public float m_MinWaitTime;

	// Token: 0x0400778A RID: 30602
	public float m_MaxWaitTime = 10f;

	// Token: 0x0400778B RID: 30603
	private float m_waitTime = -1f;

	// Token: 0x0400778C RID: 30604
	private float m_startTime;

	// Token: 0x0400778D RID: 30605
	private Animation m_animation;
}
