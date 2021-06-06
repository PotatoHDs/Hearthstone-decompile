using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class AdventureMissionDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_scenarioId;

	[SerializeField]
	private string m_noteDesc = "SYSDATE ";

	[SerializeField]
	private int m_reqWingId;

	[SerializeField]
	private int m_reqProgress;

	[SerializeField]
	private ulong m_reqFlags;

	[SerializeField]
	private int m_grantsWingId;

	[SerializeField]
	private int m_grantsProgress;

	[SerializeField]
	private ulong m_grantsFlags;

	[SerializeField]
	private string m_bossDefAssetPath;

	[SerializeField]
	private string m_classChallengePrefabPopup;

	[DbfField("SCENARIO_ID")]
	public int ScenarioId => m_scenarioId;

	public ScenarioDbfRecord ScenarioRecord => GameDbf.Scenario.GetRecord(m_scenarioId);

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("REQ_WING_ID")]
	public int ReqWingId => m_reqWingId;

	public WingDbfRecord ReqWingRecord => GameDbf.Wing.GetRecord(m_reqWingId);

	[DbfField("REQ_PROGRESS")]
	public int ReqProgress => m_reqProgress;

	[DbfField("REQ_FLAGS")]
	public ulong ReqFlags => m_reqFlags;

	[DbfField("GRANTS_WING_ID")]
	public int GrantsWingId => m_grantsWingId;

	public WingDbfRecord GrantsWingRecord => GameDbf.Wing.GetRecord(m_grantsWingId);

	[DbfField("GRANTS_PROGRESS")]
	public int GrantsProgress => m_grantsProgress;

	[DbfField("GRANTS_FLAGS")]
	public ulong GrantsFlags => m_grantsFlags;

	[DbfField("BOSS_DEF_ASSET_PATH")]
	public string BossDefAssetPath => m_bossDefAssetPath;

	[DbfField("CLASS_CHALLENGE_PREFAB_POPUP")]
	public string ClassChallengePrefabPopup => m_classChallengePrefabPopup;

	public void SetScenarioId(int v)
	{
		m_scenarioId = v;
	}

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetReqWingId(int v)
	{
		m_reqWingId = v;
	}

	public void SetReqProgress(int v)
	{
		m_reqProgress = v;
	}

	public void SetReqFlags(ulong v)
	{
		m_reqFlags = v;
	}

	public void SetGrantsWingId(int v)
	{
		m_grantsWingId = v;
	}

	public void SetGrantsProgress(int v)
	{
		m_grantsProgress = v;
	}

	public void SetGrantsFlags(ulong v)
	{
		m_grantsFlags = v;
	}

	public void SetBossDefAssetPath(string v)
	{
		m_bossDefAssetPath = v;
	}

	public void SetClassChallengePrefabPopup(string v)
	{
		m_classChallengePrefabPopup = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"SCENARIO_ID" => m_scenarioId, 
			"NOTE_DESC" => m_noteDesc, 
			"REQ_WING_ID" => m_reqWingId, 
			"REQ_PROGRESS" => m_reqProgress, 
			"REQ_FLAGS" => m_reqFlags, 
			"GRANTS_WING_ID" => m_grantsWingId, 
			"GRANTS_PROGRESS" => m_grantsProgress, 
			"GRANTS_FLAGS" => m_grantsFlags, 
			"BOSS_DEF_ASSET_PATH" => m_bossDefAssetPath, 
			"CLASS_CHALLENGE_PREFAB_POPUP" => m_classChallengePrefabPopup, 
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
		case "SCENARIO_ID":
			m_scenarioId = (int)val;
			break;
		case "NOTE_DESC":
			m_noteDesc = (string)val;
			break;
		case "REQ_WING_ID":
			m_reqWingId = (int)val;
			break;
		case "REQ_PROGRESS":
			m_reqProgress = (int)val;
			break;
		case "REQ_FLAGS":
			m_reqFlags = (ulong)val;
			break;
		case "GRANTS_WING_ID":
			m_grantsWingId = (int)val;
			break;
		case "GRANTS_PROGRESS":
			m_grantsProgress = (int)val;
			break;
		case "GRANTS_FLAGS":
			m_grantsFlags = (ulong)val;
			break;
		case "BOSS_DEF_ASSET_PATH":
			m_bossDefAssetPath = (string)val;
			break;
		case "CLASS_CHALLENGE_PREFAB_POPUP":
			m_classChallengePrefabPopup = (string)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"SCENARIO_ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"REQ_WING_ID" => typeof(int), 
			"REQ_PROGRESS" => typeof(int), 
			"REQ_FLAGS" => typeof(ulong), 
			"GRANTS_WING_ID" => typeof(int), 
			"GRANTS_PROGRESS" => typeof(int), 
			"GRANTS_FLAGS" => typeof(ulong), 
			"BOSS_DEF_ASSET_PATH" => typeof(string), 
			"CLASS_CHALLENGE_PREFAB_POPUP" => typeof(string), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAdventureMissionDbfRecords loadRecords = new LoadAdventureMissionDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AdventureMissionDbfAsset adventureMissionDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AdventureMissionDbfAsset)) as AdventureMissionDbfAsset;
		if (adventureMissionDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"AdventureMissionDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < adventureMissionDbfAsset.Records.Count; i++)
		{
			adventureMissionDbfAsset.Records[i].StripUnusedLocales();
		}
		records = adventureMissionDbfAsset.Records as List<T>;
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
