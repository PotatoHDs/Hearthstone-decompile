using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class DeckDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteName;

	[SerializeField]
	private int m_topCardId;

	[SerializeField]
	private DbfLocValue m_name;

	[SerializeField]
	private DbfLocValue m_description;

	[SerializeField]
	private DbfLocValue m_altDescription;

	[SerializeField]
	private int m_preconClass;

	[DbfField("NOTE_NAME")]
	public string NoteName => m_noteName;

	[DbfField("TOP_CARD_ID")]
	public int TopCardId => m_topCardId;

	public DeckCardDbfRecord TopCardRecord => GameDbf.DeckCard.GetRecord(m_topCardId);

	[DbfField("NAME")]
	public DbfLocValue Name => m_name;

	[DbfField("DESCRIPTION")]
	public DbfLocValue Description => m_description;

	[DbfField("ALT_DESCRIPTION")]
	public DbfLocValue AltDescription => m_altDescription;

	[Obsolete("no longer used")]
	[DbfField("PRECON_CLASS")]
	public int PreconClass => m_preconClass;

	public List<DeckCardDbfRecord> Cards => GameDbf.DeckCard.GetRecords((DeckCardDbfRecord r) => r.DeckId == base.ID);

	public void SetNoteName(string v)
	{
		m_noteName = v;
	}

	public void SetTopCardId(int v)
	{
		m_topCardId = v;
	}

	public void SetName(DbfLocValue v)
	{
		m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	public void SetDescription(DbfLocValue v)
	{
		m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	public void SetAltDescription(DbfLocValue v)
	{
		m_altDescription = v;
		v.SetDebugInfo(base.ID, "ALT_DESCRIPTION");
	}

	[Obsolete("no longer used")]
	public void SetPreconClass(int v)
	{
		m_preconClass = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_NAME" => m_noteName, 
			"TOP_CARD_ID" => m_topCardId, 
			"NAME" => m_name, 
			"DESCRIPTION" => m_description, 
			"ALT_DESCRIPTION" => m_altDescription, 
			"PRECON_CLASS" => m_preconClass, 
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
		case "NOTE_NAME":
			m_noteName = (string)val;
			break;
		case "TOP_CARD_ID":
			m_topCardId = (int)val;
			break;
		case "NAME":
			m_name = (DbfLocValue)val;
			break;
		case "DESCRIPTION":
			m_description = (DbfLocValue)val;
			break;
		case "ALT_DESCRIPTION":
			m_altDescription = (DbfLocValue)val;
			break;
		case "PRECON_CLASS":
			m_preconClass = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_NAME" => typeof(string), 
			"TOP_CARD_ID" => typeof(int), 
			"NAME" => typeof(DbfLocValue), 
			"DESCRIPTION" => typeof(DbfLocValue), 
			"ALT_DESCRIPTION" => typeof(DbfLocValue), 
			"PRECON_CLASS" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadDeckDbfRecords loadRecords = new LoadDeckDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		DeckDbfAsset deckDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(DeckDbfAsset)) as DeckDbfAsset;
		if (deckDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"DeckDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < deckDbfAsset.Records.Count; i++)
		{
			deckDbfAsset.Records[i].StripUnusedLocales();
		}
		records = deckDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_name.StripUnusedLocales();
		m_description.StripUnusedLocales();
		m_altDescription.StripUnusedLocales();
	}
}
