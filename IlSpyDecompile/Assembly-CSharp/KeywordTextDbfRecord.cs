using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class KeywordTextDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private int m_tag;

	[SerializeField]
	private string m_name;

	[SerializeField]
	private string m_text;

	[SerializeField]
	private string m_refText;

	[SerializeField]
	private string m_collectionText;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("TAG")]
	public int Tag => m_tag;

	[DbfField("NAME")]
	public string Name => m_name;

	[DbfField("TEXT")]
	public string Text => m_text;

	[DbfField("REF_TEXT")]
	public string RefText => m_refText;

	[DbfField("COLLECTION_TEXT")]
	public string CollectionText => m_collectionText;

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetTag(int v)
	{
		m_tag = v;
	}

	public void SetName(string v)
	{
		m_name = v;
	}

	public void SetText(string v)
	{
		m_text = v;
	}

	public void SetRefText(string v)
	{
		m_refText = v;
	}

	public void SetCollectionText(string v)
	{
		m_collectionText = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_DESC" => m_noteDesc, 
			"TAG" => m_tag, 
			"NAME" => m_name, 
			"TEXT" => m_text, 
			"REF_TEXT" => m_refText, 
			"COLLECTION_TEXT" => m_collectionText, 
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
		case "NOTE_DESC":
			m_noteDesc = (string)val;
			break;
		case "TAG":
			m_tag = (int)val;
			break;
		case "NAME":
			m_name = (string)val;
			break;
		case "TEXT":
			m_text = (string)val;
			break;
		case "REF_TEXT":
			m_refText = (string)val;
			break;
		case "COLLECTION_TEXT":
			m_collectionText = (string)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"TAG" => typeof(int), 
			"NAME" => typeof(string), 
			"TEXT" => typeof(string), 
			"REF_TEXT" => typeof(string), 
			"COLLECTION_TEXT" => typeof(string), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadKeywordTextDbfRecords loadRecords = new LoadKeywordTextDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		KeywordTextDbfAsset keywordTextDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(KeywordTextDbfAsset)) as KeywordTextDbfAsset;
		if (keywordTextDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"KeywordTextDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < keywordTextDbfAsset.Records.Count; i++)
		{
			keywordTextDbfAsset.Records[i].StripUnusedLocales();
		}
		records = keywordTextDbfAsset.Records as List<T>;
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
