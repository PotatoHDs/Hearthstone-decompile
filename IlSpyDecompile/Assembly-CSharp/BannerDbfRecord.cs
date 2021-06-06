using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class BannerDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private DbfLocValue m_headerText;

	[SerializeField]
	private DbfLocValue m_text;

	[SerializeField]
	private string m_prefab;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("HEADER_TEXT")]
	public DbfLocValue HeaderText => m_headerText;

	[DbfField("TEXT")]
	public DbfLocValue Text => m_text;

	[DbfField("PREFAB")]
	public string Prefab => m_prefab;

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetHeaderText(DbfLocValue v)
	{
		m_headerText = v;
		v.SetDebugInfo(base.ID, "HEADER_TEXT");
	}

	public void SetText(DbfLocValue v)
	{
		m_text = v;
		v.SetDebugInfo(base.ID, "TEXT");
	}

	public void SetPrefab(string v)
	{
		m_prefab = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_DESC" => m_noteDesc, 
			"HEADER_TEXT" => m_headerText, 
			"TEXT" => m_text, 
			"PREFAB" => m_prefab, 
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
		case "HEADER_TEXT":
			m_headerText = (DbfLocValue)val;
			break;
		case "TEXT":
			m_text = (DbfLocValue)val;
			break;
		case "PREFAB":
			m_prefab = (string)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"HEADER_TEXT" => typeof(DbfLocValue), 
			"TEXT" => typeof(DbfLocValue), 
			"PREFAB" => typeof(string), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadBannerDbfRecords loadRecords = new LoadBannerDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		BannerDbfAsset bannerDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(BannerDbfAsset)) as BannerDbfAsset;
		if (bannerDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"BannerDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < bannerDbfAsset.Records.Count; i++)
		{
			bannerDbfAsset.Records[i].StripUnusedLocales();
		}
		records = bannerDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_headerText.StripUnusedLocales();
		m_text.StripUnusedLocales();
	}
}
