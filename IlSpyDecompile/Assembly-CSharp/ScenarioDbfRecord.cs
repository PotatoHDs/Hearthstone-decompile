using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class ScenarioDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private int m_players;

	[SerializeField]
	private int m_player1HeroCardId;

	[SerializeField]
	private int m_player2HeroCardId;

	[SerializeField]
	private bool m_isTutorial;

	[SerializeField]
	private bool m_isExpert = true;

	[SerializeField]
	private bool m_isCoop;

	[SerializeField]
	private bool m_oneSimPerPlayer;

	[SerializeField]
	private int m_adventureId;

	[SerializeField]
	private int m_wingId;

	[SerializeField]
	private int m_sortOrder;

	[SerializeField]
	private int m_modeId;

	[SerializeField]
	private int m_clientPlayer2HeroCardId;

	[SerializeField]
	private int m_clientPlayer2HeroPowerCardId;

	[SerializeField]
	private DbfLocValue m_name;

	[SerializeField]
	private DbfLocValue m_shortName;

	[SerializeField]
	private DbfLocValue m_description;

	[SerializeField]
	private DbfLocValue m_shortDescription;

	[SerializeField]
	private DbfLocValue m_opponentName;

	[SerializeField]
	private DbfLocValue m_completedDescription;

	[SerializeField]
	private int m_player1DeckId;

	[SerializeField]
	private int m_deckRulesetId;

	[SerializeField]
	private Scenario.RuleType m_ruleType;

	[SerializeField]
	private DbfLocValue m_chooseHeroText;

	[SerializeField]
	private string m_tbTexture;

	[SerializeField]
	private string m_tbTexturePhone;

	[SerializeField]
	private double m_tbTexturePhoneOffsetY;

	[SerializeField]
	private int m_gameSaveDataProgressSubkeyId;

	[SerializeField]
	private int m_gameSaveDataProgressMax;

	[SerializeField]
	private bool m_hideBossHeroPowerInUi;

	[SerializeField]
	private string m_scriptObject;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("PLAYERS")]
	public int Players => m_players;

	[DbfField("PLAYER1_HERO_CARD_ID")]
	public int Player1HeroCardId => m_player1HeroCardId;

	public CardDbfRecord Player1HeroCardRecord => GameDbf.Card.GetRecord(m_player1HeroCardId);

	[DbfField("PLAYER2_HERO_CARD_ID")]
	public int Player2HeroCardId => m_player2HeroCardId;

	public CardDbfRecord Player2HeroCardRecord => GameDbf.Card.GetRecord(m_player2HeroCardId);

	[DbfField("IS_TUTORIAL")]
	public bool IsTutorial => m_isTutorial;

	[DbfField("IS_EXPERT")]
	public bool IsExpert => m_isExpert;

	[DbfField("IS_COOP")]
	public bool IsCoop => m_isCoop;

	[DbfField("ONE_SIM_PER_PLAYER")]
	public bool OneSimPerPlayer => m_oneSimPerPlayer;

	[DbfField("ADVENTURE_ID")]
	public int AdventureId => m_adventureId;

	public AdventureDbfRecord AdventureRecord => GameDbf.Adventure.GetRecord(m_adventureId);

	[DbfField("WING_ID")]
	public int WingId => m_wingId;

	public WingDbfRecord WingRecord => GameDbf.Wing.GetRecord(m_wingId);

	[DbfField("SORT_ORDER")]
	public int SortOrder => m_sortOrder;

	[DbfField("MODE_ID")]
	public int ModeId => m_modeId;

	public AdventureModeDbfRecord ModeRecord => GameDbf.AdventureMode.GetRecord(m_modeId);

	[DbfField("CLIENT_PLAYER2_HERO_CARD_ID")]
	public int ClientPlayer2HeroCardId => m_clientPlayer2HeroCardId;

	public CardDbfRecord ClientPlayer2HeroCardRecord => GameDbf.Card.GetRecord(m_clientPlayer2HeroCardId);

	[DbfField("CLIENT_PLAYER2_HERO_POWER_CARD_ID")]
	public int ClientPlayer2HeroPowerCardId => m_clientPlayer2HeroPowerCardId;

	public CardDbfRecord ClientPlayer2HeroPowerCardRecord => GameDbf.Card.GetRecord(m_clientPlayer2HeroPowerCardId);

	[DbfField("NAME")]
	public DbfLocValue Name => m_name;

	[DbfField("SHORT_NAME")]
	public DbfLocValue ShortName => m_shortName;

	[DbfField("DESCRIPTION")]
	public DbfLocValue Description => m_description;

	[DbfField("SHORT_DESCRIPTION")]
	public DbfLocValue ShortDescription => m_shortDescription;

	[DbfField("OPPONENT_NAME")]
	public DbfLocValue OpponentName => m_opponentName;

	[DbfField("COMPLETED_DESCRIPTION")]
	public DbfLocValue CompletedDescription => m_completedDescription;

	[DbfField("PLAYER1_DECK_ID")]
	public int Player1DeckId => m_player1DeckId;

	public DeckDbfRecord Player1DeckRecord => GameDbf.Deck.GetRecord(m_player1DeckId);

	[DbfField("DECK_RULESET_ID")]
	public int DeckRulesetId => m_deckRulesetId;

	public DeckRulesetDbfRecord DeckRulesetRecord => GameDbf.DeckRuleset.GetRecord(m_deckRulesetId);

	[DbfField("RULE_TYPE")]
	public Scenario.RuleType RuleType => m_ruleType;

	[DbfField("CHOOSE_HERO_TEXT")]
	public DbfLocValue ChooseHeroText => m_chooseHeroText;

	[DbfField("TB_TEXTURE")]
	public string TbTexture => m_tbTexture;

	[DbfField("TB_TEXTURE_PHONE")]
	public string TbTexturePhone => m_tbTexturePhone;

	[DbfField("TB_TEXTURE_PHONE_OFFSET_Y")]
	public double TbTexturePhoneOffsetY => m_tbTexturePhoneOffsetY;

	[DbfField("GAME_SAVE_DATA_PROGRESS_SUBKEY")]
	public int GameSaveDataProgressSubkey => m_gameSaveDataProgressSubkeyId;

	public GameSaveSubkeyDbfRecord GameSaveDataProgressSubkeyRecord => GameDbf.GameSaveSubkey.GetRecord(m_gameSaveDataProgressSubkeyId);

	[DbfField("GAME_SAVE_DATA_PROGRESS_MAX")]
	public int GameSaveDataProgressMax => m_gameSaveDataProgressMax;

	[DbfField("HIDE_BOSS_HERO_POWER_IN_UI")]
	public bool HideBossHeroPowerInUi => m_hideBossHeroPowerInUi;

	[DbfField("SCRIPT_OBJECT")]
	public string ScriptObject => m_scriptObject;

	public List<ClassExclusionsDbfRecord> ClassExclusions => GameDbf.ClassExclusions.GetRecords((ClassExclusionsDbfRecord r) => r.ScenarioId == base.ID);

	public List<ScenarioGuestHeroesDbfRecord> GuestHeroes => GameDbf.ScenarioGuestHeroes.GetRecords((ScenarioGuestHeroesDbfRecord r) => r.ScenarioId == base.ID);

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetPlayers(int v)
	{
		m_players = v;
	}

	public void SetPlayer1HeroCardId(int v)
	{
		m_player1HeroCardId = v;
	}

	public void SetPlayer2HeroCardId(int v)
	{
		m_player2HeroCardId = v;
	}

	public void SetIsTutorial(bool v)
	{
		m_isTutorial = v;
	}

	public void SetIsExpert(bool v)
	{
		m_isExpert = v;
	}

	public void SetIsCoop(bool v)
	{
		m_isCoop = v;
	}

	public void SetOneSimPerPlayer(bool v)
	{
		m_oneSimPerPlayer = v;
	}

	public void SetAdventureId(int v)
	{
		m_adventureId = v;
	}

	public void SetWingId(int v)
	{
		m_wingId = v;
	}

	public void SetSortOrder(int v)
	{
		m_sortOrder = v;
	}

	public void SetModeId(int v)
	{
		m_modeId = v;
	}

	public void SetClientPlayer2HeroCardId(int v)
	{
		m_clientPlayer2HeroCardId = v;
	}

	public void SetClientPlayer2HeroPowerCardId(int v)
	{
		m_clientPlayer2HeroPowerCardId = v;
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

	public void SetOpponentName(DbfLocValue v)
	{
		m_opponentName = v;
		v.SetDebugInfo(base.ID, "OPPONENT_NAME");
	}

	public void SetCompletedDescription(DbfLocValue v)
	{
		m_completedDescription = v;
		v.SetDebugInfo(base.ID, "COMPLETED_DESCRIPTION");
	}

	public void SetPlayer1DeckId(int v)
	{
		m_player1DeckId = v;
	}

	public void SetDeckRulesetId(int v)
	{
		m_deckRulesetId = v;
	}

	public void SetRuleType(Scenario.RuleType v)
	{
		m_ruleType = v;
	}

	public void SetChooseHeroText(DbfLocValue v)
	{
		m_chooseHeroText = v;
		v.SetDebugInfo(base.ID, "CHOOSE_HERO_TEXT");
	}

	public void SetTbTexture(string v)
	{
		m_tbTexture = v;
	}

	public void SetTbTexturePhone(string v)
	{
		m_tbTexturePhone = v;
	}

	public void SetTbTexturePhoneOffsetY(double v)
	{
		m_tbTexturePhoneOffsetY = v;
	}

	public void SetGameSaveDataProgressSubkey(int v)
	{
		m_gameSaveDataProgressSubkeyId = v;
	}

	public void SetGameSaveDataProgressMax(int v)
	{
		m_gameSaveDataProgressMax = v;
	}

	public void SetHideBossHeroPowerInUi(bool v)
	{
		m_hideBossHeroPowerInUi = v;
	}

	public void SetScriptObject(string v)
	{
		m_scriptObject = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_DESC" => m_noteDesc, 
			"PLAYERS" => m_players, 
			"PLAYER1_HERO_CARD_ID" => m_player1HeroCardId, 
			"PLAYER2_HERO_CARD_ID" => m_player2HeroCardId, 
			"IS_TUTORIAL" => m_isTutorial, 
			"IS_EXPERT" => m_isExpert, 
			"IS_COOP" => m_isCoop, 
			"ONE_SIM_PER_PLAYER" => m_oneSimPerPlayer, 
			"ADVENTURE_ID" => m_adventureId, 
			"WING_ID" => m_wingId, 
			"SORT_ORDER" => m_sortOrder, 
			"MODE_ID" => m_modeId, 
			"CLIENT_PLAYER2_HERO_CARD_ID" => m_clientPlayer2HeroCardId, 
			"CLIENT_PLAYER2_HERO_POWER_CARD_ID" => m_clientPlayer2HeroPowerCardId, 
			"NAME" => m_name, 
			"SHORT_NAME" => m_shortName, 
			"DESCRIPTION" => m_description, 
			"SHORT_DESCRIPTION" => m_shortDescription, 
			"OPPONENT_NAME" => m_opponentName, 
			"COMPLETED_DESCRIPTION" => m_completedDescription, 
			"PLAYER1_DECK_ID" => m_player1DeckId, 
			"DECK_RULESET_ID" => m_deckRulesetId, 
			"RULE_TYPE" => m_ruleType, 
			"CHOOSE_HERO_TEXT" => m_chooseHeroText, 
			"TB_TEXTURE" => m_tbTexture, 
			"TB_TEXTURE_PHONE" => m_tbTexturePhone, 
			"TB_TEXTURE_PHONE_OFFSET_Y" => m_tbTexturePhoneOffsetY, 
			"GAME_SAVE_DATA_PROGRESS_SUBKEY" => m_gameSaveDataProgressSubkeyId, 
			"GAME_SAVE_DATA_PROGRESS_MAX" => m_gameSaveDataProgressMax, 
			"HIDE_BOSS_HERO_POWER_IN_UI" => m_hideBossHeroPowerInUi, 
			"SCRIPT_OBJECT" => m_scriptObject, 
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
		case "PLAYERS":
			m_players = (int)val;
			break;
		case "PLAYER1_HERO_CARD_ID":
			m_player1HeroCardId = (int)val;
			break;
		case "PLAYER2_HERO_CARD_ID":
			m_player2HeroCardId = (int)val;
			break;
		case "IS_TUTORIAL":
			m_isTutorial = (bool)val;
			break;
		case "IS_EXPERT":
			m_isExpert = (bool)val;
			break;
		case "IS_COOP":
			m_isCoop = (bool)val;
			break;
		case "ONE_SIM_PER_PLAYER":
			m_oneSimPerPlayer = (bool)val;
			break;
		case "ADVENTURE_ID":
			m_adventureId = (int)val;
			break;
		case "WING_ID":
			m_wingId = (int)val;
			break;
		case "SORT_ORDER":
			m_sortOrder = (int)val;
			break;
		case "MODE_ID":
			m_modeId = (int)val;
			break;
		case "CLIENT_PLAYER2_HERO_CARD_ID":
			m_clientPlayer2HeroCardId = (int)val;
			break;
		case "CLIENT_PLAYER2_HERO_POWER_CARD_ID":
			m_clientPlayer2HeroPowerCardId = (int)val;
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
		case "OPPONENT_NAME":
			m_opponentName = (DbfLocValue)val;
			break;
		case "COMPLETED_DESCRIPTION":
			m_completedDescription = (DbfLocValue)val;
			break;
		case "PLAYER1_DECK_ID":
			m_player1DeckId = (int)val;
			break;
		case "DECK_RULESET_ID":
			m_deckRulesetId = (int)val;
			break;
		case "RULE_TYPE":
			if (val == null)
			{
				m_ruleType = Scenario.RuleType.NONE;
			}
			else if (val is Scenario.RuleType || val is int)
			{
				m_ruleType = (Scenario.RuleType)val;
			}
			else if (val is string)
			{
				m_ruleType = Scenario.ParseRuleTypeValue((string)val);
			}
			break;
		case "CHOOSE_HERO_TEXT":
			m_chooseHeroText = (DbfLocValue)val;
			break;
		case "TB_TEXTURE":
			m_tbTexture = (string)val;
			break;
		case "TB_TEXTURE_PHONE":
			m_tbTexturePhone = (string)val;
			break;
		case "TB_TEXTURE_PHONE_OFFSET_Y":
			m_tbTexturePhoneOffsetY = (double)val;
			break;
		case "GAME_SAVE_DATA_PROGRESS_SUBKEY":
			m_gameSaveDataProgressSubkeyId = (int)val;
			break;
		case "GAME_SAVE_DATA_PROGRESS_MAX":
			m_gameSaveDataProgressMax = (int)val;
			break;
		case "HIDE_BOSS_HERO_POWER_IN_UI":
			m_hideBossHeroPowerInUi = (bool)val;
			break;
		case "SCRIPT_OBJECT":
			m_scriptObject = (string)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"PLAYERS" => typeof(int), 
			"PLAYER1_HERO_CARD_ID" => typeof(int), 
			"PLAYER2_HERO_CARD_ID" => typeof(int), 
			"IS_TUTORIAL" => typeof(bool), 
			"IS_EXPERT" => typeof(bool), 
			"IS_COOP" => typeof(bool), 
			"ONE_SIM_PER_PLAYER" => typeof(bool), 
			"ADVENTURE_ID" => typeof(int), 
			"WING_ID" => typeof(int), 
			"SORT_ORDER" => typeof(int), 
			"MODE_ID" => typeof(int), 
			"CLIENT_PLAYER2_HERO_CARD_ID" => typeof(int), 
			"CLIENT_PLAYER2_HERO_POWER_CARD_ID" => typeof(int), 
			"NAME" => typeof(DbfLocValue), 
			"SHORT_NAME" => typeof(DbfLocValue), 
			"DESCRIPTION" => typeof(DbfLocValue), 
			"SHORT_DESCRIPTION" => typeof(DbfLocValue), 
			"OPPONENT_NAME" => typeof(DbfLocValue), 
			"COMPLETED_DESCRIPTION" => typeof(DbfLocValue), 
			"PLAYER1_DECK_ID" => typeof(int), 
			"DECK_RULESET_ID" => typeof(int), 
			"RULE_TYPE" => typeof(Scenario.RuleType), 
			"CHOOSE_HERO_TEXT" => typeof(DbfLocValue), 
			"TB_TEXTURE" => typeof(string), 
			"TB_TEXTURE_PHONE" => typeof(string), 
			"TB_TEXTURE_PHONE_OFFSET_Y" => typeof(double), 
			"GAME_SAVE_DATA_PROGRESS_SUBKEY" => typeof(int), 
			"GAME_SAVE_DATA_PROGRESS_MAX" => typeof(int), 
			"HIDE_BOSS_HERO_POWER_IN_UI" => typeof(bool), 
			"SCRIPT_OBJECT" => typeof(string), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadScenarioDbfRecords loadRecords = new LoadScenarioDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ScenarioDbfAsset scenarioDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ScenarioDbfAsset)) as ScenarioDbfAsset;
		if (scenarioDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"ScenarioDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < scenarioDbfAsset.Records.Count; i++)
		{
			scenarioDbfAsset.Records[i].StripUnusedLocales();
		}
		records = scenarioDbfAsset.Records as List<T>;
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
		m_opponentName.StripUnusedLocales();
		m_completedDescription.StripUnusedLocales();
		m_chooseHeroText.StripUnusedLocales();
	}
}
