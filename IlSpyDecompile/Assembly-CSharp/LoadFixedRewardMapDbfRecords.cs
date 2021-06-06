using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadFixedRewardMapDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<FixedRewardMapDbfRecord> GetRecords()
	{
		FixedRewardMapDbfAsset fixedRewardMapDbfAsset = assetBundleRequest.asset as FixedRewardMapDbfAsset;
		if (fixedRewardMapDbfAsset != null)
		{
			for (int i = 0; i < fixedRewardMapDbfAsset.Records.Count; i++)
			{
				fixedRewardMapDbfAsset.Records[i].StripUnusedLocales();
			}
			return fixedRewardMapDbfAsset.Records;
		}
		return null;
	}

	public LoadFixedRewardMapDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(FixedRewardMapDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
