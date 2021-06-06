using System;

// Token: 0x02000797 RID: 1943
public class JadeGolemCardTextHistoryData : CardTextHistoryData
{
	// Token: 0x06006C86 RID: 27782 RVA: 0x00231AF3 File Offset: 0x0022FCF3
	public override void SetHistoryData(Entity entity, HistoryInfo historyInfo)
	{
		base.SetHistoryData(entity, historyInfo);
		this.m_jadeGolem = entity.GetJadeGolem();
		if (entity.GetZone() == TAG_ZONE.PLAY && historyInfo != null && historyInfo.m_infoType != HistoryInfoType.CARD_PLAYED && historyInfo.m_infoType != HistoryInfoType.WEAPON_PLAYED)
		{
			this.m_wasInPlay = true;
		}
	}

	// Token: 0x040057BE RID: 22462
	public int m_jadeGolem;

	// Token: 0x040057BF RID: 22463
	public bool m_wasInPlay;
}
