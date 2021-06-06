using System.Collections.Generic;
using Assets;
using UnityEngine;

public class AdventureMissionDisplayTray : MonoBehaviour
{
	public SlidingTray m_slidingTray;

	public PegUIElement m_rewardsChest;

	public AdventureRewardsDisplayArea m_rewardsDisplay;

	public Transform m_rewardsDisplayBone;

	private void Awake()
	{
		AdventureConfig adventureConfig = AdventureConfig.Get();
		adventureConfig.AddAdventureMissionSetListener(OnMissionSelected);
		adventureConfig.AddSubSceneChangeListener(OnSubsceneChanged);
		if (m_rewardsChest != null)
		{
			m_rewardsChest.AddEventListener(UIEventType.ROLLOVER, delegate
			{
				ShowRewards();
			});
			m_rewardsChest.AddEventListener(UIEventType.ROLLOUT, delegate
			{
				HideRewards();
			});
		}
		GameMgr.Get().RegisterFindGameEvent(OnFindGameEvent);
	}

	private void OnDestroy()
	{
		AdventureConfig adventureConfig = AdventureConfig.Get();
		if (adventureConfig != null)
		{
			adventureConfig.RemoveAdventureMissionSetListener(OnMissionSelected);
			adventureConfig.RemoveSubSceneChangeListener(OnSubsceneChanged);
		}
		if (GameMgr.Get() != null)
		{
			GameMgr.Get().UnregisterFindGameEvent(OnFindGameEvent);
		}
	}

	private void OnMissionSelected(ScenarioDbId mission, bool showDetails)
	{
		if (mission != 0)
		{
			if (showDetails)
			{
				m_slidingTray.ToggleTraySlider(show: true);
			}
			ShowRewardsChest();
		}
	}

	public void EnableRewardsChest(bool enabled)
	{
		if (!(m_rewardsChest == null))
		{
			m_rewardsChest.SetEnabled(enabled);
		}
	}

	public void ShowRewardsChest()
	{
		if (!(m_rewardsChest == null))
		{
			ScenarioDbId mission = AdventureConfig.Get().GetMission();
			bool flag = AdventureProgressMgr.Get().HasDefeatedScenario((int)mission);
			bool active = AdventureProgressMgr.Get().ScenarioHasRewardData((int)mission) && !flag;
			m_rewardsChest.gameObject.SetActive(active);
		}
	}

	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		FindGameState state = eventData.m_state;
		if ((uint)(state - 2) <= 1u || (uint)(state - 7) <= 1u || state == FindGameState.SERVER_GAME_CANCELED)
		{
			EnableRewardsChest(enabled: true);
		}
		return false;
	}

	private void ShowRewards()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			NotificationManager.Get().DestroyActiveQuote(0.2f);
		}
		List<RewardData> immediateRewardsForDefeatingScenario = AdventureProgressMgr.Get().GetImmediateRewardsForDefeatingScenario((int)AdventureConfig.Get().GetMission());
		m_rewardsDisplay.ShowRewardsNoFullscreen(immediateRewardsForDefeatingScenario, m_rewardsDisplayBone.position, m_rewardsChest.transform.position);
	}

	private void HideRewards()
	{
		m_rewardsDisplay.HideRewards();
	}

	private void OnSubsceneChanged(AdventureData.Adventuresubscene newscene, bool forward)
	{
		m_slidingTray.ToggleTraySlider(show: false);
	}
}
