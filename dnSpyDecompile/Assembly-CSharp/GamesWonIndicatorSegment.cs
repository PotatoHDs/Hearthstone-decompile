using System;
using UnityEngine;

// Token: 0x020002CE RID: 718
public class GamesWonIndicatorSegment : MonoBehaviour
{
	// Token: 0x060025CE RID: 9678 RVA: 0x000BE288 File Offset: 0x000BC488
	public void Init(GamesWonIndicatorSegment.Type segmentType, Reward.Type rewardType, int rewardAmount, bool hideCrown)
	{
		switch (segmentType)
		{
		case GamesWonIndicatorSegment.Type.LEFT:
			this.m_activeSegment = this.m_leftSegment;
			this.m_middleSegment.Hide();
			this.m_rightSegment.Hide();
			break;
		case GamesWonIndicatorSegment.Type.MIDDLE:
			this.m_activeSegment = this.m_middleSegment;
			this.m_leftSegment.Hide();
			this.m_rightSegment.Hide();
			break;
		case GamesWonIndicatorSegment.Type.RIGHT:
			this.m_activeSegment = this.m_rightSegment;
			this.m_leftSegment.Hide();
			this.m_middleSegment.Hide();
			break;
		}
		this.m_activeSegment.Init(rewardType, rewardAmount, hideCrown);
	}

	// Token: 0x060025CF RID: 9679 RVA: 0x000BE322 File Offset: 0x000BC522
	public float GetWidth()
	{
		return this.m_activeSegment.GetWidth();
	}

	// Token: 0x060025D0 RID: 9680 RVA: 0x000BE32F File Offset: 0x000BC52F
	public void AnimateReward()
	{
		this.m_activeSegment.AnimateReward();
	}

	// Token: 0x04001530 RID: 5424
	public GamesWonSegment m_leftSegment;

	// Token: 0x04001531 RID: 5425
	public MiddleGamesWonSegment m_middleSegment;

	// Token: 0x04001532 RID: 5426
	public RightGamesWonSegment m_rightSegment;

	// Token: 0x04001533 RID: 5427
	private GamesWonSegment m_activeSegment;

	// Token: 0x020015E3 RID: 5603
	public enum Type
	{
		// Token: 0x0400AF4C RID: 44876
		LEFT,
		// Token: 0x0400AF4D RID: 44877
		MIDDLE,
		// Token: 0x0400AF4E RID: 44878
		RIGHT
	}
}
