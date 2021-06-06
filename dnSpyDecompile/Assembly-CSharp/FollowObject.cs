using System;
using UnityEngine;

// Token: 0x02000A2D RID: 2605
public class FollowObject : MonoBehaviour
{
	// Token: 0x06008BF4 RID: 35828 RVA: 0x002CCE1A File Offset: 0x002CB01A
	private void LateUpdate()
	{
		base.transform.position = this.targetObj.position;
	}

	// Token: 0x040074E3 RID: 29923
	public Transform targetObj;
}
