using System;

// Token: 0x020007A5 RID: 1957
public class ScriptDataNum1Num2CardTextBuilder : CardTextBuilder
{
	// Token: 0x06006CC8 RID: 27848 RVA: 0x002309C3 File Offset: 0x0022EBC3
	public ScriptDataNum1Num2CardTextBuilder()
	{
		this.m_useEntityForTextInPlay = true;
	}

	// Token: 0x06006CC9 RID: 27849 RVA: 0x002328F0 File Offset: 0x00230AF0
	public override string BuildCardTextInHand(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		string arg = entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1).ToString();
		string arg2 = entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2).ToString();
		string powersText = string.Format(rawCardTextInHand, arg, arg2);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}

	// Token: 0x06006CCA RID: 27850 RVA: 0x00232948 File Offset: 0x00230B48
	public override string BuildCardTextInHistory(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		string arg = entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1).ToString();
		string arg2 = entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2).ToString();
		string powersText = string.Format(rawCardTextInHand, arg, arg2);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}

	// Token: 0x06006CCB RID: 27851 RVA: 0x002329A0 File Offset: 0x00230BA0
	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entityDef.GetCardId());
		string arg = entityDef.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1).ToString();
		string arg2 = entityDef.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2).ToString();
		return TextUtils.TransformCardText(string.Format(rawCardTextInHand, arg, arg2));
	}

	// Token: 0x06006CCC RID: 27852 RVA: 0x002312BE File Offset: 0x0022F4BE
	public override void OnTagChange(Card card, TagDelta tagChange)
	{
		if ((tagChange.tag == 2 || tagChange.tag == 3) && card != null && card.GetActor() != null)
		{
			card.GetActor().UpdateTextComponents();
		}
	}
}
