using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadFixedRewardActionDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<FixedRewardActionDbfRecord> GetRecords()
	{
		FixedRewardActionDbfAsset fixedRewardActionDbfAsset = assetBundleRequest.asset as FixedRewardActionDbfAsset;
		if (fixedRewardActionDbfAsset != null)
		{
			for (int i = 0; i < fixedRewardActionDbfAsset.Records.Count; i++)
			{
				fixedRewardActionDbfAsset.Records[i].StripUnusedLocales();
			}
			return fixedRewardActionDbfAsset.Records;
		}
		return null;
	}

	public LoadFixedRewardActionDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(FixedRewardActionDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
