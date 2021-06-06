using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000156 RID: 342
[Serializable]
public class AchievementListDbfRecord : DbfRecord
{
	// Token: 0x17000150 RID: 336
	// (get) Token: 0x0600160F RID: 5647 RVA: 0x0007C846 File Offset: 0x0007AA46
	[DbfField("ACHIEVEMENT_SECTION_ID")]
	public int AchievementSectionId
	{
		get
		{
			return this.m_achievementSectionId;
		}
	}

	// Token: 0x17000151 RID: 337
	// (get) Token: 0x06001610 RID: 5648 RVA: 0x0007C84E File Offset: 0x0007AA4E
	[DbfField("ACHIEVEMENT")]
	public int Achievement
	{
		get
		{
			return this.m_achievementId;
		}
	}

	// Token: 0x17000152 RID: 338
	// (get) Token: 0x06001611 RID: 5649 RVA: 0x0007C856 File Offset: 0x0007AA56
	public AchievementDbfRecord AchievementRecord
	{
		get
		{
			return GameDbf.Achievement.GetRecord(this.m_achievementId);
		}
	}

	// Token: 0x17000153 RID: 339
	// (get) Token: 0x06001612 RID: 5650 RVA: 0x0007C868 File Offset: 0x0007AA68
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x06001613 RID: 5651 RVA: 0x0007C870 File Offset: 0x0007AA70
	public void SetAchievementSectionId(int v)
	{
		this.m_achievementSectionId = v;
	}

	// Token: 0x06001614 RID: 5652 RVA: 0x0007C879 File Offset: 0x0007AA79
	public void SetAchievement(int v)
	{
		this.m_achievementId = v;
	}

	// Token: 0x06001615 RID: 5653 RVA: 0x0007C882 File Offset: 0x0007AA82
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x06001616 RID: 5654 RVA: 0x0007C88C File Offset: 0x0007AA8C
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

	// Token: 0x06001617 RID: 5655 RVA: 0x0007C900 File Offset: 0x0007AB00
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

	// Token: 0x06001618 RID: 5656 RVA: 0x0007C978 File Offset: 0x0007AB78
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

	// Token: 0x06001619 RID: 5657 RVA: 0x0007C9E8 File Offset: 0x0007ABE8
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAchievementListDbfRecords loadRecords = new LoadAchievementListDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x0600161A RID: 5658 RVA: 0x0007CA00 File Offset: 0x0007AC00
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AchievementListDbfAsset achievementListDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AchievementListDbfAsset)) as AchievementListDbfAsset;
		if (achievementListDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AchievementListDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < achievementListDbfAsset.Records.Count; i++)
		{
			achievementListDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (achievementListDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x0600161B RID: 5659 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x0600161C RID: 5660 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000E79 RID: 3705
	[SerializeField]
	private int m_achievementSectionId;

	// Token: 0x04000E7A RID: 3706
	[SerializeField]
	private int m_achievementId;

	// Token: 0x04000E7B RID: 3707
	[SerializeField]
	private int m_sortOrder;
}
