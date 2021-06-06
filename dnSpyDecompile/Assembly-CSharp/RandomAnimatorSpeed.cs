using System;
using UnityEngine;

// Token: 0x02000A66 RID: 2662
public class RandomAnimatorSpeed : MonoBehaviour
{
	// Token: 0x06008EFB RID: 36603 RVA: 0x002E3744 File Offset: 0x002E1944
	private void Start()
	{
		Animator component = base.GetComponent<Animator>();
		if (component == null)
		{
			return;
		}
		component.speed = UnityEngine.Random.Range(this.minSpeed, this.maxSpeed);
	}

	// Token: 0x04007772 RID: 30578
	public float minSpeed = 0.5f;

	// Token: 0x04007773 RID: 30579
	public float maxSpeed = 1.5f;
}
