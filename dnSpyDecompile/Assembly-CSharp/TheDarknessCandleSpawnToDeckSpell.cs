using System;

// Token: 0x0200082C RID: 2092
public class TheDarknessCandleSpawnToDeckSpell : SpawnToDeckSpell
{
	// Token: 0x06007042 RID: 28738 RVA: 0x002435F8 File Offset: 0x002417F8
	protected override void OnAction(SpellStateType prevStateType)
	{
		Card card = base.GetSourceCard();
		int tag = card.GetEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_ENT_1);
		Entity entity = GameState.Get().GetEntity(tag);
		if (entity != null)
		{
			card = entity.GetCard();
		}
		this.SetSource(card.gameObject);
		card.GetActorSpell(SpellType.BATTLECRY, true).ActivateState(SpellStateType.ACTION);
		base.OnAction(prevStateType);
	}
}
