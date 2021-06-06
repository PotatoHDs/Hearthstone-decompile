using UnityEngine;

public class SimpleReward : Reward
{
	public QuestTileRewardIcon m_icon;

	protected override void InitData()
	{
		SetData(new SimpleRewardData(Type.NONE), updateVisuals: false);
	}

	protected override void ShowReward(bool updateCacheValues)
	{
		if (!(base.Data is SimpleRewardData))
		{
			Debug.LogWarning($"SimpleReward.ShowReward() - Data {base.Data} is not SimpleRewardData");
			return;
		}
		m_root.SetActive(value: true);
		Vector3 localScale = m_icon.transform.localScale;
		m_icon.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		iTween.ScaleTo(m_icon.gameObject, iTween.Hash("scale", localScale, "time", 0.5f, "easetype", iTween.EaseType.easeOutElastic));
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
		SimpleRewardData simpleRewardData = base.Data as SimpleRewardData;
		if (simpleRewardData != null && simpleRewardData.RewardType != Type.NONE)
		{
			SetRewardText(simpleRewardData.RewardHeadlineText, "", "");
			if (m_icon != null)
			{
				m_icon.InitWithRewardData(simpleRewardData, isDoubleGoldEnabled: false, 3000);
			}
		}
	}
}
