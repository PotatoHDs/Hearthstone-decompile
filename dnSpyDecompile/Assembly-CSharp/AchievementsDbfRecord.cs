using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000159 RID: 345
[Serializable]
public class AchievementsDbfRecord : DbfRecord
{
	// Token: 0x17000154 RID: 340
	// (get) Token: 0x06001622 RID: 5666 RVA: 0x0007CB1A File Offset: 0x0007AD1A
	[DbfField("ACHIEVEMENT_SECTION_ID")]
	public int AchievementSectionId
	{
		get
		{
			return this.m_achievementSectionId;
		}
	}

	// Token: 0x17000155 RID: 341
	// (get) Token: 0x06001623 RID: 5667 RVA: 0x0007CB22 File Offset: 0x0007AD22
	[DbfField("ACHIEVEMENT")]
	public int Achievement
	{
		get
		{
			return this.m_achievementId;
		}
	}

	// Token: 0x17000156 RID: 342
	// (get) Token: 0x06001624 RID: 5668 RVA: 0x0007CB2A File Offset: 0x0007AD2A
	public AchievementDbfRecord AchievementRecord
	{
		get
		{
			return GameDbf.Achievement.GetRecord(this.m_achievementId);
		}
	}

	// Token: 0x17000157 RID: 343
	// (get) Token: 0x06001625 RID: 5669 RVA: 0x0007CB3C File Offset: 0x0007AD3C
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x06001626 RID: 5670 RVA: 0x0007CB44 File Offset: 0x0007AD44
	public void SetAchievementSectionId(int v)
	{
		this.m_achievementSectionId = v;
	}

	// Token: 0x06001627 RID: 5671 RVA: 0x0007CB4D File Offset: 0x0007AD4D
	public void SetAchievement(int v)
	{
		this.m_achievementId = v;
	}

	// Token: 0x06001628 RID: 5672 RVA: 0x0007CB56 File Offset: 0x0007AD56
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x06001629 RID: 5673 RVA: 0x0007CB60 File Offset: 0x0007AD60
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

	// Token: 0x0600162A RID: 5674 RVA: 0x0007CBD4 File Offset: 0x0007ADD4
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

	// Token: 0x0600162B RID: 5675 RVA: 0x0007CC4C File Offset: 0x0007AE4C
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

	// Token: 0x0600162C RID: 5676 RVA: 0x0007CCBC File Offset: 0x0007AEBC
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAchievementsDbfRecords loadRecords = new LoadAchievementsDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x0600162D RID: 5677 RVA: 0x0007CCD4 File Offset: 0x0007AED4
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AchievementsDbfAsset achievementsDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AchievementsDbfAsset)) as AchievementsDbfAsset;
		if (achievementsDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AchievementsDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < achievementsDbfAsset.Records.Count; i++)
		{
			achievementsDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (achievementsDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x0600162E RID: 5678 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x0600162F RID: 5679 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000E7E RID: 3710
	[SerializeField]
	private int m_achievementSectionId;

	// Token: 0x04000E7F RID: 3711
	[SerializeField]
	private int m_achievementId;

	// Token: 0x04000E80 RID: 3712
	[SerializeField]
	private int m_sortOrder;
}
