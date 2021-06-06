using System;
using UnityEngine;

// Token: 0x02000676 RID: 1654
public class MountReward : Reward
{
	// Token: 0x06005C96 RID: 23702 RVA: 0x001E0CE8 File Offset: 0x001DEEE8
	protected override void InitData()
	{
		base.SetData(new MountRewardData(), false);
	}

	// Token: 0x06005C97 RID: 23703 RVA: 0x001E0CF8 File Offset: 0x001DEEF8
	protected override void ShowReward(bool updateCacheValues)
	{
		if (!(base.Data is MountRewardData))
		{
			Debug.LogWarning(string.Format("MountReward.ShowReward() - Data {0} is not MountRewardData", base.Data));
			return;
		}
		this.m_root.SetActive(true);
		Vector3 localScale = this.m_mount.transform.localScale;
		this.m_mount.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		iTween.ScaleTo(this.m_mount.gameObject, iTween.Hash(new object[]
		{
			"scale",
			localScale,
			"time",
			0.5f,
			"easetype",
			iTween.EaseType.easeOutElastic
		}));
	}

	// Token: 0x06005C98 RID: 23704 RVA: 0x001DDEDB File Offset: 0x001DC0DB
	protected override void HideReward()
	{
		base.HideReward();
		this.m_root.SetActive(false);
	}

	// Token: 0x06005C99 RID: 23705 RVA: 0x001E0DBC File Offset: 0x001DEFBC
	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		MountRewardData mountRewardData = base.Data as MountRewardData;
		if (mountRewardData == null)
		{
			return;
		}
		string headline = string.Empty;
		MountRewardData.MountType mount = mountRewardData.Mount;
		if (mount != MountRewardData.MountType.WOW_HEARTHSTEED)
		{
			if (mount == MountRewardData.MountType.HEROES_MAGIC_CARPET_CARD)
			{
				headline = GameStrings.Get("GLOBAL_REWARD_HEROES_CARD_MOUNT_HEADLINE");
			}
		}
		else
		{
			headline = GameStrings.Get("GLOBAL_REWARD_HEARTHSTEED_HEADLINE");
		}
		base.SetRewardText(headline, string.Empty, string.Empty);
	}

	// Token: 0x04004E9E RID: 20126
	public GameObject m_mount;
}
