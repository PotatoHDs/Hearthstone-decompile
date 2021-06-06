using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000164 RID: 356
public class LoadAchieveRegionDataDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001669 RID: 5737 RVA: 0x0007D590 File Offset: 0x0007B790
	public List<AchieveRegionDataDbfRecord> GetRecords()
	{
		AchieveRegionDataDbfAsset achieveRegionDataDbfAsset = this.assetBundleRequest.asset as AchieveRegionDataDbfAsset;
		if (achieveRegionDataDbfAsset != null)
		{
			for (int i = 0; i < achieveRegionDataDbfAsset.Records.Count; i++)
			{
				achieveRegionDataDbfAsset.Records[i].StripUnusedLocales();
			}
			return achieveRegionDataDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x0600166A RID: 5738 RVA: 0x0007D5E6 File Offset: 0x0007B7E6
	public LoadAchieveRegionDataDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchieveRegionDataDbfAsset));
	}

	// Token: 0x0600166B RID: 5739 RVA: 0x0007D609 File Offset: 0x0007B809
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000E90 RID: 3728
	private AssetBundleRequest assetBundleRequest;
}
