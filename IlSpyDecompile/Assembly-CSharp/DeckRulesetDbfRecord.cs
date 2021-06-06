using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class DeckRulesetDbfRecord : DbfRecord
{
	[SerializeField]
	private Assets.DeckRuleset.AssetFlags m_assetFlags = Assets.DeckRuleset.AssetFlags.NOT_PACKAGED_IN_CLIENT;

	[DbfField("ASSET_FLAGS")]
	public Assets.DeckRuleset.AssetFlags AssetFlags => m_assetFlags;

	public List<DeckRulesetRuleDbfRecord> Rules => GameDbf.DeckRulesetRule.GetRecords((DeckRulesetRuleDbfRecord r) => r.DeckRulesetId == base.ID);

	public void SetAssetFlags(Assets.DeckRuleset.AssetFlags v)
	{
		m_assetFlags = v;
	}

	public override object GetVar(string name)
	{
		if (!(name == "ID"))
		{
			if (name == "ASSET_FLAGS")
			{
				return m_assetFlags;
			}
			return null;
		}
		return base.ID;
	}

	public override void SetVar(string name, object val)
	{
		if (!(name == "ID"))
		{
			if (name == "ASSET_FLAGS")
			{
				if (val == null)
				{
					m_assetFlags = Assets.DeckRuleset.AssetFlags.NONE;
				}
				else if (val is Assets.DeckRuleset.AssetFlags || val is int)
				{
					m_assetFlags = (Assets.DeckRuleset.AssetFlags)val;
				}
				else if (val is string)
				{
					m_assetFlags = Assets.DeckRuleset.ParseAssetFlagsValue((string)val);
				}
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
			if (name == "ASSET_FLAGS")
			{
				return typeof(Assets.DeckRuleset.AssetFlags);
			}
			return null;
		}
		return typeof(int);
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadDeckRulesetDbfRecords loadRecords = new LoadDeckRulesetDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		DeckRulesetDbfAsset deckRulesetDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(DeckRulesetDbfAsset)) as DeckRulesetDbfAsset;
		if (deckRulesetDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"DeckRulesetDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < deckRulesetDbfAsset.Records.Count; i++)
		{
			deckRulesetDbfAsset.Records[i].StripUnusedLocales();
		}
		records = deckRulesetDbfAsset.Records as List<T>;
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
