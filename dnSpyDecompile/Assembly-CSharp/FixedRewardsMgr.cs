using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using FixedReward;
using Hearthstone;
using PegasusShared;
using UnityEngine;

// Token: 0x020008A5 RID: 2213
public class FixedRewardsMgr : IService
{
	// Token: 0x06007B0F RID: 31503 RVA: 0x0027E358 File Offset: 0x0027C558
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		HearthstoneApplication.Get().WillReset += this.WillReset;
		HearthstoneApplication.Get().Resetting += this.OnReset;
		serviceLocator.Get<AdventureProgressMgr>().RegisterProgressUpdatedListener(new AdventureProgressMgr.AdventureProgressUpdatedCallback(this.OnAdventureProgressUpdate));
		serviceLocator.Get<NetCache>().RegisterNewNoticesListener(new NetCache.DelNewNoticesListener(this.OnNewNotices));
		serviceLocator.Get<AchieveManager>().RegisterAchievesUpdatedListener(new AchieveManager.AchievesUpdatedCallback(this.OnAchievesUpdated), null);
		serviceLocator.Get<AccountLicenseMgr>().RegisterAccountLicensesChangedListener(new AccountLicenseMgr.AccountLicensesChangedCallback(this.OnAccountLicensesUpdate));
		yield break;
	}

	// Token: 0x06007B10 RID: 31504 RVA: 0x0027E370 File Offset: 0x0027C570
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(AdventureProgressMgr),
			typeof(NetCache),
			typeof(GameDbf),
			typeof(AchieveManager),
			typeof(AccountLicenseMgr),
			typeof(CardBackManager)
		};
	}

	// Token: 0x06007B11 RID: 31505 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x06007B12 RID: 31506 RVA: 0x0027E3D1 File Offset: 0x0027C5D1
	private void WillReset()
	{
		this.m_craftableCardRewards.Clear();
		this.m_earnedMetaActionRewards.Clear();
		this.m_rewardQueue.Clear();
		this.m_rewardMapIDsAwarded.Clear();
		this.m_isStartupFinished = false;
	}

	// Token: 0x06007B13 RID: 31507 RVA: 0x0027E406 File Offset: 0x0027C606
	private void OnReset()
	{
		HearthstoneServices.Get<AdventureProgressMgr>().RegisterProgressUpdatedListener(new AdventureProgressMgr.AdventureProgressUpdatedCallback(this.OnAdventureProgressUpdate));
		HearthstoneServices.Get<AchieveManager>().RegisterAchievesUpdatedListener(new AchieveManager.AchievesUpdatedCallback(this.OnAchievesUpdated), null);
	}

	// Token: 0x06007B14 RID: 31508 RVA: 0x0027E437 File Offset: 0x0027C637
	public static FixedRewardsMgr Get()
	{
		return HearthstoneServices.Get<FixedRewardsMgr>();
	}

	// Token: 0x06007B15 RID: 31509 RVA: 0x0027E440 File Offset: 0x0027C640
	public void InitStartupFixedRewards()
	{
		this.m_rewardMapIDsAwarded.Clear();
		List<CardRewardData> cardRewards = new List<CardRewardData>();
		foreach (AdventureMission.WingProgress wingProgress in AdventureProgressMgr.Get().GetAllProgress())
		{
			if (wingProgress.MeetsFlagsRequirement(1UL))
			{
				this.TriggerWingProgressAction(FixedRewardsMgr.ShowVisualOption.DO_NOT_SHOW, wingProgress.Wing, wingProgress.Progress, cardRewards);
				this.TriggerWingFlagsAction(FixedRewardsMgr.ShowVisualOption.DO_NOT_SHOW, wingProgress.Wing, wingProgress.Flags, cardRewards);
			}
		}
		this.GrantAchieveRewards(cardRewards);
		foreach (AccountLicenseInfo accountLicenseInfo in AccountLicenseMgr.Get().GetAllOwnedAccountLicenseInfo())
		{
			this.TriggerAccountLicenseFlagsAction(FixedRewardsMgr.ShowVisualOption.DO_NOT_SHOW, accountLicenseInfo.License, accountLicenseInfo.Flags_, cardRewards);
		}
		this.m_isStartupFinished = true;
	}

	// Token: 0x06007B16 RID: 31510 RVA: 0x0027E538 File Offset: 0x0027C738
	public void CheckForTutorialComplete()
	{
		List<CardRewardData> cardRewards = new List<CardRewardData>();
		this.CheckForTutorialComplete(cardRewards);
		if (CollectionManager.Get() != null)
		{
			CollectionManager.Get().AddCardRewards(cardRewards, false);
		}
	}

	// Token: 0x06007B17 RID: 31511 RVA: 0x0027E568 File Offset: 0x0027C768
	private void CheckForTutorialComplete(List<CardRewardData> cardRewards)
	{
		NetCache.NetCacheProfileProgress netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileProgress>();
		if (netObject == null)
		{
			Debug.LogWarning("FixedRewardsMgr.CheckForTutorialComplete(): null == NetCache.NetCacheProfileProgress");
			return;
		}
		this.TriggerTutorialProgressAction(FixedRewardsMgr.ShowVisualOption.DO_NOT_SHOW, (int)netObject.CampaignProgress, cardRewards);
	}

	// Token: 0x06007B18 RID: 31512 RVA: 0x0027E59C File Offset: 0x0027C79C
	public bool IsStartupFinished()
	{
		return this.m_isStartupFinished;
	}

	// Token: 0x06007B19 RID: 31513 RVA: 0x0027E5A4 File Offset: 0x0027C7A4
	public bool HasRewardsToShow(IEnumerable<Achieve.RewardTiming> rewardTimings)
	{
		return this.m_rewardQueue.HasRewardsToShow(rewardTimings);
	}

	// Token: 0x06007B1A RID: 31514 RVA: 0x0027E5B4 File Offset: 0x0027C7B4
	public bool ShowFixedRewards(UserAttentionBlocker blocker, HashSet<Achieve.RewardTiming> rewardVisualTimings, FixedRewardsMgr.DelOnAllFixedRewardsShown allRewardsShownCallback, FixedRewardsMgr.DelPositionNonToastReward positionNonToastRewardCallback)
	{
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) || !UserAttentionManager.CanShowAttentionGrabber(blocker, string.Format("FixedRewardsMgr.ShowFixedRewards:{0}", blocker)) || StoreManager.Get().IsPromptShowing)
		{
			return false;
		}
		FixedRewardsMgr.OnAllFixedRewardsShownCallbackInfo onAllFixedRewardsShownCallbackInfo = new FixedRewardsMgr.OnAllFixedRewardsShownCallbackInfo
		{
			rewardMapIDsToShow = new List<RewardMapIDToShow>(),
			onAllRewardsShownCallback = allRewardsShownCallback,
			positionNonToastRewardCallback = positionNonToastRewardCallback,
			showingCheatRewards = false
		};
		foreach (Achieve.RewardTiming timing in rewardVisualTimings)
		{
			HashSet<RewardMapIDToShow> collection;
			if (this.m_rewardQueue.TryGetRewards(timing, out collection))
			{
				onAllFixedRewardsShownCallbackInfo.rewardMapIDsToShow.AddRange(collection);
				this.m_rewardQueue.Clear(timing);
			}
		}
		if (onAllFixedRewardsShownCallbackInfo.rewardMapIDsToShow.Count == 0)
		{
			return false;
		}
		if (PopupDisplayManager.ShouldDisableNotificationOnLogin())
		{
			RewardMapIDToShow rewardMapIDToShow = onAllFixedRewardsShownCallbackInfo.rewardMapIDsToShow[0];
			onAllFixedRewardsShownCallbackInfo.rewardMapIDsToShow.RemoveAt(0);
			if (rewardMapIDToShow.achieveID != RewardMapIDToShow.NoAchieveID)
			{
				global::Achievement achievement = AchieveManager.Get().GetAchievement(rewardMapIDToShow.achieveID);
				if (achievement != null)
				{
					achievement.AckCurrentProgressAndRewardNotices();
				}
			}
			if (onAllFixedRewardsShownCallbackInfo.onAllRewardsShownCallback != null)
			{
				onAllFixedRewardsShownCallbackInfo.onAllRewardsShownCallback();
			}
			return false;
		}
		onAllFixedRewardsShownCallbackInfo.rewardMapIDsToShow.Sort((RewardMapIDToShow a, RewardMapIDToShow b) => a.sortOrder - b.sortOrder);
		this.ShowFixedRewards_Internal(blocker, onAllFixedRewardsShownCallbackInfo);
		return true;
	}

	// Token: 0x06007B1B RID: 31515 RVA: 0x0027E71C File Offset: 0x0027C91C
	public bool Cheat_ShowFixedReward(int fixedRewardMapID, FixedRewardsMgr.DelPositionNonToastReward positionNonToastRewardCallback)
	{
		if (!HearthstoneApplication.IsInternal())
		{
			return false;
		}
		FixedRewardMapDbfRecord record = GameDbf.FixedRewardMap.GetRecord(fixedRewardMapID);
		int sortOrder = (record != null) ? record.SortOrder : 0;
		FixedRewardsMgr.OnAllFixedRewardsShownCallbackInfo callbackInfo = new FixedRewardsMgr.OnAllFixedRewardsShownCallbackInfo
		{
			rewardMapIDsToShow = new List<RewardMapIDToShow>
			{
				new RewardMapIDToShow(fixedRewardMapID, RewardMapIDToShow.NoAchieveID, sortOrder)
			},
			onAllRewardsShownCallback = null,
			positionNonToastRewardCallback = positionNonToastRewardCallback,
			showingCheatRewards = true
		};
		this.ShowFixedRewards_Internal(UserAttentionBlocker.NONE, callbackInfo);
		return true;
	}

	// Token: 0x06007B1C RID: 31516 RVA: 0x0027E78C File Offset: 0x0027C98C
	public bool CanCraftCard(string cardID, TAG_PREMIUM premium)
	{
		if (GameUtils.GetFixedRewardForCard(cardID, premium) == null)
		{
			return true;
		}
		NetCache.CardDefinition item = new NetCache.CardDefinition
		{
			Name = cardID,
			Premium = premium
		};
		if (this.m_craftableCardRewards.Contains(item))
		{
			return true;
		}
		bool flag = GameUtils.IsCardCraftableWhenWild(cardID) && GameUtils.IsWildCard(cardID);
		bool flag2 = GameUtils.IsClassicCard(cardID) && this.CanCraftCard(GameUtils.TranslateDbIdToCardId(GameUtils.GetCardTagValue(cardID, GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID), false), premium);
		return flag || flag2;
	}

	// Token: 0x06007B1D RID: 31517 RVA: 0x0027E800 File Offset: 0x0027CA00
	private void OnAdventureProgressUpdate(bool isStartupAction, AdventureMission.WingProgress oldProgress, AdventureMission.WingProgress newProgress, object userData)
	{
		List<CardRewardData> cardRewards = new List<CardRewardData>();
		if (isStartupAction)
		{
			return;
		}
		if (newProgress == null)
		{
			return;
		}
		if (!newProgress.IsOwned())
		{
			return;
		}
		if (oldProgress == null)
		{
			this.TriggerWingProgressAction(FixedRewardsMgr.ShowVisualOption.SHOW, newProgress.Wing, newProgress.Progress, cardRewards);
			this.TriggerWingFlagsAction(FixedRewardsMgr.ShowVisualOption.SHOW, newProgress.Wing, newProgress.Flags, cardRewards);
		}
		else
		{
			bool flag = !oldProgress.IsOwned() && newProgress.IsOwned();
			if (flag || oldProgress.Progress != newProgress.Progress)
			{
				this.TriggerWingProgressAction(flag ? FixedRewardsMgr.ShowVisualOption.DO_NOT_SHOW : FixedRewardsMgr.ShowVisualOption.SHOW, newProgress.Wing, newProgress.Progress, cardRewards);
			}
			if (oldProgress.Flags != newProgress.Flags)
			{
				this.TriggerWingFlagsAction(FixedRewardsMgr.ShowVisualOption.SHOW, newProgress.Wing, newProgress.Flags, cardRewards);
			}
		}
		CollectionManager.Get().AddCardRewards(cardRewards, false);
	}

	// Token: 0x06007B1E RID: 31518 RVA: 0x0027E8BC File Offset: 0x0027CABC
	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		bool flag = false;
		foreach (NetCache.ProfileNotice profileNotice in newNotices)
		{
			if (NetCache.ProfileNotice.NoticeType.HERO_LEVEL_UP == profileNotice.Type)
			{
				NetCache.ProfileNoticeLevelUp profileNoticeLevelUp = profileNotice as NetCache.ProfileNoticeLevelUp;
				FixedRewardsMgr.ShowVisualOption showRewardVisual = (profileNotice.Origin == NetCache.ProfileNotice.NoticeOrigin.LEVEL_UP) ? FixedRewardsMgr.ShowVisualOption.SHOW : FixedRewardsMgr.ShowVisualOption.DO_NOT_SHOW;
				this.TriggerHeroLevelAction(showRewardVisual, profileNoticeLevelUp.HeroClass, profileNoticeLevelUp.NewLevel);
				this.TriggerTotalHeroLevelAction(showRewardVisual, profileNoticeLevelUp.TotalLevel);
				Network.Get().AckNotice(profileNotice.NoticeID);
			}
			else if (NetCache.ProfileNotice.NoticeType.DECK_GRANTED == profileNotice.Type)
			{
				flag = true;
			}
		}
		if (CollectionManager.Get() != null && flag && SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER)
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		}
	}

	// Token: 0x06007B1F RID: 31519 RVA: 0x0027E98C File Offset: 0x0027CB8C
	private void OnAchievesUpdated(List<global::Achievement> updatedAchieves, List<global::Achievement> achieves, object userData)
	{
		AchieveManager achieveManager = AchieveManager.Get();
		List<CardRewardData> cardRewards = new List<CardRewardData>();
		foreach (global::Achievement achievement in achieves)
		{
			FixedRewardsMgr.ShowVisualOption showRewardVisual = achieveManager.IsAchievementVisuallyBlocklisted(achievement.ID) ? FixedRewardsMgr.ShowVisualOption.DO_NOT_SHOW : FixedRewardsMgr.ShowVisualOption.SHOW;
			this.TriggerAchieveAction(showRewardVisual, achievement.ID, cardRewards);
		}
		if (CollectionManager.Get() != null)
		{
			CollectionManager.Get().AddCardRewards(cardRewards, false);
		}
	}

	// Token: 0x06007B20 RID: 31520 RVA: 0x0027EA18 File Offset: 0x0027CC18
	private void OnAccountLicensesUpdate(List<AccountLicenseInfo> changedAccountLicenses, object userData)
	{
		List<CardRewardData> cardRewards = new List<CardRewardData>();
		foreach (AccountLicenseInfo accountLicenseInfo in changedAccountLicenses)
		{
			if (AccountLicenseMgr.Get().OwnsAccountLicense(accountLicenseInfo))
			{
				this.TriggerAccountLicenseFlagsAction(FixedRewardsMgr.ShowVisualOption.FORCE_SHOW, accountLicenseInfo.License, accountLicenseInfo.Flags_, cardRewards);
			}
		}
		CollectionManager.Get().AddCardRewards(cardRewards, false);
	}

	// Token: 0x06007B21 RID: 31521 RVA: 0x0027EA94 File Offset: 0x0027CC94
	private MetaAction GetEarnedMetaActionReward(int metaActionID)
	{
		if (!this.m_earnedMetaActionRewards.ContainsKey(metaActionID))
		{
			this.m_earnedMetaActionRewards[metaActionID] = new MetaAction(metaActionID);
		}
		return this.m_earnedMetaActionRewards[metaActionID];
	}

	// Token: 0x06007B22 RID: 31522 RVA: 0x0027EAC2 File Offset: 0x0027CCC2
	private void UpdateEarnedMetaActionFlags(int metaActionID, ulong addFlags, ulong removeFlags)
	{
		this.GetEarnedMetaActionReward(metaActionID).UpdateFlags(addFlags, removeFlags);
	}

	// Token: 0x06007B23 RID: 31523 RVA: 0x0027EAD4 File Offset: 0x0027CCD4
	private bool QueueRewardVisual(FixedRewardMapDbfRecord record, int achieveID)
	{
		Achieve.RewardTiming rewardTiming = record.GetRewardTiming();
		Log.Achievements.Print(string.Format("QueueRewardVisual achieveID={0} fixedRewardMapId={1} {2} {3}", new object[]
		{
			achieveID,
			record.ID,
			record.NoteDesc,
			rewardTiming
		}), Array.Empty<object>());
		FixedReward.Reward fixedReward = record.GetFixedReward();
		if (FixedRewardUtils.ShouldSkipRewardVisual(rewardTiming, fixedReward))
		{
			return false;
		}
		this.m_rewardQueue.Add(rewardTiming, new RewardMapIDToShow(record.ID, achieveID, record.SortOrder));
		return true;
	}

	// Token: 0x06007B24 RID: 31524 RVA: 0x0027EB61 File Offset: 0x0027CD61
	private void TriggerRewardsForAction(int actionID, FixedRewardsMgr.ShowVisualOption showRewardVisual, List<CardRewardData> cardRewards)
	{
		this.TriggerRewardsForAction(actionID, showRewardVisual, cardRewards, RewardMapIDToShow.NoAchieveID);
	}

	// Token: 0x06007B25 RID: 31525 RVA: 0x0027EB74 File Offset: 0x0027CD74
	private void TriggerRewardsForAction(int actionID, FixedRewardsMgr.ShowVisualOption showRewardVisual, List<CardRewardData> cardRewards, int achieveID)
	{
		foreach (FixedRewardMapDbfRecord fixedRewardMapDbfRecord in GameUtils.GetFixedRewardMapRecordsForAction(actionID))
		{
			FixedReward.Reward fixedReward = fixedRewardMapDbfRecord.GetFixedReward();
			int id = fixedRewardMapDbfRecord.ID;
			if (this.m_rewardMapIDsAwarded.Contains(id))
			{
				if (showRewardVisual != FixedRewardsMgr.ShowVisualOption.FORCE_SHOW)
				{
					continue;
				}
			}
			else
			{
				this.m_rewardMapIDsAwarded.Add(id);
			}
			if (fixedRewardMapDbfRecord.RewardCount > 0)
			{
				bool flag = showRewardVisual > FixedRewardsMgr.ShowVisualOption.DO_NOT_SHOW;
				if (fixedReward.FixedCardRewardData != null && (!flag || !this.QueueRewardVisual(fixedRewardMapDbfRecord, achieveID)))
				{
					cardRewards.Add(fixedReward.FixedCardRewardData);
				}
				if (fixedReward.FixedCardBackRewardData != null && (!flag || !this.QueueRewardVisual(fixedRewardMapDbfRecord, achieveID)))
				{
					CardBackManager.Get().AddNewCardBack(fixedReward.FixedCardBackRewardData.CardBackID);
				}
				if (fixedReward.FixedCraftableCardRewardData != null)
				{
					this.m_craftableCardRewards.Add(fixedReward.FixedCraftableCardRewardData);
				}
				if (fixedReward.MetaActionData != null)
				{
					this.UpdateEarnedMetaActionFlags(fixedReward.MetaActionData.MetaActionID, fixedReward.MetaActionData.MetaActionFlags, 0UL);
					this.TriggerMetaActionFlagsAction(showRewardVisual, fixedReward.MetaActionData.MetaActionID, cardRewards);
				}
			}
		}
	}

	// Token: 0x06007B26 RID: 31526 RVA: 0x0027ECB8 File Offset: 0x0027CEB8
	private void TriggerWingProgressAction(FixedRewardsMgr.ShowVisualOption showRewardVisual, int wingID, int progress, List<CardRewardData> cardRewards)
	{
		foreach (FixedRewardActionDbfRecord fixedRewardActionDbfRecord in GameUtils.GetFixedActionRecords(FixedRewardAction.Type.WING_PROGRESS))
		{
			if (fixedRewardActionDbfRecord.WingId == wingID && fixedRewardActionDbfRecord.WingProgress <= progress && SpecialEventManager.Get().IsEventActive(fixedRewardActionDbfRecord.ActiveEvent, false))
			{
				this.TriggerRewardsForAction(fixedRewardActionDbfRecord.ID, showRewardVisual, cardRewards);
			}
		}
	}

	// Token: 0x06007B27 RID: 31527 RVA: 0x0027ED38 File Offset: 0x0027CF38
	private void TriggerWingFlagsAction(FixedRewardsMgr.ShowVisualOption showRewardVisual, int wingID, ulong flags, List<CardRewardData> cardRewards)
	{
		foreach (FixedRewardActionDbfRecord fixedRewardActionDbfRecord in GameUtils.GetFixedActionRecords(FixedRewardAction.Type.WING_FLAGS))
		{
			if (fixedRewardActionDbfRecord.WingId == wingID)
			{
				ulong wingFlags = fixedRewardActionDbfRecord.WingFlags;
				if ((wingFlags & flags) == wingFlags && SpecialEventManager.Get().IsEventActive(fixedRewardActionDbfRecord.ActiveEvent, false))
				{
					this.TriggerRewardsForAction(fixedRewardActionDbfRecord.ID, showRewardVisual, cardRewards);
				}
			}
		}
	}

	// Token: 0x06007B28 RID: 31528 RVA: 0x0027EDBC File Offset: 0x0027CFBC
	private void TriggerAchieveAction(FixedRewardsMgr.ShowVisualOption showRewardVisual, int achieveId, List<CardRewardData> cardRewards)
	{
		foreach (FixedRewardActionDbfRecord fixedRewardActionDbfRecord in GameUtils.GetFixedActionRecords(FixedRewardAction.Type.ACHIEVE))
		{
			if (fixedRewardActionDbfRecord.AchieveId == achieveId && SpecialEventManager.Get().IsEventActive(fixedRewardActionDbfRecord.ActiveEvent, false))
			{
				this.TriggerRewardsForAction(fixedRewardActionDbfRecord.ID, showRewardVisual, cardRewards, achieveId);
			}
		}
	}

	// Token: 0x06007B29 RID: 31529 RVA: 0x0027EE34 File Offset: 0x0027D034
	private void TriggerTotalHeroLevelAction(FixedRewardsMgr.ShowVisualOption showRewardVisual, int totalHeroLevel)
	{
		List<CardRewardData> cardRewards = new List<CardRewardData>();
		foreach (FixedRewardActionDbfRecord fixedRewardActionDbfRecord in GameUtils.GetFixedActionRecords(FixedRewardAction.Type.TOTAL_HERO_LEVEL))
		{
			if (fixedRewardActionDbfRecord.TotalHeroLevel == totalHeroLevel && SpecialEventManager.Get().IsEventActive(fixedRewardActionDbfRecord.ActiveEvent, false))
			{
				this.TriggerRewardsForAction(fixedRewardActionDbfRecord.ID, showRewardVisual, cardRewards);
			}
		}
	}

	// Token: 0x06007B2A RID: 31530 RVA: 0x0027EEB4 File Offset: 0x0027D0B4
	private void TriggerHeroLevelAction(FixedRewardsMgr.ShowVisualOption showRewardVisual, int classID, int heroLevel)
	{
		List<CardRewardData> cardRewards = new List<CardRewardData>();
		foreach (FixedRewardActionDbfRecord fixedRewardActionDbfRecord in GameUtils.GetFixedActionRecords(FixedRewardAction.Type.HERO_LEVEL))
		{
			if (fixedRewardActionDbfRecord.ClassId == classID && fixedRewardActionDbfRecord.HeroLevel == heroLevel && SpecialEventManager.Get().IsEventActive(fixedRewardActionDbfRecord.ActiveEvent, false))
			{
				this.TriggerRewardsForAction(fixedRewardActionDbfRecord.ID, showRewardVisual, cardRewards);
			}
		}
	}

	// Token: 0x06007B2B RID: 31531 RVA: 0x0027EF3C File Offset: 0x0027D13C
	private void TriggerTutorialProgressAction(FixedRewardsMgr.ShowVisualOption showRewardVisual, int tutorialProgress, List<CardRewardData> cardRewards)
	{
		foreach (FixedRewardActionDbfRecord fixedRewardActionDbfRecord in GameUtils.GetFixedActionRecords(FixedRewardAction.Type.TUTORIAL_PROGRESS))
		{
			if (fixedRewardActionDbfRecord.TutorialProgress <= tutorialProgress && SpecialEventManager.Get().IsEventActive(fixedRewardActionDbfRecord.ActiveEvent, false))
			{
				this.TriggerRewardsForAction(fixedRewardActionDbfRecord.ID, showRewardVisual, cardRewards);
			}
		}
	}

	// Token: 0x06007B2C RID: 31532 RVA: 0x0027EFB4 File Offset: 0x0027D1B4
	private void TriggerAccountLicenseFlagsAction(FixedRewardsMgr.ShowVisualOption showRewardVisual, long license, ulong flags, List<CardRewardData> cardRewards)
	{
		foreach (FixedRewardActionDbfRecord fixedRewardActionDbfRecord in GameUtils.GetFixedActionRecords(FixedRewardAction.Type.ACCOUNT_LICENSE_FLAGS))
		{
			if (fixedRewardActionDbfRecord.AccountLicenseId == license)
			{
				ulong accountLicenseFlags = fixedRewardActionDbfRecord.AccountLicenseFlags;
				if ((accountLicenseFlags & flags) == accountLicenseFlags && SpecialEventManager.Get().IsEventActive(fixedRewardActionDbfRecord.ActiveEvent, false))
				{
					this.TriggerRewardsForAction(fixedRewardActionDbfRecord.ID, showRewardVisual, cardRewards);
				}
			}
		}
	}

	// Token: 0x06007B2D RID: 31533 RVA: 0x0027F038 File Offset: 0x0027D238
	private void TriggerMetaActionFlagsAction(FixedRewardsMgr.ShowVisualOption showRewardVisual, int metaActionID, List<CardRewardData> cardRewards)
	{
		FixedRewardActionDbfRecord record = GameDbf.FixedRewardAction.GetRecord(metaActionID);
		if (record == null)
		{
			return;
		}
		ulong metaActionFlags = record.MetaActionFlags;
		if (!this.GetEarnedMetaActionReward(metaActionID).HasAllRequiredFlags(metaActionFlags))
		{
			return;
		}
		if (!SpecialEventManager.Get().IsEventActive(record.ActiveEvent, false))
		{
			return;
		}
		this.TriggerRewardsForAction(metaActionID, showRewardVisual, cardRewards);
	}

	// Token: 0x06007B2E RID: 31534 RVA: 0x0027F08C File Offset: 0x0027D28C
	private void ShowFixedRewards_Internal(UserAttentionBlocker blocker, FixedRewardsMgr.OnAllFixedRewardsShownCallbackInfo callbackInfo)
	{
		if (callbackInfo.rewardMapIDsToShow.Count == 0)
		{
			if (callbackInfo.onAllRewardsShownCallback != null)
			{
				callbackInfo.onAllRewardsShownCallback();
			}
			return;
		}
		RewardMapIDToShow rewardMapIDToShow = callbackInfo.rewardMapIDsToShow[0];
		callbackInfo.rewardMapIDsToShow.RemoveAt(0);
		FixedRewardMapDbfRecord record = GameDbf.FixedRewardMap.GetRecord(rewardMapIDToShow.rewardMapID);
		FixedReward.Reward fixedReward = record.GetFixedReward();
		RewardData rewardData = null;
		if (fixedReward.FixedCardRewardData != null)
		{
			rewardData = fixedReward.FixedCardRewardData;
		}
		else if (fixedReward.FixedCardBackRewardData != null)
		{
			rewardData = fixedReward.FixedCardBackRewardData;
		}
		Log.Achievements.Print("Showing Fixed Reward: " + rewardMapIDToShow.achieveID, Array.Empty<object>());
		if (rewardData == null)
		{
			this.ShowFixedRewards_Internal(blocker, callbackInfo);
			return;
		}
		if (callbackInfo.showingCheatRewards)
		{
			rewardData.MarkAsDummyReward();
		}
		if (rewardMapIDToShow.achieveID != RewardMapIDToShow.NoAchieveID)
		{
			global::Achievement achievement = AchieveManager.Get().GetAchievement(rewardMapIDToShow.achieveID);
			if (achievement != null)
			{
				achievement.AckCurrentProgressAndRewardNotices();
			}
		}
		if (record.UseQuestToast)
		{
			string name = record.ToastName;
			string description = record.ToastDescription;
			QuestToast.ShowFixedRewardQuestToast(blocker, delegate(object userData)
			{
				this.ShowFixedRewards_Internal(blocker, callbackInfo);
			}, rewardData, name, description);
			return;
		}
		rewardData.LoadRewardObject(delegate(global::Reward reward, object callbackData)
		{
			reward.transform.localPosition = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(0f, 0f, 43f),
				Phone = new Vector3(0f, 0f, 35f)
			};
			PlatformDependentValue<Vector3> val = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(27.6f, 27.6f, 27.6f),
				Phone = new Vector3(26.4f, 26.4f, 26.4f)
			};
			PlatformDependentValue<Vector3> val2 = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(23f, 23f, 23f),
				Phone = new Vector3(22f, 22f, 22f)
			};
			OverlayUI.Get().AddGameObject(reward.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
			SceneUtils.SetLayer(reward.gameObject, GameLayer.UI);
			if (callbackInfo.positionNonToastRewardCallback != null)
			{
				callbackInfo.positionNonToastRewardCallback(reward);
			}
			bool updateCacheValues = true;
			RewardUtils.ShowReward(blocker, reward, updateCacheValues, val, val2, delegate(object showRewardUserData)
			{
				reward.RegisterClickListener(new global::Reward.OnClickedCallback(this.OnNonToastRewardClicked), callbackInfo);
				reward.EnableClickCatcher(true);
			}, null);
		});
	}

	// Token: 0x06007B2F RID: 31535 RVA: 0x0027F210 File Offset: 0x0027D410
	private void OnNonToastRewardClicked(global::Reward reward, object userData)
	{
		FixedRewardsMgr.OnAllFixedRewardsShownCallbackInfo onAllFixedRewardsShownCallbackInfo = userData as FixedRewardsMgr.OnAllFixedRewardsShownCallbackInfo;
		reward.RemoveClickListener(new global::Reward.OnClickedCallback(this.OnNonToastRewardClicked), onAllFixedRewardsShownCallbackInfo);
		reward.Hide(true);
		this.ShowFixedRewards_Internal(UserAttentionBlocker.NONE, onAllFixedRewardsShownCallbackInfo);
	}

	// Token: 0x06007B30 RID: 31536 RVA: 0x0027F248 File Offset: 0x0027D448
	private void GrantAchieveRewards(List<CardRewardData> cardRewards)
	{
		AchieveManager achieveManager = AchieveManager.Get();
		if (achieveManager == null)
		{
			Debug.LogWarning("FixedRewardsMgr.GrantAchieveRewards(): null == AchieveManager.Get()");
			return;
		}
		foreach (global::Achievement achievement in achieveManager.GetCompletedAchieves())
		{
			FixedRewardsMgr.ShowVisualOption showRewardVisual = achievement.IsNewlyCompleted() ? FixedRewardsMgr.ShowVisualOption.SHOW : FixedRewardsMgr.ShowVisualOption.DO_NOT_SHOW;
			this.TriggerAchieveAction(showRewardVisual, achievement.ID, cardRewards);
		}
	}

	// Token: 0x06007B31 RID: 31537 RVA: 0x0027F2C0 File Offset: 0x0027D4C0
	public RewardData GetNextHeroLevelReward(TAG_CLASS classID, int currentHeroLevel, out int nextRewardLevel)
	{
		List<RewardData> list = new List<RewardData>();
		List<FixedRewardActionDbfRecord> fixedActionRecords = GameUtils.GetFixedActionRecords(FixedRewardAction.Type.HERO_LEVEL);
		FixedRewardActionDbfRecord fixedRewardActionDbfRecord = null;
		nextRewardLevel = 0;
		int num = int.MaxValue;
		foreach (FixedRewardActionDbfRecord fixedRewardActionDbfRecord2 in fixedActionRecords)
		{
			if (fixedRewardActionDbfRecord2.ClassId == (int)classID && SpecialEventManager.Get().IsEventActive(fixedRewardActionDbfRecord2.ActiveEvent, false) && fixedRewardActionDbfRecord2.HeroLevel > currentHeroLevel && fixedRewardActionDbfRecord2.HeroLevel - currentHeroLevel < num)
			{
				num = fixedRewardActionDbfRecord2.HeroLevel - currentHeroLevel;
				fixedRewardActionDbfRecord = fixedRewardActionDbfRecord2;
			}
		}
		if (fixedRewardActionDbfRecord == null)
		{
			return null;
		}
		nextRewardLevel = fixedRewardActionDbfRecord.HeroLevel;
		foreach (FixedRewardMapDbfRecord fixedRewardMapDbfRecord in GameUtils.GetFixedRewardMapRecordsForAction(fixedRewardActionDbfRecord.ID))
		{
			if (fixedRewardMapDbfRecord.RewardRecord != null)
			{
				FixedReward.Reward fixedReward = fixedRewardMapDbfRecord.GetFixedReward();
				if (fixedReward.FixedCardRewardData != null)
				{
					list.Add(fixedReward.FixedCardRewardData);
				}
			}
		}
		if (list.Count == 0)
		{
			Debug.LogFormat("No subsequent reward found for Hero Class: {0} after Level: {1}. Check FIXED REWARD MAPS if you think there should be one", new object[]
			{
				classID.ToString(),
				currentHeroLevel
			});
			return null;
		}
		if (list.Count > 1)
		{
			Debug.LogWarningFormat("More than one reward listed for the subsequent reward for Hero Class: {0} after Level: {1}. Check FIXED REWARD ACTIONS and FIXED REWARD MAPS to ensure there is only one reward per level", new object[]
			{
				classID.ToString(),
				currentHeroLevel
			});
		}
		return list[0];
	}

	// Token: 0x06007B32 RID: 31538 RVA: 0x0027F444 File Offset: 0x0027D644
	public RewardData GetNextTotalLevelReward(int currentTotalLevel, out int nextRewardLevel)
	{
		List<RewardData> list = new List<RewardData>();
		List<FixedRewardActionDbfRecord> fixedActionRecords = GameUtils.GetFixedActionRecords(FixedRewardAction.Type.TOTAL_HERO_LEVEL);
		FixedRewardActionDbfRecord fixedRewardActionDbfRecord = null;
		nextRewardLevel = 0;
		int num = int.MaxValue;
		foreach (FixedRewardActionDbfRecord fixedRewardActionDbfRecord2 in fixedActionRecords)
		{
			if (SpecialEventManager.Get().IsEventActive(fixedRewardActionDbfRecord2.ActiveEvent, false) && fixedRewardActionDbfRecord2.TotalHeroLevel > currentTotalLevel && fixedRewardActionDbfRecord2.TotalHeroLevel - currentTotalLevel < num)
			{
				num = fixedRewardActionDbfRecord2.TotalHeroLevel - currentTotalLevel;
				fixedRewardActionDbfRecord = fixedRewardActionDbfRecord2;
			}
		}
		if (fixedRewardActionDbfRecord == null)
		{
			return null;
		}
		nextRewardLevel = fixedRewardActionDbfRecord.TotalHeroLevel;
		foreach (FixedRewardMapDbfRecord fixedRewardMapDbfRecord in GameUtils.GetFixedRewardMapRecordsForAction(fixedRewardActionDbfRecord.ID))
		{
			if (fixedRewardMapDbfRecord.RewardRecord != null)
			{
				FixedReward.Reward fixedReward = fixedRewardMapDbfRecord.GetFixedReward();
				if (fixedReward.FixedCardRewardData != null)
				{
					list.Add(fixedReward.FixedCardRewardData);
				}
			}
		}
		if (list.Count == 0)
		{
			Debug.LogFormat("No subsequent reward found for after Total Level: {0}. Check FIXED REWARD MAPS if you think there should be one", new object[]
			{
				currentTotalLevel
			});
			return null;
		}
		if (list.Count > 1)
		{
			Debug.LogErrorFormat("More than one reward listed for the subsequent reward after Total Level: {0}. Check FIXED REWARD ACTIONS and FIXED REWARD MAPS to ensure there is only one reward per level", new object[]
			{
				currentTotalLevel
			});
		}
		return list[0];
	}

	// Token: 0x04005EDB RID: 24283
	private readonly HashSet<NetCache.CardDefinition> m_craftableCardRewards = new HashSet<NetCache.CardDefinition>();

	// Token: 0x04005EDC RID: 24284
	private readonly Map<int, MetaAction> m_earnedMetaActionRewards = new Map<int, MetaAction>();

	// Token: 0x04005EDD RID: 24285
	private readonly RewardQueue m_rewardQueue = new RewardQueue();

	// Token: 0x04005EDE RID: 24286
	private readonly HashSet<int> m_rewardMapIDsAwarded = new HashSet<int>();

	// Token: 0x04005EDF RID: 24287
	private bool m_isStartupFinished;

	// Token: 0x0200252C RID: 9516
	// (Invoke) Token: 0x0601322F RID: 78383
	public delegate void DelOnAllFixedRewardsShown();

	// Token: 0x0200252D RID: 9517
	// (Invoke) Token: 0x06013233 RID: 78387
	public delegate void DelPositionNonToastReward(global::Reward reward);

	// Token: 0x0200252E RID: 9518
	private class OnAllFixedRewardsShownCallbackInfo
	{
		// Token: 0x0400ECD4 RID: 60628
		public List<RewardMapIDToShow> rewardMapIDsToShow;

		// Token: 0x0400ECD5 RID: 60629
		public FixedRewardsMgr.DelOnAllFixedRewardsShown onAllRewardsShownCallback;

		// Token: 0x0400ECD6 RID: 60630
		public FixedRewardsMgr.DelPositionNonToastReward positionNonToastRewardCallback;

		// Token: 0x0400ECD7 RID: 60631
		public bool showingCheatRewards;
	}

	// Token: 0x0200252F RID: 9519
	private enum ShowVisualOption
	{
		// Token: 0x0400ECD9 RID: 60633
		DO_NOT_SHOW,
		// Token: 0x0400ECDA RID: 60634
		SHOW,
		// Token: 0x0400ECDB RID: 60635
		FORCE_SHOW
	}
}
