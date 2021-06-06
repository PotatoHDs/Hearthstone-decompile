using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class RewardTrackLevelDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_rewardTrackId;

	[SerializeField]
	private int m_level;

	[SerializeField]
	private int m_xpNeeded;

	[SerializeField]
	private string m_styleName;

	[SerializeField]
	private int m_freeRewardListId;

	[SerializeField]
	private int m_paidRewardListId;

	[DbfField("REWARD_TRACK_ID")]
	public int RewardTrackId => m_rewardTrackId;

	[DbfField("LEVEL")]
	public int Level => m_level;

	[DbfField("XP_NEEDED")]
	public int XpNeeded => m_xpNeeded;

	[DbfField("STYLE_NAME")]
	public string StyleName => m_styleName;

	[DbfField("FREE_REWARD_LIST")]
	public int FreeRewardList => m_freeRewardListId;

	public RewardListDbfRecord FreeRewardListRecord => GameDbf.RewardList.GetRecord(m_freeRewardListId);

	[DbfField("PAID_REWARD_LIST")]
	public int PaidRewardList => m_paidRewardListId;

	public RewardListDbfRecord PaidRewardListRecord => GameDbf.RewardList.GetRecord(m_paidRewardListId);

	public void SetRewardTrackId(int v)
	{
		m_rewardTrackId = v;
	}

	public void SetLevel(int v)
	{
		m_level = v;
	}

	public void SetXpNeeded(int v)
	{
		m_xpNeeded = v;
	}

	public void SetStyleName(string v)
	{
		m_styleName = v;
	}

	public void SetFreeRewardList(int v)
	{
		m_freeRewardListId = v;
	}

	public void SetPaidRewardList(int v)
	{
		m_paidRewardListId = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"REWARD_TRACK_ID" => m_rewardTrackId, 
			"LEVEL" => m_level, 
			"XP_NEEDED" => m_xpNeeded, 
			"STYLE_NAME" => m_styleName, 
			"FREE_REWARD_LIST" => m_freeRewardListId, 
			"PAID_REWARD_LIST" => m_paidRewardListId, 
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
		case "LEVEL":
			m_level = (int)val;
			break;
		case "XP_NEEDED":
			m_xpNeeded = (int)val;
			break;
		case "STYLE_NAME":
			m_styleName = (string)val;
			break;
		case "FREE_REWARD_LIST":
			m_freeRewardListId = (int)val;
			break;
		case "PAID_REWARD_LIST":
			m_paidRewardListId = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"REWARD_TRACK_ID" => typeof(int), 
			"LEVEL" => typeof(int), 
			"XP_NEEDED" => typeof(int), 
			"STYLE_NAME" => typeof(string), 
			"FREE_REWARD_LIST" => typeof(int), 
			"PAID_REWARD_LIST" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadRewardTrackLevelDbfRecords loadRecords = new LoadRewardTrackLevelDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		RewardTrackLevelDbfAsset rewardTrackLevelDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(RewardTrackLevelDbfAsset)) as RewardTrackLevelDbfAsset;
		if (rewardTrackLevelDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"RewardTrackLevelDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < rewardTrackLevelDbfAsset.Records.Count; i++)
		{
			rewardTrackLevelDbfAsset.Records[i].StripUnusedLocales();
		}
		records = rewardTrackLevelDbfAsset.Records as List<T>;
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
