using System.Collections;
using UnityEngine;

public class HeroCustomSummonSpell : Spell
{
	public Spell m_NewHeroFX;

	private Card m_oldHeroCard;

	private Card m_newHeroCard;

	private Spell m_swapSpell;

	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		StartCoroutine(SetupHeroesAndPlay());
	}

	protected override void OnCancel(SpellStateType prevStateType)
	{
		if (m_swapSpell != null && m_swapSpell.GetActiveState() != 0 && m_swapSpell.GetActiveState() != SpellStateType.CANCEL)
		{
			m_swapSpell.ActivateState(SpellStateType.CANCEL);
		}
		base.OnCancel(prevStateType);
	}

	private IEnumerator SetupHeroesAndPlay()
	{
		SetupHeroes();
		if (!m_newHeroCard.GetEntity().HasTag(GAME_TAG.SIDEKICK))
		{
			HideStats(m_oldHeroCard);
		}
		HideStats(m_newHeroCard);
		m_newHeroCard.GetActor().TurnOffCollider();
		TransformUtil.CopyWorld(m_newHeroCard, m_newHeroCard.GetZone().GetZoneTransformForCard(m_newHeroCard));
		if (m_NewHeroFX == null)
		{
			Finish();
		}
		else
		{
			yield return PlaySummonSpell();
		}
	}

	private void SetupHeroes()
	{
		m_newHeroCard = GetSourceCard();
		if (m_newHeroCard == null)
		{
			Debug.LogErrorFormat("no card for gameObject: {0}", GetSource());
		}
		else
		{
			m_oldHeroCard = GetOldHeroCard(m_newHeroCard);
		}
	}

	private IEnumerator PlaySummonSpell()
	{
		Actor actor = m_newHeroCard.GetActor();
		m_swapSpell = Object.Instantiate(m_NewHeroFX);
		GameObject gameObject = m_swapSpell.gameObject;
		SpellUtils.SetCustomSpellParent(m_swapSpell, actor);
		m_swapSpell.SetSource(m_newHeroCard.gameObject);
		m_swapSpell.Activate();
		while (!m_swapSpell.IsFinished())
		{
			yield return null;
		}
		Finish();
		while (m_swapSpell.GetActiveState() != 0)
		{
			yield return null;
		}
		if (gameObject != null)
		{
			Object.Destroy(gameObject);
		}
		Deactivate();
	}

	private void Finish()
	{
		m_newHeroCard.GetActor().TurnOnCollider();
		m_newHeroCard.ShowCard();
		if (!m_newHeroCard.GetEntity().HasTag(GAME_TAG.SIDEKICK))
		{
			m_oldHeroCard.TransitionToZone(null);
		}
		OnSpellFinished();
	}

	public static Card GetOldHeroCard(Card hero)
	{
		ZoneHero zoneHero = hero.GetZone() as ZoneHero;
		if (zoneHero == null)
		{
			Debug.LogErrorFormat("not in ZoneHero. card: {0}, zone: {1}", hero, hero.GetZone());
			return null;
		}
		int num = zoneHero.FindCardPos(hero);
		if (num <= 1)
		{
			Debug.LogErrorFormat("invalid position. card: {0}, position: {1}", hero, num);
			return null;
		}
		return zoneHero.GetCardAtPos(num - 1);
	}

	public static void HideStats(Card hero)
	{
		hero.GetActor().HideArmorSpell();
		hero.GetActor().DisableArmorSpellForTransition();
		hero.GetActor().GetHealthObject().Hide();
		hero.GetActor().GetAttackObject().Hide();
	}
}
