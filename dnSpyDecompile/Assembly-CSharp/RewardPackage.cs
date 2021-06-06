using System;
using UnityEngine;

// Token: 0x020002C2 RID: 706
public class RewardPackage : PegUIElement
{
	// Token: 0x0600251F RID: 9503 RVA: 0x0003677E File Offset: 0x0003497E
	protected override void Awake()
	{
		base.Awake();
	}

	// Token: 0x06002520 RID: 9504 RVA: 0x000BAE22 File Offset: 0x000B9022
	public void OnEnable()
	{
		this.SetEnabled(true, false);
	}

	// Token: 0x06002521 RID: 9505 RVA: 0x000BAE2C File Offset: 0x000B902C
	public void OnDisable()
	{
		this.SetEnabled(false, false);
	}

	// Token: 0x06002522 RID: 9506 RVA: 0x000BAE38 File Offset: 0x000B9038
	public void Update()
	{
		if (InputCollection.GetKeyDown(KeyCode.Alpha1) && this.m_RewardIndex == 0)
		{
			this.OpenReward();
			return;
		}
		if (InputCollection.GetKeyDown(KeyCode.Alpha2) && this.m_RewardIndex == 1)
		{
			this.OpenReward();
			return;
		}
		if (InputCollection.GetKeyDown(KeyCode.Alpha3) && this.m_RewardIndex == 2)
		{
			this.OpenReward();
			return;
		}
		if (InputCollection.GetKeyDown(KeyCode.Alpha4) && this.m_RewardIndex == 3)
		{
			this.OpenReward();
			return;
		}
		if (InputCollection.GetKeyDown(KeyCode.Alpha5) && this.m_RewardIndex == 4)
		{
			this.OpenReward();
		}
	}

	// Token: 0x06002523 RID: 9507 RVA: 0x000BAEC0 File Offset: 0x000B90C0
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		base.GetComponent<PlayMakerFSM>().SendEvent("Action");
	}

	// Token: 0x06002524 RID: 9508 RVA: 0x000BAED2 File Offset: 0x000B90D2
	protected override void OnPress()
	{
		this.OpenReward();
	}

	// Token: 0x06002525 RID: 9509 RVA: 0x000BAEDA File Offset: 0x000B90DA
	private void OpenReward()
	{
		base.GetComponent<PlayMakerFSM>().SendEvent("Death");
		RewardBoxesDisplay.Get().OpenReward(this.m_RewardIndex, base.transform.position);
		base.enabled = false;
	}

	// Token: 0x040014BB RID: 5307
	public int m_RewardIndex = -1;
}
