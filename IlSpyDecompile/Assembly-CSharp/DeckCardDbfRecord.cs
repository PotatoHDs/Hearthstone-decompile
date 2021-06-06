using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class DeckCardDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_nextCardId;

	[SerializeField]
	private int m_cardId;

	[SerializeField]
	private int m_deckId;

	[SerializeField]
	private DbfLocValue m_description;

	[DbfField("NEXT_CARD")]
	public int NextCard => m_nextCardId;

	public DeckCardDbfRecord NextCardRecord => GameDbf.DeckCard.GetRecord(m_nextCardId);

	[DbfField("CARD_ID")]
	public int CardId => m_cardId;

	public CardDbfRecord CardRecord => GameDbf.Card.GetRecord(m_cardId);

	[DbfField("DECK_ID")]
	public int DeckId => m_deckId;

	[DbfField("DESCRIPTION")]
	public DbfLocValue Description => m_description;

	public void SetNextCard(int v)
	{
		m_nextCardId = v;
	}

	public void SetCardId(int v)
	{
		m_cardId = v;
	}

	public void SetDeckId(int v)
	{
		m_deckId = v;
	}

	public void SetDescription(DbfLocValue v)
	{
		m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NEXT_CARD" => m_nextCardId, 
			"CARD_ID" => m_cardId, 
			"DECK_ID" => m_deckId, 
			"DESCRIPTION" => m_description, 
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
		case "NEXT_CARD":
			m_nextCardId = (int)val;
			break;
		case "CARD_ID":
			m_cardId = (int)val;
			break;
		case "DECK_ID":
			m_deckId = (int)val;
			break;
		case "DESCRIPTION":
			m_description = (DbfLocValue)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NEXT_CARD" => typeof(int), 
			"CARD_ID" => typeof(int), 
			"DECK_ID" => typeof(int), 
			"DESCRIPTION" => typeof(DbfLocValue), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadDeckCardDbfRecords loadRecords = new LoadDeckCardDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		DeckCardDbfAsset deckCardDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(DeckCardDbfAsset)) as DeckCardDbfAsset;
		if (deckCardDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"DeckCardDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < deckCardDbfAsset.Records.Count; i++)
		{
			deckCardDbfAsset.Records[i].StripUnusedLocales();
		}
		records = deckCardDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_description.StripUnusedLocales();
	}
}
