using System;
using UnityEngine;

[Serializable]
public class GamesWonSegment
{
	public GameObject m_root;

	public GamesWonCrown m_crown;

	public virtual void Init(Reward.Type rewardType, int rewardAmount, bool hideCrown)
	{
		if (hideCrown)
		{
			m_crown.Hide();
		}
		else
		{
			m_crown.Show();
		}
		m_root.SetActive(value: true);
	}

	public virtual void AnimateReward()
	{
		m_crown.Animate();
	}

	public virtual float GetWidth()
	{
		return m_root.GetComponent<Renderer>().bounds.size.x;
	}

	public virtual void Hide()
	{
		m_root.SetActive(value: false);
	}
}
