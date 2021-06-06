using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001DF RID: 479
public class LoadFixedRewardActionDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001B12 RID: 6930 RVA: 0x0008D7E8 File Offset: 0x0008B9E8
	public List<FixedRewardActionDbfRecord> GetRecords()
	{
		FixedRewardActionDbfAsset fixedRewardActionDbfAsset = this.assetBundleRequest.asset as FixedRewardActionDbfAsset;
		if (fixedRewardActionDbfAsset != null)
		{
			for (int i = 0; i < fixedRewardActionDbfAsset.Records.Count; i++)
			{
				fixedRewardActionDbfAsset.Records[i].StripUnusedLocales();
			}
			return fixedRewardActionDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001B13 RID: 6931 RVA: 0x0008D83E File Offset: 0x0008BA3E
	public LoadFixedRewardActionDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(FixedRewardActionDbfAsset));
	}

	// Token: 0x06001B14 RID: 6932 RVA: 0x0008D861 File Offset: 0x0008BA61
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x0400101B RID: 4123
	private AssetBundleRequest assetBundleRequest;
}
