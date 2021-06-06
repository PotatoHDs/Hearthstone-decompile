using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class RewardLevelDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_rewardTrackId;

	[SerializeField]
	private int m_levelRequirement;

	[SerializeField]
	private RewardLevel.SubscriptionRequirement m_subscriptionRequirement = RewardLevel.ParseSubscriptionRequirementValue("free");

	[SerializeField]
	private int m_rewardListId;

	[DbfField("REWARD_TRACK_ID")]
	public int RewardTrackId => m_rewardTrackId;

	[DbfField("LEVEL_REQUIREMENT")]
	public int LevelRequirement => m_levelRequirement;

	[DbfField("SUBSCRIPTION_REQUIREMENT")]
	public RewardLevel.SubscriptionRequirement SubscriptionRequirement => m_subscriptionRequirement;

	[DbfField("REWARD_LIST")]
	public int RewardList => m_rewardListId;

	public RewardListDbfRecord RewardListRecord => GameDbf.RewardList.GetRecord(m_rewardListId);

	public void SetRewardTrackId(int v)
	{
		m_rewardTrackId = v;
	}

	public void SetLevelRequirement(int v)
	{
		m_levelRequirement = v;
	}

	public void SetSubscriptionRequirement(RewardLevel.SubscriptionRequirement v)
	{
		m_subscriptionRequirement = v;
	}

	public void SetRewardList(int v)
	{
		m_rewardListId = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"REWARD_TRACK_ID" => m_rewardTrackId, 
			"LEVEL_REQUIREMENT" => m_levelRequirement, 
			"SUBSCRIPTION_REQUIREMENT" => m_subscriptionRequirement, 
			"REWARD_LIST" => m_rewardListId, 
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
		case "REWARD_TRACK_ID":
			m_rewardTrackId = (int)val;
			break;
		case "LEVEL_REQUIREMENT":
			m_levelRequirement = (int)val;
			break;
		case "SUBSCRIPTION_REQUIREMENT":
			if (val == null)
			{
				m_subscriptionRequirement = RewardLevel.SubscriptionRequirement.FREE;
			}
			else if (val is RewardLevel.SubscriptionRequirement || val is int)
			{
				m_subscriptionRequirement = (RewardLevel.SubscriptionRequirement)val;
			}
			else if (val is string)
			{
				m_subscriptionRequirement = RewardLevel.ParseSubscriptionRequirementValue((string)val);
			}
			break;
		case "REWARD_LIST":
			m_rewardListId = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"REWARD_TRACK_ID" => typeof(int), 
			"LEVEL_REQUIREMENT" => typeof(int), 
			"SUBSCRIPTION_REQUIREMENT" => typeof(RewardLevel.SubscriptionRequirement), 
			"REWARD_LIST" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadRewardLevelDbfRecords loadRecords = new LoadRewardLevelDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		RewardLevelDbfAsset rewardLevelDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(RewardLevelDbfAsset)) as RewardLevelDbfAsset;
		if (rewardLevelDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"RewardLevelDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < rewardLevelDbfAsset.Records.Count; i++)
		{
			rewardLevelDbfAsset.Records[i].StripUnusedLocales();
		}
		records = rewardLevelDbfAsset.Records as List<T>;
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
