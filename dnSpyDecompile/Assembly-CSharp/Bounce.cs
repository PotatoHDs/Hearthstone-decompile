using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000A11 RID: 2577
public class Bounce : MonoBehaviour
{
	// Token: 0x06008B3F RID: 35647 RVA: 0x002C8184 File Offset: 0x002C6384
	private void Start()
	{
		if (this.m_PlayOnStart)
		{
			this.StartAnimation();
		}
	}

	// Token: 0x06008B40 RID: 35648 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Update()
	{
	}

	// Token: 0x06008B41 RID: 35649 RVA: 0x002C8194 File Offset: 0x002C6394
	public void StartAnimation()
	{
		this.m_BounceAmountOverTime = this.m_BounceAmount;
		this.m_StartingPosition = base.transform.position;
		base.StartCoroutine("BounceAnimation");
	}

	// Token: 0x06008B42 RID: 35650 RVA: 0x002C81BF File Offset: 0x002C63BF
	private IEnumerator BounceAnimation()
	{
		yield return new WaitForSeconds(this.m_Delay);
		int num2;
		for (int c = 0; c < this.m_BounceCount; c = num2 + 1)
		{
			float time = 0f;
			for (float i = 0f; i < 1f; i += Time.deltaTime * this.m_BounceSpeed)
			{
				time += Time.deltaTime * this.m_BounceSpeed;
				Vector3 startingPosition = this.m_StartingPosition;
				float num = Mathf.Sin(time * 3.1415927f);
				if (num < 0f)
				{
					break;
				}
				base.transform.position = new Vector3(startingPosition.x, startingPosition.y + num * this.m_BounceAmountOverTime, startingPosition.z);
				yield return null;
			}
			this.m_BounceAmountOverTime *= this.m_Bounceness;
			yield return null;
			num2 = c;
		}
		base.transform.position = this.m_StartingPosition;
		yield break;
	}

	// Token: 0x040073B9 RID: 29625
	public float m_BounceSpeed = 3.5f;

	// Token: 0x040073BA RID: 29626
	public float m_BounceAmount = 3f;

	// Token: 0x040073BB RID: 29627
	public int m_BounceCount = 3;

	// Token: 0x040073BC RID: 29628
	public float m_Bounceness = 0.2f;

	// Token: 0x040073BD RID: 29629
	public float m_Delay;

	// Token: 0x040073BE RID: 29630
	public bool m_PlayOnStart;

	// Token: 0x040073BF RID: 29631
	private Vector3 m_StartingPosition;

	// Token: 0x040073C0 RID: 29632
	private float m_BounceAmountOverTime;
}
