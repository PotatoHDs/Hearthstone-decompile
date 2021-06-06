using System;

// Token: 0x0200078E RID: 1934
public class DecorateCardTextHistoryData : CardTextHistoryData
{
	// Token: 0x06006C5E RID: 27742 RVA: 0x00230E77 File Offset: 0x0022F077
	public override void SetHistoryData(Entity entity, HistoryInfo historyInfo)
	{
		base.SetHistoryData(entity, historyInfo);
		this.m_cost = entity.GetTag(GAME_TAG.COST);
		this.m_decorationProgress = entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
	}

	// Token: 0x040057AB RID: 22443
	public int m_decorationProgress;

	// Token: 0x040057AC RID: 22444
	public int m_cost;
}
