public class DecorateCardTextBuilder : CardTextBuilder
{
	public override string BuildCardTextInHand(Entity entity)
	{
		string powersText = string.Format(CardTextBuilder.GetRawCardTextInHand(entity.GetCardId()), entity.GetTag(GAME_TAG.COST), entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2));
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}

	public override string BuildCardTextInHistory(Entity entity)
	{
		DecorateCardTextHistoryData decorateCardTextHistoryData = entity.GetCardTextHistoryData() as DecorateCardTextHistoryData;
		if (decorateCardTextHistoryData == null)
		{
			Log.All.Print("DecorateCardTextBuilder.BuildCardTextInHistory: entity {0} does not have a DecorateCardTextHistoryData object.", entity.GetEntityId());
			return string.Empty;
		}
		string powersText = string.Format(CardTextBuilder.GetRawCardTextInHand(entity.GetCardId()), decorateCardTextHistoryData.m_cost, decorateCardTextHistoryData.m_decorationProgress);
		return TextUtils.TransformCardText(decorateCardTextHistoryData.m_damageBonus, decorateCardTextHistoryData.m_damageBonusDouble, decorateCardTextHistoryData.m_healingDouble, powersText);
	}

	public override CardTextHistoryData CreateCardTextHistoryData()
	{
		return new DecorateCardTextHistoryData();
	}
}
