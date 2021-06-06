using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000258 RID: 600
public class LoadRewardLevelDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001F70 RID: 8048 RVA: 0x0009DA40 File Offset: 0x0009BC40
	public List<RewardLevelDbfRecord> GetRecords()
	{
		RewardLevelDbfAsset rewardLevelDbfAsset = this.assetBundleRequest.asset as RewardLevelDbfAsset;
		if (rewardLevelDbfAsset != null)
		{
			for (int i = 0; i < rewardLevelDbfAsset.Records.Count; i++)
			{
				rewardLevelDbfAsset.Records[i].StripUnusedLocales();
			}
			return rewardLevelDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001F71 RID: 8049 RVA: 0x0009DA96 File Offset: 0x0009BC96
	public LoadRewardLevelDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(RewardLevelDbfAsset));
	}

	// Token: 0x06001F72 RID: 8050 RVA: 0x0009DAB9 File Offset: 0x0009BCB9
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040011EE RID: 4590
	private AssetBundleRequest assetBundleRequest;
}
