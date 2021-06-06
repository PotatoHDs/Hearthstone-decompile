public class HeroPlaySummonOutSpell : Spell
{
	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.OnBirth(prevStateType);
		ShowOriginalHero();
	}

	private void ShowOriginalHero()
	{
		Card heroCard = GetSourceCard().GetController().GetHeroCard();
		heroCard.GetActor().EnableArmorSpellAfterTransition();
		heroCard.GetActor().ShowArmorSpell();
		heroCard.GetActor().GetHealthObject().Show();
		heroCard.GetActor().GetAttackObject().Show();
	}
}
