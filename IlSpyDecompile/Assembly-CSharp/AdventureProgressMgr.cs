using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using PegasusUtil;
using UnityEngine;

public class AdventureProgressMgr : IService
{
	public enum WingProgressMeaning
	{
		NO_PROGRESS,
		AVAILABLE,
		COMPLETED_MISSION_1,
		COMPLETED_MISSION_2,
		COMPLETED_MISSION_3,
		COMPLETED_MISSION_4,
		COMPLETED_MISSION_5
	}

	public delegate void AdventureProgressUpdatedCallback(bool isStartupAction, AdventureMission.WingProgress oldProgress, AdventureMission.WingProgress newProgress, object userData);

	private class AdventureProgressUpdatedListener : EventListener<AdventureProgressUpdatedCallback>
	{
		public void Fire(bool isStartupAction, AdventureMission.WingProgress oldProgress, AdventureMission.WingProgress newProgress)
		{
			m_callback(isStartupAction, oldProgress, newProgress, m_userData);
		}
	}

	private Map<int, AdventureMission.WingProgress> m_wingProgress = new Map<int, AdventureMission.WingProgress>();

	private Map<int, int> m_wingAckState = new Map<int, int>();

	private Map<int, AdventureMission> m_missions = new Map<int, AdventureMission>();

	private List<AdventureProgressUpdatedListener> m_progressUpdatedListeners = new List<AdventureProgressUpdatedListener>();

