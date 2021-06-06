using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class AdventureModeDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public override object GetVar(string name)
	{
		if (!(name == "ID"))
		{
			if (name == "NOTE_DESC")
			{
				return m_noteDesc;
			}
			return null;
		}
		return base.ID;
	}

	public override void SetVar(string name, object val)
	{
		if (!(name == "ID"))
		{
			if (name == "NOTE_DESC")
			{
				m_noteDesc = (string)val;
			}
		}
		else
		{
			SetID((int)val);
		}
	}

	public override Type GetVarType(string name)
	{
		if (!(name == "ID"))
		{
			if (name == "NOTE_DESC")
			{
				return typeof(string);
			}
			return null;
		}
		return typeof(int);
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAdventureModeDbfRecords loadRecords = new LoadAdventureModeDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AdventureModeDbfAsset adventureModeDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AdventureModeDbfAsset)) as AdventureModeDbfAsset;
		if (adventureModeDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"AdventureModeDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < adventureModeDbfAsset.Records.Count; i++)
		{
			adventureModeDbfAsset.Records[i].StripUnusedLocales();
		}
		records = adventureModeDbfAsset.Records as List<T>;
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
