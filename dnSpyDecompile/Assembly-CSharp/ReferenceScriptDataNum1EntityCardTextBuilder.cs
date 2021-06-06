using System;

// Token: 0x020007A1 RID: 1953
public class ReferenceScriptDataNum1EntityCardTextBuilder : CardTextBuilder
{
	// Token: 0x06006CB4 RID: 27828 RVA: 0x002309C3 File Offset: 0x0022EBC3
	public ReferenceScriptDataNum1EntityCardTextBuilder()
	{
		this.m_useEntityForTextInPlay = true;
	}

	// Token: 0x06006CB5 RID: 27829 RVA: 0x002324E7 File Offset: 0x002306E7
	public override string BuildCardTextInHand(Entity entity)
	{
		return ReferenceScriptDataNum1EntityCardTextBuilder.BuildText(entity);
	}

	// Token: 0x06006CB6 RID: 27830 RVA: 0x002324E7 File Offset: 0x002306E7
	public override string BuildCardTextInHistory(Entity entity)
	{
		return ReferenceScriptDataNum1EntityCardTextBuilder.BuildText(entity);
	}

	// Token: 0x06006CB7 RID: 27831 RVA: 0x002324F0 File Offset: 0x002306F0
	private static string BuildText(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		Entity entity2 = GameState.Get().GetEntity(entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1));
		string arg = (entity2 != null && entity2.HasValidDisplayName()) ? entity2.GetName() : GameStrings.Get("GAMEPLAY_UNKNOWN_CREATED_BY");
		string powersText = string.Format(rawCardTextInHand, arg);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}
}
