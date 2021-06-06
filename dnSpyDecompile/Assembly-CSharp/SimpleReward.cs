using System;
using UnityEngine;

// Token: 0x02000681 RID: 1665
public class SimpleReward : Reward
{
	// Token: 0x06005D42 RID: 23874 RVA: 0x001E4D71 File Offset: 0x001E2F71
	protected override void InitData()
	{
		base.SetData(new SimpleRewardData(Reward.Type.NONE), false);
	}

	// Token: 0x06005D43 RID: 23875 RVA: 0x001E4D80 File Offset: 0x001E2F80
	protected override void ShowReward(bool updateCacheValues)
	{
		if (!(base.Data is SimpleRewardData))
		{
			Debug.LogWarning(string.Format("SimpleReward.ShowReward() - Data {0} is not SimpleRewardData", base.Data));
			return;
		}
		this.m_root.SetActive(true);
		Vector3 localScale = this.m_icon.transform.localScale;
		this.m_icon.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		iTween.ScaleTo(this.m_icon.gameObject, iTween.Hash(new object[]
		{
			"scale",
			localScale,
			"time",
			0.5f,
			"easetype",
			iTween.EaseType.easeOutElastic
		}));
	}

	// Token: 0x06005D44 RID: 23876 RVA: 0x001DDEDB File Offset: 0x001DC0DB
	protected override void HideReward()
	{
		base.HideReward();
		this.m_root.SetActive(false);
	}

	// Token: 0x06005D45 RID: 23877 RVA: 0x001E4E44 File Offset: 0x001E3044
	protected override void OnDataSet(bool updateVisuals)
	{
		if (updateVisuals)
		{
			SimpleRewardData simpleRewardData = base.Data as SimpleRewardData;
			if (simpleRewardData != null && simpleRewardData.RewardType != Reward.Type.NONE)
			{
				base.SetRewardText(simpleRewardData.RewardHeadlineText, "", "");
				if (this.m_icon != null)
				{
					this.m_icon.InitWithRewardData(simpleRewardData, false, 3000);
				}
			}
		}
	}

	// Token: 0x04004EDF RID: 20191
	public QuestTileRewardIcon m_icon;
}
