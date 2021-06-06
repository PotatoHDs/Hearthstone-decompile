using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class RewardTrackDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_season;

	[SerializeField]
	private string m_event;

	[SerializeField]
	private int m_accountLicenseId;

	[SerializeField]
	private int m_levelCapSoft = 50;

	[DbfField("SEASON")]
	public int Season => m_season;

	[DbfField("EVENT")]
	public string Event => m_event;

	[DbfField("ACCOUNT_LICENSE_ID")]
	public int AccountLicenseId => m_accountLicenseId;

	public AccountLicenseDbfRecord AccountLicenseRecord => GameDbf.AccountLicense.GetRecord(m_accountLicenseId);

	[DbfField("LEVEL_CAP_SOFT")]
	public int LevelCapSoft => m_levelCapSoft;

	public List<RewardTrackLevelDbfRecord> Levels => GameDbf.RewardTrackLevel.GetRecords((RewardTrackLevelDbfRecord r) => r.RewardTrackId == base.ID);

	public List<XpPerGameTypeDbfRecord> XpPerGameType => GameDbf.XpPerGameType.GetRecords((XpPerGameTypeDbfRecord r) => r.RewardTrackId == base.ID);

	public void SetSeason(int v)
	{
		m_season = v;
	}

	public void SetEvent(string v)
	{
		m_event = v;
	}

	public void SetAccountLicenseId(int v)
	{
		m_accountLicenseId = v;
	}

	public void SetLevelCapSoft(int v)
	{
		m_levelCapSoft = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"SEASON" => m_season, 
			"EVENT" => m_event, 
			"ACCOUNT_LICENSE_ID" => m_accountLicenseId, 
			"LEVEL_CAP_SOFT" => m_levelCapSoft, 
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
		case "SEASON":
			m_season = (int)val;
			break;
		case "EVENT":
			m_event = (string)val;
			break;
		case "ACCOUNT_LICENSE_ID":
			m_accountLicenseId = (int)val;
			break;
		case "LEVEL_CAP_SOFT":
			m_levelCapSoft = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"SEASON" => typeof(int), 
			"EVENT" => typeof(string), 
			"ACCOUNT_LICENSE_ID" => typeof(int), 
			"LEVEL_CAP_SOFT" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadRewardTrackDbfRecords loadRecords = new LoadRewardTrackDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		RewardTrackDbfAsset rewardTrackDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(RewardTrackDbfAsset)) as RewardTrackDbfAsset;
		if (rewardTrackDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"RewardTrackDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < rewardTrackDbfAsset.Records.Count; i++)
		{
			rewardTrackDbfAsset.Records[i].StripUnusedLocales();
		}
		records = rewardTrackDbfAsset.Records as List<T>;
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
