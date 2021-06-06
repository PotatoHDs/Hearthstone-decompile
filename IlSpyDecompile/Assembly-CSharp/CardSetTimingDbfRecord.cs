using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class CardSetTimingDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_cardId;

	[SerializeField]
	private int m_cardSetId;

	[SerializeField]
	private string m_eventTimingEvent = "always";

	[DbfField("CARD_ID")]
	public int CardId => m_cardId;

	[DbfField("CARD_SET_ID")]
	public int CardSetId => m_cardSetId;

	public CardSetDbfRecord CardSetRecord => GameDbf.CardSet.GetRecord(m_cardSetId);

	[DbfField("EVENT_TIMING_EVENT")]
	public string EventTimingEvent => m_eventTimingEvent;

	public void SetCardId(int v)
	{
		m_cardId = v;
	}

	public void SetCardSetId(int v)
	{
		m_cardSetId = v;
	}

	public void SetEventTimingEvent(string v)
	{
		m_eventTimingEvent = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"CARD_ID" => m_cardId, 
			"CARD_SET_ID" => m_cardSetId, 
			"EVENT_TIMING_EVENT" => m_eventTimingEvent, 
			_ => null, 
		};
	}

	public override void SetVar(string name, object val)
	{
		switch (name)
		{
		case "CARD_ID":
			m_cardId = (int)val;
			break;
		case "CARD_SET_ID":
			m_cardSetId = (int)val;
			break;
		case "EVENT_TIMING_EVENT":
			m_eventTimingEvent = (string)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"CARD_ID" => typeof(int), 
			"CARD_SET_ID" => typeof(int), 
			"EVENT_TIMING_EVENT" => typeof(string), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardSetTimingDbfRecords loadRecords = new LoadCardSetTimingDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardSetTimingDbfAsset cardSetTimingDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardSetTimingDbfAsset)) as CardSetTimingDbfAsset;
		if (cardSetTimingDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"CardSetTimingDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < cardSetTimingDbfAsset.Records.Count; i++)
		{
			cardSetTimingDbfAsset.Records[i].StripUnusedLocales();
		}
		records = cardSetTimingDbfAsset.Records as List<T>;
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
