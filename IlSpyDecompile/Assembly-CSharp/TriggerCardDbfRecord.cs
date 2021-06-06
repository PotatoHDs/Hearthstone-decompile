using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class TriggerCardDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_triggerId;

	[SerializeField]
	private int m_cardId;

	[DbfField("TRIGGER_ID")]
	public int TriggerId => m_triggerId;

	[DbfField("CARD_ID")]
	public int CardId => m_cardId;

	public CardDbfRecord CardRecord => GameDbf.Card.GetRecord(m_cardId);

	public void SetTriggerId(int v)
	{
		m_triggerId = v;
	}

	public void SetCardId(int v)
	{
		m_cardId = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"TRIGGER_ID" => m_triggerId, 
			"CARD_ID" => m_cardId, 
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
		case "TRIGGER_ID":
			m_triggerId = (int)val;
			break;
		case "CARD_ID":
			m_cardId = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"TRIGGER_ID" => typeof(int), 
			"CARD_ID" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadTriggerCardDbfRecords loadRecords = new LoadTriggerCardDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		TriggerCardDbfAsset triggerCardDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(TriggerCardDbfAsset)) as TriggerCardDbfAsset;
		if (triggerCardDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"TriggerCardDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < triggerCardDbfAsset.Records.Count; i++)
		{
			triggerCardDbfAsset.Records[i].StripUnusedLocales();
		}
		records = triggerCardDbfAsset.Records as List<T>;
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
