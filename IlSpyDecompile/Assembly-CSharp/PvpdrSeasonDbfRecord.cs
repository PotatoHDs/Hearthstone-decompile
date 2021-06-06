using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class PvpdrSeasonDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private string m_event;

	[SerializeField]
	private int m_adventureId;

	[SerializeField]
	private int m_scenarioId;

	[SerializeField]
	private int m_maxWins;

	[SerializeField]
	private int m_maxLosses;

	[SerializeField]
	private int m_deckDisplayRulesetId;

	[SerializeField]
	private int m_maxHeroesDrafted;

	[SerializeField]
	private int m_rewardChestId;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("EVENT")]
	public string Event => m_event;

	[DbfField("ADVENTURE_ID")]
	public int AdventureId => m_adventureId;

	public AdventureDbfRecord AdventureRecord => GameDbf.Adventure.GetRecord(m_adventureId);

	[DbfField("SCENARIO_ID")]
	public int ScenarioId => m_scenarioId;

	public ScenarioDbfRecord ScenarioRecord => GameDbf.Scenario.GetRecord(m_scenarioId);

	[DbfField("MAX_WINS")]
	public int MaxWins => m_maxWins;

	[DbfField("MAX_LOSSES")]
	public int MaxLosses => m_maxLosses;

	[DbfField("DECK_DISPLAY_RULESET_ID")]
	public int DeckDisplayRulesetId => m_deckDisplayRulesetId;

	public DeckRulesetDbfRecord DeckDisplayRulesetRecord => GameDbf.DeckRuleset.GetRecord(m_deckDisplayRulesetId);

	[DbfField("MAX_HEROES_DRAFTED")]
	public int MaxHeroesDrafted => m_maxHeroesDrafted;

	[DbfField("REWARD_CHEST_ID")]
	public int RewardChestId => m_rewardChestId;

	public RewardChestDbfRecord RewardChestRecord => GameDbf.RewardChest.GetRecord(m_rewardChestId);

	public List<GuestHeroSelectionRatioDbfRecord> GuestHeroSelectionRatio => GameDbf.GuestHeroSelectionRatio.GetRecords((GuestHeroSelectionRatioDbfRecord r) => r.PvpdrSeasonId == base.ID);

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetEvent(string v)
	{
		m_event = v;
	}

	public void SetAdventureId(int v)
	{
		m_adventureId = v;
	}

	public void SetScenarioId(int v)
	{
		m_scenarioId = v;
	}

	public void SetMaxWins(int v)
	{
		m_maxWins = v;
	}

	public void SetMaxLosses(int v)
	{
		m_maxLosses = v;
	}

	public void SetDeckDisplayRulesetId(int v)
	{
		m_deckDisplayRulesetId = v;
	}

	public void SetMaxHeroesDrafted(int v)
	{
		m_maxHeroesDrafted = v;
	}

	public void SetRewardChestId(int v)
	{
		m_rewardChestId = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_DESC" => m_noteDesc, 
			"EVENT" => m_event, 
			"ADVENTURE_ID" => m_adventureId, 
			"SCENARIO_ID" => m_scenarioId, 
			"MAX_WINS" => m_maxWins, 
			"MAX_LOSSES" => m_maxLosses, 
			"DECK_DISPLAY_RULESET_ID" => m_deckDisplayRulesetId, 
			"MAX_HEROES_DRAFTED" => m_maxHeroesDrafted, 
			"REWARD_CHEST_ID" => m_rewardChestId, 
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
		case "EVENT":
			m_event = (string)val;
			break;
		case "ADVENTURE_ID":
			m_adventureId = (int)val;
			break;
		case "SCENARIO_ID":
			m_scenarioId = (int)val;
			break;
		case "MAX_WINS":
			m_maxWins = (int)val;
			break;
		case "MAX_LOSSES":
			m_maxLosses = (int)val;
			break;
		case "DECK_DISPLAY_RULESET_ID":
			m_deckDisplayRulesetId = (int)val;
			break;
		case "MAX_HEROES_DRAFTED":
			m_maxHeroesDrafted = (int)val;
			break;
		case "REWARD_CHEST_ID":
			m_rewardChestId = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"EVENT" => typeof(string), 
			"ADVENTURE_ID" => typeof(int), 
			"SCENARIO_ID" => typeof(int), 
			"MAX_WINS" => typeof(int), 
			"MAX_LOSSES" => typeof(int), 
			"DECK_DISPLAY_RULESET_ID" => typeof(int), 
			"MAX_HEROES_DRAFTED" => typeof(int), 
			"REWARD_CHEST_ID" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadPvpdrSeasonDbfRecords loadRecords = new LoadPvpdrSeasonDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		PvpdrSeasonDbfAsset pvpdrSeasonDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(PvpdrSeasonDbfAsset)) as PvpdrSeasonDbfAsset;
		if (pvpdrSeasonDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"PvpdrSeasonDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < pvpdrSeasonDbfAsset.Records.Count; i++)
		{
			pvpdrSeasonDbfAsset.Records[i].StripUnusedLocales();
		}
		records = pvpdrSeasonDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
	}
}
