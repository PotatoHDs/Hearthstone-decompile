using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class ShopTierProductSaleDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_tierId;

	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private int m_slotIndex = -1;

	[SerializeField]
	private int m_pmtProductId;

	[SerializeField]
	private string m_event = "always";

	[DbfField("TIER_ID")]
	public int TierId => m_tierId;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("SLOT_INDEX")]
	public int SlotIndex => m_slotIndex;

	[DbfField("PMT_PRODUCT_ID")]
	public int PmtProductId => m_pmtProductId;

	[DbfField("EVENT")]
	public string Event => m_event;

	public void SetTierId(int v)
	{
		m_tierId = v;
	}

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetSlotIndex(int v)
	{
		m_slotIndex = v;
	}

	public void SetPmtProductId(int v)
	{
		m_pmtProductId = v;
	}

	public void SetEvent(string v)
	{
		m_event = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"TIER_ID" => m_tierId, 
			"NOTE_DESC" => m_noteDesc, 
			"SLOT_INDEX" => m_slotIndex, 
			"PMT_PRODUCT_ID" => m_pmtProductId, 
			"EVENT" => m_event, 
			_ => null, 
		};
	}

	public override void SetVar(string name, object val)
	{
		switch (name)
		{
		case "TIER_ID":
			m_tierId = (int)val;
			break;
		case "NOTE_DESC":
			m_noteDesc = (string)val;
			break;
		case "SLOT_INDEX":
			m_slotIndex = (int)val;
			break;
		case "PMT_PRODUCT_ID":
			m_pmtProductId = (int)val;
			break;
		case "EVENT":
			m_event = (string)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"TIER_ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"SLOT_INDEX" => typeof(int), 
			"PMT_PRODUCT_ID" => typeof(int), 
			"EVENT" => typeof(string), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadShopTierProductSaleDbfRecords loadRecords = new LoadShopTierProductSaleDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ShopTierProductSaleDbfAsset shopTierProductSaleDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ShopTierProductSaleDbfAsset)) as ShopTierProductSaleDbfAsset;
		if (shopTierProductSaleDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"ShopTierProductSaleDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < shopTierProductSaleDbfAsset.Records.Count; i++)
		{
			shopTierProductSaleDbfAsset.Records[i].StripUnusedLocales();
		}
		records = shopTierProductSaleDbfAsset.Records as List<T>;
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
