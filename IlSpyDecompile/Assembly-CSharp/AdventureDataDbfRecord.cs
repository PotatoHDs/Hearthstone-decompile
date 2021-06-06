using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class AdventureDataDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private int m_adventureId;

	[SerializeField]
	private int m_modeId;

	[SerializeField]
	private int m_sortOrder;

	[SerializeField]
	private DbfLocValue m_name;

	[SerializeField]
	private DbfLocValue m_shortName;

	[SerializeField]
	private DbfLocValue m_description;

	[SerializeField]
	private DbfLocValue m_shortDescription;

	[SerializeField]
	private DbfLocValue m_lockedShortName;

	[SerializeField]
	private DbfLocValue m_lockedDescription;

	[SerializeField]
	private DbfLocValue m_lockedShortDescription;

	[SerializeField]
	private DbfLocValue m_requirementsDescription;

	[SerializeField]
	private DbfLocValue m_rewardsDescription;

	[SerializeField]
	private DbfLocValue m_completeBannerText;

	[SerializeField]
	private bool m_showPlayableScenariosCount = true;

	[SerializeField]
	private AdventureData.Adventuresubscene m_startingSubscene;

	[SerializeField]
	private string m_subsceneTransitionDirection = "INVALID";

	[SerializeField]
	private string m_adventureSubDefPrefab;

	[SerializeField]
	private int m_gameSaveDataServerKeyId;

	[SerializeField]
	private int m_gameSaveDataClientKeyId;

	[SerializeField]
	private string m_dungeonCrawlBossCardPrefab;

	[SerializeField]
	private bool m_dungeonCrawlPickHeroFirst;

	[SerializeField]
	private bool m_dungeonCrawlSkipHeroSelect;

	[SerializeField]
	private bool m_dungeonCrawlMustPickShrine;

	[SerializeField]
	private bool m_dungeonCrawlSelectChapter;

	[SerializeField]
	private bool m_dungeonCrawlDisplayHeroWinsPerChapter = true;

	[SerializeField]
	private bool m_dungeonCrawlIsRetireSupported;

	[SerializeField]
	private bool m_dungeonCrawlShowBossKillCount = true;

	[SerializeField]
	private bool m_dungeonCrawlDefaultToDeckFromUpcomingScenario;

	[SerializeField]
	private bool m_ignoreHeroUnlockRequirement;

	[SerializeField]
	private int m_bossCardBackId;

	[SerializeField]
	private string m_hasSeenFeaturedModeOption;

	[SerializeField]
	private string m_hasSeenNewModePopupOption;

	[SerializeField]
	private string m_prefabShownOnComplete;

	[SerializeField]
	private int m_anomalyModeDefaultCardId;

	[SerializeField]
	private AdventureData.Adventurebooklocation m_adventureBookMapPageLocation = AdventureData.ParseAdventurebooklocationValue("Beginning");

	[SerializeField]
	private AdventureData.Adventurebooklocation m_adventureBookRewardPageLocation = AdventureData.ParseAdventurebooklocationValue("End");

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("ADVENTURE_ID")]
	public int AdventureId => m_adventureId;

	[DbfField("MODE_ID")]
	public int ModeId => m_modeId;

	public AdventureModeDbfRecord ModeRecord => GameDbf.AdventureMode.GetRecord(m_modeId);

	[DbfField("SORT_ORDER")]
	public int SortOrder => m_sortOrder;

	[DbfField("NAME")]
	public DbfLocValue Name => m_name;

	[DbfField("SHORT_NAME")]
	public DbfLocValue ShortName => m_shortName;

	[DbfField("DESCRIPTION")]
	public DbfLocValue Description => m_description;

	[DbfField("SHORT_DESCRIPTION")]
	public DbfLocValue ShortDescription => m_shortDescription;

	[DbfField("LOCKED_SHORT_NAME")]
	public DbfLocValue LockedShortName => m_lockedShortName;

	[DbfField("LOCKED_DESCRIPTION")]
	public DbfLocValue LockedDescription => m_lockedDescription;

	[DbfField("LOCKED_SHORT_DESCRIPTION")]
	public DbfLocValue LockedShortDescription => m_lockedShortDescription;

	[DbfField("REQUIREMENTS_DESCRIPTION")]
	public DbfLocValue RequirementsDescription => m_requirementsDescription;

	[DbfField("REWARDS_DESCRIPTION")]
	public DbfLocValue RewardsDescription => m_rewardsDescription;

	[DbfField("COMPLETE_BANNER_TEXT")]
	public DbfLocValue CompleteBannerText => m_completeBannerText;

	[DbfField("SHOW_PLAYABLE_SCENARIOS_COUNT")]
	public bool ShowPlayableScenariosCount => m_showPlayableScenariosCount;

	[DbfField("STARTING_SUBSCENE")]
	public AdventureData.Adventuresubscene StartingSubscene => m_startingSubscene;

	[DbfField("SUBSCENE_TRANSITION_DIRECTION")]
	public string SubsceneTransitionDirection => m_subsceneTransitionDirection;

	[DbfField("ADVENTURE_SUB_DEF_PREFAB")]
	public string AdventureSubDefPrefab => m_adventureSubDefPrefab;

	[DbfField("GAME_SAVE_DATA_SERVER_KEY")]
	public int GameSaveDataServerKey => m_gameSaveDataServerKeyId;

	[DbfField("GAME_SAVE_DATA_CLIENT_KEY")]
	public int GameSaveDataClientKey => m_gameSaveDataClientKeyId;

	[DbfField("DUNGEON_CRAWL_BOSS_CARD_PREFAB")]
	public string DungeonCrawlBossCardPrefab => m_dungeonCrawlBossCardPrefab;

	[DbfField("DUNGEON_CRAWL_PICK_HERO_FIRST")]
	public bool DungeonCrawlPickHeroFirst => m_dungeonCrawlPickHeroFirst;

	[DbfField("DUNGEON_CRAWL_SKIP_HERO_SELECT")]
	public bool DungeonCrawlSkipHeroSelect => m_dungeonCrawlSkipHeroSelect;

	[DbfField("DUNGEON_CRAWL_MUST_PICK_SHRINE")]
	public bool DungeonCrawlMustPickShrine => m_dungeonCrawlMustPickShrine;

	[DbfField("DUNGEON_CRAWL_SELECT_CHAPTER")]
	public bool DungeonCrawlSelectChapter => m_dungeonCrawlSelectChapter;

	[DbfField("DUNGEON_CRAWL_DISPLAY_HERO_WINS_PER_CHAPTER")]
	public bool DungeonCrawlDisplayHeroWinsPerChapter => m_dungeonCrawlDisplayHeroWinsPerChapter;

	[DbfField("DUNGEON_CRAWL_IS_RETIRE_SUPPORTED")]
	public bool DungeonCrawlIsRetireSupported => m_dungeonCrawlIsRetireSupported;

	[DbfField("DUNGEON_CRAWL_SHOW_BOSS_KILL_COUNT")]
	public bool DungeonCrawlShowBossKillCount => m_dungeonCrawlShowBossKillCount;

	[DbfField("DUNGEON_CRAWL_DEFAULT_TO_DECK_FROM_UPCOMING_SCENARIO")]
	public bool DungeonCrawlDefaultToDeckFromUpcomingScenario => m_dungeonCrawlDefaultToDeckFromUpcomingScenario;

	[DbfField("IGNORE_HERO_UNLOCK_REQUIREMENT")]
	public bool IgnoreHeroUnlockRequirement => m_ignoreHeroUnlockRequirement;

	[DbfField("BOSS_CARD_BACK")]
	public int BossCardBack => m_bossCardBackId;

	public CardBackDbfRecord BossCardBackRecord => GameDbf.CardBack.GetRecord(m_bossCardBackId);

	[DbfField("HAS_SEEN_FEATURED_MODE_OPTION")]
	public string HasSeenFeaturedModeOption => m_hasSeenFeaturedModeOption;

	[DbfField("HAS_SEEN_NEW_MODE_POPUP_OPTION")]
	public string HasSeenNewModePopupOption => m_hasSeenNewModePopupOption;

	[DbfField("PREFAB_SHOWN_ON_COMPLETE")]
	public string PrefabShownOnComplete => m_prefabShownOnComplete;

	[DbfField("ANOMALY_MODE_DEFAULT_CARD_ID")]
	public int AnomalyModeDefaultCardId => m_anomalyModeDefaultCardId;

	public CardDbfRecord AnomalyModeDefaultCardRecord => GameDbf.Card.GetRecord(m_anomalyModeDefaultCardId);

	[DbfField("ADVENTURE_BOOK_MAP_PAGE_LOCATION")]
	public AdventureData.Adventurebooklocation AdventureBookMapPageLocation => m_adventureBookMapPageLocation;

	[DbfField("ADVENTURE_BOOK_REWARD_PAGE_LOCATION")]
	public AdventureData.Adventurebooklocation AdventureBookRewardPageLocation => m_adventureBookRewardPageLocation;

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetAdventureId(int v)
	{
		m_adventureId = v;
	}

	public void SetModeId(int v)
	{
		m_modeId = v;
	}

	public void SetSortOrder(int v)
	{
		m_sortOrder = v;
	}

	public void SetName(DbfLocValue v)
	{
		m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	public void SetShortName(DbfLocValue v)
	{
		m_shortName = v;
		v.SetDebugInfo(base.ID, "SHORT_NAME");
	}

	public void SetDescription(DbfLocValue v)
	{
		m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	public void SetShortDescription(DbfLocValue v)
	{
		m_shortDescription = v;
		v.SetDebugInfo(base.ID, "SHORT_DESCRIPTION");
	}

	public void SetLockedShortName(DbfLocValue v)
	{
		m_lockedShortName = v;
		v.SetDebugInfo(base.ID, "LOCKED_SHORT_NAME");
	}

	public void SetLockedDescription(DbfLocValue v)
	{
		m_lockedDescription = v;
		v.SetDebugInfo(base.ID, "LOCKED_DESCRIPTION");
	}

	public void SetLockedShortDescription(DbfLocValue v)
	{
		m_lockedShortDescription = v;
		v.SetDebugInfo(base.ID, "LOCKED_SHORT_DESCRIPTION");
	}

	public void SetRequirementsDescription(DbfLocValue v)
	{
		m_requirementsDescription = v;
		v.SetDebugInfo(base.ID, "REQUIREMENTS_DESCRIPTION");
	}

	public void SetRewardsDescription(DbfLocValue v)
	{
		m_rewardsDescription = v;
		v.SetDebugInfo(base.ID, "REWARDS_DESCRIPTION");
	}

	public void SetCompleteBannerText(DbfLocValue v)
	{
		m_completeBannerText = v;
		v.SetDebugInfo(base.ID, "COMPLETE_BANNER_TEXT");
	}

	public void SetShowPlayableScenariosCount(bool v)
	{
		m_showPlayableScenariosCount = v;
	}

	public void SetStartingSubscene(AdventureData.Adventuresubscene v)
	{
		m_startingSubscene = v;
	}

	public void SetSubsceneTransitionDirection(string v)
	{
		m_subsceneTransitionDirection = v;
	}

	public void SetAdventureSubDefPrefab(string v)
	{
		m_adventureSubDefPrefab = v;
	}

	public void SetGameSaveDataServerKey(int v)
	{
		m_gameSaveDataServerKeyId = v;
	}

	public void SetGameSaveDataClientKey(int v)
	{
		m_gameSaveDataClientKeyId = v;
	}

	public void SetDungeonCrawlBossCardPrefab(string v)
	{
		m_dungeonCrawlBossCardPrefab = v;
	}

	public void SetDungeonCrawlPickHeroFirst(bool v)
	{
		m_dungeonCrawlPickHeroFirst = v;
	}

	public void SetDungeonCrawlSkipHeroSelect(bool v)
	{
		m_dungeonCrawlSkipHeroSelect = v;
	}

	public void SetDungeonCrawlMustPickShrine(bool v)
	{
		m_dungeonCrawlMustPickShrine = v;
	}

	public void SetDungeonCrawlSelectChapter(bool v)
	{
		m_dungeonCrawlSelectChapter = v;
	}

	public void SetDungeonCrawlDisplayHeroWinsPerChapter(bool v)
	{
		m_dungeonCrawlDisplayHeroWinsPerChapter = v;
	}

	public void SetDungeonCrawlIsRetireSupported(bool v)
	{
		m_dungeonCrawlIsRetireSupported = v;
	}

	public void SetDungeonCrawlShowBossKillCount(bool v)
	{
		m_dungeonCrawlShowBossKillCount = v;
	}

	public void SetDungeonCrawlDefaultToDeckFromUpcomingScenario(bool v)
	{
		m_dungeonCrawlDefaultToDeckFromUpcomingScenario = v;
	}

	public void SetIgnoreHeroUnlockRequirement(bool v)
	{
		m_ignoreHeroUnlockRequirement = v;
	}

	public void SetBossCardBack(int v)
	{
		m_bossCardBackId = v;
	}

	public void SetHasSeenFeaturedModeOption(string v)
	{
		m_hasSeenFeaturedModeOption = v;
	}

	public void SetHasSeenNewModePopupOption(string v)
	{
		m_hasSeenNewModePopupOption = v;
	}

	public void SetPrefabShownOnComplete(string v)
	{
		m_prefabShownOnComplete = v;
	}

	public void SetAnomalyModeDefaultCardId(int v)
	{
		m_anomalyModeDefaultCardId = v;
	}

	public void SetAdventureBookMapPageLocation(AdventureData.Adventurebooklocation v)
	{
		m_adventureBookMapPageLocation = v;
	}

	public void SetAdventureBookRewardPageLocation(AdventureData.Adventurebooklocation v)
	{
		m_adventureBookRewardPageLocation = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_DESC" => m_noteDesc, 
			"ADVENTURE_ID" => m_adventureId, 
			"MODE_ID" => m_modeId, 
			"SORT_ORDER" => m_sortOrder, 
			"NAME" => m_name, 
			"SHORT_NAME" => m_shortName, 
			"DESCRIPTION" => m_description, 
			"SHORT_DESCRIPTION" => m_shortDescription, 
			"LOCKED_SHORT_NAME" => m_lockedShortName, 
			"LOCKED_DESCRIPTION" => m_lockedDescription, 
			"LOCKED_SHORT_DESCRIPTION" => m_lockedShortDescription, 
			"REQUIREMENTS_DESCRIPTION" => m_requirementsDescription, 
			"REWARDS_DESCRIPTION" => m_rewardsDescription, 
			"COMPLETE_BANNER_TEXT" => m_completeBannerText, 
			"SHOW_PLAYABLE_SCENARIOS_COUNT" => m_showPlayableScenariosCount, 
			"STARTING_SUBSCENE" => m_startingSubscene, 
			"SUBSCENE_TRANSITION_DIRECTION" => m_subsceneTransitionDirection, 
			"ADVENTURE_SUB_DEF_PREFAB" => m_adventureSubDefPrefab, 
			"GAME_SAVE_DATA_SERVER_KEY" => m_gameSaveDataServerKeyId, 
			"GAME_SAVE_DATA_CLIENT_KEY" => m_gameSaveDataClientKeyId, 
			"DUNGEON_CRAWL_BOSS_CARD_PREFAB" => m_dungeonCrawlBossCardPrefab, 
			"DUNGEON_CRAWL_PICK_HERO_FIRST" => m_dungeonCrawlPickHeroFirst, 
			"DUNGEON_CRAWL_SKIP_HERO_SELECT" => m_dungeonCrawlSkipHeroSelect, 
			"DUNGEON_CRAWL_MUST_PICK_SHRINE" => m_dungeonCrawlMustPickShrine, 
			"DUNGEON_CRAWL_SELECT_CHAPTER" => m_dungeonCrawlSelectChapter, 
			"DUNGEON_CRAWL_DISPLAY_HERO_WINS_PER_CHAPTER" => m_dungeonCrawlDisplayHeroWinsPerChapter, 
			"DUNGEON_CRAWL_IS_RETIRE_SUPPORTED" => m_dungeonCrawlIsRetireSupported, 
			"DUNGEON_CRAWL_SHOW_BOSS_KILL_COUNT" => m_dungeonCrawlShowBossKillCount, 
			"DUNGEON_CRAWL_DEFAULT_TO_DECK_FROM_UPCOMING_SCENARIO" => m_dungeonCrawlDefaultToDeckFromUpcomingScenario, 
			"IGNORE_HERO_UNLOCK_REQUIREMENT" => m_ignoreHeroUnlockRequirement, 
			"BOSS_CARD_BACK" => m_bossCardBackId, 
			"HAS_SEEN_FEATURED_MODE_OPTION" => m_hasSeenFeaturedModeOption, 
			"HAS_SEEN_NEW_MODE_POPUP_OPTION" => m_hasSeenNewModePopupOption, 
			"PREFAB_SHOWN_ON_COMPLETE" => m_prefabShownOnComplete, 
			"ANOMALY_MODE_DEFAULT_CARD_ID" => m_anomalyModeDefaultCardId, 
			"ADVENTURE_BOOK_MAP_PAGE_LOCATION" => m_adventureBookMapPageLocation, 
			"ADVENTURE_BOOK_REWARD_PAGE_LOCATION" => m_adventureBookRewardPageLocation, 
			_ => null, 
		};
	}

	public override void SetVar(string name, object val)
	{
		switch (name)
		{
		case "ID":
			SetID((int)val);
			break;
		case "NOTE_DESC":
			m_noteDesc = (string)val;
			break;
		case "ADVENTURE_ID":
			m_adventureId = (int)val;
			break;
		case "MODE_ID":
			m_modeId = (int)val;
			break;
		case "SORT_ORDER":
			m_sortOrder = (int)val;
			break;
		case "NAME":
			m_name = (DbfLocValue)val;
			break;
		case "SHORT_NAME":
			m_shortName = (DbfLocValue)val;
			break;
		case "DESCRIPTION":
			m_description = (DbfLocValue)val;
			break;
		case "SHORT_DESCRIPTION":
			m_shortDescription = (DbfLocValue)val;
			break;
		case "LOCKED_SHORT_NAME":
			m_lockedShortName = (DbfLocValue)val;
			break;
		case "LOCKED_DESCRIPTION":
			m_lockedDescription = (DbfLocValue)val;
			break;
		case "LOCKED_SHORT_DESCRIPTION":
			m_lockedShortDescription = (DbfLocValue)val;
			break;
		case "REQUIREMENTS_DESCRIPTION":
			m_requirementsDescription = (DbfLocValue)val;
			break;
		case "REWARDS_DESCRIPTION":
			m_rewardsDescription = (DbfLocValue)val;
			break;
		case "COMPLETE_BANNER_TEXT":
			m_completeBannerText = (DbfLocValue)val;
			break;
		case "SHOW_PLAYABLE_SCENARIOS_COUNT":
			m_showPlayableScenariosCount = (bool)val;
			break;
		case "STARTING_SUBSCENE":
			if (val == null)
			{
				m_startingSubscene = AdventureData.Adventuresubscene.CHOOSER;
			}
			else if (val is AdventureData.Adventuresubscene || val is int)
			{
				m_startingSubscene = (AdventureData.Adventuresubscene)val;
			}
			else if (val is string)
			{
				m_startingSubscene = AdventureData.ParseAdventuresubsceneValue((string)val);
			}
			break;
		case "SUBSCENE_TRANSITION_DIRECTION":
			m_subsceneTransitionDirection = (string)val;
			break;
		case "ADVENTURE_SUB_DEF_PREFAB":
			m_adventureSubDefPrefab = (string)val;
			break;
		case "GAME_SAVE_DATA_SERVER_KEY":
			m_gameSaveDataServerKeyId = (int)val;
			break;
		case "GAME_SAVE_DATA_CLIENT_KEY":
			m_gameSaveDataClientKeyId = (int)val;
			break;
		case "DUNGEON_CRAWL_BOSS_CARD_PREFAB":
			m_dungeonCrawlBossCardPrefab = (string)val;
			break;
		case "DUNGEON_CRAWL_PICK_HERO_FIRST":
			m_dungeonCrawlPickHeroFirst = (bool)val;
			break;
		case "DUNGEON_CRAWL_SKIP_HERO_SELECT":
			m_dungeonCrawlSkipHeroSelect = (bool)val;
			break;
		case "DUNGEON_CRAWL_MUST_PICK_SHRINE":
			m_dungeonCrawlMustPickShrine = (bool)val;
			break;
		case "DUNGEON_CRAWL_SELECT_CHAPTER":
			m_dungeonCrawlSelectChapter = (bool)val;
			break;
		case "DUNGEON_CRAWL_DISPLAY_HERO_WINS_PER_CHAPTER":
			m_dungeonCrawlDisplayHeroWinsPerChapter = (bool)val;
			break;
		case "DUNGEON_CRAWL_IS_RETIRE_SUPPORTED":
			m_dungeonCrawlIsRetireSupported = (bool)val;
			break;
		case "DUNGEON_CRAWL_SHOW_BOSS_KILL_COUNT":
			m_dungeonCrawlShowBossKillCount = (bool)val;
			break;
		case "DUNGEON_CRAWL_DEFAULT_TO_DECK_FROM_UPCOMING_SCENARIO":
			m_dungeonCrawlDefaultToDeckFromUpcomingScenario = (bool)val;
			break;
		case "IGNORE_HERO_UNLOCK_REQUIREMENT":
			m_ignoreHeroUnlockRequirement = (bool)val;
			break;
		case "BOSS_CARD_BACK":
			m_bossCardBackId = (int)val;
			break;
		case "HAS_SEEN_FEATURED_MODE_OPTION":
			m_hasSeenFeaturedModeOption = (string)val;
			break;
		case "HAS_SEEN_NEW_MODE_POPUP_OPTION":
			m_hasSeenNewModePopupOption = (string)val;
			break;
		case "PREFAB_SHOWN_ON_COMPLETE":
			m_prefabShownOnComplete = (string)val;
			break;
		case "ANOMALY_MODE_DEFAULT_CARD_ID":
			m_anomalyModeDefaultCardId = (int)val;
			break;
		case "ADVENTURE_BOOK_MAP_PAGE_LOCATION":
			if (val == null)
			{
				m_adventureBookMapPageLocation = AdventureData.Adventurebooklocation.BEGINNING;
			}
			else if (val is AdventureData.Adventurebooklocation || val is int)
			{
				m_adventureBookMapPageLocation = (AdventureData.Adventurebooklocation)val;
			}
			else if (val is string)
			{
				m_adventureBookMapPageLocation = AdventureData.ParseAdventurebooklocationValue((string)val);
			}
			break;
		case "ADVENTURE_BOOK_REWARD_PAGE_LOCATION":
			if (val == null)
			{
				m_adventureBookRewardPageLocation = AdventureData.Adventurebooklocation.BEGINNING;
			}
			else if (val is AdventureData.Adventurebooklocation || val is int)
			{
				m_adventureBookRewardPageLocation = (AdventureData.Adventurebooklocation)val;
			}
			else if (val is string)
			{
				m_adventureBookRewardPageLocation = AdventureData.ParseAdventurebooklocationValue((string)val);
			}
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"ADVENTURE_ID" => typeof(int), 
			"MODE_ID" => typeof(int), 
			"SORT_ORDER" => typeof(int), 
			"NAME" => typeof(DbfLocValue), 
			"SHORT_NAME" => typeof(DbfLocValue), 
			"DESCRIPTION" => typeof(DbfLocValue), 
			"SHORT_DESCRIPTION" => typeof(DbfLocValue), 
			"LOCKED_SHORT_NAME" => typeof(DbfLocValue), 
			"LOCKED_DESCRIPTION" => typeof(DbfLocValue), 
			"LOCKED_SHORT_DESCRIPTION" => typeof(DbfLocValue), 
			"REQUIREMENTS_DESCRIPTION" => typeof(DbfLocValue), 
			"REWARDS_DESCRIPTION" => typeof(DbfLocValue), 
			"COMPLETE_BANNER_TEXT" => typeof(DbfLocValue), 
			"SHOW_PLAYABLE_SCENARIOS_COUNT" => typeof(bool), 
			"STARTING_SUBSCENE" => typeof(AdventureData.Adventuresubscene), 
			"SUBSCENE_TRANSITION_DIRECTION" => typeof(string), 
			"ADVENTURE_SUB_DEF_PREFAB" => typeof(string), 
			"GAME_SAVE_DATA_SERVER_KEY" => typeof(int), 
			"GAME_SAVE_DATA_CLIENT_KEY" => typeof(int), 
			"DUNGEON_CRAWL_BOSS_CARD_PREFAB" => typeof(string), 
			"DUNGEON_CRAWL_PICK_HERO_FIRST" => typeof(bool), 
			"DUNGEON_CRAWL_SKIP_HERO_SELECT" => typeof(bool), 
			"DUNGEON_CRAWL_MUST_PICK_SHRINE" => typeof(bool), 
			"DUNGEON_CRAWL_SELECT_CHAPTER" => typeof(bool), 
			"DUNGEON_CRAWL_DISPLAY_HERO_WINS_PER_CHAPTER" => typeof(bool), 
			"DUNGEON_CRAWL_IS_RETIRE_SUPPORTED" => typeof(bool), 
			"DUNGEON_CRAWL_SHOW_BOSS_KILL_COUNT" => typeof(bool), 
			"DUNGEON_CRAWL_DEFAULT_TO_DECK_FROM_UPCOMING_SCENARIO" => typeof(bool), 
			"IGNORE_HERO_UNLOCK_REQUIREMENT" => typeof(bool), 
			"BOSS_CARD_BACK" => typeof(int), 
			"HAS_SEEN_FEATURED_MODE_OPTION" => typeof(string), 
			"HAS_SEEN_NEW_MODE_POPUP_OPTION" => typeof(string), 
			"PREFAB_SHOWN_ON_COMPLETE" => typeof(string), 
			"ANOMALY_MODE_DEFAULT_CARD_ID" => typeof(int), 
			"ADVENTURE_BOOK_MAP_PAGE_LOCATION" => typeof(AdventureData.Adventurebooklocation), 
			"ADVENTURE_BOOK_REWARD_PAGE_LOCATION" => typeof(AdventureData.Adventurebooklocation), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAdventureDataDbfRecords loadRecords = new LoadAdventureDataDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AdventureDataDbfAsset adventureDataDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AdventureDataDbfAsset)) as AdventureDataDbfAsset;
		if (adventureDataDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"AdventureDataDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < adventureDataDbfAsset.Records.Count; i++)
		{
			adventureDataDbfAsset.Records[i].StripUnusedLocales();
		}
		records = adventureDataDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_name.StripUnusedLocales();
		m_shortName.StripUnusedLocales();
		m_description.StripUnusedLocales();
		m_shortDescription.StripUnusedLocales();
		m_lockedShortName.StripUnusedLocales();
		m_lockedDescription.StripUnusedLocales();
		m_lockedShortDescription.StripUnusedLocales();
		m_requirementsDescription.StripUnusedLocales();
		m_rewardsDescription.StripUnusedLocales();
		m_completeBannerText.StripUnusedLocales();
	}
}
