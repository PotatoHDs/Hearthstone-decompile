using System;
using System.Collections;
using UnityEngine;

// Token: 0x020007D2 RID: 2002
public class CardBurn : Spell
{
	// Token: 0x06006E16 RID: 28182 RVA: 0x00237E40 File Offset: 0x00236040
	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.StartCoroutine(this.BirthAction());
	}

	// Token: 0x06006E17 RID: 28183 RVA: 0x00237E4F File Offset: 0x0023604F
	private IEnumerator BirthAction()
	{
		if (this.m_BurnCardQuad)
		{
			this.m_BurnCardQuad.GetComponent<Renderer>().enabled = true;
			this.m_BurnCardQuad.GetComponent<Animation>().Play(this.m_BurnCardAnim, PlayMode.StopAll);
		}
		if (this.m_EdgeEmbers)
		{
			this.m_EdgeEmbers.Play();
		}
		yield return new WaitForSeconds(0.15f);
		Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.gameObject);
		if (actor == null)
		{
			yield break;
		}
		actor.Hide();
		this.OnSpellFinished();
		yield break;
	}

	// Token: 0x04005841 RID: 22593
	public GameObject m_BurnCardQuad;

	// Token: 0x04005842 RID: 22594
	public string m_BurnCardAnim = "CardBurnUpFire";

	// Token: 0x04005843 RID: 22595
	public ParticleSystem m_EdgeEmbers;
}
