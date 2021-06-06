using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class QuestDbfRecord : DbfRecord
{
	[SerializeField]
	private DbfLocValue m_name;

	[SerializeField]
	private DbfLocValue m_description;

	[SerializeField]
	private string m_icon;

	[SerializeField]
	private int m_quota;

	[SerializeField]
	private int m_nextInChainId;

	[SerializeField]
	private int m_questPoolId;

	[SerializeField]
	private bool m_poolGuaranteed;

	[SerializeField]
	private int m_rewardTrackXp;

	[SerializeField]
	private int m_rewardListId;

	[SerializeField]
	private int m_proxyForLegacyId;

	[DbfField("NAME")]
	public DbfLocValue Name => m_name;

	[DbfField("DESCRIPTION")]
	public DbfLocValue Description => m_description;

	[DbfField("ICON")]
	public string Icon => m_icon;

	[DbfField("QUOTA")]
	public int Quota => m_quota;

	[DbfField("NEXT_IN_CHAIN")]
	public int NextInChain => m_nextInChainId;

	public QuestDbfRecord NextInChainRecord => GameDbf.Quest.GetRecord(m_nextInChainId);

	[DbfField("QUEST_POOL")]
	public int QuestPool => m_questPoolId;

	public QuestPoolDbfRecord QuestPoolRecord => GameDbf.QuestPool.GetRecord(m_questPoolId);

	[DbfField("POOL_GUARANTEED")]
	public bool PoolGuaranteed => m_poolGuaranteed;

	[DbfField("REWARD_TRACK_XP")]
	public int RewardTrackXp => m_rewardTrackXp;

	[DbfField("REWARD_LIST")]
	public int RewardList => m_rewardListId;

	public RewardListDbfRecord RewardListRecord => GameDbf.RewardList.GetRecord(m_rewardListId);

	[DbfField("PROXY_FOR_LEGACY_ID")]
	public int ProxyForLegacyId => m_proxyForLegacyId;

	public AchieveDbfRecord ProxyForLegacyRecord => GameDbf.Achieve.GetRecord(m_proxyForLegacyId);

	public void SetName(DbfLocValue v)
	{
		m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	public void SetDescription(DbfLocValue v)
	{
		m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	public void SetIcon(string v)
	{
		m_icon = v;
	}

	public void SetQuota(int v)
	{
		m_quota = v;
	}

	public void SetNextInChain(int v)
	{
		m_nextInChainId = v;
	}

	public void SetQuestPool(int v)
	{
		m_questPoolId = v;
	}

	public void SetPoolGuaranteed(bool v)
	{
		m_poolGuaranteed = v;
	}

	public void SetRewardTrackXp(int v)
	{
		m_rewardTrackXp = v;
	}

	public void SetRewardList(int v)
	{
		m_rewardListId = v;
	}

	public void SetProxyForLegacyId(int v)
	{
		m_proxyForLegacyId = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NAME" => m_name, 
			"DESCRIPTION" => m_description, 
			"ICON" => m_icon, 
			"QUOTA" => m_quota, 
			"NEXT_IN_CHAIN" => m_nextInChainId, 
			"QUEST_POOL" => m_questPoolId, 
			"POOL_GUARANTEED" => m_poolGuaranteed, 
			"REWARD_TRACK_XP" => m_rewardTrackXp, 
			"REWARD_LIST" => m_rewardListId, 
			"PROXY_FOR_LEGACY_ID" => m_proxyForLegacyId, 
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
		case "NAME":
			m_name = (DbfLocValue)val;
			break;
		case "DESCRIPTION":
			m_description = (DbfLocValue)val;
			break;
		case "ICON":
			m_icon = (string)val;
			break;
		case "QUOTA":
			m_quota = (int)val;
			break;
		case "NEXT_IN_CHAIN":
			m_nextInChainId = (int)val;
			break;
		case "QUEST_POOL":
			m_questPoolId = (int)val;
			break;
		case "POOL_GUARANTEED":
			m_poolGuaranteed = (bool)val;
			break;
		case "REWARD_TRACK_XP":
			m_rewardTrackXp = (int)val;
			break;
		case "REWARD_LIST":
			m_rewardListId = (int)val;
			break;
		case "PROXY_FOR_LEGACY_ID":
			m_proxyForLegacyId = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NAME" => typeof(DbfLocValue), 
			"DESCRIPTION" => typeof(DbfLocValue), 
			"ICON" => typeof(string), 
			"QUOTA" => typeof(int), 
			"NEXT_IN_CHAIN" => typeof(int), 
			"QUEST_POOL" => typeof(int), 
			"POOL_GUARANTEED" => typeof(bool), 
			"REWARD_TRACK_XP" => typeof(int), 
			"REWARD_LIST" => typeof(int), 
			"PROXY_FOR_LEGACY_ID" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadQuestDbfRecords loadRecords = new LoadQuestDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		QuestDbfAsset questDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(QuestDbfAsset)) as QuestDbfAsset;
		if (questDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"QuestDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < questDbfAsset.Records.Count; i++)
		{
			questDbfAsset.Records[i].StripUnusedLocales();
		}
		records = questDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_name.StripUnusedLocales();
		m_description.StripUnusedLocales();
	}
}
