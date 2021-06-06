using UnityEngine;

public class CardBurstCommon : Spell
{
	public ParticleSystem m_BurstMotes;

	public GameObject m_EdgeGlow;

	protected override void OnBirth(SpellStateType prevStateType)
	{
		if ((bool)m_BurstMotes)
		{
			m_BurstMotes.Play();
		}
		if ((bool)m_EdgeGlow)
		{
			m_EdgeGlow.GetComponent<Renderer>().enabled = true;
		}
		OnSpellFinished();
	}
}
