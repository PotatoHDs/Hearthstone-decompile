using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000150 RID: 336
[Serializable]
public class AchievementDbfRecord : DbfRecord
{
	// Token: 0x1700013B RID: 315
	// (get) Token: 0x060015D2 RID: 5586 RVA: 0x0007BACA File Offset: 0x00079CCA
	[DbfField("ACHIEVEMENT_SECTION")]
	public int AchievementSection
	{
		get
		{
			return this.m_achievementSectionId;
		}
	}

	// Token: 0x1700013C RID: 316
	// (get) Token: 0x060015D3 RID: 5587 RVA: 0x0007BAD2 File Offset: 0x00079CD2
	public AchievementSectionDbfRecord AchievementSectionRecord
	{
		get
		{
			return GameDbf.AchievementSection.GetRecord(this.m_achievementSectionId);
		}
	}

	// Token: 0x1700013D RID: 317
	// (get) Token: 0x060015D4 RID: 5588 RVA: 0x0007BAE4 File Offset: 0x00079CE4
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x1700013E RID: 318
	// (get) Token: 0x060015D5 RID: 5589 RVA: 0x0007BAEC File Offset: 0x00079CEC
	[DbfField("ENABLED")]
	public bool Enabled
	{
		get
		{
			return this.m_enabled;
		}
	}

	// Token: 0x1700013F RID: 319
	// (get) Token: 0x060015D6 RID: 5590 RVA: 0x0007BAF4 File Offset: 0x00079CF4
	[DbfField("NAME")]
	public DbfLocValue Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x17000140 RID: 320
	// (get) Token: 0x060015D7 RID: 5591 RVA: 0x0007BAFC File Offset: 0x00079CFC
	[DbfField("DESCRIPTION")]
	public DbfLocValue Description
	{
		get
		{
			return this.m_description;
		}
	}

	// Token: 0x17000141 RID: 321
	// (get) Token: 0x060015D8 RID: 5592 RVA: 0x0007BB04 File Offset: 0x00079D04
	[DbfField("ACHIEVEMENT_VISIBILITY")]
	public Assets.Achievement.AchievementVisibility AchievementVisibility
	{
		get
		{
			return this.m_achievementVisibility;
		}
	}

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x060015D9 RID: 5593 RVA: 0x0007BB0C File Offset: 0x00079D0C
	[DbfField("QUOTA")]
	public int Quota
	{
		get
		{
			return this.m_quota;
		}
	}

	// Token: 0x17000143 RID: 323
	// (get) Token: 0x060015DA RID: 5594 RVA: 0x0007BB14 File Offset: 0x00079D14
	[DbfField("ALLOW_EXCEED_QUOTA")]
	public bool AllowExceedQuota
	{
		get
		{
			return this.m_allowExceedQuota;
		}
	}

	// Token: 0x17000144 RID: 324
	// (get) Token: 0x060015DB RID: 5595 RVA: 0x0007BB1C File Offset: 0x00079D1C
	[DbfField("TRIGGER")]
	public int Trigger
	{
		get
		{
			return this.m_triggerId;
		}
	}

	// Token: 0x17000145 RID: 325
	// (get) Token: 0x060015DC RID: 5596 RVA: 0x0007BB24 File Offset: 0x00079D24
	public TriggerDbfRecord TriggerRecord
	{
		get
		{
			return GameDbf.Trigger.GetRecord(this.m_triggerId);
		}
	}

	// Token: 0x17000146 RID: 326
	// (get) Token: 0x060015DD RID: 5597 RVA: 0x0007BB36 File Offset: 0x00079D36
	[DbfField("POINTS")]
	public int Points
	{
		get
		{
			return this.m_points;
		}
	}

	// Token: 0x17000147 RID: 327
	// (get) Token: 0x060015DE RID: 5598 RVA: 0x0007BB3E File Offset: 0x00079D3E
	[DbfField("REWARD_TRACK_XP")]
	public int RewardTrackXp
	{
		get
		{
			return this.m_rewardTrackXp;
		}
	}

	// Token: 0x17000148 RID: 328
	// (get) Token: 0x060015DF RID: 5599 RVA: 0x0007BB46 File Offset: 0x00079D46
	[DbfField("REWARD_LIST")]
	public int RewardList
	{
		get
		{
			return this.m_rewardListId;
		}
	}

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x060015E0 RID: 5600 RVA: 0x0007BB4E File Offset: 0x00079D4E
	public RewardListDbfRecord RewardListRecord
	{
		get
		{
			return GameDbf.RewardList.GetRecord(this.m_rewardListId);
		}
	}

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x060015E1 RID: 5601 RVA: 0x0007BB60 File Offset: 0x00079D60
	[DbfField("NEXT_TIER")]
	public int NextTier
	{
		get
		{
			return this.m_nextTierId;
		}
	}

