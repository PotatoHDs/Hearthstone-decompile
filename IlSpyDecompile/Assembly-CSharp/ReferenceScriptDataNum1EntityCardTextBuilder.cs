public class ReferenceScriptDataNum1EntityCardTextBuilder : CardTextBuilder
{
	public ReferenceScriptDataNum1EntityCardTextBuilder()
	{
		m_useEntityForTextInPlay = true;
	}

	public override string BuildCardTextInHand(Entity entity)
	{
		return BuildText(entity);
	}

	public override string BuildCardTextInHistory(Entity entity)
	{
		return BuildText(entity);
	}

	private static string BuildText(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		Entity entity2 = GameState.Get().GetEntity(entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1));
		string arg = ((entity2 != null && entity2.HasValidDisplayName()) ? entity2.GetName() : GameStrings.Get("GAMEPLAY_UNKNOWN_CREATED_BY"));
		string powersText = string.Format(rawCardTextInHand, arg);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}
}
