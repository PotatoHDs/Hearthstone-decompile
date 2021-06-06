using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class DeckRulesetRuleSubsetDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_deckRulesetRuleId;

	[SerializeField]
	private int m_subsetId;

	[DbfField("DECK_RULESET_RULE_ID")]
	public int DeckRulesetRuleId => m_deckRulesetRuleId;

	[DbfField("SUBSET_ID")]
	public int SubsetId => m_subsetId;

	public SubsetDbfRecord SubsetRecord => GameDbf.Subset.GetRecord(m_subsetId);

	public void SetDeckRulesetRuleId(int v)
	{
		m_deckRulesetRuleId = v;
	}

	public void SetSubsetId(int v)
	{
		m_subsetId = v;
	}

	public override object GetVar(string name)
	{
		if (!(name == "DECK_RULESET_RULE_ID"))
		{
			if (name == "SUBSET_ID")
			{
				return m_subsetId;
			}
			return null;
		}
		return m_deckRulesetRuleId;
	}

	public override void SetVar(string name, object val)
	{
		if (!(name == "DECK_RULESET_RULE_ID"))
		{
			if (name == "SUBSET_ID")
			{
				m_subsetId = (int)val;
			}
		}
		else
		{
			m_deckRulesetRuleId = (int)val;
		}
	}

	public override Type GetVarType(string name)
	{
		if (!(name == "DECK_RULESET_RULE_ID"))
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
		LoadDeckRulesetRuleSubsetDbfRecords loadRecords = new LoadDeckRulesetRuleSubsetDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		DeckRulesetRuleSubsetDbfAsset deckRulesetRuleSubsetDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(DeckRulesetRuleSubsetDbfAsset)) as DeckRulesetRuleSubsetDbfAsset;
		if (deckRulesetRuleSubsetDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"DeckRulesetRuleSubsetDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < deckRulesetRuleSubsetDbfAsset.Records.Count; i++)
		{
			deckRulesetRuleSubsetDbfAsset.Records[i].StripUnusedLocales();
		}
		records = deckRulesetRuleSubsetDbfAsset.Records as List<T>;
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
