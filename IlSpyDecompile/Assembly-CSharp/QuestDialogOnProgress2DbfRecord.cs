using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class QuestDialogOnProgress2DbfRecord : DbfRecord
{
	[SerializeField]
	private int m_questDialogId;

	[SerializeField]
	private int m_playOrder;

	[SerializeField]
	private string m_prefabName;

	[SerializeField]
	private string m_audioName;

	[SerializeField]
	private bool m_altBubblePosition;

	[SerializeField]
	private double m_waitBefore;

	[SerializeField]
	private double m_waitAfter;

	[SerializeField]
	private bool m_persistPrefab;

	[DbfField("QUEST_DIALOG_ID")]
	public int QuestDialogId => m_questDialogId;

	[DbfField("PLAY_ORDER")]
	public int PlayOrder => m_playOrder;

	[DbfField("PREFAB_NAME")]
	public string PrefabName => m_prefabName;

	[DbfField("AUDIO_NAME")]
	public string AudioName => m_audioName;

	[DbfField("ALT_BUBBLE_POSITION")]
	public bool AltBubblePosition => m_altBubblePosition;

	[DbfField("WAIT_BEFORE")]
	public double WaitBefore => m_waitBefore;

	[DbfField("WAIT_AFTER")]
	public double WaitAfter => m_waitAfter;

	[DbfField("PERSIST_PREFAB")]
	public bool PersistPrefab => m_persistPrefab;

	public void SetQuestDialogId(int v)
	{
		m_questDialogId = v;
	}

	public void SetPlayOrder(int v)
	{
		m_playOrder = v;
	}

	public void SetPrefabName(string v)
	{
		m_prefabName = v;
	}

	public void SetAudioName(string v)
	{
		m_audioName = v;
	}

	public void SetAltBubblePosition(bool v)
	{
		m_altBubblePosition = v;
	}

	public void SetWaitBefore(double v)
	{
		m_waitBefore = v;
	}

	public void SetWaitAfter(double v)
	{
		m_waitAfter = v;
	}

	public void SetPersistPrefab(bool v)
	{
		m_persistPrefab = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"QUEST_DIALOG_ID" => m_questDialogId, 
			"PLAY_ORDER" => m_playOrder, 
			"PREFAB_NAME" => m_prefabName, 
			"AUDIO_NAME" => m_audioName, 
			"ALT_BUBBLE_POSITION" => m_altBubblePosition, 
			"WAIT_BEFORE" => m_waitBefore, 
			"WAIT_AFTER" => m_waitAfter, 
			"PERSIST_PREFAB" => m_persistPrefab, 
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
		case "QUEST_DIALOG_ID":
			m_questDialogId = (int)val;
			break;
		case "PLAY_ORDER":
			m_playOrder = (int)val;
			break;
		case "PREFAB_NAME":
			m_prefabName = (string)val;
			break;
		case "AUDIO_NAME":
			m_audioName = (string)val;
			break;
		case "ALT_BUBBLE_POSITION":
			m_altBubblePosition = (bool)val;
			break;
		case "WAIT_BEFORE":
			m_waitBefore = (double)val;
			break;
		case "WAIT_AFTER":
			m_waitAfter = (double)val;
			break;
		case "PERSIST_PREFAB":
			m_persistPrefab = (bool)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"QUEST_DIALOG_ID" => typeof(int), 
			"PLAY_ORDER" => typeof(int), 
			"PREFAB_NAME" => typeof(string), 
			"AUDIO_NAME" => typeof(string), 
			"ALT_BUBBLE_POSITION" => typeof(bool), 
			"WAIT_BEFORE" => typeof(double), 
			"WAIT_AFTER" => typeof(double), 
			"PERSIST_PREFAB" => typeof(bool), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadQuestDialogOnProgress2DbfRecords loadRecords = new LoadQuestDialogOnProgress2DbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		QuestDialogOnProgress2DbfAsset questDialogOnProgress2DbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(QuestDialogOnProgress2DbfAsset)) as QuestDialogOnProgress2DbfAsset;
		if (questDialogOnProgress2DbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"QuestDialogOnProgress2DbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < questDialogOnProgress2DbfAsset.Records.Count; i++)
		{
			questDialogOnProgress2DbfAsset.Records[i].StripUnusedLocales();
		}
		records = questDialogOnProgress2DbfAsset.Records as List<T>;
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
