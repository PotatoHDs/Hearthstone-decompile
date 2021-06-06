using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class ExternalUrlDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private ExternalUrl.AssetFlags m_assetFlags;

	[SerializeField]
	private ExternalUrl.Endpoint m_endpoint;

	[SerializeField]
	private string m_globalUrl = "False";

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("ASSET_FLAGS")]
	public ExternalUrl.AssetFlags AssetFlags => m_assetFlags;

	[DbfField("ENDPOINT")]
	public ExternalUrl.Endpoint Endpoint => m_endpoint;

	[DbfField("GLOBAL_URL")]
	public string GlobalUrl => m_globalUrl;

	public List<RegionOverridesDbfRecord> RegionOverrides => GameDbf.RegionOverrides.GetRecords((RegionOverridesDbfRecord r) => r.ExternalUrlId == base.ID);

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetAssetFlags(ExternalUrl.AssetFlags v)
	{
		m_assetFlags = v;
	}

	public void SetEndpoint(ExternalUrl.Endpoint v)
	{
		m_endpoint = v;
	}

	public void SetGlobalUrl(string v)
	{
		m_globalUrl = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_DESC" => m_noteDesc, 
			"ASSET_FLAGS" => m_assetFlags, 
			"ENDPOINT" => m_endpoint, 
			"GLOBAL_URL" => m_globalUrl, 
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
		case "ASSET_FLAGS":
			if (val == null)
			{
				m_assetFlags = ExternalUrl.AssetFlags.NONE;
			}
			else if (val is ExternalUrl.AssetFlags || val is int)
			{
				m_assetFlags = (ExternalUrl.AssetFlags)val;
			}
			else if (val is string)
			{
				m_assetFlags = ExternalUrl.ParseAssetFlagsValue((string)val);
			}
			break;
		case "ENDPOINT":
			if (val == null)
			{
				m_endpoint = ExternalUrl.Endpoint.ACCOUNT;
			}
			else if (val is ExternalUrl.Endpoint || val is int)
			{
				m_endpoint = (ExternalUrl.Endpoint)val;
			}
			else if (val is string)
			{
				m_endpoint = ExternalUrl.ParseEndpointValue((string)val);
			}
			break;
		case "GLOBAL_URL":
			m_globalUrl = (string)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"ASSET_FLAGS" => typeof(ExternalUrl.AssetFlags), 
			"ENDPOINT" => typeof(ExternalUrl.Endpoint), 
			"GLOBAL_URL" => typeof(string), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadExternalUrlDbfRecords loadRecords = new LoadExternalUrlDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ExternalUrlDbfAsset externalUrlDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ExternalUrlDbfAsset)) as ExternalUrlDbfAsset;
		if (externalUrlDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"ExternalUrlDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < externalUrlDbfAsset.Records.Count; i++)
		{
			externalUrlDbfAsset.Records[i].StripUnusedLocales();
		}
		records = externalUrlDbfAsset.Records as List<T>;
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
