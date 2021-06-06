using System.Collections;
using UnityEngine;

public class SummonInForge : SpellImpl
{
	public GameObject m_burnIn;

	public GameObject m_blackBits;

	public GameObject m_smokePuff;

	public float m_burnInAnimationSpeed = 1f;

	public bool m_isHeroActor;

	public static string ACTOR_VISIBLE_EVENT = "ActorVisible";

	protected override void OnBirth(SpellStateType prevStateType)
	{
		StartCoroutine(BirthState());
	}

	private IEnumerator BirthState()
	{
		InitActorVariables();
		SetActorVisibility(visible: false, ignoreSpells: true);
		SetVisibility(m_burnIn, visible: true);
		SetAnimationSpeed(m_burnIn, "AllyInHandScryLines_Forge", m_burnInAnimationSpeed);
		PlayAnimation(m_burnIn, "AllyInHandScryLines_Forge", PlayMode.StopAll);
		PlayParticles(m_smokePuff, includeChildren: false);
		PlayParticles(m_blackBits, includeChildren: false);
		yield return new WaitForSeconds(0.2f);
		SetVisibility(m_burnIn, visible: true);
		Renderer renderer = ((m_smokePuff != null) ? m_smokePuff.GetComponent<Renderer>() : null);
		if (renderer != null)
		{
			renderer.enabled = true;
		}
		renderer = ((m_blackBits != null) ? m_blackBits.GetComponent<Renderer>() : null);
		if (renderer != null)
		{
			renderer.enabled = true;
		}
		SetActorVisibility(visible: true, ignoreSpells: true);
		OnSpellEvent(ACTOR_VISIBLE_EVENT, null);
		if (m_isHeroActor)
		{
			GameObject actorObject = GetActorObject("AttackObject");
			GameObject actorObject2 = GetActorObject("HealthObject");
			SetVisibilityRecursive(actorObject, visible: false);
			SetVisibilityRecursive(actorObject2, visible: false);
		}
		yield return new WaitForSeconds(0.2f);
		OnSpellFinished();
	}
}
