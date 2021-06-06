using System;
using System.Collections;
using UnityEngine;

// Token: 0x020007D4 RID: 2004
public class CardBurstEpic : Spell
{
	// Token: 0x06006E1B RID: 28187 RVA: 0x00237EB0 File Offset: 0x002360B0
	protected override void OnBirth(SpellStateType prevStateType)
	{
		if (this.m_RenderPlane)
		{
			this.m_RenderPlane.SetActive(true);
		}
		if (this.m_RaysMask)
		{
			this.m_RaysMask.SetActive(true);
		}
		if (this.m_EdgeGlow)
		{
			this.m_EdgeGlow.GetComponent<Renderer>().enabled = true;
			this.m_EdgeGlow.GetComponent<Animation>().Play(this.m_EdgeGlowBirthAnimation, PlayMode.StopAll);
		}
		if (this.m_BurstFlare)
		{
			this.m_BurstFlare.Play();
		}
		if (this.m_Bang)
		{
			this.m_Bang.Play();
		}
		if (this.m_BangLinger)
		{
			this.m_BangLinger.Play();
		}
		this.OnSpellFinished();
	}

	// Token: 0x06006E1C RID: 28188 RVA: 0x00237F73 File Offset: 0x00236173
	protected override void OnDeath(SpellStateType prevStateType)
	{
		if (this.m_EdgeGlow)
		{
			this.m_EdgeGlow.GetComponent<Animation>().Play(this.m_EdgeGlowDeathAnimation, PlayMode.StopAll);
		}
		base.StartCoroutine(this.DeathState());
	}

	// Token: 0x06006E1D RID: 28189 RVA: 0x00237FA7 File Offset: 0x002361A7
	private IEnumerator DeathState()
	{
		yield return new WaitForSeconds(0.2f);
		if (this.m_EdgeGlow)
		{
			this.m_EdgeGlow.GetComponent<Renderer>().enabled = false;
		}
		this.OnSpellFinished();
		yield break;
	}

	// Token: 0x04005846 RID: 22598
	public GameObject m_RenderPlane;

	// Token: 0x04005847 RID: 22599
	public GameObject m_RaysMask;

	// Token: 0x04005848 RID: 22600
	public GameObject m_EdgeGlow;

	// Token: 0x04005849 RID: 22601
	public string m_EdgeGlowBirthAnimation = "StandardEdgeGlowFade";

	// Token: 0x0400584A RID: 22602
	public ParticleSystem m_BurstFlare;

	// Token: 0x0400584B RID: 22603
	public ParticleSystem m_Bang;

	// Token: 0x0400584C RID: 22604
	public ParticleSystem m_BangLinger;

	// Token: 0x0400584D RID: 22605
	public string m_EdgeGlowDeathAnimation = "StandardEdgeGlowFadeOut";
}
