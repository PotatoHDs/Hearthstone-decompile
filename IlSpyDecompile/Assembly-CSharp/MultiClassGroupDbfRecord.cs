using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class MultiClassGroupDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private string m_iconAssetPath;

	[SerializeField]
	private int m_cardColorType;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("ICON_ASSET_PATH")]
	public string IconAssetPath => m_iconAssetPath;

	[DbfField("CARD_COLOR_TYPE")]
	public int CardColorType => m_cardColorType;

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetIconAssetPath(string v)
	{
		m_iconAssetPath = v;
	}

	public void SetCardColorType(int v)
	{
		m_cardColorType = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_DESC" => m_noteDesc, 
			"ICON_ASSET_PATH" => m_iconAssetPath, 
			"CARD_COLOR_TYPE" => m_cardColorType, 
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
		case "ICON_ASSET_PATH":
			m_iconAssetPath = (string)val;
			break;
		case "CARD_COLOR_TYPE":
			m_cardColorType = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"ICON_ASSET_PATH" => typeof(string), 
			"CARD_COLOR_TYPE" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadMultiClassGroupDbfRecords loadRecords = new LoadMultiClassGroupDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		MultiClassGroupDbfAsset multiClassGroupDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(MultiClassGroupDbfAsset)) as MultiClassGroupDbfAsset;
		if (multiClassGroupDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"MultiClassGroupDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < multiClassGroupDbfAsset.Records.Count; i++)
		{
			multiClassGroupDbfAsset.Records[i].StripUnusedLocales();
		}
		records = multiClassGroupDbfAsset.Records as List<T>;
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
