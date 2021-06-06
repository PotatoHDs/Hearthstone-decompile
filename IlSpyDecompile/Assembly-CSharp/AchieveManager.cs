using System;
using System.Collections.Generic;
using System.Linq;
using Assets;
using bgs;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Progression;
using PegasusUtil;
using UnityEngine;

public class AchieveManager : IService, IHasUpdate
{
	public delegate void AchieveCanceledCallback(int achieveID, bool success, object userData);

	private class AchieveCanceledListener : EventListener<AchieveCanceledCallback>
	{
		public void Fire(int achieveID, bool success)
		{
			m_callback(achieveID, success, m_userData);
		}
	}

	public delegate void AchievesUpdatedCallback(List<Achievement> updatedAchieves, List<Achievement> completedAchieves, object userData);

	private class AchievesUpdatedListener : EventListener<AchievesUpdatedCallback>
	{
		public void Fire(List<Achievement> updatedAchieves, List<Achievement> completedAchieves)
		{
			m_callback(updatedAchieves, completedAchieves, m_userData);
		}
	}

	public delegate void LicenseAddedAchievesUpdatedCallback(List<Achievement> activeLicenseAddedAchieves, object userData);

	private class LicenseAddedAchievesUpdatedListener : EventListener<LicenseAddedAchievesUpdatedCallback>
	{
		public void Fire(List<Achievement> activeLicenseAddedAchieves)
		{
			m_callback(activeLicenseAddedAchieves, m_userData);
		}
	}

	private static readonly long TIMED_ACHIEVE_VALIDATION_DELAY_TICKS = 600000000L;

	private static readonly long CHECK_LICENSE_ADDED_ACHIEVE_DELAY_TICKS = 3000000000L;

	private static readonly long TIMED_AND_LICENSE_ACHIEVE_CHECK_DELAY_TICKS = Math.Min(TIMED_ACHIEVE_VALIDATION_DELAY_TICKS, CHECK_LICENSE_ADDED_ACHIEVE_DELAY_TICKS);

	private Map<int, Achievement> m_achievements = new Map<int, Achievement>();

	private bool m_allNetAchievesReceived;

	private int m_numEventResponsesNeeded;

	private HashSet<int> m_achieveValidationsToRequest = new HashSet<int>();

	private HashSet<int> m_achieveValidationsRequested = new HashSet<int>();

	private HashSet<int> m_achievesSeenByPlayerThisSession = new HashSet<int>();

	private HashSet<int> m_visualBlocklist = new HashSet<int>();

	private bool m_disableCancelButtonUntilServerReturns;

	private Map<int, long> m_lastEventTimingValidationByAchieve = new Map<int, long>();

	private Map<int, long> m_lastCheckLicenseAddedByAchieve = new Map<int, long>();

	private long m_lastEventTimingAndLicenseAchieveCheck;

	private bool m_queueNotifications;

	private List<int> m_achieveNotificationsToQueue = new List<int>();

	private List<AchievementNotification> m_blockedAchievementNotifications = new List<AchievementNotification>();

	private List<AchieveCanceledListener> m_achieveCanceledListeners = new List<AchieveCanceledListener>();

	private List<AchievesUpdatedListener> m_achievesUpdatedListeners = new List<AchievesUpdatedListener>();

