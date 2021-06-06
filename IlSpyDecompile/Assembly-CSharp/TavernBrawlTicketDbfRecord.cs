using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class TavernBrawlTicketDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private bool m_canBeOwned;

	[SerializeField]
	private bool m_canBePurchased;

	[SerializeField]
	private DbfLocValue m_storeName;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("CAN_BE_OWNED")]
	public bool CanBeOwned => m_canBeOwned;

	[DbfField("CAN_BE_PURCHASED")]
	public bool CanBePurchased => m_canBePurchased;

	[DbfField("STORE_NAME")]
	public DbfLocValue StoreName => m_storeName;

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetCanBeOwned(bool v)
	{
		m_canBeOwned = v;
	}

	public void SetCanBePurchased(bool v)
	{
		m_canBePurchased = v;
	}

	public void SetStoreName(DbfLocValue v)
	{
		m_storeName = v;
		v.SetDebugInfo(base.ID, "STORE_NAME");
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_DESC" => m_noteDesc, 
			"CAN_BE_OWNED" => m_canBeOwned, 
			"CAN_BE_PURCHASED" => m_canBePurchased, 
			"STORE_NAME" => m_storeName, 
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
		case "CAN_BE_OWNED":
			m_canBeOwned = (bool)val;
			break;
		case "CAN_BE_PURCHASED":
			m_canBePurchased = (bool)val;
			break;
		case "STORE_NAME":
			m_storeName = (DbfLocValue)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"CAN_BE_OWNED" => typeof(bool), 
			"CAN_BE_PURCHASED" => typeof(bool), 
			"STORE_NAME" => typeof(DbfLocValue), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadTavernBrawlTicketDbfRecords loadRecords = new LoadTavernBrawlTicketDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		TavernBrawlTicketDbfAsset tavernBrawlTicketDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(TavernBrawlTicketDbfAsset)) as TavernBrawlTicketDbfAsset;
		if (tavernBrawlTicketDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"TavernBrawlTicketDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < tavernBrawlTicketDbfAsset.Records.Count; i++)
		{
			tavernBrawlTicketDbfAsset.Records[i].StripUnusedLocales();
		}
		records = tavernBrawlTicketDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_storeName.StripUnusedLocales();
	}
}
