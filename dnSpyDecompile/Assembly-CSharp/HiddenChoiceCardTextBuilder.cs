using System;

// Token: 0x02000794 RID: 1940
public class HiddenChoiceCardTextBuilder : CardTextBuilder
{
	// Token: 0x06006C76 RID: 27766 RVA: 0x002314E8 File Offset: 0x0022F6E8
	private string GetCorrectSubstring(string text, int hiddenChoiceValue)
	{
		string[] array = text.Split(new char[]
		{
			'@'
		});
		if (array.Length <= hiddenChoiceValue)
		{
			Log.All.Print("HiddenChoiceCardTextBuilder.GetCorrectSubstring: entity does not have text for hidden choice {1}.", new object[]
			{
				hiddenChoiceValue
			});
			return text;
		}
		return array[hiddenChoiceValue];
	}

	// Token: 0x06006C77 RID: 27767 RVA: 0x00231530 File Offset: 0x0022F730
	public override string BuildCardTextInHand(Entity entity)
	{
		string text = base.BuildCardTextInHand(entity);
		return this.GetCorrectSubstring(text, entity.GetTag(GAME_TAG.HIDDEN_CHOICE));
	}

	// Token: 0x06006C78 RID: 27768 RVA: 0x00231558 File Offset: 0x0022F758
	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string text = base.BuildCardTextInHand(entityDef);
		return this.GetCorrectSubstring(text, entityDef.GetTag(GAME_TAG.HIDDEN_CHOICE));
	}

	// Token: 0x06006C79 RID: 27769 RVA: 0x00231580 File Offset: 0x0022F780
	public override string BuildCardTextInHistory(Entity entity)
	{
		string text = base.BuildCardTextInHistory(entity);
		return this.GetCorrectSubstring(text, entity.GetTag(GAME_TAG.HIDDEN_CHOICE));
	}
}
