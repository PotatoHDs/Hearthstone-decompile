using UnityEngine;

public class GamesWonIndicatorSegment : MonoBehaviour
{
	public enum Type
	{
		LEFT,
		MIDDLE,
		RIGHT
	}

	public GamesWonSegment m_leftSegment;

	public MiddleGamesWonSegment m_middleSegment;

	public RightGamesWonSegment m_rightSegment;

	private GamesWonSegment m_activeSegment;

	public void Init(Type segmentType, Reward.Type rewardType, int rewardAmount, bool hideCrown)
	{
		switch (segmentType)
		{
		case Type.LEFT:
			m_activeSegment = m_leftSegment;
			m_middleSegment.Hide();
			m_rightSegment.Hide();
			break;
		case Type.MIDDLE:
			m_activeSegment = m_middleSegment;
			m_leftSegment.Hide();
			m_rightSegment.Hide();
			break;
		case Type.RIGHT:
			m_activeSegment = m_rightSegment;
			m_leftSegment.Hide();
			m_middleSegment.Hide();
			break;
		}
		m_activeSegment.Init(rewardType, rewardAmount, hideCrown);
	}

	public float GetWidth()
	{
		return m_activeSegment.GetWidth();
	}

	public void AnimateReward()
	{
		m_activeSegment.AnimateReward();
	}
}
