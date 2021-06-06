public class GalakrondCounterCardTextBuilder : CardTextBuilder
{
	public GalakrondCounterCardTextBuilder()
	{
		m_useEntityForTextInPlay = true;
	}

	public override string BuildCardTextInHand(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		string newValue = ((entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2) - entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1) == 1) ? GameStrings.Get("GALAKROND_ONCE") : GameStrings.Get("GALAKROND_TWICE"));
		string powersText = rawCardTextInHand.Replace("@", newValue);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}

	public override string BuildCardTextInHistory(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		string newValue = ((entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2) - entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1) == 1) ? GameStrings.Get("GALAKROND_ONCE") : GameStrings.Get("GALAKROND_TWICE"));
		string powersText = rawCardTextInHand.Replace("@", newValue);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}

	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entityDef.GetCardId());
		string newValue = ((entityDef.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2) - entityDef.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1) == 1) ? GameStrings.Get("GALAKROND_ONCE") : GameStrings.Get("GALAKROND_TWICE"));
		return TextUtils.TransformCardText(rawCardTextInHand.Replace("@", newValue));
	}

	public override void OnTagChange(Card card, TagDelta tagChange)
	{
		if (tagChange.tag == 2 && card != null && card.GetActor() != null)
		{
			card.GetActor().UpdateTextComponents();
		}
	}
}
