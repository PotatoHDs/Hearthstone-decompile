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

// Token: 0x02000842 RID: 2114
public class AchieveManager : IService, IHasUpdate
{
	// Token: 0x060070BD RID: 28861 RVA: 0x00245797 File Offset: 0x00243997
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		HearthstoneApplication.Get().WillReset += this.WillReset;
		HearthstoneApplication.Get().Resetting += this.OnReset;
		this.LoadAchievesFromDBF();
		Network network = serviceLocator.Get<Network>();
		network.RegisterNetHandler(CancelQuestResponse.PacketID.ID, new Network.NetHandler(this.OnQuestCanceled), null);
		network.RegisterNetHandler(ValidateAchieveResponse.PacketID.ID, new Network.NetHandler(this.OnAchieveValidated), null);
		network.RegisterNetHandler(TriggerEventResponse.PacketID.ID, new Network.NetHandler(this.OnEventTriggered), null);
		network.RegisterNetHandler(AccountLicenseAchieveResponse.PacketID.ID, new Network.NetHandler(this.OnAccountLicenseAchieveResponse), null);
		serviceLocator.Get<NetCache>().RegisterNewNoticesListener(new NetCache.DelNewNoticesListener(this.OnNewNotices));
		yield break;
	}

	// Token: 0x060070BE RID: 28862 RVA: 0x002457AD File Offset: 0x002439AD
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(Network),
			typeof(NetCache),
			typeof(GameDbf),
			typeof(SpecialEventManager)
		};
	}

	// Token: 0x060070BF RID: 28863 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x060070C0 RID: 28864 RVA: 0x002457EC File Offset: 0x002439EC
	private void WillReset()
	{
		this.m_allNetAchievesReceived = false;
		this.m_achieveValidationsToRequest.Clear();
		this.m_achieveValidationsRequested.Clear();
		this.m_achievesUpdatedListeners.Clear();
		this.m_lastEventTimingValidationByAchieve.Clear();
		this.m_lastCheckLicenseAddedByAchieve.Clear();
		this.m_licenseAddedAchievesUpdatedListeners.Clear();
		this.m_achievements.Clear();
	}

	// Token: 0x060070C1 RID: 28865 RVA: 0x0024584D File Offset: 0x00243A4D
	private void OnReset()
	{
		this.LoadAchievesFromDBF();
	}

	// Token: 0x060070C2 RID: 28866 RVA: 0x00245855 File Offset: 0x00243A55
	public static AchieveManager Get()
	{
		return HearthstoneServices.Get<AchieveManager>();
	}

	// Token: 0x060070C3 RID: 28867 RVA: 0x0024585C File Offset: 0x00243A5C
	public static bool IsPredicateTrue(Assets.Achieve.AltTextPredicate predicate)
	{
		return predicate == Assets.Achieve.AltTextPredicate.CAN_SEE_WILD && CollectionManager.Get() != null && CollectionManager.Get().ShouldAccountSeeStandardWild();
	}

	// Token: 0x060070C4 RID: 28868 RVA: 0x00245878 File Offset: 0x00243A78
	public void InitAchieveManager()
	{
		this.WillReset();
		this.LoadAchievesFromDBF();
	}

	// Token: 0x060070C5 RID: 28869 RVA: 0x00245886 File Offset: 0x00243A86
	public bool IsAchievementVisuallyBlocklisted(int achieveId)
	{
		return this.m_visualBlocklist.Contains(achieveId);
	}

	// Token: 0x060070C6 RID: 28870 RVA: 0x00245894 File Offset: 0x00243A94
	public bool IsReady()
	{
		return this.m_allNetAchievesReceived && this.m_numEventResponsesNeeded <= 0 && this.m_achieveValidationsToRequest.Count <= 0 && this.m_achieveValidationsRequested.Count <= 0 && NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>() != null;
	}

	// Token: 0x060070C7 RID: 28871 RVA: 0x002458E8 File Offset: 0x00243AE8
	public bool RegisterAchievesUpdatedListener(AchieveManager.AchievesUpdatedCallback callback, object userData = null)
	{
		if (callback == null)
		{
			return false;
		}
		AchieveManager.AchievesUpdatedListener achievesUpdatedListener = new AchieveManager.AchievesUpdatedListener();
		achievesUpdatedListener.SetCallback(callback);
		achievesUpdatedListener.SetUserData(userData);
		if (this.m_achievesUpdatedListeners.Contains(achievesUpdatedListener))
		{
			return false;
		}
		this.m_achievesUpdatedListeners.Add(achievesUpdatedListener);
		return true;
	}

	// Token: 0x060070C8 RID: 28872 RVA: 0x0024592B File Offset: 0x00243B2B
	public bool RemoveAchievesUpdatedListener(AchieveManager.AchievesUpdatedCallback callback)
	{
		return this.RemoveAchievesUpdatedListener(callback, null);
	}

	// Token: 0x060070C9 RID: 28873 RVA: 0x00245938 File Offset: 0x00243B38
	public bool RemoveAchievesUpdatedListener(AchieveManager.AchievesUpdatedCallback callback, object userData)
	{
		if (callback == null)
		{
			return false;
		}
		AchieveManager.AchievesUpdatedListener achievesUpdatedListener = new AchieveManager.AchievesUpdatedListener();
		achievesUpdatedListener.SetCallback(callback);
		achievesUpdatedListener.SetUserData(userData);
		if (!this.m_achievesUpdatedListeners.Contains(achievesUpdatedListener))
		{
			return false;
		}
		this.m_achievesUpdatedListeners.Remove(achievesUpdatedListener);
		return true;
	}

	// Token: 0x060070CA RID: 28874 RVA: 0x0024597C File Offset: 0x00243B7C
	public List<global::Achievement> GetNewCompletedAchievesToShow()
	{
		return this.m_achievements.Values.ToList<global::Achievement>().FindAll(delegate(global::Achievement obj)
		{
			if (!obj.IsNewlyCompleted())
			{
				return false;
			}
			if (obj.IsInternal())
			{
				return false;
			}
			if (this.IsAchievementVisuallyBlocklisted(obj.ID))
			{
				return false;
			}
			if (obj.RewardTiming == Assets.Achieve.RewardTiming.NEVER)
			{
				return false;
			}
			Assets.Achieve.Type achieveType = obj.AchieveType;
			return achieveType - Assets.Achieve.Type.HERO > 1 && achieveType != Assets.Achieve.Type.DAILY_REPEATABLE && !obj.IsGenericRewardChest && (!QuestManager.Get().IsSystemEnabled || GameDbf.Quest.GetRecord((QuestDbfRecord r) => r.ProxyForLegacyId == obj.DbfRecord.ID) == null);
		});
	}

	// Token: 0x060070CB RID: 28875 RVA: 0x0024599F File Offset: 0x00243B9F
	private static bool IsActiveQuest(global::Achievement obj, bool onlyNewlyActive)
	{
		return obj.Active && obj.CanShowInQuestLog && (!onlyNewlyActive || obj.IsNewlyActive());
	}

	// Token: 0x060070CC RID: 28876 RVA: 0x002459C0 File Offset: 0x00243BC0
	private static bool IsAutoDestroyQuest(global::Achievement obj)
	{
		return obj.CanShowInQuestLog && obj.AutoDestroy;
	}

	// Token: 0x060070CD RID: 28877 RVA: 0x002459D2 File Offset: 0x00243BD2
	private static bool IsDialogQuest(global::Achievement obj)
	{
		return obj.CanShowInQuestLog && obj.QuestDialogId != 0;
	}

	// Token: 0x060070CE RID: 28878 RVA: 0x002459E8 File Offset: 0x00243BE8
	public List<global::Achievement> GetActiveQuests(bool onlyNewlyActive = false)
	{
		return (from obj in this.m_achievements.Values
		where AchieveManager.IsActiveQuest(obj, onlyNewlyActive)
		select obj).ToList<global::Achievement>();
	}

	// Token: 0x060070CF RID: 28879 RVA: 0x00245A24 File Offset: 0x00243C24
	public bool HasQuestsToShow(bool onlyNewlyActive = false)
	{
		bool result = false;
		foreach (KeyValuePair<int, global::Achievement> keyValuePair in this.m_achievements)
		{
			if (AchieveManager.IsActiveQuest(keyValuePair.Value, false) && (keyValuePair.Value.IsNewlyActive() || keyValuePair.Value.AutoDestroy))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	// Token: 0x060070D0 RID: 28880 RVA: 0x00245AA4 File Offset: 0x00243CA4
	public bool MarkQuestAsSeenByPlayerThisSession(global::Achievement obj)
	{
		return this.m_achievesSeenByPlayerThisSession.Add(obj.ID);
	}

	// Token: 0x060070D1 RID: 28881 RVA: 0x00245AB7 File Offset: 0x00243CB7
	public bool ResetQuestSeenByPlayerThisSession(global::Achievement obj)
	{
		return this.m_achievesSeenByPlayerThisSession.Remove(obj.ID);
	}

	// Token: 0x060070D2 RID: 28882 RVA: 0x00245ACC File Offset: 0x00243CCC
	public bool HasActiveQuests(bool onlyNewlyActive = false)
	{
		return this.m_achievements.Any((KeyValuePair<int, global::Achievement> kv) => AchieveManager.IsActiveQuest(kv.Value, onlyNewlyActive));
	}

	// Token: 0x060070D3 RID: 28883 RVA: 0x00245AFD File Offset: 0x00243CFD
	public bool HasActiveAutoDestroyQuests()
	{
		return this.m_achievements.Any((KeyValuePair<int, global::Achievement> kv) => AchieveManager.IsActiveQuest(kv.Value, false) && AchieveManager.IsAutoDestroyQuest(kv.Value));
	}

	// Token: 0x060070D4 RID: 28884 RVA: 0x00245B29 File Offset: 0x00243D29
	public bool HasActiveUnseenWelcomeQuestDialog()
	{
		return this.m_achievements.Any((KeyValuePair<int, global::Achievement> kv) => AchieveManager.IsActiveQuest(kv.Value, false) && AchieveManager.IsDialogQuest(kv.Value) && Options.Get().GetInt(Option.LATEST_SEEN_WELCOME_QUEST_DIALOG) != kv.Value.ID);
	}

	// Token: 0x060070D5 RID: 28885 RVA: 0x00245B55 File Offset: 0x00243D55
	public bool HasActiveDialogQuests()
	{
		return this.m_achievements.Any((KeyValuePair<int, global::Achievement> kv) => AchieveManager.IsActiveQuest(kv.Value, false) && AchieveManager.IsDialogQuest(kv.Value));
	}

	// Token: 0x060070D6 RID: 28886 RVA: 0x00245B84 File Offset: 0x00243D84
	public bool HasActiveQuestId(AchievementDbId id)
	{
		using (List<global::Achievement>.Enumerator enumerator = this.GetActiveQuests(false).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ID == (int)id)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060070D7 RID: 28887 RVA: 0x00245BE0 File Offset: 0x00243DE0
	public List<global::Achievement> GetNewlyProgressedQuests()
	{
		return AchieveManager.Get().GetActiveQuests(false).FindAll((global::Achievement obj) => obj.AcknowledgedProgress < obj.Progress && obj.Progress > 0 && obj.Progress < obj.MaxProgress);
	}

	// Token: 0x060070D8 RID: 28888 RVA: 0x00245C14 File Offset: 0x00243E14
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
				return this.HasUnlockedVanillaHeroes();
			}
		}
		global::Achievement value = this.m_achievements.FirstOrDefault((KeyValuePair<int, global::Achievement> kv) => kv.Value.UnlockedFeature == feature).Value;
		if (value == null)
		{
			Debug.LogWarning(string.Format("AchieveManager.HasUnlockedFeature(): could not find achieve that unlocks feature {0}", feature));
			return false;
		}
		return value.IsCompleted();
	}

	// Token: 0x060070D9 RID: 28889 RVA: 0x00245CAC File Offset: 0x00243EAC
	public bool HasUnlockedVanillaHeroes()
	{
		foreach (object obj in Enum.GetValues(typeof(ClassUnlockAchieveIds)))
		{
			int num = (int)obj;
			global::Achievement achievement = this.GetAchievement(num);
			if (achievement == null)
			{
				Debug.LogWarning(string.Format("AchieveManager.HasUnlockedVanillaHeroes(): could not find ClassUnlockAchieve with ID {0}", num));
			}
			else if (!achievement.IsCompleted())
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060070DA RID: 28890 RVA: 0x00245D3C File Offset: 0x00243F3C
	public bool HasUnlockedArena()
	{
		return this.HasUnlockedVanillaHeroes();
	}

	// Token: 0x060070DB RID: 28891 RVA: 0x00245D44 File Offset: 0x00243F44
	public global::Achievement GetAchievement(int achieveID)
	{
		if (!this.m_achievements.ContainsKey(achieveID))
		{
			return null;
		}
		return this.m_achievements[achieveID];
	}

	// Token: 0x060070DC RID: 28892 RVA: 0x00245D64 File Offset: 0x00243F64
	public int GetMaxProgressForAchievement(int achieveID)
	{
		int result = 0;
		if (achieveID > 0)
		{
			global::Achievement achievement = this.GetAchievement(achieveID);
			if (achievement != null)
			{
				result = achievement.MaxProgress;
			}
		}
		return result;
	}

	// Token: 0x060070DD RID: 28893 RVA: 0x00245D8A File Offset: 0x00243F8A
	public int GetNumAchievesInGroup(Assets.Achieve.Type achieveType)
	{
		return this.GetAchievesInGroup(achieveType).Count;
	}

	// Token: 0x060070DE RID: 28894 RVA: 0x00245D98 File Offset: 0x00243F98
	public IEnumerable<global::Achievement> GetCompletedAchieves()
	{
		return this.GetAchieves((global::Achievement a) => a.IsCompleted());
	}

	// Token: 0x060070DF RID: 28895 RVA: 0x00245DC0 File Offset: 0x00243FC0
	public List<global::Achievement> GetAchievesInGroup(Assets.Achieve.Type achieveGroup)
	{
		return new List<global::Achievement>(this.m_achievements.Values).FindAll((global::Achievement obj) => obj.AchieveType == achieveGroup);
	}

	// Token: 0x060070E0 RID: 28896 RVA: 0x00245DFC File Offset: 0x00243FFC
	public List<global::Achievement> GetAchievesInGroup(Assets.Achieve.Type achieveGroup, bool isComplete)
	{
		return this.GetAchievesInGroup(achieveGroup).FindAll((global::Achievement obj) => obj.IsCompleted() == isComplete);
	}

	// Token: 0x060070E1 RID: 28897 RVA: 0x00245E30 File Offset: 0x00244030
	public List<global::Achievement> GetAchievesForAdventureWing(int wingID)
	{
		return new List<global::Achievement>(this.m_achievements.Values).FindAll((global::Achievement obj) => obj.Enabled && obj.WingID == wingID);
	}

	// Token: 0x060070E2 RID: 28898 RVA: 0x00245E6C File Offset: 0x0024406C
	public List<global::Achievement> GetAchievesForAdventureAndMode(int adventureId, int modeId)
	{
		return new List<global::Achievement>(this.m_achievements.Values).FindAll((global::Achievement obj) => obj.AdventureID == adventureId && obj.AdventureModeID == modeId);
	}

	// Token: 0x060070E3 RID: 28899 RVA: 0x00245EB0 File Offset: 0x002440B0
	public global::Achievement GetUnlockGoldenHeroAchievement(string heroCardID, TAG_PREMIUM premium)
	{
		return this.GetAchievesInGroup(Assets.Achieve.Type.GOLDHERO).Find(delegate(global::Achievement achieveObj)
		{
			RewardData rewardData = achieveObj.Rewards.Find((RewardData rewardObj) => rewardObj.RewardType == Reward.Type.CARD);
			if (rewardData == null)
			{
				return false;
			}
			CardRewardData cardRewardData = rewardData as CardRewardData;
			return cardRewardData != null && cardRewardData.CardID.Equals(heroCardID) && cardRewardData.Premium.Equals(premium);
		});
	}

	// Token: 0x060070E4 RID: 28900 RVA: 0x00245EEC File Offset: 0x002440EC
	public global::Achievement GetUnlockPremiumHeroAchievement(TAG_CLASS heroClass)
	{
		return this.GetAchievesInGroup(Assets.Achieve.Type.PREMIUMHERO).Find(delegate(global::Achievement achieveObj)
		{
			RewardData rewardData = achieveObj.Rewards.Find((RewardData rewardObj) => rewardObj.RewardType == Reward.Type.CARD);
			return rewardData != null && rewardData is CardRewardData && achieveObj.MyHeroClassRequirement.Equals(heroClass);
		});
	}

	// Token: 0x060070E5 RID: 28901 RVA: 0x00245F20 File Offset: 0x00244120
	public bool HasActiveAchievesForEvent(SpecialEventType eventTrigger)
	{
		return eventTrigger != SpecialEventType.IGNORE && this.m_achievements.Any(delegate(KeyValuePair<int, global::Achievement> kv)
		{
			global::Achievement value = kv.Value;
			return value.EventTrigger == eventTrigger && value.Enabled && value.Active;
		});
	}

	// Token: 0x060070E6 RID: 28902 RVA: 0x00245F5C File Offset: 0x0024415C
	public bool CanCancelQuest(int achieveID)
	{
		if (this.m_disableCancelButtonUntilServerReturns)
		{
			return false;
		}
		if (!this.CanCancelQuestNow())
		{
			return false;
		}
		if (!AchieveManager.HasAccessToDailies())
		{
			return false;
		}
		global::Achievement achievement = this.GetAchievement(achieveID);
		return achievement != null && achievement.CanBeCancelled && achievement.Active;
	}

	// Token: 0x060070E7 RID: 28903 RVA: 0x00245FA3 File Offset: 0x002441A3
	public static bool HasAccessToDailies()
	{
		return AchieveManager.Get().HasUnlockedFeature(Assets.Achieve.Unlocks.DAILY);
	}

	// Token: 0x060070E8 RID: 28904 RVA: 0x00245FB5 File Offset: 0x002441B5
	public bool RegisterQuestCanceledListener(AchieveManager.AchieveCanceledCallback callback)
	{
		return this.RegisterQuestCanceledListener(callback, null);
	}

	// Token: 0x060070E9 RID: 28905 RVA: 0x00245FC0 File Offset: 0x002441C0
	public bool RegisterQuestCanceledListener(AchieveManager.AchieveCanceledCallback callback, object userData)
	{
		AchieveManager.AchieveCanceledListener achieveCanceledListener = new AchieveManager.AchieveCanceledListener();
		achieveCanceledListener.SetCallback(callback);
		achieveCanceledListener.SetUserData(userData);
		if (this.m_achieveCanceledListeners.Contains(achieveCanceledListener))
		{
			return false;
		}
		this.m_achieveCanceledListeners.Add(achieveCanceledListener);
		return true;
	}

	// Token: 0x060070EA RID: 28906 RVA: 0x00245FFE File Offset: 0x002441FE
	public bool RemoveQuestCanceledListener(AchieveManager.AchieveCanceledCallback callback)
	{
		return this.RemoveQuestCanceledListener(callback, null);
	}

	// Token: 0x060070EB RID: 28907 RVA: 0x00246008 File Offset: 0x00244208
	public bool RemoveQuestCanceledListener(AchieveManager.AchieveCanceledCallback callback, object userData)
	{
		AchieveManager.AchieveCanceledListener achieveCanceledListener = new AchieveManager.AchieveCanceledListener();
		achieveCanceledListener.SetCallback(callback);
		achieveCanceledListener.SetUserData(userData);
		return this.m_achieveCanceledListeners.Remove(achieveCanceledListener);
	}

	// Token: 0x060070EC RID: 28908 RVA: 0x00246035 File Offset: 0x00244235
	public void CancelQuest(int achieveID)
	{
		if (!this.CanCancelQuest(achieveID))
		{
			this.FireAchieveCanceledEvent(achieveID, false);
			return;
		}
		this.BlockAllNotifications();
		this.m_disableCancelButtonUntilServerReturns = true;
		Network.Get().RequestCancelQuest(achieveID);
	}

	// Token: 0x060070ED RID: 28909 RVA: 0x00246061 File Offset: 0x00244261
	public bool RegisterLicenseAddedAchievesUpdatedListener(AchieveManager.LicenseAddedAchievesUpdatedCallback callback)
	{
		return this.RegisterLicenseAddedAchievesUpdatedListener(callback, null);
	}

	// Token: 0x060070EE RID: 28910 RVA: 0x0024606C File Offset: 0x0024426C
	public bool RegisterLicenseAddedAchievesUpdatedListener(AchieveManager.LicenseAddedAchievesUpdatedCallback callback, object userData)
	{
		AchieveManager.LicenseAddedAchievesUpdatedListener licenseAddedAchievesUpdatedListener = new AchieveManager.LicenseAddedAchievesUpdatedListener();
		licenseAddedAchievesUpdatedListener.SetCallback(callback);
		licenseAddedAchievesUpdatedListener.SetUserData(userData);
		if (this.m_licenseAddedAchievesUpdatedListeners.Contains(licenseAddedAchievesUpdatedListener))
		{
			return false;
		}
		this.m_licenseAddedAchievesUpdatedListeners.Add(licenseAddedAchievesUpdatedListener);
		return true;
	}

	// Token: 0x060070EF RID: 28911 RVA: 0x002460AA File Offset: 0x002442AA
	public bool RemoveLicenseAddedAchievesUpdatedListener(AchieveManager.LicenseAddedAchievesUpdatedCallback callback)
	{
		return this.RemoveLicenseAddedAchievesUpdatedListener(callback, null);
	}

	// Token: 0x060070F0 RID: 28912 RVA: 0x002460B4 File Offset: 0x002442B4
	public bool RemoveLicenseAddedAchievesUpdatedListener(AchieveManager.LicenseAddedAchievesUpdatedCallback callback, object userData)
	{
		AchieveManager.LicenseAddedAchievesUpdatedListener licenseAddedAchievesUpdatedListener = new AchieveManager.LicenseAddedAchievesUpdatedListener();
		licenseAddedAchievesUpdatedListener.SetCallback(callback);
		licenseAddedAchievesUpdatedListener.SetUserData(userData);
		return this.m_licenseAddedAchievesUpdatedListeners.Remove(licenseAddedAchievesUpdatedListener);
	}

	// Token: 0x060070F1 RID: 28913 RVA: 0x002460E1 File Offset: 0x002442E1
	public bool HasActiveLicenseAddedAchieves()
	{
		return this.GetActiveLicenseAddedAchieves().Count > 0;
	}

	// Token: 0x060070F2 RID: 28914 RVA: 0x002460F1 File Offset: 0x002442F1
	public bool HasIncompletePurchaseAchieves()
	{
		return this.m_achievements.Any(delegate(KeyValuePair<int, global::Achievement> kv)
		{
			global::Achievement value = kv.Value;
			return !value.IsCompleted() && value.Enabled && value.AchieveTrigger == Assets.Achieve.Trigger.PURCHASE;
		});
	}

	// Token: 0x060070F3 RID: 28915 RVA: 0x0024611D File Offset: 0x0024431D
	public bool HasIncompleteDisenchantAchieves()
	{
		return this.m_achievements.Any(delegate(KeyValuePair<int, global::Achievement> kv)
		{
			global::Achievement value = kv.Value;
			return !value.IsCompleted() && value.Enabled && value.AchieveTrigger == Assets.Achieve.Trigger.DISENCHANT;
		});
	}

	// Token: 0x060070F4 RID: 28916 RVA: 0x0024614C File Offset: 0x0024434C
	public void NotifyOfClick(global::Achievement.ClickTriggerType clickType)
	{
		global::Log.Achievements.Print("AchieveManager.NotifyOfClick(): clickType {0}", new object[]
		{
			clickType
		});
		bool hasAllVanillaHeroes = this.HasUnlockedFeature(Assets.Achieve.Unlocks.VANILLA_HEROES);
		foreach (global::Achievement achievement in this.GetAchieves(delegate(global::Achievement obj)
		{
			if (obj.AchieveTrigger != Assets.Achieve.Trigger.CLICK)
			{
				return false;
			}
			if (!obj.Enabled)
			{
				global::Log.Achievements.Print("AchieveManager.NotifyOfClick(): skip disabled achieve {0}", new object[]
				{
					obj.ID
				});
				return false;
			}
			if (obj.IsCompleted())
			{
				global::Log.Achievements.Print("AchieveManager.NotifyOfClick(): skip already completed achieve {0}", new object[]
				{
					obj.ID
				});
				return false;
			}
			if (obj.ClickType == null)
			{
				global::Log.Achievements.Print("AchieveManager.NotifyOfClick(): skip missing ClickType achieve {0}", new object[]
				{
					obj.ID
				});
				return false;
			}
			if (obj.ClickType.Value != clickType)
			{
				global::Log.Achievements.Print("AchieveManager.NotifyOfClick(): skip achieve {0} with non-matching ClickType {1}", new object[]
				{
					obj.ID,
					obj.ClickType.Value
				});
				return false;
			}
			if (clickType == global::Achievement.ClickTriggerType.BUTTON_ADVENTURE && !hasAllVanillaHeroes && AdventureUtils.DoesAdventureRequireAllHeroesUnlocked((AdventureDbId)obj.AdventureID))
			{
				global::Log.Achievements.Print("AchieveManager.NotifyOfClick(): skip achieve {0} for BUTTON_ADVENTURE requiring all heroes unlocked", new object[]
				{
					obj.ID
				});
				return false;
			}
			return true;
		}))
		{
			global::Log.Achievements.Print("AchieveManager.NotifyOfClick(): add achieve {0}", new object[]
			{
				achievement.ID
			});
			this.m_achieveValidationsToRequest.Add(achievement.ID);
		}
		this.ValidateAchievesNow();
	}

	// Token: 0x060070F5 RID: 28917 RVA: 0x00246218 File Offset: 0x00244418
	public void CompleteAutoDestroyAchieve(int achieveId)
	{
		foreach (global::Achievement achievement in this.GetAchieves((global::Achievement obj) => !obj.IsCompleted() && obj.Enabled && obj.Active && obj.AchieveTrigger == Assets.Achieve.Trigger.DESTROYED))
		{
			if (achievement.ID == achieveId)
			{
				this.m_achieveValidationsToRequest.Add(achievement.ID);
			}
		}
		this.ValidateAchievesNow();
	}

	// Token: 0x060070F6 RID: 28918 RVA: 0x002462A0 File Offset: 0x002444A0
	public void NotifyOfAccountCreation()
	{
		foreach (global::Achievement achievement in this.GetAchieves((global::Achievement obj) => !obj.IsCompleted() && obj.Enabled && obj.AchieveTrigger == Assets.Achieve.Trigger.ACCOUNT_CREATED))
		{
			this.m_achieveValidationsToRequest.Add(achievement.ID);
		}
		this.ValidateAchievesNow();
	}

	// Token: 0x060070F7 RID: 28919 RVA: 0x00246320 File Offset: 0x00244520
	public void NotifyOfPacksReadyToOpen(UnopenedPack unopenedPack)
	{
		IEnumerable<global::Achievement> achieves = this.GetAchieves((global::Achievement obj) => obj.Enabled && !obj.IsCompleted() && obj.AchieveTrigger == Assets.Achieve.Trigger.PACK_READY_TO_OPEN && obj.BoosterRequirement == unopenedPack.GetBoosterStack().Id && unopenedPack.GetBoosterStack().Count != 0 && unopenedPack.CanOpenPack());
		bool flag = false;
		foreach (global::Achievement achievement in achieves)
		{
			this.m_achieveValidationsToRequest.Add(achievement.ID);
			flag = true;
		}
		if (flag)
		{
			this.ValidateAchievesNow();
		}
	}

	// Token: 0x060070F8 RID: 28920 RVA: 0x002463A0 File Offset: 0x002445A0
	public void Update()
	{
		if (!Network.IsRunning())
		{
			return;
		}
		this.CheckTimedEventsAndLicenses(DateTime.UtcNow);
	}

	// Token: 0x060070F9 RID: 28921 RVA: 0x002463B8 File Offset: 0x002445B8
	public void ValidateAchievesNow()
	{
		if (this.m_achieveValidationsToRequest.Count == 0)
		{
			return;
		}
		this.m_achieveValidationsRequested.Union(this.m_achieveValidationsToRequest);
		using (HashSet<int>.Enumerator enumerator = this.m_achieveValidationsToRequest.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int achieveID = enumerator.Current;
				AchieveRegionDataDbfRecord record = GameDbf.AchieveRegionData.GetRecord((AchieveRegionDataDbfRecord r) => r.AchieveId == achieveID);
				if (record != null && !SpecialEventManager.Get().IsEventActive(record.ProgressableEvent, false))
				{
					global::Log.Achievements.Print("AchieveManager.ValidateAchievesNow(): skip non-progressable achieve {0} event {1}", new object[]
					{
						achieveID,
						record.ProgressableEvent
					});
				}
				else
				{
					global::Log.Achievements.Print("AchieveManager.ValidateAchievesNow(): ValidateAchieve {0}", new object[]
					{
						achieveID
					});
					Network.Get().ValidateAchieve(achieveID);
				}
			}
		}
		this.m_achieveValidationsToRequest.Clear();
	}

	// Token: 0x060070FA RID: 28922 RVA: 0x002464D0 File Offset: 0x002446D0
	public void CheckPlayedNearbyPlayerOnSubnet()
	{
		if (!this.HasActiveAchievesForEvent(SpecialEventType.FIRESIDE_GATHERINGS_CARDBACK))
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
		foreach (BnetPlayer bnetPlayer3 in nearbyPlayers)
		{
			BnetAccountId accountId2 = bnetPlayer3.GetAccountId();
			if (!(accountId2 == null) && !accountId2.Equals(accountId))
			{
				bnetPlayer2 = bnetPlayer3;
				break;
			}
		}
		if (bnetPlayer2 == null)
		{
			return;
		}
		ulong lastOpponentSessionStartTime;
		if (!BnetNearbyPlayerMgr.Get().GetNearbySessionStartTime(bnetPlayer, out lastOpponentSessionStartTime))
		{
			return;
		}
		ulong otherPlayerSessionStartTime;
		if (!BnetNearbyPlayerMgr.Get().GetNearbySessionStartTime(bnetPlayer2, out otherPlayerSessionStartTime))
		{
			return;
		}
		BnetGameAccountId hearthstoneGameAccountId = bnetPlayer.GetHearthstoneGameAccountId();
		if (hearthstoneGameAccountId == null)
		{
			return;
		}
		BnetGameAccountId hearthstoneGameAccountId2 = bnetPlayer2.GetHearthstoneGameAccountId();
		if (hearthstoneGameAccountId2 == null)
		{
			return;
		}
		this.m_numEventResponsesNeeded++;
		Network.Get().TriggerPlayedNearbyPlayerOnSubnet(hearthstoneGameAccountId, lastOpponentSessionStartTime, hearthstoneGameAccountId2, otherPlayerSessionStartTime);
	}

	// Token: 0x060070FB RID: 28923 RVA: 0x002465F0 File Offset: 0x002447F0
	public void LoadAchievesFromDBF()
	{
		this.m_achievements.Clear();
		List<AchieveDbfRecord> records = GameDbf.Achieve.GetRecords();
		List<CharacterDialogDbfRecord> records2 = GameDbf.CharacterDialog.GetRecords();
		global::Map<int, int> map = new global::Map<int, int>();
		using (List<AchieveDbfRecord>.Enumerator enumerator = records.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				AchieveDbfRecord achieveRecord = enumerator.Current;
				int id = achieveRecord.ID;
				int race = achieveRecord.Race;
				TAG_RACE? raceReq = null;
				if (race != 0)
				{
					raceReq = new TAG_RACE?((TAG_RACE)race);
				}
				int cardSet = achieveRecord.CardSet;
				TAG_CARD_SET? cardSetReq = null;
				if (cardSet != 0)
				{
					cardSetReq = new TAG_CARD_SET?((TAG_CARD_SET)cardSet);
				}
				int myHeroClassId = achieveRecord.MyHeroClassId;
				TAG_CLASS? myHeroClassReq = null;
				if (myHeroClassId != 0)
				{
					myHeroClassReq = new TAG_CLASS?((TAG_CLASS)myHeroClassId);
				}
				long rewardData = achieveRecord.RewardData1;
				long rewardData2 = achieveRecord.RewardData2;
				bool isGenericRewardChest = false;
				string chestVisualPrefabPath = "";
				List<RewardData> list = new List<RewardData>();
				TAG_CLASS? classReward = null;
				string reward = achieveRecord.Reward;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(reward);
				if (num <= 2284280159U)
				{
					if (num <= 634254458U)
					{
						if (num <= 414462460U)
						{
							if (num != 87360061U)
							{
								if (num == 414462460U)
								{
									if (reward == "event_notice")
									{
										int eventType = (rewardData > 0L) ? ((int)rewardData) : 0;
										list.Add(new EventRewardData(eventType));
									}
								}
							}
							else if (reward == "basic")
							{
								Debug.LogWarning(string.Format("AchieveManager.LoadAchievesFromFile(): unable to define reward {0} for achieve {1}", achieveRecord.Reward, id));
							}
						}
						else if (num != 456875097U)
						{
							if (num == 634254458U)
							{
								if (reward == "arcane_orbs")
								{
									list.Add(RewardUtils.CreateArcaneOrbRewardData((int)rewardData));
								}
							}
						}
						else if (reward == "hero")
						{
							classReward = new TAG_CLASS?((TAG_CLASS)rewardData2);
							string heroCardId = CollectionManager.GetHeroCardId(classReward.Value, CardHero.HeroType.VANILLA);
							if (!string.IsNullOrEmpty(heroCardId))
							{
								list.Add(new CardRewardData(heroCardId, TAG_PREMIUM.NORMAL, 1));
							}
						}
					}
					else if (num <= 932599448U)
					{
						if (num != 763242528U)
						{
							if (num == 932599448U)
							{
								if (reward == "cardback")
								{
									list.Add(new CardBackRewardData((int)rewardData));
								}
							}
						}
						else if (!(reward == "hero_level"))
						{
						}
					}
					else if (num != 1666399712U)
					{
						if (num != 1813408442U)
						{
							if (num == 2284280159U)
							{
								if (reward == "card")
								{
									string cardID = GameUtils.TranslateDbIdToCardId((int)rewardData, false);
									TAG_PREMIUM premium = (TAG_PREMIUM)rewardData2;
									list.Add(new CardRewardData(cardID, premium, 1));
								}
							}
						}
						else if (reward == "mount")
						{
							list.Add(new MountRewardData((MountRewardData.MountType)rewardData));
						}
					}
					else if (reward == "pack")
					{
						int id2 = (rewardData2 > 0L) ? ((int)rewardData2) : 1;
						list.Add(new BoosterPackRewardData(id2, (int)rewardData));
					}
				}
				else if (num <= 2866453672U)
				{
					if (num <= 2383510917U)
					{
						if (num != 2310183952U)
						{
							if (num == 2383510917U)
							{
								if (!(reward == "cardset"))
								{
								}
							}
						}
						else if (reward == "forge")
						{
							list.Add(new ForgeTicketRewardData((int)rewardData));
						}
					}
					else if (num != 2396082843U)
					{
						if (num == 2866453672U)
						{
							if (reward == "deck")
							{
								list.Add(RewardUtils.CreateDeckRewardData((int)rewardData, (int)rewardData2));
							}
						}
					}
					else if (!(reward == "craftable_golden"))
					{
					}
				}
				else if (num <= 3389733797U)
				{
					if (num != 3290395606U)
					{
						if (num == 3389733797U)
						{
							if (reward == "dust")
							{
								list.Add(new ArcaneDustRewardData((int)rewardData));
							}
						}
					}
					else if (reward == "generic_reward_chest")
					{
						isGenericRewardChest = true;
						list.AddRange(RewardUtils.GetRewardDataFromRewardChestAsset((int)rewardData, (int)rewardData2));
						chestVisualPrefabPath = GameDbf.RewardChest.GetRecord((int)rewardData).ChestPrefab;
					}
				}
				else if (num != 3516672573U)
				{
					if (num != 3966162835U)
					{
						if (num == 4072925295U)
						{
							if (reward == "goldhero")
							{
								string cardID2 = GameUtils.TranslateDbIdToCardId((int)rewardData, false);
								TAG_PREMIUM premium2 = (TAG_PREMIUM)rewardData2;
								list.Add(new CardRewardData(cardID2, premium2, 1));
							}
						}
					}
					else if (reward == "gold")
					{
						list.Add(new GoldRewardData((long)((int)rewardData)));
					}
				}
				else if (reward == "card2x")
				{
					string cardID3 = GameUtils.TranslateDbIdToCardId((int)rewardData, false);
					TAG_PREMIUM premium3 = (TAG_PREMIUM)rewardData2;
					list.Add(new CardRewardData(cardID3, premium3, 2));
				}
				Assets.Achieve.RewardTiming rewardTiming = achieveRecord.RewardTiming;
				AchieveDbfRecord achieveDbfRecord = records.Find((AchieveDbfRecord obj) => obj.NoteDesc == achieveRecord.ParentAch);
				int value = (achieveDbfRecord == null) ? 0 : achieveDbfRecord.ID;
				map[id] = value;
				AchieveDbfRecord achieveDbfRecord2 = records.Find((AchieveDbfRecord obj) => obj.NoteDesc == achieveRecord.LinkTo);
				int linkToId = (achieveDbfRecord2 == null) ? 0 : achieveDbfRecord2.ID;
				global::Achievement.ClickTriggerType? clickType = null;
				Assets.Achieve.Trigger triggered = achieveRecord.Triggered;
				if (triggered == Assets.Achieve.Trigger.CLICK)
				{
					clickType = new global::Achievement.ClickTriggerType?((global::Achievement.ClickTriggerType)rewardData);
				}
				if (achieveRecord.ID == 94)
				{
					clickType = new global::Achievement.ClickTriggerType?(global::Achievement.ClickTriggerType.BUTTON_ARENA);
				}
				List<int> scenarios = (from c in GameDbf.AchieveCondition.GetRecords((AchieveConditionDbfRecord a) => a.AchieveId == achieveRecord.ID, -1)
				select c.ScenarioId).ToList<int>();
				CharacterDialogDbfRecord characterDialogDbfRecord = records2.Find((CharacterDialogDbfRecord obj) => obj.ID == achieveRecord.QuestDialogId);
				int num2 = (characterDialogDbfRecord == null) ? 0 : characterDialogDbfRecord.ID;
				CharacterDialogSequence onReceivedDialogSequence = null;
				CharacterDialogSequence onCompleteDialogSequence = null;
				CharacterDialogSequence onProgress1DialogSequence = null;
				CharacterDialogSequence onProgress2DialogSequence = null;
				CharacterDialogSequence onDismissDialogSequence = null;
				if (characterDialogDbfRecord != null)
				{
					onReceivedDialogSequence = new CharacterDialogSequence(num2, CharacterDialogEventType.RECEIVE);
					onCompleteDialogSequence = new CharacterDialogSequence(num2, CharacterDialogEventType.COMPLETE);
					onProgress1DialogSequence = new CharacterDialogSequence(num2, CharacterDialogEventType.PROGRESS1);
					onProgress2DialogSequence = new CharacterDialogSequence(num2, CharacterDialogEventType.PROGRESS2);
					onDismissDialogSequence = new CharacterDialogSequence(num2, CharacterDialogEventType.DISMISS);
				}
				int onCompleteQuestDialogBannerId = (characterDialogDbfRecord == null) ? 0 : characterDialogDbfRecord.OnCompleteBannerId;
				global::Achievement achievement = new global::Achievement(achieveRecord, id, achieveRecord.AchType, achieveRecord.AchQuota, linkToId, achieveRecord.Triggered, achieveRecord.GameMode, raceReq, classReward, cardSetReq, myHeroClassReq, clickType, achieveRecord.Unlocks, list, scenarios, achieveRecord.AdventureWingId, achieveRecord.AdventureId, achieveRecord.AdventureModeId, rewardTiming, achieveRecord.Booster, achieveRecord.UseGenericRewardVisual, achieveRecord.ShowToReturningPlayer, num2, achieveRecord.AutoDestroy, achieveRecord.QuestTilePrefab, onCompleteQuestDialogBannerId, onReceivedDialogSequence, onCompleteDialogSequence, onProgress1DialogSequence, onProgress2DialogSequence, onDismissDialogSequence, isGenericRewardChest, chestVisualPrefabPath, achieveRecord.CustomVisualWidget, achieveRecord.EnemyHeroClassId);
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
				this.InitAchievement(achievement);
			}
		}
	}

	// Token: 0x060070FC RID: 28924 RVA: 0x00246ED8 File Offset: 0x002450D8
	private void InitAchievement(global::Achievement achievement)
	{
		if (this.m_achievements.ContainsKey(achievement.ID))
		{
			Debug.LogWarning(string.Format("AchieveManager.InitAchievement() - already registered achievement with ID {0}", achievement.ID));
			return;
		}
		this.m_achievements.Add(achievement.ID, achievement);
	}

	// Token: 0x060070FD RID: 28925 RVA: 0x00246F28 File Offset: 0x00245128
	private IEnumerable<global::Achievement> GetAchieves(Func<global::Achievement, bool> filter = null)
	{
		return this.m_achievements.Where(delegate(KeyValuePair<int, global::Achievement> kv)
		{
			if (filter != null)
			{
				Func<global::Achievement, bool> filter2 = filter;
				KeyValuePair<int, global::Achievement> keyValuePair = kv;
				return filter2(keyValuePair.Value);
			}
			return true;
		}).Select(delegate(KeyValuePair<int, global::Achievement> kv)
		{
			KeyValuePair<int, global::Achievement> keyValuePair = kv;
			return keyValuePair.Value;
		});
	}

	// Token: 0x060070FE RID: 28926 RVA: 0x00246F7D File Offset: 0x0024517D
	public void OnInitialAchievements(Achieves achievements)
	{
		if (achievements == null)
		{
			return;
		}
		this.OnAllAchieves(achievements);
	}

	// Token: 0x060070FF RID: 28927 RVA: 0x00246F8C File Offset: 0x0024518C
	private void OnAllAchieves(Achieves allAchievesList)
	{
		foreach (PegasusUtil.Achieve achieve in allAchievesList.List)
		{
			global::Achievement achievement = this.GetAchievement(achieve.Id);
			if (achievement != null)
			{
				achievement.OnAchieveData(achieve);
			}
		}
		this.CheckAllCardGainAchieves();
		this.m_allNetAchievesReceived = true;
		this.UnblockAllNotifications();
	}

	// Token: 0x06007100 RID: 28928 RVA: 0x00247004 File Offset: 0x00245204
	public void OnAchievementNotifications(List<AchievementNotification> achievementNotifications)
	{
		List<global::Achievement> list = new List<global::Achievement>();
		List<global::Achievement> list2 = new List<global::Achievement>();
		bool flag = false;
		foreach (AchievementNotification achievementNotification in achievementNotifications)
		{
			if (this.m_queueNotifications || !this.m_allNetAchievesReceived || this.m_achieveNotificationsToQueue.Contains((int)achievementNotification.AchievementId))
			{
				global::Log.Achievements.Print("Blocking AchievementNotification: ID={0}", new object[]
				{
					achievementNotification.AchievementId
				});
				this.m_blockedAchievementNotifications.Add(achievementNotification);
			}
			else
			{
				global::Achievement achievement = this.GetAchievement((int)achievementNotification.AchievementId);
				if (achievement != null)
				{
					AchieveDbfRecord record = GameDbf.Achieve.GetRecord(achievement.ID);
					if (record.VisualBlacklist != null)
					{
						foreach (VisualBlacklistDbfRecord visualBlacklistDbfRecord in record.VisualBlacklist)
						{
							AchieveDbfRecord record2 = GameDbf.Achieve.GetRecord(visualBlacklistDbfRecord.BlacklistAchieveId);
							this.m_visualBlocklist.Add(visualBlacklistDbfRecord.BlacklistAchieveId);
							Network.Get().AckAchieveProgress(visualBlacklistDbfRecord.BlacklistAchieveId, record2.AchQuota);
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
					global::Log.Achievements.Print("OnAchievementNotification: Achievement={0}", new object[]
					{
						achievement
					});
				}
			}
		}
		if (flag)
		{
			this.m_lastEventTimingAndLicenseAchieveCheck = 0L;
		}
		AchieveManager.AchievesUpdatedListener[] array = this.m_achievesUpdatedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(list2, list);
		}
	}

	// Token: 0x06007101 RID: 28929 RVA: 0x00247218 File Offset: 0x00245418
	private void BlockNotification(int achieveIdToQueue)
	{
		this.BlockNotifications(new List<int>
		{
			achieveIdToQueue
		});
	}

	// Token: 0x06007102 RID: 28930 RVA: 0x0024723C File Offset: 0x0024543C
	private void BlockNotifications(List<int> achieveIdsToQueue)
	{
		for (int i = 0; i < achieveIdsToQueue.Count; i++)
		{
			this.m_achieveNotificationsToQueue.Add(achieveIdsToQueue[i]);
		}
	}

	// Token: 0x06007103 RID: 28931 RVA: 0x0024726C File Offset: 0x0024546C
	private void UnblockNotification(int achieveIdToUnblock)
	{
		this.UnblockNotifications(new List<int>
		{
			achieveIdToUnblock
		});
	}

	// Token: 0x06007104 RID: 28932 RVA: 0x00247290 File Offset: 0x00245490
	private void UnblockNotifications(List<int> achieveIdsToUnblock)
	{
		List<AchievementNotification> list = (from obj in this.m_blockedAchievementNotifications
		where achieveIdsToUnblock.Contains((int)obj.AchievementId)
		select obj).ToList<AchievementNotification>();
		this.m_blockedAchievementNotifications.RemoveAll((AchievementNotification obj) => achieveIdsToUnblock.Contains((int)obj.AchievementId));
		this.m_achieveNotificationsToQueue.RemoveAll((int id) => achieveIdsToUnblock.Contains(id));
		if (list.Count > 0)
		{
			this.OnAchievementNotifications(list);
		}
	}

	// Token: 0x06007105 RID: 28933 RVA: 0x00247307 File Offset: 0x00245507
	public void BlockAllNotifications()
	{
		this.m_queueNotifications = true;
	}

	// Token: 0x06007106 RID: 28934 RVA: 0x00247310 File Offset: 0x00245510
	public void UnblockAllNotifications()
	{
		this.m_queueNotifications = false;
		if (this.m_blockedAchievementNotifications.Count > 0)
		{
			this.OnAchievementNotifications(this.m_blockedAchievementNotifications);
			this.m_blockedAchievementNotifications.Clear();
		}
	}

	// Token: 0x06007107 RID: 28935 RVA: 0x00247340 File Offset: 0x00245540
	private void OnQuestCanceled()
	{
		Network.CanceledQuest canceledQuest = Network.Get().GetCanceledQuest();
		global::Log.Achievements.Print("OnQuestCanceled: CanceledQuest={0}", new object[]
		{
			canceledQuest
		});
		this.m_disableCancelButtonUntilServerReturns = false;
		if (canceledQuest.Canceled)
		{
			this.GetAchievement(canceledQuest.AchieveID).OnCancelSuccess();
			NetCache.NetCacheRewardProgress netObject = NetCache.Get().GetNetObject<NetCache.NetCacheRewardProgress>();
			if (netObject != null)
			{
				netObject.NextQuestCancelDate = canceledQuest.NextQuestCancelDate;
			}
		}
		this.FireAchieveCanceledEvent(canceledQuest.AchieveID, canceledQuest.Canceled);
		this.UnblockAllNotifications();
	}

	// Token: 0x06007108 RID: 28936 RVA: 0x002473C4 File Offset: 0x002455C4
	private void OnAchieveValidated()
	{
		ValidateAchieveResponse validatedAchieve = Network.Get().GetValidatedAchieve();
		this.m_achieveValidationsRequested.Remove(validatedAchieve.Achieve);
		global::Log.Achievements.Print("AchieveManager.OnAchieveValidated(): achieve={0} success={1}", new object[]
		{
			validatedAchieve.Achieve,
			validatedAchieve.Success
		});
	}

	// Token: 0x06007109 RID: 28937 RVA: 0x0024741F File Offset: 0x0024561F
	private void OnEventTriggered()
	{
		Network.Get().GetTriggerEventResponse();
		this.m_numEventResponsesNeeded--;
	}

	// Token: 0x0600710A RID: 28938 RVA: 0x0024743C File Offset: 0x0024563C
	private void OnAccountLicenseAchieveResponse()
	{
		Network.AccountLicenseAchieveResponse accountLicenseAchieveResponse = Network.Get().GetAccountLicenseAchieveResponse();
		if (accountLicenseAchieveResponse.Result != Network.AccountLicenseAchieveResponse.AchieveResult.COMPLETE)
		{
			this.FireLicenseAddedAchievesUpdatedEvent();
			return;
		}
		global::Log.Achievements.Print("AchieveManager.OnAccountLicenseAchieveResponse(): achieve {0} is now complete, refreshing achieves", new object[]
		{
			accountLicenseAchieveResponse.Achieve
		});
		this.OnAccountLicenseAchievesUpdated(accountLicenseAchieveResponse.Achieve);
	}

	// Token: 0x0600710B RID: 28939 RVA: 0x00247498 File Offset: 0x00245698
	private void OnAccountLicenseAchievesUpdated(object userData)
	{
		int num = (int)userData;
		global::Log.Achievements.Print("AchieveManager.OnAccountLicenseAchievesUpdated(): refreshing achieves complete, triggered by achieve {0}", new object[]
		{
			num
		});
		this.FireLicenseAddedAchievesUpdatedEvent();
	}

	// Token: 0x0600710C RID: 28940 RVA: 0x002474D0 File Offset: 0x002456D0
	private void FireLicenseAddedAchievesUpdatedEvent()
	{
		List<global::Achievement> activeLicenseAddedAchieves = this.GetActiveLicenseAddedAchieves();
		AchieveManager.LicenseAddedAchievesUpdatedListener[] array = this.m_licenseAddedAchievesUpdatedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(activeLicenseAddedAchieves);
		}
	}

	// Token: 0x0600710D RID: 28941 RVA: 0x00247508 File Offset: 0x00245708
	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		foreach (NetCache.ProfileNotice profileNotice in newNotices)
		{
			if (NetCache.ProfileNotice.NoticeOrigin.ACHIEVEMENT == profileNotice.Origin)
			{
				int achieveID = (int)profileNotice.OriginData;
				global::Achievement achievement = this.GetAchievement(achieveID);
				if (achievement != null)
				{
					achievement.AddRewardNoticeID(profileNotice.NoticeID);
				}
			}
		}
	}

	// Token: 0x0600710E RID: 28942 RVA: 0x00247578 File Offset: 0x00245778
	private bool CanCancelQuestNow()
	{
		if (Vars.Key("Quests.CanCancelManyTimes").GetBool(false))
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

	// Token: 0x0600710F RID: 28943 RVA: 0x002475C4 File Offset: 0x002457C4
	private void FireAchieveCanceledEvent(int achieveID, bool success)
	{
		AchieveManager.AchieveCanceledListener[] array = this.m_achieveCanceledListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(achieveID, success);
		}
	}

	// Token: 0x06007110 RID: 28944 RVA: 0x002475F8 File Offset: 0x002457F8
	private void CheckAllCardGainAchieves()
	{
		this.GetAchieves(delegate(global::Achievement obj)
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
			return achieveTrigger - Assets.Achieve.Trigger.RACE <= 1 && obj.RaceRequirement != null;
		});
		this.GetAchieves((global::Achievement obj) => obj.Enabled && !obj.IsCompleted() && obj.AchieveTrigger == Assets.Achieve.Trigger.CARDSET && obj.CardSetRequirement != null);
		this.ValidateAchievesNow();
	}

	// Token: 0x06007111 RID: 28945 RVA: 0x00247658 File Offset: 0x00245858
	private void CheckTimedEventsAndLicenses(DateTime utcNow)
	{
		if (!this.m_allNetAchievesReceived)
		{
			return;
		}
		DateTime dateTime = utcNow.ToLocalTime();
		if (dateTime.Ticks - this.m_lastEventTimingAndLicenseAchieveCheck < AchieveManager.TIMED_AND_LICENSE_ACHIEVE_CHECK_DELAY_TICKS)
		{
			return;
		}
		this.m_lastEventTimingAndLicenseAchieveCheck = dateTime.Ticks;
		int num = 0;
		foreach (global::Achievement achievement in this.m_achievements.Values)
		{
			if (achievement.Enabled && !achievement.IsCompleted() && achievement.Active && Assets.Achieve.Trigger.EVENT_TIMING_ONLY == achievement.AchieveTrigger && SpecialEventManager.Get().IsEventActive(achievement.EventTrigger, false) && (!this.m_lastEventTimingValidationByAchieve.ContainsKey(achievement.ID) || dateTime.Ticks - this.m_lastEventTimingValidationByAchieve[achievement.ID] >= AchieveManager.TIMED_ACHIEVE_VALIDATION_DELAY_TICKS))
			{
				global::Log.Achievements.Print("AchieveManager.CheckTimedEventsAndLicenses(): checking on timed event achieve {0} time {1}", new object[]
				{
					achievement.ID,
					dateTime
				});
				this.m_lastEventTimingValidationByAchieve[achievement.ID] = dateTime.Ticks;
				this.m_achieveValidationsToRequest.Add(achievement.ID);
				num++;
			}
			if (achievement.IsActiveLicenseAddedAchieve() && (!this.m_lastCheckLicenseAddedByAchieve.ContainsKey(achievement.ID) || utcNow.Ticks - this.m_lastCheckLicenseAddedByAchieve[achievement.ID] >= AchieveManager.CHECK_LICENSE_ADDED_ACHIEVE_DELAY_TICKS))
			{
				global::Log.Achievements.Print("AchieveManager.CheckTimedEventsAndLicenses(): checking on license added achieve {0} time {1}", new object[]
				{
					achievement.ID,
					dateTime
				});
				this.m_lastCheckLicenseAddedByAchieve[achievement.ID] = utcNow.Ticks;
				Network.Get().CheckAccountLicenseAchieve(achievement.ID);
			}
		}
		if (num == 0)
		{
			return;
		}
		this.ValidateAchievesNow();
	}

	// Token: 0x06007112 RID: 28946 RVA: 0x00247860 File Offset: 0x00245A60
	private List<global::Achievement> GetActiveLicenseAddedAchieves()
	{
		return this.m_achievements.Values.ToList<global::Achievement>().FindAll((global::Achievement obj) => obj.IsActiveLicenseAddedAchieve());
	}

	// Token: 0x06007113 RID: 28947 RVA: 0x00247898 File Offset: 0x00245A98
	public List<RewardData> GetRewardsForAdventureAndMode(int adventureId, int modeId, HashSet<Assets.Achieve.RewardTiming> rewardTimings)
	{
		List<RewardData> list = new List<RewardData>();
		foreach (global::Achievement achievement in this.GetAchievesForAdventureAndMode(adventureId, modeId))
		{
			list.AddRange(this.GetRewardsForAchieve(achievement.ID, rewardTimings));
		}
		return list;
	}

	// Token: 0x06007114 RID: 28948 RVA: 0x00247900 File Offset: 0x00245B00
	public List<RewardData> GetRewardsForAdventureWing(int wingID, HashSet<Assets.Achieve.RewardTiming> rewardTimings)
	{
		List<RewardData> list = new List<RewardData>();
		foreach (global::Achievement achievement in this.GetAchievesForAdventureWing(wingID))
		{
			list.AddRange(this.GetRewardsForAchieve(achievement.ID, rewardTimings));
		}
		return list;
	}

	// Token: 0x06007115 RID: 28949 RVA: 0x00247968 File Offset: 0x00245B68
	public List<RewardData> GetRewardsForAdventureScenario(int wingID, int scenarioID, HashSet<Assets.Achieve.RewardTiming> rewardTimings)
	{
		List<RewardData> list = new List<RewardData>();
		foreach (global::Achievement achievement in this.GetAchievesForAdventureWing(wingID))
		{
			if (achievement.Scenarios.Contains(scenarioID))
			{
				list.AddRange(this.GetRewardsForAchieve(achievement.ID, rewardTimings));
			}
		}
		return list;
	}

	// Token: 0x06007116 RID: 28950 RVA: 0x002479E0 File Offset: 0x00245BE0
	public List<RewardData> GetRewardsForAchieve(int achieveID, HashSet<Assets.Achieve.RewardTiming> rewardTimings)
	{
		List<RewardData> list = new List<RewardData>();
		global::Achievement achievement = this.GetAchievement(achieveID);
		List<RewardData> rewards = achievement.Rewards;
		if (rewardTimings.Contains(achievement.RewardTiming))
		{
			foreach (RewardData item in rewards)
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x04005A78 RID: 23160
	private static readonly long TIMED_ACHIEVE_VALIDATION_DELAY_TICKS = 600000000L;

	// Token: 0x04005A79 RID: 23161
	private static readonly long CHECK_LICENSE_ADDED_ACHIEVE_DELAY_TICKS = (long)((ulong)-1294967296);

	// Token: 0x04005A7A RID: 23162
	private static readonly long TIMED_AND_LICENSE_ACHIEVE_CHECK_DELAY_TICKS = Math.Min(AchieveManager.TIMED_ACHIEVE_VALIDATION_DELAY_TICKS, AchieveManager.CHECK_LICENSE_ADDED_ACHIEVE_DELAY_TICKS);

	// Token: 0x04005A7B RID: 23163
	private global::Map<int, global::Achievement> m_achievements = new global::Map<int, global::Achievement>();

	// Token: 0x04005A7C RID: 23164
	private bool m_allNetAchievesReceived;

	// Token: 0x04005A7D RID: 23165
	private int m_numEventResponsesNeeded;

	// Token: 0x04005A7E RID: 23166
	private HashSet<int> m_achieveValidationsToRequest = new HashSet<int>();

	// Token: 0x04005A7F RID: 23167
	private HashSet<int> m_achieveValidationsRequested = new HashSet<int>();

	// Token: 0x04005A80 RID: 23168
	private HashSet<int> m_achievesSeenByPlayerThisSession = new HashSet<int>();

	// Token: 0x04005A81 RID: 23169
	private HashSet<int> m_visualBlocklist = new HashSet<int>();

	// Token: 0x04005A82 RID: 23170
	private bool m_disableCancelButtonUntilServerReturns;

	// Token: 0x04005A83 RID: 23171
	private global::Map<int, long> m_lastEventTimingValidationByAchieve = new global::Map<int, long>();

	// Token: 0x04005A84 RID: 23172
	private global::Map<int, long> m_lastCheckLicenseAddedByAchieve = new global::Map<int, long>();

	// Token: 0x04005A85 RID: 23173
	private long m_lastEventTimingAndLicenseAchieveCheck;

	// Token: 0x04005A86 RID: 23174
	private bool m_queueNotifications;

	// Token: 0x04005A87 RID: 23175
	private List<int> m_achieveNotificationsToQueue = new List<int>();

	// Token: 0x04005A88 RID: 23176
	private List<AchievementNotification> m_blockedAchievementNotifications = new List<AchievementNotification>();

	// Token: 0x04005A89 RID: 23177
	private List<AchieveManager.AchieveCanceledListener> m_achieveCanceledListeners = new List<AchieveManager.AchieveCanceledListener>();

	// Token: 0x04005A8A RID: 23178
	private List<AchieveManager.AchievesUpdatedListener> m_achievesUpdatedListeners = new List<AchieveManager.AchievesUpdatedListener>();

	// Token: 0x04005A8B RID: 23179
	private List<AchieveManager.LicenseAddedAchievesUpdatedListener> m_licenseAddedAchievesUpdatedListeners = new List<AchieveManager.LicenseAddedAchievesUpdatedListener>();

	// Token: 0x02002418 RID: 9240
	// (Invoke) Token: 0x06012E39 RID: 77369
	public delegate void AchieveCanceledCallback(int achieveID, bool success, object userData);

	// Token: 0x02002419 RID: 9241
	private class AchieveCanceledListener : global::EventListener<AchieveManager.AchieveCanceledCallback>
	{
		// Token: 0x06012E3C RID: 77372 RVA: 0x0051F135 File Offset: 0x0051D335
		public void Fire(int achieveID, bool success)
		{
			this.m_callback(achieveID, success, this.m_userData);
		}
	}

	// Token: 0x0200241A RID: 9242
	// (Invoke) Token: 0x06012E3F RID: 77375
	public delegate void AchievesUpdatedCallback(List<global::Achievement> updatedAchieves, List<global::Achievement> completedAchieves, object userData);

	// Token: 0x0200241B RID: 9243
	private class AchievesUpdatedListener : global::EventListener<AchieveManager.AchievesUpdatedCallback>
	{
		// Token: 0x06012E42 RID: 77378 RVA: 0x0051F152 File Offset: 0x0051D352
		public void Fire(List<global::Achievement> updatedAchieves, List<global::Achievement> completedAchieves)
		{
			this.m_callback(updatedAchieves, completedAchieves, this.m_userData);
		}
	}

	// Token: 0x0200241C RID: 9244
	// (Invoke) Token: 0x06012E45 RID: 77381
	public delegate void LicenseAddedAchievesUpdatedCallback(List<global::Achievement> activeLicenseAddedAchieves, object userData);

	// Token: 0x0200241D RID: 9245
	private class LicenseAddedAchievesUpdatedListener : global::EventListener<AchieveManager.LicenseAddedAchievesUpdatedCallback>
	{
		// Token: 0x06012E48 RID: 77384 RVA: 0x0051F16F File Offset: 0x0051D36F
		public void Fire(List<global::Achievement> activeLicenseAddedAchieves)
		{
			this.m_callback(activeLicenseAddedAchieves, this.m_userData);
		}
	}
}
