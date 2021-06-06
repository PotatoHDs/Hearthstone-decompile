using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000277 RID: 631
[Serializable]
public class ShopTierProductSaleDbfRecord : DbfRecord
{
	// Token: 0x1700047B RID: 1147
	// (get) Token: 0x06002093 RID: 8339 RVA: 0x000A15F2 File Offset: 0x0009F7F2
	[DbfField("TIER_ID")]
	public int TierId
	{
		get
		{
			return this.m_tierId;
		}
	}

	// Token: 0x1700047C RID: 1148
	// (get) Token: 0x06002094 RID: 8340 RVA: 0x000A15FA File Offset: 0x0009F7FA
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x1700047D RID: 1149
	// (get) Token: 0x06002095 RID: 8341 RVA: 0x000A1602 File Offset: 0x0009F802
	[DbfField("SLOT_INDEX")]
	public int SlotIndex
	{
		get
		{
			return this.m_slotIndex;
		}
	}

	// Token: 0x1700047E RID: 1150
	// (get) Token: 0x06002096 RID: 8342 RVA: 0x000A160A File Offset: 0x0009F80A
	[DbfField("PMT_PRODUCT_ID")]
	public int PmtProductId
	{
		get
		{
			return this.m_pmtProductId;
		}
	}

	// Token: 0x1700047F RID: 1151
	// (get) Token: 0x06002097 RID: 8343 RVA: 0x000A1612 File Offset: 0x0009F812
	[DbfField("EVENT")]
	public string Event
	{
		get
		{
			return this.m_event;
		}
	}

	// Token: 0x06002098 RID: 8344 RVA: 0x000A161A File Offset: 0x0009F81A
	public void SetTierId(int v)
	{
		this.m_tierId = v;
	}

	// Token: 0x06002099 RID: 8345 RVA: 0x000A1623 File Offset: 0x0009F823
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x0600209A RID: 8346 RVA: 0x000A162C File Offset: 0x0009F82C
	public void SetSlotIndex(int v)
	{
		this.m_slotIndex = v;
	}

	// Token: 0x0600209B RID: 8347 RVA: 0x000A1635 File Offset: 0x0009F835
	public void SetPmtProductId(int v)
	{
		this.m_pmtProductId = v;
	}

	// Token: 0x0600209C RID: 8348 RVA: 0x000A163E File Offset: 0x0009F83E
	public void SetEvent(string v)
	{
		this.m_event = v;
	}

	// Token: 0x0600209D RID: 8349 RVA: 0x000A1648 File Offset: 0x0009F848
	public override object GetVar(string name)
	{
		if (name == "TIER_ID")
		{
			return this.m_tierId;
		}
		if (name == "NOTE_DESC")
		{
			return this.m_noteDesc;
		}
		if (name == "SLOT_INDEX")
		{
			return this.m_slotIndex;
		}
		if (name == "PMT_PRODUCT_ID")
		{
			return this.m_pmtProductId;
		}
		if (!(name == "EVENT"))
		{
			return null;
		}
		return this.m_event;
	}

	// Token: 0x0600209E RID: 8350 RVA: 0x000A16CC File Offset: 0x0009F8CC
	public override void SetVar(string name, object val)
	{
		if (name == "TIER_ID")
		{
			this.m_tierId = (int)val;
			return;
		}
		if (name == "NOTE_DESC")
		{
			this.m_noteDesc = (string)val;
			return;
		}
		if (name == "SLOT_INDEX")
		{
			this.m_slotIndex = (int)val;
			return;
		}
		if (name == "PMT_PRODUCT_ID")
		{
			this.m_pmtProductId = (int)val;
			return;
		}
		if (!(name == "EVENT"))
		{
			return;
		}
		this.m_event = (string)val;
	}

	// Token: 0x0600209F RID: 8351 RVA: 0x000A175C File Offset: 0x0009F95C
	public override Type GetVarType(string name)
	{
		if (name == "TIER_ID")
		{
			return typeof(int);
		}
		if (name == "NOTE_DESC")
		{
			return typeof(string);
		}
		if (name == "SLOT_INDEX")
		{
			return typeof(int);
		}
		if (name == "PMT_PRODUCT_ID")
		{
			return typeof(int);
		}
		if (!(name == "EVENT"))
		{
			return null;
		}
		return typeof(string);
	}

	// Token: 0x060020A0 RID: 8352 RVA: 0x000A17E4 File Offset: 0x0009F9E4
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadShopTierProductSaleDbfRecords loadRecords = new LoadShopTierProductSaleDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060020A1 RID: 8353 RVA: 0x000A17FC File Offset: 0x0009F9FC
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ShopTierProductSaleDbfAsset shopTierProductSaleDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ShopTierProductSaleDbfAsset)) as ShopTierProductSaleDbfAsset;
		if (shopTierProductSaleDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("ShopTierProductSaleDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < shopTierProductSaleDbfAsset.Records.Count; i++)
		{
			shopTierProductSaleDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (shopTierProductSaleDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060020A2 RID: 8354 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x060020A3 RID: 8355 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04001248 RID: 4680
	[SerializeField]
	private int m_tierId;

	// Token: 0x04001249 RID: 4681
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x0400124A RID: 4682
	[SerializeField]
	private int m_slotIndex = -1;

	// Token: 0x0400124B RID: 4683
	[SerializeField]
	private int m_pmtProductId;

	// Token: 0x0400124C RID: 4684
	[SerializeField]
	private string m_event = "always";
}
