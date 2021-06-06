using System;
using UnityEngine;

// Token: 0x0200065C RID: 1628
public class ArcaneDustReward : Reward
{
	// Token: 0x06005BD2 RID: 23506 RVA: 0x001DDDEE File Offset: 0x001DBFEE
	protected override void InitData()
	{
		base.SetData(new ArcaneDustRewardData(), false);
	}

	// Token: 0x06005BD3 RID: 23507 RVA: 0x001DDDFC File Offset: 0x001DBFFC
	protected override void ShowReward(bool updateCacheValues)
	{
		ArcaneDustRewardData arcaneDustRewardData = base.Data as ArcaneDustRewardData;
		if (arcaneDustRewardData == null)
		{
			Debug.LogWarning(string.Format("ArcaneDustReward.ShowReward() - Data {0} is not ArcaneDustRewardData", base.Data));
			return;
		}
		this.m_root.SetActive(true);
		this.m_dustCount.Text = arcaneDustRewardData.Amount.ToString();
		Vector3 localScale = this.m_dustJar.transform.localScale;
		this.m_dustJar.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		iTween.ScaleTo(this.m_dustJar.gameObject, iTween.Hash(new object[]
		{
			"scale",
			localScale,
			"time",
			0.5f,
			"easetype",
			iTween.EaseType.easeOutElastic
		}));
	}

	// Token: 0x06005BD4 RID: 23508 RVA: 0x001DDEDB File Offset: 0x001DC0DB
	protected override void HideReward()
	{
		base.HideReward();
		this.m_root.SetActive(false);
	}

	// Token: 0x06005BD5 RID: 23509 RVA: 0x001DDEF0 File Offset: 0x001DC0F0
	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		string headline = GameStrings.Get("GLOBAL_REWARD_ARCANE_DUST_HEADLINE");
		base.SetRewardText(headline, "", "");
	}

	// Token: 0x04004E4C RID: 20044
	public GameObject m_dustJar;

	// Token: 0x04004E4D RID: 20045
	public UberText m_dustCount;
}
