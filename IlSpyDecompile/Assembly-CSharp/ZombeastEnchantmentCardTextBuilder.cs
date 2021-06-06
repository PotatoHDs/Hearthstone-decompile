public class ZombeastEnchantmentCardTextBuilder : CardTextBuilder
{
	public ZombeastEnchantmentCardTextBuilder()
	{
		m_useEntityForTextInPlay = true;
	}

	public override string BuildCardTextInHand(Entity entity)
	{
		if (entity.HasTag(GAME_TAG.MODULAR_ENTITY_PART_1) && entity.HasTag(GAME_TAG.MODULAR_ENTITY_PART_2))
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(entity.GetTag(GAME_TAG.MODULAR_ENTITY_PART_1));
			EntityDef entityDef2 = DefLoader.Get().GetEntityDef(entity.GetTag(GAME_TAG.MODULAR_ENTITY_PART_2));
			if (entityDef != null && entityDef2 != null)
			{
				string text = string.Format(CardTextBuilder.GetRawCardTextInHand(entity.GetCardId()), entityDef.GetName(), entityDef2.GetName());
				return TextUtils.TransformCardText(entity, text);
			}
		}
		return string.Empty;
	}
}
