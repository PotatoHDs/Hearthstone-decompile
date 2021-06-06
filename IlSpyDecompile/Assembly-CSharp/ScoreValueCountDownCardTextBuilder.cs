public class ScoreValueCountDownCardTextBuilder : CardTextBuilder
{
	public ScoreValueCountDownCardTextBuilder()
	{
		m_useEntityForTextInPlay = true;
	}

	public override string BuildCardTextInHand(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		string newValue = GetProgressRemaining(entity).ToString();
		string powersText = rawCardTextInHand.Replace("@", newValue);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}

	public override string BuildCardTextInHistory(Entity entity)
	{
		return BuildCardTextInHand(entity);
	}

	private int GetProgressRemaining(Entity entity)
	{
		int tag = entity.GetTag(GAME_TAG.SCORE_VALUE_1);
		int tag2 = entity.GetTag(GAME_TAG.SCORE_VALUE_2);
		int num = tag - tag2;
		if (num < 0)
		{
			num = 0;
		}
		return num;
	}

	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entityDef.GetCardId());
		string newValue = entityDef.GetTag(GAME_TAG.SCORE_VALUE_1).ToString();
		return TextUtils.TransformCardText(rawCardTextInHand.Replace("@", newValue));
	}

	public override void OnTagChange(Card card, TagDelta tagChange)
	{
		if (card == null)
		{
			return;
		}
		Actor actor = card.GetActor();
		if (!(actor == null))
		{
			GAME_TAG tag = (GAME_TAG)tagChange.tag;
			if (tag == GAME_TAG.SCORE_VALUE_1 || tag == GAME_TAG.SCORE_VALUE_2)
			{
				actor.UpdateTextComponents();
			}
		}
	}
}
