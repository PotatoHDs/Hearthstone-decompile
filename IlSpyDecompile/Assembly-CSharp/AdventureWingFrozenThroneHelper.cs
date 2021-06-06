using System.Collections;
using System.Collections.Generic;
using System.Text;
using Assets;
using UnityEngine;

[CustomEditClass]
[RequireComponent(typeof(AdventureWing))]
public class AdventureWingFrozenThroneHelper : MonoBehaviour
{
	public NestedPrefab m_secondaryBigChestContainer;

	public int m_secondaryChestVariation;

	public TooltipZone m_chestTwoColumnTooltipZone;

	public TooltipZone m_chestNormalTooltipZone;

	public FrozenThroneEventTable m_frozenThroneEventTable;

	public Vector3_MobileOverride m_tooltipOffsetFromReward;

	private AdventureWing m_adventureWing;

	private PegUIElement m_BigChestSecondary;

	private List<Achievement> m_classSpecificAchieves;

	private List<Achievement> m_newlyCompletedAchieves;

	private int m_numClassesAlreadyCompleted;

	private bool m_needToAnimateBigChest;

	private TooltipZone m_currentChestTooltipZone;

	private bool m_waitingForRuneAnimationEnd;

	private void Awake()
	{
		if (!(m_secondaryBigChestContainer != null))
		{
			return;
		}
		AdventureWingRewardsChest_ICC componentInChildren = m_secondaryBigChestContainer.PrefabGameObject(instantiateIfNeeded: true).GetComponentInChildren<AdventureWingRewardsChest_ICC>();
		if (componentInChildren != null)
		{
			componentInChildren.ActivateChest(m_secondaryChestVariation);
			PegUIElement component = componentInChildren.GetComponent<PegUIElement>();
			if (component != null)
			{
				m_BigChestSecondary = component;
			}
		}
	}

