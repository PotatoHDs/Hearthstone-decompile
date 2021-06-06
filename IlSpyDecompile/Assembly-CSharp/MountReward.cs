using UnityEngine;

public class MountReward : Reward
{
	public GameObject m_mount;

	protected override void InitData()
	{
		SetData(new MountRewardData(), updateVisuals: false);
	}

	protected override void ShowReward(bool updateCacheValues)
	{
		if (!(base.Data is MountRewardData))
		{
			Debug.LogWarning($"MountReward.ShowReward() - Data {base.Data} is not MountRewardData");
			return;
		}
		m_root.SetActive(value: true);
		Vector3 localScale = m_mount.transform.localScale;
		m_mount.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		iTween.ScaleTo(m_mount.gameObject, iTween.Hash("scale", localScale, "time", 0.5f, "easetype", iTween.EaseType.easeOutElastic));
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
		MountRewardData mountRewardData = base.Data as MountRewardData;
		if (mountRewardData != null)
		{
			string headline = string.Empty;
			switch (mountRewardData.Mount)
			{
			case MountRewardData.MountType.WOW_HEARTHSTEED:
				headline = GameStrings.Get("GLOBAL_REWARD_HEARTHSTEED_HEADLINE");
				break;
			case MountRewardData.MountType.HEROES_MAGIC_CARPET_CARD:
				headline = GameStrings.Get("GLOBAL_REWARD_HEROES_CARD_MOUNT_HEADLINE");
				break;
			}
			SetRewardText(headline, string.Empty, string.Empty);
		}
	}
}
