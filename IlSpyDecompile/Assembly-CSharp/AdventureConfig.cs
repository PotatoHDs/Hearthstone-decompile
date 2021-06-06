using System;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.DungeonCrawl;
using Hearthstone.UI;
using UnityEngine;

[CustomEditClass]
public class AdventureConfig : MonoBehaviour
{
	public delegate void DelBossDefLoaded(AdventureBossDef bossDef, bool success);

	public delegate void AdventureModeChange(AdventureDbId adventureId, AdventureModeDbId modeId);

	public delegate void AdventureMissionSet(ScenarioDbId mission, bool showDetails);

	public delegate void SubSceneChange(AdventureData.Adventuresubscene newscene, bool forward);

	public delegate void SelectedModeChange(AdventureDbId adventureId, AdventureModeDbId modeId);

	public delegate void AnomalyModeChangedHandler(bool anomalyModeActived);

	public const string DEFAULT_SET_UP_STATE = "SetUpState";

	public const string PLAY_BUTTON_ANOMALY_ACTIVE_STATE = "PURPLE_SWIRL";

	public const string PLAY_BUTTON_ANOMALY_INACTIVE_STATE = "BLUE_SWIRL";

	private static AdventureConfig s_instance;

	private AdventureDbId m_SelectedAdventure = AdventureDbId.PRACTICE;

	private AdventureModeDbId m_SelectedMode = AdventureModeDbId.LINEAR;

	private Stack<AdventureData.Adventuresubscene> m_SubSceneBackStack = new Stack<AdventureData.Adventuresubscene>();

	private AdventureData.Adventuresubscene m_CurrentSubScene;

	private AdventureData.Adventuresubscene m_PreviousSubScene = AdventureData.Adventuresubscene.INVALID;

	private ScenarioDbId m_SelectedMission;

	private ScenarioDbId m_MissionOverride;

	private bool m_anomalyModeActivated;

	private List<long> NeedsChapterNewlyUnlockedHighlight = new List<long>();

	private bool m_allChaptersOwned;

	private Reward.Type m_completionRewardType;

	private int m_completionRewardId;

	private List<AdventureModeChange> m_AdventureModeChangeEventList = new List<AdventureModeChange>();

	private List<SubSceneChange> m_SubSceneChangeEventList = new List<SubSceneChange>();

	private List<SelectedModeChange> m_SelectedModeChangeEventList = new List<SelectedModeChange>();

	private List<AdventureMissionSet> m_AdventureMissionSetEventList = new List<AdventureMissionSet>();

	private Map<string, int> m_WingBossesDefeatedCache = new Map<string, int>();

	private Map<string, ScenarioDbId> m_LastSelectedMissions = new Map<string, ScenarioDbId>();

	private Map<ScenarioDbId, bool> m_CachedDefeatedScenario = new Map<ScenarioDbId, bool>();

	private Map<ScenarioDbId, AdventureBossDef> m_CachedBossDef = new Map<ScenarioDbId, AdventureBossDef>();

	private Map<AdventureDbId, AdventureModeDbId> m_ClientChooserAdventureModes = new Map<AdventureDbId, AdventureModeDbId>();

