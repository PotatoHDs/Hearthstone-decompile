using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class RegionOverridesDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_externalUrlId;

	[SerializeField]
	private string m_region;

	[SerializeField]
	private string m_overrideUrl;

	[DbfField("EXTERNAL_URL_ID")]
	public int ExternalUrlId => m_externalUrlId;

	[DbfField("REGION")]
	public string Region => m_region;

	[DbfField("OVERRIDE_URL")]
	public string OverrideUrl => m_overrideUrl;

	public void SetExternalUrlId(int v)
	{
		m_externalUrlId = v;
	}

	public void SetRegion(string v)
	{
		m_region = v;
	}

	public void SetOverrideUrl(string v)
	{
		m_overrideUrl = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"EXTERNAL_URL_ID" => m_externalUrlId, 
			"REGION" => m_region, 
			"OVERRIDE_URL" => m_overrideUrl, 
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
		case "EXTERNAL_URL_ID":
			m_externalUrlId = (int)val;
			break;
		case "REGION":
			m_region = (string)val;
			break;
		case "OVERRIDE_URL":
			m_overrideUrl = (string)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"EXTERNAL_URL_ID" => typeof(int), 
			"REGION" => typeof(string), 
			"OVERRIDE_URL" => typeof(string), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadRegionOverridesDbfRecords loadRecords = new LoadRegionOverridesDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		RegionOverridesDbfAsset regionOverridesDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(RegionOverridesDbfAsset)) as RegionOverridesDbfAsset;
		if (regionOverridesDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"RegionOverridesDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < regionOverridesDbfAsset.Records.Count; i++)
		{
			regionOverridesDbfAsset.Records[i].StripUnusedLocales();
		}
		records = regionOverridesDbfAsset.Records as List<T>;
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
