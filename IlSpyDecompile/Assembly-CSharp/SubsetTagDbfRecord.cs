using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class SubsetTagDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_tagId;

	[SerializeField]
	private int m_tagValue;

	[DbfField("TAG_ID")]
	public int TagId => m_tagId;

	[DbfField("TAG_VALUE")]
	public int TagValue => m_tagValue;

	public void SetTagId(int v)
	{
		m_tagId = v;
	}

	public void SetTagValue(int v)
	{
		m_tagValue = v;
	}

	public override object GetVar(string name)
	{
		if (!(name == "TAG_ID"))
		{
			if (name == "TAG_VALUE")
			{
				return m_tagValue;
			}
			return null;
		}
		return m_tagId;
	}

	public override void SetVar(string name, object val)
	{
		if (!(name == "TAG_ID"))
		{
			if (name == "TAG_VALUE")
			{
				m_tagValue = (int)val;
			}
		}
		else
		{
			m_tagId = (int)val;
		}
	}

	public override Type GetVarType(string name)
	{
		if (!(name == "TAG_ID"))
		{
			if (name == "TAG_VALUE")
			{
				return typeof(int);
			}
			return null;
		}
		return typeof(int);
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadSubsetTagDbfRecords loadRecords = new LoadSubsetTagDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		SubsetTagDbfAsset subsetTagDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(SubsetTagDbfAsset)) as SubsetTagDbfAsset;
		if (subsetTagDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"SubsetTagDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < subsetTagDbfAsset.Records.Count; i++)
		{
			subsetTagDbfAsset.Records[i].StripUnusedLocales();
		}
		records = subsetTagDbfAsset.Records as List<T>;
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
