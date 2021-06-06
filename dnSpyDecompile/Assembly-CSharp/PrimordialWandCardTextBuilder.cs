using System;

// Token: 0x0200079F RID: 1951
public class PrimordialWandCardTextBuilder : CardTextBuilder
{
	// Token: 0x06006CAD RID: 27821 RVA: 0x002309C3 File Offset: 0x0022EBC3
	public PrimordialWandCardTextBuilder()
	{
		this.m_useEntityForTextInPlay = true;
	}

	// Token: 0x06006CAE RID: 27822 RVA: 0x002323F8 File Offset: 0x002305F8
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

	// Token: 0x06006CAF RID: 27823 RVA: 0x00232446 File Offset: 0x00230646
	public override string BuildCardTextInHand(Entity entity)
	{
		return this.BuildText(entity);
	}

	// Token: 0x06006CB0 RID: 27824 RVA: 0x00232450 File Offset: 0x00230650
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

	// Token: 0x06006CB1 RID: 27825 RVA: 0x00232446 File Offset: 0x00230646
	public override string BuildCardTextInHistory(Entity entity)
	{
		return this.BuildText(entity);
	}
}
