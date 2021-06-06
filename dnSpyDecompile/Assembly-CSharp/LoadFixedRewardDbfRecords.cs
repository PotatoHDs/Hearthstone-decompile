using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001E2 RID: 482
public class LoadFixedRewardDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001B40 RID: 6976 RVA: 0x0008E3EC File Offset: 0x0008C5EC
	public List<FixedRewardDbfRecord> GetRecords()
	{
		FixedRewardDbfAsset fixedRewardDbfAsset = this.assetBundleRequest.asset as FixedRewardDbfAsset;
		if (fixedRewardDbfAsset != null)
		{
			for (int i = 0; i < fixedRewardDbfAsset.Records.Count; i++)
			{
				fixedRewardDbfAsset.Records[i].StripUnusedLocales();
			}
			return fixedRewardDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001B41 RID: 6977 RVA: 0x0008E442 File Offset: 0x0008C642
	public LoadFixedRewardDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(FixedRewardDbfAsset));
	}

	// Token: 0x06001B42 RID: 6978 RVA: 0x0008E465 File Offset: 0x0008C665
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x0400102C RID: 4140
	private AssetBundleRequest assetBundleRequest;
}
