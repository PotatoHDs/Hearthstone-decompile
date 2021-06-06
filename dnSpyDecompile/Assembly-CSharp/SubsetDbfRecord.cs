using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200027D RID: 637
[Serializable]
public class SubsetDbfRecord : DbfRecord
{
	// Token: 0x17000483 RID: 1155
	// (get) Token: 0x060020BA RID: 8378 RVA: 0x000A1B36 File Offset: 0x0009FD36
	public List<SubsetCardDbfRecord> Cards
	{
		get
		{
			return GameDbf.SubsetCard.GetRecords((SubsetCardDbfRecord r) => r.SubsetId == base.ID, -1);
		}
	}

	// Token: 0x17000484 RID: 1156
	// (get) Token: 0x060020BB RID: 8379 RVA: 0x000A1B4F File Offset: 0x0009FD4F
	public List<SubsetRuleDbfRecord> Rules
	{
		get
		{
			return GameDbf.SubsetRule.GetRecords((SubsetRuleDbfRecord r) => r.SubsetId == base.ID, -1);
		}
	}

	// Token: 0x060020BC RID: 8380 RVA: 0x00090F7E File Offset: 0x0008F17E
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		return null;
	}

	// Token: 0x060020BD RID: 8381 RVA: 0x00090F9A File Offset: 0x0008F19A
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
		}
	}

	// Token: 0x060020BE RID: 8382 RVA: 0x00090FB5 File Offset: 0x0008F1B5
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x060020BF RID: 8383 RVA: 0x000A1B68 File Offset: 0x0009FD68
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadSubsetDbfRecords loadRecords = new LoadSubsetDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060020C0 RID: 8384 RVA: 0x000A1B80 File Offset: 0x0009FD80
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		SubsetDbfAsset subsetDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(SubsetDbfAsset)) as SubsetDbfAsset;
		if (subsetDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("SubsetDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < subsetDbfAsset.Records.Count; i++)
		{
			subsetDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (subsetDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060020C1 RID: 8385 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x060020C2 RID: 8386 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}
}
