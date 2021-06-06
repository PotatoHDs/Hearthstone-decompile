using System;

// Token: 0x0200078F RID: 1935
public class DecorateCardTextBuilder : CardTextBuilder
{
	// Token: 0x06006C60 RID: 27744 RVA: 0x00230EA4 File Offset: 0x0022F0A4
	public override string BuildCardTextInHand(Entity entity)
	{
		string powersText = string.Format(CardTextBuilder.GetRawCardTextInHand(entity.GetCardId()), entity.GetTag(GAME_TAG.COST), entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2));
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}

	// Token: 0x06006C61 RID: 27745 RVA: 0x00230EF4 File Offset: 0x0022F0F4
	public override string BuildCardTextInHistory(Entity entity)
	{
		DecorateCardTextHistoryData decorateCardTextHistoryData = entity.GetCardTextHistoryData() as DecorateCardTextHistoryData;
		if (decorateCardTextHistoryData == null)
		{
			Log.All.Print("DecorateCardTextBuilder.BuildCardTextInHistory: entity {0} does not have a DecorateCardTextHistoryData object.", new object[]
			{
				entity.GetEntityId()
			});
			return string.Empty;
		}
		string powersText = string.Format(CardTextBuilder.GetRawCardTextInHand(entity.GetCardId()), decorateCardTextHistoryData.m_cost, decorateCardTextHistoryData.m_decorationProgress);
		return TextUtils.TransformCardText(decorateCardTextHistoryData.m_damageBonus, decorateCardTextHistoryData.m_damageBonusDouble, decorateCardTextHistoryData.m_healingDouble, powersText);
	}

	// Token: 0x06006C62 RID: 27746 RVA: 0x00230F78 File Offset: 0x0022F178
	public override CardTextHistoryData CreateCardTextHistoryData()
	{
		return new DecorateCardTextHistoryData();
	}
}
