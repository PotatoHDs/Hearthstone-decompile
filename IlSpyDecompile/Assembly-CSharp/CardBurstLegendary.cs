using System.Collections;
using UnityEngine;

public class CardBurstLegendary : Spell
{
	public GameObject m_RenderPlane;

	public GameObject m_RaysMask;

	public GameObject m_EdgeGlow;

	public string m_EdgeGlowBirthAnimation = "StandardEdgeGlowFade_Forge";

	public ParticleSystem m_Shockwave;

	public ParticleSystem m_Bang;

	public string m_EdgeGlowDeathAnimation = "StandardEdgeGlowFadeOut_Forge";

	protected override void OnBirth(SpellStateType prevStateType)
	{
		if ((bool)m_RenderPlane)
		{
			m_RenderPlane.SetActive(value: true);
		}
		if ((bool)m_RaysMask)
		{
			m_RaysMask.SetActive(value: true);
		}
		if ((bool)m_EdgeGlow)
		{
			m_EdgeGlow.GetComponent<Renderer>().enabled = true;
			m_EdgeGlow.GetComponent<Animation>().Play(m_EdgeGlowBirthAnimation, PlayMode.StopAll);
		}
		if ((bool)m_Shockwave)
		{
			m_Shockwave.Play();
		}
		if ((bool)m_Bang)
		{
			m_Bang.Play();
		}
		OnSpellFinished();
	}

	protected override void OnDeath(SpellStateType prevStateType)
	{
		if ((bool)m_EdgeGlow)
		{
			m_EdgeGlow.GetComponent<Animation>().Play(m_EdgeGlowDeathAnimation, PlayMode.StopAll);
		}
		StartCoroutine(DeathState());
	}

	private IEnumerator DeathState()
	{
		yield return new WaitForSeconds(0.2f);
		if ((bool)m_EdgeGlow)
		{
			m_EdgeGlow.GetComponent<Renderer>().enabled = false;
		}
		OnSpellFinished();
	}
}
