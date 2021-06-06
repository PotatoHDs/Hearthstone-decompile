using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class RewardBagDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_bagId;

	[SerializeField]
	private RewardBag.Reward m_reward = RewardBag.ParseRewardValue("unknown");

	[SerializeField]
	private int m_base;

	[SerializeField]
	private int m_rewardData;

	[DbfField("BAG_ID")]
	public int BagId => m_bagId;

	[DbfField("REWARD")]
	public RewardBag.Reward Reward => m_reward;

	[DbfField("BASE")]
	public int Base => m_base;

	[DbfField("REWARD_DATA")]
	public int RewardData => m_rewardData;

	public void SetBagId(int v)
	{
		m_bagId = v;
	}

	public void SetReward(RewardBag.Reward v)
	{
		m_reward = v;
	}

	public void SetBase(int v)
	{
		m_base = v;
	}

	public void SetRewardData(int v)
	{
		m_rewardData = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"BAG_ID" => m_bagId, 
			"REWARD" => m_reward, 
			"BASE" => m_base, 
			"REWARD_DATA" => m_rewardData, 
			_ => null, 
		};
	}

	public override void SetVar(string name, object val)
	{
		switch (name)
		{
		case "BAG_ID":
			m_bagId = (int)val;
			break;
		case "REWARD":
			if (val == null)
			{
				m_reward = RewardBag.Reward.NONE;
			}
			else if (val is RewardBag.Reward || val is int)
			{
				m_reward = (RewardBag.Reward)val;
			}
			else if (val is string)
			{
				m_reward = RewardBag.ParseRewardValue((string)val);
			}
			break;
		case "BASE":
			m_base = (int)val;
			break;
		case "REWARD_DATA":
			m_rewardData = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"BAG_ID" => typeof(int), 
			"REWARD" => typeof(RewardBag.Reward), 
			"BASE" => typeof(int), 
			"REWARD_DATA" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadRewardBagDbfRecords loadRecords = new LoadRewardBagDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		RewardBagDbfAsset rewardBagDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(RewardBagDbfAsset)) as RewardBagDbfAsset;
		if (rewardBagDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"RewardBagDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < rewardBagDbfAsset.Records.Count; i++)
		{
			rewardBagDbfAsset.Records[i].StripUnusedLocales();
		}
		records = rewardBagDbfAsset.Records as List<T>;
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
