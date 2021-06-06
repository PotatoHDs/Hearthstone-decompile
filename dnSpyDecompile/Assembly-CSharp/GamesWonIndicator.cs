using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002C9 RID: 713
public class GamesWonIndicator : MonoBehaviour
{
	// Token: 0x060025BA RID: 9658 RVA: 0x000BDCE8 File Offset: 0x000BBEE8
	public void Init(Reward.Type rewardType, int rewardAmount, int numSegments, int numActiveSegments, GamesWonIndicator.InnKeeperTrigger trigger)
	{
		this.m_innkeeperTrigger = trigger;
		this.m_numActiveSegments = numActiveSegments;
		Vector3 position = this.m_segmentContainer.transform.position;
		float num = 0f;
		float num2 = 0f;
		for (int i = 0; i < numSegments; i++)
		{
			GamesWonIndicatorSegment.Type type;
			if (i == 0)
			{
				type = GamesWonIndicatorSegment.Type.LEFT;
			}
			else if (i == numSegments - 1)
			{
				type = GamesWonIndicatorSegment.Type.RIGHT;
			}
			else
			{
				type = GamesWonIndicatorSegment.Type.MIDDLE;
			}
			bool hideCrown = i >= numActiveSegments - 1;
			GamesWonIndicatorSegment gamesWonIndicatorSegment = UnityEngine.Object.Instantiate<GamesWonIndicatorSegment>(this.m_gamesWonSegmentPrefab);
			gamesWonIndicatorSegment.Init(type, rewardType, rewardAmount, hideCrown);
			gamesWonIndicatorSegment.transform.parent = this.m_segmentContainer.transform;
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
			this.m_segments.Add(gamesWonIndicatorSegment);
		}
		Vector3 position2 = this.m_segmentContainer.transform.position;
		position2.x -= num2 / 2f;
		position2.x += num / 5f;
		this.m_segmentContainer.transform.position = position2;
		this.m_winCountText.Text = GameStrings.Format("GAMEPLAY_WIN_REWARD_PROGRESS", new object[]
		{
			this.m_numActiveSegments,
			numSegments
		});
	}

	// Token: 0x060025BB RID: 9659 RVA: 0x000BDE80 File Offset: 0x000BC080
	public void Show()
	{
		this.m_root.SetActive(true);
		if (this.m_numActiveSegments <= 0)
		{
			Debug.LogError(string.Format("GamesWonIndicator.Show(): cannot do animation; numActiveSegments={0} but should be greater than zero", this.m_numActiveSegments));
			return;
		}
		if (this.m_numActiveSegments > this.m_segments.Count)
		{
			Debug.LogError(string.Format("GamesWonIndicator.Show(): cannot do animation; numActiveSegments = {0} but m_segments.Count = {1}", this.m_numActiveSegments, this.m_segments.Count));
			return;
		}
		this.m_segments[this.m_numActiveSegments - 1].AnimateReward();
		GamesWonIndicator.InnKeeperTrigger innkeeperTrigger = this.m_innkeeperTrigger;
	}

	// Token: 0x060025BC RID: 9660 RVA: 0x000BDF1C File Offset: 0x000BC11C
	public void Hide()
	{
		this.m_root.SetActive(false);
	}

	// Token: 0x0400151C RID: 5404
	public GameObject m_root;

	// Token: 0x0400151D RID: 5405
	public GameObject m_segmentContainer;

	// Token: 0x0400151E RID: 5406
	public UberText m_winCountText;

	// Token: 0x0400151F RID: 5407
	public GamesWonIndicatorSegment m_gamesWonSegmentPrefab;

	// Token: 0x04001520 RID: 5408
	private List<GamesWonIndicatorSegment> m_segments = new List<GamesWonIndicatorSegment>();

	// Token: 0x04001521 RID: 5409
	private int m_numActiveSegments;

	// Token: 0x04001522 RID: 5410
	private GamesWonIndicator.InnKeeperTrigger m_innkeeperTrigger;

	// Token: 0x04001523 RID: 5411
	private const float FUDGE_FACTOR = 0.01f;

	// Token: 0x020015E2 RID: 5602
	public enum InnKeeperTrigger
	{
		// Token: 0x0400AF4A RID: 44874
		NONE
	}
}
