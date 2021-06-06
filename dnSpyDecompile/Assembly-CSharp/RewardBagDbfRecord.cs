using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200024D RID: 589
[Serializable]
public class RewardBagDbfRecord : DbfRecord
{
	// Token: 0x170003F8 RID: 1016
	// (get) Token: 0x06001EFE RID: 7934 RVA: 0x0009C166 File Offset: 0x0009A366
	[DbfField("BAG_ID")]
	public int BagId
	{
		get
		{
			return this.m_bagId;
		}
	}

	// Token: 0x170003F9 RID: 1017
	// (get) Token: 0x06001EFF RID: 7935 RVA: 0x0009C16E File Offset: 0x0009A36E
	[DbfField("REWARD")]
	public RewardBag.Reward Reward
	{
		get
		{
			return this.m_reward;
		}
	}

	// Token: 0x170003FA RID: 1018
	// (get) Token: 0x06001F00 RID: 7936 RVA: 0x0009C176 File Offset: 0x0009A376
	[DbfField("BASE")]
	public int Base
	{
		get
		{
			return this.m_base;
		}
	}

	// Token: 0x170003FB RID: 1019
	// (get) Token: 0x06001F01 RID: 7937 RVA: 0x0009C17E File Offset: 0x0009A37E
	[DbfField("REWARD_DATA")]
	public int RewardData
	{
		get
		{
			return this.m_rewardData;
		}
	}

	// Token: 0x06001F02 RID: 7938 RVA: 0x0009C186 File Offset: 0x0009A386
	public void SetBagId(int v)
	{
		this.m_bagId = v;
	}

	// Token: 0x06001F03 RID: 7939 RVA: 0x0009C18F File Offset: 0x0009A38F
	public void SetReward(RewardBag.Reward v)
	{
		this.m_reward = v;
	}

	// Token: 0x06001F04 RID: 7940 RVA: 0x0009C198 File Offset: 0x0009A398
	public void SetBase(int v)
	{
		this.m_base = v;
	}

	// Token: 0x06001F05 RID: 7941 RVA: 0x0009C1A1 File Offset: 0x0009A3A1
	public void SetRewardData(int v)
	{
		this.m_rewardData = v;
	}

	// Token: 0x06001F06 RID: 7942 RVA: 0x0009C1AC File Offset: 0x0009A3AC
	public override object GetVar(string name)
	{
		if (name == "BAG_ID")
		{
			return this.m_bagId;
		}
		if (name == "REWARD")
		{
			return this.m_reward;
		}
		if (name == "BASE")
		{
			return this.m_base;
		}
		if (!(name == "REWARD_DATA"))
		{
			return null;
		}
		return this.m_rewardData;
	}

	// Token: 0x06001F07 RID: 7943 RVA: 0x0009C220 File Offset: 0x0009A420
	public override void SetVar(string name, object val)
	{
		if (!(name == "BAG_ID"))
		{
			if (!(name == "REWARD"))
			{
				if (name == "BASE")
				{
					this.m_base = (int)val;
					return;
				}
				if (!(name == "REWARD_DATA"))
				{
					return;
				}
				this.m_rewardData = (int)val;
			}
			else
			{
				if (val == null)
				{
					this.m_reward = RewardBag.Reward.NONE;
					return;
				}
				if (val is RewardBag.Reward || val is int)
				{
					this.m_reward = (RewardBag.Reward)val;
					return;
				}
				if (val is string)
				{
					this.m_reward = RewardBag.ParseRewardValue((string)val);
					return;
				}
			}
			return;
		}
		this.m_bagId = (int)val;
	}

	// Token: 0x06001F08 RID: 7944 RVA: 0x0009C2CC File Offset: 0x0009A4CC
	public override Type GetVarType(string name)
	{
		if (name == "BAG_ID")
		{
			return typeof(int);
		}
		if (name == "REWARD")
		{
			return typeof(RewardBag.Reward);
		}
		if (name == "BASE")
		{
			return typeof(int);
		}
		if (!(name == "REWARD_DATA"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x06001F09 RID: 7945 RVA: 0x0009C33C File Offset: 0x0009A53C
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadRewardBagDbfRecords loadRecords = new LoadRewardBagDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001F0A RID: 7946 RVA: 0x0009C354 File Offset: 0x0009A554
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		RewardBagDbfAsset rewardBagDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(RewardBagDbfAsset)) as RewardBagDbfAsset;
		if (rewardBagDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("RewardBagDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < rewardBagDbfAsset.Records.Count; i++)
		{
			rewardBagDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (rewardBagDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001F0B RID: 7947 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001F0C RID: 7948 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x040011C8 RID: 4552
	[SerializeField]
	private int m_bagId;

	// Token: 0x040011C9 RID: 4553
	[SerializeField]
	private RewardBag.Reward m_reward = RewardBag.ParseRewardValue("unknown");

	// Token: 0x040011CA RID: 4554
	[SerializeField]
	private int m_base;

	// Token: 0x040011CB RID: 4555
	[SerializeField]
	private int m_rewardData;
}
