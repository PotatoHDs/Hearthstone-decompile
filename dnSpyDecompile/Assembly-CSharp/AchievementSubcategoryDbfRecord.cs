using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000162 RID: 354
[Serializable]
public class AchievementSubcategoryDbfRecord : DbfRecord
{
	// Token: 0x1700015D RID: 349
	// (get) Token: 0x06001656 RID: 5718 RVA: 0x0007D2BE File Offset: 0x0007B4BE
	[DbfField("ACHIEVEMENT_CATEGORY_ID")]
	public int AchievementCategoryId
	{
		get
		{
			return this.m_achievementCategoryId;
		}
	}

	// Token: 0x1700015E RID: 350
	// (get) Token: 0x06001657 RID: 5719 RVA: 0x0007D2C6 File Offset: 0x0007B4C6
	[DbfField("NAME")]
	public DbfLocValue Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x1700015F RID: 351
	// (get) Token: 0x06001658 RID: 5720 RVA: 0x0007D2CE File Offset: 0x0007B4CE
	[DbfField("ICON")]
	public string Icon
	{
		get
		{
			return this.m_icon;
		}
	}

	// Token: 0x17000160 RID: 352
	// (get) Token: 0x06001659 RID: 5721 RVA: 0x0007D2D6 File Offset: 0x0007B4D6
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x17000161 RID: 353
	// (get) Token: 0x0600165A RID: 5722 RVA: 0x0007D2DE File Offset: 0x0007B4DE
	public List<AchievementSectionItemDbfRecord> Sections
	{
		get
		{
			return GameDbf.AchievementSectionItem.GetRecords((AchievementSectionItemDbfRecord r) => r.AchievementSubcategoryId == base.ID, -1);
		}
	}

	// Token: 0x0600165B RID: 5723 RVA: 0x0007D2F7 File Offset: 0x0007B4F7
	public void SetAchievementCategoryId(int v)
	{
		this.m_achievementCategoryId = v;
	}

	// Token: 0x0600165C RID: 5724 RVA: 0x0007D300 File Offset: 0x0007B500
	public void SetName(DbfLocValue v)
	{
		this.m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	// Token: 0x0600165D RID: 5725 RVA: 0x0007D31A File Offset: 0x0007B51A
	public void SetIcon(string v)
	{
		this.m_icon = v;
	}

	// Token: 0x0600165E RID: 5726 RVA: 0x0007D323 File Offset: 0x0007B523
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x0600165F RID: 5727 RVA: 0x0007D32C File Offset: 0x0007B52C
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "ACHIEVEMENT_CATEGORY_ID")
		{
			return this.m_achievementCategoryId;
		}
		if (name == "NAME")
		{
			return this.m_name;
		}
		if (name == "ICON")
		{
			return this.m_icon;
		}
		if (!(name == "SORT_ORDER"))
		{
			return null;
		}
		return this.m_sortOrder;
	}

	// Token: 0x06001660 RID: 5728 RVA: 0x0007D3B0 File Offset: 0x0007B5B0
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "ACHIEVEMENT_CATEGORY_ID")
		{
			this.m_achievementCategoryId = (int)val;
			return;
		}
		if (name == "NAME")
		{
			this.m_name = (DbfLocValue)val;
			return;
		}
		if (name == "ICON")
		{
			this.m_icon = (string)val;
			return;
		}
		if (!(name == "SORT_ORDER"))
		{
			return;
		}
		this.m_sortOrder = (int)val;
	}

	// Token: 0x06001661 RID: 5729 RVA: 0x0007D440 File Offset: 0x0007B640
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "ACHIEVEMENT_CATEGORY_ID")
		{
			return typeof(int);
		}
		if (name == "NAME")
		{
			return typeof(DbfLocValue);
		}
		if (name == "ICON")
		{
			return typeof(string);
		}
		if (!(name == "SORT_ORDER"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x06001662 RID: 5730 RVA: 0x0007D4C8 File Offset: 0x0007B6C8
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAchievementSubcategoryDbfRecords loadRecords = new LoadAchievementSubcategoryDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001663 RID: 5731 RVA: 0x0007D4E0 File Offset: 0x0007B6E0
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AchievementSubcategoryDbfAsset achievementSubcategoryDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AchievementSubcategoryDbfAsset)) as AchievementSubcategoryDbfAsset;
		if (achievementSubcategoryDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AchievementSubcategoryDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < achievementSubcategoryDbfAsset.Records.Count; i++)
		{
			achievementSubcategoryDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (achievementSubcategoryDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001664 RID: 5732 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001665 RID: 5733 RVA: 0x0007D55F File Offset: 0x0007B75F
	public override void StripUnusedLocales()
	{
		this.m_name.StripUnusedLocales();
	}

	// Token: 0x04000E8B RID: 3723
	[SerializeField]
	private int m_achievementCategoryId;

	// Token: 0x04000E8C RID: 3724
	[SerializeField]
	private DbfLocValue m_name;

	// Token: 0x04000E8D RID: 3725
	[SerializeField]
	private string m_icon;

	// Token: 0x04000E8E RID: 3726
	[SerializeField]
	private int m_sortOrder;
}
