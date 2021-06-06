using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000292 RID: 658
[Serializable]
public class VisualBlacklistDbfRecord : DbfRecord
{
	// Token: 0x17000498 RID: 1176
	// (get) Token: 0x06002136 RID: 8502 RVA: 0x000A2E7E File Offset: 0x000A107E
	[DbfField("ACHIEVE_ID")]
	public int AchieveId
	{
		get
		{
			return this.m_achieveId;
		}
	}

	// Token: 0x17000499 RID: 1177
	// (get) Token: 0x06002137 RID: 8503 RVA: 0x000A2E86 File Offset: 0x000A1086
	[DbfField("BLACKLIST_ACHIEVE_ID")]
	public int BlacklistAchieveId
	{
		get
		{
			return this.m_blacklistAchieveId;
		}
	}

	// Token: 0x1700049A RID: 1178
	// (get) Token: 0x06002138 RID: 8504 RVA: 0x000A2E8E File Offset: 0x000A108E
	public AchieveDbfRecord BlacklistAchieveRecord
	{
		get
		{
			return GameDbf.Achieve.GetRecord(this.m_blacklistAchieveId);
		}
	}

	// Token: 0x06002139 RID: 8505 RVA: 0x000A2EA0 File Offset: 0x000A10A0
	public void SetAchieveId(int v)
	{
		this.m_achieveId = v;
	}

	// Token: 0x0600213A RID: 8506 RVA: 0x000A2EA9 File Offset: 0x000A10A9
	public void SetBlacklistAchieveId(int v)
	{
		this.m_blacklistAchieveId = v;
	}

	// Token: 0x0600213B RID: 8507 RVA: 0x000A2EB4 File Offset: 0x000A10B4
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "ACHIEVE_ID")
		{
			return this.m_achieveId;
		}
		if (!(name == "BLACKLIST_ACHIEVE_ID"))
		{
			return null;
		}
		return this.m_blacklistAchieveId;
	}

	// Token: 0x0600213C RID: 8508 RVA: 0x000A2F10 File Offset: 0x000A1110
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "ACHIEVE_ID")
		{
			this.m_achieveId = (int)val;
			return;
		}
		if (!(name == "BLACKLIST_ACHIEVE_ID"))
		{
			return;
		}
		this.m_blacklistAchieveId = (int)val;
	}

	// Token: 0x0600213D RID: 8509 RVA: 0x000A2F6C File Offset: 0x000A116C
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "ACHIEVE_ID")
		{
			return typeof(int);
		}
		if (!(name == "BLACKLIST_ACHIEVE_ID"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x0600213E RID: 8510 RVA: 0x000A2FC4 File Offset: 0x000A11C4
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadVisualBlacklistDbfRecords loadRecords = new LoadVisualBlacklistDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x0600213F RID: 8511 RVA: 0x000A2FDC File Offset: 0x000A11DC
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		VisualBlacklistDbfAsset visualBlacklistDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(VisualBlacklistDbfAsset)) as VisualBlacklistDbfAsset;
		if (visualBlacklistDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("VisualBlacklistDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < visualBlacklistDbfAsset.Records.Count; i++)
		{
			visualBlacklistDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (visualBlacklistDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06002140 RID: 8512 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06002141 RID: 8513 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04001272 RID: 4722
	[SerializeField]
	private int m_achieveId;

	// Token: 0x04001273 RID: 4723
	[SerializeField]
	private int m_blacklistAchieveId;
}