	private void Start()
	{
		m_adventureWing = GetComponent<AdventureWing>();
		if (m_adventureWing == null)
		{
			Log.All.PrintError("AdventureWingKarazhanHelper could not find an AdventureWing component on the same GameObject!");
			return;
		}
		if (m_BigChestSecondary != null)
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				m_BigChestSecondary.AddEventListener(UIEventType.RELEASE, ShowBigChestRewards);
			}
			else
			{
				m_BigChestSecondary.AddEventListener(UIEventType.ROLLOVER, ShowBigChestRewards);
				m_BigChestSecondary.AddEventListener(UIEventType.ROLLOUT, HideBigChestRewards);
			}
		}
		AdventureMissionDisplay.Get().AddProgressStepCompletedListener(OnAdventureProgressStepCompleted);
		m_frozenThroneEventTable.AddAnimateRuneEventEndListener(RuneAnimationEndEvent);
	}

	private void OnAdventureProgressStepCompleted(AdventureMissionDisplay.ProgressStep step)
	{
		if (AdventureMissionDisplay.ProgressStep.WING_COINS_AND_CHESTS_UPDATED == step)
		{
			StartCoroutine(PlayRuneAnimationsIfNecessary());
		}
	}

	private void Update()
	{
		if (m_adventureWing.IsDevMode)
		{
			if (InputCollection.GetKeyDown(KeyCode.Q))
			{
				AnimateRuneActivation(0);
			}
			if (InputCollection.GetKeyDown(KeyCode.W))
			{
				AnimateRuneActivation(1);
			}
			if (InputCollection.GetKeyDown(KeyCode.E))
			{
				AnimateRuneActivation(2);
			}
			if (InputCollection.GetKeyDown(KeyCode.R))
			{
				AnimateRuneActivation(3);
			}
			if (InputCollection.GetKeyDown(KeyCode.T))
			{
				AnimateRuneActivation(4);
			}
			if (InputCollection.GetKeyDown(KeyCode.Y))
			{
				AnimateRuneActivation(5);
			}
			if (InputCollection.GetKeyDown(KeyCode.U))
			{
				AnimateRuneActivation(6);
			}
			if (InputCollection.GetKeyDown(KeyCode.I))
			{
				AnimateRuneActivation(7);
			}
			if (InputCollection.GetKeyDown(KeyCode.O))
			{
				AnimateRuneActivation(8);
			}
		}
	}

	public void SetBigChestRewards(WingDbId wingId)
	{
		if (AdventureConfig.Get().GetSelectedMode() == AdventureModeDbId.LINEAR)
		{
			HashSet<Achieve.RewardTiming> rewardTimings = new HashSet<Achieve.RewardTiming> { Achieve.RewardTiming.ADVENTURE_CHEST };
			List<RewardData> rewardsForAchieve = AchieveManager.Get().GetRewardsForAchieve(768, rewardTimings);
			if (m_BigChestSecondary != null)
			{
				m_BigChestSecondary.SetData(rewardsForAchieve);
			}
			m_classSpecificAchieves = GetClassSpecificAchievementsForWing(wingId);
			PrepareRuneAnimations(m_classSpecificAchieves);
		}
	}

	public List<RewardData> GetBigChestRewards()
	{
		if (!(m_BigChestSecondary != null))
		{
			return null;
		}
		return (List<RewardData>)m_BigChestSecondary.GetData();
	}

	private void ShowBigChestRewards(UIEvent e)
	{
		List<RewardData> bigChestRewards = GetBigChestRewards();
		if (bigChestRewards != null)
		{
			m_adventureWing.FireShowRewardsEvent(bigChestRewards, m_BigChestSecondary.transform.position);
			AdventureMissionDisplay.Get().m_RewardsDisplay.AddRewardsHiddenListener(SecondaryChestRewardsHidden);
			ShowProgressTooltip();
		}
	}

	private void ShowProgressTooltip()
	{
		if (m_numClassesAlreadyCompleted == 0)
		{
			m_currentChestTooltipZone = m_chestNormalTooltipZone;
			RepositionChestTooltip(m_currentChestTooltipZone);
			m_currentChestTooltipZone.ShowTooltip(GameStrings.Get("GLUE_FROSTMOURNE_REWARD_HEADER"), GameStrings.Get("GLUE_FROSTMOURNE_WING_INCOMPLETE_REWARD_BODY"), 4f);
			return;
		}
		List<StringBuilder> list = new List<StringBuilder>(2);
		list.Add(new StringBuilder());
		list.Add(new StringBuilder());
		bool flag = false;
		int num = 0;
		foreach (Achievement classSpecificAchiefe in m_classSpecificAchieves)
		{
			if (!classSpecificAchiefe.MyHeroClassRequirement.HasValue)
			{
				Log.All.PrintWarning("Something is wrong - achievement {0} has no MyHeroClass!", classSpecificAchiefe);
				continue;
			}
			TAG_CLASS value = classSpecificAchiefe.MyHeroClassRequirement.Value;
			if (!classSpecificAchiefe.IsCompleted())
			{
				flag = true;
				if (list[num].Length > 0)
				{
					list[num].Append("\n");
				}
				list[num].Append($"- {GameStrings.GetClassName(value)}");
				num = 1 - num;
			}
			else
			{
				Log.Adventures.Print("AdventureWingFrozenThroneHelper.ShowProgressTooltip(): Achievement for class {0} is completed.", GameStrings.GetClassName(value));
			}
		}
		if (flag)
		{
			m_currentChestTooltipZone = m_chestTwoColumnTooltipZone;
			RepositionChestTooltip(m_currentChestTooltipZone);
			m_currentChestTooltipZone.ShowMultiColumnTooltip(GameStrings.Get("GLUE_FROSTMOURNE_REWARD_HEADER"), GameStrings.Get("GLUE_FROSTMOURNE_REWARD_BODY"), new string[2]
			{
				list[0].ToString(),
				list[1].ToString()
			}, 4f);
		}
		else
		{
			Log.All.PrintWarning("AdventureWingFrozenThroneHelper.ShowProgressTooltip(): No classes to add to the tooltip! We should not be showing the tooltip in this case!");
		}
	}

	private void RepositionChestTooltip(TooltipZone tooltipZone)
	{
		List<GameObject> currentShownRewards = AdventureMissionDisplay.Get().m_RewardsDisplay.GetCurrentShownRewards();
		if (currentShownRewards.Count > 0)
		{
			Vector3 position = tooltipZone.tooltipDisplayLocation.transform.position;
			Vector3 vector = m_tooltipOffsetFromReward;
			position.x = currentShownRewards[0].transform.position.x + vector.x;
			position.z = currentShownRewards[0].transform.position.z + vector.z;
			tooltipZone.tooltipDisplayLocation.transform.position = position;
		}
	}

	private void HideBigChestRewards(UIEvent e)
	{
		List<RewardData> bigChestRewards = GetBigChestRewards();
		if (bigChestRewards != null)
		{
			m_adventureWing.FireHideRewardsEvent(bigChestRewards);
		}
	}

	private void SecondaryChestRewardsHidden()
	{
		AdventureMissionDisplay.Get().m_RewardsDisplay.RemoveRewardsHiddenListener(SecondaryChestRewardsHidden);
		HideProgressTooltip();
	}

	private void HideProgressTooltip()
	{
		if (m_currentChestTooltipZone != null)
		{
			m_currentChestTooltipZone.HideTooltip();
		}
	}

	private List<Achievement> GetClassSpecificAchievementsForWing(WingDbId wingId)
	{
		List<Achievement> list = new List<Achievement>();
		foreach (Achievement item in AchieveManager.Get().GetAchievesForAdventureWing((int)wingId))
		{
			if (item.AchieveType == Achieve.Type.HIDDEN && item.MyHeroClassRequirement.HasValue && item.MyHeroClassRequirement.Value != 0)
			{
				list.Add(item);
			}
		}
		return list;
	}

	private void PrepareRuneAnimations(List<Achievement> classSpecificAchieves)
	{
		if (classSpecificAchieves == null)
		{
			Log.All.PrintWarning("AdventureWingFrozenThroneHelper.PrepareRuneAnimations() - Attempting to prepare rune animations, but classSpecificAchieves is null!");
			return;
		}
		m_numClassesAlreadyCompleted = 0;
		m_newlyCompletedAchieves = new List<Achievement>();
		foreach (Achievement classSpecificAchiefe in classSpecificAchieves)
		{
			if (classSpecificAchiefe.IsCompleted())
			{
				if (classSpecificAchiefe.IsNewlyCompleted())
				{
					m_newlyCompletedAchieves.Add(classSpecificAchiefe);
				}
				else
				{
					m_numClassesAlreadyCompleted++;
				}
			}
		}
		for (int i = 0; i < m_numClassesAlreadyCompleted; i++)
		{
			m_frozenThroneEventTable.SetRuneInitiallyActivated(i);
		}
		Log.Adventures.Print("{0} runes already animated, {1} waiting for animation.", m_numClassesAlreadyCompleted, m_newlyCompletedAchieves.Count);
		Achievement achievement = AchieveManager.Get().GetAchievement(768);
		if (achievement != null && achievement.IsCompleted())
		{
			m_needToAnimateBigChest = achievement.IsNewlyCompleted();
			if (!m_needToAnimateBigChest)
			{
				m_frozenThroneEventTable.BigChestSecondaryStayOpen();
			}
		}
	}

	private IEnumerator PlayRuneAnimationsIfNecessary()
	{
		if (m_newlyCompletedAchieves == null)
		{
			Log.All.PrintWarning("AdventureWingFrozenThroneHelper.PlayRuneAnimationIfNecessary() - Attempting to play rune animations for newly completed achieves, but m_newlyCompletedAchieves is null!");
			yield break;
		}
		if (m_newlyCompletedAchieves.Count > 0)
		{
			AdventureMissionDisplay.Get().GetExternalUILock();
			m_adventureWing.BringToFocus();
			foreach (Achievement achieve in m_newlyCompletedAchieves)
			{
				Log.Adventures.Print("Playing animation for rune {0}, for class {1}", m_numClassesAlreadyCompleted, achieve.MyHeroClassRequirement.Value);
				m_waitingForRuneAnimationEnd = true;
				AnimateRuneActivation(m_numClassesAlreadyCompleted);
				while (m_waitingForRuneAnimationEnd)
				{
					yield return null;
				}
				achieve.AckCurrentProgressAndRewardNotices();
				m_numClassesAlreadyCompleted++;
			}
			AdventureMissionDisplay.Get().ReleaseExternalUILock();
			m_newlyCompletedAchieves.Clear();
		}
		Log.Adventures.Print("Finished animating runes, if applicable.");
		if (!m_needToAnimateBigChest)
		{
			yield break;
		}
		AdventureMissionDisplay.Get().GetExternalUILock();
		m_adventureWing.BringToFocus();
		bool waitingForNextStep = true;
		m_frozenThroneEventTable.AddChestOpenEndEventListener(delegate
		{
			waitingForNextStep = false;
		}, once: true);
		OpenBigChestSecondary();
		while (waitingForNextStep)
		{
			yield return null;
		}
		if (UserAttentionManager.CanShowAttentionGrabber("AdventureMissionDisplay.ShowFixedRewards"))
		{
			waitingForNextStep = true;
			PopupDisplayManager.Get().ShowAnyOutstandingPopups(delegate
			{
				waitingForNextStep = false;
			});
			while (waitingForNextStep)
			{
				yield return null;
			}
		}
		AdventureMissionDisplay.Get().ReleaseExternalUILock();
		m_needToAnimateBigChest = false;
	}

	private void AnimateRuneActivation(int rune)
	{
		m_frozenThroneEventTable.AnimateRuneActivation(rune);
	}

	private void RuneAnimationEndEvent(Spell s)
	{
		m_waitingForRuneAnimationEnd = false;
	}

	private void OpenBigChestSecondary()
	{
		m_frozenThroneEventTable.BigChestSecondaryOpen();
		if (m_BigChestSecondary != null)
		{
			m_BigChestSecondary.RemoveEventListener(UIEventType.PRESS, ShowBigChestRewards);
			m_BigChestSecondary.RemoveEventListener(UIEventType.ROLLOVER, ShowBigChestRewards);
			m_BigChestSecondary.RemoveEventListener(UIEventType.ROLLOUT, HideBigChestRewards);
		}
	}
}
