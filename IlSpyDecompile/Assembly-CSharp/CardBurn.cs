using System.Collections;
using UnityEngine;

public class CardBurn : Spell
{
	public GameObject m_BurnCardQuad;

	public string m_BurnCardAnim = "CardBurnUpFire";

	public ParticleSystem m_EdgeEmbers;

	protected override void OnBirth(SpellStateType prevStateType)
	{
		StartCoroutine(BirthAction());
	}

	private IEnumerator BirthAction()
	{
		if ((bool)m_BurnCardQuad)
		{
			m_BurnCardQuad.GetComponent<Renderer>().enabled = true;
			m_BurnCardQuad.GetComponent<Animation>().Play(m_BurnCardAnim, PlayMode.StopAll);
		}
		if ((bool)m_EdgeEmbers)
		{
			m_EdgeEmbers.Play();
		}
		yield return new WaitForSeconds(0.15f);
		Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.gameObject);
		if (!(actor == null))
		{
			actor.Hide();
			OnSpellFinished();
		}
	}
}
