using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class CardValueDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_OverrideEvent;

	[DbfField("OVERRIDE_EVENT")]
	public string OverrideEvent => m_OverrideEvent;

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardValueDbfAsset cardValueDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardValueDbfAsset)) as CardValueDbfAsset;
		if (cardValueDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"CardValueDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < cardValueDbfAsset.Records.Count; i++)
		{
			cardValueDbfAsset.Records[i].StripUnusedLocales();
		}
		records = cardValueDbfAsset.Records as List<T>;
		return true;
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardValueDbfRecords loadRecords = new LoadCardValueDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.Records as List<T>);
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
	}

	public void SetOverrideEvent(string v)
	{
		m_OverrideEvent = v;
	}

	public override object GetVar(string name)
	{
		if (name == "OVERRIDE_EVENT")
		{
			return OverrideEvent;
		}
		return null;
	}

	public override void SetVar(string name, object val)
	{
		if (name == "OVERRIDE_EVENT")
		{
			SetOverrideEvent((string)val);
		}
	}

	public override Type GetVarType(string name)
	{
		if (name == "OVERRIDE_EVENT")
		{
			return typeof(string);
		}
		return null;
	}
}
