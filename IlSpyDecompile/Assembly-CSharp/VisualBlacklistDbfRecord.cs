using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class VisualBlacklistDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_achieveId;

	[SerializeField]
	private int m_blacklistAchieveId;

	[DbfField("ACHIEVE_ID")]
	public int AchieveId => m_achieveId;

	[DbfField("BLACKLIST_ACHIEVE_ID")]
	public int BlacklistAchieveId => m_blacklistAchieveId;

	public AchieveDbfRecord BlacklistAchieveRecord => GameDbf.Achieve.GetRecord(m_blacklistAchieveId);

	public void SetAchieveId(int v)
	{
		m_achieveId = v;
	}

	public void SetBlacklistAchieveId(int v)
	{
		m_blacklistAchieveId = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"ACHIEVE_ID" => m_achieveId, 
			"BLACKLIST_ACHIEVE_ID" => m_blacklistAchieveId, 
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
		case "BLACKLIST_ACHIEVE_ID":
			m_blacklistAchieveId = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"ACHIEVE_ID" => typeof(int), 
			"BLACKLIST_ACHIEVE_ID" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadVisualBlacklistDbfRecords loadRecords = new LoadVisualBlacklistDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		VisualBlacklistDbfAsset visualBlacklistDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(VisualBlacklistDbfAsset)) as VisualBlacklistDbfAsset;
		if (visualBlacklistDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"VisualBlacklistDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < visualBlacklistDbfAsset.Records.Count; i++)
		{
			visualBlacklistDbfAsset.Records[i].StripUnusedLocales();
		}
		records = visualBlacklistDbfAsset.Records as List<T>;
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
