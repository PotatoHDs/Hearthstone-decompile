using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200027A RID: 634
[Serializable]
public class SubsetCardDbfRecord : DbfRecord
{
	// Token: 0x17000480 RID: 1152
	// (get) Token: 0x060020A9 RID: 8361 RVA: 0x000A192E File Offset: 0x0009FB2E
	[DbfField("SUBSET_ID")]
	public int SubsetId
	{
		get
		{
			return this.m_subsetId;
		}
	}

	// Token: 0x17000481 RID: 1153
	// (get) Token: 0x060020AA RID: 8362 RVA: 0x000A1936 File Offset: 0x0009FB36
	[DbfField("CARD_ID")]
	public int CardId
	{
		get
		{
			return this.m_cardId;
		}
	}

	// Token: 0x17000482 RID: 1154
	// (get) Token: 0x060020AB RID: 8363 RVA: 0x000A193E File Offset: 0x0009FB3E
	public CardDbfRecord CardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_cardId);
		}
	}

	// Token: 0x060020AC RID: 8364 RVA: 0x000A1950 File Offset: 0x0009FB50
	public void SetSubsetId(int v)
	{
		this.m_subsetId = v;
	}

	// Token: 0x060020AD RID: 8365 RVA: 0x000A1959 File Offset: 0x0009FB59
	public void SetCardId(int v)
	{
		this.m_cardId = v;
	}

	// Token: 0x060020AE RID: 8366 RVA: 0x000A1962 File Offset: 0x0009FB62
	public override object GetVar(string name)
	{
		if (name == "SUBSET_ID")
		{
			return this.m_subsetId;
		}
		if (!(name == "CARD_ID"))
		{
			return null;
		}
		return this.m_cardId;
	}

	// Token: 0x060020AF RID: 8367 RVA: 0x000A1999 File Offset: 0x0009FB99
	public override void SetVar(string name, object val)
	{
		if (name == "SUBSET_ID")
		{
			this.m_subsetId = (int)val;
			return;
		}
		if (!(name == "CARD_ID"))
		{
			return;
		}
		this.m_cardId = (int)val;
	}

	// Token: 0x060020B0 RID: 8368 RVA: 0x000A19CF File Offset: 0x0009FBCF
	public override Type GetVarType(string name)
	{
		if (name == "SUBSET_ID")
		{
			return typeof(int);
		}
		if (!(name == "CARD_ID"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x060020B1 RID: 8369 RVA: 0x000A1A04 File Offset: 0x0009FC04
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadSubsetCardDbfRecords loadRecords = new LoadSubsetCardDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060020B2 RID: 8370 RVA: 0x000A1A1C File Offset: 0x0009FC1C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		SubsetCardDbfAsset subsetCardDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(SubsetCardDbfAsset)) as SubsetCardDbfAsset;
		if (subsetCardDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("SubsetCardDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < subsetCardDbfAsset.Records.Count; i++)
		{
			subsetCardDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (subsetCardDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060020B3 RID: 8371 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x060020B4 RID: 8372 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x0400124F RID: 4687
	[SerializeField]
	private int m_subsetId;

	// Token: 0x04001250 RID: 4688
	[SerializeField]
	private int m_cardId;
}
