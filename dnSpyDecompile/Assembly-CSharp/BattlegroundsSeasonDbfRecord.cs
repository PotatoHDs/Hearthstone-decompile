using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000183 RID: 387
[Serializable]
public class BattlegroundsSeasonDbfRecord : DbfRecord
{
	// Token: 0x17000204 RID: 516
	// (get) Token: 0x06001812 RID: 6162 RVA: 0x0008409A File Offset: 0x0008229A
	[DbfField("EVENT")]
	public string Event
	{
		get
		{
			return this.m_event;
		}
	}

	// Token: 0x17000205 RID: 517
	// (get) Token: 0x06001813 RID: 6163 RVA: 0x000840A2 File Offset: 0x000822A2
	[DbfField("ACCOUNT_LICENSE_ID")]
	public int AccountLicenseId
	{
		get
		{
			return this.m_accountLicenseId;
		}
	}

	// Token: 0x17000206 RID: 518
	// (get) Token: 0x06001814 RID: 6164 RVA: 0x000840AA File Offset: 0x000822AA
	public AccountLicenseDbfRecord AccountLicenseRecord
	{
		get
		{
			return GameDbf.AccountLicense.GetRecord(this.m_accountLicenseId);
		}
	}

	// Token: 0x06001815 RID: 6165 RVA: 0x000840BC File Offset: 0x000822BC
	public void SetEvent(string v)
	{
		this.m_event = v;
	}

	// Token: 0x06001816 RID: 6166 RVA: 0x000840C5 File Offset: 0x000822C5
	public void SetAccountLicenseId(int v)
	{
		this.m_accountLicenseId = v;
	}

	// Token: 0x06001817 RID: 6167 RVA: 0x000840CE File Offset: 0x000822CE
	public override object GetVar(string name)
	{
		if (name == "EVENT")
		{
			return this.m_event;
		}
		if (!(name == "ACCOUNT_LICENSE_ID"))
		{
			return null;
		}
		return this.m_accountLicenseId;
	}

	// Token: 0x06001818 RID: 6168 RVA: 0x00084100 File Offset: 0x00082300
	public override void SetVar(string name, object val)
	{
		if (name == "EVENT")
		{
			this.m_event = (string)val;
			return;
		}
		if (!(name == "ACCOUNT_LICENSE_ID"))
		{
			return;
		}
		this.m_accountLicenseId = (int)val;
	}

	// Token: 0x06001819 RID: 6169 RVA: 0x00084136 File Offset: 0x00082336
	public override Type GetVarType(string name)
	{
		if (name == "EVENT")
		{
			return typeof(string);
		}
		if (!(name == "ACCOUNT_LICENSE_ID"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x0600181A RID: 6170 RVA: 0x0008416B File Offset: 0x0008236B
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadBattlegroundsSeasonDbfRecords loadRecords = new LoadBattlegroundsSeasonDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x0600181B RID: 6171 RVA: 0x00084184 File Offset: 0x00082384
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		BattlegroundsSeasonDbfAsset battlegroundsSeasonDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(BattlegroundsSeasonDbfAsset)) as BattlegroundsSeasonDbfAsset;
		if (battlegroundsSeasonDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("BattlegroundsSeasonDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < battlegroundsSeasonDbfAsset.Records.Count; i++)
		{
			battlegroundsSeasonDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (battlegroundsSeasonDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x0600181C RID: 6172 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x0600181D RID: 6173 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000F2B RID: 3883
	[SerializeField]
	private string m_event;

	// Token: 0x04000F2C RID: 3884
	[SerializeField]
	private int m_accountLicenseId;
}
