using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Assets;
using UnityEngine;

// Token: 0x0200005A RID: 90
[CustomEditClass]
[RequireComponent(typeof(AdventureWing))]
public class AdventureWingFrozenThroneHelper : MonoBehaviour
{
	// Token: 0x06000559 RID: 1369 RVA: 0x0001ECB8 File Offset: 0x0001CEB8
	private void Awake()
	{
		if (this.m_secondaryBigChestContainer != null)
		{
			AdventureWingRewardsChest_ICC componentInChildren = this.m_secondaryBigChestContainer.PrefabGameObject(true).GetComponentInChildren<AdventureWingRewardsChest_ICC>();
			if (componentInChildren != null)
			{
				componentInChildren.ActivateChest(this.m_secondaryChestVariation);
				PegUIElement component = componentInChildren.GetComponent<PegUIElement>();
				if (component != null)
				{
					this.m_BigChestSecondary = component;
				}
			}
		}
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x0001ED14 File Offset: 0x0001CF14
	private void Start()
	{
		this.m_adventureWing = base.GetComponent<AdventureWing>();
		if (this.m_adventureWing == null)
		{
			Log.All.PrintError("AdventureWingKarazhanHelper could not find an AdventureWing component on the same GameObject!", Array.Empty<object>());
			return;
		}
		if (this.m_BigChestSecondary != null)
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.m_BigChestSecondary.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ShowBigChestRewards));
			}
			else
			{
				this.m_BigChestSecondary.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.ShowBigChestRewards));
				this.m_BigChestSecondary.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.HideBigChestRewards));
			}
		}
		AdventureMissionDisplay.Get().AddProgressStepCompletedListener(new AdventureMissionDisplay.ProgressStepCompletedCallback(this.OnAdventureProgressStepCompleted));
		this.m_frozenThroneEventTable.AddAnimateRuneEventEndListener(new StateEventTable.StateEventTrigger(this.RuneAnimationEndEvent));
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x0001EDE4 File Offset: 0x0001CFE4
	private void OnAdventureProgressStepCompleted(AdventureMissionDisplay.ProgressStep step)
	{
		if (AdventureMissionDisplay.ProgressStep.WING_COINS_AND_CHESTS_UPDATED == step)
		{
			base.StartCoroutine(this.PlayRuneAnimationsIfNecessary());
		}
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x0001EDF8 File Offset: 0x0001CFF8
	private void Update()
	{
		if (!this.m_adventureWing.IsDevMode)
		{
			return;
		}
		if (InputCollection.GetKeyDown(KeyCode.Q))
		{
			this.AnimateRuneActivation(0);
		}
		if (InputCollection.GetKeyDown(KeyCode.W))
		{
			this.AnimateRuneActivation(1);
		}
		if (InputCollection.GetKeyDown(KeyCode.E))
		{
			this.AnimateRuneActivation(2);
		}
		if (InputCollection.GetKeyDown(KeyCode.R))
		{
			this.AnimateRuneActivation(3);
		}
		if (InputCollection.GetKeyDown(KeyCode.T))
		{
			this.AnimateRuneActivation(4);
		}
		if (InputCollection.GetKeyDown(KeyCode.Y))
		{
			this.AnimateRuneActivation(5);
		}
		if (InputCollection.GetKeyDown(KeyCode.U))
		{
			this.AnimateRuneActivation(6);
		}
		if (InputCollection.GetKeyDown(KeyCode.I))
		{
			this.AnimateRuneActivation(7);
		}
		if (InputCollection.GetKeyDown(KeyCode.O))
		{
			this.AnimateRuneActivation(8);
		}
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x0001EEA4 File Offset: 0x0001D0A4
	public void SetBigChestRewards(WingDbId wingId)
	{
		if (AdventureConfig.Get().GetSelectedMode() != AdventureModeDbId.LINEAR)
		{
			return;
		}
		HashSet<Achieve.RewardTiming> rewardTimings = new HashSet<Achieve.RewardTiming>
		{
			Achieve.RewardTiming.ADVENTURE_CHEST
		};
		List<RewardData> rewardsForAchieve = AchieveManager.Get().GetRewardsForAchieve(768, rewardTimings);
		if (this.m_BigChestSecondary != null)
		{
			this.m_BigChestSecondary.SetData(rewardsForAchieve);
		}
		this.m_classSpecificAchieves = this.GetClassSpecificAchievementsForWing(wingId);
		this.PrepareRuneAnimations(this.m_classSpecificAchieves);
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x0001EF11 File Offset: 0x0001D111
	public List<RewardData> GetBigChestRewards()
	{
		if (!(this.m_BigChestSecondary != null))
		{
			return null;
		}
		return (List<RewardData>)this.m_BigChestSecondary.GetData();
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x0001EF34 File Offset: 0x0001D134
	private void ShowBigChestRewards(UIEvent e)
	{
		List<RewardData> bigChestRewards = this.GetBigChestRewards();
		if (bigChestRewards == null)
		{
			return;
		}
		this.m_adventureWing.FireShowRewardsEvent(bigChestRewards, this.m_BigChestSecondary.transform.position);
		AdventureMissionDisplay.Get().m_RewardsDisplay.AddRewardsHiddenListener(new AdventureRewardsDisplayArea.RewardsHidden(this.SecondaryChestRewardsHidden));
		this.ShowProgressTooltip();
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x0001EF8C File Offset: 0x0001D18C
	private void ShowProgressTooltip()
	{
		if (this.m_numClassesAlreadyCompleted == 0)
		{
			this.m_currentChestTooltipZone = this.m_chestNormalTooltipZone;
			this.RepositionChestTooltip(this.m_currentChestTooltipZone);
			this.m_currentChestTooltipZone.ShowTooltip(GameStrings.Get("GLUE_FROSTMOURNE_REWARD_HEADER"), GameStrings.Get("GLUE_FROSTMOURNE_WING_INCOMPLETE_REWARD_BODY"), 4f, 0);
			return;
		}
		List<StringBuilder> list = new List<StringBuilder>(2);
		list.Add(new StringBuilder());
		list.Add(new StringBuilder());
		bool flag = false;
		int num = 0;
		foreach (global::Achievement achievement in this.m_classSpecificAchieves)
		{
			if (achievement.MyHeroClassRequirement == null)
			{
				Log.All.PrintWarning("Something is wrong - achievement {0} has no MyHeroClass!", new object[]
				{
					achievement
				});
			}
			else
			{
				TAG_CLASS value = achievement.MyHeroClassRequirement.Value;
				if (!achievement.IsCompleted())
				{
					flag = true;
					if (list[num].Length > 0)
					{
						list[num].Append("\n");
					}
					list[num].Append(string.Format("- {0}", GameStrings.GetClassName(value)));
					num = 1 - num;
				}
				else
				{
					Log.Adventures.Print("AdventureWingFrozenThroneHelper.ShowProgressTooltip(): Achievement for class {0} is completed.", new object[]
					{
						GameStrings.GetClassName(value)
					});
				}
			}
		}
		if (flag)
		{
			this.m_currentChestTooltipZone = this.m_chestTwoColumnTooltipZone;
			this.RepositionChestTooltip(this.m_currentChestTooltipZone);
			this.m_currentChestTooltipZone.ShowMultiColumnTooltip(GameStrings.Get("GLUE_FROSTMOURNE_REWARD_HEADER"), GameStrings.Get("GLUE_FROSTMOURNE_REWARD_BODY"), new string[]
			{
				list[0].ToString(),
				list[1].ToString()
			}, 4f, 0);
			return;
		}
		Log.All.PrintWarning("AdventureWingFrozenThroneHelper.ShowProgressTooltip(): No classes to add to the tooltip! We should not be showing the tooltip in this case!", Array.Empty<object>());
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x0001F174 File Offset: 0x0001D374
	private void RepositionChestTooltip(TooltipZone tooltipZone)
	{
		List<GameObject> currentShownRewards = AdventureMissionDisplay.Get().m_RewardsDisplay.GetCurrentShownRewards();
		if (currentShownRewards.Count > 0)
		{
			Vector3 position = tooltipZone.tooltipDisplayLocation.transform.position;
			Vector3 vector = this.m_tooltipOffsetFromReward;
			position.x = currentShownRewards[0].transform.position.x + vector.x;
			position.z = currentShownRewards[0].transform.position.z + vector.z;
			tooltipZone.tooltipDisplayLocation.transform.position = position;
		}
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x0001F210 File Offset: 0x0001D410
	private void HideBigChestRewards(UIEvent e)
	{
		List<RewardData> bigChestRewards = this.GetBigChestRewards();
		if (bigChestRewards == null)
		{
			return;
		}
		this.m_adventureWing.FireHideRewardsEvent(bigChestRewards);
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x0001F234 File Offset: 0x0001D434
	private void SecondaryChestRewardsHidden()
	{
		AdventureMissionDisplay.Get().m_RewardsDisplay.RemoveRewardsHiddenListener(new AdventureRewardsDisplayArea.RewardsHidden(this.SecondaryChestRewardsHidden));
		this.HideProgressTooltip();
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x0001F257 File Offset: 0x0001D457
	private void HideProgressTooltip()
	{
		if (this.m_currentChestTooltipZone != null)
		{
			this.m_currentChestTooltipZone.HideTooltip();
		}
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x0001F274 File Offset: 0x0001D474
	private List<global::Achievement> GetClassSpecificAchievementsForWing(WingDbId wingId)
	{
		List<global::Achievement> list = new List<global::Achievement>();
		foreach (global::Achievement achievement in AchieveManager.Get().GetAchievesForAdventureWing((int)wingId))
		{
			if (achievement.AchieveType == Achieve.Type.HIDDEN && achievement.MyHeroClassRequirement != null && achievement.MyHeroClassRequirement.Value != TAG_CLASS.INVALID)
			{
				list.Add(achievement);
			}
		}
		return list;
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x0001F2FC File Offset: 0x0001D4FC
	private void PrepareRuneAnimations(List<global::Achievement> classSpecificAchieves)
	{
		if (classSpecificAchieves == null)
		{
			Log.All.PrintWarning("AdventureWingFrozenThroneHelper.PrepareRuneAnimations() - Attempting to prepare rune animations, but classSpecificAchieves is null!", Array.Empty<object>());
			return;
		}
		this.m_numClassesAlreadyCompleted = 0;
		this.m_newlyCompletedAchieves = new List<global::Achievement>();
		foreach (global::Achievement achievement in classSpecificAchieves)
		{
			if (achievement.IsCompleted())
			{
				if (achievement.IsNewlyCompleted())
				{
					this.m_newlyCompletedAchieves.Add(achievement);
				}
				else
				{
					this.m_numClassesAlreadyCompleted++;
				}
			}
		}
		for (int i = 0; i < this.m_numClassesAlreadyCompleted; i++)
		{
			this.m_frozenThroneEventTable.SetRuneInitiallyActivated(i);
		}
		Log.Adventures.Print("{0} runes already animated, {1} waiting for animation.", new object[]
		{
			this.m_numClassesAlreadyCompleted,
			this.m_newlyCompletedAchieves.Count
		});
		global::Achievement achievement2 = AchieveManager.Get().GetAchievement(768);
		if (achievement2 != null && achievement2.IsCompleted())
		{
			this.m_needToAnimateBigChest = achievement2.IsNewlyCompleted();
			if (!this.m_needToAnimateBigChest)
			{
				this.m_frozenThroneEventTable.BigChestSecondaryStayOpen();
			}
		}
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x0001F428 File Offset: 0x0001D628
	private IEnumerator PlayRuneAnimationsIfNecessary()
	{
		if (this.m_newlyCompletedAchieves == null)
		{
			Log.All.PrintWarning("AdventureWingFrozenThroneHelper.PlayRuneAnimationIfNecessary() - Attempting to play rune animations for newly completed achieves, but m_newlyCompletedAchieves is null!", Array.Empty<object>());
			yield break;
		}
		if (this.m_newlyCompletedAchieves.Count > 0)
		{
			AdventureMissionDisplay.Get().GetExternalUILock();
			this.m_adventureWing.BringToFocus();
			foreach (global::Achievement achieve in this.m_newlyCompletedAchieves)
			{
				Log.Adventures.Print("Playing animation for rune {0}, for class {1}", new object[]
				{
					this.m_numClassesAlreadyCompleted,
					achieve.MyHeroClassRequirement.Value
				});
				this.m_waitingForRuneAnimationEnd = true;
				this.AnimateRuneActivation(this.m_numClassesAlreadyCompleted);
				while (this.m_waitingForRuneAnimationEnd)
				{
					yield return null;
				}
				achieve.AckCurrentProgressAndRewardNotices();
				this.m_numClassesAlreadyCompleted++;
				achieve = null;
			}
			List<global::Achievement>.Enumerator enumerator = default(List<global::Achievement>.Enumerator);
			AdventureMissionDisplay.Get().ReleaseExternalUILock();
			this.m_newlyCompletedAchieves.Clear();
		}
		Log.Adventures.Print("Finished animating runes, if applicable.", Array.Empty<object>());
		if (this.m_needToAnimateBigChest)
		{
			AdventureWingFrozenThroneHelper.<>c__DisplayClass27_0 CS$<>8__locals1 = new AdventureWingFrozenThroneHelper.<>c__DisplayClass27_0();
			AdventureMissionDisplay.Get().GetExternalUILock();
			this.m_adventureWing.BringToFocus();
			CS$<>8__locals1.waitingForNextStep = true;
			this.m_frozenThroneEventTable.AddChestOpenEndEventListener(delegate(Spell s)
			{
				CS$<>8__locals1.waitingForNextStep = false;
			}, true);
			this.OpenBigChestSecondary();
			while (CS$<>8__locals1.waitingForNextStep)
			{
				yield return null;
			}
			if (UserAttentionManager.CanShowAttentionGrabber("AdventureMissionDisplay.ShowFixedRewards"))
			{
				CS$<>8__locals1.waitingForNextStep = true;
				PopupDisplayManager.Get().ShowAnyOutstandingPopups(delegate()
				{
					CS$<>8__locals1.waitingForNextStep = false;
				});
				while (CS$<>8__locals1.waitingForNextStep)
				{
					yield return null;
				}
			}
			AdventureMissionDisplay.Get().ReleaseExternalUILock();
			this.m_needToAnimateBigChest = false;
			CS$<>8__locals1 = null;
		}
		yield break;
		yield break;
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x0001F437 File Offset: 0x0001D637
	private void AnimateRuneActivation(int rune)
	{
		this.m_frozenThroneEventTable.AnimateRuneActivation(rune);
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x0001F445 File Offset: 0x0001D645
	private void RuneAnimationEndEvent(Spell s)
	{
		this.m_waitingForRuneAnimationEnd = false;
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x0001F450 File Offset: 0x0001D650
	private void OpenBigChestSecondary()
	{
		this.m_frozenThroneEventTable.BigChestSecondaryOpen();
		if (this.m_BigChestSecondary != null)
		{
			this.m_BigChestSecondary.RemoveEventListener(UIEventType.PRESS, new UIEvent.Handler(this.ShowBigChestRewards));
			this.m_BigChestSecondary.RemoveEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.ShowBigChestRewards));
			this.m_BigChestSecondary.RemoveEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.HideBigChestRewards));
		}
	}

	// Token: 0x040003A7 RID: 935
	public NestedPrefab m_secondaryBigChestContainer;

	// Token: 0x040003A8 RID: 936
	public int m_secondaryChestVariation;

	// Token: 0x040003A9 RID: 937
	public TooltipZone m_chestTwoColumnTooltipZone;

	// Token: 0x040003AA RID: 938
	public TooltipZone m_chestNormalTooltipZone;

	// Token: 0x040003AB RID: 939
	public FrozenThroneEventTable m_frozenThroneEventTable;

	// Token: 0x040003AC RID: 940
	public Vector3_MobileOverride m_tooltipOffsetFromReward;

	// Token: 0x040003AD RID: 941
	private AdventureWing m_adventureWing;

	// Token: 0x040003AE RID: 942
	private PegUIElement m_BigChestSecondary;

	// Token: 0x040003AF RID: 943
	private List<global::Achievement> m_classSpecificAchieves;

	// Token: 0x040003B0 RID: 944
	private List<global::Achievement> m_newlyCompletedAchieves;

	// Token: 0x040003B1 RID: 945
	private int m_numClassesAlreadyCompleted;

	// Token: 0x040003B2 RID: 946
	private bool m_needToAnimateBigChest;

	// Token: 0x040003B3 RID: 947
	private TooltipZone m_currentChestTooltipZone;

	// Token: 0x040003B4 RID: 948
	private bool m_waitingForRuneAnimationEnd;
}
