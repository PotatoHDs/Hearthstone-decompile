using System.Collections;
using UnityEngine;

public class CardBurstEpic : Spell
{
	public GameObject m_RenderPlane;

	public GameObject m_RaysMask;

	public GameObject m_EdgeGlow;

	public string m_EdgeGlowBirthAnimation = "StandardEdgeGlowFade";

	public ParticleSystem m_BurstFlare;

	public ParticleSystem m_Bang;

	public ParticleSystem m_BangLinger;

	public string m_EdgeGlowDeathAnimation = "StandardEdgeGlowFadeOut";

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
		if ((bool)m_BurstFlare)
		{
			m_BurstFlare.Play();
		}
		if ((bool)m_Bang)
		{
			m_Bang.Play();
		}
		if ((bool)m_BangLinger)
		{
			m_BangLinger.Play();
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
