using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000298 RID: 664
[Serializable]
public class XpPerGameTypeDbfRecord : DbfRecord
{
	// Token: 0x170004BD RID: 1213
	// (get) Token: 0x06002193 RID: 8595 RVA: 0x000A472E File Offset: 0x000A292E
	[DbfField("REWARD_TRACK_ID")]
	public int RewardTrackId
	{
		get
		{
			return this.m_rewardTrackId;
		}
	}

	// Token: 0x06002194 RID: 8596 RVA: 0x000A4736 File Offset: 0x000A2936
	public void SetRewardTrackId(int v)
	{
		this.m_rewardTrackId = v;
	}

	// Token: 0x06002195 RID: 8597 RVA: 0x000A473F File Offset: 0x000A293F
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (!(name == "REWARD_TRACK_ID"))
		{
			return null;
		}
		return this.m_rewardTrackId;
	}

	// Token: 0x06002196 RID: 8598 RVA: 0x000A4776 File Offset: 0x000A2976
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (!(name == "REWARD_TRACK_ID"))
		{
			return;
		}
		this.m_rewardTrackId = (int)val;
	}

	// Token: 0x06002197 RID: 8599 RVA: 0x000A47AC File Offset: 0x000A29AC
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (!(name == "REWARD_TRACK_ID"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x06002198 RID: 8600 RVA: 0x000A47E1 File Offset: 0x000A29E1
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadXpPerGameTypeDbfRecords loadRecords = new LoadXpPerGameTypeDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06002199 RID: 8601 RVA: 0x000A47F8 File Offset: 0x000A29F8
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		XpPerGameTypeDbfAsset xpPerGameTypeDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(XpPerGameTypeDbfAsset)) as XpPerGameTypeDbfAsset;
		if (xpPerGameTypeDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("XpPerGameTypeDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < xpPerGameTypeDbfAsset.Records.Count; i++)
		{
			xpPerGameTypeDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (xpPerGameTypeDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x0600219A RID: 8602 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x0600219B RID: 8603 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04001296 RID: 4758
	[SerializeField]
	private int m_rewardTrackId;
}
