using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class CardAdditonalSearchTermsDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_cardId;

	[SerializeField]
	private DbfLocValue m_searchTerm;

	[DbfField("CARD_ID")]
	public int CardId => m_cardId;

	[DbfField("SEARCH_TERM")]
	public DbfLocValue SearchTerm => m_searchTerm;

	public void SetCardId(int v)
	{
		m_cardId = v;
	}

	public void SetSearchTerm(DbfLocValue v)
	{
		m_searchTerm = v;
		v.SetDebugInfo(base.ID, "SEARCH_TERM");
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"CARD_ID" => m_cardId, 
			"SEARCH_TERM" => m_searchTerm, 
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
		case "SEARCH_TERM":
			m_searchTerm = (DbfLocValue)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"CARD_ID" => typeof(int), 
			"SEARCH_TERM" => typeof(DbfLocValue), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardAdditonalSearchTermsDbfRecords loadRecords = new LoadCardAdditonalSearchTermsDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardAdditonalSearchTermsDbfAsset cardAdditonalSearchTermsDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardAdditonalSearchTermsDbfAsset)) as CardAdditonalSearchTermsDbfAsset;
		if (cardAdditonalSearchTermsDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"CardAdditonalSearchTermsDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < cardAdditonalSearchTermsDbfAsset.Records.Count; i++)
		{
			cardAdditonalSearchTermsDbfAsset.Records[i].StripUnusedLocales();
		}
		records = cardAdditonalSearchTermsDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_searchTerm.StripUnusedLocales();
	}
}
