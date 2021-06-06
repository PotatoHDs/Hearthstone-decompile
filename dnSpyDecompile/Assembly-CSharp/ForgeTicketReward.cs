using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000671 RID: 1649
[Obsolete("use ArenaTicketReward")]
public class ForgeTicketReward : Reward
{
	// Token: 0x06005C79 RID: 23673 RVA: 0x001DDF9C File Offset: 0x001DC19C
	protected override void InitData()
	{
		base.SetData(new ForgeTicketRewardData(), false);
	}

	// Token: 0x06005C7A RID: 23674 RVA: 0x001E07B4 File Offset: 0x001DE9B4
	protected override void ShowReward(bool updateCacheValues)
	{
		string headline = string.Empty;
		string empty = string.Empty;
		string source = string.Empty;
		if (base.Data.Origin == NetCache.ProfileNotice.NoticeOrigin.OUT_OF_BAND_LICENSE)
		{
			ForgeTicketRewardData forgeTicketRewardData = base.Data as ForgeTicketRewardData;
			headline = GameStrings.Get("GLOBAL_REWARD_FORGE_HEADLINE");
			source = GameStrings.Format("GLOBAL_REWARD_BOOSTER_DETAILS_OUT_OF_BAND", new object[]
			{
				forgeTicketRewardData.Quantity
			});
		}
		else
		{
			headline = GameStrings.Get("GLOBAL_REWARD_FORGE_UNLOCKED_HEADLINE");
			source = GameStrings.Get("GLOBAL_REWARD_FORGE_UNLOCKED_SOURCE");
		}
		base.SetRewardText(headline, empty, source);
		this.m_root.SetActive(true);
		this.m_rotateParent.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			new Vector3(0f, 0f, 540f),
			"time",
			1.5f,
			"easeType",
			iTween.EaseType.easeOutElastic,
			"space",
			Space.Self
		});
		iTween.RotateAdd(this.m_rotateParent, args);
	}

	// Token: 0x06005C7B RID: 23675 RVA: 0x001DDEDB File Offset: 0x001DC0DB
	protected override void HideReward()
	{
		base.HideReward();
		this.m_root.SetActive(false);
	}

	// Token: 0x04004E98 RID: 20120
	public GameObject m_rotateParent;
}
