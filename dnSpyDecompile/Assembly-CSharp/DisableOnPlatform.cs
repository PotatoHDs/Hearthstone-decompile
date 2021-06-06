using System;
using UnityEngine;

// Token: 0x020005E5 RID: 1509
public class DisableOnPlatform : MonoBehaviour
{
	// Token: 0x060052A9 RID: 21161 RVA: 0x001B1A4B File Offset: 0x001AFC4B
	private void OnEnable()
	{
		this.UpdateState();
	}

	// Token: 0x060052AA RID: 21162 RVA: 0x001B1A4B File Offset: 0x001AFC4B
	private void Update()
	{
		this.UpdateState();
	}

	// Token: 0x060052AB RID: 21163 RVA: 0x001B1A53 File Offset: 0x001AFC53
	private void UpdateState()
	{
		if (Application.IsPlaying(this) && PlatformSettings.Screen == this.m_screenCategory)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x040049B3 RID: 18867
	public ScreenCategory m_screenCategory;
}
