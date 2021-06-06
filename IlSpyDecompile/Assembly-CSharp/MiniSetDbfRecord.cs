using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class MiniSetDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_deckId;

	[SerializeField]
	private int m_boosterId;

	[DbfField("DECK_ID")]
	public int DeckId => m_deckId;

	public DeckDbfRecord DeckRecord => GameDbf.Deck.GetRecord(m_deckId);

	[DbfField("BOOSTER_ID")]
	public int BoosterId => m_boosterId;

	public BoosterDbfRecord BoosterRecord => GameDbf.Booster.GetRecord(m_boosterId);

	public void SetDeckId(int v)
	{
		m_deckId = v;
	}

	public void SetBoosterId(int v)
	{
		m_boosterId = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"DECK_ID" => m_deckId, 
			"BOOSTER_ID" => m_boosterId, 
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
		case "DECK_ID":
			m_deckId = (int)val;
			break;
		case "BOOSTER_ID":
			m_boosterId = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"DECK_ID" => typeof(int), 
			"BOOSTER_ID" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadMiniSetDbfRecords loadRecords = new LoadMiniSetDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		MiniSetDbfAsset miniSetDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(MiniSetDbfAsset)) as MiniSetDbfAsset;
		if (miniSetDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"MiniSetDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < miniSetDbfAsset.Records.Count; i++)
		{
			miniSetDbfAsset.Records[i].StripUnusedLocales();
		}
		records = miniSetDbfAsset.Records as List<T>;
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
