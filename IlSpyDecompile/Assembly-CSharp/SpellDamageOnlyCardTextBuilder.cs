public class SpellDamageOnlyCardTextBuilder : CardTextBuilder
{
	public SpellDamageOnlyCardTextBuilder()
	{
		m_useEntityForTextInPlay = true;
	}

	public override string BuildCardTextInHand(Entity entity)
	{
		return TextUtils.TransformCardText(entity.GetDamageBonus(), 0, 0, CardTextBuilder.GetRawCardTextInHand(entity.GetCardId()));
	}

	public override string BuildCardTextInHistory(Entity entity)
	{
		CardTextHistoryData cardTextHistoryData = entity.GetCardTextHistoryData();
		if (cardTextHistoryData == null)
		{
			Log.All.Print("SpellDamageOnlyCardTextBuilder.BuildCardTextInHistory: entity {0} does not have a CardTextHistoryData object.", entity.GetEntityId());
			return string.Empty;
		}
		return TextUtils.TransformCardText(cardTextHistoryData.m_damageBonus, 0, 0, CardTextBuilder.GetRawCardTextInHand(entity.GetCardId()));
	}
}
