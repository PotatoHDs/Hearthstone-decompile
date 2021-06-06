using System;
using UnityEngine;

// Token: 0x020008D0 RID: 2256
public class GUISkinContainer : MonoBehaviour
{
	// Token: 0x06007CD1 RID: 31953 RVA: 0x0028982E File Offset: 0x00287A2E
	public GUISkin GetGUISkin()
	{
		return this.m_guiSkin;
	}

	// Token: 0x04006574 RID: 25972
	public GUISkin m_guiSkin;
}
