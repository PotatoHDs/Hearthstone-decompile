using System;

// Token: 0x020007F7 RID: 2039
public class HeroPlaySummonOutSpell : Spell
{
	// Token: 0x06006EE6 RID: 28390 RVA: 0x0023BCF4 File Offset: 0x00239EF4
	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.OnBirth(prevStateType);
		this.ShowOriginalHero();
	}

	// Token: 0x06006EE7 RID: 28391 RVA: 0x0023BD04 File Offset: 0x00239F04
	private void ShowOriginalHero()
	{
		Card heroCard = base.GetSourceCard().GetController().GetHeroCard();
		heroCard.GetActor().EnableArmorSpellAfterTransition();
		heroCard.GetActor().ShowArmorSpell();
		heroCard.GetActor().GetHealthObject().Show();
		heroCard.GetActor().GetAttackObject().Show();
	}
}
