public class PrimordialWandCardTextBuilder : CardTextBuilder
{
	public PrimordialWandCardTextBuilder()
	{
		m_useEntityForTextInPlay = true;
	}

	private string BuildText(Entity entity)
	{
		string text = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		int num = text.IndexOf('@');
		if (num >= 0)
		{
			text = text.Substring(num + 1);
		}
		text = string.Format(text, entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1) + 1);
		return TextUtils.TransformCardText(entity, text);
	}

	public override string BuildCardTextInHand(Entity entity)
	{
		return BuildText(entity);
	}

	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string text = base.BuildCardTextInHand(entityDef);
		int num = text.IndexOf('@');
		if (num >= 0)
		{
			text = text.Substring(0, num);
		}
		return text;
	}

	public override string BuildCardTextInHistory(Entity entity)
	{
		return BuildText(entity);
	}
}
