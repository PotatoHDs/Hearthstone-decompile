public class ReferenceScriptDataNum1Num2EntityCardTextBuilder : CardTextBuilder
{
	public ReferenceScriptDataNum1Num2EntityCardTextBuilder()
	{
		m_useEntityForTextInPlay = true;
	}

	public override string BuildCardTextInHand(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		Entity entity2 = GameState.Get().GetEntity(entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1));
		Entity entity3 = GameState.Get().GetEntity(entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2));
		string arg = ((entity2 != null && entity2.HasValidDisplayName()) ? entity2.GetName() : GameStrings.Get("GAMEPLAY_UNKNOWN_CREATED_BY"));
		string arg2 = ((entity3 != null && entity3.HasValidDisplayName()) ? entity3.GetName() : GameStrings.Get("GAMEPLAY_UNKNOWN_CREATED_BY"));
		string powersText = string.Format(rawCardTextInHand, arg, arg2);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}

	public override string BuildCardTextInHistory(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		Entity entity2 = GameState.Get().GetEntity(entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1));
		Entity entity3 = GameState.Get().GetEntity(entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2));
		string arg = ((entity2 != null && entity2.HasValidDisplayName()) ? entity2.GetName() : GameStrings.Get("GAMEPLAY_UNKNOWN_CREATED_BY"));
		string arg2 = ((entity3 != null && entity3.HasValidDisplayName()) ? entity3.GetName() : GameStrings.Get("GAMEPLAY_UNKNOWN_CREATED_BY"));
		string powersText = string.Format(rawCardTextInHand, arg, arg2);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}
}
