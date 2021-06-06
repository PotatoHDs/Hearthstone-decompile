using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class ShopTierDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private string m_style;

	[SerializeField]
	private string m_tags;

	[SerializeField]
	private DbfLocValue m_header;

	[SerializeField]
	private int m_sortOrder;

	[SerializeField]
	private bool m_disabled;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("STYLE")]
	public string Style => m_style;

	[DbfField("TAGS")]
	public string Tags => m_tags;

	[DbfField("HEADER")]
	public DbfLocValue Header => m_header;

	[DbfField("SORT_ORDER")]
	public int SortOrder => m_sortOrder;

	[DbfField("DISABLED")]
	public bool Disabled => m_disabled;

	public List<ShopTierProductSaleDbfRecord> ProductSales => GameDbf.ShopTierProductSale.GetRecords((ShopTierProductSaleDbfRecord r) => r.TierId == base.ID);

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetStyle(string v)
	{
		m_style = v;
	}

	public void SetTags(string v)
	{
		m_tags = v;
	}

	public void SetHeader(DbfLocValue v)
	{
		m_header = v;
		v.SetDebugInfo(base.ID, "HEADER");
	}

	public void SetSortOrder(int v)
	{
		m_sortOrder = v;
	}

	public void SetDisabled(bool v)
	{
		m_disabled = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_DESC" => m_noteDesc, 
			"STYLE" => m_style, 
			"TAGS" => m_tags, 
			"HEADER" => m_header, 
			"SORT_ORDER" => m_sortOrder, 
			"DISABLED" => m_disabled, 
			_ => null, 
		};
	}

	public override void SetVar(string name, object val)
	{
		switch (name)
		{
		case "ID":
			SetID((int)val);
			break;
		case "NOTE_DESC":
			m_noteDesc = (string)val;
			break;
		case "STYLE":
			m_style = (string)val;
			break;
		case "TAGS":
			m_tags = (string)val;
			break;
		case "HEADER":
			m_header = (DbfLocValue)val;
			break;
		case "SORT_ORDER":
			m_sortOrder = (int)val;
			break;
		case "DISABLED":
			m_disabled = (bool)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"STYLE" => typeof(string), 
			"TAGS" => typeof(string), 
			"HEADER" => typeof(DbfLocValue), 
			"SORT_ORDER" => typeof(int), 
			"DISABLED" => typeof(bool), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadShopTierDbfRecords loadRecords = new LoadShopTierDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ShopTierDbfAsset shopTierDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ShopTierDbfAsset)) as ShopTierDbfAsset;
		if (shopTierDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"ShopTierDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < shopTierDbfAsset.Records.Count; i++)
		{
			shopTierDbfAsset.Records[i].StripUnusedLocales();
		}
		records = shopTierDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_header.StripUnusedLocales();
	}
}
