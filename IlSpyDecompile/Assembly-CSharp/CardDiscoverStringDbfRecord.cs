using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class CardDiscoverStringDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteMiniGuid;

	[SerializeField]
	private string m_stringId;

	[DbfField("NOTE_MINI_GUID")]
	public string NoteMiniGuid => m_noteMiniGuid;

	[DbfField("STRING_ID")]
	public string StringId => m_stringId;

	public void SetNoteMiniGuid(string v)
	{
		m_noteMiniGuid = v;
	}

	public void SetStringId(string v)
	{
		m_stringId = v;
	}

	public override object GetVar(string name)
	{
		if (!(name == "NOTE_MINI_GUID"))
		{
			if (name == "STRING_ID")
			{
				return m_stringId;
			}
			return null;
		}
		return m_noteMiniGuid;
	}

	public override void SetVar(string name, object val)
	{
		if (!(name == "NOTE_MINI_GUID"))
		{
			if (name == "STRING_ID")
			{
				m_stringId = (string)val;
			}
		}
		else
		{
			m_noteMiniGuid = (string)val;
		}
	}

	public override Type GetVarType(string name)
	{
		if (!(name == "NOTE_MINI_GUID"))
		{
			if (name == "STRING_ID")
			{
				return typeof(string);
			}
			return null;
		}
		return typeof(string);
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardDiscoverStringDbfRecords loadRecords = new LoadCardDiscoverStringDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardDiscoverStringDbfAsset cardDiscoverStringDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardDiscoverStringDbfAsset)) as CardDiscoverStringDbfAsset;
		if (cardDiscoverStringDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"CardDiscoverStringDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < cardDiscoverStringDbfAsset.Records.Count; i++)
		{
			cardDiscoverStringDbfAsset.Records[i].StripUnusedLocales();
		}
		records = cardDiscoverStringDbfAsset.Records as List<T>;
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
