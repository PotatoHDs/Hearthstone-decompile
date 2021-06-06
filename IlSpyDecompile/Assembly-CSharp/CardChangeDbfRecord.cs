using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class CardChangeDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_cardId;

	[SerializeField]
	private int m_tagId;

	[SerializeField]
	private CardChange.ChangeType m_changeType = CardChange.ParseChangeTypeValue("Invalid");

	[SerializeField]
	private int m_sortOrder;

	[DbfField("CARD_ID")]
	public int CardId => m_cardId;

	[DbfField("TAG_ID")]
	public int TagId => m_tagId;

	[DbfField("CHANGE_TYPE")]
	public CardChange.ChangeType ChangeType => m_changeType;

	[DbfField("SORT_ORDER")]
	public int SortOrder => m_sortOrder;

	public void SetCardId(int v)
	{
		m_cardId = v;
	}

	public void SetTagId(int v)
	{
		m_tagId = v;
	}

	public void SetChangeType(CardChange.ChangeType v)
	{
		m_changeType = v;
	}

	public void SetSortOrder(int v)
	{
		m_sortOrder = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"CARD_ID" => m_cardId, 
			"TAG_ID" => m_tagId, 
			"CHANGE_TYPE" => m_changeType, 
			"SORT_ORDER" => m_sortOrder, 
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
		case "TAG_ID":
			m_tagId = (int)val;
			break;
		case "CHANGE_TYPE":
			if (val == null)
			{
				m_changeType = CardChange.ChangeType.INVALID;
			}
			else if (val is CardChange.ChangeType || val is int)
			{
				m_changeType = (CardChange.ChangeType)val;
			}
			else if (val is string)
			{
				m_changeType = CardChange.ParseChangeTypeValue((string)val);
			}
			break;
		case "SORT_ORDER":
			m_sortOrder = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"CARD_ID" => typeof(int), 
			"TAG_ID" => typeof(int), 
			"CHANGE_TYPE" => typeof(CardChange.ChangeType), 
			"SORT_ORDER" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardChangeDbfRecords loadRecords = new LoadCardChangeDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardChangeDbfAsset cardChangeDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardChangeDbfAsset)) as CardChangeDbfAsset;
		if (cardChangeDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"CardChangeDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < cardChangeDbfAsset.Records.Count; i++)
		{
			cardChangeDbfAsset.Records[i].StripUnusedLocales();
		}
		records = cardChangeDbfAsset.Records as List<T>;
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
