using System;
using System.Collections;
using UnityEngine;

// Token: 0x020007E3 RID: 2019
public class DeckCardBarFlareUp : SpellImpl
{
	// Token: 0x06006E74 RID: 28276 RVA: 0x0023A0B0 File Offset: 0x002382B0
	protected override void OnBirth(SpellStateType prevStateType)
	{
		if (base.gameObject.activeSelf)
		{
			base.StartCoroutine(this.BirthState());
		}
	}

	// Token: 0x06006E75 RID: 28277 RVA: 0x0023A0CC File Offset: 0x002382CC
	private IEnumerator BirthState()
	{
		base.SetVisibility(this.m_fuseQuad, true);
		base.PlayParticles(this.m_fxSparks, false);
		base.PlayAnimation(this.m_fuseQuad, "DeckCardBar_FuseInOut", PlayMode.StopAll, 0f);
		this.OnSpellFinished();
		yield return new WaitForSeconds(2f);
		base.SetVisibility(this.m_fuseQuad, false);
		yield break;
	}

	// Token: 0x040058A1 RID: 22689
	public GameObject m_fuseQuad;

	// Token: 0x040058A2 RID: 22690
	public GameObject m_fxSparks;
}
