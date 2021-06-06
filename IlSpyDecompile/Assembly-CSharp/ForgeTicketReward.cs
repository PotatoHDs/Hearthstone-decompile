using System;
using System.Collections;
using UnityEngine;

[Obsolete("use ArenaTicketReward")]
public class ForgeTicketReward : Reward
{
	public GameObject m_rotateParent;

	protected override void InitData()
	{
		SetData(new ForgeTicketRewardData(), updateVisuals: false);
	}

	protected override void ShowReward(bool updateCacheValues)
	{
		string empty = string.Empty;
		string empty2 = string.Empty;
		string empty3 = string.Empty;
		if (base.Data.Origin == NetCache.ProfileNotice.NoticeOrigin.OUT_OF_BAND_LICENSE)
		{
			ForgeTicketRewardData forgeTicketRewardData = base.Data as ForgeTicketRewardData;
			empty = GameStrings.Get("GLOBAL_REWARD_FORGE_HEADLINE");
			empty3 = GameStrings.Format("GLOBAL_REWARD_BOOSTER_DETAILS_OUT_OF_BAND", forgeTicketRewardData.Quantity);
		}
		else
		{
			empty = GameStrings.Get("GLOBAL_REWARD_FORGE_UNLOCKED_HEADLINE");
			empty3 = GameStrings.Get("GLOBAL_REWARD_FORGE_UNLOCKED_SOURCE");
		}
		SetRewardText(empty, empty2, empty3);
		m_root.SetActive(value: true);
		m_rotateParent.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
		Hashtable args = iTween.Hash("amount", new Vector3(0f, 0f, 540f), "time", 1.5f, "easeType", iTween.EaseType.easeOutElastic, "space", Space.Self);
		iTween.RotateAdd(m_rotateParent, args);
	}

	protected override void HideReward()
	{
		base.HideReward();
		m_root.SetActive(value: false);
	}
}
