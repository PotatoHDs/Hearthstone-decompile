using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000271 RID: 625
[Serializable]
public class SellableDeckDbfRecord : DbfRecord
{
	// Token: 0x17000470 RID: 1136
	// (get) Token: 0x06002067 RID: 8295 RVA: 0x000A0E1E File Offset: 0x0009F01E
	[DbfField("DECK_TEMPLATE_ID")]
	public int DeckTemplateId
	{
		get
		{
			return this.m_deckTemplateId;
		}
	}

	// Token: 0x17000471 RID: 1137
	// (get) Token: 0x06002068 RID: 8296 RVA: 0x000A0E26 File Offset: 0x0009F026
	public DeckTemplateDbfRecord DeckTemplateRecord
	{
		get
		{
			return GameDbf.DeckTemplate.GetRecord(this.m_deckTemplateId);
		}
	}

	// Token: 0x17000472 RID: 1138
	// (get) Token: 0x06002069 RID: 8297 RVA: 0x000A0E38 File Offset: 0x0009F038
	[DbfField("BOOSTER_ID")]
	public int BoosterId
	{
		get
		{
			return this.m_boosterId;
		}
	}

	// Token: 0x17000473 RID: 1139
	// (get) Token: 0x0600206A RID: 8298 RVA: 0x000A0E40 File Offset: 0x0009F040
	public BoosterDbfRecord BoosterRecord
	{
		get
		{
			return GameDbf.Booster.GetRecord(this.m_boosterId);
		}
	}

	// Token: 0x0600206B RID: 8299 RVA: 0x000A0E52 File Offset: 0x0009F052
	public void SetDeckTemplateId(int v)
	{
		this.m_deckTemplateId = v;
	}

	// Token: 0x0600206C RID: 8300 RVA: 0x000A0E5B File Offset: 0x0009F05B
	public void SetBoosterId(int v)
	{
		this.m_boosterId = v;
	}

	// Token: 0x0600206D RID: 8301 RVA: 0x000A0E64 File Offset: 0x0009F064
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "DECK_TEMPLATE_ID")
		{
			return this.m_deckTemplateId;
		}
		if (!(name == "BOOSTER_ID"))
		{
			return null;
		}
		return this.m_boosterId;
	}

	// Token: 0x0600206E RID: 8302 RVA: 0x000A0EC0 File Offset: 0x0009F0C0
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "DECK_TEMPLATE_ID")
		{
			this.m_deckTemplateId = (int)val;
			return;
		}
		if (!(name == "BOOSTER_ID"))
		{
			return;
		}
		this.m_boosterId = (int)val;
	}

	// Token: 0x0600206F RID: 8303 RVA: 0x000A0F1C File Offset: 0x0009F11C
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "DECK_TEMPLATE_ID")
		{
			return typeof(int);
		}
		if (!(name == "BOOSTER_ID"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x06002070 RID: 8304 RVA: 0x000A0F74 File Offset: 0x0009F174
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadSellableDeckDbfRecords loadRecords = new LoadSellableDeckDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06002071 RID: 8305 RVA: 0x000A0F8C File Offset: 0x0009F18C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		SellableDeckDbfAsset sellableDeckDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(SellableDeckDbfAsset)) as SellableDeckDbfAsset;
		if (sellableDeckDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("SellableDeckDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < sellableDeckDbfAsset.Records.Count; i++)
		{
			sellableDeckDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (sellableDeckDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06002072 RID: 8306 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06002073 RID: 8307 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x0400123C RID: 4668
	[SerializeField]
	private int m_deckTemplateId;

	// Token: 0x0400123D RID: 4669
	[SerializeField]
	private int m_boosterId;
}
