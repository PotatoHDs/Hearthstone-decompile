using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000276 RID: 630
public class LoadShopTierProductSaleDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06002090 RID: 8336 RVA: 0x000A156C File Offset: 0x0009F76C
	public List<ShopTierProductSaleDbfRecord> GetRecords()
	{
		ShopTierProductSaleDbfAsset shopTierProductSaleDbfAsset = this.assetBundleRequest.asset as ShopTierProductSaleDbfAsset;
		if (shopTierProductSaleDbfAsset != null)
		{
			for (int i = 0; i < shopTierProductSaleDbfAsset.Records.Count; i++)
			{
				shopTierProductSaleDbfAsset.Records[i].StripUnusedLocales();
			}
			return shopTierProductSaleDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06002091 RID: 8337 RVA: 0x000A15C2 File Offset: 0x0009F7C2
	public LoadShopTierProductSaleDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ShopTierProductSaleDbfAsset));
	}

	// Token: 0x06002092 RID: 8338 RVA: 0x000A15E5 File Offset: 0x0009F7E5
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001247 RID: 4679
	private AssetBundleRequest assetBundleRequest;
}
