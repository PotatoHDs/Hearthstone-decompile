using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using FixedReward;
using Hearthstone;
using PegasusShared;
using UnityEngine;

public class FixedRewardsMgr : IService
{
	public delegate void DelOnAllFixedRewardsShown();

	public delegate void DelPositionNonToastReward(Reward reward);

	private class OnAllFixedRewardsShownCallbackInfo
	{
		public List<RewardMapIDToShow> rewardMapIDsToShow;

		public DelOnAllFixedRewardsShown onAllRewardsShownCallback;

		public DelPositionNonToastReward positionNonToastRewardCallback;

		public bool showingCheatRewards;
	}

	private enum ShowVisualOption
	{
		DO_NOT_SHOW,
		SHOW,
		FORCE_SHOW
	}

	private readonly HashSet<NetCache.CardDefinition> m_craftableCardRewards = new HashSet<NetCache.CardDefinition>();

	private readonly Map<int, MetaAction> m_earnedMetaActionRewards = new Map<int, MetaAction>();

	private readonly RewardQueue m_rewardQueue = new RewardQueue();

	private readonly HashSet<int> m_rewardMapIDsAwarded = new HashSet<int>();

	private bool m_isStartupFinished;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		HearthstoneApplication.Get().WillReset += WillReset;
		HearthstoneApplication.Get().Resetting += OnReset;
		serviceLocator.Get<AdventureProgressMgr>().RegisterProgressUpdatedListener(OnAdventureProgressUpdate);
		serviceLocator.Get<NetCache>().RegisterNewNoticesListener(OnNewNotices);
		serviceLocator.Get<AchieveManager>().RegisterAchievesUpdatedListener(OnAchievesUpdated);
		serviceLocator.Get<AccountLicenseMgr>().RegisterAccountLicensesChangedListener(OnAccountLicensesUpdate);
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[6]
		{
			typeof(AdventureProgressMgr),
			typeof(NetCache),
			typeof(GameDbf),
			typeof(AchieveManager),
			typeof(AccountLicenseMgr),
			typeof(CardBackManager)
		};
	}

	public void Shutdown()
	{
	}

	private void WillReset()
	{
		m_craftableCardRewards.Clear();
		m_earnedMetaActionRewards.Clear();
		m_rewardQueue.Clear();
		m_rewardMapIDsAwarded.Clear();
		m_isStartupFinished = false;
	}

	private void OnReset()
	{
		HearthstoneServices.Get<AdventureProgressMgr>().RegisterProgressUpdatedListener(OnAdventureProgressUpdate);
		HearthstoneServices.Get<AchieveManager>().RegisterAchievesUpdatedListener(OnAchievesUpdated);
	}

	public static FixedRewardsMgr Get()
	{
		return HearthstoneServices.Get<FixedRewardsMgr>();
	}

	public void InitStartupFixedRewards()
	{
		m_rewardMapIDsAwarded.Clear();
		List<CardRewardData> cardRewards = new List<CardRewardData>();
		foreach (AdventureMission.WingProgress item in AdventureProgressMgr.Get().GetAllProgress())
		{
			if (item.MeetsFlagsRequirement(1uL))
			{
				TriggerWingProgressAction(ShowVisualOption.DO_NOT_SHOW, item.Wing, item.Progress, cardRewards);
				TriggerWingFlagsAction(ShowVisualOption.DO_NOT_SHOW, item.Wing, item.Flags, cardRewards);
			}
		}
		GrantAchieveRewards(cardRewards);
		foreach (AccountLicenseInfo item2 in AccountLicenseMgr.Get().GetAllOwnedAccountLicenseInfo())
		{
			TriggerAccountLicenseFlagsAction(ShowVisualOption.DO_NOT_SHOW, item2.License, item2.Flags_, cardRewards);
		}
		m_isStartupFinished = true;
	}

	public void CheckForTutorialComplete()
	{
		List<CardRewardData> cardRewards = new List<CardRewardData>();
		CheckForTutorialComplete(cardRewards);
		if (CollectionManager.Get() != null)
		{
			CollectionManager.Get().AddCardRewards(cardRewards, markAsNew: false);
		}
	}

	private void CheckForTutorialComplete(List<CardRewardData> cardRewards)
	{
		NetCache.NetCacheProfileProgress netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileProgress>();
		if (netObject == null)
		{
			Debug.LogWarning("FixedRewardsMgr.CheckForTutorialComplete(): null == NetCache.NetCacheProfileProgress");
		}
		else
		{
			TriggerTutorialProgressAction(ShowVisualOption.DO_NOT_SHOW, (int)netObject.CampaignProgress, cardRewards);
		}
	}

	public bool IsStartupFinished()
	{
		return m_isStartupFinished;
	}

	public bool HasRewardsToShow(IEnumerable<Achieve.RewardTiming> rewardTimings)
	{
		return m_rewardQueue.HasRewardsToShow(rewardTimings);
	}

	public bool ShowFixedRewards(UserAttentionBlocker blocker, HashSet<Achieve.RewardTiming> rewardVisualTimings, DelOnAllFixedRewardsShown allRewardsShownCallback, DelPositionNonToastReward positionNonToastRewardCallback)
	{
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) || !UserAttentionManager.CanShowAttentionGrabber(blocker, $"FixedRewardsMgr.ShowFixedRewards:{blocker}") || StoreManager.Get().IsPromptShowing)
		{
			return false;
		}
		OnAllFixedRewardsShownCallbackInfo onAllFixedRewardsShownCallbackInfo = new OnAllFixedRewardsShownCallbackInfo
		{
			rewardMapIDsToShow = new List<RewardMapIDToShow>(),
			onAllRewardsShownCallback = allRewardsShownCallback,
			positionNonToastRewardCallback = positionNonToastRewardCallback,
			showingCheatRewards = false
		};
		foreach (Achieve.RewardTiming rewardVisualTiming in rewardVisualTimings)
		{
			if (m_rewardQueue.TryGetRewards(rewardVisualTiming, out var rewards))
			{
				onAllFixedRewardsShownCallbackInfo.rewardMapIDsToShow.AddRange(rewards);
				m_rewardQueue.Clear(rewardVisualTiming);
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
				AchieveManager.Get().GetAchievement(rewardMapIDToShow.achieveID)?.AckCurrentProgressAndRewardNotices();
			}
			if (onAllFixedRewardsShownCallbackInfo.onAllRewardsShownCallback != null)
			{
				onAllFixedRewardsShownCallbackInfo.onAllRewardsShownCallback();
			}
			return false;
		}
		onAllFixedRewardsShownCallbackInfo.rewardMapIDsToShow.Sort((RewardMapIDToShow a, RewardMapIDToShow b) => a.sortOrder - b.sortOrder);
		ShowFixedRewards_Internal(blocker, onAllFixedRewardsShownCallbackInfo);
		return true;
	}

	public bool Cheat_ShowFixedReward(int fixedRewardMapID, DelPositionNonToastReward positionNonToastRewardCallback)
	{
		if (!HearthstoneApplication.IsInternal())
		{
			return false;
		}
		int sortOrder = GameDbf.FixedRewardMap.GetRecord(fixedRewardMapID)?.SortOrder ?? 0;
		OnAllFixedRewardsShownCallbackInfo callbackInfo = new OnAllFixedRewardsShownCallbackInfo
		{
			rewardMapIDsToShow = new List<RewardMapIDToShow>
			{
				new RewardMapIDToShow(fixedRewardMapID, RewardMapIDToShow.NoAchieveID, sortOrder)
			},
			onAllRewardsShownCallback = null,
			positionNonToastRewardCallback = positionNonToastRewardCallback,
			showingCheatRewards = true
		};
		ShowFixedRewards_Internal(UserAttentionBlocker.NONE, callbackInfo);
		return true;
	}

	public bool CanCraftCard(string cardID, TAG_PREMIUM premium)
	{
		if (GameUtils.GetFixedRewardForCard(cardID, premium) != null)
		{
			NetCache.CardDefinition item = new NetCache.CardDefinition
			{
				Name = cardID,
				Premium = premium
			};
			if (m_craftableCardRewards.Contains(item))
			{
				return true;
			}
			bool num = GameUtils.IsCardCraftableWhenWild(cardID) && GameUtils.IsWildCard(cardID);
			bool flag = GameUtils.IsClassicCard(cardID) && CanCraftCard(GameUtils.TranslateDbIdToCardId(GameUtils.GetCardTagValue(cardID, GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID)), premium);
			return num || flag;
		}
		return true;
	}

	private void OnAdventureProgressUpdate(bool isStartupAction, AdventureMission.WingProgress oldProgress, AdventureMission.WingProgress newProgress, object userData)
	{
		List<CardRewardData> cardRewards = new List<CardRewardData>();
		if (isStartupAction || newProgress == null || !newProgress.IsOwned())
		{
			return;
		}
		if (oldProgress == null)
		{
			TriggerWingProgressAction(ShowVisualOption.SHOW, newProgress.Wing, newProgress.Progress, cardRewards);
			TriggerWingFlagsAction(ShowVisualOption.SHOW, newProgress.Wing, newProgress.Flags, cardRewards);
		}
		else
		{
			bool flag = !oldProgress.IsOwned() && newProgress.IsOwned();
			if (flag || oldProgress.Progress != newProgress.Progress)
			{
				TriggerWingProgressAction((!flag) ? ShowVisualOption.SHOW : ShowVisualOption.DO_NOT_SHOW, newProgress.Wing, newProgress.Progress, cardRewards);
			}
			if (oldProgress.Flags != newProgress.Flags)
			{
				TriggerWingFlagsAction(ShowVisualOption.SHOW, newProgress.Wing, newProgress.Flags, cardRewards);
			}
		}
		CollectionManager.Get().AddCardRewards(cardRewards, markAsNew: false);
	}

	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		bool flag = false;
		foreach (NetCache.ProfileNotice newNotice in newNotices)
		{
			if (NetCache.ProfileNotice.NoticeType.HERO_LEVEL_UP == newNotice.Type)
			{
				NetCache.ProfileNoticeLevelUp profileNoticeLevelUp = newNotice as NetCache.ProfileNoticeLevelUp;
				ShowVisualOption showRewardVisual = ((newNotice.Origin == NetCache.ProfileNotice.NoticeOrigin.LEVEL_UP) ? ShowVisualOption.SHOW : ShowVisualOption.DO_NOT_SHOW);
				TriggerHeroLevelAction(showRewardVisual, profileNoticeLevelUp.HeroClass, profileNoticeLevelUp.NewLevel);
				TriggerTotalHeroLevelAction(showRewardVisual, profileNoticeLevelUp.TotalLevel);
				Network.Get().AckNotice(newNotice.NoticeID);
			}
			else if (NetCache.ProfileNotice.NoticeType.DECK_GRANTED == newNotice.Type)
			{
				flag = true;
			}
		}
		if (CollectionManager.Get() != null && flag && SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER)
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
		}
	}

	private void OnAchievesUpdated(List<Achievement> updatedAchieves, List<Achievement> achieves, object userData)
	{
		AchieveManager achieveManager = AchieveManager.Get();
		List<CardRewardData> cardRewards = new List<CardRewardData>();
		foreach (Achievement achiefe in achieves)
		{
			ShowVisualOption showRewardVisual = ((!achieveManager.IsAchievementVisuallyBlocklisted(achiefe.ID)) ? ShowVisualOption.SHOW : ShowVisualOption.DO_NOT_SHOW);
			TriggerAchieveAction(showRewardVisual, achiefe.ID, cardRewards);
		}
		if (CollectionManager.Get() != null)
		{
			CollectionManager.Get().AddCardRewards(cardRewards, markAsNew: false);
		}
	}

	private void OnAccountLicensesUpdate(List<AccountLicenseInfo> changedAccountLicenses, object userData)
	{
		List<CardRewardData> cardRewards = new List<CardRewardData>();
		foreach (AccountLicenseInfo changedAccountLicense in changedAccountLicenses)
		{
			if (AccountLicenseMgr.Get().OwnsAccountLicense(changedAccountLicense))
			{
				TriggerAccountLicenseFlagsAction(ShowVisualOption.FORCE_SHOW, changedAccountLicense.License, changedAccountLicense.Flags_, cardRewards);
			}
		}
		CollectionManager.Get().AddCardRewards(cardRewards, markAsNew: false);
	}

	private MetaAction GetEarnedMetaActionReward(int metaActionID)
	{
		if (!m_earnedMetaActionRewards.ContainsKey(metaActionID))
		{
			m_earnedMetaActionRewards[metaActionID] = new MetaAction(metaActionID);
		}
		return m_earnedMetaActionRewards[metaActionID];
	}

	private void UpdateEarnedMetaActionFlags(int metaActionID, ulong addFlags, ulong removeFlags)
	{
		GetEarnedMetaActionReward(metaActionID).UpdateFlags(addFlags, removeFlags);
	}

	private bool QueueRewardVisual(FixedRewardMapDbfRecord record, int achieveID)
	{
		Achieve.RewardTiming rewardTiming = record.GetRewardTiming();
		Log.Achievements.Print($"QueueRewardVisual achieveID={achieveID} fixedRewardMapId={record.ID} {record.NoteDesc} {rewardTiming}");
		FixedReward.Reward fixedReward = record.GetFixedReward();
		if (FixedRewardUtils.ShouldSkipRewardVisual(rewardTiming, fixedReward))
		{
			return false;
		}
		m_rewardQueue.Add(rewardTiming, new RewardMapIDToShow(record.ID, achieveID, record.SortOrder));
		return true;
	}

	private void TriggerRewardsForAction(int actionID, ShowVisualOption showRewardVisual, List<CardRewardData> cardRewards)
	{
		TriggerRewardsForAction(actionID, showRewardVisual, cardRewards, RewardMapIDToShow.NoAchieveID);
	}

	private void TriggerRewardsForAction(int actionID, ShowVisualOption showRewardVisual, List<CardRewardData> cardRewards, int achieveID)
	{
		foreach (FixedRewardMapDbfRecord item in GameUtils.GetFixedRewardMapRecordsForAction(actionID))
		{
			FixedReward.Reward fixedReward = item.GetFixedReward();
			int iD = item.ID;
			if (m_rewardMapIDsAwarded.Contains(iD))
			{
				if (showRewardVisual != ShowVisualOption.FORCE_SHOW)
				{
					continue;
				}
			}
			else
			{
				m_rewardMapIDsAwarded.Add(iD);
			}
			if (item.RewardCount > 0)
			{
				bool flag = showRewardVisual != ShowVisualOption.DO_NOT_SHOW;
				if (fixedReward.FixedCardRewardData != null && (!flag || !QueueRewardVisual(item, achieveID)))
				{
					cardRewards.Add(fixedReward.FixedCardRewardData);
				}
				if (fixedReward.FixedCardBackRewardData != null && (!flag || !QueueRewardVisual(item, achieveID)))
				{
					CardBackManager.Get().AddNewCardBack(fixedReward.FixedCardBackRewardData.CardBackID);
				}
				if (fixedReward.FixedCraftableCardRewardData != null)
				{
					m_craftableCardRewards.Add(fixedReward.FixedCraftableCardRewardData);
				}
				if (fixedReward.MetaActionData != null)
				{
					UpdateEarnedMetaActionFlags(fixedReward.MetaActionData.MetaActionID, fixedReward.MetaActionData.MetaActionFlags, 0uL);
					TriggerMetaActionFlagsAction(showRewardVisual, fixedReward.MetaActionData.MetaActionID, cardRewards);
				}
			}
		}
	}

	private void TriggerWingProgressAction(ShowVisualOption showRewardVisual, int wingID, int progress, List<CardRewardData> cardRewards)
	{
		foreach (FixedRewardActionDbfRecord fixedActionRecord in GameUtils.GetFixedActionRecords(FixedRewardAction.Type.WING_PROGRESS))
		{
			if (fixedActionRecord.WingId == wingID && fixedActionRecord.WingProgress <= progress && SpecialEventManager.Get().IsEventActive(fixedActionRecord.ActiveEvent, activeIfDoesNotExist: false))
			{
				TriggerRewardsForAction(fixedActionRecord.ID, showRewardVisual, cardRewards);
			}
		}
	}

	private void TriggerWingFlagsAction(ShowVisualOption showRewardVisual, int wingID, ulong flags, List<CardRewardData> cardRewards)
	{
		foreach (FixedRewardActionDbfRecord fixedActionRecord in GameUtils.GetFixedActionRecords(FixedRewardAction.Type.WING_FLAGS))
		{
			if (fixedActionRecord.WingId == wingID)
			{
				ulong wingFlags = fixedActionRecord.WingFlags;
				if ((wingFlags & flags) == wingFlags && SpecialEventManager.Get().IsEventActive(fixedActionRecord.ActiveEvent, activeIfDoesNotExist: false))
				{
					TriggerRewardsForAction(fixedActionRecord.ID, showRewardVisual, cardRewards);
				}
			}
		}
	}

	private void TriggerAchieveAction(ShowVisualOption showRewardVisual, int achieveId, List<CardRewardData> cardRewards)
	{
		foreach (FixedRewardActionDbfRecord fixedActionRecord in GameUtils.GetFixedActionRecords(FixedRewardAction.Type.ACHIEVE))
		{
			if (fixedActionRecord.AchieveId == achieveId && SpecialEventManager.Get().IsEventActive(fixedActionRecord.ActiveEvent, activeIfDoesNotExist: false))
			{
				TriggerRewardsForAction(fixedActionRecord.ID, showRewardVisual, cardRewards, achieveId);
			}
		}
	}

	private void TriggerTotalHeroLevelAction(ShowVisualOption showRewardVisual, int totalHeroLevel)
	{
		List<CardRewardData> cardRewards = new List<CardRewardData>();
		foreach (FixedRewardActionDbfRecord fixedActionRecord in GameUtils.GetFixedActionRecords(FixedRewardAction.Type.TOTAL_HERO_LEVEL))
		{
			if (fixedActionRecord.TotalHeroLevel == totalHeroLevel && SpecialEventManager.Get().IsEventActive(fixedActionRecord.ActiveEvent, activeIfDoesNotExist: false))
			{
				TriggerRewardsForAction(fixedActionRecord.ID, showRewardVisual, cardRewards);
			}
		}
	}

	private void TriggerHeroLevelAction(ShowVisualOption showRewardVisual, int classID, int heroLevel)
	{
		List<CardRewardData> cardRewards = new List<CardRewardData>();
		foreach (FixedRewardActionDbfRecord fixedActionRecord in GameUtils.GetFixedActionRecords(FixedRewardAction.Type.HERO_LEVEL))
		{
			if (fixedActionRecord.ClassId == classID && fixedActionRecord.HeroLevel == heroLevel && SpecialEventManager.Get().IsEventActive(fixedActionRecord.ActiveEvent, activeIfDoesNotExist: false))
			{
				TriggerRewardsForAction(fixedActionRecord.ID, showRewardVisual, cardRewards);
			}
		}
	}

	private void TriggerTutorialProgressAction(ShowVisualOption showRewardVisual, int tutorialProgress, List<CardRewardData> cardRewards)
	{
		foreach (FixedRewardActionDbfRecord fixedActionRecord in GameUtils.GetFixedActionRecords(FixedRewardAction.Type.TUTORIAL_PROGRESS))
		{
			if (fixedActionRecord.TutorialProgress <= tutorialProgress && SpecialEventManager.Get().IsEventActive(fixedActionRecord.ActiveEvent, activeIfDoesNotExist: false))
			{
				TriggerRewardsForAction(fixedActionRecord.ID, showRewardVisual, cardRewards);
			}
		}
	}

	private void TriggerAccountLicenseFlagsAction(ShowVisualOption showRewardVisual, long license, ulong flags, List<CardRewardData> cardRewards)
	{
		foreach (FixedRewardActionDbfRecord fixedActionRecord in GameUtils.GetFixedActionRecords(FixedRewardAction.Type.ACCOUNT_LICENSE_FLAGS))
		{
			if (fixedActionRecord.AccountLicenseId == license)
			{
				ulong accountLicenseFlags = fixedActionRecord.AccountLicenseFlags;
				if ((accountLicenseFlags & flags) == accountLicenseFlags && SpecialEventManager.Get().IsEventActive(fixedActionRecord.ActiveEvent, activeIfDoesNotExist: false))
				{
					TriggerRewardsForAction(fixedActionRecord.ID, showRewardVisual, cardRewards);
				}
			}
		}
	}

	private void TriggerMetaActionFlagsAction(ShowVisualOption showRewardVisual, int metaActionID, List<CardRewardData> cardRewards)
	{
		FixedRewardActionDbfRecord record = GameDbf.FixedRewardAction.GetRecord(metaActionID);
		if (record != null)
		{
			ulong metaActionFlags = record.MetaActionFlags;
			if (GetEarnedMetaActionReward(metaActionID).HasAllRequiredFlags(metaActionFlags) && SpecialEventManager.Get().IsEventActive(record.ActiveEvent, activeIfDoesNotExist: false))
			{
				TriggerRewardsForAction(metaActionID, showRewardVisual, cardRewards);
			}
		}
	}

	private void ShowFixedRewards_Internal(UserAttentionBlocker blocker, OnAllFixedRewardsShownCallbackInfo callbackInfo)
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
		Log.Achievements.Print("Showing Fixed Reward: " + rewardMapIDToShow.achieveID);
		if (rewardData == null)
		{
			ShowFixedRewards_Internal(blocker, callbackInfo);
			return;
		}
		if (callbackInfo.showingCheatRewards)
		{
			rewardData.MarkAsDummyReward();
		}
		if (rewardMapIDToShow.achieveID != RewardMapIDToShow.NoAchieveID)
		{
			AchieveManager.Get().GetAchievement(rewardMapIDToShow.achieveID)?.AckCurrentProgressAndRewardNotices();
		}
		if (record.UseQuestToast)
		{
			string name = record.ToastName;
			string description = record.ToastDescription;
			QuestToast.ShowFixedRewardQuestToast(blocker, delegate
			{
				ShowFixedRewards_Internal(blocker, callbackInfo);
			}, rewardData, name, description);
			return;
		}
		rewardData.LoadRewardObject(delegate(Reward reward, object callbackData)
		{
			reward.transform.localPosition = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(0f, 0f, 43f),
				Phone = new Vector3(0f, 0f, 35f)
			};
			PlatformDependentValue<Vector3> platformDependentValue = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(27.6f, 27.6f, 27.6f),
				Phone = new Vector3(26.4f, 26.4f, 26.4f)
			};
			PlatformDependentValue<Vector3> platformDependentValue2 = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(23f, 23f, 23f),
				Phone = new Vector3(22f, 22f, 22f)
			};
			OverlayUI.Get().AddGameObject(reward.gameObject);
			SceneUtils.SetLayer(reward.gameObject, GameLayer.UI);
			if (callbackInfo.positionNonToastRewardCallback != null)
			{
				callbackInfo.positionNonToastRewardCallback(reward);
			}
			bool updateCacheValues = true;
			RewardUtils.ShowReward(blocker, reward, updateCacheValues, platformDependentValue, platformDependentValue2, delegate
			{
				reward.RegisterClickListener(OnNonToastRewardClicked, callbackInfo);
				reward.EnableClickCatcher(enabled: true);
			}, null);
		});
	}

	private void OnNonToastRewardClicked(Reward reward, object userData)
	{
		OnAllFixedRewardsShownCallbackInfo onAllFixedRewardsShownCallbackInfo = userData as OnAllFixedRewardsShownCallbackInfo;
		reward.RemoveClickListener(OnNonToastRewardClicked, onAllFixedRewardsShownCallbackInfo);
		reward.Hide(animate: true);
		ShowFixedRewards_Internal(UserAttentionBlocker.NONE, onAllFixedRewardsShownCallbackInfo);
	}

	private void GrantAchieveRewards(List<CardRewardData> cardRewards)
	{
		AchieveManager achieveManager = AchieveManager.Get();
		if (achieveManager == null)
		{
			Debug.LogWarning("FixedRewardsMgr.GrantAchieveRewards(): null == AchieveManager.Get()");
			return;
		}
		foreach (Achievement completedAchiefe in achieveManager.GetCompletedAchieves())
		{
			ShowVisualOption showRewardVisual = (completedAchiefe.IsNewlyCompleted() ? ShowVisualOption.SHOW : ShowVisualOption.DO_NOT_SHOW);
			TriggerAchieveAction(showRewardVisual, completedAchiefe.ID, cardRewards);
		}
	}

	public RewardData GetNextHeroLevelReward(TAG_CLASS classID, int currentHeroLevel, out int nextRewardLevel)
	{
		List<RewardData> list = new List<RewardData>();
		List<FixedRewardActionDbfRecord> fixedActionRecords = GameUtils.GetFixedActionRecords(FixedRewardAction.Type.HERO_LEVEL);
		FixedRewardActionDbfRecord fixedRewardActionDbfRecord = null;
		nextRewardLevel = 0;
		int num = int.MaxValue;
		foreach (FixedRewardActionDbfRecord item in fixedActionRecords)
		{
			if (item.ClassId == (int)classID && SpecialEventManager.Get().IsEventActive(item.ActiveEvent, activeIfDoesNotExist: false) && item.HeroLevel > currentHeroLevel && item.HeroLevel - currentHeroLevel < num)
			{
				num = item.HeroLevel - currentHeroLevel;
				fixedRewardActionDbfRecord = item;
			}
		}
		if (fixedRewardActionDbfRecord == null)
		{
			return null;
		}
		nextRewardLevel = fixedRewardActionDbfRecord.HeroLevel;
		foreach (FixedRewardMapDbfRecord item2 in GameUtils.GetFixedRewardMapRecordsForAction(fixedRewardActionDbfRecord.ID))
		{
			if (item2.RewardRecord != null)
			{
				FixedReward.Reward fixedReward = item2.GetFixedReward();
				if (fixedReward.FixedCardRewardData != null)
				{
					list.Add(fixedReward.FixedCardRewardData);
				}
			}
		}
		if (list.Count == 0)
		{
			Debug.LogFormat("No subsequent reward found for Hero Class: {0} after Level: {1}. Check FIXED REWARD MAPS if you think there should be one", classID.ToString(), currentHeroLevel);
			return null;
		}
		if (list.Count > 1)
		{
			Debug.LogWarningFormat("More than one reward listed for the subsequent reward for Hero Class: {0} after Level: {1}. Check FIXED REWARD ACTIONS and FIXED REWARD MAPS to ensure there is only one reward per level", classID.ToString(), currentHeroLevel);
		}
		return list[0];
	}

	public RewardData GetNextTotalLevelReward(int currentTotalLevel, out int nextRewardLevel)
	{
		List<RewardData> list = new List<RewardData>();
		List<FixedRewardActionDbfRecord> fixedActionRecords = GameUtils.GetFixedActionRecords(FixedRewardAction.Type.TOTAL_HERO_LEVEL);
		FixedRewardActionDbfRecord fixedRewardActionDbfRecord = null;
		nextRewardLevel = 0;
		int num = int.MaxValue;
		foreach (FixedRewardActionDbfRecord item in fixedActionRecords)
		{
			if (SpecialEventManager.Get().IsEventActive(item.ActiveEvent, activeIfDoesNotExist: false) && item.TotalHeroLevel > currentTotalLevel && item.TotalHeroLevel - currentTotalLevel < num)
			{
				num = item.TotalHeroLevel - currentTotalLevel;
				fixedRewardActionDbfRecord = item;
			}
		}
		if (fixedRewardActionDbfRecord == null)
		{
			return null;
		}
		nextRewardLevel = fixedRewardActionDbfRecord.TotalHeroLevel;
		foreach (FixedRewardMapDbfRecord item2 in GameUtils.GetFixedRewardMapRecordsForAction(fixedRewardActionDbfRecord.ID))
		{
			if (item2.RewardRecord != null)
			{
				FixedReward.Reward fixedReward = item2.GetFixedReward();
				if (fixedReward.FixedCardRewardData != null)
				{
					list.Add(fixedReward.FixedCardRewardData);
				}
			}
		}
		if (list.Count == 0)
		{
			Debug.LogFormat("No subsequent reward found for after Total Level: {0}. Check FIXED REWARD MAPS if you think there should be one", currentTotalLevel);
			return null;
		}
		if (list.Count > 1)
		{
			Debug.LogErrorFormat("More than one reward listed for the subsequent reward after Total Level: {0}. Check FIXED REWARD ACTIONS and FIXED REWARD MAPS to ensure there is only one reward per level", currentTotalLevel);
		}
		return list[0];
	}
}
