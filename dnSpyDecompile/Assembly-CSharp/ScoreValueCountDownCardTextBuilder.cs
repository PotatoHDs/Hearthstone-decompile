using System;

// Token: 0x020007A3 RID: 1955
public class ScoreValueCountDownCardTextBuilder : CardTextBuilder
{
	// Token: 0x06006CBB RID: 27835 RVA: 0x002309C3 File Offset: 0x0022EBC3
	public ScoreValueCountDownCardTextBuilder()
	{
		this.m_useEntityForTextInPlay = true;
	}

	// Token: 0x06006CBC RID: 27836 RVA: 0x00232690 File Offset: 0x00230890
	public override string BuildCardTextInHand(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		string newValue = this.GetProgressRemaining(entity).ToString();
		string powersText = rawCardTextInHand.Replace("@", newValue);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}

	// Token: 0x06006CBD RID: 27837 RVA: 0x002326DC File Offset: 0x002308DC
	public override string BuildCardTextInHistory(Entity entity)
	{
		return this.BuildCardTextInHand(entity);
	}

	// Token: 0x06006CBE RID: 27838 RVA: 0x002326E8 File Offset: 0x002308E8
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

	// Token: 0x06006CBF RID: 27839 RVA: 0x00232718 File Offset: 0x00230918
	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entityDef.GetCardId());
		string newValue = entityDef.GetTag(GAME_TAG.SCORE_VALUE_1).ToString();
		return TextUtils.TransformCardText(rawCardTextInHand.Replace("@", newValue));
	}

	// Token: 0x06006CC0 RID: 27840 RVA: 0x00232754 File Offset: 0x00230954
	public override void OnTagChange(Card card, TagDelta tagChange)
	{
		if (card == null)
		{
			return;
		}
		Actor actor = card.GetActor();
		if (actor == null)
		{
			return;
		}
		GAME_TAG tag = (GAME_TAG)tagChange.tag;
		if (tag == GAME_TAG.SCORE_VALUE_1 || tag == GAME_TAG.SCORE_VALUE_2)
		{
			actor.UpdateTextComponents();
		}
	}
}
