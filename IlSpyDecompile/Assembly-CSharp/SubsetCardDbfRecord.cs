using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class SubsetCardDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_subsetId;

	[SerializeField]
	private int m_cardId;

	[DbfField("SUBSET_ID")]
	public int SubsetId => m_subsetId;

	[DbfField("CARD_ID")]
	public int CardId => m_cardId;

	public CardDbfRecord CardRecord => GameDbf.Card.GetRecord(m_cardId);

	public void SetSubsetId(int v)
	{
		m_subsetId = v;
	}

	public void SetCardId(int v)
	{
		m_cardId = v;
	}

	public override object GetVar(string name)
	{
		if (!(name == "SUBSET_ID"))
		{
			if (name == "CARD_ID")
			{
				return m_cardId;
			}
			return null;
		}
		return m_subsetId;
	}

	public override void SetVar(string name, object val)
	{
		if (!(name == "SUBSET_ID"))
		{
			if (name == "CARD_ID")
			{
				m_cardId = (int)val;
			}
		}
		else
		{
			m_subsetId = (int)val;
		}
	}

	public override Type GetVarType(string name)
	{
		if (!(name == "SUBSET_ID"))
		{
			if (name == "CARD_ID")
			{
				return typeof(int);
			}
			return null;
		}
		return typeof(int);
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadSubsetCardDbfRecords loadRecords = new LoadSubsetCardDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		SubsetCardDbfAsset subsetCardDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(SubsetCardDbfAsset)) as SubsetCardDbfAsset;
		if (subsetCardDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"SubsetCardDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < subsetCardDbfAsset.Records.Count; i++)
		{
			subsetCardDbfAsset.Records[i].StripUnusedLocales();
		}
		records = subsetCardDbfAsset.Records as List<T>;
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
