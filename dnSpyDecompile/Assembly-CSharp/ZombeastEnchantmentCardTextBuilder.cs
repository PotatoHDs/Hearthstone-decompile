using System;

// Token: 0x020007A9 RID: 1961
public class ZombeastEnchantmentCardTextBuilder : CardTextBuilder
{
	// Token: 0x06006CD4 RID: 27860 RVA: 0x002309C3 File Offset: 0x0022EBC3
	public ZombeastEnchantmentCardTextBuilder()
	{
		this.m_useEntityForTextInPlay = true;
	}

	// Token: 0x06006CD5 RID: 27861 RVA: 0x00232BD0 File Offset: 0x00230DD0
	public override string BuildCardTextInHand(Entity entity)
	{
		if (entity.HasTag(GAME_TAG.MODULAR_ENTITY_PART_1) && entity.HasTag(GAME_TAG.MODULAR_ENTITY_PART_2))
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(entity.GetTag(GAME_TAG.MODULAR_ENTITY_PART_1), true);
			EntityDef entityDef2 = DefLoader.Get().GetEntityDef(entity.GetTag(GAME_TAG.MODULAR_ENTITY_PART_2), true);
			if (entityDef != null && entityDef2 != null)
			{
				string text = string.Format(CardTextBuilder.GetRawCardTextInHand(entity.GetCardId()), entityDef.GetName(), entityDef2.GetName());
				return TextUtils.TransformCardText(entity, text);
			}
		}
		return string.Empty;
	}
}
