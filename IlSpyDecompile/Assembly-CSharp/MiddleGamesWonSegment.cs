using System;
using UnityEngine;

[Serializable]
public class MiddleGamesWonSegment : GamesWonSegment
{
	public GameObject m_root1;

	public GameObject m_root2;

	private GameObject m_activeRoot;

	public override void Init(Reward.Type rewardType, int rewardAmount, bool hideCrown)
	{
		base.Init(rewardType, rewardAmount, hideCrown);
		if (UnityEngine.Random.value < 0.5f)
		{
			m_activeRoot = m_root1;
			m_root2.SetActive(value: false);
		}
		else
		{
			m_activeRoot = m_root2;
			m_root1.SetActive(value: false);
		}
		m_activeRoot.SetActive(value: true);
	}

	public override float GetWidth()
	{
		return m_activeRoot.GetComponent<Renderer>().bounds.size.x;
	}
}
