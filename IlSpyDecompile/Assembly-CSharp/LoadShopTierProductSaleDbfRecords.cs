using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadShopTierProductSaleDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<ShopTierProductSaleDbfRecord> GetRecords()
	{
		ShopTierProductSaleDbfAsset shopTierProductSaleDbfAsset = assetBundleRequest.asset as ShopTierProductSaleDbfAsset;
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

	public LoadShopTierProductSaleDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ShopTierProductSaleDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
