using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000153 RID: 339
[Serializable]
public class AchievementItemDbfRecord : DbfRecord
{
	// Token: 0x1700014C RID: 332
	// (get) Token: 0x060015FC RID: 5628 RVA: 0x0007C572 File Offset: 0x0007A772
	[DbfField("ACHIEVEMENT_SECTION_ID")]
	public int AchievementSectionId
	{
		get
		{
			return this.m_achievementSectionId;
		}
	}

	// Token: 0x1700014D RID: 333
	// (get) Token: 0x060015FD RID: 5629 RVA: 0x0007C57A File Offset: 0x0007A77A
	[DbfField("ACHIEVEMENT")]
	public int Achievement
	{
		get
		{
			return this.m_achievementId;
		}
	}

	// Token: 0x1700014E RID: 334
	// (get) Token: 0x060015FE RID: 5630 RVA: 0x0007C582 File Offset: 0x0007A782
	public AchievementDbfRecord AchievementRecord
	{
		get
		{
			return GameDbf.Achievement.GetRecord(this.m_achievementId);
		}
	}

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x060015FF RID: 5631 RVA: 0x0007C594 File Offset: 0x0007A794
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x06001600 RID: 5632 RVA: 0x0007C59C File Offset: 0x0007A79C
	public void SetAchievementSectionId(int v)
	{
		this.m_achievementSectionId = v;
	}

	// Token: 0x06001601 RID: 5633 RVA: 0x0007C5A5 File Offset: 0x0007A7A5
	public void SetAchievement(int v)
	{
		this.m_achievementId = v;
	}

	// Token: 0x06001602 RID: 5634 RVA: 0x0007C5AE File Offset: 0x0007A7AE
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x06001603 RID: 5635 RVA: 0x0007C5B8 File Offset: 0x0007A7B8
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "ACHIEVEMENT_SECTION_ID")
		{
			return this.m_achievementSectionId;
		}
		if (name == "ACHIEVEMENT")
		{
			return this.m_achievementId;
		}
		if (!(name == "SORT_ORDER"))
		{
			return null;
		}
		return this.m_sortOrder;
	}

	// Token: 0x06001604 RID: 5636 RVA: 0x0007C62C File Offset: 0x0007A82C
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "ACHIEVEMENT_SECTION_ID")
		{
			this.m_achievementSectionId = (int)val;
			return;
		}
		if (name == "ACHIEVEMENT")
		{
			this.m_achievementId = (int)val;
			return;
		}
		if (!(name == "SORT_ORDER"))
		{
			return;
		}
		this.m_sortOrder = (int)val;
	}

	// Token: 0x06001605 RID: 5637 RVA: 0x0007C6A4 File Offset: 0x0007A8A4
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "ACHIEVEMENT_SECTION_ID")
		{
			return typeof(int);
		}
		if (name == "ACHIEVEMENT")
		{
			return typeof(int);
		}
		if (!(name == "SORT_ORDER"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x06001606 RID: 5638 RVA: 0x0007C714 File Offset: 0x0007A914
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAchievementItemDbfRecords loadRecords = new LoadAchievementItemDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001607 RID: 5639 RVA: 0x0007C72C File Offset: 0x0007A92C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AchievementItemDbfAsset achievementItemDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AchievementItemDbfAsset)) as AchievementItemDbfAsset;
		if (achievementItemDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AchievementItemDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < achievementItemDbfAsset.Records.Count; i++)
		{
			achievementItemDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (achievementItemDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001608 RID: 5640 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001609 RID: 5641 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000E74 RID: 3700
	[SerializeField]
	private int m_achievementSectionId;

	// Token: 0x04000E75 RID: 3701
	[SerializeField]
	private int m_achievementId;

	// Token: 0x04000E76 RID: 3702
	[SerializeField]
	private int m_sortOrder;
}
