using System;
using UnityEngine;

// Token: 0x02000B3A RID: 2874
[ExecuteInEditMode]
public class HastyLookat : MonoBehaviour
{
	// Token: 0x0600987C RID: 39036 RVA: 0x00315DB0 File Offset: 0x00313FB0
	private void Update()
	{
		base.transform.LookAt(this.target);
	}

	// Token: 0x04007F7A RID: 32634
	public Transform target;
}
