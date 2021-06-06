using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class QuestModifierDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_event = "none";

	[SerializeField]
	private int m_quota;

	[SerializeField]
	private string m_description;

	[SerializeField]
	private string m_styleName;

	[SerializeField]
	private DbfLocValue m_questName;

	[DbfField("EVENT")]
	public string Event => m_event;

	[DbfField("QUOTA")]
	public int Quota => m_quota;

	[DbfField("DESCRIPTION")]
	public string Description => m_description;

	[DbfField("STYLE_NAME")]
	public string StyleName => m_styleName;

	[DbfField("QUEST_NAME")]
	public DbfLocValue QuestName => m_questName;

	public void SetEvent(string v)
	{
		m_event = v;
	}

	public void SetQuota(int v)
	{
		m_quota = v;
	}

	public void SetDescription(string v)
	{
		m_description = v;
	}

	public void SetStyleName(string v)
	{
		m_styleName = v;
	}

	public void SetQuestName(DbfLocValue v)
	{
		m_questName = v;
		v.SetDebugInfo(base.ID, "QUEST_NAME");
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"EVENT" => m_event, 
			"QUOTA" => m_quota, 
			"DESCRIPTION" => m_description, 
			"STYLE_NAME" => m_styleName, 
			"QUEST_NAME" => m_questName, 
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
		case "EVENT":
			m_event = (string)val;
			break;
		case "QUOTA":
			m_quota = (int)val;
			break;
		case "DESCRIPTION":
			m_description = (string)val;
			break;
		case "STYLE_NAME":
			m_styleName = (string)val;
			break;
		case "QUEST_NAME":
			m_questName = (DbfLocValue)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"EVENT" => typeof(string), 
			"QUOTA" => typeof(int), 
			"DESCRIPTION" => typeof(string), 
			"STYLE_NAME" => typeof(string), 
			"QUEST_NAME" => typeof(DbfLocValue), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadQuestModifierDbfRecords loadRecords = new LoadQuestModifierDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		QuestModifierDbfAsset questModifierDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(QuestModifierDbfAsset)) as QuestModifierDbfAsset;
		if (questModifierDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"QuestModifierDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < questModifierDbfAsset.Records.Count; i++)
		{
			questModifierDbfAsset.Records[i].StripUnusedLocales();
		}
		records = questModifierDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_questName.StripUnusedLocales();
	}
}
