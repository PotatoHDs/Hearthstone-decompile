using System;

// Token: 0x02000792 RID: 1938
public class GalakrondCounterCardTextBuilder : CardTextBuilder
{
	// Token: 0x06006C6B RID: 27755 RVA: 0x002309C3 File Offset: 0x0022EBC3
	public GalakrondCounterCardTextBuilder()
	{
		this.m_useEntityForTextInPlay = true;
	}

	// Token: 0x06006C6C RID: 27756 RVA: 0x002310F8 File Offset: 0x0022F2F8
	public override string BuildCardTextInHand(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		string newValue = (entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2) - entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1) == 1) ? GameStrings.Get("GALAKROND_ONCE") : GameStrings.Get("GALAKROND_TWICE");
		string powersText = rawCardTextInHand.Replace("@", newValue);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}

	// Token: 0x06006C6D RID: 27757 RVA: 0x00231160 File Offset: 0x0022F360
	public override string BuildCardTextInHistory(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		string newValue = (entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2) - entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1) == 1) ? GameStrings.Get("GALAKROND_ONCE") : GameStrings.Get("GALAKROND_TWICE");
		string powersText = rawCardTextInHand.Replace("@", newValue);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}

	// Token: 0x06006C6E RID: 27758 RVA: 0x002311C8 File Offset: 0x0022F3C8
	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entityDef.GetCardId());
		string newValue = (entityDef.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2) - entityDef.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1) == 1) ? GameStrings.Get("GALAKROND_ONCE") : GameStrings.Get("GALAKROND_TWICE");
		return TextUtils.TransformCardText(rawCardTextInHand.Replace("@", newValue));
	}

	// Token: 0x06006C6F RID: 27759 RVA: 0x00231219 File Offset: 0x0022F419
	public override void OnTagChange(Card card, TagDelta tagChange)
	{
		if (tagChange.tag == 2 && card != null && card.GetActor() != null)
		{
			card.GetActor().UpdateTextComponents();
		}
	}
}
