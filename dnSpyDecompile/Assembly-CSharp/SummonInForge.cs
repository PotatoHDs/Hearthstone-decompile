using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200082A RID: 2090
public class SummonInForge : SpellImpl
{
	// Token: 0x0600703A RID: 28730 RVA: 0x00243527 File Offset: 0x00241727
	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.StartCoroutine(this.BirthState());
	}

	// Token: 0x0600703B RID: 28731 RVA: 0x00243536 File Offset: 0x00241736
	private IEnumerator BirthState()
	{
		base.InitActorVariables();
		base.SetActorVisibility(false, true);
		base.SetVisibility(this.m_burnIn, true);
		base.SetAnimationSpeed(this.m_burnIn, "AllyInHandScryLines_Forge", this.m_burnInAnimationSpeed);
		base.PlayAnimation(this.m_burnIn, "AllyInHandScryLines_Forge", PlayMode.StopAll, 0f);
		base.PlayParticles(this.m_smokePuff, false);
		base.PlayParticles(this.m_blackBits, false);
		yield return new WaitForSeconds(0.2f);
		base.SetVisibility(this.m_burnIn, true);
		Renderer renderer = (this.m_smokePuff != null) ? this.m_smokePuff.GetComponent<Renderer>() : null;
		if (renderer != null)
		{
			renderer.enabled = true;
		}
		renderer = ((this.m_blackBits != null) ? this.m_blackBits.GetComponent<Renderer>() : null);
		if (renderer != null)
		{
			renderer.enabled = true;
		}
		base.SetActorVisibility(true, true);
		this.OnSpellEvent(SummonInForge.ACTOR_VISIBLE_EVENT, null);
		if (this.m_isHeroActor)
		{
			GameObject actorObject = base.GetActorObject("AttackObject");
			GameObject actorObject2 = base.GetActorObject("HealthObject");
			base.SetVisibilityRecursive(actorObject, false);
			base.SetVisibilityRecursive(actorObject2, false);
		}
		yield return new WaitForSeconds(0.2f);
		this.OnSpellFinished();
		yield break;
	}

	// Token: 0x04005A21 RID: 23073
	public GameObject m_burnIn;

	// Token: 0x04005A22 RID: 23074
	public GameObject m_blackBits;

	// Token: 0x04005A23 RID: 23075
	public GameObject m_smokePuff;

	// Token: 0x04005A24 RID: 23076
	public float m_burnInAnimationSpeed = 1f;

	// Token: 0x04005A25 RID: 23077
	public bool m_isHeroActor;

	// Token: 0x04005A26 RID: 23078
	public static string ACTOR_VISIBLE_EVENT = "ActorVisible";
}
