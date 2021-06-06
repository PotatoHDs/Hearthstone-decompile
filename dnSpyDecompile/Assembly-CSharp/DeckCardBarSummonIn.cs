using System;
using System.Collections;
using UnityEngine;

// Token: 0x020007E4 RID: 2020
public class DeckCardBarSummonIn : SpellImpl
{
	// Token: 0x06006E77 RID: 28279 RVA: 0x0023A0E4 File Offset: 0x002382E4
	private void OnDisable()
	{
		if (this.m_echoQuad != null)
		{
			this.m_echoQuad.GetComponent<Renderer>().GetMaterial().color = Color.clear;
		}
		if (this.m_fxEvaporate != null)
		{
			this.m_fxEvaporate.GetComponent<ParticleSystem>().Clear();
		}
	}

	// Token: 0x06006E78 RID: 28280 RVA: 0x0023A137 File Offset: 0x00238337
	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.StartCoroutine(this.BirthState());
	}

	// Token: 0x06006E79 RID: 28281 RVA: 0x0023A146 File Offset: 0x00238346
	private IEnumerator BirthState()
	{
		base.InitActorVariables();
		GameObject actorObject = base.GetActorObject("Frame");
		base.SetVisibilityRecursive(actorObject, false);
		base.SetVisibility(this.m_echoQuad, true);
		base.SetVisibilityRecursive(actorObject, true);
		base.PlayParticles(this.m_fxEvaporate, false);
		base.SetAnimationSpeed(this.m_echoQuad, "Secret_AbilityEchoFade", 0.5f);
		base.PlayAnimation(this.m_echoQuad, "Secret_AbilityEchoFade", PlayMode.StopAll, 0f);
		yield return new WaitForSeconds(1f);
		this.OnSpellFinished();
		base.SetVisibility(this.m_echoQuad, false);
		yield break;
	}

	// Token: 0x040058A3 RID: 22691
	public GameObject m_echoQuad;

	// Token: 0x040058A4 RID: 22692
	public GameObject m_fxEvaporate;
}
