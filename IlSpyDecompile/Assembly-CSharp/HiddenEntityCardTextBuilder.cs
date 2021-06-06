public class HiddenEntityCardTextBuilder : CardTextBuilder
{
	public override string BuildCardTextInHand(Entity entity)
	{
		string[] array = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId()).Split('@');
		string text = array[0];
		int tag = entity.GetTag(GAME_TAG.HIDDEN_CHOICE);
		if (tag > 0 && array.Length > 1)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(tag);
			if (entityDef != null && entityDef.HasValidDisplayName())
			{
				text = string.Format(array[1], CardTextBuilder.GetRawCardName(entityDef));
			}
		}
		return TextUtils.TransformCardText(entity, text);
	}
}
