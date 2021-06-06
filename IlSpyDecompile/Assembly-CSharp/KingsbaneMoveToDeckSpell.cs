public class KingsbaneMoveToDeckSpell : SpawnToDeckSpell
{
	protected override void OnActorLoaded(Actor actor)
	{
		actor.SetEntityDef(null);
		actor.SetEntity(GetSourceCard().GetEntity());
	}
}
