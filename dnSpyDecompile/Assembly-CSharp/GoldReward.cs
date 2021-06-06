using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000672 RID: 1650
public class GoldReward : Reward
{
	// Token: 0x06005C7D RID: 23677 RVA: 0x001E08DF File Offset: 0x001DEADF
	protected override void InitData()
	{
		base.SetData(new GoldRewardData(), false);
	}

	// Token: 0x06005C7E RID: 23678 RVA: 0x001E08F0 File Offset: 0x001DEAF0
	protected override void ShowReward(bool updateCacheValues)
	{
		this.m_root.SetActive(true);
		Vector3 localScale = this.m_coin.transform.localScale;
		this.m_coin.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		iTween.ScaleTo(this.m_coin.gameObject, iTween.Hash(new object[]
		{
			"scale",
			localScale,
			"time",
			0.5f,
			"easetype",
			iTween.EaseType.easeOutElastic
		}));
		if (this.m_RotateIn)
		{
			this.m_coin.transform.localEulerAngles = new Vector3(0f, 180f, 180f);
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
			iTween.RotateAdd(this.m_coin.gameObject, args);
		}
	}

	// Token: 0x06005C7F RID: 23679 RVA: 0x001DDEDB File Offset: 0x001DC0DB
	protected override void HideReward()
	{
		base.HideReward();
		this.m_root.SetActive(false);
	}

	// Token: 0x06005C80 RID: 23680 RVA: 0x001E0A3C File Offset: 0x001DEC3C
	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		GoldRewardData goldRewardData = base.Data as GoldRewardData;
		if (goldRewardData == null)
		{
			Debug.LogWarning(string.Format("goldRewardData.SetData() - data {0} is not GoldRewardData", base.Data));
			return;
		}
		string headline = GameStrings.Get("GLOBAL_REWARD_GOLD_HEADLINE");
		string details = goldRewardData.Amount.ToString();
		string source = string.Empty;
		UberText uberText = this.m_coin.GetComponentsInChildren<UberText>(true)[0];
		if (uberText != null)
		{
			this.m_rewardBanner.m_detailsText = uberText;
			this.m_rewardBanner.AlignHeadlineToCenterBone();
		}
		NetCache.ProfileNotice.NoticeOrigin origin = base.Data.Origin;
		if (origin != NetCache.ProfileNotice.NoticeOrigin.BETA_REIMBURSE)
		{
			if (origin != NetCache.ProfileNotice.NoticeOrigin.TOURNEY)
			{
				if (origin == NetCache.ProfileNotice.NoticeOrigin.IGR)
				{
					if (goldRewardData.Date != null)
					{
						string text = GameStrings.Format("GLOBAL_CURRENT_DATE", new object[]
						{
							goldRewardData.Date
						});
						source = GameStrings.Format("GLOBAL_REWARD_GOLD_SOURCE_IGR_DATED", new object[]
						{
							text
						});
					}
					else
					{
						source = GameStrings.Get("GLOBAL_REWARD_GOLD_SOURCE_IGR");
					}
				}
			}
			else
			{
				NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
				if (netObject != null)
				{
					source = GameStrings.Format("GLOBAL_REWARD_GOLD_SOURCE_TOURNEY", new object[]
					{
						netObject.WinsPerGold
					});
				}
			}
		}
		else
		{
			headline = GameStrings.Get("GLOBAL_BETA_REIMBURSEMENT_HEADLINE");
			source = GameStrings.Get("GLOBAL_BETA_REIMBURSEMENT_DETAILS");
		}
		base.SetRewardText(headline, details, source);
	}

	// Token: 0x04004E99 RID: 20121
	public GameObject m_coin;

	// Token: 0x04004E9A RID: 20122
	public bool m_RotateIn = true;
}
