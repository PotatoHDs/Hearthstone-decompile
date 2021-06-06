using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001E5 RID: 485
public class LoadFixedRewardMapDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001B5D RID: 7005 RVA: 0x0008EA50 File Offset: 0x0008CC50
	public List<FixedRewardMapDbfRecord> GetRecords()
	{
		FixedRewardMapDbfAsset fixedRewardMapDbfAsset = this.assetBundleRequest.asset as FixedRewardMapDbfAsset;
		if (fixedRewardMapDbfAsset != null)
		{
			for (int i = 0; i < fixedRewardMapDbfAsset.Records.Count; i++)
			{
				fixedRewardMapDbfAsset.Records[i].StripUnusedLocales();
			}
			return fixedRewardMapDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001B5E RID: 7006 RVA: 0x0008EAA6 File Offset: 0x0008CCA6
	public LoadFixedRewardMapDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(FixedRewardMapDbfAsset));
	}

	// Token: 0x06001B5F RID: 7007 RVA: 0x0008EAC9 File Offset: 0x0008CCC9
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001035 RID: 4149
	private AssetBundleRequest assetBundleRequest;
}
