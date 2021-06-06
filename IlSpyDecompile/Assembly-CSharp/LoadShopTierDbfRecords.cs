using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadShopTierDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<ShopTierDbfRecord> GetRecords()
	{
		ShopTierDbfAsset shopTierDbfAsset = assetBundleRequest.asset as ShopTierDbfAsset;
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

	public LoadShopTierDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ShopTierDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
