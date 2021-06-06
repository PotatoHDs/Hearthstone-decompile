using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadRegionOverridesDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<RegionOverridesDbfRecord> GetRecords()
	{
		RegionOverridesDbfAsset regionOverridesDbfAsset = assetBundleRequest.asset as RegionOverridesDbfAsset;
		if (regionOverridesDbfAsset != null)
		{
			for (int i = 0; i < regionOverridesDbfAsset.Records.Count; i++)
			{
				regionOverridesDbfAsset.Records[i].StripUnusedLocales();
			}
			return regionOverridesDbfAsset.Records;
		}
		return null;
	}

	public LoadRegionOverridesDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(RegionOverridesDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
