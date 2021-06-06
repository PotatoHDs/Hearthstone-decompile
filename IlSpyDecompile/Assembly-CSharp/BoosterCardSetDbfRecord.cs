using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class BoosterCardSetDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_subsetId;

	[DbfField("SUBSET_ID")]
	public int SubsetId => m_subsetId;

	public SubsetDbfRecord SubsetRecord => GameDbf.Subset.GetRecord(m_subsetId);

	public void SetSubsetId(int v)
	{
		m_subsetId = v;
	}

	public override object GetVar(string name)
	{
		if (!(name == "ID"))
		{
			if (name == "SUBSET_ID")
			{
				return m_subsetId;
			}
			return null;
		}
		return base.ID;
	}

	public override void SetVar(string name, object val)
	{
		if (!(name == "ID"))
		{
			if (name == "SUBSET_ID")
			{
				m_subsetId = (int)val;
			}
		}
		else
		{
			SetID((int)val);
		}
	}

	public override Type GetVarType(string name)
	{
		if (!(name == "ID"))
		{
			if (name == "SUBSET_ID")
			{
				return typeof(int);
			}
			return null;
		}
		return typeof(int);
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadBoosterCardSetDbfRecords loadRecords = new LoadBoosterCardSetDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		BoosterCardSetDbfAsset boosterCardSetDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(BoosterCardSetDbfAsset)) as BoosterCardSetDbfAsset;
		if (boosterCardSetDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"BoosterCardSetDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < boosterCardSetDbfAsset.Records.Count; i++)
		{
			boosterCardSetDbfAsset.Records[i].StripUnusedLocales();
		}
		records = boosterCardSetDbfAsset.Records as List<T>;
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
