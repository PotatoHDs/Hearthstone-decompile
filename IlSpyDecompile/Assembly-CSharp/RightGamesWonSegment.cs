using System;
using UnityEngine;

[Serializable]
public class RightGamesWonSegment : GamesWonSegment
{
	public GameObject m_boosterRoot;

	public GameObject m_dustRoot;

	public GameObject m_goldRoot;

	public UberText m_dustAmountText;

	public UberText m_goldAmountText;

	private Reward.Type m_rewardType;

	public override void Init(Reward.Type rewardType, int rewardAmount, bool hideCrown)
	{
		base.Init(rewardType, rewardAmount, hideCrown);
		m_rewardType = rewardType;
		switch (m_rewardType)
		{
		case Reward.Type.ARCANE_DUST:
			m_boosterRoot.SetActive(value: false);
			m_goldRoot.SetActive(value: false);
			m_dustRoot.SetActive(value: true);
			m_dustAmountText.Text = rewardAmount.ToString();
			break;
		case Reward.Type.BOOSTER_PACK:
			m_dustRoot.SetActive(value: false);
			m_goldRoot.SetActive(value: false);
			m_boosterRoot.SetActive(value: true);
			break;
		case Reward.Type.GOLD:
			m_boosterRoot.SetActive(value: false);
			m_dustRoot.SetActive(value: false);
			m_goldRoot.SetActive(value: true);
			m_goldAmountText.Text = rewardAmount.ToString();
			break;
		default:
			Debug.LogError($"GamesWonIndicatorSegment(): don't know how to init right segment with reward type {m_rewardType}");
			m_boosterRoot.SetActive(value: false);
			m_dustRoot.SetActive(value: false);
			m_goldRoot.SetActive(value: false);
			break;
		}
	}

	public override void AnimateReward()
	{
		m_crown.Animate();
		PlayMakerFSM playMakerFSM = null;
		switch (m_rewardType)
		{
		case Reward.Type.ARCANE_DUST:
			playMakerFSM = m_dustRoot.GetComponent<PlayMakerFSM>();
			break;
		case Reward.Type.BOOSTER_PACK:
			playMakerFSM = m_boosterRoot.GetComponent<PlayMakerFSM>();
			break;
		case Reward.Type.GOLD:
			playMakerFSM = m_goldRoot.GetComponent<PlayMakerFSM>();
			break;
		}
		if (playMakerFSM == null)
		{
			Debug.LogError($"GamesWonIndicatorSegment(): missing playMaker component for reward type {m_rewardType}");
		}
		else
		{
			playMakerFSM.SendEvent("Birth");
		}
	}

	public override float GetWidth()
	{
		return m_rewardType switch
		{
			Reward.Type.ARCANE_DUST => m_dustRoot.GetComponent<Renderer>().bounds.size.x, 
			Reward.Type.BOOSTER_PACK => m_boosterRoot.GetComponent<Renderer>().bounds.size.x, 
			Reward.Type.GOLD => m_goldRoot.GetComponent<Renderer>().bounds.size.x, 
			_ => 0f, 
		};
	}
}
