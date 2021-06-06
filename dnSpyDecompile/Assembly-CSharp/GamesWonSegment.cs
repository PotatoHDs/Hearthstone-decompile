using System;
using UnityEngine;

// Token: 0x020002CD RID: 717
[Serializable]
public class GamesWonSegment
{
	// Token: 0x060025C9 RID: 9673 RVA: 0x000BE21A File Offset: 0x000BC41A
	public virtual void Init(Reward.Type rewardType, int rewardAmount, bool hideCrown)
	{
		if (hideCrown)
		{
			this.m_crown.Hide();
		}
		else
		{
			this.m_crown.Show();
		}
		this.m_root.SetActive(true);
	}

	// Token: 0x060025CA RID: 9674 RVA: 0x000BE243 File Offset: 0x000BC443
	public virtual void AnimateReward()
	{
		this.m_crown.Animate();
	}

	// Token: 0x060025CB RID: 9675 RVA: 0x000BE250 File Offset: 0x000BC450
	public virtual float GetWidth()
	{
		return this.m_root.GetComponent<Renderer>().bounds.size.x;
	}

	// Token: 0x060025CC RID: 9676 RVA: 0x000BE27A File Offset: 0x000BC47A
	public virtual void Hide()
	{
		this.m_root.SetActive(false);
	}

	// Token: 0x0400152E RID: 5422
	public GameObject m_root;

	// Token: 0x0400152F RID: 5423
	public GamesWonCrown m_crown;
}
