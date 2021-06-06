using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200025E RID: 606
public class LoadRewardTrackDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001F97 RID: 8087 RVA: 0x0009E064 File Offset: 0x0009C264
	public List<RewardTrackDbfRecord> GetRecords()
	{
		RewardTrackDbfAsset rewardTrackDbfAsset = this.assetBundleRequest.asset as RewardTrackDbfAsset;
		if (rewardTrackDbfAsset != null)
		{
			for (int i = 0; i < rewardTrackDbfAsset.Records.Count; i++)
			{
				rewardTrackDbfAsset.Records[i].StripUnusedLocales();
			}
			return rewardTrackDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001F98 RID: 8088 RVA: 0x0009E0BA File Offset: 0x0009C2BA
	public LoadRewardTrackDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(RewardTrackDbfAsset));
	}

	// Token: 0x06001F99 RID: 8089 RVA: 0x0009E0DD File Offset: 0x0009C2DD
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040011F8 RID: 4600
	private AssetBundleRequest assetBundleRequest;
}
