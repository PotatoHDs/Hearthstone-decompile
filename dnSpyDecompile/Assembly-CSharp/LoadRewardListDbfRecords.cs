using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200025B RID: 603
public class LoadRewardListDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001F85 RID: 8069 RVA: 0x0009DDBC File Offset: 0x0009BFBC
	public List<RewardListDbfRecord> GetRecords()
	{
		RewardListDbfAsset rewardListDbfAsset = this.assetBundleRequest.asset as RewardListDbfAsset;
		if (rewardListDbfAsset != null)
		{
			for (int i = 0; i < rewardListDbfAsset.Records.Count; i++)
			{
				rewardListDbfAsset.Records[i].StripUnusedLocales();
			}
			return rewardListDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001F86 RID: 8070 RVA: 0x0009DE12 File Offset: 0x0009C012
	public LoadRewardListDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(RewardListDbfAsset));
	}

	// Token: 0x06001F87 RID: 8071 RVA: 0x0009DE35 File Offset: 0x0009C035
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040011F4 RID: 4596
	private AssetBundleRequest assetBundleRequest;
}
