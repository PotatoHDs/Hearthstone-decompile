using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200024C RID: 588
public class LoadRewardBagDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001EFB RID: 7931 RVA: 0x0009C0E0 File Offset: 0x0009A2E0
	public List<RewardBagDbfRecord> GetRecords()
	{
		RewardBagDbfAsset rewardBagDbfAsset = this.assetBundleRequest.asset as RewardBagDbfAsset;
		if (rewardBagDbfAsset != null)
		{
			for (int i = 0; i < rewardBagDbfAsset.Records.Count; i++)
			{
				rewardBagDbfAsset.Records[i].StripUnusedLocales();
			}
			return rewardBagDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001EFC RID: 7932 RVA: 0x0009C136 File Offset: 0x0009A336
	public LoadRewardBagDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(RewardBagDbfAsset));
	}

	// Token: 0x06001EFD RID: 7933 RVA: 0x0009C159 File Offset: 0x0009A359
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040011C7 RID: 4551
	private AssetBundleRequest assetBundleRequest;
}
