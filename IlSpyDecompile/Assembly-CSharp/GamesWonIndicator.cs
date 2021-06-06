using System.Collections.Generic;
using UnityEngine;

public class GamesWonIndicator : MonoBehaviour
{
	public enum InnKeeperTrigger
	{
		NONE
	}

	public GameObject m_root;

	public GameObject m_segmentContainer;

	public UberText m_winCountText;

	public GamesWonIndicatorSegment m_gamesWonSegmentPrefab;

	private List<GamesWonIndicatorSegment> m_segments = new List<GamesWonIndicatorSegment>();

	private int m_numActiveSegments;

	private InnKeeperTrigger m_innkeeperTrigger;

	private const float FUDGE_FACTOR = 0.01f;

	public void Init(Reward.Type rewardType, int rewardAmount, int numSegments, int numActiveSegments, InnKeeperTrigger trigger)
	{
		m_innkeeperTrigger = trigger;
		m_numActiveSegments = numActiveSegments;
		Vector3 position = m_segmentContainer.transform.position;
		float num = 0f;
		float num2 = 0f;
		for (int i = 0; i < numSegments; i++)
		{
			GamesWonIndicatorSegment.Type type = ((i != 0) ? ((i != numSegments - 1) ? GamesWonIndicatorSegment.Type.MIDDLE : GamesWonIndicatorSegment.Type.RIGHT) : GamesWonIndicatorSegment.Type.LEFT);
			bool hideCrown = i >= numActiveSegments - 1;
			GamesWonIndicatorSegment gamesWonIndicatorSegment = Object.Instantiate(m_gamesWonSegmentPrefab);
			gamesWonIndicatorSegment.Init(type, rewardType, rewardAmount, hideCrown);
			gamesWonIndicatorSegment.transform.parent = m_segmentContainer.transform;
			gamesWonIndicatorSegment.transform.localScale = Vector3.one;
			float num3 = gamesWonIndicatorSegment.GetWidth() - 0.01f;
			if (type != GamesWonIndicatorSegment.Type.RIGHT)
			{
				position.x += num3;
			}
			else
			{
				position.x -= 0.01f;
			}
			gamesWonIndicatorSegment.transform.position = position;
			gamesWonIndicatorSegment.transform.rotation = Quaternion.identity;
			num = num3;
			num2 += num3;
			m_segments.Add(gamesWonIndicatorSegment);
		}
		Vector3 position2 = m_segmentContainer.transform.position;
		position2.x -= num2 / 2f;
		position2.x += num / 5f;
		m_segmentContainer.transform.position = position2;
		m_winCountText.Text = GameStrings.Format("GAMEPLAY_WIN_REWARD_PROGRESS", m_numActiveSegments, numSegments);
	}

	public void Show()
	{
		m_root.SetActive(value: true);
		if (m_numActiveSegments <= 0)
		{
			Debug.LogError($"GamesWonIndicator.Show(): cannot do animation; numActiveSegments={m_numActiveSegments} but should be greater than zero");
			return;
		}
		if (m_numActiveSegments > m_segments.Count)
		{
			Debug.LogError($"GamesWonIndicator.Show(): cannot do animation; numActiveSegments = {m_numActiveSegments} but m_segments.Count = {m_segments.Count}");
			return;
		}
		m_segments[m_numActiveSegments - 1].AnimateReward();
		InnKeeperTrigger innkeeperTrigger = m_innkeeperTrigger;
	}

	public void Hide()
	{
		m_root.SetActive(value: false);
	}
}
