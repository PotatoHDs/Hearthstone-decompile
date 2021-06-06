using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class SubsetDbfRecord : DbfRecord
{
	public List<SubsetCardDbfRecord> Cards => GameDbf.SubsetCard.GetRecords((SubsetCardDbfRecord r) => r.SubsetId == base.ID);

	public List<SubsetRuleDbfRecord> Rules => GameDbf.SubsetRule.GetRecords((SubsetRuleDbfRecord r) => r.SubsetId == base.ID);

	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		return null;
	}

	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			SetID((int)val);
		}
	}

	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		return null;
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadSubsetDbfRecords loadRecords = new LoadSubsetDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		SubsetDbfAsset subsetDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(SubsetDbfAsset)) as SubsetDbfAsset;
		if (subsetDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"SubsetDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < subsetDbfAsset.Records.Count; i++)
		{
			subsetDbfAsset.Records[i].StripUnusedLocales();
		}
		records = subsetDbfAsset.Records as List<T>;
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
