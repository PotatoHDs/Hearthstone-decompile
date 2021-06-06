using UnityEngine;

public class EventReward : Reward
{
	protected override void InitData()
	{
		SetData(new EventRewardData(), updateVisuals: false);
	}

	protected override void ShowReward(bool updateCacheValues)
	{
		m_root.SetActive(value: true);
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
		EventRewardData eventRewardData = base.Data as EventRewardData;
		if (eventRewardData == null)
		{
			Debug.LogWarning($"EventRewardData.SetData() - data {base.Data} is not EventRewardData");
			return;
		}
		string headline = string.Empty;
		if (eventRewardData.EventType == 0)
		{
			headline = GameStrings.Get("GLUE_2X_GOLD_EVENT_BANNER_HEADLINE");
		}
		string empty = string.Empty;
		string empty2 = string.Empty;
		SetRewardText(headline, empty, empty2);
	}
}
