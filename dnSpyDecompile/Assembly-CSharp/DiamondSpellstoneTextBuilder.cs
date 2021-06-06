using System;

// Token: 0x02000790 RID: 1936
public class DiamondSpellstoneTextBuilder : CardTextBuilder
{
	// Token: 0x06006C64 RID: 27748 RVA: 0x00230F88 File Offset: 0x0022F188
	public override string BuildCardTextInHand(Entity entity)
	{
		string text = base.BuildCardTextInHand(entity);
		string newValue = GameStrings.Get("GAMEPLAY_DIAMOND_SPELLSTONE_" + (entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1) + 1).ToString());
		return text.Replace("@", newValue);
	}

	// Token: 0x06006C65 RID: 27749 RVA: 0x00230FC8 File Offset: 0x0022F1C8
	public override void OnTagChange(Card card, TagDelta tagChange)
	{
		if (tagChange.tag == 2 || tagChange.tag == 3)
		{
			card.GetActor().UpdateTextComponents();
		}
	}

	// Token: 0x06006C66 RID: 27750 RVA: 0x00230FE7 File Offset: 0x0022F1E7
	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		return base.BuildCardTextInHand(entityDef).Replace("@", GameStrings.Get("GAMEPLAY_DIAMOND_SPELLSTONE_1"));
	}

	// Token: 0x06006C67 RID: 27751 RVA: 0x00231004 File Offset: 0x0022F204
	public override string BuildCardTextInHistory(Entity entity)
	{
		CardTextHistoryData cardTextHistoryData = entity.GetCardTextHistoryData();
		if (cardTextHistoryData == null)
		{
			Log.All.Print("DiamondSpellstoneTextBuilder.BuildCardTextInHistory: entity {0} does not have a CardTextHistoryData object.", new object[]
			{
				entity.GetEntityId()
			});
			return "";
		}
		string text = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		text = text.Replace("@", GameStrings.Get("GAMEPLAY_DIAMOND_SPELLSTONE_1"));
		return TextUtils.TransformCardText(cardTextHistoryData.m_damageBonus, cardTextHistoryData.m_damageBonusDouble, cardTextHistoryData.m_healingDouble, text);
	}
}
