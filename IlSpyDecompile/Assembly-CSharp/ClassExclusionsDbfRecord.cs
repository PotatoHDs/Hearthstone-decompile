using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class ClassExclusionsDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_scenarioId;

	[SerializeField]
	private int m_classId;

	[DbfField("SCENARIO_ID")]
	public int ScenarioId => m_scenarioId;

	[DbfField("CLASS_ID")]
	public int ClassId => m_classId;

	public ClassDbfRecord ClassRecord => GameDbf.Class.GetRecord(m_classId);

	public void SetScenarioId(int v)
	{
		m_scenarioId = v;
	}

	public void SetClassId(int v)
	{
		m_classId = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"SCENARIO_ID" => m_scenarioId, 
			"CLASS_ID" => m_classId, 
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
		case "CLASS_ID":
			m_classId = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"SCENARIO_ID" => typeof(int), 
			"CLASS_ID" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadClassExclusionsDbfRecords loadRecords = new LoadClassExclusionsDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ClassExclusionsDbfAsset classExclusionsDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ClassExclusionsDbfAsset)) as ClassExclusionsDbfAsset;
		if (classExclusionsDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"ClassExclusionsDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < classExclusionsDbfAsset.Records.Count; i++)
		{
			classExclusionsDbfAsset.Records[i].StripUnusedLocales();
		}
		records = classExclusionsDbfAsset.Records as List<T>;
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
