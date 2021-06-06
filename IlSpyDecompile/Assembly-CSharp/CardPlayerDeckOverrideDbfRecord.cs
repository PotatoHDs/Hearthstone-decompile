using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class CardPlayerDeckOverrideDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_cardId;

	[SerializeField]
	private int m_heroCardId;

	[SerializeField]
	private DbfLocValue m_deckName;

	[SerializeField]
	private DbfLocValue m_addToDeckWarningHeader;

	[SerializeField]
	private DbfLocValue m_addToDeckWarningBody;

	[DbfField("CARD_ID")]
	public int CardId => m_cardId;

	[DbfField("HERO_CARD_ID")]
	public int HeroCardId => m_heroCardId;

	public CardDbfRecord HeroCardRecord => GameDbf.Card.GetRecord(m_heroCardId);

	[DbfField("DECK_NAME")]
	public DbfLocValue DeckName => m_deckName;

	[DbfField("ADD_TO_DECK_WARNING_HEADER")]
	public DbfLocValue AddToDeckWarningHeader => m_addToDeckWarningHeader;

	[DbfField("ADD_TO_DECK_WARNING_BODY")]
	public DbfLocValue AddToDeckWarningBody => m_addToDeckWarningBody;

	public void SetCardId(int v)
	{
		m_cardId = v;
	}

	public void SetHeroCardId(int v)
	{
		m_heroCardId = v;
	}

	public void SetDeckName(DbfLocValue v)
	{
		m_deckName = v;
		v.SetDebugInfo(base.ID, "DECK_NAME");
	}

	public void SetAddToDeckWarningHeader(DbfLocValue v)
	{
		m_addToDeckWarningHeader = v;
		v.SetDebugInfo(base.ID, "ADD_TO_DECK_WARNING_HEADER");
	}

	public void SetAddToDeckWarningBody(DbfLocValue v)
	{
		m_addToDeckWarningBody = v;
		v.SetDebugInfo(base.ID, "ADD_TO_DECK_WARNING_BODY");
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"CARD_ID" => m_cardId, 
			"HERO_CARD_ID" => m_heroCardId, 
			"DECK_NAME" => m_deckName, 
			"ADD_TO_DECK_WARNING_HEADER" => m_addToDeckWarningHeader, 
			"ADD_TO_DECK_WARNING_BODY" => m_addToDeckWarningBody, 
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
		case "CARD_ID":
			m_cardId = (int)val;
			break;
		case "HERO_CARD_ID":
			m_heroCardId = (int)val;
			break;
		case "DECK_NAME":
			m_deckName = (DbfLocValue)val;
			break;
		case "ADD_TO_DECK_WARNING_HEADER":
			m_addToDeckWarningHeader = (DbfLocValue)val;
			break;
		case "ADD_TO_DECK_WARNING_BODY":
			m_addToDeckWarningBody = (DbfLocValue)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"CARD_ID" => typeof(int), 
			"HERO_CARD_ID" => typeof(int), 
			"DECK_NAME" => typeof(DbfLocValue), 
			"ADD_TO_DECK_WARNING_HEADER" => typeof(DbfLocValue), 
			"ADD_TO_DECK_WARNING_BODY" => typeof(DbfLocValue), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardPlayerDeckOverrideDbfRecords loadRecords = new LoadCardPlayerDeckOverrideDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardPlayerDeckOverrideDbfAsset cardPlayerDeckOverrideDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardPlayerDeckOverrideDbfAsset)) as CardPlayerDeckOverrideDbfAsset;
		if (cardPlayerDeckOverrideDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"CardPlayerDeckOverrideDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < cardPlayerDeckOverrideDbfAsset.Records.Count; i++)
		{
			cardPlayerDeckOverrideDbfAsset.Records[i].StripUnusedLocales();
		}
		records = cardPlayerDeckOverrideDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_deckName.StripUnusedLocales();
		m_addToDeckWarningHeader.StripUnusedLocales();
		m_addToDeckWarningBody.StripUnusedLocales();
	}
}
