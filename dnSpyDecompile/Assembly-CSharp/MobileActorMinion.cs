using System;
using UnityEngine;

// Token: 0x0200032A RID: 810
public class MobileActorMinion : MonoBehaviour
{
	// Token: 0x06002DE1 RID: 11745 RVA: 0x000E9378 File Offset: 0x000E7578
	private void Awake()
	{
		if (PlatformSettings.IsMobile() && UniversalInputManager.UsePhoneUI)
		{
			Vector3 localScale = base.gameObject.transform.localScale;
			localScale.x *= this.m_minionScaleFactor.x;
			localScale.y *= this.m_minionScaleFactor.y;
			localScale.z *= this.m_minionScaleFactor.z;
			base.gameObject.transform.localScale = localScale;
		}
	}

	// Token: 0x0400194F RID: 6479
	public Vector3 m_minionScaleFactor = Vector3.one;
}
