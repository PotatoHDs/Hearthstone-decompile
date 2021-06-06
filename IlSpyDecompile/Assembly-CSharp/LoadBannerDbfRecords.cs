using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadBannerDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<BannerDbfRecord> GetRecords()
	{
		BannerDbfAsset bannerDbfAsset = assetBundleRequest.asset as BannerDbfAsset;
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

	public LoadBannerDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(BannerDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
