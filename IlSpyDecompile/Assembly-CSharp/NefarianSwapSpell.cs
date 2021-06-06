using System.Collections;
using UnityEngine;

public class NefarianSwapSpell : HeroSwapSpell
{
	public float m_obsoleteRemovalDelay;

	private Card m_obsoleteHeroCard;

	public override bool AddPowerTargets()
	{
		if (!base.AddPowerTargets())
		{
			return false;
		}
		int num = m_oldHeroCard.GetEntity().GetTag(GAME_TAG.LINKED_ENTITY);
		if (num != 0)
		{
			m_obsoleteHeroCard = GameState.Get().GetEntity(num).GetCard();
		}
		if (m_obsoleteHeroCard == null)
		{
			return false;
		}
		return true;
	}

	public override void CustomizeFXProcess(Actor heroActor)
	{
		if (heroActor == m_newHeroCard.GetActor())
		{
			StartCoroutine(DestroyObsolete());
		}
	}

	private IEnumerator DestroyObsolete()
	{
		yield return new WaitForSeconds(m_obsoleteRemovalDelay);
		Actor actor = m_obsoleteHeroCard.GetActor();
		if (actor != null)
		{
			Object.Destroy(actor.gameObject);
		}
	}
}
