using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class PvpdrRewardDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_pvpdrSeasonId;

	[SerializeField]
	private int m_wins;

	[SerializeField]
	private int m_rewardChestId;

	[DbfField("PVPDR_SEASON_ID")]
	public int PvpdrSeasonId => m_pvpdrSeasonId;

	[DbfField("WINS")]
	public int Wins => m_wins;

	[DbfField("REWARD_CHEST_ID")]
	public int RewardChestId => m_rewardChestId;

	public RewardChestDbfRecord RewardChestRecord => GameDbf.RewardChest.GetRecord(m_rewardChestId);

	public void SetPvpdrSeasonId(int v)
	{
		m_pvpdrSeasonId = v;
	}

	public void SetWins(int v)
	{
		m_wins = v;
	}

	public void SetRewardChestId(int v)
	{
		m_rewardChestId = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"PVPDR_SEASON_ID" => m_pvpdrSeasonId, 
			"WINS" => m_wins, 
			"REWARD_CHEST_ID" => m_rewardChestId, 
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
		case "PVPDR_SEASON_ID":
			m_pvpdrSeasonId = (int)val;
			break;
		case "WINS":
			m_wins = (int)val;
			break;
		case "REWARD_CHEST_ID":
			m_rewardChestId = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"PVPDR_SEASON_ID" => typeof(int), 
			"WINS" => typeof(int), 
			"REWARD_CHEST_ID" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadPvpdrRewardDbfRecords loadRecords = new LoadPvpdrRewardDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		PvpdrRewardDbfAsset pvpdrRewardDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(PvpdrRewardDbfAsset)) as PvpdrRewardDbfAsset;
		if (pvpdrRewardDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"PvpdrRewardDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < pvpdrRewardDbfAsset.Records.Count; i++)
		{
			pvpdrRewardDbfAsset.Records[i].StripUnusedLocales();
		}
		records = pvpdrRewardDbfAsset.Records as List<T>;
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
