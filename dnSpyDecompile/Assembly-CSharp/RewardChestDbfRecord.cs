using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000253 RID: 595
[Serializable]
public class RewardChestDbfRecord : DbfRecord
{
	// Token: 0x17000408 RID: 1032
	// (get) Token: 0x06001F35 RID: 7989 RVA: 0x0009CD16 File Offset: 0x0009AF16
	[DbfField("NAME")]
	public DbfLocValue Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x17000409 RID: 1033
	// (get) Token: 0x06001F36 RID: 7990 RVA: 0x0009CD1E File Offset: 0x0009AF1E
	[DbfField("DESCRIPTION")]
	public DbfLocValue Description
	{
		get
		{
			return this.m_description;
		}
	}

	// Token: 0x1700040A RID: 1034
	// (get) Token: 0x06001F37 RID: 7991 RVA: 0x0009CD26 File Offset: 0x0009AF26
	[DbfField("SHOW_TO_RETURNING_PLAYER")]
	public bool ShowToReturningPlayer
	{
		get
		{
			return this.m_showToReturningPlayer;
		}
	}

	// Token: 0x1700040B RID: 1035
	// (get) Token: 0x06001F38 RID: 7992 RVA: 0x0009CD2E File Offset: 0x0009AF2E
	[DbfField("CHEST_PREFAB")]
	public string ChestPrefab
	{
		get
		{
			return this.m_chestPrefab;
		}
	}

	// Token: 0x06001F39 RID: 7993 RVA: 0x0009CD36 File Offset: 0x0009AF36
	public void SetName(DbfLocValue v)
	{
		this.m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	// Token: 0x06001F3A RID: 7994 RVA: 0x0009CD50 File Offset: 0x0009AF50
	public void SetDescription(DbfLocValue v)
	{
		this.m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	// Token: 0x06001F3B RID: 7995 RVA: 0x0009CD6A File Offset: 0x0009AF6A
	public void SetShowToReturningPlayer(bool v)
	{
		this.m_showToReturningPlayer = v;
	}

	// Token: 0x06001F3C RID: 7996 RVA: 0x0009CD73 File Offset: 0x0009AF73
	public void SetChestPrefab(string v)
	{
		this.m_chestPrefab = v;
	}

	// Token: 0x06001F3D RID: 7997 RVA: 0x0009CD7C File Offset: 0x0009AF7C
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "NAME")
		{
			return this.m_name;
		}
		if (name == "DESCRIPTION")
		{
			return this.m_description;
		}
		if (name == "SHOW_TO_RETURNING_PLAYER")
		{
			return this.m_showToReturningPlayer;
		}
		if (!(name == "CHEST_PREFAB"))
		{
			return null;
		}
		return this.m_chestPrefab;
	}

	// Token: 0x06001F3E RID: 7998 RVA: 0x0009CDFC File Offset: 0x0009AFFC
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "NAME")
		{
			this.m_name = (DbfLocValue)val;
			return;
		}
		if (name == "DESCRIPTION")
		{
			this.m_description = (DbfLocValue)val;
			return;
		}
		if (name == "SHOW_TO_RETURNING_PLAYER")
		{
			this.m_showToReturningPlayer = (bool)val;
			return;
		}
		if (!(name == "CHEST_PREFAB"))
		{
			return;
		}
		this.m_chestPrefab = (string)val;
	}

	// Token: 0x06001F3F RID: 7999 RVA: 0x0009CE8C File Offset: 0x0009B08C
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "NAME")
		{
			return typeof(DbfLocValue);
		}
		if (name == "DESCRIPTION")
		{
			return typeof(DbfLocValue);
		}
		if (name == "SHOW_TO_RETURNING_PLAYER")
		{
			return typeof(bool);
		}
		if (!(name == "CHEST_PREFAB"))
		{
			return null;
		}
		return typeof(string);
	}

	// Token: 0x06001F40 RID: 8000 RVA: 0x0009CF14 File Offset: 0x0009B114
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadRewardChestDbfRecords loadRecords = new LoadRewardChestDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001F41 RID: 8001 RVA: 0x0009CF2C File Offset: 0x0009B12C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		RewardChestDbfAsset rewardChestDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(RewardChestDbfAsset)) as RewardChestDbfAsset;
		if (rewardChestDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("RewardChestDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < rewardChestDbfAsset.Records.Count; i++)
		{
			rewardChestDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (rewardChestDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001F42 RID: 8002 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001F43 RID: 8003 RVA: 0x0009CFAB File Offset: 0x0009B1AB
	public override void StripUnusedLocales()
	{
		this.m_name.StripUnusedLocales();
		this.m_description.StripUnusedLocales();
	}

	// Token: 0x040011DB RID: 4571
	[SerializeField]
	private DbfLocValue m_name;

	// Token: 0x040011DC RID: 4572
	[SerializeField]
	private DbfLocValue m_description;

	// Token: 0x040011DD RID: 4573
	[SerializeField]
	private bool m_showToReturningPlayer;

	// Token: 0x040011DE RID: 4574
	[SerializeField]
	private string m_chestPrefab;
}
