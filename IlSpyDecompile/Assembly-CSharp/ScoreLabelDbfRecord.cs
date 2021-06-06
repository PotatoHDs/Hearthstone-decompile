using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class ScoreLabelDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private DbfLocValue m_text;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("TEXT")]
	public DbfLocValue Text => m_text;

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetText(DbfLocValue v)
	{
		m_text = v;
		v.SetDebugInfo(base.ID, "TEXT");
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_DESC" => m_noteDesc, 
			"TEXT" => m_text, 
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
		case "TEXT":
			m_text = (DbfLocValue)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"TEXT" => typeof(DbfLocValue), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadScoreLabelDbfRecords loadRecords = new LoadScoreLabelDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ScoreLabelDbfAsset scoreLabelDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ScoreLabelDbfAsset)) as ScoreLabelDbfAsset;
		if (scoreLabelDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"ScoreLabelDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < scoreLabelDbfAsset.Records.Count; i++)
		{
			scoreLabelDbfAsset.Records[i].StripUnusedLocales();
		}
		records = scoreLabelDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_text.StripUnusedLocales();
	}
}
