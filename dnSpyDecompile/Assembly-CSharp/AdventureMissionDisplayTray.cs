using System;
using System.Collections.Generic;
using Assets;
using UnityEngine;

// Token: 0x02000045 RID: 69
public class AdventureMissionDisplayTray : MonoBehaviour
{
	// Token: 0x060003C9 RID: 969 RVA: 0x00017BF0 File Offset: 0x00015DF0
	private void Awake()
	{
		AdventureConfig adventureConfig = AdventureConfig.Get();
		adventureConfig.AddAdventureMissionSetListener(new AdventureConfig.AdventureMissionSet(this.OnMissionSelected));
		adventureConfig.AddSubSceneChangeListener(new AdventureConfig.SubSceneChange(this.OnSubsceneChanged));
		if (this.m_rewardsChest != null)
		{
			this.m_rewardsChest.AddEventListener(UIEventType.ROLLOVER, delegate(UIEvent e)
			{
				this.ShowRewards();
			});
			this.m_rewardsChest.AddEventListener(UIEventType.ROLLOUT, delegate(UIEvent e)
			{
				this.HideRewards();
			});
		}
		GameMgr.Get().RegisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
	}

	// Token: 0x060003CA RID: 970 RVA: 0x00017C7C File Offset: 0x00015E7C
	private void OnDestroy()
	{
		AdventureConfig adventureConfig = AdventureConfig.Get();
		if (adventureConfig != null)
		{
			adventureConfig.RemoveAdventureMissionSetListener(new AdventureConfig.AdventureMissionSet(this.OnMissionSelected));
			adventureConfig.RemoveSubSceneChangeListener(new AdventureConfig.SubSceneChange(this.OnSubsceneChanged));
		}
		if (GameMgr.Get() != null)
		{
			GameMgr.Get().UnregisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		}
	}

	// Token: 0x060003CB RID: 971 RVA: 0x00017CDA File Offset: 0x00015EDA
	private void OnMissionSelected(ScenarioDbId mission, bool showDetails)
	{
		if (mission == ScenarioDbId.INVALID)
		{
			return;
		}
		if (showDetails)
		{
			this.m_slidingTray.ToggleTraySlider(true, null, true);
		}
		this.ShowRewardsChest();
	}

	// Token: 0x060003CC RID: 972 RVA: 0x00017CF7 File Offset: 0x00015EF7
	public void EnableRewardsChest(bool enabled)
	{
		if (this.m_rewardsChest == null)
		{
			return;
		}
		this.m_rewardsChest.SetEnabled(enabled, false);
	}

	// Token: 0x060003CD RID: 973 RVA: 0x00017D18 File Offset: 0x00015F18
	public void ShowRewardsChest()
	{
		if (this.m_rewardsChest == null)
		{
			return;
		}
		ScenarioDbId mission = AdventureConfig.Get().GetMission();
		bool flag = AdventureProgressMgr.Get().HasDefeatedScenario((int)mission);
		bool active = AdventureProgressMgr.Get().ScenarioHasRewardData((int)mission) && !flag;
		this.m_rewardsChest.gameObject.SetActive(active);
	}

	// Token: 0x060003CE RID: 974 RVA: 0x00017D74 File Offset: 0x00015F74
	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		FindGameState state = eventData.m_state;
		if (state - FindGameState.CLIENT_CANCELED <= 1 || state - FindGameState.BNET_QUEUE_CANCELED <= 1 || state == FindGameState.SERVER_GAME_CANCELED)
		{
			this.EnableRewardsChest(true);
		}
		return false;
	}

	// Token: 0x060003CF RID: 975 RVA: 0x00017DA4 File Offset: 0x00015FA4
	private void ShowRewards()
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			NotificationManager.Get().DestroyActiveQuote(0.2f, false);
		}
		List<RewardData> immediateRewardsForDefeatingScenario = AdventureProgressMgr.Get().GetImmediateRewardsForDefeatingScenario((int)AdventureConfig.Get().GetMission());
		this.m_rewardsDisplay.ShowRewardsNoFullscreen(immediateRewardsForDefeatingScenario, this.m_rewardsDisplayBone.position, new Vector3?(this.m_rewardsChest.transform.position));
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x00017E0E File Offset: 0x0001600E
	private void HideRewards()
	{
		this.m_rewardsDisplay.HideRewards();
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x00017E1B File Offset: 0x0001601B
	private void OnSubsceneChanged(AdventureData.Adventuresubscene newscene, bool forward)
	{
		this.m_slidingTray.ToggleTraySlider(false, null, true);
	}

	// Token: 0x0400029D RID: 669
	public SlidingTray m_slidingTray;

	// Token: 0x0400029E RID: 670
	public PegUIElement m_rewardsChest;

	// Token: 0x0400029F RID: 671
	public AdventureRewardsDisplayArea m_rewardsDisplay;

	// Token: 0x040002A0 RID: 672
	public Transform m_rewardsDisplayBone;
}
