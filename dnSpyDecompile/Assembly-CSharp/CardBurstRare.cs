using System;
using System.Collections;
using UnityEngine;

// Token: 0x020007D6 RID: 2006
public class CardBurstRare : Spell
{
	// Token: 0x06006E23 RID: 28195 RVA: 0x002380E0 File Offset: 0x002362E0
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
		if (this.m_BurstMotes)
		{
			this.m_BurstMotes.Play();
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

	// Token: 0x06006E24 RID: 28196 RVA: 0x002381A3 File Offset: 0x002363A3
	protected override void OnDeath(SpellStateType prevStateType)
	{
		if (this.m_EdgeGlow)
		{
			this.m_EdgeGlow.GetComponent<Animation>().Play(this.m_EdgeGlowDeathAnimation, PlayMode.StopAll);
		}
		base.StartCoroutine(this.DeathState());
	}

	// Token: 0x06006E25 RID: 28197 RVA: 0x002381D7 File Offset: 0x002363D7
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

	// Token: 0x04005855 RID: 22613
	public GameObject m_RenderPlane;

	// Token: 0x04005856 RID: 22614
	public GameObject m_RaysMask;

	// Token: 0x04005857 RID: 22615
	public GameObject m_EdgeGlow;

	// Token: 0x04005858 RID: 22616
	public string m_EdgeGlowBirthAnimation = "StandardEdgeGlowFade_Forge";

	// Token: 0x04005859 RID: 22617
	public ParticleSystem m_Bang;

	// Token: 0x0400585A RID: 22618
	public ParticleSystem m_BangLinger;

	// Token: 0x0400585B RID: 22619
	public ParticleSystem m_BurstMotes;

	// Token: 0x0400585C RID: 22620
	public string m_EdgeGlowDeathAnimation = "StandardEdgeGlowFadeOut_Forge";
}
