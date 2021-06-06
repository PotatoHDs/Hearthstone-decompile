using System;

// Token: 0x02000674 RID: 1652
public class MiniSetReward : Reward
{
	// Token: 0x06005C8D RID: 23693 RVA: 0x001DEACC File Offset: 0x001DCCCC
	protected override void InitData()
	{
		base.SetData(new CardBackRewardData(), false);
	}

	// Token: 0x06005C8E RID: 23694 RVA: 0x001E06E0 File Offset: 0x001DE8E0
	protected override void ShowReward(bool updateCacheValues)
	{
		this.m_root.SetActive(true);
	}

	// Token: 0x06005C8F RID: 23695 RVA: 0x001DDEDB File Offset: 0x001DC0DB
	protected override void HideReward()
	{
		base.HideReward();
		this.m_root.SetActive(false);
	}
}
