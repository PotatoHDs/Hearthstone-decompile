using System.Collections;
using UnityEngine;

public class GoldReward : Reward
{
	public GameObject m_coin;

	public bool m_RotateIn = true;

	protected override void InitData()
	{
		SetData(new GoldRewardData(), updateVisuals: false);
	}

	protected override void ShowReward(bool updateCacheValues)
	{
		m_root.SetActive(value: true);
		Vector3 localScale = m_coin.transform.localScale;
		m_coin.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		iTween.ScaleTo(m_coin.gameObject, iTween.Hash("scale", localScale, "time", 0.5f, "easetype", iTween.EaseType.easeOutElastic));
		if (m_RotateIn)
		{
			m_coin.transform.localEulerAngles = new Vector3(0f, 180f, 180f);
			Hashtable args = iTween.Hash("amount", new Vector3(0f, 0f, 540f), "time", 1.5f, "easeType", iTween.EaseType.easeOutElastic, "space", Space.Self);
			iTween.RotateAdd(m_coin.gameObject, args);
		}
	}

	protected override void HideReward()
	{
		base.HideReward();
		m_root.SetActive(value: false);
	}

	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		GoldRewardData goldRewardData = base.Data as GoldRewardData;
		if (goldRewardData == null)
		{
			Debug.LogWarning($"goldRewardData.SetData() - data {base.Data} is not GoldRewardData");
			return;
		}
		string headline = GameStrings.Get("GLOBAL_REWARD_GOLD_HEADLINE");
		string details = goldRewardData.Amount.ToString();
		string source = string.Empty;
		UberText uberText = m_coin.GetComponentsInChildren<UberText>(includeInactive: true)[0];
		if (uberText != null)
		{
			m_rewardBanner.m_detailsText = uberText;
			m_rewardBanner.AlignHeadlineToCenterBone();
		}
		switch (base.Data.Origin)
		{
		case NetCache.ProfileNotice.NoticeOrigin.TOURNEY:
		{
			NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
			if (netObject != null)
			{
				source = GameStrings.Format("GLOBAL_REWARD_GOLD_SOURCE_TOURNEY", netObject.WinsPerGold);
			}
			break;
		}
		case NetCache.ProfileNotice.NoticeOrigin.BETA_REIMBURSE:
			headline = GameStrings.Get("GLOBAL_BETA_REIMBURSEMENT_HEADLINE");
			source = GameStrings.Get("GLOBAL_BETA_REIMBURSEMENT_DETAILS");
			break;
		case NetCache.ProfileNotice.NoticeOrigin.IGR:
			if (goldRewardData.Date.HasValue)
			{
				string text = GameStrings.Format("GLOBAL_CURRENT_DATE", goldRewardData.Date);
				source = GameStrings.Format("GLOBAL_REWARD_GOLD_SOURCE_IGR_DATED", text);
			}
			else
			{
				source = GameStrings.Get("GLOBAL_REWARD_GOLD_SOURCE_IGR");
			}
			break;
		}
		SetRewardText(headline, details, source);
	}
}
