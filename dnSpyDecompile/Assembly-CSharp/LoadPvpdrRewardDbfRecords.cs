using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200022B RID: 555
public class LoadPvpdrRewardDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001DD8 RID: 7640 RVA: 0x000985D4 File Offset: 0x000967D4
	public List<PvpdrRewardDbfRecord> GetRecords()
	{
		PvpdrRewardDbfAsset pvpdrRewardDbfAsset = this.assetBundleRequest.asset as PvpdrRewardDbfAsset;
		if (pvpdrRewardDbfAsset != null)
		{
			for (int i = 0; i < pvpdrRewardDbfAsset.Records.Count; i++)
			{
				pvpdrRewardDbfAsset.Records[i].StripUnusedLocales();
			}
			return pvpdrRewardDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001DD9 RID: 7641 RVA: 0x0009862A File Offset: 0x0009682A
	public LoadPvpdrRewardDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(PvpdrRewardDbfAsset));
	}

	// Token: 0x06001DDA RID: 7642 RVA: 0x0009864D File Offset: 0x0009684D
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x0400116B RID: 4459
	private AssetBundleRequest assetBundleRequest;
}