	private List<LicenseAddedAchievesUpdatedListener> m_licenseAddedAchievesUpdatedListeners = new List<LicenseAddedAchievesUpdatedListener>();

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		HearthstoneApplication.Get().WillReset += WillReset;
		HearthstoneApplication.Get().Resetting += OnReset;
		LoadAchievesFromDBF();
		Network network = serviceLocator.Get<Network>();
		network.RegisterNetHandler(CancelQuestResponse.PacketID.ID, OnQuestCanceled);
		network.RegisterNetHandler(ValidateAchieveResponse.PacketID.ID, OnAchieveValidated);
		network.RegisterNetHandler(TriggerEventResponse.PacketID.ID, OnEventTriggered);
		network.RegisterNetHandler(AccountLicenseAchieveResponse.PacketID.ID, OnAccountLicenseAchieveResponse);
		serviceLocator.Get<NetCache>().RegisterNewNoticesListener(OnNewNotices);
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[4]
		{
			typeof(Network),
			typeof(NetCache),
			typeof(GameDbf),
			typeof(SpecialEventManager)
		};
	}

	public void Shutdown()
	{
	}

	private void WillReset()
	{
		m_allNetAchievesReceived = false;
		m_achieveValidationsToRequest.Clear();
		m_achieveValidationsRequested.Clear();
		m_achievesUpdatedListeners.Clear();
		m_lastEventTimingValidationByAchieve.Clear();
		m_lastCheckLicenseAddedByAchieve.Clear();
		m_licenseAddedAchievesUpdatedListeners.Clear();
		m_achievements.Clear();
	}

	private void OnReset()
	{
		LoadAchievesFromDBF();
	}

	public static AchieveManager Get()
	{
		return HearthstoneServices.Get<AchieveManager>();
	}

	public static bool IsPredicateTrue(Assets.Achieve.AltTextPredicate predicate)
	{
		if (predicate == Assets.Achieve.AltTextPredicate.CAN_SEE_WILD && CollectionManager.Get() != null && CollectionManager.Get().ShouldAccountSeeStandardWild())
		{
			return true;
		}
		return false;
	}

	public void InitAchieveManager()
	{
		WillReset();
		LoadAchievesFromDBF();
	}

	public bool IsAchievementVisuallyBlocklisted(int achieveId)
	{
		return m_visualBlocklist.Contains(achieveId);
	}

	public bool IsReady()
	{
		if (!m_allNetAchievesReceived)
		{
			return false;
		}
		if (m_numEventResponsesNeeded > 0)
		{
			return false;
		}
		if (m_achieveValidationsToRequest.Count > 0)
		{
			return false;
		}
		if (m_achieveValidationsRequested.Count > 0)
		{
			return false;
		}
		if (NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>() == null)
		{
			return false;
		}
		return true;
	}

	public bool RegisterAchievesUpdatedListener(AchievesUpdatedCallback callback, object userData = null)
	{
		if (callback == null)
		{
			return false;
		}
		AchievesUpdatedListener achievesUpdatedListener = new AchievesUpdatedListener();
		achievesUpdatedListener.SetCallback(callback);
		achievesUpdatedListener.SetUserData(userData);
		if (m_achievesUpdatedListeners.Contains(achievesUpdatedListener))
		{
			return false;
		}
		m_achievesUpdatedListeners.Add(achievesUpdatedListener);
		return true;
	}

	public bool RemoveAchievesUpdatedListener(AchievesUpdatedCallback callback)
	{
		return RemoveAchievesUpdatedListener(callback, null);
	}

	public bool RemoveAchievesUpdatedListener(AchievesUpdatedCallback callback, object userData)
	{
		if (callback == null)
		{
			return false;
		}
		AchievesUpdatedListener achievesUpdatedListener = new AchievesUpdatedListener();
		achievesUpdatedListener.SetCallback(callback);
		achievesUpdatedListener.SetUserData(userData);
		if (!m_achievesUpdatedListeners.Contains(achievesUpdatedListener))
		{
			return false;
		}
		m_achievesUpdatedListeners.Remove(achievesUpdatedListener);
		return true;
	}

	public List<Achievement> GetNewCompletedAchievesToShow()
	{
		return m_achievements.Values.ToList().FindAll(delegate(Achievement obj)
		{
			if (!obj.IsNewlyCompleted())
			{
				return false;
			}
			if (obj.IsInternal())
			{
				return false;
			}
			if (IsAchievementVisuallyBlocklisted(obj.ID))
			{
				return false;
			}
			if (obj.RewardTiming == Assets.Achieve.RewardTiming.NEVER)
			{
				return false;
			}
			Assets.Achieve.Type achieveType = obj.AchieveType;
			if ((uint)(achieveType - 2) <= 1u || achieveType == Assets.Achieve.Type.DAILY_REPEATABLE)
			{
				return false;
			}
			if (obj.IsGenericRewardChest)
			{
				return false;
			}
			return (!QuestManager.Get().IsSystemEnabled || GameDbf.Quest.GetRecord((QuestDbfRecord r) => r.ProxyForLegacyId == obj.DbfRecord.ID) == null) ? true : false;
		});
	}

	private static bool IsActiveQuest(Achievement obj, bool onlyNewlyActive)
	{
		if (!obj.Active)
		{
			return false;
		}
		if (!obj.CanShowInQuestLog)
		{
			return false;
		}
		if (onlyNewlyActive)
		{
			return obj.IsNewlyActive();
		}
		return true;
	}

	private static bool IsAutoDestroyQuest(Achievement obj)
	{
		if (!obj.CanShowInQuestLog)
		{
			return false;
		}
		return obj.AutoDestroy;
	}

	private static bool IsDialogQuest(Achievement obj)
	{
		if (!obj.CanShowInQuestLog)
		{
			return false;
		}
		return obj.QuestDialogId != 0;
	}

	public List<Achievement> GetActiveQuests(bool onlyNewlyActive = false)
	{
		return m_achievements.Values.Where((Achievement obj) => IsActiveQuest(obj, onlyNewlyActive)).ToList();
	}

	public bool HasQuestsToShow(bool onlyNewlyActive = false)
	{
		bool result = false;
		foreach (KeyValuePair<int, Achievement> achievement in m_achievements)
		{
			if (IsActiveQuest(achievement.Value, onlyNewlyActive: false) && (achievement.Value.IsNewlyActive() || achievement.Value.AutoDestroy))
			{
				return true;
			}
		}
		return result;
	}

	public bool MarkQuestAsSeenByPlayerThisSession(Achievement obj)
	{
		return m_achievesSeenByPlayerThisSession.Add(obj.ID);
	}

	public bool ResetQuestSeenByPlayerThisSession(Achievement obj)
	{
		return m_achievesSeenByPlayerThisSession.Remove(obj.ID);
	}

	public bool HasActiveQuests(bool onlyNewlyActive = false)
	{
		return m_achievements.Any((KeyValuePair<int, Achievement> kv) => IsActiveQuest(kv.Value, onlyNewlyActive));
	}

	public bool HasActiveAutoDestroyQuests()
	{
		return m_achievements.Any((KeyValuePair<int, Achievement> kv) => IsActiveQuest(kv.Value, onlyNewlyActive: false) && IsAutoDestroyQuest(kv.Value));
	}

	public bool HasActiveUnseenWelcomeQuestDialog()
	{
		return m_achievements.Any((KeyValuePair<int, Achievement> kv) => IsActiveQuest(kv.Value, onlyNewlyActive: false) && IsDialogQuest(kv.Value) && Options.Get().GetInt(Option.LATEST_SEEN_WELCOME_QUEST_DIALOG) != kv.Value.ID);
	}

	public bool HasActiveDialogQuests()
	{
		return m_achievements.Any((KeyValuePair<int, Achievement> kv) => IsActiveQuest(kv.Value, onlyNewlyActive: false) && IsDialogQuest(kv.Value));
	}

	public bool HasActiveQuestId(AchievementDbId id)
	{
		foreach (Achievement activeQuest in GetActiveQuests())
		{
			if (activeQuest.ID == (int)id)
			{
				return true;
			}
		}
		return false;
	}

	public List<Achievement> GetNewlyProgressedQuests()
	{
		return Get().GetActiveQuests().FindAll((Achievement obj) => obj.AcknowledgedProgress < obj.Progress && obj.Progress > 0 && obj.Progress < obj.MaxProgress);
	}

	public bool HasUnlockedFeature(Assets.Achieve.Unlocks feature)
	{
		if (DemoMgr.Get().ArenaIs1WinMode() && feature == Assets.Achieve.Unlocks.FORGE)
		{
			return true;
		}
		if (feature == Assets.Achieve.Unlocks.VANILLA_HEROES)
		{
			AchievementManager achievementManager = AchievementManager.Get();
			if (achievementManager != null && achievementManager.IsSystemEnabled)
			{
				return HasUnlockedVanillaHeroes();
			}
		}
		Achievement value = m_achievements.FirstOrDefault((KeyValuePair<int, Achievement> kv) => kv.Value.UnlockedFeature == feature).Value;
		if (value == null)
		{
			Debug.LogWarning($"AchieveManager.HasUnlockedFeature(): could not find achieve that unlocks feature {feature}");
			return false;
		}
		return value.IsCompleted();
	}

	public bool HasUnlockedVanillaHeroes()
	{
		foreach (int value in Enum.GetValues(typeof(ClassUnlockAchieveIds)))
		{
			Achievement achievement = GetAchievement(value);
			if (achievement == null)
			{
				Debug.LogWarning($"AchieveManager.HasUnlockedVanillaHeroes(): could not find ClassUnlockAchieve with ID {value}");
			}
			else if (!achievement.IsCompleted())
			{
				return false;
			}
		}
		return true;
	}

	public bool HasUnlockedArena()
	{
		return HasUnlockedVanillaHeroes();
	}

	public Achievement GetAchievement(int achieveID)
	{
		if (!m_achievements.ContainsKey(achieveID))
		{
			return null;
		}
		return m_achievements[achieveID];
	}

	public int GetMaxProgressForAchievement(int achieveID)
	{
		int result = 0;
		if (achieveID > 0)
		{
			Achievement achievement = GetAchievement(achieveID);
			if (achievement != null)
			{
				result = achievement.MaxProgress;
			}
		}
		return result;
	}

	public int GetNumAchievesInGroup(Assets.Achieve.Type achieveType)
	{
		return GetAchievesInGroup(achieveType).Count;
	}

	public IEnumerable<Achievement> GetCompletedAchieves()
	{
		return GetAchieves((Achievement a) => a.IsCompleted());
	}

	public List<Achievement> GetAchievesInGroup(Assets.Achieve.Type achieveGroup)
	{
		return new List<Achievement>(m_achievements.Values).FindAll((Achievement obj) => obj.AchieveType == achieveGroup);
	}

	public List<Achievement> GetAchievesInGroup(Assets.Achieve.Type achieveGroup, bool isComplete)
	{
		return GetAchievesInGroup(achieveGroup).FindAll((Achievement obj) => obj.IsCompleted() == isComplete);
	}

	public List<Achievement> GetAchievesForAdventureWing(int wingID)
	{
		return new List<Achievement>(m_achievements.Values).FindAll((Achievement obj) => obj.Enabled && obj.WingID == wingID);
	}

	public List<Achievement> GetAchievesForAdventureAndMode(int adventureId, int modeId)
	{
		return new List<Achievement>(m_achievements.Values).FindAll((Achievement obj) => obj.AdventureID == adventureId && obj.AdventureModeID == modeId);
	}

	public Achievement GetUnlockGoldenHeroAchievement(string heroCardID, TAG_PREMIUM premium)
	{
		return GetAchievesInGroup(Assets.Achieve.Type.GOLDHERO).Find(delegate(Achievement achieveObj)
		{
			RewardData rewardData = achieveObj.Rewards.Find((RewardData rewardObj) => rewardObj.RewardType == Reward.Type.CARD);
			if (rewardData == null)
			{
				return false;
			}
			CardRewardData cardRewardData = rewardData as CardRewardData;
			if (cardRewardData == null)
			{
				return false;
			}
			return cardRewardData.CardID.Equals(heroCardID) && cardRewardData.Premium.Equals(premium);
		});
	}

	public Achievement GetUnlockPremiumHeroAchievement(TAG_CLASS heroClass)
	{
		return GetAchievesInGroup(Assets.Achieve.Type.PREMIUMHERO).Find(delegate(Achievement achieveObj)
		{
			RewardData rewardData = achieveObj.Rewards.Find((RewardData rewardObj) => rewardObj.RewardType == Reward.Type.CARD);
			if (rewardData == null)
			{
				return false;
			}
			return rewardData is CardRewardData && achieveObj.MyHeroClassRequirement.Equals(heroClass);
		});
	}

	public bool HasActiveAchievesForEvent(SpecialEventType eventTrigger)
	{
		if (eventTrigger == SpecialEventType.IGNORE)
		{
			return false;
		}
		return m_achievements.Any(delegate(KeyValuePair<int, Achievement> kv)
		{
			Achievement value = kv.Value;
			if (value.EventTrigger != eventTrigger)
			{
				return false;
			}
			return value.Enabled && value.Active;
		});
	}

	public bool CanCancelQuest(int achieveID)
	{
		if (m_disableCancelButtonUntilServerReturns)
		{
			return false;
		}
		if (!CanCancelQuestNow())
		{
			return false;
		}
		if (!HasAccessToDailies())
		{
			return false;
		}
		Achievement achievement = GetAchievement(achieveID);
		if (achievement == null)
		{
			return false;
		}
		if (!achievement.CanBeCancelled)
		{
			return false;
		}
		return achievement.Active;
	}

	public static bool HasAccessToDailies()
	{
		if (!Get().HasUnlockedFeature(Assets.Achieve.Unlocks.DAILY))
		{
			return false;
		}
		return true;
	}

	public bool RegisterQuestCanceledListener(AchieveCanceledCallback callback)
	{
		return RegisterQuestCanceledListener(callback, null);
	}

	public bool RegisterQuestCanceledListener(AchieveCanceledCallback callback, object userData)
	{
		AchieveCanceledListener achieveCanceledListener = new AchieveCanceledListener();
		achieveCanceledListener.SetCallback(callback);
		achieveCanceledListener.SetUserData(userData);
		if (m_achieveCanceledListeners.Contains(achieveCanceledListener))
		{
			return false;
		}
		m_achieveCanceledListeners.Add(achieveCanceledListener);
		return true;
	}

	public bool RemoveQuestCanceledListener(AchieveCanceledCallback callback)
	{
		return RemoveQuestCanceledListener(callback, null);
	}

	public bool RemoveQuestCanceledListener(AchieveCanceledCallback callback, object userData)
	{
		AchieveCanceledListener achieveCanceledListener = new AchieveCanceledListener();
		achieveCanceledListener.SetCallback(callback);
		achieveCanceledListener.SetUserData(userData);
		return m_achieveCanceledListeners.Remove(achieveCanceledListener);
	}

	public void CancelQuest(int achieveID)
	{
		if (!CanCancelQuest(achieveID))
		{
			FireAchieveCanceledEvent(achieveID, success: false);
			return;
		}
		BlockAllNotifications();
		m_disableCancelButtonUntilServerReturns = true;
		Network.Get().RequestCancelQuest(achieveID);
	}

	public bool RegisterLicenseAddedAchievesUpdatedListener(LicenseAddedAchievesUpdatedCallback callback)
	{
		return RegisterLicenseAddedAchievesUpdatedListener(callback, null);
	}

	public bool RegisterLicenseAddedAchievesUpdatedListener(LicenseAddedAchievesUpdatedCallback callback, object userData)
	{
		LicenseAddedAchievesUpdatedListener licenseAddedAchievesUpdatedListener = new LicenseAddedAchievesUpdatedListener();
		licenseAddedAchievesUpdatedListener.SetCallback(callback);
		licenseAddedAchievesUpdatedListener.SetUserData(userData);
		if (m_licenseAddedAchievesUpdatedListeners.Contains(licenseAddedAchievesUpdatedListener))
		{
			return false;
		}
		m_licenseAddedAchievesUpdatedListeners.Add(licenseAddedAchievesUpdatedListener);
		return true;
	}

	public bool RemoveLicenseAddedAchievesUpdatedListener(LicenseAddedAchievesUpdatedCallback callback)
	{
		return RemoveLicenseAddedAchievesUpdatedListener(callback, null);
	}

	public bool RemoveLicenseAddedAchievesUpdatedListener(LicenseAddedAchievesUpdatedCallback callback, object userData)
	{
		LicenseAddedAchievesUpdatedListener licenseAddedAchievesUpdatedListener = new LicenseAddedAchievesUpdatedListener();
		licenseAddedAchievesUpdatedListener.SetCallback(callback);
		licenseAddedAchievesUpdatedListener.SetUserData(userData);
		return m_licenseAddedAchievesUpdatedListeners.Remove(licenseAddedAchievesUpdatedListener);
	}

	public bool HasActiveLicenseAddedAchieves()
	{
		return GetActiveLicenseAddedAchieves().Count > 0;
	}

	public bool HasIncompletePurchaseAchieves()
	{
		return m_achievements.Any(delegate(KeyValuePair<int, Achievement> kv)
		{
			Achievement value = kv.Value;
			if (value.IsCompleted())
			{
				return false;
			}
			return value.Enabled && value.AchieveTrigger == Assets.Achieve.Trigger.PURCHASE;
		});
	}

	public bool HasIncompleteDisenchantAchieves()
	{
		return m_achievements.Any(delegate(KeyValuePair<int, Achievement> kv)
		{
			Achievement value = kv.Value;
			if (value.IsCompleted())
			{
				return false;
			}
			return value.Enabled && value.AchieveTrigger == Assets.Achieve.Trigger.DISENCHANT;
		});
	}

	public void NotifyOfClick(Achievement.ClickTriggerType clickType)
	{
		Log.Achievements.Print("AchieveManager.NotifyOfClick(): clickType {0}", clickType);
		bool hasAllVanillaHeroes = HasUnlockedFeature(Assets.Achieve.Unlocks.VANILLA_HEROES);
		foreach (Achievement achiefe in GetAchieves(delegate(Achievement obj)
		{
			if (obj.AchieveTrigger != Assets.Achieve.Trigger.CLICK)
			{
				return false;
			}
			if (!obj.Enabled)
			{
				Log.Achievements.Print("AchieveManager.NotifyOfClick(): skip disabled achieve {0}", obj.ID);
				return false;
			}
			if (obj.IsCompleted())
			{
				Log.Achievements.Print("AchieveManager.NotifyOfClick(): skip already completed achieve {0}", obj.ID);
				return false;
			}
			if (!obj.ClickType.HasValue)
			{
				Log.Achievements.Print("AchieveManager.NotifyOfClick(): skip missing ClickType achieve {0}", obj.ID);
				return false;
			}
			if (obj.ClickType.Value != clickType)
			{
				Log.Achievements.Print("AchieveManager.NotifyOfClick(): skip achieve {0} with non-matching ClickType {1}", obj.ID, obj.ClickType.Value);
				return false;
			}
			if (clickType == Achievement.ClickTriggerType.BUTTON_ADVENTURE && !hasAllVanillaHeroes && AdventureUtils.DoesAdventureRequireAllHeroesUnlocked((AdventureDbId)obj.AdventureID))
			{
				Log.Achievements.Print("AchieveManager.NotifyOfClick(): skip achieve {0} for BUTTON_ADVENTURE requiring all heroes unlocked", obj.ID);
				return false;
			}
			return true;
		}))
		{
			Log.Achievements.Print("AchieveManager.NotifyOfClick(): add achieve {0}", achiefe.ID);
			m_achieveValidationsToRequest.Add(achiefe.ID);
		}
		ValidateAchievesNow();
	}

	public void CompleteAutoDestroyAchieve(int achieveId)
	{
		foreach (Achievement achiefe in GetAchieves(delegate(Achievement obj)
		{
			if (obj.IsCompleted())
			{
				return false;
			}
			if (!obj.Enabled)
			{
				return false;
			}
			return obj.Active && obj.AchieveTrigger == Assets.Achieve.Trigger.DESTROYED;
		}))
		{
			if (achiefe.ID == achieveId)
			{
				m_achieveValidationsToRequest.Add(achiefe.ID);
			}
		}
		ValidateAchievesNow();
	}

	public void NotifyOfAccountCreation()
	{
		foreach (Achievement achiefe in GetAchieves(delegate(Achievement obj)
		{
			if (obj.IsCompleted())
			{
				return false;
			}
			return obj.Enabled && obj.AchieveTrigger == Assets.Achieve.Trigger.ACCOUNT_CREATED;
		}))
		{
			m_achieveValidationsToRequest.Add(achiefe.ID);
		}
		ValidateAchievesNow();
	}

	public void NotifyOfPacksReadyToOpen(UnopenedPack unopenedPack)
	{
		IEnumerable<Achievement> achieves = GetAchieves(delegate(Achievement obj)
		{
			if (!obj.Enabled)
			{
				return false;
			}
			if (obj.IsCompleted())
			{
				return false;
			}
			if (obj.AchieveTrigger != Assets.Achieve.Trigger.PACK_READY_TO_OPEN)
			{
				return false;
			}
			if (obj.BoosterRequirement != unopenedPack.GetBoosterStack().Id)
			{
				return false;
			}
			if (unopenedPack.GetBoosterStack().Count == 0)
			{
				return false;
			}
			return unopenedPack.CanOpenPack() ? true : false;
		});
		bool flag = false;
		foreach (Achievement item in achieves)
		{
			m_achieveValidationsToRequest.Add(item.ID);
			flag = true;
		}
		if (flag)
		{
			ValidateAchievesNow();
		}
	}

	public void Update()
	{
		if (Network.IsRunning())
		{
			CheckTimedEventsAndLicenses(DateTime.UtcNow);
		}
	}

	public void ValidateAchievesNow()
	{
		if (m_achieveValidationsToRequest.Count == 0)
		{
			return;
		}
		m_achieveValidationsRequested.Union(m_achieveValidationsToRequest);
		foreach (int achieveID in m_achieveValidationsToRequest)
		{
			AchieveRegionDataDbfRecord record = GameDbf.AchieveRegionData.GetRecord((AchieveRegionDataDbfRecord r) => r.AchieveId == achieveID);
			if (record != null && !SpecialEventManager.Get().IsEventActive(record.ProgressableEvent, activeIfDoesNotExist: false))
			{
				Log.Achievements.Print("AchieveManager.ValidateAchievesNow(): skip non-progressable achieve {0} event {1}", achieveID, record.ProgressableEvent);
			}
			else
			{
				Log.Achievements.Print("AchieveManager.ValidateAchievesNow(): ValidateAchieve {0}", achieveID);
				Network.Get().ValidateAchieve(achieveID);
			}
		}
		m_achieveValidationsToRequest.Clear();
	}

	public void CheckPlayedNearbyPlayerOnSubnet()
	{
		if (!HasActiveAchievesForEvent(SpecialEventType.FIRESIDE_GATHERINGS_CARDBACK))
		{
			return;
		}
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		if (opposingSidePlayer == null)
		{
			return;
		}
		BnetPlayer bnetPlayer = BnetNearbyPlayerMgr.Get().FindNearbyPlayer(opposingSidePlayer.GetGameAccountId());
		if (bnetPlayer == null)
		{
			return;
		}
		BnetAccountId accountId = bnetPlayer.GetAccountId();
		if (accountId == null)
		{
			return;
		}
		List<BnetPlayer> nearbyPlayers = BnetNearbyPlayerMgr.Get().GetNearbyPlayers();
		BnetPlayer bnetPlayer2 = null;
		foreach (BnetPlayer item in nearbyPlayers)
		{
			BnetAccountId accountId2 = item.GetAccountId();
			if (!(accountId2 == null) && !accountId2.Equals(accountId))
			{
				bnetPlayer2 = item;
				break;
			}
		}
		if (bnetPlayer2 == null || !BnetNearbyPlayerMgr.Get().GetNearbySessionStartTime(bnetPlayer, out var sessionStartTime) || !BnetNearbyPlayerMgr.Get().GetNearbySessionStartTime(bnetPlayer2, out var sessionStartTime2))
		{
			return;
		}
		BnetGameAccountId hearthstoneGameAccountId = bnetPlayer.GetHearthstoneGameAccountId();
		if (!(hearthstoneGameAccountId == null))
		{
			BnetGameAccountId hearthstoneGameAccountId2 = bnetPlayer2.GetHearthstoneGameAccountId();
			if (!(hearthstoneGameAccountId2 == null))
			{
				m_numEventResponsesNeeded++;
				Network.Get().TriggerPlayedNearbyPlayerOnSubnet(hearthstoneGameAccountId, sessionStartTime, hearthstoneGameAccountId2, sessionStartTime2);
			}
		}
	}

	public void LoadAchievesFromDBF()
	{
		m_achievements.Clear();
		List<AchieveDbfRecord> records = GameDbf.Achieve.GetRecords();
		List<CharacterDialogDbfRecord> records2 = GameDbf.CharacterDialog.GetRecords();
		Map<int, int> map = new Map<int, int>();
		foreach (AchieveDbfRecord achieveRecord in records)
		{
			int iD = achieveRecord.ID;
			int race = achieveRecord.Race;
			TAG_RACE? raceReq = null;
			if (race != 0)
			{
				raceReq = (TAG_RACE)race;
			}
			int cardSet = achieveRecord.CardSet;
			TAG_CARD_SET? cardSetReq = null;
			if (cardSet != 0)
			{
				cardSetReq = (TAG_CARD_SET)cardSet;
			}
			int myHeroClassId = achieveRecord.MyHeroClassId;
			TAG_CLASS? myHeroClassReq = null;
			if (myHeroClassId != 0)
			{
				myHeroClassReq = (TAG_CLASS)myHeroClassId;
			}
			long rewardData = achieveRecord.RewardData1;
			long rewardData2 = achieveRecord.RewardData2;
			bool isGenericRewardChest = false;
			string chestVisualPrefabPath = "";
			List<RewardData> list = new List<RewardData>();
			TAG_CLASS? classReward = null;
			switch (achieveRecord.Reward)
			{
			case "basic":
				Debug.LogWarning($"AchieveManager.LoadAchievesFromFile(): unable to define reward {achieveRecord.Reward} for achieve {iD}");
				break;
			case "card":
			{
				string cardID2 = GameUtils.TranslateDbIdToCardId((int)rewardData);
				TAG_PREMIUM premium2 = (TAG_PREMIUM)rewardData2;
				list.Add(new CardRewardData(cardID2, premium2, 1));
				break;
			}
			case "card2x":
			{
				string cardID = GameUtils.TranslateDbIdToCardId((int)rewardData);
				TAG_PREMIUM premium = (TAG_PREMIUM)rewardData2;
				list.Add(new CardRewardData(cardID, premium, 2));
				break;
			}
			case "cardback":
				list.Add(new CardBackRewardData((int)rewardData));
				break;
			case "dust":
				list.Add(new ArcaneDustRewardData((int)rewardData));
				break;
			case "forge":
				list.Add(new ForgeTicketRewardData((int)rewardData));
				break;
			case "gold":
				list.Add(new GoldRewardData((int)rewardData));
				break;
			case "goldhero":
			{
				string cardID3 = GameUtils.TranslateDbIdToCardId((int)rewardData);
				TAG_PREMIUM premium3 = (TAG_PREMIUM)rewardData2;
				list.Add(new CardRewardData(cardID3, premium3, 1));
				break;
			}
			case "hero":
			{
				classReward = (TAG_CLASS)rewardData2;
				string heroCardId = CollectionManager.GetHeroCardId(classReward.Value, CardHero.HeroType.VANILLA);
				if (!string.IsNullOrEmpty(heroCardId))
				{
					list.Add(new CardRewardData(heroCardId, TAG_PREMIUM.NORMAL, 1));
				}
				break;
			}
			case "mount":
				list.Add(new MountRewardData((MountRewardData.MountType)rewardData));
				break;
			case "pack":
			{
				int id = (int)((rewardData2 <= 0) ? 1 : rewardData2);
				list.Add(new BoosterPackRewardData(id, (int)rewardData));
				break;
			}
			case "event_notice":
			{
				int eventType = (int)((rewardData > 0) ? rewardData : 0);
				list.Add(new EventRewardData(eventType));
				break;
			}
			case "generic_reward_chest":
				isGenericRewardChest = true;
				list.AddRange(RewardUtils.GetRewardDataFromRewardChestAsset((int)rewardData, (int)rewardData2));
				chestVisualPrefabPath = GameDbf.RewardChest.GetRecord((int)rewardData).ChestPrefab;
				break;
			case "arcane_orbs":
				list.Add(RewardUtils.CreateArcaneOrbRewardData((int)rewardData));
				break;
			case "deck":
				list.Add(RewardUtils.CreateDeckRewardData((int)rewardData, (int)rewardData2));
				break;
			}
			Assets.Achieve.RewardTiming rewardTiming = achieveRecord.RewardTiming;
			int num2 = (map[iD] = records.Find((AchieveDbfRecord obj) => obj.NoteDesc == achieveRecord.ParentAch)?.ID ?? 0);
			int linkToId = records.Find((AchieveDbfRecord obj) => obj.NoteDesc == achieveRecord.LinkTo)?.ID ?? 0;
			Achievement.ClickTriggerType? clickType = null;
			Assets.Achieve.Trigger triggered = achieveRecord.Triggered;
			if (triggered == Assets.Achieve.Trigger.CLICK)
			{
				clickType = (Achievement.ClickTriggerType)rewardData;
			}
			if (achieveRecord.ID == 94)
			{
				clickType = Achievement.ClickTriggerType.BUTTON_ARENA;
			}
			List<int> scenarios = (from c in GameDbf.AchieveCondition.GetRecords((AchieveConditionDbfRecord a) => a.AchieveId == achieveRecord.ID)
				select c.ScenarioId).ToList();
			CharacterDialogDbfRecord characterDialogDbfRecord = records2.Find((CharacterDialogDbfRecord obj) => obj.ID == achieveRecord.QuestDialogId);
			int num3 = characterDialogDbfRecord?.ID ?? 0;
			CharacterDialogSequence onReceivedDialogSequence = null;
			CharacterDialogSequence onCompleteDialogSequence = null;
			CharacterDialogSequence onProgress1DialogSequence = null;
			CharacterDialogSequence onProgress2DialogSequence = null;
			CharacterDialogSequence onDismissDialogSequence = null;
			if (characterDialogDbfRecord != null)
			{
				onReceivedDialogSequence = new CharacterDialogSequence(num3, CharacterDialogEventType.RECEIVE);
				onCompleteDialogSequence = new CharacterDialogSequence(num3, CharacterDialogEventType.COMPLETE);
				onProgress1DialogSequence = new CharacterDialogSequence(num3, CharacterDialogEventType.PROGRESS1);
				onProgress2DialogSequence = new CharacterDialogSequence(num3, CharacterDialogEventType.PROGRESS2);
				onDismissDialogSequence = new CharacterDialogSequence(num3, CharacterDialogEventType.DISMISS);
			}
			int onCompleteQuestDialogBannerId = characterDialogDbfRecord?.OnCompleteBannerId ?? 0;
			Achievement achievement = new Achievement(achieveRecord, iD, achieveRecord.AchType, achieveRecord.AchQuota, linkToId, achieveRecord.Triggered, achieveRecord.GameMode, raceReq, classReward, cardSetReq, myHeroClassReq, clickType, achieveRecord.Unlocks, list, scenarios, achieveRecord.AdventureWingId, achieveRecord.AdventureId, achieveRecord.AdventureModeId, rewardTiming, achieveRecord.Booster, achieveRecord.UseGenericRewardVisual, achieveRecord.ShowToReturningPlayer, num3, achieveRecord.AutoDestroy, achieveRecord.QuestTilePrefab, onCompleteQuestDialogBannerId, onReceivedDialogSequence, onCompleteDialogSequence, onProgress1DialogSequence, onProgress2DialogSequence, onDismissDialogSequence, isGenericRewardChest, chestVisualPrefabPath, achieveRecord.CustomVisualWidget, achieveRecord.EnemyHeroClassId);
			SpecialEventType eventTrigger = SpecialEventType.IGNORE;
			triggered = achieveRecord.Triggered;
			if (triggered == Assets.Achieve.Trigger.FINISH || triggered == Assets.Achieve.Trigger.EVENT_TIMING_ONLY)
			{
				AchieveRegionDataDbfRecord currentRegionData = achievement.GetCurrentRegionData();
				if (currentRegionData != null)
				{
					eventTrigger = SpecialEventManager.GetEventType(currentRegionData.ProgressableEvent);
				}
			}
			achievement.SetEventTrigger(eventTrigger);
			achievement.SetClientFlags(achieveRecord.ClientFlags);
			achievement.SetAltTextPredicate(achieveRecord.AltTextPredicate);
			achievement.SetName(achieveRecord.Name, achieveRecord.AltName);
			achievement.SetDescription(achieveRecord.Description, achieveRecord.AltDescription);
			InitAchievement(achievement);
		}
	}

	private void InitAchievement(Achievement achievement)
	{
		if (m_achievements.ContainsKey(achievement.ID))
		{
			Debug.LogWarning($"AchieveManager.InitAchievement() - already registered achievement with ID {achievement.ID}");
		}
		else
		{
			m_achievements.Add(achievement.ID, achievement);
		}
	}

	private IEnumerable<Achievement> GetAchieves(Func<Achievement, bool> filter = null)
	{
		return m_achievements.Where(delegate(KeyValuePair<int, Achievement> kv)
		{
			if (filter != null)
			{
				Func<Achievement, bool> func = filter;
				KeyValuePair<int, Achievement> keyValuePair2 = kv;
				return func(keyValuePair2.Value);
			}
			return true;
		}).Select(delegate(KeyValuePair<int, Achievement> kv)
		{
			KeyValuePair<int, Achievement> keyValuePair = kv;
			return keyValuePair.Value;
		});
	}

	public void OnInitialAchievements(Achieves achievements)
	{
		if (achievements != null)
		{
			OnAllAchieves(achievements);
		}
	}

	private void OnAllAchieves(Achieves allAchievesList)
	{
		foreach (PegasusUtil.Achieve item in allAchievesList.List)
		{
			GetAchievement(item.Id)?.OnAchieveData(item);
		}
		CheckAllCardGainAchieves();
		m_allNetAchievesReceived = true;
		UnblockAllNotifications();
	}

	public void OnAchievementNotifications(List<AchievementNotification> achievementNotifications)
	{
		List<Achievement> list = new List<Achievement>();
		List<Achievement> list2 = new List<Achievement>();
		bool flag = false;
		foreach (AchievementNotification achievementNotification in achievementNotifications)
		{
			if (m_queueNotifications || !m_allNetAchievesReceived || m_achieveNotificationsToQueue.Contains((int)achievementNotification.AchievementId))
			{
				Log.Achievements.Print("Blocking AchievementNotification: ID={0}", achievementNotification.AchievementId);
				m_blockedAchievementNotifications.Add(achievementNotification);
				continue;
			}
			Achievement achievement = GetAchievement((int)achievementNotification.AchievementId);
			if (achievement == null)
			{
				continue;
			}
			AchieveDbfRecord record = GameDbf.Achieve.GetRecord(achievement.ID);
			if (record.VisualBlacklist != null)
			{
				foreach (VisualBlacklistDbfRecord item in record.VisualBlacklist)
				{
					AchieveDbfRecord record2 = GameDbf.Achieve.GetRecord(item.BlacklistAchieveId);
					m_visualBlocklist.Add(item.BlacklistAchieveId);
					Network.Get().AckAchieveProgress(item.BlacklistAchieveId, record2.AchQuota);
				}
			}
			if (achievement.AchieveTrigger == Assets.Achieve.Trigger.LICENSEADDED || achievement.AchieveTrigger == Assets.Achieve.Trigger.EVENT_TIMING_ONLY)
			{
				flag = true;
			}
			achievement.OnAchieveNotification(achievementNotification);
			if (!achievement.Active && achievementNotification.Complete)
			{
				list.Add(achievement);
			}
			else
			{
				list2.Add(achievement);
			}
			Log.Achievements.Print("OnAchievementNotification: Achievement={0}", achievement);
		}
		if (flag)
		{
			m_lastEventTimingAndLicenseAchieveCheck = 0L;
		}
		AchievesUpdatedListener[] array = m_achievesUpdatedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(list2, list);
		}
	}

	private void BlockNotification(int achieveIdToQueue)
	{
		List<int> list = new List<int>();
		list.Add(achieveIdToQueue);
		BlockNotifications(list);
	}

	private void BlockNotifications(List<int> achieveIdsToQueue)
	{
		for (int i = 0; i < achieveIdsToQueue.Count; i++)
		{
			m_achieveNotificationsToQueue.Add(achieveIdsToQueue[i]);
		}
	}

	private void UnblockNotification(int achieveIdToUnblock)
	{
		List<int> list = new List<int>();
		list.Add(achieveIdToUnblock);
		UnblockNotifications(list);
	}

	private void UnblockNotifications(List<int> achieveIdsToUnblock)
	{
		List<AchievementNotification> list = m_blockedAchievementNotifications.Where((AchievementNotification obj) => achieveIdsToUnblock.Contains((int)obj.AchievementId)).ToList();
		m_blockedAchievementNotifications.RemoveAll((AchievementNotification obj) => achieveIdsToUnblock.Contains((int)obj.AchievementId));
		m_achieveNotificationsToQueue.RemoveAll((int id) => achieveIdsToUnblock.Contains(id));
		if (list.Count > 0)
		{
			OnAchievementNotifications(list);
		}
	}

	public void BlockAllNotifications()
	{
		m_queueNotifications = true;
	}

	public void UnblockAllNotifications()
	{
		m_queueNotifications = false;
		if (m_blockedAchievementNotifications.Count > 0)
		{
			OnAchievementNotifications(m_blockedAchievementNotifications);
			m_blockedAchievementNotifications.Clear();
		}
	}

	private void OnQuestCanceled()
	{
		Network.CanceledQuest canceledQuest = Network.Get().GetCanceledQuest();
		Log.Achievements.Print("OnQuestCanceled: CanceledQuest={0}", canceledQuest);
		m_disableCancelButtonUntilServerReturns = false;
		if (canceledQuest.Canceled)
		{
			GetAchievement(canceledQuest.AchieveID).OnCancelSuccess();
			NetCache.NetCacheRewardProgress netObject = NetCache.Get().GetNetObject<NetCache.NetCacheRewardProgress>();
			if (netObject != null)
			{
				netObject.NextQuestCancelDate = canceledQuest.NextQuestCancelDate;
			}
		}
		FireAchieveCanceledEvent(canceledQuest.AchieveID, canceledQuest.Canceled);
		UnblockAllNotifications();
	}

	private void OnAchieveValidated()
	{
		ValidateAchieveResponse validatedAchieve = Network.Get().GetValidatedAchieve();
		m_achieveValidationsRequested.Remove(validatedAchieve.Achieve);
		Log.Achievements.Print("AchieveManager.OnAchieveValidated(): achieve={0} success={1}", validatedAchieve.Achieve, validatedAchieve.Success);
	}

	private void OnEventTriggered()
	{
		Network.Get().GetTriggerEventResponse();
		m_numEventResponsesNeeded--;
	}

	private void OnAccountLicenseAchieveResponse()
	{
		Network.AccountLicenseAchieveResponse accountLicenseAchieveResponse = Network.Get().GetAccountLicenseAchieveResponse();
		if (accountLicenseAchieveResponse.Result != Network.AccountLicenseAchieveResponse.AchieveResult.COMPLETE)
		{
			FireLicenseAddedAchievesUpdatedEvent();
			return;
		}
		Log.Achievements.Print("AchieveManager.OnAccountLicenseAchieveResponse(): achieve {0} is now complete, refreshing achieves", accountLicenseAchieveResponse.Achieve);
		OnAccountLicenseAchievesUpdated(accountLicenseAchieveResponse.Achieve);
	}

	private void OnAccountLicenseAchievesUpdated(object userData)
	{
		int num = (int)userData;
		Log.Achievements.Print("AchieveManager.OnAccountLicenseAchievesUpdated(): refreshing achieves complete, triggered by achieve {0}", num);
		FireLicenseAddedAchievesUpdatedEvent();
	}

	private void FireLicenseAddedAchievesUpdatedEvent()
	{
		List<Achievement> activeLicenseAddedAchieves = GetActiveLicenseAddedAchieves();
		LicenseAddedAchievesUpdatedListener[] array = m_licenseAddedAchievesUpdatedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(activeLicenseAddedAchieves);
		}
	}

	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		foreach (NetCache.ProfileNotice newNotice in newNotices)
		{
			if (NetCache.ProfileNotice.NoticeOrigin.ACHIEVEMENT == newNotice.Origin)
			{
				int achieveID = (int)newNotice.OriginData;
				GetAchievement(achieveID)?.AddRewardNoticeID(newNotice.NoticeID);
			}
		}
	}

	private bool CanCancelQuestNow()
	{
		if (Vars.Key("Quests.CanCancelManyTimes").GetBool(def: false))
		{
			return true;
		}
		NetCache.NetCacheRewardProgress netObject = NetCache.Get().GetNetObject<NetCache.NetCacheRewardProgress>();
		if (netObject == null)
		{
			return false;
		}
		long num = DateTime.Now.ToFileTimeUtc();
		return netObject.NextQuestCancelDate <= num;
	}

	private void FireAchieveCanceledEvent(int achieveID, bool success)
	{
		AchieveCanceledListener[] array = m_achieveCanceledListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(achieveID, success);
		}
	}

	private void CheckAllCardGainAchieves()
	{
		GetAchieves(delegate(Achievement obj)
		{
			if (!obj.Enabled)
			{
				return false;
			}
			if (obj.IsCompleted())
			{
				return false;
			}
			Assets.Achieve.Trigger achieveTrigger = obj.AchieveTrigger;
			return (uint)(achieveTrigger - 6) <= 1u && obj.RaceRequirement.HasValue;
		});
		GetAchieves(delegate(Achievement obj)
		{
			if (!obj.Enabled)
			{
				return false;
			}
			if (obj.IsCompleted())
			{
				return false;
			}
			return obj.AchieveTrigger == Assets.Achieve.Trigger.CARDSET && obj.CardSetRequirement.HasValue;
		});
		ValidateAchievesNow();
	}

	private void CheckTimedEventsAndLicenses(DateTime utcNow)
	{
		if (!m_allNetAchievesReceived)
		{
			return;
		}
		DateTime dateTime = utcNow.ToLocalTime();
		if (dateTime.Ticks - m_lastEventTimingAndLicenseAchieveCheck < TIMED_AND_LICENSE_ACHIEVE_CHECK_DELAY_TICKS)
		{
			return;
		}
		m_lastEventTimingAndLicenseAchieveCheck = dateTime.Ticks;
		int num = 0;
		foreach (Achievement value in m_achievements.Values)
		{
			if (value.Enabled && !value.IsCompleted() && value.Active && Assets.Achieve.Trigger.EVENT_TIMING_ONLY == value.AchieveTrigger && SpecialEventManager.Get().IsEventActive(value.EventTrigger, activeIfDoesNotExist: false) && (!m_lastEventTimingValidationByAchieve.ContainsKey(value.ID) || dateTime.Ticks - m_lastEventTimingValidationByAchieve[value.ID] >= TIMED_ACHIEVE_VALIDATION_DELAY_TICKS))
			{
				Log.Achievements.Print("AchieveManager.CheckTimedEventsAndLicenses(): checking on timed event achieve {0} time {1}", value.ID, dateTime);
				m_lastEventTimingValidationByAchieve[value.ID] = dateTime.Ticks;
				m_achieveValidationsToRequest.Add(value.ID);
				num++;
			}
			if (value.IsActiveLicenseAddedAchieve() && (!m_lastCheckLicenseAddedByAchieve.ContainsKey(value.ID) || utcNow.Ticks - m_lastCheckLicenseAddedByAchieve[value.ID] >= CHECK_LICENSE_ADDED_ACHIEVE_DELAY_TICKS))
			{
				Log.Achievements.Print("AchieveManager.CheckTimedEventsAndLicenses(): checking on license added achieve {0} time {1}", value.ID, dateTime);
				m_lastCheckLicenseAddedByAchieve[value.ID] = utcNow.Ticks;
				Network.Get().CheckAccountLicenseAchieve(value.ID);
			}
		}
		if (num != 0)
		{
			ValidateAchievesNow();
		}
	}

	private List<Achievement> GetActiveLicenseAddedAchieves()
	{
		return m_achievements.Values.ToList().FindAll((Achievement obj) => obj.IsActiveLicenseAddedAchieve());
	}

	public List<RewardData> GetRewardsForAdventureAndMode(int adventureId, int modeId, HashSet<Assets.Achieve.RewardTiming> rewardTimings)
	{
		List<RewardData> list = new List<RewardData>();
		foreach (Achievement item in GetAchievesForAdventureAndMode(adventureId, modeId))
		{
			list.AddRange(GetRewardsForAchieve(item.ID, rewardTimings));
		}
		return list;
	}

	public List<RewardData> GetRewardsForAdventureWing(int wingID, HashSet<Assets.Achieve.RewardTiming> rewardTimings)
	{
		List<RewardData> list = new List<RewardData>();
		foreach (Achievement item in GetAchievesForAdventureWing(wingID))
		{
			list.AddRange(GetRewardsForAchieve(item.ID, rewardTimings));
		}
		return list;
	}

	public List<RewardData> GetRewardsForAdventureScenario(int wingID, int scenarioID, HashSet<Assets.Achieve.RewardTiming> rewardTimings)
	{
		List<RewardData> list = new List<RewardData>();
		foreach (Achievement item in GetAchievesForAdventureWing(wingID))
		{
			if (item.Scenarios.Contains(scenarioID))
			{
				list.AddRange(GetRewardsForAchieve(item.ID, rewardTimings));
			}
		}
		return list;
	}

	public List<RewardData> GetRewardsForAchieve(int achieveID, HashSet<Assets.Achieve.RewardTiming> rewardTimings)
	{
		List<RewardData> list = new List<RewardData>();
		Achievement achievement = GetAchievement(achieveID);
		List<RewardData> rewards = achievement.Rewards;
		if (rewardTimings.Contains(achievement.RewardTiming))
		{
			foreach (RewardData item in rewards)
			{
				list.Add(item);
			}
			return list;
		}
		return list;
	}
}
