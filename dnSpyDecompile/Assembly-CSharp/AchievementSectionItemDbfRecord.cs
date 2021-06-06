using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200015F RID: 351
[Serializable]
public class AchievementSectionItemDbfRecord : DbfRecord
{
	// Token: 0x17000159 RID: 345
	// (get) Token: 0x06001643 RID: 5699 RVA: 0x0007CFEA File Offset: 0x0007B1EA
	[DbfField("ACHIEVEMENT_SUBCATEGORY_ID")]
	public int AchievementSubcategoryId
	{
		get
		{
			return this.m_achievementSubcategoryId;
		}
	}

	// Token: 0x1700015A RID: 346
	// (get) Token: 0x06001644 RID: 5700 RVA: 0x0007CFF2 File Offset: 0x0007B1F2
	[DbfField("ACHIEVEMENT_SECTION")]
	public int AchievementSection
	{
		get
		{
			return this.m_achievementSectionId;
		}
	}

	// Token: 0x1700015B RID: 347
	// (get) Token: 0x06001645 RID: 5701 RVA: 0x0007CFFA File Offset: 0x0007B1FA
	public AchievementSectionDbfRecord AchievementSectionRecord
	{
		get
		{
			return GameDbf.AchievementSection.GetRecord(this.m_achievementSectionId);
		}
	}

	// Token: 0x1700015C RID: 348
	// (get) Token: 0x06001646 RID: 5702 RVA: 0x0007D00C File Offset: 0x0007B20C
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x06001647 RID: 5703 RVA: 0x0007D014 File Offset: 0x0007B214
	public void SetAchievementSubcategoryId(int v)
	{
		this.m_achievementSubcategoryId = v;
	}

	// Token: 0x06001648 RID: 5704 RVA: 0x0007D01D File Offset: 0x0007B21D
	public void SetAchievementSection(int v)
	{
		this.m_achievementSectionId = v;
	}

	// Token: 0x06001649 RID: 5705 RVA: 0x0007D026 File Offset: 0x0007B226
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x0600164A RID: 5706 RVA: 0x0007D030 File Offset: 0x0007B230
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "ACHIEVEMENT_SUBCATEGORY_ID")
		{
			return this.m_achievementSubcategoryId;
		}
		if (name == "ACHIEVEMENT_SECTION")
		{
			return this.m_achievementSectionId;
		}
		if (!(name == "SORT_ORDER"))
		{
			return null;
		}
		return this.m_sortOrder;
	}

	// Token: 0x0600164B RID: 5707 RVA: 0x0007D0A4 File Offset: 0x0007B2A4
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "ACHIEVEMENT_SUBCATEGORY_ID")
		{
			this.m_achievementSubcategoryId = (int)val;
			return;
		}
		if (name == "ACHIEVEMENT_SECTION")
		{
			this.m_achievementSectionId = (int)val;
			return;
		}
		if (!(name == "SORT_ORDER"))
		{
			return;
		}
		this.m_sortOrder = (int)val;
	}

	// Token: 0x0600164C RID: 5708 RVA: 0x0007D11C File Offset: 0x0007B31C
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "ACHIEVEMENT_SUBCATEGORY_ID")
		{
			return typeof(int);
		}
		if (name == "ACHIEVEMENT_SECTION")
		{
			return typeof(int);
		}
		if (!(name == "SORT_ORDER"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x0600164D RID: 5709 RVA: 0x0007D18C File Offset: 0x0007B38C
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAchievementSectionItemDbfRecords loadRecords = new LoadAchievementSectionItemDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x0600164E RID: 5710 RVA: 0x0007D1A4 File Offset: 0x0007B3A4
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AchievementSectionItemDbfAsset achievementSectionItemDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AchievementSectionItemDbfAsset)) as AchievementSectionItemDbfAsset;
		if (achievementSectionItemDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AchievementSectionItemDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < achievementSectionItemDbfAsset.Records.Count; i++)
		{
			achievementSectionItemDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (achievementSectionItemDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x0600164F RID: 5711 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001650 RID: 5712 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000E86 RID: 3718
	[SerializeField]
	private int m_achievementSubcategoryId;

	// Token: 0x04000E87 RID: 3719
	[SerializeField]
	private int m_achievementSectionId;

	// Token: 0x04000E88 RID: 3720
	[SerializeField]
	private int m_sortOrder;
}
