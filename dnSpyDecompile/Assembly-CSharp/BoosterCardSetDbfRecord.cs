using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000189 RID: 393
[Serializable]
public class BoosterCardSetDbfRecord : DbfRecord
{
	// Token: 0x17000209 RID: 521
	// (get) Token: 0x06001833 RID: 6195 RVA: 0x000844FA File Offset: 0x000826FA
	[DbfField("SUBSET_ID")]
	public int SubsetId
	{
		get
		{
			return this.m_subsetId;
		}
	}

	// Token: 0x1700020A RID: 522
	// (get) Token: 0x06001834 RID: 6196 RVA: 0x00084502 File Offset: 0x00082702
	public SubsetDbfRecord SubsetRecord
	{
		get
		{
			return GameDbf.Subset.GetRecord(this.m_subsetId);
		}
	}

	// Token: 0x06001835 RID: 6197 RVA: 0x00084514 File Offset: 0x00082714
	public void SetSubsetId(int v)
	{
		this.m_subsetId = v;
	}

	// Token: 0x06001836 RID: 6198 RVA: 0x0008451D File Offset: 0x0008271D
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (!(name == "SUBSET_ID"))
		{
			return null;
		}
		return this.m_subsetId;
	}

	// Token: 0x06001837 RID: 6199 RVA: 0x00084554 File Offset: 0x00082754
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (!(name == "SUBSET_ID"))
		{
			return;
		}
		this.m_subsetId = (int)val;
	}

	// Token: 0x06001838 RID: 6200 RVA: 0x0008458A File Offset: 0x0008278A
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (!(name == "SUBSET_ID"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x06001839 RID: 6201 RVA: 0x000845BF File Offset: 0x000827BF
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadBoosterCardSetDbfRecords loadRecords = new LoadBoosterCardSetDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x0600183A RID: 6202 RVA: 0x000845D8 File Offset: 0x000827D8
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		BoosterCardSetDbfAsset boosterCardSetDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(BoosterCardSetDbfAsset)) as BoosterCardSetDbfAsset;
		if (boosterCardSetDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("BoosterCardSetDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < boosterCardSetDbfAsset.Records.Count; i++)
		{
			boosterCardSetDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (boosterCardSetDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x0600183B RID: 6203 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x0600183C RID: 6204 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000F33 RID: 3891
	[SerializeField]
	private int m_subsetId;
}
