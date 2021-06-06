using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000252 RID: 594
public class LoadRewardChestDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001F32 RID: 7986 RVA: 0x0009CC90 File Offset: 0x0009AE90
	public List<RewardChestDbfRecord> GetRecords()
	{
		RewardChestDbfAsset rewardChestDbfAsset = this.assetBundleRequest.asset as RewardChestDbfAsset;
		if (rewardChestDbfAsset != null)
		{
			for (int i = 0; i < rewardChestDbfAsset.Records.Count; i++)
			{
				rewardChestDbfAsset.Records[i].StripUnusedLocales();
			}
			return rewardChestDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001F33 RID: 7987 RVA: 0x0009CCE6 File Offset: 0x0009AEE6
	public LoadRewardChestDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(RewardChestDbfAsset));
	}

	// Token: 0x06001F34 RID: 7988 RVA: 0x0009CD09 File Offset: 0x0009AF09
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040011DA RID: 4570
	private AssetBundleRequest assetBundleRequest;
}
