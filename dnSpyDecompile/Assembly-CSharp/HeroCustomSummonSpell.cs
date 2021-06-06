using System;
using System.Collections;
using UnityEngine;

// Token: 0x020007F5 RID: 2037
public class HeroCustomSummonSpell : Spell
{
	// Token: 0x06006ED9 RID: 28377 RVA: 0x0023BA62 File Offset: 0x00239C62
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		base.StartCoroutine(this.SetupHeroesAndPlay());
	}

	// Token: 0x06006EDA RID: 28378 RVA: 0x0023BA78 File Offset: 0x00239C78
	protected override void OnCancel(SpellStateType prevStateType)
	{
		if (this.m_swapSpell != null && this.m_swapSpell.GetActiveState() != SpellStateType.NONE && this.m_swapSpell.GetActiveState() != SpellStateType.CANCEL)
		{
			this.m_swapSpell.ActivateState(SpellStateType.CANCEL);
		}
		base.OnCancel(prevStateType);
	}

	// Token: 0x06006EDB RID: 28379 RVA: 0x0023BAB6 File Offset: 0x00239CB6
	private IEnumerator SetupHeroesAndPlay()
	{
		this.SetupHeroes();
		if (!this.m_newHeroCard.GetEntity().HasTag(GAME_TAG.SIDEKICK))
		{
			HeroCustomSummonSpell.HideStats(this.m_oldHeroCard);
		}
		HeroCustomSummonSpell.HideStats(this.m_newHeroCard);
		this.m_newHeroCard.GetActor().TurnOffCollider();
		TransformUtil.CopyWorld(this.m_newHeroCard, this.m_newHeroCard.GetZone().GetZoneTransformForCard(this.m_newHeroCard));
		if (this.m_NewHeroFX == null)
		{
			this.Finish();
			yield break;
		}
		yield return this.PlaySummonSpell();
		yield break;
	}

	// Token: 0x06006EDC RID: 28380 RVA: 0x0023BAC8 File Offset: 0x00239CC8
	private void SetupHeroes()
	{
		this.m_newHeroCard = base.GetSourceCard();
		if (this.m_newHeroCard == null)
		{
			Debug.LogErrorFormat("no card for gameObject: {0}", new object[]
			{
				base.GetSource()
			});
			return;
		}
		this.m_oldHeroCard = HeroCustomSummonSpell.GetOldHeroCard(this.m_newHeroCard);
	}

	// Token: 0x06006EDD RID: 28381 RVA: 0x0023BB1A File Offset: 0x00239D1A
	private IEnumerator PlaySummonSpell()
	{
		Actor actor = this.m_newHeroCard.GetActor();
		this.m_swapSpell = UnityEngine.Object.Instantiate<Spell>(this.m_NewHeroFX);
		GameObject gameObject = this.m_swapSpell.gameObject;
		SpellUtils.SetCustomSpellParent(this.m_swapSpell, actor);
		this.m_swapSpell.SetSource(this.m_newHeroCard.gameObject);
		this.m_swapSpell.Activate();
		while (!this.m_swapSpell.IsFinished())
		{
			yield return null;
		}
		this.Finish();
		while (this.m_swapSpell.GetActiveState() != SpellStateType.NONE)
		{
			yield return null;
		}
		if (gameObject != null)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		base.Deactivate();
		yield break;
	}

	// Token: 0x06006EDE RID: 28382 RVA: 0x0023BB2C File Offset: 0x00239D2C
	private void Finish()
	{
		this.m_newHeroCard.GetActor().TurnOnCollider();
		this.m_newHeroCard.ShowCard();
		if (!this.m_newHeroCard.GetEntity().HasTag(GAME_TAG.SIDEKICK))
		{
			this.m_oldHeroCard.TransitionToZone(null, null);
		}
		this.OnSpellFinished();
	}

	// Token: 0x06006EDF RID: 28383 RVA: 0x0023BB80 File Offset: 0x00239D80
	public static Card GetOldHeroCard(Card hero)
	{
		ZoneHero zoneHero = hero.GetZone() as ZoneHero;
		if (zoneHero == null)
		{
			Debug.LogErrorFormat("not in ZoneHero. card: {0}, zone: {1}", new object[]
			{
				hero,
				hero.GetZone()
			});
			return null;
		}
		int num = zoneHero.FindCardPos(hero);
		if (num <= 1)
		{
			Debug.LogErrorFormat("invalid position. card: {0}, position: {1}", new object[]
			{
				hero,
				num
			});
			return null;
		}
		return zoneHero.GetCardAtPos(num - 1);
	}

	// Token: 0x06006EE0 RID: 28384 RVA: 0x0023BBF5 File Offset: 0x00239DF5
	public static void HideStats(Card hero)
	{
		hero.GetActor().HideArmorSpell();
		hero.GetActor().DisableArmorSpellForTransition();
		hero.GetActor().GetHealthObject().Hide();
		hero.GetActor().GetAttackObject().Hide();
	}

	// Token: 0x040058EE RID: 22766
	public Spell m_NewHeroFX;

	// Token: 0x040058EF RID: 22767
	private Card m_oldHeroCard;

	// Token: 0x040058F0 RID: 22768
	private Card m_newHeroCard;

	// Token: 0x040058F1 RID: 22769
	private Spell m_swapSpell;
}
