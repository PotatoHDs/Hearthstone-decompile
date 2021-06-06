using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class AchieveDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private Achieve.Type m_achType = Achieve.ParseTypeValue("invalid");

	[SerializeField]
	private bool m_enabled = true;

	[SerializeField]
	private string m_parentAch;

	[SerializeField]
	private string m_linkTo;

	[SerializeField]
	private int m_sharedAchieveId;

	[SerializeField]
	private Achieve.ClientFlags m_clientFlags;

	[SerializeField]
	private Achieve.Trigger m_triggered = Achieve.ParseTriggerValue("none");

	[SerializeField]
	private int m_achQuota;

	[SerializeField]
	private Achieve.GameMode m_gameMode = Achieve.ParseGameModeValue("any");

	[SerializeField]
	private int m_raceId;

	[SerializeField]
	private int m_cardSetId;

	[SerializeField]
	private int m_myHeroClassId;

	[SerializeField]
	private int m_enemyHeroClassId;

	[SerializeField]
	private int m_maxDefense;

	[SerializeField]
	private Achieve.PlayerType m_playerType = Achieve.ParsePlayerTypeValue("any");

	[SerializeField]
	private int m_leagueVersionMin;

	[SerializeField]
	private int m_leagueVersionMax;

	[SerializeField]
	private int m_scenarioId;

	[SerializeField]
	private int m_adventureId;

	[SerializeField]
	private int m_adventureModeId;

	[SerializeField]
	private int m_adventureWingId;

	[SerializeField]
	private int m_boosterId;

	[SerializeField]
	private Achieve.RewardTiming m_rewardTiming = Achieve.ParseRewardTimingValue("immediate");

	[SerializeField]
	private string m_reward = "none";

	[SerializeField]
	private long m_rewardData1;

	[SerializeField]
	private long m_rewardData2;

	[SerializeField]
	private Achieve.Unlocks m_unlocks = Achieve.ParseUnlocksValue("none");

	[SerializeField]
	private DbfLocValue m_name;

	[SerializeField]
	private DbfLocValue m_description;

	[SerializeField]
	private Achieve.AltTextPredicate m_altTextPredicate = Achieve.ParseAltTextPredicateValue("none");

	[SerializeField]
	private DbfLocValue m_altName;

	[SerializeField]
	private DbfLocValue m_altDescription;

	[SerializeField]
	private string m_customVisualWidget;

	[SerializeField]
	private bool m_useGenericRewardVisual;

	[SerializeField]
	private Achieve.ShowToReturningPlayer m_showToReturningPlayer = Achieve.ParseShowToReturningPlayerValue("always");

	[SerializeField]
	private int m_questDialogId;

	[SerializeField]
	private bool m_autoDestroy;

	[SerializeField]
	private string m_questTilePrefab;

	[SerializeField]
	private Achieve.AttentionBlocker m_attentionBlocker = Achieve.ParseAttentionBlockerValue("NONE");

	[SerializeField]
	private bool m_enabledWithProgression = true;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("ACH_TYPE")]
	public Achieve.Type AchType => m_achType;

	[DbfField("ENABLED")]
	public bool Enabled => m_enabled;

	[DbfField("PARENT_ACH")]
	public string ParentAch => m_parentAch;

	[DbfField("LINK_TO")]
	public string LinkTo => m_linkTo;

	[DbfField("SHARED_ACHIEVE_ID")]
	public int SharedAchieveId => m_sharedAchieveId;

	public AchieveDbfRecord SharedAchieveRecord => GameDbf.Achieve.GetRecord(m_sharedAchieveId);

	[DbfField("CLIENT_FLAGS")]
	public Achieve.ClientFlags ClientFlags => m_clientFlags;

	[DbfField("TRIGGERED")]
	public Achieve.Trigger Triggered => m_triggered;

	[DbfField("ACH_QUOTA")]
	public int AchQuota => m_achQuota;

	[DbfField("GAME_MODE")]
	public Achieve.GameMode GameMode => m_gameMode;

	[DbfField("RACE")]
	public int Race => m_raceId;

	[DbfField("CARD_SET")]
	public int CardSet => m_cardSetId;

	public CardSetDbfRecord CardSetRecord => GameDbf.CardSet.GetRecord(m_cardSetId);

	[DbfField("MY_HERO_CLASS_ID")]
	public int MyHeroClassId => m_myHeroClassId;

	public ClassDbfRecord MyHeroClassRecord => GameDbf.Class.GetRecord(m_myHeroClassId);

	[DbfField("ENEMY_HERO_CLASS_ID")]
	public int EnemyHeroClassId => m_enemyHeroClassId;

	public ClassDbfRecord EnemyHeroClassRecord => GameDbf.Class.GetRecord(m_enemyHeroClassId);

	[DbfField("MAX_DEFENSE")]
	public int MaxDefense => m_maxDefense;

	[DbfField("PLAYER_TYPE")]
	public Achieve.PlayerType PlayerType => m_playerType;

	[DbfField("LEAGUE_VERSION_MIN")]
	public int LeagueVersionMin => m_leagueVersionMin;

	[DbfField("LEAGUE_VERSION_MAX")]
	public int LeagueVersionMax => m_leagueVersionMax;

	[Obsolete("Use ACHIEVE_CONDITION.SCENARIO_ID instead")]
	[DbfField("SCENARIO_ID")]
	public int ScenarioId => m_scenarioId;

	public ScenarioDbfRecord ScenarioRecord => GameDbf.Scenario.GetRecord(m_scenarioId);

	[DbfField("ADVENTURE_ID")]
	public int AdventureId => m_adventureId;

	public AdventureDbfRecord AdventureRecord => GameDbf.Adventure.GetRecord(m_adventureId);

	[DbfField("ADVENTURE_MODE_ID")]
	public int AdventureModeId => m_adventureModeId;

	public AdventureModeDbfRecord AdventureModeRecord => GameDbf.AdventureMode.GetRecord(m_adventureModeId);

	[DbfField("ADVENTURE_WING_ID")]
	public int AdventureWingId => m_adventureWingId;

	public WingDbfRecord AdventureWingRecord => GameDbf.Wing.GetRecord(m_adventureWingId);

	[DbfField("BOOSTER")]
	public int Booster => m_boosterId;

	public BoosterDbfRecord BoosterRecord => GameDbf.Booster.GetRecord(m_boosterId);

	[DbfField("REWARD_TIMING")]
	public Achieve.RewardTiming RewardTiming => m_rewardTiming;

	[DbfField("REWARD")]
	public string Reward => m_reward;

	[DbfField("REWARD_DATA1")]
	public long RewardData1 => m_rewardData1;

	[DbfField("REWARD_DATA2")]
	public long RewardData2 => m_rewardData2;

	[DbfField("UNLOCKS")]
	public Achieve.Unlocks Unlocks => m_unlocks;

	[DbfField("NAME")]
	public DbfLocValue Name => m_name;

	[DbfField("DESCRIPTION")]
	public DbfLocValue Description => m_description;

	[DbfField("ALT_TEXT_PREDICATE")]
	public Achieve.AltTextPredicate AltTextPredicate => m_altTextPredicate;

	[DbfField("ALT_NAME")]
	public DbfLocValue AltName => m_altName;

	[DbfField("ALT_DESCRIPTION")]
	public DbfLocValue AltDescription => m_altDescription;

	[DbfField("CUSTOM_VISUAL_WIDGET")]
	public string CustomVisualWidget => m_customVisualWidget;

	[DbfField("USE_GENERIC_REWARD_VISUAL")]
	public bool UseGenericRewardVisual => m_useGenericRewardVisual;

	[DbfField("SHOW_TO_RETURNING_PLAYER")]
	public Achieve.ShowToReturningPlayer ShowToReturningPlayer => m_showToReturningPlayer;

	[DbfField("QUEST_DIALOG_ID")]
	public int QuestDialogId => m_questDialogId;

	public CharacterDialogDbfRecord QuestDialogRecord => GameDbf.CharacterDialog.GetRecord(m_questDialogId);

	[DbfField("AUTO_DESTROY")]
	public bool AutoDestroy => m_autoDestroy;

	[DbfField("QUEST_TILE_PREFAB")]
	public string QuestTilePrefab => m_questTilePrefab;

	[DbfField("ATTENTION_BLOCKER")]
	public Achieve.AttentionBlocker AttentionBlocker => m_attentionBlocker;

	[DbfField("ENABLED_WITH_PROGRESSION")]
	public bool EnabledWithProgression => m_enabledWithProgression;

	public List<AchieveConditionDbfRecord> Conditions => GameDbf.AchieveCondition.GetRecords((AchieveConditionDbfRecord r) => r.AchieveId == base.ID);

	public List<AchieveRegionDataDbfRecord> RegionDataList => GameDbf.AchieveRegionData.GetRecords((AchieveRegionDataDbfRecord r) => r.AchieveId == base.ID);

	public List<VisualBlacklistDbfRecord> VisualBlacklist => GameDbf.VisualBlacklist.GetRecords((VisualBlacklistDbfRecord r) => r.AchieveId == base.ID);

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetAchType(Achieve.Type v)
	{
		m_achType = v;
	}

	public void SetEnabled(bool v)
	{
		m_enabled = v;
	}

	public void SetParentAch(string v)
	{
		m_parentAch = v;
	}

	public void SetLinkTo(string v)
	{
		m_linkTo = v;
	}

	public void SetSharedAchieveId(int v)
	{
		m_sharedAchieveId = v;
	}

	public void SetClientFlags(Achieve.ClientFlags v)
	{
		m_clientFlags = v;
	}

	public void SetTriggered(Achieve.Trigger v)
	{
		m_triggered = v;
	}

	public void SetAchQuota(int v)
	{
		m_achQuota = v;
	}

	public void SetGameMode(Achieve.GameMode v)
	{
		m_gameMode = v;
	}

	public void SetRace(int v)
	{
		m_raceId = v;
	}

	public void SetCardSet(int v)
	{
		m_cardSetId = v;
	}

	public void SetMyHeroClassId(int v)
	{
		m_myHeroClassId = v;
	}

	public void SetEnemyHeroClassId(int v)
	{
		m_enemyHeroClassId = v;
	}

	public void SetMaxDefense(int v)
	{
		m_maxDefense = v;
	}

	public void SetPlayerType(Achieve.PlayerType v)
	{
		m_playerType = v;
	}

	public void SetLeagueVersionMin(int v)
	{
		m_leagueVersionMin = v;
	}

	public void SetLeagueVersionMax(int v)
	{
		m_leagueVersionMax = v;
	}

	[Obsolete("Use ACHIEVE_CONDITION.SCENARIO_ID instead")]
	public void SetScenarioId(int v)
	{
		m_scenarioId = v;
	}

	public void SetAdventureId(int v)
	{
		m_adventureId = v;
	}

	public void SetAdventureModeId(int v)
	{
		m_adventureModeId = v;
	}

	public void SetAdventureWingId(int v)
	{
		m_adventureWingId = v;
	}

	public void SetBooster(int v)
	{
		m_boosterId = v;
	}

	public void SetRewardTiming(Achieve.RewardTiming v)
	{
		m_rewardTiming = v;
	}

	public void SetReward(string v)
	{
		m_reward = v;
	}

	public void SetRewardData1(long v)
	{
		m_rewardData1 = v;
	}

	public void SetRewardData2(long v)
	{
		m_rewardData2 = v;
	}

	public void SetUnlocks(Achieve.Unlocks v)
	{
		m_unlocks = v;
	}

	public void SetName(DbfLocValue v)
	{
		m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	public void SetDescription(DbfLocValue v)
	{
		m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	public void SetAltTextPredicate(Achieve.AltTextPredicate v)
	{
		m_altTextPredicate = v;
	}

	public void SetAltName(DbfLocValue v)
	{
		m_altName = v;
		v.SetDebugInfo(base.ID, "ALT_NAME");
	}

	public void SetAltDescription(DbfLocValue v)
	{
		m_altDescription = v;
		v.SetDebugInfo(base.ID, "ALT_DESCRIPTION");
	}

	public void SetCustomVisualWidget(string v)
	{
		m_customVisualWidget = v;
	}

	public void SetUseGenericRewardVisual(bool v)
	{
		m_useGenericRewardVisual = v;
	}

	public void SetShowToReturningPlayer(Achieve.ShowToReturningPlayer v)
	{
		m_showToReturningPlayer = v;
	}

	public void SetQuestDialogId(int v)
	{
		m_questDialogId = v;
	}

	public void SetAutoDestroy(bool v)
	{
		m_autoDestroy = v;
	}

	public void SetQuestTilePrefab(string v)
	{
		m_questTilePrefab = v;
	}

	public void SetAttentionBlocker(Achieve.AttentionBlocker v)
	{
		m_attentionBlocker = v;
	}

	public void SetEnabledWithProgression(bool v)
	{
		m_enabledWithProgression = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_DESC" => m_noteDesc, 
			"ACH_TYPE" => m_achType, 
			"ENABLED" => m_enabled, 
			"PARENT_ACH" => m_parentAch, 
			"LINK_TO" => m_linkTo, 
			"SHARED_ACHIEVE_ID" => m_sharedAchieveId, 
			"CLIENT_FLAGS" => m_clientFlags, 
			"TRIGGERED" => m_triggered, 
			"ACH_QUOTA" => m_achQuota, 
			"GAME_MODE" => m_gameMode, 
			"RACE" => m_raceId, 
			"CARD_SET" => m_cardSetId, 
			"MY_HERO_CLASS_ID" => m_myHeroClassId, 
			"ENEMY_HERO_CLASS_ID" => m_enemyHeroClassId, 
			"MAX_DEFENSE" => m_maxDefense, 
			"PLAYER_TYPE" => m_playerType, 
			"LEAGUE_VERSION_MIN" => m_leagueVersionMin, 
			"LEAGUE_VERSION_MAX" => m_leagueVersionMax, 
			"SCENARIO_ID" => m_scenarioId, 
			"ADVENTURE_ID" => m_adventureId, 
			"ADVENTURE_MODE_ID" => m_adventureModeId, 
			"ADVENTURE_WING_ID" => m_adventureWingId, 
			"BOOSTER" => m_boosterId, 
			"REWARD_TIMING" => m_rewardTiming, 
			"REWARD" => m_reward, 
			"REWARD_DATA1" => m_rewardData1, 
			"REWARD_DATA2" => m_rewardData2, 
			"UNLOCKS" => m_unlocks, 
			"NAME" => m_name, 
			"DESCRIPTION" => m_description, 
			"ALT_TEXT_PREDICATE" => m_altTextPredicate, 
			"ALT_NAME" => m_altName, 
			"ALT_DESCRIPTION" => m_altDescription, 
			"CUSTOM_VISUAL_WIDGET" => m_customVisualWidget, 
			"USE_GENERIC_REWARD_VISUAL" => m_useGenericRewardVisual, 
			"SHOW_TO_RETURNING_PLAYER" => m_showToReturningPlayer, 
			"QUEST_DIALOG_ID" => m_questDialogId, 
			"AUTO_DESTROY" => m_autoDestroy, 
			"QUEST_TILE_PREFAB" => m_questTilePrefab, 
			"ATTENTION_BLOCKER" => m_attentionBlocker, 
			"ENABLED_WITH_PROGRESSION" => m_enabledWithProgression, 
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
		case "ACH_TYPE":
			if (val == null)
			{
				m_achType = Achieve.Type.INVALID;
			}
			else if (val is Achieve.Type || val is int)
			{
				m_achType = (Achieve.Type)val;
			}
			else if (val is string)
			{
				m_achType = Achieve.ParseTypeValue((string)val);
			}
			break;
		case "ENABLED":
			m_enabled = (bool)val;
			break;
		case "PARENT_ACH":
			m_parentAch = (string)val;
			break;
		case "LINK_TO":
			m_linkTo = (string)val;
			break;
		case "SHARED_ACHIEVE_ID":
			m_sharedAchieveId = (int)val;
			break;
		case "CLIENT_FLAGS":
			if (val == null)
			{
				m_clientFlags = Achieve.ClientFlags.NONE;
			}
			else if (val is Achieve.ClientFlags || val is int)
			{
				m_clientFlags = (Achieve.ClientFlags)val;
			}
			else if (val is string)
			{
				m_clientFlags = Achieve.ParseClientFlagsValue((string)val);
			}
			break;
		case "TRIGGERED":
			if (val == null)
			{
				m_triggered = Achieve.Trigger.UNKNOWN;
			}
			else if (val is Achieve.Trigger || val is int)
			{
				m_triggered = (Achieve.Trigger)val;
			}
			else if (val is string)
			{
				m_triggered = Achieve.ParseTriggerValue((string)val);
			}
			break;
		case "ACH_QUOTA":
			m_achQuota = (int)val;
			break;
		case "GAME_MODE":
			if (val == null)
			{
				m_gameMode = Achieve.GameMode.ANY;
			}
			else if (val is Achieve.GameMode || val is int)
			{
				m_gameMode = (Achieve.GameMode)val;
			}
			else if (val is string)
			{
				m_gameMode = Achieve.ParseGameModeValue((string)val);
			}
			break;
		case "RACE":
			m_raceId = (int)val;
			break;
		case "CARD_SET":
			m_cardSetId = (int)val;
			break;
		case "MY_HERO_CLASS_ID":
			m_myHeroClassId = (int)val;
			break;
		case "ENEMY_HERO_CLASS_ID":
			m_enemyHeroClassId = (int)val;
			break;
		case "MAX_DEFENSE":
			m_maxDefense = (int)val;
			break;
		case "PLAYER_TYPE":
			if (val == null)
			{
				m_playerType = Achieve.PlayerType.ANY;
			}
			else if (val is Achieve.PlayerType || val is int)
			{
				m_playerType = (Achieve.PlayerType)val;
			}
			else if (val is string)
			{
				m_playerType = Achieve.ParsePlayerTypeValue((string)val);
			}
			break;
		case "LEAGUE_VERSION_MIN":
			m_leagueVersionMin = (int)val;
			break;
		case "LEAGUE_VERSION_MAX":
			m_leagueVersionMax = (int)val;
			break;
		case "SCENARIO_ID":
			m_scenarioId = (int)val;
			break;
		case "ADVENTURE_ID":
			m_adventureId = (int)val;
			break;
		case "ADVENTURE_MODE_ID":
			m_adventureModeId = (int)val;
			break;
		case "ADVENTURE_WING_ID":
			m_adventureWingId = (int)val;
			break;
		case "BOOSTER":
			m_boosterId = (int)val;
			break;
		case "REWARD_TIMING":
			if (val == null)
			{
				m_rewardTiming = Achieve.RewardTiming.IMMEDIATE;
			}
			else if (val is Achieve.RewardTiming || val is int)
			{
				m_rewardTiming = (Achieve.RewardTiming)val;
			}
			else if (val is string)
			{
				m_rewardTiming = Achieve.ParseRewardTimingValue((string)val);
			}
			break;
		case "REWARD":
			m_reward = (string)val;
			break;
		case "REWARD_DATA1":
			m_rewardData1 = (long)val;
			break;
		case "REWARD_DATA2":
			m_rewardData2 = (long)val;
			break;
		case "UNLOCKS":
			if (val == null)
			{
				m_unlocks = Achieve.Unlocks.FORGE;
			}
			else if (val is Achieve.Unlocks || val is int)
			{
				m_unlocks = (Achieve.Unlocks)val;
			}
			else if (val is string)
			{
				m_unlocks = Achieve.ParseUnlocksValue((string)val);
			}
			break;
		case "NAME":
			m_name = (DbfLocValue)val;
			break;
		case "DESCRIPTION":
			m_description = (DbfLocValue)val;
			break;
		case "ALT_TEXT_PREDICATE":
			if (val == null)
			{
				m_altTextPredicate = Achieve.AltTextPredicate.NONE;
			}
			else if (val is Achieve.AltTextPredicate || val is int)
			{
				m_altTextPredicate = (Achieve.AltTextPredicate)val;
			}
			else if (val is string)
			{
				m_altTextPredicate = Achieve.ParseAltTextPredicateValue((string)val);
			}
			break;
		case "ALT_NAME":
			m_altName = (DbfLocValue)val;
			break;
		case "ALT_DESCRIPTION":
			m_altDescription = (DbfLocValue)val;
			break;
		case "CUSTOM_VISUAL_WIDGET":
			m_customVisualWidget = (string)val;
			break;
		case "USE_GENERIC_REWARD_VISUAL":
			m_useGenericRewardVisual = (bool)val;
			break;
		case "SHOW_TO_RETURNING_PLAYER":
			if (val == null)
			{
				m_showToReturningPlayer = Achieve.ShowToReturningPlayer.ALWAYS;
			}
			else if (val is Achieve.ShowToReturningPlayer || val is int)
			{
				m_showToReturningPlayer = (Achieve.ShowToReturningPlayer)val;
			}
			else if (val is string)
			{
				m_showToReturningPlayer = Achieve.ParseShowToReturningPlayerValue((string)val);
			}
			break;
		case "QUEST_DIALOG_ID":
			m_questDialogId = (int)val;
			break;
		case "AUTO_DESTROY":
			m_autoDestroy = (bool)val;
			break;
		case "QUEST_TILE_PREFAB":
			m_questTilePrefab = (string)val;
			break;
		case "ATTENTION_BLOCKER":
			if (val == null)
			{
				m_attentionBlocker = Achieve.AttentionBlocker.NONE;
			}
			else if (val is Achieve.AttentionBlocker || val is int)
			{
				m_attentionBlocker = (Achieve.AttentionBlocker)val;
			}
			else if (val is string)
			{
				m_attentionBlocker = Achieve.ParseAttentionBlockerValue((string)val);
			}
			break;
		case "ENABLED_WITH_PROGRESSION":
			m_enabledWithProgression = (bool)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"ACH_TYPE" => typeof(Achieve.Type), 
			"ENABLED" => typeof(bool), 
			"PARENT_ACH" => typeof(string), 
			"LINK_TO" => typeof(string), 
			"SHARED_ACHIEVE_ID" => typeof(int), 
			"CLIENT_FLAGS" => typeof(Achieve.ClientFlags), 
			"TRIGGERED" => typeof(Achieve.Trigger), 
			"ACH_QUOTA" => typeof(int), 
			"GAME_MODE" => typeof(Achieve.GameMode), 
			"RACE" => typeof(int), 
			"CARD_SET" => typeof(int), 
			"MY_HERO_CLASS_ID" => typeof(int), 
			"ENEMY_HERO_CLASS_ID" => typeof(int), 
			"MAX_DEFENSE" => typeof(int), 
			"PLAYER_TYPE" => typeof(Achieve.PlayerType), 
			"LEAGUE_VERSION_MIN" => typeof(int), 
			"LEAGUE_VERSION_MAX" => typeof(int), 
			"SCENARIO_ID" => typeof(int), 
			"ADVENTURE_ID" => typeof(int), 
			"ADVENTURE_MODE_ID" => typeof(int), 
			"ADVENTURE_WING_ID" => typeof(int), 
			"BOOSTER" => typeof(int), 
			"REWARD_TIMING" => typeof(Achieve.RewardTiming), 
			"REWARD" => typeof(string), 
			"REWARD_DATA1" => typeof(long), 
			"REWARD_DATA2" => typeof(long), 
			"UNLOCKS" => typeof(Achieve.Unlocks), 
			"NAME" => typeof(DbfLocValue), 
			"DESCRIPTION" => typeof(DbfLocValue), 
			"ALT_TEXT_PREDICATE" => typeof(Achieve.AltTextPredicate), 
			"ALT_NAME" => typeof(DbfLocValue), 
			"ALT_DESCRIPTION" => typeof(DbfLocValue), 
			"CUSTOM_VISUAL_WIDGET" => typeof(string), 
			"USE_GENERIC_REWARD_VISUAL" => typeof(bool), 
			"SHOW_TO_RETURNING_PLAYER" => typeof(Achieve.ShowToReturningPlayer), 
			"QUEST_DIALOG_ID" => typeof(int), 
			"AUTO_DESTROY" => typeof(bool), 
			"QUEST_TILE_PREFAB" => typeof(string), 
			"ATTENTION_BLOCKER" => typeof(Achieve.AttentionBlocker), 
			"ENABLED_WITH_PROGRESSION" => typeof(bool), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAchieveDbfRecords loadRecords = new LoadAchieveDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AchieveDbfAsset achieveDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AchieveDbfAsset)) as AchieveDbfAsset;
		if (achieveDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"AchieveDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < achieveDbfAsset.Records.Count; i++)
		{
			achieveDbfAsset.Records[i].StripUnusedLocales();
		}
		records = achieveDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_name.StripUnusedLocales();
		m_description.StripUnusedLocales();
		m_altName.StripUnusedLocales();
		m_altDescription.StripUnusedLocales();
	}
}
