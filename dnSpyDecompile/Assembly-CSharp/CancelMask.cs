using System;
using UnityEngine;

// Token: 0x02000B44 RID: 2884
public class CancelMask : MonoBehaviour
{
	// Token: 0x060098DF RID: 39135 RVA: 0x00317874 File Offset: 0x00315A74
	public void Trigger()
	{
		this.m_root.gameObject.SetActive(false);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04007FD8 RID: 32728
	public GameObject m_root;
}
