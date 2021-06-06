using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200024F RID: 591
public class LoadRewardChestContentsDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001F0F RID: 7951 RVA: 0x0009C400 File Offset: 0x0009A600
	public List<RewardChestContentsDbfRecord> GetRecords()
	{
		RewardChestContentsDbfAsset rewardChestContentsDbfAsset = this.assetBundleRequest.asset as RewardChestContentsDbfAsset;
		if (rewardChestContentsDbfAsset != null)
		{
			for (int i = 0; i < rewardChestContentsDbfAsset.Records.Count; i++)
			{
				rewardChestContentsDbfAsset.Records[i].StripUnusedLocales();
			}
			return rewardChestContentsDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001F10 RID: 7952 RVA: 0x0009C456 File Offset: 0x0009A656
	public LoadRewardChestContentsDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(RewardChestContentsDbfAsset));
	}

	// Token: 0x06001F11 RID: 7953 RVA: 0x0009C479 File Offset: 0x0009A679
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040011CD RID: 4557
	private AssetBundleRequest assetBundleRequest;
}
