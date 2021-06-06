public class AlternateCardTextCardTextBuilder : CardTextBuilder
{
	public AlternateCardTextCardTextBuilder()
	{
		m_useEntityForTextInPlay = true;
	}

	public override string BuildCardTextInHand(Entity entity)
	{
		string builtText = base.BuildCardTextInHand(entity);
		return GetAlternateCardText(builtText, entity.GetTag(GAME_TAG.USE_ALTERNATE_CARD_TEXT));
	}

	private string GetAlternateCardText(string builtText, int alternateCardTextIndex)
	{
		int num = builtText.IndexOf('@');
		if (num < 0)
		{
			return builtText;
		}
		for (int i = 0; i < alternateCardTextIndex; i++)
		{
			builtText = builtText.Substring(num + 1);
			num = builtText.IndexOf('@');
			if (num < 0)
			{
				break;
			}
		}
		if (num >= 0)
		{
			builtText = builtText.Substring(0, num);
		}
		return builtText;
	}

	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string builtText = base.BuildCardTextInHand(entityDef);
		return GetAlternateCardText(builtText, entityDef.GetTag(GAME_TAG.USE_ALTERNATE_CARD_TEXT));
	}

	public override string BuildCardTextInHistory(Entity entity)
	{
		string builtText = base.BuildCardTextInHand(entity);
		return GetAlternateCardText(builtText, entity.GetTag(GAME_TAG.USE_ALTERNATE_CARD_TEXT));
	}

	public override void OnTagChange(Card card, TagDelta tagChange)
	{
		GAME_TAG tag = (GAME_TAG)tagChange.tag;
		if (tag == GAME_TAG.USE_ALTERNATE_CARD_TEXT && card != null && card.GetActor() != null)
		{
			card.GetActor().UpdatePowersText();
		}
	}
}
