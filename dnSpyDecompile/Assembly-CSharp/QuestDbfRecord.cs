using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000232 RID: 562
[Serializable]
public class QuestDbfRecord : DbfRecord
{
	// Token: 0x170003B6 RID: 950
	// (get) Token: 0x06001E12 RID: 7698 RVA: 0x000990A6 File Offset: 0x000972A6
	[DbfField("NAME")]
	public DbfLocValue Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x170003B7 RID: 951
	// (get) Token: 0x06001E13 RID: 7699 RVA: 0x000990AE File Offset: 0x000972AE
	[DbfField("DESCRIPTION")]
	public DbfLocValue Description
	{
		get
		{
			return this.m_description;
		}
	}

	// Token: 0x170003B8 RID: 952
	// (get) Token: 0x06001E14 RID: 7700 RVA: 0x000990B6 File Offset: 0x000972B6
	[DbfField("ICON")]
	public string Icon
	{
		get
		{
			return this.m_icon;
		}
	}

	// Token: 0x170003B9 RID: 953
	// (get) Token: 0x06001E15 RID: 7701 RVA: 0x000990BE File Offset: 0x000972BE
	[DbfField("QUOTA")]
	public int Quota
	{
		get
		{
			return this.m_quota;
		}
	}

	// Token: 0x170003BA RID: 954
	// (get) Token: 0x06001E16 RID: 7702 RVA: 0x000990C6 File Offset: 0x000972C6
	[DbfField("NEXT_IN_CHAIN")]
	public int NextInChain
	{
		get
		{
			return this.m_nextInChainId;
		}
	}

	// Token: 0x170003BB RID: 955
	// (get) Token: 0x06001E17 RID: 7703 RVA: 0x000990CE File Offset: 0x000972CE
	public QuestDbfRecord NextInChainRecord
	{
		get
		{
			return GameDbf.Quest.GetRecord(this.m_nextInChainId);
		}
	}

	// Token: 0x170003BC RID: 956
	// (get) Token: 0x06001E18 RID: 7704 RVA: 0x000990E0 File Offset: 0x000972E0
	[DbfField("QUEST_POOL")]
	public int QuestPool
	{
		get
		{
			return this.m_questPoolId;
		}
	}

	// Token: 0x170003BD RID: 957
	// (get) Token: 0x06001E19 RID: 7705 RVA: 0x000990E8 File Offset: 0x000972E8
	public QuestPoolDbfRecord QuestPoolRecord
	{
		get
		{
			return GameDbf.QuestPool.GetRecord(this.m_questPoolId);
		}
	}

	// Token: 0x170003BE RID: 958
	// (get) Token: 0x06001E1A RID: 7706 RVA: 0x000990FA File Offset: 0x000972FA
	[DbfField("POOL_GUARANTEED")]
	public bool PoolGuaranteed
	{
		get
		{
			return this.m_poolGuaranteed;
		}
	}

	// Token: 0x170003BF RID: 959
	// (get) Token: 0x06001E1B RID: 7707 RVA: 0x00099102 File Offset: 0x00097302
	[DbfField("REWARD_TRACK_XP")]
	public int RewardTrackXp
	{
		get
		{
			return this.m_rewardTrackXp;
		}
	}

	// Token: 0x170003C0 RID: 960
	// (get) Token: 0x06001E1C RID: 7708 RVA: 0x0009910A File Offset: 0x0009730A
	[DbfField("REWARD_LIST")]
	public int RewardList
	{
		get
		{
			return this.m_rewardListId;
		}
	}

	// Token: 0x170003C1 RID: 961
	// (get) Token: 0x06001E1D RID: 7709 RVA: 0x00099112 File Offset: 0x00097312
	public RewardListDbfRecord RewardListRecord
	{
		get
		{
			return GameDbf.RewardList.GetRecord(this.m_rewardListId);
		}
	}

	// Token: 0x170003C2 RID: 962
	// (get) Token: 0x06001E1E RID: 7710 RVA: 0x00099124 File Offset: 0x00097324
	[DbfField("PROXY_FOR_LEGACY_ID")]
	public int ProxyForLegacyId
	{
		get
		{
			return this.m_proxyForLegacyId;
		}
	}

	// Token: 0x170003C3 RID: 963
	// (get) Token: 0x06001E1F RID: 7711 RVA: 0x0009912C File Offset: 0x0009732C
	public AchieveDbfRecord ProxyForLegacyRecord
	{
		get
		{
			return GameDbf.Achieve.GetRecord(this.m_proxyForLegacyId);
		}
	}

