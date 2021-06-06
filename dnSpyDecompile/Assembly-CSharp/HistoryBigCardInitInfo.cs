using System;

// Token: 0x02000315 RID: 789
public class HistoryBigCardInitInfo : HistoryItemInitInfo
{
	// Token: 0x04001868 RID: 6248
	public HistoryInfoType m_historyInfoType;

	// Token: 0x04001869 RID: 6249
	public HistoryManager.BigCardFinishedCallback m_finishedCallback;

	// Token: 0x0400186A RID: 6250
	public bool m_countered;

	// Token: 0x0400186B RID: 6251
	public bool m_waitForSecretSpell;

	// Token: 0x0400186C RID: 6252
	public bool m_fromMetaData;

	// Token: 0x0400186D RID: 6253
	public Entity m_postTransformedEntity;

	// Token: 0x0400186E RID: 6254
	public int m_displayTimeMS;
}
