using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000259 RID: 601
[Serializable]
public class RewardLevelDbfRecord : DbfRecord
{
	// Token: 0x1700041E RID: 1054
	// (get) Token: 0x06001F73 RID: 8051 RVA: 0x0009DAC6 File Offset: 0x0009BCC6
	[DbfField("REWARD_TRACK_ID")]
	public int RewardTrackId
	{
		get
		{
			return this.m_rewardTrackId;
		}
	}

	// Token: 0x1700041F RID: 1055
	// (get) Token: 0x06001F74 RID: 8052 RVA: 0x0009DACE File Offset: 0x0009BCCE
	[DbfField("LEVEL_REQUIREMENT")]
	public int LevelRequirement
	{
		get
		{
			return this.m_levelRequirement;
		}
	}

	// Token: 0x17000420 RID: 1056
	// (get) Token: 0x06001F75 RID: 8053 RVA: 0x0009DAD6 File Offset: 0x0009BCD6
	[DbfField("SUBSCRIPTION_REQUIREMENT")]
	public RewardLevel.SubscriptionRequirement SubscriptionRequirement
	{
		get
		{
			return this.m_subscriptionRequirement;
		}
	}

	// Token: 0x17000421 RID: 1057
	// (get) Token: 0x06001F76 RID: 8054 RVA: 0x0009DADE File Offset: 0x0009BCDE
	[DbfField("REWARD_LIST")]
	public int RewardList
	{
		get
		{
			return this.m_rewardListId;
		}
	}

	// Token: 0x17000422 RID: 1058
	// (get) Token: 0x06001F77 RID: 8055 RVA: 0x0009DAE6 File Offset: 0x0009BCE6
	public RewardListDbfRecord RewardListRecord
	{
		get
		{
			return GameDbf.RewardList.GetRecord(this.m_rewardListId);
		}
	}

	// Token: 0x06001F78 RID: 8056 RVA: 0x0009DAF8 File Offset: 0x0009BCF8
	public void SetRewardTrackId(int v)
	{
		this.m_rewardTrackId = v;
	}

	// Token: 0x06001F79 RID: 8057 RVA: 0x0009DB01 File Offset: 0x0009BD01
	public void SetLevelRequirement(int v)
	{
		this.m_levelRequirement = v;
	}

	// Token: 0x06001F7A RID: 8058 RVA: 0x0009DB0A File Offset: 0x0009BD0A
	public void SetSubscriptionRequirement(RewardLevel.SubscriptionRequirement v)
	{
		this.m_subscriptionRequirement = v;
	}

	// Token: 0x06001F7B RID: 8059 RVA: 0x0009DB13 File Offset: 0x0009BD13
	public void SetRewardList(int v)
	{
		this.m_rewardListId = v;
	}

	// Token: 0x06001F7C RID: 8060 RVA: 0x0009DB1C File Offset: 0x0009BD1C
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "REWARD_TRACK_ID")
		{
			return this.m_rewardTrackId;
		}
		if (name == "LEVEL_REQUIREMENT")
		{
			return this.m_levelRequirement;
		}
		if (name == "SUBSCRIPTION_REQUIREMENT")
		{
			return this.m_subscriptionRequirement;
		}
		if (!(name == "REWARD_LIST"))
		{
			return null;
		}
		return this.m_rewardListId;
	}

	// Token: 0x06001F7D RID: 8061 RVA: 0x0009DBAC File Offset: 0x0009BDAC
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "REWARD_TRACK_ID")
		{
			this.m_rewardTrackId = (int)val;
			return;
		}
		if (!(name == "LEVEL_REQUIREMENT"))
		{
			if (!(name == "SUBSCRIPTION_REQUIREMENT"))
			{
				if (!(name == "REWARD_LIST"))
				{
					return;
				}
				this.m_rewardListId = (int)val;
			}
			else
			{
				if (val == null)
				{
					this.m_subscriptionRequirement = RewardLevel.SubscriptionRequirement.FREE;
					return;
				}
				if (val is RewardLevel.SubscriptionRequirement || val is int)
				{
					this.m_subscriptionRequirement = (RewardLevel.SubscriptionRequirement)val;
					return;
				}
				if (val is string)
				{
					this.m_subscriptionRequirement = RewardLevel.ParseSubscriptionRequirementValue((string)val);
					return;
				}
			}
			return;
		}
		this.m_levelRequirement = (int)val;
	}

	// Token: 0x06001F7E RID: 8062 RVA: 0x0009DC70 File Offset: 0x0009BE70
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "REWARD_TRACK_ID")
		{
			return typeof(int);
		}
		if (name == "LEVEL_REQUIREMENT")
		{
			return typeof(int);
		}
		if (name == "SUBSCRIPTION_REQUIREMENT")
		{
			return typeof(RewardLevel.SubscriptionRequirement);
		}
		if (!(name == "REWARD_LIST"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x06001F7F RID: 8063 RVA: 0x0009DCF8 File Offset: 0x0009BEF8
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadRewardLevelDbfRecords loadRecords = new LoadRewardLevelDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001F80 RID: 8064 RVA: 0x0009DD10 File Offset: 0x0009BF10
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		RewardLevelDbfAsset rewardLevelDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(RewardLevelDbfAsset)) as RewardLevelDbfAsset;
		if (rewardLevelDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("RewardLevelDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < rewardLevelDbfAsset.Records.Count; i++)
		{
			rewardLevelDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (rewardLevelDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001F81 RID: 8065 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001F82 RID: 8066 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x040011EF RID: 4591
	[SerializeField]
	private int m_rewardTrackId;

	// Token: 0x040011F0 RID: 4592
	[SerializeField]
	private int m_levelRequirement;

	// Token: 0x040011F1 RID: 4593
	[SerializeField]
	private RewardLevel.SubscriptionRequirement m_subscriptionRequirement = RewardLevel.ParseSubscriptionRequirementValue("free");

	// Token: 0x040011F2 RID: 4594
	[SerializeField]
	private int m_rewardListId;
}
