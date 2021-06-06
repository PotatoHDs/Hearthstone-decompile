using System;

// Token: 0x0200078A RID: 1930
public class AlternateCardTextCardTextBuilder : CardTextBuilder
{
	// Token: 0x06006C42 RID: 27714 RVA: 0x002309C3 File Offset: 0x0022EBC3
	public AlternateCardTextCardTextBuilder()
	{
		this.m_useEntityForTextInPlay = true;
	}

	// Token: 0x06006C43 RID: 27715 RVA: 0x002309D4 File Offset: 0x0022EBD4
	public override string BuildCardTextInHand(Entity entity)
	{
		string builtText = base.BuildCardTextInHand(entity);
		return this.GetAlternateCardText(builtText, entity.GetTag(GAME_TAG.USE_ALTERNATE_CARD_TEXT));
	}

	// Token: 0x06006C44 RID: 27716 RVA: 0x002309FC File Offset: 0x0022EBFC
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

	// Token: 0x06006C45 RID: 27717 RVA: 0x00230A4C File Offset: 0x0022EC4C
	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string builtText = base.BuildCardTextInHand(entityDef);
		return this.GetAlternateCardText(builtText, entityDef.GetTag(GAME_TAG.USE_ALTERNATE_CARD_TEXT));
	}

	// Token: 0x06006C46 RID: 27718 RVA: 0x00230A74 File Offset: 0x0022EC74
	public override string BuildCardTextInHistory(Entity entity)
	{
		string builtText = base.BuildCardTextInHand(entity);
		return this.GetAlternateCardText(builtText, entity.GetTag(GAME_TAG.USE_ALTERNATE_CARD_TEXT));
	}

	// Token: 0x06006C47 RID: 27719 RVA: 0x00230A9C File Offset: 0x0022EC9C
	public override void OnTagChange(Card card, TagDelta tagChange)
	{
		GAME_TAG tag = (GAME_TAG)tagChange.tag;
		if (tag == GAME_TAG.USE_ALTERNATE_CARD_TEXT && card != null && card.GetActor() != null)
		{
			card.GetActor().UpdatePowersText();
		}
	}
}
