public class DiamondSpellstoneTextBuilder : CardTextBuilder
{
	public override string BuildCardTextInHand(Entity entity)
	{
		string text = base.BuildCardTextInHand(entity);
		string newValue = GameStrings.Get("GAMEPLAY_DIAMOND_SPELLSTONE_" + (entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1) + 1));
		return text.Replace("@", newValue);
	}

	public override void OnTagChange(Card card, TagDelta tagChange)
	{
		if (tagChange.tag == 2 || tagChange.tag == 3)
		{
			card.GetActor().UpdateTextComponents();
		}
	}

	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		return base.BuildCardTextInHand(entityDef).Replace("@", GameStrings.Get("GAMEPLAY_DIAMOND_SPELLSTONE_1"));
	}

	public override string BuildCardTextInHistory(Entity entity)
	{
		CardTextHistoryData cardTextHistoryData = entity.GetCardTextHistoryData();
		if (cardTextHistoryData == null)
		{
			Log.All.Print("DiamondSpellstoneTextBuilder.BuildCardTextInHistory: entity {0} does not have a CardTextHistoryData object.", entity.GetEntityId());
			return "";
		}
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		rawCardTextInHand = rawCardTextInHand.Replace("@", GameStrings.Get("GAMEPLAY_DIAMOND_SPELLSTONE_1"));
		return TextUtils.TransformCardText(cardTextHistoryData.m_damageBonus, cardTextHistoryData.m_damageBonusDouble, cardTextHistoryData.m_healingDouble, rawCardTextInHand);
	}
}
