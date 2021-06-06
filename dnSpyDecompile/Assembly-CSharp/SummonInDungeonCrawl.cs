using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000829 RID: 2089
public class SummonInDungeonCrawl : SpellImpl
{
	// Token: 0x06007037 RID: 28727 RVA: 0x002434F6 File Offset: 0x002416F6
	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.StartCoroutine(this.BirthState());
	}

	// Token: 0x06007038 RID: 28728 RVA: 0x00243505 File Offset: 0x00241705
	private IEnumerator BirthState()
	{
		base.InitActorVariables();
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

	// Token: 0x04005A1C RID: 23068
	public GameObject m_burnIn;

	// Token: 0x04005A1D RID: 23069
	public GameObject m_blackBits;

	// Token: 0x04005A1E RID: 23070
	public GameObject m_smokePuff;

	// Token: 0x04005A1F RID: 23071
	public float m_burnInAnimationSpeed = 1f;

	// Token: 0x04005A20 RID: 23072
	public bool m_isHeroActor;
}
