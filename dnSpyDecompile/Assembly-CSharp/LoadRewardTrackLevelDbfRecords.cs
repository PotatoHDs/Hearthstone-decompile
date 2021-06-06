using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000261 RID: 609
public class LoadRewardTrackLevelDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001FB0 RID: 8112 RVA: 0x0009E3F0 File Offset: 0x0009C5F0
	public List<RewardTrackLevelDbfRecord> GetRecords()
	{
		RewardTrackLevelDbfAsset rewardTrackLevelDbfAsset = this.assetBundleRequest.asset as RewardTrackLevelDbfAsset;
		if (rewardTrackLevelDbfAsset != null)
		{
			for (int i = 0; i < rewardTrackLevelDbfAsset.Records.Count; i++)
			{
				rewardTrackLevelDbfAsset.Records[i].StripUnusedLocales();
			}
			return rewardTrackLevelDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001FB1 RID: 8113 RVA: 0x0009E446 File Offset: 0x0009C646
	public LoadRewardTrackLevelDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(RewardTrackLevelDbfAsset));
	}

	// Token: 0x06001FB2 RID: 8114 RVA: 0x0009E469 File Offset: 0x0009C669
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040011FE RID: 4606
	private AssetBundleRequest assetBundleRequest;
}
