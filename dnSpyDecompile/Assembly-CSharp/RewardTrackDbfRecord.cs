using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200025F RID: 607
[Serializable]
public class RewardTrackDbfRecord : DbfRecord
{
	// Token: 0x17000426 RID: 1062
	// (get) Token: 0x06001F9A RID: 8090 RVA: 0x0009E0EA File Offset: 0x0009C2EA
	[DbfField("SEASON")]
	public int Season
	{
		get
		{
			return this.m_season;
		}
	}

	// Token: 0x17000427 RID: 1063
	// (get) Token: 0x06001F9B RID: 8091 RVA: 0x0009E0F2 File Offset: 0x0009C2F2
	[DbfField("EVENT")]
	public string Event
	{
		get
		{
			return this.m_event;
		}
	}

	// Token: 0x17000428 RID: 1064
	// (get) Token: 0x06001F9C RID: 8092 RVA: 0x0009E0FA File Offset: 0x0009C2FA
	[DbfField("ACCOUNT_LICENSE_ID")]
	public int AccountLicenseId
	{
		get
		{
			return this.m_accountLicenseId;
		}
	}

	// Token: 0x17000429 RID: 1065
	// (get) Token: 0x06001F9D RID: 8093 RVA: 0x0009E102 File Offset: 0x0009C302
	public AccountLicenseDbfRecord AccountLicenseRecord
	{
		get
		{
			return GameDbf.AccountLicense.GetRecord(this.m_accountLicenseId);
		}
	}

	// Token: 0x1700042A RID: 1066
	// (get) Token: 0x06001F9E RID: 8094 RVA: 0x0009E114 File Offset: 0x0009C314
	[DbfField("LEVEL_CAP_SOFT")]
	public int LevelCapSoft
	{
		get
		{
			return this.m_levelCapSoft;
		}
	}

	// Token: 0x1700042B RID: 1067
	// (get) Token: 0x06001F9F RID: 8095 RVA: 0x0009E11C File Offset: 0x0009C31C
	public List<RewardTrackLevelDbfRecord> Levels
	{
		get
		{
			return GameDbf.RewardTrackLevel.GetRecords((RewardTrackLevelDbfRecord r) => r.RewardTrackId == base.ID, -1);
		}
	}

	// Token: 0x1700042C RID: 1068
	// (get) Token: 0x06001FA0 RID: 8096 RVA: 0x0009E135 File Offset: 0x0009C335
	public List<XpPerGameTypeDbfRecord> XpPerGameType
	{
		get
		{
			return GameDbf.XpPerGameType.GetRecords((XpPerGameTypeDbfRecord r) => r.RewardTrackId == base.ID, -1);
		}
	}

	// Token: 0x06001FA1 RID: 8097 RVA: 0x0009E14E File Offset: 0x0009C34E
	public void SetSeason(int v)
	{
		this.m_season = v;
	}

	// Token: 0x06001FA2 RID: 8098 RVA: 0x0009E157 File Offset: 0x0009C357
	public void SetEvent(string v)
	{
		this.m_event = v;
	}

	// Token: 0x06001FA3 RID: 8099 RVA: 0x0009E160 File Offset: 0x0009C360
	public void SetAccountLicenseId(int v)
	{
		this.m_accountLicenseId = v;
	}

	// Token: 0x06001FA4 RID: 8100 RVA: 0x0009E169 File Offset: 0x0009C369
	public void SetLevelCapSoft(int v)
	{
		this.m_levelCapSoft = v;
	}

	// Token: 0x06001FA5 RID: 8101 RVA: 0x0009E174 File Offset: 0x0009C374
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "SEASON")
		{
			return this.m_season;
		}
		if (name == "EVENT")
		{
			return this.m_event;
		}
		if (name == "ACCOUNT_LICENSE_ID")
		{
			return this.m_accountLicenseId;
		}
		if (!(name == "LEVEL_CAP_SOFT"))
		{
			return null;
		}
		return this.m_levelCapSoft;
	}

	// Token: 0x06001FA6 RID: 8102 RVA: 0x0009E1FC File Offset: 0x0009C3FC
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "SEASON")
		{
			this.m_season = (int)val;
			return;
		}
		if (name == "EVENT")
		{
			this.m_event = (string)val;
			return;
		}
		if (name == "ACCOUNT_LICENSE_ID")
		{
			this.m_accountLicenseId = (int)val;
			return;
		}
		if (!(name == "LEVEL_CAP_SOFT"))
		{
			return;
		}
		this.m_levelCapSoft = (int)val;
	}

	// Token: 0x06001FA7 RID: 8103 RVA: 0x0009E28C File Offset: 0x0009C48C
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "SEASON")
		{
			return typeof(int);
		}
		if (name == "EVENT")
		{
			return typeof(string);
		}
		if (name == "ACCOUNT_LICENSE_ID")
		{
			return typeof(int);
		}
		if (!(name == "LEVEL_CAP_SOFT"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x06001FA8 RID: 8104 RVA: 0x0009E314 File Offset: 0x0009C514
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadRewardTrackDbfRecords loadRecords = new LoadRewardTrackDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001FA9 RID: 8105 RVA: 0x0009E32C File Offset: 0x0009C52C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		RewardTrackDbfAsset rewardTrackDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(RewardTrackDbfAsset)) as RewardTrackDbfAsset;
		if (rewardTrackDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("RewardTrackDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < rewardTrackDbfAsset.Records.Count; i++)
		{
			rewardTrackDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (rewardTrackDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001FAA RID: 8106 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001FAB RID: 8107 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x040011F9 RID: 4601
	[SerializeField]
	private int m_season;

	// Token: 0x040011FA RID: 4602
	[SerializeField]
	private string m_event;

	// Token: 0x040011FB RID: 4603
	[SerializeField]
	private int m_accountLicenseId;

	// Token: 0x040011FC RID: 4604
	[SerializeField]
	private int m_levelCapSoft = 50;
}
