using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class BattlegroundsSeasonDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_event;

	[SerializeField]
	private int m_accountLicenseId;

	[DbfField("EVENT")]
	public string Event => m_event;

	[DbfField("ACCOUNT_LICENSE_ID")]
	public int AccountLicenseId => m_accountLicenseId;

	public AccountLicenseDbfRecord AccountLicenseRecord => GameDbf.AccountLicense.GetRecord(m_accountLicenseId);

	public void SetEvent(string v)
	{
		m_event = v;
	}

	public void SetAccountLicenseId(int v)
	{
		m_accountLicenseId = v;
	}

	public override object GetVar(string name)
	{
		if (!(name == "EVENT"))
		{
			if (name == "ACCOUNT_LICENSE_ID")
			{
				return m_accountLicenseId;
			}
			return null;
		}
		return m_event;
	}

	public override void SetVar(string name, object val)
	{
		if (!(name == "EVENT"))
		{
			if (name == "ACCOUNT_LICENSE_ID")
			{
				m_accountLicenseId = (int)val;
			}
		}
		else
		{
			m_event = (string)val;
		}
	}

	public override Type GetVarType(string name)
	{
		if (!(name == "EVENT"))
		{
			if (name == "ACCOUNT_LICENSE_ID")
			{
				return typeof(int);
			}
			return null;
		}
		return typeof(string);
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadBattlegroundsSeasonDbfRecords loadRecords = new LoadBattlegroundsSeasonDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		BattlegroundsSeasonDbfAsset battlegroundsSeasonDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(BattlegroundsSeasonDbfAsset)) as BattlegroundsSeasonDbfAsset;
		if (battlegroundsSeasonDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"BattlegroundsSeasonDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < battlegroundsSeasonDbfAsset.Records.Count; i++)
		{
			battlegroundsSeasonDbfAsset.Records[i].StripUnusedLocales();
		}
		records = battlegroundsSeasonDbfAsset.Records as List<T>;
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
