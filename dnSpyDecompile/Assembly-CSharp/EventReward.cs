using System;
using UnityEngine;

// Token: 0x0200066F RID: 1647
public class EventReward : Reward
{
	// Token: 0x06005C6E RID: 23662 RVA: 0x001E06D2 File Offset: 0x001DE8D2
	protected override void InitData()
	{
		base.SetData(new EventRewardData(), false);
	}

	// Token: 0x06005C6F RID: 23663 RVA: 0x001E06E0 File Offset: 0x001DE8E0
	protected override void ShowReward(bool updateCacheValues)
	{
		this.m_root.SetActive(true);
	}

	// Token: 0x06005C70 RID: 23664 RVA: 0x001DDEDB File Offset: 0x001DC0DB
	protected override void HideReward()
	{
		base.HideReward();
		this.m_root.SetActive(false);
	}

	// Token: 0x06005C71 RID: 23665 RVA: 0x001E06F0 File Offset: 0x001DE8F0
	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		EventRewardData eventRewardData = base.Data as EventRewardData;
		if (eventRewardData == null)
		{
			Debug.LogWarning(string.Format("EventRewardData.SetData() - data {0} is not EventRewardData", base.Data));
			return;
		}
		string headline = string.Empty;
		if (eventRewardData.EventType == 0)
		{
			headline = GameStrings.Get("GLUE_2X_GOLD_EVENT_BANNER_HEADLINE");
		}
		string empty = string.Empty;
		string empty2 = string.Empty;
		base.SetRewardText(headline, empty, empty2);
	}
}