	private AdventureDbId SelectedAdventure
	{
		get
		{
			return m_SelectedAdventure;
		}
		set
		{
			if (value != m_SelectedAdventure)
			{
				ResetLoadout();
			}
			m_SelectedAdventure = value;
			AdventureDataModel adventureDataModel = GetAdventureDataModel();
			if (adventureDataModel != null)
			{
				adventureDataModel.SelectedAdventure = value;
				AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)value);
				adventureDataModel.StoreDescriptionTextTimelockedTrue = ((record != null) ? ((string)record.StoreBuyRemainingWingsDescTimelockedTrue) : string.Empty);
				adventureDataModel.StoreDescriptionTextTimelockedFalse = ((record != null) ? ((string)record.StoreBuyRemainingWingsDescTimelockedFalse) : string.Empty);
			}
		}
	}

	private AdventureModeDbId SelectedMode
	{
		get
		{
			return m_SelectedMode;
		}
		set
		{
			if (value != m_SelectedMode)
			{
				ResetLoadout();
			}
			m_SelectedMode = value;
			AdventureDataModel adventureDataModel = GetAdventureDataModel();
			if (adventureDataModel != null)
			{
				adventureDataModel.SelectedAdventureMode = value;
				adventureDataModel.IsSelectedModeHeroic = GameUtils.IsModeHeroic(value);
			}
		}
	}

	public AdventureData.Adventuresubscene CurrentSubScene => m_CurrentSubScene;

	public AdventureData.Adventuresubscene PreviousSubScene => m_PreviousSubScene;

	public bool AnomalyModeActivated
	{
		get
		{
			return m_anomalyModeActivated;
		}
		set
		{
			if (value != m_anomalyModeActivated)
			{
				m_anomalyModeActivated = value;
				AdventureDataModel adventureDataModel = GetAdventureDataModel();
				if (adventureDataModel != null)
				{
					adventureDataModel.AnomalyActivated = m_anomalyModeActivated;
				}
				if (this.OnAnomalyModeChanged != null)
				{
					this.OnAnomalyModeChanged(value);
				}
			}
		}
	}

	public TAG_CLASS SelectedHeroClass { get; set; }

	public long SelectedLoadoutTreasureDbId { get; set; }

	public long SelectedDeckId { get; set; }

	public long SelectedHeroPowerDbId { get; set; }

	public bool ShouldSeeFirstTimeFlow
	{
		get
		{
			return GetAdventureDataModel().ShouldSeeFirstTimeFlow;
		}
		set
		{
			GetAdventureDataModel().ShouldSeeFirstTimeFlow = value;
		}
	}

	public bool AllChaptersOwned
	{
		get
		{
			return m_allChaptersOwned;
		}
		set
		{
			m_allChaptersOwned = value;
			AdventureDataModel adventureDataModel = GetAdventureDataModel();
			if (adventureDataModel != null)
			{
				adventureDataModel.AllChaptersOwned = m_allChaptersOwned;
			}
		}
	}

	public RewardListDataModel CompletionRewards
	{
		get
		{
			AdventureDataModel adventureDataModel = GetAdventureDataModel();
			if (adventureDataModel != null)
			{
				if (adventureDataModel.CompletionRewards == null)
				{
					adventureDataModel.CompletionRewards = new RewardListDataModel();
				}
				return adventureDataModel.CompletionRewards;
			}
			return null;
		}
		set
		{
			AdventureDataModel adventureDataModel = GetAdventureDataModel();
			if (adventureDataModel != null)
			{
				adventureDataModel.CompletionRewards = value;
			}
		}
	}

	public Reward.Type CompletionRewardType
	{
		get
		{
			return m_completionRewardType;
		}
		set
		{
			m_completionRewardType = value;
			AdventureDataModel adventureDataModel = GetAdventureDataModel();
			if (adventureDataModel != null)
			{
				adventureDataModel.CompletionRewardType = m_completionRewardType;
			}
		}
	}

	public int CompletionRewardId
	{
		get
		{
			return m_completionRewardId;
		}
		set
		{
			m_completionRewardId = value;
			AdventureDataModel adventureDataModel = GetAdventureDataModel();
			if (adventureDataModel != null)
			{
				adventureDataModel.CompletionRewardId = m_completionRewardId;
			}
		}
	}

	public event AnomalyModeChangedHandler OnAnomalyModeChanged;

	public event Action OnAdventureSceneUnloadEvent;

	public static AdventureConfig Get()
	{
		return s_instance;
	}

	public static AdventureData.Adventuresubscene GetSubSceneFromMode(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)modeId);
		if (adventureDataRecord == null)
		{
			Debug.LogErrorFormat("AdventureConfig.GetSubSceneFromMode() - No Adventure Data record found for Adventure {0} and Mode {1}", (int)adventureId, (int)modeId);
			return AdventureData.Adventuresubscene.CHOOSER;
		}
		if (adventureDataRecord.StartingSubscene == AdventureData.Adventuresubscene.DUNGEON_CRAWL)
		{
			return Get().GetCorrectSubSceneWhenLoadingDungeonCrawlMode();
		}
		return adventureDataRecord.StartingSubscene;
	}

	public AdventureDbId GetSelectedAdventure()
	{
		return SelectedAdventure;
	}

	public AdventureModeDbId GetSelectedMode()
	{
		return SelectedMode;
	}

	public AdventureDataModel GetAdventureDataModel()
	{
		if (!GlobalDataContext.Get().GetDataModel(7, out var model))
		{
			model = new AdventureDataModel();
			GlobalDataContext.Get().BindDataModel(model);
		}
		AdventureDataModel obj = model as AdventureDataModel;
		if (obj == null)
		{
			Log.Adventures.PrintWarning("AdventureDataModel is null!");
		}
		return obj;
	}

	public AdventureDataDbfRecord GetSelectedAdventureDataRecord()
	{
		return GetAdventureDataRecord(GetSelectedAdventure(), GetSelectedMode());
	}

	public AdventureModeDbId GetClientChooserAdventureMode(AdventureDbId adventureDbId)
	{
		if (m_ClientChooserAdventureModes.TryGetValue(adventureDbId, out var value))
		{
			return value;
		}
		if (SelectedAdventure != adventureDbId)
		{
			return AdventureModeDbId.LINEAR;
		}
		return SelectedMode;
	}

	public static AdventureDataDbfRecord GetAdventureDataRecord(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		return GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == (int)adventureId && r.ModeId == (int)modeId);
	}

	public static bool CanPlayMode(AdventureDbId adventureId, AdventureModeDbId modeId, bool checkEventTimings = true)
	{
		bool flag = AchieveManager.Get().HasUnlockedFeature(Achieve.Unlocks.VANILLA_HEROES);
		if (adventureId == AdventureDbId.PRACTICE)
		{
			if (modeId == AdventureModeDbId.EXPERT)
			{
				return flag;
			}
			return true;
		}
		if (!flag && AdventureUtils.DoesAdventureRequireAllHeroesUnlocked(adventureId, modeId))
		{
			return false;
		}
		if (modeId == AdventureModeDbId.LINEAR || modeId == AdventureModeDbId.DUNGEON_CRAWL)
		{
			return true;
		}
		return GameDbf.Scenario.GetRecord((ScenarioDbfRecord r) => r.AdventureId == (int)adventureId && r.ModeId == (int)modeId && r.WingId > 0 && AdventureProgressMgr.Get().CanPlayScenario(r.ID, checkEventTimings)) != null;
	}

	public static bool IsFeaturedMode(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		if (!CanPlayMode(adventureId, modeId))
		{
			return false;
		}
		Option hasSeenFeaturedModeOptionFromAdventureData = GetHasSeenFeaturedModeOptionFromAdventureData(adventureId, modeId);
		if (hasSeenFeaturedModeOptionFromAdventureData == Option.INVALID)
		{
			return false;
		}
		return !Options.Get().GetBool(hasSeenFeaturedModeOptionFromAdventureData, defaultVal: false);
	}

	public static bool MarkFeaturedMode(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		if (!CanPlayMode(adventureId, modeId))
		{
			return false;
		}
		Option hasSeenFeaturedModeOptionFromAdventureData = GetHasSeenFeaturedModeOptionFromAdventureData(adventureId, modeId);
		if (hasSeenFeaturedModeOptionFromAdventureData == Option.INVALID)
		{
			return false;
		}
		Options.Get().SetBool(hasSeenFeaturedModeOptionFromAdventureData, val: true);
		return true;
	}

	public static bool ShouldShowNewModePopup(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		if (!CanPlayMode(adventureId, modeId))
		{
			return false;
		}
		Option hasSeenNewModePopupOptionFromAdventureData = GetHasSeenNewModePopupOptionFromAdventureData(adventureId, modeId);
		if (hasSeenNewModePopupOptionFromAdventureData == Option.INVALID)
		{
			return false;
		}
		return !Options.Get().GetBool(hasSeenNewModePopupOptionFromAdventureData, defaultVal: false);
	}

	public static bool MarkHasSeenNewModePopup(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		if (!CanPlayMode(adventureId, modeId))
		{
			return false;
		}
		Option hasSeenNewModePopupOptionFromAdventureData = GetHasSeenNewModePopupOptionFromAdventureData(adventureId, modeId);
		if (hasSeenNewModePopupOptionFromAdventureData == Option.INVALID)
		{
			return false;
		}
		Options.Get().SetBool(hasSeenNewModePopupOptionFromAdventureData, val: true);
		return true;
	}

	private static Option GetHasSeenFeaturedModeOptionFromAdventureData(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)modeId);
		if (adventureDataRecord == null)
		{
			return Option.INVALID;
		}
		return OptionUtils.GetOptionFromString(adventureDataRecord.HasSeenFeaturedModeOption);
	}

	private static Option GetHasSeenNewModePopupOptionFromAdventureData(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)modeId);
		if (adventureDataRecord == null)
		{
			return Option.INVALID;
		}
		return OptionUtils.GetOptionFromString(adventureDataRecord.HasSeenNewModePopupOption);
	}

	public string GetSelectedAdventureAndModeString()
	{
		return $"{SelectedAdventure}_{SelectedMode}";
	}

	public void SetSelectedAdventureMode(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		SelectedAdventure = adventureId;
		SelectedMode = modeId;
		m_ClientChooserAdventureModes[adventureId] = modeId;
		Options.Get().SetInt(Option.SELECTED_ADVENTURE, (int)SelectedAdventure);
		Options.Get().SetInt(Option.SELECTED_ADVENTURE_MODE, (int)SelectedMode);
		SetPropertiesForAdventureAndMode();
		FireSelectedModeChangeEvent();
	}

	public void MarkHasSeenFirstTimeFlowComplete()
	{
		if (!GameUtils.IsModeHeroic(SelectedMode))
		{
			AdventureDataDbfRecord record = GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == (int)SelectedAdventure && r.ModeId == (int)SelectedMode);
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest((GameSaveKeyId)record.GameSaveDataClientKey, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_FIRST_TIME_FLOW, 1L));
			ShouldSeeFirstTimeFlow = false;
		}
	}

	public void UpdateShouldSeeFirstTimeFlowForSelectedMode()
	{
		if (GameUtils.IsModeHeroic(SelectedMode))
		{
			ShouldSeeFirstTimeFlow = false;
			return;
		}
		long value = 0L;
		AdventureDataDbfRecord record = GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == (int)SelectedAdventure && r.ModeId == (int)SelectedMode);
		if (record == null)
		{
			ShouldSeeFirstTimeFlow = true;
			return;
		}
		if (record.GameSaveDataClientKey <= 0)
		{
			ShouldSeeFirstTimeFlow = false;
			return;
		}
		GameSaveDataManager.Get().GetSubkeyValue((GameSaveKeyId)record.GameSaveDataClientKey, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_FIRST_TIME_FLOW, out value);
		ShouldSeeFirstTimeFlow = value <= 0;
	}

	public static AdventureModeDbId GetDefaultModeDbIdForAdventure(AdventureDbId adventureId)
	{
		if (adventureId == AdventureDbId.INVALID)
		{
			return AdventureModeDbId.INVALID;
		}
		return (AdventureModeDbId)(GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == (int)adventureId)?.ModeId ?? 0);
	}

	public ScenarioDbId GetMission()
	{
		return m_SelectedMission;
	}

	public ScenarioDbId GetMissionToPlay()
	{
		if (m_MissionOverride == ScenarioDbId.INVALID)
		{
			return GetMission();
		}
		return m_MissionOverride;
	}

	public ScenarioDbId GetLastSelectedMission()
	{
		string selectedAdventureAndModeString = GetSelectedAdventureAndModeString();
		ScenarioDbId value = ScenarioDbId.INVALID;
		m_LastSelectedMissions.TryGetValue(selectedAdventureAndModeString, out value);
		return value;
	}

	public bool IsScenarioDefeatedAndInitCache(ScenarioDbId mission)
	{
		bool flag = AdventureProgressMgr.Get().HasDefeatedScenario((int)mission);
		if (!m_CachedDefeatedScenario.ContainsKey(mission))
		{
			m_CachedDefeatedScenario[mission] = flag;
		}
		return flag;
	}

	public bool IsScenarioJustDefeated(ScenarioDbId mission)
	{
		bool flag = AdventureProgressMgr.Get().HasDefeatedScenario((int)mission);
		bool value = false;
		m_CachedDefeatedScenario.TryGetValue(mission, out value);
		m_CachedDefeatedScenario[mission] = flag;
		return flag != value;
	}

	public AdventureBossDef GetBossDef(ScenarioDbId mission)
	{
		AdventureBossDef value = null;
		if (!m_CachedBossDef.TryGetValue(mission, out value) && !string.IsNullOrEmpty(GetBossDefAssetPath(mission)))
		{
			Debug.LogErrorFormat("Boss def for mission not loaded: {0}\nCall LoadBossDef first.", mission);
		}
		return value;
	}

	public void LoadBossDef(ScenarioDbId mission, DelBossDefLoaded callback)
	{
		AdventureBossDef value = null;
		if (m_CachedBossDef.TryGetValue(mission, out value))
		{
			callback(value, success: true);
			return;
		}
		string bossDefAssetPath = GetBossDefAssetPath(mission);
		if (string.IsNullOrEmpty(bossDefAssetPath))
		{
			if (callback != null)
			{
				callback(null, success: false);
			}
			return;
		}
		AssetLoader.Get().InstantiatePrefab(bossDefAssetPath, delegate(AssetReference path, GameObject go, object data)
		{
			if (go == null)
			{
				Debug.LogError($"Unable to instantiate boss def: {path}");
				callback?.Invoke(null, success: false);
			}
			else
			{
				AdventureBossDef component = go.GetComponent<AdventureBossDef>();
				if (component == null)
				{
					Debug.LogError($"Object does not contain AdventureBossDef component: {path}");
				}
				else
				{
					m_CachedBossDef[mission] = component;
				}
				callback?.Invoke(component, component != null);
			}
		});
	}

	public static string GetBossDefAssetPath(ScenarioDbId mission)
	{
		return GameDbf.AdventureMission.GetRecord((AdventureMissionDbfRecord r) => r.ScenarioId == (int)mission)?.BossDefAssetPath;
	}

	public void ClearBossDefs()
	{
		foreach (KeyValuePair<ScenarioDbId, AdventureBossDef> item in m_CachedBossDef)
		{
			UnityEngine.Object.Destroy(item.Value);
		}
		m_CachedBossDef.Clear();
	}

	public void SetMission(ScenarioDbId mission, bool showDetails = true)
	{
		m_SelectedMission = mission;
		Log.Adventures.Print("Selected Mission set to {0}", mission);
		string selectedAdventureAndModeString = GetSelectedAdventureAndModeString();
		m_LastSelectedMissions[selectedAdventureAndModeString] = mission;
		AdventureMissionSet[] array = m_AdventureMissionSetEventList.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](mission, showDetails);
		}
	}

	public void SetMissionOverride(ScenarioDbId missionOverride)
	{
		m_MissionOverride = missionOverride;
	}

	public ScenarioDbId GetMissionOverride()
	{
		return m_MissionOverride;
	}

	public bool DoesSelectedMissionRequireDeck()
	{
		return DoesMissionRequireDeck(m_SelectedMission);
	}

	public static bool DoesMissionRequireDeck(ScenarioDbId scenario)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord((int)scenario);
		if (record == null)
		{
			return true;
		}
		return record.Player1DeckId == 0;
	}

	public void AddAdventureMissionSetListener(AdventureMissionSet dlg)
	{
		m_AdventureMissionSetEventList.Add(dlg);
	}

	public void RemoveAdventureMissionSetListener(AdventureMissionSet dlg)
	{
		m_AdventureMissionSetEventList.Remove(dlg);
	}

	public void AddAdventureModeChangeListener(AdventureModeChange dlg)
	{
		m_AdventureModeChangeEventList.Add(dlg);
	}

	public void RemoveAdventureModeChangeListener(AdventureModeChange dlg)
	{
		m_AdventureModeChangeEventList.Remove(dlg);
	}

	public void AddSubSceneChangeListener(SubSceneChange dlg)
	{
		m_SubSceneChangeEventList.Add(dlg);
	}

	public void RemoveSubSceneChangeListener(SubSceneChange dlg)
	{
		m_SubSceneChangeEventList.Remove(dlg);
	}

	public void AddSelectedModeChangeListener(SelectedModeChange dlg)
	{
		m_SelectedModeChangeEventList.Add(dlg);
	}

	public void RemoveSelectedModeChangeListener(SelectedModeChange dlg)
	{
		m_SelectedModeChangeEventList.Remove(dlg);
	}

	public void ResetSubScene(AdventureData.Adventuresubscene subscene)
	{
		m_CurrentSubScene = subscene;
		m_PreviousSubScene = AdventureData.Adventuresubscene.INVALID;
		m_SubSceneBackStack.Clear();
	}

	public void ChangeSubScene(AdventureData.Adventuresubscene subscene, bool pushToBackStack = true)
	{
		if (subscene == m_CurrentSubScene)
		{
			Debug.Log($"Sub scene {subscene} is already set.");
			return;
		}
		if (pushToBackStack)
		{
			m_SubSceneBackStack.Push(m_CurrentSubScene);
		}
		m_PreviousSubScene = m_CurrentSubScene;
		m_CurrentSubScene = subscene;
		FireSubSceneChangeEvent(forward: true);
		FireAdventureModeChangeEvent();
	}

	public void SubSceneGoBack(bool fireevent = true)
	{
		if (m_SubSceneBackStack.Count == 0)
		{
			Debug.Log("No sub scenes exist in the back stack.");
			return;
		}
		m_PreviousSubScene = m_CurrentSubScene;
		m_CurrentSubScene = m_SubSceneBackStack.Pop();
		if (fireevent)
		{
			FireSubSceneChangeEvent(forward: false);
		}
		FireAdventureModeChangeEvent();
	}

	public void RemoveSubScenesFromStackUntilTargetReached(AdventureData.Adventuresubscene targetSubscene)
	{
		while (m_SubSceneBackStack.Count > 0 && m_SubSceneBackStack.Peek() != targetSubscene)
		{
			m_SubSceneBackStack.Pop();
		}
	}

	public void RemoveSubSceneIfOnTopOfStack(AdventureData.Adventuresubscene subscene)
	{
		if (m_SubSceneBackStack.Peek() == subscene)
		{
			m_SubSceneBackStack.Pop();
		}
	}

	public void ChangeSubSceneToSelectedAdventure()
	{
		RequestGameSaveDataKeysForSelectedAdventure(delegate(bool success)
		{
			if (success)
			{
				if (GameUtils.DoesAdventureModeUseDungeonCrawlFormat(GetSelectedMode()))
				{
					AdventureDataDbfRecord selectedAdventureDataRecord = GetSelectedAdventureDataRecord();
					if (selectedAdventureDataRecord != null)
					{
						DungeonCrawlUtil.MigrateDungeonCrawlSubkeys((GameSaveKeyId)selectedAdventureDataRecord.GameSaveDataClientKey, (GameSaveKeyId)selectedAdventureDataRecord.GameSaveDataServerKey);
					}
				}
			}
			else
			{
				Debug.LogError("ChangeSubSceneToSelectedAdventure - Request for Adventure Game Save Keys failed.");
			}
			AdventureData.Adventuresubscene subSceneFromMode = GetSubSceneFromMode(SelectedAdventure, SelectedMode);
			UpdateShouldSeeFirstTimeFlowForSelectedMode();
			if (ShouldSeeFirstTimeFlow && AllChaptersOwned && !AdventureUtils.IsEntireAdventureFree(SelectedAdventure))
			{
				MarkHasSeenFirstTimeFlowComplete();
			}
			ChangeSubScene(subSceneFromMode);
		});
	}

	public void RequestGameSaveDataKeysForSelectedAdventure(GameSaveDataManager.OnRequestDataResponseDelegate onCompleteCallback)
	{
		AdventureDataDbfRecord selectedAdventureDataRecord = GetSelectedAdventureDataRecord();
		List<GameSaveKeyId> list = new List<GameSaveKeyId>();
		if (selectedAdventureDataRecord != null && selectedAdventureDataRecord.GameSaveDataClientKey != 0)
		{
			list.Add((GameSaveKeyId)selectedAdventureDataRecord.GameSaveDataClientKey);
		}
		if (selectedAdventureDataRecord != null && selectedAdventureDataRecord.GameSaveDataServerKey != 0)
		{
			list.Add((GameSaveKeyId)selectedAdventureDataRecord.GameSaveDataServerKey);
		}
		if (GameUtils.IsModeHeroic(GetSelectedMode()))
		{
			AdventureDataDbfRecord adventureDataRecord = GetAdventureDataRecord(GetSelectedAdventure(), GameUtils.GetNormalModeFromHeroicMode(GetSelectedMode()));
			if (adventureDataRecord != null && adventureDataRecord.GameSaveDataClientKey != 0)
			{
				list.Add((GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey);
			}
		}
		if (list.Count > 0)
		{
			GameSaveDataManager.Get().Request(list, onCompleteCallback);
		}
		else
		{
			onCompleteCallback(success: true);
		}
	}

	public static bool IsMissionAvailable(int missionId)
	{
		bool flag = AdventureProgressMgr.Get().CanPlayScenario(missionId);
		if (!flag)
		{
			return false;
		}
		int missionReqProgress = 0;
		int wingId = 0;
		if (!GetMissionPlayableParameters(missionId, ref wingId, ref missionReqProgress))
		{
			return false;
		}
		int ack = 0;
		AdventureProgressMgr.Get().GetWingAck(wingId, out ack);
		if (flag)
		{
			return missionReqProgress <= ack;
		}
		return false;
	}

	public static bool IsMissionNewlyAvailableAndGetReqs(int missionId, ref int wingId, ref int missionReqProgress)
	{
		if (!GetMissionPlayableParameters(missionId, ref wingId, ref missionReqProgress))
		{
			return false;
		}
		bool flag = AdventureProgressMgr.Get().CanPlayScenario(missionId);
		int ack = 0;
		AdventureProgressMgr.Get().GetWingAck(wingId, out ack);
		if (ack < missionReqProgress && flag)
		{
			return true;
		}
		return false;
	}

	public static bool AckCurrentWingProgress(int wingId)
	{
		int ackProgress = AdventureProgressMgr.Get().GetProgress(wingId)?.Progress ?? 0;
		return SetWingAckIfGreater(wingId, ackProgress);
	}

	public static bool SetWingAckIfGreater(int wingId, int ackProgress)
	{
		int ack = 0;
		AdventureProgressMgr.Get().GetWingAck(wingId, out ack);
		if (ackProgress > ack)
		{
			AdventureProgressMgr.Get().SetWingAck(wingId, ackProgress);
			return true;
		}
		return false;
	}

	public static bool ShouldDisplayAdventure(AdventureDbId adventureId)
	{
		if (GameUtils.IsAdventureRotated(adventureId) && !AdventureProgressMgr.Get().OwnsOneOrMoreAdventureWings(adventureId))
		{
			return false;
		}
		if (adventureId != AdventureDbId.PRACTICE && !AchieveManager.Get().HasUnlockedFeature(Achieve.Unlocks.VANILLA_HEROES) && !AdventureProgressMgr.Get().OwnsOneOrMoreAdventureWings(adventureId) && AdventureUtils.DoesAdventureRequireAllHeroesUnlocked(adventureId))
		{
			return false;
		}
		if (IsAdventureComingSoon(adventureId))
		{
			return true;
		}
		if (!IsAdventureEventActive(adventureId))
		{
			return false;
		}
		return true;
	}

	public static bool IsAdventureEventActive(AdventureDbId advId)
	{
		bool result = true;
		foreach (WingDbfRecord record in GameDbf.Wing.GetRecords())
		{
			if (record.AdventureId == (int)advId)
			{
				if (AdventureProgressMgr.IsWingEventActive(record.ID))
				{
					return true;
				}
				result = false;
			}
		}
		return result;
	}

	public static SpecialEventType GetEarliestWingEventTiming(AdventureDbId advId)
	{
		SpecialEventType specialEventType = SpecialEventType.SPECIAL_EVENT_NEVER;
		foreach (WingDbfRecord record in GameDbf.Wing.GetRecords())
		{
			if (record.AdventureId == (int)advId)
			{
				SpecialEventType wingEventTiming = AdventureProgressMgr.GetWingEventTiming(record.ID);
				if (specialEventType == SpecialEventType.SPECIAL_EVENT_NEVER || SpecialEventManager.Get().GetEventStartTimeUtc(wingEventTiming) < SpecialEventManager.Get().GetEventStartTimeUtc(specialEventType))
				{
					specialEventType = wingEventTiming;
				}
			}
		}
		return specialEventType;
	}

	public static bool IsAdventureComingSoon(AdventureDbId advId)
	{
		AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)advId);
		if (record == null)
		{
			Debug.LogErrorFormat("IsAdventureComingSoon - Adventure Id is invalid: {0}", (int)advId);
			return false;
		}
		return SpecialEventManager.Get().IsEventActive(record.ComingSoonEvent, activeIfDoesNotExist: false);
	}

	public static AdventureDbId GetAdventurePlayerShouldSee(out int latestActiveAdventureWing)
	{
		latestActiveAdventureWing = 0;
		if (!Options.Get().GetBool(Option.HAS_SEEN_PRACTICE_MODE, defaultVal: false))
		{
			return AdventureDbId.INVALID;
		}
		AdventureDbfRecord activeExpansionAdventureWithHighestSortOrder = GetActiveExpansionAdventureWithHighestSortOrder();
		if (activeExpansionAdventureWithHighestSortOrder == null)
		{
			return AdventureDbId.INVALID;
		}
		long num = AdventureUtils.GetFinalAdventureWing(activeExpansionAdventureWithHighestSortOrder.ID, excludeOwnedWings: false, excludeInactiveWings: true);
		latestActiveAdventureWing = (int)num;
		long value = 0L;
		if (!GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.PLAYER_OPTIONS, GameSaveKeySubkeyId.LATEST_ADVENTURE_WING_SEEN, out value))
		{
			value = 2522L;
		}
		if (num != value)
		{
			return (AdventureDbId)activeExpansionAdventureWithHighestSortOrder.ID;
		}
		return AdventureDbId.INVALID;
	}

	public static AdventureDbId GetAdventurePlayerShouldSee()
	{
		int latestActiveAdventureWing = 0;
		return GetAdventurePlayerShouldSee(out latestActiveAdventureWing);
	}

	public static AdventureDbfRecord GetActiveExpansionAdventureWithHighestSortOrder()
	{
		List<AdventureDbfRecord> adventureRecordsWithDefPrefab = GameUtils.GetAdventureRecordsWithDefPrefab();
		AdventureDbfRecord adventureDbfRecord = null;
		foreach (AdventureDbfRecord item in adventureRecordsWithDefPrefab)
		{
			if (GameUtils.IsExpansionAdventure((AdventureDbId)item.ID) && ShouldDisplayAdventure((AdventureDbId)item.ID) && !IsAdventureComingSoon((AdventureDbId)item.ID) && (adventureDbfRecord == null || item.SortOrder > adventureDbfRecord.SortOrder))
			{
				adventureDbfRecord = item;
			}
		}
		return adventureDbfRecord;
	}

	public static bool GetMissionPlayableParameters(int missionId, ref int wingId, ref int missionReqProgress)
	{
		ScenarioDbfRecord scenarioRecord = GameDbf.Scenario.GetRecord(missionId);
		if (scenarioRecord == null)
		{
			return false;
		}
		AdventureMissionDbfRecord record = GameDbf.AdventureMission.GetRecord((AdventureMissionDbfRecord r) => r.ScenarioId == scenarioRecord.ID);
		if (record == null)
		{
			return false;
		}
		WingDbfRecord record2 = GameDbf.Wing.GetRecord(record.ReqWingId);
		if (record2 == null)
		{
			return false;
		}
		missionReqProgress = record.ReqProgress;
		wingId = record2.ID;
		return true;
	}

	public int GetWingBossesDefeated(AdventureDbId advId, AdventureModeDbId mode, WingDbId wing, int defaultvalue = 0)
	{
		int value = 0;
		if (m_WingBossesDefeatedCache.TryGetValue(GetWingUniqueId(advId, mode, wing), out value))
		{
			return value;
		}
		return defaultvalue;
	}

	public void UpdateWingBossesDefeated(AdventureDbId advId, AdventureModeDbId mode, WingDbId wing, int bossesDefeated)
	{
		m_WingBossesDefeatedCache[GetWingUniqueId(advId, mode, wing)] = bossesDefeated;
	}

	private string GetWingUniqueId(AdventureDbId advId, AdventureModeDbId modeId, WingDbId wing)
	{
		return $"{advId}_{modeId}_{wing}";
	}

	private void Awake()
	{
		s_instance = this;
		base.gameObject.AddComponent<HSDontDestroyOnLoad>();
	}

	private void Start()
	{
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(OnSuccessfulPurchaseAck);
		AddSubSceneChangeListener(OnSubSceneChange);
	}

	private void OnDestroy()
	{
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(OnSuccessfulPurchaseAck);
		s_instance = null;
	}

	public void OnAdventureSceneAwake()
	{
		SelectedAdventure = Options.Get().GetEnum(Option.SELECTED_ADVENTURE, AdventureDbId.PRACTICE);
		SelectedMode = Options.Get().GetEnum(Option.SELECTED_ADVENTURE_MODE, AdventureModeDbId.LINEAR);
		if (!ShouldDisplayAdventure(SelectedAdventure))
		{
			SelectedAdventure = AdventureDbId.PRACTICE;
			SelectedMode = AdventureModeDbId.LINEAR;
		}
		SetPropertiesForAdventureAndMode();
	}

	public void OnAdventureSceneUnload()
	{
		if (this.OnAdventureSceneUnloadEvent != null)
		{
			this.OnAdventureSceneUnloadEvent();
		}
		SelectedAdventure = AdventureDbId.INVALID;
		SelectedMode = AdventureModeDbId.INVALID;
	}

	public void ResetSubScene()
	{
		ResetSubScene(AdventureData.Adventuresubscene.CHOOSER);
	}

	private void FireAdventureModeChangeEvent()
	{
		AdventureModeChange[] array = m_AdventureModeChangeEventList.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](SelectedAdventure, SelectedMode);
		}
	}

	private void FireSubSceneChangeEvent(bool forward)
	{
		UpdatePresence();
		SubSceneChange[] array = m_SubSceneChangeEventList.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](m_CurrentSubScene, forward);
		}
	}

	private void FireSelectedModeChangeEvent()
	{
		SelectedModeChange[] array = m_SelectedModeChangeEventList.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](SelectedAdventure, SelectedMode);
		}
	}

	public void UpdatePresence()
	{
		AdventureData.Adventuresubscene currentSubScene = m_CurrentSubScene;
		if ((uint)(currentSubScene - 2) <= 4u || currentSubScene == AdventureData.Adventuresubscene.LOCATION_SELECT)
		{
			PresenceMgr.Get().SetStatus_EnteringAdventure(SelectedAdventure, SelectedMode);
		}
		else if (AdventureScene.Get() != null && !AdventureScene.Get().IsUnloading())
		{
			PresenceMgr.Get().SetStatus(Global.PresenceStatus.ADVENTURE_CHOOSING_MODE);
		}
	}

	public bool IsHeroSelectedBeforeDungeonCrawlScreenForSelectedAdventure()
	{
		return GetSelectedAdventureDataRecord()?.DungeonCrawlPickHeroFirst ?? false;
	}

	public bool IsChapterSelectedBeforeDungeonCrawlScreenForSelectedAdventure()
	{
		return GetSelectedAdventureDataRecord()?.DungeonCrawlSelectChapter ?? false;
	}

	public bool ValidLoadoutIsLockedInForSelectedAdventure()
	{
		AdventureDataDbfRecord selectedAdventureDataRecord = GetSelectedAdventureDataRecord();
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)selectedAdventureDataRecord.GameSaveDataServerKey;
		if (!GameSaveDataManager.Get().ValidateIfKeyCanBeAccessed(gameSaveDataServerKey, selectedAdventureDataRecord.Name))
		{
			return false;
		}
		long value = 0L;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_SCENARIO_ID, out value);
		long value2 = 0L;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS, out value2);
		long value3 = 0L;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_POWER, out value3);
		long value4 = 0L;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_DECK, out value4);
		long value5 = 0L;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_LOADOUT_TREASURE_ID, out value5);
		return AdventureUtils.IsValidLoadoutForSelectedAdventure(SelectedAdventure, SelectedMode, (ScenarioDbId)value, (TAG_CLASS)value2, (int)value3, (int)value4, (int)value5);
	}

	public bool GuestHeroesExistForCurrentAdventure()
	{
		return GameDbf.AdventureGuestHeroes.HasRecord((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)GetSelectedAdventure());
	}

	public List<int> GetGuestHeroesForCurrentAdventure()
	{
		return AdventureUtils.GetGuestHeroesForAdventure(GetSelectedAdventure());
	}

	public static List<int> GetGuestHeroesForWing(int wingId)
	{
		List<AdventureGuestHeroesDbfRecord> records = GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.WingId == wingId);
		records.Sort((AdventureGuestHeroesDbfRecord a, AdventureGuestHeroesDbfRecord b) => a.SortOrder.CompareTo(b.SortOrder));
		List<int> list = new List<int>();
		foreach (AdventureGuestHeroesDbfRecord item in records)
		{
			list.Add(GameUtils.GetCardIdFromGuestHeroDbId(item.GuestHeroId));
		}
		return list;
	}

	public static int GetAdventureBossesInRun(WingDbfRecord wingRecord)
	{
		if (wingRecord == null)
		{
			Debug.LogError("GetAdventureBossesInRun - no WingDbfRecord passed in!");
			return 0;
		}
		return wingRecord.DungeonCrawlBosses;
	}

	public AdventureData.Adventuresubscene SubSceneForPickingHeroForCurrentAdventure()
	{
		if (!GuestHeroesExistForCurrentAdventure())
		{
			return AdventureData.Adventuresubscene.MISSION_DECK_PICKER;
		}
		return AdventureData.Adventuresubscene.ADVENTURER_PICKER;
	}

	public AdventureData.Adventuresubscene GetCorrectSubSceneWhenLoadingDungeonCrawlMode()
	{
		bool flag = DungeonCrawlUtil.IsDungeonRunInProgress(SelectedAdventure, SelectedMode) || ValidLoadoutIsLockedInForSelectedAdventure();
		if (!flag && IsChapterSelectedBeforeDungeonCrawlScreenForSelectedAdventure())
		{
			return AdventureData.Adventuresubscene.LOCATION_SELECT;
		}
		if (!flag && IsHeroSelectedBeforeDungeonCrawlScreenForSelectedAdventure())
		{
			return SubSceneForPickingHeroForCurrentAdventure();
		}
		return AdventureData.Adventuresubscene.DUNGEON_CRAWL;
	}

	private void OnSuccessfulPurchaseAck(Network.Bundle bundle, PaymentMethod purchaseMethod)
	{
		EvaluateIfAllWingsOwnedForSelectedAdventure();
	}

	private void OnSubSceneChange(AdventureData.Adventuresubscene subScene, bool forward)
	{
		bool num = GameUtils.DoesAdventureModeUseDungeonCrawlFormat(GetSelectedMode());
		bool flag = subScene == AdventureData.Adventuresubscene.MISSION_DECK_PICKER || subScene == AdventureData.Adventuresubscene.ADVENTURER_PICKER;
		if (num && flag)
		{
			WingDbId wingIdFromMissionId = GameUtils.GetWingIdFromMissionId(GetMission());
			DungeonCrawlSubDef_VOLines.PlayVOLine(GetSelectedAdventure(), wingIdFromMissionId, 0, DungeonCrawlSubDef_VOLines.VOEventType.CHARACTER_SELECT);
		}
	}

	private void SetPropertiesForAdventureAndMode()
	{
		EvaluateIfAllWingsOwnedForSelectedAdventure();
		UpdateCompletionRewards();
	}

	private void EvaluateIfAllWingsOwnedForSelectedAdventure()
	{
		if (SelectedAdventure != 0 && SelectedMode != 0)
		{
			AllChaptersOwned = AdventureProgressMgr.Get().OwnsAllAdventureWings(SelectedAdventure);
		}
	}

	private void UpdateCompletionRewards()
	{
		List<RewardData> rewardsForAdventureByMode = AdventureProgressMgr.GetRewardsForAdventureByMode((int)SelectedAdventure, (int)SelectedMode, new HashSet<Achieve.RewardTiming> { Achieve.RewardTiming.ADVENTURE_CHEST });
		Legacy_UpdateCompletionRewardData(rewardsForAdventureByMode);
		CompletionRewards.Items.Clear();
		foreach (RewardData item in rewardsForAdventureByMode)
		{
			RewardItemDataModel rewardItemDataModel = RewardUtils.RewardDataToRewardItemDataModel(item);
			if (rewardItemDataModel != null)
			{
				CompletionRewards.Items.Add(rewardItemDataModel);
			}
		}
	}

	private void Legacy_UpdateCompletionRewardData(List<RewardData> adventureCompletionRewards)
	{
		bool flag = false;
		foreach (RewardData adventureCompletionReward in adventureCompletionRewards)
		{
			if (adventureCompletionReward is CardBackRewardData)
			{
				flag = true;
				CardBackRewardData cardBackRewardData = adventureCompletionReward as CardBackRewardData;
				CompletionRewardType = Reward.Type.CARD_BACK;
				CompletionRewardId = cardBackRewardData.CardBackID;
			}
		}
		if (adventureCompletionRewards.Count < 1 || !flag)
		{
			CompletionRewardType = Reward.Type.NONE;
			CompletionRewardId = 0;
		}
	}

	public void ResetLoadout()
	{
		AnomalyModeActivated = false;
		SelectedHeroClass = TAG_CLASS.INVALID;
		SelectedLoadoutTreasureDbId = 0L;
		SelectedHeroPowerDbId = 0L;
		SelectedDeckId = 0L;
		SetMissionOverride(ScenarioDbId.INVALID);
	}

	public void SetHasSeenUnlockedChapterPage(WingDbId wingId, bool hasSeen)
	{
		if (hasSeen)
		{
			NeedsChapterNewlyUnlockedHighlight.Remove((long)wingId);
		}
		else if (GetHasSeenUnlockedChapterPage(wingId))
		{
			NeedsChapterNewlyUnlockedHighlight.Add((long)wingId);
		}
	}

	public bool GetHasSeenUnlockedChapterPage(WingDbId wingId)
	{
		return !NeedsChapterNewlyUnlockedHighlight.Contains((long)wingId);
	}

	public bool HasUnacknowledgedChapterUnlocks()
	{
		foreach (WingDbfRecord record in GameDbf.Wing.GetRecords((WingDbfRecord r) => r.AdventureId == (int)SelectedAdventure))
		{
			AdventureChapterState num = AdventureProgressMgr.Get().AdventureBookChapterStateForWing(record, SelectedMode);
			AdventureProgressMgr.Get().GetWingAck(record.ID, out var ack);
			if (num == AdventureChapterState.UNLOCKED && ack == 0)
			{
				return true;
			}
		}
		return false;
	}

	public bool HasValidLoadoutForSelectedAdventure()
	{
		return AdventureUtils.IsValidLoadoutForSelectedAdventure(SelectedAdventure, SelectedMode, m_SelectedMission, SelectedHeroClass, (int)SelectedHeroPowerDbId, (int)SelectedDeckId, (int)SelectedLoadoutTreasureDbId);
	}
}
