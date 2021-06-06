using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200017F RID: 383
public class LoadBannerDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x060017FB RID: 6139 RVA: 0x00083CD0 File Offset: 0x00081ED0
	public List<BannerDbfRecord> GetRecords()
	{
		BannerDbfAsset bannerDbfAsset = this.assetBundleRequest.asset as BannerDbfAsset;
		if (bannerDbfAsset != null)
		{
			for (int i = 0; i < bannerDbfAsset.Records.Count; i++)
			{
				bannerDbfAsset.Records[i].StripUnusedLocales();
			}
			return bannerDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x060017FC RID: 6140 RVA: 0x00083D26 File Offset: 0x00081F26
	public LoadBannerDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(BannerDbfAsset));
	}

	// Token: 0x060017FD RID: 6141 RVA: 0x00083D49 File Offset: 0x00081F49
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000F24 RID: 3876
	private AssetBundleRequest assetBundleRequest;
}
