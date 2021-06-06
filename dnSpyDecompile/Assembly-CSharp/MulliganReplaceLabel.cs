using System;
using UnityEngine;

// Token: 0x0200032D RID: 813
public class MulliganReplaceLabel : MonoBehaviour
{
	// Token: 0x06002E4B RID: 11851 RVA: 0x000EC1D0 File Offset: 0x000EA3D0
	private void Awake()
	{
		this.m_labelText.Text = GameStrings.Get("GAMEPLAY_MULLIGAN_REPLACE");
	}

	// Token: 0x040019BC RID: 6588
	public UberText m_labelText;
}