	// Token: 0x1700014B RID: 331
	// (get) Token: 0x060015E2 RID: 5602 RVA: 0x0007BB68 File Offset: 0x00079D68
	public AchievementDbfRecord NextTierRecord
	{
		get
		{
			return GameDbf.Achievement.GetRecord(this.m_nextTierId);
		}
	}

	// Token: 0x060015E3 RID: 5603 RVA: 0x0007BB7A File Offset: 0x00079D7A
	public void SetAchievementSection(int v)
	{
		this.m_achievementSectionId = v;
	}

	// Token: 0x060015E4 RID: 5604 RVA: 0x0007BB83 File Offset: 0x00079D83
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x060015E5 RID: 5605 RVA: 0x0007BB8C File Offset: 0x00079D8C
	public void SetEnabled(bool v)
	{
		this.m_enabled = v;
	}

	// Token: 0x060015E6 RID: 5606 RVA: 0x0007BB95 File Offset: 0x00079D95
	public void SetName(DbfLocValue v)
	{
		this.m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	// Token: 0x060015E7 RID: 5607 RVA: 0x0007BBAF File Offset: 0x00079DAF
	public void SetDescription(DbfLocValue v)
	{
		this.m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	// Token: 0x060015E8 RID: 5608 RVA: 0x0007BBC9 File Offset: 0x00079DC9
	public void SetAchievementVisibility(Assets.Achievement.AchievementVisibility v)
	{
		this.m_achievementVisibility = v;
	}

	// Token: 0x060015E9 RID: 5609 RVA: 0x0007BBD2 File Offset: 0x00079DD2
	public void SetQuota(int v)
	{
		this.m_quota = v;
	}

	// Token: 0x060015EA RID: 5610 RVA: 0x0007BBDB File Offset: 0x00079DDB
	public void SetAllowExceedQuota(bool v)
	{
		this.m_allowExceedQuota = v;
	}

	// Token: 0x060015EB RID: 5611 RVA: 0x0007BBE4 File Offset: 0x00079DE4
	public void SetTrigger(int v)
	{
		this.m_triggerId = v;
	}

	// Token: 0x060015EC RID: 5612 RVA: 0x0007BBED File Offset: 0x00079DED
	public void SetPoints(int v)
	{
		this.m_points = v;
	}

	// Token: 0x060015ED RID: 5613 RVA: 0x0007BBF6 File Offset: 0x00079DF6
	public void SetRewardTrackXp(int v)
	{
		this.m_rewardTrackXp = v;
	}

	// Token: 0x060015EE RID: 5614 RVA: 0x0007BBFF File Offset: 0x00079DFF
	public void SetRewardList(int v)
	{
		this.m_rewardListId = v;
	}

	// Token: 0x060015EF RID: 5615 RVA: 0x0007BC08 File Offset: 0x00079E08
	public void SetNextTier(int v)
	{
		this.m_nextTierId = v;
	}

	// Token: 0x060015F0 RID: 5616 RVA: 0x0007BC14 File Offset: 0x00079E14
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1387956774U)
		{
			if (num <= 416172651U)
			{
				if (num != 91158759U)
				{
					if (num != 257123909U)
					{
						if (num == 416172651U)
						{
							if (name == "QUOTA")
							{
								return this.m_quota;
							}
						}
					}
					else if (name == "ACHIEVEMENT_VISIBILITY")
					{
						return this.m_achievementVisibility;
					}
				}
				else if (name == "REWARD_LIST")
				{
					return this.m_rewardListId;
				}
			}
			else if (num <= 1103584457U)
			{
				if (num != 937620916U)
				{
					if (num == 1103584457U)
					{
						if (name == "DESCRIPTION")
						{
							return this.m_description;
						}
					}
				}
				else if (name == "ALLOW_EXCEED_QUOTA")
				{
					return this.m_allowExceedQuota;
				}
			}
			else if (num != 1223790211U)
			{
				if (num == 1387956774U)
				{
					if (name == "NAME")
					{
						return this.m_name;
					}
				}
			}
			else if (name == "REWARD_TRACK_XP")
			{
				return this.m_rewardTrackXp;
			}
		}
		else if (num <= 1777744857U)
		{
			if (num != 1458105184U)
			{
				if (num != 1752184140U)
				{
					if (num == 1777744857U)
					{
						if (name == "NEXT_TIER")
						{
							return this.m_nextTierId;
						}
					}
				}
				else if (name == "ACHIEVEMENT_SECTION")
				{
					return this.m_achievementSectionId;
				}
			}
			else if (name == "ID")
			{
				return base.ID;
			}
		}
		else if (num <= 2294480894U)
		{
			if (num != 1951464006U)
			{
				if (num == 2294480894U)
				{
					if (name == "ENABLED")
					{
						return this.m_enabled;
					}
				}
			}
			else if (name == "POINTS")
			{
				return this.m_points;
			}
		}
		else if (num != 4214602626U)
		{
			if (num == 4220586723U)
			{
				if (name == "TRIGGER")
				{
					return this.m_triggerId;
				}
			}
		}
		else if (name == "SORT_ORDER")
		{
			return this.m_sortOrder;
		}
		return null;
	}

