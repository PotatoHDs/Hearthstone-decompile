using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAchieveRegionDataDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AchieveRegionDataDbfRecord> GetRecords()
	{
		AchieveRegionDataDbfAsset achieveRegionDataDbfAsset = assetBundleRequest.asset as AchieveRegionDataDbfAsset;
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

	public LoadAchieveRegionDataDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchieveRegionDataDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
