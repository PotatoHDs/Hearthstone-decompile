using System;
using UnityEngine;

// Token: 0x020007F3 RID: 2035
public class GlobalZeroPosition : MonoBehaviour
{
	// Token: 0x06006ED4 RID: 28372 RVA: 0x0023B93E File Offset: 0x00239B3E
	private void LateUpdate()
	{
		base.transform.position = Vector3.zero;
		base.transform.rotation = Quaternion.identity;
		base.transform.localScale = Vector3.one;
	}
}
