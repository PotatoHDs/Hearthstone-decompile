using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200014D RID: 333
[Serializable]
public class AchievementCategoryDbfRecord : DbfRecord
{
	// Token: 0x17000137 RID: 311
	// (get) Token: 0x060015BE RID: 5566 RVA: 0x0007B7CA File Offset: 0x000799CA
	[DbfField("NAME")]
	public DbfLocValue Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x060015BF RID: 5567 RVA: 0x0007B7D2 File Offset: 0x000799D2
	[DbfField("ICON")]
	public string Icon
	{
		get
		{
			return this.m_icon;
		}
	}

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x060015C0 RID: 5568 RVA: 0x0007B7DA File Offset: 0x000799DA
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x1700013A RID: 314
	// (get) Token: 0x060015C1 RID: 5569 RVA: 0x0007B7E2 File Offset: 0x000799E2
	public List<AchievementSubcategoryDbfRecord> Subcategories
	{
		get
		{
			return GameDbf.AchievementSubcategory.GetRecords((AchievementSubcategoryDbfRecord r) => r.AchievementCategoryId == base.ID, -1);
		}
	}

	// Token: 0x060015C2 RID: 5570 RVA: 0x0007B7FB File Offset: 0x000799FB
	public void SetName(DbfLocValue v)
	{
		this.m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	// Token: 0x060015C3 RID: 5571 RVA: 0x0007B815 File Offset: 0x00079A15
	public void SetIcon(string v)
	{
		this.m_icon = v;
	}

	// Token: 0x060015C4 RID: 5572 RVA: 0x0007B81E File Offset: 0x00079A1E
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x060015C5 RID: 5573 RVA: 0x0007B828 File Offset: 0x00079A28
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

	// Token: 0x060015C6 RID: 5574 RVA: 0x0007B894 File Offset: 0x00079A94
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

	// Token: 0x060015C7 RID: 5575 RVA: 0x0007B90C File Offset: 0x00079B0C
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

	// Token: 0x060015C8 RID: 5576 RVA: 0x0007B97C File Offset: 0x00079B7C
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAchievementCategoryDbfRecords loadRecords = new LoadAchievementCategoryDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060015C9 RID: 5577 RVA: 0x0007B994 File Offset: 0x00079B94
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AchievementCategoryDbfAsset achievementCategoryDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AchievementCategoryDbfAsset)) as AchievementCategoryDbfAsset;
		if (achievementCategoryDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AchievementCategoryDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < achievementCategoryDbfAsset.Records.Count; i++)
		{
			achievementCategoryDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (achievementCategoryDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060015CA RID: 5578 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x060015CB RID: 5579 RVA: 0x0007BA13 File Offset: 0x00079C13
	public override void StripUnusedLocales()
	{
		this.m_name.StripUnusedLocales();
	}

	// Token: 0x04000E60 RID: 3680
	[SerializeField]
	private DbfLocValue m_name;

	// Token: 0x04000E61 RID: 3681
	[SerializeField]
	private string m_icon;

	// Token: 0x04000E62 RID: 3682
	[SerializeField]
	private int m_sortOrder;
}
