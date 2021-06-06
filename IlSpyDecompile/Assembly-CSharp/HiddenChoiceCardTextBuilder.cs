public class HiddenChoiceCardTextBuilder : CardTextBuilder
{
	private string GetCorrectSubstring(string text, int hiddenChoiceValue)
	{
		string[] array = text.Split('@');
		if (array.Length <= hiddenChoiceValue)
		{
			Log.All.Print("HiddenChoiceCardTextBuilder.GetCorrectSubstring: entity does not have text for hidden choice {1}.", hiddenChoiceValue);
			return text;
		}
		return array[hiddenChoiceValue];
	}

	public override string BuildCardTextInHand(Entity entity)
	{
		string text = base.BuildCardTextInHand(entity);
		return GetCorrectSubstring(text, entity.GetTag(GAME_TAG.HIDDEN_CHOICE));
	}

	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string text = base.BuildCardTextInHand(entityDef);
		return GetCorrectSubstring(text, entityDef.GetTag(GAME_TAG.HIDDEN_CHOICE));
	}

	public override string BuildCardTextInHistory(Entity entity)
	{
		string text = base.BuildCardTextInHistory(entity);
		return GetCorrectSubstring(text, entity.GetTag(GAME_TAG.HIDDEN_CHOICE));
	}
}
