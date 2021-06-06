public class JadeGolemCardTextHistoryData : CardTextHistoryData
{
	public int m_jadeGolem;

	public bool m_wasInPlay;

	public override void SetHistoryData(Entity entity, HistoryInfo historyInfo)
	{
		base.SetHistoryData(entity, historyInfo);
		m_jadeGolem = entity.GetJadeGolem();
		if (entity.GetZone() == TAG_ZONE.PLAY && historyInfo != null && historyInfo.m_infoType != HistoryInfoType.CARD_PLAYED && historyInfo.m_infoType != HistoryInfoType.WEAPON_PLAYED)
		{
			m_wasInPlay = true;
		}
	}
}
