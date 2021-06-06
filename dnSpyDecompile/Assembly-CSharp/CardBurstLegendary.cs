using System;
using System.Collections;
using UnityEngine;

// Token: 0x020007D5 RID: 2005
public class CardBurstLegendary : Spell
{
	// Token: 0x06006E1F RID: 28191 RVA: 0x00237FD4 File Offset: 0x002361D4
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
		if (this.m_Shockwave)
		{
			this.m_Shockwave.Play();
		}
		if (this.m_Bang)
		{
			this.m_Bang.Play();
		}
		this.OnSpellFinished();
	}

	// Token: 0x06006E20 RID: 28192 RVA: 0x0023807F File Offset: 0x0023627F
	protected override void OnDeath(SpellStateType prevStateType)
	{
		if (this.m_EdgeGlow)
		{
			this.m_EdgeGlow.GetComponent<Animation>().Play(this.m_EdgeGlowDeathAnimation, PlayMode.StopAll);
		}
		base.StartCoroutine(this.DeathState());
	}

	// Token: 0x06006E21 RID: 28193 RVA: 0x002380B3 File Offset: 0x002362B3
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

	// Token: 0x0400584E RID: 22606
	public GameObject m_RenderPlane;

	// Token: 0x0400584F RID: 22607
	public GameObject m_RaysMask;

	// Token: 0x04005850 RID: 22608
	public GameObject m_EdgeGlow;

	// Token: 0x04005851 RID: 22609
	public string m_EdgeGlowBirthAnimation = "StandardEdgeGlowFade_Forge";

	// Token: 0x04005852 RID: 22610
	public ParticleSystem m_Shockwave;

	// Token: 0x04005853 RID: 22611
	public ParticleSystem m_Bang;

	// Token: 0x04005854 RID: 22612
	public string m_EdgeGlowDeathAnimation = "StandardEdgeGlowFadeOut_Forge";
}
