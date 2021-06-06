using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class CreditsYearDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private string m_contentsFilename;

	[SerializeField]
	private DbfLocValue m_buttonLabel;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("CONTENTS_FILENAME")]
	public string ContentsFilename => m_contentsFilename;

	[DbfField("BUTTON_LABEL")]
	public DbfLocValue ButtonLabel => m_buttonLabel;

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetContentsFilename(string v)
	{
		m_contentsFilename = v;
	}

	public void SetButtonLabel(DbfLocValue v)
	{
		m_buttonLabel = v;
		v.SetDebugInfo(base.ID, "BUTTON_LABEL");
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_DESC" => m_noteDesc, 
			"CONTENTS_FILENAME" => m_contentsFilename, 
			"BUTTON_LABEL" => m_buttonLabel, 
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
		case "CONTENTS_FILENAME":
			m_contentsFilename = (string)val;
			break;
		case "BUTTON_LABEL":
			m_buttonLabel = (DbfLocValue)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"CONTENTS_FILENAME" => typeof(string), 
			"BUTTON_LABEL" => typeof(DbfLocValue), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCreditsYearDbfRecords loadRecords = new LoadCreditsYearDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CreditsYearDbfAsset creditsYearDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CreditsYearDbfAsset)) as CreditsYearDbfAsset;
		if (creditsYearDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"CreditsYearDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < creditsYearDbfAsset.Records.Count; i++)
		{
			creditsYearDbfAsset.Records[i].StripUnusedLocales();
		}
		records = creditsYearDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_buttonLabel.StripUnusedLocales();
	}
}
