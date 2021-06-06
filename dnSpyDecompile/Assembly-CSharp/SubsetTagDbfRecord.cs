using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000283 RID: 643
[Serializable]
public class SubsetTagDbfRecord : DbfRecord
{
	// Token: 0x1700048B RID: 1163
	// (get) Token: 0x060020E2 RID: 8418 RVA: 0x000A222A File Offset: 0x000A042A
	[DbfField("TAG_ID")]
	public int TagId
	{
		get
		{
			return this.m_tagId;
		}
	}

	// Token: 0x1700048C RID: 1164
	// (get) Token: 0x060020E3 RID: 8419 RVA: 0x000A2232 File Offset: 0x000A0432
	[DbfField("TAG_VALUE")]
	public int TagValue
	{
		get
		{
			return this.m_tagValue;
		}
	}

	// Token: 0x060020E4 RID: 8420 RVA: 0x000A223A File Offset: 0x000A043A
	public void SetTagId(int v)
	{
		this.m_tagId = v;
	}

	// Token: 0x060020E5 RID: 8421 RVA: 0x000A2243 File Offset: 0x000A0443
	public void SetTagValue(int v)
	{
		this.m_tagValue = v;
	}

	// Token: 0x060020E6 RID: 8422 RVA: 0x000A224C File Offset: 0x000A044C
	public override object GetVar(string name)
	{
		if (name == "TAG_ID")
		{
			return this.m_tagId;
		}
		if (!(name == "TAG_VALUE"))
		{
			return null;
		}
		return this.m_tagValue;
	}

	// Token: 0x060020E7 RID: 8423 RVA: 0x000A2283 File Offset: 0x000A0483
	public override void SetVar(string name, object val)
	{
		if (name == "TAG_ID")
		{
			this.m_tagId = (int)val;
			return;
		}
		if (!(name == "TAG_VALUE"))
		{
			return;
		}
		this.m_tagValue = (int)val;
	}

	// Token: 0x060020E8 RID: 8424 RVA: 0x000A22B9 File Offset: 0x000A04B9
	public override Type GetVarType(string name)
	{
		if (name == "TAG_ID")
		{
			return typeof(int);
		}
		if (!(name == "TAG_VALUE"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x060020E9 RID: 8425 RVA: 0x000A22EE File Offset: 0x000A04EE
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadSubsetTagDbfRecords loadRecords = new LoadSubsetTagDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060020EA RID: 8426 RVA: 0x000A2304 File Offset: 0x000A0504
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		SubsetTagDbfAsset subsetTagDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(SubsetTagDbfAsset)) as SubsetTagDbfAsset;
		if (subsetTagDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("SubsetTagDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < subsetTagDbfAsset.Records.Count; i++)
		{
			subsetTagDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (subsetTagDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060020EB RID: 8427 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x060020EC RID: 8428 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x0400125D RID: 4701
	[SerializeField]
	private int m_tagId;

	// Token: 0x0400125E RID: 4702
	[SerializeField]
	private int m_tagValue;
}
