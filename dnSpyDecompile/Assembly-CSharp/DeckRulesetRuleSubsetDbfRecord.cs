using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001D4 RID: 468
[Serializable]
public class DeckRulesetRuleSubsetDbfRecord : DbfRecord
{
	// Token: 0x170002BC RID: 700
	// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x0008C806 File Offset: 0x0008AA06
	[DbfField("DECK_RULESET_RULE_ID")]
	public int DeckRulesetRuleId
	{
		get
		{
			return this.m_deckRulesetRuleId;
		}
	}

	// Token: 0x170002BD RID: 701
	// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x0008C80E File Offset: 0x0008AA0E
	[DbfField("SUBSET_ID")]
	public int SubsetId
	{
		get
		{
			return this.m_subsetId;
		}
	}

	// Token: 0x170002BE RID: 702
	// (get) Token: 0x06001ABA RID: 6842 RVA: 0x0008C816 File Offset: 0x0008AA16
	public SubsetDbfRecord SubsetRecord
	{
		get
		{
			return GameDbf.Subset.GetRecord(this.m_subsetId);
		}
	}

	// Token: 0x06001ABB RID: 6843 RVA: 0x0008C828 File Offset: 0x0008AA28
	public void SetDeckRulesetRuleId(int v)
	{
		this.m_deckRulesetRuleId = v;
	}

	// Token: 0x06001ABC RID: 6844 RVA: 0x0008C831 File Offset: 0x0008AA31
	public void SetSubsetId(int v)
	{
		this.m_subsetId = v;
	}

	// Token: 0x06001ABD RID: 6845 RVA: 0x0008C83A File Offset: 0x0008AA3A
	public override object GetVar(string name)
	{
		if (name == "DECK_RULESET_RULE_ID")
		{
			return this.m_deckRulesetRuleId;
		}
		if (!(name == "SUBSET_ID"))
		{
			return null;
		}
		return this.m_subsetId;
	}

	// Token: 0x06001ABE RID: 6846 RVA: 0x0008C871 File Offset: 0x0008AA71
	public override void SetVar(string name, object val)
	{
		if (name == "DECK_RULESET_RULE_ID")
		{
			this.m_deckRulesetRuleId = (int)val;
			return;
		}
		if (!(name == "SUBSET_ID"))
		{
			return;
		}
		this.m_subsetId = (int)val;
	}

	// Token: 0x06001ABF RID: 6847 RVA: 0x0008C8A7 File Offset: 0x0008AAA7
	public override Type GetVarType(string name)
	{
		if (name == "DECK_RULESET_RULE_ID")
		{
			return typeof(int);
		}
		if (!(name == "SUBSET_ID"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x06001AC0 RID: 6848 RVA: 0x0008C8DC File Offset: 0x0008AADC
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadDeckRulesetRuleSubsetDbfRecords loadRecords = new LoadDeckRulesetRuleSubsetDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001AC1 RID: 6849 RVA: 0x0008C8F4 File Offset: 0x0008AAF4
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		DeckRulesetRuleSubsetDbfAsset deckRulesetRuleSubsetDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(DeckRulesetRuleSubsetDbfAsset)) as DeckRulesetRuleSubsetDbfAsset;
		if (deckRulesetRuleSubsetDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("DeckRulesetRuleSubsetDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < deckRulesetRuleSubsetDbfAsset.Records.Count; i++)
		{
			deckRulesetRuleSubsetDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (deckRulesetRuleSubsetDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001AC2 RID: 6850 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001AC3 RID: 6851 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04001001 RID: 4097
	[SerializeField]
	private int m_deckRulesetRuleId;

	// Token: 0x04001002 RID: 4098
	[SerializeField]
	private int m_subsetId;
}
