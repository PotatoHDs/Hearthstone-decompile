using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000144 RID: 324
[Serializable]
public class AccountLicenseDbfRecord : DbfRecord
{
	// Token: 0x170000FD RID: 253
	// (get) Token: 0x06001531 RID: 5425 RVA: 0x000793AE File Offset: 0x000775AE
	[DbfField("LICENSE_ID")]
	public long LicenseId
	{
		get
		{
			return this.m_licenseId;
		}
	}

	// Token: 0x06001532 RID: 5426 RVA: 0x000793B6 File Offset: 0x000775B6
	public void SetLicenseId(long v)
	{
		this.m_licenseId = v;
	}

	// Token: 0x06001533 RID: 5427 RVA: 0x000793BF File Offset: 0x000775BF
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (!(name == "LICENSE_ID"))
		{
			return null;
		}
		return this.m_licenseId;
	}

	// Token: 0x06001534 RID: 5428 RVA: 0x000793F6 File Offset: 0x000775F6
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (!(name == "LICENSE_ID"))
		{
			return;
		}
		this.m_licenseId = (long)val;
	}

	// Token: 0x06001535 RID: 5429 RVA: 0x0007942C File Offset: 0x0007762C
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (!(name == "LICENSE_ID"))
		{
			return null;
		}
		return typeof(long);
	}

	// Token: 0x06001536 RID: 5430 RVA: 0x00079461 File Offset: 0x00077661
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAccountLicenseDbfRecords loadRecords = new LoadAccountLicenseDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001537 RID: 5431 RVA: 0x00079478 File Offset: 0x00077678
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AccountLicenseDbfAsset accountLicenseDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AccountLicenseDbfAsset)) as AccountLicenseDbfAsset;
		if (accountLicenseDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AccountLicenseDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < accountLicenseDbfAsset.Records.Count; i++)
		{
			accountLicenseDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (accountLicenseDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001538 RID: 5432 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001539 RID: 5433 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000E2E RID: 3630
	[SerializeField]
	private long m_licenseId;
}
