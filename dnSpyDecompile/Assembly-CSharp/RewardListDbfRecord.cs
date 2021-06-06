using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200025C RID: 604
[Serializable]
public class RewardListDbfRecord : DbfRecord
{
	// Token: 0x17000423 RID: 1059
	// (get) Token: 0x06001F88 RID: 8072 RVA: 0x0009DE42 File Offset: 0x0009C042
	[DbfField("DESCRIPTION")]
	public DbfLocValue Description
	{
		get
		{
			return this.m_description;
		}
	}

	// Token: 0x17000424 RID: 1060
	// (get) Token: 0x06001F89 RID: 8073 RVA: 0x0009DE4A File Offset: 0x0009C04A
	[DbfField("CHOOSE_ONE")]
	public bool ChooseOne
	{
		get
		{
			return this.m_chooseOne;
		}
	}

	// Token: 0x17000425 RID: 1061
	// (get) Token: 0x06001F8A RID: 8074 RVA: 0x0009DE52 File Offset: 0x0009C052
	public List<RewardItemDbfRecord> RewardItems
	{
		get
		{
			return GameDbf.RewardItem.GetRecords((RewardItemDbfRecord r) => r.RewardListId == base.ID, -1);
		}
	}

	// Token: 0x06001F8B RID: 8075 RVA: 0x0009DE6B File Offset: 0x0009C06B
	public void SetDescription(DbfLocValue v)
	{
		this.m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	// Token: 0x06001F8C RID: 8076 RVA: 0x0009DE85 File Offset: 0x0009C085
	public void SetChooseOne(bool v)
	{
		this.m_chooseOne = v;
	}

	// Token: 0x06001F8D RID: 8077 RVA: 0x0009DE90 File Offset: 0x0009C090
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "DESCRIPTION")
		{
			return this.m_description;
		}
		if (!(name == "CHOOSE_ONE"))
		{
			return null;
		}
		return this.m_chooseOne;
	}

	// Token: 0x06001F8E RID: 8078 RVA: 0x0009DEE8 File Offset: 0x0009C0E8
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "DESCRIPTION")
		{
			this.m_description = (DbfLocValue)val;
			return;
		}
		if (!(name == "CHOOSE_ONE"))
		{
			return;
		}
		this.m_chooseOne = (bool)val;
	}

	// Token: 0x06001F8F RID: 8079 RVA: 0x0009DF44 File Offset: 0x0009C144
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "DESCRIPTION")
		{
			return typeof(DbfLocValue);
		}
		if (!(name == "CHOOSE_ONE"))
		{
			return null;
		}
		return typeof(bool);
	}

	// Token: 0x06001F90 RID: 8080 RVA: 0x0009DF9C File Offset: 0x0009C19C
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadRewardListDbfRecords loadRecords = new LoadRewardListDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001F91 RID: 8081 RVA: 0x0009DFB4 File Offset: 0x0009C1B4
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		RewardListDbfAsset rewardListDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(RewardListDbfAsset)) as RewardListDbfAsset;
		if (rewardListDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("RewardListDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < rewardListDbfAsset.Records.Count; i++)
		{
			rewardListDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (rewardListDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001F92 RID: 8082 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001F93 RID: 8083 RVA: 0x0009E033 File Offset: 0x0009C233
	public override void StripUnusedLocales()
	{
		this.m_description.StripUnusedLocales();
	}

	// Token: 0x040011F5 RID: 4597
	[SerializeField]
	private DbfLocValue m_description;

	// Token: 0x040011F6 RID: 4598
	[SerializeField]
	private bool m_chooseOne;
}
