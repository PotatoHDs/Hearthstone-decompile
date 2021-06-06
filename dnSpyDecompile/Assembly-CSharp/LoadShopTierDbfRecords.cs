using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000273 RID: 627
public class LoadShopTierDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06002076 RID: 8310 RVA: 0x000A1020 File Offset: 0x0009F220
	public List<ShopTierDbfRecord> GetRecords()
	{
		ShopTierDbfAsset shopTierDbfAsset = this.assetBundleRequest.asset as ShopTierDbfAsset;
		if (shopTierDbfAsset != null)
		{
			for (int i = 0; i < shopTierDbfAsset.Records.Count; i++)
			{
				shopTierDbfAsset.Records[i].StripUnusedLocales();
			}
			return shopTierDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06002077 RID: 8311 RVA: 0x000A1076 File Offset: 0x0009F276
	public LoadShopTierDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ShopTierDbfAsset));
	}

	// Token: 0x06002078 RID: 8312 RVA: 0x000A1099 File Offset: 0x0009F299
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x0400123F RID: 4671
	private AssetBundleRequest assetBundleRequest;
}
