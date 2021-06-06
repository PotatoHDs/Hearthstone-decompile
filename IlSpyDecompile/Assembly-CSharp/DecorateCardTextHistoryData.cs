public class DecorateCardTextHistoryData : CardTextHistoryData
{
	public int m_decorationProgress;

	public int m_cost;

	public override void SetHistoryData(Entity entity, HistoryInfo historyInfo)
	{
		base.SetHistoryData(entity, historyInfo);
		m_cost = entity.GetTag(GAME_TAG.COST);
		m_decorationProgress = entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
	}
}
