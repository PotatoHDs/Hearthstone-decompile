using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000019 RID: 25
public class AdventureProgressMgr : IService
{
	// Token: 0x0600009C RID: 156 RVA: 0x00003BA3 File Offset: 0x00001DA3
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		HearthstoneApplication.Get().WillReset += this.WillReset;
		this.LoadAdventureMissionsFromDBF();
		serviceLocator.Get<Network>().RegisterNetHandler(AdventureProgressResponse.PacketID.ID, new Network.NetHandler(this.OnAdventureProgress), null);
		serviceLocator.Get<NetCache>().RegisterNewNoticesListener(new NetCache.DelNewNoticesListener(this.OnNewNotices));
		yield break;
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00003BB9 File Offset: 0x00001DB9
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(Network),
			typeof(NetCache),
			typeof(GameDbf)
		};
	}

	// Token: 0x0600009E RID: 158 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00003BEA File Offset: 0x00001DEA
	private void WillReset()
	{
		this.m_wingProgress.Clear();
		this.m_wingAckState.Clear();
		this.m_progressUpdatedListeners.Clear();
		this.IsReady = false;
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00003C14 File Offset: 0x00001E14
	public static AdventureProgressMgr Get()
	{
		return HearthstoneServices.Get<AdventureProgressMgr>();
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00003C1B File Offset: 0x00001E1B
	public static void InitRequests()
	{
		Network.Get().RequestAdventureProgress();
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003C27 File Offset: 0x00001E27
	// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003C2F File Offset: 0x00001E2F
	public bool IsReady { get; private set; }

	// Token: 0x060000A4 RID: 164 RVA: 0x00003C38 File Offset: 0x00001E38
	public bool RegisterProgressUpdatedListener(AdventureProgressMgr.AdventureProgressUpdatedCallback callback)
	{
		return this.RegisterProgressUpdatedListener(callback, null);
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00003C44 File Offset: 0x00001E44
	public bool RegisterProgressUpdatedListener(AdventureProgressMgr.AdventureProgressUpdatedCallback callback, object userData)
	{
		if (callback == null)
		{
			return false;
		}
		AdventureProgressMgr.AdventureProgressUpdatedListener adventureProgressUpdatedListener = new AdventureProgressMgr.AdventureProgressUpdatedListener();
		adventureProgressUpdatedListener.SetCallback(callback);
		adventureProgressUpdatedListener.SetUserData(userData);
		if (this.m_progressUpdatedListeners.Contains(adventureProgressUpdatedListener))
		{
			return false;
		}
		this.m_progressUpdatedListeners.Add(adventureProgressUpdatedListener);
		return true;
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00003C87 File Offset: 0x00001E87
	public bool RemoveProgressUpdatedListener(AdventureProgressMgr.AdventureProgressUpdatedCallback callback)
	{
		return this.RemoveProgressUpdatedListener(callback, null);
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00003C94 File Offset: 0x00001E94
	public bool RemoveProgressUpdatedListener(AdventureProgressMgr.AdventureProgressUpdatedCallback callback, object userData)
	{
		if (callback == null)
		{
			return false;
		}
		AdventureProgressMgr.AdventureProgressUpdatedListener adventureProgressUpdatedListener = new AdventureProgressMgr.AdventureProgressUpdatedListener();
		adventureProgressUpdatedListener.SetCallback(callback);
		adventureProgressUpdatedListener.SetUserData(userData);
		if (!this.m_progressUpdatedListeners.Contains(adventureProgressUpdatedListener))
		{
			return false;
		}
		this.m_progressUpdatedListeners.Remove(adventureProgressUpdatedListener);
		return true;
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x00003CD8 File Offset: 0x00001ED8
	public List<AdventureMission.WingProgress> GetAllProgress()
	{
		return new List<AdventureMission.WingProgress>(this.m_wingProgress.Values);
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x00003CEA File Offset: 0x00001EEA
	public AdventureMission.WingProgress GetProgress(int wing)
	{
		if (!this.m_wingProgress.ContainsKey(wing))
		{
			return null;
		}
		return this.m_wingProgress[wing];
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00003D08 File Offset: 0x00001F08
	public bool OwnsOneOrMoreAdventureWings(AdventureDbId adventureID)
	{
		foreach (WingDbfRecord wingDbfRecord in GameDbf.Wing.GetRecords())
		{
			if (wingDbfRecord.AdventureId == (int)adventureID && this.OwnsWing(wingDbfRecord.ID))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00003D78 File Offset: 0x00001F78
	public bool OwnsAllAdventureWings(AdventureDbId adventureID)
	{
		foreach (WingDbfRecord wingDbfRecord in GameDbf.Wing.GetRecords())
		{
			if (wingDbfRecord.AdventureId == (int)adventureID && !this.OwnsWing(wingDbfRecord.ID))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060000AC RID: 172 RVA: 0x00003DE8 File Offset: 0x00001FE8
	public bool OwnsWing(int wing)
	{
		return this.m_wingProgress.ContainsKey(wing) && this.m_wingProgress[wing].IsOwned();
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00003E0C File Offset: 0x0000200C
	public WingDbfRecord GetFirstUnownedAdventureWing(AdventureDbId adventureID)
	{
		WingDbfRecord wingDbfRecord = null;
		foreach (WingDbfRecord wingDbfRecord2 in GameDbf.Wing.GetRecords((WingDbfRecord r) => r.AdventureId == (int)adventureID, -1))
		{
			if (!this.OwnsWing(wingDbfRecord2.ID) && (wingDbfRecord == null || wingDbfRecord2.UnlockOrder < wingDbfRecord.UnlockOrder))
			{
				wingDbfRecord = wingDbfRecord2;
			}
		}
		return wingDbfRecord;
	}

	// Token: 0x060000AE RID: 174 RVA: 0x00003E9C File Offset: 0x0000209C
	public static int GetTotalNumberOfWings(int adventureId)
	{
		return GameDbf.Wing.GetRecords().FindAll((WingDbfRecord wing) => wing.AdventureId == adventureId).Count;
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00003ED8 File Offset: 0x000020D8
	public bool IsWingComplete(AdventureDbId adventureID, AdventureModeDbId modeID, WingDbId wingId)
	{
		bool flag;
		return this.IsWingComplete(adventureID, modeID, wingId, out flag);
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x00003EF0 File Offset: 0x000020F0
	public bool IsWingComplete(AdventureDbId adventureID, AdventureModeDbId modeID, WingDbId wingId, out bool wingHasUnackedProgress)
	{
		List<ScenarioDbfRecord> records = GameDbf.Scenario.GetRecords();
		wingHasUnackedProgress = false;
		foreach (ScenarioDbfRecord scenarioDbfRecord in records)
		{
			if (scenarioDbfRecord.AdventureId == (int)adventureID && scenarioDbfRecord.ModeId == (int)modeID && scenarioDbfRecord.WingId == (int)wingId)
			{
				bool flag = false;
				if (!this.HasDefeatedScenario(scenarioDbfRecord.ID, out flag))
				{
					return false;
				}
				if (flag)
				{
					wingHasUnackedProgress = true;
				}
			}
		}
		return true;
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x00003F80 File Offset: 0x00002180
	public bool IsAdventureModeAndSectionComplete(AdventureDbId adventureID, AdventureModeDbId modeID, int bookSection = 0)
	{
		foreach (ScenarioDbfRecord scenarioDbfRecord in GameDbf.Scenario.GetRecords((ScenarioDbfRecord r) => r.AdventureId == (int)adventureID && r.ModeId == (int)modeID, -1))
		{
			int wingId = scenarioDbfRecord.WingId;
			if (wingId > 0)
			{
				WingDbfRecord record = GameDbf.Wing.GetRecord(wingId);
				if (record != null && bookSection == record.BookSection && !this.HasDefeatedScenario(scenarioDbfRecord.ID))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00004030 File Offset: 0x00002230
	public bool IsAdventureComplete(AdventureDbId adventureID)
	{
		List<AdventureDataDbfRecord> records = GameDbf.AdventureData.GetRecords((AdventureDataDbfRecord r) => r.AdventureId == (int)adventureID, -1);
		if (records.Count == 0)
		{
			Debug.LogWarningFormat("No Adventure mode records found for AdventureDbId {0}! Returning True for IsAdventureComplete()", new object[]
			{
				adventureID
			});
			return true;
		}
		foreach (AdventureDataDbfRecord adventureDataDbfRecord in records)
		{
			if (!this.IsAdventureModeAndSectionComplete(adventureID, (AdventureModeDbId)adventureDataDbfRecord.ModeId, 0))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x000040E4 File Offset: 0x000022E4
	public bool IsWingLocked(AdventureWingDef wingDef)
	{
		if (wingDef.GetWingId() == WingDbId.LOE_HALL_OF_EXPLORERS)
		{
			bool flag = this.IsWingComplete(AdventureDbId.LOE, AdventureModeDbId.LINEAR, WingDbId.LOE_TEMPLE_OF_ORSIS);
			bool flag2 = this.IsWingComplete(AdventureDbId.LOE, AdventureModeDbId.LINEAR, WingDbId.LOE_ULDAMAN);
			bool flag3 = this.IsWingComplete(AdventureDbId.LOE, AdventureModeDbId.LINEAR, WingDbId.LOE_RUINED_CITY);
			return !flag || !flag2 || !flag3;
		}
		if (wingDef.GetOpenPrereqId() != WingDbId.INVALID)
		{
			int num;
			this.GetWingAck((int)wingDef.GetOpenPrereqId(), out num);
			if (num < 1)
			{
				return true;
			}
			if (wingDef.GetMustCompleteOpenPrereq() && !this.IsWingComplete(wingDef.GetAdventureId(), AdventureConfig.Get().GetSelectedMode(), wingDef.GetOpenPrereqId()))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00004170 File Offset: 0x00002370
	public int GetNumPlayableAdventureScenarios(AdventureDbId adventureID, AdventureModeDbId modeID)
	{
		List<WingDbfRecord> records = GameDbf.Wing.GetRecords((WingDbfRecord r) => r.AdventureId == (int)adventureID, -1);
		int num = 0;
		foreach (WingDbfRecord wing in records)
		{
			num += this.GetNumPlayableScenariosForWing(wing, modeID);
		}
		return num;
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x000041E8 File Offset: 0x000023E8
	private int GetNumPlayableScenariosForWing(WingDbfRecord wing, AdventureModeDbId modeID)
	{
		int num = 0;
		if (!this.OwnsWing(wing.ID) || !AdventureProgressMgr.IsWingEventActive(wing.ID))
		{
			return 0;
		}
		foreach (ScenarioDbfRecord scenarioDbfRecord in GameDbf.Scenario.GetRecords((ScenarioDbfRecord r) => r.WingId == wing.ID && r.ModeId == (int)modeID, -1))
		{
			if (!this.HasDefeatedScenario(scenarioDbfRecord.ID) && this.CanPlayScenario(scenarioDbfRecord.ID, true))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x000042A4 File Offset: 0x000024A4
	public int GetPlayableClassChallenges(AdventureDbId adventureID, AdventureModeDbId modeID)
	{
		int num = 0;
		foreach (ScenarioDbfRecord scenarioDbfRecord in GameDbf.Scenario.GetRecords())
		{
			if (scenarioDbfRecord.AdventureId == (int)adventureID && scenarioDbfRecord.ModeId == (int)modeID && this.CanPlayScenario(scenarioDbfRecord.ID, true) && !this.HasDefeatedScenario(scenarioDbfRecord.ID))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x0000432C File Offset: 0x0000252C
	public static List<RewardData> GetRewardsForWing(int wing, HashSet<Assets.Achieve.RewardTiming> rewardTimings)
	{
		List<RewardData> rewardsForAdventureWing = AchieveManager.Get().GetRewardsForAdventureWing(wing, rewardTimings);
		List<RewardData> list = new List<RewardData>();
		foreach (RewardData rewardData in rewardsForAdventureWing)
		{
			if (Reward.Type.CARD == rewardData.RewardType)
			{
				list.Add(rewardData as CardRewardData);
			}
			if (Reward.Type.CARD_BACK == rewardData.RewardType)
			{
				list.Add(rewardData as CardBackRewardData);
			}
			if (Reward.Type.BOOSTER_PACK == rewardData.RewardType)
			{
				list.Add(rewardData as BoosterPackRewardData);
			}
			if (Reward.Type.RANDOM_CARD == rewardData.RewardType)
			{
				list.Add(rewardData as RandomCardRewardData);
			}
		}
		return list;
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x000043DC File Offset: 0x000025DC
	public static List<RewardData> GetRewardsForAdventureByMode(int adventureId, int adventureModeId, HashSet<Assets.Achieve.RewardTiming> rewardTimings)
	{
		List<RewardData> rewardsForAdventureAndMode = AchieveManager.Get().GetRewardsForAdventureAndMode(adventureId, adventureModeId, rewardTimings);
		List<RewardData> list = new List<RewardData>();
		foreach (RewardData rewardData in rewardsForAdventureAndMode)
		{
			if (Reward.Type.CARD == rewardData.RewardType)
			{
				list.Add(rewardData as CardRewardData);
			}
			if (Reward.Type.CARD_BACK == rewardData.RewardType)
			{
				list.Add(rewardData as CardBackRewardData);
			}
			if (Reward.Type.BOOSTER_PACK == rewardData.RewardType)
			{
				list.Add(rewardData as BoosterPackRewardData);
			}
			if (Reward.Type.RANDOM_CARD == rewardData.RewardType)
			{
				list.Add(rewardData as RandomCardRewardData);
			}
		}
		return list;
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x0000448C File Offset: 0x0000268C
	public static SpecialEventType GetWingEventTiming(int wing)
	{
		WingDbfRecord record = GameDbf.Wing.GetRecord(wing);
		if (record == null)
		{
			Debug.LogWarning(string.Format("AdventureProgressMgr.GetWingEventTiming could not find DBF record for wing {0}, assuming it is has no open event", wing));
			return SpecialEventType.IGNORE;
		}
		string requiredEvent = record.RequiredEvent;
		SpecialEventType eventType = SpecialEventManager.GetEventType(requiredEvent);
		if (eventType == SpecialEventType.UNKNOWN)
		{
			Debug.LogWarning(string.Format("AdventureProgressMgr.GetWing wing={0} could not find SpecialEventType record for event '{1}'", wing, requiredEvent));
			return SpecialEventType.IGNORE;
		}
		return eventType;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x000044EC File Offset: 0x000026EC
	public static string GetWingName(int wing)
	{
		WingDbfRecord record = GameDbf.Wing.GetRecord(wing);
		if (record == null)
		{
			Debug.LogWarning(string.Format("AdventureProgressMgr.GetWingName could not find DBF record for wing {0}", wing));
			return string.Empty;
		}
		return record.Name;
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00004530 File Offset: 0x00002730
	public static bool IsWingEventActive(int wing)
	{
		SpecialEventType wingEventTiming = AdventureProgressMgr.GetWingEventTiming(wing);
		return SpecialEventManager.Get().IsEventActive(wingEventTiming, false);
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00004550 File Offset: 0x00002750
	public bool CanPlayScenario(int scenarioID, bool checkEventTiming = true)
	{
		if (DemoMgr.Get().GetMode() == DemoMode.BLIZZCON_2015 && 1061 != scenarioID)
		{
			return false;
		}
		if (!this.m_missions.ContainsKey(scenarioID))
		{
			return true;
		}
		AdventureMission adventureMission = this.m_missions[scenarioID];
		if (!adventureMission.HasRequiredProgress())
		{
			return true;
		}
		AdventureMission.WingProgress progress = this.GetProgress(adventureMission.RequiredProgress.Wing);
		return progress != null && progress.MeetsProgressAndFlagsRequirements(adventureMission.RequiredProgress) && (!checkEventTiming || AdventureProgressMgr.IsWingEventActive(adventureMission.RequiredProgress.Wing));
	}

	// Token: 0x060000BD RID: 189 RVA: 0x000045DC File Offset: 0x000027DC
	public bool HasDefeatedScenario(int scenarioID)
	{
		bool flag;
		return this.HasDefeatedScenario(scenarioID, out flag);
	}

	// Token: 0x060000BE RID: 190 RVA: 0x000045F4 File Offset: 0x000027F4
	public bool HasDefeatedScenario(int scenarioID, out bool hasUnackedProgress)
	{
		hasUnackedProgress = false;
		AdventureMission adventureMission;
		if (!this.m_missions.TryGetValue(scenarioID, out adventureMission))
		{
			return false;
		}
		if (adventureMission.RequiredProgress == null)
		{
			return false;
		}
		if (adventureMission.GrantedProgress == null)
		{
			return false;
		}
		AdventureMission.WingProgress progress = this.GetProgress(adventureMission.GrantedProgress.Wing);
		if (progress == null)
		{
			return false;
		}
		int num;
		this.GetWingAck(adventureMission.GrantedProgress.Wing, out num);
		hasUnackedProgress = (num < adventureMission.GrantedProgress.Progress);
		return progress.MeetsProgressAndFlagsRequirements(adventureMission.GrantedProgress);
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00004674 File Offset: 0x00002874
	public static bool GetGameSaveDataProgressForScenario(int scenarioId, out int progress, out int maxProgress)
	{
		if (!AdventureProgressMgr.ScenarioUsesGameSaveDataProgress(scenarioId))
		{
			progress = 0;
			maxProgress = 0;
			Debug.LogError(string.Format("Attempting to get Game Save Data progress for Scenario={0}, which does not have any Game Save Data. Add a GSD Subkey to that scenario's dbi.", scenarioId));
			return false;
		}
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(scenarioId);
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)AdventureConfig.Get().GetSelectedAdventureDataRecord().GameSaveDataServerKey;
		GameSaveKeySubkeyId gameSaveDataProgressSubkey = (GameSaveKeySubkeyId)record.GameSaveDataProgressSubkey;
		long num;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, gameSaveDataProgressSubkey, out num);
		progress = (int)num;
		maxProgress = record.GameSaveDataProgressMax;
		return true;
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x000046E8 File Offset: 0x000028E8
	public static bool ScenarioUsesGameSaveDataProgress(int scenarioId)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(scenarioId);
		return record.GameSaveDataProgressSubkey != 0 && Enum.IsDefined(typeof(GameSaveKeySubkeyId), record.GameSaveDataProgressSubkey);
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00004728 File Offset: 0x00002928
	public bool ScenarioHasRewardData(int scenarioId)
	{
		List<RewardData> immediateRewardsForDefeatingScenario = this.GetImmediateRewardsForDefeatingScenario(scenarioId);
		return immediateRewardsForDefeatingScenario != null && immediateRewardsForDefeatingScenario.Count > 0;
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x0000474C File Offset: 0x0000294C
	public List<RewardData> GetImmediateRewardsForDefeatingScenario(int scenarioID)
	{
		HashSet<Assets.Achieve.RewardTiming> rewardTimings = new HashSet<Assets.Achieve.RewardTiming>
		{
			Assets.Achieve.RewardTiming.IMMEDIATE
		};
		return this.GetRewardsForDefeatingScenario(scenarioID, rewardTimings);
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00004770 File Offset: 0x00002970
	public List<RewardData> GetRewardsForDefeatingScenario(int scenarioID, HashSet<Assets.Achieve.RewardTiming> rewardTimings)
	{
		AdventureMission adventureMission;
		if (!this.m_missions.TryGetValue(scenarioID, out adventureMission))
		{
			return new List<RewardData>();
		}
		List<RewardData> result = null;
		if (GameUtils.IsHeroicAdventureMission(scenarioID) || GameUtils.IsClassChallengeMission(scenarioID) || adventureMission.GrantedProgress != null)
		{
			result = AchieveManager.Get().GetRewardsForAdventureScenario(adventureMission.GrantedProgress.Wing, scenarioID, rewardTimings);
		}
		return result;
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x000047C8 File Offset: 0x000029C8
	public bool SetWingAck(int wing, int ackId)
	{
		Log.Adventures.Print("SetWingAck for wing {0}", new object[]
		{
			wing
		});
		int num;
		if (this.m_wingAckState.TryGetValue(wing, out num))
		{
			if (ackId < num)
			{
				return false;
			}
			if (ackId == num)
			{
				return true;
			}
		}
		this.m_wingAckState[wing] = ackId;
		Network.Get().AckWingProgress(wing, ackId);
		return true;
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00004829 File Offset: 0x00002A29
	public bool GetWingAck(int wing, out int ack)
	{
		return this.m_wingAckState.TryGetValue(wing, out ack);
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x00004838 File Offset: 0x00002A38
	public AdventureMissionState AdventureMissionStateForScenario(int scenarioID)
	{
		if (this.HasDefeatedScenario(scenarioID))
		{
			return AdventureMissionState.COMPLETED;
		}
		if (this.CanPlayScenario(scenarioID, true))
		{
			return AdventureMissionState.UNLOCKED;
		}
		return AdventureMissionState.LOCKED;
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00004852 File Offset: 0x00002A52
	public AdventureChapterState AdventureBookChapterStateForWing(WingDbfRecord wingRecord, AdventureModeDbId adventureMode)
	{
		if (this.IsWingComplete((AdventureDbId)wingRecord.AdventureId, adventureMode, (WingDbId)wingRecord.ID))
		{
			return AdventureChapterState.COMPLETED;
		}
		if (this.GetNumPlayableScenariosForWing(wingRecord, adventureMode) > 0)
		{
			return AdventureChapterState.UNLOCKED;
		}
		return AdventureChapterState.LOCKED;
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00004879 File Offset: 0x00002A79
	public bool OwnershipPrereqWingIsOwned(AdventureWingDef wingDef)
	{
		return wingDef.GetOwnershipPrereqId() == WingDbId.INVALID || this.OwnsWing((int)wingDef.GetOwnershipPrereqId());
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00004891 File Offset: 0x00002A91
	public bool OwnershipPrereqWingIsOwned(WingDbfRecord wingRecord)
	{
		return wingRecord.OwnershipPrereqWingId == 0 || this.OwnsWing(wingRecord.OwnershipPrereqWingId);
	}

	// Token: 0x060000CA RID: 202 RVA: 0x000048AC File Offset: 0x00002AAC
	private void LoadAdventureMissionsFromDBF()
	{
		foreach (AdventureMissionDbfRecord adventureMissionDbfRecord in GameDbf.AdventureMission.GetRecords())
		{
			int scenarioId = adventureMissionDbfRecord.ScenarioId;
			if (this.m_missions.ContainsKey(scenarioId))
			{
				Debug.LogWarning(string.Format("AdventureProgressMgr.LoadAdventureMissionsFromDBF(): duplicate entry found for scenario ID {0}", scenarioId));
			}
			else
			{
				string noteDesc = adventureMissionDbfRecord.NoteDesc;
				AdventureMission.WingProgress requiredProgress = new AdventureMission.WingProgress(adventureMissionDbfRecord.ReqWingId, adventureMissionDbfRecord.ReqProgress, adventureMissionDbfRecord.ReqFlags);
				AdventureMission.WingProgress grantedProgress = new AdventureMission.WingProgress(adventureMissionDbfRecord.GrantsWingId, adventureMissionDbfRecord.GrantsProgress, adventureMissionDbfRecord.GrantsFlags);
				this.m_missions[scenarioId] = new AdventureMission(scenarioId, noteDesc, requiredProgress, grantedProgress);
			}
		}
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00004980 File Offset: 0x00002B80
	private void OnAdventureProgress()
	{
		foreach (Network.AdventureProgress adventureProgress in Network.Get().GetAdventureProgressResponse())
		{
			this.CreateOrUpdateProgress(true, adventureProgress.Wing, adventureProgress.Progress);
			this.CreateOrUpdateWingFlags(true, adventureProgress.Wing, adventureProgress.Flags);
			this.CreateOrUpdateWingAck(adventureProgress.Wing, adventureProgress.Ack);
		}
		this.IsReady = true;
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00004A10 File Offset: 0x00002C10
	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		List<long> list = new List<long>();
		foreach (NetCache.ProfileNotice profileNotice in newNotices)
		{
			if (profileNotice.Type == NetCache.ProfileNotice.NoticeType.ADVENTURE_PROGRESS)
			{
				NetCache.ProfileNoticeAdventureProgress profileNoticeAdventureProgress = profileNotice as NetCache.ProfileNoticeAdventureProgress;
				if (profileNoticeAdventureProgress.Progress != null)
				{
					this.CreateOrUpdateProgress(false, profileNoticeAdventureProgress.Wing, profileNoticeAdventureProgress.Progress.Value);
				}
				if (profileNoticeAdventureProgress.Flags != null)
				{
					this.CreateOrUpdateWingFlags(false, profileNoticeAdventureProgress.Wing, profileNoticeAdventureProgress.Flags.Value);
				}
				list.Add(profileNotice.NoticeID);
			}
		}
		foreach (long id in list)
		{
			Network.Get().AckNotice(id);
		}
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00004B1C File Offset: 0x00002D1C
	private void FireProgressUpdate(bool isStartupAction, AdventureMission.WingProgress oldProgress, AdventureMission.WingProgress newProgress)
	{
		AdventureProgressMgr.AdventureProgressUpdatedListener[] array = this.m_progressUpdatedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(isStartupAction, oldProgress, newProgress);
		}
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00004B50 File Offset: 0x00002D50
	private void CreateOrUpdateProgress(bool isStartupAction, int wing, int progress)
	{
		if (!this.m_wingProgress.ContainsKey(wing))
		{
			this.m_wingProgress[wing] = new AdventureMission.WingProgress(wing, progress, 0UL);
			this.FireProgressUpdate(isStartupAction, null, this.m_wingProgress[wing]);
			return;
		}
		AdventureMission.WingProgress wingProgress = this.m_wingProgress[wing].Clone();
		this.m_wingProgress[wing].SetProgress(progress);
		Log.Adventures.Print("AdventureProgressMgr.CreateOrUpdateProgress: updating wing {0} : PROGRESS {1} (former progress {2})", new object[]
		{
			wing,
			this.m_wingProgress[wing],
			wingProgress
		});
		this.FireProgressUpdate(isStartupAction, wingProgress, this.m_wingProgress[wing]);
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00004C00 File Offset: 0x00002E00
	private void CreateOrUpdateWingFlags(bool isStartupAction, int wing, ulong flags)
	{
		if (!this.m_wingProgress.ContainsKey(wing))
		{
			this.m_wingProgress[wing] = new AdventureMission.WingProgress(wing, 0, flags);
			Log.Adventures.Print("AdventureProgressMgr.CreateOrUpdateWingFlags: creating wing {0} : PROGRESS {1}", new object[]
			{
				wing,
				this.m_wingProgress[wing]
			});
			this.FireProgressUpdate(isStartupAction, null, this.m_wingProgress[wing]);
			return;
		}
		AdventureMission.WingProgress oldProgress = this.m_wingProgress[wing].Clone();
		this.m_wingProgress[wing].SetFlags(flags);
		this.FireProgressUpdate(isStartupAction, oldProgress, this.m_wingProgress[wing]);
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00004CA9 File Offset: 0x00002EA9
	private void CreateOrUpdateWingAck(int wing, int ack)
	{
		this.m_wingAckState[wing] = ack;
	}

	// Token: 0x04000064 RID: 100
	private Map<int, AdventureMission.WingProgress> m_wingProgress = new Map<int, AdventureMission.WingProgress>();

	// Token: 0x04000065 RID: 101
	private Map<int, int> m_wingAckState = new Map<int, int>();

	// Token: 0x04000066 RID: 102
	private Map<int, AdventureMission> m_missions = new Map<int, AdventureMission>();

	// Token: 0x04000067 RID: 103
	private List<AdventureProgressMgr.AdventureProgressUpdatedListener> m_progressUpdatedListeners = new List<AdventureProgressMgr.AdventureProgressUpdatedListener>();

	// Token: 0x02001267 RID: 4711
	public enum WingProgressMeaning
	{
		// Token: 0x0400A371 RID: 41841
		NO_PROGRESS,
		// Token: 0x0400A372 RID: 41842
		AVAILABLE,
		// Token: 0x0400A373 RID: 41843
		COMPLETED_MISSION_1,
		// Token: 0x0400A374 RID: 41844
		COMPLETED_MISSION_2,
		// Token: 0x0400A375 RID: 41845
		COMPLETED_MISSION_3,
		// Token: 0x0400A376 RID: 41846
		COMPLETED_MISSION_4,
		// Token: 0x0400A377 RID: 41847
		COMPLETED_MISSION_5
	}

	// Token: 0x02001268 RID: 4712
	// (Invoke) Token: 0x0600D408 RID: 54280
	public delegate void AdventureProgressUpdatedCallback(bool isStartupAction, AdventureMission.WingProgress oldProgress, AdventureMission.WingProgress newProgress, object userData);

	// Token: 0x02001269 RID: 4713
	private class AdventureProgressUpdatedListener : EventListener<AdventureProgressMgr.AdventureProgressUpdatedCallback>
	{
		// Token: 0x0600D40B RID: 54283 RVA: 0x003E37CC File Offset: 0x003E19CC
		public void Fire(bool isStartupAction, AdventureMission.WingProgress oldProgress, AdventureMission.WingProgress newProgress)
		{
			this.m_callback(isStartupAction, oldProgress, newProgress, this.m_userData);
		}
	}
}
