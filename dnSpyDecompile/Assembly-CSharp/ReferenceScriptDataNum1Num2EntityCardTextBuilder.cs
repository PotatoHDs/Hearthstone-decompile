using System;

// Token: 0x020007A2 RID: 1954
public class ReferenceScriptDataNum1Num2EntityCardTextBuilder : CardTextBuilder
{
	// Token: 0x06006CB8 RID: 27832 RVA: 0x002309C3 File Offset: 0x0022EBC3
	public ReferenceScriptDataNum1Num2EntityCardTextBuilder()
	{
		this.m_useEntityForTextInPlay = true;
	}

	// Token: 0x06006CB9 RID: 27833 RVA: 0x00232558 File Offset: 0x00230758
	public override string BuildCardTextInHand(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		Entity entity2 = GameState.Get().GetEntity(entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1));
		Entity entity3 = GameState.Get().GetEntity(entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2));
		string arg = (entity2 != null && entity2.HasValidDisplayName()) ? entity2.GetName() : GameStrings.Get("GAMEPLAY_UNKNOWN_CREATED_BY");
		string arg2 = (entity3 != null && entity3.HasValidDisplayName()) ? entity3.GetName() : GameStrings.Get("GAMEPLAY_UNKNOWN_CREATED_BY");
		string powersText = string.Format(rawCardTextInHand, arg, arg2);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}

	// Token: 0x06006CBA RID: 27834 RVA: 0x002325F4 File Offset: 0x002307F4
	public override string BuildCardTextInHistory(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		Entity entity2 = GameState.Get().GetEntity(entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1));
		Entity entity3 = GameState.Get().GetEntity(entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2));
		string arg = (entity2 != null && entity2.HasValidDisplayName()) ? entity2.GetName() : GameStrings.Get("GAMEPLAY_UNKNOWN_CREATED_BY");
		string arg2 = (entity3 != null && entity3.HasValidDisplayName()) ? entity3.GetName() : GameStrings.Get("GAMEPLAY_UNKNOWN_CREATED_BY");
		string powersText = string.Format(rawCardTextInHand, arg, arg2);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}
}
