using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class AchieveConditionDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_achieveId;

	[SerializeField]
	private int m_scenarioId;

	[DbfField("ACHIEVE_ID")]
	public int AchieveId => m_achieveId;

	[DbfField("SCENARIO_ID")]
	public int ScenarioId => m_scenarioId;

	public ScenarioDbfRecord ScenarioRecord => GameDbf.Scenario.GetRecord(m_scenarioId);

	public void SetAchieveId(int v)
	{
		m_achieveId = v;
	}

	public void SetScenarioId(int v)
	{
		m_scenarioId = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"ACHIEVE_ID" => m_achieveId, 
			"SCENARIO_ID" => m_scenarioId, 
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
		case "ACHIEVE_ID":
			m_achieveId = (int)val;
			break;
		case "SCENARIO_ID":
			m_scenarioId = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"ACHIEVE_ID" => typeof(int), 
			"SCENARIO_ID" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAchieveConditionDbfRecords loadRecords = new LoadAchieveConditionDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AchieveConditionDbfAsset achieveConditionDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AchieveConditionDbfAsset)) as AchieveConditionDbfAsset;
		if (achieveConditionDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"AchieveConditionDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < achieveConditionDbfAsset.Records.Count; i++)
		{
			achieveConditionDbfAsset.Records[i].StripUnusedLocales();
		}
		records = achieveConditionDbfAsset.Records as List<T>;
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
