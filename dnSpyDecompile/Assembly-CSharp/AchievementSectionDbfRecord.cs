using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200015C RID: 348
[Serializable]
public class AchievementSectionDbfRecord : DbfRecord
{
	// Token: 0x17000158 RID: 344
	// (get) Token: 0x06001635 RID: 5685 RVA: 0x0007CDEE File Offset: 0x0007AFEE
	[DbfField("NAME")]
	public DbfLocValue Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x06001636 RID: 5686 RVA: 0x0007CDF6 File Offset: 0x0007AFF6
	public void SetName(DbfLocValue v)
	{
		this.m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	// Token: 0x06001637 RID: 5687 RVA: 0x0007CE10 File Offset: 0x0007B010
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (!(name == "NAME"))
		{
			return null;
		}
		return this.m_name;
	}

	// Token: 0x06001638 RID: 5688 RVA: 0x0007CE42 File Offset: 0x0007B042
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (!(name == "NAME"))
		{
			return;
		}
		this.m_name = (DbfLocValue)val;
	}

	// Token: 0x06001639 RID: 5689 RVA: 0x0007CE78 File Offset: 0x0007B078
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (!(name == "NAME"))
		{
			return null;
		}
		return typeof(DbfLocValue);
	}

	// Token: 0x0600163A RID: 5690 RVA: 0x0007CEAD File Offset: 0x0007B0AD
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAchievementSectionDbfRecords loadRecords = new LoadAchievementSectionDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x0600163B RID: 5691 RVA: 0x0007CEC4 File Offset: 0x0007B0C4
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AchievementSectionDbfAsset achievementSectionDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AchievementSectionDbfAsset)) as AchievementSectionDbfAsset;
		if (achievementSectionDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AchievementSectionDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < achievementSectionDbfAsset.Records.Count; i++)
		{
			achievementSectionDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (achievementSectionDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x0600163C RID: 5692 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x0600163D RID: 5693 RVA: 0x0007CF43 File Offset: 0x0007B143
	public override void StripUnusedLocales()
	{
		this.m_name.StripUnusedLocales();
	}

	// Token: 0x04000E83 RID: 3715
	[SerializeField]
	private DbfLocValue m_name;
}