	public bool IsReady { get; private set; }

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		HearthstoneApplication.Get().WillReset += WillReset;
		LoadAdventureMissionsFromDBF();
		serviceLocator.Get<Network>().RegisterNetHandler(AdventureProgressResponse.PacketID.ID, OnAdventureProgress);
		serviceLocator.Get<NetCache>().RegisterNewNoticesListener(OnNewNotices);
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[3]
		{
			typeof(Network),
			typeof(NetCache),
			typeof(GameDbf)
		};
	}

	public void Shutdown()
	{
	}

	private void WillReset()
	{
		m_wingProgress.Clear();
		m_wingAckState.Clear();
		m_progressUpdatedListeners.Clear();
		IsReady = false;
	}

	public static AdventureProgressMgr Get()
	{
		return HearthstoneServices.Get<AdventureProgressMgr>();
	}

	public static void InitRequests()
	{
		Network.Get().RequestAdventureProgress();
	}

	public bool RegisterProgressUpdatedListener(AdventureProgressUpdatedCallback callback)
	{
		return RegisterProgressUpdatedListener(callback, null);
	}

	public bool RegisterProgressUpdatedListener(AdventureProgressUpdatedCallback callback, object userData)
	{
		if (callback == null)
		{
			return false;
		}
		AdventureProgressUpdatedListener adventureProgressUpdatedListener = new AdventureProgressUpdatedListener();
		adventureProgressUpdatedListener.SetCallback(callback);
		adventureProgressUpdatedListener.SetUserData(userData);
		if (m_progressUpdatedListeners.Contains(adventureProgressUpdatedListener))
		{
			return false;
		}
		m_progressUpdatedListeners.Add(adventureProgressUpdatedListener);
		return true;
	}

	public bool RemoveProgressUpdatedListener(AdventureProgressUpdatedCallback callback)
	{
		return RemoveProgressUpdatedListener(callback, null);
	}

	public bool RemoveProgressUpdatedListener(AdventureProgressUpdatedCallback callback, object userData)
	{
		if (callback == null)
		{
			return false;
		}
		AdventureProgressUpdatedListener adventureProgressUpdatedListener = new AdventureProgressUpdatedListener();
		adventureProgressUpdatedListener.SetCallback(callback);
		adventureProgressUpdatedListener.SetUserData(userData);
		if (!m_progressUpdatedListeners.Contains(adventureProgressUpdatedListener))
		{
			return false;
		}
		m_progressUpdatedListeners.Remove(adventureProgressUpdatedListener);
		return true;
	}

	public List<AdventureMission.WingProgress> GetAllProgress()
	{
		return new List<AdventureMission.WingProgress>(m_wingProgress.Values);
	}

	public AdventureMission.WingProgress GetProgress(int wing)
	{
		if (!m_wingProgress.ContainsKey(wing))
		{
			return null;
		}
		return m_wingProgress[wing];
	}

	public bool OwnsOneOrMoreAdventureWings(AdventureDbId adventureID)
	{
		foreach (WingDbfRecord record in GameDbf.Wing.GetRecords())
		{
			if (record.AdventureId == (int)adventureID && OwnsWing(record.ID))
			{
				return true;
			}
		}
		return false;
	}

	public bool OwnsAllAdventureWings(AdventureDbId adventureID)
	{
		foreach (WingDbfRecord record in GameDbf.Wing.GetRecords())
		{
			if (record.AdventureId == (int)adventureID && !OwnsWing(record.ID))
			{
				return false;
			}
		}
		return true;
	}

	public bool OwnsWing(int wing)
	{
		if (!m_wingProgress.ContainsKey(wing))
		{
			return false;
		}
		return m_wingProgress[wing].IsOwned();
	}

	public WingDbfRecord GetFirstUnownedAdventureWing(AdventureDbId adventureID)
	{
		WingDbfRecord wingDbfRecord = null;
		foreach (WingDbfRecord record in GameDbf.Wing.GetRecords((WingDbfRecord r) => r.AdventureId == (int)adventureID))
		{
			if (!OwnsWing(record.ID) && (wingDbfRecord == null || record.UnlockOrder < wingDbfRecord.UnlockOrder))
			{
				wingDbfRecord = record;
			}
		}
		return wingDbfRecord;
	}

	public static int GetTotalNumberOfWings(int adventureId)
	{
		return GameDbf.Wing.GetRecords().FindAll((WingDbfRecord wing) => wing.AdventureId == adventureId).Count;
	}

	public bool IsWingComplete(AdventureDbId adventureID, AdventureModeDbId modeID, WingDbId wingId)
	{
		bool wingHasUnackedProgress;
		return IsWingComplete(adventureID, modeID, wingId, out wingHasUnackedProgress);
	}

	public bool IsWingComplete(AdventureDbId adventureID, AdventureModeDbId modeID, WingDbId wingId, out bool wingHasUnackedProgress)
	{
		List<ScenarioDbfRecord> records = GameDbf.Scenario.GetRecords();
		wingHasUnackedProgress = false;
		foreach (ScenarioDbfRecord item in records)
		{
			if (item.AdventureId == (int)adventureID && item.ModeId == (int)modeID && item.WingId == (int)wingId)
			{
				bool hasUnackedProgress = false;
				if (!HasDefeatedScenario(item.ID, out hasUnackedProgress))
				{
					return false;
				}
				if (hasUnackedProgress)
				{
					wingHasUnackedProgress = true;
				}
			}
		}
		return true;
	}

	public bool IsAdventureModeAndSectionComplete(AdventureDbId adventureID, AdventureModeDbId modeID, int bookSection = 0)
	{
		foreach (ScenarioDbfRecord record2 in GameDbf.Scenario.GetRecords((ScenarioDbfRecord r) => r.AdventureId == (int)adventureID && r.ModeId == (int)modeID))
		{
			int wingId = record2.WingId;
			if (wingId > 0)
			{
				WingDbfRecord record = GameDbf.Wing.GetRecord(wingId);
				if (record != null && bookSection == record.BookSection && !HasDefeatedScenario(record2.ID))
				{
					return false;
				}
			}
		}
		return true;
	}

	public bool IsAdventureComplete(AdventureDbId adventureID)
	{
		List<AdventureDataDbfRecord> records = GameDbf.AdventureData.GetRecords((AdventureDataDbfRecord r) => r.AdventureId == (int)adventureID);
		if (records.Count == 0)
		{
			Debug.LogWarningFormat("No Adventure mode records found for AdventureDbId {0}! Returning True for IsAdventureComplete()", adventureID);
			return true;
		}
		foreach (AdventureDataDbfRecord item in records)
		{
			if (!IsAdventureModeAndSectionComplete(adventureID, (AdventureModeDbId)item.ModeId))
			{
				return false;
			}
		}
		return true;
	}

	public bool IsWingLocked(AdventureWingDef wingDef)
	{
		if (wingDef.GetWingId() == WingDbId.LOE_HALL_OF_EXPLORERS)
		{
			bool num = IsWingComplete(AdventureDbId.LOE, AdventureModeDbId.LINEAR, WingDbId.LOE_TEMPLE_OF_ORSIS);
			bool flag = IsWingComplete(AdventureDbId.LOE, AdventureModeDbId.LINEAR, WingDbId.LOE_ULDAMAN);
			bool flag2 = IsWingComplete(AdventureDbId.LOE, AdventureModeDbId.LINEAR, WingDbId.LOE_RUINED_CITY);
			return !(num && flag && flag2);
		}
		if (wingDef.GetOpenPrereqId() != 0)
		{
			GetWingAck((int)wingDef.GetOpenPrereqId(), out var ack);
			if (ack < 1)
			{
				return true;
			}
			if (wingDef.GetMustCompleteOpenPrereq() && !IsWingComplete(wingDef.GetAdventureId(), AdventureConfig.Get().GetSelectedMode(), wingDef.GetOpenPrereqId()))
			{
				return true;
			}
		}
		return false;
	}

	public int GetNumPlayableAdventureScenarios(AdventureDbId adventureID, AdventureModeDbId modeID)
	{
		List<WingDbfRecord> records = GameDbf.Wing.GetRecords((WingDbfRecord r) => r.AdventureId == (int)adventureID);
		int num = 0;
		foreach (WingDbfRecord item in records)
		{
			num += GetNumPlayableScenariosForWing(item, modeID);
		}
		return num;
	}

	private int GetNumPlayableScenariosForWing(WingDbfRecord wing, AdventureModeDbId modeID)
	{
		int num = 0;
		if (!OwnsWing(wing.ID) || !IsWingEventActive(wing.ID))
		{
			return 0;
		}
		foreach (ScenarioDbfRecord record in GameDbf.Scenario.GetRecords((ScenarioDbfRecord r) => r.WingId == wing.ID && r.ModeId == (int)modeID))
		{
			if (!HasDefeatedScenario(record.ID) && CanPlayScenario(record.ID))
			{
				num++;
			}
		}
		return num;
	}

	public int GetPlayableClassChallenges(AdventureDbId adventureID, AdventureModeDbId modeID)
	{
		int num = 0;
		foreach (ScenarioDbfRecord record in GameDbf.Scenario.GetRecords())
		{
			if (record.AdventureId == (int)adventureID && record.ModeId == (int)modeID && CanPlayScenario(record.ID) && !HasDefeatedScenario(record.ID))
			{
				num++;
			}
		}
		return num;
	}

	public static List<RewardData> GetRewardsForWing(int wing, HashSet<Assets.Achieve.RewardTiming> rewardTimings)
	{
		List<RewardData> rewardsForAdventureWing = AchieveManager.Get().GetRewardsForAdventureWing(wing, rewardTimings);
		List<RewardData> list = new List<RewardData>();
		foreach (RewardData item in rewardsForAdventureWing)
		{
			if (Reward.Type.CARD == item.RewardType)
			{
				list.Add(item as CardRewardData);
			}
			if (Reward.Type.CARD_BACK == item.RewardType)
			{
				list.Add(item as CardBackRewardData);
			}
			if (Reward.Type.BOOSTER_PACK == item.RewardType)
			{
				list.Add(item as BoosterPackRewardData);
			}
			if (Reward.Type.RANDOM_CARD == item.RewardType)
			{
				list.Add(item as RandomCardRewardData);
			}
		}
		return list;
	}

	public static List<RewardData> GetRewardsForAdventureByMode(int adventureId, int adventureModeId, HashSet<Assets.Achieve.RewardTiming> rewardTimings)
	{
		List<RewardData> rewardsForAdventureAndMode = AchieveManager.Get().GetRewardsForAdventureAndMode(adventureId, adventureModeId, rewardTimings);
		List<RewardData> list = new List<RewardData>();
		foreach (RewardData item in rewardsForAdventureAndMode)
		{
			if (Reward.Type.CARD == item.RewardType)
			{
				list.Add(item as CardRewardData);
			}
			if (Reward.Type.CARD_BACK == item.RewardType)
			{
				list.Add(item as CardBackRewardData);
			}
			if (Reward.Type.BOOSTER_PACK == item.RewardType)
			{
				list.Add(item as BoosterPackRewardData);
			}
			if (Reward.Type.RANDOM_CARD == item.RewardType)
			{
				list.Add(item as RandomCardRewardData);
			}
		}
		return list;
	}

	public static SpecialEventType GetWingEventTiming(int wing)
	{
		WingDbfRecord record = GameDbf.Wing.GetRecord(wing);
		if (record == null)
		{
			Debug.LogWarning($"AdventureProgressMgr.GetWingEventTiming could not find DBF record for wing {wing}, assuming it is has no open event");
			return SpecialEventType.IGNORE;
		}
		string requiredEvent = record.RequiredEvent;
		SpecialEventType eventType = SpecialEventManager.GetEventType(requiredEvent);
		if (eventType == SpecialEventType.UNKNOWN)
		{
			Debug.LogWarning($"AdventureProgressMgr.GetWing wing={wing} could not find SpecialEventType record for event '{requiredEvent}'");
			return SpecialEventType.IGNORE;
		}
		return eventType;
	}

	public static string GetWingName(int wing)
	{
		WingDbfRecord record = GameDbf.Wing.GetRecord(wing);
		if (record == null)
		{
			Debug.LogWarning($"AdventureProgressMgr.GetWingName could not find DBF record for wing {wing}");
			return string.Empty;
		}
		return record.Name;
	}

	public static bool IsWingEventActive(int wing)
	{
		SpecialEventType wingEventTiming = GetWingEventTiming(wing);
		return SpecialEventManager.Get().IsEventActive(wingEventTiming, activeIfDoesNotExist: false);
	}

	public bool CanPlayScenario(int scenarioID, bool checkEventTiming = true)
	{
		if (DemoMgr.Get().GetMode() == DemoMode.BLIZZCON_2015 && 1061 != scenarioID)
		{
			return false;
		}
		if (!m_missions.ContainsKey(scenarioID))
		{
			return true;
		}
		AdventureMission adventureMission = m_missions[scenarioID];
		if (!adventureMission.HasRequiredProgress())
		{
			return true;
		}
		AdventureMission.WingProgress progress = GetProgress(adventureMission.RequiredProgress.Wing);
		if (progress == null)
		{
			return false;
		}
		if (!progress.MeetsProgressAndFlagsRequirements(adventureMission.RequiredProgress))
		{
			return false;
		}
		if (checkEventTiming && !IsWingEventActive(adventureMission.RequiredProgress.Wing))
		{
			return false;
		}
		return true;
	}

	public bool HasDefeatedScenario(int scenarioID)
	{
		bool hasUnackedProgress;
		return HasDefeatedScenario(scenarioID, out hasUnackedProgress);
	}

	public bool HasDefeatedScenario(int scenarioID, out bool hasUnackedProgress)
	{
		hasUnackedProgress = false;
		if (!m_missions.TryGetValue(scenarioID, out var value))
		{
			return false;
		}
		if (value.RequiredProgress == null)
		{
			return false;
		}
		if (value.GrantedProgress == null)
		{
			return false;
		}
		AdventureMission.WingProgress progress = GetProgress(value.GrantedProgress.Wing);
		if (progress == null)
		{
			return false;
		}
		GetWingAck(value.GrantedProgress.Wing, out var ack);
		hasUnackedProgress = ack < value.GrantedProgress.Progress;
		return progress.MeetsProgressAndFlagsRequirements(value.GrantedProgress);
	}

	public static bool GetGameSaveDataProgressForScenario(int scenarioId, out int progress, out int maxProgress)
	{
		if (!ScenarioUsesGameSaveDataProgress(scenarioId))
		{
			progress = 0;
			maxProgress = 0;
			Debug.LogError($"Attempting to get Game Save Data progress for Scenario={scenarioId}, which does not have any Game Save Data. Add a GSD Subkey to that scenario's dbi.");
			return false;
		}
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(scenarioId);
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)AdventureConfig.Get().GetSelectedAdventureDataRecord().GameSaveDataServerKey;
		GameSaveKeySubkeyId gameSaveDataProgressSubkey = (GameSaveKeySubkeyId)record.GameSaveDataProgressSubkey;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, gameSaveDataProgressSubkey, out long value);
		progress = (int)value;
		maxProgress = record.GameSaveDataProgressMax;
		return true;
	}

	public static bool ScenarioUsesGameSaveDataProgress(int scenarioId)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(scenarioId);
		if (record.GameSaveDataProgressSubkey != 0)
		{
			return Enum.IsDefined(typeof(GameSaveKeySubkeyId), record.GameSaveDataProgressSubkey);
		}
		return false;
	}

	public bool ScenarioHasRewardData(int scenarioId)
	{
		List<RewardData> immediateRewardsForDefeatingScenario = GetImmediateRewardsForDefeatingScenario(scenarioId);
		if (immediateRewardsForDefeatingScenario != null)
		{
			return immediateRewardsForDefeatingScenario.Count > 0;
		}
		return false;
	}

	public List<RewardData> GetImmediateRewardsForDefeatingScenario(int scenarioID)
	{
		HashSet<Assets.Achieve.RewardTiming> rewardTimings = new HashSet<Assets.Achieve.RewardTiming> { Assets.Achieve.RewardTiming.IMMEDIATE };
		return GetRewardsForDefeatingScenario(scenarioID, rewardTimings);
	}

	public List<RewardData> GetRewardsForDefeatingScenario(int scenarioID, HashSet<Assets.Achieve.RewardTiming> rewardTimings)
	{
		if (!m_missions.TryGetValue(scenarioID, out var value))
		{
			return new List<RewardData>();
		}
		List<RewardData> result = null;
		if (GameUtils.IsHeroicAdventureMission(scenarioID) || GameUtils.IsClassChallengeMission(scenarioID) || value.GrantedProgress != null)
		{
			result = AchieveManager.Get().GetRewardsForAdventureScenario(value.GrantedProgress.Wing, scenarioID, rewardTimings);
		}
		return result;
	}

	public bool SetWingAck(int wing, int ackId)
	{
		Log.Adventures.Print("SetWingAck for wing {0}", wing);
		if (m_wingAckState.TryGetValue(wing, out var value))
		{
			if (ackId < value)
			{
				return false;
			}
			if (ackId == value)
			{
				return true;
			}
		}
		m_wingAckState[wing] = ackId;
		Network.Get().AckWingProgress(wing, ackId);
		return true;
	}

	public bool GetWingAck(int wing, out int ack)
	{
		return m_wingAckState.TryGetValue(wing, out ack);
	}

	public AdventureMissionState AdventureMissionStateForScenario(int scenarioID)
	{
		if (HasDefeatedScenario(scenarioID))
		{
			return AdventureMissionState.COMPLETED;
		}
		if (CanPlayScenario(scenarioID))
		{
			return AdventureMissionState.UNLOCKED;
		}
		return AdventureMissionState.LOCKED;
	}

	public AdventureChapterState AdventureBookChapterStateForWing(WingDbfRecord wingRecord, AdventureModeDbId adventureMode)
	{
		if (IsWingComplete((AdventureDbId)wingRecord.AdventureId, adventureMode, (WingDbId)wingRecord.ID))
		{
			return AdventureChapterState.COMPLETED;
		}
		if (GetNumPlayableScenariosForWing(wingRecord, adventureMode) > 0)
		{
			return AdventureChapterState.UNLOCKED;
		}
		return AdventureChapterState.LOCKED;
	}

	public bool OwnershipPrereqWingIsOwned(AdventureWingDef wingDef)
	{
		if (wingDef.GetOwnershipPrereqId() != 0)
		{
			return OwnsWing((int)wingDef.GetOwnershipPrereqId());
		}
		return true;
	}

	public bool OwnershipPrereqWingIsOwned(WingDbfRecord wingRecord)
	{
		if (wingRecord.OwnershipPrereqWingId != 0)
		{
			return OwnsWing(wingRecord.OwnershipPrereqWingId);
		}
		return true;
	}

	private void LoadAdventureMissionsFromDBF()
	{
		foreach (AdventureMissionDbfRecord record in GameDbf.AdventureMission.GetRecords())
		{
			int scenarioId = record.ScenarioId;
			if (m_missions.ContainsKey(scenarioId))
			{
				Debug.LogWarning($"AdventureProgressMgr.LoadAdventureMissionsFromDBF(): duplicate entry found for scenario ID {scenarioId}");
				continue;
			}
			string noteDesc = record.NoteDesc;
			AdventureMission.WingProgress requiredProgress = new AdventureMission.WingProgress(record.ReqWingId, record.ReqProgress, record.ReqFlags);
			AdventureMission.WingProgress grantedProgress = new AdventureMission.WingProgress(record.GrantsWingId, record.GrantsProgress, record.GrantsFlags);
			m_missions[scenarioId] = new AdventureMission(scenarioId, noteDesc, requiredProgress, grantedProgress);
		}
	}

	private void OnAdventureProgress()
	{
		foreach (Network.AdventureProgress item in Network.Get().GetAdventureProgressResponse())
		{
			CreateOrUpdateProgress(isStartupAction: true, item.Wing, item.Progress);
			CreateOrUpdateWingFlags(isStartupAction: true, item.Wing, item.Flags);
			CreateOrUpdateWingAck(item.Wing, item.Ack);
		}
		IsReady = true;
	}

	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		List<long> list = new List<long>();
		foreach (NetCache.ProfileNotice newNotice in newNotices)
		{
			if (newNotice.Type == NetCache.ProfileNotice.NoticeType.ADVENTURE_PROGRESS)
			{
				NetCache.ProfileNoticeAdventureProgress profileNoticeAdventureProgress = newNotice as NetCache.ProfileNoticeAdventureProgress;
				if (profileNoticeAdventureProgress.Progress.HasValue)
				{
					CreateOrUpdateProgress(isStartupAction: false, profileNoticeAdventureProgress.Wing, profileNoticeAdventureProgress.Progress.Value);
				}
				if (profileNoticeAdventureProgress.Flags.HasValue)
				{
					CreateOrUpdateWingFlags(isStartupAction: false, profileNoticeAdventureProgress.Wing, profileNoticeAdventureProgress.Flags.Value);
				}
				list.Add(newNotice.NoticeID);
			}
		}
		foreach (long item in list)
		{
			Network.Get().AckNotice(item);
		}
	}

	private void FireProgressUpdate(bool isStartupAction, AdventureMission.WingProgress oldProgress, AdventureMission.WingProgress newProgress)
	{
		AdventureProgressUpdatedListener[] array = m_progressUpdatedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(isStartupAction, oldProgress, newProgress);
		}
	}

	private void CreateOrUpdateProgress(bool isStartupAction, int wing, int progress)
	{
		if (!m_wingProgress.ContainsKey(wing))
		{
			m_wingProgress[wing] = new AdventureMission.WingProgress(wing, progress, 0uL);
			FireProgressUpdate(isStartupAction, null, m_wingProgress[wing]);
			return;
		}
		AdventureMission.WingProgress wingProgress = m_wingProgress[wing].Clone();
		m_wingProgress[wing].SetProgress(progress);
		Log.Adventures.Print("AdventureProgressMgr.CreateOrUpdateProgress: updating wing {0} : PROGRESS {1} (former progress {2})", wing, m_wingProgress[wing], wingProgress);
		FireProgressUpdate(isStartupAction, wingProgress, m_wingProgress[wing]);
	}

	private void CreateOrUpdateWingFlags(bool isStartupAction, int wing, ulong flags)
	{
		if (!m_wingProgress.ContainsKey(wing))
		{
			m_wingProgress[wing] = new AdventureMission.WingProgress(wing, 0, flags);
			Log.Adventures.Print("AdventureProgressMgr.CreateOrUpdateWingFlags: creating wing {0} : PROGRESS {1}", wing, m_wingProgress[wing]);
			FireProgressUpdate(isStartupAction, null, m_wingProgress[wing]);
		}
		else
		{
			AdventureMission.WingProgress oldProgress = m_wingProgress[wing].Clone();
			m_wingProgress[wing].SetFlags(flags);
			FireProgressUpdate(isStartupAction, oldProgress, m_wingProgress[wing]);
		}
	}

	private void CreateOrUpdateWingAck(int wing, int ack)
	{
		m_wingAckState[wing] = ack;
	}
}
