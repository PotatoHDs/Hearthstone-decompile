using System;
using UnityEngine;

// Token: 0x02000AC6 RID: 2758
[ExecuteAlways]
public class DelayedDestroy : MonoBehaviour
{
	// Token: 0x06009331 RID: 37681 RVA: 0x002FBB48 File Offset: 0x002F9D48
	private void Awake()
	{
		base.hideFlags = HideFlags.HideAndDontSave;
		base.gameObject.hideFlags = HideFlags.HideAndDontSave;
	}

	// Token: 0x06009332 RID: 37682 RVA: 0x002FBB5F File Offset: 0x002F9D5F
	private void Start()
	{
		UnityEngine.Object.DestroyImmediate(base.transform.gameObject);
	}
}
