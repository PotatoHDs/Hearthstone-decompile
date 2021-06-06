using System;
using UnityEngine;

// Token: 0x02000A81 RID: 2689
public class ScreenEffect : MonoBehaviour
{
	// Token: 0x06009028 RID: 36904 RVA: 0x002ECE97 File Offset: 0x002EB097
	private void Awake()
	{
		this.m_ScreenEffectsMgr = ScreenEffectsMgr.Get();
	}

	// Token: 0x06009029 RID: 36905 RVA: 0x002ECEA4 File Offset: 0x002EB0A4
	private void OnEnable()
	{
		if (this.m_ScreenEffectsMgr == null)
		{
			this.m_ScreenEffectsMgr = ScreenEffectsMgr.Get();
		}
		ScreenEffectsMgr.RegisterScreenEffect(this);
	}

	// Token: 0x0600902A RID: 36906 RVA: 0x002ECEBF File Offset: 0x002EB0BF
	private void OnDisable()
	{
		if (this.m_ScreenEffectsMgr == null)
		{
			this.m_ScreenEffectsMgr = ScreenEffectsMgr.Get();
		}
		if (this.m_ScreenEffectsMgr != null)
		{
			ScreenEffectsMgr.UnRegisterScreenEffect(this);
		}
	}

	// Token: 0x04007917 RID: 30999
	private ScreenEffectsMgr m_ScreenEffectsMgr;
}
