public class ScriptDataNum1Num2CardTextBuilder : CardTextBuilder
{
	public ScriptDataNum1Num2CardTextBuilder()
	{
		m_useEntityForTextInPlay = true;
	}

	public override string BuildCardTextInHand(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		string arg = entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1).ToString();
		string arg2 = entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2).ToString();
		string powersText = string.Format(rawCardTextInHand, arg, arg2);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}

	public override string BuildCardTextInHistory(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		string arg = entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1).ToString();
		string arg2 = entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2).ToString();
		string powersText = string.Format(rawCardTextInHand, arg, arg2);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}

	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entityDef.GetCardId());
		string arg = entityDef.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1).ToString();
		string arg2 = entityDef.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2).ToString();
		return TextUtils.TransformCardText(string.Format(rawCardTextInHand, arg, arg2));
	}

	public override void OnTagChange(Card card, TagDelta tagChange)
	{
		if ((tagChange.tag == 2 || tagChange.tag == 3) && card != null && card.GetActor() != null)
		{
			card.GetActor().UpdateTextComponents();
		}
	}
}
