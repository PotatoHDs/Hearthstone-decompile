using System;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.DungeonCrawl;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000847 RID: 2119
[CustomEditClass]
public class AdventureConfig : MonoBehaviour
{
	// Token: 0x06007284 RID: 29316 RVA: 0x0024EBA1 File Offset: 0x0024CDA1
	public static AdventureConfig Get()
	{
		return AdventureConfig.s_instance;
	}

	// Token: 0x170006CB RID: 1739
	// (get) Token: 0x06007285 RID: 29317 RVA: 0x0024EBA8 File Offset: 0x0024CDA8
	// (set) Token: 0x06007286 RID: 29318 RVA: 0x0024EBB0 File Offset: 0x0024CDB0
	private AdventureDbId SelectedAdventure
	{
		get
		{
			return this.m_SelectedAdventure;
		}
		set
		{
			if (value != this.m_SelectedAdventure)
			{
				this.ResetLoadout();
			}
			this.m_SelectedAdventure = value;
			AdventureDataModel adventureDataModel = this.GetAdventureDataModel();
			if (adventureDataModel != null)
			{
				adventureDataModel.SelectedAdventure = value;
				AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)value);
				adventureDataModel.StoreDescriptionTextTimelockedTrue = ((record != null) ? record.StoreBuyRemainingWingsDescTimelockedTrue : string.Empty);
				adventureDataModel.StoreDescriptionTextTimelockedFalse = ((record != null) ? record.StoreBuyRemainingWingsDescTimelockedFalse : string.Empty);
			}
		}
	}

	// Token: 0x170006CC RID: 1740
	// (get) Token: 0x06007287 RID: 29319 RVA: 0x0024EC26 File Offset: 0x0024CE26
	// (set) Token: 0x06007288 RID: 29320 RVA: 0x0024EC30 File Offset: 0x0024CE30
	private AdventureModeDbId SelectedMode
	{
		get
		{
			return this.m_SelectedMode;
		}
		set
		{
			if (value != this.m_SelectedMode)
			{
				this.ResetLoadout();
			}
			this.m_SelectedMode = value;
			AdventureDataModel adventureDataModel = this.GetAdventureDataModel();
			if (adventureDataModel != null)
			{
				adventureDataModel.SelectedAdventureMode = value;
				adventureDataModel.IsSelectedModeHeroic = GameUtils.IsModeHeroic(value);
			}
		}
	}

	// Token: 0x170006CD RID: 1741
	// (get) Token: 0x06007289 RID: 29321 RVA: 0x0024EC70 File Offset: 0x0024CE70
	public AdventureData.Adventuresubscene CurrentSubScene
	{
		get
		{
			return this.m_CurrentSubScene;
		}
	}

	// Token: 0x170006CE RID: 1742
	// (get) Token: 0x0600728A RID: 29322 RVA: 0x0024EC78 File Offset: 0x0024CE78
	public AdventureData.Adventuresubscene PreviousSubScene
	{
		get
		{
			return this.m_PreviousSubScene;
		}
	}

	// Token: 0x14000076 RID: 118
	// (add) Token: 0x0600728B RID: 29323 RVA: 0x0024EC80 File Offset: 0x0024CE80
	// (remove) Token: 0x0600728C RID: 29324 RVA: 0x0024ECB8 File Offset: 0x0024CEB8
	public event AdventureConfig.AnomalyModeChangedHandler OnAnomalyModeChanged;

	// Token: 0x170006CF RID: 1743
	// (get) Token: 0x0600728D RID: 29325 RVA: 0x0024ECED File Offset: 0x0024CEED
	// (set) Token: 0x0600728E RID: 29326 RVA: 0x0024ECF8 File Offset: 0x0024CEF8
	public bool AnomalyModeActivated
	{
		get
		{
			return this.m_anomalyModeActivated;
		}
		set
		{
			if (value != this.m_anomalyModeActivated)
			{
				this.m_anomalyModeActivated = value;
				AdventureDataModel adventureDataModel = this.GetAdventureDataModel();
				if (adventureDataModel != null)
				{
					adventureDataModel.AnomalyActivated = this.m_anomalyModeActivated;
				}
				if (this.OnAnomalyModeChanged != null)
				{
					this.OnAnomalyModeChanged(value);
				}
			}
		}
	}

	// Token: 0x170006D0 RID: 1744
	// (get) Token: 0x0600728F RID: 29327 RVA: 0x0024ED3F File Offset: 0x0024CF3F
	// (set) Token: 0x06007290 RID: 29328 RVA: 0x0024ED47 File Offset: 0x0024CF47
	public TAG_CLASS SelectedHeroClass { get; set; }

	// Token: 0x170006D1 RID: 1745
	// (get) Token: 0x06007291 RID: 29329 RVA: 0x0024ED50 File Offset: 0x0024CF50
	// (set) Token: 0x06007292 RID: 29330 RVA: 0x0024ED58 File Offset: 0x0024CF58
	public long SelectedLoadoutTreasureDbId { get; set; }

	// Token: 0x170006D2 RID: 1746
	// (get) Token: 0x06007293 RID: 29331 RVA: 0x0024ED61 File Offset: 0x0024CF61
	// (set) Token: 0x06007294 RID: 29332 RVA: 0x0024ED69 File Offset: 0x0024CF69
	public long SelectedDeckId { get; set; }

	// Token: 0x170006D3 RID: 1747
	// (get) Token: 0x06007295 RID: 29333 RVA: 0x0024ED72 File Offset: 0x0024CF72
	// (set) Token: 0x06007296 RID: 29334 RVA: 0x0024ED7A File Offset: 0x0024CF7A
	public long SelectedHeroPowerDbId { get; set; }

	// Token: 0x170006D4 RID: 1748
	// (get) Token: 0x06007297 RID: 29335 RVA: 0x0024ED83 File Offset: 0x0024CF83
	// (set) Token: 0x06007298 RID: 29336 RVA: 0x0024ED90 File Offset: 0x0024CF90
	public bool ShouldSeeFirstTimeFlow
	{
		get
		{
			return this.GetAdventureDataModel().ShouldSeeFirstTimeFlow;
		}
		set
		{
			this.GetAdventureDataModel().ShouldSeeFirstTimeFlow = value;
		}
	}

	// Token: 0x170006D5 RID: 1749
	// (get) Token: 0x06007299 RID: 29337 RVA: 0x0024ED9E File Offset: 0x0024CF9E
	// (set) Token: 0x0600729A RID: 29338 RVA: 0x0024EDA8 File Offset: 0x0024CFA8
	public bool AllChaptersOwned
	{
		get
		{
			return this.m_allChaptersOwned;
		}
		set
		{
			this.m_allChaptersOwned = value;
			AdventureDataModel adventureDataModel = this.GetAdventureDataModel();
			if (adventureDataModel != null)
			{
				adventureDataModel.AllChaptersOwned = this.m_allChaptersOwned;
			}
		}
	}

	// Token: 0x170006D6 RID: 1750
	// (get) Token: 0x0600729B RID: 29339 RVA: 0x0024EDD4 File Offset: 0x0024CFD4
	// (set) Token: 0x0600729C RID: 29340 RVA: 0x0024EE08 File Offset: 0x0024D008
	public RewardListDataModel CompletionRewards
	{
		get
		{
			AdventureDataModel adventureDataModel = this.GetAdventureDataModel();
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
			AdventureDataModel adventureDataModel = this.GetAdventureDataModel();
			if (adventureDataModel != null)
			{
				adventureDataModel.CompletionRewards = value;
			}
		}
	}

	// Token: 0x170006D7 RID: 1751
	// (get) Token: 0x0600729D RID: 29341 RVA: 0x0024EE26 File Offset: 0x0024D026
	// (set) Token: 0x0600729E RID: 29342 RVA: 0x0024EE30 File Offset: 0x0024D030
	public Reward.Type CompletionRewardType
	{
		get
		{
			return this.m_completionRewardType;
		}
		set
		{
			this.m_completionRewardType = value;
			AdventureDataModel adventureDataModel = this.GetAdventureDataModel();
			if (adventureDataModel != null)
			{
				adventureDataModel.CompletionRewardType = this.m_completionRewardType;
			}
		}
	}

	// Token: 0x170006D8 RID: 1752
	// (get) Token: 0x0600729F RID: 29343 RVA: 0x0024EE5A File Offset: 0x0024D05A
	// (set) Token: 0x060072A0 RID: 29344 RVA: 0x0024EE64 File Offset: 0x0024D064
	public int CompletionRewardId
	{
		get
		{
			return this.m_completionRewardId;
		}
		set
		{
			this.m_completionRewardId = value;
			AdventureDataModel adventureDataModel = this.GetAdventureDataModel();
			if (adventureDataModel != null)
			{
				adventureDataModel.CompletionRewardId = this.m_completionRewardId;
			}
		}
	}

	// Token: 0x14000077 RID: 119
	// (add) Token: 0x060072A1 RID: 29345 RVA: 0x0024EE90 File Offset: 0x0024D090
	// (remove) Token: 0x060072A2 RID: 29346 RVA: 0x0024EEC8 File Offset: 0x0024D0C8
	public event Action OnAdventureSceneUnloadEvent;

	// Token: 0x060072A3 RID: 29347 RVA: 0x0024EF00 File Offset: 0x0024D100
	public static AdventureData.Adventuresubscene GetSubSceneFromMode(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)modeId);
		if (adventureDataRecord == null)
		{
			Debug.LogErrorFormat("AdventureConfig.GetSubSceneFromMode() - No Adventure Data record found for Adventure {0} and Mode {1}", new object[]
			{
				(int)adventureId,
				(int)modeId
			});
			return AdventureData.Adventuresubscene.CHOOSER;
		}
		if (adventureDataRecord.StartingSubscene == AdventureData.Adventuresubscene.DUNGEON_CRAWL)
		{
			return AdventureConfig.Get().GetCorrectSubSceneWhenLoadingDungeonCrawlMode();
		}
		return adventureDataRecord.StartingSubscene;
	}

	// Token: 0x060072A4 RID: 29348 RVA: 0x0024EF5A File Offset: 0x0024D15A
	public AdventureDbId GetSelectedAdventure()
	{
		return this.SelectedAdventure;
	}

	// Token: 0x060072A5 RID: 29349 RVA: 0x0024EF62 File Offset: 0x0024D162
	public AdventureModeDbId GetSelectedMode()
	{
		return this.SelectedMode;
	}

	// Token: 0x060072A6 RID: 29350 RVA: 0x0024EF6C File Offset: 0x0024D16C
	public AdventureDataModel GetAdventureDataModel()
	{
		IDataModel dataModel;
		if (!GlobalDataContext.Get().GetDataModel(7, out dataModel))
		{
			dataModel = new AdventureDataModel();
			GlobalDataContext.Get().BindDataModel(dataModel);
		}
		AdventureDataModel adventureDataModel = dataModel as AdventureDataModel;
		if (adventureDataModel == null)
		{
			Log.Adventures.PrintWarning("AdventureDataModel is null!", Array.Empty<object>());
		}
		return adventureDataModel;
	}

	// Token: 0x060072A7 RID: 29351 RVA: 0x0024EFB6 File Offset: 0x0024D1B6
	public AdventureDataDbfRecord GetSelectedAdventureDataRecord()
	{
		return AdventureConfig.GetAdventureDataRecord(this.GetSelectedAdventure(), this.GetSelectedMode());
	}

	// Token: 0x060072A8 RID: 29352 RVA: 0x0024EFCC File Offset: 0x0024D1CC
	public AdventureModeDbId GetClientChooserAdventureMode(AdventureDbId adventureDbId)
	{
		AdventureModeDbId result;
		if (this.m_ClientChooserAdventureModes.TryGetValue(adventureDbId, out result))
		{
			return result;
		}
		if (this.SelectedAdventure != adventureDbId)
		{
			return AdventureModeDbId.LINEAR;
		}
		return this.SelectedMode;
	}

	// Token: 0x060072A9 RID: 29353 RVA: 0x0024EFFC File Offset: 0x0024D1FC
	public static AdventureDataDbfRecord GetAdventureDataRecord(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		return GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == (int)adventureId && r.ModeId == (int)modeId);
	}

	// Token: 0x060072AA RID: 29354 RVA: 0x0024F034 File Offset: 0x0024D234
	public static bool CanPlayMode(AdventureDbId adventureId, AdventureModeDbId modeId, bool checkEventTimings = true)
	{
		bool flag = AchieveManager.Get().HasUnlockedFeature(Achieve.Unlocks.VANILLA_HEROES);
		if (adventureId == AdventureDbId.PRACTICE)
		{
			return modeId != AdventureModeDbId.EXPERT || flag;
		}
		return (flag || !AdventureUtils.DoesAdventureRequireAllHeroesUnlocked(adventureId, modeId)) && (modeId == AdventureModeDbId.LINEAR || modeId == AdventureModeDbId.DUNGEON_CRAWL || GameDbf.Scenario.GetRecord((ScenarioDbfRecord r) => r.AdventureId == (int)adventureId && r.ModeId == (int)modeId && r.WingId > 0 && AdventureProgressMgr.Get().CanPlayScenario(r.ID, checkEventTimings)) != null);
	}

	// Token: 0x060072AB RID: 29355 RVA: 0x0024F0C4 File Offset: 0x0024D2C4
	public static bool IsFeaturedMode(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		if (!AdventureConfig.CanPlayMode(adventureId, modeId, true))
		{
			return false;
		}
		Option hasSeenFeaturedModeOptionFromAdventureData = AdventureConfig.GetHasSeenFeaturedModeOptionFromAdventureData(adventureId, modeId);
		return hasSeenFeaturedModeOptionFromAdventureData != Option.INVALID && !Options.Get().GetBool(hasSeenFeaturedModeOptionFromAdventureData, false);
	}

	// Token: 0x060072AC RID: 29356 RVA: 0x0024F0FC File Offset: 0x0024D2FC
	public static bool MarkFeaturedMode(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		if (!AdventureConfig.CanPlayMode(adventureId, modeId, true))
		{
			return false;
		}
		Option hasSeenFeaturedModeOptionFromAdventureData = AdventureConfig.GetHasSeenFeaturedModeOptionFromAdventureData(adventureId, modeId);
		if (hasSeenFeaturedModeOptionFromAdventureData == Option.INVALID)
		{
			return false;
		}
		Options.Get().SetBool(hasSeenFeaturedModeOptionFromAdventureData, true);
		return true;
	}

	// Token: 0x060072AD RID: 29357 RVA: 0x0024F130 File Offset: 0x0024D330
	public static bool ShouldShowNewModePopup(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		if (!AdventureConfig.CanPlayMode(adventureId, modeId, true))
		{
			return false;
		}
		Option hasSeenNewModePopupOptionFromAdventureData = AdventureConfig.GetHasSeenNewModePopupOptionFromAdventureData(adventureId, modeId);
		return hasSeenNewModePopupOptionFromAdventureData != Option.INVALID && !Options.Get().GetBool(hasSeenNewModePopupOptionFromAdventureData, false);
	}

	// Token: 0x060072AE RID: 29358 RVA: 0x0024F168 File Offset: 0x0024D368
	public static bool MarkHasSeenNewModePopup(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		if (!AdventureConfig.CanPlayMode(adventureId, modeId, true))
		{
			return false;
		}
		Option hasSeenNewModePopupOptionFromAdventureData = AdventureConfig.GetHasSeenNewModePopupOptionFromAdventureData(adventureId, modeId);
		if (hasSeenNewModePopupOptionFromAdventureData == Option.INVALID)
		{
			return false;
		}
		Options.Get().SetBool(hasSeenNewModePopupOptionFromAdventureData, true);
		return true;
	}

	// Token: 0x060072AF RID: 29359 RVA: 0x0024F19C File Offset: 0x0024D39C
	private static Option GetHasSeenFeaturedModeOptionFromAdventureData(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)modeId);
		if (adventureDataRecord == null)
		{
			return Option.INVALID;
		}
		return OptionUtils.GetOptionFromString(adventureDataRecord.HasSeenFeaturedModeOption);
	}

	// Token: 0x060072B0 RID: 29360 RVA: 0x0024F1C4 File Offset: 0x0024D3C4
	private static Option GetHasSeenNewModePopupOptionFromAdventureData(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)modeId);
		if (adventureDataRecord == null)
		{
			return Option.INVALID;
		}
		return OptionUtils.GetOptionFromString(adventureDataRecord.HasSeenNewModePopupOption);
	}

	// Token: 0x060072B1 RID: 29361 RVA: 0x0024F1E9 File Offset: 0x0024D3E9
	public string GetSelectedAdventureAndModeString()
	{
		return string.Format("{0}_{1}", this.SelectedAdventure, this.SelectedMode);
	}

	// Token: 0x060072B2 RID: 29362 RVA: 0x0024F20C File Offset: 0x0024D40C
	public void SetSelectedAdventureMode(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		this.SelectedAdventure = adventureId;
		this.SelectedMode = modeId;
		this.m_ClientChooserAdventureModes[adventureId] = modeId;
		Options.Get().SetInt(Option.SELECTED_ADVENTURE, (int)this.SelectedAdventure);
		Options.Get().SetInt(Option.SELECTED_ADVENTURE_MODE, (int)this.SelectedMode);
		this.SetPropertiesForAdventureAndMode();
		this.FireSelectedModeChangeEvent();
	}

	// Token: 0x060072B3 RID: 29363 RVA: 0x0024F264 File Offset: 0x0024D464
	public void MarkHasSeenFirstTimeFlowComplete()
	{
		if (!GameUtils.IsModeHeroic(this.SelectedMode))
		{
			AdventureDataDbfRecord record = GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == (int)this.SelectedAdventure && r.ModeId == (int)this.SelectedMode);
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest((GameSaveKeyId)record.GameSaveDataClientKey, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_FIRST_TIME_FLOW, new long[]
			{
				1L
			}), null);
			this.ShouldSeeFirstTimeFlow = false;
		}
	}

	// Token: 0x060072B4 RID: 29364 RVA: 0x0024F2C4 File Offset: 0x0024D4C4
	public void UpdateShouldSeeFirstTimeFlowForSelectedMode()
	{
		if (GameUtils.IsModeHeroic(this.SelectedMode))
		{
			this.ShouldSeeFirstTimeFlow = false;
			return;
		}
		long num = 0L;
		AdventureDataDbfRecord record = GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == (int)this.SelectedAdventure && r.ModeId == (int)this.SelectedMode);
		if (record == null)
		{
			this.ShouldSeeFirstTimeFlow = true;
			return;
		}
		if (record.GameSaveDataClientKey <= 0)
		{
			this.ShouldSeeFirstTimeFlow = false;
			return;
		}
		GameSaveDataManager.Get().GetSubkeyValue((GameSaveKeyId)record.GameSaveDataClientKey, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_FIRST_TIME_FLOW, out num);
		this.ShouldSeeFirstTimeFlow = (num <= 0L);
	}

	// Token: 0x060072B5 RID: 29365 RVA: 0x0024F344 File Offset: 0x0024D544
	public static AdventureModeDbId GetDefaultModeDbIdForAdventure(AdventureDbId adventureId)
	{
		if (adventureId == AdventureDbId.INVALID)
		{
			return AdventureModeDbId.INVALID;
		}
		AdventureDataDbfRecord record = GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == (int)adventureId);
		if (record == null)
		{
			return AdventureModeDbId.INVALID;
		}
		return (AdventureModeDbId)record.ModeId;
	}

	// Token: 0x060072B6 RID: 29366 RVA: 0x0024F38A File Offset: 0x0024D58A
	public ScenarioDbId GetMission()
	{
		return this.m_SelectedMission;
	}

	// Token: 0x060072B7 RID: 29367 RVA: 0x0024F392 File Offset: 0x0024D592
	public ScenarioDbId GetMissionToPlay()
	{
		if (this.m_MissionOverride == ScenarioDbId.INVALID)
		{
			return this.GetMission();
		}
		return this.m_MissionOverride;
	}

	// Token: 0x060072B8 RID: 29368 RVA: 0x0024F3AC File Offset: 0x0024D5AC
	public ScenarioDbId GetLastSelectedMission()
	{
		string selectedAdventureAndModeString = this.GetSelectedAdventureAndModeString();
		ScenarioDbId result = ScenarioDbId.INVALID;
		this.m_LastSelectedMissions.TryGetValue(selectedAdventureAndModeString, out result);
		return result;
	}

	// Token: 0x060072B9 RID: 29369 RVA: 0x0024F3D4 File Offset: 0x0024D5D4
	public bool IsScenarioDefeatedAndInitCache(ScenarioDbId mission)
	{
		bool flag = AdventureProgressMgr.Get().HasDefeatedScenario((int)mission);
		if (!this.m_CachedDefeatedScenario.ContainsKey(mission))
		{
			this.m_CachedDefeatedScenario[mission] = flag;
		}
		return flag;
	}

	// Token: 0x060072BA RID: 29370 RVA: 0x0024F40C File Offset: 0x0024D60C
	public bool IsScenarioJustDefeated(ScenarioDbId mission)
	{
		bool flag = AdventureProgressMgr.Get().HasDefeatedScenario((int)mission);
		bool flag2 = false;
		this.m_CachedDefeatedScenario.TryGetValue(mission, out flag2);
		this.m_CachedDefeatedScenario[mission] = flag;
		return flag != flag2;
	}

	// Token: 0x060072BB RID: 29371 RVA: 0x0024F44C File Offset: 0x0024D64C
	public AdventureBossDef GetBossDef(ScenarioDbId mission)
	{
		AdventureBossDef result = null;
		if (!this.m_CachedBossDef.TryGetValue(mission, out result) && !string.IsNullOrEmpty(AdventureConfig.GetBossDefAssetPath(mission)))
		{
			Debug.LogErrorFormat("Boss def for mission not loaded: {0}\nCall LoadBossDef first.", new object[]
			{
				mission
			});
		}
		return result;
	}

	// Token: 0x060072BC RID: 29372 RVA: 0x0024F494 File Offset: 0x0024D694
	public void LoadBossDef(ScenarioDbId mission, AdventureConfig.DelBossDefLoaded callback)
	{
		AdventureBossDef bossDef = null;
		if (this.m_CachedBossDef.TryGetValue(mission, out bossDef))
		{
			callback(bossDef, true);
			return;
		}
		string bossDefAssetPath = AdventureConfig.GetBossDefAssetPath(mission);
		if (string.IsNullOrEmpty(bossDefAssetPath))
		{
			if (callback != null)
			{
				callback(null, false);
			}
			return;
		}
		AssetLoader.Get().InstantiatePrefab(bossDefAssetPath, delegate(AssetReference path, GameObject go, object data)
		{
			if (go == null)
			{
				Debug.LogError(string.Format("Unable to instantiate boss def: {0}", path));
				AdventureConfig.DelBossDefLoaded callback2 = callback;
				if (callback2 == null)
				{
					return;
				}
				callback2(null, false);
				return;
			}
			else
			{
				AdventureBossDef component = go.GetComponent<AdventureBossDef>();
				if (component == null)
				{
					Debug.LogError(string.Format("Object does not contain AdventureBossDef component: {0}", path));
				}
				else
				{
					this.m_CachedBossDef[mission] = component;
				}
				AdventureConfig.DelBossDefLoaded callback3 = callback;
				if (callback3 == null)
				{
					return;
				}
				callback3(component, component != null);
				return;
			}
		}, null, AssetLoadingOptions.None);
	}

	// Token: 0x060072BD RID: 29373 RVA: 0x0024F52C File Offset: 0x0024D72C
	public static string GetBossDefAssetPath(ScenarioDbId mission)
	{
		AdventureMissionDbfRecord record = GameDbf.AdventureMission.GetRecord((AdventureMissionDbfRecord r) => r.ScenarioId == (int)mission);
		if (record == null)
		{
			return null;
		}
		return record.BossDefAssetPath;
	}

	// Token: 0x060072BE RID: 29374 RVA: 0x0024F568 File Offset: 0x0024D768
	public void ClearBossDefs()
	{
		foreach (KeyValuePair<ScenarioDbId, AdventureBossDef> keyValuePair in this.m_CachedBossDef)
		{
			UnityEngine.Object.Destroy(keyValuePair.Value);
		}
		this.m_CachedBossDef.Clear();
	}

	// Token: 0x060072BF RID: 29375 RVA: 0x0024F5CC File Offset: 0x0024D7CC
	public void SetMission(ScenarioDbId mission, bool showDetails = true)
	{
		this.m_SelectedMission = mission;
		Log.Adventures.Print("Selected Mission set to {0}", new object[]
		{
			mission
		});
		string selectedAdventureAndModeString = this.GetSelectedAdventureAndModeString();
		this.m_LastSelectedMissions[selectedAdventureAndModeString] = mission;
		AdventureConfig.AdventureMissionSet[] array = this.m_AdventureMissionSetEventList.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](mission, showDetails);
		}
	}

	// Token: 0x060072C0 RID: 29376 RVA: 0x0024F636 File Offset: 0x0024D836
	public void SetMissionOverride(ScenarioDbId missionOverride)
	{
		this.m_MissionOverride = missionOverride;
	}

	// Token: 0x060072C1 RID: 29377 RVA: 0x0024F63F File Offset: 0x0024D83F
	public ScenarioDbId GetMissionOverride()
	{
		return this.m_MissionOverride;
	}

	// Token: 0x060072C2 RID: 29378 RVA: 0x0024F647 File Offset: 0x0024D847
	public bool DoesSelectedMissionRequireDeck()
	{
		return AdventureConfig.DoesMissionRequireDeck(this.m_SelectedMission);
	}

	// Token: 0x060072C3 RID: 29379 RVA: 0x0024F654 File Offset: 0x0024D854
	public static bool DoesMissionRequireDeck(ScenarioDbId scenario)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord((int)scenario);
		return record == null || record.Player1DeckId == 0;
	}

	// Token: 0x060072C4 RID: 29380 RVA: 0x0024F67B File Offset: 0x0024D87B
	public void AddAdventureMissionSetListener(AdventureConfig.AdventureMissionSet dlg)
	{
		this.m_AdventureMissionSetEventList.Add(dlg);
	}

	// Token: 0x060072C5 RID: 29381 RVA: 0x0024F689 File Offset: 0x0024D889
	public void RemoveAdventureMissionSetListener(AdventureConfig.AdventureMissionSet dlg)
	{
		this.m_AdventureMissionSetEventList.Remove(dlg);
	}

	// Token: 0x060072C6 RID: 29382 RVA: 0x0024F698 File Offset: 0x0024D898
	public void AddAdventureModeChangeListener(AdventureConfig.AdventureModeChange dlg)
	{
		this.m_AdventureModeChangeEventList.Add(dlg);
	}

	// Token: 0x060072C7 RID: 29383 RVA: 0x0024F6A6 File Offset: 0x0024D8A6
	public void RemoveAdventureModeChangeListener(AdventureConfig.AdventureModeChange dlg)
	{
		this.m_AdventureModeChangeEventList.Remove(dlg);
	}

	// Token: 0x060072C8 RID: 29384 RVA: 0x0024F6B5 File Offset: 0x0024D8B5
	public void AddSubSceneChangeListener(AdventureConfig.SubSceneChange dlg)
	{
		this.m_SubSceneChangeEventList.Add(dlg);
	}

	// Token: 0x060072C9 RID: 29385 RVA: 0x0024F6C3 File Offset: 0x0024D8C3
	public void RemoveSubSceneChangeListener(AdventureConfig.SubSceneChange dlg)
	{
		this.m_SubSceneChangeEventList.Remove(dlg);
	}

	// Token: 0x060072CA RID: 29386 RVA: 0x0024F6D2 File Offset: 0x0024D8D2
	public void AddSelectedModeChangeListener(AdventureConfig.SelectedModeChange dlg)
	{
		this.m_SelectedModeChangeEventList.Add(dlg);
	}

	// Token: 0x060072CB RID: 29387 RVA: 0x0024F6E0 File Offset: 0x0024D8E0
	public void RemoveSelectedModeChangeListener(AdventureConfig.SelectedModeChange dlg)
	{
		this.m_SelectedModeChangeEventList.Remove(dlg);
	}

	// Token: 0x060072CC RID: 29388 RVA: 0x0024F6EF File Offset: 0x0024D8EF
	public void ResetSubScene(AdventureData.Adventuresubscene subscene)
	{
		this.m_CurrentSubScene = subscene;
		this.m_PreviousSubScene = AdventureData.Adventuresubscene.INVALID;
		this.m_SubSceneBackStack.Clear();
	}

	// Token: 0x060072CD RID: 29389 RVA: 0x0024F70C File Offset: 0x0024D90C
	public void ChangeSubScene(AdventureData.Adventuresubscene subscene, bool pushToBackStack = true)
	{
		if (subscene == this.m_CurrentSubScene)
		{
			Debug.Log(string.Format("Sub scene {0} is already set.", subscene));
			return;
		}
		if (pushToBackStack)
		{
			this.m_SubSceneBackStack.Push(this.m_CurrentSubScene);
		}
		this.m_PreviousSubScene = this.m_CurrentSubScene;
		this.m_CurrentSubScene = subscene;
		this.FireSubSceneChangeEvent(true);
		this.FireAdventureModeChangeEvent();
	}

	// Token: 0x060072CE RID: 29390 RVA: 0x0024F76C File Offset: 0x0024D96C
	public void SubSceneGoBack(bool fireevent = true)
	{
		if (this.m_SubSceneBackStack.Count == 0)
		{
			Debug.Log("No sub scenes exist in the back stack.");
			return;
		}
		this.m_PreviousSubScene = this.m_CurrentSubScene;
		this.m_CurrentSubScene = this.m_SubSceneBackStack.Pop();
		if (fireevent)
		{
			this.FireSubSceneChangeEvent(false);
		}
		this.FireAdventureModeChangeEvent();
	}

	// Token: 0x060072CF RID: 29391 RVA: 0x0024F7BE File Offset: 0x0024D9BE
	public void RemoveSubScenesFromStackUntilTargetReached(AdventureData.Adventuresubscene targetSubscene)
	{
		while (this.m_SubSceneBackStack.Count > 0)
		{
			if (this.m_SubSceneBackStack.Peek() == targetSubscene)
			{
				return;
			}
			this.m_SubSceneBackStack.Pop();
		}
	}

	// Token: 0x060072D0 RID: 29392 RVA: 0x0024F7EB File Offset: 0x0024D9EB
	public void RemoveSubSceneIfOnTopOfStack(AdventureData.Adventuresubscene subscene)
	{
		if (this.m_SubSceneBackStack.Peek() == subscene)
		{
			this.m_SubSceneBackStack.Pop();
		}
	}

	// Token: 0x060072D1 RID: 29393 RVA: 0x0024F807 File Offset: 0x0024DA07
	public void ChangeSubSceneToSelectedAdventure()
	{
		this.RequestGameSaveDataKeysForSelectedAdventure(delegate(bool success)
		{
			if (success)
			{
				if (GameUtils.DoesAdventureModeUseDungeonCrawlFormat(this.GetSelectedMode()))
				{
					AdventureDataDbfRecord selectedAdventureDataRecord = this.GetSelectedAdventureDataRecord();
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
			AdventureData.Adventuresubscene subSceneFromMode = AdventureConfig.GetSubSceneFromMode(this.SelectedAdventure, this.SelectedMode);
			this.UpdateShouldSeeFirstTimeFlowForSelectedMode();
			if (this.ShouldSeeFirstTimeFlow && this.AllChaptersOwned && !AdventureUtils.IsEntireAdventureFree(this.SelectedAdventure))
			{
				this.MarkHasSeenFirstTimeFlowComplete();
			}
			this.ChangeSubScene(subSceneFromMode, true);
		});
	}

	// Token: 0x060072D2 RID: 29394 RVA: 0x0024F81C File Offset: 0x0024DA1C
	public void RequestGameSaveDataKeysForSelectedAdventure(GameSaveDataManager.OnRequestDataResponseDelegate onCompleteCallback)
	{
		AdventureDataDbfRecord selectedAdventureDataRecord = this.GetSelectedAdventureDataRecord();
		List<GameSaveKeyId> list = new List<GameSaveKeyId>();
		if (selectedAdventureDataRecord != null && selectedAdventureDataRecord.GameSaveDataClientKey != 0)
		{
			list.Add((GameSaveKeyId)selectedAdventureDataRecord.GameSaveDataClientKey);
		}
		if (selectedAdventureDataRecord != null && selectedAdventureDataRecord.GameSaveDataServerKey != 0)
		{
			list.Add((GameSaveKeyId)selectedAdventureDataRecord.GameSaveDataServerKey);
		}
		if (GameUtils.IsModeHeroic(this.GetSelectedMode()))
		{
			AdventureDataDbfRecord adventureDataRecord = AdventureConfig.GetAdventureDataRecord(this.GetSelectedAdventure(), GameUtils.GetNormalModeFromHeroicMode(this.GetSelectedMode()));
			if (adventureDataRecord != null && adventureDataRecord.GameSaveDataClientKey != 0)
			{
				list.Add((GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey);
			}
		}
		if (list.Count > 0)
		{
			GameSaveDataManager.Get().Request(list, onCompleteCallback);
			return;
		}
		onCompleteCallback(true);
	}

	// Token: 0x060072D3 RID: 29395 RVA: 0x0024F8BC File Offset: 0x0024DABC
	public static bool IsMissionAvailable(int missionId)
	{
		bool flag = AdventureProgressMgr.Get().CanPlayScenario(missionId, true);
		if (!flag)
		{
			return false;
		}
		int num = 0;
		int wing = 0;
		if (!AdventureConfig.GetMissionPlayableParameters(missionId, ref wing, ref num))
		{
			return false;
		}
		int num2 = 0;
		AdventureProgressMgr.Get().GetWingAck(wing, out num2);
		return flag && num <= num2;
	}

	// Token: 0x060072D4 RID: 29396 RVA: 0x0024F90C File Offset: 0x0024DB0C
	public static bool IsMissionNewlyAvailableAndGetReqs(int missionId, ref int wingId, ref int missionReqProgress)
	{
		if (!AdventureConfig.GetMissionPlayableParameters(missionId, ref wingId, ref missionReqProgress))
		{
			return false;
		}
		bool flag = AdventureProgressMgr.Get().CanPlayScenario(missionId, true);
		int num = 0;
		AdventureProgressMgr.Get().GetWingAck(wingId, out num);
		return num < missionReqProgress && flag;
	}

	// Token: 0x060072D5 RID: 29397 RVA: 0x0024F950 File Offset: 0x0024DB50
	public static bool AckCurrentWingProgress(int wingId)
	{
		AdventureMission.WingProgress progress = AdventureProgressMgr.Get().GetProgress(wingId);
		int ackProgress = (progress != null) ? progress.Progress : 0;
		return AdventureConfig.SetWingAckIfGreater(wingId, ackProgress);
	}

	// Token: 0x060072D6 RID: 29398 RVA: 0x0024F980 File Offset: 0x0024DB80
	public static bool SetWingAckIfGreater(int wingId, int ackProgress)
	{
		int num = 0;
		AdventureProgressMgr.Get().GetWingAck(wingId, out num);
		if (ackProgress > num)
		{
			AdventureProgressMgr.Get().SetWingAck(wingId, ackProgress);
			return true;
		}
		return false;
	}

	// Token: 0x060072D7 RID: 29399 RVA: 0x0024F9B4 File Offset: 0x0024DBB4
	public static bool ShouldDisplayAdventure(AdventureDbId adventureId)
	{
		return (!GameUtils.IsAdventureRotated(adventureId) || AdventureProgressMgr.Get().OwnsOneOrMoreAdventureWings(adventureId)) && (adventureId == AdventureDbId.PRACTICE || AchieveManager.Get().HasUnlockedFeature(Achieve.Unlocks.VANILLA_HEROES) || AdventureProgressMgr.Get().OwnsOneOrMoreAdventureWings(adventureId) || !AdventureUtils.DoesAdventureRequireAllHeroesUnlocked(adventureId)) && (AdventureConfig.IsAdventureComingSoon(adventureId) || AdventureConfig.IsAdventureEventActive(adventureId));
	}

	// Token: 0x060072D8 RID: 29400 RVA: 0x0024FA18 File Offset: 0x0024DC18
	public static bool IsAdventureEventActive(AdventureDbId advId)
	{
		bool result = true;
		foreach (WingDbfRecord wingDbfRecord in GameDbf.Wing.GetRecords())
		{
			if (wingDbfRecord.AdventureId == (int)advId)
			{
				if (AdventureProgressMgr.IsWingEventActive(wingDbfRecord.ID))
				{
					return true;
				}
				result = false;
			}
		}
		return result;
	}

	// Token: 0x060072D9 RID: 29401 RVA: 0x0024FA8C File Offset: 0x0024DC8C
	public static SpecialEventType GetEarliestWingEventTiming(AdventureDbId advId)
	{
		SpecialEventType specialEventType = SpecialEventType.SPECIAL_EVENT_NEVER;
		foreach (WingDbfRecord wingDbfRecord in GameDbf.Wing.GetRecords())
		{
			if (wingDbfRecord.AdventureId == (int)advId)
			{
				SpecialEventType wingEventTiming = AdventureProgressMgr.GetWingEventTiming(wingDbfRecord.ID);
				if (specialEventType == SpecialEventType.SPECIAL_EVENT_NEVER || SpecialEventManager.Get().GetEventStartTimeUtc(wingEventTiming) < SpecialEventManager.Get().GetEventStartTimeUtc(specialEventType))
				{
					specialEventType = wingEventTiming;
				}
			}
		}
		return specialEventType;
	}

	// Token: 0x060072DA RID: 29402 RVA: 0x0024FB48 File Offset: 0x0024DD48
	public static bool IsAdventureComingSoon(AdventureDbId advId)
	{
		AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)advId);
		if (record == null)
		{
			Debug.LogErrorFormat("IsAdventureComingSoon - Adventure Id is invalid: {0}", new object[]
			{
				(int)advId
			});
			return false;
		}
		return SpecialEventManager.Get().IsEventActive(record.ComingSoonEvent, false);
	}

	// Token: 0x060072DB RID: 29403 RVA: 0x0024FB90 File Offset: 0x0024DD90
	public static AdventureDbId GetAdventurePlayerShouldSee(out int latestActiveAdventureWing)
	{
		latestActiveAdventureWing = 0;
		if (!Options.Get().GetBool(Option.HAS_SEEN_PRACTICE_MODE, false))
		{
			return AdventureDbId.INVALID;
		}
		AdventureDbfRecord activeExpansionAdventureWithHighestSortOrder = AdventureConfig.GetActiveExpansionAdventureWithHighestSortOrder();
		if (activeExpansionAdventureWithHighestSortOrder == null)
		{
			return AdventureDbId.INVALID;
		}
		long num = (long)AdventureUtils.GetFinalAdventureWing(activeExpansionAdventureWithHighestSortOrder.ID, false, true);
		latestActiveAdventureWing = (int)num;
		long num2 = 0L;
		if (!GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.PLAYER_OPTIONS, GameSaveKeySubkeyId.LATEST_ADVENTURE_WING_SEEN, out num2))
		{
			num2 = 2522L;
		}
		if (num != num2)
		{
			return (AdventureDbId)activeExpansionAdventureWithHighestSortOrder.ID;
		}
		return AdventureDbId.INVALID;
	}

	// Token: 0x060072DC RID: 29404 RVA: 0x0024FC00 File Offset: 0x0024DE00
	public static AdventureDbId GetAdventurePlayerShouldSee()
	{
		int num = 0;
		return AdventureConfig.GetAdventurePlayerShouldSee(out num);
	}

	// Token: 0x060072DD RID: 29405 RVA: 0x0024FC18 File Offset: 0x0024DE18
	public static AdventureDbfRecord GetActiveExpansionAdventureWithHighestSortOrder()
	{
		List<AdventureDbfRecord> adventureRecordsWithDefPrefab = GameUtils.GetAdventureRecordsWithDefPrefab();
		AdventureDbfRecord adventureDbfRecord = null;
		foreach (AdventureDbfRecord adventureDbfRecord2 in adventureRecordsWithDefPrefab)
		{
			if (GameUtils.IsExpansionAdventure((AdventureDbId)adventureDbfRecord2.ID) && AdventureConfig.ShouldDisplayAdventure((AdventureDbId)adventureDbfRecord2.ID) && !AdventureConfig.IsAdventureComingSoon((AdventureDbId)adventureDbfRecord2.ID) && (adventureDbfRecord == null || adventureDbfRecord2.SortOrder > adventureDbfRecord.SortOrder))
			{
				adventureDbfRecord = adventureDbfRecord2;
			}
		}
		return adventureDbfRecord;
	}

	// Token: 0x060072DE RID: 29406 RVA: 0x0024FCA0 File Offset: 0x0024DEA0
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

	// Token: 0x060072DF RID: 29407 RVA: 0x0024FD14 File Offset: 0x0024DF14
	public int GetWingBossesDefeated(AdventureDbId advId, AdventureModeDbId mode, WingDbId wing, int defaultvalue = 0)
	{
		int result = 0;
		if (this.m_WingBossesDefeatedCache.TryGetValue(this.GetWingUniqueId(advId, mode, wing), out result))
		{
			return result;
		}
		return defaultvalue;
	}

	// Token: 0x060072E0 RID: 29408 RVA: 0x0024FD3F File Offset: 0x0024DF3F
	public void UpdateWingBossesDefeated(AdventureDbId advId, AdventureModeDbId mode, WingDbId wing, int bossesDefeated)
	{
		this.m_WingBossesDefeatedCache[this.GetWingUniqueId(advId, mode, wing)] = bossesDefeated;
	}

	// Token: 0x060072E1 RID: 29409 RVA: 0x0024FD57 File Offset: 0x0024DF57
	private string GetWingUniqueId(AdventureDbId advId, AdventureModeDbId modeId, WingDbId wing)
	{
		return string.Format("{0}_{1}_{2}", advId, modeId, wing);
	}

	// Token: 0x060072E2 RID: 29410 RVA: 0x0024FD75 File Offset: 0x0024DF75
	private void Awake()
	{
		AdventureConfig.s_instance = this;
		base.gameObject.AddComponent<HSDontDestroyOnLoad>();
	}

	// Token: 0x060072E3 RID: 29411 RVA: 0x0024FD89 File Offset: 0x0024DF89
	private void Start()
	{
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnSuccessfulPurchaseAck));
		this.AddSubSceneChangeListener(new AdventureConfig.SubSceneChange(this.OnSubSceneChange));
	}

	// Token: 0x060072E4 RID: 29412 RVA: 0x0024FDB3 File Offset: 0x0024DFB3
	private void OnDestroy()
	{
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnSuccessfulPurchaseAck));
		AdventureConfig.s_instance = null;
	}

	// Token: 0x060072E5 RID: 29413 RVA: 0x0024FDD4 File Offset: 0x0024DFD4
	public void OnAdventureSceneAwake()
	{
		this.SelectedAdventure = Options.Get().GetEnum<AdventureDbId>(Option.SELECTED_ADVENTURE, AdventureDbId.PRACTICE);
		this.SelectedMode = Options.Get().GetEnum<AdventureModeDbId>(Option.SELECTED_ADVENTURE_MODE, AdventureModeDbId.LINEAR);
		if (!AdventureConfig.ShouldDisplayAdventure(this.SelectedAdventure))
		{
			this.SelectedAdventure = AdventureDbId.PRACTICE;
			this.SelectedMode = AdventureModeDbId.LINEAR;
		}
		this.SetPropertiesForAdventureAndMode();
	}

	// Token: 0x060072E6 RID: 29414 RVA: 0x0024FE28 File Offset: 0x0024E028
	public void OnAdventureSceneUnload()
	{
		if (this.OnAdventureSceneUnloadEvent != null)
		{
			this.OnAdventureSceneUnloadEvent();
		}
		this.SelectedAdventure = AdventureDbId.INVALID;
		this.SelectedMode = AdventureModeDbId.INVALID;
	}

	// Token: 0x060072E7 RID: 29415 RVA: 0x0024FE4B File Offset: 0x0024E04B
	public void ResetSubScene()
	{
		this.ResetSubScene(AdventureData.Adventuresubscene.CHOOSER);
	}

	// Token: 0x060072E8 RID: 29416 RVA: 0x0024FE54 File Offset: 0x0024E054
	private void FireAdventureModeChangeEvent()
	{
		AdventureConfig.AdventureModeChange[] array = this.m_AdventureModeChangeEventList.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](this.SelectedAdventure, this.SelectedMode);
		}
	}

	// Token: 0x060072E9 RID: 29417 RVA: 0x0024FE90 File Offset: 0x0024E090
	private void FireSubSceneChangeEvent(bool forward)
	{
		this.UpdatePresence();
		AdventureConfig.SubSceneChange[] array = this.m_SubSceneChangeEventList.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](this.m_CurrentSubScene, forward);
		}
	}

	// Token: 0x060072EA RID: 29418 RVA: 0x0024FECC File Offset: 0x0024E0CC
	private void FireSelectedModeChangeEvent()
	{
		AdventureConfig.SelectedModeChange[] array = this.m_SelectedModeChangeEventList.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](this.SelectedAdventure, this.SelectedMode);
		}
	}

	// Token: 0x060072EB RID: 29419 RVA: 0x0024FF08 File Offset: 0x0024E108
	public void UpdatePresence()
	{
		AdventureData.Adventuresubscene currentSubScene = this.m_CurrentSubScene;
		if (currentSubScene - AdventureData.Adventuresubscene.MISSION_DECK_PICKER <= 4 || currentSubScene == AdventureData.Adventuresubscene.LOCATION_SELECT)
		{
			PresenceMgr.Get().SetStatus_EnteringAdventure(this.SelectedAdventure, this.SelectedMode);
			return;
		}
		if (AdventureScene.Get() != null && !AdventureScene.Get().IsUnloading())
		{
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.ADVENTURE_CHOOSING_MODE
			});
		}
	}

	// Token: 0x060072EC RID: 29420 RVA: 0x0024FF74 File Offset: 0x0024E174
	public bool IsHeroSelectedBeforeDungeonCrawlScreenForSelectedAdventure()
	{
		AdventureDataDbfRecord selectedAdventureDataRecord = this.GetSelectedAdventureDataRecord();
		return selectedAdventureDataRecord != null && selectedAdventureDataRecord.DungeonCrawlPickHeroFirst;
	}

	// Token: 0x060072ED RID: 29421 RVA: 0x0024FF94 File Offset: 0x0024E194
	public bool IsChapterSelectedBeforeDungeonCrawlScreenForSelectedAdventure()
	{
		AdventureDataDbfRecord selectedAdventureDataRecord = this.GetSelectedAdventureDataRecord();
		return selectedAdventureDataRecord != null && selectedAdventureDataRecord.DungeonCrawlSelectChapter;
	}

	// Token: 0x060072EE RID: 29422 RVA: 0x0024FFB4 File Offset: 0x0024E1B4
	public bool ValidLoadoutIsLockedInForSelectedAdventure()
	{
		AdventureDataDbfRecord selectedAdventureDataRecord = this.GetSelectedAdventureDataRecord();
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)selectedAdventureDataRecord.GameSaveDataServerKey;
		if (!GameSaveDataManager.Get().ValidateIfKeyCanBeAccessed(gameSaveDataServerKey, selectedAdventureDataRecord.Name))
		{
			return false;
		}
		long num = 0L;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_SCENARIO_ID, out num);
		long num2 = 0L;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS, out num2);
		long num3 = 0L;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_POWER, out num3);
		long num4 = 0L;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_DECK, out num4);
		long num5 = 0L;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_LOADOUT_TREASURE_ID, out num5);
		return AdventureUtils.IsValidLoadoutForSelectedAdventure(this.SelectedAdventure, this.SelectedMode, (ScenarioDbId)num, (TAG_CLASS)num2, (int)num3, (int)num4, (int)num5);
	}

	// Token: 0x060072EF RID: 29423 RVA: 0x00250078 File Offset: 0x0024E278
	public bool GuestHeroesExistForCurrentAdventure()
	{
		return GameDbf.AdventureGuestHeroes.HasRecord((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)this.GetSelectedAdventure());
	}

	// Token: 0x060072F0 RID: 29424 RVA: 0x00250090 File Offset: 0x0024E290
	public List<int> GetGuestHeroesForCurrentAdventure()
	{
		return AdventureUtils.GetGuestHeroesForAdventure(this.GetSelectedAdventure());
	}

	// Token: 0x060072F1 RID: 29425 RVA: 0x002500A0 File Offset: 0x0024E2A0
	public static List<int> GetGuestHeroesForWing(int wingId)
	{
		List<AdventureGuestHeroesDbfRecord> records = GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.WingId == wingId, -1);
		records.Sort((AdventureGuestHeroesDbfRecord a, AdventureGuestHeroesDbfRecord b) => a.SortOrder.CompareTo(b.SortOrder));
		List<int> list = new List<int>();
		foreach (AdventureGuestHeroesDbfRecord adventureGuestHeroesDbfRecord in records)
		{
			list.Add(GameUtils.GetCardIdFromGuestHeroDbId(adventureGuestHeroesDbfRecord.GuestHeroId));
		}
		return list;
	}

	// Token: 0x060072F2 RID: 29426 RVA: 0x00250148 File Offset: 0x0024E348
	public static int GetAdventureBossesInRun(WingDbfRecord wingRecord)
	{
		if (wingRecord == null)
		{
			Debug.LogError("GetAdventureBossesInRun - no WingDbfRecord passed in!");
			return 0;
		}
		return wingRecord.DungeonCrawlBosses;
	}

	// Token: 0x060072F3 RID: 29427 RVA: 0x0025015F File Offset: 0x0024E35F
	public AdventureData.Adventuresubscene SubSceneForPickingHeroForCurrentAdventure()
	{
		if (!this.GuestHeroesExistForCurrentAdventure())
		{
			return AdventureData.Adventuresubscene.MISSION_DECK_PICKER;
		}
		return AdventureData.Adventuresubscene.ADVENTURER_PICKER;
	}

	// Token: 0x060072F4 RID: 29428 RVA: 0x0025016C File Offset: 0x0024E36C
	public AdventureData.Adventuresubscene GetCorrectSubSceneWhenLoadingDungeonCrawlMode()
	{
		bool flag = DungeonCrawlUtil.IsDungeonRunInProgress(this.SelectedAdventure, this.SelectedMode) || this.ValidLoadoutIsLockedInForSelectedAdventure();
		if (!flag && this.IsChapterSelectedBeforeDungeonCrawlScreenForSelectedAdventure())
		{
			return AdventureData.Adventuresubscene.LOCATION_SELECT;
		}
		if (!flag && this.IsHeroSelectedBeforeDungeonCrawlScreenForSelectedAdventure())
		{
			return this.SubSceneForPickingHeroForCurrentAdventure();
		}
		return AdventureData.Adventuresubscene.DUNGEON_CRAWL;
	}

	// Token: 0x060072F5 RID: 29429 RVA: 0x002501B6 File Offset: 0x0024E3B6
	private void OnSuccessfulPurchaseAck(Network.Bundle bundle, PaymentMethod purchaseMethod)
	{
		this.EvaluateIfAllWingsOwnedForSelectedAdventure();
	}

	// Token: 0x060072F6 RID: 29430 RVA: 0x002501C0 File Offset: 0x0024E3C0
	private void OnSubSceneChange(AdventureData.Adventuresubscene subScene, bool forward)
	{
		bool flag = GameUtils.DoesAdventureModeUseDungeonCrawlFormat(this.GetSelectedMode());
		bool flag2 = subScene == AdventureData.Adventuresubscene.MISSION_DECK_PICKER || subScene == AdventureData.Adventuresubscene.ADVENTURER_PICKER;
		if (flag && flag2)
		{
			WingDbId wingIdFromMissionId = GameUtils.GetWingIdFromMissionId(this.GetMission());
			DungeonCrawlSubDef_VOLines.PlayVOLine(this.GetSelectedAdventure(), wingIdFromMissionId, 0, DungeonCrawlSubDef_VOLines.VOEventType.CHARACTER_SELECT, 0, true);
		}
	}

	// Token: 0x060072F7 RID: 29431 RVA: 0x00250205 File Offset: 0x0024E405
	private void SetPropertiesForAdventureAndMode()
	{
		this.EvaluateIfAllWingsOwnedForSelectedAdventure();
		this.UpdateCompletionRewards();
	}

	// Token: 0x060072F8 RID: 29432 RVA: 0x00250213 File Offset: 0x0024E413
	private void EvaluateIfAllWingsOwnedForSelectedAdventure()
	{
		if (this.SelectedAdventure == AdventureDbId.INVALID || this.SelectedMode == AdventureModeDbId.INVALID)
		{
			return;
		}
		this.AllChaptersOwned = AdventureProgressMgr.Get().OwnsAllAdventureWings(this.SelectedAdventure);
	}

	// Token: 0x060072F9 RID: 29433 RVA: 0x0025023C File Offset: 0x0024E43C
	private void UpdateCompletionRewards()
	{
		List<RewardData> rewardsForAdventureByMode = AdventureProgressMgr.GetRewardsForAdventureByMode((int)this.SelectedAdventure, (int)this.SelectedMode, new HashSet<Achieve.RewardTiming>
		{
			Achieve.RewardTiming.ADVENTURE_CHEST
		});
		this.Legacy_UpdateCompletionRewardData(rewardsForAdventureByMode);
		this.CompletionRewards.Items.Clear();
		foreach (RewardData rewardData in rewardsForAdventureByMode)
		{
			RewardItemDataModel rewardItemDataModel = RewardUtils.RewardDataToRewardItemDataModel(rewardData);
			if (rewardItemDataModel != null)
			{
				this.CompletionRewards.Items.Add(rewardItemDataModel);
			}
		}
	}

	// Token: 0x060072FA RID: 29434 RVA: 0x002502D4 File Offset: 0x0024E4D4
	private void Legacy_UpdateCompletionRewardData(List<RewardData> adventureCompletionRewards)
	{
		bool flag = false;
		foreach (RewardData rewardData in adventureCompletionRewards)
		{
			if (rewardData is CardBackRewardData)
			{
				flag = true;
				CardBackRewardData cardBackRewardData = rewardData as CardBackRewardData;
				this.CompletionRewardType = Reward.Type.CARD_BACK;
				this.CompletionRewardId = cardBackRewardData.CardBackID;
			}
		}
		if (adventureCompletionRewards.Count < 1 || !flag)
		{
			this.CompletionRewardType = Reward.Type.NONE;
			this.CompletionRewardId = 0;
		}
	}

	// Token: 0x060072FB RID: 29435 RVA: 0x0025035C File Offset: 0x0024E55C
	public void ResetLoadout()
	{
		this.AnomalyModeActivated = false;
		this.SelectedHeroClass = TAG_CLASS.INVALID;
		this.SelectedLoadoutTreasureDbId = 0L;
		this.SelectedHeroPowerDbId = 0L;
		this.SelectedDeckId = 0L;
		this.SetMissionOverride(ScenarioDbId.INVALID);
	}

	// Token: 0x060072FC RID: 29436 RVA: 0x0025038B File Offset: 0x0024E58B
	public void SetHasSeenUnlockedChapterPage(WingDbId wingId, bool hasSeen)
	{
		if (hasSeen)
		{
			this.NeedsChapterNewlyUnlockedHighlight.Remove((long)wingId);
			return;
		}
		if (this.GetHasSeenUnlockedChapterPage(wingId))
		{
			this.NeedsChapterNewlyUnlockedHighlight.Add((long)wingId);
		}
	}

	// Token: 0x060072FD RID: 29437 RVA: 0x002503B5 File Offset: 0x0024E5B5
	public bool GetHasSeenUnlockedChapterPage(WingDbId wingId)
	{
		return !this.NeedsChapterNewlyUnlockedHighlight.Contains((long)wingId);
	}

	// Token: 0x060072FE RID: 29438 RVA: 0x002503C8 File Offset: 0x0024E5C8
	public bool HasUnacknowledgedChapterUnlocks()
	{
		foreach (WingDbfRecord wingDbfRecord in GameDbf.Wing.GetRecords((WingDbfRecord r) => r.AdventureId == (int)this.SelectedAdventure, -1))
		{
			int num = (int)AdventureProgressMgr.Get().AdventureBookChapterStateForWing(wingDbfRecord, this.SelectedMode);
			int num2;
			AdventureProgressMgr.Get().GetWingAck(wingDbfRecord.ID, out num2);
			if (num == 1 && num2 == 0)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060072FF RID: 29439 RVA: 0x00250458 File Offset: 0x0024E658
	public bool HasValidLoadoutForSelectedAdventure()
	{
		return AdventureUtils.IsValidLoadoutForSelectedAdventure(this.SelectedAdventure, this.SelectedMode, this.m_SelectedMission, this.SelectedHeroClass, (int)this.SelectedHeroPowerDbId, (int)this.SelectedDeckId, (int)this.SelectedLoadoutTreasureDbId);
	}

	// Token: 0x04005B86 RID: 23430
	public const string DEFAULT_SET_UP_STATE = "SetUpState";

	// Token: 0x04005B87 RID: 23431
	public const string PLAY_BUTTON_ANOMALY_ACTIVE_STATE = "PURPLE_SWIRL";

	// Token: 0x04005B88 RID: 23432
	public const string PLAY_BUTTON_ANOMALY_INACTIVE_STATE = "BLUE_SWIRL";

	// Token: 0x04005B89 RID: 23433
	private static AdventureConfig s_instance;

	// Token: 0x04005B8A RID: 23434
	private AdventureDbId m_SelectedAdventure = AdventureDbId.PRACTICE;

	// Token: 0x04005B8B RID: 23435
	private AdventureModeDbId m_SelectedMode = AdventureModeDbId.LINEAR;

	// Token: 0x04005B8C RID: 23436
	private Stack<AdventureData.Adventuresubscene> m_SubSceneBackStack = new Stack<AdventureData.Adventuresubscene>();

	// Token: 0x04005B8D RID: 23437
	private AdventureData.Adventuresubscene m_CurrentSubScene;

	// Token: 0x04005B8E RID: 23438
	private AdventureData.Adventuresubscene m_PreviousSubScene = AdventureData.Adventuresubscene.INVALID;

	// Token: 0x04005B8F RID: 23439
	private ScenarioDbId m_SelectedMission;

	// Token: 0x04005B90 RID: 23440
	private ScenarioDbId m_MissionOverride;

	// Token: 0x04005B92 RID: 23442
	private bool m_anomalyModeActivated;

	// Token: 0x04005B93 RID: 23443
	private List<long> NeedsChapterNewlyUnlockedHighlight = new List<long>();

	// Token: 0x04005B98 RID: 23448
	private bool m_allChaptersOwned;

	// Token: 0x04005B99 RID: 23449
	private Reward.Type m_completionRewardType;

	// Token: 0x04005B9A RID: 23450
	private int m_completionRewardId;

	// Token: 0x04005B9B RID: 23451
	private List<AdventureConfig.AdventureModeChange> m_AdventureModeChangeEventList = new List<AdventureConfig.AdventureModeChange>();

	// Token: 0x04005B9C RID: 23452
	private List<AdventureConfig.SubSceneChange> m_SubSceneChangeEventList = new List<AdventureConfig.SubSceneChange>();

	// Token: 0x04005B9D RID: 23453
	private List<AdventureConfig.SelectedModeChange> m_SelectedModeChangeEventList = new List<AdventureConfig.SelectedModeChange>();

	// Token: 0x04005B9E RID: 23454
	private List<AdventureConfig.AdventureMissionSet> m_AdventureMissionSetEventList = new List<AdventureConfig.AdventureMissionSet>();

	// Token: 0x04005BA0 RID: 23456
	private Map<string, int> m_WingBossesDefeatedCache = new Map<string, int>();

	// Token: 0x04005BA1 RID: 23457
	private Map<string, ScenarioDbId> m_LastSelectedMissions = new Map<string, ScenarioDbId>();

	// Token: 0x04005BA2 RID: 23458
	private Map<ScenarioDbId, bool> m_CachedDefeatedScenario = new Map<ScenarioDbId, bool>();

	// Token: 0x04005BA3 RID: 23459
	private Map<ScenarioDbId, AdventureBossDef> m_CachedBossDef = new Map<ScenarioDbId, AdventureBossDef>();

	// Token: 0x04005BA4 RID: 23460
	private Map<AdventureDbId, AdventureModeDbId> m_ClientChooserAdventureModes = new Map<AdventureDbId, AdventureModeDbId>();

	// Token: 0x0200243A RID: 9274
	// (Invoke) Token: 0x06012EA8 RID: 77480
	public delegate void DelBossDefLoaded(AdventureBossDef bossDef, bool success);

	// Token: 0x0200243B RID: 9275
	// (Invoke) Token: 0x06012EAC RID: 77484
	public delegate void AdventureModeChange(AdventureDbId adventureId, AdventureModeDbId modeId);

	// Token: 0x0200243C RID: 9276
	// (Invoke) Token: 0x06012EB0 RID: 77488
	public delegate void AdventureMissionSet(ScenarioDbId mission, bool showDetails);

	// Token: 0x0200243D RID: 9277
	// (Invoke) Token: 0x06012EB4 RID: 77492
	public delegate void SubSceneChange(AdventureData.Adventuresubscene newscene, bool forward);

	// Token: 0x0200243E RID: 9278
	// (Invoke) Token: 0x06012EB8 RID: 77496
	public delegate void SelectedModeChange(AdventureDbId adventureId, AdventureModeDbId modeId);

	// Token: 0x0200243F RID: 9279
	// (Invoke) Token: 0x06012EBC RID: 77500
	public delegate void AnomalyModeChangedHandler(bool anomalyModeActived);
}
