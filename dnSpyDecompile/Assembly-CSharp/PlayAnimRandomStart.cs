using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A5F RID: 2655
public class PlayAnimRandomStart : MonoBehaviour
{
	// Token: 0x06008EBE RID: 36542 RVA: 0x002E1694 File Offset: 0x002DF894
	private void Start()
	{
		base.StartCoroutine(this.PlayRandomBubbles());
	}

	// Token: 0x06008EBF RID: 36543 RVA: 0x002E16A3 File Offset: 0x002DF8A3
	private IEnumerator PlayRandomBubbles()
	{
		for (;;)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(this.minWait, this.maxWait));
			int index = UnityEngine.Random.Range(0, this.m_Bubbles.Count);
			GameObject gameObject = this.m_Bubbles[index];
			if (!(gameObject == null))
			{
				gameObject.GetComponent<Animation>().Play();
				gameObject.GetComponent<Animation>()[this.animName].speed = UnityEngine.Random.Range(this.MinSpeed, this.MaxSpeed);
			}
		}
		yield break;
	}

	// Token: 0x04007715 RID: 30485
	public List<GameObject> m_Bubbles;

	// Token: 0x04007716 RID: 30486
	public float minWait;

	// Token: 0x04007717 RID: 30487
	public float maxWait = 10f;

	// Token: 0x04007718 RID: 30488
	public float MinSpeed = 0.2f;

	// Token: 0x04007719 RID: 30489
	public float MaxSpeed = 1.1f;

	// Token: 0x0400771A RID: 30490
	public string animName = "Bubble1";
}
