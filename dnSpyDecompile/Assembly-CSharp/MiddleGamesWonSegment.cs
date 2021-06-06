using System;
using UnityEngine;

// Token: 0x020002CC RID: 716
[Serializable]
public class MiddleGamesWonSegment : GamesWonSegment
{
	// Token: 0x060025C6 RID: 9670 RVA: 0x000BE190 File Offset: 0x000BC390
	public override void Init(Reward.Type rewardType, int rewardAmount, bool hideCrown)
	{
		base.Init(rewardType, rewardAmount, hideCrown);
		if (UnityEngine.Random.value < 0.5f)
		{
			this.m_activeRoot = this.m_root1;
			this.m_root2.SetActive(false);
		}
		else
		{
			this.m_activeRoot = this.m_root2;
			this.m_root1.SetActive(false);
		}
		this.m_activeRoot.SetActive(true);
	}

	// Token: 0x060025C7 RID: 9671 RVA: 0x000BE1F0 File Offset: 0x000BC3F0
	public override float GetWidth()
	{
		return this.m_activeRoot.GetComponent<Renderer>().bounds.size.x;
	}

	// Token: 0x0400152B RID: 5419
	public GameObject m_root1;

	// Token: 0x0400152C RID: 5420
	public GameObject m_root2;

	// Token: 0x0400152D RID: 5421
	private GameObject m_activeRoot;
}
