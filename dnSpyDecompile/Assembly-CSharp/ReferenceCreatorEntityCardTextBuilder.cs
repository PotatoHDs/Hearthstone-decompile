using System;

// Token: 0x020007A0 RID: 1952
public class ReferenceCreatorEntityCardTextBuilder : CardTextBuilder
{
	// Token: 0x06006CB2 RID: 27826 RVA: 0x002309C3 File Offset: 0x0022EBC3
	public ReferenceCreatorEntityCardTextBuilder()
	{
		this.m_useEntityForTextInPlay = true;
	}

	// Token: 0x06006CB3 RID: 27827 RVA: 0x0023247C File Offset: 0x0023067C
	public override string BuildCardTextInHand(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		Entity entity2 = GameState.Get().GetEntity(entity.GetTag(GAME_TAG.CREATOR));
		string arg = (entity2 != null && entity2.HasValidDisplayName()) ? entity2.GetName() : GameStrings.Get("GAMEPLAY_UNKNOWN_CREATED_BY");
		string powersText = string.Format(rawCardTextInHand, arg);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}
}
