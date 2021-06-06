public class ReferenceCreatorEntityCardTextBuilder : CardTextBuilder
{
	public ReferenceCreatorEntityCardTextBuilder()
	{
		m_useEntityForTextInPlay = true;
	}

	public override string BuildCardTextInHand(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		Entity entity2 = GameState.Get().GetEntity(entity.GetTag(GAME_TAG.CREATOR));
		string arg = ((entity2 != null && entity2.HasValidDisplayName()) ? entity2.GetName() : GameStrings.Get("GAMEPLAY_UNKNOWN_CREATED_BY"));
		string powersText = string.Format(rawCardTextInHand, arg);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}
}
