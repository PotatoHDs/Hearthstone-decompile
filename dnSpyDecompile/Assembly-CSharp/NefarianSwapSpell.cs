using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200080A RID: 2058
public class NefarianSwapSpell : HeroSwapSpell
{
	// Token: 0x06006F7C RID: 28540 RVA: 0x0023F23C File Offset: 0x0023D43C
	public override bool AddPowerTargets()
	{
		if (!base.AddPowerTargets())
		{
			return false;
		}
		int tag = this.m_oldHeroCard.GetEntity().GetTag(GAME_TAG.LINKED_ENTITY);
		if (tag != 0)
		{
			this.m_obsoleteHeroCard = GameState.Get().GetEntity(tag).GetCard();
		}
		return !(this.m_obsoleteHeroCard == null);
	}

	// Token: 0x06006F7D RID: 28541 RVA: 0x0023F293 File Offset: 0x0023D493
	public override void CustomizeFXProcess(Actor heroActor)
	{
		if (heroActor == this.m_newHeroCard.GetActor())
		{
			base.StartCoroutine(this.DestroyObsolete());
		}
	}

	// Token: 0x06006F7E RID: 28542 RVA: 0x0023F2B5 File Offset: 0x0023D4B5
	private IEnumerator DestroyObsolete()
	{
		yield return new WaitForSeconds(this.m_obsoleteRemovalDelay);
		Actor actor = this.m_obsoleteHeroCard.GetActor();
		if (actor != null)
		{
			UnityEngine.Object.Destroy(actor.gameObject);
		}
		yield break;
	}

	// Token: 0x0400595C RID: 22876
	public float m_obsoleteRemovalDelay;

	// Token: 0x0400595D RID: 22877
	private Card m_obsoleteHeroCard;
}
