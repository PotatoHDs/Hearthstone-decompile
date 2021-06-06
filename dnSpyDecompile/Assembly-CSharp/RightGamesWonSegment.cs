using System;
using UnityEngine;

// Token: 0x020002CB RID: 715
[Serializable]
public class RightGamesWonSegment : GamesWonSegment
{
	// Token: 0x060025C2 RID: 9666 RVA: 0x000BDF78 File Offset: 0x000BC178
	public override void Init(Reward.Type rewardType, int rewardAmount, bool hideCrown)
	{
		base.Init(rewardType, rewardAmount, hideCrown);
		this.m_rewardType = rewardType;
		Reward.Type rewardType2 = this.m_rewardType;
		if (rewardType2 == Reward.Type.ARCANE_DUST)
		{
			this.m_boosterRoot.SetActive(false);
			this.m_goldRoot.SetActive(false);
			this.m_dustRoot.SetActive(true);
			this.m_dustAmountText.Text = rewardAmount.ToString();
			return;
		}
		if (rewardType2 == Reward.Type.BOOSTER_PACK)
		{
			this.m_dustRoot.SetActive(false);
			this.m_goldRoot.SetActive(false);
			this.m_boosterRoot.SetActive(true);
			return;
		}
		if (rewardType2 != Reward.Type.GOLD)
		{
			Debug.LogError(string.Format("GamesWonIndicatorSegment(): don't know how to init right segment with reward type {0}", this.m_rewardType));
			this.m_boosterRoot.SetActive(false);
			this.m_dustRoot.SetActive(false);
			this.m_goldRoot.SetActive(false);
			return;
		}
		this.m_boosterRoot.SetActive(false);
		this.m_dustRoot.SetActive(false);
		this.m_goldRoot.SetActive(true);
		this.m_goldAmountText.Text = rewardAmount.ToString();
	}

	// Token: 0x060025C3 RID: 9667 RVA: 0x000BE080 File Offset: 0x000BC280
	public override void AnimateReward()
	{
		this.m_crown.Animate();
		PlayMakerFSM playMakerFSM = null;
		Reward.Type rewardType = this.m_rewardType;
		if (rewardType != Reward.Type.ARCANE_DUST)
		{
			if (rewardType != Reward.Type.BOOSTER_PACK)
			{
				if (rewardType == Reward.Type.GOLD)
				{
					playMakerFSM = this.m_goldRoot.GetComponent<PlayMakerFSM>();
				}
			}
			else
			{
				playMakerFSM = this.m_boosterRoot.GetComponent<PlayMakerFSM>();
			}
		}
		else
		{
			playMakerFSM = this.m_dustRoot.GetComponent<PlayMakerFSM>();
		}
		if (playMakerFSM == null)
		{
			Debug.LogError(string.Format("GamesWonIndicatorSegment(): missing playMaker component for reward type {0}", this.m_rewardType));
			return;
		}
		playMakerFSM.SendEvent("Birth");
	}

	// Token: 0x060025C4 RID: 9668 RVA: 0x000BE108 File Offset: 0x000BC308
	public override float GetWidth()
	{
		Reward.Type rewardType = this.m_rewardType;
		if (rewardType == Reward.Type.ARCANE_DUST)
		{
			return this.m_dustRoot.GetComponent<Renderer>().bounds.size.x;
		}
		if (rewardType == Reward.Type.BOOSTER_PACK)
		{
			return this.m_boosterRoot.GetComponent<Renderer>().bounds.size.x;
		}
		if (rewardType != Reward.Type.GOLD)
		{
			return 0f;
		}
		return this.m_goldRoot.GetComponent<Renderer>().bounds.size.x;
	}

	// Token: 0x04001525 RID: 5413
	public GameObject m_boosterRoot;

	// Token: 0x04001526 RID: 5414
	public GameObject m_dustRoot;

	// Token: 0x04001527 RID: 5415
	public GameObject m_goldRoot;

	// Token: 0x04001528 RID: 5416
	public UberText m_dustAmountText;

	// Token: 0x04001529 RID: 5417
	public UberText m_goldAmountText;

	// Token: 0x0400152A RID: 5418
	private Reward.Type m_rewardType;
}
