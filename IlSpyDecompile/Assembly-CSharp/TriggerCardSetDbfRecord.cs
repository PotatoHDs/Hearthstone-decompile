using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class TriggerCardSetDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_triggerId;

	[SerializeField]
	private int m_cardSetId;

	[DbfField("TRIGGER_ID")]
	public int TriggerId => m_triggerId;

	[DbfField("CARD_SET_ID")]
	public int CardSetId => m_cardSetId;

	public CardSetDbfRecord CardSetRecord => GameDbf.CardSet.GetRecord(m_cardSetId);

	public void SetTriggerId(int v)
	{
		m_triggerId = v;
	}

	public void SetCardSetId(int v)
	{
		m_cardSetId = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"TRIGGER_ID" => m_triggerId, 
			"CARD_SET_ID" => m_cardSetId, 
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
		case "CARD_SET_ID":
			m_cardSetId = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"TRIGGER_ID" => typeof(int), 
			"CARD_SET_ID" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadTriggerCardSetDbfRecords loadRecords = new LoadTriggerCardSetDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		TriggerCardSetDbfAsset triggerCardSetDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(TriggerCardSetDbfAsset)) as TriggerCardSetDbfAsset;
		if (triggerCardSetDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"TriggerCardSetDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < triggerCardSetDbfAsset.Records.Count; i++)
		{
			triggerCardSetDbfAsset.Records[i].StripUnusedLocales();
		}
		records = triggerCardSetDbfAsset.Records as List<T>;
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
