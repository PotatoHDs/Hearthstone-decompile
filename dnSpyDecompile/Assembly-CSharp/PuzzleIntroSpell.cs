using System;
using UnityEngine;

// Token: 0x020006DB RID: 1755
public class PuzzleIntroSpell : Spell
{
	// Token: 0x0600621D RID: 25117 RVA: 0x0020065C File Offset: 0x001FE85C
	public Transform GetConfirmButton()
	{
		return this.m_ConfirmButton;
	}

	// Token: 0x040051A0 RID: 20896
	[SerializeField]
	private Transform m_ConfirmButton;
}
