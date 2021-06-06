using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001CE RID: 462
[Serializable]
public class DeckRulesetDbfRecord : DbfRecord
{
	// Token: 0x170002AB RID: 683
	// (get) Token: 0x06001A7F RID: 6783 RVA: 0x0008BB22 File Offset: 0x00089D22
	[DbfField("ASSET_FLAGS")]
	public Assets.DeckRuleset.AssetFlags AssetFlags
	{
		get
		{
			return this.m_assetFlags;
		}
	}

	// Token: 0x170002AC RID: 684
	// (get) Token: 0x06001A80 RID: 6784 RVA: 0x0008BB2A File Offset: 0x00089D2A
	public List<DeckRulesetRuleDbfRecord> Rules
	{
		get
		{
			return GameDbf.DeckRulesetRule.GetRecords((DeckRulesetRuleDbfRecord r) => r.DeckRulesetId == base.ID, -1);
		}
	}

	// Token: 0x06001A81 RID: 6785 RVA: 0x0008BB43 File Offset: 0x00089D43
	public void SetAssetFlags(Assets.DeckRuleset.AssetFlags v)
	{
		this.m_assetFlags = v;
	}

	// Token: 0x06001A82 RID: 6786 RVA: 0x0008BB4C File Offset: 0x00089D4C
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (!(name == "ASSET_FLAGS"))
		{
			return null;
		}
		return this.m_assetFlags;
	}

	// Token: 0x06001A83 RID: 6787 RVA: 0x0008BB84 File Offset: 0x00089D84
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (!(name == "ASSET_FLAGS"))
		{
			return;
		}
		if (val == null)
		{
			this.m_assetFlags = Assets.DeckRuleset.AssetFlags.NONE;
			return;
		}
		if (val is Assets.DeckRuleset.AssetFlags || val is int)
		{
			this.m_assetFlags = (Assets.DeckRuleset.AssetFlags)val;
			return;
		}
		if (val is string)
		{
			this.m_assetFlags = Assets.DeckRuleset.ParseAssetFlagsValue((string)val);
		}
	}

	// Token: 0x06001A84 RID: 6788 RVA: 0x0008BBFA File Offset: 0x00089DFA
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (!(name == "ASSET_FLAGS"))
		{
			return null;
		}
		return typeof(Assets.DeckRuleset.AssetFlags);
	}

	// Token: 0x06001A85 RID: 6789 RVA: 0x0008BC2F File Offset: 0x00089E2F
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadDeckRulesetDbfRecords loadRecords = new LoadDeckRulesetDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001A86 RID: 6790 RVA: 0x0008BC48 File Offset: 0x00089E48
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		DeckRulesetDbfAsset deckRulesetDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(DeckRulesetDbfAsset)) as DeckRulesetDbfAsset;
		if (deckRulesetDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("DeckRulesetDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < deckRulesetDbfAsset.Records.Count; i++)
		{
			deckRulesetDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (deckRulesetDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001A87 RID: 6791 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001A88 RID: 6792 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000FEF RID: 4079
	[SerializeField]
	private Assets.DeckRuleset.AssetFlags m_assetFlags = Assets.DeckRuleset.AssetFlags.NOT_PACKAGED_IN_CLIENT;
}
