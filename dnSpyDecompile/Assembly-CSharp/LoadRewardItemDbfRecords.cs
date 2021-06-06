using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000255 RID: 597
public class LoadRewardItemDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001F46 RID: 8006 RVA: 0x0009CFD8 File Offset: 0x0009B1D8
	public List<RewardItemDbfRecord> GetRecords()
	{
		RewardItemDbfAsset rewardItemDbfAsset = this.assetBundleRequest.asset as RewardItemDbfAsset;
		if (rewardItemDbfAsset != null)
		{
			for (int i = 0; i < rewardItemDbfAsset.Records.Count; i++)
			{
				rewardItemDbfAsset.Records[i].StripUnusedLocales();
			}
			return rewardItemDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001F47 RID: 8007 RVA: 0x0009D02E File Offset: 0x0009B22E
	public LoadRewardItemDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(RewardItemDbfAsset));
	}

	// Token: 0x06001F48 RID: 8008 RVA: 0x0009D051 File Offset: 0x0009B251
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040011E0 RID: 4576
	private AssetBundleRequest assetBundleRequest;
}
