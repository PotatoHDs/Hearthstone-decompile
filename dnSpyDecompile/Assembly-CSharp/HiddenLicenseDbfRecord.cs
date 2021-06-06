using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001FC RID: 508
[Serializable]
public class HiddenLicenseDbfRecord : DbfRecord
{
	// Token: 0x1700031E RID: 798
	// (get) Token: 0x06001C1B RID: 7195 RVA: 0x0009234A File Offset: 0x0009054A
	[DbfField("ACCOUNT_LICENSE_ID")]
	public int AccountLicenseId
	{
		get
		{
			return this.m_accountLicenseId;
		}
	}

	// Token: 0x1700031F RID: 799
	// (get) Token: 0x06001C1C RID: 7196 RVA: 0x00092352 File Offset: 0x00090552
	public AccountLicenseDbfRecord AccountLicenseRecord
	{
		get
		{
			return GameDbf.AccountLicense.GetRecord(this.m_accountLicenseId);
		}
	}

	// Token: 0x17000320 RID: 800
	// (get) Token: 0x06001C1D RID: 7197 RVA: 0x00092364 File Offset: 0x00090564
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x17000321 RID: 801
	// (get) Token: 0x06001C1E RID: 7198 RVA: 0x0009236C File Offset: 0x0009056C
	[DbfField("IS_BLOCKING")]
	public bool IsBlocking
	{
		get
		{
			return this.m_isBlocking;
		}
	}

	// Token: 0x06001C1F RID: 7199 RVA: 0x00092374 File Offset: 0x00090574
	public void SetAccountLicenseId(int v)
	{
		this.m_accountLicenseId = v;
	}

	// Token: 0x06001C20 RID: 7200 RVA: 0x0009237D File Offset: 0x0009057D
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001C21 RID: 7201 RVA: 0x00092386 File Offset: 0x00090586
	public void SetIsBlocking(bool v)
	{
		this.m_isBlocking = v;
	}

	// Token: 0x06001C22 RID: 7202 RVA: 0x00092390 File Offset: 0x00090590
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "ACCOUNT_LICENSE_ID")
		{
			return this.m_accountLicenseId;
		}
		if (name == "NOTE_DESC")
		{
			return this.m_noteDesc;
		}
		if (!(name == "IS_BLOCKING"))
		{
			return null;
		}
		return this.m_isBlocking;
	}

	// Token: 0x06001C23 RID: 7203 RVA: 0x00092400 File Offset: 0x00090600
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "ACCOUNT_LICENSE_ID")
		{
			this.m_accountLicenseId = (int)val;
			return;
		}
		if (name == "NOTE_DESC")
		{
			this.m_noteDesc = (string)val;
			return;
		}
		if (!(name == "IS_BLOCKING"))
		{
			return;
		}
		this.m_isBlocking = (bool)val;
	}

	// Token: 0x06001C24 RID: 7204 RVA: 0x00092478 File Offset: 0x00090678
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "ACCOUNT_LICENSE_ID")
		{
			return typeof(int);
		}
		if (name == "NOTE_DESC")
		{
			return typeof(string);
		}
		if (!(name == "IS_BLOCKING"))
		{
			return null;
		}
		return typeof(bool);
	}

	// Token: 0x06001C25 RID: 7205 RVA: 0x000924E8 File Offset: 0x000906E8
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadHiddenLicenseDbfRecords loadRecords = new LoadHiddenLicenseDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001C26 RID: 7206 RVA: 0x00092500 File Offset: 0x00090700
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		HiddenLicenseDbfAsset hiddenLicenseDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(HiddenLicenseDbfAsset)) as HiddenLicenseDbfAsset;
		if (hiddenLicenseDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("HiddenLicenseDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < hiddenLicenseDbfAsset.Records.Count; i++)
		{
			hiddenLicenseDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (hiddenLicenseDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001C27 RID: 7207 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001C28 RID: 7208 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x040010D7 RID: 4311
	[SerializeField]
	private int m_accountLicenseId;

	// Token: 0x040010D8 RID: 4312
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x040010D9 RID: 4313
	[SerializeField]
	private bool m_isBlocking = true;
}
