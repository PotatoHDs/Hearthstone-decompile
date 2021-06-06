using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class QuestDialogDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_onCompleteBannerId;

	[SerializeField]
	private string m_noteDesc;

	[DbfField("ON_COMPLETE_BANNER_ID")]
	public int OnCompleteBannerId => m_onCompleteBannerId;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	public List<QuestDialogOnCompleteDbfRecord> OnCompleteData => GameDbf.QuestDialogOnComplete.GetRecords((QuestDialogOnCompleteDbfRecord r) => r.QuestDialogId == base.ID);

	public List<QuestDialogOnProgress1DbfRecord> OnProgress1 => GameDbf.QuestDialogOnProgress1.GetRecords((QuestDialogOnProgress1DbfRecord r) => r.QuestDialogId == base.ID);

	public List<QuestDialogOnProgress2DbfRecord> OnProgress2 => GameDbf.QuestDialogOnProgress2.GetRecords((QuestDialogOnProgress2DbfRecord r) => r.QuestDialogId == base.ID);

	public List<QuestDialogOnReceivedDbfRecord> OnReceivedData => GameDbf.QuestDialogOnReceived.GetRecords((QuestDialogOnReceivedDbfRecord r) => r.QuestDialogId == base.ID);

	public void SetOnCompleteBannerId(int v)
	{
		m_onCompleteBannerId = v;
	}

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"ON_COMPLETE_BANNER_ID" => m_onCompleteBannerId, 
			"NOTE_DESC" => m_noteDesc, 
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
		case "ON_COMPLETE_BANNER_ID":
			m_onCompleteBannerId = (int)val;
			break;
		case "NOTE_DESC":
			m_noteDesc = (string)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"ON_COMPLETE_BANNER_ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadQuestDialogDbfRecords loadRecords = new LoadQuestDialogDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		QuestDialogDbfAsset questDialogDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(QuestDialogDbfAsset)) as QuestDialogDbfAsset;
		if (questDialogDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"QuestDialogDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < questDialogDbfAsset.Records.Count; i++)
		{
			questDialogDbfAsset.Records[i].StripUnusedLocales();
		}
		records = questDialogDbfAsset.Records as List<T>;
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