	// Token: 0x060015F1 RID: 5617 RVA: 0x0007BEC4 File Offset: 0x0007A0C4
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1387956774U)
		{
			if (num <= 416172651U)
			{
				if (num != 91158759U)
				{
					if (num != 257123909U)
					{
						if (num != 416172651U)
						{
							return;
						}
						if (!(name == "QUOTA"))
						{
							return;
						}
						this.m_quota = (int)val;
						return;
					}
					else
					{
						if (!(name == "ACHIEVEMENT_VISIBILITY"))
						{
							return;
						}
						if (val == null)
						{
							this.m_achievementVisibility = Assets.Achievement.AchievementVisibility.VISIBLE;
							return;
						}
						if (val is Assets.Achievement.AchievementVisibility || val is int)
						{
							this.m_achievementVisibility = (Assets.Achievement.AchievementVisibility)val;
							return;
						}
						if (val is string)
						{
							this.m_achievementVisibility = Assets.Achievement.ParseAchievementVisibilityValue((string)val);
							return;
						}
					}
				}
				else
				{
					if (!(name == "REWARD_LIST"))
					{
						return;
					}
					this.m_rewardListId = (int)val;
					return;
				}
			}
			else if (num <= 1103584457U)
			{
				if (num != 937620916U)
				{
					if (num != 1103584457U)
					{
						return;
					}
					if (!(name == "DESCRIPTION"))
					{
						return;
					}
					this.m_description = (DbfLocValue)val;
					return;
				}
				else
				{
					if (!(name == "ALLOW_EXCEED_QUOTA"))
					{
						return;
					}
					this.m_allowExceedQuota = (bool)val;
					return;
				}
			}
			else if (num != 1223790211U)
			{
				if (num != 1387956774U)
				{
					return;
				}
				if (!(name == "NAME"))
				{
					return;
				}
				this.m_name = (DbfLocValue)val;
				return;
			}
			else
			{
				if (!(name == "REWARD_TRACK_XP"))
				{
					return;
				}
				this.m_rewardTrackXp = (int)val;
				return;
			}
		}
		else if (num <= 1777744857U)
		{
			if (num != 1458105184U)
			{
				if (num != 1752184140U)
				{
					if (num != 1777744857U)
					{
						return;
					}
					if (!(name == "NEXT_TIER"))
					{
						return;
					}
					this.m_nextTierId = (int)val;
				}
				else
				{
					if (!(name == "ACHIEVEMENT_SECTION"))
					{
						return;
					}
					this.m_achievementSectionId = (int)val;
					return;
				}
			}
			else
			{
				if (!(name == "ID"))
				{
					return;
				}
				base.SetID((int)val);
				return;
			}
		}
		else if (num <= 2294480894U)
		{
			if (num != 1951464006U)
			{
				if (num != 2294480894U)
				{
					return;
				}
				if (!(name == "ENABLED"))
				{
					return;
				}
				this.m_enabled = (bool)val;
				return;
			}
			else
			{
				if (!(name == "POINTS"))
				{
					return;
				}
				this.m_points = (int)val;
				return;
			}
		}
		else if (num != 4214602626U)
		{
			if (num != 4220586723U)
			{
				return;
			}
			if (!(name == "TRIGGER"))
			{
				return;
			}
			this.m_triggerId = (int)val;
			return;
		}
		else
		{
			if (!(name == "SORT_ORDER"))
			{
				return;
			}
			this.m_sortOrder = (int)val;
			return;
		}
	}

	// Token: 0x060015F2 RID: 5618 RVA: 0x0007C16C File Offset: 0x0007A36C
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1387956774U)
		{
			if (num <= 416172651U)
			{
				if (num != 91158759U)
				{
					if (num != 257123909U)
					{
						if (num == 416172651U)
						{
							if (name == "QUOTA")
							{
								return typeof(int);
							}
						}
					}
					else if (name == "ACHIEVEMENT_VISIBILITY")
					{
						return typeof(Assets.Achievement.AchievementVisibility);
					}
				}
				else if (name == "REWARD_LIST")
				{
					return typeof(int);
				}
			}
			else if (num <= 1103584457U)
			{
				if (num != 937620916U)
				{
					if (num == 1103584457U)
					{
						if (name == "DESCRIPTION")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "ALLOW_EXCEED_QUOTA")
				{
					return typeof(bool);
				}
			}
			else if (num != 1223790211U)
			{
				if (num == 1387956774U)
				{
					if (name == "NAME")
					{
						return typeof(DbfLocValue);
					}
				}
			}
			else if (name == "REWARD_TRACK_XP")
			{
				return typeof(int);
			}
		}
		else if (num <= 1777744857U)
		{
			if (num != 1458105184U)
			{
				if (num != 1752184140U)
				{
					if (num == 1777744857U)
					{
						if (name == "NEXT_TIER")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "ACHIEVEMENT_SECTION")
				{
					return typeof(int);
				}
			}
			else if (name == "ID")
			{
				return typeof(int);
			}
		}
		else if (num <= 2294480894U)
		{
			if (num != 1951464006U)
			{
				if (num == 2294480894U)
				{
					if (name == "ENABLED")
					{
						return typeof(bool);
					}
				}
			}
			else if (name == "POINTS")
			{
				return typeof(int);
			}
		}
		else if (num != 4214602626U)
		{
			if (num == 4220586723U)
			{
				if (name == "TRIGGER")
				{
					return typeof(int);
				}
			}
		}
		else if (name == "SORT_ORDER")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x060015F3 RID: 5619 RVA: 0x0007C415 File Offset: 0x0007A615
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAchievementDbfRecords loadRecords = new LoadAchievementDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060015F4 RID: 5620 RVA: 0x0007C42C File Offset: 0x0007A62C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AchievementDbfAsset achievementDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AchievementDbfAsset)) as AchievementDbfAsset;
		if (achievementDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AchievementDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < achievementDbfAsset.Records.Count; i++)
		{
			achievementDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (achievementDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060015F5 RID: 5621 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x060015F6 RID: 5622 RVA: 0x0007C4AB File Offset: 0x0007A6AB
	public override void StripUnusedLocales()
	{
		this.m_name.StripUnusedLocales();
		this.m_description.StripUnusedLocales();
	}

	// Token: 0x04000E65 RID: 3685
	[SerializeField]
	private int m_achievementSectionId;

	// Token: 0x04000E66 RID: 3686
	[SerializeField]
	private int m_sortOrder;

	// Token: 0x04000E67 RID: 3687
	[SerializeField]
	private bool m_enabled = true;

	// Token: 0x04000E68 RID: 3688
	[SerializeField]
	private DbfLocValue m_name;

	// Token: 0x04000E69 RID: 3689
	[SerializeField]
	private DbfLocValue m_description;

	// Token: 0x04000E6A RID: 3690
	[SerializeField]
	private Assets.Achievement.AchievementVisibility m_achievementVisibility;

	// Token: 0x04000E6B RID: 3691
	[SerializeField]
	private int m_quota;

	// Token: 0x04000E6C RID: 3692
	[SerializeField]
	private bool m_allowExceedQuota;

	// Token: 0x04000E6D RID: 3693
	[SerializeField]
	private int m_triggerId;

	// Token: 0x04000E6E RID: 3694
	[SerializeField]
	private int m_points = 1;

	// Token: 0x04000E6F RID: 3695
	[SerializeField]
	private int m_rewardTrackXp;

	// Token: 0x04000E70 RID: 3696
	[SerializeField]
	private int m_rewardListId;

	// Token: 0x04000E71 RID: 3697
	[SerializeField]
	private int m_nextTierId;
}
