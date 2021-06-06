public class HistoryBigCardInitInfo : HistoryItemInitInfo
{
	public HistoryInfoType m_historyInfoType;

	public HistoryManager.BigCardFinishedCallback m_finishedCallback;

	public bool m_countered;

	public bool m_waitForSecretSpell;

	public bool m_fromMetaData;

	public Entity m_postTransformedEntity;

	public int m_displayTimeMS;
}