	// Token: 0x06001E20 RID: 7712 RVA: 0x0009913E File Offset: 0x0009733E
	public void SetName(DbfLocValue v)
	{
		this.m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	// Token: 0x06001E21 RID: 7713 RVA: 0x00099158 File Offset: 0x00097358
	public void SetDescription(DbfLocValue v)
	{
		this.m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	// Token: 0x06001E22 RID: 7714 RVA: 0x00099172 File Offset: 0x00097372
	public void SetIcon(string v)
	{
		this.m_icon = v;
	}

	// Token: 0x06001E23 RID: 7715 RVA: 0x0009917B File Offset: 0x0009737B
	public void SetQuota(int v)
	{
		this.m_quota = v;
	}

	// Token: 0x06001E24 RID: 7716 RVA: 0x00099184 File Offset: 0x00097384
	public void SetNextInChain(int v)
	{
		this.m_nextInChainId = v;
	}

	// Token: 0x06001E25 RID: 7717 RVA: 0x0009918D File Offset: 0x0009738D
	public void SetQuestPool(int v)
	{
		this.m_questPoolId = v;
	}

	// Token: 0x06001E26 RID: 7718 RVA: 0x00099196 File Offset: 0x00097396
	public void SetPoolGuaranteed(bool v)
	{
		this.m_poolGuaranteed = v;
	}

	// Token: 0x06001E27 RID: 7719 RVA: 0x0009919F File Offset: 0x0009739F
	public void SetRewardTrackXp(int v)
	{
		this.m_rewardTrackXp = v;
	}

	// Token: 0x06001E28 RID: 7720 RVA: 0x000991A8 File Offset: 0x000973A8
	public void SetRewardList(int v)
	{
		this.m_rewardListId = v;
	}

	// Token: 0x06001E29 RID: 7721 RVA: 0x000991B1 File Offset: 0x000973B1
	public void SetProxyForLegacyId(int v)
	{
		this.m_proxyForLegacyId = v;
	}

	// Token: 0x06001E2A RID: 7722 RVA: 0x000991BC File Offset: 0x000973BC
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1103584457U)
		{
			if (num <= 91158759U)
			{
				if (num != 74569132U)
				{
					if (num == 91158759U)
					{
						if (name == "REWARD_LIST")
						{
							return this.m_rewardListId;
						}
					}
				}
				else if (name == "QUEST_POOL")
				{
					return this.m_questPoolId;
				}
			}
			else if (num != 231185982U)
			{
				if (num != 416172651U)
				{
					if (num == 1103584457U)
					{
						if (name == "DESCRIPTION")
						{
							return this.m_description;
						}
					}
				}
				else if (name == "QUOTA")
				{
					return this.m_quota;
				}
			}
			else if (name == "POOL_GUARANTEED")
			{
				return this.m_poolGuaranteed;
			}
		}
		else if (num <= 1261696513U)
		{
			if (num != 1138013160U)
			{
				if (num != 1223790211U)
				{
					if (num == 1261696513U)
					{
						if (name == "PROXY_FOR_LEGACY_ID")
						{
							return this.m_proxyForLegacyId;
						}
					}
				}
				else if (name == "REWARD_TRACK_XP")
				{
					return this.m_rewardTrackXp;
				}
			}
			else if (name == "NEXT_IN_CHAIN")
			{
				return this.m_nextInChainId;
			}
		}
		else if (num != 1387956774U)
		{
			if (num != 1458105184U)
			{
				if (num == 3828435440U)
				{
					if (name == "ICON")
					{
						return this.m_icon;
					}
				}
			}
			else if (name == "ID")
			{
				return base.ID;
			}
		}
		else if (name == "NAME")
		{
			return this.m_name;
		}
		return null;
	}

