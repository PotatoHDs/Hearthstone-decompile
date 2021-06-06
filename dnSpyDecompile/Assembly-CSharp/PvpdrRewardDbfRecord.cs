using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200022C RID: 556
[Serializable]
public class PvpdrRewardDbfRecord : DbfRecord
{
	// Token: 0x170003A4 RID: 932
	// (get) Token: 0x06001DDB RID: 7643 RVA: 0x0009865A File Offset: 0x0009685A
	[DbfField("PVPDR_SEASON_ID")]
	public int PvpdrSeasonId
	{
		get
		{
			return this.m_pvpdrSeasonId;
		}
	}

	// Token: 0x170003A5 RID: 933
	// (get) Token: 0x06001DDC RID: 7644 RVA: 0x00098662 File Offset: 0x00096862
	[DbfField("WINS")]
	public int Wins
	{
		get
		{
			return this.m_wins;
		}
	}

	// Token: 0x170003A6 RID: 934
	// (get) Token: 0x06001DDD RID: 7645 RVA: 0x0009866A File Offset: 0x0009686A
	[DbfField("REWARD_CHEST_ID")]
	public int RewardChestId
	{
		get
		{
			return this.m_rewardChestId;
		}
	}

	// Token: 0x170003A7 RID: 935
	// (get) Token: 0x06001DDE RID: 7646 RVA: 0x00098672 File Offset: 0x00096872
	public RewardChestDbfRecord RewardChestRecord
	{
		get
		{
			return GameDbf.RewardChest.GetRecord(this.m_rewardChestId);
		}
	}

	// Token: 0x06001DDF RID: 7647 RVA: 0x00098684 File Offset: 0x00096884
	public void SetPvpdrSeasonId(int v)
	{
		this.m_pvpdrSeasonId = v;
	}

	// Token: 0x06001DE0 RID: 7648 RVA: 0x0009868D File Offset: 0x0009688D
	public void SetWins(int v)
	{
		this.m_wins = v;
	}

	// Token: 0x06001DE1 RID: 7649 RVA: 0x00098696 File Offset: 0x00096896
	public void SetRewardChestId(int v)
	{
		this.m_rewardChestId = v;
	}

	// Token: 0x06001DE2 RID: 7650 RVA: 0x000986A0 File Offset: 0x000968A0
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "PVPDR_SEASON_ID")
		{
			return this.m_pvpdrSeasonId;
		}
		if (name == "WINS")
		{
			return this.m_wins;
		}
		if (!(name == "REWARD_CHEST_ID"))
		{
			return null;
		}
		return this.m_rewardChestId;
	}

	// Token: 0x06001DE3 RID: 7651 RVA: 0x00098714 File Offset: 0x00096914
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "PVPDR_SEASON_ID")
		{
			this.m_pvpdrSeasonId = (int)val;
			return;
		}
		if (name == "WINS")
		{
			this.m_wins = (int)val;
			return;
		}
		if (!(name == "REWARD_CHEST_ID"))
		{
			return;
		}
		this.m_rewardChestId = (int)val;
	}

	// Token: 0x06001DE4 RID: 7652 RVA: 0x0009878C File Offset: 0x0009698C
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "PVPDR_SEASON_ID")
		{
			return typeof(int);
		}
		if (name == "WINS")
		{
			return typeof(int);
		}
		if (!(name == "REWARD_CHEST_ID"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x06001DE5 RID: 7653 RVA: 0x000987FC File Offset: 0x000969FC
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadPvpdrRewardDbfRecords loadRecords = new LoadPvpdrRewardDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001DE6 RID: 7654 RVA: 0x00098814 File Offset: 0x00096A14
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		PvpdrRewardDbfAsset pvpdrRewardDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(PvpdrRewardDbfAsset)) as PvpdrRewardDbfAsset;
		if (pvpdrRewardDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("PvpdrRewardDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < pvpdrRewardDbfAsset.Records.Count; i++)
		{
			pvpdrRewardDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (pvpdrRewardDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001DE7 RID: 7655 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001DE8 RID: 7656 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x0400116C RID: 4460
	[SerializeField]
	private int m_pvpdrSeasonId;

	// Token: 0x0400116D RID: 4461
	[SerializeField]
	private int m_wins;

	// Token: 0x0400116E RID: 4462
	[SerializeField]
	private int m_rewardChestId;
}
