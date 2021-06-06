using System;
using UnityEngine;

// Token: 0x02000ADB RID: 2779
public abstract class PegUICustomBehavior : MonoBehaviour
{
	// Token: 0x06009408 RID: 37896 RVA: 0x00300F83 File Offset: 0x002FF183
	protected virtual void Awake()
	{
		PegUI.Get().RegisterCustomBehavior(this);
	}

	// Token: 0x06009409 RID: 37897 RVA: 0x00300F90 File Offset: 0x002FF190
	protected virtual void OnDestroy()
	{
		if (PegUI.Get() != null)
		{
			PegUI.Get().UnregisterCustomBehavior(this);
		}
	}

	// Token: 0x0600940A RID: 37898
	public abstract bool UpdateUI();
}
