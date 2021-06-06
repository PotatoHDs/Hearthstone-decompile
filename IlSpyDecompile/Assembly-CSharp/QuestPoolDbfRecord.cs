using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class QuestPoolDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_grantDayOfWeek = -1;

	[SerializeField]
	private int m_grantHourOfDay;

	[SerializeField]
	private int m_numQuestsGranted = 1;

	[SerializeField]
	private int m_maxQuestsActive = 1;

	[SerializeField]
	private int m_rerollCountMax;

	[SerializeField]
	private QuestPool.QuestPoolType m_questPoolType = QuestPool.QuestPoolType.DAILY;

	[DbfField("GRANT_DAY_OF_WEEK")]
	public int GrantDayOfWeek => m_grantDayOfWeek;

	[DbfField("GRANT_HOUR_OF_DAY")]
	public int GrantHourOfDay => m_grantHourOfDay;

	[DbfField("NUM_QUESTS_GRANTED")]
	public int NumQuestsGranted => m_numQuestsGranted;

	[DbfField("MAX_QUESTS_ACTIVE")]
	public int MaxQuestsActive => m_maxQuestsActive;

	[DbfField("REROLL_COUNT_MAX")]
	public int RerollCountMax => m_rerollCountMax;

	[DbfField("QUEST_POOL_TYPE")]
	public QuestPool.QuestPoolType QuestPoolType => m_questPoolType;

	public void SetGrantDayOfWeek(int v)
	{
		m_grantDayOfWeek = v;
	}

	public void SetGrantHourOfDay(int v)
	{
		m_grantHourOfDay = v;
	}

	public void SetNumQuestsGranted(int v)
	{
		m_numQuestsGranted = v;
	}

	public void SetMaxQuestsActive(int v)
	{
		m_maxQuestsActive = v;
	}

	public void SetRerollCountMax(int v)
	{
		m_rerollCountMax = v;
	}

	public void SetQuestPoolType(QuestPool.QuestPoolType v)
	{
		m_questPoolType = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"GRANT_DAY_OF_WEEK" => m_grantDayOfWeek, 
			"GRANT_HOUR_OF_DAY" => m_grantHourOfDay, 
			"NUM_QUESTS_GRANTED" => m_numQuestsGranted, 
			"MAX_QUESTS_ACTIVE" => m_maxQuestsActive, 
			"REROLL_COUNT_MAX" => m_rerollCountMax, 
			"QUEST_POOL_TYPE" => m_questPoolType, 
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
		case "GRANT_DAY_OF_WEEK":
			m_grantDayOfWeek = (int)val;
			break;
		case "GRANT_HOUR_OF_DAY":
			m_grantHourOfDay = (int)val;
			break;
		case "NUM_QUESTS_GRANTED":
			m_numQuestsGranted = (int)val;
			break;
		case "MAX_QUESTS_ACTIVE":
			m_maxQuestsActive = (int)val;
			break;
		case "REROLL_COUNT_MAX":
			m_rerollCountMax = (int)val;
			break;
		case "QUEST_POOL_TYPE":
			if (val == null)
			{
				m_questPoolType = QuestPool.QuestPoolType.NONE;
			}
			else if (val is QuestPool.QuestPoolType || val is int)
			{
				m_questPoolType = (QuestPool.QuestPoolType)val;
			}
			else if (val is string)
			{
				m_questPoolType = QuestPool.ParseQuestPoolTypeValue((string)val);
			}
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"GRANT_DAY_OF_WEEK" => typeof(int), 
			"GRANT_HOUR_OF_DAY" => typeof(int), 
			"NUM_QUESTS_GRANTED" => typeof(int), 
			"MAX_QUESTS_ACTIVE" => typeof(int), 
			"REROLL_COUNT_MAX" => typeof(int), 
			"QUEST_POOL_TYPE" => typeof(QuestPool.QuestPoolType), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadQuestPoolDbfRecords loadRecords = new LoadQuestPoolDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		QuestPoolDbfAsset questPoolDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(QuestPoolDbfAsset)) as QuestPoolDbfAsset;
		if (questPoolDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"QuestPoolDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < questPoolDbfAsset.Records.Count; i++)
		{
			questPoolDbfAsset.Records[i].StripUnusedLocales();
		}
		records = questPoolDbfAsset.Records as List<T>;
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
