public class KazakusPotionEffectCardTextBuilder : CardTextBuilder
{
	private string GetCorrectSubstring(string text)
	{
		int num = text.IndexOf('@');
		if (num >= 0)
		{
			return text.Substring(0, num);
		}
		return text;
	}

	public override string BuildCardTextInHand(Entity entity)
	{
		string text = base.BuildCardTextInHand(entity);
		return GetCorrectSubstring(text);
	}

	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string text = base.BuildCardTextInHand(entityDef);
		return GetCorrectSubstring(text);
	}

	public override string BuildCardTextInHistory(Entity entity)
	{
		string text = base.BuildCardTextInHistory(entity);
		return GetCorrectSubstring(text);
	}
}