	// Token: 0x06001E2B RID: 7723 RVA: 0x000993B8 File Offset: 0x000975B8
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1103584457U)
		{
			if (num <= 91158759U)
			{
				if (num != 74569132U)
				{
					if (num != 91158759U)
					{
						return;
					}
					if (!(name == "REWARD_LIST"))
					{
						return;
					}
					this.m_rewardListId = (int)val;
					return;
				}
				else
				{
					if (!(name == "QUEST_POOL"))
					{
						return;
					}
					this.m_questPoolId = (int)val;
					return;
				}
			}
			else if (num != 231185982U)
			{
				if (num != 416172651U)
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
					if (!(name == "QUOTA"))
					{
						return;
					}
					this.m_quota = (int)val;
					return;
				}
			}
			else
			{
				if (!(name == "POOL_GUARANTEED"))
				{
					return;
				}
				this.m_poolGuaranteed = (bool)val;
				return;
			}
		}
		else if (num <= 1261696513U)
		{
			if (num != 1138013160U)
			{
				if (num != 1223790211U)
				{
					if (num != 1261696513U)
					{
						return;
					}
					if (!(name == "PROXY_FOR_LEGACY_ID"))
					{
						return;
					}
					this.m_proxyForLegacyId = (int)val;
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
			else
			{
				if (!(name == "NEXT_IN_CHAIN"))
				{
					return;
				}
				this.m_nextInChainId = (int)val;
				return;
			}
		}
		else if (num != 1387956774U)
		{
			if (num != 1458105184U)
			{
				if (num != 3828435440U)
				{
					return;
				}
				if (!(name == "ICON"))
				{
					return;
				}
				this.m_icon = (string)val;
				return;
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
		else
		{
			if (!(name == "NAME"))
			{
				return;
			}
			this.m_name = (DbfLocValue)val;
			return;
		}
	}

	// Token: 0x06001E2C RID: 7724 RVA: 0x000995A0 File Offset: 0x000977A0
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1103584457U)
		{
			if (num <= 91158759U)
			{
				if (num != 74569132U)
				{
					if (num == 91158759U)
					{
						if (name == "REWARD_LIST")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "QUEST_POOL")
				{
					return typeof(int);
				}
			}
			else if (num != 231185982U)
			{
				if (num != 416172651U)
				{
					if (num == 1103584457U)
					{
						if (name == "DESCRIPTION")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "QUOTA")
				{
					return typeof(int);
				}
			}
			else if (name == "POOL_GUARANTEED")
			{
				return typeof(bool);
			}
		}
		else if (num <= 1261696513U)
		{
			if (num != 1138013160U)
			{
				if (num != 1223790211U)
				{
					if (num == 1261696513U)
					{
						if (name == "PROXY_FOR_LEGACY_ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "REWARD_TRACK_XP")
				{
					return typeof(int);
				}
			}
			else if (name == "NEXT_IN_CHAIN")
			{
				return typeof(int);
			}
		}
		else if (num != 1387956774U)
		{
			if (num != 1458105184U)
			{
				if (num == 3828435440U)
				{
					if (name == "ICON")
					{
						return typeof(string);
					}
				}
			}
			else if (name == "ID")
			{
				return typeof(int);
			}
		}
		else if (name == "NAME")
		{
			return typeof(DbfLocValue);
		}
		return null;
	}

	// Token: 0x06001E2D RID: 7725 RVA: 0x000997A2 File Offset: 0x000979A2
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadQuestDbfRecords loadRecords = new LoadQuestDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001E2E RID: 7726 RVA: 0x000997B8 File Offset: 0x000979B8
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		QuestDbfAsset questDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(QuestDbfAsset)) as QuestDbfAsset;
		if (questDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("QuestDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < questDbfAsset.Records.Count; i++)
		{
			questDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (questDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001E2F RID: 7727 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001E30 RID: 7728 RVA: 0x00099837 File Offset: 0x00097A37
	public override void StripUnusedLocales()
	{
		this.m_name.StripUnusedLocales();
		this.m_description.StripUnusedLocales();
	}

	// Token: 0x0400117C RID: 4476
	[SerializeField]
	private DbfLocValue m_name;

	// Token: 0x0400117D RID: 4477
	[SerializeField]
	private DbfLocValue m_description;

	// Token: 0x0400117E RID: 4478
	[SerializeField]
	private string m_icon;

	// Token: 0x0400117F RID: 4479
	[SerializeField]
	private int m_quota;

	// Token: 0x04001180 RID: 4480
	[SerializeField]
	private int m_nextInChainId;

	// Token: 0x04001181 RID: 4481
	[SerializeField]
	private int m_questPoolId;

	// Token: 0x04001182 RID: 4482
	[SerializeField]
	private bool m_poolGuaranteed;

	// Token: 0x04001183 RID: 4483
	[SerializeField]
	private int m_rewardTrackXp;

	// Token: 0x04001184 RID: 4484
	[SerializeField]
	private int m_rewardListId;

	// Token: 0x04001185 RID: 4485
	[SerializeField]
	private int m_proxyForLegacyId;
}
