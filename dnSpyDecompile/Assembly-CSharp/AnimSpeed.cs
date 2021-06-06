using System;
using UnityEngine;

// Token: 0x02000A0E RID: 2574
public class AnimSpeed : MonoBehaviour
{
	// Token: 0x06008B33 RID: 35635 RVA: 0x002C7D58 File Offset: 0x002C5F58
	private void Awake()
	{
		foreach (object obj in base.GetComponent<Animation>())
		{
			((AnimationState)obj).speed = this.animspeed;
		}
	}

	// Token: 0x040073A8 RID: 29608
	public float animspeed = 1f;
}
