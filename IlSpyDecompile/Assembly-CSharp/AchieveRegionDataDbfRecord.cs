using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class AchieveRegionDataDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_achieveId;

	[SerializeField]
	private int m_region;

	[SerializeField]
	private int m_rewardableLimit;

	[SerializeField]
	private double m_rewardableInterval;

	[SerializeField]
	private string m_progressableEvent = "none";

	[SerializeField]
	private string m_activateEvent = "none";

	[DbfField("ACHIEVE_ID")]
	public int AchieveId => m_achieveId;

	[DbfField("REGION")]
	public int Region => m_region;

	[DbfField("REWARDABLE_LIMIT")]
	public int RewardableLimit => m_rewardableLimit;

	[DbfField("REWARDABLE_INTERVAL")]
	public double RewardableInterval => m_rewardableInterval;

	[DbfField("PROGRESSABLE_EVENT")]
	public string ProgressableEvent => m_progressableEvent;

	[DbfField("ACTIVATE_EVENT")]
	public string ActivateEvent => m_activateEvent;

	public void SetAchieveId(int v)
	{
		m_achieveId = v;
	}

	public void SetRegion(int v)
	{
		m_region = v;
	}

	public void SetRewardableLimit(int v)
	{
		m_rewardableLimit = v;
	}

	public void SetRewardableInterval(double v)
	{
		m_rewardableInterval = v;
	}

	public void SetProgressableEvent(string v)
	{
		m_progressableEvent = v;
	}

	public void SetActivateEvent(string v)
	{
		m_activateEvent = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"ACHIEVE_ID" => m_achieveId, 
			"REGION" => m_region, 
			"REWARDABLE_LIMIT" => m_rewardableLimit, 
			"REWARDABLE_INTERVAL" => m_rewardableInterval, 
			"PROGRESSABLE_EVENT" => m_progressableEvent, 
			"ACTIVATE_EVENT" => m_activateEvent, 
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
		case "REGION":
			m_region = (int)val;
			break;
		case "REWARDABLE_LIMIT":
			m_rewardableLimit = (int)val;
			break;
		case "REWARDABLE_INTERVAL":
			m_rewardableInterval = (double)val;
			break;
		case "PROGRESSABLE_EVENT":
			m_progressableEvent = (string)val;
			break;
		case "ACTIVATE_EVENT":
			m_activateEvent = (string)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"ACHIEVE_ID" => typeof(int), 
			"REGION" => typeof(int), 
			"REWARDABLE_LIMIT" => typeof(int), 
			"REWARDABLE_INTERVAL" => typeof(double), 
			"PROGRESSABLE_EVENT" => typeof(string), 
			"ACTIVATE_EVENT" => typeof(string), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAchieveRegionDataDbfRecords loadRecords = new LoadAchieveRegionDataDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AchieveRegionDataDbfAsset achieveRegionDataDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AchieveRegionDataDbfAsset)) as AchieveRegionDataDbfAsset;
		if (achieveRegionDataDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"AchieveRegionDataDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < achieveRegionDataDbfAsset.Records.Count; i++)
		{
			achieveRegionDataDbfAsset.Records[i].StripUnusedLocales();
		}
		records = achieveRegionDataDbfAsset.Records as List<T>;
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
