using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class SellableDeckDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_deckTemplateId;

	[SerializeField]
	private int m_boosterId;

	[DbfField("DECK_TEMPLATE_ID")]
	public int DeckTemplateId => m_deckTemplateId;

	public DeckTemplateDbfRecord DeckTemplateRecord => GameDbf.DeckTemplate.GetRecord(m_deckTemplateId);

	[DbfField("BOOSTER_ID")]
	public int BoosterId => m_boosterId;

	public BoosterDbfRecord BoosterRecord => GameDbf.Booster.GetRecord(m_boosterId);

	public void SetDeckTemplateId(int v)
	{
		m_deckTemplateId = v;
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
			"DECK_TEMPLATE_ID" => m_deckTemplateId, 
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
		case "DECK_TEMPLATE_ID":
			m_deckTemplateId = (int)val;
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
			"DECK_TEMPLATE_ID" => typeof(int), 
			"BOOSTER_ID" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadSellableDeckDbfRecords loadRecords = new LoadSellableDeckDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		SellableDeckDbfAsset sellableDeckDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(SellableDeckDbfAsset)) as SellableDeckDbfAsset;
		if (sellableDeckDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"SellableDeckDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < sellableDeckDbfAsset.Records.Count; i++)
		{
			sellableDeckDbfAsset.Records[i].StripUnusedLocales();
		}
		records = sellableDeckDbfAsset.Records as List<T>;
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
