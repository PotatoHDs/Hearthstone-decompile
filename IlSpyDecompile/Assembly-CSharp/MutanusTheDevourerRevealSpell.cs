public class MutanusTheDevourerRevealSpell : Spell
{
	protected override void OnBirth(SpellStateType prevStateType)
	{
		Card sourceCard = GetSourceCard();
		Entity entity = sourceCard.GetEntity();
		if (entity != null && entity.IsControlledByOpposingSidePlayer())
		{
			string handActor = ActorNames.GetHandActor(entity);
			sourceCard.UpdateActor(forceIfNullZone: false, handActor);
		}
		base.OnBirth(prevStateType);
	}

	public override void OnSpellFinished()
	{
		base.OnSpellFinished();
	}
}
