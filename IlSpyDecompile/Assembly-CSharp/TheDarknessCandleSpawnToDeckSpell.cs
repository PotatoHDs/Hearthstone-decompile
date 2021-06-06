public class TheDarknessCandleSpawnToDeckSpell : SpawnToDeckSpell
{
	protected override void OnAction(SpellStateType prevStateType)
	{
		Card card = GetSourceCard();
		int id = card.GetEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_ENT_1);
		Entity entity = GameState.Get().GetEntity(id);
		if (entity != null)
		{
			card = entity.GetCard();
		}
		SetSource(card.gameObject);
		card.GetActorSpell(SpellType.BATTLECRY).ActivateState(SpellStateType.ACTION);
		base.OnAction(prevStateType);
	}
}
